using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Infragistics.Win.UltraWinGrid;
using NybSys.MTBF.Utility.Helper;
using NybSys.MTBF.Utility.Constants;
using IMFS.Common.DTO;
using IFMS.BLL;
using IMFS.Common.Utility;
using NybSys.MTBF.Utility.Enums;

namespace Tiles_Inventory
{
    public partial class frmViewBankAccount : BaseForm
    {
        private int _BankAccountID = 0;
        public frmViewBankAccount()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Method to create bank account table.
        /// </summary>
        /// <returns></returns>
        private DataTable CreateBankAccountTable()
        {
            DataTable table = new DataTable();

            table.Columns.Add(MTBFConstants.GridHeader.BANK_ACCOUNT_ID);
            table.Columns.Add(MTBFConstants.GridHeader.ACCOUNT_NUMBER);
            table.Columns.Add(MTBFConstants.GridHeader.BANK_NAME);
            table.Columns.Add(MTBFConstants.GridHeader.CURRENT_BALANCE);
            table.Columns.Add(MTBFConstants.GridHeader.SPACE_CHARACTER);

            return table;
        }

        /// <summary>
        /// Method to load bank account info.
        /// </summary>
        private void LoadBankAccountInfo()
        {
            DataTable bankAccountTable = this.CreateBankAccountTable();

            List<BankAccount> lstBankAccount = new List<BankAccount>();
            lstBankAccount = new BankAccountManager().GetAllBankAccount().Cast<BankAccount>().ToList();
            string filter = string.Format("{0}='{1}'", "ReferenceType", (int)MTBFEnums.ChildAccountType.Bank);
            List<ChildAccount> lstChildAccount = new ChildAccountManager().GetFilteredChildAccount(filter).Cast<ChildAccount>().ToList();



            foreach (BankAccount bankAccount in lstBankAccount)
            {
                DataRow row = bankAccountTable.NewRow();

                ChildAccount childAccount = lstChildAccount.Where(c => c.ReferenceID == bankAccount.BankAccountID).FirstOrDefault();

                row[MTBFConstants.GridHeader.BANK_ACCOUNT_ID] = bankAccount.BankAccountID;
                row[MTBFConstants.GridHeader.ACCOUNT_NUMBER] = bankAccount.BankAccountNumber;
                row[MTBFConstants.GridHeader.BANK_NAME] = bankAccount.BankName;
                row[MTBFConstants.GridHeader.CURRENT_BALANCE] = this.GetCurrentBalanceByAccountID(childAccount, bankAccount.OpeningBalance);

                row[MTBFConstants.GridHeader.SPACE_CHARACTER] = MTBFConstants.DataField.TRANSACTION_HISTORY;

                bankAccountTable.Rows.Add(row);
            }

            this.grvBankAccount.DataSource = bankAccountTable;
            this.grvBankAccount.DisplayLayout.Bands[0].Columns[MTBFConstants.GridHeader.BANK_ACCOUNT_ID].Hidden = true;

            this.grvBankAccount.DisplayLayout.Bands[0].Columns[MTBFConstants.GridHeader.SPACE_CHARACTER].CellAppearance.ForeColor = Color.RoyalBlue;
            this.grvBankAccount.DisplayLayout.Bands[0].Columns[MTBFConstants.GridHeader.SPACE_CHARACTER].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.URL;
            this.grvBankAccount.DisplayLayout.Bands[0].Columns[MTBFConstants.GridHeader.SPACE_CHARACTER].Width = 120;

            this.grvBankAccount.DisplayLayout.Override.ActiveCellAppearance.Reset();
            this.grvBankAccount.DisplayLayout.Override.ActiveRowAppearance.Reset();


        }



        /// <summary>
        /// Method to get current balance by account id.
        /// </summary>
        /// <param name="bankAccountID"></param>
        /// <returns></returns>
        private decimal GetCurrentBalanceByAccountID(ChildAccount childAccount, decimal openingBalance)
        {
            decimal debitAmount = 0;
            decimal creditAmount = 0;
            decimal balance = 0;
            if (childAccount != null)
            {
                string filter = string.Format("{0}={1} and {2}={3}", "ChildAccountID", childAccount.ChildAccountID, "BranchID", MTBFConstants.AppConstants.BranchID);

                List<Journal> lstJournal = new JournalManager().GetFilteredJournal(filter);
                List<Journal> lstDebitJournal = lstJournal.Where(j => j.DrCrIndecator == "Dr" && j.AccountID == childAccount.AccountID).ToList();

                List<Journal> lstCreditJournal = lstJournal.Where(j => j.DrCrIndecator == "Cr" && j.AccountID == childAccount.AccountID).ToList();

                debitAmount = GetTotalJournalAmount(lstDebitJournal);
                creditAmount = GetTotalJournalAmount(lstCreditJournal);

                balance = (debitAmount) - (creditAmount);
            }

            return balance;
        }

        /// <summary>
        /// Method to calculate journal amount.
        /// </summary>
        /// <param name="lstJournal"></param>
        /// <returns></returns>
        private decimal GetTotalJournalAmount(List<Journal> lstJournal)
        {
            decimal journalAmount = 0;

            foreach (Journal journal in lstJournal)
            {
                journalAmount = (journal.DrCrIndecator == "Dr") ? journalAmount + journal.Amount : journalAmount + journal.Amount;
            }

            return journalAmount;
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            if (grvBankAccount.Selected.Rows.Count > 0)
            {
                int bankAccountID = Convert.ToInt32(grvBankAccount.Selected.Rows[0].Cells[MTBFConstants.GridHeader.BANK_ACCOUNT_ID].Value);
                frmBankDeposit frm = new frmBankDeposit(bankAccountID);
                frm.OnDepositInformationLoad += new frmBankDeposit.OnDepositInformationLoadEventHandler(frm_OnDepositInformationLoad);
                frm.ShowDialog();
            }
            else
            {
                MessageBoxHelper.ShowInformation("Please select a bank account.");
            }
        }

        void frm_OnDepositInformationLoad()
        {
            LoadBankAccountInfo();
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            if (grvBankAccount.Selected.Rows.Count > 0)
            {
                int bankAccountID = Convert.ToInt32(grvBankAccount.Selected.Rows[0].Cells[MTBFConstants.GridHeader.BANK_ACCOUNT_ID].Value);
                frmBankWithdraw frm = new frmBankWithdraw(bankAccountID);
                frm.OnWithdrawInformationLoad += new frmBankWithdraw.OnWithdrawInformationLoadEventHandler(frm_OnWithdrawInformationLoad);
                frm.ShowDialog();
            }
            else
            {
                MessageBoxHelper.ShowInformation("Please select a bank account.");
            }
        }

        void frm_OnWithdrawInformationLoad()
        {
            LoadBankAccountInfo();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (grvBankAccount.Selected.Rows.Count > 0)
            {
                int bankAccountID = Convert.ToInt32(grvBankAccount.Selected.Rows[0].Cells[MTBFConstants.GridHeader.BANK_ACCOUNT_ID].Value);
                frmBankAccount frm = new frmBankAccount(bankAccountID, true);
                frm.ShowDialog();
            }
            else
            {
                MessageBoxHelper.ShowInformation("Please select a bank account.");
            }
        }

        private void frmViewBankAccount_Load(object sender, EventArgs e)
        {
            LoadBankAccountInfo();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmBankAccount frm = new frmBankAccount();
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvBankAccount_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            int bankAccountID = 0;
            UltraGridRow selectedRow = this.grvBankAccount.Rows[e.Cell.Row.Index];

            if (e.Cell.Text == MTBFConstants.DataField.TRANSACTION_HISTORY)
            {
                bankAccountID = Convert.ToInt32(selectedRow.Cells[MTBFConstants.GridHeader.BANK_ACCOUNT_ID].Value.ToString());
                frmTransactionHistory transactionHistoryForm = new frmTransactionHistory(bankAccountID);
                transactionHistoryForm.ShowDialog();
            }

            else
            {

                bankAccountID = Convert.ToInt32(selectedRow.Cells[MTBFConstants.GridHeader.BANK_ACCOUNT_ID].Value.ToString());
                string bankName = selectedRow.Cells[1].Value.ToString();
                _BankAccountID = bankAccountID;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadBankAccountInfo();
        }

    }
}
