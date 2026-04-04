/*
 * Project: ToolsUI
 * Program: ToolsUI.dll
 * Path: Tools/ToolsUI/Helpers/VaultManager.cs
 * File: VaultManager.cs
 * Created: 2026-03-31
 * Modified: 2026-04-01
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */

/*
 * Project: ToolsUI
 * Program: ToolsUI.dll
 * Path: Tools/ToolsUI/Helpers/VaultManager.cs
 * File: VaultManager.cs
 * Created: 2026-04-01
 * Modified: 2026-04-01
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace ToolsUI.Helpers
{
    public static class VaultManager
    {
        private const int CRED_TYPE_GENERIC = 1;

        [DllImport("advapi32", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool CredReadW(
            string target,
            int type,
            int reservedFlag,
            out IntPtr credentialPtr);

        [DllImport("advapi32", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool CredReadW(
            string target,
            uint type,
            uint flags,
            out IntPtr credentialPtr);

        [DllImport("advapi32", SetLastError = true)]
        private static extern void CredFree(IntPtr buffer);
        [DllImport("advapi32", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool CredWriteW(
            ref CREDENTIAL credential,
            uint flags);

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

        private const uint CRED_PERSIST_LOCAL_MACHINE = 2;
        // -------------------------
        // Write a secret
        // -------------------------
        public static void WriteSecret(string key, string secret)
        {
            // Parse key: must be "Product.PasswordType"
            var parts = key.Split('.', 2);

            if (parts.Length != 2)
                return; // or throw, depending on your philosophy

            string targetName = key;
            string userName = parts[1];

            byte[] secretBytes = Encoding.Unicode.GetBytes(secret);

            var credential = new CREDENTIAL
            {
                TargetName = targetName,
                UserName = userName,
                Type = CRED_TYPE_GENERIC,
                Persist = CRED_PERSIST_LOCAL_MACHINE,
                CredentialBlobSize = (uint)secretBytes.Length,
                CredentialBlob = Marshal.AllocHGlobal(secretBytes.Length)
            };

            Marshal.Copy(secretBytes, 0, credential.CredentialBlob, secretBytes.Length);

            try
            {
                if (!CredWriteW(ref credential, 0))
                    throw new Exception("CredWriteW failed.");
            }
            finally
            {
                Marshal.FreeHGlobal(credential.CredentialBlob);
            }
        }

        // -------------------------
        // Read a secret
        // -------------------------
        public static string? ReadSecret(string key)
        {
            var parts = key.Split('.', 2);
            if (parts.Length != 2)
                return null;

            // We ignore parts[0] and parts[1] here because CredReadW
            // uses the FULL key as the lookup target.
            // (TargetName/UserName are only metadata for display.)

            if (!CredReadW(key, CRED_TYPE_GENERIC, 0, out var credPtr))
                return null;

            try
            {
                var cred = Marshal.PtrToStructure<CREDENTIAL>(credPtr);

                if (cred.CredentialBlob == IntPtr.Zero || cred.CredentialBlobSize == 0)
                    return null;

                int charCount = (int)cred.CredentialBlobSize / 2;
                return Marshal.PtrToStringUni(cred.CredentialBlob, charCount);
            }
            finally
            {
                CredFree(credPtr);
            }
        }

        // -------------------------
        // Delete a secret
        // -------------------------
        public static void DeleteSecret(string key)
        {
            CredDelete(key, 1, 0);
        }

        // -------------------------
        // Check if a secret exists
        // -------------------------
        public static bool Exists(string key)
        {
            return CredRead(key, 1, 0, out _);
        }

        // -------------------------
        // Native structs
        // -------------------------
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct Credential
        {
            public int Flags;
            public int Type;
            public string TargetName;
            public string Comment;
            public long LastWritten;
            public int CredentialBlobSize;
            public IntPtr CredentialBlob;
            public int Persist;
            public int AttributeCount;
            public IntPtr Attributes;
            public string TargetAlias;
            public string UserName;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct NativeCredential
        {
            public int Flags;
            public int Type;
            public string TargetName;
            public string Comment;
            public long LastWritten;
            public int CredentialBlobSize;
            public string CredentialBlob;
            public int Persist;
            public int AttributeCount;
            public IntPtr Attributes;
            public string TargetAlias;
            public string UserName;
        }

        // -------------------------
        // P/Invoke
        // -------------------------
        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool CredWrite(ref NativeCredential userCredential, uint flags);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool CredRead(string target, int type, int reservedFlag, out IntPtr credentialPtr);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool CredDelete(string target, int type, int flags);
    }
}
