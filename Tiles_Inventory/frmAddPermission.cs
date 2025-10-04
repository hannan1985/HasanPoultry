using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IMFS.Common.DTO;
using IFMS.BLL;
using NybSys.MTBF.Utility.Enums;
using NybSys.MTBF.Utility.Helper;
using Infragistics.Win.UltraWinGrid;

namespace Tiles_Inventory
{
    public partial class frmAddPermission : Form
    {
        private Permission _permission = new Permission();
        public frmAddPermission()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SavePermission();
        }

        private void SavePermission()
        {
            _permission.FormName = txtFormName.Text.Trim();
            _permission.PermissionName = txtPermissionName.Text;
            _permission.ModuleID = 1;
            if (new SecurityManager().InsertPermission(_permission) == (int)MTBFEnums.ReturnResult.SUCCESS)
            {
                MessageBoxHelper.ShowInformation("Saved successfully");
                ResetAllControls();
            }
            else
            {
                MessageBoxHelper.ShowInformation("Failed to save information");
            }
        }

        private void ResetAllControls()
        {
            txtFormName.Clear();
            txtPermissionName.Clear();
            _permission = new Permission();
        }

        private void grvPermission_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            if (grvPermission.Selected.Rows.Count > 0)
            {
                int permissionID = Convert.ToInt32(grvPermission.Selected.Rows[0].Cells["PermissionID"].Value);
                _permission = new SecurityManager().GetPermissionByID(permissionID);
                txtFormName.Text = _permission.FormName;
                txtPermissionName.Text = _permission.PermissionName;

            }
        }

        private void frmAddPermission_Load(object sender, EventArgs e)
        {
            List<Permission> lstPermission = new List<Permission>();
            lstPermission = new SecurityManager().GetAllPermission();

            foreach (Permission permission in lstPermission)
            {
                UltraGridRow row = grvPermission.DisplayLayout.Bands[0].AddNew();
                row.Cells["PermissionID"].Value = permission.PermissionID;
                row.Cells["Form Name"].Value = permission.FormName;
                row.Cells["Permission Name"].Value = permission.PermissionName;
            }
        }

    }
}
