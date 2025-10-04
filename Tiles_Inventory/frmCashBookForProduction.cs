using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;

using NybSys.MTBF.Utility.Enums;
using NybSys.MTBF.Utility.Helper;

using System.Globalization;
using NybSys.MTBF.Utility.Constants;
using IMFS.Common.DTO;
using IFMS.BLL;
using Tiles_Inventory.Reports;

namespace Tiles_Inventory
{
    public partial class frmCashBookForProduction : BaseForm
    {
        List<CashBook> lstCashBook = new List<CashBook>();



        public frmCashBookForProduction()
        {
            InitializeComponent();
        }

        private void frmCashBook_Load(object sender, EventArgs e)
        {

        }


        private DataTable BuildReceiveTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Receive Date");
            table.Columns.Add("Line Item");
            table.Columns.Add("Amount");

            return table;
        }

        private void LoadCashReceiveInformation(string fromDate, string toDate)
        {


            string filter = string.Format("{0} between '{1}' and '{2}'", "ReceiveDate", fromDate, toDate);

            List<CashReceive> lstCashReceive = new CashReceiveManager().GetFilteredCashReceive(filter).Cast<CashReceive>().ToList();
            foreach (CashReceive cashReceive in lstCashReceive)
            {
                //DataRow row = cashReceivetable.NewRow();
                //row["Receive Date"] = cashReceive.ReceiveDate.ToString("yyyy/MM/dd");
                //row["Line Item"] = "Cash Receive";
                //row["Amount"] = cashReceive.Amount;
                //cashReceivetable.Rows.Add(row);

                UltraGridRow row = grvCashReceveInfo.DisplayLayout.Bands[0].AddNew();
                row.Cells["Receive Date"].Value = cashReceive.ReceiveDate.ToString("yyyy/MM/dd");
                row.Cells["Line Item"].Value = "Cash Receive";
                row.Cells["Amount"].Value = cashReceive.Amount;

            }


            CalculateReceiveAmount();
        }

        //private void LoadFeeReceiveInformation(string fromDate, string toDate)
        //{          
        //    List<FeeReceive> lstCashReceive = new FeeReceiveManager().GetAllFeeReceiveByDate(fromDate, toDate).Cast<FeeReceive>().ToList();
        //    foreach (FeeReceive cashReceive in lstCashReceive)
        //    {
        //        UltraGridRow row = grvCashReceveInfo.DisplayLayout.Bands[0].AddNew();
        //        row.Cells["Receive Date"].Value = cashReceive.ReceiveDate.ToString("yyyy/MM/dd");
        //        row.Cells["Line Item"].Value = "Student Collection";
        //        row.Cells["Amount"].Value = cashReceive.ReceiveAmount;             
        //    }


        //    CalculateReceiveAmount();
        //}


        /// <summary>
        /// Method to build expense table.
        /// </summary>
        /// <returns></returns>
        private DataTable BuildExpenseTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Expense Date");
            table.Columns.Add("Line Item");
            table.Columns.Add("Amount");
            return table;
        }


        private void CalculateReceiveAmount()
        {
            decimal total = 0;
            foreach (UltraGridRow row in grvCashReceveInfo.Rows)
            {
                total = total + Convert.ToDecimal(row.Cells["Amount"].Text);
            }
            txtReceiveTotal.Text = total.ToString("F2");
            txtTotalIncome.Text = total.ToString("F2");
        }

        private void CalculateExpenseAmount()
        {
            decimal total = 0;
            foreach (UltraGridRow row in grvExpenseInformation.Rows)
            {
                total = total + Convert.ToDecimal(row.Cells["Amount"].Text);
            }
            txtExpenseTotal.Text = total.ToString("F2");
            txtTotalExpense.Text = total.ToString("F2");
        }



        private void LoadPaymentInformation(string fromDate, string toDate)
        {
            List<Payment> lstPayment = new List<Payment>();
            string filter = string.Format("{0} between '{1}' and '{2}' And BranchID={3} and OrganizationID={4} ", "PaymentDate", fromDate, toDate, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID);
            lstPayment = new PaymentManager().GetFilteredPaymentInformation(filter).Cast<Payment>().ToList();

            if (lstPayment.Count > 0)
            {
                foreach (Payment payment in lstPayment)
                {
                    UltraGridRow row = grvExpenseInformation.DisplayLayout.Bands[0].AddNew();
                    row.Cells["Expense Date"].Value = payment.PaymentDate.ToString("dd/MM/yyyy");
                    row.Cells["Line Item"].Value = "Cash Payment";
                    row.Cells["Amount"].Value = payment.Amount;

                }
            }

            CalculateExpenseAmount();
        }



        private void LoadExpenseInformation(string fromDate, string toDate)
        {

            List<Expense> lstExpense = new List<Expense>();
            lstExpense = new ExpenseManager().GetAllExpenseByDate(fromDate, toDate).Cast<Expense>().ToList();
            if (lstExpense.Count > 0)
            {
                foreach (Expense expense in lstExpense)
                {
                    Employee employee = new EmployeeManager().GetEmployeeByID(expense.EmployeeID);
                    if (employee != null)
                    {
                        ExpenseType expenseType = new ExpenseManager().GetExpenseTypeByID(expense.ExpenseType);
                        UltraGridRow row = grvExpenseInformation.DisplayLayout.Bands[0].AddNew();
                        row.Cells["Expense Date"].Value = expense.ExpenseDate.ToString("dd/MM/yyyy");
                        row.Cells["Line Item"].Value = (expenseType != null) ? expenseType.ExpenseTypeName : string.Empty;
                        row.Cells["Amount"].Value = expense.Amount;

                    }
                }

            }

            CalculateExpenseAmount();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            decimal totalIncome = 0;
            decimal totalExpense = 0;

            decimal openingBalance = 0;
            grvExpenseInformation.DataSource = BuildExpenseTable();
            grvCashReceveInfo.DataSource = BuildReceiveTable();

            string fromDate = dtpFromDate.Value.ToString("yyyy/MM/dd") + MTBFConstants.DAY_START_TIME;
            string toDate = dtpToDate.Value.ToString("yyyy/MM/dd") + MTBFConstants.DAY_END_TIME;

            LoadCashReceiveInformation(fromDate, toDate);
            LoadSalesCashReceiveInformation(fromDate, toDate);
            LoadBankWithdrawInformationByDate(fromDate, toDate);

            LoadExpenseInformation(fromDate, toDate);
            LoadBankDepositInformation(fromDate, toDate);
            LoadPaymentInformation(fromDate, toDate);

            CalcualateOpeningBalance();

            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

            decimal.TryParse(txtOpeningBalance.Text, out openingBalance);
            decimal.TryParse(txtTotalIncome.Text, out totalIncome);
            decimal.TryParse(txtTotalExpense.Text, out totalExpense);
            txtTotalCash.Text = (openingBalance + totalIncome).ToString("N", nfi); ;
            txtBalance.Text = ((openingBalance + totalIncome) - totalExpense).ToString("N", nfi); ;
        }



        private void LoadSalesCashReceiveInformation(string fromDate, string toDate)
        {
            string filter = string.Format("{0} between '{1}' and '{2}'", "SalesDate", fromDate, toDate);

            List<SalesOrder> lstSalesOrder = new SalesManager().GetFilteredSalesInformation(filter).Cast<SalesOrder>().ToList();
            foreach (SalesOrder salesOrder in lstSalesOrder)
            {
                if (salesOrder.ReceiveAmount > 0)
                {
                    UltraGridRow row = grvCashReceveInfo.DisplayLayout.Bands[0].AddNew();
                    row.Cells["Receive Date"].Value = salesOrder.SalesDate.ToString("yyyy/MM/dd");
                    row.Cells["Line Item"].Value = "Receive from Sales";
                    row.Cells["Amount"].Value = salesOrder.ReceiveAmount;
                }
            }


            CalculateReceiveAmount();
        }

        private void LoadBankDepositInformation(string fromDate, string toDate)
        {
            DataTable table = BuildExpenseTable();
            List<BankDeposit> lstBankDeposit = new List<BankDeposit>();
            lstBankDeposit = new BankAccountManager().GetAllBankDepositByDate(fromDate, toDate).Cast<BankDeposit>().ToList();

            if (lstBankDeposit.Count > 0)
            {
                foreach (BankDeposit bankDeposit in lstBankDeposit)
                {
                    BankAccount bankAccount = new BankAccountManager().GetBankAccountByID(bankDeposit.BankAccountID);
                    UltraGridRow row = grvExpenseInformation.DisplayLayout.Bands[0].AddNew();
                    row.Cells["Expense Date"].Value = bankDeposit.DepositDate.ToString("dd/MM/yyyy");
                    row.Cells["Line Item"].Value = bankDeposit.ShortNote;
                    row.Cells["Amount"].Value = bankDeposit.DepositAmount;

                }
            }

            CalculateExpenseAmount();
        }


        private void LoadBankWithdrawInformationByDate(string fromDate, string toDate)
        {

            List<BankWithdraw> lstBankWithdraw = new BankAccountManager().GetAllBankWithdrawByDate(fromDate, toDate).Cast<BankWithdraw>().ToList();
            foreach (BankWithdraw bankWithdraw in lstBankWithdraw)
            {
                BankAccount bankAccount = new BankAccountManager().GetBankAccountByID(bankWithdraw.BankAccountID);

                UltraGridRow row = grvCashReceveInfo.DisplayLayout.Bands[0].AddNew();
                row.Cells["Receive Date"].Value = bankWithdraw.WithdrawDate.ToString("yyyy/MM/dd");
                row.Cells["Line Item"].Value = bankWithdraw.ShortNote;
                row.Cells["Amount"].Value = bankWithdraw.WithdrawAmount;

            }
            CalculateReceiveAmount();
        }

        private void CalcualateOpeningBalance()
        {
            decimal receiveAmount = 0;
            decimal expenseAmount = 0;


            List<CashReceive> lstCashReceive = new CashReceiveManager().GetAllCashReceive().Cast<CashReceive>().Where(c => c.ReceiveDate < dtpFromDate.Value.AddDays(-1)).ToList();

            List<BankWithdraw> lstBankWithdraw = new BankAccountManager().GetAllBankWithdraw().Cast<BankWithdraw>().Where(c => c.WithdrawDate < dtpFromDate.Value.AddDays(-1)).ToList();


            List<Expense> lstExpense = new ExpenseManager().GetAllExpense().Cast<Expense>().Where(c => c.ExpenseDate < dtpFromDate.Value.AddDays(-1)).ToList();

            List<BankDeposit> lstBankDeposit = new List<BankDeposit>();
            lstBankDeposit = new BankAccountManager().GetAllBankDeposit().Cast<BankDeposit>().Where(c => c.DepositDate < dtpFromDate.Value.AddDays(-1)).ToList();

            List<SalesOrder> lstSalesOrder = new SalesManager().GetAllSalesOrderByBranchAndOrganization(MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<SalesOrder>().Where(c => c.SalesDate < dtpFromDate.Value.AddDays(-1)).ToList();

            List<Payment> lstPayment = new List<Payment>();
            string filter = string.Format("BranchID={0} and OrganizationID={1} ", MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID);
            lstPayment = new PaymentManager().GetFilteredPaymentInformation(filter).Cast<Payment>().ToList();
            lstPayment = lstPayment.Cast<Payment>().Where(c => c.PaymentDate < dtpFromDate.Value.AddDays(-1)).ToList();


            foreach (CashReceive cashReceive in lstCashReceive)
            {
                receiveAmount = receiveAmount + cashReceive.Amount;
            }

            foreach (SalesOrder salesOrder in lstSalesOrder)
            {
                receiveAmount = receiveAmount + salesOrder.ReceiveAmount;
            }

            foreach (BankWithdraw bWithdraw in lstBankWithdraw)
            {
                BankAccount bankAccount = new BankAccountManager().GetBankAccountByID(bWithdraw.BankAccountID);

                receiveAmount = receiveAmount + bWithdraw.WithdrawAmount;

            }

            foreach (Expense expense in lstExpense)
            {
                expenseAmount = expenseAmount + expense.Amount;
            }


            foreach (Payment  payment in lstPayment )
            {
                expenseAmount = expenseAmount + payment.Amount;
            }

            foreach (BankDeposit bDeposit in lstBankDeposit)
            {
                BankAccount bankAccount = new BankAccountManager().GetBankAccountByID(bDeposit.BankAccountID);

                expenseAmount = expenseAmount + bDeposit.DepositAmount;

            }

            //foreach (PayTable payment in lstPayTable)
            //{
            //    expenseAmount = expenseAmount + payment.Amount;
            //}

            //foreach (ReceiveEntry rEntry in lstReceiveEntry)
            //{
            //    expenseAmount = expenseAmount + rEntry.ReceivableAmount;
            //}

            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

            txtOpeningBalance.Text = (receiveAmount - expenseAmount).ToString("N", nfi);

        }



        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string fileName = @"\Cash Book.csv";
            string location = "";
            DataTable table = new DataTable();

            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            DialogResult result = folderBrowser.ShowDialog();

            if (result == DialogResult.OK)
            {
                List<CashBook> lstCashBook = new List<CashBook>();
                lstCashBook = GetData();
                DataTable Ctable = BuildCashBookTable(lstCashBook);
                location = folderBrowser.SelectedPath + fileName;
                CSVCreator.CreateCSVFile(Ctable, location);
                MessageBoxHelper.ShowInformation("Generate successfully.");
            }


        }

        private DataTable BuildCashBookTable(List<CashBook> lstCashBook)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Date");
            table.Columns.Add("Description");
            table.Columns.Add("Opening Balance");
            table.Columns.Add("Received");
            table.Columns.Add("Payment");
            table.Columns.Add("Balance");
            int count = 0;

            foreach (CashBook cBook in lstCashBook)
            {
                DataRow row = table.NewRow();
                if (cBook.OpeningBalance > 0)
                {
                    row = table.NewRow();
                    row["Date"] = cBook.ReceiveDate;
                    row["Description"] = cBook.ItemHead;
                    row["Received"] = cBook.OpeningBalance;
                    row["Balance"] = cBook.OpeningBalance;
                    table.Rows.Add(row);
                    count++;
                }
                else
                {
                    if (cBook.ReceiveAmount > 0)
                    {
                        row = table.NewRow();
                        decimal balance = (count > 0) ? Convert.ToDecimal(table.Rows[count - 1]["Balance"].ToString()) : 0;

                        row["Date"] = cBook.ReceiveDate;
                        row["Description"] = cBook.ItemHead;
                        row["Received"] = cBook.ReceiveAmount;
                        row["Payment"] = 0;
                        row["Balance"] = balance + cBook.ReceiveAmount;
                        table.Rows.Add(row);
                        count++;
                    }
                    if (cBook.ExpenseAmount > 0)
                    {
                        row = table.NewRow();
                        decimal balance = (count > 0) ? Convert.ToDecimal(table.Rows[count - 1]["Balance"].ToString()) : 0;
                        row["Date"] = cBook.ExpenseDate;
                        row["Description"] = cBook.ExpenseHead;
                        row["Received"] = 0;
                        row["Payment"] = cBook.ExpenseAmount;
                        row["Balance"] = balance - cBook.ExpenseAmount;
                        table.Rows.Add(row);
                        count++;
                    }
                }

            }
            return table;
        }


        private class CashBook
        {
            public decimal OpeningBalance { get; set; }
            public string ReceiveDate { get; set; }
            public string ItemHead { get; set; }
            public decimal ReceiveAmount { get; set; }
            public string ExpenseDate { get; set; }
            public string ExpenseHead { get; set; }
            public decimal ExpenseAmount { get; set; }

        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrintReport_Click(object sender, EventArgs e)
        {
            PrintCashBookReport();
        }
        private List<CashBook> GetData()
        {
            List<CashBook> lstCashBook = new List<CashBook>();
            CashBook cashBook = new CashBook();
            cashBook.OpeningBalance = Convert.ToDecimal(txtOpeningBalance.Text);
            cashBook.ItemHead = "Opening Balance";
            cashBook.ReceiveDate = dtpFromDate.Value.AddDays(-1).ToString("dd/MM/yyyy");
            cashBook.ReceiveAmount = Convert.ToDecimal(txtOpeningBalance.Text);
            lstCashBook.Add(cashBook);

            if (grvCashReceveInfo.Rows.Count > grvExpenseInformation.Rows.Count)
            {
                foreach (UltraGridRow row in grvCashReceveInfo.Rows)
                {
                    cashBook = new CashBook();
                    cashBook.ReceiveDate = row.Cells["Receive Date"].Text;
                    cashBook.ItemHead = row.Cells["Line Item"].Text;
                    cashBook.ReceiveAmount = Convert.ToDecimal(row.Cells["Amount"].Text);
                    cashBook.ExpenseDate = string.Empty;
                    cashBook.ExpenseHead = string.Empty;
                    cashBook.ExpenseAmount = 0;
                    lstCashBook.Add(cashBook);
                }

                int count = 0;

                foreach (CashBook cBook in lstCashBook)
                {
                    if (grvExpenseInformation.Rows.Count - 1 >= count)
                    {
                        cBook.ExpenseDate = grvExpenseInformation.Rows[count].Cells["Expense Date"].Text;
                        cBook.ExpenseHead = grvExpenseInformation.Rows[count].Cells["Line Item"].Text;
                        cBook.ExpenseAmount = Convert.ToDecimal(grvExpenseInformation.Rows[count].Cells["Amount"].Text);
                    }
                    count++;
                }
            }
            else if (grvExpenseInformation.Rows.Count > grvCashReceveInfo.Rows.Count)
            {
                foreach (UltraGridRow row in grvExpenseInformation.Rows)
                {
                    cashBook = new CashBook();
                    cashBook.ReceiveDate = string.Empty;
                    cashBook.ItemHead = string.Empty;
                    cashBook.ReceiveAmount = 0;
                    cashBook.ExpenseDate = row.Cells["Expense Date"].Text;
                    cashBook.ExpenseHead = row.Cells["Line Item"].Text;
                    cashBook.ExpenseAmount = Convert.ToDecimal(row.Cells["Amount"].Text);
                    lstCashBook.Add(cashBook);
                }


                int count = 0;

                foreach (CashBook cBook in lstCashBook)
                {
                    //if (count > 0)
                    //{
                    if (grvCashReceveInfo.Rows.Count - 1 >= count)
                    {
                        cBook.ReceiveDate = grvCashReceveInfo.Rows[count].Cells["Receive Date"].Text;
                        cBook.ItemHead = grvCashReceveInfo.Rows[count].Cells["Line Item"].Text;
                        cBook.ReceiveAmount = Convert.ToDecimal(grvCashReceveInfo.Rows[count].Cells["Amount"].Text);
                    }
                    // }
                    count++;
                }
            }
            else if (grvExpenseInformation.Rows.Count == grvCashReceveInfo.Rows.Count)
            {
                foreach (UltraGridRow row in grvExpenseInformation.Rows)
                {
                    cashBook = new CashBook();
                    cashBook.ReceiveDate = grvCashReceveInfo.Rows[row.Index].Cells["Receive Date"].Text;
                    cashBook.ItemHead = grvCashReceveInfo.Rows[row.Index].Cells["Line Item"].Text;
                    cashBook.ReceiveAmount = Convert.ToDecimal(grvCashReceveInfo.Rows[row.Index].Cells["Amount"].Text);
                    cashBook.ExpenseDate = row.Cells["Expense Date"].Text;
                    cashBook.ExpenseHead = row.Cells["Line Item"].Text;
                    cashBook.ExpenseAmount = Convert.ToDecimal(row.Cells["Amount"].Text);
                    lstCashBook.Add(cashBook);
                }
            }

            return lstCashBook;
        }


        private void PrintCashBookReport()
        {
            string pharmacyName = string.Empty;
            string pharmacyAddress = string.Empty;
            List<CashBook> lstCashBook = new List<CashBook>();
            lstCashBook = GetData();
            SetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
            DataTable Ctable = BuildCashBookTable(lstCashBook);
            rptCashBook op = new rptCashBook();
            frmSalesReport objmainReport = new frmSalesReport();

            op.DataSource = Ctable;
            op.txtAddress.Text = pharmacyAddress;
            op.txtPharmacyName.Text = pharmacyName;
            DateTime fDate = (DateTime)dtpFromDate.Value;
            DateTime tDate = (DateTime)dtpToDate.Value;
            op.txtFromDate.Text = fDate.ToString("yyyy/MM/dd");
            op.txtToDate.Text = tDate.ToString("yyyy/MM/dd");

            objmainReport.rptViewer.Document = op.Document;
            objmainReport.rptViewer.Dock = DockStyle.Fill;
            op.Run();
            objmainReport.ShowDialog();
        }

        private void SetPharmachyInforamation(ref string PharmacyName, ref string Address)
        {
            Branch branch = new BranchManager().GetBranchByID(MTBFConstants.AppConstants.BranchID);
            if (branch != null)
            {
                PharmacyName = branch.BranchName;
                Address = branch.Address + ", " + branch.Phone + ", " + branch.Fax;
            }
        }





    }
}
