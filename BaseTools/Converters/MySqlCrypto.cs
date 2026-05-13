/*
 * Linktech Engineering Tools Suite
 * (c) 2026 Leon McClatchey
 * (c) 2026 Linktech Engineering, LLC
 * Licensed under the MIT License.
 */

/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Converters/MySqlCrypto.cs
 * File: MySqlCrypto.cs
 * Version: 1.0.1
 * Created: 2026-05-13
 * Modified: 2026-05-13
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
#region System Libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
#endregion
#region Project Libraries
#endregion
namespace Tools.Converters
{
    public static class MySqlCrypto
    {
        private static byte[] MakeKey(string key)
        {
            // key is Base64 from GenerateKeyString()
            byte[] raw = Convert.FromBase64String(key);

            if (raw.Length != 32)
                throw new InvalidOperationException("Encryption key must be 32 bytes (256-bit).");

            return raw;
        }

        public static string Encrypt(string plaintext, string key)
        {
            byte[] keyBytes = MakeKey(key);

            using var aes = Aes.Create();
            aes.Key = keyBytes;
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;

            using var encryptor = aes.CreateEncryptor();
            byte[] input = Encoding.UTF8.GetBytes(plaintext);
            byte[] encrypted = encryptor.TransformFinalBlock(input, 0, input.Length);

            return Convert.ToBase64String(encrypted);
        }

        public static string Decrypt(string encryptedBase64, string key)
        {
            byte[] keyBytes = MakeKey(key);

            using var aes = Aes.Create();
            aes.Key = keyBytes;
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;

            using var decryptor = aes.CreateDecryptor();
            byte[] encrypted = Convert.FromBase64String(encryptedBase64);
            byte[] decrypted = decryptor.TransformFinalBlock(encrypted, 0, encrypted.Length);

            return Encoding.UTF8.GetString(decrypted);
        }
    }
}
