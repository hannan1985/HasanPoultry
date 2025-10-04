using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using IFMS.Facade;
using Tiles_Inventory.Reports;
using IMFS.Common.View;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Constants;
using System.Reflection;
using IMFS.Common.Utility;
using NybSys.MTBF.Utility.Helper;
using IFMS.BLL;
using NybSys.MTBF.Utility.Enums;
using Infragistics.Win.UltraWinGrid;

namespace Tiles_Inventory
{
    public partial class ReportsView : BaseForm
    {
        List<ParentAccount> lstParentAccount = new List<ParentAccount>();
        private string pharmacyName;
        private string pharmacyAddress;
        List<ChildAccount> _lstChildAccount = new List<ChildAccount>();

        public ReportsView()
        {
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }


        DataTable BuildTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("AccountName", typeof(string));
            table.Columns.Add("Amount", typeof(Decimal));
            table.Columns.Add("Total", typeof(decimal));
            return table;
        }

        private void btnShowReport_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.FinancialReportName.TrialBalance)
            {
                LoadTrialBalance();
            }
            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.FinancialReportName.IncomeStatement)
            {
                LoadIncomeStatement();
            }
            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.FinancialReportName.BalanceSheet)
            {
                LoadBalanceSheet();
            }
            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.FinancialReportName.BalanceSheetDetail)
            {
                LoadBalanceSheetDetail();
            }

        }



        private void LoadTrialBalance()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("AccountName");
            dt.Columns.Add("Description");
            dt.Columns.Add("DebitAmount");
            dt.Columns.Add("CreditAmount");
            string _fromDate = dtpFromDate.Value.ToString("yyyy/MM/dd") + MTBFConstants.DAY_START_TIME;
            string _toDate = dtpToDate.Value.ToString("yyyy/MM/dd") + MTBFConstants.DAY_END_TIME;

            List<TrialBalance> lstTrialBalance = new List<TrialBalance>();
            lstTrialBalance = new FinancialReportManager().GetTrialBalance(MTBFConstants.AppConstants.BranchID, _fromDate, _toDate);

            lstTrialBalance = lstTrialBalance.OrderBy(t => t.Head).ToList();

            foreach (TrialBalance trialBalance in lstTrialBalance)
            {
                DataRow row = dt.NewRow();
                if ((trialBalance.Head == (int)IFMSEnum.AccountHead.Asset || trialBalance.Head == (int)IFMSEnum.AccountHead.Expense) && trialBalance.Balance > 0)
                {
                    row["AccountName"] = (!string.IsNullOrEmpty(trialBalance.Description)) ? trialBalance.Description : trialBalance.AccountsName;
                    row["Description"] = trialBalance.Description;
                    row["DebitAmount"] = trialBalance.Balance;
                    row["CreditAmount"] = 0;

                }
                else if ((trialBalance.Head == (int)IFMSEnum.AccountHead.Asset || trialBalance.Head == (int)IFMSEnum.AccountHead.Expense) && trialBalance.Balance < 0)
                {
                    row["AccountName"] = (!string.IsNullOrEmpty(trialBalance.Description)) ? trialBalance.Description : trialBalance.AccountsName;
                    row["Description"] = trialBalance.Description;
                    row["DebitAmount"] = 0;
                    row["CreditAmount"] = trialBalance.Balance * -1;

                }
                if (trialBalance.Head != (int)IFMSEnum.AccountHead.Asset && trialBalance.Head != (int)IFMSEnum.AccountHead.Expense && trialBalance.Balance > 0)
                {
                    row["AccountName"] = (!string.IsNullOrEmpty(trialBalance.Description)) ? trialBalance.Description : trialBalance.AccountsName;
                    row["Description"] = trialBalance.Description;
                    row["DebitAmount"] = 0;
                    row["CreditAmount"] = trialBalance.Balance;

                }
                else if (trialBalance.Head != (int)IFMSEnum.AccountHead.Asset && trialBalance.Head != (int)IFMSEnum.AccountHead.Expense && trialBalance.Balance > 0)
                {
                    row["AccountName"] = (!string.IsNullOrEmpty(trialBalance.Description)) ? trialBalance.Description : trialBalance.AccountsName;
                    row["Description"] = trialBalance.Description;
                    row["DebitAmount"] = trialBalance.Balance * -1;
                    row["CreditAmount"] = 0;

                }
                dt.Rows.Add(row);
            }

            rptTrialBalance op = new rptTrialBalance();
            op.DataSource = dt;
            op.txtFromDate.Text = dtpFromDate.Value.ToString("dd-MMM-yyyy");
            op.txtToDate.Text = dtpToDate.Value.ToString("dd-MMM-yyyy");
            rptViewer.Document = op.Document;
            rptViewer.Dock = DockStyle.Fill;
            GetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
            op.txtPharmacyName.Text = pharmacyName;
            op.txtAddress.Text = pharmacyAddress;

            op.Run();

        }

        private void ReportsView_Load(object sender, EventArgs e)
        {
            LoadReportName();
            LoadAccountsName();
        }

        private void LoadReportName()
        {
            Dictionary<string, int> lstReportName = new Dictionary<string, int>();

            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("Name");

            foreach (IFMSEnum.FinancialReportName enumValue in Enum.GetValues(typeof(IFMSEnum.FinancialReportName)))
            {
                DataRow row = table.NewRow();
                row[0] = (int)enumValue;
                row[1] = enumValue.GetDescription();
                table.Rows.Add(row);
                //   lstReportName.Add(enumValue.GetDescription(), (int)enumValue);
            }

            cmbReportName.DisplayMember = "Name";
            cmbReportName.ValueMember = "ID";
            cmbReportName.DataSource = table;
            cmbReportName.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
            cmbReportName.DisplayLayout.PerformAutoResizeColumns(false, PerformAutoSizeType.AllRowsInBand, true);
        }

        private void LoadBalanceSheet()
        {
            string _fromDate = dtpFromDate.Value.ToString("yyyy/MM/dd") + MTBFConstants.DAY_START_TIME;
            string _toDate = dtpToDate.Value.ToString("yyyy/MM/dd") + MTBFConstants.DAY_END_TIME;

            List<BalanceSheet> lstBalanceSheet = new List<BalanceSheet>();
            List<BalanceSheetBackup> lstbalanceSheetBackup = new List<BalanceSheetBackup>();

            lstBalanceSheet = DataAccessProxy.GetAllBalanceSheet(_fromDate, _toDate).Cast<BalanceSheet>().Where(b => b.BranchID == MTBFConstants.AppConstants.BranchID && b.OrganizationID == MTBFConstants.AppConstants.OrganizationID).ToList();
            decimal profit = DataAccessProxy.GetProfitOrLoss(_fromDate, _toDate, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID);

            InsertRetainEarning(profit, dtpToDate.Value.Year);
            //lstbalanceSheetBackup = DataAccessProxy.GetBalanceSheetBackUpByFiscalYear(cmbCustomerName.Text).Cast<BalanceSheetBackup>().ToList();

            //if (lstbalanceSheetBackup.Count > 0)
            //{
            //    DeleteBalanceSheetBackup(lstbalanceSheetBackup);
            //    InsertBalanceSheetBackup(Convert.ToInt32(cmbCustomerName.Text), lstBalanceSheet);
            //}
            //else
            //{
            //    InsertBalanceSheetBackup(Convert.ToInt32(cmbCustomerName.Text), lstBalanceSheet);
            //}

            rptBalanceSheet op = new rptBalanceSheet();
            op.DataSource = ListToDataTable(lstBalanceSheet, profit, dtpToDate.Value.Year);//lstBalanceSheet;
            rptViewer.Document = op.Document;
            rptViewer.Dock = DockStyle.Fill;
            op.txtFromDate.Text = dtpFromDate.Value.ToString("dd-MMM-yyyy");
            op.txtToDate.Text = dtpToDate.Value.ToString("dd-MMM-yyyy");
            GetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
            op.txtPharmacyName.Text = pharmacyName;
            op.txtAddress.Text = pharmacyAddress;
            //  op.txtReportCaption.Text = "For Year Ending Dec. 31, " + cmbCustomerName.Text;
            op.Run();
        }

        private void LoadBalanceSheetDetail()
        {
            string _fromDate = dtpFromDate.Value.ToString("yyyy/MM/dd") + MTBFConstants.DAY_START_TIME;
            string _toDate = dtpToDate.Value.ToString("yyyy/MM/dd") + MTBFConstants.DAY_END_TIME;

            List<BalanceSheet> lstBalanceSheet = new List<BalanceSheet>();

            List<BalanceSheet> lstBalanceShortedSheet = new List<BalanceSheet>();
            List<BalanceSheet> lstBalanceSheetLiabilities = new List<BalanceSheet>();
            List<BalanceSheet> lstBalanceSheetAsset = new List<BalanceSheet>();
            List<BalanceSheetBackup> lstbalanceSheetBackup = new List<BalanceSheetBackup>();

            lstBalanceSheet = new FinancialReportManager().GetAllBalanceSheetDetail(_fromDate, _toDate).Cast<BalanceSheet>().Where(b => b.BranchID == MTBFConstants.AppConstants.BranchID && b.OrganizationID == MTBFConstants.AppConstants.OrganizationID).ToList();
            decimal profit = DataAccessProxy.GetProfitOrLoss(_fromDate, _toDate, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID);

            InsertRetainEarning(profit, dtpToDate.Value.Year);
            //lstbalanceSheetBackup = DataAccessProxy.GetBalanceSheetBackUpByFiscalYear(cmbCustomerName.Text).Cast<BalanceSheetBackup>().ToList();

            //if (lstbalanceSheetBackup.Count > 0)
            //{
            //    DeleteBalanceSheetBackup(lstbalanceSheetBackup);
            //    InsertBalanceSheetBackup(Convert.ToInt32(cmbCustomerName.Text), lstBalanceSheet);
            //}
            //else
            //{
            //    InsertBalanceSheetBackup(Convert.ToInt32(cmbCustomerName.Text), lstBalanceSheet);
            //}
            lstBalanceSheet = lstBalanceSheet.OrderBy(b => b.AccountType).ToList();

            lstBalanceSheetAsset = lstBalanceSheet.Where(b => b.AccountType == "Asset").ToList();
            lstBalanceSheetLiabilities = lstBalanceSheet.Where(b => b.AccountType == "Liability").ToList();

            BalanceSheet balanceSheet = new BalanceSheet();
            balanceSheet .AccountType= "Liability";
            balanceSheet.AccountTypeName = "Short term Liabilities";
            balanceSheet.AccountsName = "Retain Earning";
            RetainEarning retinEarning = new RetainEarning();
            retinEarning = DataAccessProxy.GetRetainEarningByFiscalYear(dtpToDate.Value.Year);
            if (retinEarning != null)
            {
               balanceSheet.Amount = retinEarning.EarningAmount;
            }


            lstBalanceSheetLiabilities.Add(balanceSheet);

            lstBalanceSheetLiabilities = lstBalanceSheetLiabilities.OrderBy(b => b.AccountTypeName).ToList();

            lstBalanceShortedSheet.AddRange(lstBalanceSheetAsset);
            lstBalanceShortedSheet.AddRange(lstBalanceSheetLiabilities);

            rptBalanceSheet op = new rptBalanceSheet();
            op.DataSource = lstBalanceShortedSheet;// ListToDataTable(lstBalanceSheet, profit, dtpToDate.Value.Year);//lstBalanceSheet;
            rptViewer.Document = op.Document;
            rptViewer.Dock = DockStyle.Fill;
            op.txtFromDate.Text = dtpFromDate.Value.ToString("dd-MMM-yyyy");
            op.txtToDate.Text = dtpToDate.Value.ToString("dd-MMM-yyyy");
            GetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
            op.txtPharmacyName.Text = pharmacyName;
            op.txtAddress.Text = pharmacyAddress;
            //  op.txtReportCaption.Text = "For Year Ending Dec. 31, " + cmbCustomerName.Text;
            op.Run();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="profitAndLoss"></param>
        /// <param name="fiscalYear"></param>
        /// <returns></returns>
        private int InsertRetainEarning(decimal profitAndLoss, int fiscalYear)
        {
            int resut = (int)IFMSEnum.ReturnResult.Success;
            RetainEarning retainEarning = new RetainEarning();
            RetainEarning previousRetainEarning = DataAccessProxy.GetRetainEarningByFiscalYear(fiscalYear - 1);
            retainEarning.EarningDate = DateTime.Now;
            if (previousRetainEarning != null)
            {
                profitAndLoss = previousRetainEarning.EarningAmount + profitAndLoss;
            }
            retainEarning.EarningAmount = profitAndLoss;
            retainEarning.FiscalYear = fiscalYear;
            resut = DataAccessProxy.InsertRetainEarning(retainEarning);
            return resut;
        }


        public DataTable ListToDataTable<T>(List<T> list, decimal profit, int fiscalYear)
        {
            RetainEarning retinEarning = new RetainEarning();
            int previousFiscalYear = 0;
            DataTable dt = new DataTable();

            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                dt.Columns.Add(new DataColumn(info.Name, info.PropertyType));
            }

           

            foreach (T t in list)
            {
                DataRow row = dt.NewRow();
                foreach (PropertyInfo info in typeof(T).GetProperties())
                {
                    row[info.Name] = info.GetValue(t, null);
                }
                dt.Rows.Add(row);
            }



            DataRow profitrow = dt.NewRow();
            profitrow[0] = "Liability";
            profitrow[1] = "Short term Liabilities";
            profitrow[2] = "Retain Earning";

            retinEarning = DataAccessProxy.GetRetainEarningByFiscalYear(fiscalYear);
            if (retinEarning != null)
            {
                profitrow[3] = retinEarning.EarningAmount;

            }
            else
            {
                int.TryParse(fiscalYear.ToString(), out previousFiscalYear);
                retinEarning = DataAccessProxy.GetRetainEarningByFiscalYear(previousFiscalYear - 1);
                if (retinEarning != null)
                {
                    profitrow[3] = (retinEarning.EarningAmount + profit).ToString();
                }
                else
                {
                    profitrow[3] = profit.ToString();
                }
            }

            dt.Rows.Add(profitrow);
            return dt;
        }


        private void LoadLedger()
        {
            DataTable Drtable = new DataTable();
            DataTable dt = new DataTable();

            dt.Columns.Add("Date");
            dt.Columns.Add("AccountName");

            dt.Columns.Add("Description");
            dt.Columns.Add("DebitAmount");
            dt.Columns.Add("CreditAmount");
            dt.Columns.Add("Balance");
            //  SqlDataAdapter adapter;

            int accountID = 0;
            int childAccountID = 0;
            if (cmbAccountName.Value == null)
            {
                MessageBoxHelper.ShowInformation("You need to select account name.");
                cmbAccountName.Focus();
                return;
            }
            if (cmbChildAccount.Value == null)
            {
                MessageBoxHelper.ShowInformation("You need to select sub account name.");
                cmbChildAccount.Focus();
                return;
            }
            int.TryParse(cmbAccountName.Value.ToString(), out accountID);
            int.TryParse(cmbChildAccount.Value.ToString(), out childAccountID);
            List<Journal> lstJournal = new List<Journal>();
            string filter = string.Empty;
            if (childAccountID > 0)
            {

                filter = string.Format("{0}={1} and {2}={3}", "ChildAccountID", childAccountID, "BranchID", MTBFConstants.AppConstants.BranchID);
            }
            else
            {
                filter = string.Format("{0}={1} and {2}={3}", "AccountID", accountID, "BranchID", MTBFConstants.AppConstants.BranchID);
            }
            if (cbPrintAll.Checked)
            {
                filter = string.Format("{0}={1}", "BranchID", MTBFConstants.AppConstants.BranchID);
            }

            lstJournal = new JournalManager().GetFilteredJournal(filter);



            List<Journal> lstAllJournal = new List<Journal>();

            int[] journalReferenceNos = lstJournal.Select(s => s.ReferenceNo).Distinct().ToArray();
            if (journalReferenceNos.Length > 0)
            {
                filter = String.Format("{0} IN ({1})", "ReferenceNo", String.Join(",", journalReferenceNos));
                lstAllJournal = new JournalManager().GetFilteredJournal(filter);
            }

            List<ChildAccount> lstChildAccount = new List<ChildAccount>();
            lstChildAccount = new ChildAccountManager().GetAllChildAccountByBranchID(MTBFConstants.AppConstants.BranchID);

            decimal totalDebit = 0;
            decimal totalCredit = 0;
            foreach (Journal journal in lstJournal)
            {
                decimal balance = 0;
                totalDebit += (journal.DrCrIndecator == "Dr") ? journal.Amount : 0;
                totalCredit += (journal.DrCrIndecator == "Cr") ? journal.Amount : 0;
                string searchIndicato = (journal.DrCrIndecator == "Dr") ? "Cr" : "Dr";
                Journal opsitJournal = lstAllJournal.Where(j => j.ReferenceNo == journal.ReferenceNo && j.DrCrIndecator == searchIndicato).FirstOrDefault();


                ParentAccount parentAccount = (opsitJournal != null) ? lstParentAccount.Where(p => p.AccountID == opsitJournal.AccountID).FirstOrDefault() : null;
                ChildAccount OpositchildAccount = lstChildAccount.Where(c => c.ChildAccountID == opsitJournal.ChildAccountID).FirstOrDefault();

                DataRow row = dt.NewRow();
                string accountName = string.Empty;
                if (OpositchildAccount != null)
                {
                    accountName = OpositchildAccount.Description;
                }
                else
                {
                    accountName = (parentAccount != null) ? parentAccount.AccountsName : string.Empty;
                }


                row["Description"] = journal.Description;
                row["AccountName"] = accountName;
                row["DebitAmount"] = (journal.DrCrIndecator == "Dr") ? journal.Amount : 0;
                row["CreditAmount"] = (journal.DrCrIndecator == "Cr") ? journal.Amount : 0;
                row["Date"] = journal.JournalDate.ToString("dd/MM/yyyy");

                parentAccount = lstParentAccount.Where(p => p.AccountID == journal.AccountID).FirstOrDefault();
                if (parentAccount.AccountHeadID == (int)IFMSEnum.AccountHead.Asset)
                {
                    balance = totalDebit - totalCredit;
                }
                else if (parentAccount.AccountHeadID == (int)IFMSEnum.AccountHead.Expense)
                {
                    balance = totalDebit - totalCredit;
                }
                else if (parentAccount.AccountHeadID == (int)IFMSEnum.AccountHead.Income)
                {
                    balance = totalDebit - totalCredit;
                }
                else if (parentAccount.AccountHeadID == (int)IFMSEnum.AccountHead.Liabilities)
                {
                    balance = totalCredit - totalDebit;
                }
                else if (parentAccount.AccountHeadID == (int)IFMSEnum.AccountHead.OwnerEquity)
                {
                    balance = totalCredit - totalDebit;
                }
                row["Balance"] = balance;
                dt.Rows.Add(row);
            }



            AllLedger op = new AllLedger();
            op.DataSource = dt;
            op.lblAccountName.Text = (cmbChildAccount.Text == MTBFConstants.DataField.COMBO_DEFAULT_NAME) ? cmbAccountName.Text : cmbChildAccount.Text;
            rptViewer.Document = op.Document;
            op.Run();
            rptViewer.Show();


        }

        private void LoadAccountsName()
        {
            DataTable table = new DataTable();
            string filter = string.Format("IsDefault='false'");
            //lstParentAccount = new ParentAccountManger().GetFilteredParentAccount(filter);
            lstParentAccount = DataAccessProxy.GetAllParentAccount().Cast<ParentAccount>().ToList();

            table.Columns.Add("ID");
            table.Columns.Add("Name");

            DataRow tableDR = table.NewRow();
            tableDR[0] = -1;
            tableDR[1] = "-Select-";
            table.Rows.Add(tableDR);

            cmbAccountName.DataSource = null;

            foreach (ParentAccount PAccount in lstParentAccount)
            {
                DataRow row = table.NewRow();
                row[0] = PAccount.AccountID;
                row[1] = PAccount.AccountsName;
                table.Rows.Add(row);

            }
            cmbAccountName.DataSource = table;
            cmbAccountName.DisplayMember = "Name";
            cmbAccountName.ValueMember = "ID";
            cmbAccountName.DisplayLayout.Bands[0].Columns["ID"].Hidden = true;
            cmbAccountName.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
            cmbAccountName.DisplayLayout.PerformAutoResizeColumns(false, PerformAutoSizeType.AllRowsInBand, true);
        }

        private void LoadIncomeStatement()
        {
            //_fromDate = "01-01-" + cmbCustomerName.Text;
            //_toDate = "12-31-" + cmbCustomerName.Text;
            string _fromDate = dtpFromDate.Value.ToString("yyyy/MM/dd") + MTBFConstants.DAY_START_TIME;
            string _toDate = dtpToDate.Value.ToString("yyyy/MM/dd") + MTBFConstants.DAY_END_TIME;

            List<IncomeStatement> lstBalanceSheet = new List<IncomeStatement>();
            lstBalanceSheet = DataAccessProxy.GetAllIncomeStatement(_fromDate, _toDate).Cast<IncomeStatement>().Where(b => b.BranchID == MTBFConstants.AppConstants.BranchID && b.OrganizationID == MTBFConstants.AppConstants.OrganizationID).ToList();
            decimal profit = DataAccessProxy.GetProfitOrLoss(_fromDate, _toDate, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID);
            rptIncomeStatement op = new rptIncomeStatement();
            op.DataSource = lstBalanceSheet;
            rptViewer.Document = op.Document;
            rptViewer.Dock = DockStyle.Fill;
            GetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
            op.txtPharmacyName.Text = pharmacyName;
            op.txtAddress.Text = pharmacyAddress;
            op.txtProfitOrLoss.Text = profit.ToString();
            op.txtFromDate.Text = dtpFromDate.Value.ToString("dd-MMM-yyyy");
            op.txtToDate.Text = dtpToDate.Value.ToString("dd-MMM-yyyy");
            //  op.txtReportCaption.Text = "For Year Ending Dec. 31, " + cmbCustomerName.Text;
            op.Run();
        }

        private void GetPharmachyInforamation(ref string PharmacyName, ref string Address)
        {
            Branch branch = DataAccessProxy.GetBracnhByID(MTBFConstants.AppConstants.BranchID);
            if (branch != null)
            {
                PharmacyName = branch.BranchName;
                Address = branch.Address + ", " + branch.Phone + ", " + branch.Fax;
            }
        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnShowLedger_Click(object sender, EventArgs e)
        {
            LoadLedger();
        }

        private void cmbAccountName_ValueChanged(object sender, EventArgs e)
        {
            int parentAccountID = 0;

            if (cmbAccountName.Value != null)
            {
                int.TryParse(cmbAccountName.Value.ToString(), out parentAccountID);

                LoadChildAccount();
            }
        }

        private void LoadChildAccount()
        {
            int parentAccountID = 0;
            int.TryParse(cmbAccountName.Value.ToString(), out parentAccountID);
            _lstChildAccount = new ChildAccountManager().GetAllChildAccountByParentID(parentAccountID);

            DataTable table = new DataTable();

            table.Columns.Add("ID");
            table.Columns.Add("Name");

            DataRow tableDR = table.NewRow();
            tableDR[0] = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            tableDR[1] = MTBFConstants.DataField.COMBO_DEFAULT_NAME;
            table.Rows.Add(tableDR);



            foreach (ChildAccount childAccount in _lstChildAccount)
            {
                DataRow row = table.NewRow();
                row[0] = childAccount.ChildAccountID;
                row[1] = childAccount.Description;
                table.Rows.Add(row);

            }
            cmbChildAccount.DataSource = table;
            cmbChildAccount.DisplayMember = "Name";
            cmbChildAccount.ValueMember = "ID";

            cmbChildAccount.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            cmbChildAccount.DisplayLayout.Bands[0].Columns["ID"].Hidden = true;
            cmbChildAccount.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
            cmbChildAccount.DisplayLayout.PerformAutoResizeColumns(false, PerformAutoSizeType.AllRowsInBand, true);
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                string location = string.Empty;
                FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                DialogResult result = folderBrowser.ShowDialog();
                location = folderBrowser.SelectedPath;
                DataDynamics.ActiveReports.Export.Pdf.PdfExport pdf = new DataDynamics.ActiveReports.Export.Pdf.PdfExport();
                pdf.Export(this.rptViewer.Document, location + "\\" + cmbReportName.Text + ".pdf");
                MessageBox.Show("Downloaded Successfully.");
            }
            catch (Exception)
            {
                MessageBox.Show("Download Failed.");
            }
        }
    }
}
