using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IFMS.Facade;
using IMFS.Common.DTO;

namespace Tiles_Inventory
{
    public partial class frmUserSettings : BaseForm
    {
        public frmUserSettings()
        {
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
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

            List<ParentAccount> lstParentAccount = DataAccessProxy.GetAllParentAccount().Cast<ParentAccount>().ToList();

            table.Columns.Add("ID");
            table.Columns.Add("Name");

            DataRow tableDR = table.NewRow();
            tableDR[0] = -1;
            tableDR[1] = "-Select-";
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

        private void LoadJournalAccountInfo(int childAccountId)
        {
            DataTable table = new DataTable();
            List<JournalAccountsInformation> lstJournalAccount = DataAccessProxy.GetAllJournalAccount().Cast<JournalAccountsInformation>().Where(j => j.ChildAccountID == childAccountId).ToList();

            table.Columns.Add("ID");
            table.Columns.Add("Name");

            DataRow tableDR = table.NewRow();
            tableDR[0] = -1;
            tableDR[1] = "-Select-";
            table.Rows.Add(tableDR);



            foreach (JournalAccountsInformation jAccount in lstJournalAccount)
            {
                DataRow row = table.NewRow();
                row[0] = jAccount.JAccountID;
                row[1] = jAccount.AccountName;
                table.Rows.Add(row);
            }
            cmbSubAccountName.DataSource = null;

            cmbSubAccountName.DataSource = table;
            cmbSubAccountName.DisplayMember = "Name";
            cmbSubAccountName.ValueMember = "ID";

            cmbSubAccountName.Value = -1;

        }

        private void InactiveAllCombo()
        {
            cmbParentAccount.Enabled = false;
            cmbSubAccountName.Enabled = false;
            cmbAccountName.Enabled = false;
            cmbSalesParentAccount.Enabled = false;
            cmbSalesChildAccount.Enabled = false;
            cmbSalesJournalAccount.Enabled = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
