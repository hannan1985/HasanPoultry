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
    public partial class frmAccountType : Form
    {
        private SqlConnection con = new SqlConnection();//Connection.ConnectionString()
        private SqlCommand cmd = null;
        private SqlDataReader dr = null;

        public delegate void LodaEventHandaler();
        public event LodaEventHandaler OnAccountTypeAdded;

        public string aHCode = string.Empty;

        public frmAccountType(string accountHeadCode)
        {
            aHCode = accountHeadCode;
            InitializeComponent();
        }

        private void frmAccountType_Load(object sender, EventArgs e)
        {
            LoadAccountHead();
        }


        /// <summary>
        /// Method to laod account head in combo box.
        /// </summary>
        private void LoadAccountHead()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            cmd = new SqlCommand("Select Distinct AccountHeadID,Description  from AccountHead order by Description  ", con);

            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(table);

            DataRow tableDR = table.NewRow();
            tableDR[0] = -1;
            tableDR[1] = "-Select-";
            table.Rows.Add(tableDR);

            cmbAccountHead.DataSource = table;
            cmbAccountHead.DisplayMember = "Description";
            cmbAccountHead.ValueMember = "AccountHeadID";

            if (aHCode != string.Empty)
            {
                cmbAccountHead.SelectedValue = aHCode;
                cmbAccountHead.Enabled = false;
            }
            else
            {
                cmbAccountHead.SelectedValue = -1;
            }


            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }


        private string GetAccountTypeCode(string accountHeadCode)
        {
            int accountTypeCode = 0;


            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            cmd = new SqlCommand("SELECT isnull(MAX(CAST(SUBSTRING(AccountTypeCode,1,2)AS INT)),0) FROM AccountType WHERE AccountHeadCode ='" + accountHeadCode + "'", con);
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                int.TryParse(dr[0].ToString(), out accountTypeCode);
            }

            dr.Close();



            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            return GenerateAccountTypeCode(accountHeadCode, accountTypeCode + 1);
        }
        /// <summary>
        /// Method to generate activity center code.
        /// </summary>
        private string GenerateAccountTypeCode(string accountHeadCode, int accountTypeCode)
        {
            return accountHeadCode + accountTypeCode.ToString().PadLeft(2, '0');
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string accountTypeCode = string.Empty;

            accountTypeCode = GetAccountTypeCode(cmbAccountHead.SelectedValue.ToString());

            if (Convert.ToInt32(cmbAccountHead.SelectedValue) != -1 && txtAccountName.Text != String.Empty)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                cmd = new SqlCommand("Insert Into AccountType values('" + accountTypeCode + "','" + txtAccountName.Text + "','" + cmbAccountHead.SelectedValue + "','" + "Hannan" + "','" + "2013-03-12" + "', 1" + ")", con);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Successfully saved", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbAccountHead.Enabled = true;

                if (OnAccountTypeAdded != null)
                    OnAccountTypeAdded();

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
