using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NybSys.MTBF.Utility.Helper;
using IMFS.Common.DTO;
using IFMS.BLL;
using NybSys.MTBF.Utility.Enums;
using NybSys.MTBF.Utility.Constants;


namespace Tiles_Inventory
{
    public partial class frmBankWithdraw : BaseForm
    {
        int _bankAccountID = 0;
        public event OnWithdrawInformationLoadEventHandler OnWithdrawInformationLoad;
        public delegate void OnWithdrawInformationLoadEventHandler();
        private bool isUpdating = false;
        int _bankWithdrawID = 0;

        public frmBankWithdraw()
        {
           
            InitializeComponent();
        }

        public frmBankWithdraw(int bankAccountID)
        {            
            _bankAccountID = bankAccountID;
            InitializeComponent();
        }

        public frmBankWithdraw(int bankAccountID, bool isEdit, int bankWithdrawID)
        {
            _bankAccountID = bankAccountID;
            _bankWithdrawID = bankWithdrawID;
            isUpdating = isEdit;
            InitializeComponent();
        }

        private void frmBankWithdraw_Load(object sender, EventArgs e)
        {
            BankAccount bankAccount = new  BankAccountManager().GetBankAccountByID(_bankAccountID);
            txtBankName.Text = bankAccount.BankName;
            txtAccountNumber.Text = bankAccount.BankAccountNumber;
            txtWithdrawAmount.Focus();
            if (isUpdating)
            {
                LoadExistingData();
            }
        }

        private void LoadExistingData()
        {
            BankWithdraw bankWithdraw = new BankWithdraw();
            bankWithdraw = new BankAccountManager().GetBankWithdrawByID(_bankWithdrawID);
            txtWithdrawAmount.Text = bankWithdraw.WithdrawAmount.ToString();
            txtShortNote.Text = bankWithdraw.ShortNote;
            dtpWithdrawDate.Value = bankWithdraw.WithdrawDate;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidFormData())
            {
                if (isUpdating)
                {
                    if (UpdateBankWithdraw() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Information saved successfully.");

                        this.Close();
                        if (OnWithdrawInformationLoad != null)
                            OnWithdrawInformationLoad();

                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to saved information.");
                    }
                }
                else
                {
                    if (InsertBankWithdraw() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Information saved successfully.");

                        this.Close();
                        if (OnWithdrawInformationLoad != null)
                            OnWithdrawInformationLoad();

                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to saved information.");
                    }
                }


                
            }
        }

        private int UpdateBankWithdraw()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            BankWithdraw bankWithdraw = new BankAccountManager().GetBankWithdrawByID(_bankWithdrawID);
            bankWithdraw.BankAccountID = _bankAccountID;
            bankWithdraw.BankAccountNumber = txtAccountNumber.Text;
            bankWithdraw.WithdrawAmount = Convert.ToDecimal(txtWithdrawAmount.Text);
            bankWithdraw.ShortNote = txtShortNote.Text;
            bankWithdraw.WithdrawDate = dtpWithdrawDate.Value;
            bankWithdraw.BranchID = MTBFConstants.AppConstants.BranchID;
            bankWithdraw.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            result = new BankAccountManager().UpdateBankWithdraw(bankWithdraw);

            return result;
        }

        /// <summary>
        /// Method to insert bank withdraw information.
        /// </summary>
        /// <returns></returns>
        private int InsertBankWithdraw()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            BankWithdraw bankWithdraw = new BankWithdraw();
            bankWithdraw.BankAccountID = _bankAccountID;
            bankWithdraw.BankAccountNumber = txtAccountNumber.Text;
            bankWithdraw.WithdrawAmount = Convert.ToDecimal(txtWithdrawAmount.Text);
            bankWithdraw.ShortNote = txtShortNote.Text;
            bankWithdraw.WithdrawDate = dtpWithdrawDate.Value;
            bankWithdraw.BranchID = MTBFConstants.AppConstants.BranchID;
            bankWithdraw.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            result = new  BankAccountManager().InsertBankWithdraw(bankWithdraw);

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
            if (string.IsNullOrEmpty(txtWithdrawAmount.Text))
            {
                MessageBoxHelper.ShowInformation("You need to enter amount.");
                txtWithdrawAmount.Focus();
                return false;
            }


            decimal.TryParse(txtWithdrawAmount.Text, out amount);

            if (amount == 0)
            {
                MessageBoxHelper.ShowInformation("Amount can't be zero.");
                txtWithdrawAmount.Focus();
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
