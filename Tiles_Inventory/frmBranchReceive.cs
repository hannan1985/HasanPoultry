using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NybSys.MTBF.Utility.Constants;
using IFMS.Facade;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Helper;
using NybSys.MTBF.Utility.Enums;
using Infragistics.Win.UltraWinGrid;
using IMFS.Common.View;
using IFMS.BLL;

namespace Tiles_Inventory
{
    public partial class frmBranchReceive : BaseForm
    {
        private List<ProductInformationForSales> lstProductInfo = new List<ProductInformationForSales>();

        public delegate void LodaEventHandaler(VMReceiveProduct vmReceiveProduct);
        public event LodaEventHandaler OnGetreceiveProduct;

        VMReceiveProduct _vmReceievProduct = new VMReceiveProduct();
        private int _receiveID = 0;
        int _caller = 0;
        public frmBranchReceive()
        {
            DataAccessProxy = new FacadeManager();
            InitializeComponent();
        }
        public frmBranchReceive(int caller, VMReceiveProduct vmReceievProduct, bool isEdit)
        {
            _caller = caller;
            _vmReceievProduct = vmReceievProduct;
            IsUpdating = isEdit;
            DataAccessProxy = new FacadeManager();
            InitializeComponent();
        }


        public frmBranchReceive(int receiveID, bool isEdit)
        {
            _receiveID = receiveID;
            IsUpdating = isEdit;
            DataAccessProxy = new FacadeManager();
            InitializeComponent();
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

            if (grvCreditProductDetails.Rows.Count == 0)
            {
                MessageBoxHelper.ShowInformation("Please add information in grid.");
                return false;
            }

            return true;
        }




        /// <summary>
        /// Method to add product information in grid.
        /// </summary>
        private void AddProductInGrid()
        {
            _vmReceievProduct = (_vmReceievProduct == null) ? new VMReceiveProduct() : _vmReceievProduct;
            int quantity = 0;
            ReceiveProductDetail receiveProductDetail = new ReceiveProductDetail();
            int.TryParse(UltraQuantity.Text, out quantity);
            receiveProductDetail.ProductCode = cmbProductInformation.Value.ToString();
            receiveProductDetail.ProductName = cmbProductInformation.Text;
            receiveProductDetail.Quantity = quantity;
            _vmReceievProduct.lstRecevieProductDetail.Add(receiveProductDetail);

            cmbProductInformation.Text = string.Empty;
            cmbProductInformation.Focus();
            UltraQuantity.Value = 0;

            LoadDataInGrid();

            //decimal price = default(decimal);
            //decimal vat = 0;
            //int rowIndex = 0;

            //decimal previousSquareFeet = 0;
            //decimal squareFeet = 0;

            //if ((cmbProductInformation.Value != null))
            //{
            //    Product product = DataAccessProxy.GetProductInformationByID(cmbProductInformation.Value.ToString());
            //    price = DataAccessProxy.GetSalesPriceByProductID(cmbProductInformation.Value.ToString());
            //    ProductSize productSize = DataAccessProxy.GetProductSizeByID(product.ProductSizeID);
            //    quantity = Convert.ToInt32(UltraQuantity.Value);


            //    if (IsExistingProduct(product, out rowIndex))
            //    {
            //        decimal previousQuantity = 0;
            //        decimal.TryParse(grvCreditProductDetails.Rows[rowIndex].Cells["Quantity"].Value.ToString(), out previousQuantity);

            //        decimal.TryParse(grvCreditProductDetails.Rows[rowIndex].Cells["SquareFeet"].Value.ToString(), out previousSquareFeet);
            //        vat = (price * (quantity + previousQuantity)) / 100 * product.VAT;
            //        grvCreditProductDetails.Rows[rowIndex].Cells["Quantity"].Value = previousQuantity + quantity;

            //    }
            //    else
            //    {
            //        DataGridViewRow row = grvCreditProductDetails.Rows[0].Clone() as DataGridViewRow;
            //        int index = grvCreditProductDetails.Rows.Add(row);
            //        vat = (price * quantity) / 100 * product.VAT;
            //        grvCreditProductDetails.Rows[index].Cells["ProductCode"].Value = product.ProductID;
            //        grvCreditProductDetails.Rows[index].Cells["ProductName"].Value = product.ProductName;
            //        grvCreditProductDetails.Rows[index].Cells["Quantity"].Value = quantity.ToString();

            //    }

            //    grvCreditProductDetails.Columns[0].Visible = false;
            //    cmbProductInformation.Text = string.Empty;

            //    cmbProductInformation.Focus();
            //    UltraQuantity.Value = 0;
            //}

        }

        private DataTable BuildMaterialTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ProductID");
            table.Columns.Add("Product Name");
            table.Columns.Add("Quantity");
            table.Columns.Add("Delete");
            return table;

        }

        private void LoadDataInGrid()
        {
            DataTable dt = BuildMaterialTable();

            foreach (ReceiveProductDetail receveiDetail in _vmReceievProduct.lstRecevieProductDetail)
            {
                DataRow row = dt.NewRow();
                row["ProductID"] = receveiDetail.ProductCode;
                row["Product Name"] = receveiDetail.ProductName;
                row["Quantity"] = receveiDetail.Quantity;
                row["Delete"] = "Delete";
                dt.Rows.Add(row);
            }
            grvCreditProductDetails.DataSource = dt;

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
            foreach (UltraGridRow row in grvCreditProductDetails.Rows)
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
        private List<ReceiveProductDetail> GetAllReceiveProductDetail()
        {
            List<ReceiveProductDetail> lstTransferDetail = new List<ReceiveProductDetail>();
            foreach (UltraGridRow row in grvCreditProductDetails.Rows)
            {
                if (row.Cells["ProductID"].Value != null)
                {
                    ReceiveProductDetail receiveProductDetail = new ReceiveProductDetail();
                    receiveProductDetail.ProductCode = row.Cells["ProductID"].Value.ToString();
                    receiveProductDetail.ProductName = row.Cells["Product Name"].Value.ToString();
                    receiveProductDetail.Quantity = Convert.ToDecimal(row.Cells["Quantity"].Value);
                    lstTransferDetail.Add(receiveProductDetail);
                }
            }

            return lstTransferDetail;
        }

        /// <summary>
        /// Method to insert transfer information.
        /// </summary>
        /// <returns></returns>
        private int InsertReceiveProductInformation()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            List<ReceiveProductDetail> lstTransferDetail = new List<ReceiveProductDetail>();
            ReceiveProduct receiveProduct = new ReceiveProduct();

            _vmReceievProduct.ReceiveProduct.ReceiveDate = dtpTransferDate.Value;
            _vmReceievProduct.ReceiveProduct.SalesCenterID = Convert.ToInt32(cmbSalesCenter.Value);
            _vmReceievProduct.ReceiveProduct.ReceiveBy = MTBFConstants.AppConstants.LoggedinUserID;
            _vmReceievProduct.ReceiveProduct.BranchID = MTBFConstants.AppConstants.BranchID;
            _vmReceievProduct.ReceiveProduct.OrganizationID = MTBFConstants.AppConstants.OrganizationID;


            lstTransferDetail = GetAllReceiveProductDetail();
            result = new ReceiveProductManager().SaveReceiveProductInformation(_vmReceievProduct);

            return result;
        }

        /// <summary>
        /// Method to update transfer information
        /// </summary>
        /// <returns></returns>
        private int UpdateTransferInformation()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;


            List<ReceiveProductDetail> lstTransferDetail = new List<ReceiveProductDetail>();
            ReceiveProduct receiveProduct = DataAccessProxy.GetReceiveProductByID(_receiveID);
            receiveProduct.ReceiveDate = dtpTransferDate.Value;
            receiveProduct.SalesCenterID = Convert.ToInt32(cmbSalesCenter.Value);
            receiveProduct.ReceiveBy = MTBFConstants.AppConstants.LoggedinUserID;
            receiveProduct.BranchID = MTBFConstants.AppConstants.BranchID;
            receiveProduct.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            lstTransferDetail = GetAllReceiveProductDetail();
            result = DataAccessProxy.UpdateReceiveProductInformation(receiveProduct, lstTransferDetail);


            return result;
        }

        private void frmBranchReceive_Load(object sender, EventArgs e)
        {
            LoadSalesCenterCombo();
            LoadProductInformationCombo();
            if (IsUpdating)
            {
                LoadExistingReceiveProductInformation();

            }
        }



        private void LoadProductInformationCombo()
        {

            DataTable productTable = new DataTable();
            productTable.Columns.Add("ID");
            productTable.Columns.Add("Name");
            productTable.Columns.Add("Type");
            List<ProductType> lstProductType = new List<ProductType>();
            List<ProductModel> lstProductModel = new List<ProductModel>();

            List<Product> lstProductInfo = DataAccessProxy.GetAllProduct(MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<Product>().ToList();

            int[] productTypeID = lstProductInfo.Select(d => d.ProductTypeID).Distinct().ToArray();
            string filter = String.Format("{0} IN ({1})", "ProductTypeID", String.Join(",", productTypeID));
            lstProductType = DataAccessProxy.GetFilteedProductType(filter);

            int[] productModelID = lstProductInfo.Select(d => d.ProductModelID).Distinct().ToArray();
            filter = String.Format("{0} IN ({1})", "ProductModelID", String.Join(",", productModelID));
            lstProductModel = DataAccessProxy.GetFilteedProductModel(filter);


            foreach (Product product in lstProductInfo)
            {
                ProductType productType = lstProductType.Where(p => p.ProductTypeID == product.ProductTypeID).Cast<ProductType>().ToList().FirstOrDefault();
                DataRow row = productTable.NewRow();
                row["ID"] = product.ProductID;
                row["Name"] = product.ProductName;
                row["Type"] = (productType != null) ? productType.TypeName : string.Empty;
                productTable.Rows.Add(row);

            }
            cmbProductInformation.DataSource = productTable;
            cmbProductInformation.DisplayMember = "Name";
            cmbProductInformation.ValueMember = "ID";

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

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (ValidFormData())
            {
                AddProductInGrid();
            }
        }

        private void LoadExistingReceiveProductInformation()
        {
            cmbSalesCenter.Value = _vmReceievProduct.ReceiveProduct.SalesCenterID;
            dtpTransferDate.Value = _vmReceievProduct.ReceiveProduct.ReceiveDate;
            // cmbSalesCenter.Enabled = false;

            LoadDataInGrid();
        }

        //private void LoadExistingReceiveProductDetailInformation(int _tranferID)
        //{
        //    foreach (ReceiveProductDetail receiveDetail in DataAccessProxy.GetAllReceiveProductDetailByReceiveProductID(_receiveID))
        //    {
        //        DataGridViewRow row = grvCreditProductDetails.Rows[0].Clone() as DataGridViewRow;
        //        int index = grvCreditProductDetails.Rows.Add(row);

        //        grvCreditProductDetails.Rows[index].Cells["ProductCode"].Value = receiveDetail.ProductCode;
        //        grvCreditProductDetails.Rows[index].Cells["ProductName"].Value = receiveDetail.ProductName;
        //        grvCreditProductDetails.Rows[index].Cells["Quantity"].Value = receiveDetail.Quantity;
        //        grvCreditProductDetails.Rows[index].Cells["SquareFeet"].Value = receiveDetail.SquareFeet;
        //    }
        //}

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
            decimal.TryParse(UltraQuantity.Value.ToString(), out quantity);
            decimal.TryParse(txtProductSize.Text, out size);


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
                Product product = DataAccessProxy.GetProductByName(cmbProductInformation.Text).Where(p => p.BranchID == MTBFConstants.AppConstants.BranchID && p.OrganizationID == MTBFConstants.AppConstants.OrganizationID).Cast<Product>().FirstOrDefault();
                ProductSize productSize = DataAccessProxy.GetProductSizeByID(product.ProductSizeID);
                txtProductSize.Text = (productSize != null) ? productSize.Name : string.Empty;
            }
        }

        private void btCreditClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void btCreditSave_Click(object sender, EventArgs e)
        {
            if (ValidInsertionData())
            {
                if (_caller == 0)
                {
                    if (InsertReceiveProductInformation() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Product receive information saved successfully.");
                        IsUpdating = false;
                        grvCreditProductDetails.DataSource = BuildMaterialTable();
                        _receiveID = 0;
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save product receive information.");
                    }
                }
                else
                {
                    _vmReceievProduct.ReceiveProduct.ReceiveDate = dtpTransferDate.Value;
                    _vmReceievProduct.ReceiveProduct.SalesCenterID = Convert.ToInt32(cmbSalesCenter.Value);
                    _vmReceievProduct.ReceiveProduct.ReceiveBy = MTBFConstants.AppConstants.LoggedinUserID;
                    _vmReceievProduct.ReceiveProduct.BranchID = MTBFConstants.AppConstants.BranchID;
                    _vmReceievProduct.ReceiveProduct.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                    _receiveID = 0;
                    if (OnGetreceiveProduct != null)
                    {
                        OnGetreceiveProduct(_vmReceievProduct);
                    }

                    this.Close();
                }



            }

        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            LoadSalesCenterCombo();
            LoadProductInformationCombo();
        }

        private void btReset_Click(object sender, EventArgs e)
        {
            cmbSalesCenter.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;


        }

        private void grvCreditProductDetails_ClickCell(object sender, ClickCellEventArgs e)
        {
            if (e.Cell.Column.Header.Caption == "Delete")
            {
                string productID = grvCreditProductDetails.Rows[e.Cell.Row.Index].Cells["ProductID"].Value.ToString();
                int rowIndex = e.Cell.Row.Index;
                if (grvCreditProductDetails.Rows[e.Cell.Row.Index].Delete())
                {
                    ReceiveProductDetail sDetail = _vmReceievProduct.lstRecevieProductDetail.Where(p => p.ProductCode == productID).FirstOrDefault();
                    if (sDetail != null)
                    {
                        _vmReceievProduct.lstRecevieProductDetail.Remove(sDetail);
                    }
                    LoadDataInGrid();
                }
            }
        }

    }
}
