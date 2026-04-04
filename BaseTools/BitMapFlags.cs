/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/BitMapFlags.cs
 * File: BitMapFlags.cs
 * Created: None
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
#region User Libraries
#endregion 
namespace Tools
{
    public class BitMapFlags
    {
        #region Private Properties
        // ---------------------------------------------------------
        //  Private enum: canonical list of all flag identifiers
        // ---------------------------------------------------------
        public enum FlagNames
        {
            AddExtension = 0,
            AppendFile = 1,
            AppendFlag = 2,
            AppendLog = 3,
            BrowseCompressed = 4,
            CreateFile = 5,
            CreateLog = 6,
            EmptyTrash = 7,
            FileExists = 8,
            FileHeaders = 9,
            FolderExists = 10,
            ListRecycle = 11,
            MultiDotExtensions = 12,
            MultiSelect = 13,
            RestoreFolder = 14,
            ShowNewFolder = 15,
            ShowRecycle = 16
        }
        private BitArray _flags;
        #endregion Private Properties
        #region Constructors/Destructors
        public BitMapFlags()
        {
            int size = Enum.GetNames(typeof(FlagNames)).Length;
            _flags = new BitArray(size, false);
        }
        #endregion
        #region Public Methods
        // ---------------------------------------------------------
        //  Universal flag getter
        // ---------------------------------------------------------
        public bool GetFlag(FlagNames name)
        {
            return _flags.Get((int)name);
        }

        // ---------------------------------------------------------
        //  Universal flag setter
        // ---------------------------------------------------------
        public void SetFlag(FlagNames name, bool value)
        {
            _flags.Set((int)name, value);
        }
        // ---------------------------------------------------------
        public bool GetFlag(string name)
        {
            if (Enum.TryParse(name, out FlagNames flag))
                return GetFlag(flag);

            throw new ArgumentException($"Unknown flag name: {name}");
        }

        public void SetFlag(string name, bool value)
        {
            if (Enum.TryParse(name, out FlagNames flag))
                SetFlag(flag, value);
            else
                throw new ArgumentException($"Unknown flag name: {name}");
        }
        // ---------------------------------------------------------
        //  Group getter: return all flags as (name, value) pairs
        // ---------------------------------------------------------
        public IEnumerable<(FlagNames Name, bool Value)> GetAllFlags()
        {
            foreach (FlagNames name in Enum.GetValues(typeof(FlagNames)))
                yield return (name, GetFlag(name));
        }
        // ---------------------------------------------------------
        //  Group setter: update all flags from (name, value) pairs
        // ---------------------------------------------------------
        public void SetAllFlags(IEnumerable<(FlagNames Name, bool Value)> items)
        {
            foreach (var item in items)
                SetFlag(item.Name, item.Value);
        }
        // ---------------------------------------------------------
        //  UI helper: export all flags as a dictionary
        // ---------------------------------------------------------
        public Dictionary<string, bool> ToDictionary()
        {
            var dict = new Dictionary<string, bool>();

            foreach (FlagNames name in Enum.GetValues(typeof(FlagNames)))
                dict[name.ToString()] = GetFlag(name);

            return dict;
        }
        // ---------------------------------------------------------
        //  UI helper: import flags from a dictionary
        // ---------------------------------------------------------
        public void FromDictionary(Dictionary<string, bool> dict)
        {
            foreach (var kv in dict)
                SetFlag(kv.Key, kv.Value);
        }
        // ---------------------------------------------------------
        //  Return a list of active flag names
        // ---------------------------------------------------------
        public List<string> GetActiveFlagNames()
        {
            var list = new List<string>();

            foreach (FlagNames name in Enum.GetValues(typeof(FlagNames)))
                if (GetFlag(name))
                    list.Add(name.ToString());

            return list;
        }
        // ---------------------------------------------------------
        //  Set flags based on a list of active names
        // ---------------------------------------------------------
        public void SetActiveFlags(IEnumerable<string> activeNames)
        {
            // First clear all flags
            foreach (FlagNames name in Enum.GetValues(typeof(FlagNames)))
                SetFlag(name, false);

            // Then activate the ones provided
            foreach (var name in activeNames)
                SetFlag(name, true);
        }
        // ---------------------------------------------------------
        //  Serialize flags to a hex string (compact INI storage)
        // ---------------------------------------------------------
        public string ToHex()
        {
            byte[] bytes = new byte[(int)Math.Ceiling(_flags.Length / 8.0)];
            _flags.CopyTo(bytes, 0);
            return BitConverter.ToString(bytes).Replace("-", "");
        }
        // ---------------------------------------------------------
        //  Load flags from a hex string
        // ---------------------------------------------------------
        public void FromHex(string hex)
        {
            if (string.IsNullOrWhiteSpace(hex))
                return;

            int byteCount = hex.Length / 2;
            byte[] bytes = new byte[byteCount];

            for (int i = 0; i < byteCount; i++)
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);

            var bits = new BitArray(bytes);

            // Copy into our internal BitArray (truncate or pad as needed)
            for (int i = 0; i < _flags.Length; i++)
                _flags[i] = (i < bits.Length) && bits[i];
        }
        // ---------------------------------------------------------
        //  Serialize flags to a name=value;name=value string
        // ---------------------------------------------------------
        public string ToNameValueString()
        {
            var parts = new List<string>();

            foreach (FlagNames name in Enum.GetValues(typeof(FlagNames)))
                parts.Add($"{name}={(GetFlag(name) ? "true" : "false")}");

            return string.Join(";", parts);
        }
        // ---------------------------------------------------------
        //  Load flags from a name=value;name=value string
        // ---------------------------------------------------------
        public void FromNameValueString(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
                return;

            var parts = data.Split(';', StringSplitOptions.RemoveEmptyEntries);

            foreach (var part in parts)
            {
                var kv = part.Split('=', StringSplitOptions.RemoveEmptyEntries);
                if (kv.Length == 2)
                    SetFlag(kv[0], kv[1].Equals("true", StringComparison.OrdinalIgnoreCase));
            }
        }
        // ---------------------------------------------------------
        //  Create a deep copy of this BitMapFlags instance
        // ---------------------------------------------------------
        public BitMapFlags Clone()
        {
            var clone = new BitMapFlags();

            for (int i = 0; i < _flags.Length; i++)
                clone._flags[i] = _flags[i];

            return clone;
        }
        // ---------------------------------------------------------
        //  Reset all flags to false
        // ---------------------------------------------------------
        public void Reset()
        {
            for (int i = 0; i < _flags.Length; i++)
                _flags[i] = false;
        }
        // ---------------------------------------------------------
        //  Load default flag values (customize later)
        // ---------------------------------------------------------
        public void LoadDefaults()
        {
            // Example defaults — adjust as needed
            SetFlag(FlagNames.ShowNewFolder, true);
            SetFlag(FlagNames.MultiSelect, true);
            // Add more defaults later
        }
        #endregion
    }
}