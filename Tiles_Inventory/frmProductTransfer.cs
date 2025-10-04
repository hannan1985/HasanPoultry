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
    public partial class frmProductTransfer : BaseForm
    {
        private List<ProductInformationForSales> lstProductInfo = new List<ProductInformationForSales>();

        private int _transferID = 0;

        public frmProductTransfer()
        {
            DataAccessProxy = new FacadeManager();
            InitializeComponent();
        }

        public frmProductTransfer(int transferID, bool isEdit)
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
                    CalculateTotal();
                }
            }
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (ValidFormData())
            {
                AddProductInGrid();
                LoadProductInformation();
                CalculateTotal();
            }
        }


        /// <summary>
        /// Method to check valid data.
        /// </summary>
        /// <returns></returns>
        private bool ValidFormData()
        {
            if (Convert.ToInt32(cmbSalesCenter.Value) == MTBFConstants.DataField.COMBO_DEFAULT_ID)
            {
                MessageBoxHelper.ShowInformation("You need to select sales center.");
                cmbSalesCenter.Focus();
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
            if (Convert.ToInt32(cmbSalesCenter.Value) == MTBFConstants.DataField.COMBO_DEFAULT_ID)
            {
                MessageBoxHelper.ShowInformation("You need to select sales center.");
                cmbSalesCenter.Focus();
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
                ProductSize productSize = DataAccessProxy.GetProductSizeByID(product.ProductSizeID);
                int.TryParse(UltraQuantity.Text, out Quantity);
                decimal.TryParse(txtPrice.Text, out price);

                if (IsExistingProduct(product, out rowIndex))
                {
                    decimal previousQuantity = 0;
                    decimal.TryParse(grvCreditProductDetails.Rows[rowIndex].Cells["Quantity"].Value.ToString(), out previousQuantity);
                    decimal.TryParse(grvCreditProductDetails.Rows[rowIndex].Cells["Price"].Value.ToString(), out price);
                    grvCreditProductDetails.Rows[rowIndex].Cells["Quantity"].Value = previousQuantity + Quantity;
                    grvCreditProductDetails.Rows[rowIndex].Cells["Total"].Value = (price * (Quantity + previousQuantity)).ToString("F2");
                }
                else
                {
                    DataGridViewRow row = grvCreditProductDetails.Rows[0].Clone() as DataGridViewRow;
                    int index = grvCreditProductDetails.Rows.Add(row);
                    vat = (price * Quantity) / 100 * product.VAT;
                    grvCreditProductDetails.Rows[index].Cells["ProductCode"].Value = product.ProductID;
                    grvCreditProductDetails.Rows[index].Cells["ProductName"].Value = product.ProductName;
                    grvCreditProductDetails.Rows[index].Cells["Quantity"].Value = Quantity.ToString();
                    grvCreditProductDetails.Rows[rowIndex].Cells["Price"].Value = price;
                    grvCreditProductDetails.Rows[rowIndex].Cells["Total"].Value = (price * Quantity).ToString("F2");
                }

                grvCreditProductDetails.Columns[0].Visible = false;
                cmbProductInformation.Text = string.Empty;

                cmbProductInformation.Focus();
                UltraQuantity.Text = 0.ToString();
                txtPrice.Text = 0.ToString();
            }
        }


        private void CalculateTotal()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in grvCreditProductDetails.Rows)
            {
                if (row.Cells["Total"].Value != null)
                {
                    if (!string.IsNullOrEmpty(row.Cells["Total"].Value.ToString()))
                    {
                        total = total + Convert.ToDecimal(row.Cells["Total"].Value);
                    }
                }

            }

            txtGrandTotal.Text = total.ToString("F2");
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
        /// Method to load sales center combo.
        /// </summary>
        private void LoadSalesCenterCombo()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("Name");

            DataRow emptyRrow = table.NewRow();
            emptyRrow["ID"] = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            emptyRrow["Name"] = MTBFConstants.DataField.COMBO_DEFAULT_NAME;
            table.Rows.Add(emptyRrow);


            foreach (SalesCenter salesCenter in DataAccessProxy.GetAllSalesCenter().Where(s => s.BranchID == MTBFConstants.AppConstants.BranchID && s.OrganizationID == MTBFConstants.AppConstants.OrganizationID))
            {
                DataRow row = table.NewRow();
                row["ID"] = salesCenter.SalesCenterID;
                row["Name"] = salesCenter.SalesCenterName;
                table.Rows.Add(row);
            }
            cmbSalesCenter.DataSource = table;
            cmbSalesCenter.DisplayMember = "Name";
            cmbSalesCenter.ValueMember = "ID";
            cmbSalesCenter.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
        }

        /// <summary>
        /// Method to get all transfer detail.
        /// </summary>
        /// <returns></returns>
        private List<TransferDetail> GetAllTransferDetail()
        {
            List<TransferDetail> lstTransferDetail = new List<TransferDetail>();
            foreach (DataGridViewRow row in grvCreditProductDetails.Rows)
            {
                if (row.Cells["ProductCode"].Value != null)
                {
                    TransferDetail transferDetail = new TransferDetail();
                    transferDetail.ProductCode = row.Cells["ProductCode"].Value.ToString();
                    transferDetail.ProductName = row.Cells["ProductName"].Value.ToString();
                    transferDetail.Quantity = Convert.ToDecimal(row.Cells["Quantity"].Value);
                    transferDetail.Price = Convert.ToDecimal(row.Cells["Price"].Value);
                    lstTransferDetail.Add(transferDetail);
                }
            }

            return lstTransferDetail;
        }

        /// <summary>
        /// Method to insert transfer information.
        /// </summary>
        /// <returns></returns>
        private int InsertTransferInformation()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            List<TransferDetail> lstTransferDetail = new List<TransferDetail>();
            Transfer transfer = new Transfer();
            transfer.TransferDate = dtpTransferDate.Value;
            transfer.Total = Convert.ToDecimal(txtGrandTotal.Text);
            transfer.SalesCenterID = Convert.ToInt32(cmbSalesCenter.Value);
            transfer.TransferBy = MTBFConstants.AppConstants.LoggedinUserID;
            transfer.BranchID = MTBFConstants.AppConstants.BranchID;
            transfer.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            lstTransferDetail = GetAllTransferDetail();
            DataAccessProxy.InsertTransferInformation(transfer, lstTransferDetail);

            return result;
        }

        /// <summary>
        /// Method to update transfer information
        /// </summary>
        /// <returns></returns>
        private int UpdateTransferInformation()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            List<TransferDetail> lstTransferDetail = new List<TransferDetail>();
            Transfer transfer = DataAccessProxy.GetTransferByID(_transferID);
            transfer.TransferDate = dtpTransferDate.Value;
            transfer.SalesCenterID = Convert.ToInt32(cmbSalesCenter.Value);
            transfer.UpdatedBy = MTBFConstants.AppConstants.LoggedinUserID;
            transfer.UpdatedDate = DateTime.Now;
            lstTransferDetail = GetAllTransferDetail();
            DataAccessProxy.UpdateTransferInformation(transfer, lstTransferDetail);

            return result;
        }


        private void frmProductTransfer_Load(object sender, EventArgs e)
        {
            LoadSalesCenterCombo();
            LoadProductInformation();
            if (IsUpdating)
            {
                LoadExistingTransferInformation(_transferID);
                LoadExistingTransferDetailInformation(_transferID);
                CalculateTotal();
            }
        }

        private void LoadExistingTransferInformation(int _transferID)
        {
            Transfer transfer = DataAccessProxy.GetTransferByID(_transferID);
            cmbSalesCenter.Value = transfer.SalesCenterID;
            dtpTransferDate.Value = transfer.TransferDate;
            cmbSalesCenter.Enabled = false;
        }

        private void LoadExistingTransferDetailInformation(int _tranferID)
        {
            foreach (TransferDetail transferDetail in DataAccessProxy.GetAllTransferDetailByTransferID(_transferID))
            {
                DataGridViewRow row = grvCreditProductDetails.Rows[0].Clone() as DataGridViewRow;
                int index = grvCreditProductDetails.Rows.Add(row);

                grvCreditProductDetails.Rows[index].Cells["ProductCode"].Value = transferDetail.ProductCode;
                grvCreditProductDetails.Rows[index].Cells["ProductName"].Value = transferDetail.ProductName;
                grvCreditProductDetails.Rows[index].Cells["Quantity"].Value = transferDetail.Quantity;
                grvCreditProductDetails.Rows[index].Cells["Price"].Value = transferDetail.Price;
                grvCreditProductDetails.Rows[index].Cells["Total"].Value = (transferDetail.Price * transferDetail.Quantity).ToString("F2");
            }
        }

        private void cmbSalesCenter_ValueChanged(object sender, EventArgs e)
        {
            if (cmbSalesCenter.Value != null)
            {
                SalesCenter salesCenter = DataAccessProxy.GetSalesCenterByID(Convert.ToInt32(cmbSalesCenter.Value));
                if (salesCenter != null)
                {
                    txtResponsiblePerson.Text = salesCenter.ResponsiblePerson;
                    txtSalesCenterAddress.Text = salesCenter.Address;
                    cmbProductInformation.Focus();
                }
                else
                {
                    txtResponsiblePerson.Clear();
                    txtSalesCenterAddress.Clear();
                }
            }
        }

        private void UltraQuantity_ValueChanged(object sender, EventArgs e)
        {
            decimal quantity = 0;
            decimal size = 0;
            decimal.TryParse(UltraQuantity.Text, out quantity);
            decimal.TryParse(txtProductSize.Text, out size);

            txtSqureFeet.Text = (quantity * size).ToString();
        }

        private void cmbProductInformation_KeyUp(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 13 && cmbProductInformation.Text != string.Empty)
            {
                UltraQuantity.Focus();
            }
        }

        private void cmbProductInformation_Leave(object sender, EventArgs e)
        {
            UltraGridRow row = cmbProductInformation.SelectedRow;
            if (cmbProductInformation.Value != null)
            {
                Product product = DataAccessProxy.GetProductByID(cmbProductInformation.Value.ToString());
                if (product != null && product.BranchID == MTBFConstants.AppConstants.BranchID && product.OrganizationID == MTBFConstants.AppConstants.OrganizationID)
                {
                    ProductSize productSize = DataAccessProxy.GetProductSizeByID(product.ProductSizeID);
                    txtProductSize.Text = (productSize != null) ? productSize.Name : string.Empty;
                }
                else
                {
                    MessageBoxHelper.ShowInformation("Invalid product.");
                    cmbProductInformation.Focus();
                }

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
                    if (UpdateTransferInformation() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Transfer information saved successfully.");
                        IsUpdating = false;
                        _transferID = 0;
                        grvCreditProductDetails.Rows.Clear();
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save transfer information.");
                    }
                }
                else
                {

                    if (InsertTransferInformation() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Transfer information saved successfully.");
                        grvCreditProductDetails.Rows.Clear();
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save transfer information.");
                    }
                }
            }

        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            LoadSalesCenterCombo();
            LoadProductInformation();
        }

        private void btReset_Click(object sender, EventArgs e)
        {
            cmbSalesCenter.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            txtSqureFeet.Clear();

        }

    }
}
