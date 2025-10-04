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
using Tiles_Inventory.Reports;
using System.Diagnostics;

namespace Tiles_Inventory
{
    public partial class frmSalesReturn : BaseForm
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

        public frmSalesReturn()
        {
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        public frmSalesReturn(PurchaseOrder purchaseOrder, bool isEdit, List<PurchaseOrderDetail> lstPurcahseOrderDetail)
        {
            _lstPurcahseOrderDetail = lstPurcahseOrderDetail;
            _purchaseOrderID = purchaseOrder.PurchaseID;
            _purchaseOrder = purchaseOrder;
            IsUpdating = isEdit;
            DataAccessProxy = new FacadeManager();
            InitializeComponent();
        }

        private void frmPurchase_Load(object sender, EventArgs e)
        {
            txtInvoiceNumber.Focus();
            Random invoiceNumber = new Random();
            int dice = invoiceNumber.Next(0, 1000000);
            txtInvoiceNumber.Text = dice.ToString();
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
        /// Method to insert purchase order information.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private int InsertPurchaseOrderInformation(ChildAccount supplierChildAccount)
        {
            int importTrip = 0;
            Employee employee = new Employee();
            List<PurchaseOrderDetail> lstPurchaseOrderDetail = new List<PurchaseOrderDetail>();

            PurchaseOrder purchaseOrder = new PurchaseOrder();
            int salesID = 0;
            int.TryParse(txtSalesOrderNo.Text, out salesID);
            purchaseOrder.InvoiceNumber = txtInvoiceNumber.Text;
            purchaseOrder.PurchaseDate = DateTime.Now;
            purchaseOrder.CompanyID = 0;
            purchaseOrder.SupplierID = 0;
            purchaseOrder.CompanyName = string.Empty;
            purchaseOrder.SupplierName = string.Empty;

            purchaseOrder.PurchaseAmount = txtTotalAmount.Text == string.Empty ? 0 : Convert.ToDecimal(txtTotalAmount.Text);
            purchaseOrder.SalesID = salesID;
            purchaseOrder.Status = (int)IFMSEnum.PurchaseOrderStatus.Issued;
            purchaseOrder.IsSalesReturn = true;
            employee = DataAccessProxy.GetEmployeeByID(IFMSConstant.LoggedinUserID);
            purchaseOrder.SalesmenName = (employee != null) ? employee.EmployeeName : string.Empty;
            purchaseOrder.PaidAmount = (txtPaidAmount.Text == string.Empty ? 0 : Convert.ToDecimal(txtPaidAmount.Text));
            purchaseOrder.BranchID = MTBFConstants.AppConstants.BranchID;
            purchaseOrder.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            purchaseOrder.ImportTrip = importTrip;
            lstPurchaseOrderDetail = GetPurchaseOrderDetailInformation();
            return DataAccessProxy.InsertPurchaseOrderInformation(purchaseOrder, lstPurchaseOrderDetail, MTBFConstants.AppConstants.LoggedinUserID, supplierChildAccount);
        }

        private int UpdatePurchaseOrderInformation(ChildAccount supplierChildAccount)
        {
            Employee employee = new Employee();
            List<PurchaseOrderDetail> lstPurchaseOrderDetail = new List<PurchaseOrderDetail>();
            int importTrip = 0;
            PurchaseOrder purchaseOrder = DataAccessProxy.GetPurchaseOrderByID(_purchaseOrderID); ;
            int salesID = 0;
            int.TryParse(txtSalesOrderNo.Text, out salesID);
            purchaseOrder.InvoiceNumber = txtInvoiceNumber.Text;

            purchaseOrder.PurchaseAmount = txtTotalAmount.Text == string.Empty ? 0 : Convert.ToDecimal(txtTotalAmount.Text);
            purchaseOrder.SalesID = salesID;
            purchaseOrder.IsSalesReturn = true;
            employee = DataAccessProxy.GetEmployeeByID(IFMSConstant.LoggedinUserID);
            purchaseOrder.SalesmenName = (employee != null) ? employee.EmployeeName : string.Empty;
            purchaseOrder.PaidAmount = (txtPaidAmount.Text == string.Empty ? 0 : Convert.ToDecimal(txtPaidAmount.Text));
            purchaseOrder.ImportTrip = importTrip;
            lstPurchaseOrderDetail = GetPurchaseOrderDetailInformation();
            return DataAccessProxy.UpdatePurchaseOrderInformation(purchaseOrder, lstPurchaseOrderDetail, MTBFConstants.AppConstants.LoggedinUserID, supplierChildAccount);
        }


        /// <summary>
        /// Method to insert purchase order detail information.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private List<PurchaseOrderDetail> GetPurchaseOrderDetailInformation()
        {
            int Quantity = 0;
            string strProductId = null;
            decimal Price = default(decimal);
            decimal salesPrice = default(decimal);
            string expireDate = string.Empty;
            decimal commission = default(decimal);
            decimal vat = default(decimal);
            decimal squareFeet = 0;
            List<PurchaseOrderDetail> lstPurchaseOrderDetails = new List<PurchaseOrderDetail>();

            for (int i = 0; i <= grvPurchaseDetails.Rows.Count - 2; i++)
            {
                PurchaseOrderDetail purchaseOrderDetails = new PurchaseOrderDetail();

                strProductId = grvPurchaseDetails.Rows[i].Cells["ProductID"].Value.ToString();
                Quantity = Convert.ToInt32(grvPurchaseDetails.Rows[i].Cells["Quantity"].Value);
                Price = Convert.ToDecimal(grvPurchaseDetails.Rows[i].Cells["UnitPrice"].Value);
                salesPrice = Convert.ToDecimal(grvPurchaseDetails.Rows[i].Cells["UnitMRP"].Value);
                vat = Convert.ToDecimal(grvPurchaseDetails.Rows[i].Cells["VAT"].Value);
                decimal.TryParse(grvPurchaseDetails.Rows[i].Cells["SquareFeet"].Value.ToString(), out squareFeet);
                commission = salesPrice - Price;
                commission = (100 * commission) / salesPrice;
                Product product = DataAccessProxy.GetProductByID(strProductId);
                purchaseOrderDetails.ProductName = product.ProductName;
                purchaseOrderDetails.ProductID = strProductId;

                purchaseOrderDetails.Quantity = Quantity;
                purchaseOrderDetails.PurchasePrice = Price;
                purchaseOrderDetails.SalesPrice = salesPrice;
                purchaseOrderDetails.SquareFeet = squareFeet;
                purchaseOrderDetails.Barcode = grvPurchaseDetails.Rows[i].Cells["BarCode"].Value.ToString();
                lstPurchaseOrderDetails.Add(purchaseOrderDetails);
            }

            return lstPurchaseOrderDetails;

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

            int salesID = 0;
            int.TryParse(txtSalesOrderNo.Text, out salesID);
            if (string.IsNullOrEmpty(txtSalesOrderNo.Text))
            {
                MessageBoxHelper.ShowInformation("You need to enter sales order id.");
                txtSalesOrderNo.Focus();
                return false;
            }
            SalesOrder salesOrder = DataAccessProxy.GetSalesOrderByID(salesID);
            if (salesOrder == null)
            {
                MessageBoxHelper.ShowInformation("Invalid sales order id.");
                txtSalesOrderNo.Focus();
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
                    if (cell.OwningColumn.HeaderText != "VAT" && cell.OwningColumn.HeaderText != "Product Name" && cell.OwningColumn.ToolTipText != "Delete" && cell.OwningColumn.HeaderText != "Barcode")
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

            int salesID = 0;
            int.TryParse(txtSalesOrderNo.Text, out salesID);
            if (string.IsNullOrEmpty(txtSalesOrderNo.Text))
            {
                MessageBoxHelper.ShowInformation("You need to enter sales order id.");
                txtSalesOrderNo.Focus();
                return false;
            }
            SalesOrder salesOrder = DataAccessProxy.GetSalesOrderByID(salesID);
            if (salesOrder == null)
            {
                MessageBoxHelper.ShowInformation("Invalid sales order id.");
                txtSalesOrderNo.Focus();
                return false;
            }


            if (grvPurchaseDetails.Rows.Count < 2)
            {
                MessageBoxHelper.ShowInformation("Please add information in grid.");
                return false;
            }

            for (int i = 0; i < grvPurchaseDetails.Rows.Count - 1; i++)
            {
                if (!IsBlankDataRow(grvPurchaseDetails.Rows[i]))
                {
                    return false;
                }
            }

            return true;
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
            decimal squareFeet = 0;
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

                if (grvPurchaseDetails.Rows[i].Cells["SquareFeet"].Value != null)
                {
                    decimal.TryParse(grvPurchaseDetails.Rows[i].Cells["SquareFeet"].Value.ToString(), out squareFeet);
                }
                total = squareFeet * mrp;
                grvPurchaseDetails.Rows[i].Cells["Total"].Value = total.ToString("F2");
                sum = sum + total;
            }

            return sum;
        }

        /// <summary>
        /// Method to reset all controlls.
        /// </summary>
        private void ResetControl()
        {
            grvPurchaseDetails.Columns[0].Visible = false;
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





        private void DataGridView1_CellContentClick(System.Object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            string deleteString = grvPurchaseDetails.CurrentCell.OwningColumn.ToolTipText;
            if (!grvPurchaseDetails.CurrentRow.IsNewRow)
            {
                if (deleteString == "Delete")
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure want to delete this record ?", "Conformation Message", MessageBoxButtons.YesNo);
                    if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                    {
                        grvPurchaseDetails.Rows.Remove(grvPurchaseDetails.CurrentRow);
                        txtTotalAmount.Text = CalculateTotalPurchaseCost().ToString();
                    }
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
                if (base.IsUpdating)
                {

                    if (UpdatePurchaseOrderInformation(supplierChildAccount) == (int)IFMSEnum.ReturnResult.Success)
                    {
                        MessageBoxHelper.ShowInformation("Purchase information saved successfully.");
                        grvPurchaseDetails.Rows.Clear();
                        txtTotalAmount.Clear();
                        txtPaidAmount.Clear();
                        txtInvoiceNumber.Focus();
                        if (OnLoadPurchaseInformation != null)
                            OnLoadPurchaseInformation();
                    }

                }
                else
                {

                    if (InsertPurchaseOrderInformation(supplierChildAccount) == (int)IFMSEnum.ReturnResult.Success)
                    {
                        MessageBoxHelper.ShowInformation("Purchase information saved successfully.");

                        int salesID = 0;
                        int.TryParse(txtSalesOrderNo.Text, out salesID);
                        PrintInvoice(salesID);
                        grvPurchaseDetails.Rows.Clear();
                        txtTotalAmount.Clear();
                        txtPaidAmount.Clear();
                        txtInvoiceNumber.Focus();

                        if (OnLoadPurchaseInformation != null)
                            OnLoadPurchaseInformation();
                    }

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

            rptSalesReturn op = new rptSalesReturn();
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
                creditSales.SalesID = salesOrder.SalesID;
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






        private void grvPurchaseDetails_CellValueChanged(System.Object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (!isUpdateLoad)
            {
                if (_lstPurcahseOrderDetail.Count == 0)
                {
                    Product product = new Product();
                    decimal quantity = 0;
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
                        pSize = DataAccessProxy.GetProductSizeByID(product.ProductSizeID);
                        grvPurchaseDetails.Rows[e.RowIndex].Cells["SquareFeet"].Value = quantity * Convert.ToDecimal(pSize.Name);
                    }
                }
            }

            txtTotalAmount.Text = CalculateTotalPurchaseCost().ToString("F2");

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

        private void UltraQuantity_KeyPress(System.Object sender, System.Windows.Forms.KeyPressEventArgs e)
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

            if (grvPurchaseDetails.Rows.Count > 1)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure want to delete all this record from grid ?", "Conformation Message", MessageBoxButtons.YesNo);
                if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                {
                    grvPurchaseDetails.Rows.Clear();
                    txtSalesOrderNo.Clear();
                    txtSalesOrderNo.Focus();
                }
            }

        }






        private void LoadExistingPurchaseOrderDetailInformation()
        {  // Product product = DataAccessProxy.GetProductByID(pDetail.ProductID);

            Stopwatch watch = Stopwatch.StartNew();
            decimal timeCount = 0;
            foreach (PurchaseOrderDetail pDetail in _lstPurcahseOrderDetail)
            {
                DataGridViewRow row = grvPurchaseDetails.Rows[0].Clone() as DataGridViewRow;
                int index = grvPurchaseDetails.Rows.Add(row);

                grvPurchaseDetails.Rows[index].Cells["ProductID"].Value = pDetail.ProductID;
                grvPurchaseDetails.Rows[index].Cells["ProductName"].Value = pDetail.ProductName;
                grvPurchaseDetails.Rows[index].Cells["Quantity"].Value = pDetail.Quantity;
                grvPurchaseDetails.Rows[index].Cells["SquareFeet"].Value = pDetail.SquareFeet;
                grvPurchaseDetails.Rows[index].Cells["UnitPrice"].Value = pDetail.PurchasePrice;
                grvPurchaseDetails.Rows[index].Cells["UnitMRP"].Value = pDetail.SalesPrice;
                grvPurchaseDetails.Rows[index].Cells["Total"].Value = (pDetail.PurchasePrice * pDetail.Quantity).ToString("F2");
                grvPurchaseDetails.Rows[index].Cells["Barcode"].Value = pDetail.Barcode;
                timeCount = watch.ElapsedMilliseconds;

                //MessageBox.Show(timeCount.ToString());
            }
            watch.Stop();
            _lstPurcahseOrderDetail = new List<PurchaseOrderDetail>();
        }

        private void LoadExistingPurchaseOrderInformation()
        {
            PurchaseOrder purchaseOrder = _purchaseOrder;
            txtInvoiceNumber.Text = purchaseOrder.InvoiceNumber;
        }



        private void cmbProductName_TextChanged(object sender, EventArgs e)
        {
            // GetProductInformationByCompanyID(Convert.ToInt32(cmbCompanyName.Value), cmbProductName.Text);
        }



        private void cmbCompanyName_Leave(object sender, EventArgs e)
        {

        }



        private void cmbProductName_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridLayout layout = e.Layout;
            layout.PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
        }

        private void cbSalesReturn_CheckedChanged(object sender, EventArgs e)
        {
            //txtSalesOrderNo.Enabled = cbSalesReturn.Checked;
            //txtInvoiceNumber.Enabled = (!cbSalesReturn.Checked) ? true : false;
            //if (cbSalesReturn.Checked)
            //{


            //}
            //else
            //{
            //    txtInvoiceNumber.Clear();
            //    txtSalesOrderNo.Clear();
            //}
        }

        private void txtSalesOrderNo_TextChanged(object sender, EventArgs e)
        {
           

        }

        private void LoadSalesOrderInformation()
        {
            int salesID = 0;
            int.TryParse(txtSalesOrderNo.Text, out salesID);
            SalesOrder salesOrder = DataAccessProxy.GetSalesOrderByID(salesID);
            if (salesOrder != null && salesOrder.BranchID == MTBFConstants.AppConstants.BranchID && salesOrder.OrganizationID == MTBFConstants.AppConstants.OrganizationID)
            {
                grvPurchaseDetails.Rows.Clear();


                List<SalesOrderDetail> lstSalesOrderDetail = new List<SalesOrderDetail>();
                lstSalesOrderDetail = DataAccessProxy.GetAllSalesDetailBySalesID(salesID).Cast<SalesOrderDetail>().ToList();


                string filter = string.Empty;
                string[] productIDs = lstSalesOrderDetail.Select(t => t.ProductID).Distinct().ToArray();

                if (productIDs.Length > 0)
                {
                    for (int i = 0; i <= productIDs.Length - 1; i++)
                    {
                        if (filter != string.Empty) filter = filter + ",";
                        filter = filter + "'" + productIDs[i] + "'";
                    }

                    filter = String.Format("{0} IN ({1})", "ProductID", filter);
                }

                int[] purchaseIDs = lstSalesOrderDetail.Select(d => d.PurchaseID).Distinct().ToArray();
                filter = String.Format("{0}  And {1} IN ({2})", filter, "PurchaseID", String.Join(",", purchaseIDs));
                List<PurchaseOrderDetail> lstPurchaseOrderDetail = new List<PurchaseOrderDetail>();
                if (productIDs.Length > 0 && purchaseIDs.Length > 0)
                {
                    lstPurchaseOrderDetail = DataAccessProxy.GetFilteredPurchaseOrderDetail(filter);
                }




                foreach (SalesOrderDetail sDetail in lstSalesOrderDetail)
                {
                    DataGridViewRow row = grvPurchaseDetails.Rows[0].Clone() as DataGridViewRow;
                    int index = grvPurchaseDetails.Rows.Add(row);
                    // Product product = DataAccessProxy.GetProductByID(sDetail.ProductID);

                    PurchaseOrderDetail purchaseOrderDetail = lstPurchaseOrderDetail.Where(p => p.ProductID == sDetail.ProductID && p.PurchaseID == sDetail.PurchaseID).Cast<PurchaseOrderDetail>().ToList().FirstOrDefault();

                    grvPurchaseDetails.Rows[index].Cells["ProductID"].Value = sDetail.ProductID;
                    grvPurchaseDetails.Rows[index].Cells["ProductName"].Value = sDetail.ProductName;
                    grvPurchaseDetails.Rows[index].Cells["Quantity"].Value = sDetail.Quantity;
                    grvPurchaseDetails.Rows[index].Cells["SquareFeet"].Value = sDetail.SquareFeet;
                    grvPurchaseDetails.Rows[index].Cells["UnitPrice"].Value = sDetail.Price;
                    grvPurchaseDetails.Rows[index].Cells["UnitMRP"].Value = sDetail.Price;
                    grvPurchaseDetails.Rows[index].Cells["Total"].Value = (sDetail.Price * sDetail.Quantity).ToString("F2");
                    grvPurchaseDetails.Rows[index].Cells["Barcode"].Value = (purchaseOrderDetail != null) ? purchaseOrderDetail.Barcode : string.Empty;
                }
            }
            else
            {
                MessageBoxHelper.ShowInformation("Invalid sales order number");
                txtSalesOrderNo.Focus();
            }


        }




        private bool IsExistingProduct(Product product, out int rowIndex)
        {
            rowIndex = 0;
            foreach (DataGridViewRow row in grvPurchaseDetails.Rows)
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

        private void btnSelectProduct_Click(object sender, EventArgs e)
        {
            frmSelectProduct frm = new frmSelectProduct();
            frm.LoadProductInfo += new frmSelectProduct.LoadProductInfoEventHandler(frm_LoadProductInfo);
            frm.ShowDialog();
        }

        void frm_LoadProductInfo(string productID)
        {
            //   int intBoxQuantity = 0;
            int Quantity = 0;
            decimal PurchasePrice = 0;
            decimal MRP = 0;
            decimal UnitPrice = 0;
            decimal UnitMRP = 0;
            decimal vat = 0;
            decimal squareFeet = 0;
            Product product = DataAccessProxy.GetProductByID(productID);

            Quantity = 1;


            PurchasePrice = 1;
            MRP = 2;
            decimal.TryParse(product.SizeName, out squareFeet);

            UnitPrice = PurchasePrice + vat;
            UnitMRP = MRP;
            int index = 0;

            if (IsExistingProduct(product, out index))
            {
                decimal previousQuantity = 0;
                decimal.TryParse(grvPurchaseDetails.Rows[index].Cells["Quantity"].Value.ToString(), out previousQuantity);

                grvPurchaseDetails.Rows[index].Cells["Quantity"].Value = (previousQuantity + Quantity).ToString();

                squareFeet = (previousQuantity + Quantity) * squareFeet;
                grvPurchaseDetails.Rows[index].Cells["SquareFeet"].Value = squareFeet.ToString();
                grvPurchaseDetails.Rows[index].Cells["UnitPrice"].Value = UnitPrice.ToString();
                grvPurchaseDetails.Rows[index].Cells["UnitMRP"].Value = UnitMRP;
                grvPurchaseDetails.Rows[index].Cells["Total"].Value = (UnitPrice * squareFeet).ToString("F2");
                grvPurchaseDetails.Rows[index].Cells["BarCode"].Value = string.Empty;

            }
            else
            {
                DataGridViewRow row = grvPurchaseDetails.Rows[0].Clone() as DataGridViewRow;
                index = grvPurchaseDetails.Rows.Add(row);
                grvPurchaseDetails.Rows[index].Cells["ProductID"].Value = product.ProductID;
                grvPurchaseDetails.Rows[index].Cells["ProductName"].Value = product.ProductName;
                grvPurchaseDetails.Rows[index].Cells["Quantity"].Value = Quantity.ToString();
                grvPurchaseDetails.Rows[index].Cells["SquareFeet"].Value = squareFeet.ToString();
                grvPurchaseDetails.Rows[index].Cells["UnitPrice"].Value = UnitPrice.ToString();
                grvPurchaseDetails.Rows[index].Cells["UnitMRP"].Value = UnitMRP;
                grvPurchaseDetails.Rows[index].Cells["Total"].Value = (UnitPrice * squareFeet).ToString("F2");
                grvPurchaseDetails.Rows[index].Cells["BarCode"].Value = string.Empty;
            }


            txtTotalAmount.Text = CalculateTotalPurchaseCost().ToString("F2");
            ResetControl();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSalesOrderNo.Text))
            {
                isUpdateLoad = true;
                LoadSalesOrderInformation();
                isUpdateLoad = false;
            }
        }



    }
}
