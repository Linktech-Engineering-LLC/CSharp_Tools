/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Config/Vault.cs
 * File: Vault.cs
 * Version: 1.0.0
 * Created: 2026-04-30
 * Modified: 2026-04-30
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Config
{
    public static class Vault
    {
        private const int CRED_TYPE_GENERIC = 1;
        private const uint CRED_PERSIST_LOCAL_MACHINE = 2;

        // -----------------------------
        // P/Invoke declarations
        // -----------------------------
        [DllImport("advapi32", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool CredReadW(
            string target,
            int type,
            int reservedFlag,
            out IntPtr credentialPtr);

        [DllImport("advapi32", SetLastError = true)]
        private static extern bool CredWriteW(
            ref CREDENTIAL userCredential,
            uint flags);

        [DllImport("advapi32", SetLastError = true)]
        private static extern bool CredDeleteW(
            string target,
            int type,
            int flags);

        [DllImport("advapi32", SetLastError = true)]
        private static extern void CredFree(IntPtr buffer);

        // -----------------------------
        // CREDENTIAL struct
        // -----------------------------
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct CREDENTIAL
        {
            public uint Flags;
            public uint Type;
            public string TargetName;
            public string Comment;
            public System.Runtime.InteropServices.ComTypes.FILETIME LastWritten;
            public uint CredentialBlobSize;
            public IntPtr CredentialBlob;
            public uint Persist;
            public uint AttributeCount;
            public IntPtr Attributes;
            public string TargetAlias;
            public string UserName;
        }

        // -----------------------------
        // Public API
        // -----------------------------

        /// <summary>
        /// Read a password from the Windows Credential Manager.
        /// Returns null if not found.
        /// </summary>
        public static string? Read(string key)
        {
            if (!CredReadW(key, CRED_TYPE_GENERIC, 0, out var credPtr))
                return null;

            try
            {
                var cred = Marshal.PtrToStructure<CREDENTIAL>(credPtr);

                if (cred.CredentialBlob == IntPtr.Zero || cred.CredentialBlobSize == 0)
                    return null;

                return Marshal.PtrToStringUni(cred.CredentialBlob, (int)cred.CredentialBlobSize / 2);
            }
            finally
            {
                CredFree(credPtr);
            }
        }

        /// <summary>
        /// Write a password to the Windows Credential Manager.
        /// Returns true on success.
        /// </summary>
        public static bool Write(string key, string password)
        {
            byte[] secretBytes = Encoding.Unicode.GetBytes(password);

            var credential = new CREDENTIAL
            {
                TargetName = key,
                Type = CRED_TYPE_GENERIC,
                Persist = CRED_PERSIST_LOCAL_MACHINE,
                CredentialBlobSize = (uint)secretBytes.Length,
                UserName = Environment.UserName
            };

            credential.CredentialBlob = Marshal.AllocHGlobal(secretBytes.Length);
            Marshal.Copy(secretBytes, 0, credential.CredentialBlob, secretBytes.Length);

            try
            {
                return CredWriteW(ref credential, 0);
            }
            finally
            {
                Marshal.FreeHGlobal(credential.CredentialBlob);
            }
        }

        /// <summary>
        /// Delete a password from the Windows Credential Manager.
        /// Returns true on success.
        /// </summary>
        public static bool Delete(string key)
        {
            return CredDeleteW(key, CRED_TYPE_GENERIC, 0);
        }
    }
}
