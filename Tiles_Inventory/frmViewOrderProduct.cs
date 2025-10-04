using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IFMS.Facade;
using IMFS.Common.View;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Constants;
using IFMS.BLL;

namespace Tiles_Inventory
{
    public partial class frmViewOrderProduct : BaseForm
    {
        public frmViewOrderProduct()
        {
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        /// <summary>
        /// Method to load orderable product in grid  by company .
        /// </summary>
        /// <param name="companyID"></param>
        /// <remarks></remarks>
        private void LoadOrderableProductByCompanyID(int companyID)
        {
            List<ReorderProduct> lstOrderProduct = new List<ReorderProduct>();

            lstOrderProduct = DataAccessProxy.GetOrderableProductByCompanyID(companyID).Cast<ReorderProduct>().ToList();

            grvProductDetials.DataSource = lstOrderProduct;

        }

        /// <summary>
        /// Method to load company informaiton in combo box.
        /// </summary>
        private void LoadCompanyName()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("Name");

            List<Supplier> lstSupplier = new List<Supplier>();
            lstSupplier = new SupplierManager().GetAllSupplierByBranchID(MTBFConstants.AppConstants.BranchID);

            foreach (Supplier company in lstSupplier)
            {
                DataRow row = table.NewRow();
                row[0] = company.SupplierID;
                row[1] = company.SupplierName;
                table.Rows.Add(row);
            }

            cmbCompanyName.DisplayMember = "Name";
            cmbCompanyName.ValueMember = "ID";
            cmbCompanyName.DataSource = table;
        }

        private void cmbCompanyName_ValueChanged(System.Object sender, System.EventArgs e)
        {
            LoadOrderableProductByCompanyID(Convert.ToInt32(cmbCompanyName.Value));
        }

        private void frmViewOrderedProduct_Load(System.Object sender, System.EventArgs e)
        {
            LoadCompanyName();
        }

        private void btnPrint_Click(System.Object sender, System.EventArgs e)
        {
            if (cmbCompanyName.Text != string.Empty && grvProductDetials.Rows.Count > 0)
            {
                frmOrder frm = new frmOrder(Convert.ToInt32(cmbCompanyName.Value));
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("There is no product to order.", "Pharmacy Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnRefresh_Click(System.Object sender, System.EventArgs e)
        {
            if (cmbCompanyName.Text != string.Empty)
            {
                LoadOrderableProductByCompanyID(Convert.ToInt32(cmbCompanyName.Value));
            }
        }

        private void btnClose_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

    }
}
