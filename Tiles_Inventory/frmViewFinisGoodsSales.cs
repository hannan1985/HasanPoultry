using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IMFS.Common.DTO;
using IFMS.Facade;
using NybSys.MTBF.Utility.Helper;

using System.IO;
using IMFS.Common.Utility;
using Tiles_Inventory.Reports;
using NybSys.MTBF.Utility.Constants;
using NybSys.MTBF.Utility.Enums;
using IFMS.BLL;

namespace Tiles_Inventory
{
    public partial class frmViewFinisGoodsSales : BaseForm
    {
        private string pharmacyName;
        private string pharmacyAddress;
        private List<StockSalesDetails> _lstStockDetails = new List<StockSalesDetails>();
        private List<StockSales> _lstStockSales = new List<StockSales>();

        public frmViewFinisGoodsSales()
        {
            InitializeComponent();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            LoadStockSalesInformationByDate(dtpFromDate.Value.ToString("yyyy-MM-dd"), dtpToDate.Value.ToString("yyyy-MM-dd"));
        }

        /// <summary>
        /// Method to build sales detail table
        /// </summary>
        /// <returns></returns>
        private DataTable BuildSalesDetailTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Product Name");
            table.Columns.Add("Quantity");
            table.Columns.Add("Price");
            table.Columns.Add("Total");
            return table;
        }


        private void LoadStockDetails(int salesID)
        {
            DataTable purchaseDetailtable = BuildSalesDetailTable();
            _lstStockDetails = new StockSalesManager().GetAllStockSalesDetailByStockSalesID(salesID).Cast<StockSalesDetails>().ToList();

            foreach (StockSalesDetails orderDetail in _lstStockDetails)
            {
                DataRow row = purchaseDetailtable.NewRow();
                Product product = new ProductManager().GetProductInformationByID(orderDetail.ProductID);
                row["Product Name"] = product.ProductName;
                row["Quantity"] = orderDetail.Quantity;
                row["Price"] = orderDetail.Price;
                row["Total"] = (orderDetail.Price * orderDetail.Quantity).ToString("00.00");
                purchaseDetailtable.Rows.Add(row);
            }
            grvStockDetails.DataSource = purchaseDetailtable;
        }

        private DataTable BuildSalesTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("SalesID");
            table.Columns.Add("Sales Date");
            table.Columns.Add("Customer Name");
            table.Columns.Add("Customer Phone");
            table.Columns.Add("Sales Amount");
            table.Columns.Add("Discount");
            table.Columns.Add("Receive Amount");
            table.Columns.Add("Staus");
            return table;
        }

        /// <summary>
        /// Method to load purchase order infromation in grid
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        private void LoadStockSalesInformationByDate(string fromDate, string toDate)
        {
            grvStockSales.Rows.Clear();
            //new StockSalesManager().GetAllPurchaseOrder().Cast<PurchaseOrder>().Where(p => p.PurchaseDate >= Convert.ToDateTime(fromDate) && p.PurchaseDate <= Convert.ToDateTime(toDate))
            fromDate = fromDate + " 00:00:01";
            toDate = toDate + " 23:59:59";

            string filter = string.Format("{0} between '{1}' and '{2}' And {3}={4} And {5}={6}", "SalesDate", fromDate, toDate, "BranchID", MTBFConstants.AppConstants.BranchID, "OrganizationID", MTBFConstants.AppConstants.OrganizationID);

            _lstStockSales = new StockSalesManager().GetFilteredStockSales(filter).Cast<StockSales>().ToList();

            DataTable salesTable = BuildSalesTable();

            foreach (StockSales stockSales in _lstStockSales)
            {
                SalesParty salesParty = new StockSalesManager().GetSalesPartyByID(stockSales.PartyID);

                DataGridViewRow row = grvStockSales.Rows[0].Clone() as DataGridViewRow;
                int index = grvStockSales.Rows.Add(row);

                grvStockSales.Rows[index].Cells["SalesID"].Value = stockSales.StockSalesID;
                grvStockSales.Rows[index].Cells["SalesDate"].Value = stockSales.SalesDate.ToShortDateString();
                grvStockSales.Rows[index].Cells["CustomerName"].Value = salesParty.PartyName;
                grvStockSales.Rows[index].Cells["CustomerPhone"].Value = salesParty.Phone;
                grvStockSales.Rows[index].Cells["SalesAmount"].Value = stockSales.Total;
                grvStockSales.Rows[index].Cells["ReceiveAmount"].Value = stockSales.ReceiveAmount;
            }
            if (_lstStockSales.Count == 0)
            {
                MessageBoxHelper.ShowInformation("No data found for this combination.");
            }
        }

        private void grvStockSales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grvStockSales.SelectedRows.Count > 0 && grvStockSales.SelectedRows[0].Cells["SalesID"].Value != null && !string.IsNullOrEmpty(grvStockSales.SelectedRows[0].Cells["SalesID"].Value.ToString()))
            {
                int salesID = Convert.ToInt32(grvStockSales.SelectedRows[0].Cells["SalesID"].Value);
                LoadStockDetails(salesID);
            }
        }


        /// <summary>
        /// Method to insert sales order information.
        /// </summary>
        /// <param name="salesId"></param>
        /// <returns></returns>
        private List<StockSalesDetails> GetAllStockDetailsInformation(int stockSalesID)
        {
            List<StockSalesDetails> lstStockDetails = new List<StockSalesDetails>();

            foreach (StockSalesDetails orderDetail in new StockSalesManager().GetAllStockSalesDetailByStockSalesID(stockSalesID))
            {
                orderDetail.Quantity = 0;
                lstStockDetails.Add(orderDetail);
            }
            return lstStockDetails;
        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (grvStockSales.SelectedRows.Count > 0)
            {
                int salesID = Convert.ToInt32(grvStockSales.SelectedRows[0].Cells["SalesID"].Value);
                PrintInvoice(salesID);
            }
            else
            {
                MessageBoxHelper.ShowInformation("You need to select a sales order");
            }
        }



        private DataTable BuildReportTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("SalesID");
            table.Columns.Add("Party Name");
            table.Columns.Add("Phone");
            table.Columns.Add("Address");
            table.Columns.Add("Sales Date");
            table.Columns.Add("Product Name");
            table.Columns.Add("Quantity");
            table.Columns.Add("Price");
            table.Columns.Add("Total");
            table.Columns.Add("Sales Amount");
            table.Columns.Add("Receive Amount");
            table.Columns.Add("Due");
            return table;
        }

        private void PrintInvoice(int salesId)
        {
            string pharmacyName = string.Empty;
            string pharmacyAddress = string.Empty;
            Employee employee = new Employee();
            DataTable salesReport = new DataTable();
            Organization pharmacy = new Organization();
            salesReport = SetReportData(salesId);

            rptPartySalesReport op = new rptPartySalesReport();
            frmSalesReport objmainReport = new frmSalesReport();
            op.DataSource = salesReport;
            objmainReport.rptViewer.Document = op.Document;
            objmainReport.rptViewer.Dock = DockStyle.Fill;

            SetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress, ref pharmacy);

            op.txtPharmacyName.Text = pharmacyName;
            op.txtAddress.Text = pharmacyAddress;
            employee = new EmployeeManager().GetEmployeeByID(IFMSConstant.LoggedinUserID);
            if (pharmacy.OrganizationLogo != null)
            {
                MemoryStream ms = new MemoryStream(pharmacy.OrganizationLogo);
                Image returnImage = Image.FromStream(ms);
                op.picLogo.Image = returnImage;
            }

            op.txtServiceMan.Text = (employee != null) ? employee.EmployeeName : "Super Admin";
            op.Run();
            objmainReport.MdiParent = this.MdiParent;
            objmainReport.Show();

        }

        private DataTable SetReportData(int salesId)
        {

            DataTable table = BuildReportTable();

            StockSales stockSales = new StockSalesManager().GetStockSalesByID(salesId);
            SalesParty party = new StockSalesManager().GetSalesPartyByID(stockSales.PartyID);
            foreach (StockSalesDetails sDetail in new StockSalesManager().GetAllStockSalesDetailByStockSalesID(salesId))
            {
                Product product = new ProductManager().GetProductByID(sDetail.ProductID);
                DataRow row = table.NewRow();
                row["SalesID"] = stockSales .StockSalesID;
                row["Party Name"] = party.PartyName;
                row["Phone"] = party.Phone;
                row["Address"] = party.Address;
                row["Sales Date"] = stockSales.SalesDate.ToString("dd/MM/yyyy");
                row["Product Name"] = product.ProductName;
                row["Quantity"] = sDetail.Quantity;
                row["Price"] = sDetail.Price;
                row["Total"] = sDetail.Total;
                row["Sales Amount"] = stockSales.Total;
                row["Receive Amount"] = stockSales.ReceiveAmount;
                row["Due"] = stockSales.Total - stockSales.ReceiveAmount;
                table.Rows.Add(row);
            }

            return table;

        }

        private void SetPharmachyInforamation(ref string PharmacyName, ref string Address, ref Organization pharmacy)
        {
            pharmacy = new PharmacyManager().GetAllPharmacy().Cast<Organization>().ToList().FirstOrDefault();
            Branch branch = new BranchManager().GetBranchByID(MTBFConstants.AppConstants.BranchID);
            if (pharmacy != null)
            {
                PharmacyName = branch.BranchName;
                Address = branch.Address + ", " + branch.Phone + ", " + pharmacy.Fax;
            }
        }








        private DataTable BuildChalanTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ProductName");
            table.Columns.Add("Price");
            table.Columns.Add("Box");
            table.Columns.Add("Quantity");
            table.Columns.Add("SquareFeet");
            table.Columns.Add("Price");
            table.Columns.Add("Total");
            return table;
        }



        private void btnSalesHistory_Click(object sender, EventArgs e)
        {
            frmProductSalesHistory frm = new frmProductSalesHistory();
            frm.ShowDialog();
        }

        private void btnPrintChalan_Click(object sender, EventArgs e)
        {
            if (grvStockSales.SelectedRows.Count > 0)
            {
                int salesID = Convert.ToInt32(grvStockSales.SelectedRows[0].Cells["SalesID"].Value);

            }
            else
            {
                MessageBoxHelper.ShowInformation("You need to select a sales order");
            }
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {

            if (grvStockSales.SelectedRows.Count > 0)
            {
                int salesID = Convert.ToInt32(grvStockSales.SelectedRows[0].Cells["SalesID"].Value);
                StockSales StockSales = _lstStockSales.Where(s => s.StockSalesID == salesID).Cast<StockSales>().ToList().FirstOrDefault();

                frmSalesFinishGoods frm = new frmSalesFinishGoods(salesID, true);
                frm.ShowDialog();
            }
            else
            {
                MessageBoxHelper.ShowInformation("You need to select a sales order");
            }
        }

        private void frmViewSales_Load(object sender, EventArgs e)
        {
            base.CheckAction(this);
        }




    }
}
