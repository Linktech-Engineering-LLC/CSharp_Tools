/*
 * Linktech Engineering Tools Suite
 * (c) 2026 Leon McClatchey
 * (c) 2026 Linktech Engineering, LLC
 * Licensed under the MIT License.
 */

/*
 * Project: ToolsUI
 * Program: ToolsUI.dll
 * Path: Tools/ToolsUI/UserControls/UcLoggerEditor.cs
 * File: UcLoggerEditor.cs
 * Version: 1.0.7
 * Created: 2026-06-05
 * Modified: 2026-08-21
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
#region Public Libraries
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion
#region Project Libraries
using Tools.Config;
using Tools.Logging;
using Tools.Enums;
#endregion
namespace ToolsUI.UserControls
{
    public partial class UcLoggerEditor : EditorBase
    {
        #region Private Fields
        private SettingsModel _settings;
        private LoggerConfig _config;
        #endregion
        #region Constructors/Destructors
        public UcLoggerEditor()
        {
            InitializeComponent();
            cboSize.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSize.DataSource = Enum.GetValues(typeof(SizeUnit));
            cboRetention.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRetention.DataSource = Enum.GetValues(typeof(RetentionUnit));
            Initialize();
        }
        #endregion
        #region Private Helper Methods
        private void EditPath(PathLocation loc)
        {
            var editor = new UcPathEditor
            {
                CurrentSettings = CurrentSettings
            };

            editor.Initialize(loc, CurrentSettings);

            editor.RefreshRequested += (s, e) =>
            {
                txtLogDirectory.Text = CurrentSettings.Logger.LogDirectory?.PathValue ?? "";
                txtRotateDirectory.Text = CurrentSettings.Logger.RotatePath?.PathValue ?? "";
                txtArchiveDirectory.Text = CurrentSettings.Logger.ArchivePath?.PathValue ?? "";
                RequestRefresh();
            };

            using (var frm = new Form())
            {
                frm.Text = "Edit Path";
                frm.Width = 600;
                frm.Height = 400;
                editor.Dock = DockStyle.Fill;
                frm.Controls.Add(editor);
                frm.StartPosition = FormStartPosition.CenterParent;   // ⭐ THIS LINE
                frm.ShowDialog();
            }
        }
        private void Initialize()
        {
            txtLogDirectory.ReadOnly = true;
            txtRotateDirectory.ReadOnly = true;
            txtArchiveDirectory.ReadOnly = true;

            txtLogDirectory.Click += (s, e) => EditPath(PathLocation.LogPath);
            txtRotateDirectory.Click += (s, e) => EditPath(PathLocation.RotatePath);
            txtArchiveDirectory.Click += (s, e) => EditPath(PathLocation.ArchivePath);
        }
        private void InitializeOptions()
        {
            clbOptions.Items.Clear();

            foreach (LoggerOption opt in Enum.GetValues(typeof(LoggerOption)))
            {
                clbOptions.Items.Add(opt);
            }
        }
        #endregion
        #region Public Properties/Methods
        public SettingsModel CurrentSettings { get; set; }
        public void ApplyChanges()
        {
            _config.MinimumLevel = (LogLevel)cboMinimumLevel.SelectedItem;
            _config.RotationType = (RotationType)cboRotationType.SelectedItem;
            _config.ArchiveType = (ArchiveType)cboArchiveType.SelectedItem;

            if (_config.LogDirectory != null)
                _config.LogDirectory.PathValue = txtLogDirectory.Text;

            if (_config.RotatePath != null)
                _config.RotatePath.PathValue = txtRotateDirectory.Text;

            if (_config.ArchivePath != null)
                _config.ArchivePath.PathValue = txtArchiveDirectory.Text;

            long sizeBytes = ParseLogSize(
                (long)numMaxLogSize.Value,
                (SizeUnit)cboSize.SelectedItem
            );

            int retentionDays = ParseRetention(
                (int)numRetention.Value,
                (RetentionUnit)cboRetention.SelectedItem
            );

            _config.MaxLogSizeBytes = sizeBytes;
            _config.RetentionDays = retentionDays;

            _config.Options.Clear();
            foreach (var item in clbOptions.CheckedItems)
                _config.Options.Add((LoggerOption)item);
        }
        public static (long Value, SizeUnit Unit) FormatLogSize(long bytes)
        {
            if (bytes % (1024L * 1024L * 1024L * 1024L) == 0)
                return (bytes / (1024L * 1024L * 1024L * 1024L), SizeUnit.T);

            if (bytes % (1024L * 1024L * 1024L) == 0)
                return (bytes / (1024L * 1024L * 1024L), SizeUnit.G);

            if (bytes % (1024L * 1024L) == 0)
                return (bytes / (1024L * 1024L), SizeUnit.M);

            if (bytes % 1024L == 0)
                return (bytes / 1024L, SizeUnit.K);

            return (bytes, SizeUnit.B);
        }
        public static (int Value, RetentionUnit Unit) FormatRetention(int days)
        {
            if (days % 365 == 0)
                return (days / 365, RetentionUnit.Years);

            if (days % 30 == 0)
                return (days / 30, RetentionUnit.Months);

            if (days % 7 == 0)
                return (days / 7, RetentionUnit.Weeks);

            return (days, RetentionUnit.Days);
        }

        public void Initialize(LoggerConfig settings)
        {
            _config = settings;

            // Populate dropdowns
            cboMinimumLevel.DataSource = Enum.GetValues(typeof(LogLevel));
            cboRotationType.DataSource = Enum.GetValues(typeof(RotationType));
            cboArchiveType.DataSource = Enum.GetValues(typeof(ArchiveType));
            InitializeOptions();

            // Set selected values
            cboMinimumLevel.SelectedItem = _config.MinimumLevel;
            cboRotationType.SelectedItem = _config.RotationType;
            cboArchiveType.SelectedItem = _config.ArchiveType;

            // Populate paths
            txtLogDirectory.Text = _config.LogDirectory?.PathValue ?? "";
            txtRotateDirectory.Text = _config.RotatePath?.PathValue ?? "";
            txtArchiveDirectory.Text = _config.ArchivePath?.PathValue ?? "";

            // Populate numeric fields
            var sizeInfo = FormatLogSize(_config.MaxLogSizeBytes);
            numMaxLogSize.Value = sizeInfo.Value;
            cboSize.SelectedItem = sizeInfo.Unit;

            var retentionInfo = FormatRetention(_config.RetentionDays);
            numRetention.Value = retentionInfo.Value;
            cboRetention.SelectedItem = retentionInfo.Unit;

            // Populate options
            for (int i = 0; i < clbOptions.Items.Count; i++)
            {
                var opt = (LoggerOption)clbOptions.Items[i];
                clbOptions.SetItemChecked(i, _config.Options.Contains(opt));
            }
        }
        public static long ParseLogSize(long value, SizeUnit unit)
        {
            return unit switch
            {
                SizeUnit.B => value,
                SizeUnit.K => value * 1024L,
                SizeUnit.M => value * 1024L * 1024L,
                SizeUnit.G => value * 1024L * 1024L * 1024L,
                SizeUnit.T => value * 1024L * 1024L * 1024L * 1024L,
                _ => throw new ArgumentOutOfRangeException(nameof(unit))
            };
        }
        public static int ParseRetention(int value, RetentionUnit unit)
        {
            return unit switch
            {
                RetentionUnit.Days => value,
                RetentionUnit.Weeks => value * 7,
                RetentionUnit.Months => value * 30,   // deterministic, avoids calendar complexity
                RetentionUnit.Years => value * 365,
                _ => throw new ArgumentOutOfRangeException(nameof(unit))
            };
        }

        #endregion
        #region Event Handlers
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ApplyChanges();
            RequestRefresh();
        }
        #endregion
    }
}
