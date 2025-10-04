using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using IFMS.Facade;
using IMFS.Common.View;
using IMFS.Common.DTO;
using IMFS.Common.Utility;
using Tiles_Inventory.Reports;
using NybSys.MTBF.Utility.Constants;
using Infragistics.Win.UltraWinGrid;
using NybSys.MTBF.Utility.Helper;
using IFMS.BLL;


namespace Tiles_Inventory
{
    public partial class frmMainReport : BaseForm
    {
        private string pharmacyName;
        private string pharmacyAddress;
        private string _fromDate = string.Empty;
        private string _toDate = string.Empty;
        private string dayStartTime = " 00:00:00";
        private string dayEndTime = " 23:59:59";
        List<Zone> lstZone = new List<Zone>();
        List<SalesRepresentative> lstSalesRepresentative = new List<SalesRepresentative>();
        public frmMainReport()
        {
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        private void MainReport_Load(System.Object sender, System.EventArgs e)
        {
            dtpFromDate.Visible = false;
            dtpToDate.Visible = false;
            Label2.Visible = false;
            Label3.Visible = false;
            LoadReportName();
            LoadCustomerCombo();
            rptViewer.Dock = DockStyle.Fill;
        }

        private void btnClose_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void cmbReportName_ValueChanged(System.Object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbReportName.Text))
            {
                return;
            }
            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.PurchaseDetial)
            {
                HideAllControlls();
                dtpFromDate.Visible = true;
                dtpToDate.Visible = true;
                Label2.Visible = true;
                Label3.Visible = true;

            }
            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.SalesDetail)
            {
                HideAllControlls();
                dtpFromDate.Visible = true;
                dtpToDate.Visible = true;
                Label2.Visible = true;
                Label3.Visible = true;

            }
            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.CustomerOutstanding)
            {
                HideAllControlls();
                cmbCustomerName.DataSource = null;
                lblCaption.Text = "Customer Name";
                LoadCustomerCombo();
                cmbCustomerName.Visible = true;
                lblCaption.Visible = true;
                dtpFromDate.Visible = true;
                dtpToDate.Visible = true;
                Label2.Visible = true;
                Label3.Visible = true;
            }
            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.SupplierOutstanding)
            {
                HideAllControlls();
                cmbCustomerName.DataSource = null;
                lblCaption.Text = "Supplier Name";
                LoadSupplierInformation();
                cmbCustomerName.Visible = true;
                lblCaption.Visible = true;
            }

            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.ProfitReport)
            {
                HideAllControlls();
                dtpFromDate.Visible = true;
                dtpToDate.Visible = true;
                Label2.Visible = true;
                Label3.Visible = true;
            }
            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.AllCustomerOutstanding)
            {
                HideAllControlls();
                dtpFromDate.Visible = true;
                dtpToDate.Visible = true;
                Label2.Visible = true;
                Label3.Visible = true;
            }
            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.AllSupplierOutstanding)
            {
                HideAllControlls();
                dtpFromDate.Visible = true;
                dtpToDate.Visible = true;
                Label2.Visible = true;
                Label3.Visible = true;
            }

            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.CashReceiveAccordingToDate)
            {
                HideAllControlls();
                dtpFromDate.Visible = true;
                dtpToDate.Visible = true;
                Label2.Visible = true;
                Label3.Visible = true;
            }

            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.CashPaymentAccordingToDate)
            {
                HideAllControlls();
                dtpFromDate.Visible = true;
                dtpToDate.Visible = true;
                Label2.Visible = true;
                Label3.Visible = true;
            }
            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.StockDetail)
            {
                HideAllControlls();
                dtpFromDate.Visible = true;
                dtpToDate.Visible = true;
                Label2.Visible = true;
                Label3.Visible = true;
            }
            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.ZoneWiseSalesReport)
            {
                HideAllControlls();
                lblCaption.Visible = true;
                lblCaption.Text = "Zone Name";
                cmbCustomerName.Visible = true;
                dtpFromDate.Visible = true;
                dtpToDate.Visible = true;
                Label2.Visible = true;
                Label3.Visible = true;
                LoadZoneInformation();
            }
            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.DateAndZoneWiseDue)
            {
                HideAllControlls();
                lblCaption.Visible = true;
                lblCaption.Text = "Zone Name";
                cmbCustomerName.Visible = true;
                dtpFromDate.Visible = true;
                dtpToDate.Visible = true;
                Label2.Visible = true;
                Label3.Visible = true;
                LoadZoneInformation();
            }
            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.CustomerSalesDetail)
            {
                HideAllControlls();
                cmbCustomerName.DataSource = null;
                lblCaption.Text = "Customer Name";
                LoadCustomerCombo();
                cmbCustomerName.Visible = true;
                lblCaption.Visible = true;
                dtpFromDate.Visible = true;
                dtpToDate.Visible = true;
                Label2.Visible = true;
                Label3.Visible = true;
            }

            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.SalesmanwiseSalesReport)
            {
                HideAllControlls();
                dtpFromDate.Visible = true;
                dtpToDate.Visible = true;
                Label2.Visible = true;
                Label3.Visible = true;
            }
            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.SalesReturnHistory)
            {
                HideAllControlls();
                dtpFromDate.Visible = true;
                dtpToDate.Visible = true;
                Label2.Visible = true;
                Label3.Visible = true;

            }
            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.CustomerTransaction)
            {
                HideAllControlls();
                cmbCustomerName.DataSource = null;
                lblCaption.Text = "Customer Name";
                LoadCustomerCombo();
                cmbCustomerName.Visible = true;
                lblCaption.Visible = true;
                dtpFromDate.Visible = true;
                dtpToDate.Visible = true;
                Label2.Visible = true;
                Label3.Visible = true;
            }

            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.SalesRepresentativeSales)
            {
                HideAllControlls();
                cmbCustomerName.DataSource = null;
                lblCaption.Text = "Representative";
                LoadSalesRepresentativeCombo();
                cmbCustomerName.Visible = true;
                lblCaption.Visible = true;
                dtpFromDate.Visible = true;
                dtpToDate.Visible = true;
                Label2.Visible = true;
                Label3.Visible = true;

            }
            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.ProductUsedAndReceivedReport)
            {

                HideAllControlls();
                cmbCustomerName.DataSource = null;
                lblCaption.Text = "Party Name";
                LoadSalesCenterCombo();
                cmbCustomerName.Visible = false;
                cmbSellerCombo.Visible = true;

                lblCaption.Visible = true;
                dtpFromDate.Visible = true;
                dtpToDate.Visible = true;
                Label2.Visible = true;
                Label3.Visible = true;
            }
            else
            {
                HideAllControlls();
            }
        }

        private void LoadSalesCenterCombo()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");

            DataRow emptyRrow = dt.NewRow();
            emptyRrow["ID"] = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            emptyRrow["Name"] = MTBFConstants.DataField.COMBO_DEFAULT_NAME;
            dt.Rows.Add(emptyRrow);

            List<SalesCenter> lstSalesCenter = new List<SalesCenter>();
            lstSalesCenter = new SalesCenterManager().GetAllSalesCenter().Where(s => s.BranchID == MTBFConstants.AppConstants.BranchID && s.OrganizationID == MTBFConstants.AppConstants.OrganizationID).ToList();
            foreach (SalesCenter salesCenter in lstSalesCenter)
            {
                DataRow row = dt.NewRow();
                row["ID"] = salesCenter.SalesCenterID;
                row["Name"] = salesCenter.SalesCenterName;
                dt.Rows.Add(row);
            }

            cmbSellerCombo.DataSource = dt;
            cmbSellerCombo.DisplayMember = "Name";
            cmbSellerCombo.ValueMember = "ID";
            cmbSellerCombo.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
        }


        private void btnShowReport_Click(System.Object sender, System.EventArgs e)
        {
            btnDownload.Enabled = false;
            if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.PurchaseDetial)
            {
                PrintPurchaseReportAccourdingToDate();
            }
            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.TypeWiseInventory)
            {
                PrintProductTypeWiseInventoryReport();
            }
            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.SalesDetail)
            {
                PrintSalesReportAccourdingToDate();
            }
            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.InventoryDetail)
            {
                PrintInventoryReport();
            }
            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.CustomerOutstanding)
            {
                if (cmbCustomerName.Value != null)
                {
                    CustomerOutStandingReport();
                }
                else
                {
                    MessageBox.Show("Please select customer name.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbCustomerName.Focus();
                }
            }
            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.SupplierOutstanding)
            {
                if (cmbCustomerName.Value != null)
                {
                    PrintSuplierOutstandingReport();
                }
                else
                {
                    MessageBox.Show("Please select customer name.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbCustomerName.Focus();
                }
            }
            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.ProfitReport)
            {
                PrintProfitReport();
            }
            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.AllCustomerOutstanding)
            {
                PrintAllCustomerOutstanding();

            }
            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.AllSupplierOutstanding)
            {
                PrintAllSupplierOutstanding();

            }
            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.CashReceiveAccordingToDate)
            {
                CashReceiveInformationAccordingToDate();

            }
            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.CashPaymentAccordingToDate)
            {
                CashPaymentInformationAccordingToDate();
            }
            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.StockDetail)
            {
                StockDetailInformation();
            }
            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.ZoneWiseSalesReport)
            {
                ZoneWiseSalesReport();
            }
            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.DateAndZoneWiseDue)
            {
                DateAndZoneWiseDueReport();
            }
            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.CustomerSalesDetail)
            {
                if (cmbCustomerName.Value != null)
                {
                    CustomerWiseSalesDetailReport(Convert.ToInt32(cmbCustomerName.Value));
                }
                else
                {
                    MessageBox.Show("Please select customer name.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbCustomerName.Focus();
                }

            }
            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.SalesReturnHistory)
            {
                PrintSalesReturnReport();
            }

            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.SalesmanwiseSalesReport)
            {
                PrintSalesmanWiseSalesReport();
            }
            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.SalesRepresentativeSales)
            {
                int salresrepresentativeID = 0;

                if (cmbCustomerName.Value != null)
                {
                    int.TryParse(cmbCustomerName.Value.ToString(), out salresrepresentativeID);
                    SalesRepresentativeSalesReport(salresrepresentativeID);
                }
                else
                {
                    MessageBox.Show("Please select sales representative.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbCustomerName.Focus();
                }

            }

            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.CustomerTransaction)
            {
                if (cmbCustomerName.Value != null)
                {
                    CustomerTransactionReport();
                }
                else
                {
                    MessageBox.Show("Please select customer name.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbCustomerName.Focus();
                }
            }

            else if (Convert.ToInt32(cmbReportName.Value) == (int)IFMSEnum.ReportName.ProductUsedAndReceivedReport)
            {
                int salresrepresentativeID = 0;
                if (cmbSellerCombo.Value != null)
                {
                    int.TryParse(cmbSellerCombo.Value.ToString(), out salresrepresentativeID);
                    LoadPartyProductConsumtionAndReceiveReport(salresrepresentativeID);
                }
                else
                {
                    MessageBox.Show("Please select customer name.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbCustomerName.Focus();
                }
            }

        }



        private void LoadPartyProductConsumtionAndReceiveReport(int partyID)
        {

            List<ProductConsumtionAndReceive> lstProductConsumtionAndReceive = new List<ProductConsumtionAndReceive>();
            lstProductConsumtionAndReceive = new ProductUsedManager().GetProductUsedAndReceieveInfo(partyID);
            if (lstProductConsumtionAndReceive.Count > 0)
            {
                rptProductReceive op = new rptProductReceive();
                op.DataSource = lstProductConsumtionAndReceive;
                rptViewer.Document = op.Document;
                rptViewer.Dock = DockStyle.Fill;
                GetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
                op.txtPharmacyName.Text = pharmacyName;
                op.txtAddress.Text = pharmacyAddress;

                op.txtFromDate.Text = dtpFromDate.Value.ToString("dd-MMM-yyyy");
                op.txtToDate.Text = dtpToDate.Value.ToString("dd-MMM-yyyy");



                op.Run();
                btnDownload.Enabled = true;
            }
            else
            {
                MessageBox.Show("No available customer out standing report.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }



        }





        private void CustomerTransactionReport()
        {
            int customerID = 0;
            int.TryParse(cmbCustomerName.Value.ToString(), out customerID);

            List<CustomerOutstanding> lstCustomerOutstanding = new List<CustomerOutstanding>();
            List<CustomerOutstanding> lstCustomerPreviousOutstanding = new List<CustomerOutstanding>();

            DateTime fDate = Convert.ToDateTime(dtpFromDate.Value.ToString("yyyy/MM/dd"));
            DateTime tDate = Convert.ToDateTime(dtpToDate.Value.ToString("yyyy/MM/dd"));

            string fromDate = dtpFromDate.Value.ToString("yyyy/MM/dd") + MTBFConstants.DAY_START_TIME;
            string toDate = dtpToDate.Value.ToString("yyyy/MM/dd") + MTBFConstants.DAY_END_TIME;

            //  lstCustomerOutstanding = new SalesManager().GetCustomerTransactionByCustomerID(customerID).OrderBy(c => c.SalesDate).ToList();
            lstCustomerOutstanding = new SalesManager().GetCustomerTransactionByCustomerID(customerID, fromDate, toDate).OrderBy(c => c.SalesDate).ToList();

            decimal totalDiscount = new SalesManager().GetTotalDicountByCustomerID(customerID);

            fromDate = "2014/01/01" + MTBFConstants.DAY_START_TIME;
            toDate = dtpFromDate.Value.AddDays(-1).ToString("yyyy/MM/dd") + MTBFConstants.DAY_END_TIME;

            lstCustomerPreviousOutstanding = new SalesManager().GetCustomerTransactionByCustomerID(customerID, fromDate, toDate).OrderBy(c => c.SalesDate).ToList(); ;

            decimal balance = 0;
            decimal previousDue = 0;
            foreach (CustomerOutstanding cDue in lstCustomerPreviousOutstanding)
            {
                previousDue = (cDue.Debit > 0) ? (previousDue + cDue.Debit) : (previousDue - cDue.Credit);
            }

            CustomerOutstanding pDue = new CustomerOutstanding();
            pDue.SalesDate = fDate.AddDays(-1);
            pDue.Debit = (previousDue > 0) ? previousDue : 0;
            pDue.Credit = (previousDue < 0) ? previousDue : 0;
            pDue.Description = "Balance BF";
            pDue.BillNumber = "N/A";



            //  lstCustomerOutstanding = lstCustomerOutstanding.Where(c => c.SalesDate >= fDate && c.SalesDate <= tDate).ToList();

            lstCustomerOutstanding.Insert(0, pDue);
            foreach (CustomerOutstanding customerOutStanding in lstCustomerOutstanding.OrderBy(c => c.SalesDate).ToList())
            {
                balance = (customerOutStanding.Debit > 0) ? (balance + customerOutStanding.Debit) : (balance - customerOutStanding.Credit);
                customerOutStanding.Balance = balance;
            }


            if (lstCustomerOutstanding.Count > 0)
            {
                rptCustomerHistory op = new rptCustomerHistory();
                op.DataSource = lstCustomerOutstanding;
                rptViewer.Document = op.Document;
                rptViewer.Dock = DockStyle.Fill;
                GetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
                op.txtPharmacyName.Text = pharmacyName;
                op.txtAddress.Text = pharmacyAddress;
                op.lblTotalDiscount.Text = totalDiscount.ToString();
                op.txtFromDate.Text = dtpFromDate.Value.ToString("dd-MMM-yyyy");
                op.txtToDate.Text = dtpToDate.Value.ToString("dd-MMM-yyyy");

                Customer customer = DataAccessProxy.GetCustomerByID(customerID);
                if (customer != null)
                {
                    op.txtCustomerName.Text = customer.CustomerName;
                    op.txtCustomerAddress.Text = customer.Address;
                    op.txtPhone.Text = customer.Phone;
                }

                op.Run();
                btnDownload.Enabled = true;
            }
            else
            {
                MessageBox.Show("No available customer out standing report.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }




        }

        private void PrintSalesReturnReport()
        {
            List<SalesReturnReport> lstSalesReturnReport = new List<SalesReturnReport>();

            _fromDate = dtpFromDate.Value.ToString("yyyy/MM/dd") + dayStartTime;
            _toDate = dtpToDate.Value.ToString("yyyy/MM/dd") + dayEndTime;

            rptSalesReturnHistory op = new rptSalesReturnHistory();
            lstSalesReturnReport = new SalesReturnManager().GetSalesReturnReportByDate(_fromDate, _toDate, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<SalesReturnReport>().ToList();



            if (lstSalesReturnReport.Count > 0)
            {
                op.DataSource = lstSalesReturnReport;
                rptViewer.Document = op.Document;
                rptViewer.Dock = DockStyle.Fill;
                GetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
                op.txtPharmacyName.Text = pharmacyName;
                op.txtAddress.Text = pharmacyAddress;
                // Employee employee = DataAccessProxy.GetEmployeeByID(IFMSConstant.LoggedinUserID);

                // op.txtOrderBy.Text = employee.EmployeeName;
                // op.txtOrderDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                op.Run();
                btnDownload.Enabled = true;
            }
            else
            {
                MessageBox.Show("No sales report for this date range.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }




        private void StockDetailInformation()
        {
            List<StockDetailInformation> lstAllStockDetail = new List<StockDetailInformation>();
            List<StockDetailInformation> lstStockDetail = new List<StockDetailInformation>();
            string fromDate = dtpFromDate.Value.ToString("yyyy-MM-dd") + MTBFConstants.DAY_START_TIME; ;
            string toDate = dtpToDate.Value.ToString("yyyy-MM-dd") + MTBFConstants.DAY_END_TIME;


            List<DistributeSample> lstDistributeSambple = DataAccessProxy.GetAllSampleDistributionByProductIDAndDate(MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID, fromDate, toDate);
            List<DamageDetail> lstDamageDetail = DataAccessProxy.GetAllDamageProductByProductCodeAndDate(MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID, fromDate, toDate);
            List<SalesOrderDetail> lstSalesOrderDetail = DataAccessProxy.GetAllSalesProductByProductIDandDate(MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID, fromDate, toDate);
            List<TransferDetail> lstTransferDetail = DataAccessProxy.GetAllTransterProductByProductIDandDAate(MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID, fromDate, toDate);
            List<ReceiveProductDetail> lstReceiveProductDetail = DataAccessProxy.GetAllReceiveProductByProductIDAndDate(MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID, fromDate, toDate);
            List<SalesReturnDetail> lstSalesReturnDetail = new SalesReturnManager().GetAllSalesReturnProductByProductIDandDAate(MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID, fromDate, toDate);
            List<PurchaseReturnDetail> lstPurchaseReturnDetail = new SalesReturnManager().GetAllPurchaseReturnProductByDaate(MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID, fromDate, toDate);



            lstAllStockDetail = DataAccessProxy.GetAllStockDetailInformationByDate(fromDate, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).OrderBy(p => p.ProductID).Cast<StockDetailInformation>().ToList();


            //lstAllStockDetail = lstAllStockDetail.Where(s => s.ProductID == "15651").ToList();

            string[] productID = lstAllStockDetail.Select(i => i.ProductID).Distinct().ToArray();
            string filter = string.Empty;


            if (productID.Length > 0)
            {
                for (int i = 0; i < productID.Length; i++)
                {
                    if (filter != string.Empty) filter = filter + ",";
                    filter = filter + "'" + productID[i] + "'";
                }

                filter = String.Format("{0} IN ({1})", "ProductID", filter);
            }
            List<Product> lstProduct = DataAccessProxy.GetFilteredProduct(filter).Cast<Product>().ToList();

            lstAllStockDetail = lstAllStockDetail.OrderBy(p => p.ProductName).Cast<StockDetailInformation>().ToList();

            //   lstAllStockDetail = lstAllStockDetail.Where(p => p.ProductID == "0001").ToList();

            foreach (StockDetailInformation stockDetail in lstAllStockDetail)
            {

                DistributeSample distributeSambple = lstDistributeSambple.Where(d => d.ProductID == stockDetail.ProductID).Cast<DistributeSample>().ToList().FirstOrDefault();
                DamageDetail damageDetail = lstDamageDetail.Where(d => d.ProductCode == stockDetail.ProductID).Cast<DamageDetail>().ToList().FirstOrDefault();
                SalesOrderDetail salesOrderDetail = lstSalesOrderDetail.Where(d => d.ProductID == stockDetail.ProductID).Cast<SalesOrderDetail>().ToList().FirstOrDefault();
                TransferDetail transferDetail = lstTransferDetail.Where(d => d.ProductCode == stockDetail.ProductID).Cast<TransferDetail>().ToList().FirstOrDefault();
                ReceiveProductDetail receiveDetail = lstReceiveProductDetail.Where(d => d.ProductCode == stockDetail.ProductID).Cast<ReceiveProductDetail>().ToList().FirstOrDefault();
                SalesReturnDetail salesReturnDetail = lstSalesReturnDetail.Where(d => d.ProductCode == stockDetail.ProductID).Cast<SalesReturnDetail>().ToList().FirstOrDefault();
                PurchaseReturnDetail prDetail = lstPurchaseReturnDetail.Where(p => p.ProductID == stockDetail.ProductID).FirstOrDefault();



                stockDetail.SampleQuantity = (distributeSambple != null) ? Convert.ToInt32(distributeSambple.GivenQuantity) : 0;
                stockDetail.DamageQuantity = (damageDetail != null) ? Convert.ToInt32(damageDetail.Quantity) : 0;
                stockDetail.SalesQuantity = (salesOrderDetail != null) ? Convert.ToInt32(salesOrderDetail.Quantity) : 0;
                stockDetail.TransferQuantity = (transferDetail != null) ? Convert.ToInt32(transferDetail.Quantity) : 0;
                stockDetail.SalesReturnQuantity = (salesReturnDetail != null) ? Convert.ToInt32(salesReturnDetail.Quantity) : 0;
                stockDetail.ReceiveQuantity = (receiveDetail != null) ? Convert.ToInt32(receiveDetail.Quantity) : 0;
                stockDetail.PReturnQuantity = (prDetail != null) ? Convert.ToInt32(prDetail.Quantity) : 0;
                stockDetail.ClosingStock = (stockDetail.PreviousQuantity + stockDetail.ReceiveQuantity + stockDetail.SalesReturnQuantity) - (stockDetail.SalesQuantity + stockDetail.TransferQuantity + stockDetail.DamageQuantity + stockDetail.SampleQuantity + stockDetail.PReturnQuantity);
                stockDetail.ProductName = lstProduct.Where(p => p.ProductID == stockDetail.ProductID).Cast<Product>().ToList().FirstOrDefault().ProductName;
                lstStockDetail.Add(stockDetail);
            }

            if (lstStockDetail.Count > 0)
            {
                rptStockDetail op = new rptStockDetail();
                op.DataSource = lstStockDetail;


                rptViewer.Document = op.Document;
                rptViewer.Dock = DockStyle.Fill;
                GetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
                op.txtPharmacyName.Text = pharmacyName;
                op.txtAddress.Text = pharmacyAddress;
                op.txtFromDate.Text = dtpFromDate.Value.ToString("dd MM yyyy");
                op.txtToDate.Text = dtpToDate.Value.ToString("dd MM yyyy");
                op.Run();
                btnDownload.Enabled = true;
            }


        }

        private int CalculateAllSampleDistributionByProductIDAndDate(string fromDate, string toDate)
        {
            return 0;
        }

        private void CashPaymentInformationAccordingToDate()
        {
            _fromDate = dtpFromDate.Value.ToString("yyyy/MM/dd") + dayStartTime;
            _toDate = dtpToDate.Value.ToString("yyyy/MM/dd") + dayEndTime;

            List<CashPaymentInformation> lstCashPaymentInformation = new List<CashPaymentInformation>();

            rptTotalCashPaymentByDate op = new rptTotalCashPaymentByDate();
            lstCashPaymentInformation = DataAccessProxy.GetTotalCashPaymentInformationByDate(_fromDate, _toDate, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<CashPaymentInformation>().ToList();
            if (lstCashPaymentInformation.Count > 0)
            {
                op.DataSource = lstCashPaymentInformation;
                rptViewer.Document = op.Document;
                rptViewer.Dock = DockStyle.Fill;
                GetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
                op.txtPharmacyName.Text = pharmacyName;
                op.txtAddress.Text = pharmacyAddress;
                op.Run();
                btnDownload.Enabled = true;
            }


            if (lstCashPaymentInformation.Count == 0)
            {
                MessageBox.Show("No information found for this combination.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CashReceiveInformationAccordingToDate()
        {
            _fromDate = dtpFromDate.Value.ToString("yyyy/MM/dd") + dayStartTime;
            _toDate = dtpToDate.Value.ToString("yyyy/MM/dd") + dayEndTime;

            List<CashReceiveInformation> lstAllCustomerOutstanding = new List<CashReceiveInformation>();

            rptTotalCashReceiveByDate op = new rptTotalCashReceiveByDate();
            lstAllCustomerOutstanding = DataAccessProxy.GetTotalCashReceiveInformationByDate(_fromDate, _toDate, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<CashReceiveInformation>().ToList();

            lstAllCustomerOutstanding = lstAllCustomerOutstanding.OrderBy(c => c.ReceiveDate).Where(r => r.ReceiveAmount > 0).ToList();

            if (lstAllCustomerOutstanding.Count > 0)
            {
                op.DataSource = lstAllCustomerOutstanding;
                rptViewer.Document = op.Document;
                rptViewer.Dock = DockStyle.Fill;
                GetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
                op.txtPharmacyName.Text = pharmacyName;
                op.txtAddress.Text = pharmacyAddress;
                op.txtFromDate.Text = dtpFromDate.Value.ToString("dd/MM/yyyy");
                op.txtToDate.Text = dtpToDate.Value.ToString("dd/MM/yyyy");
                op.Run();
                btnDownload.Enabled = true;
            }

            if (lstAllCustomerOutstanding.Count == 0)
            {
                MessageBox.Show("No information found for this combination.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void PrintAllSupplierOutstanding()
        {
            _fromDate = dtpFromDate.Value.ToString("yyyy/MM/dd");
            _toDate = dtpToDate.Value.AddDays(1).ToString("yyyy/MM/dd");

            List<AllCustomerOutstanding> lstAllCustomerOutstanding = new List<AllCustomerOutstanding>();

            List<AllCustomerOutstanding> lstNewAllCustomerOutstanding = new List<AllCustomerOutstanding>();

            rptAllSupplierOutstanding op = new rptAllSupplierOutstanding();
            lstAllCustomerOutstanding = new SupplierManager().GetAllSupplierOutstandingByDate(_fromDate, _toDate, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<AllCustomerOutstanding>().ToList();

            lstAllCustomerOutstanding = lstAllCustomerOutstanding.OrderBy(c => c.CustomerName).ToList();

            foreach (AllCustomerOutstanding customerOutStanding in lstAllCustomerOutstanding)
            {
                List<SalesReturn> lstSalesReturn = new List<SalesReturn>();
                lstSalesReturn = new SalesReturnManager().GetAllSalesReturnByCustomerID(customerOutStanding.CustomerID);
                decimal totoalReturnAmount = 0;
                totoalReturnAmount = CalculateTotalReturnAmount(lstSalesReturn);

                customerOutStanding.DueAmount = customerOutStanding.DueAmount - totoalReturnAmount;

                lstNewAllCustomerOutstanding.Add(customerOutStanding);

            }




            if (lstNewAllCustomerOutstanding.Count > 0)
            {
                op.DataSource = lstNewAllCustomerOutstanding;
                rptViewer.Document = op.Document;
                rptViewer.Dock = DockStyle.Fill;
                GetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
                op.txtPharmacyName.Text = pharmacyName;
                op.txtAddress.Text = pharmacyAddress;
                op.Run();
                btnDownload.Enabled = true;
            }
            else
            {
                MessageBox.Show("No information found for this combination.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void PrintAllCustomerOutstanding()
        {

            _fromDate = dtpFromDate.Value.ToString("yyyy/MM/dd") + " 00:00:00";
            _toDate = dtpToDate.Value.ToString("yyyy/MM/dd") + " 23:59:59";
            string startingDate = "2016/01/01";
            string _previousfromDate = dtpFromDate.Value.AddDays(-1).ToString("yyyy/MM/dd") + " 00:00:00";
            List<AllCustomerOutstanding> lstAllCustomerOutstanding = new List<AllCustomerOutstanding>();

            List<AllCustomerOutstanding> lstPreviousAllCustomerOutstanding = new List<AllCustomerOutstanding>();
            lstPreviousAllCustomerOutstanding = DataAccessProxy.GetAllCustomerOutstandingByDate(startingDate, _previousfromDate, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<AllCustomerOutstanding>().ToList();

            rptAllCustomerOutstanding op = new rptAllCustomerOutstanding();
            lstAllCustomerOutstanding = DataAccessProxy.GetAllCustomerOutstandingByDate(_fromDate, _toDate, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<AllCustomerOutstanding>().ToList();

            foreach (AllCustomerOutstanding outStanding in lstAllCustomerOutstanding)
            {
                AllCustomerOutstanding previousOutStanding = lstPreviousAllCustomerOutstanding.Where(c => c.CustomerID == outStanding.CustomerID).FirstOrDefault();
                if (previousOutStanding != null)
                {
                    outStanding.DueAmount = outStanding.DueAmount + previousOutStanding.DueAmount;
                }
            }


            if (lstAllCustomerOutstanding.Count > 0)
            {
                lstAllCustomerOutstanding = lstAllCustomerOutstanding.OrderBy(c => c.CustomerName).ToList();
                op.DataSource = lstAllCustomerOutstanding;
                rptViewer.Document = op.Document;
                rptViewer.Dock = DockStyle.Fill;
                GetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
                op.txtPharmacyName.Text = pharmacyName;
                op.txtAddress.Text = pharmacyAddress;
                //  op.txtFromDate.Text = dtpFromDate.Value.ToString("dd-MMM-yyyy");
                // op.txtToDate.Text = dtpToDate.Value.ToString("dd-MMM-yyyy");
                //op.txtCustomerType.Text = "All";
                op.Run();
            }
            else
            {
                MessageBox.Show("No information found for this combination.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //private void PrintAllCustomerOutstanding()
        //{
        //    _fromDate = dtpFromDate.Value.ToString("yyyy/MM/dd");
        //    _toDate = dtpToDate.Value.AddDays(1).ToString("yyyy/MM/dd");

        //    List<AllCustomerOutstanding> lstAllCustomerOutstanding = new List<AllCustomerOutstanding>();

        //    List<AllCustomerOutstanding> lstNewAllCustomerOutstanding = new List<AllCustomerOutstanding>();

        //    rptAllCustomerOutstanding op = new rptAllCustomerOutstanding();
        //    lstAllCustomerOutstanding = DataAccessProxy.GetAllCustomerOutstandingByDate(_fromDate, _toDate, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<AllCustomerOutstanding>().ToList();

        //    lstAllCustomerOutstanding = lstAllCustomerOutstanding.OrderBy(c => c.CustomerName).ToList();

        //    foreach (AllCustomerOutstanding customerOutStanding in lstAllCustomerOutstanding)
        //    {
        //        List<SalesReturn> lstSalesReturn = new List<SalesReturn>();
        //        lstSalesReturn = new SalesReturnManager().GetAllSalesReturnByCustomerID(customerOutStanding.CustomerID);
        //        decimal totoalReturnAmount = 0;
        //        totoalReturnAmount = CalculateTotalReturnAmount(lstSalesReturn);

        //        customerOutStanding.DueAmount = customerOutStanding.DueAmount - totoalReturnAmount;

        //        lstNewAllCustomerOutstanding.Add(customerOutStanding);

        //    }

        //    //List<CustomerPreviousDue> lstCustomerPreviousDue = new List<CustomerPreviousDue>();
        //    //int[] customerIDs = lstAllCustomerOutstanding.Select(c => c.CustomerID).Distinct().ToArray();
        //    //if (customerIDs.Length > 0)
        //    //{
        //    //    string filter = String.Format("{0} IN ({1})", "CustomerID", String.Join(",", customerIDs));               
        //    //    lstCustomerPreviousDue = new PreviousDueManager().GetFilteredCustomerDue(filter);
        //    //}
        //    //foreach (CustomerPreviousDue previousDue in lstCustomerPreviousDue)
        //    //{
        //    //    Customer customer = new CustomerManager().GetCustomerByID(previousDue.CustomerID);
        //    //    AllCustomerOutstanding customerOutStanding = new AllCustomerOutstanding();
        //    //    customerOutStanding.Address = customer.Address;
        //    //    customerOutStanding.Phone = customer.Phone;
        //    //    customerOutStanding.CustomerName = customer.CustomerName;
        //    //    customerOutStanding.DueAmount = previousDue.Amount;
        //    //    lstNewAllCustomerOutstanding.Add(customerOutStanding);
        //    //}



        //    if (lstNewAllCustomerOutstanding.Count > 0)
        //    {
        //        op.DataSource = lstNewAllCustomerOutstanding;
        //        rptViewer.Document = op.Document;
        //        rptViewer.Dock = DockStyle.Fill;
        //        GetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
        //        op.txtPharmacyName.Text = pharmacyName;
        //        op.txtAddress.Text = pharmacyAddress;
        //        op.Run();
        //        btnDownload.Enabled = true;
        //    }
        //    else
        //    {
        //        MessageBox.Show("No information found for this combination.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        private void DeleteEmployee(Employee employee)
        {
            DataAccessProxy.DeleteEmployee(employee);
        }

        private void HideAllControlls()
        {
            dtpFromDate.Visible = false;
            dtpToDate.Visible = false;
            Label2.Visible = false;
            Label3.Visible = false;
            cmbCustomerName.Visible = false;
            cmbSellerCombo.Visible = false;
            lblCaption.Visible = false;
        }

        /// <summary>
        /// Method to load report name in combo box.
        /// </summary>
        private void LoadReportName()
        {
            Dictionary<string, int> lstReportName = new Dictionary<string, int>();

            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("Name");

            foreach (IFMSEnum.ReportName enumValue in Enum.GetValues(typeof(IFMSEnum.ReportName)))
            {
                DataRow row = table.NewRow();
                row[0] = (int)enumValue;
                row[1] = enumValue.GetDescription();
                table.Rows.Add(row);
                //   lstReportName.Add(enumValue.GetDescription(), (int)enumValue);
            }

            cmbReportName.DisplayMember = "Name";
            cmbReportName.ValueMember = "ID";
            cmbReportName.DataSource = table;
            cmbReportName.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
            cmbReportName.DisplayLayout.PerformAutoResizeColumns(false, PerformAutoSizeType.AllRowsInBand, true);
        }

        /// <summary>
        /// Method to load cusomer information in combo box.
        /// </summary>
        private DataTable BuildCustomerTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("CustomerID");
            table.Columns.Add("Customer Name");
            table.Columns.Add("Address");
            table.Columns.Add("Phone");
            return table;
        }

        private void LoadCustomerCombo()
        {
            List<Customer> lstCustomer = new List<Customer>();
            lstCustomer = DataAccessProxy.GetAllCustomer().Cast<Customer>().Where(c => c.BranchID == MTBFConstants.AppConstants.BranchID && c.OrganizationID == MTBFConstants.AppConstants.OrganizationID).ToList();
            DataTable table = BuildCustomerTable();
            List<Zone> lstzone = new List<IMFS.Common.DTO.Zone>();
            lstzone = new CustomerManager().GetAllZone();
            foreach (Customer customer in lstCustomer)
            {
                Zone zone = lstzone.Where(z => z.ZoneID == customer.ZoneID).FirstOrDefault();
                DataRow row = table.NewRow();
                row["CustomerID"] = customer.CustomerID;
                row["Customer Name"] = customer.CustomerName;
                row["Address"] = customer.Address;
                row["Phone"] = customer.Phone;
                table.Rows.Add(row);

            }

            cmbCustomerName.DisplayMember = "Customer Name";
            cmbCustomerName.ValueMember = "CustomerID";
            cmbCustomerName.DataSource = table;

            cmbCustomerName.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
            cmbCustomerName.DisplayLayout.PerformAutoResizeColumns(false, PerformAutoSizeType.AllRowsInBand, true);
            cmbCustomerName.DisplayLayout.Bands[0].Columns["CustomerID"].Hidden = true;
        }

        private DataTable BuildSalesRepresentativeTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("SalesRepresentativeID");
            table.Columns.Add("Name");
            table.Columns.Add("Address");
            table.Columns.Add("Phone");
            return table;
        }

        private void LoadSalesRepresentativeCombo()
        {

            string filter = string.Format("{0}={1}", "BranchID", MTBFConstants.AppConstants.BranchID);

            lstSalesRepresentative = new SalesRepresentativeManager().GetFilteredSalesRepresentative(filter).Cast<SalesRepresentative>().ToList();
            DataTable table = BuildSalesRepresentativeTable();

            DataRow emptyrow = table.NewRow();
            emptyrow["SalesRepresentativeID"] = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            emptyrow["Name"] = MTBFConstants.DataField.COMBO_DEFAULT_NAME;
            emptyrow["Address"] = "";
            emptyrow["Phone"] = "";
            table.Rows.Add(emptyrow);
            foreach (SalesRepresentative customer in lstSalesRepresentative)
            {
                DataRow row = table.NewRow();
                row["SalesRepresentativeID"] = customer.SalesRepresentativeID;
                row["Name"] = customer.Name;
                row["Address"] = customer.Address;
                row["Phone"] = customer.Phone;
                table.Rows.Add(row);

            }

            cmbCustomerName.DisplayMember = "Name";
            cmbCustomerName.ValueMember = "SalesRepresentativeID";
            cmbCustomerName.DataSource = table;
            cmbCustomerName.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;

            cmbCustomerName.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
            cmbCustomerName.DisplayLayout.PerformAutoResizeColumns(false, PerformAutoSizeType.AllRowsInBand, true);
            cmbCustomerName.DisplayLayout.Bands[0].Columns["SalesRepresentativeID"].Hidden = true;
        }


        private void LoadZoneInformation()
        {
            lstZone = new CustomerManager().GetAllZone();
            Zone emptyZone = new Zone();
            emptyZone.ZoneID = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            emptyZone.ZoneName = MTBFConstants.DataField.COMBO_DEFULT_TASK_TYPE;
            lstZone.Insert(0, emptyZone);

            UIHelper.SetComboBoxData(cmbCustomerName, lstZone, "ZoneName", "ZoneID");
            cmbCustomerName.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
        }



        /// <summary>
        /// Method to load supplier information in combo box.
        /// </summary>
        private void LoadSupplierInformation()
        {
            cmbCustomerName.DataSource = null;
            List<Supplier> lstSupplier = new List<Supplier>();
            DataTable supplierTable = new DataTable();
            supplierTable.Columns.Add("SupplierID");
            supplierTable.Columns.Add("Supplier Name");
            //supplierTable.Columns.Add("Company Name");

            lstSupplier = DataAccessProxy.GetAllSupplier().Cast<Supplier>().Where(s => s.BranchID == MTBFConstants.AppConstants.BranchID && s.OrganizationID == MTBFConstants.AppConstants.OrganizationID).ToList();
            foreach (Supplier supplier in lstSupplier)
            {
                //Company company = DataAccessProxy.GetCompanyByID(supplier.CompanyID);
                DataRow row = supplierTable.NewRow();

                row[0] = supplier.SupplierID;
                row[1] = supplier.SupplierName;
                //row[2] = company.CompanyName;
                supplierTable.Rows.Add(row);
            }


            cmbCustomerName.DisplayMember = "Supplier Name";
            cmbCustomerName.ValueMember = "SupplierID";
            cmbCustomerName.DataSource = supplierTable;
        }

        /// <summary>
        /// Method to print profit report.
        /// </summary>
        private void PrintProfitReport()
        {
            _fromDate = dtpFromDate.Value.ToString("yyyy/MM/dd") + MTBFConstants.DAY_START_TIME;
            _toDate = dtpToDate.Value.ToString("yyyy/MM/dd") + MTBFConstants.DAY_END_TIME;

            List<ProfitCalculationAccordingToDate> lstProfit = new List<ProfitCalculationAccordingToDate>();
            string sql = @"Select cs.SalesDate ,SUM(cs.SalesAmount) as TotalSales, SUM (cs.Discount) as Discount from CashSales cs 
                        where cs.SalesDate between '" + _fromDate + "' and '" + _toDate + "' and BranchID ='" + MTBFConstants.AppConstants.BranchID + "' and OrganizationID ='" + MTBFConstants.AppConstants.OrganizationID + @"'
                        group by cs.SalesDate ";

            rptDailyProfit op = new rptDailyProfit();
            lstProfit = new SalesManager().GetFilteredProfitInformationAccordingToDate(sql).Cast<ProfitCalculationAccordingToDate>().OrderBy(p => p.SalesDate).ToList();

            List<Expense> lstExpense = DataAccessProxy.GetTotalExpenseAmountByDate(_fromDate, _toDate, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<Expense>().OrderBy(e => e.ExpenseDate).ToList();


            List<DailyProfit> lstDailyProfit = new List<DailyProfit>();

            DataTable profitTable = BuildProfitTable();

            foreach (ProfitCalculationAccordingToDate profit in lstProfit)
            {
                decimal purchasePrice;
                //                string salesFilter = @"select  SUM (tbl.PurchasePrice *tbl2.Quantity ) as PurchasePrice from (
                //                                        Select distinct pod.ProductID,  Max(pod.PurchasePrice) as PurchasePrice  from dbo.PurchaseOrderDetails pod where pod.ProductID in (
                //                                        Select csd.ProductID  from CashSalesOrderDetails csd join CashSales cs on csd.SalesID =cs.SalesID 
                //                                        where cs.SalesDate ='" + profit.SalesDate.ToString("yyyy/MM/dd") + @"') group by pod.ProductID )tbl
                //                                        join (
                //                                        Select csd.ProductID, csd.Quantity   from CashSalesOrderDetails csd join CashSales cs on csd.SalesID =cs.SalesID 
                //                                        where cs.SalesDate ='" + profit.SalesDate.ToString("yyyy/MM/dd") + "')tbl2 on  tbl.ProductID =tbl2.ProductID ";

                string salesFilter = @"Select sum(pod.Total)as PurchasePrice  from dbo.ProductUsedDetail pod 
                                        left outer join ProductUsed pu on pod.ProductUsedID=pu.ProductUsedID
                                        where pu.UsedDate ='" + profit.SalesDate.ToString("yyyy/MM/dd") + "'";



                purchasePrice = new SalesManager().GetConsumedProductAmount(salesFilter);

                ProfitCalculationAccordingToDate pPurchasePortion = new ProfitCalculationAccordingToDate();


                DailyProfit dailyProfit = new DailyProfit();
                dailyProfit.SalesDate = profit.SalesDate;
                dailyProfit.TotalSales = profit.TotalSales;
                dailyProfit.Discount = profit.Discount;
                dailyProfit.TotalPurchase = purchasePrice;
                dailyProfit.TotalExpense = CalculateExpnse(profit.SalesDate.ToString("yyy/MM/dd"), lstExpense);
                dailyProfit.Profit = (profit.TotalSales - (profit.Discount + purchasePrice)) - dailyProfit.TotalExpense;
                lstDailyProfit.Add(dailyProfit);
            }


            foreach (Expense expense in lstExpense)
            {
                if (!IsExistDate(expense.ExpenseDate.ToString("yyyy/MM/dd"), lstDailyProfit))
                {
                    DailyProfit dailyProfit = new DailyProfit();
                    dailyProfit.SalesDate = expense.ExpenseDate;

                    dailyProfit.TotalExpense = CalculateExpnse(expense.ExpenseDate.ToString("yyyy/MM/dd"), lstExpense);
                    dailyProfit.Profit = dailyProfit.Profit - dailyProfit.TotalExpense;
                    lstDailyProfit.Add(dailyProfit);
                }
            }

            lstDailyProfit = lstDailyProfit.OrderBy(p => p.SalesDate).Cast<DailyProfit>().ToList();

            if (lstDailyProfit.Count > 0)
            {
                op.DataSource = lstDailyProfit;
                rptViewer.Document = op.Document;
                rptViewer.Dock = DockStyle.Fill;
                GetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
                op.txtPharmacyName.Text = pharmacyName;
                op.txtAddress.Text = pharmacyAddress;
                op.txtFromDate.Text = Convert.ToDateTime(_fromDate).ToString("dd/MM/yyyy");
                op.txtToDate.Text = Convert.ToDateTime(_toDate).ToString("dd/MM/yyyy");
                // Employee employee = DataAccessProxy.GetEmployeeByID(IFMSConstant.LoggedinUserID);

                // op.txtOrderBy.Text = employee.EmployeeName;
                // op.txtOrderDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                op.Run();
                btnDownload.Enabled = true;
            }
            else
            {
                MessageBox.Show("No information found for this report.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool IsExistDate(string date, List<DailyProfit> lstDailyProfit)
        {
            foreach (DailyProfit dProfit in lstDailyProfit)
            {
                if (date == dProfit.SalesDate.ToString("yyyy/MM/dd"))
                {
                    return true;
                }
            }
            return false;
        }

        private decimal CalculateExpnse(string date, List<Expense> lstExpense)
        {
            decimal totalExpense = 0;
            foreach (Expense expense in lstExpense)
            {
                if (date == expense.ExpenseDate.ToString("yyyy/MM/dd"))
                {
                    totalExpense = totalExpense + expense.Amount;
                }
            }
            return totalExpense;
        }

        private class DailyProfit
        {
            public DateTime SalesDate { get; set; }
            public decimal TotalSales { get; set; }
            public decimal Discount { get; set; }
            public decimal TotalPurchase { get; set; }
            public decimal TotalExpense { get; set; }
            public decimal Profit { get; set; }
        }


        private DataTable BuildProfitTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("SalesDate");
            table.Columns.Add("TotalSales");
            table.Columns.Add("Discount");
            table.Columns.Add("TotalPurchase");
            table.Columns.Add("TotalExpense");
            table.Columns.Add("Profit");
            return table;
        }

        /// <summary>
        /// Method to print inventory report.
        /// </summary>
        private void PrintInventoryReport()
        {
            List<StockPrice> lstStockPrice = new List<StockPrice>();
            List<Inventroy> lstInventory = new List<Inventroy>();
            List<PurchaseOrderDetail> lstPurcahseOrder = new List<PurchaseOrderDetail>();
            List<ProductModel> lstProductModel = new List<ProductModel>();


            lstInventory = DataAccessProxy.GetAllInventroyInformation(MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<Inventroy>().ToList();
            if (lstInventory.Count > 0)
            {
                string[] productID = lstInventory.Select(i => i.ProductID).Distinct().ToArray();
                string filter = string.Empty;


                if (productID.Length > 0)
                {
                    for (int i = 0; i < productID.Length; i++)
                    {
                        if (filter != string.Empty) filter = filter + ",";
                        filter = filter + "'" + productID[i] + "'";
                    }

                    filter = String.Format("{0} IN ({1})", "ProductID", filter);
                    lstPurcahseOrder = DataAccessProxy.GetPurcahseOrderDetailFiltered(filter).Cast<PurchaseOrderDetail>().ToList();
                    lstStockPrice = new StockPriceManager().GetFilteredStockPrice(filter);
                }

                List<Product> lstProduct = DataAccessProxy.GetFilteredProduct(filter).Cast<Product>().ToList();

                int[] productModelID = lstProduct.Select(p => p.ProductModelID).Distinct().ToArray();
                if (productModelID.Length > 0)
                {
                    string modelfilter = String.Format("{0} IN ({1})", "ProductModelID", String.Join(",", productModelID));
                    lstProductModel = DataAccessProxy.GetFilteedProductModel(modelfilter);

                }

                List<Inventroy> lstNewProduct = new List<Inventroy>();
                foreach (Inventroy inventory in lstInventory)
                {
                    StockPrice stockPrice = null;// lstStockPrice.Where(s => s.ProductID == inventory.ProductID).FirstOrDefault();
                    PurchaseOrderDetail orderDetail = lstPurcahseOrder.Where(p => p.ProductID == inventory.ProductID).OrderBy(p=> p.SerialNo).Cast<PurchaseOrderDetail>().LastOrDefault();

                    decimal purchasePrice = (orderDetail != null) ? orderDetail.PurchasePrice : 0;
                    ProductModel pModel = lstProductModel.Where(m => m.ProductModelID == inventory.ProductModelID).FirstOrDefault();

                    Product product = lstProduct.Where(p => p.ProductID == inventory.ProductID).Cast<Product>().ToList().FirstOrDefault();
                    inventory.PurchasePrice = (stockPrice != null) ? stockPrice.Price : purchasePrice;
                    inventory.Total = (inventory.Quantity > 0) ? inventory.PurchasePrice * Convert.ToDecimal(inventory.Quantity) : 0;
                    inventory.CompanyName = (product != null) ? product.CompanyName : string.Empty;
                    //  inventory.Carton = inventory.Quantity / inventory.CartonSize;
                    inventory.ModelName = (pModel != null) ? pModel.Name : string.Empty;
                    lstNewProduct.Add(inventory);
                }

                lstNewProduct = lstNewProduct.OrderBy(p => p.ProductName).ToList();


                if (lstNewProduct.Count > 0)
                {
                    //if (MTBFConstants.AppConstants.BranchID == 2)
                    //{
                    //    rptInventory op = new rptInventory();
                    //    op.DataSource = lstNewProduct;
                    //    rptViewer.Document = op.Document;
                    //    rptViewer.Dock = DockStyle.Fill;
                    //    GetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
                    //    op.txtPharmacyName.Text = pharmacyName;
                    //    op.txtAddress.Text = pharmacyAddress;
                    //    // Employee employee = DataAccessProxy.GetEmployeeByID(IFMSConstant.LoggedinUserID);

                    //    // op.txtOrderBy.Text = employee.EmployeeName;
                    //    // op.txtOrderDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    //    op.Run();
                    //    btnDownload.Enabled = true;
                    //}
                    //else
                    //{
                    rptInventoryForShop op = new rptInventoryForShop();
                    op.DataSource = lstNewProduct;
                    rptViewer.Document = op.Document;
                    rptViewer.Dock = DockStyle.Fill;
                    GetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
                    op.txtPharmacyName.Text = pharmacyName;
                    op.txtAddress.Text = pharmacyAddress;
                    // Employee employee = DataAccessProxy.GetEmployeeByID(IFMSConstant.LoggedinUserID);

                    // op.txtOrderBy.Text = employee.EmployeeName;
                    // op.txtOrderDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    op.Run();
                    btnDownload.Enabled = true;
                    //  }

                }
            }
            else
            {
                MessageBox.Show("No information found for this report.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void PrintProductTypeWiseInventoryReport()
        {
            List<Inventroy> lstInventory = new List<Inventroy>();
            List<PurchaseOrderDetail> lstPurcahseOrder = new List<PurchaseOrderDetail>();

            rptTypeWiseInventory op = new rptTypeWiseInventory();
            lstInventory = DataAccessProxy.GetAllInventroyInformation(MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<Inventroy>().ToList();
            if (lstInventory.Count > 0)
            {
                string[] productID = lstInventory.Select(i => i.ProductID).Distinct().ToArray();
                string filter = string.Empty;


                if (productID.Length > 0)
                {
                    for (int i = 0; i < productID.Length; i++)
                    {
                        if (filter != string.Empty) filter = filter + ",";
                        filter = filter + "'" + productID[i] + "'";
                    }

                    filter = String.Format("{0} IN ({1})", "ProductID", filter);
                    lstPurcahseOrder = DataAccessProxy.GetPurcahseOrderDetailFiltered(filter).Cast<PurchaseOrderDetail>().ToList();
                }

                List<Product> lstProduct = DataAccessProxy.GetFilteredProduct(filter).Cast<Product>().ToList();
                List<ProductType> lstProductType = new ProductManager().GetAllProductType().Cast<ProductType>().ToList();
                List<Inventroy> lstNewProduct = new List<Inventroy>();



                foreach (Inventroy inventory in lstInventory)
                {
                    Product product = lstProduct.Where(p => p.ProductID == inventory.ProductID).Cast<Product>().ToList().FirstOrDefault();
                    ProductType productType = (product != null) ? lstProductType.Where(t => t.ProductTypeID == product.ProductTypeID).FirstOrDefault() : null;
                    inventory.PurchasePrice = lstPurcahseOrder.Where(p => p.ProductID == inventory.ProductID).Cast<PurchaseOrderDetail>().FirstOrDefault().PurchasePrice;
                    inventory.Total = inventory.PurchasePrice * Convert.ToDecimal(inventory.Quantity);
                    inventory.ProductType = (productType != null) ? productType.TypeName : string.Empty;
                    lstNewProduct.Add(inventory);
                }

                lstNewProduct = lstNewProduct.OrderBy(p => p.ProductType).ToList();


                if (lstNewProduct.Count > 0)
                {
                    op.DataSource = lstNewProduct;
                    rptViewer.Document = op.Document;
                    rptViewer.Dock = DockStyle.Fill;
                    GetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
                    op.txtPharmacyName.Text = pharmacyName;
                    op.txtAddress.Text = pharmacyAddress;
                    // Employee employee = DataAccessProxy.GetEmployeeByID(IFMSConstant.LoggedinUserID);

                    // op.txtOrderBy.Text = employee.EmployeeName;
                    // op.txtOrderDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    op.Run();
                    btnDownload.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("No information found for this report.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private List<Inventroy> RemoveTransferProduct(List<Inventroy> lstProductInfo)
        {
            List<Inventroy> lstFilteredProductInfo = new List<Inventroy>();

            foreach (Inventroy productifo in lstProductInfo)
            {
                Product product = DataAccessProxy.GetProductByName(productifo.ProductName).Where(p => p.BranchID == MTBFConstants.AppConstants.BranchID && p.OrganizationID == MTBFConstants.AppConstants.OrganizationID).Cast<Product>().FirstOrDefault();
                int transferProduct = Convert.ToInt32(DataAccessProxy.GetAllTransferProductByProudctCode(product.ProductID, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID));
                productifo.Quantity = (productifo.Quantity - transferProduct);
                lstFilteredProductInfo.Add(productifo);
            }

            return lstFilteredProductInfo;
        }

        /// <summary>
        /// Method to remove all damage product 
        /// </summary>
        /// <param name="lstProductInfo"></param>
        /// <returns></returns>
        private List<Inventroy> RemoveDamageProduct(List<Inventroy> lstProductInfo)
        {
            List<Inventroy> lstFilteredProductInfo = new List<Inventroy>();

            foreach (Inventroy productifo in lstProductInfo)
            {
                Product product = DataAccessProxy.GetProductByName(productifo.ProductName).Where(p => p.BranchID == MTBFConstants.AppConstants.BranchID && p.OrganizationID == MTBFConstants.AppConstants.OrganizationID).Cast<Product>().FirstOrDefault();
                int transferProduct = Convert.ToInt32(DataAccessProxy.GetAllDamageProductByProudctCode(product.ProductID, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID));
                productifo.Quantity = (productifo.Quantity - transferProduct);
                lstFilteredProductInfo.Add(productifo);
            }

            return lstFilteredProductInfo;
        }

        /// <summary>
        /// Method to deduct all given sample to stock.
        /// </summary>
        /// <param name="lstProductInfo"></param>
        /// <returns></returns>
        private List<Inventroy> RemoveAllGivenSample(List<Inventroy> lstProductInfo)
        {

            List<Inventroy> lstFilteredProductInfo = new List<Inventroy>();

            foreach (Inventroy productifo in lstProductInfo)
            {
                Product product = DataAccessProxy.GetProductByName(productifo.ProductName).Where(p => p.BranchID == MTBFConstants.AppConstants.BranchID && p.OrganizationID == MTBFConstants.AppConstants.OrganizationID).Cast<Product>().FirstOrDefault();
                int givenSample = Convert.ToInt32(DataAccessProxy.GetAllGivenSampleByProductID(product.ProductID, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID));
                productifo.Quantity = (productifo.Quantity - givenSample);
                lstFilteredProductInfo.Add(productifo);

            }
            return lstFilteredProductInfo;

        }

        /// <summary>
        /// Method to print expire product report.
        /// </summary>
        private void PrintExpireProductReport()
        {
            List<ExpireProduct> lstExpireProduct = new List<ExpireProduct>();

            rptExpireProduct op = new rptExpireProduct();
            lstExpireProduct = DataAccessProxy.GetAllExpireProduct().Cast<ExpireProduct>().ToList();

            if (lstExpireProduct.Count > 0)
            {
                op.DataSource = lstExpireProduct;
                rptViewer.Document = op.Document;
                rptViewer.Dock = DockStyle.Fill;
                GetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
                op.txtPharmacyName.Text = pharmacyName;
                op.txtAddress.Text = pharmacyAddress;
                // Employee employee = DataAccessProxy.GetEmployeeByID(IFMSConstant.LoggedinUserID);

                // op.txtOrderBy.Text = employee.EmployeeName;
                // op.txtOrderDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                op.Run();

            }
            else
            {
                MessageBox.Show("No information found for this report.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void PrintCashReceiveInformation()
        {
            _fromDate = DateTime.Now.ToString("yyyy-MM-dd");
            _toDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");

            DataTable receiveTable = BuildCashReceiveTable();
            List<CashReceive> lstCashReceive = new List<CashReceive>();

            rptDailyCasReceive op = new rptDailyCasReceive();
            lstCashReceive = DataAccessProxy.GetAllCashReceiveInformationByDate(_fromDate, _toDate).Cast<CashReceive>().ToList();
            foreach (CashReceive cashReceive in lstCashReceive.Where(c => c.BranchID == MTBFConstants.AppConstants.BranchID && c.OrganizationID == MTBFConstants.AppConstants.OrganizationID))
            {
                decimal dueAmount = DataAccessProxy.GetCustomerDueAmountByCustomerID(cashReceive.CustomerID, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID);
                DataRow row = receiveTable.NewRow();
                Customer customer = DataAccessProxy.GetCustomerByID(cashReceive.CustomerID);
                row[0] = customer.CustomerName;
                row[1] = cashReceive.ReceiveDate.ToShortDateString();
                row[2] = cashReceive.Amount;
                row[3] = dueAmount;
                row[4] = cashReceive.ReferenceNo;
                receiveTable.Rows.Add(row);
            }

            if (lstCashReceive.Count > 0)
            {
                op.DataSource = receiveTable;
                rptViewer.Document = op.Document;
                rptViewer.Dock = DockStyle.Fill;
                GetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
                op.txtPharmacyName.Text = pharmacyName;
                op.txtAddress.Text = pharmacyAddress;
                // Employee employee = DataAccessProxy.GetEmployeeByID(IFMSConstant.LoggedinUserID);

                // op.txtOrderBy.Text = employee.EmployeeName;
                // op.txtOrderDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                op.Run();

            }
            else
            {
                MessageBox.Show("No available information found.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private DataTable BuildCashReceiveTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Customer Name");
            table.Columns.Add("Receive Date");
            table.Columns.Add("Receive Amount");
            table.Columns.Add("Due Amount");
            table.Columns.Add("ReferenceNo");
            return table;

        }

        /// <summary>
        /// Method to print purchase report.
        /// </summary>
        private void PrintPurchaseReportAccourdingToDate()
        {
            _fromDate = dtpFromDate.Value.ToString("yyyy/MM/dd") + dayStartTime;
            _toDate = dtpToDate.Value.ToString("yyyy/MM/dd") + dayEndTime;

            List<SalesAndPurchaseReport> lstPurchaseReport = new List<SalesAndPurchaseReport>();
            lstPurchaseReport = DataAccessProxy.GetAllPurchaseInformationAccordingToDate(_fromDate, _toDate, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<SalesAndPurchaseReport>().ToList();
            if (lstPurchaseReport.Count > 0)
            {
                rptPurchaseDetail op = new rptPurchaseDetail();
                op.DataSource = lstPurchaseReport;
                rptViewer.Document = op.Document;
                rptViewer.Dock = DockStyle.Fill;
                GetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
                op.txtPharmacyName.Text = pharmacyName;
                op.txtAddress.Text = pharmacyAddress;
                // Employee employee = DataAccessProxy.GetEmployeeByID(IFMSConstant.LoggedinUserID);

                // op.txtOrderBy.Text = employee.EmployeeName;
                // op.txtOrderDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                op.Run();
                btnDownload.Enabled = true;
            }
            else
            {
                MessageBox.Show("No purchase report for this date range.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Method to print sales report according to date.
        /// </summary>
        private void PrintSalesReportAccourdingToDate()
        {
            _fromDate = dtpFromDate.Value.ToString("yyyy/MM/dd") + dayStartTime;
            _toDate = dtpToDate.Value.ToString("yyyy/MM/dd") + dayEndTime;

            List<SalesAndPurchaseReport> lstSalesReport = new List<SalesAndPurchaseReport>();

            rptSalesDetail op = new rptSalesDetail();
            lstSalesReport = DataAccessProxy.GetAllSalesInformationAccordingToDate(_fromDate, _toDate, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<SalesAndPurchaseReport>().ToList();

            if (lstSalesReport.Count > 0)
            {
                op.DataSource = lstSalesReport;
                rptViewer.Document = op.Document;
                rptViewer.Dock = DockStyle.Fill;
                GetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
                op.txtPharmacyName.Text = pharmacyName;
                op.txtAddress.Text = pharmacyAddress;
                op.Run();
            }
            else
            {
                MessageBox.Show("No sales report for this date range.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SalesRepresentativeSalesReport(int salesRepresentativeID)
        {

            List<SalesOrder> lstReport = new List<SalesOrder>();

            List<SalesAndPurchaseReport> lstSalesReport = new List<SalesAndPurchaseReport>();
            List<Zone> lstZone = new List<Zone>();

            lstZone = new CustomerManager().GetAllZone();
            _fromDate = dtpFromDate.Value.ToString("yyyy/MM/dd") + dayStartTime;
            _toDate = dtpToDate.Value.ToString("yyyy/MM/dd") + dayEndTime;

            string filter = string.Format("{0} between '{1}' and '{2}'", "SalesDate", _fromDate, _toDate);
            rptSalesRepresentativeSales op = new rptSalesRepresentativeSales();
            lstSalesReport = new SalesManager().GetFilteredSalesRepresentativeSalesInformation(_fromDate, _toDate, MTBFConstants.AppConstants.BranchID).ToList();

            lstSalesReport = lstSalesReport.Where(s => s.SalesRepresentativeID > 0).ToList();

            //int[] customerIDs = lstSalesReport.Select(s => s.CustomerID).Distinct().ToArray();

            //List<CashReceive> lstCashReceive = new List<CashReceive>();

            //if (customerIDs.Length > 0)
            //{
            //    string modelfilter = String.Format("{0} IN ({1}) and {2} between '{3}' and '{4}'", "CustomerID", String.Join(",", customerIDs), "ReceiveDate", _fromDate, _toDate);
            //    lstCashReceive = new CashReceiveManager().GetFilteredCashReceive(modelfilter);
            //}


            if (salesRepresentativeID > 0)
            {
                lstSalesReport = lstSalesReport.Where(s => s.SalesRepresentativeID == salesRepresentativeID).ToList();
            }
            lstSalesReport = lstSalesReport.OrderBy(s => s.SalesRepresentativeID).ToList();
            foreach (SalesAndPurchaseReport salesReport in lstSalesReport)
            {
                SalesRepresentative salesRepresentative = lstSalesRepresentative.Where(s => s.SalesRepresentativeID == salesReport.SalesRepresentativeID).FirstOrDefault();

                //decimal totalDiscount = 0;
                //decimal receiveTotal = 0;
                //receiveTotal = CalculateTotalReceive(lstCashReceive, salesReport.CustomerID, out totalDiscount);
                // SalesAndPurchaseReport salesReport = new SalesAndPurchaseReport();
                //salesReport.ReceiveAmount += receiveTotal;
                //salesReport.Discount += totalDiscount;
                //salesReport.BillNumber = salesOrder.BillNumber;
                //salesReport.CustomerName = salesOrder.CustomerName;
                salesReport.RepresentativeName = (salesRepresentative != null) ? salesRepresentative.Name : string.Empty;
                //salesReport.SalesDate = salesOrder.SalesDate;
                //salesReport.SalesAmount = salesOrder.SalesAmount;
                //salesReport.Discount = salesOrder.Discount;
                //salesReport.Total = salesOrder.SalesAmount - salesOrder.Discount;
                //lstSalesReport.Add(salesReport);
            }

            op.DataSource = lstSalesReport;
            rptViewer.Document = op.Document;
            rptViewer.Dock = DockStyle.Fill;
            GetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
            op.txtPharmacyName.Text = pharmacyName;
            op.txtAddress.Text = pharmacyAddress;
            op.txtFromDate.Text = dtpFromDate.Value.ToString("dd-MMM-yyyy");
            op.txtToDate.Text = dtpToDate.Value.ToString("dd-MMM-yyyy");
            op.Run();


        }


        private decimal CalculateTotalReceive(List<CashReceive> lstCashReceive, int customerID, out decimal discount)
        {
            decimal total = 0;
            discount = 0;
            foreach (CashReceive cashReceive in lstCashReceive)
            {
                if (customerID == cashReceive.CustomerID)
                {
                    total += cashReceive.Amount;
                    discount += cashReceive.Discount;
                }
            }

            return total;
        }


        private void PrintSalesmanWiseSalesReport()
        {
            List<SalesAndPurchaseReport> lstSalesReport = new List<SalesAndPurchaseReport>();

            List<SalesmanWiseSalesReport> lstReport = new List<SalesmanWiseSalesReport>();

            _fromDate = dtpFromDate.Value.ToString("yyyy/MM/dd") + dayStartTime;
            _toDate = dtpToDate.Value.ToString("yyyy/MM/dd") + dayEndTime;

            rptSalesmanWiseSalesDetail op = new rptSalesmanWiseSalesDetail();
            lstSalesReport = DataAccessProxy.GetAllSalesInformationAccordingToDate(_fromDate, _toDate, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<SalesAndPurchaseReport>().ToList();

            foreach (SalesAndPurchaseReport salesReport in lstSalesReport)
            {
                SalesmanWiseSalesReport sReport = new SalesmanWiseSalesReport();
                Employee employee = new EmployeeManager().GetEmployeeByID(salesReport.EmployeeID);
                sReport.EmployeeName = (employee != null) ? employee.EmployeeName : "Super Admin";
                sReport.SalesDate = salesReport.SalesDate.ToString("dd/MM/yyyy");
                sReport.SalesId = salesReport.SalesID;
                sReport.ProductName = salesReport.ProductName;
                sReport.Quantity = salesReport.Quantity;
                sReport.Price = salesReport.Price;
                sReport.Total = salesReport.Total;
                lstReport.Add(sReport);
            }

            lstReport = lstReport.OrderBy(s => s.EmployeeName).ToList();

            if (lstSalesReport.Count > 0)
            {
                op.DataSource = lstReport;
                rptViewer.Document = op.Document;
                rptViewer.Dock = DockStyle.Fill;
                GetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
                op.txtPharmacyName.Text = pharmacyName;
                op.txtAddress.Text = pharmacyAddress;
                // Employee employee = DataAccessProxy.GetEmployeeByID(IFMSConstant.LoggedinUserID);

                // op.txtOrderBy.Text = employee.EmployeeName;
                // op.txtOrderDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                op.Run();

            }
            else
            {
                MessageBox.Show("No sales report for this date range.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private class SalesmanWiseSalesReport
        {
            public string EmployeeName { get; set; }
            public int SalesId { get; set; }
            public string SalesDate { get; set; }
            public string ProductName { get; set; }
            public decimal Quantity { get; set; }
            public decimal Price { get; set; }
            public decimal Total { get; set; }

        }


        private void CustomerWiseSalesDetailReport(int customerID)
        {
            _fromDate = dtpFromDate.Value.ToString("yyyy/MM/dd") + dayStartTime;
            _toDate = dtpToDate.Value.ToString("yyyy/MM/dd") + dayEndTime;

            List<SalesAndPurchaseReport> lstSalesReport = new List<SalesAndPurchaseReport>();
            Customer customer = new CustomerManager().GetCustomerByID(customerID);

            rptCustomerWiseSalesDetail op = new rptCustomerWiseSalesDetail();
            lstSalesReport = DataAccessProxy.GetAllSalesInformationAccordingToDate(_fromDate, _toDate, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<SalesAndPurchaseReport>().ToList();

            lstSalesReport = lstSalesReport.Where(s => s.CustomerID == customerID).Cast<SalesAndPurchaseReport>().ToList();


            if (lstSalesReport.Count > 0)
            {
                op.DataSource = lstSalesReport;
                rptViewer.Document = op.Document;
                rptViewer.Dock = DockStyle.Fill;
                GetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
                op.txtPharmacyName.Text = pharmacyName;
                op.txtAddress.Text = pharmacyAddress;
                op.txtCustomerName.Text = customer.CustomerName;
                op.txtCustomerAddress.Text = customer.Address;
                op.txtPhone.Text = customer.Phone;
                // Employee employee = DataAccessProxy.GetEmployeeByID(IFMSConstant.LoggedinUserID);

                // op.txtOrderBy.Text = employee.EmployeeName;
                // op.txtOrderDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                op.Run();
                btnDownload.Enabled = true;
            }
            else
            {
                MessageBox.Show("No sales report for this date range.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        private class ZonewisesalesReport
        {
            public string ZoneName { get; set; }
            public string CustomerName { get; set; }
            public string Address { get; set; }
            public decimal DueAmount { get; set; }
        }


        private decimal GetDueAmountByCustomerID(int customerId)
        {
            decimal dueAmount = default(decimal);
            dueAmount = DataAccessProxy.GetCustomerDueAmountByCustomerID(customerId, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID);
            return dueAmount;
        }


        private void ZoneWiseSalesReport()
        {
            _fromDate = dtpFromDate.Value.ToString("yyyy/MM/dd") + dayStartTime;
            _toDate = dtpToDate.Value.ToString("yyyy/MM/dd") + dayEndTime;

            int zoneID = 0;
            int.TryParse(cmbCustomerName.Value.ToString(), out zoneID);

            List<SalesAndPurchaseReport> lstSalesReport = new List<SalesAndPurchaseReport>();

            rptZoneWiseSalesDetail op = new rptZoneWiseSalesDetail();
            lstSalesReport = DataAccessProxy.GetAllZoneWiseSalesInformationAccordingToDate(_fromDate, _toDate, MTBFConstants.AppConstants.BranchID, zoneID).Cast<SalesAndPurchaseReport>().ToList();

            lstSalesReport = lstSalesReport.OrderBy(z => z.ZoneName).ToList();

            if (lstSalesReport.Count > 0)
            {
                op.DataSource = lstSalesReport;
                rptViewer.Document = op.Document;
                rptViewer.Dock = DockStyle.Fill;
                GetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
                op.txtPharmacyName.Text = pharmacyName;
                op.txtAddress.Text = pharmacyAddress;
                op.Run();
                btnDownload.Enabled = true;
            }
            else
            {
                MessageBox.Show("No sales report for this date range.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void DateAndZoneWiseDueReport()
        {
            string startDate = "2016/01/01";
            string endDate = dtpFromDate.Value.AddDays(-1).ToString("yyyy/MM/dd") + dayEndTime;
            _fromDate = dtpFromDate.Value.ToString("yyyy/MM/dd") + dayStartTime;
            _toDate = dtpToDate.Value.ToString("yyyy/MM/dd") + dayEndTime;

            int zoneID = 0;
            int.TryParse(cmbCustomerName.Value.ToString(), out zoneID);

            List<CustomerZoneWiseDue> lstSalesReport = new List<CustomerZoneWiseDue>();
            List<CustomerZoneWiseDue> lstPreviousSalesReport = new List<CustomerZoneWiseDue>();
            rptZoneWiseSalesDetailDue op = new rptZoneWiseSalesDetailDue();
            lstSalesReport = new SalesManager().GetDateAndZoneWiseDue(_fromDate, _toDate, MTBFConstants.AppConstants.BranchID).Cast<CustomerZoneWiseDue>().ToList();

            lstPreviousSalesReport = new SalesManager().GetDateAndZoneWiseDue(startDate, endDate, MTBFConstants.AppConstants.BranchID).Cast<CustomerZoneWiseDue>().ToList();


            if (zoneID > 0)
            {
                lstSalesReport = lstSalesReport.Where(z => z.ZoneID == zoneID).ToList();
            }

            lstSalesReport = lstSalesReport.OrderBy(z => z.OrganizationName).ToList();

            foreach (CustomerZoneWiseDue zoneWiseDue in lstSalesReport)
            {
                CustomerZoneWiseDue prevousDeue = lstPreviousSalesReport.Where(d => d.CustomerID == zoneWiseDue.CustomerID).FirstOrDefault();
                zoneWiseDue.PreviousDue = (prevousDeue != null) ? prevousDeue.PresentDue - prevousDeue.ReturnAmount : 0;
                zoneWiseDue.ActualDue = ((zoneWiseDue.PresentDue - zoneWiseDue.ReturnAmount) + zoneWiseDue.PreviousDue);
            }

            lstPreviousSalesReport = lstPreviousSalesReport.Where(z => z.ZoneID == zoneID).ToList();

            foreach (CustomerZoneWiseDue zoneWiseDue in lstPreviousSalesReport)
            {
                CustomerZoneWiseDue alreadyAdded = lstSalesReport.Where(d => d.CustomerID == zoneWiseDue.CustomerID).FirstOrDefault();
                if (alreadyAdded == null)
                {
                    //if (zoneWiseDue.SalesAmount == 0 && zoneWiseDue.ReceiveAmount > 0)
                    //{
                    //    zoneWiseDue.PreviousDue = (-1 * zoneWiseDue.PresentDue);
                    //    zoneWiseDue.PresentDue = zoneWiseDue.PreviousDue - zoneWiseDue.ReceiveAmount;
                    //}
                    //else
                    //{
                    //    zoneWiseDue.PreviousDue = zoneWiseDue.PresentDue;
                    //    zoneWiseDue.PresentDue = 0;
                    //}

                    zoneWiseDue.ActualDue = zoneWiseDue.PresentDue - zoneWiseDue.ReturnAmount;
                    zoneWiseDue.PreviousDue = zoneWiseDue.PresentDue - zoneWiseDue.ReturnAmount;

                    zoneWiseDue.PresentDue = 0;
                    zoneWiseDue.SalesAmount = 0;
                    zoneWiseDue.ReceiveAmount = 0;
                    zoneWiseDue.ReturnAmount = 0;

                    lstSalesReport.Add(zoneWiseDue);
                }
            }




            lstSalesReport = lstSalesReport.OrderBy(z => z.ZoneName).ToList();

            if (lstSalesReport.Count > 0)
            {
                op.DataSource = lstSalesReport;
                op.txtFromDate.Text = dtpFromDate.Value.ToString("dd/MM/yyyy");
                op.txtToDate.Text = dtpToDate.Value.ToString("dd/MM/yyyy");
                op.txtZoneName.Text = cmbCustomerName.Text;
                rptViewer.Document = op.Document;
                rptViewer.Dock = DockStyle.Fill;
                GetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
                op.txtPharmacyName.Text = pharmacyName;
                op.txtAddress.Text = pharmacyAddress;
                op.Run();
                btnDownload.Enabled = true;
            }
            else
            {
                MessageBox.Show("No sales report for this date range.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private DataTable BuildCustomerOutstandingTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("CustomerID");
            table.Columns.Add("SalesID");
            table.Columns.Add("Sales Date");
            table.Columns.Add("Sales Amount");
            table.Columns.Add("Rec Amount");
            table.Columns.Add("Due Amount");
            table.Columns.Add("Receive Number");
            table.Columns.Add("Receive Date");
            table.Columns.Add("Receive Amount");
            table.Columns.Add("Actual Due");
            return table;
        }


        private DataTable LoadCustomerOutstandingReport(List<SalesOrder> lstSalesOrder, List<CashReceive> lstCashReceive, int customerID)
        {
            DataTable customerOutstandingTable = BuildCustomerOutstandingTable();

            CustomerPreviousDue previousDue = new PreviousDueManager().GetCustomerPreviousDueByCustomerID(customerID);

            List<ClassCustomerOutStanding> lstCustomerOutStanding = new List<ClassCustomerOutStanding>();
            List<ClassCustomerOutStanding> lstNewCustomerOutStanding = new List<ClassCustomerOutStanding>();

            if (previousDue != null)
            {
                ClassCustomerOutStanding cusOutStanding = new ClassCustomerOutStanding();
                cusOutStanding.CustomerID = previousDue.CustomerID;
                cusOutStanding.SalesID = 0;
                cusOutStanding.SalesDate = previousDue.DueDate.ToString("dd/MM/yyyy");
                cusOutStanding.SalesAmount = previousDue.Amount;
                cusOutStanding.RecAmount = 0;
                cusOutStanding.DueAmount = previousDue.Amount;
                cusOutStanding.ReceiveNumber = 0;
                cusOutStanding.ReceiveDate = "";
                cusOutStanding.ReceievAmount = 0;
                cusOutStanding.ActualDue = 0;
                lstCustomerOutStanding.Add(cusOutStanding);

            }



            foreach (SalesOrder salesOrder in lstSalesOrder)
            {
                ClassCustomerOutStanding cusOutStanding = new ClassCustomerOutStanding();
                cusOutStanding.CustomerID = salesOrder.CustomerID;
                cusOutStanding.SalesID = salesOrder.SalesID;
                cusOutStanding.SalesAmount = salesOrder.SalesAmount + salesOrder.CarryingLoading;
                cusOutStanding.SalesDate = salesOrder.SalesDate.ToString("dd/MM/yyyy");
                cusOutStanding.RecAmount = salesOrder.ReceiveAmount;
                cusOutStanding.DueAmount = ((salesOrder.SalesAmount + salesOrder.CarryingLoading) - (salesOrder.Discount + salesOrder.ReceiveAmount));
                cusOutStanding.ReceiveNumber = 0;
                cusOutStanding.ReceiveDate = "";
                cusOutStanding.ReceievAmount = 0;
                cusOutStanding.ActualDue = 0;
                lstCustomerOutStanding.Add(cusOutStanding);
            }


            for (int i = 0; i < lstCashReceive.Count; i++)
            {
                ClassCustomerOutStanding custOutStanding = (i > lstCustomerOutStanding.Count - 1) ? null : lstCustomerOutStanding[i];
                if (custOutStanding != null)
                {
                    custOutStanding.ReceiveNumber = lstCashReceive[i].CashReceiveID;
                    custOutStanding.ReceiveDate = lstCashReceive[i].ReceiveDate.ToString("dd/MM/yyyy");
                    custOutStanding.ReceievAmount = lstCashReceive[i].Amount;
                    // lstNewCustomerOutStanding.Add(custOutStanding);
                }
                else
                {
                    custOutStanding = new ClassCustomerOutStanding();
                    custOutStanding.ReceiveNumber = lstCashReceive[i].CashReceiveID;
                    custOutStanding.ReceiveDate = lstCashReceive[i].ReceiveDate.ToString("dd/MM/yyyy");
                    custOutStanding.ReceievAmount = lstCashReceive[i].Amount;
                    // lstNewCustomerOutStanding.Add(custOutStanding);
                    lstCustomerOutStanding.Add(custOutStanding);
                }
            }

            lstNewCustomerOutStanding = (lstNewCustomerOutStanding.Count == 0) ? lstCustomerOutStanding : lstNewCustomerOutStanding;


            foreach (ClassCustomerOutStanding cOutStanding in lstNewCustomerOutStanding)
            {
                DataRow row = customerOutstandingTable.NewRow();

                row["CustomerID"] = cOutStanding.CustomerID;
                row["SalesID"] = cOutStanding.SalesID;
                row["Sales Date"] = cOutStanding.SalesDate;
                row["Sales Amount"] = cOutStanding.SalesAmount;
                row["Rec Amount"] = cOutStanding.RecAmount;
                row["Due Amount"] = cOutStanding.DueAmount;
                row["Receive Number"] = cOutStanding.ReceiveNumber;
                row["Receive Date"] = cOutStanding.ReceiveDate;
                // decimal receiveAmount = CalcualteTotalReceiveAmountByDate(lstCashReceive, salesOrder.SalesDate.ToString("dd/MM/yyyy"));
                row["Receive Amount"] = cOutStanding.ReceievAmount;
                row["Actual Due"] = cOutStanding.ActualDue;
                customerOutstandingTable.Rows.Add(row);
            }


            return customerOutstandingTable;
        }

        private class ClassCustomerOutStanding
        {
            public int CustomerID { get; set; }
            public int SalesID { get; set; }
            public string SalesDate { get; set; }
            public decimal SalesAmount { get; set; }
            public decimal RecAmount { get; set; }
            public decimal DueAmount { get; set; }
            public int ReceiveNumber { get; set; }
            public string ReceiveDate { get; set; }
            public decimal ReceievAmount { get; set; }
            public decimal ActualDue { get; set; }


        }

        /// <summary>
        /// Method to print customer outstanding report.
        /// </summary>
        private void CustomerOutStandingReport()
        {
            int customerID = 0;
            int.TryParse(cmbCustomerName.Value.ToString(), out customerID);
            List<SalesOrder> lstSalesOrder = new List<SalesOrder>();
            lstSalesOrder = DataAccessProxy.GetSalesDetailByCustomerID(customerID).Cast<SalesOrder>().ToList();

            List<CustomerOutstanding> lstCustomerOutstanding = new List<CustomerOutstanding>();

            _fromDate = dtpFromDate.Value.ToString("yyyy/MM/dd") + dayStartTime;
            _toDate = dtpToDate.Value.ToString("yyyy/MM/dd") + dayEndTime;

            lstCustomerOutstanding = new SalesManager().GetCustomerOutStandingByCustomerID(customerID, MTBFConstants.AppConstants.BranchID, _fromDate, _toDate).OrderBy(c => c.SalesDate).ToList();
            decimal balance = 0;
            decimal totalsales = 0;

            foreach (SalesOrder salesOrder in lstSalesOrder)
            {
                totalsales = totalsales + salesOrder.SalesAmount;
            }


            foreach (CustomerOutstanding customerOutStanding in lstCustomerOutstanding)
            {
                balance = (customerOutStanding.Debit > 0) ? (balance + customerOutStanding.Debit) : (balance - customerOutStanding.Credit);
                customerOutStanding.Balance = balance;
            }


            if (lstCustomerOutstanding.Count > 0)
            {
                rptCustomerDue op = new rptCustomerDue();
                op.DataSource = lstCustomerOutstanding;
                rptViewer.Document = op.Document;
                rptViewer.Dock = DockStyle.Fill;
                GetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
                op.txtPharmacyName.Text = pharmacyName;
                op.txtAddress.Text = pharmacyAddress;
                op.txtTotalSales.Text = totalsales.ToString("F2");
                Customer customer = DataAccessProxy.GetCustomerByID(customerID);
                if (customer != null)
                {
                    op.txtCustomerName.Text = customer.CustomerName;
                    op.txtCustomerAddress.Text = customer.Address;
                    op.txtPhone.Text = customer.Phone;
                }

                op.Run();
                btnDownload.Enabled = true;
            }
            else
            {
                MessageBox.Show("No available customer out standing report.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private decimal CalculateTotalReturnAmount(List<SalesReturn> lstSalesReturn)
        {
            decimal totalReturnAmount = 0;

            foreach (SalesReturn sReturn in lstSalesReturn)
            {
                totalReturnAmount = totalReturnAmount + sReturn.Total;
            }

            return totalReturnAmount;
        }

        private decimal CalcualteAcutalDueAmount(DataTable table, decimal totalReturnAmount)
        {
            decimal actualDueAmount = 0;
            decimal receiveAmount = 0;
            decimal dueAmount = 0;
            foreach (DataRow row in table.Rows)
            {
                receiveAmount = receiveAmount + Convert.ToDecimal(row["Receive Amount"]);
                dueAmount = dueAmount + Convert.ToDecimal(row["Due Amount"]);

            }

            actualDueAmount = dueAmount - (receiveAmount + totalReturnAmount);
            return actualDueAmount;
        }

        private DataTable LoadSupplierOutstandingReport(List<PurchaseOrder> lstPurchaseOrder, List<Payment> lstPayment, int supplierID)
        {
            DataTable SupplierOutstandingTable = BuildSupplierOutstandingTable();
            Supplier supplier = DataAccessProxy.GetSupplierByID(supplierID);
            Company company = DataAccessProxy.GetCompanyByID(supplier.CompanyID);


            CustomerPreviousDue previousDue = new PreviousDueManager().GetSupplierPreviousDueBySupplierID(supplierID);
            if (previousDue != null)
            {

                DataRow row = SupplierOutstandingTable.NewRow();

                row[0] = company.CompanyName;
                row[1] = supplier.SupplierName;
                row[2] = previousDue.DueDate.ToString("dd/MM/yyyy");
                row[3] = previousDue.Amount;
                row[4] = 0;
                row[5] = 0;
                row[6] = 0;
                row[7] = 0;
                SupplierOutstandingTable.Rows.Add(row);

            }

            if (lstPurchaseOrder.Count >= lstPayment.Count)
            {
                foreach (PurchaseOrder purchaseOrder in lstPurchaseOrder)
                {

                    DataRow row = SupplierOutstandingTable.NewRow();

                    row[0] = company.CompanyName;
                    row[1] = supplier.SupplierName;
                    row[2] = purchaseOrder.PurchaseDate.ToShortDateString();
                    row[3] = purchaseOrder.PurchaseAmount;
                    row[4] = purchaseOrder.PaidAmount;
                    row[5] = 0;
                    row[6] = 0;
                    row[7] = 0;
                    SupplierOutstandingTable.Rows.Add(row);
                }
            }
            else if (lstPayment.Count >= lstPurchaseOrder.Count)
            {
                foreach (Payment payment in lstPayment)
                {
                    DataRow row = SupplierOutstandingTable.NewRow();
                    row[0] = company.CompanyName;
                    row[1] = supplier.SupplierName;
                    row[2] = 0;
                    row[3] = 0;
                    row[4] = 0;
                    row[5] = payment.PaymentDate.ToShortDateString();
                    row[6] = payment.Amount;
                    row[7] = 0;
                    SupplierOutstandingTable.Rows.Add(row);
                }
            }
            return SupplierOutstandingTable;
        }

        private void PrintSuplierOutstandingReport()
        {

            int supplierID = 0;
            int.TryParse(cmbCustomerName.Value.ToString(), out supplierID);

            List<SupplierStatement> lstSupplierStatement = new List<SupplierStatement>();
            lstSupplierStatement = new SupplierManager().GetSupplierStatementSupplierID(supplierID);

            decimal balance = 0;
            foreach (SupplierStatement statement in lstSupplierStatement)
            {
                balance = (statement.DrAmount > 0) ? (balance + statement.DrAmount) : (balance - statement.CrAmount);
                statement.Balance = balance;
            }

            if (lstSupplierStatement.Count > 0)
            {
                rptSupplierOutstanding op = new rptSupplierOutstanding();
                op.DataSource = lstSupplierStatement;
                rptViewer.Document = op.Document;
                rptViewer.Dock = DockStyle.Fill;
                GetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
                op.txtPharmacyName.Text = pharmacyName;
                op.txtAddress.Text = pharmacyAddress;


                // Employee employee = DataAccessProxy.GetEmployeeByID(IFMSConstant.LoggedinUserID);

                // op.txtOrderBy.Text = employee.EmployeeName;
                // op.txtOrderDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                op.Run();
                btnDownload.Enabled = true;

            }
            else
            {
                MessageBox.Show("No available supplier outstanding report.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


            //List<PurchaseOrder> lstPurchaseOrder = new List<PurchaseOrder>();
            //lstPurchaseOrder = DataAccessProxy.GetPurchaseOrderBySupplierID(supplierID).Cast<PurchaseOrder>().Where(p => p.BranchID == MTBFConstants.AppConstants.BranchID && p.OrganizationID == MTBFConstants.AppConstants.OrganizationID).ToList();

            //List<Payment> lstPayment = new List<Payment>();
            //lstPayment = DataAccessProxy.GetPaymentInformationBySupplierID(supplierID).Cast<Payment>().ToList();

            //DataTable supplierOutstandingTable = LoadSupplierOutstandingReport(lstPurchaseOrder, lstPayment, supplierID);

            //decimal dueAmount = 0;
            //decimal previousDueAmount = 0;
            //decimal receiveAmount = 0;
            //decimal discount = 0;
            //decimal salesAmount = 0;

            //if (lstPurchaseOrder.Count >= lstPayment.Count)
            //{

            //    for (int i = 0; i <= supplierOutstandingTable.Rows.Count - 1; i++)
            //    {
            //        if (i < lstPayment.Count)
            //        {
            //            if (i > 0)
            //            {
            //                DataRow row1 = supplierOutstandingTable.Rows[i - 1];
            //                previousDueAmount = Convert.ToDecimal(row1[7]);
            //            }
            //            decimal.TryParse(supplierOutstandingTable.Rows[i][3].ToString(), out salesAmount);
            //            // decimal.TryParse(supplierOutstandingTable.Rows[i][3].ToString() ,out discount );
            //            decimal.TryParse(supplierOutstandingTable.Rows[i][4].ToString(), out receiveAmount);

            //            Payment payment = lstPayment[i];

            //            dueAmount = (salesAmount + previousDueAmount) - (discount + receiveAmount + payment.Amount);
            //            supplierOutstandingTable.Rows[i][5] = payment.PaymentDate.ToShortDateString();
            //            supplierOutstandingTable.Rows[i][6] = payment.Amount;
            //            supplierOutstandingTable.Rows[i][7] = dueAmount;
            //        }
            //        else if (i >= lstPayment.Count)
            //        {
            //            DataRow row1 = supplierOutstandingTable.Rows[i];
            //            previousDueAmount = Convert.ToDecimal(row1[7]);

            //            decimal.TryParse(supplierOutstandingTable.Rows[i][3].ToString(), out salesAmount);

            //            // decimal.TryParse(supplierOutstandingTable.Rows[i][3].ToString(), out discount);
            //            decimal.TryParse(supplierOutstandingTable.Rows[i][4].ToString(), out receiveAmount);

            //            dueAmount = (salesAmount + previousDueAmount) - (discount + receiveAmount);
            //            supplierOutstandingTable.Rows[i][7] = dueAmount;
            //        }
            //    }

            //}
            //else if (lstPayment.Count >= lstPurchaseOrder.Count)
            //{
            //    for (int i = 0; i <= supplierOutstandingTable.Rows.Count - 1; i++)
            //    {

            //        if (i < lstPurchaseOrder.Count)
            //        {
            //            if (i > 0)
            //            {
            //                DataRow row1 = supplierOutstandingTable.Rows[i - 1];
            //                previousDueAmount = Convert.ToDecimal(row1[7]);
            //            }
            //            receiveAmount = Convert.ToDecimal(supplierOutstandingTable.Rows[i][6]);

            //            PurchaseOrder purchaseOrder = lstPurchaseOrder[i];

            //            dueAmount = (purchaseOrder.PurchaseAmount + previousDueAmount) - (discount + receiveAmount + purchaseOrder.PaidAmount);

            //            supplierOutstandingTable.Rows[i][2] = purchaseOrder.PurchaseDate.ToShortDateString();
            //            supplierOutstandingTable.Rows[i][3] = purchaseOrder.PurchaseAmount;
            //            supplierOutstandingTable.Rows[i][4] = purchaseOrder.PaidAmount;
            //            supplierOutstandingTable.Rows[i][7] = dueAmount;
            //        }
            //        else if (i >= lstPurchaseOrder.Count)
            //        {
            //            DataRow row1 = supplierOutstandingTable.Rows[i - 1];
            //            previousDueAmount = Convert.ToDecimal(row1[7]);
            //            receiveAmount = Convert.ToDecimal(supplierOutstandingTable.Rows[i][6]);
            //            dueAmount = (previousDueAmount) - (receiveAmount);
            //            supplierOutstandingTable.Rows[i][2] = string.Empty;
            //            supplierOutstandingTable.Rows[i][7] = dueAmount;
            //        }
            //    }
            //}

            //if (supplierOutstandingTable.Rows.Count > 0)
            //{
            //    rptSupplierOutstanding op = new rptSupplierOutstanding();
            //    op.DataSource = supplierOutstandingTable;
            //    rptViewer.Document = op.Document;
            //    rptViewer.Dock = DockStyle.Fill;
            //    GetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
            //    op.txtPharmacyName.Text = pharmacyName;
            //    op.txtAddress.Text = pharmacyAddress;


            //    // Employee employee = DataAccessProxy.GetEmployeeByID(IFMSConstant.LoggedinUserID);

            //    // op.txtOrderBy.Text = employee.EmployeeName;
            //    // op.txtOrderDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            //    op.Run();
            //    btnDownload.Enabled = true;

            //}
            //else
            //{
            //    MessageBox.Show("No available supplier outstanding report.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}


        }

        private DataTable BuildSupplierOutstandingTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Company Name");
            table.Columns.Add("Supplier Name");
            table.Columns.Add("Purchase Date");
            table.Columns.Add("Purchase Amount");
            table.Columns.Add("Paid Amount");
            table.Columns.Add("Payment Date");
            table.Columns.Add("Payment Amount");
            table.Columns.Add("Due Amount");
            return table;
        }

        /// <summary>
        /// Method to Get pharmacy information 
        /// </summary>
        /// <param name="PharmacyName"></param>
        /// <param name="Address"></param>
        private void GetPharmachyInforamation(ref string PharmacyName, ref string Address)
        {
            Branch branch = DataAccessProxy.GetBracnhByID(MTBFConstants.AppConstants.BranchID);
            if (branch != null)
            {
                PharmacyName = branch.BranchName;
                Address = branch.Address + ", " + branch.Phone + ", " + branch.Fax;
            }
        }

        private void PrintBalanceSheet()
        {
            _fromDate = "01-01-" + cmbCustomerName.Text;
            _toDate = "12-31-" + cmbCustomerName.Text;

            List<BalanceSheet> lstBalanceSheet = new List<BalanceSheet>();
            List<BalanceSheetBackup> lstbalanceSheetBackup = new List<BalanceSheetBackup>();

            lstBalanceSheet = DataAccessProxy.GetAllBalanceSheet(_fromDate, _toDate).Cast<BalanceSheet>().Where(b => b.BranchID == MTBFConstants.AppConstants.BranchID && b.OrganizationID == MTBFConstants.AppConstants.OrganizationID).ToList();
            decimal profit = DataAccessProxy.GetProfitOrLoss(_fromDate, _toDate, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID);

            InsertRetainEarning(profit, Convert.ToInt32(cmbCustomerName.Text));
            lstbalanceSheetBackup = DataAccessProxy.GetBalanceSheetBackUpByFiscalYear(cmbCustomerName.Text).Cast<BalanceSheetBackup>().ToList();

            if (lstbalanceSheetBackup.Count > 0)
            {
                DeleteBalanceSheetBackup(lstbalanceSheetBackup);
                InsertBalanceSheetBackup(Convert.ToInt32(cmbCustomerName.Text), lstBalanceSheet);
            }
            else
            {
                InsertBalanceSheetBackup(Convert.ToInt32(cmbCustomerName.Text), lstBalanceSheet);
            }

            rptBalanceSheet op = new rptBalanceSheet();
            op.DataSource = ListToDataTable(lstBalanceSheet, profit, Convert.ToInt32(cmbCustomerName.Text));
            rptViewer.Document = op.Document;
            rptViewer.Dock = DockStyle.Fill;
            GetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
            op.txtPharmacyName.Text = pharmacyName;
            op.txtAddress.Text = pharmacyAddress;
            op.txtReportCaption.Text = "For Year Ending Dec. 31, " + cmbCustomerName.Text;
            op.Run();
        }

        private int InsertBalanceSheetBackup(int fiscalYear, List<BalanceSheet> lsBalanceSheets)
        {
            int resut = (int)IFMSEnum.ReturnResult.Success;
            List<BalanceSheetBackup> lstBalanceSheetBackup = new List<BalanceSheetBackup>();

            foreach (BalanceSheet balanceSheet in lsBalanceSheets)
            {
                BalanceSheetBackup balanceSheetBackup = new BalanceSheetBackup();
                balanceSheetBackup.FiscalYear = fiscalYear;
                balanceSheetBackup.AccountType = balanceSheet.AccountType;
                balanceSheetBackup.AccountTypeName = balanceSheet.AccountTypeName;
                balanceSheetBackup.AccountsName = balanceSheet.AccountsName;
                balanceSheetBackup.Amount = balanceSheet.Amount;

                lstBalanceSheetBackup.Add(balanceSheetBackup);
            }
            resut = DataAccessProxy.InsertBalanceSheetBackup(lstBalanceSheetBackup);
            return resut;
        }

        private int DeleteBalanceSheetBackup(List<BalanceSheetBackup> lstBalanceSheetBackup)
        {
            return DataAccessProxy.DeleteBalanceSheetBackup(lstBalanceSheetBackup);
        }

        private void PrintIncomeStatement()
        {
            //_fromDate = "01-01-" + cmbCustomerName.Text;
            //_toDate = "12-31-" + cmbCustomerName.Text;
            _fromDate = dtpFromDate.Value.ToString("yyyy/MM/dd");
            _toDate = dtpToDate.Value.ToString("yyyy/MM/dd");

            List<IncomeStatement> lstBalanceSheet = new List<IncomeStatement>();
            lstBalanceSheet = DataAccessProxy.GetAllIncomeStatement(_fromDate, _toDate).Cast<IncomeStatement>().Where(b => b.BranchID == MTBFConstants.AppConstants.BranchID && b.OrganizationID == MTBFConstants.AppConstants.OrganizationID).ToList();
            decimal profit = DataAccessProxy.GetProfitOrLoss(_fromDate, _toDate, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID);
            rptIncomeStatement op = new rptIncomeStatement();
            op.DataSource = lstBalanceSheet;
            rptViewer.Document = op.Document;
            rptViewer.Dock = DockStyle.Fill;
            GetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
            op.txtPharmacyName.Text = pharmacyName;
            op.txtAddress.Text = pharmacyAddress;
            op.txtProfitOrLoss.Text = profit.ToString();
            //  op.txtReportCaption.Text = "For Year Ending Dec. 31, " + cmbCustomerName.Text;
            op.Run();
        }

        public DataTable ListToDataTable<T>(List<T> list, decimal profit, int fiscalYear)
        {
            RetainEarning retinEarning = new RetainEarning();
            int previousFiscalYear = 0;
            DataTable dt = new DataTable();

            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                dt.Columns.Add(new DataColumn(info.Name, info.PropertyType));
            }
            foreach (T t in list)
            {
                DataRow row = dt.NewRow();
                foreach (PropertyInfo info in typeof(T).GetProperties())
                {
                    row[info.Name] = info.GetValue(t, null);
                }
                dt.Rows.Add(row);
            }
            DataRow profitrow = dt.NewRow();
            profitrow[0] = "Liability";
            profitrow[1] = "Short term Liabilities";
            profitrow[2] = "Retain Earning";

            retinEarning = DataAccessProxy.GetRetainEarningByFiscalYear(fiscalYear);
            if (retinEarning != null)
            {
                profitrow[3] = retinEarning.EarningAmount;

            }
            else
            {
                int.TryParse(fiscalYear.ToString(), out previousFiscalYear);
                retinEarning = DataAccessProxy.GetRetainEarningByFiscalYear(previousFiscalYear - 1);
                if (retinEarning != null)
                {
                    profitrow[3] = (retinEarning.EarningAmount + profit).ToString();
                }
                else
                {
                    profitrow[3] = profit.ToString();
                }
            }

            dt.Rows.Add(profitrow);
            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="profitAndLoss"></param>
        /// <param name="fiscalYear"></param>
        /// <returns></returns>
        private int InsertRetainEarning(decimal profitAndLoss, int fiscalYear)
        {
            int resut = (int)IFMSEnum.ReturnResult.Success;
            RetainEarning retainEarning = new RetainEarning();
            RetainEarning previousRetainEarning = DataAccessProxy.GetRetainEarningByFiscalYear(fiscalYear - 1);
            retainEarning.EarningDate = DateTime.Now;
            if (previousRetainEarning != null)
            {
                profitAndLoss = previousRetainEarning.EarningAmount + profitAndLoss;
            }
            retainEarning.EarningAmount = profitAndLoss;
            retainEarning.FiscalYear = fiscalYear;
            resut = DataAccessProxy.InsertRetainEarning(retainEarning);
            return resut;
        }

        /// <summary>
        /// Method to load fiscal year combo.
        /// </summary>
        private void LoadFinancialYear()
        {
            RetainEarning retainEarning = DataAccessProxy.GetAllRetainEarning().Cast<RetainEarning>().ToList().FirstOrDefault();
            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("Name");

            int year = DateTime.Now.Year;

            if (retainEarning != null)
            {
                year = Convert.ToInt32(retainEarning.FiscalYear);
            }

            int id = 1;
            for (int i = year; i <= DateTime.Now.Year; i++)
            {
                DataRow row = table.NewRow();
                row[0] = id;
                row[1] = i;
                table.Rows.Add(row);
                id++;
            }

            cmbCustomerName.DisplayMember = "Name";
            cmbCustomerName.ValueMember = "ID";
            cmbCustomerName.DataSource = table;



        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                string location = string.Empty;
                FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                DialogResult result = folderBrowser.ShowDialog();
                location = folderBrowser.SelectedPath;
                DataDynamics.ActiveReports.Export.Pdf.PdfExport pdf = new DataDynamics.ActiveReports.Export.Pdf.PdfExport();
                pdf.Export(this.rptViewer.Document, location + "\\" + cmbReportName.Text + ".pdf");
                MessageBox.Show("Downloaded Successfully.");
            }
            catch (Exception)
            {
                MessageBox.Show("Download Failed.");
            }
        }


    }
}
