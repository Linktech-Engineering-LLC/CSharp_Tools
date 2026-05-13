/*
 * Linktech Engineering Tools Suite
 * (c) 2026 Leon McClatchey
 * (c) 2026 Linktech Engineering, LLC
 * Licensed under the MIT License.
 */

/*
 * Project: ToolsUI
 * Program: ToolsUI.dll
 * Path: Tools/ToolsUI/Helpers/ComboBoxHelper.cs
 * File: ComboBoxHelper.cs
 * Version: 1.0.1
 * Created: 2026-05-13
 * Modified: 2026-05-13
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsUI.Helpers
{
    public static class ComboBoxHelper
    {
        public static void BindEnum<TEnum>(ComboBox cbo) where TEnum : Enum
        {
            cbo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbo.DataSource = Enum.GetValues(typeof(TEnum));
        }
    }
}
