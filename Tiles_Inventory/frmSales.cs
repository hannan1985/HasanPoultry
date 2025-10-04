using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IFMS.Facade;

using IMFS.Common.Utility;
using IMFS.Common.DTO;
using IMFS.Common.View;
using Infragistics.Win.UltraWinGrid;
using NybSys.MTBF.Utility.Helper;
using System.IO;
using Tiles_Inventory.Reports;
using NybSys.MTBF.Utility.Constants;
using IFMS.BLL;
using NybSys.MTBF.Utility.Enums;

namespace Tiles_Inventory
{
    public partial class frmSales : BaseForm
    {

        private string pharmacyName;
        private string pharmacyAddress;
        //  private int _purchaseID = 0;
        private List<ProductInformationForSales> lstProductInfo = new List<ProductInformationForSales>();
        private VMSales _vmSales = new VMSales();
        System.Threading.Thread thread = null;


        public frmSales()
        {
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
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


        private class ProductSock
        {
            public string ProductID { get; set; }
            public string ProductName { get; set; }
            public string Model { get; set; }
            public decimal Quantity { get; set; }
            public string Type { get; set; }
            public decimal OldPrice { get; set; }
            public decimal Price { get; set; }
        }







        /// <summary>
        /// Method to check valid data.
        /// </summary>
        /// <returns></returns>
        private bool ValidFormData()
        {
            if (cmbProductInformation.Text == string.Empty)
            {
                MessageBoxHelper.ShowInformation("You need to select product name.");
                cmbProductInformation.Focus();
                return false;
            }

            if (txtQuantity.Text == string.Empty)
            {
                MessageBoxHelper.ShowInformation("You need to enter quantity.");
                txtQuantity.Focus();
                return false;
            }


            if ((cmbProductInformation.Value == null))
            {

                MessageBoxHelper.ShowInformation("You need to select product name.");
                cmbProductInformation.Focus();
                return false;
            }

            if (Convert.ToInt32(txtQuantity.Value) == 0)
            {
                MessageBoxHelper.ShowInformation("Quantity can't be zero.");
                txtQuantity.Focus();
                return false;
            }

            return true;
        }






        /// <summary>
        /// Method to add product information in grid.
        /// </summary>
        private void AddProductInGrid()
        {
            decimal squareFeet = 0;
            decimal Quantity = 0;
            decimal price = default(decimal);
            price = DataAccessProxy.GetSalesPriceByProductID(cmbProductInformation.Value.ToString());
            ProductInformationForSales product = lstProductInfo.Where(p => p.ProductID == cmbProductInformation.Value.ToString()).FirstOrDefault();
            decimal.TryParse(txtQuantity.Text, out Quantity);
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
            txtBarCode.Focus();

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
        /// Method to calculate total cost.
        /// </summary>
        private void CalculateTotalCost()
        {
            decimal sum = 0;
            decimal discount = 0;

            foreach (SalesOrderDetail sDetail in _vmSales.lstSalesOrderDetail)
            {
                sum += (sDetail.SquareFeet > 0) ? sDetail.Price * sDetail.SquareFeet : sDetail.Price * sDetail.Quantity;
            }

            decimal.TryParse(txtDiscount.Text, out discount);

            txtTotal.Text = Math.Floor(sum).ToString();
            txtReceiveAmount.Text = 0.ToString().ToString();
            txtChange.Text = 0.ToString().ToString();
            txtGrandTotal.Text = Math.Floor((discount > 0) ? ((sum) - discount) : (sum)).ToString();
        }

        private bool IsValidQuantity(DataGridViewRow row)
        {
            if (row.Cells["ProductName"].Value != null)
            {
                UIHelper.MarkDataGridRowAsNormal(row);
                Product product = DataAccessProxy.GetProductByName(row.Cells["ProductName"].Value.ToString()).Cast<Product>().ToList().FirstOrDefault();
                decimal salesQuantity = 0;


                List<PurchaseOrderDetail> lstpurchaseOrderDetail = new List<PurchaseOrderDetail>();
                List<SalesOrderDetail> lstSalesOrderDetail = new List<SalesOrderDetail>();

                lstpurchaseOrderDetail = DataAccessProxy.GetPurchaseOrderDetailByProductID(product.ProductID, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<PurchaseOrderDetail>().ToList();
                lstSalesOrderDetail = DataAccessProxy.GetSalesOrderDetailByProductID(product.ProductID, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<SalesOrderDetail>().ToList();

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

        /// <summary>
        /// Method to reset all controlls.
        /// </summary>
        private void Clear()
        {
            txtTotal.Clear();
            txtDiscount.Clear();
            txtReceiveAmount.Clear();
            cmbProductInformation.Focus();
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



        /// <summary>
        /// Method to check save data.
        /// </summary>
        /// <returns></returns>
        private bool ValidFormSaveData()
        {
            decimal salesAmount = 0;
            decimal receiveAmount = 0;
            decimal.TryParse(txtReceiveAmount.Text, out receiveAmount);
            decimal.TryParse(txtGrandTotal.Text, out salesAmount);
            if (receiveAmount < salesAmount)
            {
                MessageBoxHelper.ShowInformation("Receive amount can't be less than sales amount.");
                txtReceiveAmount.Focus();
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


            salesID = InsertSalesOrderInformation(salesAmount, discount, receiveAmount);

            result = salesID;
            return result;
        }


        private int InsertSalesOrderInformation(decimal salesAmount, decimal discount, decimal receiveAmount)
        {
            decimal carryingAndLoading = 0;
            int salesRepresentativeID = 0;


            _vmSales.SalesOrder.BillNumber = txtBillNumber.Text;
            _vmSales.SalesOrder.SalesDate = dtpSalesDate.Value;
            _vmSales.SalesOrder.SalesAmount = salesAmount;
            _vmSales.SalesOrder.EmployeeID = IFMSConstant.LoggedinUserID;
            _vmSales.SalesOrder.Discount = discount;
            _vmSales.SalesOrder.BranchID = MTBFConstants.AppConstants.BranchID;
            _vmSales.SalesOrder.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            _vmSales.SalesOrder.ReceiveAmount = receiveAmount;
            _vmSales.SalesOrder.CarryingLoading = carryingAndLoading;
            _vmSales.SalesOrder.SalesRepresentativeID = salesRepresentativeID;


            _vmSales.SalesOrder.Status = (_vmSales.SalesOrder.Status != (int)MTBFEnums.DeliveryStatus.Delivered) ? (int)MTBFEnums.DeliveryStatus.NotDelivered : _vmSales.SalesOrder.Status;

            return new SalesManager().SaveSalesInformation(_vmSales);

            // return DataAccessProxy.InsertSalesOrderDetail(salesOrder);
        }


        private void SetPharmachyInforamation(ref string PharmacyName, ref string Address, ref Organization pharmacy)
        {
            pharmacy = DataAccessProxy.GetAllPharmacy().Cast<Organization>().ToList().FirstOrDefault();
            Branch branch = DataAccessProxy.GetBracnhByID(MTBFConstants.AppConstants.BranchID);
            if (branch != null)
            {
                PharmacyName = branch.BranchName;
                Address = branch.Address + ", " + branch.Phone + ", " + branch.Fax;
            }
        }




        /// <summary>
        /// Method to insert journal information for cash sales.
        /// </summary>
        /// <returns></returns>
        public int InsertJournalInformationForCashSales(int salesID)
        {
            int result = 0;
            int referenceID = 1;
            decimal amount = Convert.ToDecimal(txtReceiveAmount.Text);


            ChildAccount childAccount = null;
            referenceID = DataAccessProxy.GetJournalReferenceID();

            for (int i = 0; i <= 1; i++)
            {
                IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();

                if (i == 0)
                {
                    journal.DrCrIndecator = "Dr";
                    journal.AccountID = IFMSConstant.CashAccountID;

                }
                else
                {
                    journal.DrCrIndecator = "Cr";
                    childAccount = DataAccessProxy.GetChildAccountByID(IFMSConstant.GoodsSalesAccountID);
                    if (childAccount != null)
                    {
                        journal.AccountID = childAccount.AccountID;
                        journal.ChildAccountID = IFMSConstant.GoodsSalesAccountID;
                    }
                    else
                    {
                        journal.AccountID = IFMSConstant.GoodsSalesAccountID;
                    }

                }

                journal.JAccountID = 0;
                journal.Amount = amount;
                journal.ReferenceNo = referenceID;
                journal.SalesID = salesID;
                journal.BranchID = MTBFConstants.AppConstants.BranchID;
                journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                result = DataAccessProxy.InsertJournal(journal);
            }

            return result;
        }




        private decimal CalculatePriceByProductID(string productID)
        {
            decimal purchasePrice = 0;
            decimal totalQuantity = 0;
            decimal totalPrice = 0;
            foreach (PurchaseOrderDetail orderDetail in DataAccessProxy.GetPurchaseOrderDetailByProductID(productID, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID))
            {
                totalQuantity = totalQuantity + orderDetail.Quantity;
                totalPrice = totalPrice + (orderDetail.PurchasePrice * orderDetail.Quantity);
            }

            purchasePrice = totalPrice / totalQuantity;
            return purchasePrice;

        }




        private void cmbProductInformation_TextChanged(object sender, EventArgs e)
        {
            // LoadProductInformationWithLike(cmbProductInformation.Text);
        }

        private void cmbProductInformation_KeyUp(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 13 && cmbProductInformation.Text != string.Empty)
            {
                txtQuantity.Focus();
            }
            else
            {

            }
        }

        private void UltraQuantity_KeyUp(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 13)
            {
                if (ValidFormData())
                {
                    //if (CheckValidOrderQuantity())
                    //{

                    //}
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
                btSave.Enabled = false;
                salesID = InsertSalesInformationNew();

                if (salesID > 0)
                {
                    btSave.Enabled = true;
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
                }
                else
                {
                    MessageBoxHelper.ShowInformation("Failed to save sales information.");
                    btSave.Enabled = true;
                }
            }
        }

        private void ResetAllControls()
        {
            grvCreditProductDetails.DataSource = BuildSalesDetailTable();
            txtGrandTotal.Clear();
            txtDiscount.Clear();
            txtReceiveAmount.Clear();
            txtGrandTotal.Clear();
            txtTotal.Clear();
            base.IsUpdating = false;
            _vmSales = new VMSales();
        }


        /// <summary>
        /// Method to validate insert data.
        /// </summary>
        /// <returns></returns>
        private bool ValdateInsertionData()
        {
            //if (string.IsNullOrEmpty(txtBillNumber.Text))
            //{
            //    MessageBoxHelper.ShowInformation("You need to enter memo number");
            //    txtBillNumber.Focus();
            //    return false;
            //}
            //else
            //{
            //    string filter = string.Empty;
            //    filter = string.Format("{0}='{1}' And {2}={3}", "BillNumber", txtBillNumber.Text.Trim(), "BranchID", MTBFConstants.AppConstants.BranchID);
            //    SalesOrder salesOrder = new SalesManager().GetFilteredSalesInformation(filter).FirstOrDefault();
            //    if (salesOrder != null && salesOrder.SalesID != _vmSales.SalesOrder.SalesID)
            //    {
            //        MessageBoxHelper.ShowInformation("Duplicate memo number.");
            //        txtBillNumber.Focus();
            //        return false;
            //    }
            //}




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


        private bool IsBlankDataRow(DataGridViewRow row)
        {

            foreach (DataGridViewCell cell in row.Cells)
            {
                if (cell.Value == null || cell.Value.ToString() == string.Empty || cell.Value.ToString() == "0")
                {
                    UIHelper.MarkDataGridRowAsNormal(row);
                    if (cell.OwningColumn.HeaderText != "VAT" && cell.OwningColumn.HeaderText != "Delete")
                    {
                        MessageBoxHelper.ShowInformation(cell.OwningColumn.HeaderText + " " + "Can't be blank");
                        UIHelper.MarkDataGridRowAsInvalid(row);
                        return true;
                    }
                }
            }
            return false;
        }

        private bool IsValidGridRow(DataGridViewRow row)
        {
            decimal quantity = 0;
            decimal salesPrice = 0;

            foreach (DataGridViewCell cell in row.Cells)
            {
                if (cell.Value != null)
                    if (cell.OwningColumn.HeaderText == "Quantity")
                    {
                        decimal.TryParse(cell.Value.ToString(), out quantity);
                    }
            }

            return true;
        }

        private void grvCreditProductDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmSales_Load(object sender, EventArgs e)
        {
            // LoadProductInformation();
            LoadProductInformation();
            txtBarCode.Focus();
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

        private decimal CalculatePurchaseQuantity(List<PurchaseOrderDetail> lstpurchaseOrderDetail)
        {
            decimal purchaseQuantity = 0;
            foreach (PurchaseOrderDetail product in lstpurchaseOrderDetail)
            {
                purchaseQuantity = purchaseQuantity + product.Quantity;
            }
            return purchaseQuantity;
        }

        private decimal CalculateSalesQuantity(List<SalesOrderDetail> lstSalesOrderDetail)
        {
            decimal salesQuantity = 0;
            foreach (SalesOrderDetail product in lstSalesOrderDetail)
            {
                salesQuantity = salesQuantity + product.Quantity;
            }
            return salesQuantity;
        }


        private bool CheckValidOrderQuantity()
        {
            List<PurchaseOrderDetail> lstpurchaseOrderDetail = new List<PurchaseOrderDetail>();
            List<SalesOrderDetail> lstSalesOrderDetail = new List<SalesOrderDetail>();

            lstpurchaseOrderDetail = DataAccessProxy.GetPurchaseOrderDetailByProductID(cmbProductInformation.Value.ToString(), MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<PurchaseOrderDetail>().ToList();
            lstSalesOrderDetail = DataAccessProxy.GetSalesOrderDetailByProductID(cmbProductInformation.Value.ToString(), MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<SalesOrderDetail>().ToList();

            decimal purchaseQuantity = CalculatePurchaseQuantity(lstpurchaseOrderDetail);
            decimal salesQuantity = CalculateSalesQuantity(lstSalesOrderDetail);
            decimal orderQuantity = Convert.ToDecimal(txtQuantity.Value);

            if (orderQuantity > (purchaseQuantity - salesQuantity))
            {
                MessageBoxHelper.ShowInformation("There is no available product for this product please check.");
                txtQuantity.Focus();
                return false;
            }
            return true;
        }

        private void cmbProductInformation_Leave(object sender, EventArgs e)
        {
            UltraGridRow row = cmbProductInformation.SelectedRow;
            if (row != null)
            {
                // _purchaseID = Convert.ToInt32(row.Cells["PurchaseID"].Value);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadProductInformation();
        }

        private void grvCreditProductDetails_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            CalculateTotalCost();
        }

        private void cmbProductInformation_Click(object sender, EventArgs e)
        {
            // LoadProductInformation();
        }

        private void cmbProductInformation_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            UltraGridLayout layout = e.Layout;
            layout.PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
        }

        private void cmbProductInformation_KeyPress(object sender, KeyPressEventArgs e)
        {
            cmbProductInformation.DropDownSearchMethod = DropDownSearchMethod.Linear;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            LoadProductInformation();
            txtTotal.Clear();
            txtDiscount.Clear();
            txtReceiveAmount.Clear();
            txtQuantity.Value = 0;
        }

        private void txtBarCode_KeyDown(object sender, KeyEventArgs e)
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


        private bool CheckValidOrderQuantityForBarCode()
        {
            List<PurchaseOrderDetail> lstpurchaseOrderDetail = new List<PurchaseOrderDetail>();
            List<SalesOrderDetail> lstSalesOrderDetail = new List<SalesOrderDetail>();

            ProductInformationForSales saleableproduct = lstProductInfo.Cast<ProductInformationForSales>().Where(p => p.Barcode == txtBarCode.Text).ToList().FirstOrDefault();

            if (saleableproduct != null)
            {
                lstpurchaseOrderDetail = DataAccessProxy.GetPurchaseOrderDetailByProductID(saleableproduct.ProductID, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<PurchaseOrderDetail>().ToList();
                lstSalesOrderDetail = DataAccessProxy.GetSalesOrderDetailByProductID(saleableproduct.ProductID, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<SalesOrderDetail>().ToList();

                decimal purchaseQuantity = CalculatePurchaseQuantity(lstpurchaseOrderDetail);
                decimal salesQuantity = CalculateSalesQuantity(lstSalesOrderDetail);
                decimal orderQuantity = Convert.ToDecimal(txtQuantity.Value);

                if (orderQuantity > (purchaseQuantity - salesQuantity))
                {
                    MessageBoxHelper.ShowInformation("There is no available product for this product please check.");
                    txtQuantity.Focus();
                    return false;
                }
            }
            else
            {
                MessageBoxHelper.ShowInformation("There is no product for this bar code");
                return false;
            }

            return true;
        }



        private void txtReceiveAmount_TextChanged(object sender, EventArgs e)
        {
            decimal grandTotal = 0;
            decimal receiveAmount = 0;

            decimal.TryParse(txtGrandTotal.Text, out grandTotal);
            decimal.TryParse(txtReceiveAmount.Text, out receiveAmount);
            txtChange.Text = (receiveAmount - grandTotal).ToString("F2");

        }

        private void txtReceiveAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ("1234567890.".IndexOf(e.KeyChar) > -1 | e.KeyChar == Convert.ToChar(8))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ("1234567890.".IndexOf(e.KeyChar) > -1 | e.KeyChar == Convert.ToChar(8))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void UltraGroupBox1_Click(object sender, EventArgs e)
        {

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


            _vmSales.lstSalesOrderDetail = new List<SalesOrderDetail>();
            foreach (UltraGridRow row in grvCreditProductDetails.Rows)
            {
                SalesOrderDetail sDetail = new SalesOrderDetail();
                decimal.TryParse(row.Cells["SalesPrice"].Text, out  salesPrice);
                decimal.TryParse(row.Cells["Quantity"].Text, out  quantity);

                sDetail.Price = salesPrice;
                sDetail.Quantity = quantity;
                sDetail.ProductID = row.Cells["ProductID"].Text;
                sDetail.ProductName = row.Cells["ProductName"].Text;
                sDetail.IsFree = (salesPrice == 0) ? true : false;
                _vmSales.lstSalesOrderDetail.Add(sDetail);
            }


            CalculateTotalCost();
        }



    }
}
