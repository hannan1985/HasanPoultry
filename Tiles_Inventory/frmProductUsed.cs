using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IMFS.Common.View;
using IMFS.Common.DTO;
using IFMS.BLL;
using NybSys.MTBF.Utility.Helper;
using NybSys.MTBF.Utility.Constants;
using NybSys.MTBF.Utility.Enums;

namespace Tiles_Inventory
{
    public partial class frmProductUsed : BaseForm
    {
        private VMProductUsed _vmProductUsed = new VMProductUsed();
        List<TransferProductPrice> lstTransferProductPrice = new List<TransferProductPrice>();
        VMReceiveProduct vmReceiveProduct = new VMReceiveProduct();

        public frmProductUsed()
        {
            InitializeComponent();
        }

        public frmProductUsed(VMProductUsed vmProductUsed, bool isEdit)
        {
            _vmProductUsed = vmProductUsed;
            this.IsUpdating = isEdit;
            InitializeComponent();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (_vmProductUsed.VMReceiveProduct != null && _vmProductUsed.VMReceiveProduct.ReceiveProduct.ReceiveProductID > 0)
            {
                frmBranchReceive frm = new frmBranchReceive(1, _vmProductUsed.VMReceiveProduct, true);
                frm.OnGetreceiveProduct += new frmBranchReceive.LodaEventHandaler(frm_OnGetreceiveProduct);
                frm.ShowDialog();
            }
            else
            {
                frmBranchReceive frm = new frmBranchReceive(1, _vmProductUsed.VMReceiveProduct, false);
                frm.OnGetreceiveProduct += new frmBranchReceive.LodaEventHandaler(frm_OnGetreceiveProduct);
                frm.ShowDialog();
            }



        }

        void frm_OnGetreceiveProduct(VMReceiveProduct vmReceiveProduct)
        {
            if (vmReceiveProduct.lstRecevieProductDetail.Count > 0)
            {
                if (SaveProductConsumed(vmReceiveProduct) == (int)MTBFEnums.ReturnResult.SUCCESS)
                {
                    MessageBoxHelper.ShowInformation("Information saved successfully");
                    grvProductDetails.DataSource = BuildMaterialTable();
                    grvProductDetails.DisplayLayout.Bands[0].Columns["ProductID"].Hidden = true;
                }
                else
                {
                    MessageBoxHelper.ShowInformation("Failed to save information");
                }
            }
            else
            {
                MessageBoxHelper.ShowInformation("Please save receive receive product first.");
            }
        }

        void frm_OnGetreceiveProduct(bool isSaved)
        {


        }

        private int SaveProductConsumed(VMReceiveProduct vmReceiveProduct)
        {
            _vmProductUsed.VMReceiveProduct = vmReceiveProduct;
            _vmProductUsed.ProductUsed.UsedDate = dtpConsumedDate.Value;
            _vmProductUsed.ProductUsed.PartyID = Convert.ToInt32(cmbPartyName.Value);
            _vmProductUsed.ProductUsed.Total = Convert.ToDecimal(txtTotal.Text);
            return new ProductUsedManager().SaveProductUsed(_vmProductUsed);
        }
        private void btAdd_Click(object sender, EventArgs e)
        {
            if (ValidAddData())
            {
                ProductUsedDetail productUsedDetail = new ProductUsedDetail();
                productUsedDetail.ProductID = cmbProductInformation.Value.ToString();
                productUsedDetail.Price = Convert.ToDecimal(txtPrice.Value);
                productUsedDetail.Quantity = Convert.ToDecimal(txtQuantity.Value);
                productUsedDetail.Total = productUsedDetail.Price * productUsedDetail.Quantity;
                _vmProductUsed.lstProductUsedDetail.Add(productUsedDetail);
                AddDataInGrid();
                txtQuantity.Value = 0;
                txtPrice.Value = 0;
                cmbProductInformation.Focus();
            }
        }

        private DataTable BuildMaterialTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ProductID");
            table.Columns.Add("Product Name");
            table.Columns.Add("Quantity");
            table.Columns.Add("Price");
            table.Columns.Add("Total");
            table.Columns.Add("Delete");
            return table;

        }

        private void AddDataInGrid()
        {
            DataTable dt = BuildMaterialTable();
            decimal total = 0;
            foreach (ProductUsedDetail pUsedDetail in _vmProductUsed.lstProductUsedDetail)
            {
                TransferProductPrice product = lstTransferProductPrice.Where(p => p.ProductID == pUsedDetail.ProductID).FirstOrDefault();
                DataRow row = dt.NewRow();
                row["ProductID"] = pUsedDetail.ProductID;
                row["Product Name"] = (product != null) ? product.ProductName : string.Empty;
                row["Quantity"] = pUsedDetail.Quantity;
                row["Price"] = pUsedDetail.Price;
                row["Total"] = pUsedDetail.Total;
                row["Delete"] = "Delete";
                dt.Rows.Add(row);
                total += pUsedDetail.Total;
            }
            grvProductDetails.DataSource = dt;
            grvProductDetails.DisplayLayout.Bands[0].Columns["ProductID"].Hidden = true;
            txtTotal.Text = total.ToString("F2");
        }

        private bool ValidAddData()
        {

            if (cmbProductInformation.Value != null)
            {
                TransferProductPrice material = lstTransferProductPrice.Where(p => p.ProductID == cmbProductInformation.Value.ToString().Trim()).FirstOrDefault();
                if (material == null)
                {
                    MessageBoxHelper.ShowInformation("Invalid material informaiton.");
                    cmbProductInformation.Focus();
                }

            }

            decimal quantity = 0;
            decimal price = 0;
            decimal.TryParse(txtQuantity.Text, out quantity);
            decimal.TryParse(txtPrice.Text, out price);

            if (quantity == 0)
            {
                MessageBoxHelper.ShowInformation("You need to enter quantity.");
                txtQuantity.Focus();
            }

            if (price == 0)
            {
                MessageBoxHelper.ShowInformation("You need to enter price.");
                txtPrice.Focus();
            }

            return true;
        }

        private void frmProductUsed_Load(object sender, EventArgs e)
        {
            LoadSalesCenterCombo();
            LoadProductCombo();
            if (this.IsUpdating)
            {
                LoadExistingData();
            }
        }

        private void LoadExistingData()
        {
            dtpConsumedDate.Value = _vmProductUsed.ProductUsed.UsedDate;
            cmbPartyName.Value = _vmProductUsed.ProductUsed.PartyID;
            AddDataInGrid();
            txtTotal.Text = _vmProductUsed.ProductUsed.Total.ToString();
            string filter = string.Format("{0}={1}", "ProductUsedID", _vmProductUsed.ProductUsed.ProductUsedID);
            vmReceiveProduct = new ReceiveProductManager().GetFilteredReceiveProduct(filter).FirstOrDefault();

            _vmProductUsed.VMReceiveProduct = vmReceiveProduct;
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


            foreach (SalesCenter salesCenter in new SalesCenterManager().GetAllSalesCenter().Where(s => s.BranchID == MTBFConstants.AppConstants.BranchID && s.OrganizationID == MTBFConstants.AppConstants.OrganizationID))
            {
                DataRow row = table.NewRow();
                row["ID"] = salesCenter.SalesCenterID;
                row["Name"] = salesCenter.SalesCenterName;
                table.Rows.Add(row);
            }
            cmbPartyName.DataSource = table;
            cmbPartyName.DisplayMember = "Name";
            cmbPartyName.ValueMember = "ID";
            cmbPartyName.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
        }

        private void LoadProductCombo()
        {

            lstTransferProductPrice = new TransferManager().GetAllTransferProductPrice();

            foreach (TransferProductPrice transferProductPrice in lstTransferProductPrice)
            {
                transferProductPrice.Price = Convert.ToDecimal(transferProductPrice.Price.ToString("F2"));
            }

            cmbProductInformation.DataSource = lstTransferProductPrice;
            cmbProductInformation.DisplayMember = "ProductName";
            cmbProductInformation.ValueMember = "ProductID";
            cmbProductInformation.DisplayLayout.Bands[0].Columns["ProductID"].Hidden = true;
        }

        private void cmbProductInformation_ValueChanged(object sender, EventArgs e)
        {
            if (cmbProductInformation.Value != null)
            {
                TransferProductPrice product = lstTransferProductPrice.Where(p => p.ProductID == cmbProductInformation.Value.ToString()).FirstOrDefault();
                if (product != null)
                {
                    txtPrice.Value = product.Price.ToString("F2");
                    txtQuantity.Focus();
                }
            }
        }

        private void grvProductDetails_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            if (e.Cell.Column.Header.Caption == "Delete")
            {
                string productID = grvProductDetails.Rows[e.Cell.Row.Index].Cells["ProductID"].Value.ToString();
                int rowIndex = e.Cell.Row.Index;
                if (grvProductDetails.Rows[e.Cell.Row.Index].Delete())
                {
                    ProductUsedDetail sDetail = _vmProductUsed.lstProductUsedDetail.Where(p => p.ProductID == productID).FirstOrDefault();
                    if (sDetail != null)
                    {
                        _vmProductUsed.lstProductUsedDetail.Remove(sDetail);
                    }
                    AddDataInGrid();
                }
            }

        }
    }
}
