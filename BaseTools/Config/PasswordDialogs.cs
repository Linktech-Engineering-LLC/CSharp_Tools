/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Config/PasswordDialogs.cs
 * File: PasswordDialogs.cs
 * Created: 2026-04-04
 * Modified: 2026-04-04
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
    public class PasswordDialogs
    {
    }
    public class PasswordDialogResult
    {
        public bool Accepted { get; set; }
        public string Password { get; set; }
        public PasswordMetadata Metadata { get; set; }
    }
    public class PasswordMetadata
    {
        public StorageLocation StorageLocation { get; set; }
        public PasswordRepresentation Representation { get; set; }
        public string Value { get; set; } // plaintext, encrypted blob, hash, or vault key
    }

}
