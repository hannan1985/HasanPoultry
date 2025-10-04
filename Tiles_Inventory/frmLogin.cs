using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IMFS.Common.Utility;
using IFMS.Facade;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Constants;
using NybSys.MTBF.Utility.Helper;
using NybSys.MTBF.Utility.Resources;
using NybSys.MTBF.Utility.Enums;

namespace Tiles_Inventory
{
    public partial class frmLogin : BaseForm
    {
        public frmLogin()
        {
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }



        private void btnLogin_Click(System.Object sender, System.EventArgs e)
        {
            if (ValidateForm())
            {
                if (IsLogin())
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        private bool IsLogin()
        {
            bool loginValid = false;
            int trialperiod = 0;
            int CheckPeriod = 0;
            DateTime CheckDate = default(System.DateTime);
            try
            {
                IFMSEncription objEncryptDecrypt = new IFMSEncription();
                TrialPeriodInformation objtrialPeriod = DataAccessProxy.GetAllTrialPeriod();

                if (objtrialPeriod != null)
                {
                    trialperiod = Convert.ToInt32(objEncryptDecrypt.Decrypt(objtrialPeriod.TrialPeriod));
                    CheckPeriod = Convert.ToInt32(objEncryptDecrypt.Decrypt(objtrialPeriod.CheckPeriod));
                    CheckDate = Convert.ToDateTime(objEncryptDecrypt.Decrypt(objtrialPeriod.CheckDate));
                }

                string date = System.DateTime.Now.ToShortDateString();
                System.DateTime CompareDate = Convert.ToDateTime(date);

                if (trialperiod <= CheckPeriod & CheckDate != CompareDate)
                {
                    MessageBoxHelper.ShowInformation("Invalid userid or password**");
                }
                else
                {
                    if (CheckDate.ToString("yyyy/MM/dd") != DateTime.Now.ToString("yyyy/MM/dd"))
                    {
                        CheckPeriod = CheckPeriod + 1;
                        dynamic strPeriod = objEncryptDecrypt.Encrypt(CheckPeriod.ToString());
                        dynamic strDate = objEncryptDecrypt.Encrypt(System.DateTime.Now.ToString("yyyy/MM/dd"));

                        /*Update Trial Period Information.*/
                        objtrialPeriod.CheckPeriod = strPeriod;
                        objtrialPeriod.CheckDate = strDate;
                        DataAccessProxy.UpdateTrialPeriod(objtrialPeriod);
                    }

                    string password = objEncryptDecrypt.Encrypt(txtPassword.Text);
                    string username = txtUserName.Text;
                    Users user = DataAccessProxy.GetUserByUserNameAndPassword(username, password);

                    if (user != null && user.Status == (int)MTBFEnums.UserStatus.Active)
                    {
                        MTBFConstants.AppConstants.LoggedinUser = user;
                        if (user.UserType == (int)MTBFEnums.UserType.Admin)
                        {
                            loginValid = true;
                            MTBFConstants.AppConstants.LoggedinUserID = user.UserID;
                            IFMSConstant.LoggedinUserID = user.EmployeeID;
                            Organization organization = DataAccessProxy.GetAllPharmacy().Cast<Organization>().ToList().FirstOrDefault();
                            MTBFConstants.AppConstants.OrganizationID = organization.OrganizationID;
                            MTBFConstants.AppConstants.BranchID = Convert.ToInt32(cmbBranchName.Value);
                        }
                        else
                        {
                            if (user.BranchID == Convert.ToInt32(cmbBranchName.Value))
                            {
                                loginValid = true;
                                MTBFConstants.AppConstants.LoggedinUserID = user.UserID;
                                IFMSConstant.LoggedinUserID = user.EmployeeID;
                                Organization organization = DataAccessProxy.GetAllPharmacy().Cast<Organization>().ToList().FirstOrDefault();
                                MTBFConstants.AppConstants.OrganizationID = organization.OrganizationID;
                                MTBFConstants.AppConstants.BranchID = Convert.ToInt32(cmbBranchName.Value);
                            }
                            else
                            {
                                MessageBoxHelper.ShowInformation("You have no permission for this branch.");
                            }
                        }
                    }
                    else
                    {

                        MessageBoxHelper.ShowInformation("Invalid user name or password.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxHelper.ShowInformation(ex.Message);
            }

            return loginValid;
        }


        private void btnClose_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }


        private void txtPassword_KeyUp(System.Object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 13)
            {
                if (IsLogin())
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        /// <summary>
        /// Method to validate form data.
        /// </summary>
        /// <returns></returns>
        private bool ValidateForm()
        {
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                MessageBoxHelper.ShowWarning(SystemMessages.ENTER_USER_ID);
                txtUserName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBoxHelper.ShowWarning(SystemMessages.ENTER_PASSWORD);
                txtPassword.Focus();
                return false;
            }

            if (cmbBranchName.Value == null || Convert.ToInt32(cmbBranchName.Value) == MTBFConstants.DataField.COMBO_DEFAULT_ID)
            {
                MessageBoxHelper.ShowWarning("You need to select branch name");
                cmbBranchName.Focus();
                return false;
            }

            return true;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            LoadBranchInformation();
        }


        private void LoadBranchInformation()
        {
            List<Branch> lstBranch = new List<Branch>();

            Branch branch = new Branch();
            branch.BranchID = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            branch.BranchName = MTBFConstants.DataField.COMBO_DEFAULT_NAME;

            lstBranch = DataAccessProxy.GetAllBranch().Cast<Branch>().ToList();
            lstBranch.Insert(0, branch);

            UIHelper.SetComboBoxData(cmbBranchName, lstBranch, "BranchName", "BranchID");
        }

    }
}
