using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NybSys.MTBF.Utility.Constants;
using NybSys.MTBF.Utility.Helper;
using Infragistics.Win.UltraWinGrid;
using NybSys.MTBF.Utility.Resources;
using NybSys.MTBF.Utility.Enums;
using IMFS.Common.DTO;



namespace Tiles_Inventory
{
    public partial class frmDeliveryProduct : BaseForm
    {
        public delegate void LodaEventHandaler();
        public event LodaEventHandaler OnLoadDeliveryProductInformation;

        private int _deliveryProductID = 0;
        private List<SalesOrder> _lstSalesOrder = new List<SalesOrder>();

        public frmDeliveryProduct()
        {
            DataAccessProxy = new IFMS.Facade.FacadeManager();
            InitializeComponent();
        }

        private void frmDeliveryProduct_Load(object sender, EventArgs e)
        {
            LoadSalesOrderCombo();
            LoadCustomerCombo();

            if (IsUpdating)
            {
                LoadExistingDeliveryProductInformation();
            }
        }

        public frmDeliveryProduct(bool isEdit, int deliveryProductID)
        {
            IsUpdating = isEdit;
            _deliveryProductID = deliveryProductID;
            DataAccessProxy = new IFMS.Facade.FacadeManager();
            InitializeComponent();
        }

        //#region "Event Handellers"

        //private void btnAddCustomer_Click(object sender, EventArgs e)
        //{
        //    //NybSys.Sales.frmCustomer frm = new Sales.frmCustomer();
        //    //frm.OnLoadCustomerInfo += new Sales.frmCustomer.LoadEventHandaler(frm_OnLoadCustomerInfo);
        //    //frm.ShowDialog();
        //}

        //void frm_OnLoadCustomerInfo()
        //{
        //    LoadCustomerCombo(cmbCompany.Value.ToString());
        //}

        //private void frmDeliveryProduct_Load(object sender, EventArgs e)
        //{
        //    LoadCompanyCombo();

        //    if (IsUpdating)
        //    {
        //        LoadProductCombo(cmbCompany.Value.ToString());
        //        LoadExistingDeliveryProductInformation();
        //        LoadExistingDeliveryProductDetailInformation();
        //    }

        //}

        private void cmbCustomer_ValueChanged(object sender, EventArgs e)
        {
            int customerID = 0;
            if (cmbCustomer.Value != null)
            {
                customerID = Convert.ToInt32(cmbCustomer.Value);
                Customer customer = DataAccessProxy.GetCustomerByID(customerID);
                if (customer != null)
                {
                    txtCustomerPhone.Text = customer.Phone;
                    txtCustomerAddress.Text = customer.Address;
                }
                else
                {
                    txtCustomerPhone.Clear();
                    txtCustomerAddress.Clear();
                }
            }
        }

        private void grvDeliveryProduct_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            CellEditActivate(grvDeliveryProduct, e.Cell, e.Cell.Row.Index);
        }

        private void grvDeliveryProduct_CellChange(object sender, CellEventArgs e)
        {
            decimal quatity = 0;
            decimal deliveredQty = 0;
            decimal salesQty = 0;
            if (e.Cell.Column.Header.Caption == MTBFConstants.GridHeader.QUANTITY)
            {
                decimal.TryParse(grvDeliveryProduct.Rows[e.Cell.Row.Index].Cells["Quantity"].Text, out quatity);
                decimal.TryParse(grvDeliveryProduct.Rows[e.Cell.Row.Index].Cells["Sales Quantity"].Text, out salesQty);
                decimal.TryParse(grvDeliveryProduct.Rows[e.Cell.Row.Index].Cells["Delivered Quantity"].Text, out deliveredQty);
                grvDeliveryProduct.Rows[e.Cell.Row.Index].Cells["Due"].Value = salesQty - (quatity + deliveredQty);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateFormData())
            {
                if (IsUpdating)
                {
                    if (UpdateDeliveryProduct() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Product delivery informaitn saved successfully");
                        IsUpdating = false;
                        if (OnLoadDeliveryProductInformation != null)
                            OnLoadDeliveryProductInformation();

                        ResetAllControls();
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save product delivery information");
                    }
                }
                else
                {
                    if (InsertDeliveryProduct() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Product delivery informaitn saved successfully");
                        if (OnLoadDeliveryProductInformation != null)
                            OnLoadDeliveryProductInformation();
                        ResetAllControls();
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save product delivery information");
                    }
                }

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.ResetAllControls();
        }



        //private void grvDeliveryProduct_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        //{
        //    UltraGridLayout layout = e.Layout;
        //    UltraGridBand band = layout.Bands[0];

        //    band.Columns[MTBFConstants.GridHeader.QUANTITY].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
        //    band.Columns[MTBFConstants.GridHeader.AVAILABLE_QUANTITY].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
        //    band.Columns[MTBFConstants.GridHeader.DISCOUNT].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
        //    band.Columns[MTBFConstants.GridHeader.TOTAL].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;

        //    band.Columns[MTBFConstants.GridHeader.QUANTITY].PromptChar = ' ';
        //    band.Columns[MTBFConstants.GridHeader.DISCOUNT].PromptChar = ' ';
        //    band.Columns[MTBFConstants.GridHeader.TOTAL].PromptChar = ' ';

        //}

        //private void grvDeliveryProduct_InitializeRow(object sender, InitializeRowEventArgs e)
        //{
        //    e.Row.Cells[MTBFConstants.GridHeader.QUANTITY].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DoubleNonNegative;
        //    e.Row.Cells[MTBFConstants.GridHeader.DISCOUNT].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DoubleNonNegative;
        //}

        //#endregion

        //#region "Private Event"


        /// <summary>
        /// Method to load existing delivery product information.
        /// </summary>
        private void LoadExistingDeliveryProductInformation()
        {
            DeliveryProduct deliveryProduct = DataAccessProxy.GetDeliveryProductByID(_deliveryProductID);

            if (deliveryProduct != null)
            {
                cmbSoNumber.Value = deliveryProduct.SalesID;
                cmbCustomer.Value = deliveryProduct.CustomerID;
                txtDescription.Text = deliveryProduct.Description;
                dtpDeliveryDate.Value = deliveryProduct.DeliveryDate;
                LoadExistingDeliveryProductDetailInformation(deliveryProduct);
            }
        }

        /// <summary>
        /// Method to load existing delviery product detail.
        /// </summary>
        private void LoadExistingDeliveryProductDetailInformation(DeliveryProduct deliveryProduct)
        {
            DataTable quotationDetailTable = BuildDeliveryProductTable();
            decimal deliveredQty = 0;
            decimal dueQty = 0;


            List<DeliveryProductDetail> lstDeliveryProductDetail = new List<DeliveryProductDetail>();
            lstDeliveryProductDetail = DataAccessProxy.GetAllDeliveryProductDetailByDeliveryID(_deliveryProductID).Cast<DeliveryProductDetail>().ToList(); ;
            foreach (DeliveryProductDetail detail in lstDeliveryProductDetail)
            {
                DataRow row = quotationDetailTable.NewRow();
                deliveredQty = GetDeliveredQuantity(deliveryProduct.SalesID, detail.ProductID);
                dueQty = (detail.SalesQuantity - (deliveredQty));
                row["ProductID"] = detail.ProductID;
                row["Product Name"] = detail.ProductName;
                row["Sales Quantity"] = detail.SalesQuantity;
                row["Delivered Quantity"] = deliveredQty - detail.Quantity;
                row["Quantity"] = detail.Quantity;
                row["Due"] = dueQty;
                quotationDetailTable.Rows.Add(row);

            }
            grvDeliveryProduct.DataSource = quotationDetailTable;

        }

        /// <summary>
        /// Method to build receive product table.
        /// </summary>
        /// <returns></returns>
        private DataTable BuildDeliveryProductTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ProductID");
            table.Columns.Add("Product Name");
            table.Columns.Add("Sales Quantity");
            table.Columns.Add("Delivered Quantity");
            table.Columns.Add("Quantity");
            table.Columns.Add("Due");

            return table;
        }





        /// <summary>
        /// Method to load company inforamtion in combo box.
        /// </summary>
        private void LoadCustomerCombo()
        {
            List<Customer> lstCustomer = new List<Customer>();
            Customer customer = new Customer();
            customer.CustomerID = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            customer.CustomerName = MTBFConstants.DataField.COMBO_DEFAULT_NAME;

            lstCustomer = DataAccessProxy.GetAllCustomer().Cast<Customer>().Where(c => c.BranchID == MTBFConstants.AppConstants.BranchID && c.OrganizationID == MTBFConstants.AppConstants.OrganizationID).ToList();
            lstCustomer.Insert(0, customer);

            UIHelper.SetComboBoxData(cmbCustomer, lstCustomer, MTBFConstants.DataField.CUSTOMER_NAME, MTBFConstants.DataField.CUSTOMER_ID);
        }

        /// <summary>
        /// Method to load company inforamtion in combo box.
        /// </summary>
        private void LoadSalesOrderCombo()
        {
            _lstSalesOrder = DataAccessProxy.GetAllSalesOrderByBranchAndOrganization(MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<SalesOrder>().ToList();
            if (_lstSalesOrder.Count > 0)
                UIHelper.SetComboBoxData(cmbSoNumber, _lstSalesOrder, "SalesID", "SalesID");
        }

        private void cmbSoNumber_ValueChanged(object sender, EventArgs e)
        {
            if (cmbSoNumber.Value != null)
            {
                SalesOrder salesOrder = _lstSalesOrder.Where(s => s.SalesID == Convert.ToInt32(cmbSoNumber.Value)).Cast<SalesOrder>().ToList().FirstOrDefault();
                LoadSalesOrderDetailInformation(Convert.ToInt32(cmbSoNumber.Value));
                if (salesOrder != null)
                    cmbCustomer.Value = salesOrder.CustomerID;
            }
        }

        ///// <summary>
        ///// Method to load product combo.
        ///// </summary>
        //private void LoadProductCombo(string companyCode)
        //{
        //    List<Product> lstProduct = new List<Product>();
        //    lstProduct = new ProductManager().GetAllProduct(companyCode).Cast<Product>().ToList();

        //    //Product product = new Product();
        //    //product.ProductCode = MTBFConstants.DataField.COMBO_DEFULT_CODE;
        //    //product.ProductName = MTBFConstants.DataField.COMBO_DEFULT_NAME;
        //    //lstProduct.Insert(0, product);
        //    UIHelper.SetComboBoxData(cmbProductName, lstProduct, MTBFConstants.DataField.PRODUCT_NAME, MTBFConstants.DataField.PRODUCT_CODE);
        //}

        ///// <summary>
        ///// Method to add editor component.
        ///// </summary>
        ///// <param name="grv"></param>
        //private void AddEditorControls(UltraGrid grv)
        //{
        //    grv.DisplayLayout.Bands[0].Columns[MTBFConstants.GridHeader.PRODUCT_NAME].EditorComponent = cmbProductName;
        //}

        ///// <summary>
        ///// Method to load calculate purchase price.
        ///// </summary>
        ///// <returns></returns>
        //private Decimal CalculatePurchasePrice()
        //{
        //    decimal quantity = 0;
        //    decimal unitPrice = 0;
        //    decimal totalDeiscount = 0;
        //    decimal discount = 0;
        //    decimal total = 0;

        //    foreach (UltraGridRow row in grvDeliveryProduct.Rows)
        //    {
        //        decimal.TryParse(row.Cells[MTBFConstants.GridHeader.QUANTITY].Text, out quantity);
        //        decimal.TryParse(row.Cells[MTBFConstants.GridHeader.DISCOUNT].Text, out discount);
        //        decimal.TryParse(row.Cells[MTBFConstants.GridHeader.PRICE].Text, out unitPrice);
        //        totalDeiscount = totalDeiscount + discount;
        //        total = total + ((quantity * unitPrice) - discount);
        //        row.Cells[MTBFConstants.GridHeader.TOTAL].Value = ((quantity * unitPrice) - discount).ToString("00.00");
        //    }

        //    txtTotal.Text = total.ToString("00.00");
        //    txtDiscount.Text = totalDeiscount.ToString();
        //    return quantity;
        //}

        /// <summary>
        /// Method to activate grid cell
        /// </summary>
        /// <param name="grv"></param>
        /// <param name="cell"></param>
        /// <param name="rowIndex"></param>
        private void CellEditActivate(UltraGrid grv, UltraGridCell cell, int rowIndex)
        {

            if (cell.Column.Header.Caption == MTBFConstants.GridHeader.QUANTITY)
            {
                cell.Activate();
                grv.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode);
            }
        }

        /// <summary>
        /// Method to validate form data.
        /// </summary>
        /// <returns></returns>
        private bool ValidateFormData()
        {
            if (Convert.ToInt32(cmbSoNumber.Value) == MTBFConstants.DataField.COMBO_DEFAULT_ID)
            {
                MessageBoxHelper.ShowInformation("You need to select sales order number.");
                cmbSoNumber.Focus();
                return false;
            }

            if (Convert.ToInt32(cmbCustomer.Value) == MTBFConstants.DataField.COMBO_DEFAULT_ID)
            {
                MessageBoxHelper.ShowInformation("You need to select customer");
                cmbCustomer.Focus();
                return false;
            }



            foreach (UltraGridRow row in grvDeliveryProduct.Rows)
            {
                decimal salesQty = 0;
                decimal quantity = 0;
                decimal deliveryQty = 0;

                decimal.TryParse(row.Cells["Sales Quantity"].Text, out salesQty);
                decimal.TryParse(row.Cells["Delivered Quantity"].Text, out deliveryQty);
                decimal.TryParse(row.Cells[MTBFConstants.GridHeader.QUANTITY].Text, out quantity);


                if (quantity == 0)
                {
                    MessageBoxHelper.ShowInformation(SystemMessages.QUANTITY_OF_INPUT_MUST_ENTER);
                    UIHelper.MarkDataGridRowAsInvalid(row);
                    return false;
                }

                if (salesQty < (quantity + deliveryQty))
                {
                    MessageBoxHelper.ShowInformation("Quantity can't be greater than sales quantity.");
                    UIHelper.MarkDataGridRowAsInvalid(row);
                    return false;
                }



            }

            return true;
        }

        /// <summary>
        /// Method to insert delivery product information.
        /// </summary>
        /// <returns></returns>
        private int InsertDeliveryProduct()
        {
            decimal total = 0;

            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            List<DeliveryProductDetail> lstDeliveryProductDetail = new List<DeliveryProductDetail>();
            DeliveryProduct deliveryProduct = new DeliveryProduct();

            deliveryProduct.CustomerID = Convert.ToInt32(cmbCustomer.Value);
            deliveryProduct.SalesID = Convert.ToInt32(cmbSoNumber.Value);

            deliveryProduct.DeliveryDate = dtpDeliveryDate.Value;
            deliveryProduct.Description = txtDescription.Text;
            deliveryProduct.Total = total;
            deliveryProduct.CustomerName = cmbCustomer.Text;
            deliveryProduct.Phone = txtCustomerPhone.Text;
            lstDeliveryProductDetail = GetAllDeliveryProductDetail();

            result = DataAccessProxy.InsertDeliveryProduct(deliveryProduct, lstDeliveryProductDetail);

            return result;
        }

        /// <summary>
        /// Method to update delivery product information.
        /// </summary>
        /// <returns></returns>
        private int UpdateDeliveryProduct()
        {
            decimal total = 0;

            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            List<DeliveryProductDetail> lstDeliveryProductDetail = new List<DeliveryProductDetail>();
            DeliveryProduct deliveryProduct = DataAccessProxy.GetDeliveryProductByID(_deliveryProductID);
            deliveryProduct.CustomerID = Convert.ToInt32(cmbCustomer.Value);
            deliveryProduct.SalesID = Convert.ToInt32(cmbSoNumber.Value);

            deliveryProduct.DeliveryDate = dtpDeliveryDate.Value;
            deliveryProduct.Description = txtDescription.Text;
            deliveryProduct.Total = total;
            deliveryProduct.CustomerName = cmbCustomer.Text;
            deliveryProduct.Phone = txtCustomerPhone.Text;

            lstDeliveryProductDetail = GetAllDeliveryProductDetail();

            result = DataAccessProxy.UpdateDeliveryProduct(deliveryProduct, lstDeliveryProductDetail);

            return result;
        }

        /// <summary>
        /// Method to get all delivery product detail.
        /// </summary>
        /// <returns></returns>
        List<DeliveryProductDetail> GetAllDeliveryProductDetail()
        {
            List<DeliveryProductDetail> lstDeliveryProductDetail = new List<DeliveryProductDetail>();

            decimal quantity = 0;
            decimal deliveredQty = 0;
            decimal salesQty = 0;
            decimal dueQty = 0;
            foreach (UltraGridRow row in grvDeliveryProduct.Rows)
            {
                DeliveryProductDetail deliveryProductDetail = new DeliveryProductDetail();
                Product product = DataAccessProxy.GetProductByID(row.Cells["ProductID"].Text);
                decimal.TryParse(row.Cells[MTBFConstants.GridHeader.QUANTITY].Text, out quantity);
                decimal.TryParse(row.Cells["Sales Quantity"].Text, out salesQty);
                decimal.TryParse(row.Cells["Delivered Quantity"].Text, out deliveredQty);
                decimal.TryParse(row.Cells["Due"].Text, out dueQty);

                deliveryProductDetail.ProductName = row.Cells["Product Name"].Text;
                deliveryProductDetail.ProductID = row.Cells["ProductID"].Text;
                deliveryProductDetail.Quantity = quantity;
                deliveryProductDetail.DeliveredQuantity = deliveredQty;
                deliveryProductDetail.SalesQuantity = salesQty;
                deliveryProductDetail.Due = dueQty;

                lstDeliveryProductDetail.Add(deliveryProductDetail);
            }

            return lstDeliveryProductDetail;
        }

        /// <summary>
        /// Method to get all delivered quantity.
        /// </summary>
        /// <param name="salesOrderNumber"></param>
        /// <param name="productCode"></param>
        /// <returns></returns>
        private decimal GetDeliveredQuantity(int salesID, string productCode)
        {
            decimal deliveredQty = 0;
            foreach (DeliveryProduct deliveryProduct in DataAccessProxy.GetDeliveryProductBySalesID(salesID))
            {
                List<DeliveryProductDetail> lstDeliveryProductDetail = DataAccessProxy.GetAllDeliveryProductDetailByDeliveryIDAndProductCode(deliveryProduct.DeliveryID, productCode).Cast<DeliveryProductDetail>().ToList();
                deliveredQty = deliveredQty + GetProductDeliveredQuantity(lstDeliveryProductDetail);
            }
            return deliveredQty;
        }




        private decimal GetProductDeliveredQuantity(List<DeliveryProductDetail> lstDeliveryProductDetail)
        {
            decimal deliveredQty = 0;
            foreach (DeliveryProductDetail item in lstDeliveryProductDetail)
            {
                deliveredQty = deliveredQty + item.Quantity;
            }
            return deliveredQty;
        }

        ///// <summary>
        ///// Method to build sales quotation detail table.
        ///// </summary>
        ///// <returns></returns>
        //private DataTable BuildQuotationDetailTable()
        //{
        //    DataTable table = new DataTable();
        //    table.Columns.Add(MTBFConstants.GridHeader.TYPE_OF_SUPPLY);
        //    table.Columns.Add(MTBFConstants.GridHeader.PRODUCT_NAME);
        //    table.Columns.Add(MTBFConstants.GridHeader.AVAILABLE_QUANTITY);
        //    table.Columns.Add(MTBFConstants.GridHeader.QUANTITY);
        //    table.Columns.Add(MTBFConstants.GridHeader.DISCOUNT);
        //    table.Columns.Add(MTBFConstants.GridHeader.CURRENCY);
        //    table.Columns.Add(MTBFConstants.GridHeader.PRICE);
        //    table.Columns.Add(MTBFConstants.GridHeader.TOTAL);
        //    return table;
        //}

        /// <summary>
        /// Method to load sales order detail information.
        /// </summary>
        /// <param name="salesOrderID"></param>
        private void LoadSalesOrderDetailInformation(int salesOrderID)
        {
            DataTable quotationDetailTable = BuildDeliveryProductTable();

            foreach (SalesOrderDetail detail in DataAccessProxy.GetAllSalesDetailBySalesID(salesOrderID))
            {
                decimal deliveryQty = GetDeliveredQuantity(detail.SalesID, detail.ProductID);
                DataRow row = quotationDetailTable.NewRow();
                row["ProductID"] = detail.ProductID;
                row["Product Name"] = detail.ProductName;
                row["Sales Quantity"] = detail.Quantity;
                row["Delivered Quantity"] = deliveryQty;
                row["Quantity"] = 0.ToString();
                row["Due"] = detail.Quantity - deliveryQty;
                quotationDetailTable.Rows.Add(row);
            }
            grvDeliveryProduct.DataSource = quotationDetailTable;

            grvDeliveryProduct.DisplayLayout.Override.ActiveCellAppearance.Reset();
            grvDeliveryProduct.DisplayLayout.Override.ActiveRowAppearance.Reset();
        }



        ///// <summary>
        ///// Method to calculate total quotation value.
        ///// </summary>
        //private void CalculateOrdertotal()
        //{
        //    decimal totalDiscount = 0;
        //    decimal total = 0;
        //    decimal discount = 0;
        //    decimal rowTotal = 0;
        //    foreach (UltraGridRow row in grvDeliveryProduct.Rows)
        //    {
        //        decimal.TryParse(row.Cells[MTBFConstants.GridHeader.DISCOUNT].Text, out discount);
        //        decimal.TryParse(row.Cells[MTBFConstants.GridHeader.TOTAL].Text, out rowTotal);
        //        total = total + rowTotal;
        //        totalDiscount = totalDiscount + discount;
        //    }

        //    txtTotal.Text = total.ToString("00.00");
        //    txtDiscount.Text = totalDiscount.ToString("00.00");
        //}

        /// <summary>
        /// Method to reset form controls.
        /// </summary>
        private void ResetAllControls()
        {
            this.txtCustomerPhone.Clear();
            this.txtCustomerAddress.Clear();
            this.txtDescription.Clear();
            this.dtpDeliveryDate.Value = DateTime.Now;
            cmbCustomer.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            cmbSoNumber.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            this.grvDeliveryProduct.DataSource = BuildDeliveryProductTable();
        }

        //#endregion
    }
}
