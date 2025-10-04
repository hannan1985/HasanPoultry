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
using IMFS.Common.Utility;
using Tiles_Inventory.Reports;


namespace Tiles_Inventory
{
    public partial class frmOrder : BaseForm
    {
        private int _companyID;

        private string pharmacyName;
        private string pharmacyAddress;

        public frmOrder(int companyID)
        {
            _companyID = companyID;
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        private void frmOrder_Load(System.Object sender, System.EventArgs e)
        {
            LoadOrderableProductByCompanyID();
        }

        /// <summary>
        /// Method to load orderable product in grid  by company .
        /// </summary>
        /// <param name="companyID"></param>
        /// <remarks></remarks>
        private void LoadOrderableProductByCompanyID()
        {
            List<ReorderProduct> lstOrderProduct = new List<ReorderProduct>();
            lstOrderProduct = DataAccessProxy.GetOrderableProductByCompanyID(_companyID).Cast<ReorderProduct>().ToList();

            foreach (ReorderProduct reorderProduct in lstOrderProduct)
            {
                Product product = DataAccessProxy.GetProductInformationByID(reorderProduct.ProductID);
                ProductType productType = DataAccessProxy.GetProductTypeByID(product.ProductTypeID);

                grvProductDetials.Rows.Add(reorderProduct.ProductName, productType.TypeName);
            }
        }

        /// <summary>
        /// Method to print order. 
        /// </summary>
        private void PrintOrderData()
        {
            rptOrder op = new rptOrder();
            frmSalesReport objmainReport = new frmSalesReport();
            op.DataSource = BuildReportTable();
            objmainReport.rptViewer.Document = op.Document;
            objmainReport.rptViewer.Dock = DockStyle.Fill;
            SetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
            op.txtPharmacyName.Text = pharmacyName;
            op.txtAddress.Text = pharmacyAddress;
            Employee employee = DataAccessProxy.GetEmployeeByID(IFMSConstant.LoggedinUserID);

            op.txtOrderBy.Text = employee.EmployeeName;
            op.txtOrderDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            op.Run();
            objmainReport.ShowDialog();
        }

        private DataTable BuildReportTable()
        {
            DataTable table = new DataTable();

            table.Columns.Add("Product Name", typeof(string));
            table.Columns.Add("Product Type", typeof(string));
            table.Columns.Add("Box", typeof(decimal));
            table.Columns.Add("Pieces", typeof(int));

            for (int i = 0; i <= grvProductDetials.Rows.Count - 2; i++)
            {
                DataRow row = table.NewRow();

                row[0] = grvProductDetials.Rows[i].Cells[0].Value;
                row[1] = grvProductDetials.Rows[i].Cells[1].Value;
                row[2] = (grvProductDetials.Rows[i].Cells[2].Value == null ? 0 : grvProductDetials.Rows[i].Cells[2].Value);
                row[3] = (grvProductDetials.Rows[i].Cells[3].Value == null ? 0 : grvProductDetials.Rows[i].Cells[3].Value);

                table.Rows.Add(row);
            }


            return table;
        }

        private void btnPrint_Click(System.Object sender, System.EventArgs e)
        {
            PrintOrderData();
        }

        private void SetPharmachyInforamation(ref string PharmacyName, ref string Address)
        {
            Organization pharmacy = DataAccessProxy.GetAllPharmacy().Cast<Organization>().ToList().FirstOrDefault();
            if (pharmacy != null)
            {
                PharmacyName = pharmacy.OrganizationName;
                Address = pharmacy.Address + ", " + pharmacy.Phone + ", " + pharmacy.Fax;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
