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
using IMFS.Common.View;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Excel;

namespace Tiles_Inventory
{
    public partial class frmViewSales : BaseForm
    {
        private string pharmacyName;
        private string pharmacyAddress;
        private List<SalesOrderDetail> _lstSalesOrderDetail = new List<SalesOrderDetail>();
        private List<SalesOrder> _lstSalesOrder = new List<SalesOrder>();
        List<SalesRepresentative> lstSalesRepresentative = new List<SalesRepresentative>();
        System.Threading.Thread thread = null;

        public frmViewSales()
        {
            DataAccessProxy = new FacadeManager();
            InitializeComponent();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            LoadSalesOrderInformationByDate(dtpFromDate.Value.ToString("yyyy-MM-dd"), dtpToDate.Value.ToString("yyyy-MM-dd"));
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
            table.Columns.Add("Square Feet");
            table.Columns.Add("Price");
            table.Columns.Add("Total");
            return table;
        }


        private void LoadSalesOrderDetail(int salesID)
        {
            DataTable purchaseDetailtable = BuildSalesDetailTable();
            _lstSalesOrderDetail = DataAccessProxy.GetAllSalesDetailBySalesID(salesID).Cast<SalesOrderDetail>().ToList();
            //  Product product = DataAccessProxy.GetProductInformationByID(orderDetail.ProductID);

            foreach (SalesOrderDetail orderDetail in _lstSalesOrderDetail)
            {
                DataRow row = purchaseDetailtable.NewRow();

                row["Product Name"] = orderDetail.ProductName;
                row["Quantity"] = orderDetail.Quantity;
                row["Square Feet"] = orderDetail.SquareFeet;
                row["Price"] = orderDetail.Price;
                row["Total"] = (orderDetail.SquareFeet > 0) ? (orderDetail.Price * orderDetail.SquareFeet).ToString("F2") : (orderDetail.Price * orderDetail.Quantity).ToString("F2");
                purchaseDetailtable.Rows.Add(row);
            }
            grvSalesOrderDetail.DataSource = purchaseDetailtable;
        }

        private DataTable BuildSalesTable()
        {
            DataTable table = new DataTable();

            table.Columns.Add("SalesID");
            table.Columns.Add("M. Number");
            table.Columns.Add("Sales Date");
            table.Columns.Add("Customer Name");
            table.Columns.Add("Customer Phone");
            table.Columns.Add("Sales Amount", typeof(decimal));
            table.Columns.Add("C.L.Charge", typeof(decimal));
            table.Columns.Add("Discount", typeof(decimal));
            table.Columns.Add("Receive Amount", typeof(decimal));
            // table.Columns.Add("Status");
            table.Columns.Add("Representative");
            return table;
        }

        /// <summary>
        /// Method to load purchase order infromation in grid
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        private void LoadSalesOrderInformationByDate(string fromDate, string toDate)
        {


            fromDate = fromDate + " 00:00:01";
            toDate = toDate + " 23:59:59";
            int customerType = Convert.ToInt32(cmbCustomerType.Value);

            if (customerType > 0)
            {
                _lstSalesOrder = new SalesManager().GetAllDeveloperSalesOrderByDate(fromDate, toDate, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID, customerType).Cast<SalesOrder>().ToList();
            }
            else
            {
                _lstSalesOrder = new SalesManager().GetAllSalesOrderByDate(fromDate, toDate, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<SalesOrder>().ToList();
            }


            LoadDataInGrid(_lstSalesOrder);

        }

        private void LoadDataInGrid(List<SalesOrder> lstSalesOrder)
        {
            lblTotalRecord.Text = lstSalesOrder.Count.ToString();
            // grvSalesOrder.Rows.Clear();
            List<Customer> lstCustomer = new List<Customer>();
            int[] customerIds = lstSalesOrder.Select(i => i.CustomerID).Distinct().ToArray();

            string customerfilter = String.Format("{0} IN ({1})", "CustomerID", String.Join(",", customerIds));
            if (customerIds.Length > 0)
            {
                lstCustomer = new CustomerManager().GetFilteredCustomer(customerfilter);
            }

            DataTable dt = BuildSalesTable();

            int count = 0;
            foreach (SalesOrder salesOrder in lstSalesOrder)
            {
                Customer customer = lstCustomer.Where(c => c.CustomerID == salesOrder.CustomerID).FirstOrDefault();
                SalesRepresentative salesRepre = lstSalesRepresentative.Where(r => r.SalesRepresentativeID == salesOrder.SalesRepresentativeID).FirstOrDefault();
                DataRow dRow = dt.NewRow();
                dRow["SalesID"] = salesOrder.SalesID;
                dRow["M. Number"] = salesOrder.BillNumber;
                dRow["Sales Date"] = salesOrder.SalesDate.ToShortDateString();
                dRow["Customer Name"] = (customer != null) ? customer.CustomerName : "Cash Sales";
                dRow["Customer Phone"] = (customer != null) ? customer.Phone : string.Empty;
                dRow["Sales Amount"] = salesOrder.SalesAmount;
                dRow["C.L.Charge"] = salesOrder.CarryingLoading;
                dRow["Discount"] = salesOrder.Discount;
                dRow["Receive Amount"] = salesOrder.ReceiveAmount;
                // dRow["Status"] = Enum.GetName(typeof(MTBFEnums.DeliveryStatus), salesOrder.Status);
                dRow["Representative"] = (salesRepre != null) ? salesRepre.Name : "N/A";
                dt.Rows.Add(dRow);

            }



            if (_lstSalesOrder.Count == 0)
            {
                MessageBoxHelper.ShowInformation("No data found for this combination.");
            }
            grvSales.DataSource = dt;


            foreach (SalesOrder salesOrder in lstSalesOrder)
            {
                if (salesOrder.IsCanceled)
                {
                    UIHelper.MarkDataGridRowAsInvalid(grvSales.Rows[count]);
                }
                else
                {
                    UIHelper.MarkDataGridRowAsNormal(grvSales.Rows[count]);
                }
                count++;
            }
            grvSales.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
            grvSales.DisplayLayout.PerformAutoResizeColumns(false, PerformAutoSizeType.AllRowsInBand, true);
            grvSales.DisplayLayout.Override.ActiveCellAppearance.Reset();
            grvSales.DisplayLayout.Override.ActiveRowAppearance.Reset();
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (grvSales.Selected.Rows.Count > 0)
            {
                int salesID = Convert.ToInt32(grvSales.Selected.Rows[0].Cells["SalesID"].Value);
                SalesOrder salesorder = DataAccessProxy.GetSalesOrderByID(salesID);
                if (salesorder != null && salesorder.ReceiveAmount == 0)
                {
                    if (!salesorder.IsCanceled)
                    {
                        DialogResult m = MessageBoxHelper.ShowConfirmation("Do you want to cencel this order?");
                        if (m == System.Windows.Forms.DialogResult.Yes)
                        {
                            if (CancleSalesInformation(salesID) == (int)IFMSEnum.ReturnResult.Success)
                            {
                                MessageBoxHelper.ShowInformation("Sales order canceled successfully.");
                                LoadSalesOrderInformationByDate(dtpFromDate.Value.ToString("yyyy-MM-dd"), dtpToDate.Value.ToString("yyyy-MM-dd"));
                            }
                        }
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("This order already canceled.");
                    }
                }
                else
                {
                    MessageBoxHelper.ShowInformation("You can cancel only unpaid sales order");
                }
            }
        }




        private int CancleSalesInformation(int salesOrderID)
        {
            int result = (int)IFMSEnum.ReturnResult.Success;
            List<SalesOrderDetail> lstSalesOrderDetail = new List<SalesOrderDetail>();

            SalesOrder salesOrder = DataAccessProxy.GetSalesOrderByID(salesOrderID);
            salesOrder.SalesAmount = 0;
            salesOrder.Discount = 0;
            salesOrder.ReceiveAmount = 0;
            salesOrder.IsCanceled = true;
            lstSalesOrderDetail = GetAllSalesOrderDetailInformation(salesOrderID);
            result = DataAccessProxy.UpdateSalesOrderInformation(salesOrder, lstSalesOrderDetail);

            return result;

        }


        /// <summary>
        /// Method to insert sales order information.
        /// </summary>
        /// <param name="salesId"></param>
        /// <returns></returns>
        private List<SalesOrderDetail> GetAllSalesOrderDetailInformation(int salesorderID)
        {
            List<SalesOrderDetail> lstSalesOrderDetail = new List<SalesOrderDetail>();
            foreach (SalesOrderDetail orderDetail in DataAccessProxy.GetAllSalesDetailBySalesID(salesorderID))
            {
                orderDetail.Quantity = 0;

                lstSalesOrderDetail.Add(orderDetail);
            }
            return lstSalesOrderDetail;
        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (grvSales.Selected.Rows.Count > 0)
            {
                int salesID = Convert.ToInt32(grvSales.Selected.Rows[0].Cells["SalesID"].Value);
                PrintCreditInvoice(salesID);
            }
            else
            {
                MessageBoxHelper.ShowInformation("You need to select a sales order");
            }
        }


        public void GetBranchAndProductTypeWiseDetailSales(string fromDate, string toDate)
        {
            fromDate = fromDate + " 00:00:01";
            toDate = toDate + " 23:59:59";

            List<ProductTypeWiseSales> lstReportData = new List<ProductTypeWiseSales>();
            List<ProductTypeWiseSales> lstDiscountData = new List<ProductTypeWiseSales>();
            List<ProductTypeWiseSales> lstOtherReceive = new List<ProductTypeWiseSales>();
            lstReportData = new SalesManager().BranchAndProductTypeWiseDetailReport(fromDate, toDate, MTBFConstants.AppConstants.BranchID);
            lstDiscountData = new SalesManager().BranchAndProductTypeWiseDiscountReport(fromDate, toDate, MTBFConstants.AppConstants.BranchID);
            lstOtherReceive = new SalesManager().BranchAndProductTypeWiseOtherReceiveReport(fromDate, toDate, MTBFConstants.AppConstants.BranchID);




            string[] productTypes = lstReportData.Select(r => r.TypeName).Distinct().ToArray();
            int[] customerTypes = lstReportData.Select(r => r.CustomerType).Distinct().ToArray();

            DataTable dt = new DataTable();

            dt.Columns.Add("Customer Type");
            foreach (string pType in productTypes)
            {
                dt.Columns.Add(pType, typeof(decimal));
            }
            dt.Columns.Add("Carrying Loading", typeof(decimal));
            dt.Columns.Add("Cutting Charge", typeof(decimal));
            dt.Columns.Add("Other Charge", typeof(decimal));
            dt.Columns.Add("Gross Sales", typeof(decimal));
            dt.Columns.Add("Discount", typeof(decimal));
            dt.Columns.Add("Net Sales", typeof(decimal));


            List<ProductType> lstProductType = new List<ProductType>();
            lstProductType = new ProductManager().GetAllProductType();

            List<string> lstMemoNumber = new List<string>();

            foreach (int customerType in customerTypes)
            {
                decimal total = 0;
                DataRow row = dt.NewRow();
                row["Customer Type"] = Enum.GetName(typeof(MTBFEnums.CustomerType), customerType);
                foreach (string pType in productTypes)
                {
                    if (!string.IsNullOrEmpty(pType))
                    {
                        decimal salesAmount = CalculateTotalSalesByProductType(lstReportData, pType, customerType);
                        row[pType] = salesAmount.ToString("F2");
                        total = total + salesAmount;
                    }

                }
                ProductTypeWiseSales typeWiseDiscount = lstDiscountData.Where(s => s.CustomerType == customerType).FirstOrDefault();
                ProductTypeWiseSales typeWiseOtherReceive = lstOtherReceive.Where(s => s.CustomerType == customerType).FirstOrDefault();

                row["Discount"] = (typeWiseDiscount != null) ? typeWiseDiscount.Discount : 0;
                row["Gross Sales"] = total.ToString("F2");
                row["Carrying Loading"] = CalculateCarringAmount(lstOtherReceive, customerType);
                row["Cutting Charge"] = CalculateCuttingAmount(lstOtherReceive, customerType);
                row["Other Charge"] = CalculateOtherAmount(lstOtherReceive, customerType);
                total = (typeWiseOtherReceive != null) ? (total + typeWiseOtherReceive.CarryingLoading + typeWiseOtherReceive.CuttingCharge + typeWiseOtherReceive.OtherCharge) : total;
                row["Net Sales"] = (typeWiseDiscount != null) ? (total - (typeWiseDiscount.Discount)).ToString("F2") : total.ToString("F2");
                dt.Rows.Add(row);
            }

            grvExport.DataSource = null;
            grvExport.DataSource = dt;

            ExportTypeSummaryGridData();
            //dt.ToString();

            //ExportTypeWiseReport(@"\Product Typewise Sales.csv", dt);

        }

        private object CalculateCuttingAmount(List<ProductTypeWiseSales> lstOtherReceive, int customerType)
        {
            decimal total = 0;
            foreach (ProductTypeWiseSales sales in lstOtherReceive)
            {
                if (customerType == sales.CustomerType)
                {
                    total = total + sales.CuttingCharge;
                }

            }

            return total;
        }

        private object CalculateOtherAmount(List<ProductTypeWiseSales> lstOtherReceive, int customerType)
        {
            decimal total = 0;
            foreach (ProductTypeWiseSales sales in lstOtherReceive)
            {
                if (customerType == sales.CustomerType)
                {
                    total = total + sales.OtherCharge;
                }
            }

            return total;
        }

        private object CalculateCarringAmount(List<ProductTypeWiseSales> lstOtherReceive, int customerType)
        {
            decimal total = 0;
            foreach (ProductTypeWiseSales sales in lstOtherReceive)
            {
                if (customerType == sales.CustomerType)
                {
                    total = total + sales.CarryingLoading;
                }
            }
            return total;
        }




        public void GetBranchAndProductTypeWiseSales(string fromDate, string toDate)
        {
            fromDate = fromDate + " 00:00:01";
            toDate = toDate + " 23:59:59";

            List<ProductTypeWiseSales> lstReportData = new List<ProductTypeWiseSales>();
            int customerType = Convert.ToInt32(cmbCustomerType.Value);
            if (customerType > 0)
            {
                lstReportData = new SalesManager().BranchAndProductTypeWiseReportByCustomerType(fromDate, toDate, MTBFConstants.AppConstants.BranchID, customerType);
            }
            else
            {
                lstReportData = new SalesManager().BranchAndProductTypeWiseReport(fromDate, toDate, MTBFConstants.AppConstants.BranchID);
            }

            string[] productTypes = lstReportData.Select(r => r.TypeName).Distinct().ToArray();

            DataTable dt = new DataTable();
            dt.Columns.Add("Sales Date");
            dt.Columns.Add("Memo Number");
            dt.Columns.Add("Customer Name");
            foreach (string pType in productTypes)
            {
                dt.Columns.Add(pType);
            }
            dt.Columns.Add("Gross Sales");
            dt.Columns.Add("Discount");
            dt.Columns.Add("Net Sales");


            List<ProductType> lstProductType = new List<ProductType>();
            lstProductType = new ProductManager().GetAllProductType();

            List<string> lstMemoNumber = new List<string>();

            foreach (ProductTypeWiseSales salesReport in lstReportData)
            {
                decimal total = 0;

                if (!lstMemoNumber.Contains(salesReport.MemoNumber))
                {
                    lstMemoNumber.Add(salesReport.MemoNumber);
                    DataRow row = dt.NewRow();
                    row["Sales Date"] = salesReport.SalesDate.ToString("dd/MM/yyyy");
                    row["Memo Number"] = salesReport.MemoNumber;
                    row["Customer Name"] = salesReport.CustomerName.Replace(',', '.');

                    foreach (string pType in productTypes)
                    {
                        decimal salesAmount = CalculateTotalSalesByProductType(lstReportData, pType, salesReport.CustomerName, salesReport.MemoNumber);
                        row[pType] = salesAmount.ToString("F2");
                        total = total + salesAmount;
                    }
                    decimal discount = CalculateTotalDiscount(lstReportData, salesReport.CustomerName, salesReport.MemoNumber);
                    row["Discount"] = discount;
                    row["Gross Sales"] = total.ToString("F2");
                    row["Net Sales"] = (total - discount).ToString("F2");

                    dt.Rows.Add(row);

                }
            }



            dt.ToString();

            ExportTypeWiseReport(@"\Type Wise Sales Report.csv", dt);

        }

        private void ExportTypeWiseReport(string fileName, DataTable table)
        {
            string location = "";


            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            DialogResult result = folderBrowser.ShowDialog();

            if (result == DialogResult.OK)
            {
                location = folderBrowser.SelectedPath + fileName;
                CSVCreator.CreateCSVFile(table, location);
                MessageBoxHelper.ShowInformation("Export successfully.");
            }
        }


        private decimal CalculateTotalSalesByProductType(List<ProductTypeWiseSales> lstReportData, string pType, string customerName, string memoNumber)
        {
            decimal salesAmount = 0;
            foreach (ProductTypeWiseSales tSales in lstReportData)
            {
                if (tSales.MemoNumber == memoNumber && tSales.CustomerName == customerName && tSales.TypeName == pType)
                {
                    salesAmount = salesAmount + tSales.SalesAmount;
                }
            }

            return salesAmount;
        }

        private decimal CalculateTotalSalesByProductType(List<ProductTypeWiseSales> lstReportData, string productType, int customerType)
        {
            decimal salesAmount = 0;
            foreach (ProductTypeWiseSales tSales in lstReportData)
            {
                if (tSales.TypeName == productType && tSales.CustomerType == customerType)
                {
                    salesAmount = salesAmount + tSales.SalesAmount;
                }
            }

            return salesAmount;
        }

        private decimal CalculateTotalDiscount(List<ProductTypeWiseSales> lstReportData, string customerName, string memoNumber)
        {
            decimal discount = 0;
            foreach (ProductTypeWiseSales tSales in lstReportData)
            {
                if (tSales.MemoNumber == memoNumber && tSales.CustomerName == customerName && discount == 0)
                {
                    discount = discount + tSales.Discount;
                }
            }

            return discount;
        }

        private decimal CalculateTotalDiscountByCustomerType(List<ProductTypeWiseSales> lstReportData, int customerType)
        {
            decimal discount = 0;
            foreach (ProductTypeWiseSales tSales in lstReportData)
            {
                if (tSales.CustomerType == customerType)
                {
                    discount = discount + tSales.Discount;
                }
            }

            return discount;
        }


        private void PrintCashInvoice(int salesId, SalesOrder salesOrder)
        {
            Employee employee = new Employee();
            DataTable salesReport = new DataTable();
            Organization pharmacy = new Organization();

            salesReport = DataAccessProxy.GetSalesReport(salesId);

            foreach (DataRow row in salesReport.Rows)
            {
                decimal total = 0;
                decimal.TryParse(row["Total"].ToString(), out total);
                row["Total"] = total.ToString("F2");
            }

            rptInvoice op = new rptInvoice();
            frmSalesReport objmainReport = new frmSalesReport();
            op.DataSource = salesReport;
            objmainReport.rptViewer.Document = op.Document;
            objmainReport.rptViewer.Dock = DockStyle.Fill;
            SetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress, ref pharmacy);
            op.lblCompanyName.Text = pharmacyName;
            op.lblAddress.Text = pharmacyAddress;

            op.txtSubTotal.Text = (salesOrder != null) ? (salesOrder.SalesAmount).ToString() : 0.ToString();
            op.txtDiscount.Text = (salesOrder != null) ? salesOrder.Discount.ToString() : 0.00.ToString();
            op.txtGrandTotal.Text = (salesOrder != null) ? (salesOrder.SalesAmount - salesOrder.Discount).ToString() : 0.ToString();
            op.lblInvoiceNumber.Text = (salesOrder != null) ? (salesOrder.BillNumber).ToString() : 0.ToString();

            // op.lblVATRegistrationNo.Text = pharmacy.RegistrationNumber;
            employee = DataAccessProxy.GetEmployeeByID(IFMSConstant.LoggedinUserID);

            if (pharmacy.OrganizationLogo != null)
            {
                MemoryStream ms = new MemoryStream(pharmacy.OrganizationLogo);
                Image returnImage = Image.FromStream(ms);
                op.picLogo.Image = returnImage;
            }

            op.lblServedby.Text = (employee != null) ? employee.EmployeeName : string.Empty;
            op.Run();
            objmainReport.MdiParent = this.MdiParent;
            objmainReport.Show();

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
            SalesOrder salesOrder = new SalesManager().GetSalesOrderByID(salesId);

            if (salesOrder.CustomerID > 0)
            {
                salesReport = DataAccessProxy.GetCreditSalesReport(salesId);

                decimal salesAmount = 0;
                foreach (DataRow row in salesReport.Rows)
                {
                    decimal total = 0;
                    decimal quantity = 0;
                    decimal qty = 0;
                    decimal price = 0;
                    decimal.TryParse(row["Total"].ToString(), out total);
                    decimal.TryParse(row["SquareFeet"].ToString(), out quantity);
                    decimal.TryParse(row["Quantity"].ToString(), out qty);

                    decimal.TryParse(row["Price"].ToString(), out price);
                    row["Total"] = (qty * price).ToString("f2");
                    row["ProductName"] = row["ProductName"].ToString() + "   " + qty.ToString("F0") + " Pcs";
                    salesAmount = salesAmount + (qty * price);
                }

                decimal dueAmount = default(decimal);
                dueAmount = DataAccessProxy.GetCustomerDueAmountByCustomerID(salesOrder.CustomerID, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID);


                string strAmount = salesAmount.ToString("F2");

                string amountWord = string.Empty;
                string amountInCent = string.Empty;
                if (strAmount.Contains("."))
                {
                    strAmount = strAmount.Substring(strAmount.LastIndexOf(".") + 1);
                    if (strAmount != "00")
                    {
                        amountInCent = UIHelper.NumberToWords(Convert.ToInt32(strAmount));
                    }
                    strAmount = salesAmount.ToString("F2");
                    strAmount = strAmount.Substring(0, strAmount.LastIndexOf("."));
                    amountWord = UIHelper.NumberToWords(Convert.ToInt32(strAmount));
                    if (string.IsNullOrEmpty(amountInCent))
                    {
                        amountWord = amountWord + " " + " only.";
                    }
                    else
                    {
                        amountWord = amountWord + " " + "Taka and " + amountInCent + " poysha only";
                    }
                }
                else
                {
                    amountWord = UIHelper.NumberToWords(Convert.ToInt32(salesAmount));
                    amountWord = amountWord + " " + " only.";
                }

                rptCreditSalesShowRoom op = new rptCreditSalesShowRoom();
                //  rptCreditSalesDesignCollection op = new rptCreditSalesDesignCollection();
                frmSalesReport objmainReport = new frmSalesReport();
                op.DataSource = salesReport;
                objmainReport.rptViewer.Document = op.Document;
                objmainReport.rptViewer.Dock = DockStyle.Fill;
                op.txtTotalDue.Text = dueAmount.ToString();
                SetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress, ref pharmacy);
                op.txtAddress.Text = pharmacyAddress;
                op.txtPharmacyName.Text = pharmacyName;
                op.txtServiceMan.Text = salesReport.Rows[0]["EmployeeName"].ToString();
                op.Run();
                objmainReport.ShowDialog();
            }
            else
            {
                PrintCashInvoice(salesId, salesOrder);
            }



        }




        /// <summary>
        /// Method to insert print invoice.
        /// </summary>
        /// <param name="salesId"></param>
        private void PrintChalan(int salesId)
        {
            Employee employee = new Employee();

            DataTable salesReport = new DataTable();

            Organization pharmacy = new Organization();
            salesReport = DataAccessProxy.GetCreditSalesReport(salesId);
            salesReport.Columns.Add("Box");
            decimal packSize = 0;
            rptChalan op = new rptChalan();
            frmSalesReport objmainReport = new frmSalesReport();
            salesReport.Columns["Box"].ReadOnly = false;
            foreach (DataRow row in salesReport.Rows)
            {
                // Product product = DataAccessProxy.GetProductByName(row["ProductName"].ToString()).Where(p => p.BranchID == MTBFConstants.AppConstants.BranchID && p.OrganizationID == MTBFConstants.AppConstants.OrganizationID).Cast<Product>().FirstOrDefault();
                decimal quantity = Convert.ToDecimal(row["Quantity"]);
                decimal.TryParse(row["Size"].ToString(), out packSize);
                decimal box = (quantity / packSize);
                if (box >= 1)
                {
                    box = Math.Truncate(box);
                    row["Box"] = box;
                    row["Quantity"] = quantity - (box * packSize);
                }
                else
                {
                    row["Box"] = 0;
                }

            }
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

            // op.txtServiceMan.Text = employee.EmployeeName;
            op.Run();
            objmainReport.ShowDialog();

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


        private void SetPharmachyInforamation(ref string PharmacyName, ref string Address, ref Organization pharmacy)
        {
            pharmacy = DataAccessProxy.GetAllPharmacy().Cast<Organization>().ToList().FirstOrDefault();
            Branch brach = new BranchManager().GetBranchByID(MTBFConstants.AppConstants.BranchID);
            if (pharmacy != null)
            {
                PharmacyName = (brach != null) ? brach.BranchName : pharmacy.OrganizationName;
                Address = (brach != null) ? brach.Address + Environment.NewLine + "Phone :" + brach.Phone : pharmacy.Address + Environment.NewLine + "Phone: " + pharmacy.Phone + ", " + pharmacy.Fax;
            }
        }

        private void btnSalesHistory_Click(object sender, EventArgs e)
        {
            frmProductSalesHistory frm = new frmProductSalesHistory();
            frm.ShowDialog();
        }

        private void btnPrintChalan_Click(object sender, EventArgs e)
        {
            if (grvSales.Selected.Rows.Count > 0)
            {
                int salesID = Convert.ToInt32(grvSales.Selected.Rows[0].Cells["SalesID"].Value);
                PrintChalan(salesID);
            }
            else
            {
                MessageBoxHelper.ShowInformation("You need to select a sales order");
            }
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {

            if (grvSales.Selected.Rows.Count > 0)
            {
                int salesID = Convert.ToInt32(grvSales.Selected.Rows[0].Cells["SalesID"].Value);
                SalesOrder salesOrder = _lstSalesOrder.Where(s => s.SalesID == salesID).Cast<SalesOrder>().ToList().FirstOrDefault();
                if (salesOrder.CustomerID > 0)
                {
                    frmWholeSales frm = new frmWholeSales(true, salesOrder, _lstSalesOrderDetail);
                    frm.ShowDialog();
                }
                else
                {
                    MessageBoxHelper.ShowInformation("You can edit only credit sales.");
                }

            }
            else
            {
                MessageBoxHelper.ShowInformation("You need to select a sales order");
            }
        }

        private void frmViewSales_Load(object sender, EventArgs e)
        {
            base.CheckAction(this);
            LoadCustoemrTypeCombo();
            LoadSalesRepresentativeCombo();
            grvSales.DataSource = BuildSalesTable();
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

            cmbSalesRefresentative.DisplayMember = "Name";
            cmbSalesRefresentative.ValueMember = "SalesRepresentativeID";
            cmbSalesRefresentative.DataSource = table;
            cmbSalesRefresentative.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;

            cmbSalesRefresentative.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
            cmbSalesRefresentative.DisplayLayout.PerformAutoResizeColumns(false, PerformAutoSizeType.AllRowsInBand, true);
            cmbSalesRefresentative.DisplayLayout.Bands[0].Columns["SalesRepresentativeID"].Hidden = true;
        }

        private void LoadCustoemrTypeCombo()
        {
            Dictionary<string, int> list = new Dictionary<string, int>();
            list.Add(MTBFConstants.DataField.COMBO_DEFULT_TASK_TYPE, MTBFConstants.DataField.COMBO_DEFAULT_ID);

            foreach (int enumValue in Enum.GetValues(typeof(MTBFEnums.CustomerType)))
            {
                list.Add(Enum.GetName(typeof(MTBFEnums.CustomerType), enumValue), enumValue);
            }
            cmbCustomerType.DisplayMember = "Key";
            cmbCustomerType.ValueMember = "Value";
            cmbCustomerType.DataSource = list.ToList();
            cmbCustomerType.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
        }

        private void btSearchByMemoNumber_Click(object sender, EventArgs e)
        {
            _lstSalesOrder = new SalesManager().GetSalesOrderByBillNumber(txtMemoNumber.Text, MTBFConstants.AppConstants.BranchID).Cast<SalesOrder>().ToList();

            LoadDataInGrid(_lstSalesOrder);
        }

        private void btnTypeWiseReport_Click(object sender, EventArgs e)
        {
            // GetBranchAndProductTypeWiseSales(dtpFromDate.Value.ToString("yyyy-MM-dd"), dtpToDate.Value.ToString("yyyy-MM-dd"));
            GetBranchAndProductTypeWiseDetailSales(dtpFromDate.Value.ToString("yyyy-MM-dd"), dtpToDate.Value.ToString("yyyy-MM-dd"));

        }

        private void txtCustomerName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCustomerName.Text))
            {
                List<SalesOrder> lstFilteredSalesOrder = new List<SalesOrder>();
                lstFilteredSalesOrder = _lstSalesOrder.Where(s => s.CustomerName.ToUpper().StartsWith(txtCustomerName.Text.ToUpper())).ToList();
                LoadDataInGrid(lstFilteredSalesOrder);
            }
            else
            {
                LoadDataInGrid(_lstSalesOrder);
            }

        }

        private void txtPhoneNumber_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPhoneNumber.Text))
            {
                List<SalesOrder> lstFilteredSalesOrder = new List<SalesOrder>();
                lstFilteredSalesOrder = _lstSalesOrder.Where(s => s.CustomerPhone.ToUpper().StartsWith(txtPhoneNumber.Text)).ToList();
                LoadDataInGrid(lstFilteredSalesOrder);
            }
            else
            {
                LoadDataInGrid(_lstSalesOrder);
            }
        }

        private void grvSales_ClickCell(object sender, ClickCellEventArgs e)
        {
            if (grvSales.Selected.Rows.Count > 0)
            {
                int salesID = Convert.ToInt32(grvSales.Selected.Rows[0].Cells["SalesID"].Value);
                LoadSalesOrderDetail(salesID);
            }

        }



        private void btnExport_Click(object sender, EventArgs e)
        {

            ExportGridData();
        }

        private void ExportGridData()
        {
            Branch branch = new BranchManager().GetBranchByID(MTBFConstants.AppConstants.BranchID);
            try
            {
                if (grvSales.DataSource == null)
                {
                    MessageBoxHelper.ShowInformation("No information foud for export.");
                }
                else
                {
                    string fileLoacation = "";
                    FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                    DialogResult result = folderBrowser.ShowDialog();

                    try
                    {
                        if (result == DialogResult.OK)
                        {
                            string nameOfFile = cmbCustomerType.Text + " Sales Report Report " + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                            fileLoacation = string.Format("{0}\\{1}", folderBrowser.SelectedPath, nameOfFile);

                            // Check if file already exists. If yes, delete it. 
                            if (File.Exists(fileLoacation))
                            {
                                File.Delete(fileLoacation);
                            }

                            Workbook WB = new Workbook();
                            WB.Worksheets.Add("Sales Report");

                            // add Title
                            Worksheet sheet = WB.Worksheets["Sales Report"];
                            sheet.Rows[0].Cells[0].Value = (branch != null) ? branch.BranchName : string.Empty;
                            sheet.Rows[0].Cells[0].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;

                            sheet.Rows[1].Cells[0].Value = cmbCustomerType.Text + " Sales";
                            sheet.Rows[1].Cells[0].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;

                            sheet.Rows[2].Cells[0].Value = "From :";
                            sheet.Rows[2].Cells[0].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
                            sheet.Rows[2].Cells[1].Value = dtpFromDate.Value.ToString("dd MMM yyyy");
                            sheet.Rows[2].Cells[1].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
                            sheet.Rows[3].Cells[0].Value = "To :";
                            sheet.Rows[3].Cells[0].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
                            sheet.Rows[3].Cells[1].Value = dtpToDate.Value.ToString("dd MMM yyyy");
                            sheet.Rows[3].Cells[1].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;

                            ultraGridExcelExporter1.Export(grvSales, WB.Worksheets["Sales Report"], 6, 0);
                            int count = 8;
                            //decimal total = 0;
                            //foreach (UltraGridRow row in grvSales.Rows)
                            //{
                            //    count++;
                            //    total = total + (Convert.ToDecimal(row.Cells["Sales Amount"].Value) + Convert.ToDecimal(row.Cells["O. Charge"].Value) + Convert.ToDecimal(row.Cells["C. Charge"].Value) + Convert.ToDecimal(row.Cells["C.L.Charge"].Value) - Convert.ToDecimal(row.Cells["Discount"].Value));
                            //}
                            int total = Convert.ToInt32(lblTotalRecord.Text) + 8;
                            sheet.Rows[total].Cells[4].Value = "Total :";
                            sheet.Rows[total].Cells[4].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;

                            sheet.Rows[total].Cells[5].ApplyFormula("=sum(F8:" + "F" + total + ")");
                            sheet.Rows[total].Cells[5].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;


                            BIFF8Writer.WriteWorkbookToFile(WB, fileLoacation);
                            WB.Save(fileLoacation);

                            MessageBoxHelper.ShowInformation("Exported Successfully");
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBoxHelper.ShowInformation(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxHelper.ShowInformation(ex.Message);
            }
        }


        private void ExportTypeSummaryGridData()
        {
            Branch branch = new BranchManager().GetBranchByID(MTBFConstants.AppConstants.BranchID);
            try
            {
                if (grvSales.DataSource == null)
                {
                    MessageBoxHelper.ShowInformation("No information foud for export.");
                }
                else
                {
                    string fileLoacation = "";
                    FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                    DialogResult result = folderBrowser.ShowDialog();

                    try
                    {
                        if (result == DialogResult.OK)
                        {
                            string nameOfFile = "Customer Type wise sales.xls";
                            fileLoacation = string.Format("{0}\\{1}", folderBrowser.SelectedPath, nameOfFile);

                            // Check if file already exists. If yes, delete it. 
                            if (File.Exists(fileLoacation))
                            {
                                File.Delete(fileLoacation);
                            }

                            Workbook WB = new Workbook();
                            WB.Worksheets.Add("Customer Type Sales Report");

                            // add Title
                            Worksheet sheet = WB.Worksheets["Customer Type Sales Report"];
                            sheet.Rows[0].Cells[0].Value = (branch != null) ? branch.BranchName : string.Empty; // Title
                            sheet.Rows[0].Cells[0].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;

                            sheet.Rows[1].Cells[0].Value = "Customer Type wise Sales";
                            sheet.Rows[1].Cells[0].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;

                            sheet.Rows[2].Cells[0].Value = "From :";
                            sheet.Rows[2].Cells[0].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
                            sheet.Rows[2].Cells[1].Value = dtpFromDate.Value.ToString("dd MMM yyyy");
                            sheet.Rows[2].Cells[1].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
                            sheet.Rows[3].Cells[0].Value = "To :";
                            sheet.Rows[3].Cells[0].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
                            sheet.Rows[3].Cells[1].Value = dtpToDate.Value.ToString("dd MMM yyyy");
                            sheet.Rows[3].Cells[1].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;

                            ultraGridExcelExporter1.Export(grvExport, WB.Worksheets["Customer Type Sales Report"], 6, 0);

                            string[] Columns = new[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ", "BA", "BB", "BC", "BD", "BE", "BF", "BG", "BH" };



                            int netSalesIndex = grvExport.DisplayLayout.Bands[0].Columns["Net Sales"].Index;
                            string test = Columns[netSalesIndex];
                            int totalRowCount = grvExport.Rows.Count + 1 + 6;
                            sheet.Rows[totalRowCount].Cells[netSalesIndex - 1].Value = "Total :";
                            sheet.Rows[totalRowCount].Cells[netSalesIndex - 1].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
                            sheet.Rows[totalRowCount].Cells[netSalesIndex].ApplyFormula("=sum(" + test + "8:" + "" + test + "" + totalRowCount + ")");
                            sheet.Rows[totalRowCount].Cells[netSalesIndex].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;


                            BIFF8Writer.WriteWorkbookToFile(WB, fileLoacation);
                            WB.Save(fileLoacation);

                            MessageBoxHelper.ShowInformation("Exported Successfully");
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBoxHelper.ShowInformation(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxHelper.ShowInformation(ex.Message);
            }
        }

        private void btLoadDataBySalesRepresentative_Click(object sender, EventArgs e)
        {
            int salesRepresentativeID = 0;
            if (cmbSalesRefresentative.Value != null && Convert.ToInt32(cmbSalesRefresentative.Value) != MTBFConstants.DataField.COMBO_DEFAULT_ID)
            {
                int.TryParse(cmbSalesRefresentative.Value.ToString(), out salesRepresentativeID);
                string filter = string.Format("{0}={1}", "SalesRepresentativeID", salesRepresentativeID);
                _lstSalesOrder = new SalesManager().GetFilteredSalesInformation(filter).Cast<SalesOrder>().ToList();
                LoadDataInGrid(_lstSalesOrder);
            }
            else
            {
                _lstSalesOrder = new List<SalesOrder>();
                LoadDataInGrid(_lstSalesOrder);
                MessageBoxHelper.ShowInformation("Plese select sales representative.");
            }

        }

    }
}
