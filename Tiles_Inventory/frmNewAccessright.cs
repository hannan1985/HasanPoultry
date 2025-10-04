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

namespace Tiles_Inventory
{
    public partial class frmNewAccessright : BaseForm
    {
        List<Permission> _lstPerssion = new List<Permission>();
        List<IMFS.Common.DTO.Action> _lstAction = new List<IMFS.Common.DTO.Action>();

        public frmNewAccessright()
        {
            InitializeComponent();
        }

        private void frmNewAccessright_Load(object sender, EventArgs e)
        {
            LoadRoleInformation();
            LoadAllPermission();
            LoadAllAction();
            LoadTree();

        }

        private void LoadAllAction()
        {
            _lstAction = new SecurityManager().GetAllAction();
        }

        private void LoadAllPermission()
        {
            _lstPerssion = new SecurityManager().GetAllPermission();
        }

        private void LoadRoleInformation()
        {
            List<Role> lstRole = new List<Role>();

            Role role = new Role();
            role.RoleID = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            role.RoleName = MTBFConstants.DataField.COMBO_DEFAULT_NAME;

            lstRole = new SecurityManager().GetAllRole().Cast<Role>().ToList();
            lstRole.Insert(0, role);

            UIHelper.SetComboBoxData(cmbRoleName, lstRole, MTBFConstants.DataField.ROLE_NAME, MTBFConstants.DataField.ROLE_ID);
        }

        private void LoadTree()
        {
            // Suppress repainting the TreeView until all the objects have been created.
            treeView1.BeginUpdate();

            // Clear the TreeView each time the method is called.
            treeView1.Nodes.Clear();
            int mInd = 0;



            int pInd = 0;
            foreach (Permission permission in _lstPerssion)
            {
                TreeNode trPermission = new TreeNode();
                trPermission.Name = permission.PermissionID.ToString();
                trPermission.Text = permission.PermissionName;
                treeView1.Nodes.Add(trPermission);

                List<IMFS.Common.DTO.Action> lstAction = _lstAction.Where(x => x.PermissionID == permission.PermissionID).Cast<IMFS.Common.DTO.Action>().ToList();

                foreach (IMFS.Common.DTO.Action action in lstAction)
                {
                    TreeNode trAction = new TreeNode();
                    trAction.Name = action.ActionID.ToString();
                    trAction.Text = action.ActionName;
                    treeView1.Nodes[pInd].Nodes.Add(trAction);

                }
                pInd++;
            }
            mInd++;


            // Reset the cursor to the default for all controls.
            Cursor.Current = Cursors.Default;

            // Begin repainting the TreeView.
            treeView1.EndUpdate();
        }

        private void btUserRole_Click(object sender, EventArgs e)
        {
            frmAddRole frm = new frmAddRole();
            frm.OnRoleInformationLoad += new frmAddRole.OnRoleInformationLoadEventHandler(frm_OnRoleInformationLoad);
            frm.ShowDialog();
        }

        void frm_OnRoleInformationLoad()
        {
            LoadRoleInformation();
        }

       
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbRoleName.Value == null || Convert.ToInt32(cmbRoleName.Value) == MTBFConstants.DataField.COMBO_DEFAULT_ID)
            {
                MessageBoxHelper.ShowInformation("Plese select role information.");
                return;
            }

            List<RolePermissionMapping> lstRolePermission = new List<RolePermissionMapping>();
            List<RoleActionMapping> lstRoleActionMap = new List<RoleActionMapping>();
            GetAllCheckedParmissionAndAction(out lstRolePermission, out lstRoleActionMap);

            if (lstRoleActionMap.Count == 0 & lstRolePermission.Count == 0)
            {
                MessageBoxHelper.ShowInformation("Plese select permission or action.");
                return;
            }

            if (new SecurityManager().InsertActionPermission(lstRolePermission, lstRoleActionMap) == (int)MTBFEnums.ReturnResult.SUCCESS)
            {
                MessageBoxHelper.ShowInformation("Information saved successfully");
            }
            else
            {
                MessageBoxHelper.ShowInformation("Failed to save information");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void GetAllCheckedParmissionAndAction(out List<RolePermissionMapping> lstRolePermission, out List<RoleActionMapping> lstRoleActionMap)
        {
            lstRoleActionMap = new List<RoleActionMapping>();
            lstRolePermission = new List<RolePermissionMapping>();

            int perIndex = 0;
            foreach (TreeNode permissionNode in treeView1.Nodes)
            {

                if (permissionNode.Checked)
                {
                    if (!string.IsNullOrEmpty(permissionNode.Name))
                    {
                        RolePermissionMapping permissionRole = new RolePermissionMapping();
                        int permissinID = Convert.ToInt32(permissionNode.Name);
                        permissionRole.RoleID = Convert.ToInt32(cmbRoleName.Value);
                        permissionRole.PermissionID = permissinID;
                        permissionRole.ModuleID = 1;
                        lstRolePermission.Add(permissionRole);
                    }
                }

                foreach (TreeNode actionNode in treeView1.Nodes[perIndex].Nodes)
                {
                    if (actionNode.Checked)
                    {
                        if (!string.IsNullOrEmpty(actionNode.Name))
                        {

                            RoleActionMapping actionRole = new RoleActionMapping();

                            int permisID = Convert.ToInt32(permissionNode.Name);
                            int actionID = Convert.ToInt32(actionNode.Name);
                            actionRole.RoleID = Convert.ToInt32(cmbRoleName.Value);
                            actionRole.PermissionID = permisID;
                            actionRole.ActionID = actionID;
                            lstRoleActionMap.Add(actionRole);
                        }
                    }

                }
                perIndex++;
            }


        }

        private void cmbRoleName_ValueChanged(object sender, EventArgs e)
        {
            if (cmbRoleName.Value != null)
            {
                LoadExistingInformation(Convert.ToInt32(cmbRoleName.Value));
            }
        }


        private void LoadExistingInformation(int roleID)
        {
            List<RolePermissionMapping> lstRolePermissionMapping = new List<RolePermissionMapping>();
            List<RoleActionMapping> lstRoleActionMap = new List<RoleActionMapping>();

            lstRolePermissionMapping = new SecurityManager().GetAllRolePermissionMappingByRoleID(roleID);

            int[] permissionIDs = lstRolePermissionMapping.Select(p => p.PermissionID).Distinct().ToArray();
            if (permissionIDs.Length > 0)
            {
                string permissionFilter = String.Format("{0} IN ({1})", "PermissionID", String.Join(",", permissionIDs));
                lstRoleActionMap = new SecurityManager().GetFilteredPermissionAction(permissionFilter);
            }


            int perIndex = 0;
            foreach (TreeNode permissionNode in treeView1.Nodes)
            {
                int permissionID = Convert.ToInt32(permissionNode.Name);
                RolePermissionMapping permissionMap = lstRolePermissionMapping.Where(p => p.PermissionID == permissionID).FirstOrDefault();

                if (permissionMap != null)
                {
                    permissionNode.Checked = true;
                }
                else
                {
                    permissionNode.Checked = false ;
                }

                foreach (TreeNode actionNode in treeView1.Nodes[perIndex].Nodes)
                {
                    int actionID = Convert.ToInt32(actionNode.Name);
                    RoleActionMapping roleActionMap = lstRoleActionMap.Where(a => a.ActionID == actionID).FirstOrDefault();
                    if (roleActionMap != null)
                    {
                        actionNode.Checked = true;
                    }
                    else
                    {
                        actionNode.Checked = false ;
                    }

                }
                perIndex++;
            }
        }

        private void btnNewPermission_Click(object sender, EventArgs e)
        {
            frmAddPermission frm = new frmAddPermission();
            frm.ShowDialog();
        }

        private void btnNewAction_Click(object sender, EventArgs e)
        {
            frmAddAction frm = new frmAddAction();
            frm.ShowDialog();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadRoleInformation();
            LoadTree();
        }

    }
}
