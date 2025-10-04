
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


namespace Tiles_Inventory
{

    public partial class frmEditWholeSales : BaseForm
    {
        private string pharmacyName;
        private string pharmacyAddress;
        List<ProductInformationForSales> lstProductInfo = new List<ProductInformationForSales>();

        int _purchaseID = 0;
        private bool isUpdate = false;
        private int _salesID = 0;

        public frmEditWholeSales()
        {
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        public frmEditWholeSales(bool isEdit, int salesID)
        {
            isUpdate = isEdit;
            _salesID = salesID;
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        private void WholeSales_Load(System.Object sender, System.EventArgs e)
        {
            //LoadProductInformationWithLike("A");
            LoadProductInformation();
            if (isUpdate)
            {
                LoadExistingSalesInformation();
                CalculateTotalSalesAmount();
            }
        }


        private void CalculateTotalSalesAmount()
        {
            decimal total = 0;
            foreach (UltraGridRow row in grvCreditProductDetails.Rows)
            {
                if (row.Cells["ProductName"].Value != null)
                {
                    total = total + Convert.ToDecimal(row.Cells["Total"].Value);
                }
            }
            txtTotal.Text = total.ToString("F2");
        }


        private void LoadExistingSalesInformation()
        {
            SalesOrder salesOrder = DataAccessProxy.GetSalesOrderByID(_salesID);
            Customer customer = DataAccessProxy.GetCustomerByID(salesOrder.CustomerID);
            txtPhone.Text = customer.Phone;
            txtCustomerName.Text = customer.CustomerName;
            txtCustomerAddress.Text = customer.Address;

            txtCustomerName.ReadOnly = true;
            txtCustomerAddress.ReadOnly = true;
            txtPhone.ReadOnly = true;
            DataTable purchaseDetailtable = BuildSalesDetailTable();

            foreach (SalesOrderDetail orderDetail in DataAccessProxy.GetAllSalesDetailBySalesID(salesOrder.SalesID))
            {
                DataRow row = purchaseDetailtable.NewRow();
                Product product = DataAccessProxy.GetProductInformationByID(orderDetail.ProductID);
                row["ProductID"] = (product != null) ? product.ProductID : string.Empty;
                row["ProductName"] = (product != null) ? product.ProductName : string.Empty;
                row["Quantity"] = orderDetail.Quantity;
                row["SquareFeet"] = orderDetail.SquareFeet;
                row["SalesPrice"] = orderDetail.Price;
                row["Total"] = (orderDetail.Price * orderDetail.Quantity).ToString("00.00");
                row["Delete"] = "Delete";
                row["PurchaseID"] = orderDetail.PurchaseID;
                row["SalesOrderDetailID"] = orderDetail.SerialNo;
                purchaseDetailtable.Rows.Add(row);
            }
            grvCreditProductDetails.DataSource = purchaseDetailtable;
            grvCreditProductDetails.DisplayLayout.Bands[0].Columns["PurchaseID"].Hidden = true;
            grvCreditProductDetails.DisplayLayout.Bands[0].Columns["ProductID"].Hidden = true;
            grvCreditProductDetails.DisplayLayout.Bands[0].Columns["SalesOrderDetailID"].Hidden = true;
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
            table.Columns.Add("SquareFeet");
            table.Columns.Add("SalesPrice");
            table.Columns.Add("VAT");
            table.Columns.Add("Total");
            table.Columns.Add("Delete");
            table.Columns.Add("PurchaseID");
            table.Columns.Add("SalesOrderDetailID");

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
                    int purchaseID = Convert.ToInt32(row.Cells["PurchaseID"].Value);
                    int listCount = lstProductInfo.Count;


                    for (int j = 0; j < listCount; j++)
                    {
                        product = lstProductInfo[j];
                        if (productID == product.ProductID)//&& purchaseID == product.PurchaseID
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

            if (txtCustomerName.Text == string.Empty)
            {
                MessageBoxHelper.ShowInformation("Required field can't be blank.");
                txtCustomerName.Focus();
                return false;
            }



            if ((cmbProductInformation.Value == null))
            {
                MessageBoxHelper.ShowInformation("Required field can't be blank.");
                cmbProductInformation.Focus();
                return false;
            }

            if (Convert.ToInt32(UltraQuantity.Value) == 0)
            {
                MessageBoxHelper.ShowInformation("Required field can't be blank.");

                UltraQuantity.Focus();
                return false;
            }

            return true;
        }

        ///// <summary>
        ///// Method to add product information in grid.
        ///// </summary>
        //private void AddProductInGrid()
        //{
        //    int Quantity = 0;
        //    decimal Price = default(decimal);

        //    if ((cmbProductInformation.Value != null))
        //    {

        //        Price = DataAccessProxy.GetSalesPriceByProductID(cmbProductInformation.Value.ToString());

        //        Quantity = Convert.ToInt32(UltraQuantity.Value);

        //        grvCreditProductDetails.Rows.Add(cmbProductInformation.Value, Quantity.ToString(), cmbProductInformation.Text, Price, Price * Quantity, _purchaseID);

        //        grvCreditProductDetails.Columns[0].Visible = false;
        //        cmbProductInformation.Text = string.Empty;

        //        cmbProductInformation.Focus();
        //        UltraQuantity.Value = 0;
        //    }

        //    CalculateTotalCost();

        //}


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
                Product product = DataAccessProxy.GetProductInformationByID(cmbProductInformation.Value.ToString());
                price = DataAccessProxy.GetSalesPriceByProductID(cmbProductInformation.Value.ToString());
                ProductSize productSize = DataAccessProxy.GetProductSizeByID(product.ProductSizeID);
                Quantity = Convert.ToInt32(UltraQuantity.Value);
                squareFeet = Convert.ToDecimal(txtSqureFeet.Text);

                if (IsExistingProduct(product, out rowIndex))
                {
                    decimal previousQuantity = 0;
                    decimal.TryParse(grvCreditProductDetails.Rows[rowIndex].Cells["Quantity"].Value.ToString(), out previousQuantity);

                    decimal.TryParse(grvCreditProductDetails.Rows[rowIndex].Cells["SquareFeet"].Value.ToString(), out previousSquareFeet);
                    vat = (price * (Quantity + previousQuantity)) / 100 * product.VAT;
                    grvCreditProductDetails.Rows[rowIndex].Cells["Quantity"].Value = previousQuantity + Quantity;
                    grvCreditProductDetails.Rows[rowIndex].Cells["SquareFeet"].Value = previousSquareFeet + squareFeet;
                    grvCreditProductDetails.Rows[rowIndex].Cells["VAT"].Value = vat.ToString("F2");
                    total = ((price * (previousQuantity + 1)) + vat);
                    grvCreditProductDetails.Rows[rowIndex].Cells["Delete"].Value = "Delete";
                    grvCreditProductDetails.Rows[rowIndex].Cells["Total"].Value = total.ToString("F2");

                }
                else
                {
                    UltraGridRow row = grvCreditProductDetails.DisplayLayout.Bands[0].AddNew();

                    vat = (price * Quantity) / 100 * product.VAT;
                    row.Cells["ProductID"].Value = product.ProductID;
                    row.Cells["ProductName"].Value = product.ProductName;
                    row.Cells["Quantity"].Value = Quantity.ToString();
                    row.Cells["SquareFeet"].Value = Convert.ToDecimal(txtSqureFeet.Text);
                    row.Cells["SalesPrice"].Value = price.ToString();
                    row.Cells["VAT"].Value = vat.ToString("F2");
                    total = ((price * Quantity) + vat);
                    row.Cells["Total"].Value = total.ToString("F2");
                    row.Cells["Delete"].Value = "Delete";
                    row.Cells["PurchaseID"].Value = _purchaseID.ToString();
                }
                grvCreditProductDetails.DisplayLayout.Bands[0].Columns["ProductID"].Hidden = true;
                grvCreditProductDetails.DisplayLayout.Bands[0].Columns["PurchaseID"].Hidden = true;
                cmbProductInformation.Text = string.Empty;

                cmbProductInformation.Focus();
                UltraQuantity.Value = 0;
            }

            CalculateTotalCost();
            CalculateTotalSalesAmount();
        }


        private bool IsExistingProduct(Product product, out int rowIndex)
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
        private List<SalesOrderDetail> GetAllSalesOrderDetailInformation()
        {
            string productID = string.Empty;
            int quantity = 0;
            decimal squareFeet = 0;
            int salesOrderDtailID = 0;
            decimal price = default(decimal);
            List<SalesOrderDetail> lstSalesOrderDetail = new List<SalesOrderDetail>();

            foreach (UltraGridRow row in grvCreditProductDetails.Rows)
            {
                int.TryParse(row.Cells["SalesOrderDetailID"].Value.ToString(), out salesOrderDtailID);
                SalesOrderDetail salesOrderDetail = DataAccessProxy.GetSalesDetailByID(salesOrderDtailID);
                _purchaseID = Convert.ToInt32(row.Cells["PurchaseID"].Value);
                productID = row.Cells["ProductID"].Value.ToString();
                quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                price = Convert.ToDecimal(row.Cells["SalesPrice"].Value);
                decimal.TryParse(row.Cells["SquareFeet"].Value.ToString(), out squareFeet);
                salesOrderDetail.ProductID = productID;
                salesOrderDetail.SalesID = _salesID;
                salesOrderDetail.Price = price;
                salesOrderDetail.Quantity = quantity;
                salesOrderDetail.SquareFeet = squareFeet;
                salesOrderDetail.PurchaseID = _purchaseID;
                lstSalesOrderDetail.Add(salesOrderDetail);
            }


            return lstSalesOrderDetail;
        }


        /// <summary>
        /// Method to get customer id by phone number.
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        private int GetCustomerIDByPhoneNumber(string phoneNumber)
        {

            int customerID = 0;
            Customer customer = DataAccessProxy.GetCustomerByPhoneNumber(phoneNumber, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID);
            if (customer != null)
            {
                customerID = customer.CustomerID;
                txtCustomerName.Text = customer.CustomerName;
                txtCustomerAddress.Text = customer.Address;
                txtBarCode.Focus();
            }

            return customerID;
        }

        /// <summary>
        /// Method to check duplicate customer information.
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        private bool IsDuplicateCustomer(string phoneNumber)
        {
            bool isDuplicate = false;
            Customer customer = DataAccessProxy.GetCustomerByPhoneNumber(phoneNumber, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID);

            if (customer != null)
            {
                isDuplicate = true;
            }

            return isDuplicate;
        }

        /// <summary>
        /// Method to insert customer information.
        /// </summary>
        /// <returns></returns>
        private int InsertCustomerInformation()
        {
            int result = 0;
            Customer customer = new Customer();

            customer.CustomerName = txtCustomerName.Text;
            customer.Address = txtCustomerAddress.Text;
            customer.Phone = txtPhone.Text;

            DataAccessProxy.InsertCustomer(customer);
            result = 1;

            return result;
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
            salesReport = DataAccessProxy.GetCreditSalesReport(salesId);

            rptCreditSalesShowRoom op = new rptCreditSalesShowRoom();
            frmSalesReport objmainReport = new frmSalesReport();
            op.DataSource = salesReport;
            objmainReport.rptViewer.Document = op.Document;
            objmainReport.rptViewer.Dock = DockStyle.Fill;

            SetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress, ref pharmacy);

            op.txtPharmacyName.Text = pharmacyName;
            op.txtAddress.Text = pharmacyAddress;
            employee = DataAccessProxy.GetEmployeeByID(IFMSConstant.LoggedinUserID);
            if (pharmacy.OrganizationLogo != null)
            {
                MemoryStream ms = new MemoryStream(pharmacy.OrganizationLogo);
                Image returnImage = Image.FromStream(ms);
                op.picLogo.Image = returnImage;
            }

            op.txtServiceMan.Text = (employee != null) ? employee.EmployeeName : string.Empty;
            op.Run();
            objmainReport.MdiParent = this.MdiParent;
            objmainReport.Show();

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

        /// <summary>
        /// Method to calculate total cost.
        /// </summary>
        private void CalculateTotalCost()
        {
            decimal sum = 0;
            int quantity = 0;
            decimal price = 0;
            decimal vat = 0;
            decimal discount = 0;
            decimal squareFeet = 0;

            for (int i = 0; i <= grvCreditProductDetails.Rows.Count - 2; i++)
            {
                Product product = new Product();
                if (grvCreditProductDetails.Rows[i].Cells["ProductID"].Value != null)
                {
                    product = DataAccessProxy.GetProductInformationByID(grvCreditProductDetails.Rows[i].Cells["ProductID"].Value.ToString());
                }
                if (grvCreditProductDetails.Rows[i].Cells["SalesPrice"].Value != null)
                {
                    decimal.TryParse(grvCreditProductDetails.Rows[i].Cells["SalesPrice"].Value.ToString(), out price);
                }
                if (grvCreditProductDetails.Rows[i].Cells["Quantity"].Value != null)
                {
                    int.TryParse(grvCreditProductDetails.Rows[i].Cells["Quantity"].Value.ToString(), out quantity);
                }
                if (grvCreditProductDetails.Rows[i].Cells["SquareFeet"].Value != null)
                {
                    decimal.TryParse(grvCreditProductDetails.Rows[i].Cells["SquareFeet"].Value.ToString(), out squareFeet);
                }

                vat = (price * (squareFeet)) / 100 * product.VAT;
                grvCreditProductDetails.Rows[i].Cells["VAT"].Value = vat.ToString("F2");

                if (grvCreditProductDetails.Rows[i].Cells["VAT"].Value != null)
                {
                    decimal.TryParse(grvCreditProductDetails.Rows[i].Cells["VAT"].Value.ToString(), out vat);
                }
                grvCreditProductDetails.Rows[i].Cells["Total"].Value = ((price * squareFeet) + vat).ToString("F2");
                // sum = sum + (price * squareFeet) + vat;
            }
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

            if (txtCustomerName.Text == string.Empty)
            {
                MessageBoxHelper.ShowInformation("Required field can't be blank.");
                txtCustomerName.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Method to insert sales information.
        /// </summary>
        /// <returns></returns>
        private int UpdateSalesInformation()
        {
            int result = 0;
            int customerID = 0;
            decimal salesAmount = 0;
            decimal discount = 0;
            decimal receiveAmount = 0;

            if (ValidFormSaveData())
            {
                salesAmount = Convert.ToDecimal(txtTotal.Text);
                discount = txtDiscount.Text == string.Empty ? 0 : Convert.ToDecimal(txtDiscount.Text);
                decimal.TryParse(txtReceiveAmount.Text, out receiveAmount);
                decimal grandTotal = 0;
                decimal.TryParse(txtGrandTotal.Text, out grandTotal);
                receiveAmount = (receiveAmount > grandTotal) ? grandTotal : receiveAmount;

                List<SalesOrderDetail> lstSalesOrderDetail = new List<SalesOrderDetail>();
                SalesOrder salesOrder = DataAccessProxy.GetSalesOrderByID(_salesID);
                salesOrder.SalesDate = DateTime.Now;
                salesOrder.SalesAmount = salesAmount;
                salesOrder.EmployeeID = IFMSConstant.LoggedinUserID;
                salesOrder.Discount = discount;
                salesOrder.ReceiveAmount = receiveAmount;
                lstSalesOrderDetail = GetAllSalesOrderDetailInformation();
                result = DataAccessProxy.UpdateSalesOrderInformation(salesOrder, lstSalesOrderDetail);

            }
            return result;

        }


        /// <summary>
        /// Method to insert child account information.
        /// </summary>
        /// <returns></returns>
        private int InsertChildAccount(int customerID)
        {
            int result = 0;
            try
            {
                ChildAccount childAccount = new ChildAccount();
                childAccount.AccountID = IFMSConstant.AccountPayableID;
                childAccount.Description = txtCustomerName.Text;
                Employee employee = DataAccessProxy.GetEmployeeByID(IFMSConstant.LoggedinUserID);
                childAccount.CustomerID = customerID;
                childAccount.CreatedBy = (employee != null) ? employee.EmployeeName : string.Empty;
                childAccount.CreatedDate = DateTime.Now;
                childAccount.Status = 1;
                result = DataAccessProxy.InsertChildAccount(childAccount);

            }
            catch (Exception ex)
            {
                result = 0;
            }
            return result;
        }


        /// <summary>
        /// Method to insert journal information for credit sales.
        /// </summary>
        /// <returns></returns>
        public int InsertJournalInformationForCreditSales()
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
                    childAccount = DataAccessProxy.GetChildAccountByID(IFMSConstant.GoodsSalesAccountID);
                    journal.DrCrIndecator = "Cr";

                    if (childAccount != null)
                    {
                        journal.AccountID = childAccount.AccountID;
                        journal.ChildAccountID = IFMSConstant.GoodsSalesAccountID;
                    }

                }

                journal.JAccountID = 0;
                journal.Amount = amount;
                journal.ReferenceNo = referenceID;
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
            decimal quantity = 0;
            PurchaseOrderDetail purchaseOrderDetail = null;
            decimal purchasePrice = 0;
            for (int i = 0; i <= grvCreditProductDetails.Rows.Count - 2; i++)
            {
                purchaseID = Convert.ToInt32(grvCreditProductDetails.Rows[i].Cells["PurchaseID"].Value);
                productID = grvCreditProductDetails.Rows[i].Cells["ProductID"].Value.ToString();
                quantity = Convert.ToInt32(grvCreditProductDetails.Rows[i].Cells["Quantity"].Value);
                purchaseOrderDetail = DataAccessProxy.GetPurchaseOrderDetailByProductAndPurchaseID(productID, purchaseID);
                if (purchaseOrderDetail != null)
                {
                    purchasePrice = purchasePrice + (purchaseOrderDetail.PurchasePrice * quantity);
                }
            }
            return purchasePrice;
        }

        /// <summary>
        /// Method to insert journal information for credit sales.
        /// </summary>
        /// <returns></returns>
        public int InsertJournalInformationForGoodsDelivery()
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

                journal.JAccountID = 0;
                journal.Amount = amount;
                journal.ReferenceNo = referenceID;
                result = DataAccessProxy.InsertJournal(journal);
            }

            return result;
        }

        /// <summary>
        /// Method to insert journal information after cash receive
        /// </summary>
        /// <returns></returns>
        public int InsertJournalInformationForCashReceive()
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

                journal.JAccountID = 0;
                journal.Amount = amount;
                journal.ReferenceNo = referenceID;
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



        private void LoadProductInformation()
        {
            //  List<ProductInformationForSales> lstProductInfo = new List<ProductInformationForSales>();
            lstProductInfo = DataAccessProxy.GetAllProductForSales(MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<ProductInformationForSales>().ToList();

            lstProductInfo = RemoveGridAddedProductQuantity(lstProductInfo);

            cmbProductInformation.DataSource = lstProductInfo;
            cmbProductInformation.DisplayMember = "ProductName";
            cmbProductInformation.ValueMember = "ProductId";
            cmbProductInformation.DisplayLayout.Bands[0].Columns[0].Hidden = true;
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

            lstpurchaseOrderDetail = DataAccessProxy.GetAllPurchaseOrderDetailProductByProductAndPurchaseID(cmbProductInformation.Value.ToString(), _purchaseID).Cast<PurchaseOrderDetail>().ToList();
            lstSalesOrderDetail = DataAccessProxy.GetAllSalesProductByProductAndPurchaseID(cmbProductInformation.Value.ToString(), _purchaseID).Cast<SalesOrderDetail>().ToList();

            decimal purchaseQuantity = CalculatePurchaseQuantity(lstpurchaseOrderDetail);
            decimal salesQuantity = CalculateSalesQuantity(lstSalesOrderDetail);
            decimal orderQuantity = Convert.ToDecimal(UltraQuantity.Value);

            if (orderQuantity > (purchaseQuantity - salesQuantity))
            {
                MessageBoxHelper.ShowInformation("There is no available product for this product please check.");
                UltraQuantity.Focus();
                return false;
            }
            return true;
        }

        public void WholeSales()
        {
            Load += WholeSales_Load;
        }

        private void txtPhone_Leave(object sender, EventArgs e)
        {
            GetCustomerIDByPhoneNumber(txtPhone.Text);
        }

        private void cmbProductInformation_TextChanged(object sender, EventArgs e)
        {
            //  LoadProductInformationWithLike(cmbProductInformation.Text);
        }

        private void cmbProductInformation_KeyUp(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 13 && cmbProductInformation.Text != string.Empty)
            {
                UltraQuantity.Focus();
            }
        }

        private void UltraQuantity_KeyUp(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 13)
            {
                if (ValidFormData())
                {
                    AddProductInGrid();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidFormData())
            {
                AddProductInGrid();
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


                if (UpdateSalesInformation() == (int)IFMSEnum.ReturnResult.Success)
                {
                    MessageBoxHelper.ShowInformation("Information saved successfully.");
                    grvCreditProductDetails.DataSource = BuildSalesDetailTable();
                    PrintInvoice(salesID);
                }
                else
                {
                    MessageBoxHelper.ShowInformation("Operation failed please try again.");
                }
            }
        }


        /// <summary>
        /// Method to validate insert data.
        /// </summary>
        /// <returns></returns>
        private bool ValdateInsertionData()
        {
            if (grvCreditProductDetails.Rows.Count < 2)
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


            //if (txtReceiveAmount.Text == string.Empty)
            //{
            //    MessageBoxHelper.ShowInformation("Check receive amount");
            //    txtReceiveAmount.Focus();
            //    return false;
            //}

            return true;
        }


        private bool IsBlankDataRow(UltraGridRow row)
        {

            foreach (UltraGridCell cell in row.Cells)
            {
                if (cell.Value == null || cell.Value.ToString() == string.Empty)
                {
                    UIHelper.MarkDataGridRowAsNormal(row);
                    if (cell.Column.Header.Caption != "VAT" && cell.Column.Header.Caption != "Delete")
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
                Product product = DataAccessProxy.GetProductByName(row.Cells["ProductName"].Value.ToString()).Where(p => p.BranchID == MTBFConstants.AppConstants.BranchID && p.OrganizationID == MTBFConstants.AppConstants.OrganizationID).Cast<Product>().FirstOrDefault();
                _purchaseID = Convert.ToInt32(row.Cells["PurchaseID"].Value);

                decimal salesQuantity = 0;


                List<PurchaseOrderDetail> lstpurchaseOrderDetail = new List<PurchaseOrderDetail>();
                List<SalesOrderDetail> lstSalesOrderDetail = new List<SalesOrderDetail>();

                lstpurchaseOrderDetail = DataAccessProxy.GetAllPurchaseOrderDetailProductByProductAndPurchaseID(product.ProductID, _purchaseID).Cast<PurchaseOrderDetail>().ToList();
                lstSalesOrderDetail = DataAccessProxy.GetAllSalesProductByProductAndPurchaseID(product.ProductID, _purchaseID).Cast<SalesOrderDetail>().ToList();

                decimal purchaseQuantity = CalculatePurchaseQuantity(lstpurchaseOrderDetail);
                salesQuantity = CalculateSalesQuantity(lstSalesOrderDetail);
                decimal orderQuantity = Convert.ToDecimal(row.Cells["Quantity"].Value);

                if (orderQuantity > (purchaseQuantity - salesQuantity))
                {
                    if (DialogResult.No == MessageBoxHelper.ShowConfirmation("The stock limit of this '" + product.ProductName + "' is exceeded." + Environment.NewLine + "Are you sure to continue?"))
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

        //private bool IsValidQuantity(DataGridViewRow row)
        //{
        //    if (row.Cells["ProductName"].Value != null)
        //    {
        //        UIHelper.MarkDataGridRowAsNormal(row);
        //        Product product = DataAccessProxy.GetProductByName(row.Cells["ProductName"].Value.ToString());
        //        decimal salesQuantity = 0;


        //        List<PurchaseOrderDetail> lstpurchaseOrderDetail = new List<PurchaseOrderDetail>();
        //        List<SalesOrderDetail> lstSalesOrderDetail = new List<SalesOrderDetail>();

        //        lstpurchaseOrderDetail = DataAccessProxy.GetAllPurchaseOrderDetailProductByProductAndPurchaseID(product.ProductID, _purchaseID).Cast<PurchaseOrderDetail>().ToList();
        //        lstSalesOrderDetail = DataAccessProxy.GetAllSalesProductByProductAndPurchaseID(product.ProductID, _purchaseID).Cast<SalesOrderDetail>().ToList();

        //        decimal purchaseQuantity = CalculatePurchaseQuantity(lstpurchaseOrderDetail);
        //        salesQuantity = CalculateSalesQuantity(lstSalesOrderDetail);
        //        decimal orderQuantity = Convert.ToDecimal(row.Cells["Quantity"].Value);

        //        if (orderQuantity > (purchaseQuantity - salesQuantity))
        //        {
        //            if (DialogResult.No == MessageBoxHelper.ShowConfirmation("The stock limit of this '" + product.ProductName + "' is exceeded." + Environment.NewLine + "Are you sure to continue?"))
        //            {
        //                UIHelper.MarkDataGridRowAsInvalid(row);
        //                return false;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        MessageBoxHelper.ShowInformation("Please delete invalid grid row.");
        //        UIHelper.MarkDataGridRowAsInvalid(row);
        //        return false;
        //    }

        //    return true;
        //}



        private void grvCreditProductDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{

            //        string celltext = grvCreditProductDetails. .OwningColumn.HeaderText;
            //        if (celltext == "Delete")
            //        {
            //            DialogResult m = MessageBoxHelper.ShowConfirmation("Do you want to delete?");
            //            if (m == System.Windows.Forms.DialogResult.Yes)
            //            {
            //                grvCreditProductDetails.Rows.Remove(grvCreditProductDetails.CurrentRow);
            //                CalculateTotalCost();
            //            }
            //        }


            //}
            //catch (Exception ex)
            //{
            //    MessageBoxHelper.ShowInformation("Operation fail Please try again");
            //}

        }

        private void cmbProductInformation_Leave(object sender, EventArgs e)
        {
            UltraGridRow row = cmbProductInformation.SelectedRow;
            if (row != null)
            {
                Product product = DataAccessProxy.GetProductByName(cmbProductInformation.Text).Where(p => p.BranchID == MTBFConstants.AppConstants.BranchID && p.OrganizationID == MTBFConstants.AppConstants.OrganizationID).Cast<Product>().FirstOrDefault();
                ProductSize productSize = DataAccessProxy.GetProductSizeByID(product.ProductSizeID);
                txtProductSize.Text = productSize.Name;
                _purchaseID = Convert.ToInt32(row.Cells["PurchaseID"].Value);
            }
        }

        private void grvCreditProductDetails_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            decimal quantity = 0;
            if (e.RowIndex != -1 && grvCreditProductDetails.Rows[e.RowIndex].Cells["ProductName"].Value != null)
            {
                Product product = new Product();
                if (grvCreditProductDetails.Rows[e.RowIndex].Cells["Quantity"].Value != null)
                {
                    decimal.TryParse(grvCreditProductDetails.Rows[e.RowIndex].Cells["Quantity"].Value.ToString(), out quantity);
                }

                if (grvCreditProductDetails.Rows[e.RowIndex].Cells["ProductName"].Value != null)
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
            LoadProductInformation();
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
            txtCustomerName.Clear();
            txtCustomerAddress.Clear();
            txtPhone.Clear();
            UltraQuantity.Value = 0;
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


            Product product = DataAccessProxy.GetProductInformationByID(productID);

            ProductSize productSize = DataAccessProxy.GetProductSizeByID(product.ProductSizeID);
            price = DataAccessProxy.GetSalesPriceByProductID(productID);
            Quantity = 1;

            if (IsExistingProduct(product, out rowIndex))
            {
                decimal previousQuantity = 0;
                decimal.TryParse(grvCreditProductDetails.Rows[rowIndex].Cells["Quantity"].Value.ToString(), out previousQuantity);
                decimal.TryParse(grvCreditProductDetails.Rows[rowIndex].Cells["SquareFeet"].Value.ToString(), out previousSquareFeet);

                vat = (price * (Quantity + previousQuantity)) / 100 * product.VAT;
                grvCreditProductDetails.Rows[rowIndex].Cells["Quantity"].Value = previousQuantity + 1;
                grvCreditProductDetails.Rows[rowIndex].Cells["SquareFeet"].Value = previousSquareFeet + Convert.ToDecimal(productSize.Name);
                grvCreditProductDetails.Rows[rowIndex].Cells["VAT"].Value = vat.ToString("00.00");
                total = ((price * (previousQuantity + 1)) + vat);
                grvCreditProductDetails.Rows[rowIndex].Cells["Total"].Value = total.ToString("00.00");
            }
            else
            {
                vat = (price * Quantity) / 100 * product.VAT;

                UltraGridRow row = grvCreditProductDetails.DisplayLayout.Bands[0].AddNew();

                vat = (price * Quantity) / 100 * product.VAT;
                row.Cells["ProductID"].Value = product.ProductID;
                row.Cells["ProductName"].Value = product.ProductName;
                row.Cells["Quantity"].Value = Quantity.ToString();
                row.Cells["SquareFeet"].Value = Convert.ToDecimal(txtSqureFeet.Text);
                row.Cells["SalesPrice"].Value = price.ToString();
                row.Cells["VAT"].Value = vat.ToString("F2");
                total = ((price * Quantity) + vat);
                row.Cells["Total"].Value = total.ToString("F2");
                row.Cells["PurchaseID"].Value = _purchaseID.ToString();

            }
            grvCreditProductDetails.DisplayLayout.Bands[0].Columns["ProductID"].Hidden = true;
            grvCreditProductDetails.DisplayLayout.Bands[0].Columns["PurchaseID"].Hidden = true;

            cmbProductInformation.Text = string.Empty;

            cmbProductInformation.Focus();
            UltraQuantity.Value = 0;
            CalculateTotalCost();

        }



        private void txtBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                ProductInformationForSales saleableproduct = lstProductInfo.Cast<ProductInformationForSales>().Where(p => p.Barcode == txtBarCode.Text).ToList().FirstOrDefault();
                if (saleableproduct != null)
                {
                    // _purchaseID = saleableproduct.PurchaseID;
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
            decimal.TryParse(txtTotal.Text, out total);
            decimal.TryParse(txtReceiveAmount.Text, out receiveAmount);
            decimal.TryParse(txtDiscount.Text, out discount);
            txtGrandTotal.Text = (total - discount).ToString();

            txtReceiveAmount.Text = 0.ToString();
            txtChange.Text = 0.ToString();
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
            decimal.TryParse(UltraQuantity.Value.ToString(), out quantity);
            decimal.TryParse(txtProductSize.Text, out size);

            txtSqureFeet.Text = (quantity * size).ToString();
        }

        private void grvCreditProductDetails_ClickCell(object sender, ClickCellEventArgs e)
        {
            if (e.Cell.Column.Header.Caption == "Quantity")
            {
                grvCreditProductDetails.Rows[e.Cell.Row.Index].Cells["Quantity"].Activate();
                grvCreditProductDetails.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode);
            }

            if (e.Cell.Column.Header.Caption == "SalesPrice")
            {
                grvCreditProductDetails.Rows[e.Cell.Row.Index].Cells["SalesPrice"].Activate();
                grvCreditProductDetails.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode);
            }
        }

        private void grvCreditProductDetails_ClickCellButton(object sender, CellEventArgs e)
        {
            if (e.Cell.Value.ToString() == "Delete")
            {
                grvCreditProductDetails.Rows[e.Cell.Row.Index].Delete();
                CalculateTotalSalesAmount();
            }
        }

        private void grvCreditProductDetails_CellChange(object sender, CellEventArgs e)
        {
            decimal quantity = 0;
            decimal salesPrice = 0;
            if (grvCreditProductDetails.Rows[e.Cell.Row.Index].Cells["Quantity"].Value != null)
            {
                decimal.TryParse(grvCreditProductDetails.Rows[e.Cell.Row.Index].Cells["Quantity"].Text, out quantity);
                decimal.TryParse(grvCreditProductDetails.Rows[e.Cell.Row.Index].Cells["SalesPrice"].Text, out salesPrice);
                Product product = DataAccessProxy.GetProductInformationByID(grvCreditProductDetails.Rows[e.Cell.Row.Index].Cells["ProductID"].Value.ToString());
                ProductSize pSize = DataAccessProxy.GetProductSizeByID(product.ProductSizeID);
                grvCreditProductDetails.Rows[e.Cell.Row.Index].Cells["SquareFeet"].Value = quantity * Convert.ToDecimal(pSize.Name);
                grvCreditProductDetails.Rows[e.Cell.Row.Index].Cells["Total"].Value = salesPrice * (quantity * Convert.ToDecimal(pSize.Name));
            }
            CalculateTotalCost();
            CalculateTotalSalesAmount();
        }
    }

}