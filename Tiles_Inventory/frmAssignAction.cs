using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NybSys.MTBF.Utility.Helper;
using NybSys.MTBF.Utility.Enums;
using NybSys.MTBF.Utility.Constants;
using IMFS.Common.DTO;
using IFMS.Facade;



namespace Tiles_Inventory
{
    public partial class frmAssignAction : BaseForm
    {
      FacadeManager _MTBFProxy = new FacadeManager();
        private int _roleActionMappingID = 0;

        public frmAssignAction()
        {
            InitializeComponent();
        }

        #region "Private Events"

        private void frmAssignAction_Load(object sender, EventArgs e)
        {
            LoadRoleInformation();
            LoadAllRoleActionMapping();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbRoleName_ValueChanged(object sender, EventArgs e)
        {
            if (cmbRoleName.Value != null)
            {
                LoadPermissionInformation(Convert.ToInt32(cmbRoleName.Value));
            }
        }

        private void cmbPermissionName_ValueChanged(object sender, EventArgs e)
        {
            if (cmbPermissionName.Value != null)
            {
                LoadActionInformation(Convert.ToInt32(cmbPermissionName.Value));
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidFormData())
            {
                if (base.IsUpdating)
                {
                    if (UpdateRoleActionMapping() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Mapping information saved successfully");
                        LoadAllRoleActionMapping();
                        base.IsUpdating = false;
                        _roleActionMappingID = 0;
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save mapping information");
                    }
                }
                else
                {
                    if (InsertRoleActionMapping() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Mapping information saved successfully");
                        LoadAllRoleActionMapping();
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save mapping information");
                    }
                }
            }
        }

        private void grvActionInfo_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            if (grvActionInfo.Selected.Rows.Count > 0)
            {
                _roleActionMappingID = Convert.ToInt32(grvActionInfo.Selected.Rows[0].Cells[MTBFConstants.DataField.ROLE_ACTION_MAP_ID].Value);

                RoleActionMapping roleActionMap = _MTBFProxy.GetRoleActionMappByID(_roleActionMappingID);

                if (roleActionMap != null)
                {
                    cmbRoleName.Value = roleActionMap.RoleID;
                    cmbPermissionName.Value = roleActionMap.PermissionID;
                    cmbActionName.Value = roleActionMap.ActionID;
                    _roleActionMappingID = roleActionMap.RoleActionMappingID;
                    base.IsUpdating = true;
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadRoleInformation();
            LoadAllRoleActionMapping();
        }

        #endregion

        #region "Private Methods"

        /// <summary>
        /// Method to insert role action mapping information.
        /// </summary>
        /// <returns></returns>
        private int InsertRoleActionMapping()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            RoleActionMapping roleActionMapping = new RoleActionMapping();

            roleActionMapping.RoleID = Convert.ToInt32(cmbRoleName.Value);
            roleActionMapping.PermissionID = Convert.ToInt32(cmbPermissionName.Value);
            roleActionMapping.ActionID = Convert.ToInt32(cmbActionName.Value);
            result = _MTBFProxy.InsertRoleActionMapping(roleActionMapping);

            return result;
        }

        /// <summary>
        /// Method to update role action mapping informaiton.
        /// </summary>
        /// <returns></returns>
        private int UpdateRoleActionMapping()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            RoleActionMapping roleActionMapping = _MTBFProxy.GetRoleActionMappByID(_roleActionMappingID);

            roleActionMapping.RoleID = Convert.ToInt32(cmbRoleName.Value);
            roleActionMapping.PermissionID = Convert.ToInt32(cmbPermissionName.Value);
            roleActionMapping.ActionID = Convert.ToInt32(cmbActionName.Value);
            result = _MTBFProxy.UpdateRoleActionMapping(roleActionMapping);

            return result;
        }


        /// <summary>
        /// Method to load role information in combo box.
        /// </summary>
        private void LoadRoleInformation()
        {
            List<Role> lstRole = new List<Role>();

            Role role = new Role();
            role.RoleID = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            role.RoleName = MTBFConstants.DataField.COMBO_DEFAULT_NAME;

            lstRole = _MTBFProxy.GetAllRole().Cast<Role>().ToList();
            lstRole.Insert(0, role);

            UIHelper.SetComboBoxData(cmbRoleName, lstRole, MTBFConstants.DataField.ROLE_NAME, MTBFConstants.DataField.ROLE_ID);
        }

        /// <summary>
        /// Method to load permission information in combo box.
        /// </summary>
        private void LoadPermissionInformation(int roleID)
        {
            List<Permission> lstPermission = new List<Permission>();

            Permission permission = new Permission();
            permission.PermissionID = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            permission.PermissionName = MTBFConstants.DataField.COMBO_DEFAULT_NAME;

            lstPermission = _MTBFProxy.GetAllPermissionByRoleID(roleID).Cast<Permission>().ToList();
            lstPermission.Insert(0, permission);

            UIHelper.SetComboBoxData(cmbPermissionName, lstPermission, MTBFConstants.DataField.PERMISSION_NAME, MTBFConstants.DataField.PERMISSION_ID);
        }

        /// <summary>
        /// Method to load action informaiton in combo box.
        /// </summary>
        private void LoadActionInformation(int permissionID)
        {
            List<IMFS.Common.DTO.Action> lstAction = new List<IMFS.Common.DTO .Action>();

           IMFS.Common.DTO.Action action = new IMFS.Common.DTO.Action();
            action.ActionID = MTBFConstants.DataField.COMBO_DEFAULT_ID ;
            action.ActionName = MTBFConstants.DataField.COMBO_DEFAULT_NAME;

            lstAction = _MTBFProxy.GetAllActionByPermissionID(permissionID).Cast<IMFS.Common.DTO.Action>().ToList();
            lstAction.Insert(0, action);

            UIHelper.SetComboBoxData(cmbActionName, lstAction, MTBFConstants.DataField.ACTION_NAME, MTBFConstants.DataField.ACTION_ID);
        }


        /// <summary>
        /// Method to load all role in grid.
        /// </summary>
        private void LoadAllRoleActionMapping()
        {
            DataTable roleTable = new DataTable();
            roleTable.Columns.Add(MTBFConstants.DataField.ROLE_ACTION_MAP_ID);
            roleTable.Columns.Add(MTBFConstants.GridHeader.ROLE_NAME);
            roleTable.Columns.Add(MTBFConstants.GridHeader.PERMISSION_NAME);
            roleTable.Columns.Add(MTBFConstants.GridHeader.ACTION_NAME);
            foreach (RoleActionMapping roleActionMap in _MTBFProxy.GetAllRoleActionMapping())
            {
                DataRow row = roleTable.NewRow();
                Role role = _MTBFProxy.GetRoleByID(roleActionMap.RoleID);
                Permission permission = _MTBFProxy.GetPermissionByID(roleActionMap.PermissionID);
               IMFS.Common.DTO.Action action = _MTBFProxy.GetActionByID(roleActionMap.ActionID);

                row[MTBFConstants.DataField.ROLE_ACTION_MAP_ID] = roleActionMap.RoleActionMappingID;
                row[MTBFConstants.GridHeader.ROLE_NAME] = (role != null) ? role.RoleName : string.Empty;
                row[MTBFConstants.GridHeader.PERMISSION_NAME] = (permission != null) ? permission.PermissionName : string.Empty;
                row[MTBFConstants.GridHeader.ACTION_NAME] = (action != null) ? action.ActionName : string.Empty;

                roleTable.Rows.Add(row);
            }
            grvActionInfo.DataSource = roleTable;

            grvActionInfo.DisplayLayout.Bands[0].Columns[MTBFConstants.DataField.ROLE_ACTION_MAP_ID].Hidden = true;
        }

        /// <summary>
        /// Method to valid form data.
        /// </summary>
        /// <returns></returns>
        private bool ValidFormData()
        {
            if (Convert.ToInt32(cmbRoleName.Value) == MTBFConstants.DataField.COMBO_DEFAULT_ID)
            {
                MessageBoxHelper.ShowInformation("You need to select role.");
                cmbRoleName.Focus();
                return false;
            }

            if (Convert.ToInt32(cmbPermissionName.Value) == MTBFConstants.DataField.COMBO_DEFAULT_ID)
            {
                MessageBoxHelper.ShowInformation("You need to select permission.");
                cmbPermissionName.Focus();
                return false;
            }

            if (Convert.ToInt32(cmbActionName.Value) == MTBFConstants.DataField.COMBO_DEFAULT_ID)
            {
                MessageBoxHelper.ShowInformation("You need to select action.");
                cmbActionName.Focus();
                return false;
            }

            if (IsDuplicateMapping())
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Method to check duplicate mapping information.
        /// </summary>
        /// <returns></returns>
        private bool IsDuplicateMapping()
        {
            RoleActionMapping roleActionMapping = _MTBFProxy.GetRoleActionMapByRolePermissionAndActionID(Convert.ToInt32(cmbRoleName.Value), Convert.ToInt32(cmbPermissionName.Value), Convert.ToInt32(cmbActionName.Value));
            if (roleActionMapping != null)
            {
                if (_roleActionMappingID != roleActionMapping.RoleActionMappingID)
                {
                    MessageBoxHelper.ShowInformation("This mapping is already exists");
                    return true;
                }
            }
            return false;
        }

        #endregion

    }
}
