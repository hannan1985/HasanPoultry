using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using IFMS.Facade;
using IMFS.Common.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Tiles_Inventory
{
    public partial class AddJournalAccountInformation : BaseForm
    {
        private SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=AccountingDB;Integrated Security=True");

        private SqlCommand cmd = null;
        private SqlDataReader dr = null;
        private int _childAccountID = 0;

        public delegate void LodaEventHandaler();
        public event LodaEventHandaler OnJournalAccountAddad;

        public AddJournalAccountInformation(string childAccountID)
        {
            _childAccountID = Convert.ToInt32(childAccountID);
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        private void AddJournalAccountInformation_Load(object sender, EventArgs e)
        {
            LoadChildAccount();
            cmbChildAccount.SelectedValue = _childAccountID;
            if (_childAccountID > 0)
            {
                cmbChildAccount.Enabled = false;
            }

        }

        /// <summary>
        /// Method to load child account information in combo box.
        /// </summary>
        private void LoadChildAccount()
        {

            ChildAccount childAccount = new ChildAccount();
            List<ChildAccount> lstChildAccount = new List<ChildAccount>();
            childAccount.ChildAccountID = -1;
            childAccount.Description = "-Select-";

            lstChildAccount = DataAccessProxy.GetAllChildAccount().Cast<ChildAccount>().ToList();

            lstChildAccount.Insert(0, childAccount);

            cmbChildAccount.DataSource = lstChildAccount;
            cmbChildAccount.DisplayMember = "Description";
            cmbChildAccount.ValueMember = "ChildAccountID";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (InsertJournalAccount() > 0)
            {
                MessageBox.Show("Information saved succssfully", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (OnJournalAccountAddad != null)
                    OnJournalAccountAddad();
            }
        }


        /// <summary>
        /// Method to insert journal account Information.
        /// </summary>
        /// <returns></returns>
        private int InsertJournalAccount()
        {
            int result = 0;
            JournalAccountsInformation journalAccount = new JournalAccountsInformation();
            ChildAccount childAccount = new ChildAccount();
            ParentAccount parentAccount = new ParentAccount();
            AccountType accountType = new AccountType();

            try
            {
                childAccount = DataAccessProxy.GetChildAccountByID(Convert.ToInt32(cmbChildAccount.SelectedValue));
                parentAccount = DataAccessProxy.GetParentAccountByID(childAccount.AccountID);
                accountType = DataAccessProxy.GetAccountTypeByID(parentAccount.AccountTypeID);

                journalAccount.ChildAccountID = Convert.ToInt32(cmbChildAccount.SelectedValue);
                journalAccount.AccountID = childAccount.AccountID;
                journalAccount.AccountTypeID = parentAccount.AccountTypeID;
                journalAccount.AccountHeadID = accountType.AccountHeadID;
                journalAccount.AccountName = txtAccountName.Text;
                journalAccount.CreatedBy = "Hannan";
                journalAccount.CreatedDate = DateTime.Now;
                journalAccount.Status = 1;
                DataAccessProxy.InsertJournalAccount(journalAccount);

                result = 1;
            }
            catch (Exception)
            {
                result = 0;
            }

            return result;
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
