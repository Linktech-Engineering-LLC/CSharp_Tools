/*
 * Linktech Engineering Tools Suite
 * (c) 2026 Leon McClatchey
 * (c) 2026 Linktech Engineering, LLC
 * Licensed under the MIT License.
 */

/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Config/PasswordDialogs.cs
 * File: PasswordDialogs.cs
 * Version: 1.0.1
 * Created: 2026-04-04
 * Modified: 2026-05-19
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using Tools.Enums;

namespace Tools.Config
{
    public class PasswordDialogResult
    {
        public bool Accepted { get; set; }
        public string Password { get; set; }
        public PasswordMetadata Metadata { get; set; }
    }
    public class PasswordMetadata
    {
        public string Key { get; set; }
        public PasswordLocation Location { get; set; }
        public PasswordRepresentation Representation { get; set; }

        // Stored value:
        // - Plaintext: actual password
        // - Hashed: hash string
        // - Encrypted: encrypted blob
        // - Vault: vault key name
        public string Password { get; set; }

        // Only used when Representation == Encrypted
        // This is the vault key name that stores the encryption key
        public string EncryptionKeyName { get; set; }
    }

}
