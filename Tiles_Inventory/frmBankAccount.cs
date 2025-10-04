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
using IMFS.Common.DTO;
using IFMS.BLL;


namespace Tiles_Inventory
{
    public partial class frmBankAccount : BaseForm
    {
        private int _bankAccountID = 0;
        BankAccount _bankAccount = new BankAccount();

        public frmBankAccount()
        {
            InitializeComponent();
        }

        public frmBankAccount(int bankAccountID, bool isEdit)
        {
            IsUpdating = isEdit;
            _bankAccountID = bankAccountID;
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidFormData())
            {

                if (InsertBankAccount() == (int)MTBFEnums.ReturnResult.SUCCESS)
                {
                    MessageBoxHelper.ShowInformation("Bank account information saved successfully.");
                    ResetAllControls();
                }
                else
                {
                    MessageBoxHelper.ShowInformation("Failed to save bank account information.");
                }

            }


        }

        private bool ValidFormData()
        {
            decimal openingBalance = 0;


            if (string.IsNullOrEmpty(txtBankName.Text))
            {
                MessageBoxHelper.ShowInformation("You need to enter bank name.");
                txtBankName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtAccountNumber.Text))
            {
                MessageBoxHelper.ShowInformation("You need to enter bank account number.");
                txtAccountNumber.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(cmbAccountType.Text))
            {
                MessageBoxHelper.ShowInformation("You need to select account type.");
                cmbAccountType.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtBankName.Text))
            {
                MessageBoxHelper.ShowInformation("You need to enter bank name.");
                txtBankName.Focus();
                return false;
            }
            //if (string.IsNullOrEmpty(txtOpeaningBalance.Text))
            //{
            //    MessageBoxHelper.ShowInformation("You need to enter opening balance.");
            //    txtOpeaningBalance.Focus();
            //    return false;
            //}


            decimal.TryParse(txtOpeaningBalance.Text, out openingBalance);

            //if (openingBalance == 0)
            //{
            //    MessageBoxHelper.ShowInformation("Opening balance can't be zero.");
            //    txtOpeaningBalance.Focus();
            //    return false;
            //}


            return true;
        }



        private void ResetAllControls()
        {
            txtBankName.Clear();
            txtBranch.Clear();
            txtAddress.Clear();
            txtManagerName.Clear();
            txtAccountNumber.Clear();
            txtOpeaningBalance.Clear();
            txtResponsiblePerson.Clear();
            dtpOpenDate.Value = DateTime.Now;
            txtBankName.Focus();
        }


        private int InsertBankAccount()
        {
            decimal balance = 0;
            decimal.TryParse(txtOpeaningBalance.Text, out balance);
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            _bankAccount.BankName = txtBankName.Text;
            _bankAccount.Branch = txtBranch.Text;
            _bankAccount.Address = txtAddress.Text;
            _bankAccount.ResponsiblePerson = txtResponsiblePerson.Text;
            _bankAccount.BankAccountNumber = txtAccountNumber.Text;
            _bankAccount.ManagerName = txtManagerName.Text;
            _bankAccount.AccountType = cmbAccountType.Text;
            _bankAccount.OpeningBalance = balance;
            _bankAccount.OpeningDate = dtpOpenDate.Value;
            result = new BankAccountManager().SaveBankAccount(_bankAccount);
            return result;
        }



        private void txtOpeaningBalance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ("-1234567890".IndexOf(e.KeyChar) > -1 | e.KeyChar == Convert.ToChar(8))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void frmBankAccount_Load(object sender, EventArgs e)
        {
            if (IsUpdating)
            {
                LoadExistingBankInformation();
            }
        }

        private void LoadExistingBankInformation()
        {
            _bankAccount = new BankAccountManager().GetBankAccountByID(_bankAccountID);
            txtBankName.Text = _bankAccount.BankName;
            txtAddress.Text = _bankAccount.Address;
            txtBranch.Text = _bankAccount.Branch;
            txtResponsiblePerson.Text = _bankAccount.ResponsiblePerson;
            txtAccountNumber.Text = _bankAccount.BankAccountNumber;
            txtManagerName.Text = _bankAccount.ManagerName;
            cmbAccountType.Text = _bankAccount.AccountType;
            txtOpeaningBalance.Text = _bankAccount.OpeningBalance.ToString();
            dtpOpenDate.Value = _bankAccount.OpeningDate;
        }

    }
}
