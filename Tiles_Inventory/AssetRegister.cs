using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IMFS.Common.DTO;

namespace Tiles_Inventory
{
    public partial class AssetRegister : Form
    {
        public delegate void InsertAsserRegister(string [] arrString);
        InsertAsserRegister InsertAssetReg=null;

        private SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=AccountingDB;Integrated Security=True");
        private SqlCommand cmd = null;

        public AssetRegister()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<string> lstAssetRegister = null;
            String[] assetRegister = new string[] { txtAssetName.Text, txtSize.Text, txtLifeTime.Text, txtWarranty.Text, txtDescriptionMethod.Text, cmbSupplierName.SelectedValue.ToString(), txtContactPerson.Text };
            Journal objJournalInformation = new Journal();
           // InsertAssetReg = objJournalInformation.InsertJournalInformation;
            InsertAssetReg.Invoke(assetRegister);
        }

        private void AssetRegister_Load(object sender, EventArgs e)
        {
            LoadSupplierInformation();
        }

        private void LoadSupplierInformation()
        {

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            cmd = new SqlCommand("Select SupplierCode,Name   from Supplier order by Name ", con);

            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(table);

            DataRow tableDR = table.NewRow();
            tableDR[0] = -1;
            tableDR[1] = "-Select-";
            table.Rows.Add(tableDR);

            cmbSupplierName.DataSource = table;
            cmbSupplierName.DisplayMember = "Name";
            cmbSupplierName.ValueMember = "SupplierCode";

            cmbSupplierName.SelectedValue = -1;



            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}
