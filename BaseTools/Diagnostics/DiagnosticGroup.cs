/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Diagnostics/DiagnosticGroup.cs
 * File: DiagnosticGroup.cs
 * Created: 2026-04-03
 * Modified: 2026-04-03
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Diagnostics
{
    public enum DiagnosticGroup
    {
        FileIntegrity,
        Paths,
        Passwords,
        Database
    }
}
