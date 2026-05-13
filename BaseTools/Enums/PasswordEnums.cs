/*
 * Linktech Engineering Tools Suite
 * (c) 2026 Leon McClatchey
 * (c) 2026 Linktech Engineering, LLC
 * Licensed under the MIT License.
 */

/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Enums/PasswordEnums.cs
 * File: PasswordEnums.cs
 * Version: 1.0.1
 * Created: 2026-04-04
 * Modified: 2026-05-12
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
    public enum PasswordTarget
    {
        Application,
        Configuration,
        Diagnostics,
        Archiving,
        Historical,
        Database
    }
    public enum PasswordLocation
    {
        Unknown,
        Vault,
        Inline
    }
}
