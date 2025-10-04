using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using IFMS.Facade;
using System.Collections.Generic;
using IMFS.Common.DTO;
using System.Linq;
using NybSys.MTBF.Utility.Constants;
using IFMS.BLL;
using NybSys.MTBF.Utility.Enums;
using NybSys.MTBF.Utility.Helper;
using Infragistics.Win.UltraWinGrid;

namespace Tiles_Inventory
{
    public partial class frmJournalInformationNew : BaseForm
    {
        int rowIndex = 0;

        public event OnJournalInformationCloseEventHandler OnJournalInformationClose;
        public delegate void OnJournalInformationCloseEventHandler();
        List<Journal> _lstJournal = new List<Journal>();

        List<JournalVoucher> _lstJournalVoucher = new List<JournalVoucher>();
        List<ParentAccount> lstParentAccount = new List<ParentAccount>();
        List<ChildAccount> lstChildAccount = new List<ChildAccount>();
        int _referenceNo = -1;

        decimal totalDebit = 0;
        decimal totalCredit = 0;

        public frmJournalInformationNew()
        {
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        public frmJournalInformationNew(List<JournalVoucher> lstJournalVoucher, bool isEdit)
        {
            _lstJournalVoucher = lstJournalVoucher;
            IsUpdating = isEdit;
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        private void JournalInformation_Load(object sender, EventArgs e)
        {
            // loadParentAccountInfo();
            //  LoadAccountType();
            LoadChildAccount();
            LoadInitialData();
            if (IsUpdating)
            {
                LoadExistingData();
            }
            else
            {
                txtVoucherNo.Text = new JournalVoucherManager().GetVoucherNo();
            }

        }

        private void LoadInitialData()
        {
            DataTable dt = BuildJournalTable();
            DataRow row = dt.NewRow();
            row["AccountID"] = 0;
            row["Account Name"] = string.Empty;
            row["Debit Amount"] = string.Empty;
            row["Credit Amount"] = 0;
            row["Short Note"] = 0;
            row["Delete"] = "Delete";
            row["Add"] = "Add";
            dt.Rows.Add(row);
            grvJournalInfo.DataSource = dt;
        }

        private DataTable BuildJournalTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("AccountID");
            dt.Columns.Add("Account Name");
            dt.Columns.Add("Debit Amount");
            dt.Columns.Add("Credit Amount");
            dt.Columns.Add("Short Note");
            dt.Columns.Add("Delete");
            dt.Columns.Add("Add");
            return dt;
        }


        private void LoadExistingData()
        {
            lstChildAccount = new ChildAccountManager().GetAllChildAccountByBranchID(MTBFConstants.AppConstants.BranchID);
            txtVoucherNo.Text = _lstJournalVoucher[0].VoucherNumber.ToString();
            dtpJournalDate.Value = _lstJournalVoucher[0].Date;
            _referenceNo = _lstJournalVoucher[0].ReferenceNo;
            string filter = string.Format("{0}={1}", "ReferenceNo", _lstJournalVoucher[0].ReferenceNo);
            _lstJournal = new JournalManager().GetFilteredJournal(filter);

            LoadJournalInGrid();
            _lstJournal = new List<Journal>();
            _lstJournalVoucher = new List<JournalVoucher>();

        }


        /// <summary>
        /// Method to load account type in cobo box.
        /// </summary>
        private void LoadAccountType()
        {
            DataTable table = new DataTable();
            List<AccountType> lstAccountType = new List<AccountType>();
            lstAccountType = DataAccessProxy.GetAllAccountType().Cast<AccountType>().ToList();


            DataRow tableDR = table.NewRow();
            tableDR[0] = -1;
            tableDR[1] = "-Select-";
            table.Rows.Add(tableDR);

            foreach (AccountType accountType in lstAccountType)
            {
                DataRow row = table.NewRow();
                row[0] = accountType.AccountTypeID;
                row[1] = accountType.AccountTypeName;
                table.Rows.Add(row);

            }

            cmbParentAccount.DataSource = table;
            cmbParentAccount.DisplayMember = "AccountTypeName";
            cmbParentAccount.ValueMember = "AccountTypeID";

            cmbParentAccount.Value = -1;


        }

        public void loadParentAccountInfo()
        {

            DataTable table = new DataTable();

            lstParentAccount = DataAccessProxy.GetAllParentAccount().Cast<ParentAccount>().ToList();

            table.Columns.Add("ID");
            table.Columns.Add("Name");

            DataRow tableDR = table.NewRow();
            tableDR[0] = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            tableDR[1] = MTBFConstants.DataField.COMBO_DEFAULT_NAME;
            table.Rows.Add(tableDR);

            cmbParentAccount.DataSource = null;

            foreach (ParentAccount PAccount in lstParentAccount)
            {
                DataRow row = table.NewRow();
                row[0] = PAccount.AccountID;
                row[1] = PAccount.AccountsName;
                table.Rows.Add(row);

            }
            cmbParentAccount.DataSource = table;
            cmbParentAccount.DisplayMember = "Name";
            cmbParentAccount.ValueMember = "ID";

            cmbParentAccount.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            cmbParentAccount.DisplayLayout.Bands[0].Columns["ID"].Hidden = true;

        }


        //Dim db As New DataAccess.DirectDatabase.Database
        // Dim ds As DataSet = db.GetDataSet(SQL)

        // If Opt = True Then
        //     Dim Anotherrow As DataRow
        //     Anotherrow = ds.Tables(0).NewRow
        //     Anotherrow(0) = "00"
        //     Anotherrow(1) = heading
        //     ds.Tables(0).Rows.Add(Anotherrow)
        // End If
        // cmb.DataSource = ds
        // cmb.DataBind()
        // If ds.Tables(0).Rows.Count > 0 Then
        //     cmb.SelectedRow = cmb.Rows(0)
        // End If







        //private DataTable BuildJournalTable()
        //{
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("AccountID");
        //    dt.Columns.Add("Account Name");
        //    dt.Columns.Add("Sub Account");
        //    dt.Columns.Add("Debit");
        //    dt.Columns.Add("Credit");
        //    dt.Columns.Add("Delete");
        //    return dt;
        //}

        private bool ValidAddData()
        {
            if (cmbParentAccount.Value == null || Convert.ToInt32(cmbParentAccount.Value) == MTBFConstants.DataField.COMBO_DEFAULT_ID)
            {
                MessageBoxHelper.ShowInformation("You need to select account name");
                cmbParentAccount.Focus();
                return false;
            }
            if (cmbChildAccount.Value == null || Convert.ToInt32(cmbChildAccount.Value) == MTBFConstants.DataField.COMBO_DEFAULT_ID)
            {
                MessageBoxHelper.ShowInformation("You need to select sub account name");
                cmbChildAccount.Focus();
                return false;
            }

            int accountID = 0;
            int childAccountID = 0;
            int.TryParse(cmbParentAccount.Value.ToString(), out accountID);
            int.TryParse(cmbChildAccount.Value.ToString(), out childAccountID);

            ParentAccount parentAccount = lstParentAccount.Where(c => c.AccountID == accountID).FirstOrDefault();
            if (parentAccount == null)
            {
                cmbChildAccount.Focus();
                MessageBoxHelper.ShowInformation("Invalid  account selection");
                return false;
            }

            ChildAccount childAccount = lstChildAccount.Where(c => c.ChildAccountID == childAccountID).FirstOrDefault();
            if (childAccount == null)
            {
                cmbChildAccount.Focus();
                MessageBoxHelper.ShowInformation("Invalid sub account selection");
                return false;
            }


            return true;

        }






        private void LoadJournalInGrid()
        {
            decimal debitAmount = 0;
            decimal creditAmount = 0;

            DataTable dt = BuildJournalTable();
            foreach (Journal journal in _lstJournal)
            {
                ParentAccount parentAccount = lstParentAccount.Where(p => p.AccountID == journal.AccountID).FirstOrDefault();
                ChildAccount childAccount = lstChildAccount.Where(p => p.ChildAccountID == journal.ChildAccountID).FirstOrDefault();
                DataRow row = dt.NewRow();
                row["AccountID"] = journal.ChildAccountID;

                debitAmount += (journal.DrCrIndecator == "Dr") ? journal.Amount : 0;
                creditAmount += (journal.DrCrIndecator == "Cr") ? journal.Amount : 0;

                row["Account Name"] = (childAccount != null) ? childAccount.ChildAccountID : 0;
                // row["Sub Account"] = (childAccount != null) ? childAccount.Description : string.Empty;
                row["Debit Amount"] = (journal.DrCrIndecator == "Dr") ? journal.Amount : 0;
                row["Credit Amount"] = (journal.DrCrIndecator == "Cr") ? journal.Amount : 0;
                row["Short Note"] = journal.Description;
                row["Delete"] = "Delete";
                row["Add"] = "Add";
                dt.Rows.Add(row);
            }
            grvJournalInfo.DataSource = dt;
            grvJournalInfo.DisplayLayout.Bands[0].Columns["AccountID"].Hidden = true;
            lblDebitTotal.Text = debitAmount.ToString(MTBFConstants.currencyFormat);
            lblCreditTotal.Text = creditAmount.ToString(MTBFConstants.currencyFormat);
        }

        private void grvJournalInfo_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            decimal totalDebit = 0;
            decimal totalCredit = 0;

            foreach (Journal journal in _lstJournal)
            {
                totalCredit = totalCredit + ((journal.DrCrIndecator == "Cr") ? totalCredit + journal.Amount : 0);
                totalDebit = totalDebit + ((journal.DrCrIndecator == "Dr") ? totalCredit + journal.Amount : 0);
            }
            //for (int i = 0; i <= grvJournalInfo.Rows.Count - 2; i++)
            //{
            //    totalCredit = totalCredit + Convert.ToDecimal(grvJournalInfo.Rows[i].Cells[3].Value.ToString());
            //    totalDebit = totalDebit + Convert.ToDecimal(grvJournalInfo.Rows[i].Cells[2].Value.ToString());
            //}

            lblCreditTotal.Text = totalCredit.ToString();
            lblDebitTotal.Text = totalDebit.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidFormData())
            {
                if (InsertJournalInformation() == (int)MTBFEnums.ReturnResult.SUCCESS)
                {
                    MessageBox.Show("Information saved successfully", "INFORMATION ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetAllControl();
                }
            }

        }


        private bool ValidFormData()
        {
            decimal debitAmount = 0;
            decimal creditAmount = 0;
            decimal.TryParse(lblDebitTotal.Text, out debitAmount);
            decimal.TryParse(lblCreditTotal.Text, out creditAmount);
            if (grvJournalInfo.Rows.Count == 0)
            {
                MessageBoxHelper.ShowInformation("You need to add data in grid");
                return false;
            }

            if (debitAmount == 0 || creditAmount == 0)
            {
                MessageBoxHelper.ShowInformation("You need to add data in grid");
                return false;
            }

            if (debitAmount != creditAmount)
            {
                MessageBoxHelper.ShowInformation("Debit and credit amount must be equal");
                return false;
            }

            foreach (UltraGridRow row in grvJournalInfo.Rows)
            {
                int accountID = 0;
                int.TryParse(row.Cells["Account Name"].Value.ToString(), out accountID);
                if (accountID == 0)
                {
                    MessageBoxHelper.ShowInformation("Invalid row");
                    UIHelper.MarkDataGridRowAsInvalid(row);
                    return false;
                }
            }


            return true;
        }

        /// <summary>
        /// Method to insert journal information.
        /// </summary>
        /// <returns></returns>
        public int InsertJournalInformation()
        {
            decimal debitAmount = 0;
            decimal creditAmout = 0;
            int accountID = 0;
            int referenceID = 1;
            string indicator = String.Empty;          
            referenceID = DataAccessProxy.GetJournalReferenceID();
            foreach (UltraGridRow row in grvJournalInfo.Rows)
            {
                string drCrindicator = string.Empty;
                decimal.TryParse(row.Cells["Debit Amount"].Text, out debitAmount);
                decimal.TryParse(row.Cells["Credit Amount"].Text, out creditAmout);
                int.TryParse(row.Cells["Account Name"].Value.ToString(), out accountID);
                drCrindicator = (debitAmount > 0) ? "Dr" : "Cr";

                Journal journal = new Journal();
                journal.AccountID = accountID;

                ChildAccount childAccount = lstChildAccount.Where(c => c.ChildAccountID == accountID).FirstOrDefault();

                journal.JournalDate = DateTime.Now;
                journal.Description = row.Cells["Short Note"].Text;

                if (debitAmount > 0)
                {
                    journal.AccountID = (childAccount != null) ? childAccount.AccountID : 0;
                    journal.ChildAccountID = accountID;
                    journal.Amount = debitAmount;
                    journal.DrCrIndecator = "Dr";
                }
                else
                {
                    journal.AccountID = (childAccount != null) ? childAccount.AccountID : 0;
                    journal.ChildAccountID = accountID;
                    journal.Amount = creditAmout;
                    journal.DrCrIndecator = "Cr";
                }


                journal.ReferenceNo = referenceID;
                journal.BranchID = MTBFConstants.AppConstants.BranchID;
                journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                _lstJournal.Add(journal);
            }

            foreach (UltraGridRow row in grvJournalInfo.Rows)
            {
                decimal.TryParse(row.Cells["Debit Amount"].Text, out debitAmount);
                decimal.TryParse(row.Cells["Credit Amount"].Text, out creditAmout);
                int.TryParse(row.Cells["Account Name"].Value.ToString(), out accountID);

                JournalVoucher journalVoucher = new JournalVoucher();
                journalVoucher.VoucherNumber = Convert.ToInt64(txtVoucherNo.Text);
                ChildAccount childAccount = lstChildAccount.Where(c => c.ChildAccountID == accountID).FirstOrDefault();
                if (debitAmount > 0)
                {
                    journalVoucher.DebitAccountID = (childAccount != null) ? childAccount.AccountID : 0;
                    journalVoucher.DebitChildAccountID = accountID;
                    journalVoucher.DebitChildAccountName = row.Cells["Account Name"].Text;
                    journalVoucher.Amount = debitAmount;
                }
                else
                {
                    journalVoucher.CreditAccountID = (childAccount != null) ? childAccount.AccountID : 0;
                    journalVoucher.CreditChildAccountID = accountID;
                    journalVoucher.CreditChildAccountName = row.Cells["Account Name"].Text;
                    journalVoucher.Amount = creditAmout;
                }

                journalVoucher.Description = row.Cells["Short Note"].Text;
                journalVoucher.Date = dtpJournalDate.Value;

                journalVoucher.BranchID = MTBFConstants.AppConstants.BranchID;
                journalVoucher.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                journalVoucher.Date = dtpJournalDate.Value;
                journalVoucher.ReferenceNo = referenceID;
                _lstJournalVoucher.Add(journalVoucher);
            }
            return new JournalAccountsManager().SaveJournal(_lstJournal, _lstJournalVoucher, _referenceNo);
        }

        private void grvJournalInfo_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //decimal dAmount = 0;
            //decimal cAmount = 0;


            //if (grvJournalInfo.SelectedRows.Count > 0)
            //{
            //    cmbParentAccount.Value = grvJournalInfo.SelectedRows[0].Cells[5].Value.ToString();
            //    loadChildAccountInfo(Convert.ToInt32(cmbParentAccount.Value));
            //    cmbAccountName.Value = grvJournalInfo.SelectedRows[0].Cells[4].Value.ToString();
            //    LoadJournalAccountInfo(Convert.ToInt32(cmbAccountName.Value));
            //    cmbSubAccountName.Value = grvJournalInfo.SelectedRows[0].Cells[0].Value.ToString();

            //    dAmount = Convert.ToDecimal(grvJournalInfo.SelectedRows[0].Cells[2].Value.ToString());
            //    cAmount = Convert.ToDecimal(grvJournalInfo.SelectedRows[0].Cells[3].Value.ToString());

            //    if (cAmount > 0)
            //    {
            //        txtAmount.Text = cAmount.ToString();
            //        rbCr.Checked = true;
            //    }
            //    else
            //    {
            //        txtAmount.Text = dAmount.ToString();
            //        rbDr.Checked = true;
            //    }
            //    btnAdd.Text = "Update";

            //    rowIndex = grvJournalInfo.SelectedRows[0].Index;
            //    grvJournalInfo.ClearSelection();
            // }

        }

        private void CalculateGridTotal()
        {

            decimal totalDebit = 0;
            decimal totalCredit = 0;

            for (int i = 0; i <= grvJournalInfo.Rows.Count - 2; i++)
            {
                totalCredit = totalCredit + Convert.ToDecimal(grvJournalInfo.Rows[i].Cells[3].Value.ToString());
                totalDebit = totalDebit + Convert.ToDecimal(grvJournalInfo.Rows[i].Cells[2].Value.ToString());
            }

            lblCreditTotal.Text = totalCredit.ToString();
            lblDebitTotal.Text = totalDebit.ToString();
        }

        private void grvJournalInfo_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            CalculateGridTotal();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //grvJournalInfo.Rows.RemoveAt(rowIndex);

            //btnAdd.Text = "Add";
        }

        private void btnAddnew_Click(object sender, EventArgs e)
        {

            ResetAllControl();
        }

        private void ResetAllControl()
        {
            grvJournalInfo.DataSource = BuildJournalTable();
            txtVoucherNo.Text = new JournalVoucherManager().GetVoucherNo();
            _referenceNo = -1;
            lblCreditTotal.Text = string.Empty;
            lblDebitTotal.Text = string.Empty;
            _lstJournal = new List<Journal>();
            _lstJournalVoucher = new List<JournalVoucher>();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            ReportsView objRV = new ReportsView();
            objRV.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddParentAccount frm = new AddParentAccount(cmbParentAccount.Value.ToString(), 0, false);
            // frm.OnParentAccountAddad += new AddParentAccount.LodaEventHandaler(frm_OnParentAccountAddad);
            frm.ShowDialog();

        }

        void frm_OnParentAccountAddad(int parentAccountID)
        {
            loadParentAccountInfo();
        }



        private void btnRefresh_Click(object sender, EventArgs e)
        {
            loadParentAccountInfo();
        }


        private void OpenUpdateAccountInfo_Click(object sender, EventArgs e)
        {
            // UpdateAccountInformation objUpdateAccountInformation = new UpdateAccountInformation();
            // objUpdateAccountInformation.ShowDialog();
        }

        private void cmbParentAccountName_ValueChanged(object sender, EventArgs e)
        {

        }



        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void JournalInformation_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (OnJournalInformationClose != null)
            {
                OnJournalInformationClose();
            }
        }

        private void grvJournalInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvJournalInfo_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            try
            {
                decimal debitAmount = 0;
                decimal creditAmout = 0;

                string celltext = e.Cell.Column.Header.Caption;
                if (celltext == "Delete")
                {
                    int rowIndex = e.Cell.Row.Index;
                    grvJournalInfo.Rows[e.Cell.Row.Index].Delete();
                    CalculateDebitCreditAmount();

                }
                else if (celltext == "Add")
                {
                    UltraGridRow row = grvJournalInfo.DisplayLayout.Bands[0].AddNew();
                    row.Cells["Delete"].Value = "Delete";
                    row.Cells["Add"].Value = "Add";
                }
                else
                {
                    decimal.TryParse(grvJournalInfo.Rows[e.Cell.Row.Index].Cells["Debit Amount"].Text, out debitAmount);
                    decimal.TryParse(grvJournalInfo.Rows[e.Cell.Row.Index].Cells["Credit Amount"].Text, out creditAmout);

                    if (celltext == "Account Name")
                    {
                        grvJournalInfo.Rows[e.Cell.Row.Index].Cells["Account Name"].Activate();
                        grvJournalInfo.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode);
                    }
                    if (celltext == "Debit Amount")
                    {
                        if (creditAmout == 0)
                        {
                            grvJournalInfo.Rows[e.Cell.Row.Index].Cells["Debit Amount"].Activate();
                            grvJournalInfo.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode);
                        }
                    }

                    if (celltext == "Credit Amount")
                    {
                        if (debitAmount == 0)
                        {
                            grvJournalInfo.Rows[e.Cell.Row.Index].Cells["Credit Amount"].Activate();
                            grvJournalInfo.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode);
                        }
                    }

                    if (celltext == "Short Note")
                    {
                        grvJournalInfo.Rows[e.Cell.Row.Index].Cells["Short Note"].Activate();
                        grvJournalInfo.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Operation fail Please try again", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btAddChildAccount_Click(object sender, EventArgs e)
        {
            int parentAccountID = 0;

            if (cmbParentAccount.Value != null && Convert.ToInt32(cmbParentAccount.Value) != -1)
            {
                int.TryParse(cmbParentAccount.Value.ToString(), out parentAccountID);
                AddChildAccount objAddChildAccount = new AddChildAccount(parentAccountID);
                objAddChildAccount.OnChildAccountAddad += new AddChildAccount.LodaEventHandaler(objAddChildAccount_OnChildAccountAddad);
                objAddChildAccount.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select parent account", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbParentAccount.Focus();
            }
        }

        private void cmbParentAccount_ValueChanged(object sender, EventArgs e)
        {
            int parentAccountID = 0;

            if (cmbParentAccount.Value != null)
            {
                int.TryParse(cmbParentAccount.Value.ToString(), out parentAccountID);

                LoadChildAccount();
            }
        }

        void objAddChildAccount_OnChildAccountAddad()
        {
            LoadChildAccount();
        }

        private void LoadChildAccount()
        {
            //int parentAccountID = 0;
            //int.TryParse(cmbParentAccount.Value.ToString(), out parentAccountID);
            lstChildAccount = new ChildAccountManager().GetAllChildAccountByBranchID(MTBFConstants.AppConstants.BranchID);

            DataTable table = new DataTable();

            table.Columns.Add("ID");
            table.Columns.Add("Name");

            DataRow tableDR = table.NewRow();
            tableDR[0] = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            tableDR[1] = MTBFConstants.DataField.COMBO_DEFAULT_NAME;
            table.Rows.Add(tableDR);



            foreach (ChildAccount childAccount in lstChildAccount)
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
        }

        private void grvJournalInfo_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            grvJournalInfo.DisplayLayout.Bands[0].Columns["Delete"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.URL;
        }

        private void grvJournalInfo_CellChange(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e)
        {
            decimal debitAmount = 0;
            decimal creditAmout = 0;
            decimal.TryParse(grvJournalInfo.Rows[e.Cell.Row.Index].Cells["Debit Amount"].Text, out debitAmount);
            decimal.TryParse(grvJournalInfo.Rows[e.Cell.Row.Index].Cells["Credit Amount"].Text, out creditAmout);

            if (e.Cell.Text == "Debit Amount")
            {
                if (creditAmout == 0)
                {
                    grvJournalInfo.Rows[e.Cell.Row.Index].Cells["Debit Amount"].Activate();
                    grvJournalInfo.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode);
                }
            }

            if (e.Cell.Text == "Credit Amount")
            {
                if (debitAmount == 0)
                {
                    grvJournalInfo.Rows[e.Cell.Row.Index].Cells["Credit Amount"].Activate();
                    grvJournalInfo.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode);
                }
            }

            CalculateDebitCreditAmount();
        }


        private void CalculateDebitCreditAmount()
        {
            decimal totalDebit = 0;
            decimal totalCredit = 0;

            foreach (UltraGridRow row in grvJournalInfo.Rows)
            {
                decimal debitAmount = 0;
                decimal creditAmount = 0;
                decimal.TryParse(row.Cells["Debit Amount"].Text, out debitAmount);
                decimal.TryParse(row.Cells["Credit Amount"].Text, out creditAmount);
                totalDebit += debitAmount;
                totalCredit += creditAmount;
            }

            lblDebitTotal.Text = totalDebit.ToString();
            lblCreditTotal.Text = totalCredit.ToString();
        }

        private void grvJournalInfo_KeyUp(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 13)
            {
                UltraGridRow row = grvJournalInfo.DisplayLayout.Bands[0].AddNew();
                row.Cells["Delete"].Value = "Delete";
                row.Cells["Add"].Value = "Add";
            }
        }

    }
}
