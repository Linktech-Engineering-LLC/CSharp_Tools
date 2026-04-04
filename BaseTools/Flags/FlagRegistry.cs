/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Flags/FlagRegistry.cs
 * File: FlagRegistry.cs
 * Created: 2025-12-27
 * Modified: 2026-01-31
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */








#region System Libraries
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion
#region Project Libraries
using Tools.Utilities;
#endregion
namespace Tools.Flags
{
    public class FlagRegistry
    {
        #region Private Fields
        protected readonly Dictionary<string, object> Config;
        #endregion Private Fields
        #region Constructors/Destructors
        public FlagRegistry(Dictionary<string, object> config)
        {
            Config = config;
        }

        #endregion Constructors/Destructors
        #region Public Properties
        public virtual BitArray Flags
        {
            get => Config.ContainsKey("Flags")
                ? (BitArray)Config["Flags"]
                : new BitArray(11, false);

            set => DictionaryTools.Update(Config, "Flags", value);
        }

        #endregion Public Properties
        #region Public Methods
        #endregion Public Methods
    }
}
