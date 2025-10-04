//Author : Md. Jahangir Alam Sarker.
//Created Date:11-07-2013
//Modified By: Md. Jahangir Alam Sarker.
// Modification Date :12-07-2013.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NybSys.MTBF.Utility.Enums;
using System.Data.SqlClient;
using NybSys.MTBF.Utility.Constants;
using NybSys.MTBF.Utility.Resources;
using NybSys.MTBF.Utility.Encryption;
using System.Text.RegularExpressions;
using NybSys.MTBF.Utility.Helper;
using Infragistics.Win.UltraWinGrid;
using IMFS.Common.DTO;
using IFMS.Facade;
using IFMS.BLL;

namespace Tiles_Inventory
{
    public partial class frmUser : BaseForm
    {
        FacadeManager _MTBFProxy = new FacadeManager();
        public frmUser()
        {
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        #region "Member Variable"

        private string _usersID = string.Empty;

        MTBFEncryption objCrypto = new MTBFEncryption();

        #endregion

        #region "Event Handlers"

        private void frmUser_Load(object sender, EventArgs e)
        {
            LoadStatus();
            LoadUserTypeCombo();
            LoadEmployeeCombo();
            LoadRoleInformation();
            LoadUsersInformation();
            base.CheckAction(this);
            if (base.IsUpdating)
            {
                ResetAllControls();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidFormData())
            {
                if (base.IsUpdating)
                {
                    if (UpdateUsers() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation(SystemMessages.USERS_SAVED);

                        LoadUsersInformation();
                        ResetAllControls();
                        _usersID = string.Empty;
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation(SystemMessages.USERS_SAVED_FAILED);
                    }
                }
                else
                {
                    if (InsertUsers() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation(SystemMessages.USERS_SAVED);

                        LoadUsersInformation();
                        ResetAllControls();
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation(SystemMessages.USERS_SAVED_FAILED);
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadUsersInformation();
            LoadEmployeeCombo();
            LoadRoleInformation();
            txtDesignation.Clear();
            txtEmail.Clear();
            txtTelephone.Clear();
        }




        private void cmbEmployeeName_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbEmployeeName.Value) != MTBFConstants.DataField.COMBO_DEFAULT_ID)
            {
                Employee employee = _MTBFProxy.GetEmployeeByID(Convert.ToInt32(cmbEmployeeName.Value));
                if (employee != null)
                {
                    Designation designation = new EmployeeManager().GetDesignationByID(employee.DesignationID);

                    txtDesignation.Text = (designation != null) ? designation.DesignationName : string.Empty;
                    txtTelephone.Text = employee.Phone;
                    txtEmail.Text = employee.Email;
                }
                else
                {
                    txtDesignation.Clear();
                    txtTelephone.Clear();
                    txtEmail.Clear();
                }
            }
        }

        private void grvRoleInfo_ClickCell(object sender, ClickCellEventArgs e)
        {
            if (e.Cell.Column.Index == 0)
            {
                if (Convert.ToBoolean(e.Cell.Value))
                {
                    e.Cell.Activation = Activation.AllowEdit;
                    e.Cell.Value = false;
                }
                else
                {
                    e.Cell.Activation = Activation.AllowEdit;
                    e.Cell.Value = true;
                }

            }
        }

        private void grvUsers_ClickCell(object sender, ClickCellEventArgs e)
        {
            if (grvUsers.Selected.Rows.Count > 0)
            {
                _usersID = grvUsers.Selected.Rows[0].Cells[MTBFConstants.GridHeader.USER_ID].Value.ToString();
                ViewUsersInformation(_usersID);

                txtUserName.Enabled = false;
                base.IsUpdating = true;
            }
            else
            {
                base.IsUpdating = false;
            }
        }

        #endregion

        #region "Private Methods"


        /// <summary>
        /// Method to laod controlling officer in combo box.
        /// </summary>
        private void LoadEmployeeCombo()
        {
            Employee employee = new Employee();
            employee.EmployeeID = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            employee.EmployeeName = MTBFConstants.DataField.COMBO_DEFAULT_NAME;

            List<Employee> lstEmployee = _MTBFProxy.GetAllEmployee().Cast<Employee>().Where(e => e.BranchID == MTBFConstants.AppConstants.BranchID && e.OrganizationID == MTBFConstants.AppConstants.OrganizationID).ToList();
            lstEmployee.Insert(0, employee);
            UIHelper.SetComboBoxData(cmbEmployeeName, lstEmployee, MTBFConstants.DataField.EMPLOYEE_NAME, MTBFConstants.DataField.EMPLOYEE_ID);
        }





        /// <summary>
        /// Method to reset all controls.
        /// </summary>
        private void ResetAllControls()
        {
            txtDesignation.Text = string.Empty;
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtTelephone.Text = string.Empty;
            txtRetypePassword.Text = string.Empty;
            cmbUserStatus.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            cmbEmployeeName.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            txtUserName.Enabled = true;
            base.IsUpdating = false;


        }

        /// <summary>
        /// Method to check blank data in required field.
        /// </summary>
        /// <returns></returns>
        private bool ValidFormData()
        {
            Regex rEMail = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");


            if (Convert.ToInt64(cmbEmployeeName.Value) == MTBFConstants.DataField.COMBO_DEFAULT_ID)
            {
                MessageBoxHelper.ShowInformation("You need to select employee");
                cmbEmployeeName.Focus();
                return false;
            }

            if (txtUserName.Text.Trim() == string.Empty)
            {
                MessageBoxHelper.ShowInformation(SystemMessages.ENTER_USER_ID);
                txtUserName.Focus();
                return false;
            }

            if (!base.IsUpdating)
            {
                if (string.IsNullOrEmpty(this.txtPassword.Text) && string.IsNullOrEmpty(this._usersID))
                {
                    MessageBoxHelper.ShowInformation(SystemMessages.ENTER_PASSWORD);
                    this.txtPassword.Focus();
                    return false;
                }
            }

            if (string.IsNullOrEmpty(this.txtRetypePassword.Text) && string.IsNullOrEmpty(this._usersID))
            {
                MessageBoxHelper.ShowInformation(SystemMessages.ENTER_PASSWORD);
                this.txtRetypePassword.Focus();
                return false;
            }

            if (!string.IsNullOrEmpty(this.txtPassword.Text) && this.txtPassword.Text != this.txtRetypePassword.Text)
            {
                MessageBoxHelper.ShowInformation("Password and retype password are not match");
                this.txtRetypePassword.Focus();
                return false;
            }

            if (cmbUserStatus.Value == null || (int)cmbUserStatus.Value == MTBFConstants.DataField.COMBO_DEFAULT_ID)
            {
                MessageBoxHelper.ShowInformation(SystemMessages.ENTER_REQUIRE_DATA);
                cmbUserStatus.Focus();
                return false;
            }

            if (IsDuplicateUser())
            {
                return false;
            }

            if (IsBlankUserRole())
            {
                MessageBoxHelper.ShowInformation("You need to check at least one role");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Method to check balnk user role.
        /// </summary>
        /// <returns></returns>
        private bool IsBlankUserRole()
        {
            foreach (UltraGridRow row in grvRoleInfo.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Select"].Value))
                {
                    return false;
                }
            }

            return true;
        }


        /// <summary>
        /// Method to check duplicate user.
        /// </summary>
        /// <returns></returns>
        private bool IsDuplicateUser()
        {
            Users users = _MTBFProxy.GetUserByUserName(txtUserName.Text);
            if (users != null)
            {
                if (_usersID != users.UserID)
                {
                    MessageBoxHelper.ShowInformation(SystemMessages.DUPLICATE_USERS);
                    txtUserName.Focus();
                    return true;
                }

            }
            return false;
        }

        /// <summary>
        /// Method to load user information in grid.
        /// Email table ref set(user into employee) by foyjul bary
        /// </summary>
        private void LoadUsersInformation()
        {
            List<Users> lstUsers = new List<Users>();
            lstUsers = _MTBFProxy.GetAllUses().Cast<Users>().Where(u => u.BranchID == MTBFConstants.AppConstants.BranchID && u.OrganizationID == MTBFConstants.AppConstants.OrganizationID && u.IsSuperAdmin == false).ToList();

            grvUsers.DataSource = BuildUserTable();
            foreach (Users users in lstUsers)
            {
                UltraGridRow row = grvUsers.DisplayLayout.Bands[0].AddNew();
                Employee employee = _MTBFProxy.GetEmployeeByID(users.EmployeeID);
                Designation designation = new EmployeeManager().GetDesignationByID(employee.DesignationID);

                row.Cells[MTBFConstants.GridHeader.NAME].Value = users.Name;
                row.Cells[MTBFConstants.GridHeader.DESIGNATION].Value = (designation != null) ? designation.DesignationName : string.Empty;
                row.Cells[MTBFConstants.GridHeader.USER_ID].Value = users.UserID;
                row.Cells[MTBFConstants.GridHeader.STATUS].Value = Enum.GetName(typeof(MTBFEnums.UserStatus), users.Status);
                row.Cells[MTBFConstants.GridHeader.EMAIL].Value = (employee != null) ? employee.Email : string.Empty;
            }
        }


        /// <summary>
        /// Method to build user table
        /// </summary>
        /// <returns></returns>
        private DataTable BuildUserTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add(MTBFConstants.GridHeader.NAME);
            table.Columns.Add(MTBFConstants.GridHeader.DESIGNATION);
            table.Columns.Add(MTBFConstants.GridHeader.USER_ID);
            table.Columns.Add(MTBFConstants.DataField.STATUS);
            table.Columns.Add(MTBFConstants.DataField.EMAIL);
            return table;
        }

        /// <summary>
        /// Method to insert users informaiton.
        /// </summary>
        /// <returns></returns>
        private int InsertUsers()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            Users users = new Users();
            List<Role> lstRole = new List<Role>();

            users.UserID = txtUserName.Text.Trim();
            users.Name = cmbEmployeeName.Text;
            users.EmployeeID = Convert.ToInt32(cmbEmployeeName.Value);
            users.Password = objCrypto.Encrypt(this.txtPassword.Text);
            users.Status = Convert.ToInt32(cmbUserStatus.Value.ToString());
            users.Email = txtEmail.Text;
            users.UserType = Convert.ToInt32(cmbUserType.Value);
            users.BranchID = MTBFConstants.AppConstants.BranchID;
            users.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            

            lstRole = GetAllUserRoles();
            result = _MTBFProxy.InsertUsers(users, lstRole);
            return result;

        }

        /// <summary>
        /// Method to load role information in combo. 
        /// </summary>
        private void LoadRoleInformation()
        {
            DataTable roleTable = BuildRoleTable();
            foreach (Role role in _MTBFProxy.GetAllRole().Cast<Role>())
            {
                DataRow row = roleTable.NewRow();
                row["Select"] = false;
                row[MTBFConstants.GridHeader.ROLE_NAME] = role.RoleName;
                row[MTBFConstants.GridHeader.DESCRIPTION] = role.Description;
                row[MTBFConstants.DataField.ROLE_ID] = role.RoleID;
                roleTable.Rows.Add(row);
            }

            grvRoleInfo.DataSource = roleTable;
            grvRoleInfo.DisplayLayout.Bands[0].Columns[MTBFConstants.DataField.ROLE_ID].Hidden = true;
        }

        /// <summary>
        /// Method to build role table.
        /// </summary>
        /// <returns></returns>
        private DataTable BuildRoleTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Select");
            table.Columns.Add(MTBFConstants.GridHeader.ROLE_NAME);
            table.Columns.Add(MTBFConstants.GridHeader.DESCRIPTION);
            table.Columns.Add(MTBFConstants.DataField.ROLE_ID);
            return table;
        }


        /// <summary>
        /// Method to update users informaiton.
        /// </summary>
        /// <returns></returns>
        private int UpdateUsers()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            Users users = _MTBFProxy.GetUserByUserID(_usersID);

            List<Role> lstRole = new List<Role>();
            try
            {
                // users.Surname = txtSurName.Text;
                users.Email = txtEmail.Text;
                users.EmployeeID = Convert.ToInt32(cmbEmployeeName.Value);
                users.Name = cmbEmployeeName.Text;
                users.UserType = Convert.ToInt32(cmbUserType.Value);
                if (txtPassword.Text != string.Empty)
                {
                    users.Password = objCrypto.Encrypt(this.txtPassword.Text);
                }
                users.Status = Convert.ToInt32(cmbUserStatus.Value.ToString());

                lstRole = GetAllUserRoles();
                _MTBFProxy.UpdateUsers(users, lstRole);

            }

            catch (SqlException ex)
            {
                if (ex.Number == MTBFConstants.SqlErrorCode.PRIMAY_KEY_VIOLATION)
                {
                    MessageBoxHelper.ShowInformation(SystemMessages.DUPLICATE_USERS);
                }

                result = (int)MTBFEnums.ReturnResult.FAILED;
            }

            return result;
        }


        private List<Role> GetAllUserRoles()
        {
            List<Role> lstRole = new List<Role>();
            foreach (UltraGridRow row in grvRoleInfo.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Select"].Value))
                {
                    Role role = _MTBFProxy.GetRoleByID(Convert.ToInt32(row.Cells[MTBFConstants.DataField.ROLE_ID].Value));
                    lstRole.Add(role);
                }
            }
            return lstRole;
        }


        /// <summary>
        /// Method to view users information for update.
        /// </summary>
        /// <param name="usersID"></param>
        private void ViewUsersInformation(string usersID)
        {
            Users users = null;
            users = _MTBFProxy.GetUserByUserID(usersID);

            if (users.UserID == MTBFConstants.AppConstants.LoggedinUserID || users.IsSuperAdmin == true)
            {
                cmbUserStatus.Enabled = false;
            }
            else
            {
                cmbUserStatus.Enabled = true;
            }

            cmbEmployeeName.Value = users.EmployeeID;

            txtUserName.Text = users.UserID;
            cmbUserStatus.Value = users.Status;
            cmbUserType.Value = users.UserType;
            UncheckedAllRoles();
            CheckedUserRole(users.UserID);
        }

        /// <summary>
        /// Method to checked user role in grid.
        /// </summary>
        /// <param name="userID"></param>
        private void CheckedUserRole(string userID)
        {
            foreach (UserRole role in _MTBFProxy.GetAllUserRoleByUserID(userID))
            {
                foreach (UltraGridRow row in grvRoleInfo.Rows)
                {
                    if (role.RoleID == Convert.ToInt32(row.Cells[MTBFConstants.DataField.ROLE_ID].Value))
                    {
                        row.Cells["Select"].Activation = Activation.AllowEdit;
                        row.Cells["Select"].Value = true;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Method to uncheck all role.
        /// </summary>
        private void UncheckedAllRoles()
        {
            foreach (UltraGridRow row in grvRoleInfo.Rows)
            {
                row.Cells["Select"].Activation = Activation.AllowEdit;
                row.Cells["Select"].Value = false;
            }
        }

        /// <summary>
        /// Method to load status information in combo. 
        /// </summary>
        private void LoadStatus()
        {
            Dictionary<string, int> lstUserStatus = new Dictionary<string, int>();

            foreach (int enumValue in Enum.GetValues(typeof(MTBFEnums.UserStatus)))
            {
                lstUserStatus.Add(Enum.GetName(typeof(MTBFEnums.UserStatus), enumValue), enumValue);
            }

            // lstUserStatus.Add("-Select-", 0);
            cmbUserStatus.DisplayMember = "Key";
            cmbUserStatus.ValueMember = "Value";
            cmbUserStatus.DataSource = lstUserStatus.ToList();
            cmbUserStatus.Value = (int)MTBFEnums.UserStatus.Active;
        }

        private void LoadUserTypeCombo()
        {
            Dictionary<string, int> lstUserStatus = new Dictionary<string, int>();

            foreach (int enumValue in Enum.GetValues(typeof(MTBFEnums.UserType)))
            {
                lstUserStatus.Add(Enum.GetName(typeof(MTBFEnums.UserType), enumValue), enumValue);
            }

            // lstUserStatus.Add("-Select-", 0);
            cmbUserType.DisplayMember = "Key";
            cmbUserType.ValueMember = "Value";
            cmbUserType.DataSource = lstUserStatus.ToList();
            cmbUserType.Value = (int)MTBFEnums.UserType.Other;
        }

        #endregion

    }
}
