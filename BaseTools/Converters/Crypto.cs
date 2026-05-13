/*
 * Linktech Engineering Tools Suite
 * (c) 2026 Leon McClatchey
 * (c) 2026 Linktech Engineering, LLC
 * Licensed under the MIT License.
 */

/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Converters/Crypto.cs
 * File: Crypto.cs
 * Version: 1.0.1
 * Created: 2026-05-13
 * Modified: 2026-05-13
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
#region Sytem Libraries
using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion
#region Project Libraries
#endregion
namespace Tools.Converters
{

    public static class Crypto
    {
        public static string Decrypt(string encryptedBase64, byte[] key, byte[] iv)
        {
            byte[] encrypted = Convert.FromBase64String(encryptedBase64);

            byte[] decrypted = AesCtrTransform(encrypted, key, iv);

            // PKCS7 unpad
            int pad = decrypted[^1];
            if (pad > 0 && pad <= 16)
                decrypted = decrypted[..^pad];

            return Encoding.UTF8.GetString(decrypted);
        }

        public static byte[] AesCtrTransform(byte[] input, byte[] key, byte[] iv)
        {
            using var aes = Aes.Create();
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.None;
            aes.Key = key;

            byte[] counter = (byte[])iv.Clone();
            byte[] buffer = new byte[16];
            byte[] output = new byte[input.Length];

            int offset = 0;

            using ICryptoTransform encryptor = aes.CreateEncryptor();

            while (offset < input.Length)
            {
                encryptor.TransformBlock(counter, 0, 16, buffer, 0);

                int blockSize = Math.Min(16, input.Length - offset);

                for (int i = 0; i < blockSize; i++)
                    output[offset + i] = (byte)(input[offset + i] ^ buffer[i]);

                IncrementCounter(counter);
                offset += blockSize;
            }

            return output;
        }

        private static void IncrementCounter(byte[] counter)
        {
            for (int i = 15; i >= 0; i--)
            {
                if (++counter[i] != 0)
                    break;
            }
        }
        public static byte[] DeriveKey(string password, byte[] salt, int iterations, int keyLen)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            return pbkdf2.GetBytes(keyLen);
        }
        public static string GenerateKeyString()
        {
            // 32 bytes = 256-bit AES key
            byte[] keyBytes = new byte[32];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(keyBytes);
            }

            // Base64 is safe for vault storage and config display
            return Convert.ToBase64String(keyBytes);
        }
        public static string HashPassword(string plaintext)
        {
            using var sha = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(plaintext);
            byte[] hash = sha.ComputeHash(bytes);

            // Convert to hex string
            var sb = new StringBuilder(hash.Length * 2);
            foreach (byte b in hash)
                sb.AppendFormat("{0:x2}", b);

            return sb.ToString();
        }
    }
}
