using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Constants;
using IFMS.BLL;
using NybSys.MTBF.Utility.Helper;
using NybSys.MTBF.Utility.Enums;
using Infragistics.Win.UltraWinGrid;

namespace Tiles_Inventory
{
    public partial class frmAddAction : Form
    {
        private IMFS.Common.DTO.Action _action = new IMFS.Common.DTO.Action();
        List<Permission> lstPermission = new List<Permission>();
        public frmAddAction()
        {
            InitializeComponent();
        }

        private void frmAddAction_Load(object sender, EventArgs e)
        {
            LoadPermissionCombo();
            LoadAllAction();
        }

        /// <summary>
        /// Method to load permission information in combo box.
        /// </summary>
        private void LoadPermissionCombo()
        {


            Permission permission = new Permission();
            permission.PermissionID = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            permission.PermissionName = MTBFConstants.DataField.COMBO_DEFAULT_NAME;

            lstPermission = new SecurityManager().GetAllPermission().Cast<Permission>().ToList();
            lstPermission.Insert(0, permission);

            UIHelper.SetComboBoxData(cmbPermissionName, lstPermission, MTBFConstants.DataField.PERMISSION_NAME, MTBFConstants.DataField.PERMISSION_ID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbPermissionName.Value == null || Convert.ToInt32(cmbPermissionName.Value) == MTBFConstants.DataField.COMBO_DEFAULT_ID)
            {
                cmbPermissionName.Focus();
                MessageBoxHelper.ShowInformation("Plese select permission name.");
                return;
            }

            if (string.IsNullOrEmpty(txtButtonName.Text))
            {
                MessageBoxHelper.ShowInformation("Plese enter button name.");
                txtButtonName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtActionName.Text))
            {
                MessageBoxHelper.ShowInformation("Plese enter action name.");
                txtActionName.Focus();
                return;
            }

            SaveAction();
        }

        private void ResetControls()
        {
            txtActionName.Clear();
            txtButtonName.Clear();
            txtButtonName.Focus();
            cmbPermissionName.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            _action = new IMFS.Common.DTO.Action();
        }

        private void SaveAction()
        {
            _action.PermissionID = Convert.ToInt32(cmbPermissionName.Value);
            _action.ButtonName = txtButtonName.Text.Trim();
            _action.ActionName = txtActionName.Text;
            if (new SecurityManager().InsertAction(_action) == (int)MTBFEnums.ReturnResult.SUCCESS)
            {
                MessageBoxHelper.ShowInformation("Saved Successfully");
                ResetControls();
            }
            else
            {
                MessageBoxHelper.ShowInformation("Failed to save information");
            }

        }

        private void LoadAllAction()
        {
            List<IMFS.Common.DTO.Action> lstAction = new List<IMFS.Common.DTO.Action>();
            lstAction = new SecurityManager().GetAllAction();

            foreach (IMFS.Common.DTO.Action action in lstAction)
            {
                Permission permission = lstPermission.Where(p => p.PermissionID == action.PermissionID).FirstOrDefault();
                UltraGridRow row = grvActionInfo.DisplayLayout.Bands[0].AddNew();
                row.Cells["ActionID"].Value = action.ActionID;
                row.Cells["Action Name"].Value = action.ActionName;
                row.Cells["Button Name"].Value = action.ButtonName;
                row.Cells["Permission Name"].Value = (permission != null) ? permission.PermissionName : string.Empty;
            }
        }

        private void grvActionInfo_ClickCell(object sender, ClickCellEventArgs e)
        {
            if (grvActionInfo.Selected.Rows.Count > 0)
            {
                int actionID = Convert.ToInt32(grvActionInfo.Selected.Rows[0].Cells["ActionID"].Value);
                _action = new SecurityManager().GetActionByID(actionID);

                cmbPermissionName.Value = _action.PermissionID;
                txtButtonName.Text = _action.ButtonName;
                txtActionName.Text = _action.ActionName;

            }
        }

    }
}
