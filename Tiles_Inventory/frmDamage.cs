using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Enums;
using NybSys.MTBF.Utility.Constants;
using NybSys.MTBF.Utility.Helper;
using Infragistics.Win.UltraWinGrid;
using IFMS.Facade;
using IMFS.Common.View;
using IFMS.BLL;

namespace Tiles_Inventory
{
    public partial class frmDamage : BaseForm
    {
        private List<ProductInformationForSales> lstProductInfo = new List<ProductInformationForSales>();
        private int _damageInfoID = 0;

        VMDamage _vmDamage = new VMDamage();

        public frmDamage()
        {
            DataAccessProxy = new FacadeManager();
            InitializeComponent();
        }


        public frmDamage(int damageInfoID, bool isEdit)
        {
            _damageInfoID = damageInfoID;
            IsUpdating = isEdit;
            DataAccessProxy = new FacadeManager();
            InitializeComponent();
        }

        private void frmDamage_Load(object sender, EventArgs e)
        {
            LoadProductInformation();
            if (IsUpdating)
            {
                LoadExistingDamageInformation();
                LoadExistingDamageDetailInformation();
            }
        }

        /// <summary>
        /// Method to load existing damage information.
        /// </summary>
        private void LoadExistingDamageInformation()
        {
            DamageInfo damageInfo = DataAccessProxy.GetDamageInfoByID(_damageInfoID);
            if (damageInfo != null)
            {
                _vmDamage.DamageInfo = damageInfo;
                txtDamageReason.Text = damageInfo.DamageReason;
                dtpDamageDate.Value = damageInfo.DamageDate;
            }
        }


        private void LoadExistingDamageDetailInformation()
        {
            List<DamageDetail> lstDamageDetail = new List<DamageDetail>();
            lstDamageDetail = new DamageManager().GetAllDamageDetailByDamageInfoID(_damageInfoID);

            foreach (DamageDetail damageDetail in lstDamageDetail)
            {
                DataGridViewRow row = grvCreditProductDetails.Rows[0].Clone() as DataGridViewRow;
                int index = grvCreditProductDetails.Rows.Add(row);
                grvCreditProductDetails.Rows[index].Cells["ProductCode"].Value = damageDetail.ProductCode;
                grvCreditProductDetails.Rows[index].Cells["ProductName"].Value = damageDetail.ProductName;
                grvCreditProductDetails.Rows[index].Cells["Quantity"].Value = damageDetail.Quantity.ToString();
                grvCreditProductDetails.Rows[index].Cells["SquareFeet"].Value = damageDetail.SquareFeet;
            }
        }

        /// <summary>
        /// Method to check valid data.
        /// </summary>
        /// <returns></returns>
        private bool ValidFormData()
        {

            if ((cmbProductInformation.Value == null))
            {
                MessageBoxHelper.ShowInformation("You need to select product information.");
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
            if (string.IsNullOrEmpty(txtDamageReason.Text))
            {
                txtDamageReason.Focus();
                MessageBoxHelper.ShowInformation("You need to enter damage reason.");
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
            //  List<ProductInformationForSales> lstProductInfo = new List<ProductInformationForSales>();
            lstProductInfo = DataAccessProxy.GetAllProductForSales(MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<ProductInformationForSales>().Where(p => p.BranchID == MTBFConstants.AppConstants.BranchID && p.OrganizationID == MTBFConstants.AppConstants.OrganizationID).ToList();
            // lstProductInfo = RemoveAllGivenSample(lstProductInfo);
            lstProductInfo = RemoveGridAddedProductQuantity(lstProductInfo);
            // lstProductInfo = RemoveTransferProduct(lstProductInfo);
            //  lstProductInfo = RemoveDamageProduct(lstProductInfo);

            cmbProductInformation.DataSource = lstProductInfo;
            cmbProductInformation.DisplayMember = "ProductName";
            cmbProductInformation.ValueMember = "ProductId";
            cmbProductInformation.DisplayLayout.Bands[0].Columns[0].Hidden = true;
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
                    decimal quantity = Convert.ToDecimal(grvCreditProductDetails.Rows[i].Cells["Quantity"].Value);
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
                }
                else
                {
                    DataGridViewRow row = grvCreditProductDetails.Rows[0].Clone() as DataGridViewRow;
                    int index = grvCreditProductDetails.Rows.Add(row);
                    vat = (price * Quantity) / 100 * product.VAT;
                    grvCreditProductDetails.Rows[index].Cells["ProductCode"].Value = product.ProductID;
                    grvCreditProductDetails.Rows[index].Cells["ProductName"].Value = product.ProductName;
                    grvCreditProductDetails.Rows[index].Cells["Quantity"].Value = Quantity.ToString();
                    grvCreditProductDetails.Rows[rowIndex].Cells["SquareFeet"].Value = Convert.ToDecimal(txtSqureFeet.Text);
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
        private List<DamageDetail> GetAllDamageDetail()
        {
            List<DamageDetail> lstDamageDetail = new List<DamageDetail>();
            foreach (DataGridViewRow row in grvCreditProductDetails.Rows)
            {
                if (row.Cells["ProductCode"].Value != null)
                {
                    DamageDetail damageDetail = new DamageDetail();
                    damageDetail.ProductCode = row.Cells["ProductCode"].Value.ToString();
                    damageDetail.ProductName = row.Cells["ProductName"].Value.ToString();
                    damageDetail.Quantity = Convert.ToDecimal(row.Cells["Quantity"].Value);
                    damageDetail.SquareFeet = Convert.ToDecimal(row.Cells["SquareFeet"].Value);
                    lstDamageDetail.Add(damageDetail);
                }
            }

            return lstDamageDetail;
        }

        /// <summary>
        /// Method to insert transfer information.
        /// </summary>
        /// <returns></returns>
        private int SaveDamageInformation()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            List<DamageDetail> lstDamageDetail = new List<DamageDetail>();
            _vmDamage.DamageInfo.DamageReason = txtDamageReason.Text;
            _vmDamage.DamageInfo.DamageDate = dtpDamageDate.Value;
            _vmDamage.DamageInfo.CreatedBy = MTBFConstants.AppConstants.LoggedinUserID;
            _vmDamage.DamageInfo.Status = (int)MTBFEnums.DamageStatus.Issued;
            _vmDamage.DamageInfo.BranchID = MTBFConstants.AppConstants.BranchID;
            _vmDamage.DamageInfo.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            lstDamageDetail = GetAllDamageDetail();

            _vmDamage.lstDamageDetail = lstDamageDetail;
            new DamageManager().SaveDamageInformation(_vmDamage);

            return result;
        }



        private void btCreditSave_Click(object sender, EventArgs e)
        {
            if (ValidInsertionData())
            {
                btCreditSave.Enabled = false;
                if (SaveDamageInformation() == (int)MTBFEnums.ReturnResult.SUCCESS)
                {
                    MessageBoxHelper.ShowInformation("Damage information saved successfully.");
                    ResetAllControls();
                }
                else
                {
                    MessageBoxHelper.ShowInformation("Failed to save damage information.");
                }

            }
        }

        private void ResetAllControls()
        {
            _vmDamage = new VMDamage();
            txtDamageReason.Clear();
            grvCreditProductDetails.Rows.Clear();
            btCreditSave.Enabled = true;

        }

        private void btCreditClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            LoadProductInformation();
        }

        private void UltraQuantity_KeyUp(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 13)
            {
                if (ValidFormData())
                {
                    AddProductInGrid();
                    LoadProductInformation();
                }
            }
        }

        private void UltraQuantity_ValueChanged(object sender, EventArgs e)
        {
            decimal quantity = 0;
            decimal size = 0;
            decimal.TryParse(UltraQuantity.Value.ToString(), out quantity);
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
            if (row != null)
            {
                Product product = DataAccessProxy.GetProductByID(cmbProductInformation.Value.ToString());
                ProductSize productSize = DataAccessProxy.GetProductSizeByID(product.ProductSizeID);
                txtProductSize.Text = (productSize != null) ? productSize.Name : string.Empty;
            }
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (ValidFormData())
            {
                AddProductInGrid();
                LoadProductInformation();
            }
        }

        private void grvCreditProductDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string deleteString = grvCreditProductDetails.CurrentCell.OwningColumn.ToolTipText;
            if (!grvCreditProductDetails.CurrentRow.IsNewRow)
            {
                if (deleteString == "Delete")
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure want to delete this record ?", "Conformation Message", MessageBoxButtons.YesNo);
                    if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                    {
                        grvCreditProductDetails.Rows.Remove(grvCreditProductDetails.CurrentRow);
                    }
                }
            }

        }




    }
}
