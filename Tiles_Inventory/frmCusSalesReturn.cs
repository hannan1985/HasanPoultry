using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Constants;
using NybSys.MTBF.Utility.Helper;
using Infragistics.Win.UltraWinGrid;
using NybSys.MTBF.Utility.Enums;
using IMFS.Common.Utility;
using IFMS.Facade;
using IFMS.BLL;

namespace Tiles_Inventory
{
    public partial class frmCusSalesReturn : BaseForm
    {
        private List<ProductInformationForSales> lstProductInfo = new List<ProductInformationForSales>();

        private int _transferID = 0;

        public frmCusSalesReturn()
        {
            DataAccessProxy = new FacadeManager();
            InitializeComponent();
        }

        public frmCusSalesReturn(int transferID, bool isEdit)
        {
            _transferID = transferID;
            IsUpdating = isEdit;

            DataAccessProxy = new FacadeManager();
            InitializeComponent();
        }

        private void UltraQuantity_KeyUp(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 13)
            {
                if (ValidFormData())
                {
                    AddProductInGrid();
                    LoadProductInformation();
                    txtTotalAmount.Text = CalculateTotalPurchaseCost().ToString("F2");
                    // txtPaidAmount.Text = txtTotalAmount.Text;
                }
            }
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (ValidFormData())
            {
                AddProductInGrid();
                LoadProductInformation();
                txtTotalAmount.Text = CalculateTotalPurchaseCost().ToString("F2");
                // txtPaidAmount.Text = txtTotalAmount.Text;
            }
        }


        /// <summary>
        /// Method to check valid data.
        /// </summary>
        /// <returns></returns>
        private bool ValidFormData()
        {
            if (cmbCustomerName.Value == null)
            {
                MessageBoxHelper.ShowInformation("You need to select customer name.");
                cmbCustomerName.Focus();
                return false;
            }

            if ((cmbProductInformation.Value == null))
            {
                MessageBoxHelper.ShowInformation("You need to select product name.");
                cmbProductInformation.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Method to valid intertion data.
        /// </summary>
        /// <returns></returns>
        private bool ValidInsertionData()
        {

            if (cmbCustomerName.Value == null && Convert.ToInt32(cmbCustomerName.Value) == MTBFConstants.DataField.COMBO_DEFAULT_ID)
            {
                MessageBoxHelper.ShowInformation("You need to select customer name.");
                cmbCustomerName.Focus();
                return false;
            }

            if (grvCreditProductDetails.Rows.Count < 2)
            {
                MessageBoxHelper.ShowInformation("Please add information in grid.");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Method to load product information in combo box.
        /// </summary>
        private void LoadProductInformation()
        {
            List<ProductModel> lstProductModel = new List<ProductModel>();
            List<Product> lstProduct = new List<Product>();
            lstProduct = new ProductManager().GetAllProduct(MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<Product>().ToList();

            int[] productModelID = lstProduct.Select(p => p.ProductModelID).Distinct().ToArray();
            if (productModelID.Length > 0)
            {
                string modelfilter = String.Format("{0} IN ({1})", "ProductModelID", String.Join(",", productModelID));
                lstProductModel = DataAccessProxy.GetFilteedProductModel(modelfilter);
            }





            DataTable dt = new DataTable();
            dt.Columns.Add("ProductID");
            dt.Columns.Add("Model");
            dt.Columns.Add("ProductName");
            foreach (Product product in lstProduct)
            {
                ProductModel productModel = lstProductModel.Where(m => m.ProductModelID == product.ProductModelID).FirstOrDefault();
                DataRow row = dt.NewRow();
                row["ProductID"] = product.ProductID;
                row["Model"] = (productModel != null) ? productModel.Name : string.Empty;
                row["ProductName"] = product.ProductName;
                dt.Rows.Add(row);
            }

            cmbProductInformation.DataSource = dt;
            cmbProductInformation.DisplayMember = "ProductName";
            cmbProductInformation.ValueMember = "ProductID";
            cmbProductInformation.DisplayLayout.Bands[0].Columns["ProductID"].Hidden = true;

            cmbProductInformation.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
            cmbProductInformation.DisplayLayout.PerformAutoResizeColumns(false, PerformAutoSizeType.AllRowsInBand, true);
        }



        /// <summary>
        /// Method to remove grid added product.
        /// </summary>
        /// <param name="lstProductInfo"></param>
        /// <returns></returns>
        private List<ProductInformationForSales> RemoveGridAddedProductQuantity(List<ProductInformationForSales> lstProductInfo)
        {
            ProductInformationForSales product = null;

            for (int i = 0; i < grvCreditProductDetails.Rows.Count - 1; i++)
            {
                if (grvCreditProductDetails.Rows[i].Cells["ProductCode"].Value != null)
                {
                    string productID = grvCreditProductDetails.Rows[i].Cells["ProductCode"].Value.ToString();
                    int quantity = Convert.ToInt32(grvCreditProductDetails.Rows[i].Cells["Quantity"].Value);
                    //  int purchaseID = Convert.ToInt32(grvCreditProductDetails.Rows[i].Cells["PurchaseID"].Value);
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
        /// Method to add product information in grid.
        /// </summary>
        private void AddProductInGrid()
        {
            int Quantity = 0;
            decimal price = default(decimal);
            decimal vat = 0;
            int rowIndex = 0;


            if ((cmbProductInformation.Value != null))
            {
                Product product = DataAccessProxy.GetProductInformationByID(cmbProductInformation.Value.ToString());
                price = Convert.ToDecimal(txtPrice.Text);
                ProductSize productSize = DataAccessProxy.GetProductSizeByID(product.ProductSizeID);
                Quantity = Convert.ToInt32(UltraQuantity.Value);


                if (IsExistingProduct(product, out rowIndex))
                {
                    decimal previousQuantity = 0;
                    decimal.TryParse(grvCreditProductDetails.Rows[rowIndex].Cells["Quantity"].Value.ToString(), out previousQuantity);


                    vat = (price * (Quantity + previousQuantity)) / 100 * product.VAT;
                    grvCreditProductDetails.Rows[rowIndex].Cells["UnitPrice"].Value = price;
                    grvCreditProductDetails.Rows[rowIndex].Cells["Quantity"].Value = previousQuantity + Quantity;

                    grvCreditProductDetails.Rows[rowIndex].Cells["Total"].Value = price * (previousQuantity + Quantity);
                }
                else
                {
                    DataGridViewRow row = grvCreditProductDetails.Rows[0].Clone() as DataGridViewRow;
                    int index = grvCreditProductDetails.Rows.Add(row);
                    vat = (price * Quantity) / 100 * product.VAT;
                    grvCreditProductDetails.Rows[index].Cells["ProductCode"].Value = product.ProductID;
                    grvCreditProductDetails.Rows[index].Cells["ProductName"].Value = product.ProductName;
                    grvCreditProductDetails.Rows[rowIndex].Cells["UnitPrice"].Value = price;
                    grvCreditProductDetails.Rows[index].Cells["Quantity"].Value = Quantity.ToString();
                    grvCreditProductDetails.Rows[rowIndex].Cells["Total"].Value = price * Quantity;
                }

                grvCreditProductDetails.Columns[0].Visible = false;
                cmbProductInformation.Text = string.Empty;

                cmbProductInformation.Focus();
                UltraQuantity.Value = 0;
            }
        }

        /// <summary>
        /// Method to cehck exiting product.
        /// </summary>
        /// <param name="product"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        private bool IsExistingProduct(Product product, out int rowIndex)
        {
            rowIndex = 0;
            foreach (DataGridViewRow row in grvCreditProductDetails.Rows)
            {
                if (row.Cells["ProductName"].Value != null)
                {
                    if (row.Cells["ProductName"].Value.ToString().Trim().ToLower() == product.ProductName.Trim().ToLower())
                    {
                        rowIndex = row.Index;
                        return true;
                    }
                    rowIndex++;
                }
            }
            return false;
        }

        /// <summary>
        /// Method to get all transfer detail.
        /// </summary>
        /// <returns></returns>
        private List<SalesReturnDetail> GetAllSalesReturnDetail()
        {
            List<SalesReturnDetail> lstSalesReturnDetail = new List<SalesReturnDetail>();
            foreach (DataGridViewRow row in grvCreditProductDetails.Rows)
            {
                if (row.Cells["ProductCode"].Value != null)
                {
                    SalesReturnDetail salesReturnDetail = new SalesReturnDetail();
                    salesReturnDetail.ProductCode = row.Cells["ProductCode"].Value.ToString();
                    salesReturnDetail.ProductName = row.Cells["ProductName"].Value.ToString();
                    salesReturnDetail.Price = Convert.ToDecimal(row.Cells["UnitPrice"].Value);
                    salesReturnDetail.Quantity = Convert.ToDecimal(row.Cells["Quantity"].Value);
                    lstSalesReturnDetail.Add(salesReturnDetail);
                }
            }

            return lstSalesReturnDetail;
        }

        /// <summary>
        /// Method to insert transfer information.
        /// </summary>
        /// <returns></returns>
        private int InsertSalesReturnInformation()
        {
            decimal paidAmount = 0;

            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            List<SalesReturnDetail> lstSalesReturnDetail = new List<SalesReturnDetail>();
            SalesReturn salesReturn = new SalesReturn();
            salesReturn.ReturnDate = dtpTransferDate.Value;
            salesReturn.CustomerName = cmbCustomerName.Text;
            salesReturn.CustomerID = Convert.ToInt32(cmbCustomerName.Value);
            salesReturn.Phone = txtPhone.Text;
            salesReturn.ShortNote = txtShortNote.Text;
            salesReturn.CreatedBy = MTBFConstants.AppConstants.LoggedinUserID;
            salesReturn.BranchID = MTBFConstants.AppConstants.BranchID;
            salesReturn.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            salesReturn.Total = Convert.ToDecimal(txtTotalAmount.Text);
            decimal discount = 0;
            decimal.TryParse(txtDiscount.Text, out discount);
            salesReturn.Discount = discount;
            decimal.TryParse(txtPaidAmount.Text, out paidAmount);
            salesReturn.PaidAmount = paidAmount;
            lstSalesReturnDetail = GetAllSalesReturnDetail();
            result = new SalesReturnManager().InsertSalesReturnInformation(salesReturn, lstSalesReturnDetail);

            return result;
        }

        /// <summary>
        /// Method to update transfer information
        /// </summary>
        /// <returns></returns>
        private int UpdateSalesReturnInformation()
        {
            decimal paidAmount = 0;
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            List<SalesReturnDetail> lstSalesReturnDetail = new List<SalesReturnDetail>();
            SalesReturn salesReturn = new SalesReturnManager().GetSalesReturnByID(_transferID);
            salesReturn.ReturnDate = dtpTransferDate.Value;
            salesReturn.CustomerName = cmbCustomerName.Text;
            salesReturn.CustomerID = Convert.ToInt32(cmbCustomerName.Value);
            salesReturn.Phone = txtPhone.Text;
            salesReturn.ShortNote = txtShortNote.Text;
            decimal discount = 0;
            decimal.TryParse(txtDiscount.Text, out discount);
            salesReturn.Discount = discount;
            decimal.TryParse(txtPaidAmount.Text, out paidAmount);
            salesReturn.PaidAmount = paidAmount;
            salesReturn.Total = Convert.ToDecimal(txtTotalAmount.Text);
            salesReturn.UpdatedBy = MTBFConstants.AppConstants.LoggedinUserID;
            salesReturn.UpdatedDate = DateTime.Now;
            lstSalesReturnDetail = GetAllSalesReturnDetail();
            result = new SalesReturnManager().UpdateSalesReturnInformation(salesReturn, lstSalesReturnDetail);

            return result;
        }


        private void frmProductTransfer_Load(object sender, EventArgs e)
        {
            LoadProductInformation();
            LoadCustomerCombo();
            if (IsUpdating)
            {
                LoadExistingSalesReturnDetailInformation(_transferID);
                LoadExistingTransferInformation(_transferID);

            }
        }

        private void LoadExistingTransferInformation(int _transferID)
        {
            SalesReturn transfer = new SalesReturnManager().GetSalesReturnByID(_transferID);
            cmbCustomerName.Value = transfer.CustomerID;
            txtPhone.Text = transfer.Phone;
            dtpTransferDate.Value = transfer.ReturnDate;
            txtDiscount.Text = transfer.Discount.ToString();
            txtShortNote.Text = transfer.ShortNote;
            txtGrandTotal.Text = (transfer.Total - transfer.Discount).ToString("F2");
            txtPaidAmount.Text = transfer.PaidAmount.ToString("F2");
        }

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
            foreach (Customer customer in lstCustomer)
            {
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

        private void LoadExistingSalesReturnDetailInformation(int salesReturnID)
        {
            foreach (SalesReturnDetail salesReturnDetail in new SalesReturnManager().GetAllSalesReturnDetailBySalesID(salesReturnID))
            {
                DataGridViewRow row = grvCreditProductDetails.Rows[0].Clone() as DataGridViewRow;
                int index = grvCreditProductDetails.Rows.Add(row);

                grvCreditProductDetails.Rows[index].Cells["ProductCode"].Value = salesReturnDetail.ProductCode;
                grvCreditProductDetails.Rows[index].Cells["ProductName"].Value = salesReturnDetail.ProductName;
                grvCreditProductDetails.Rows[index].Cells["UnitPrice"].Value = salesReturnDetail.Price;
                grvCreditProductDetails.Rows[index].Cells["Quantity"].Value = salesReturnDetail.Quantity;
                grvCreditProductDetails.Rows[index].Cells["Total"].Value = salesReturnDetail.Quantity * salesReturnDetail.Price;
            }


            txtTotalAmount.Text = CalculateTotalPurchaseCost().ToString("F2");

        }



        private void UltraQuantity_ValueChanged(object sender, EventArgs e)
        {



        }

        private void cmbProductInformation_KeyUp(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 13 && cmbProductInformation.Text != string.Empty)
            {
                txtPrice.Focus();
            }
        }

        private void cmbProductInformation_Leave(object sender, EventArgs e)
        {
            UltraGridRow row = cmbProductInformation.SelectedRow;
            if (row != null)
            {
                Product product = DataAccessProxy.GetProductByID(cmbProductInformation.Value.ToString());
                ProductSize productSize = DataAccessProxy.GetProductSizeByID(product.ProductSizeID);
                txtProductSize.Text = (productSize != null) ? productSize.Name : string.Empty;

                List<StockPrice> lstStockPrice = new List<StockPrice>();
                List<PurchaseOrderDetail> lstPurcahseOrder = new List<PurchaseOrderDetail>();
                string filter = String.Format("{0} IN ({1})", "ProductID", cmbProductInformation.Value.ToString());
                lstPurcahseOrder = DataAccessProxy.GetPurcahseOrderDetailFiltered(filter).Cast<PurchaseOrderDetail>().ToList();
                lstStockPrice = new StockPriceManager().GetFilteredStockPrice(filter);

                StockPrice stockPrice = lstStockPrice.Where(s => s.ProductID == cmbProductInformation.Value.ToString()).FirstOrDefault();
                PurchaseOrderDetail pDetail = lstPurcahseOrder.Where(p => p.ProductID == cmbProductInformation.Value.ToString()).Cast<PurchaseOrderDetail>().FirstOrDefault();
                decimal purchasePrice = (stockPrice != null) ? stockPrice.Price : 0;

                if (purchasePrice == 0)
                {
                    purchasePrice = (pDetail != null) ? pDetail.PurchasePrice : 1;
                }



                txtPrice.Value = purchasePrice;
            }
        }

        private void btCreditClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvCreditProductDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!grvCreditProductDetails.Rows[0].IsNewRow)
                {
                    string celltext = grvCreditProductDetails.CurrentCell.OwningColumn.HeaderText;
                    if (celltext == "Delete")
                    {
                        DialogResult m = MessageBoxHelper.ShowConfirmation("Do you want to delete?");
                        if (m == System.Windows.Forms.DialogResult.Yes)
                        {
                            grvCreditProductDetails.Rows.Remove(grvCreditProductDetails.CurrentRow);
                            txtTotalAmount.Text = CalculateTotalPurchaseCost().ToString("F2");
                            // txtPaidAmount.Text = txtTotalAmount.Text;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBoxHelper.ShowInformation("Operation fail Please try again");
            }
        }

        private void btCreditSave_Click(object sender, EventArgs e)
        {
            if (ValidInsertionData())
            {
                if (IsUpdating)
                {
                    if (UpdateSalesReturnInformation() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Sales return information saved successfully.");
                        IsUpdating = false;
                        _transferID = 0;
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save sales return information.");
                    }
                }
                else
                {

                    if (InsertSalesReturnInformation() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Sales return information saved successfully.");
                        grvCreditProductDetails.Rows.Clear();
                        txtPhone.Clear();
                        cmbCustomerName.Focus();
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save sales return information.");
                    }
                }
            }

        }


        /// <summary>
        /// Method to calculate total purchase cost.
        /// </summary>
        /// <returns></returns>
        //private decimal CalculateTotalPurchaseCost()
        //{

        //    decimal total = 0;


        //    foreach (DataGridViewRow row in grvCreditProductDetails.Rows)
        //    {
        //        if (row.Cells["Total"].Value != null)
        //        {
        //            decimal sum = 0;
        //            if (row.Cells["Total"].Value != null)
        //            {
        //                decimal.TryParse(row.Cells["Total"].Value.ToString(), out sum);
        //            }
        //            total = total + sum;
        //        }


        //    }


        //    return total;
        //}



        private void btRefresh_Click(object sender, EventArgs e)
        {

            LoadProductInformation();
        }

        private void btReset_Click(object sender, EventArgs e)
        {
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {

            decimal discount = 0;
            decimal total = 0;
            decimal.TryParse(txtTotalAmount.Text, out total);
            decimal.TryParse(txtDiscount.Text, out discount);
            txtGrandTotal.Text = (total - discount).ToString("F2");
            // txtPaidAmount.Text = (total - discount).ToString("F2");
        }

        private void cmbCustomerName_ValueChanged(object sender, EventArgs e)
        {
            int customerID = 0;
            if (cmbCustomerName.Value != null)
            {
                int.TryParse(cmbCustomerName.Value.ToString(), out customerID);
                Customer custoemr = new CustomerManager().GetCustomerByID(customerID);
                if (custoemr != null)
                {
                    txtPhone.Text = custoemr.Phone;
                }
            }
        }

        private decimal CalculateTotalPurchaseCost()
        {
            decimal sum = 0;
            decimal total = 0;
            decimal mrp = 0;
            decimal quantity = 0;
            decimal squareFeet = 0;
            for (int i = 0; i <= grvCreditProductDetails.Rows.Count - 2; i++)
            {
                if (grvCreditProductDetails.Rows[i].Cells["UnitPrice"].Value != null)
                {
                    decimal.TryParse(grvCreditProductDetails.Rows[i].Cells["UnitPrice"].Value.ToString(), out mrp);
                }
                if (grvCreditProductDetails.Rows[i].Cells["Quantity"].Value != null)
                {
                    decimal.TryParse(grvCreditProductDetails.Rows[i].Cells["Quantity"].Value.ToString(), out quantity);
                }

                total = quantity * mrp;
                grvCreditProductDetails.Rows[i].Cells["Total"].Value = total.ToString("F2");
                sum = sum + total;
            }

            return sum;
        }

        private void grvCreditProductDetails_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (grvCreditProductDetails.Rows.Count > 1)
            {
                txtTotalAmount.Text = CalculateTotalPurchaseCost().ToString("F2");
            }

        }

        private void txtDiscountPercent_TextChanged(object sender, EventArgs e)
        {
            decimal total = 0;
            decimal percentage = 0;
            if (!string.IsNullOrEmpty(txtDiscountPercent.Text))
            {
                decimal.TryParse(txtTotalAmount.Text, out total);
                decimal.TryParse(txtDiscountPercent.Text, out percentage);
                txtDiscount.Text = ((total / 100) * percentage).ToString("F2");
            }
            else
            {
                txtDiscount.Text = 0.ToString();
            }
        }

    }
}
