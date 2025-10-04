using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IFMS.Facade;

using IMFS.Common.DTO;
using IMFS.Common.View;
using System.IO;
using NybSys.MTBF.Utility.Helper;
using Tiles_Inventory.Reports;
using NybSys.MTBF.Utility.Constants;

namespace Tiles_Inventory
{
    public partial class frmSearchSalesInformation : BaseForm
    {

        private string pharmacyName;
        private string pharmacyAddress;
        private int _CustomerID = 0;

        public frmSearchSalesInformation()
        {
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        private void btnShowInvoice_Click(System.Object sender, System.EventArgs e)
        {
            int salesID = 0;

            if (cmbSalesId.Text != string.Empty)
            {
                int.TryParse(cmbSalesId.Text, out salesID);
            }
            SalesOrder salesOrder = DataAccessProxy.GetSalesOrderByID(salesID);

            if (salesID > 0)
            {
                if (salesOrder.CustomerID > 0)
                {
                    PrintCreditInvoice(Convert.ToInt32(cmbSalesId.SelectedValue));
                }
                else
                {
                    PrintInvoice(Convert.ToInt32(cmbSalesId.SelectedValue));
                }
            }
            else
            {
                MessageBoxHelper.ShowInformation("Please select sales ID"); return;
            }

        }


        /// <summary>
        /// Method to insert print invoice.
        /// </summary>
        /// <param name="salesId"></param>
        private void PrintInvoice(int salesId)
        {
            Employee employee = new Employee();
            DataTable salesReport = new DataTable();
            Organization pharmacy = new Organization();

            salesReport = DataAccessProxy.GetSalesReport(salesId);

            SalesOrder salesOrder = DataAccessProxy.GetSalesOrderByID(salesId);

            rptInvoice op = new rptInvoice();
            frmSalesReport objmainReport = new frmSalesReport();
            op.DataSource = FillDataInSalesOrderTable();
            objmainReport.rptViewer.Document = op.Document;
            objmainReport.rptViewer.Dock = DockStyle.Fill;
            SetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress, ref pharmacy);
            op.lblCompanyName.Text = pharmacyName;
            op.lblAddress.Text = pharmacyAddress;

            op.txtSubTotal.Text = (salesOrder != null) ? (salesOrder.SalesAmount).ToString() : 0.ToString();
            op.txtDiscount.Text = (salesOrder != null) ? salesOrder.Discount.ToString() : 0.00.ToString();
            op.txtGrandTotal.Text = (salesOrder != null) ? (salesOrder.SalesAmount - salesOrder.Discount).ToString() : 0.ToString();
            op.lblInvoiceNumber.Text = (salesOrder != null) ? (salesOrder.SalesID).ToString() : 0.ToString();
            op.lblVATRegistrationNo.Text = pharmacy.RegistrationNumber;
            employee = DataAccessProxy.GetEmployeeByID(1);
            if (pharmacy.OrganizationLogo != null)
            {
                MemoryStream ms = new MemoryStream(pharmacy.OrganizationLogo);
                Image returnImage = Image.FromStream(ms);
                op.picLogo.Image = returnImage;
            }


            op.lblServedby.Text = employee.EmployeeName;
            op.Run();
            objmainReport.ShowDialog();

        }

        /// <summary>
        /// Method to build sales quotation detail table.
        /// </summary>
        /// <returns></returns>
        private DataTable BuildSalesTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Product Name");
            table.Columns.Add("Quantity");
            table.Columns.Add("Price");
            table.Columns.Add("VAT");
            table.Columns.Add("Total");
            return table;
        }

        private DataTable FillDataInSalesOrderTable()
        {
            DataTable productTable = BuildSalesTable();
            foreach (DataGridViewRow row in grvSearchSalesInformation.Rows)
            {
                // Product product = _InventoryProxy.GetProductByName(row.Cells[MTBFConstants.GridHeader.PRODUCT_NAME].Text);
                DataRow dtRow = productTable.NewRow();
                dtRow["Product Name"] = row.Cells["Product Name"].Value;// product.ProductName + "_" + product.ProductCode;
                dtRow["Quantity"] = row.Cells["Quantity"].Value;
                dtRow["Price"] = row.Cells["Price"].Value;
                dtRow["VAT"] = row.Cells["VAT"].Value;
                dtRow["Total"] = row.Cells["Total"].Value;
                productTable.Rows.Add(dtRow);
            }

            return productTable;
        }


        /// <summary>
        /// Method to insert print invoice.
        /// </summary>
        /// <param name="salesId"></param>
        private void PrintCreditInvoice(int salesId)
        {
            Employee employee = new Employee();
            DataTable salesReport = new DataTable();
            Organization pharmacy = new Organization();
            salesReport = DataAccessProxy.GetCreditSalesReport(salesId);

            rptCreditSalesShowRoom op = new rptCreditSalesShowRoom();
            frmSalesReport objmainReport = new frmSalesReport();
            op.DataSource = salesReport;
            objmainReport.rptViewer.Document = op.Document;
            objmainReport.rptViewer.Dock = DockStyle.Fill;

            SetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress, ref pharmacy);
            op.txtPharmacyName.Text = pharmacyName;
            op.txtAddress.Text = pharmacyAddress;
            if (pharmacy.OrganizationLogo != null)
            {
                MemoryStream ms = new MemoryStream(pharmacy.OrganizationLogo);
                Image returnImage = Image.FromStream(ms);
                op.picLogo.Image = returnImage;
            }
            employee = DataAccessProxy.GetEmployeeByID(1);

            // op.txtNetTotal.Text = DataAccessProxy.GetTotalSalesAmountWithOutDiscount(salesId).ToString();

            op.txtServiceMan.Text = employee.EmployeeName;
            op.Run();
            objmainReport.ShowDialog();

        }

        private void SetPharmachyInforamation(ref string PharmacyName, ref string Address, ref Organization pharmacy)
        {
            pharmacy = DataAccessProxy.GetAllPharmacy().Cast<Organization>().ToList().FirstOrDefault();
            if (pharmacy != null)
            {
                PharmacyName = pharmacy.OrganizationName;
                Address = pharmacy.Address + ", " + pharmacy.Phone + ", " + pharmacy.Fax;
            }
        }

        private void btnClose_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void frmSearchSalesInformation_Load(System.Object sender, System.EventArgs e)
        {
            LoadSalesIDCombo();
        }

        /// <summary>
        /// Method to load sales id combo
        /// </summary>
        private void LoadSalesIDCombo()
        {
            List<SalesOrder> lstsalesOrder = new List<SalesOrder>();
            lstsalesOrder = DataAccessProxy.GetAllSalesOrder().Cast<SalesOrder>().Where(p => p.BranchID == MTBFConstants.AppConstants.BranchID && p.OrganizationID == MTBFConstants.AppConstants.OrganizationID).ToList();
            cmbSalesId.DisplayMember = "SalesID";
            cmbSalesId.ValueMember = "SalesID";
            cmbSalesId.DataSource = lstsalesOrder;
        }


        /// <summary>
        /// Method to build sales information.
        /// </summary>
        /// <returns></returns>
        private DataTable BuildSalesInformationTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("SalesID");
            table.Columns.Add("Product Name");
            table.Columns.Add("Quantity");
            table.Columns.Add("Price");
            table.Columns.Add("VAT");
            table.Columns.Add("Total");
            table.Columns.Add("CustomerID");
            return table;
        }



        /// <summary>
        /// Method to load sales information in grid.
        /// </summary>
        /// <param name="salesID"></param>
        private void LoadSalesInformation(int salesID)
        {
            DataTable salesTable = BuildSalesInformationTable();
            try
            {
                foreach (CreditSales sales in DataAccessProxy.GetSalesInformationByID(salesID))
                {
                    DataRow row = salesTable.NewRow();
                    row["SalesID"] = sales.SalesID;
                    row["Product Name"] = sales.ProductName;
                    row["Quantity"] = sales.Quantity;
                    row["Price"] = sales.Price;
                    row["VAT"] = sales.VAT;
                    row["Total"] = ((sales.Price * sales.Quantity) + sales.VAT).ToString("00.00");
                    row["CustomerID"] = sales.CustomerID;
                    salesTable.Rows.Add(row);
                }
                grvSearchSalesInformation.DataSource = salesTable;
                grvSearchSalesInformation.Columns["CustomerID"].Visible = false;
                grvSearchSalesInformation.Columns[0].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Operation fail Please try again", "Pharmacy Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void cmbSalesId_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            if (cmbSalesId.Text != string.Empty)
            {
                LoadSalesInformation(Convert.ToInt32(cmbSalesId.SelectedValue));
            }
        }



    }
}
