/*
 * Project: ToolsUI
 * Program: ToolsUI.dll
 * Path: Tools/ToolsUI/Config/FrmPassword.cs
 * File: FrmPassword.cs
 * Created: 2026-04-04
 * Modified: 2026-04-04
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
#region System Libraries
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools.Config;

#endregion
#region Project Libraries
using Tools.Enums;
#endregion

namespace ToolsUI.Config
{
    public partial class FrmPassword : Form
    {
        #region Private Fields
        private PasswordMetadata _initialData;
        #endregion
        #region Constructors/Destructors
        public FrmPassword(PasswordMetadata Initial)
        {
            InitializeComponent();
            InitializeControls();
            _initialData = Initial;
        }
        #endregion
        #region Public Properties
        public PasswordDialogResult PasswordData { get; private set; }
        #endregion
        #region Private Helpers
        private void InitializeControls()
        {
            cboRepresentation.DataSource = Enum.GetValues(typeof(PasswordRepresentation));
            btnCancel.Click += ButtonClicked;
            btnConfirm.Click += ButtonClicked;
            btnCurrent.Click += ButtonClicked;
            btnNew.Click += ButtonClicked;
            btnOk.Click += ButtonClicked;
            cbxVault.CheckedChanged += CheckBox_CheckedChanged;
        }
        private void TogglePassword(TextBox txt, Button btn)
        {
            bool isShowing = txt.PasswordChar == '\0';

            // Toggle the mask
            txt.PasswordChar = isShowing ? '•' : '\0';

            // Update the button label
            btn.Text = isShowing ? "Show" : "Hide";
        }
        #endregion
        #region Private Event Functions
        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox cbx)
            {
                cboRepresentation.Enabled = !cbx.Checked;
            }
        }
        private void ButtonClicked(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is string Tags)
            {
                switch (Tags)
                {
                    case "Cancel":
                        DialogResult = DialogResult.Cancel;
                        Close();
                        break;
                    case "ConfirmShow":
                        TogglePassword(txtConfirm, btn);
                        break;
                    case "CurrentShow":
                        TogglePassword(txtCurrent, btn);
                        break;
                    case "NewShow":
                        TogglePassword(txtNew, btn);
                        break;
                    case "OK":
                        PasswordData = new PasswordDialogResult
                        {
                            Accepted = true,
                            Password = txtNew.Text,
                            Metadata = new PasswordMetadata
                            {
                                StorageLocation = cbxVault.Checked
                                    ? StorageLocation.Vault
                                    : StorageLocation.Database,
                                Representation = cbxVault.Checked
                                    ? PasswordRepresentation.Secret
                                    : (PasswordRepresentation)cboRepresentation.SelectedItem,
                                Value = txtNew.Text
                            }
                        };
                        DialogResult = DialogResult.OK;
                        Close();
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
    }
}
