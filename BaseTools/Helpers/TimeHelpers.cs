/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Helpers/TimeHelpers.cs
 * File: TimeHelpers.cs
 * Created: None
 * Modified: 2026-01-31
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
#region System Libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion
#region Project Libraries
#endregion
namespace Tools.Helpers
{
    public static class TimeHelpers
    {
        #region Public Properties
        public static string LogCurrentTimeStamp => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        #endregion
        #region Public Methods
        #endregion
    }
}
