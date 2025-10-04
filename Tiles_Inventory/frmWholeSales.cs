
using System.Data;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using IFMS.Facade;
using IMFS.Common.DTO;
using System.Linq;
using IMFS.Common.Utility;
using IMFS.Common.View;
using Infragistics.Win.UltraWinGrid;
using NybSys.MTBF.Utility.Helper;
using System.IO;
using System.Drawing;
using Tiles_Inventory.Reports;
using NybSys.MTBF.Utility.Constants;
using NybSys.MTBF.Utility.Enums;
using IFMS.BLL;
using System.Diagnostics;


namespace Tiles_Inventory
{

    public partial class frmWholeSales : BaseForm
    {
        private string pharmacyName;
        private string pharmacyAddress;
        public delegate void DoWorkDelegate();
        List<ProductInformationForSales> lstProductInfo = new List<ProductInformationForSales>();
        private List<SalesOrderDetail> _lstSalesOrderDetail = new List<SalesOrderDetail>();
        List<Customer> lstCustomer = new List<Customer>();
        List<Inventroy> lstInventory = new List<Inventroy>();
        private VMSales _vmSales = new VMSales();

        //  int _purchaseID = 0;
        private bool isUpdate = false;
        private SalesOrder _salesOrder = new SalesOrder();
        System.Threading.Thread thread = null;

        public frmWholeSales()
        {
            thread = new System.Threading.Thread(() => AppUtil.ShowLoader());
            thread.Start();
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        public frmWholeSales(bool isEdit, SalesOrder salesOrder, List<SalesOrderDetail> lstSalesOrderDetail)
        {
            thread = new System.Threading.Thread(() => AppUtil.ShowLoader());
            thread.Start();
            base.IsUpdating = isEdit;
            _vmSales.SalesOrder = salesOrder;
            _vmSales.lstSalesOrderDetail = lstSalesOrderDetail;
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        private void WholeSales_Load(System.Object sender, System.EventArgs e)
        {
            LoadCustomerCombo();
            LoadSalesRepresentativeCombo();
            if (base.IsUpdating)
            {
                LoadExistingSalesInformation();
            }
            else
            {
                txtBillNumber.Text = new SalesManager().GetSaleMemoNumber().ToString();
                LoadProductInformation();
            }

            timer1.Start();
            thread.Abort();

        }



        private DataTable BuildCustomerTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("CustomerID");
            table.Columns.Add("Customer Name");
            table.Columns.Add("Address");
            table.Columns.Add("Phone");
            table.Columns.Add("Customer Type");
            return table;
        }

        private void LoadCustomerCombo()
        {

            lstCustomer = new CustomerManager().GetAllCustomerByBranchID(MTBFConstants.AppConstants.BranchID).ToList();
            DataTable table = BuildCustomerTable();

            foreach (Customer customer in lstCustomer)
            {
                DataRow row = table.NewRow();
                row["CustomerID"] = customer.CustomerID;
                row["Customer Name"] = customer.CustomerName;
                row["Address"] = customer.Address;
                row["Phone"] = customer.Phone;
                row["Customer Type"] = Enum.GetName(typeof(MTBFEnums.CustomerType), customer.CustomerType);
                table.Rows.Add(row);

            }

            cmbCustomerName.DisplayMember = "Customer Name";
            cmbCustomerName.ValueMember = "CustomerID";
            cmbCustomerName.DataSource = table;

            cmbCustomerName.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
            cmbCustomerName.DisplayLayout.PerformAutoResizeColumns(false, PerformAutoSizeType.AllRowsInBand, true);
            cmbCustomerName.DisplayLayout.Bands[0].Columns["CustomerID"].Hidden = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            LoadProductInformation();
        }

        public void DoWorkMethod()
        {

            //Do something
        }



        private void LoadExistingSalesInformation()
        {
            AddDataInGrid(_vmSales.lstSalesOrderDetail);
            cmbSalesRefresentative.Value = _vmSales.SalesOrder.SalesRepresentativeID;
            cmbCustomerName.Value = _vmSales.SalesOrder.CustomerID;
            txtTotal.Text = _vmSales.SalesOrder.SalesAmount.ToString();
            txtDiscount.Text = _vmSales.SalesOrder.Discount.ToString();
            txtTransportCost.Text = _vmSales.SalesOrder.CarryingLoading.ToString();
            txtReceiveAmount.Text = _vmSales.SalesOrder.ReceiveAmount.ToString();
            dtpSalesDate.Value = _vmSales.SalesOrder.SalesDate;
            txtBillNumber.Text = _vmSales.SalesOrder.BillNumber;
            txtShortNote.Text = _vmSales.SalesOrder.ShortNote;
        }

        /// <summary>
        /// Method to build sales detail table
        /// </summary>
        /// <returns></returns>
        private DataTable BuildSalesDetailTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ProductID");
            table.Columns.Add("ProductName");
            table.Columns.Add("Quantity");
            table.Columns.Add("SalesPrice");
            table.Columns.Add("VAT");
            table.Columns.Add("Total");
            table.Columns.Add("Delete");
            return table;
        }


        /// <summary>
        /// Method to load product information 
        /// </summary>
        private void LoadProductInformationWithLike(string productName)
        {

            lstProductInfo = DataAccessProxy.GetAllProductForSales(MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<ProductInformationForSales>().Where(p => p.ProductName.ToLower().StartsWith(productName.ToLower())).ToList();
            lstProductInfo = RemoveGridAddedProductQuantity(lstProductInfo);

            cmbProductInformation.DataSource = lstProductInfo;
            cmbProductInformation.DisplayMember = "ProductName";
            cmbProductInformation.ValueMember = "ProductId";
        }

        private List<ProductInformationForSales> RemoveGridAddedProductQuantity(List<ProductInformationForSales> lstProductInfo)
        {
            ProductInformationForSales product = null;
            foreach (UltraGridRow row in grvCreditProductDetails.Rows)
            {
                if (row.Cells["ProductID"].Value != null)
                {
                    string productID = row.Cells["ProductID"].Value.ToString();
                    decimal quantity = Convert.ToDecimal(row.Cells["Quantity"].Value);

                    int listCount = lstProductInfo.Count;


                    for (int j = 0; j < listCount; j++)
                    {
                        product = lstProductInfo[j];
                        if (productID == product.ProductID)
                        {
                            if (product.Quantity > quantity)
                            {
                                lstProductInfo.RemoveAt(j);
                                product.Quantity = product.Quantity - quantity;
                                lstProductInfo.Add(product);
                                break;
                            }
                            else if (product.Quantity <= quantity)
                            {
                                lstProductInfo.RemoveAt(j);
                                break;
                            }
                        }
                    }
                }

            }
            return lstProductInfo;
        }

        /// <summary>
        /// Method to check valid data.
        /// </summary>
        /// <returns></returns>
        private bool ValidFormData()
        {


            if (txtPhone.Text == string.Empty)
            {
                MessageBoxHelper.ShowInformation("Required field can't be blank.");
                txtPhone.Focus();
                return false;
            }

            if (cmbCustomerName.Value == null)
            {
                MessageBoxHelper.ShowInformation("You need to select customer name.");
                cmbCustomerName.Focus();
                return false;
            }



            if ((cmbProductInformation.Value == null))
            {
                MessageBoxHelper.ShowInformation("Required field can't be blank.");
                cmbProductInformation.Focus();
                return false;
            }
            decimal quantity = 0;
            decimal.TryParse(txtQuantity.Text, out quantity);

            if (quantity == 0)
            {
                MessageBoxHelper.ShowInformation("Required field can't be blank.");

                txtQuantity.Focus();
                return false;
            }

            return true;
        }



        private void AddDataInGrid(List<SalesOrderDetail> lstSalesOrderDetail)
        {
            DataTable dt = BuildSalesDetailTable();

            foreach (SalesOrderDetail sDetail in lstSalesOrderDetail)
            {
                DataRow row = dt.NewRow();
                row["ProductID"] = sDetail.ProductID;
                row["ProductName"] = sDetail.ProductName;
                row["Quantity"] = sDetail.Quantity;
                row["SalesPrice"] = sDetail.Price;
                row["VAT"] = sDetail.VAT;
                row["Total"] = (sDetail.SquareFeet > 0) ? (sDetail.Price * sDetail.SquareFeet).ToString("F2") : (sDetail.Price * sDetail.Quantity).ToString("F2");
                row["Delete"] = "Delete";
                dt.Rows.Add(row);
            }
            grvCreditProductDetails.DataSource = dt;
            grvCreditProductDetails.DisplayLayout.Bands[0].Columns["ProductID"].Hidden = true;
            grvCreditProductDetails.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
            grvCreditProductDetails.DisplayLayout.PerformAutoResizeColumns(false, PerformAutoSizeType.AllRowsInBand, true);
            CalculateTotalCost();
        }

        /// <summary>
        /// Method to add product information in grid.
        /// </summary>
        private void AddProductInGrid()
        {
            int Quantity = 0;
            decimal price = default(decimal);
            decimal vat = 0;
            int rowIndex = 0;
            decimal total = 0;
            decimal previousSquareFeet = 0;
            decimal squareFeet = 0;

            if ((cmbProductInformation.Value != null))
            {

                ProductInformationForSales product = lstProductInfo.Where(p => p.ProductID == cmbProductInformation.Value.ToString()).FirstOrDefault();
                if (product != null)
                {
                    price = DataAccessProxy.GetSalesPriceByProductID(cmbProductInformation.Value.ToString());

                    Quantity = Convert.ToInt32(txtQuantity.Text);
                    decimal.TryParse(txtSqureFeet.Text, out squareFeet);



                    if (IsExistingProduct(product, out rowIndex))
                    {
                        decimal previousQuantity = 0;
                        decimal.TryParse(grvCreditProductDetails.Rows[rowIndex].Cells["Quantity"].Value.ToString(), out previousQuantity);

                        decimal.TryParse(grvCreditProductDetails.Rows[rowIndex].Cells["SquareFeet"].Value.ToString(), out previousSquareFeet);
                        // vat = (price * (Quantity + previousQuantity)) / 100 * product.VAT;
                        grvCreditProductDetails.Rows[rowIndex].Cells["Quantity"].Value = previousQuantity + Quantity;
                        grvCreditProductDetails.Rows[rowIndex].Cells["SquareFeet"].Value = previousSquareFeet + squareFeet;
                        grvCreditProductDetails.Rows[rowIndex].Cells["VAT"].Value = vat.ToString("F2");
                        total = ((price * (previousQuantity + 1)) + vat);
                        grvCreditProductDetails.Rows[rowIndex].Cells["Total"].Value = total.ToString("F2");

                    }
                    else
                    {
                        UltraGridRow row = grvCreditProductDetails.DisplayLayout.Bands[0].AddNew();

                        //  vat = (price * Quantity) / 100 * product.VAT;
                        row.Cells["ProductID"].Value = product.ProductID;
                        row.Cells["ProductName"].Value = product.ProductName;
                        row.Cells["Quantity"].Value = Quantity.ToString();
                        row.Cells["SquareFeet"].Value = squareFeet;
                        row.Cells["SalesPrice"].Value = price.ToString();
                        row.Cells["VAT"].Value = vat.ToString("F2");
                        total = ((price * Quantity) + vat);
                        row.Cells["Total"].Value = total.ToString("F2");
                        row.Cells["Delete"].Value = "Delete";
                    }



                    grvCreditProductDetails.DisplayLayout.Bands[0].Columns["ProductID"].Hidden = true;
                    cmbProductInformation.Text = string.Empty;

                    cmbProductInformation.Focus();
                    txtQuantity.Text = 0.ToString().ToString();
                }
                else
                {
                    MessageBoxHelper.ShowInformation("Invalid product selection.");
                    cmbProductInformation.Focus();
                    return;
                }

            }

            CalculateTotalCost();
            grvCreditProductDetails.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
            grvCreditProductDetails.DisplayLayout.PerformAutoResizeColumns(false, PerformAutoSizeType.AllRowsInBand, true);

        }

        private bool IsExistingProduct(ProductInformationForSales product, out int rowIndex)
        {
            rowIndex = 0;
            foreach (UltraGridRow row in grvCreditProductDetails.Rows)
            {
                if (row.Cells["ProductName"].Value != null)
                {
                    if (row.Cells["ProductName"].Value.ToString().Trim().ToLower() == product.ProductName.Trim().ToLower())
                    {
                        rowIndex = row.Index;
                        return true;
                    }
                }

            }
            return false;
        }

        /// <summary>
        /// Method to insert sales order information.
        /// </summary>
        /// <param name="salesId"></param>
        /// <returns></returns>
        private int InsertSalesOrderDetailInformation(int salesId)
        {
            int result = 0;
            string productID = string.Empty;
            decimal quantity = 0;
            decimal squareFeet = 0;
            decimal price = default(decimal);
            List<SalesOrderDetail> lstSalesOrderDetail = new List<SalesOrderDetail>();

            foreach (UltraGridRow row in grvCreditProductDetails.Rows)
            {
                SalesOrderDetail salesOrderDetail = new SalesOrderDetail();
                // _purchaseID = Convert.ToInt32(row.Cells["PurchaseID"].Value);
                productID = row.Cells["ProductID"].Value.ToString();
                //  quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                price = Convert.ToDecimal(row.Cells["SalesPrice"].Value);

                decimal.TryParse(row.Cells["Quantity"].Text, out quantity);
                decimal.TryParse(row.Cells["SquareFeet"].Value.ToString(), out squareFeet);
                salesOrderDetail.ProductID = productID;
                salesOrderDetail.SalesID = salesId;
                salesOrderDetail.Price = price;
                salesOrderDetail.Quantity = quantity;
                salesOrderDetail.SquareFeet = squareFeet;
                salesOrderDetail.ProductName = row.Cells["ProductName"].Text;
                lstSalesOrderDetail.Add(salesOrderDetail);
            }

            DataAccessProxy.InsertSalesOrderDetail(lstSalesOrderDetail);
            result = 1;
            return result;
        }

        /// <summary>
        /// Method to insert sales order details information.
        /// </summary>
        /// <param name="salesAmount"></param>
        /// <param name="customerID"></param>
        /// <param name="discount"></param>
        /// <param name="receiveAmount"></param>
        private int InsertSalesOrderInformation(decimal salesAmount, int customerID, decimal discount, decimal receiveAmount)
        {
            decimal carryingAndLoading = 0;
            decimal cuttingCharge = 0;
            decimal otherCharge = 0;
            decimal.TryParse(txtTransportCost.Text, out carryingAndLoading);


            SalesOrder salesOrder = new SalesOrder();

            salesOrder.SalesDate = dtpSalesDate.Value;
            salesOrder.SalesAmount = salesAmount;
            salesOrder.EmployeeID = IFMSConstant.LoggedinUserID;
            salesOrder.CustomerID = customerID;
            salesOrder.CustomerName = cmbCustomerName.Text;
            salesOrder.CustomerPhone = txtPhone.Text;
            salesOrder.Discount = discount;
            salesOrder.BillNumber = txtBillNumber.Text;
            salesOrder.BranchID = MTBFConstants.AppConstants.BranchID;
            salesOrder.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            salesOrder.ReceiveAmount = receiveAmount;
            salesOrder.CarryingLoading = carryingAndLoading;
            salesOrder.Status = (int)MTBFEnums.DeliveryStatus.NotDelivered;
            return DataAccessProxy.InsertSalesOrderDetail(salesOrder);
        }


        private int UpdateSalesOrderInformation(decimal salesAmount, int customerID, decimal discount, decimal receiveAmount)
        {

            decimal carryingAndLoading = 0;

            decimal.TryParse(txtTransportCost.Text, out carryingAndLoading);


            _salesOrder.SalesDate = dtpSalesDate.Value;
            _salesOrder.SalesAmount = salesAmount;
            _salesOrder.EmployeeID = IFMSConstant.LoggedinUserID;
            _salesOrder.CustomerID = customerID;
            _salesOrder.CustomerName = cmbCustomerName.Text;
            _salesOrder.CustomerPhone = txtPhone.Text;
            _salesOrder.BillNumber = txtBillNumber.Text;
            _salesOrder.Discount = discount;
            _salesOrder.BranchID = MTBFConstants.AppConstants.BranchID;
            _salesOrder.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            _salesOrder.ReceiveAmount = receiveAmount;
            _salesOrder.CarryingLoading = carryingAndLoading;
            return DataAccessProxy.UpdateSalesOrder(_salesOrder);
        }






        private void PrintInvoice(int salesId)
        {

            Employee employee = new Employee();
            DataTable salesReport = new DataTable();
            Organization pharmacy = new Organization();
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
                row["Total"] = (quantity == 0) ? (qty * price).ToString("f2") : (quantity * price).ToString("f2");

                row["ProductName"] = row["ProductName"].ToString();

                salesAmount = salesAmount + (quantity * price);
            }

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
                    amountWord = amountWord + " " + "Taka.";
                }
                else
                {
                    amountWord = amountWord + " " + "Taka and " + amountInCent + " poysha";
                }
            }
            else
            {
                amountWord = UIHelper.NumberToWords(Convert.ToInt32(salesAmount));
                amountWord = amountWord + " " + "Taka.";
            }

            int customerID = 0;
            if (salesReport.Rows.Count > 0)
            {
                int.TryParse(salesReport.Rows[0]["CustomerID"].ToString(), out customerID);
            }

            decimal dueAmount = default(decimal);
            dueAmount = DataAccessProxy.GetCustomerDueAmountByCustomerID(customerID, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID);

            rptCreditSalesShowRoom op = new rptCreditSalesShowRoom();
            frmSalesReport objmainReport = new frmSalesReport();
            op.DataSource = salesReport;
            objmainReport.rptViewer.Document = op.Document;
            objmainReport.rptViewer.Dock = DockStyle.Fill;
            SetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress, ref pharmacy);
            op.txtAddress.Text = pharmacyAddress;
            op.txtTotalDue.Text = dueAmount.ToString();
            op.txtServiceMan.Text = salesReport.Rows[0]["EmployeeName"].ToString();
            op.Run();
            objmainReport.ShowDialog();
        }

        private void SetPharmachyInforamation(ref string PharmacyName, ref string Address, ref Organization pharmacy)
        {
            pharmacy = DataAccessProxy.GetAllPharmacy().Cast<Organization>().ToList().FirstOrDefault();
            Branch brach = new BranchManager().GetBranchByID(MTBFConstants.AppConstants.BranchID);
            if (pharmacy != null)
            {
                PharmacyName = (brach != null) ? brach.BranchName : pharmacy.OrganizationName;
                Address = (brach != null) ? brach.Address + " " + brach.Phone : pharmacy.Address + ", " + pharmacy.Phone + ", " + pharmacy.Fax;
            }
        }

        /// <summary>
        /// Method to calculate total cost.
        /// </summary>
        private void CalculateTotalCost()
        {
            decimal sum = 0;
            decimal discount = 0;
            decimal carryingAndLoading = 0;
            foreach (SalesOrderDetail sDetail in _vmSales.lstSalesOrderDetail)
            {
                sum += (sDetail.SquareFeet > 0) ? sDetail.Price * sDetail.SquareFeet : sDetail.Price * sDetail.Quantity;
            }

            decimal.TryParse(txtDiscount.Text, out discount);
            decimal.TryParse(txtTransportCost.Text, out carryingAndLoading);
            txtTotal.Text = Math.Floor(sum).ToString();
            txtReceiveAmount.Text = 0.ToString().ToString();
            txtChange.Text = 0.ToString().ToString();
            txtGrandTotal.Text = Math.Floor((discount > 0) ? ((sum + carryingAndLoading) - discount) : (sum + carryingAndLoading)).ToString();
        }

        /// <summary>
        /// Method to check save data.
        /// </summary>
        /// <returns></returns>
        private bool ValidFormSaveData()
        {

            if (txtPhone.Text == string.Empty)
            {
                MessageBoxHelper.ShowInformation("Required field can't be blank.");
                txtPhone.Focus();
                return false;
            }

            if (cmbCustomerName.Value == null)
            {
                MessageBoxHelper.ShowInformation("You need to select customer name.");
                cmbCustomerName.Focus();
                return false;
            }


            return true;
        }

        /// <summary>
        /// Method to insert sales information.
        /// </summary>
        /// <returns></returns>
        private int InsertSalesInformation()
        {
            int result = 0;

            decimal salesAmount = 0;
            decimal discount = 0;
            decimal receiveAmount = 0;

            int salesID = 0;

            try
            {
                if (ValidFormSaveData())
                {
                    salesAmount = Convert.ToDecimal(txtTotal.Text);
                    discount = txtDiscount.Text == string.Empty ? 0 : Convert.ToDecimal(txtDiscount.Text);

                    decimal.TryParse(txtReceiveAmount.Text, out receiveAmount);
                    decimal grandTotal = 0;
                    decimal.TryParse(txtGrandTotal.Text, out grandTotal);


                    receiveAmount = (receiveAmount > grandTotal) ? grandTotal : receiveAmount;
                    // receiveAmount = txtReceiveAmount.Text == string.Empty ? 0 : Convert.ToDecimal(txtReceiveAmount.Text);

                    DataAccessProxy.BegainTransaction();
                    int customerID = Convert.ToInt32(cmbCustomerName.Value);

                    ChildAccount childAccount = DataAccessProxy.GetChildAccountByCustomerID(customerID);
                    if (childAccount != null)
                    {
                        salesID = InsertSalesOrderInformation(salesAmount, customerID, discount, receiveAmount);
                        InsertSalesOrderDetailInformation(salesID);
                        InsertJournalInformationForCreditSales(salesID);
                        InsertJournalInformationForGoodsDelivery(salesID);
                        if (receiveAmount > 0)
                        {
                            InsertJournalInformationForCashReceive(salesID);
                        }
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("There is no child account for this customer.");

                    }


                    DataAccessProxy.CommitTransaction();
                    result = salesID;
                }
            }
            catch (Exception ex)
            {
                DataAccessProxy.Rollback();
            }
            finally
            {
                DataAccessProxy.EndTransaction();
            }
            return result;
        }

        private int InsertSalesInformationNew()
        {
            int result = 0;
            Customer customer = new Customer();
            decimal salesAmount = 0;
            decimal discount = 0;
            decimal receiveAmount = 0;

            int salesID = 0;


            salesAmount = Convert.ToDecimal(txtTotal.Text);
            discount = txtDiscount.Text == string.Empty ? 0 : Convert.ToDecimal(txtDiscount.Text);

            decimal.TryParse(txtReceiveAmount.Text, out receiveAmount);
            decimal grandTotal = 0;
            decimal.TryParse(txtGrandTotal.Text, out grandTotal);

            receiveAmount = (receiveAmount > grandTotal) ? grandTotal : receiveAmount;
            customer = lstCustomer.Where(c => c.CustomerID == Convert.ToInt32(cmbCustomerName.Value)).FirstOrDefault();

            salesID = InsertSalesOrderInformation(salesAmount, customer, discount, receiveAmount);

            result = salesID;
            return result;
        }


        private int InsertSalesOrderInformation(decimal salesAmount, Customer customer, decimal discount, decimal receiveAmount)
        {
            decimal carryingAndLoading = 0;
            int salesRepresentativeID = 0;
            if (cmbSalesRefresentative.Value != null)
            {
                int.TryParse(cmbSalesRefresentative.Value.ToString(), out salesRepresentativeID);
            }

            decimal.TryParse(txtTransportCost.Text, out carryingAndLoading);


            _vmSales.SalesOrder.BillNumber = txtBillNumber.Text;
            _vmSales.SalesOrder.SalesDate = dtpSalesDate.Value;
            _vmSales.SalesOrder.SalesAmount = salesAmount;
            _vmSales.SalesOrder.EmployeeID = IFMSConstant.LoggedinUserID;
            _vmSales.SalesOrder.CustomerID = customer.CustomerID;
            _vmSales.SalesOrder.CustomerName = customer.CustomerName;
            _vmSales.SalesOrder.CustomerPhone = customer.Phone;
            _vmSales.SalesOrder.Discount = discount;
            _vmSales.SalesOrder.ShortNote = txtShortNote.Text;
            _vmSales.SalesOrder.BranchID = MTBFConstants.AppConstants.BranchID;
            _vmSales.SalesOrder.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            _vmSales.SalesOrder.ReceiveAmount = receiveAmount;
            _vmSales.SalesOrder.CarryingLoading = carryingAndLoading;
            _vmSales.SalesOrder.SalesRepresentativeID = salesRepresentativeID;
            _vmSales.SalesOrder.Status = (_vmSales.SalesOrder.Status != (int)MTBFEnums.DeliveryStatus.Delivered) ? (int)MTBFEnums.DeliveryStatus.NotDelivered : _vmSales.SalesOrder.Status;

            return new SalesManager().SaveSalesInformation(_vmSales);

            // return DataAccessProxy.InsertSalesOrderDetail(salesOrder);
        }


        private int UpdateSalesOrderInformation(decimal salesAmount, Customer customer, decimal discount, decimal receiveAmount)
        {
            decimal carryingAndLoading = 0;
            decimal.TryParse(txtTransportCost.Text, out carryingAndLoading);
            _salesOrder.BillNumber = txtBillNumber.Text;
            _salesOrder.SalesDate = dtpSalesDate.Value;
            _salesOrder.SalesAmount = salesAmount;
            _salesOrder.EmployeeID = IFMSConstant.LoggedinUserID;
            _salesOrder.CustomerID = customer.CustomerID;
            _salesOrder.CustomerName = customer.CustomerName;
            _salesOrder.CustomerPhone = customer.Phone;
            _salesOrder.Discount = discount;
            _salesOrder.BranchID = MTBFConstants.AppConstants.BranchID;
            _salesOrder.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            _salesOrder.ReceiveAmount = receiveAmount;
            _salesOrder.CarryingLoading = carryingAndLoading;
            return DataAccessProxy.UpdateSalesOrder(_salesOrder);
        }

        private int UpdateSalesOrderInformation()
        {
            int result = 0;
            Customer customer = new Customer();
            decimal salesAmount = 0;
            decimal discount = 0;
            decimal receiveAmount = 0;

            try
            {
                if (ValidFormSaveData())
                {
                    salesAmount = Convert.ToDecimal(txtTotal.Text);
                    discount = txtDiscount.Text == string.Empty ? 0 : Convert.ToDecimal(txtDiscount.Text);

                    decimal.TryParse(txtReceiveAmount.Text, out receiveAmount);
                    decimal grandTotal = 0;
                    decimal.TryParse(txtGrandTotal.Text, out grandTotal);
                    receiveAmount = (receiveAmount > grandTotal) ? grandTotal : receiveAmount;


                    DataAccessProxy.BegainTransaction();
                    int customerID = Convert.ToInt32(cmbCustomerName.Value);

                    ChildAccount childAccount = DataAccessProxy.GetChildAccountByCustomerID(customerID);
                    if (childAccount != null)
                    {
                        UpdateSalesOrderInformation(salesAmount, customerID, discount, receiveAmount);

                        DeleteAllJournalInformationForSales(_salesOrder.SalesID);
                        DeleteSalesOrderDetail(_salesOrder.SalesID);

                        InsertSalesOrderDetailInformation(_salesOrder.SalesID);

                        InsertJournalInformationForCreditSales(_salesOrder.SalesID);
                        InsertJournalInformationForGoodsDelivery(_salesOrder.SalesID);

                        if (receiveAmount > 0)
                        {
                            InsertJournalInformationForCashReceive(_salesOrder.SalesID);
                        }
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("There is no child account for this supplier.");

                    }


                    DataAccessProxy.CommitTransaction();
                    result = _salesOrder.SalesID;
                }
            }
            catch (Exception ex)
            {
                DataAccessProxy.Rollback();
            }
            finally
            {
                DataAccessProxy.EndTransaction();
            }
            return result;

        }

        private void DeleteSalesOrderDetail(int _salesID)
        {
            foreach (SalesOrderDetail orderDetail in DataAccessProxy.GetAllSalesDetailBySalesID(_salesID))
            {
                DataAccessProxy.DeleteSalesDetail(orderDetail);
            }
        }

        private void DeleteAllJournalInformationForSales(int _salesID)
        {
            foreach (Journal journal in DataAccessProxy.GetAllJournalBySalesID(_salesID))
            {
                DataAccessProxy.DeleteJournal(journal);
            }
        }



        /// <summary>
        /// Method to insert journal information for credit sales.
        /// </summary>
        /// <returns></returns>
        public int InsertJournalInformationForCreditSales(int salesID)
        {
            int result = 0;
            int referenceID = 1;
            decimal amount = Convert.ToDecimal(txtTotal.Text);

            referenceID = DataAccessProxy.GetJournalReferenceID();

            Customer customer = DataAccessProxy.GetCustomerByPhoneNumber(txtPhone.Text, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID);

            for (int i = 0; i <= 1; i++)
            {
                IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();
                ChildAccount childAccount = DataAccessProxy.GetChildAccountByCustomerID(customer.CustomerID);

                if (i == 0)
                {
                    journal.DrCrIndecator = "Dr";
                    journal.AccountID = childAccount.AccountID;
                    journal.ChildAccountID = childAccount.ChildAccountID;
                }
                else
                {
                    journal.DrCrIndecator = "Cr";
                    journal.AccountID = IFMSConstant.GoodsSalesAccountID;
                }

                journal.SalesID = salesID;
                journal.JAccountID = 0;
                journal.Amount = amount;
                journal.ReferenceNo = referenceID;
                journal.BranchID = MTBFConstants.AppConstants.BranchID;
                journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                result = DataAccessProxy.InsertJournal(journal);
            }

            return result;
        }

        /// <summary>
        /// Method to calculate purchase price of product.
        /// </summary>
        /// <returns></returns>
        private decimal CalculatePurchasePrice()
        {
            string productID = string.Empty;
            int purchaseID = 0;
            decimal squareFeet = 0;

            decimal purchasePrice = 0;
            foreach (UltraGridRow row in grvCreditProductDetails.Rows)
            { // purchaseID = Convert.ToInt32(row.Cells["PurchaseID"].Value);
                productID = row.Cells["ProductID"].Value.ToString();
                squareFeet = Convert.ToDecimal(row.Cells["SquareFeet"].Value);

                purchasePrice = purchasePrice + (CalculatePriceByProductID(productID) * squareFeet);

            }
            return purchasePrice;
        }


        private decimal CalculatePriceByProductID(string productID)
        {
            decimal purchasePrice = 0;
            decimal totalSquareFeet = 0;
            decimal totalPrice = 0;
            foreach (PurchaseOrderDetail orderDetail in DataAccessProxy.GetPurchaseOrderDetailByProductID(productID, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID))
            {
                totalSquareFeet = totalSquareFeet + orderDetail.SquareFeet;
                totalPrice = totalPrice + (orderDetail.PurchasePrice * orderDetail.SquareFeet);
            }

            purchasePrice = (totalPrice > 0) ? totalPrice / totalSquareFeet : 0;
            return purchasePrice;

        }


        /// <summary>
        /// Method to insert journal information for credit sales.
        /// </summary>
        /// <returns></returns>
        public int InsertJournalInformationForGoodsDelivery(int salesID)
        {
            int result = 0;
            int referenceID = 1;
            decimal amount = CalculatePurchasePrice();

            referenceID = DataAccessProxy.GetJournalReferenceID();
            for (int i = 0; i <= 1; i++)
            {
                IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();

                if (i == 0)
                {
                    journal.DrCrIndecator = "Dr";
                    journal.AccountID = IFMSConstant.CostOfGoodsSoldAccountID;
                }
                else
                {
                    journal.DrCrIndecator = "Cr";
                    journal.AccountID = IFMSConstant.InventoryAccountID;
                }

                journal.SalesID = salesID;
                journal.JAccountID = 0;
                journal.Amount = amount;
                journal.ReferenceNo = referenceID;
                journal.BranchID = MTBFConstants.AppConstants.BranchID;
                journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                result = DataAccessProxy.InsertJournal(journal);
            }

            return result;
        }

        /// <summary>
        /// Method to insert journal information after cash receive
        /// </summary>
        /// <returns></returns>
        public int InsertJournalInformationForCashReceive(int salesID)
        {
            int result = 0;
            int referenceID = 1;
            decimal amount = Convert.ToDecimal(txtReceiveAmount.Text);

            referenceID = DataAccessProxy.GetJournalReferenceID();
            Customer customer = DataAccessProxy.GetCustomerByPhoneNumber(txtPhone.Text, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID);

            for (int i = 0; i <= 1; i++)
            {
                IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();
                ChildAccount childAccount = DataAccessProxy.GetChildAccountByCustomerID(customer.CustomerID);

                if (i == 0)
                {
                    journal.DrCrIndecator = "Dr";
                    journal.AccountID = IFMSConstant.CashAccountID;
                }
                else
                {
                    journal.DrCrIndecator = "Cr";
                    journal.AccountID = childAccount.AccountID;
                    journal.ChildAccountID = childAccount.ChildAccountID;
                }

                journal.SalesID = salesID;
                journal.JAccountID = 0;
                journal.Amount = amount;
                journal.ReferenceNo = referenceID;
                journal.BranchID = MTBFConstants.AppConstants.BranchID;
                journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                result = DataAccessProxy.InsertJournal(journal);
            }

            return result;
        }

        /// <summary>
        /// Method to calculate total purchased product. 
        /// </summary>
        /// <param name="lstpurchaseOrderDetail"></param>
        /// <returns></returns>
        private decimal CalculatePurchaseQuantity(List<PurchaseOrderDetail> lstpurchaseOrderDetail)
        {
            decimal purchaseQuantity = 0;
            foreach (PurchaseOrderDetail product in lstpurchaseOrderDetail)
            {
                purchaseQuantity = purchaseQuantity + product.Quantity;
            }
            return purchaseQuantity;
        }


        private void LoadProductInformationWithoutGridAdded()
        {
            List<ProductInformationForSales> lstProductInfoForSales = new List<ProductInformationForSales>();
            // lstProductInfo = new ProductManager().GetAllProductForSalesNew().Cast<ProductInformationForSales>().Where(p => p.InventoryID == MTBFConstants.AppConstants.InventoryID).ToList();


            lstProductInfoForSales = RemoveGridAddedProductQuantity(lstProductInfo);
            cmbProductInformation.DataSource = lstProductInfoForSales;

            cmbProductInformation.DisplayMember = "ProductName";
            cmbProductInformation.ValueMember = "ProductId";
            cmbProductInformation.DisplayLayout.Bands[0].Columns["ProductID"].Hidden = true;
            cmbProductInformation.DisplayLayout.Bands[0].Columns["BranchID"].Hidden = true;
            cmbProductInformation.DisplayLayout.Bands[0].Columns["OrganizationID"].Hidden = true;
            cmbProductInformation.DisplayLayout.Bands[0].Columns["SalesPrice"].Hidden = true;
            cmbProductInformation.DisplayLayout.Bands[0].Columns["InventoryID"].Hidden = true;
            cmbProductInformation.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
            cmbProductInformation.DisplayLayout.PerformAutoResizeColumns(false, PerformAutoSizeType.AllRowsInBand, true);
        }

        private void LoadProductInformation()
        {
            lstProductInfo = new ProductManager().GetAllProductForSales(MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<ProductInformationForSales>().ToList();
            cmbProductInformation.DataSource = lstProductInfo;

            cmbProductInformation.DisplayMember = "ProductName";
            cmbProductInformation.ValueMember = "ProductId";
            cmbProductInformation.DisplayLayout.Bands[0].Columns["ProductID"].Hidden = true;
            cmbProductInformation.DisplayLayout.Bands[0].Columns["BranchID"].Hidden = true;
            cmbProductInformation.DisplayLayout.Bands[0].Columns["OrganizationID"].Hidden = true;
            cmbProductInformation.DisplayLayout.Bands[0].Columns["SalesPrice"].Hidden = true;
            cmbProductInformation.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
            cmbProductInformation.DisplayLayout.PerformAutoResizeColumns(false, PerformAutoSizeType.AllRowsInBand, true);
        }


        private List<ProductInformationForSales> RemoveTransferProduct(List<ProductInformationForSales> lstProductInfo)
        {
            List<ProductInformationForSales> lstFilteredProductInfo = new List<ProductInformationForSales>();
            foreach (ProductInformationForSales productifo in lstProductInfo)
            {
                decimal transferProduct = DataAccessProxy.GetAllTransferProductByProudctCode(productifo.ProductID, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID);
                productifo.Quantity = (productifo.Quantity - transferProduct);
                lstFilteredProductInfo.Add(productifo);
            }

            return lstFilteredProductInfo;
        }

        /// <summary>
        /// Method to remove damage product.
        /// </summary>
        /// <param name="lstProductInfo"></param>
        /// <returns></returns>
        private List<ProductInformationForSales> RemoveDamageProduct(List<ProductInformationForSales> lstProductInfo)
        {
            List<ProductInformationForSales> lstFilteredProductInfo = new List<ProductInformationForSales>();
            foreach (ProductInformationForSales productifo in lstProductInfo)
            {
                decimal transferProduct = DataAccessProxy.GetAllDamageProductByProudctCode(productifo.ProductID, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID);
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
        private List<ProductInformationForSales> RemoveAllGivenSample(List<ProductInformationForSales> lstProductInfo)
        {

            List<ProductInformationForSales> lstFilteredProductInfo = new List<ProductInformationForSales>();

            foreach (ProductInformationForSales productifo in lstProductInfo)
            {
                decimal givenSample = DataAccessProxy.GetAllGivenSampleByProductID(productifo.ProductID, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID);
                productifo.Quantity = (productifo.Quantity - givenSample);
                lstFilteredProductInfo.Add(productifo);

            }
            return lstFilteredProductInfo;

        }


        /// <summary>
        /// Method to calculate total sold product.
        /// </summary>
        /// <param name="lstSalesOrderDetail"></param>
        /// <returns></returns>
        private decimal CalculateSalesQuantity(List<SalesOrderDetail> lstSalesOrderDetail)
        {
            decimal salesQuantity = 0;
            foreach (SalesOrderDetail product in lstSalesOrderDetail)
            {
                salesQuantity = salesQuantity + product.Quantity;
            }
            return salesQuantity;
        }

        /// <summary>
        /// Method to check valid quantity.
        /// </summary>
        /// <returns></returns>
        private bool CheckValidOrderQuantity()
        {
            List<PurchaseOrderDetail> lstpurchaseOrderDetail = new List<PurchaseOrderDetail>();
            List<SalesOrderDetail> lstSalesOrderDetail = new List<SalesOrderDetail>();

            lstpurchaseOrderDetail = DataAccessProxy.GetPurchaseOrderDetailByProductID(cmbProductInformation.Value.ToString(), MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<PurchaseOrderDetail>().ToList();
            lstSalesOrderDetail = DataAccessProxy.GetSalesOrderDetailByProductID(cmbProductInformation.Value.ToString(), MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<SalesOrderDetail>().ToList();

            decimal purchaseQuantity = CalculatePurchaseQuantity(lstpurchaseOrderDetail);
            decimal salesQuantity = CalculateSalesQuantity(lstSalesOrderDetail);
            decimal orderQuantity = Convert.ToDecimal(txtQuantity.Text);

            if (orderQuantity > (purchaseQuantity - salesQuantity))
            {
                MessageBoxHelper.ShowInformation("There is no available product for this product please check.");
                txtQuantity.Focus();
                return false;
            }
            return true;
        }

        public void WholeSales()
        {
            Load += WholeSales_Load;
        }



        private void cmbProductInformation_TextChanged(object sender, EventArgs e)
        {
            //  LoadProductInformationWithLike(cmbProductInformation.Text);
        }

        private void cmbProductInformation_KeyUp(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 13 && cmbProductInformation.Text != string.Empty)
            {
                txtQuantity.Focus();
            }
        }

        private void UltraQuantity_KeyUp(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 13)
            {
                if (ValidFormData())
                {
                    decimal squareFeet = 0;
                    decimal Quantity = 0;
                    decimal price = default(decimal);
                    price = DataAccessProxy.GetSalesPriceByProductID(cmbProductInformation.Value.ToString());
                    ProductInformationForSales product = lstProductInfo.Where(p => p.ProductID == cmbProductInformation.Value.ToString()).FirstOrDefault();

                    decimal.TryParse(txtQuantity.Text, out Quantity);
                    decimal.TryParse(txtSqureFeet.Text, out squareFeet);


                    SalesOrderDetail sOrderDetail = new SalesOrderDetail();
                    sOrderDetail.ProductID = product.ProductID;
                    sOrderDetail.ProductName = product.ProductName;
                    sOrderDetail.Quantity = Quantity;
                    sOrderDetail.SquareFeet = squareFeet;
                    sOrderDetail.Price = price;
                    sOrderDetail.VAT = 0;
                    _vmSales.lstSalesOrderDetail.Add(sOrderDetail);

                    //SalesOrderDetail exDetail = _vmSales.lstSalesOrderDetail.Where(p => p.ProductID == cmbProductInformation.Value.ToString()).FirstOrDefault();
                    //if (exDetail != null)
                    //{
                    //    exDetail.Quantity = exDetail.Quantity + Quantity;
                    //}
                    //else
                    //{
                    //    SalesOrderDetail sOrderDetail = new SalesOrderDetail();
                    //    sOrderDetail.ProductID = product.ProductID;
                    //    sOrderDetail.ProductName = product.ProductName;
                    //    sOrderDetail.Quantity = Quantity;
                    //    sOrderDetail.SquareFeet = squareFeet;
                    //    sOrderDetail.Price = price;
                    //    sOrderDetail.VAT = 0;
                    //    _vmSales.lstSalesOrderDetail.Add(sOrderDetail);
                    //}
                    AddDataInGrid(_vmSales.lstSalesOrderDetail);

                    cmbProductInformation.Text = string.Empty;
                    txtQuantity.Text = 0.ToString();
                    cmbProductInformation.Focus();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidFormData())
            {
                decimal squareFeet = 0;
                decimal Quantity = 0;
                decimal price = default(decimal);
                price = DataAccessProxy.GetSalesPriceByProductID(cmbProductInformation.Value.ToString());
                ProductInformationForSales product = lstProductInfo.Where(p => p.ProductID == cmbProductInformation.Value.ToString()).FirstOrDefault();
                decimal.TryParse(txtQuantity.Text, out Quantity);
                decimal.TryParse(txtSqureFeet.Text, out squareFeet);
                SalesOrderDetail exDetail = _vmSales.lstSalesOrderDetail.Where(p => p.ProductID == cmbProductInformation.Value.ToString()).FirstOrDefault();
                if (exDetail != null)
                {
                    exDetail.Quantity = exDetail.Quantity + Quantity;
                }
                else
                {
                    SalesOrderDetail sOrderDetail = new SalesOrderDetail();
                    sOrderDetail.ProductID = product.ProductID;
                    sOrderDetail.ProductName = product.ProductName;
                    sOrderDetail.Quantity = Quantity;
                    sOrderDetail.SquareFeet = squareFeet;
                    sOrderDetail.Price = price;
                    sOrderDetail.VAT = 0;
                    _vmSales.lstSalesOrderDetail.Add(sOrderDetail);
                }

                //SalesOrderDetail sOrderDetail = new SalesOrderDetail();
                //sOrderDetail.ProductID = product.ProductID;
                //sOrderDetail.ProductName = product.ProductName;
                //sOrderDetail.Quantity = Quantity;
                //sOrderDetail.SquareFeet = squareFeet;
                //sOrderDetail.Price = price;
                //sOrderDetail.VAT = 0;
                //_vmSales.lstSalesOrderDetail.Add(sOrderDetail);

                AddDataInGrid(_vmSales.lstSalesOrderDetail);

                cmbProductInformation.Text = string.Empty;
                txtQuantity.Text = 0.ToString();
                cmbProductInformation.Focus();
            }
        }

        private void btnCreditClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            frmSearchSalesInformation frm = new frmSearchSalesInformation();
            frm.ShowDialog();
        }

        private void btnCreditSave_Click(object sender, EventArgs e)
        {
            int salesID = 0;
            if (ValdateInsertionData())
            {
                thread = new System.Threading.Thread(() => AppUtil.ShowLoader());
                thread.Start();
                salesID = InsertSalesInformationNew();

                if (salesID > 0)
                {
                    thread.Abort();
                    if (base.IsUpdating)
                    {
                        MessageBoxHelper.ShowInformation("Successfully saved");
                        this.Close();
                    }
                    else
                    {
                        PrintInvoice(salesID);
                    }
                    ResetAllControls();

                    timer1.Start();
                }
                else
                {
                    MessageBoxHelper.ShowInformation("Failed to save sales information.");
                }
            }

        }

        private void ResetAllControls()
        {
            grvCreditProductDetails.DataSource = BuildSalesDetailTable();
            txtGrandTotal.Clear();
            txtDiscount.Clear();
            txtTransportCost.Clear();
            txtReceiveAmount.Clear();
            txtGrandTotal.Clear();
            txtTotal.Clear();
            txtBillNumber.Text = new SalesManager().GetSaleMemoNumber().ToString();
            base.IsUpdating = false;
            _vmSales = new VMSales();
        }


        /// <summary>
        /// Method to validate insert data.
        /// </summary>
        /// <returns></returns>
        private bool ValdateInsertionData()
        {
            if (string.IsNullOrEmpty(txtBillNumber.Text))
            {
                MessageBoxHelper.ShowInformation("You need to enter memo number");
                txtBillNumber.Focus();
                return false;
            }
            else
            {
                string filter = string.Empty;
                filter = string.Format("{0}='{1}' And {2}={3}", "BillNumber", txtBillNumber.Text.Trim(), "BranchID", MTBFConstants.AppConstants.BranchID);
                SalesOrder salesOrder = new SalesManager().GetFilteredSalesInformation(filter).FirstOrDefault();
                if (salesOrder != null && salesOrder.SalesID != _vmSales.SalesOrder.SalesID)
                {
                    MessageBoxHelper.ShowInformation("Duplicate memo number.");
                    txtBillNumber.Focus();
                    return false;
                }
            }


            if (cmbCustomerName.Value == null)
            {
                MessageBoxHelper.ShowInformation("You need to select customer name.");
                cmbCustomerName.Focus();
                return false;
            }


            if (grvCreditProductDetails.Rows.Count == 0)
            {
                MessageBoxHelper.ShowInformation("Please add information in grid.");
                return false;
            }

            decimal salesAmount = 0;
            decimal receiveAmount = 0;
            decimal disocunt = 0;
            decimal total = 0;
            decimal.TryParse(txtReceiveAmount.Text, out receiveAmount);
            decimal.TryParse(txtGrandTotal.Text, out salesAmount);
            decimal.TryParse(txtDiscount.Text, out disocunt);
            decimal.TryParse(txtTotal.Text, out total);


            foreach (UltraGridRow row in grvCreditProductDetails.Rows)
            {
                if (!IsValidQuantity(row))
                {
                    return false;
                }

                if (IsBlankDataRow(row))
                {
                    return false;
                }
            }

            if (disocunt > total)
            {
                MessageBoxHelper.ShowInformation("Discount can't be greater than total amount.");
                txtDiscount.Focus();
                return false;
            }


            if (txtReceiveAmount.Text == string.Empty)
            {
                MessageBoxHelper.ShowInformation("Check receive amount");
                txtReceiveAmount.Focus();
                return false;
            }

            return true;
        }


        private bool IsBlankDataRow(UltraGridRow row)
        {

            foreach (UltraGridCell cell in row.Cells)
            {

                if (cell.Value == null || cell.Value.ToString() == string.Empty || cell.Value.ToString() == "0")
                {
                    UIHelper.MarkDataGridRowAsNormal(row);
                    if (cell.Column.Header.Caption != "VAT" && cell.Column.Header.Caption != "Delete" && cell.Column.Header.Caption != "Square Feet" && cell.Column.Header.Caption != "Sales Price" && cell.Column.Header.Caption != "Total")
                    {
                        MessageBoxHelper.ShowInformation(cell.Column.Header.Caption + " " + "Can't be blank");
                        UIHelper.MarkDataGridRowAsInvalid(row);
                        return true;
                    }
                }
            }
            return false;
        }


        private bool IsValidQuantity(UltraGridRow row)
        {
            if (row.Cells["ProductName"].Value != null)
            {


                UIHelper.MarkDataGridRowAsNormal(row);
                string productID = row.Cells["ProductID"].Value.ToString();
                decimal salesQuantity = 0;


                ProductInformationForSales inventory = lstProductInfo.Where(i => i.ProductID == productID).FirstOrDefault();

                decimal stockQty = (inventory != null) ? inventory.Quantity : 0;

                decimal orderQuantity = Convert.ToDecimal(row.Cells["Quantity"].Value);

                if (orderQuantity > stockQty)
                {
                    if (DialogResult.No == MessageBoxHelper.ShowConfirmation("The stock limit of this '" + row.Cells["ProductName"].Value.ToString() + "' is exceeded." + Environment.NewLine + "Are you sure to continue?"))
                    {
                        UIHelper.MarkDataGridRowAsInvalid(row);
                        return false;
                    }
                }
            }
            else
            {
                MessageBoxHelper.ShowInformation("Please delete invalid grid row.");
                UIHelper.MarkDataGridRowAsInvalid(row);
                return false;
            }

            return true;
        }



        private void grvCreditProductDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbProductInformation_Leave(object sender, EventArgs e)
        {
            UltraGridRow row = cmbProductInformation.SelectedRow;
            if (row != null)
            {
                ProductInformationForSales product = lstProductInfo.Where(p => p.ProductID == cmbProductInformation.Value.ToString()).FirstOrDefault();
                txtProductSize.Text = (product != null) ? product.Size : 0.ToString();
            }
        }

        private void grvCreditProductDetails_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int quantity = 0;
            if (e.RowIndex != -1)
            {
                Product product = new Product();
                if (grvCreditProductDetails.Rows[e.RowIndex].Cells["Quantity"].Value != null)
                {
                    int.TryParse(grvCreditProductDetails.Rows[e.RowIndex].Cells["Quantity"].Value.ToString(), out quantity);
                }

                if (grvCreditProductDetails.Rows[e.RowIndex].Cells["Quantity"].Value != null)
                {
                    product = DataAccessProxy.GetProductByName(grvCreditProductDetails.Rows[e.RowIndex].Cells["ProductName"].Value.ToString()).Where(p => p.BranchID == MTBFConstants.AppConstants.BranchID && p.OrganizationID == MTBFConstants.AppConstants.OrganizationID).Cast<Product>().FirstOrDefault();

                    ProductSize productSize = DataAccessProxy.GetProductSizeByID(product.ProductSizeID);
                    grvCreditProductDetails.Rows[e.RowIndex].Cells["SquareFeet"].Value = quantity * Convert.ToDecimal(productSize.Name);
                }


                CalculateTotalCost();
            }

        }

        private void cmbProductInformation_Click(object sender, EventArgs e)
        {
            //  LoadProductInformation();
        }

        private void cmbProductInformation_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            UltraGridLayout layout = e.Layout;
            layout.PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadProductInformation();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {

            txtDiscount.Clear();
            txtReceiveAmount.Clear();
            txtTotal.Clear();
            txtCustomerAddress.Clear();
            txtPhone.Clear();
            txtQuantity.Text = 0.ToString().ToString();
        }

        /// <summary>
        /// Method to add product information in grid.
        /// </summary>
        private void AddProductInGridUsingBarCode(string productID)
        {
            int Quantity = 0;
            int rowIndex = 0;
            decimal price = default(decimal);
            decimal vat = 0;
            decimal total = 0;
            decimal previousSquareFeet = 0;
            decimal squareFeet = 0;


            ProductInformationForSales product = lstProductInfo.Where(p => p.ProductID == productID).FirstOrDefault();

            //  ProductSize productSize = DataAccessProxy.GetProductSizeByID(product.ProductSizeID);
            price = DataAccessProxy.GetSalesPriceByProductID(productID);
            Quantity = 1;

            if (IsExistingProduct(product, out rowIndex))
            {
                decimal previousQuantity = 0;
                decimal.TryParse(grvCreditProductDetails.Rows[rowIndex].Cells["Quantity"].Value.ToString(), out previousQuantity);
                decimal.TryParse(grvCreditProductDetails.Rows[rowIndex].Cells["SquareFeet"].Value.ToString(), out previousSquareFeet);

                vat = (price * (Quantity + previousQuantity)) / 100 * product.VAT;
                grvCreditProductDetails.Rows[rowIndex].Cells["Quantity"].Value = previousQuantity + 1;
                grvCreditProductDetails.Rows[rowIndex].Cells["SquareFeet"].Value = previousSquareFeet + Convert.ToDecimal(txtSqureFeet.Text);
                grvCreditProductDetails.Rows[rowIndex].Cells["VAT"].Value = vat.ToString("00.00");
                total = ((price * (previousQuantity + 1)) + vat);
                grvCreditProductDetails.Rows[rowIndex].Cells["Total"].Value = total.ToString("00.00");
            }
            else
            {
                vat = (price * Quantity) / 100 * product.VAT;

                UltraGridRow row = grvCreditProductDetails.DisplayLayout.Bands[0].AddNew();



                row.Cells["ProductID"].Value = product.ProductID;
                row.Cells["ProductName"].Value = product.ProductName;
                row.Cells["Quantity"].Value = Quantity.ToString();
                row.Cells["SquareFeet"].Value = Convert.ToDecimal(txtSqureFeet.Text);
                row.Cells["SalesPrice"].Value = price.ToString();
                row.Cells["VAT"].Value = vat.ToString("00.00");
                total = ((price * Quantity) + vat);
                row.Cells["Total"].Value = total.ToString("00.00");
                // grvCreditProductDetails.Rows[index].Cells["PurchaseID"].Value = _purchaseID.ToString();

            }
            grvCreditProductDetails.DisplayLayout.Bands[0].Columns["ProductID"].Hidden = true;
            cmbProductInformation.Text = string.Empty;

            cmbProductInformation.Focus();
            txtQuantity.Text = 0.ToString().ToString();


            CalculateTotalCost();

        }

        private void txtBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                ProductInformationForSales saleableproduct = lstProductInfo.Cast<ProductInformationForSales>().Where(p => p.Barcode == txtBarCode.Text).ToList().FirstOrDefault();
                if (saleableproduct != null)
                {
                    //_purchaseID = saleableproduct.PurchaseID;
                    AddProductInGridUsingBarCode(saleableproduct.ProductID);
                }
                else
                {
                    MessageBoxHelper.ShowInformation("There is no product for this bar code");
                }

                txtBarCode.Clear();
                txtBarCode.Focus();
            }
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            decimal total = 0;
            decimal receiveAmount = 0;
            decimal discount = 0;
            decimal transportCost = 0;
            decimal cuttignCharge = 0;
            decimal otherCharge = 0;

            decimal.TryParse(txtTotal.Text, out total);
            decimal.TryParse(txtReceiveAmount.Text, out receiveAmount);
            decimal.TryParse(txtDiscount.Text, out discount);
            decimal.TryParse(txtTransportCost.Text, out transportCost);


            txtGrandTotal.Text = ((total + transportCost + cuttignCharge + otherCharge) - discount).ToString();

            txtReceiveAmount.Text = 0.ToString().ToString();
            txtChange.Text = 0.ToString().ToString();
        }

        private void txtReceiveAmount_TextChanged(object sender, EventArgs e)
        {
            decimal grandTotal = 0;
            decimal receiveAmount = 0;

            decimal.TryParse(txtGrandTotal.Text, out grandTotal);
            decimal.TryParse(txtReceiveAmount.Text, out receiveAmount);
            txtChange.Text = (receiveAmount - grandTotal).ToString("F2");
        }

        private void UltraQuantity_ValueChanged(object sender, EventArgs e)
        {
            decimal quantity = 0;
            decimal size = 0;
            decimal.TryParse(txtQuantity.Text, out quantity);
            decimal.TryParse(txtProductSize.Text, out size);

            txtSqureFeet.Text = (quantity * size).ToString();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintSalesData();
        }


        private DataTable BuildSalesTable()
        {
            DataTable table = new DataTable();

            table.Columns.Add("SalesDate");
            table.Columns.Add("ProductName");
            table.Columns.Add("Quantity");
            table.Columns.Add("Price");
            table.Columns.Add("Size");
            table.Columns.Add("Total");
            table.Columns.Add("SquareFeet");
            table.Columns.Add("CustomerName");
            table.Columns.Add("Address");
            table.Columns.Add("Phone");
            table.Columns.Add("SalesAmount");
            table.Columns.Add("Discount");
            table.Columns.Add("ReceiveAmount");

            return table;
        }


        private void PrintSalesData()
        {
            Employee employee = new Employee();
            DataTable salesReport = BuildSalesTable();
            Organization pharmacy = new Organization();
            foreach (UltraGridRow row in grvCreditProductDetails.Rows)
            {
                if (row.Cells["ProductName"].Value != null)
                {
                    Product product = DataAccessProxy.GetProductByName(row.Cells["ProductName"].Value.ToString()).Where(p => p.BranchID == MTBFConstants.AppConstants.BranchID && p.OrganizationID == MTBFConstants.AppConstants.OrganizationID).Cast<Product>().FirstOrDefault();
                    ProductSize productSize = DataAccessProxy.GetProductSizeByID(product.ProductSizeID);
                    DataRow dtrow = salesReport.NewRow();
                    dtrow["SalesDate"] = dtpSalesDate.Value;
                    dtrow["ProductName"] = row.Cells["ProductName"].Value;
                    dtrow["Quantity"] = row.Cells["Quantity"].Value;
                    dtrow["Price"] = row.Cells["SalesPrice"].Value;
                    dtrow["Size"] = productSize.Name;
                    dtrow["SquareFeet"] = row.Cells["SquareFeet"].Value;
                    dtrow["Total"] = row.Cells["Total"].Value;
                    dtrow["CustomerName"] = cmbCustomerName.Text;
                    dtrow["Address"] = txtCustomerAddress.Text;
                    dtrow["Phone"] = txtPhone.Text;
                    dtrow["SalesAmount"] = txtTotal.Text;
                    dtrow["ReceiveAmount"] = txtReceiveAmount.Text;
                    dtrow["Discount"] = txtDiscount.Text;
                    salesReport.Rows.Add(dtrow);
                }
            }

            rptCreditSalesShowRoom op = new rptCreditSalesShowRoom();
            frmSalesReport objmainReport = new frmSalesReport();
            op.DataSource = salesReport;
            objmainReport.rptViewer.Document = op.Document;
            objmainReport.rptViewer.Dock = DockStyle.Fill;
            SetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress, ref pharmacy);
            op.txtServiceMan.Text = (employee != null) ? employee.EmployeeName : "Super Admin";
            op.Run();
            objmainReport.MdiParent = this.MdiParent;
            objmainReport.Show();

        }

        private void grvCreditProductDetails_ClickCell(object sender, ClickCellEventArgs e)
        {
            string celltext = e.Cell.Column.Header.Caption;
            if (celltext == "Delete")
            {
                string productID = grvCreditProductDetails.Rows[e.Cell.Row.Index].Cells["ProductID"].Value.ToString();
                int rowIndex = e.Cell.Row.Index;
                if (grvCreditProductDetails.Rows[e.Cell.Row.Index].Delete())
                {
                    SalesOrderDetail sDetail = _vmSales.lstSalesOrderDetail.Where(p => p.ProductID == productID).FirstOrDefault();
                    if (sDetail != null)
                    {
                        _vmSales.lstSalesOrderDetail.Remove(sDetail);
                    }
                    AddDataInGrid(_vmSales.lstSalesOrderDetail);
                }
            }

            if (celltext == "Sales Price")
            {
                grvCreditProductDetails.Rows[e.Cell.Row.Index].Cells["SalesPrice"].Activate();
                grvCreditProductDetails.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode);
            }

            if (celltext == "Quantity")
            {
                grvCreditProductDetails.Rows[e.Cell.Row.Index].Cells["Quantity"].Activate();
                grvCreditProductDetails.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode);
            }

        }

        private void grvCreditProductDetails_CellChange(object sender, CellEventArgs e)
        {
            decimal quantity = 0;
            decimal salesPrice = 0;
            decimal productSize = 0;
            string productID = grvCreditProductDetails.Rows[e.Cell.Row.Index].Cells["ProductID"].Text;
            if (_lstSalesOrderDetail.Count == 0)
            {

                ProductInformationForSales product = new ProductInformationForSales();

                product = lstProductInfo.Where(p => p.ProductID == productID).FirstOrDefault();

                decimal.TryParse(product.Size, out productSize);

                if (!string.IsNullOrEmpty(grvCreditProductDetails.Rows[e.Cell.Row.Index].Cells["Quantity"].Text))
                {
                    decimal.TryParse(grvCreditProductDetails.Rows[e.Cell.Row.Index].Cells["Quantity"].Text, out quantity);

                }


                if (!string.IsNullOrEmpty(grvCreditProductDetails.Rows[e.Cell.Row.Index].Cells["SalesPrice"].Text))
                {
                    decimal.TryParse(grvCreditProductDetails.Rows[e.Cell.Row.Index].Cells["SalesPrice"].Text, out salesPrice);

                }


                grvCreditProductDetails.Rows[e.Cell.Row.Index].Cells["Total"].Value = Math.Floor(quantity * salesPrice).ToString();

            }
            // SalesOrderDetail sDetail = _vmSales.lstSalesOrderDetail.Where(s => s.ProductID == productID).FirstOrDefault();

            _vmSales.lstSalesOrderDetail = new List<SalesOrderDetail>();
            foreach (UltraGridRow row in grvCreditProductDetails.Rows)
            {
                SalesOrderDetail sDetail = new SalesOrderDetail();
                decimal.TryParse(row.Cells["SalesPrice"].Text, out salesPrice);
                decimal.TryParse(row.Cells["Quantity"].Text, out quantity);

                sDetail.Price = salesPrice;
                sDetail.Quantity = quantity;
                sDetail.ProductID = row.Cells["ProductID"].Text;
                sDetail.ProductName = row.Cells["ProductName"].Text;
                sDetail.IsFree = (salesPrice == 0) ? true : false;
                _vmSales.lstSalesOrderDetail.Add(sDetail);
            }




            CalculateTotalCost();
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
            List<SalesRepresentative> lstSalesRepresentative = new List<SalesRepresentative>();
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


        private void btnSelectProduct_Click(object sender, EventArgs e)
        {
            frmSelectProduct frm = new frmSelectProduct();
            frm.LoadProductInfo += new frmSelectProduct.LoadProductInfoEventHandler(frm_LoadProductInfo);
            frm.ShowDialog();

        }

        void frm_LoadProductInfo(string productID)
        {
            decimal Quantity = 1;
            decimal price = default(decimal);

            decimal squareFeet = 0;

            Product product = new Product();
            product = new ProductManager().GetProductByID(productID);
            price = DataAccessProxy.GetSalesPriceByProductID(productID);
            // ProductInformationForSales product = lstProductInfo.Where(p => p.ProductID == cmbProductInformation.Value.ToString()).FirstOrDefault();

            decimal.TryParse(product.SizeName, out squareFeet);
            SalesOrderDetail exDetail = _vmSales.lstSalesOrderDetail.Where(p => p.ProductID == productID).FirstOrDefault();
            if (exDetail != null)
            {
                exDetail.Quantity = exDetail.Quantity + Quantity;
            }
            else
            {
                SalesOrderDetail sOrderDetail = new SalesOrderDetail();
                sOrderDetail.ProductID = product.ProductID;
                sOrderDetail.ProductName = product.ProductName;
                sOrderDetail.Quantity = Quantity;
                sOrderDetail.SquareFeet = squareFeet;
                sOrderDetail.Price = price;
                sOrderDetail.VAT = 0;
                _vmSales.lstSalesOrderDetail.Add(sOrderDetail);
            }

            AddDataInGrid(_vmSales.lstSalesOrderDetail);

            cmbProductInformation.Text = string.Empty;
            txtQuantity.Text = 0.ToString();
        }

        private void frm_LoadProductInfo(ProductInformationForSales product)
        {
            decimal Quantity = 1;
            decimal price = default(decimal);

            decimal squareFeet = 0;


            price = DataAccessProxy.GetSalesPriceByProductID(product.ProductID);
            // ProductInformationForSales product = lstProductInfo.Where(p => p.ProductID == cmbProductInformation.Value.ToString()).FirstOrDefault();

            decimal.TryParse(product.Size, out squareFeet);
            SalesOrderDetail exDetail = _vmSales.lstSalesOrderDetail.Where(p => p.ProductID == cmbProductInformation.Value.ToString()).FirstOrDefault();
            if (exDetail != null)
            {
                exDetail.Quantity = exDetail.Quantity + Quantity;
            }
            else
            {
                SalesOrderDetail sOrderDetail = new SalesOrderDetail();
                sOrderDetail.ProductID = product.ProductID;
                sOrderDetail.ProductName = product.ProductName;
                sOrderDetail.Quantity = Quantity;
                sOrderDetail.SquareFeet = squareFeet;
                sOrderDetail.Price = price;
                sOrderDetail.VAT = 0;
                _vmSales.lstSalesOrderDetail.Add(sOrderDetail);
            }

            AddDataInGrid(_vmSales.lstSalesOrderDetail);

            cmbProductInformation.Text = string.Empty;
            txtQuantity.Text = 0.ToString();

            //if (!string.IsNullOrEmpty(productID))
            //{
            //    ProductInformationForSales product = lstProductInfo.Where(p => p.ProductID == productID).FirstOrDefault();
            //    if (product != null)
            //    {
            //        price = DataAccessProxy.GetSalesPriceByProductID(productID);
            //        //  ProductSize productSize = DataAccessProxy.GetProductSizeByID(product.ProductSizeID);
            //        Quantity = 1;

            //        decimal.TryParse(product.Size, out size);
            //        squareFeet = size;

            //        if (IsExistingProduct(product, out rowIndex))
            //        {
            //            decimal previousQuantity = 0;
            //            decimal.TryParse(grvCreditProductDetails.Rows[rowIndex].Cells["Quantity"].Value.ToString(), out previousQuantity);

            //            decimal.TryParse(grvCreditProductDetails.Rows[rowIndex].Cells["SquareFeet"].Value.ToString(), out previousSquareFeet);
            //            vat = (price * (Quantity + previousQuantity)) / 100 * product.VAT;
            //            grvCreditProductDetails.Rows[rowIndex].Cells["Quantity"].Value = previousQuantity + Quantity;
            //            grvCreditProductDetails.Rows[rowIndex].Cells["SquareFeet"].Value = previousSquareFeet + squareFeet;
            //            grvCreditProductDetails.Rows[rowIndex].Cells["VAT"].Value = vat.ToString("F2");
            //            total = ((price * (previousQuantity + 1)) + vat);
            //            grvCreditProductDetails.Rows[rowIndex].Cells["Total"].Value = total.ToString("F2");

            //        }
            //        else
            //        {
            //            UltraGridRow row = grvCreditProductDetails.DisplayLayout.Bands[0].AddNew();

            //            vat = (price * Quantity) / 100 * product.VAT;
            //            row.Cells["ProductID"].Value = product.ProductID;
            //            row.Cells["ProductName"].Value = product.ProductName;
            //            row.Cells["Quantity"].Value = Quantity.ToString();
            //            row.Cells["SquareFeet"].Value = size;
            //            row.Cells["SalesPrice"].Value = price.ToString();
            //            row.Cells["VAT"].Value = vat.ToString("F2");
            //            total = ((price * Quantity) + vat);
            //            row.Cells["Total"].Value = total.ToString("F2");
            //            row.Cells["Delete"].Value = "Delete";
            //        }


            //        grvCreditProductDetails.DisplayLayout.Bands[0].Columns["ProductID"].Hidden = true;
            //        cmbProductInformation.Text = string.Empty;

            //        cmbProductInformation.Focus();
            //        txtQuantity.Text = 0.ToString();
            //    }
            //    else
            //    {
            //        MessageBoxHelper.ShowInformation("Invalid product selection.");
            //        cmbProductInformation.Focus();
            //    }
            //}

            //  CalculateTotalCost();
        }

        private void grvCreditProductDetails_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case (int)Keys.Up:
                    grvCreditProductDetails.PerformAction(UltraGridAction.ExitEditMode, false, false);
                    grvCreditProductDetails.PerformAction(UltraGridAction.AboveCell, false, false);
                    e.Handled = true;
                    grvCreditProductDetails.PerformAction(UltraGridAction.EnterEditMode, false, false);
                    break;
                case (int)Keys.Down:
                    grvCreditProductDetails.PerformAction(UltraGridAction.ExitEditMode, false, false);
                    grvCreditProductDetails.PerformAction(UltraGridAction.BelowCell, false, false);
                    e.Handled = true;
                    grvCreditProductDetails.PerformAction(UltraGridAction.EnterEditMode, false, false);
                    break;
                case (int)Keys.Right:
                    grvCreditProductDetails.PerformAction(UltraGridAction.ExitEditMode, false, false);
                    grvCreditProductDetails.PerformAction(UltraGridAction.NextCellByTab, false, false);
                    e.Handled = true;
                    grvCreditProductDetails.PerformAction(UltraGridAction.EnterEditMode, false, false);
                    break;
                case (int)Keys.Left:
                    grvCreditProductDetails.PerformAction(UltraGridAction.ExitEditMode, false, false);
                    grvCreditProductDetails.PerformAction(UltraGridAction.PrevCellByTab, false, false);
                    e.Handled = true;
                    grvCreditProductDetails.PerformAction(UltraGridAction.EnterEditMode, false, false);
                    break;
            }
        }

        private void cmbCustomerName_ValueChanged(object sender, EventArgs e)
        {
            int custoemrID = 0;
            if (cmbCustomerName.Value != null)
            {
                int.TryParse(cmbCustomerName.Value.ToString(), out custoemrID);
                Customer customer = lstCustomer.Where(c => c.CustomerID == custoemrID).FirstOrDefault();
                txtPreviousDue.Text = new CashReceiveManager().GetCustomerDueAmountByCustomerID(custoemrID, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).ToString();
                if (customer != null)
                {
                    txtPhone.Text = customer.Phone;
                    txtCustomerAddress.Text = customer.Address;
                    txtCreditLimit.Text = customer.CreditLimit.ToString();
                }
                else
                {
                    txtPhone.Clear();
                    txtCustomerAddress.Clear();
                    MessageBoxHelper.ShowWarning("Invalid customer selection.");
                    cmbCustomerName.Focus();
                }
            }
        }

        private void btAddCustomer_Click(object sender, EventArgs e)
        {
            frmCustomer frm = new frmCustomer(0, false);
            frm.OnLoadCustomerInformation += new frmCustomer.LodaEventHandaler(frm_OnLoadCustomerInformation);
            frm.ShowDialog();
        }

        void frm_OnLoadCustomerInformation(int customerID)
        {
            LoadCustomerCombo();
            cmbCustomerName.Value = customerID;
        }

        private void txtDiscountPercent_TextChanged(object sender, EventArgs e)
        {
            decimal total = 0;
            decimal percentage = 0;
            if (!string.IsNullOrEmpty(txtDiscountPercent.Text))
            {
                decimal.TryParse(txtTotal.Text, out total);
                decimal.TryParse(txtDiscountPercent.Text, out percentage);
                txtDiscount.Text = ((total / 100) * percentage).ToString("F2");
            }
            else
            {
                txtDiscount.Text = 0.ToString();
            }
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            frmCustomer frm = new frmCustomer(0, false);
            frm.OnLoadCustomerInformation += new frmCustomer.LodaEventHandaler(frm_OnLoadCustomerInformation);
            frm.ShowDialog();
        }

        private void txtBarCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                ProductInformationForSales product = lstProductInfo.Cast<ProductInformationForSales>().Where(p => p.Barcode == txtBarCode.Text).ToList().FirstOrDefault();
                if (product != null)
                {
                    decimal price = default(decimal);
                    price = DataAccessProxy.GetSalesPriceByProductID(product.ProductID);

                    SalesOrderDetail exDetail = _vmSales.lstSalesOrderDetail.Where(p => p.ProductID == product.ProductID).FirstOrDefault();
                    if (exDetail != null)
                    {
                        exDetail.Quantity = exDetail.Quantity + 1;
                    }
                    else
                    {
                        SalesOrderDetail sOrderDetail = new SalesOrderDetail();
                        sOrderDetail.ProductID = product.ProductID;
                        sOrderDetail.ProductName = product.ProductName;
                        sOrderDetail.Quantity = 1;
                        sOrderDetail.Price = price;
                        sOrderDetail.VAT = 0;
                        _vmSales.lstSalesOrderDetail.Add(sOrderDetail);
                    }
                    AddDataInGrid(_vmSales.lstSalesOrderDetail);

                }
                else
                {
                    MessageBoxHelper.ShowInformation("There is no product for this bar code");
                }

                txtBarCode.Clear();
                txtBarCode.Focus();
            }
        }





    }

}