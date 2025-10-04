using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IMFS.Common.DTO;
using IFMS.BLL;
using NybSys.MTBF.Utility.Helper;
using NybSys.MTBF.Utility.Enums;
using NybSys.MTBF.Utility.Constants;


namespace Tiles_Inventory
{
    public partial class frmBankDeposit : BaseForm
    {
        int _bankAccountID = 0;
        int _bankDepositID = 0;
        public event OnDepositInformationLoadEventHandler OnDepositInformationLoad;
        public delegate void OnDepositInformationLoadEventHandler();
        private bool isUpdating = false;

        public frmBankDeposit()
        {

            InitializeComponent();
        }


        public frmBankDeposit(int bankAccountID)
        {
            _bankAccountID = bankAccountID;
            InitializeComponent();
        }

        public frmBankDeposit(int bankAccountID, bool isEdit, int depositID)
        {
            isUpdating = isEdit;
            _bankAccountID = bankAccountID;
            _bankDepositID = depositID;
            InitializeComponent();
        }

        private void frmBankDeposit_Load(object sender, EventArgs e)
        {
            BankAccount bankAccount = new BankAccountManager().GetBankAccountByID(_bankAccountID);
            txtBankName.Text = bankAccount.BankName;
            txtAccountNumber.Text = bankAccount.BankAccountNumber;
            txtDepositedAmount.Focus();
            if (isUpdating)
            {
                LoadExistingData();

            }
        }

        private void LoadExistingData()
        {
            BankDeposit bankDeposit = new BankDeposit();
            bankDeposit = new BankAccountManager().GetBankDebositByID(_bankDepositID);
            txtDepositedAmount.Text = bankDeposit.DepositAmount.ToString();
            txtShortNote.Text = bankDeposit.ShortNote;
            dtpDepositDate.Value = bankDeposit.DepositDate;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidFormData())
            {
                if (isUpdating)
                {
                    if (UpdateBankDeposit() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Information saved successfully.");
                        this.Close();
                        if (OnDepositInformationLoad != null)
                            OnDepositInformationLoad();
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to saved information.");
                    }
                }
                else
                {
                    if (InsertBankDeposit() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Information saved successfully.");
                        this.Close();
                        if (OnDepositInformationLoad != null)
                            OnDepositInformationLoad();
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to saved information.");
                    }
                }                
            }
        }

        private int UpdateBankDeposit()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            BankDeposit bankDeposit = new BankAccountManager().GetBankDebositByID(_bankDepositID);
            bankDeposit.BankAccountID = _bankAccountID;
            bankDeposit.BankAccountNumber = txtAccountNumber.Text;
            bankDeposit.DepositAmount = Convert.ToDecimal(txtDepositedAmount.Text);
            bankDeposit.ShortNote = txtShortNote.Text;
            bankDeposit.DepositDate = dtpDepositDate.Value;
            bankDeposit.BranchID = MTBFConstants.AppConstants.BranchID;
            bankDeposit.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            result = new BankAccountManager().UpdateBankDeposit(bankDeposit);

            return result;
        }

        /// <summary>
        /// Method to insert bank withdraw information.
        /// </summary>
        /// <returns></returns>
        private int InsertBankDeposit()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            BankDeposit bankDeposit = new BankDeposit();
            bankDeposit.BankAccountID = _bankAccountID;
            bankDeposit.BankAccountNumber = txtAccountNumber.Text;
            bankDeposit.DepositAmount = Convert.ToDecimal(txtDepositedAmount.Text);
            bankDeposit.ShortNote = txtShortNote.Text;
            bankDeposit.DepositDate = dtpDepositDate.Value;
            bankDeposit.BranchID = MTBFConstants.AppConstants.BranchID;
            bankDeposit.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            result = new BankAccountManager().InsertBankDeposit(bankDeposit);

            return result;
        }

        private bool ValidFormData()
        {
            decimal amount = 0;


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


            if (string.IsNullOrEmpty(txtBankName.Text))
            {
                MessageBoxHelper.ShowInformation("You need to enter bank name.");
                txtBankName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtDepositedAmount.Text))
            {
                MessageBoxHelper.ShowInformation("You need to enter amount.");
                txtDepositedAmount.Focus();
                return false;
            }


            decimal.TryParse(txtDepositedAmount.Text, out amount);

            if (amount == 0)
            {
                MessageBoxHelper.ShowInformation("Amount can't be zero.");
                txtDepositedAmount.Focus();
                return false;
            }


            return true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
