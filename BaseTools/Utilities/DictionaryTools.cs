/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Utilities/DictionaryTools.cs
 * File: DictionaryTools.cs
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
#region BaseTools Libraries
#endregion
namespace Tools.Utilities
{
    public static class DictionaryTools
    {
        #region Private Fields
        #endregion Private Fields
        #region Constructors/Destructors
        #endregion Constructors/Destructors
        #region Public Properties
        #endregion Public Properties
        #region Public Methods
        /// <summary>
        /// Safely retrieves a typed value from a Dictionary<string, object>.
        /// If the key is missing or cannot be cast, persists and returns the provided defaultValue.
        /// Guarantees a non-null return for reference types.
        /// </summary>
        public static T? Get<T>(Dictionary<string, object> dict, string key, T? defaultValue)
            where T : class
        {
            if (dict == null)
                return defaultValue;

            if (dict.TryGetValue(key, out var obj) && obj is T cast)
                return cast;

            // Persist the default into the dictionary so it’s not lost
            dict[key] = defaultValue;
            return defaultValue;
        }
        public static bool Update<K, V>(Dictionary<K, V> dict, K key, V obj)
            where K : notnull
        {
            if (dict == null || obj == null)
                return false;

            try
            {
                if (dict.ContainsKey(key))
                {
                    var str = obj.ToString();
                    if (string.IsNullOrEmpty(str))
                        dict.Remove(key);
                    else
                        dict[key] = obj;
                }
                else
                {
                    dict.Add(key, obj);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Overload for Dictionary<string, object>
        public static bool Update(Dictionary<string, object> dict, string key, object obj)
        {
            if (dict == null || obj == null)
                return false;

            try
            {
                if (dict.TryGetValue(key, out _))
                {
                    var str = obj.ToString();
                    if (string.IsNullOrEmpty(str))
                        dict.Remove(key);
                    else
                        dict[key] = obj;
                }
                else
                {
                    dict.Add(key, obj);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
