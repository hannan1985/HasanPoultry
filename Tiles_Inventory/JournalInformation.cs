using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using IFMS.Facade;
using System.Collections.Generic;
using IMFS.Common.DTO;
using System.Linq;
using NybSys.MTBF.Utility.Constants;

namespace Tiles_Inventory
{
    public partial class JournalInformation : BaseForm
    {
        int rowIndex = 0;

        public event OnJournalInformationCloseEventHandler OnJournalInformationClose;
        public delegate void OnJournalInformationCloseEventHandler();

        public JournalInformation()
        {
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        private void JournalInformation_Load(object sender, EventArgs e)
        {
            loadParentAccountInfo();
            //  LoadAccountType();

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

            List<ParentAccouont> lstParentAccount = DataAccessProxy.GetAllParentAccount().Cast<ParentAccouont>().ToList();

            table.Columns.Add("ID");
            table.Columns.Add("Name");

            DataRow tableDR = table.NewRow();
            tableDR[0] = -1;
            tableDR[1] = "-Select-";
            table.Rows.Add(tableDR);

            cmbParentAccount.DataSource = null;

            foreach (ParentAccouont PAccount in lstParentAccount)
            {
                DataRow row = table.NewRow();
                row[0] = PAccount.AccountID;
                row[1] = PAccount.AccountsName;
                table.Rows.Add(row);

            }
            cmbParentAccount.DataSource = table;
            cmbParentAccount.DisplayMember = "Name";
            cmbParentAccount.ValueMember = "ID";

            cmbParentAccount.Value = -1;

        }

        private void loadChildAccountInfo(int parentAccountID)
        {
            DataTable table = new DataTable();

            List<ChildAccount> lstChildAccount = DataAccessProxy.GetAllChildAccount().Cast<ChildAccount>().Where(c => c.AccountID == parentAccountID).ToList();

            table.Columns.Add("ID");
            table.Columns.Add("Name");

            DataRow tableDR = table.NewRow();
            tableDR[0] = -1;
            tableDR[1] = "-Select-";
            table.Rows.Add(tableDR);

            foreach (ChildAccount childAccount in lstChildAccount)
            {
                DataRow row = table.NewRow();
                row[0] = childAccount.ChildAccountID;
                row[1] = childAccount.Description;
                table.Rows.Add(row);
            }

            cmbAccountName.DataSource = null;
            cmbAccountName.DataSource = table;
            cmbAccountName.DisplayMember = "Name";
            cmbAccountName.ValueMember = "ID";

            cmbAccountName.Value = -1;
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




        private void cmbParentAccountName_SelectionChangeCommitted(object sender, EventArgs e)
        {

            if (cmbParentAccount.Value != null && Convert.ToInt32(cmbParentAccount.Value) != -1)
            {
                loadChildAccountInfo(Convert.ToInt32(cmbParentAccount.Value));
            }
            else
            {
                cmbAccountName.DataSource = null;
            }

        }


        private DataTable BuildSuplierTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Description");
            dt.Columns.Add("Supplier Name");
            dt.Columns.Add("Address");
            dt.Columns.Add("Phone");
            dt.Columns.Add("Email");
            dt.Columns.Add("Duscontinued");
            return dt;

        }






        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbParentAccount.Value) != -1 && txtAmount.Text != String.Empty)
            {
                if (rbCr.Checked)
                {
                    grvJournalInfo.Rows.Add(cmbParentAccount.Value, cmbParentAccount.Text, 0, txtAmount.Text, "Delete", cmbAccountName.Value, cmbParentAccount.Value);
                }
                else if (rbDr.Checked)
                {
                    grvJournalInfo.Rows.Add(cmbParentAccount.Value, cmbParentAccount.Text, txtAmount.Text, 0, "Delete", cmbAccountName.Value, cmbParentAccount.Value);
                }
                cmbParentAccount.Focus();
            }

        }

        private void grvJournalInfo_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (grvJournalInfo.Rows.Count > 1)
            {
                if (lblCreditTotal.Text == lblDebitTotal.Text)
                {
                    if (InsertJournalInformation() > 0)
                    {
                        MessageBox.Show("Information saved successfully", "INFORMATION ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Total Debit amount must be equal to Total Credit amount", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }

        /// <summary>
        /// Method to insert journal information.
        /// </summary>
        /// <returns></returns>
        public int InsertJournalInformation()
        {
            int result = 0;
            int referenceID = 1;
            decimal amount = 0;
            string indicator = String.Empty;
            try
            {
                referenceID = DataAccessProxy.GetJournalReferenceID();

                for (int i = 0; i <= grvJournalInfo.Rows.Count - 2; i++)
                {
                    if (Convert.ToDecimal(grvJournalInfo.Rows[i].Cells[2].Value) > 0)
                    {
                        indicator = "Dr";
                        amount = Convert.ToDecimal(grvJournalInfo.Rows[i].Cells[2].Value);
                    }
                    else
                    {
                        indicator = "Cr";
                        amount = Convert.ToDecimal(grvJournalInfo.Rows[i].Cells[3].Value);
                    }

                    IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();

                    int accountID = 0;
                    int childAccountID = 0;
                    int.TryParse(grvJournalInfo.Rows[i].Cells[5].Value.ToString(), out accountID);
                    int.TryParse(grvJournalInfo.Rows[i].Cells[6].Value.ToString(), out childAccountID);

                    journal.AccountID = accountID;
                    journal.ChildAccountID = childAccountID;
                    journal.DrCrIndecator = indicator;
                    journal.Amount = amount;
                    journal.ReferenceNo = referenceID;
                    journal.BranchID = MTBFConstants.AppConstants.BranchID;
                    journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                    result = DataAccessProxy.InsertJournal(journal);
                }

                ResetAllControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save information.", "INFORMATION ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                result = 0;
            }

            return result;
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
            grvJournalInfo.Rows.RemoveAt(rowIndex);

            btnAdd.Text = "Add";
        }

        private void btnAddnew_Click(object sender, EventArgs e)
        {

            ResetAllControl();
        }

        private void ResetAllControl()
        {
            grvJournalInfo.Rows.Clear();
            txtAmount.Text = String.Empty;
            rbDr.Checked = true;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            ReportsView objRV = new ReportsView();
            objRV.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddParentAccount frm = new AddParentAccount(cmbParentAccount.Value.ToString(), 0, false);
            frm.OnParentAccountAddad += new AddParentAccount.LodaEventHandaler(objParentAccount_OnParentAccountAddad);
            frm.ShowDialog();

        }

        void objParentAccount_OnParentAccountAddad()
        {
            loadParentAccountInfo();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            loadParentAccountInfo();
        }

        private void btnOpenChildAccount_Click(object sender, EventArgs e)
        {

            if (cmbParentAccount.Value != null && Convert.ToInt32(cmbParentAccount.Value) != -1)
            {
                AddChildAccount objAddChildAccount = new AddChildAccount(cmbParentAccount.Value.ToString());
                objAddChildAccount.OnChildAccountAddad += new AddChildAccount.LodaEventHandaler(objAddChildAccount_OnChildAccountAddad);
                objAddChildAccount.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select parent account", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbParentAccount.Focus();
            }

        }

        void objAddChildAccount_OnChildAccountAddad()
        {
            int accountID = 0;
            int.TryParse(cmbParentAccount.Value.ToString(), out accountID);

            if (accountID != -1)
            {
                loadChildAccountInfo(accountID);
            }
        }





        private void OpenUpdateAccountInfo_Click(object sender, EventArgs e)
        {
            // UpdateAccountInformation objUpdateAccountInformation = new UpdateAccountInformation();
            // objUpdateAccountInformation.ShowDialog();
        }

        private void cmbParentAccountName_ValueChanged(object sender, EventArgs e)
        {
            int accountID = 0;

            if (cmbParentAccount.Value != null)
            {
                int.TryParse(cmbParentAccount.Value.ToString(), out accountID);
            }


            if (accountID != -1)
            {
                loadChildAccountInfo(accountID);
            }

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
            try
            {
                if (!grvJournalInfo.Rows[0].IsNewRow)
                {
                    string celltext = grvJournalInfo.CurrentCell.Value.ToString();
                    if (celltext == "Delete")
                    {
                        DialogResult m = MessageBox.Show("Do you want to delete?", "Pharmacy Management", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (m == System.Windows.Forms.DialogResult.Yes)
                        {
                            grvJournalInfo.Rows.Remove(grvJournalInfo.CurrentRow);
                            CalculateGridTotal();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Operation fail Please try again", "Pharmacy Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }



    }
}
