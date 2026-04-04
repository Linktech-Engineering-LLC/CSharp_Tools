/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/ToolBox.cs
 * File: ToolBox.cs
 * Created: 2025-12-27
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
#region Tools Libraries
using Tools.Containers;
using Tools.Converters;
using Tools.Defaults;
using Tools.Files;
using Tools.Flags;
using Tools.Logging;
#endregion

namespace Tools
{
    public static class ToolBox
    {
        #region Private Fields
        private static readonly Dictionary<string, object> _config = [];
        #endregion Private Fields
        #region Constructors/Destructors
        #endregion Constructors/Destructors
        #region Public Properties
        public static Dictionary<string, object> Config => _config;
        public static IReadOnlyDictionary<string, object> All => _config;
        public static T GetConfig<T>(string key, T defaultValue = default!)
        {
            return TryGetConfig(key, out T value) ? value : defaultValue;
        }
        public static void SetConfig(string key, object value) => _config[key] = value;
        #endregion Public Properties
        #region Public Methods
        public static void Initialize()
        {
            // Populate the dictionary programmatically
            _config["TypeConverters"] = new TypeConverterCollection();
            _config["Containers"] = new ContainerFactory();
            _config["Defaults"] = new DefaultSettings();
            _config["Flags"] = new FlagRegistry(_config);
            // etc...
        }

        public static T Get<T>(string key)
        {
            return (T)_config[key];
        }
        public static bool TryGetConfig<T>(string key, out T value)
        {
            if (_config.TryGetValue(key, out var obj) && obj is T cast)
            {
                value = cast;
                return true;
            }
            value = default!;
            return false;
        }

        #endregion Public Methods
    }
    public static class AppServices
    {
        public static Logger Logger { get; set; }
        public static Filer Filer { get; set; }
    }
}
