using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NybSys.MTBF.Utility.Enums;
using NybSys.MTBF.Utility.Helper;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Constants;
using IFMS.Facade;



namespace Tiles_Inventory
{
    public partial class frmAddRole : BaseForm
    {
        FacadeManager _MTBFProxy = new FacadeManager();
        public event OnRoleInformationLoadEventHandler OnRoleInformationLoad;
        public delegate void OnRoleInformationLoadEventHandler();
        private int _roleID = 0;

        public frmAddRole()
        {
            InitializeComponent();
        }

        #region "Private Events"

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRoleName.Text))
            {
                MessageBoxHelper.ShowInformation("You need to enter role name.");
                txtRoleName.Focus();
                return;
            }

            if (!IsDuplicateRole())
            {
                if (base.IsUpdating)
                {
                    if (UpdateUserRole() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {

                        MessageBoxHelper.ShowInformation("Role information saved successfully");
                        LoadAllRole();
                        txtRoleName.Clear();
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save role information");
                    }
                }
                else
                {
                    if (InsertUserRole() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Role information saved successfully");
                        LoadAllRole();
                        txtRoleName.Clear();
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save role information");
                    }
                }

                if (OnRoleInformationLoad != null)
                    OnRoleInformationLoad();
            }

        }




        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void grvRoleInfo_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            if (grvRoleInfo.Selected.Rows.Count > 0)
            {
                int roleID = Convert.ToInt32(grvRoleInfo.Selected.Rows[0].Cells[MTBFConstants.DataField.ROLE_ID].Value);

                Role role = _MTBFProxy.GetRoleByID(roleID);
                if (role != null)
                {
                    txtRoleName.Text = role.RoleName;
                    _roleID = role.RoleID;
                    base.IsUpdating = true;
                }
            }
        }

        private void frmAddRole_Load(object sender, EventArgs e)
        {
            LoadAllRole();
        }

        #endregion

        #region "Private Methods"

        /// <summary>
        /// Method to check duplicate role name.
        /// </summary>
        /// <returns></returns>
        private bool IsDuplicateRole()
        {
            List<Role> lstRole = _MTBFProxy.GetAllRole().Cast<Role>().Where(r => r.RoleName.ToLower() == txtRoleName.Text.ToLower()).ToList();

            if (lstRole.Count > 0 && lstRole[0].RoleID != _roleID)
            {
                MessageBoxHelper.ShowInformation("This role is already exists");
                txtRoleName.Focus();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method to insert 
        /// </summary>
        /// <returns></returns>
        private int InsertUserRole()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            Role role = new Role();
            role.RoleName = txtRoleName.Text;
            result = _MTBFProxy.InsertRole(role);
            return result;
        }

        /// <summary>
        /// Method to update role
        /// </summary>
        /// <returns></returns>
        private int UpdateUserRole()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            Role role = _MTBFProxy.GetRoleByID(_roleID);
            role.RoleName = txtRoleName.Text;
            result = _MTBFProxy.UpdateRole(role);
            return result;
        }

        /// <summary>
        /// Method to load all role in grid.
        /// </summary>
        private void LoadAllRole()
        {
            DataTable roleTable = new DataTable();
            roleTable.Columns.Add(MTBFConstants.DataField.ROLE_ID);
            roleTable.Columns.Add(MTBFConstants.GridHeader.ROLE_NAME);
            foreach (Role role in _MTBFProxy.GetAllRole())
            {
                DataRow row = roleTable.NewRow();
                row[MTBFConstants.DataField.ROLE_ID] = role.RoleID;
                row[MTBFConstants.GridHeader.ROLE_NAME] = role.RoleName;
                roleTable.Rows.Add(row);
            }
            grvRoleInfo.DataSource = roleTable;

            grvRoleInfo.DisplayLayout.Bands[0].Columns[MTBFConstants.DataField.ROLE_ID].Hidden = true;
        }

        #endregion

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtRoleName.Clear();
        }

    }
}
