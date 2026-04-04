/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Containers/ContainerFactory.cs
 * File: ContainerFactory.cs
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
namespace Tools.Containers
{
    public class ContainerFactory
    {
        #region Private Fields
        #endregion Private Fields
        #region Constructors/Destructors
        #endregion Constructors/Destructors
        #region Public Properties
        #endregion Public Properties
        #region Public Methods
        public bool UpdateDictionary<K, V>(Dictionary<K, V> dict, K key, V obj) where K : notnull
        {
            if (obj == null)
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
            catch (Exception ex)
            {
                // Optional: log the exception if you wire in BaseToolsManager logging later
                return false;
            }
        }
        #endregion Public Methods
    }
}
