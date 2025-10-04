using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using IFMS.Facade;
using IMFS.Common.DTO;
using Infragistics.Win.UltraWinGrid;
using NybSys.MTBF.Utility.Constants;
using IFMS.BLL;

namespace Tiles_Inventory
{
    public partial class frmViewSupplier : BaseForm
    {
        int supplierID = 0;

        public frmViewSupplier()
        {
            InitializeComponent();
            DataAccessProxy = new FacadeManager();

        }

        private void frmViewSupplier_Load(System.Object sender, System.EventArgs e)
        {
            LoadSupplierInformation();
        }


        private void btnAdd_Click(System.Object sender, System.EventArgs e)
        {
            SuppliersInformation frm = new SuppliersInformation(supplierID, false);
            frm.OnLoadSupplierInformation += OnSupplierInformationLoad;
            frm.ShowDialog();
        }

        private void btnEdit_Click(System.Object sender, System.EventArgs e)
        {

            if (grvSupplierInformation.Selected.Rows.Count > 0 )
            {
                supplierID = Convert.ToInt32(grvSupplierInformation.Selected.Rows[0].Cells["SupplierID"].Value);

                SuppliersInformation frm = new SuppliersInformation(supplierID, true);
                frm.OnLoadSupplierInformation += OnSupplierInformationLoad;
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a row", "Pharmacy Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClose_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }



        private DataTable BuildSuplierTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SupplierID");
            dt.Columns.Add("Supplier Name");
            dt.Columns.Add("Address");
            dt.Columns.Add("Phone");
            dt.Columns.Add("Email");
            dt.Columns.Add("Duscontinued");
            return dt;

        }


        /// <summary>
        /// Method to load supplier information in gid.
        /// </summary>
        /// <param name="companyId"></param>
        /// <remarks></remarks>
        private void LoadSupplierInformation()
        {
            List<Supplier> lstSuppler = new SupplierManager().GetAllSupplierByBranchID(MTBFConstants.AppConstants.BranchID);
            DataTable dt = BuildSuplierTable();
            foreach (Supplier supplier in lstSuppler)
            {
                DataRow row = dt.NewRow();
                row["SupplierID"] = supplier.SupplierID;
                row["Supplier Name"] = supplier.SupplierName;
                row["Address"] = supplier.Address;
                row["Phone"] = supplier.PhoneNumber;
                row["Email"] = supplier.Email;
                row["Duscontinued"] = supplier.Discontinued;
                dt.Rows.Add(row);
            }
            grvSupplierInformation.DataSource = dt;
            grvSupplierInformation.DisplayLayout.Bands[0].Columns["SupplierID"].Hidden = true;

        }

        private void OnSupplierInformationLoad()
        {

            LoadSupplierInformation();
        }



        private void btnRefresh_Click(object sender, EventArgs e)
        {

            LoadSupplierInformation();

        }

        private void cmbCompanyName_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridLayout layout = e.Layout;
            layout.PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
        }
    }
}
