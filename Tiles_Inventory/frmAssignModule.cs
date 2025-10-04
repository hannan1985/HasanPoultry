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
    public partial class frmAssignModule : BaseForm
    {
        FacadeManager _MTBFProxy = new FacadeManager();
        private int _roleModuleID = 0;

        public frmAssignModule()
        {
            InitializeComponent();
        }

        #region "Private Events"

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidFormData())
            {
                if (base.IsUpdating)
                {
                    if (UpdateRoleModuleMapping() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Mapping information saved successfully");
                        LoadAllRoleModuleMapping();
                        base.IsUpdating = false;
                        _roleModuleID = 0;
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save mapping information");
                    }
                }
                else
                {
                    if (InsertRoleModuleMapping() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {

                        MessageBoxHelper.ShowInformation("Mapping information saved successfully");
                        LoadAllRoleModuleMapping();
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save mapping information");
                    }
                }
            }

        }

        private void frmAssignModule_Load(object sender, EventArgs e)
        {
            LoadRoleInformation();
            LoadModuleInformation();
            LoadAllRoleModuleMapping();
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadRoleInformation();
            LoadModuleInformation();
            LoadAllRoleModuleMapping();
        }

        private void grmModuleInfo_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            if (grmModuleInfo.Selected.Rows.Count > 0)
            {
                _roleModuleID = Convert.ToInt32(grmModuleInfo.Selected.Rows[0].Cells[MTBFConstants.DataField.ROLE_MODULE_MAP_ID].Value);

                RoleModuleMapping roleModuleMapping = _MTBFProxy.GetRoleModuleMapByID(_roleModuleID);
                if (roleModuleMapping != null)
                {
                    cmbRoleName.Value = roleModuleMapping.RoleID;
                    cmbModuleName.Value = roleModuleMapping.ModuleID;
                    _roleModuleID = roleModuleMapping.RoleModuleMappingID;
                    base.IsUpdating = true;
                }

            }
        }


        #endregion

        #region "Private Methods"

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

            if (IsDuplicateMapping())
            {
                return false;
            }

            return true;
        }


        private bool IsDuplicateMapping()
        {
            RoleModuleMapping roleModuleMapping = _MTBFProxy.GetRoleModuleMapByRoleAndModuleID(Convert.ToInt32(cmbRoleName.Value), Convert.ToInt32(cmbModuleName.Value));
            if (roleModuleMapping != null)
            {
                if (_roleModuleID != roleModuleMapping.RoleModuleMappingID)
                {
                    MessageBoxHelper.ShowInformation("This mapping is already exists.");
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Method to insert 
        /// </summary>
        /// <returns></returns>
        private int InsertRoleModuleMapping()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            RoleModuleMapping roleModuleMap = new RoleModuleMapping();
            roleModuleMap.RoleID = Convert.ToInt32(cmbRoleName.Value);
            roleModuleMap.ModuleID = Convert.ToInt32(cmbModuleName.Value);
            result = _MTBFProxy.InsertRoleModuleMapping(roleModuleMap);
            return result;
        }

        /// <summary>
        /// Method to update role
        /// </summary>
        /// <returns></returns>
        private int UpdateRoleModuleMapping()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            RoleModuleMapping roleModuleMap = _MTBFProxy.GetRoleModuleMapByID(_roleModuleID);
            roleModuleMap.RoleID = Convert.ToInt32(cmbRoleName.Value);
            roleModuleMap.ModuleID = Convert.ToInt32(cmbModuleName.Value);
            result = _MTBFProxy.UpdateRoleModuleMapping(roleModuleMap);
            return result;
        }

        /// <summary>
        /// Method to load module information in combo box.
        /// </summary>
        private void LoadModuleInformation()
        {
            List<Module> lstModule = new List<Module>();

            Module module = new Module();
            module.ModuleID = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            module.Description = MTBFConstants.DataField.COMBO_DEFAULT_NAME;

            lstModule = _MTBFProxy.GetAllModule().Cast<Module>().ToList();
            lstModule.Insert(0, module);

            UIHelper.SetComboBoxData(cmbModuleName, lstModule, MTBFConstants.DataField.DESCRIPTION, MTBFConstants.DataField.MODULE_ID);
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
        /// Method to load all role in grid.
        /// </summary>
        private void LoadAllRoleModuleMapping()
        {
            DataTable roleTable = new DataTable();
            roleTable.Columns.Add(MTBFConstants.DataField.ROLE_MODULE_MAP_ID);
            roleTable.Columns.Add(MTBFConstants.GridHeader.MODULE_NAME);
            roleTable.Columns.Add(MTBFConstants.GridHeader.ROLE_NAME);
            foreach (RoleModuleMapping roleModuleMap in _MTBFProxy.GetAllRoleModuleMapping())
            {
                DataRow row = roleTable.NewRow();
                Role role = _MTBFProxy.GetRoleByID(roleModuleMap.RoleID);
                Module module = _MTBFProxy.GetModuleByID(roleModuleMap.ModuleID);

                row[MTBFConstants.DataField.ROLE_MODULE_MAP_ID] = roleModuleMap.RoleModuleMappingID;
                row[MTBFConstants.GridHeader.MODULE_NAME] = (module != null) ? module.Description : string.Empty;
                row[MTBFConstants.GridHeader.ROLE_NAME] = (role != null) ? role.RoleName : string.Empty;

                roleTable.Rows.Add(row);
            }
            grmModuleInfo.DataSource = roleTable;

            grmModuleInfo.DisplayLayout.Bands[0].Columns[MTBFConstants.DataField.ROLE_MODULE_MAP_ID].Hidden = true;
        }

        #endregion
    }
}
