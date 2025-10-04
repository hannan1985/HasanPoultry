using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IMFS.Common.DTO;
using IFMS.BLL;
using NybSys.MTBF.Utility.Constants;
using NybSys.MTBF.Utility.Enums;



namespace Tiles_Inventory
{
    public partial class frmJournalViewForProduction : BaseForm
    {
        public frmJournalViewForProduction()
        {
            InitializeComponent();
        }

        private void btLoad_Click(object sender, EventArgs e)
        {
            LoadJournalInformation(dtpFromDate.Value.ToString(MTBFConstants.DateFormat) + MTBFConstants.DAY_START_TIME, dtpToDate.Value.ToString(MTBFConstants.DateFormat) + MTBFConstants.DAY_END_TIME);
            ShowGroupBy();
        }

        /// <summary>
        /// Method to shwo group by in grid .
        /// </summary>
        private void ShowGroupBy()
        {
            var band = grvJournalInformaiton.DisplayLayout.Bands[0];
            var sortedColumns = band.SortedColumns;
            sortedColumns.Add("Reference No", false, true);
        }

        private void LoadJournalInformation(string fromDate, string toDate)
        {
            List<Journal> lstJournal = new List<Journal>();
            lstJournal = new JournalManager().GetAllJournalByDate(fromDate, toDate).Cast<Journal>().OrderBy(j => j.ReferenceNo).ToList();
            lstJournal = lstJournal.Where(j => j.JournalType == (int)MTBFEnums.JournalType.Production).ToList();

            string strFilter = string.Empty;
            DataTable table = BuildJournalTable();
            if (lstJournal.Count > 0)
            {

                foreach (Journal journal in lstJournal)
                {
                    string accountName = string.Empty;
                    ParentAccount detailItem = new JournalManager().GetParentAccountByID(journal.AccountID);
                    accountName = (detailItem != null) ? detailItem.AccountsName : string.Empty;
                    if (journal.ExpenseID > 0 && journal.DrCrIndecator == "Dr")
                    {
                        Expense expense = new ExpenseManager().GetExpenseByID(journal.ExpenseID);
                        ExpenseType expenseType = new ExpenseManager().GetExpenseTypeByID(expense.ExpenseType);
                        accountName = (expenseType != null) ? expenseType.ExpenseTypeName : string.Empty;
                    }

                    DataRow row = table.NewRow();
                    row["Reference No"] = journal.ReferenceNo;
                    row["Account Name"] = accountName;
                    row["Debit Amount"] = (journal.DrCrIndecator == "Dr") ? journal.Amount : 0;
                    row["Credit Amount"] = (journal.DrCrIndecator == "Cr") ? journal.Amount : 0; ;
                    table.Rows.Add(row);
                }
            }

            grvJournalInformaiton.DataSource = table;
        }




        private DataTable BuildJournalTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Reference No");
            table.Columns.Add("Account Name");
            table.Columns.Add("Debit Amount");
            table.Columns.Add("Credit Amount");
            return table;

        }

        private void rbExpandAll_CheckedChanged(object sender, EventArgs e)
        {
            grvJournalInformaiton.Rows.ExpandAll(true);
        }

        private void rbCollapseAll_CheckedChanged(object sender, EventArgs e)
        {
            grvJournalInformaiton.Rows.CollapseAll(true);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }




    }
}
