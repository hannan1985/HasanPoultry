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
using NybSys.MTBF.Utility.Constants;
using IMFS.Common.Utility;
using NybSys.MTBF.Utility.Enums;

namespace Tiles_Inventory
{
    public partial class frmTransactionHistory : BaseForm
    {
        private int _bankAccountID = 0;
        public frmTransactionHistory()
        {
            InitializeComponent();
        }

        public frmTransactionHistory(int bankAccountID)
        {
            _bankAccountID = bankAccountID;

            InitializeComponent();

        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadTransactionHistoryByDate(dtpFromDate.Value.ToString("yyyy/MM/dd"), dtpToDate.Value.ToString("yyyy/MM/dd"));
        }

        /// <summary>
        /// Method to load transaction history
        /// </summary>
        private void LoadTransactionHistoryByDate(string fromDate, string toDate)
        {

            int depositID = 0;
            int withdrawID = 0;
            fromDate = fromDate + MTBFConstants.DAY_START_TIME;
            toDate = toDate + MTBFConstants.DAY_END_TIME;

            DataTable journalTable = BuildTransactionHitoryTable();

            BankAccount bankAccount = new BankAccountManager().GetBankAccountByID(_bankAccountID);
            string filter = string.Empty;


            filter = string.Format("{0}='{1}' and {2}={3}", "ReferenceType", (int)MTBFEnums.ChildAccountType.Bank, "ReferenceID", bankAccount.BankAccountID);
            ChildAccount childAccount = new ChildAccountManager().GetFilteredChildAccount(filter).Cast<ChildAccount>().ToList().FirstOrDefault();


            filter = string.Format("{0} between '{1}' and '{2}' and {3}={4} and {5}={6}", "JournalDate", fromDate, toDate, "ChildAccountID", childAccount.ChildAccountID, "BranchID", MTBFConstants.AppConstants.BranchID);

            List<Journal> lstJournal = new JournalManager().GetFilteredJournal(filter);



            lstJournal = lstJournal.Where(b => b.AccountID == childAccount.AccountID).ToList();

            foreach (Journal journal in lstJournal)
            {
                DataRow row = journalTable.NewRow();
                BankDeposit bankDeposit = new BankAccountManager().GetBankDebositByID(journal.BankDepositID);
                BankWithdraw bankWithdraw = new BankAccountManager().GetBankWithdrawByID(journal.BankWithdrawID);
                depositID = (bankDeposit != null) ? bankDeposit.BankDepositID : 0;
                withdrawID = (bankWithdraw != null) ? bankWithdraw.WithdrawID : 0;

                string transactionDate = GetTransactionDate(bankDeposit, bankWithdraw, journal);
                row["DepositID"] = depositID;
                row["WithdrawID"] = withdrawID;
                row[MTBFConstants.GridHeader.REFERENCE_NUMBER] = journal.ReferenceNo;
                row[MTBFConstants.GridHeader.DATE] = transactionDate;
                row[MTBFConstants.GridHeader.BANK_NAME] = bankAccount.BankName;
                row["Transaction Type"] = (journal.DrCrIndecator == "Dr") ? "Deposit" : "Withdraw";
                row["Short Note"] = journal.Description; // GetShortNote(journal);
                row[MTBFConstants.GridHeader.AMOUNT] = (journal.DrCrIndecator == "Dr") ? journal.Amount : journal.Amount;
                journalTable.Rows.Add(row);
            }

            grvTransactionHistory.DataSource = journalTable;
            grvTransactionHistory.DisplayLayout.Bands[0].Columns[MTBFConstants.GridHeader.REFERENCE_NUMBER].Hidden = true;
            grvTransactionHistory.DisplayLayout.Bands[0].Columns["DepositID"].Hidden = true;
            grvTransactionHistory.DisplayLayout.Bands[0].Columns["WithdrawID"].Hidden = true;
            if (grvTransactionHistory.Rows.Count == 0)
            {
                MessageBoxHelper.ShowInformation("No data found");
            }
        }

        private string GetTransactionDate(BankDeposit bankDeposit, BankWithdraw bankWithdraw, Journal journal)
        {
            string transactionDate = string.Empty;

            if (journal.BankDepositID > 0)
            {
                transactionDate = bankDeposit.DepositDate.ToString("dd/MM/yyyy");
            }
            else if (journal.BankWithdrawID > 0)
            {
                transactionDate = bankWithdraw.WithdrawDate.ToString("dd/MM/yyyy");
            }
            else
            {
                transactionDate = journal.JournalDate.ToString("dd/MM/yyyy");
            }

            return transactionDate;
        }


        private string GetShortNote(Journal journal)
        {
            string shortNote = string.Empty;
            if (journal.BankDepositID > 0)
            {
                BankDeposit bankDeposit = new BankAccountManager().GetBankDebositByID(journal.BankDepositID);
                shortNote = (bankDeposit != null) ? bankDeposit.ShortNote : string.Empty;
            }
            else if (journal.BankWithdrawID > 0)
            {
                BankWithdraw bankWithdraw = new BankAccountManager().GetBankWithdrawByID(journal.BankWithdrawID);
                shortNote = (bankWithdraw != null) ? bankWithdraw.ShortNote : string.Empty;
            }
            else
            {
                shortNote = "Opening Balance";
            }

            return shortNote;
        }

        /// <summary>
        /// Method to  build transaction history table.
        /// </summary>
        /// <returns></returns>
        private DataTable BuildTransactionHitoryTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("DepositID");
            table.Columns.Add("WithdrawID");
            table.Columns.Add(MTBFConstants.GridHeader.REFERENCE_NUMBER);
            table.Columns.Add(MTBFConstants.GridHeader.DATE);
            table.Columns.Add(MTBFConstants.GridHeader.BANK_NAME);
            table.Columns.Add("Transaction Type");
            table.Columns.Add("Short Note");
            table.Columns.Add(MTBFConstants.GridHeader.AMOUNT);

            return table;
        }

        private void frmTransactionHistory_Load(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int depositID = Convert.ToInt32(grvTransactionHistory.Selected.Rows[0].Cells["DepositID"].Value);
            int withdrawID = Convert.ToInt32(grvTransactionHistory.Selected.Rows[0].Cells["WithdrawID"].Value);
            if (grvTransactionHistory.Selected.Rows.Count > 0)
            {
                if (depositID == 0 && withdrawID == 0)
                {
                    MessageBoxHelper.ShowInformation("You can not edit this data.");
                }
                else if (depositID > 0)
                {
                    frmBankDeposit frm = new frmBankDeposit(_bankAccountID, true, depositID);
                    frm.ShowDialog();
                }
                else if (withdrawID > 0)
                {
                    frmBankWithdraw frm = new frmBankWithdraw(_bankAccountID, true, withdrawID);
                    frm.ShowDialog();
                }
            }
            else
            {
                MessageBoxHelper.ShowInformation("You need to select a row.");
            }

        }


    }
}
