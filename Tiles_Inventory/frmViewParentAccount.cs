using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Tiles_Inventory
{
    public partial class frmViewParentAccount : Form
    {
        private SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=AccountingDB;Integrated Security=True");

        private SqlCommand cmd = null;
        private SqlDataReader dr = null;

        public frmViewParentAccount()
        {
            InitializeComponent();
        }

        private void frmViewParentAccount_Load(object sender, EventArgs e)
        {
            LoadAccountType();
        }


        /// <summary>
        /// Method to load parent account information in grid.
        /// </summary>
        private void LoadParentAccountInfo(int accountTypeID)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            cmd = new SqlCommand("Select pa.AccountID,at.AccountTypeID,at.AccountTypeName ,AccountsName,IsBalanceSheetItem,IsIncomeStatementItem ,IsTradingAccountItem  from dbo.ParentAccount pa join AccountType at on pa.AccountTypeID =at.AccountTypeID  where at.AccountTypeID =" + accountTypeID + " order by pa.AccountsName ", con);

            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(table);

            foreach (DataRow row in table.Rows)
            {
                grvParentAccountInfo.Rows.Add(row[0], row[1], row[2], row[3], row[4], row[5], row[6]);
            }

            //grvParentAccountInfo.DataSource = table;
            grvParentAccountInfo.Columns[0].Visible = false;
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        /// <summary>
        /// Method to laod account type in combo box.
        /// </summary>
        private void LoadAccountType()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            cmd = new SqlCommand("Select Distinct AccountTypeID ,AccountTypeName  from AccountType  order by AccountTypeName    ", con);

            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(table);

            DataRow tableDR = table.NewRow();
            tableDR[0] = -1;
            tableDR[1] = "-Select-";
            table.Rows.Add(tableDR);


            cmbAccountType.DataSource = table;

            cmbAccountType.DisplayMember = "AccountTypeName";
            cmbAccountType.ValueMember = "AccountTypeID";

            cmbAccountType.Value = -1;

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbAccountType.Value) != -1)
            {
                grvParentAccountInfo.Rows.Clear();
                LoadParentAccountInfo(Convert.ToInt32(cmbAccountType.Value));
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int parentAccountID = 0;
            if (grvParentAccountInfo.SelectedRows.Count > 0)
            {
                parentAccountID = Convert.ToInt32(grvParentAccountInfo.SelectedRows[0].Cells[0].Value);
                AddParentAccount frm = new AddParentAccount(grvParentAccountInfo.SelectedRows[0].Cells[1].Value.ToString(), parentAccountID, true);
                frm.OnParentAccountAddad += new AddParentAccount.LodaEventHandaler(frm_OnParentAccountAddad);
                frm.ShowDialog();
            }
        }

        void frm_OnParentAccountAddad()
        {
            if (Convert.ToInt32(cmbAccountType.Value) != -1)
            {
                grvParentAccountInfo.Rows.Clear();
                LoadParentAccountInfo(Convert.ToInt32(cmbAccountType.Value));
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
