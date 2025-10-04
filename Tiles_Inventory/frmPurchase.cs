using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using IFMS.Facade;
using IMFS.Common.DTO;
using IMFS.Common.Utility;
using IMFS.Common.View;
using Infragistics.Win.UltraWinGrid;
using NybSys.MTBF.Utility.Helper;
using NybSys.MTBF.Utility.Constants;

using System.IO;
using System.Drawing;

using System.Diagnostics;
using IFMS.BLL;
using Tiles_Inventory.Reports;

namespace Tiles_Inventory
{
    public partial class frmPurchase : BaseForm
    {
        private BackgroundWorker backgroundWorker1 = new BackgroundWorker();
        private int _purchaseOrderID = 0;
        private List<PurchaseOrderDetail> _lstPurcahseOrderDetail = new List<PurchaseOrderDetail>();
        private PurchaseOrder _purchaseOrder = new PurchaseOrder();
        private string pharmacyName;
        private string pharmacyAddress;
        public delegate void LoadEventHandaler();
        public event LoadEventHandaler OnLoadPurchaseInformation;
        public bool isUpdateLoad = false;
        List<Product> lstProductInformation = new List<Product>();
        VMPurchase _vmPurchase = new VMPurchase();

        public frmPurchase()
        {
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        public frmPurchase(PurchaseOrder purchaseOrder, bool isEdit, List<PurchaseOrderDetail> lstPurcahseOrderDetail)
        {
            _vmPurchase.lstPurchaseOrderDetail = lstPurcahseOrderDetail;
            _vmPurchase.PurchaseOrder = purchaseOrder;
            IsUpdating = isEdit;
            DataAccessProxy = new FacadeManager();
            InitializeComponent();
        }

        private void frmPurchase_Load(object sender, EventArgs e)
        {
            txtInvoiceNumber.Focus();
            this.LoadSupplierNameCombo();
            LoadProductInformationCombo();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            if (base.IsUpdating)
            {
                isUpdateLoad = false;
                LoadExistingPurchaseOrderInformation();
                LoadExistingPurchaseOrderDetailInformation();
                isUpdateLoad = true;
            }
        }

        /// <summary>
        /// Method to load supplier information in combo box.
        /// </summary>
        private void LoadSupplierNameCombo()
        {
            List<Supplier> lstSupplier = new List<Supplier>();
            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("Name");

            DataRow emptyRow = table.NewRow();
            emptyRow[0] = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            emptyRow[1] = "-Select-";
            table.Rows.Add(emptyRow);

            lstSupplier = new SupplierManager().GetAllSupplierByBranchID(MTBFConstants.AppConstants.BranchID);

            if (lstSupplier.Count > 0)
            {
                foreach (Supplier supplier in lstSupplier)
                {
                    DataRow row = table.NewRow();
                    row[0] = supplier.SupplierID;
                    row[1] = supplier.SupplierName;
                    table.Rows.Add(row);
                }

                CmbSupplierName.ValueMember = "ID";
                CmbSupplierName.DisplayMember = "Name";
                CmbSupplierName.DataSource = table;
                CmbSupplierName.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
                CmbSupplierName.Enabled = true;
            }
        }

        /// <summary>
        /// Method to insert purchase order information.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private int SavePurchaseOrderInformation()
        {
            Employee employee = new Employee();
            _vmPurchase.PurchaseOrder.InvoiceNumber = txtInvoiceNumber.Text;
            _vmPurchase.PurchaseOrder.PurchaseDate = dtpPurchaseDate.Value;
            _vmPurchase.PurchaseOrder.CompanyID = 1;// Convert.ToInt32(cmbCompanyName.Value);
            _vmPurchase.PurchaseOrder.SupplierID = Convert.ToInt32(CmbSupplierName.Value);
            _vmPurchase.PurchaseOrder.CompanyName = cmbCompanyName.Text;
            _vmPurchase.PurchaseOrder.SupplierName = CmbSupplierName.Text;
            _vmPurchase.PurchaseOrder.PurchaseAmount = txtTotalAmount.Text == string.Empty ? 0 : Convert.ToDecimal(txtTotalAmount.Text);
            _vmPurchase.PurchaseOrder.Status = (int)IFMSEnum.PurchaseOrderStatus.Issued;
            employee = DataAccessProxy.GetEmployeeByID(IFMSConstant.LoggedinUserID);
            _vmPurchase.PurchaseOrder.SalesmenName = (employee != null) ? employee.EmployeeName : string.Empty;
            _vmPurchase.PurchaseOrder.PaidAmount = (txtPaidAmount.Text == string.Empty ? 0 : Convert.ToDecimal(txtPaidAmount.Text));
            _vmPurchase.PurchaseOrder.BranchID = MTBFConstants.AppConstants.BranchID;
            _vmPurchase.PurchaseOrder.OrganizationID = MTBFConstants.AppConstants.OrganizationID;

            return new PurchaseManager().SavePurchaseOrderInformation(_vmPurchase.PurchaseOrder, _vmPurchase.lstPurchaseOrderDetail);
        }





        /// <summary>
        /// Method to get pack size information.
        /// </summary>
        /// <param name="productID"></param>
        private void GetPackSizeByProductID(string productID)
        {
            Product product = DataAccessProxy.GetProductInformationByID(productID);
            if (product != null)
            {
                ProductSize productSize = DataAccessProxy.GetProductSizeByID(product.ProductSizeID);
                txtPackSize.Text = (product != null) ? product.PackSize : string.Empty;
                txtProductSize.Text = (productSize != null) ? productSize.Name : string.Empty;
            }
            else
            {
                MessageBoxHelper.ShowInformation("Invalid product selection.");
                cmbProductName.Focus();
            }
        }

        /// <summary>
        /// Method to load company informaiton in combo box.
        /// </summary>
        private void LoadCompanyName()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("Name");

            DataRow emptyRow = table.NewRow();
            emptyRow[0] = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            emptyRow[1] = "-Select-";
            table.Rows.Add(emptyRow);

            List<Company> lstCompany = new List<Company>();
            lstCompany = DataAccessProxy.GetAllCompany().Cast<Company>().ToList();

            foreach (Company company in lstCompany)
            {
                DataRow row = table.NewRow();
                row[0] = company.CompanyID;
                row[1] = company.CompanyName;
                table.Rows.Add(row);
            }

            cmbCompanyName.DisplayMember = "Name";
            cmbCompanyName.ValueMember = "ID";
            cmbCompanyName.DataSource = table;
            cmbCompanyName.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            cmbCompanyName.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
            cmbCompanyName.DisplayLayout.PerformAutoResizeColumns(false, PerformAutoSizeType.AllRowsInBand, true);
        }

        private void btnClose_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Method to valid form data.
        /// </summary>
        /// <returns></returns>
        private bool ValidFormData()
        {

            decimal cartonQty = 0;

            if (txtInvoiceNumber.Text == string.Empty)
            {
                MessageBoxHelper.ShowInformation("You need to enter invoice number.");
                txtInvoiceNumber.Focus();
                return false;
            }

            decimal.TryParse(txtCarton.Text, out cartonQty);

            //if ((Convert.ToInt32(cmbCompanyName.Value) == MTBFConstants.DataField.COMBO_DEFAULT_ID))
            //{
            //    MessageBoxHelper.ShowInformation("You need to select company name.");
            //    cmbCompanyName.Focus();
            //    return false;
            //}

            if ((Convert.ToInt32(CmbSupplierName.Value) == MTBFConstants.DataField.COMBO_DEFAULT_ID))
            {
                MessageBoxHelper.ShowInformation("You need to select supplier name.");
                CmbSupplierName.Focus();
                return false;
            }

            if ((cmbProductName.Value == null))
            {
                MessageBoxHelper.ShowInformation("You need to select product name.");
                cmbProductName.Focus();
                return false;
            }

            if (txtPurchasePrice.Text == string.Empty)
            {
                MessageBoxHelper.ShowInformation("You need to enter purchase price."); ;
                txtPurchasePrice.Focus();
                return false;
            }

            if (txtSalesPrice.Text == string.Empty)
            {
                MessageBoxHelper.ShowInformation("You need to enter sales price.");
                txtSalesPrice.Focus();
                return false;
            }

            //if (MTBFConstants.AppConstants.BranchID == 2)
            //{

            //    if (cartonQty == 0)
            //    {
            //        MessageBoxHelper.ShowInformation("You need to enter carton quantity.");
            //        txtCarton.Focus();
            //        return false;
            //    }
            //}


            if (txtQuantity.Text == string.Empty)
            {
                MessageBoxHelper.ShowInformation("You need to enter quantity.");
                txtQuantity.Focus();
                return false;
            }

            //if (Convert.ToDecimal(txtPurchasePrice.Text) == 0)
            //{
            //    MessageBoxHelper.ShowInformation("Purchase price can't be zero.");
            //    txtPurchasePrice.Focus();
            //    return false;
            //}

            if (Convert.ToDecimal(txtSalesPrice.Text) == 0)
            {
                MessageBoxHelper.ShowInformation("Sales price can't be zero.");
                txtSalesPrice.Focus();
                return false;
            }

            if (Convert.ToDecimal(txtQuantity.Text) == 0)
            {
                MessageBoxHelper.ShowInformation("Quantity can't be zero.");
                txtQuantity.Focus();
                return false;
            }



            return true;
        }

        /// <summary>
        /// Method to check grid row is blank or not
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private bool IsBlankDataRow(DataGridViewRow row)
        {

            foreach (DataGridViewCell cell in row.Cells)
            {
                UIHelper.MarkDataGridRowAsNormal(row);
                if (cell.Value == null || cell.Value.ToString() == string.Empty || cell.Value.ToString() == "0")
                {
                    if (cell.OwningColumn.HeaderText != "Product Name" && cell.OwningColumn.ToolTipText != "Delete" && cell.OwningColumn.HeaderText != "Barcode" && cell.OwningColumn.HeaderText != "Square Feet")
                    {
                        MessageBoxHelper.ShowInformation("You need to delete invalid grid row.");
                        UIHelper.MarkDataGridRowAsInvalid(row);
                        return false;
                    }

                }
            }
            return true;
        }

        /// <summary>
        /// Method to validate insert data.
        /// </summary>
        /// <returns></returns>
        private bool ValdateInsertionData()
        {
            if (txtInvoiceNumber.Text == string.Empty)
            {
                MessageBoxHelper.ShowInformation("You need to enter invoice number.");
                txtInvoiceNumber.Focus();
                return false;
            }



            if ((Convert.ToInt32(CmbSupplierName.Value) == MTBFConstants.DataField.COMBO_DEFAULT_ID))
            {
                MessageBoxHelper.ShowInformation("You need to select supplier name.");
                CmbSupplierName.Focus();
                return false;
            }


            if (grvPurchaseDetails.Rows.Count == 0)
            {
                MessageBoxHelper.ShowInformation("Please add information in grid.");
                return false;
            }
            foreach (UltraGridRow row in grvPurchaseDetails.Rows)
            {
                if (IsBlankDataRow(row))
                {
                    return false;
                }
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
                    if (cell.Column.Header.Caption != "Delete" && cell.Column.Header.Caption != "Carton" && cell.Column.Header.Caption != "PurchasePrice")
                    {
                        MessageBoxHelper.ShowInformation(cell.Column.Header.Caption + " " + "Can't be blank");
                        UIHelper.MarkDataGridRowAsInvalid(row);
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Method to add product in grid.
        /// </summary>
        private void AddProductInGrid()
        {
            decimal Quantity = 0;
            decimal PurchasePrice = 0;
            decimal MRP = 0;
            decimal UnitPrice = 0;
            decimal UnitMRP = 0;
            decimal vat = 0;

            Quantity = Convert.ToDecimal(txtQuantity.Text);
            PurchasePrice = Convert.ToDecimal(txtPurchasePrice.Text);
            MRP = Convert.ToDecimal(txtSalesPrice.Text);

            UnitPrice = PurchasePrice + vat;
            UnitMRP = MRP;
            Product product = lstProductInformation.Where(p => p.ProductID == cmbProductName.Value.ToString()).FirstOrDefault();
            decimal carton = 0;
            decimal.TryParse(txtCarton.Text, out carton);

            PurchaseOrderDetail purchaseOrderDetail = _vmPurchase.lstPurchaseOrderDetail.Where(p => p.ProductID == product.ProductID).FirstOrDefault();
            if (purchaseOrderDetail != null)
            {
                purchaseOrderDetail = new PurchaseOrderDetail();
                purchaseOrderDetail.ProductID = product.ProductID;
                purchaseOrderDetail.ProductName = product.ProductName;
                purchaseOrderDetail.Carton = carton;
                purchaseOrderDetail.Quantity = purchaseOrderDetail.Quantity + Quantity;
                purchaseOrderDetail.PurchasePrice = UnitPrice;
                purchaseOrderDetail.SalesPrice = UnitMRP;
                purchaseOrderDetail.Total = (UnitPrice * Quantity);
            }
            else
            {
                purchaseOrderDetail = new PurchaseOrderDetail();
                purchaseOrderDetail.ProductID = product.ProductID;
                purchaseOrderDetail.ProductName = product.ProductName;
                purchaseOrderDetail.Quantity = Quantity;
                purchaseOrderDetail.PurchasePrice = UnitPrice;
                purchaseOrderDetail.Carton = carton;
                purchaseOrderDetail.SalesPrice = UnitMRP;
                purchaseOrderDetail.Total = (UnitPrice * Quantity);
            }

            _vmPurchase.lstPurchaseOrderDetail.Add(purchaseOrderDetail);

            LoadDataInGrid(_vmPurchase.lstPurchaseOrderDetail);

            ResetControl();
        }



        private DataTable BuildPurcahseTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ProductID");
            dt.Columns.Add("ProductName");
            dt.Columns.Add("Carton");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("PurchasePrice");
            dt.Columns.Add("SalesPrice");
            dt.Columns.Add("Total");
            dt.Columns.Add("Delete");


            return dt;
        }



        private void LoadDataInGrid(List<PurchaseOrderDetail> lstPurchaseOrderDetail)
        {
            grvPurchaseDetails.DataSource = BuildPurcahseTable();
            decimal sum = 0;
            foreach (PurchaseOrderDetail pDetail in lstPurchaseOrderDetail)
            {
                UltraGridRow row = grvPurchaseDetails.DisplayLayout.Bands[0].AddNew();
                row.Cells["ProductID"].Value = pDetail.ProductID;
                row.Cells["ProductName"].Value = pDetail.ProductName;
                row.Cells["Carton"].Value = pDetail.Carton;
                row.Cells["Quantity"].Value = pDetail.Quantity.ToString();
                row.Cells["PurchasePrice"].Value = pDetail.PurchasePrice.ToString();
                row.Cells["SalesPrice"].Value = pDetail.SalesPrice;
                row.Cells["Total"].Value = (pDetail.PurchasePrice * pDetail.Quantity).ToString("F2");
                row.Cells["Delete"].Value = "Delete";
                sum = sum + (pDetail.PurchasePrice * pDetail.Quantity);
            }

            txtTotalAmount.Text = sum.ToString("F2");
            grvPurchaseDetails.DisplayLayout.Bands[0].Columns["ProductID"].Hidden = true;

            grvPurchaseDetails.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
            grvPurchaseDetails.DisplayLayout.PerformAutoResizeColumns(false, PerformAutoSizeType.AllRowsInBand, true);

            if (MTBFConstants.AppConstants.BranchID == 1)
            {
                grvPurchaseDetails.DisplayLayout.Bands[0].Columns["Carton"].Hidden = true;
            }

        }

        /// <summary>
        /// Method to load product information in combo box.
        /// </summary>
        /// <param name="supplierID"></param>
        /// <remarks></remarks>
        //private void GetProductInformationByCompanyID(int companyID, string productName)
        //{
        //    try
        //    {

        //        lstProductInformation = DataAccessProxy.GetProductInformationByCompanyID(companyID).Cast<ProductInformationForPurchase>().Where(p => p.ProductName.ToLower().StartsWith(productName.ToLower())).ToList();

        //        cmbProductName.DisplayMember = "ProductName";
        //        cmbProductName.ValueMember = "ProductId";
        //        cmbProductName.DataSource = lstProductInformation;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Product information load fail.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    }
        //}


        private void LoadProductInformationCombo()
        {
            DataTable productTable = new DataTable();
            productTable.Columns.Add("ProductID");
            productTable.Columns.Add("Product Name");
            productTable.Columns.Add("Type");
            productTable.Columns.Add("Model");

            lstProductInformation = new ProductManager().GetAllProductByBranchAndOrganizationID(MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID);

            foreach (Product product in lstProductInformation)
            {
                DataRow row = productTable.NewRow();
                row["ProductID"] = product.ProductID;
                row["Product Name"] = product.ProductName;
                row["Type"] = product.TypeName;
                row["Model"] = product.ProductModelName;
                productTable.Rows.Add(row);
            }

            cmbProductName.DisplayMember = "Product Name";
            cmbProductName.ValueMember = "ProductID";
            cmbProductName.DataSource = productTable;
            cmbProductName.DisplayLayout.Bands[0].Columns["ProductID"].Hidden = true;

        }


        /// <summary>
        /// Method to calculate total purchase cost.
        /// </summary>
        /// <returns></returns>
        private decimal CalculateTotalPurchaseCost()
        {
            decimal sum = 0;
            decimal total = 0;
            decimal mrp = 0;
            decimal quantity = 0;

            for (int i = 0; i <= grvPurchaseDetails.Rows.Count - 2; i++)
            {
                if (grvPurchaseDetails.Rows[i].Cells["UnitPrice"].Value != null)
                {
                    decimal.TryParse(grvPurchaseDetails.Rows[i].Cells["UnitPrice"].Value.ToString(), out mrp);
                }
                if (grvPurchaseDetails.Rows[i].Cells["Quantity"].Value != null)
                {
                    decimal.TryParse(grvPurchaseDetails.Rows[i].Cells["Quantity"].Value.ToString(), out quantity);
                }


                total = quantity * mrp;
                grvPurchaseDetails.Rows[i].Cells["Total"].Value = total.ToString("F2");
                sum = sum + total;
            }

            return sum;
        }


        private decimal CalculateTotalPurchaseCost(List<PurchaseOrderDetail> lstPurchaseOrderDetail)
        {
            decimal sum = 0;

            foreach (PurchaseOrderDetail pDetail in lstPurchaseOrderDetail)
            {
                sum = sum + pDetail.Total;
            }

            return sum;
        }
        /// <summary>
        /// Method to reset all controlls.
        /// </summary>
        private void ResetControl()
        {
            cmbProductName.Text = string.Empty;
            txtQuantity.Clear();
            txtPurchasePrice.Clear();
            txtSalesPrice.Clear();
            cmbProductName.Focus();
            txtCarton.Clear();
            txtBarCode.Clear();

        }

        /// <summary>
        /// Method to get box quantity.
        /// </summary>
        /// <param name="prodcutID"></param>
        /// <returns></returns>
        private int GetBoxQuantity(string prodcutID)
        {
            int intBoxQuantity = 0;
            string stripsize = null;
            string noOfstrip = null;
            Product product = DataAccessProxy.GetProductInformationByID(prodcutID);

            if (product != null)
            {
                stripsize = product.PackSize;
            }

            intBoxQuantity = Convert.ToInt32(stripsize) * Convert.ToInt32(noOfstrip);
            return intBoxQuantity;
        }


        /// <summary>
        /// Method to insert journal information for credit sales.
        /// </summary>
        /// <returns></returns>
        public int InsertJournalInformationForGoodsReceive()
        {
            int result = 0;
            int referenceID = 1;
            decimal amount = Convert.ToDecimal(txtTotalAmount.Text);

            referenceID = DataAccessProxy.GetJournalReferenceID();

            for (int i = 0; i <= 1; i++)
            {
                Journal journal = new Journal();
                ChildAccount childAccount = DataAccessProxy.GetChildAccountBySupplierID(Convert.ToInt32(CmbSupplierName.Value));

                if (i == 0)
                {
                    journal.DrCrIndecator = "Dr";
                    journal.AccountID = IFMSConstant.InventoryAccountID;
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
        /// Method to insert journal information for credit sales.
        /// </summary>
        /// <returns></returns>
        public int InsertJournalInformationForCashPayment()
        {
            int result = 0;
            int referenceID = 1;
            decimal amount = Convert.ToDecimal(txtPaidAmount.Text);
            referenceID = DataAccessProxy.GetJournalReferenceID();

            for (int i = 0; i <= 1; i++)
            {
                IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();
                ChildAccount childAccount = DataAccessProxy.GetChildAccountBySupplierID(Convert.ToInt32(CmbSupplierName.Value));

                if (i == 0)
                {
                    journal.DrCrIndecator = "Dr";
                    journal.ChildAccountID = childAccount.ChildAccountID;
                    journal.AccountID = childAccount.AccountID;
                }
                else
                {
                    journal.DrCrIndecator = "Cr";
                    journal.AccountID = IFMSConstant.CashAccountID;
                }

                journal.JAccountID = 0;
                journal.Amount = amount;
                journal.ReferenceNo = referenceID;
                result = DataAccessProxy.InsertJournal(journal);
            }

            return result;
        }

        private void DataGridView1_CellContentClick(System.Object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            string celltext = grvPurchaseDetails.DisplayLayout.Bands[0].Columns[e.ColumnIndex].Header.Caption;
            if (celltext == "Delete")
            {
                if (grvPurchaseDetails.Rows[e.RowIndex].Delete())
                {
                    _vmPurchase.lstPurchaseOrderDetail.RemoveAt(e.RowIndex);
                    LoadDataInGrid(_vmPurchase.lstPurchaseOrderDetail);
                }
            }

        }

        /// <summary>
        /// Method to check duplicate child account.
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        private bool IsAvailableChildAccount(int supplierID, out  ChildAccount childAccount)
        {
            childAccount = DataAccessProxy.GetChildAccountBySupplierID(supplierID);
            if (childAccount != null)
            {
                return true;
            }
            return false;
        }

        private void btnSave_Click(System.Object sender, System.EventArgs e)
        {
            ChildAccount supplierChildAccount = new ChildAccount();

            if (ValdateInsertionData())
            {
                if (SavePurchaseOrderInformation() == (int)IFMSEnum.ReturnResult.Success)
                {
                    MessageBoxHelper.ShowInformation("Purchase information saved successfully.");
                    _vmPurchase = new VMPurchase();
                    grvPurchaseDetails.DataSource = _vmPurchase.lstPurchaseOrderDetail;
                    cmbProductName.Focus();
                    txtTotalAmount.Clear();
                    txtPaidAmount.Clear();
                    txtInvoiceNumber.Focus();

                    if (OnLoadPurchaseInformation != null)
                        OnLoadPurchaseInformation();
                }
            }
        }


        /// <summary>
        /// Method to insert print invoice.
        /// </summary>
        /// <param name="salesId"></param>
        private void PrintInvoice(int salesId)
        {
            Employee employee = new Employee();

            SalesOrder salesOrder = DataAccessProxy.GetSalesOrderByID(salesId);
            Customer customer = DataAccessProxy.GetCustomerByID(salesOrder.CustomerID);

            Organization pharmacy = new Organization();

            rptCreditSalesShowRoom op = new rptCreditSalesShowRoom();
            frmSalesReport objmainReport = new frmSalesReport();
            List<CreditSales> lstcreditSales = new List<CreditSales>();


            for (int i = 0; i <= grvPurchaseDetails.Rows.Count - 2; i++)
            {
                CreditSales creditSales = new CreditSales();
                Product product = DataAccessProxy.GetProductByName(grvPurchaseDetails.Rows[i].Cells["ProductName"].Value.ToString()).Where(p => p.BranchID == MTBFConstants.AppConstants.BranchID && p.OrganizationID == MTBFConstants.AppConstants.OrganizationID).Cast<Product>().FirstOrDefault();
                ProductSize productSize = DataAccessProxy.GetProductSizeByID(product.ProductSizeID);
                creditSales.ProductName = grvPurchaseDetails.Rows[i].Cells["ProductName"].Value.ToString();
                creditSales.Price = Convert.ToDecimal(grvPurchaseDetails.Rows[i].Cells["UnitMRP"].Value);
                creditSales.Quantity = Convert.ToDecimal(grvPurchaseDetails.Rows[i].Cells["Quantity"].Value);
                creditSales.Size = productSize.Name;
                creditSales.SquareFeet = grvPurchaseDetails.Rows[i].Cells["SquareFeet"].Value.ToString();
                creditSales.Total = Convert.ToDecimal(grvPurchaseDetails.Rows[i].Cells["Total"].Value.ToString());
                creditSales.ReceiveAmount = Convert.ToDecimal(txtPaidAmount.Text);
                creditSales.CustomerName = customer.CustomerName;
                creditSales.Address = customer.Address;
                creditSales.Phone = customer.Phone;
                creditSales.DueAmount = (Convert.ToDecimal(txtTotalAmount.Text) - Convert.ToDecimal(txtPaidAmount.Text));

                lstcreditSales.Add(creditSales);
            }

            op.DataSource = lstcreditSales;
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
            pharmacy = DataAccessProxy.GetPharmachyByID(MTBFConstants.AppConstants.OrganizationID);
            Branch brach = DataAccessProxy.GetBracnhByID(MTBFConstants.AppConstants.BranchID);
            if (brach != null)
            {
                PharmacyName = brach.BranchName;
                Address = brach.Address + ", " + brach.Phone + ", " + brach.Fax;
            }
        }



        private void cmbProductInformation_KeyUp(System.Object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 13)
            {
                txtPurchasePrice.Focus();
            }
        }




        private void grvPurchaseDetails_CellValueChanged(System.Object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (!isUpdateLoad)
            {
                if (_lstPurcahseOrderDetail.Count == 0)
                {
                    Product product = new Product();
                    decimal quantity = 0;
                    decimal size = 0;

                    ProductSize pSize = new ProductSize();
                    if (e.RowIndex != -1 && grvPurchaseDetails.Rows[e.RowIndex].Cells["ProductName"].Value != null)
                    {
                        product = DataAccessProxy.GetProductByID(grvPurchaseDetails.Rows[e.RowIndex].Cells["ProductID"].Value.ToString());
                        pSize = DataAccessProxy.GetProductSizeByID(product.ProductSizeID);

                    }
                    if (e.RowIndex != -1 && grvPurchaseDetails.Rows[e.RowIndex].Cells["Quantity"].Value != null)
                    {
                        product = DataAccessProxy.GetProductByID(grvPurchaseDetails.Rows[e.RowIndex].Cells["ProductID"].Value.ToString());
                        decimal.TryParse(grvPurchaseDetails.Rows[e.RowIndex].Cells["Quantity"].Value.ToString(), out quantity);

                    }
                }
            }

            txtTotalAmount.Text = CalculateTotalPurchaseCost().ToString("F2");

        }

        private void btnAdd_Click(System.Object sender, System.EventArgs e)
        {
            if (ValidFormData())
            {
                AddProductInGrid();
                cmbProductName.Focus();
            }
        }

        private void CmbSupplierName_ValueChanged(System.Object sender, System.EventArgs e)
        {

        }

        private void txtPurchasePrice_KeyPress(System.Object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (".1234567890".IndexOf(e.KeyChar) > -1 | e.KeyChar == Convert.ToChar(8))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtCommission_KeyPress(System.Object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (".1234567890".IndexOf(e.KeyChar) > -1 | e.KeyChar == Convert.ToChar(8))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtSalesPrice_KeyPress(System.Object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (".1234567890".IndexOf(e.KeyChar) > -1 | e.KeyChar == Convert.ToChar(8))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }



        private void btnRefresh_Click(System.Object sender, System.EventArgs e)
        {

            if (grvPurchaseDetails.Rows.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure want to delete all this record from grid ?", "Conformation Message", MessageBoxButtons.YesNo);
                if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                {
                    cmbCompanyName.Enabled = true;
                    CmbSupplierName.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;

                    _vmPurchase.lstPurchaseOrderDetail = new List<PurchaseOrderDetail>();
                    LoadDataInGrid(_vmPurchase.lstPurchaseOrderDetail);
                    txtInvoiceNumber.Focus();
                }
            }
            cmbCompanyName.Enabled = true;
            CmbSupplierName.Enabled = true;
        }

        private void btnAddProduct_Click(System.Object sender, System.EventArgs e)
        {
            frmProduct frm = new frmProduct(0, false, string.Empty);
            frm.Load_Product_Type += OnProductInformationLoad;
            frm.ShowDialog();
        }

        private void btnEdit_Click(System.Object sender, System.EventArgs e)
        {
            int serialNo = 0;
            string pid = string.Empty;

            if (cmbProductName.Text.Trim() != string.Empty)
            {
                Product product = DataAccessProxy.GetProductInformationByID(cmbProductName.Value.ToString());
                if (product != null)
                {
                    serialNo = product.SerialNo;
                    pid = product.ProductID;

                    frmProduct frm = new frmProduct(serialNo, true, pid);
                    frm.Load_Product_Type += OnPackSizeLoad;
                    frm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Please select a product for modify.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbProductName.Focus();
            }
        }

        private void OnPackSizeLoad()
        {
            GetPackSizeByProductID(cmbProductName.Value.ToString());
        }

        private void OnProductInformationLoad()
        {
            LoadProductInformationCombo();
        }


        private void LoadExistingPurchaseOrderDetailInformation()
        {
            List<Product> lstProduct = new List<Product>();
            string[] productID = _vmPurchase.lstPurchaseOrderDetail.Select(i => i.ProductID).Distinct().ToArray();
            string filter = string.Empty;


            if (productID.Length > 0)
            {
                for (int i = 0; i < productID.Length; i++)
                {
                    if (filter != string.Empty) filter = filter + ",";
                    filter = filter + "'" + productID[i] + "'";
                }

                filter = String.Format("{0} IN ({1})", "ProductID", filter);
                lstProduct = DataAccessProxy.GetFilteredProduct(filter);
            }

            foreach (PurchaseOrderDetail pDetail in _vmPurchase.lstPurchaseOrderDetail)
            {
                Product product = lstProduct.Where(p => p.ProductID == pDetail.ProductID).FirstOrDefault();
                //    decimal cartonSize = (product != null) ? product.CartonSize : 1;
                //  pDetail.Carton = pDetail.Quantity / cartonSize;
            }


            LoadDataInGrid(_vmPurchase.lstPurchaseOrderDetail);
        }

        private void LoadExistingPurchaseOrderInformation()
        {
            txtInvoiceNumber.Text = _vmPurchase.PurchaseOrder.InvoiceNumber;
            cmbCompanyName.Value = _vmPurchase.PurchaseOrder.CompanyID;
            CmbSupplierName.Value = _vmPurchase.PurchaseOrder.SupplierID;
            dtpPurchaseDate.Value = _vmPurchase.PurchaseOrder.PurchaseDate;
            txtInvoiceNumber.ReadOnly = true;
            cmbCompanyName.Enabled = false;
            CmbSupplierName.Enabled = false;
        }

        private void UltraQuantity_KeyUp(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 13)
            {
                if (ValidFormData())
                {
                    AddProductInGrid();
                    cmbProductName.Focus();
                }
            }
        }

        private void cmbProductName_TextChanged(object sender, EventArgs e)
        {
            // GetProductInformationByCompanyID(Convert.ToInt32(cmbCompanyName.Value), cmbProductName.Text);
        }

        private void cmbProductName_Leave(object sender, EventArgs e)
        {
            if (cmbProductName.Value != null)
            {
                GetPackSizeByProductID(cmbProductName.Value.ToString());
            }

        }

        private void cmbCompanyName_Leave(object sender, EventArgs e)
        {

        }

        private void cmbProductName_Click(object sender, EventArgs e)
        {
            //if (cmbCompanyName.Value != null)
            //{
            //    GetProductInformationByCompanyIDByClick(Convert.ToInt32(cmbCompanyName.Value));
            //}
        }

        private void cmbProductName_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridLayout layout = e.Layout;
            layout.PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
        }



        private void btnSelectProduct_Click(object sender, EventArgs e)
        {
            frmSelectProduct frm = new frmSelectProduct();
            frm.LoadProductInfo += new frmSelectProduct.LoadProductInfoEventHandler(frm_LoadProductInfo);
            frm.ShowDialog();
        }

        void frm_LoadProductInfo(string productID)
        {
            Product product = lstProductInformation.Where(p => p.ProductID == productID).FirstOrDefault();

            PurchaseOrderDetail purchaseOrderDetail = _vmPurchase.lstPurchaseOrderDetail.Where(p => p.ProductID == product.ProductID).FirstOrDefault();
            if (purchaseOrderDetail != null)
            {
                purchaseOrderDetail = new PurchaseOrderDetail();
                purchaseOrderDetail.ProductID = product.ProductID;
                purchaseOrderDetail.ProductName = product.ProductName;
                purchaseOrderDetail.Quantity = purchaseOrderDetail.Quantity + 1;
                purchaseOrderDetail.Total = purchaseOrderDetail.PurchasePrice * purchaseOrderDetail.Quantity;
            }
            else
            {
                purchaseOrderDetail = new PurchaseOrderDetail();
                purchaseOrderDetail.ProductID = product.ProductID;
                purchaseOrderDetail.ProductName = product.ProductName;
                purchaseOrderDetail.Quantity = 1;
                purchaseOrderDetail.PurchasePrice = 1;
                purchaseOrderDetail.SalesPrice = 2;
                purchaseOrderDetail.Total = 1;
            }

            _vmPurchase.lstPurchaseOrderDetail.Add(purchaseOrderDetail);

            LoadDataInGrid(_vmPurchase.lstPurchaseOrderDetail);
            ResetControl();
        }

        private void grvPurchaseDetails_CellChange(object sender, CellEventArgs e)
        {
            decimal quantity = 0;
            decimal purchasePrice = 0;
            if (e.Cell.Column.Header.Caption == "Quantity")
            {
                decimal.TryParse(e.Cell.Text, out quantity);
            }

            if (e.Cell.Column.Header.Caption == "PurchasePrice")
            {
                decimal.TryParse(e.Cell.Text, out purchasePrice);
            }
            if (quantity > 0)
            {
                _vmPurchase.lstPurchaseOrderDetail[e.Cell.Row.Index].Quantity = quantity;
            }
            if (purchasePrice > 0)
            {
                _vmPurchase.lstPurchaseOrderDetail[e.Cell.Row.Index].PurchasePrice = purchasePrice;
            }

            decimal salesPrice=0;

            decimal.TryParse(grvPurchaseDetails.Rows[e.Cell.Row.Index].Cells["SalesPrice"].Text, out salesPrice);
            _vmPurchase.lstPurchaseOrderDetail[e.Cell.Row.Index].SalesPrice = salesPrice;
            grvPurchaseDetails.Rows[e.Cell.Row.Index].Cells["Total"].Value = (_vmPurchase.lstPurchaseOrderDetail[e.Cell.Row.Index].PurchasePrice * _vmPurchase.lstPurchaseOrderDetail[e.Cell.Row.Index].Quantity);

            _vmPurchase.lstPurchaseOrderDetail[e.Cell.Row.Index].Total = _vmPurchase.lstPurchaseOrderDetail[e.Cell.Row.Index].PurchasePrice * _vmPurchase.lstPurchaseOrderDetail[e.Cell.Row.Index].Quantity;

            decimal sum = 0;
            foreach (PurchaseOrderDetail pDetail in _vmPurchase.lstPurchaseOrderDetail)
            {
                sum = sum + (pDetail.PurchasePrice * pDetail.Quantity);
            }
            txtTotalAmount.Text = sum.ToString("F2");
            // LoadDataInGrid(_vmPurchase.lstPurchaseOrderDetail);
        }

        private void grvPurchaseDetails_ClickCell(object sender, ClickCellEventArgs e)
        {
            string celltext = grvPurchaseDetails.DisplayLayout.Bands[0].Columns[e.Cell.Column.Index].Header.Caption;
            if (celltext == "Delete")
            {
                if (MessageBoxHelper.ShowConfirmation("Are you sure to delete this record?") == DialogResult.Yes)
                {
                    _vmPurchase.lstPurchaseOrderDetail.RemoveAt(e.Cell.Row.Index);
                    //   grvPurchaseDetails.Rows[e.Cell.Row.Index].Delete(false);
                    LoadDataInGrid(_vmPurchase.lstPurchaseOrderDetail);
                }
            }



            if (celltext == "PurchasePrice")
            {
                grvPurchaseDetails.Rows[e.Cell.Row.Index].Cells["PurchasePrice"].Activate();
                grvPurchaseDetails.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode);
            }

            if (celltext == "Sales Price")
            {
                grvPurchaseDetails.Rows[e.Cell.Row.Index].Cells["SalesPrice"].Activate();
                grvPurchaseDetails.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode);
            }

            if (celltext == "Quantity")
            {
                grvPurchaseDetails.Rows[e.Cell.Row.Index].Cells["Quantity"].Activate();
                grvPurchaseDetails.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode);
            }

        }

        private void txtCarton_TextChanged(object sender, EventArgs e)
        {
            decimal cartonQuantity = 0;
            if (cmbProductName.Value != null)
            {
                Product product = lstProductInformation.Where(p => p.ProductID == cmbProductName.Value.ToString()).FirstOrDefault();
                if (product != null)
                {
                    decimal.TryParse(txtCarton.Text, out cartonQuantity);
                    if (cartonQuantity > 0)
                    {
                        txtQuantity.Text = (cartonQuantity * product.CartonSize).ToString();
                    }
                    else
                    {
                        txtQuantity.Clear();
                    }
                }
                else
                {
                    MessageBoxHelper.ShowInformation("Invalid product.");
                    cmbProductName.Focus();
                }
            }
        }

        private void txtCarton_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Product product = lstProductInformation.Where(p => p.BarCode == txtBarCode.Text.Trim()).FirstOrDefault();
                if (product != null)
                {
                    PurchaseOrderDetail purchaseOrderDetail = _vmPurchase.lstPurchaseOrderDetail.Where(p => p.ProductID == product.ProductID).FirstOrDefault();
                    if (purchaseOrderDetail != null)
                    {
                        purchaseOrderDetail.Quantity = purchaseOrderDetail.Quantity + 1;
                        purchaseOrderDetail.Total = purchaseOrderDetail.PurchasePrice * purchaseOrderDetail.Quantity;
                    }
                    else
                    {
                        purchaseOrderDetail = new PurchaseOrderDetail();
                        purchaseOrderDetail.ProductID = product.ProductID;
                        purchaseOrderDetail.ProductName = product.ProductName;
                        purchaseOrderDetail.Quantity = 1;
                        purchaseOrderDetail.PurchasePrice = 1;
                        purchaseOrderDetail.SalesPrice = 2;
                        purchaseOrderDetail.Total = 1;
                        _vmPurchase.lstPurchaseOrderDetail.Add(purchaseOrderDetail);
                    }



                    LoadDataInGrid(_vmPurchase.lstPurchaseOrderDetail);
                }

                ResetControl();
                txtBarCode.Focus();
            }
        }



    }
}
