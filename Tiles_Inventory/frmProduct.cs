using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IFMS.Facade;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Constants;
using NybSys.MTBF.Utility.Helper;
using NybSys.MTBF.Utility.Resources;
using IFMS.BLL;

namespace Tiles_Inventory
{
    public partial class frmProduct : BaseForm
    {
        private int _serialNo = 0;
        private bool isUpdate = false;
        private string _ProductID = string.Empty;
        private int _caller = 0;

        public event Load_Product_TypeEventHandler Load_Product_Type;
        public delegate void Load_Product_TypeEventHandler();

        public event Load_Product_InformationEventHandler Load_Product_Information;
        public delegate void Load_Product_InformationEventHandler();

        public frmProduct(int slNo, bool IsEdit, string pId, int caller = 0)
        {
            InitializeComponent();
            DataAccessProxy = new FacadeManager();

            if (IsEdit)
            {
                _serialNo = slNo;
                isUpdate = IsEdit;
                _ProductID = pId;
                _caller = caller;
            }
        }

        /// <summary>
        /// Method to load product type information.
        /// </summary>
        private void LoadProductTypeInformation()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("Name");
            List<ProductType> lstProductType = new List<ProductType>();
            // lstProductType = DataAccessProxy.GetAllProductType().Cast<ProductType>().Where(t => t.BranchID == MTBFConstants.AppConstants.BranchID && t.OrganizationID == MTBFConstants.AppConstants.OrganizationID).ToList();
            lstProductType = DataAccessProxy.GetAllProductType().Cast<ProductType>().OrderBy(p => p.TypeName).ToList();


            DataRow emptyRow = table.NewRow();
            emptyRow[0] = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            emptyRow[1] = MTBFConstants.DataField.COMBO_DEFAULT_NAME;
            table.Rows.Add(emptyRow);

            foreach (ProductType productType in lstProductType)
            {
                DataRow row = table.NewRow();
                row[0] = productType.ProductTypeID;
                row[1] = productType.TypeName;
                table.Rows.Add(row);
            }


            cmbProductType.DataSource = table;
            cmbProductType.DisplayMember = "Name";
            cmbProductType.ValueMember = "ID";
            cmbProductType.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
        }

        /// <summary>
        /// Method to load product size combo.
        /// </summary>
        private void LoadProductSizeCombo()
        {
            // List<ProductSize> lstProductSize = DataAccessProxy.GetAllProductSize().Cast<ProductSize>().Where(t => t.BranchID == MTBFConstants.AppConstants.BranchID && t.OrganizationID == MTBFConstants.AppConstants.OrganizationID).ToList();

            List<ProductSize> lstProductSize = DataAccessProxy.GetAllProductSize().Cast<ProductSize>().OrderBy(p => p.Name).ToList();

            ProductSize productSize = new ProductSize();
            productSize.ProductSizeID = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            productSize.Name = MTBFConstants.DataField.COMBO_DEFAULT_NAME;
            lstProductSize.Insert(0, productSize);
            UIHelper.SetComboBoxData(cmbProductSize, lstProductSize, MTBFConstants.DataField.NAME, MTBFConstants.DataField.PRODUCT_SIZE_ID);
        }

        /// <summary>
        /// Method to load product model combo.
        /// </summary>
        private void LoadProductModelCombo()
        {
            //  List<ProductModel> lstProductModel = DataAccessProxy.GetAllProductModel().Cast<ProductModel>().Where(m => m.BranchID == MTBFConstants.AppConstants.BranchID && m.OrganizationID == MTBFConstants.AppConstants.OrganizationID).ToList();
            List<ProductModel> lstProductModel = DataAccessProxy.GetAllProductModel().Cast<ProductModel>().OrderBy(p => p.Name).ToList();

            ProductModel productModel = new ProductModel();
            productModel.ProductModelID = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            productModel.Name = MTBFConstants.DataField.COMBO_DEFAULT_NAME;
            lstProductModel.Insert(0, productModel);
            UIHelper.SetComboBoxData(cmbProductModel, lstProductModel, MTBFConstants.DataField.NAME, MTBFConstants.DataField.PRODUCT_MODEL_ID);
        }

        /// <summary>
        /// Method to load product color combo.
        /// </summary>
        private void LoadProductColorCombo()
        {
            List<ProductColor> lstProductColor = DataAccessProxy.GetAllProductColor().Cast<ProductColor>().ToList();

            ProductColor productColor = new ProductColor();
            productColor.ProductColorID = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            productColor.Name = MTBFConstants.DataField.COMBO_DEFAULT_NAME;
            lstProductColor.Insert(0, productColor);
            UIHelper.SetComboBoxData(cmbProductColor, lstProductColor, MTBFConstants.DataField.NAME, MTBFConstants.DataField.PRODUCT_COLOR_ID);
        }

        /// <summary>
        /// Method to laod supplier informatin in combo box.
        /// </summary>
        /// <remarks></remarks>
        private void LoadSupplierCombo()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("Name");

            List<Supplier> lstSupplier = new List<Supplier>();
            lstSupplier = new SupplierManager().GetAllSupplierByBranchID(MTBFConstants.AppConstants.BranchID).Cast<Supplier>().ToList();

            DataRow emptyRow = table.NewRow();
            emptyRow[0] = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            emptyRow[1] = MTBFConstants.DataField.COMBO_DEFAULT_NAME;
            table.Rows.Add(emptyRow);

            foreach (Supplier supplier in lstSupplier)
            {
                DataRow row = table.NewRow();
                row[0] = supplier.SupplierID;
                row[1] = supplier.SupplierName;
                table.Rows.Add(row);
            }

            cmbSupplierName.DataSource = table;
            cmbSupplierName.DisplayMember = "Name";
            cmbSupplierName.ValueMember = "ID";
            cmbSupplierName.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;

        }

        /// <summary>
        /// Method to reset all controlls.
        /// </summary>
        private void ResetAllControls()
        {
            LoadProductTypeInformation();
            LoadProductSizeCombo();
            LoadProductModelCombo();
            LoadProductColorCombo();
            LoadSupplierCombo();
            cmbSubCategory.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;

            txtProductName.Clear();
            txtPackSize.Clear();
            cbDiscontinued.Checked = false;
            txtReorderQuantity.Clear();
            txtVATPercentage.Clear();
            txtShelfNumber.Clear();
            txtBarCode.Clear();
            //  txtCartonSize.Clear();

        }

        /// <summary>
        /// Method to valid form data.
        /// </summary>
        /// <returns></returns>
        private bool ValidFormData()
        {

            if (txtProductName.Text.Trim() == string.Empty)
            {
                MessageBoxHelper.ShowInformation(SystemMessages.YOU_NEED_TO_ENTER_PRODUCT_NAME);
                txtProductName.Focus();
                return false;
            }

            if (Convert.ToInt32(cmbProductType.Value) == MTBFConstants.DataField.COMBO_DEFAULT_ID)
            {
                MessageBoxHelper.ShowInformation(SystemMessages.YOU_NEED_TO_SELECT_PRODUCT_TYPE);
                cmbProductType.Focus();
                return false;
            }




            //if (Convert.ToInt32(cmbCompanyName.Value) == MTBFConstants.DataField.COMBO_DEFAULT_ID)
            //{
            //    MessageBoxHelper.ShowInformation("You need to select company name");
            //    cmbCompanyName.Focus();
            //    return false;
            //}

            if (Convert.ToInt32(cmbSupplierName.Value) == MTBFConstants.DataField.COMBO_DEFAULT_ID)
            {
                MessageBoxHelper.ShowInformation("You need to select supplier name");
                cmbSupplierName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(cmbUnit.Text))
            {
                MessageBoxHelper.ShowInformation("You need to select unit");
                cmbUnit.Focus();
                return false;
            }

            //if (!string.IsNullOrEmpty(txtBarCode.Text))
            //{
            //    if (txtBarCode.Text.Trim().Length < 6)
            //    {
            //        MessageBoxHelper.ShowWarning("Barcode should be 6 digit long.");
            //        txtBarCode.Focus();
            //        return false;
            //    }
            //}

            if (!string.IsNullOrEmpty(txtBarCode.Text))
            {
                if (DuplicateBarCode(txtBarCode.Text))
                {
                    MessageBoxHelper.ShowWarning("You already add this barcode in another product.");
                    txtBarCode.Focus();
                    return false;
                }


            }



            if (DuplicateProduct())
            {
                MessageBoxHelper.ShowInformation(SystemMessages.DUPLICATE_PRODUCT);
                return false;
            }

            return true;
        }

        private bool DuplicateBarCode(string barcode)
        {
            Product product = DataAccessProxy.GetProductByBarCode(barcode);
            if (product != null && _ProductID != product.ProductID)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// Method to check duplicate product.
        /// </summary>
        /// <returns></returns>
        private bool DuplicateProduct()
        {
            string productName = txtProductName.Text;
            Product product = DataAccessProxy.GetProductByName(productName.Trim().Replace("'", "''")).Where(p => p.BranchID == MTBFConstants.AppConstants.BranchID && p.OrganizationID == MTBFConstants.AppConstants.OrganizationID).Cast<Product>().FirstOrDefault();
            if (product != null)
            {
                if (_ProductID != product.ProductID)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Method to insert product information.
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="productName"></param>
        /// <returns></returns>
        private int InsertProductInformation(string productID, string productName)
        {
            decimal cartonSize = 0;
            int productTypeID = 0;
            int productSize = 0;
            int productModel = 0;
            int productColorID = 0;
            int subCategoryID = 0;
            int reorderQty = 0;
            decimal vat = 0;
            int.TryParse(cmbProductType.Value.ToString(), out productTypeID);
            int.TryParse(cmbProductSize.Value.ToString(), out productSize);
            int.TryParse(cmbProductModel.Value.ToString(), out productModel);
            int.TryParse(cmbProductColor.Value.ToString(), out productColorID);
            int.TryParse(cmbSubCategory.Value.ToString(), out subCategoryID);

            int.TryParse(txtReorderQuantity.Text, out reorderQty);
            decimal.TryParse(txtVATPercentage.Text, out vat);
            //   decimal.TryParse(txtCartonSize.Text, out cartonSize);
            Product product = new Product();
            product.SubCategoryID = subCategoryID;
            product.Unit = cmbUnit.Text;
            product.ProductID = productID;
            product.ProductName = productName;
            product.ProductTypeID = Convert.ToInt32(cmbProductType.Value);
            product.ProductSizeID = productSize;
            product.ProductModelID = productModel;
            product.ProductColorID = productColorID;
            // product.GenericName = txtGenericName.Text;
            product.PackSize = txtPackSize.Text;
            product.ShelfNumber = txtShelfNumber.Text;
            product.CompanyID = 1;// Convert.ToInt32(cmbCompanyName.Value);
            product.SupplierID = Convert.ToInt32(cmbSupplierName.Value);
            product.Discountinued = cbDiscontinued.Checked;
            product.ReorderQuantity = reorderQty;
            product.VAT = vat;
            product.BranchID = MTBFConstants.AppConstants.BranchID;
            product.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            product.BarCode = txtBarCode.Text.Trim();
            product.ProductModelName = (cmbProductModel.Text == "-Select-") ? string.Empty : cmbProductModel.Text;
            product.TypeName = (cmbProductType.Text == "-Select-") ? string.Empty : cmbProductType.Text;
            product.ColorName = (cmbProductColor.Text == "-Select-") ? string.Empty : cmbProductColor.Text;
            product.CompanyName = cmbCompanyName.Text;
            product.SupplierName = cmbSupplierName.Text;
            product.SizeName = (cmbProductSize.Text == "-Select-") ? string.Empty : cmbProductSize.Text;
            product.CartonSize = cartonSize;


            return DataAccessProxy.InsertProduct(product);

        }

        /// <summary>
        /// Method to update product information.
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        private int UpdateProductInformation(string productName)
        {
            decimal cartonSize = 0;
            int productTypeID = 0;
            int productSize = 0;
            int productModel = 0;
            int productColorID = 0;
            int subCategoryID = 0;

            int reorderQty = 0;
            decimal vat = 0;
            int.TryParse(cmbProductType.Value.ToString(), out productTypeID);
            int.TryParse(cmbProductSize.Value.ToString(), out productSize);
            int.TryParse(cmbProductModel.Value.ToString(), out productModel);
            int.TryParse(cmbProductColor.Value.ToString(), out productColorID);
            int.TryParse(cmbSubCategory.Value.ToString(), out subCategoryID);

            int.TryParse(txtReorderQuantity.Text, out reorderQty);
            decimal.TryParse(txtVATPercentage.Text, out vat);
            //  decimal.TryParse(txtCartonSize.Text, out cartonSize);
            Product product = DataAccessProxy.GetProductInformationByID(_ProductID);
            product.SubCategoryID = subCategoryID;
            product.Unit = cmbUnit.Text;
            product.ProductName = productName;
            product.ProductTypeID = productTypeID;
            // product.GenericName = txtGenericName.Text;
            product.ProductSizeID = productSize;
            product.ProductModelID = productModel;
            product.ProductColorID = productColorID;
            product.PackSize = txtPackSize.Text;
            product.ShelfNumber = txtShelfNumber.Text;
            product.CompanyID = 1;// Convert.ToInt32(cmbCompanyName.Value);
            product.SupplierID = Convert.ToInt32(cmbSupplierName.Value);
            product.Discountinued = cbDiscontinued.Checked;
            product.ReorderQuantity = reorderQty;
            product.VAT = vat;
            product.BarCode = txtBarCode.Text.Trim();
            product.ProductModelName = (cmbProductModel.Text == "-Select-") ? string.Empty : cmbProductModel.Text;
            product.TypeName = (cmbProductType.Text == "-Select-") ? string.Empty : cmbProductType.Text;
            product.ColorName = (cmbProductColor.Text == "-Select-") ? string.Empty : cmbProductColor.Text;
            product.CompanyName = cmbCompanyName.Text;
            product.SupplierName = cmbSupplierName.Text;
            product.SizeName = (cmbProductSize.Text == "-Select-") ? string.Empty : cmbProductSize.Text;
            product.CartonSize = cartonSize;
            return DataAccessProxy.UpdateProduct(product);

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
            emptyRow[1] = MTBFConstants.DataField.COMBO_DEFAULT_NAME;
            table.Rows.Add(emptyRow);

            List<Company> lstCompany = new List<Company>();
            //  lstCompany = DataAccessProxy.GetAllCompany().Cast<Company>().Where(s => s.BranchID == MTBFConstants.AppConstants.BranchID && s.OrganizationID == MTBFConstants.AppConstants.OrganizationID).ToList();
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
        }

        /// <summary>
        /// Method to check duplicate product.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool Duplicate(string productId)
        {

            bool isDuplicate = false;

            Product product = DataAccessProxy.GetProductInformationByID(productId);
            if (product != null)
            {
                isDuplicate = true;
                MessageBoxHelper.ShowInformation("Duplicate record.");
            }

            return isDuplicate;
        }

        /// <summary>
        /// Method to load product information for update.
        /// </summary>
        /// <remarks></remarks>
        private void LoadExistingProductInformation()
        {
            try
            {
                Product product = DataAccessProxy.GetProductInformationByID(_ProductID);

                if (product != null)
                {
                    txtProductName.Text = product.ProductName;//.Substring(0, (product.ProductName.Length - (productName.Length)));
                    cmbProductType.Value = product.ProductTypeID.ToString();
                    cmbProductModel.Value = product.ProductModelID.ToString();
                    cmbProductSize.Value = product.ProductSizeID.ToString();
                    cmbProductColor.Value = product.ProductColorID.ToString();
                    txtPackSize.Text = product.PackSize;
                    cbDiscontinued.Checked = product.Discountinued;
                    cmbUnit.Text = product.Unit;
                    txtReorderQuantity.Text = product.ReorderQuantity.ToString();
                    cmbCompanyName.Value = product.CompanyID;
                    cmbSupplierName.Value = product.SupplierID;
                    txtShelfNumber.Text = product.ShelfNumber;
                    txtVATPercentage.Text = product.VAT.ToString();
                    txtBarCode.Text = product.BarCode;
                    // txtCartonSize.Text = product.CartonSize.ToString();
                }

            }
            catch
            {

            }


        }

        /// <summary>
        /// Method to generate product name.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        private string GenerateProductName()
        {
            string productName = txtProductName.Text.Trim();

            int productTypeID = 0;
            int productSizeID = 0;
            int productModelID = 0;
            int productColorID = 0;

            int.TryParse(cmbProductType.Value.ToString(), out productTypeID);
            int.TryParse(cmbProductSize.Value.ToString(), out productSizeID);
            int.TryParse(cmbProductModel.Value.ToString(), out productModelID);
            int.TryParse(cmbProductColor.Value.ToString(), out productColorID);


            ProductType productType = DataAccessProxy.GetProductTypeByID(productTypeID);
            ProductSize productSize = DataAccessProxy.GetProductSizeByID(productSizeID);
            ProductModel productModel = DataAccessProxy.GetProductModelByID(productModelID);
            ProductColor productColor = DataAccessProxy.GetProductColorByID(productColorID);

            if (productType != null)
            {
                productName = productName + " " + productType.TypeName;
            }

            if (productSize != null)
            {
                productName = productName + " " + productSize.Name;
            }

            if (productModel != null)
            {
                productName = productName + " " + productModel.Name;
            }

            if (productColor != null)
            {
                productName = productName + " " + productColor.Name;
            }

            //  productName = productName + " " + txtPackSize.Text;

            return productName;
        }

        private void Product_Load(System.Object sender, System.EventArgs e)
        {
            LoadProductTypeInformation();
            LoadProductSizeCombo();
            LoadProductModelCombo();
            LoadProductColorCombo();
            LoadSupplierCombo();
            if (isUpdate)
            {
                LoadExistingProductInformation();

                //cmbSupplierName.Enabled = false;
                //   cmbProductType.Enabled = false;
                //  cmbCompanyName.Enabled = false;
                // txtProductName.Enabled = False
            }
            else
            {

                cmbSupplierName.Enabled = true;
                cmbProductType.Enabled = true;
                txtProductName.Enabled = true;
                cmbCompanyName.Enabled = true;
            }

        }

        private void btnSave_Click(System.Object sender, System.EventArgs e)
        {
            string productID = null;
            string productName = null;

            int serialno = 0;

            serialno = DataAccessProxy.GetMaxProductSerialNumber();

            if (ValidFormData())
            {
                productName = txtProductName.Text;// GenerateProductName();
                productID = serialno.ToString() + "_" + productName;

                try
                {
                    if (isUpdate)
                    {
                        productID = _serialNo.ToString() + "_" + productName;
                        if (_ProductID != productID)
                        {

                            if (UpdateProductInformation(productName) > 0)
                            {
                                MessageBoxHelper.ShowInformation("Successfully saved");
                                cmbSupplierName.Enabled = true;
                                cmbProductType.Enabled = true;
                                txtProductName.Enabled = true;
                                cmbCompanyName.Enabled = true;
                                isUpdate = false;
                            }
                        }
                        else
                        {
                            if (UpdateProductInformation(productName) > 0)
                            {
                                MessageBoxHelper.ShowInformation("Successfully saved");
                                isUpdate = false;
                            }
                        }
                    }
                    else
                    {
                        if (InsertProductInformation(productID, productName) > 0)
                        {
                            MessageBoxHelper.ShowInformation("Successfully saved");
                            txtProductName.Focus();
                        }
                    }

                    this.ResetAllControls();
                    if (Load_Product_Type != null)
                    {
                        Load_Product_Type();
                    }

                    if (_caller == 2)
                    {
                        if (Load_Product_Information != null)
                        {
                            Load_Product_Information();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxHelper.ShowInformation("Operation failed.");
                }

            }
        }

        private void btnClose_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void cmbSupplierName_KeyPress(System.Object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            cmbSupplierName.ToggleDropdown();
        }

        private void cmbSupplierName_KeyUp(System.Object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 13)
            {
                cmbSupplierName.ToggleDropdown();
            }
        }

        private void btnAdd_Click(System.Object sender, System.EventArgs e)
        {
            frmProductType frm = new frmProductType();
            frm.LoadProductType += new frmProductType.LoadProductTypeEventHandler(frm_LoadProductType);
            frm.ShowDialog();
        }

        void frm_LoadProductType(string typeName)
        {
            LoadProductTypeInformation();
        }



        private void btnAddSupplier_Click(System.Object sender, System.EventArgs e)
        {
            SuppliersInformation frm = new SuppliersInformation(0, false);
            frm.OnLoadSupplierInformation += OnSupplierInformationLoad;
            frm.ShowDialog();
        }

        private void OnSupplierInformationLoad()
        {
            LoadSupplierCombo();
        }



        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ResetAllControls();
        }

        private void btnAddProductSize_Click(object sender, EventArgs e)
        {
            frmProductSize frm = new frmProductSize();
            frm.LoadProductSize += new frmProductSize.LoadProductTypeEventHandler(frm_LoadProductSize);
            frm.ShowDialog();
        }

        void frm_LoadProductSize()
        {
            LoadProductSizeCombo();
        }

        private void btnAddModel_Click(object sender, EventArgs e)
        {
            frmProductModel frm = new frmProductModel();
            frm.LoadProductModel += new frmProductModel.LoadProductTypeEventHandler(frm_LoadProductModel);
            frm.ShowDialog();
        }

        void frm_LoadProductModel()
        {
            LoadProductModelCombo();
        }

        private void btnAddProductColor_Click(object sender, EventArgs e)
        {
            frmProductColor frm = new frmProductColor();
            frm.LoadProductColor += new frmProductColor.LoadProductTypeEventHandler(frm_LoadProductColor);
            frm.ShowDialog();
        }

        void frm_LoadProductColor()
        {
            LoadProductColorCombo();
        }

        private void txtVATPercentage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ("1234567890".IndexOf(e.KeyChar) > -1 | e.KeyChar == Convert.ToChar(8))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            List<Product> lstProductInfo = new List<Product>();

            DataTable productTable = new DataTable();
            productTable.Columns.Add("ID");
            productTable.Columns.Add("Name");

            lstProductInfo = DataAccessProxy.GetAllProduct().Cast<Product>().Where(p => p.BranchID == 1 && p.OrganizationID == 1).ToList();
            foreach (Product product in lstProductInfo)
            {
                product.BranchID = 3;
                product.OrganizationID = 1;
                product.SupplierID = 2;
                product.CompanyID = 3;

                DataAccessProxy.InsertProduct(product);
            }


        }

        private void btSubCategory_Click(object sender, EventArgs e)
        {
            frmSubCategory frm = new frmSubCategory();
            frm.LoadSubCategory += new frmSubCategory.LoadProductTypeEventHandler(frm_LoadSubCategory);
            frm.ShowDialog();
        }

        void frm_LoadSubCategory()
        {
            LoadSuCategoryByCategoryID();
        }

        private void LoadSuCategoryByCategoryID()
        {
            int categoryID = 0;
            int.TryParse(cmbProductType.Value.ToString(), out categoryID);
            string filter = string.Format("{0}={1}", "CategoryID", categoryID);
            List<SubCategory> lstSubCategory = new CategoryManager().GetFilteredSubCategory(filter);

            SubCategory subCategory = new SubCategory();
            subCategory.SubCategoryID = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            subCategory.SubCategoryName = MTBFConstants.DataField.COMBO_DEFAULT_NAME;
            lstSubCategory.Insert(0, subCategory);
            UIHelper.SetComboBoxData(cmbSubCategory, lstSubCategory, "SubCategoryName", "SubCategoryID");

        }

        private void cmbProductType_ValueChanged(object sender, EventArgs e)
        {
            if (cmbProductType.Value != null)
            {
                LoadSuCategoryByCategoryID();
            }
        }



    }
}
