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
using NybSys.MTBF.Utility.Helper;

namespace Tiles_Inventory
{
    public partial class frmChangePassword : BaseForm
    {
        public frmChangePassword()
        {
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        IFMSEncription objEncryptDecrypt = new IFMSEncription();

        private void btnSave_Click(System.Object sender, System.EventArgs e)
        {
            if ((CheckValidOldPassword()))
            {
                if (txtNewPassword.Text == txtConfirmPassword.Text)
                {
                    UpdatePassword();
                    ResetAllControls();
                }
                else
                {
                    MessageBoxHelper.ShowInformation("Confirm password not match.");
                    txtConfirmPassword.Focus();
                }
            }
        }

        private void ResetAllControls()
        {
            txtOldPassword.Clear();
            txtNewPassword.Clear();
            txtConfirmPassword.Clear();
        }

        private void UpdatePassword()
        {
            Users user = DataAccessProxy.GetUserByEmployeeID(IFMSConstant.LoggedinUserID);

            user.Password = objEncryptDecrypt.Encrypt(txtNewPassword.Text);
            DataAccessProxy.UpdateUser(user);

            MessageBoxHelper.ShowInformation("Password changed successfully.");
        }

        private bool CheckValidOldPassword()
        {
            if (string.IsNullOrEmpty(txtNewPassword.Text))
            {
                MessageBoxHelper.ShowInformation("You need to enter new password.");
                txtNewPassword.Focus();
                return false;
            }

            Users user = DataAccessProxy.GetUserByEmployeeID(IFMSConstant.LoggedinUserID);

            if (user != null)
            {
                if (user.Password == objEncryptDecrypt.Encrypt(txtOldPassword.Text))
                {
                    return true;
                }
                else
                {
                    MessageBoxHelper.ShowInformation("Old password not match.");
                    txtOldPassword.Focus();
                }
            }
            return false;
        }

        private void btnClose_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void frmChangePassword_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }


    }
}
