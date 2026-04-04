/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Enums/PasswordEnums.cs
 * File: PasswordEnums.cs
 * Created: 2026-04-04
 * Modified: 2026-04-04
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Enums
{
    public enum PasswordRepresentation
    {
        Unknown,
        Plaintext,
        Encrypted,
        Hashed,
        Secret
    }
    public enum PasswordStyles
    {
        Vault = 0,
        Encrypted = 1,
        PlainText = 2,
        Prompt = 3
    }
    public enum StorageLocation
    {
        Unknown,
        Vault,
        Database
    }

}
