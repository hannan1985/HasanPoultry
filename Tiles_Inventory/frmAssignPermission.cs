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
    public partial class frmAssignPermission : BaseForm
    {
        FacadeManager _MTBFProxy = new FacadeManager();
        private int _rolePermissionMappingID = 0;

        public frmAssignPermission()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #region "Private Methods"

        /// <summary>
        /// Method to insert role permission mapping information.
        /// </summary>
        /// <returns></returns>
        private int InsertRolePermissionMapping()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            RolePermissionMapping rolePermisssionMap = new RolePermissionMapping();

            rolePermisssionMap.RoleID = Convert.ToInt32(cmbRoleName.Value);
            rolePermisssionMap.ModuleID = Convert.ToInt32(cmbModuleName.Value);
            rolePermisssionMap.PermissionID = Convert.ToInt32(cmbPermissionName.Value);

            result = _MTBFProxy.InsertRolePermissionMapping(rolePermisssionMap);

            return result;
        }

        /// <summary>
        /// Method to update role action mapping informaiton.
        /// </summary>
        /// <returns></returns>
        private int UpdateRolePermissionMapping()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            RolePermissionMapping rolePermisssionMap = _MTBFProxy.GetRolePermissionMapByID(_rolePermissionMappingID);

            rolePermisssionMap.RoleID = Convert.ToInt32(cmbRoleName.Value);
            rolePermisssionMap.ModuleID = Convert.ToInt32(cmbModuleName.Value);
            rolePermisssionMap.PermissionID = Convert.ToInt32(cmbPermissionName.Value);

            result = _MTBFProxy.UpdateRolePermissionMapping(rolePermisssionMap);

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
        /// Method to load module informaiton in combo box.
        /// </summary>
        private void LoadModuleInformation(int roleID)
        {
            List<Module> lstModule = new List<Module>();

            Module module = new Module();
            module.ModuleID = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            module.Description = MTBFConstants.DataField.COMBO_DEFAULT_NAME;

            lstModule = _MTBFProxy.GetAllModuleByRoleID(roleID).Cast<Module>().ToList();
            lstModule.Insert(0, module);

            UIHelper.SetComboBoxData(cmbModuleName, lstModule, MTBFConstants.DataField.DESCRIPTION, MTBFConstants.DataField.MODULE_ID);
        }


        /// <summary>
        /// Method to load permission information in combo box.
        /// </summary>
        private void LoadPermissionInformation(int moduleID)
        {
            List<Permission> lstPermission = new List<Permission>();

            Permission permission = new Permission();
            permission.PermissionID = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            permission.PermissionName = MTBFConstants.DataField.COMBO_DEFAULT_NAME;

            lstPermission = _MTBFProxy.GetAllPermissionByModuleID(moduleID).Cast<Permission>().ToList();
            lstPermission.Insert(0, permission);

            UIHelper.SetComboBoxData(cmbPermissionName, lstPermission, MTBFConstants.DataField.PERMISSION_NAME, MTBFConstants.DataField.PERMISSION_ID);
        }

        /// <summary>
        /// Method to load all role in grid.
        /// </summary>
        private void LoadAllRolePermissionMapping()
        {
            DataTable roleTable = new DataTable();

            roleTable.Columns.Add(MTBFConstants.DataField.ROLE_PERMISSION_MAP_ID);
            roleTable.Columns.Add(MTBFConstants.GridHeader.ROLE_NAME);
            roleTable.Columns.Add(MTBFConstants.GridHeader.MODULE_NAME);
            roleTable.Columns.Add(MTBFConstants.GridHeader.PERMISSION_NAME);

            List<Permission> lstPermission = new List<Permission>();
            List<Module> lstModule = new List<Module>();

            lstPermission = _MTBFProxy.GetAllPermission().Cast<Permission>().ToList();
            lstModule = _MTBFProxy.GetAllModule().Cast<Module>().ToList();

            foreach (RolePermissionMapping rolePermissionMap in _MTBFProxy.GetAllRolePermissionMappingByRoleID(Convert.ToInt32(cmbRoleName.Value)))
            {
                DataRow row = roleTable.NewRow();

                Permission permission = lstPermission.Where(p => p.PermissionID == rolePermissionMap.PermissionID).Cast<Permission>().ToList().FirstOrDefault();
                Module module = lstModule.Where(m => m.ModuleID == rolePermissionMap.ModuleID).Cast<Module>().ToList().FirstOrDefault();

                row[MTBFConstants.DataField.ROLE_PERMISSION_MAP_ID] = rolePermissionMap.RolePermissionID;
                row[MTBFConstants.GridHeader.ROLE_NAME] = (cmbRoleName.Value != null) ? cmbRoleName.Text : string.Empty;
                row[MTBFConstants.GridHeader.MODULE_NAME] = (module != null) ? module.Description : string.Empty;
                row[MTBFConstants.GridHeader.PERMISSION_NAME] = (permission != null) ? permission.PermissionName : string.Empty;
                roleTable.Rows.Add(row);
            }
            grvPermissionInfo.DataSource = roleTable;

            grvPermissionInfo.DisplayLayout.Bands[0].Columns[MTBFConstants.DataField.ROLE_PERMISSION_MAP_ID].Hidden = true;
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

            if (Convert.ToInt32(cmbModuleName.Value) == MTBFConstants.DataField.COMBO_DEFAULT_ID)
            {
                MessageBoxHelper.ShowInformation("You need to select module.");
                cmbModuleName.Focus();
                return false;
            }

            if (Convert.ToInt32(cmbPermissionName.Value) == MTBFConstants.DataField.COMBO_DEFAULT_ID)
            {
                MessageBoxHelper.ShowInformation("You need to select permission");
                cmbPermissionName.Focus();
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
            RolePermissionMapping rolePermissionMap = _MTBFProxy.GetRolePermissionMapByRolePermissionAndModuleID(Convert.ToInt32(cmbRoleName.Value), Convert.ToInt32(cmbModuleName.Value), Convert.ToInt32(cmbPermissionName.Value));
            if (rolePermissionMap != null)
            {
                if (_rolePermissionMappingID != rolePermissionMap.RolePermissionID)
                {
                    MessageBoxHelper.ShowInformation("This mapping is already exists");
                    return true;
                }
            }
            return false;
        }

        #endregion

        private void cmbRoleName_ValueChanged(object sender, EventArgs e)
        {
            if (cmbRoleName.Value != null)
            {
                LoadModuleInformation(Convert.ToInt32(cmbRoleName.Value));
                LoadAllRolePermissionMapping();
            }
        }

        private void cmbModuleName_ValueChanged(object sender, EventArgs e)
        {
            if (cmbModuleName.Value != null)
            {
                LoadPermissionInformation(Convert.ToInt32(cmbModuleName.Value));
            }
        }

        private void frmAssignPermission_Load(object sender, EventArgs e)
        {
            LoadRoleInformation();
            //  LoadAllRolePermissionMapping();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidFormData())
            {
                if (base.IsUpdating)
                {
                    if (UpdateRolePermissionMapping() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Mapping information saved successfully.");
                        LoadAllRolePermissionMapping();
                        base.IsUpdating = false;
                        _rolePermissionMappingID = 0;
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save mapping information");
                    }
                }
                else
                {
                    if (InsertRolePermissionMapping() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Mapping information saved successfully.");
                        LoadAllRolePermissionMapping();
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save mapping information");
                    }
                }
            }
        }

        private void grvPermissionInfo_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            if (grvPermissionInfo.Selected.Rows.Count > 0)
            {
                _rolePermissionMappingID = Convert.ToInt32(grvPermissionInfo.Selected.Rows[0].Cells[MTBFConstants.DataField.ROLE_PERMISSION_MAP_ID].Value);

                RolePermissionMapping rolePermissionMapping = _MTBFProxy.GetRolePermissionMapByID(_rolePermissionMappingID);
                if (rolePermissionMapping != null)
                {
                    cmbRoleName.Value = rolePermissionMapping.RoleID;
                    cmbModuleName.Value = rolePermissionMapping.ModuleID;
                    cmbPermissionName.Value = rolePermissionMapping.PermissionID;
                    _rolePermissionMappingID = rolePermissionMapping.RolePermissionID;
                    base.IsUpdating = true;
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadRoleInformation();
            LoadAllRolePermissionMapping();
        }
    }
}
