/*
 * Project: ToolsUI
 * Program: ToolsUI.dll
 * Path: Tools/ToolsUI/Helpers/EncryptionHelper.cs
 * File: EncryptionHelper.cs
 * Created: 2026-04-01
 * Modified: 2026-04-02
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
using System;
using System.Security.Cryptography;
using System.Text;

namespace ToolsUI.Helpers
{
    public static class EncryptionHelper
    {
        public static string Encrypt(string plain)
        {
            if (string.IsNullOrEmpty(plain))
                return "";

            byte[] data = Encoding.UTF8.GetBytes(plain);
            byte[] encrypted = ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encrypted);
        }

        public static string Decrypt(string encrypted)
        {
            if (string.IsNullOrEmpty(encrypted))
                return "";

            byte[] data = Convert.FromBase64String(encrypted);
            byte[] decrypted = ProtectedData.Unprotect(data, null, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(decrypted);
        }
    }
}
