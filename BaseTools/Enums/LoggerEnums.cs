/*
 * Linktech Engineering Tools Suite
 * (c) 2026 Leon McClatchey
 * (c) 2026 Linktech Engineering, LLC
 * Licensed under the MIT License.
 */

/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Enums/LoggerEnums.cs
 * File: LoggerEnums.cs
 * Version: 1.0.2
 * Created: 2026-04-04
 * Modified: 2026-08-19
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
#region Public Libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion
#region Project Libraries
#endregion
namespace Tools.Enums
{
    public enum SizeUnit
    {
        B,
        K,
        M,
        G,
        T
    }

    public enum RetentionUnit
    {
        Days,
        Weeks,
        Months,
        Years
    }
}
