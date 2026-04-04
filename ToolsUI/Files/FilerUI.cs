/*
 * Project: ToolsUI
 * Program: ToolsUI.dll
 * Path: Tools/ToolsUI/Files/FilerUI.cs
 * File: FilerUI.cs
 * Created: 2026-01-07
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
using System.Windows.Forms;
#endregion
#region Project Libraries
using Tools;
using Tools.Files;
using Tools.Logging;
#endregion
namespace ToolsUI.Files
{
    public class FilerUI
    {
        #region Private Variables
        private readonly BitMapFlags flags;
        private readonly Logger lgr;
        private readonly FilerUIConfig cfg;
        #endregion
        #region Constructors/Destructors
        public FilerUI(Logger logger, string appname)
        {
            lgr = logger;
            flags = new BitMapFlags();
            cfg = new FilerUIConfig
            {
                ProgramName = appname
            };
        }
        #endregion
        #region Public Variables
        #endregion
        #region Private Helper Methods
        private static string ExtractDefaultExt(string filter)
        {
            if (string.IsNullOrEmpty(filter)) return string.Empty;

            // Split on '|' and take the second part (the mask list)
            string[] parts = filter.Split('|');
            if (parts.Length < 2) return string.Empty;

            string masks = parts[1].Trim(); // e.g. "*.exe;*.com"

            // Take the first mask and strip "*."
            string firstMask = masks.Split(';')[0].Trim();
            return Path.GetExtension(firstMask) ?? string.Empty;
        }
        #endregion
        #region Public Thin Wrappers
        public string SelectCsvFile(string title) => SelectFile(title, Filer.CSV_FILTER, cfg.DataPath);
        public string SelectLogFile(string title)
        {
            string logDir = lgr.Config.LogDirectory;

            return SelectFile(title, Filer.LOG_FILTER, logDir);
        }
        public string SelectTextFile(string title) => SelectFile(title, Filer.TEXT_FILTER, cfg.DataPath);
        #endregion
        #region UI Methods (Dialogs, Forms)
        public string SaveFile(string title)
        {
            using var sd = new SaveFileDialog
            {
                InitialDirectory = string.IsNullOrEmpty(cfg.DataPath) ? @"C:\" : cfg.DataPath,
                Filter = string.IsNullOrEmpty(cfg.Filter) ? Filer.CSV_FILTER : cfg.Filter,
                Title = title,
                DefaultExt = ExtractDefaultExt(cfg.Filter) // ensures extension is applied
            };

            return sd.ShowDialog() == DialogResult.OK ? sd.FileName : string.Empty;
        }
        /// <summary>
        /// Selects a directory for group file manipulations
        /// </summary>
        /// <param name="title">A description of what the Folder Browser is looking for</param>
        /// <returns>The name/path of the target folder</returns>
        private string SelectFile(string title, string filter, string initialDir)
        {
            using var dlg = new OpenFileDialog
            {
                Title = title,
                Filter = filter,
                InitialDirectory = initialDir,
                Multiselect = cfg.MultiSelect,
                RestoreDirectory = cfg.RestoreDirectory,
                SupportMultiDottedExtensions = cfg.AllowMultiDotExtensions
            };

            return dlg.ShowDialog() == DialogResult.OK ? dlg.FileName : string.Empty;
        }
        public string SelectFolder(string title)
        {
            using var fd = new FolderBrowserDialog
            {
                Description = title,
                SelectedPath = string.IsNullOrEmpty(cfg.DataPath)
                    ? Environment.CurrentDirectory
                    : cfg.DataPath
            };

            return fd.ShowDialog() == DialogResult.OK
                ? fd.SelectedPath
                : string.Empty;
        }
        public string[] SelectMultipleFiles(string title)
        {
            using var fd = new OpenFileDialog
            {
                Title = title,
                InitialDirectory = string.IsNullOrEmpty(cfg.DataPath)
                    ? @"C:\"
                    : cfg.DataPath,

                Filter = string.IsNullOrEmpty(cfg.Filter)
                    ? Filer.CSV_FILTER
                    : cfg.Filter,

                Multiselect = true,
                RestoreDirectory = cfg.RestoreDirectory,
                SupportMultiDottedExtensions = cfg.AllowMultiDotExtensions
            };

            return fd.ShowDialog() == DialogResult.OK
                ? fd.FileNames
                : Array.Empty<string>();
        }
        #endregion
        #region Error Handlers
        public static void DisplayError(string title, string msg)
            => MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        public static void DisplayError(string title, string msg, MessageBoxIcon icn)
            => MessageBox.Show(msg, title, MessageBoxButtons.OK, icn);
        protected void HaltOnError(Exception err)
        {
            string title = err.GetType().Name;
            string msg = err.Message;

            if (err is FileNotFoundException)
                msg += "\nPlease check the path for the missing file\nand relaunch this application once resolved!";
            else if (err is IOException)
                msg += "\nPlease close the file in any other application\nand re-launch this application!";
            else if (err is OutOfMemoryException)
                msg += "\nPlease close any unneeded applications before re-launching!";
            else if (err is ObjectDisposedException)
                msg += "\nInternal error. Please contact the application author.";
            else if (err is ArgumentNullException)
                msg += "\nUnable to execute operation. Please correct the input.";

            // Log structured exception entry
            lgr.Error(nameof(FilerUI), lgr.FormatExceptionEntry(msg, err));

            // Show dialog
            DisplayError(title, msg);

            // Close all forms and exit
            foreach (Form f in Application.OpenForms)
                f.Close();

            Application.Exit();
        }

        #endregion
    }
}
