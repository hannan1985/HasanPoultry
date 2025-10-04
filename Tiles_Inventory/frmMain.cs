using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IFMS.Facade;
using IMFS.Common.DTO;
using IMFS.Common.Utility;
using NybSys.MTBF.Utility.Constants;
using NybSys.MTBF.Utility.Helper;
using IFMS.BLL;

namespace Tiles_Inventory
{
    public partial class frmMain : BaseForm
    {
        #region "Constructor"

        public frmMain()
        {
            DataAccessProxy = new FacadeManager();

            frmLogin testDialog = new frmLogin();

            if (testDialog.ShowDialog(this) == DialogResult.OK)
            {
                // Add any initialization after the InitializeComponent() call.
                InitializeComponent();
            }
            else
            {
                System.Environment.Exit(0);
            }
        }

        #endregion


        private Dictionary<string, Form> _openFormList = new Dictionary<string, Form>();
        public Dictionary<string, Form> OpenFormList
        {
            get { return _openFormList; }
            set { _openFormList = value; }
        }


        private void Navigate(string navigationKey, object parameters)
        {
            Form frm = null;
            string typeName = string.Empty;
            string formKey = navigationKey + parameters;

            if (OpenFormList.Keys.Contains(formKey))
            {
                frm = OpenFormList[formKey];
                frm.Focus();
            }
            else
            {
                typeName = GetTypeName(navigationKey);
                if (Type.GetType(typeName) != null)
                {
                    if (parameters == null)
                    {
                        frm = Activator.CreateInstance(Type.GetType(typeName)) as BaseForm;
                    }
                    else
                    {
                        frm = Activator.CreateInstance(Type.GetType(typeName), new object[] { parameters }) as Form;
                    }

                    if (frm.Name == "frmChangePassword" || frm.Name == "frmDataBaseBackup")
                    {
                        // OpenFormList[formKey] = frm;
                        frm.ShowDialog();
                    }
                    else
                    {
                        OpenFormList[formKey] = frm;
                        frm.MdiParent = this;
                        frm.AutoScrollMinSize = frm.Size;
                        frm.VerticalScroll.Enabled = true;
                        frm.Show();
                    }

                }
            }
        }


        /// <summary>
        /// Method to get full type name based on class name.
        /// </summary>
        /// <param name="formName"></param>
        /// <returns></returns>
        private string GetTypeName(string formName)
        {
            return this.GetType().ToString().Replace(this.Name, formName);
        }

        private void tabbedmdimgr_TabClosing(System.Object sender, Infragistics.Win.UltraWinTabbedMdi.CancelableMdiTabEventArgs e)
        {
            Form frm = e.Tab.Form as Form;
            if (frm != null)
            {
                OpenFormList.Remove(frm.Name);
            }
        }

        private void OpeningForm_Load(System.Object sender, System.EventArgs e)
        {
            Employee employee = DataAccessProxy.GetEmployeeByID(IFMSConstant.LoggedinUserID);
            Branch branch = new BranchManager().GetBranchByID(MTBFConstants.AppConstants.BranchID);
            if (employee != null)
            {
                lblLogedInUser.Text = "Welcome : " + employee.EmployeeName;
            }
            else
            {
                lblLogedInUser.Text = "Welcome : " + MTBFConstants.AppConstants.LoggedinUser.Name;
            }
            lblBranchName.Text = (branch != null) ? "Branch: " + branch.BranchName : string.Empty;
            CheckUserAccess();

        }


        private void timer_Tick(System.Object sender, System.EventArgs e)
        {
            TimeSpan systemUsage = (DateTime.Now - IFMSConstant.LoggedinTime);
            lbltime.Text = string.Format("Loggedin Time : {0}:{1}:{2}", systemUsage.Hours.ToString().PadLeft(2, '0'), systemUsage.Minutes.ToString().PadLeft(2, '0'), systemUsage.Seconds.ToString().PadLeft(2, '0'));

        }

        private void OpeningForm_FormClosing(System.Object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            DialogResult mresult = MessageBoxHelper.ShowConfirmation("Do you want to close?");
            if (mresult == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void Explorer_ItemClick(object sender, Infragistics.Win.UltraWinExplorerBar.ItemEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Item.Key))
            {
                string menuPath = e.Item.Group.Text + @" \ " + e.Item.Text;
                Navigate(e.Item.Key, e.Item.Tag);
            }
        }



        #region "User Access"

        private void CheckUserAccess()
        {
            FacadeManager _MTBFProxy = new FacadeManager();
            List<string> lstPermissionName = new List<string>();

            Users user = _MTBFProxy.GetUserByUserID(MTBFConstants.AppConstants.LoggedinUserID);
            if (user != null)
            {
                if (!user.IsSuperAdmin)
                {
                    string moduleName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                    moduleName = moduleName.Substring(moduleName.LastIndexOf(".") + 1);
                    List<Permission> lstPermission = new List<Permission>();
                    lstPermission = new SecurityManager().GetAllPermission();
                    Module module = _MTBFProxy.GetModuleByName(moduleName);

                    if (module != null)
                    {
                        List<RolePermissionMapping> lstRolePermission = _MTBFProxy.GetAllRolePermissionMappingByUserAndModuleID(MTBFConstants.AppConstants.LoggedinUserID, module.ModuleID).Cast<RolePermissionMapping>().ToList();
                        if (lstRolePermission.Count > 0)
                        {
                            foreach (RolePermissionMapping rolePermission in lstRolePermission)
                            {
                                Permission permission = lstPermission.Where(p => p.PermissionID == rolePermission.PermissionID).FirstOrDefault();
                                if (permission != null)
                                    lstPermissionName.Add(permission.FormName);
                            }
                            VisibleManuItem(lstPermissionName);
                        }
                        else
                        {
                            HideManuItem();
                        }
                    }
                }
            }
        }

        private void VisibleManuItem(List<string> lstPermissionName)
        {
            foreach (Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup ultraGroup in Explorer.Groups)
            {
                foreach (Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarItem item in ultraGroup.Items)
                {
                    if (lstPermissionName.Contains(item.Key))
                    {
                        item.Visible = true;
                    }
                    else
                    {
                        item.Visible = false;
                    }

                }
            }
        }

        private void HideManuItem()
        {
            foreach (Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup ultraGroup in Explorer.Groups)
            {
                foreach (Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarItem item in ultraGroup.Items)
                {
                    item.Visible = false;
                }
            }
        }


        #endregion

    }
}
