//Modified By: Md.Foyjul Bary.
// Last Modification Date :27-08-2014
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NybSys.MTBF.Utility.Helper;
using NybSys.MTBF.Utility.Resources;
using NybSys.MTBF.Utility.Constants;
using NybSys.MTBF.Utility.Enums;
using IMFS.Common.DTO;
using IFMS.BLL;


namespace Tiles_Inventory
{
    public partial class frmProductType : BaseForm
    {
        #region "Global declaration"

        private int _ProductTypeID = 0;

        public frmProductType()
        {
            InitializeComponent();

        }

        public event LoadProductTypeEventHandler LoadProductType;
        public delegate void LoadProductTypeEventHandler(string typeName);

        #endregion

        #region "Event Handler"

        private void btnSave_Click(System.Object sender, System.EventArgs e)
        {
            if (ValidFormData())
            {
                if (base.IsUpdating)
                {
                    if (UpdateProductType() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation(SystemMessages.PRODUCT_TYPE_SAVED);
                        LoadProductTypeInformation();

                        if (LoadProductType != null)
                            LoadProductType(txtTypeName.Text);
                        ResetAllControls();
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save type information");
                    }
                }
                else
                {
                    if (InsertProductType() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation(SystemMessages.PRODUCT_TYPE_SAVED);
                        LoadProductTypeInformation();

                        if (LoadProductType != null)
                            LoadProductType(txtTypeName.Text);
                        ResetAllControls();
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save type information");
                    }
                }
            }
        }

        private void btnClose_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void frmProductType_Load(object sender, System.EventArgs e)
        {
            //  UIHelper.CheckUserAccessibility(cmbCompany, MTBFConstants.AppConstants.LoggedinUser);
            LoadProductTypeInformation();
        }



        private void grvProductType_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            if (grvProductType.Selected.Rows.Count > 0)
            {
                int ProductTypeID = Convert.ToInt32(grvProductType.Selected.Rows[0].Cells["TypeID"].Value);

                ProductType productType = new ProductManager().GetProductTypeByID(ProductTypeID);
                if (productType != null)
                {
                    txtTypeName.Text = productType.TypeName;
                    _ProductTypeID = productType.ProductTypeID;
                    base.IsUpdating = true;
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ResetAllControls();
        }

        #endregion

        #region "Private Methods"

        /// <summary>
        /// Method to reset all controls.
        /// </summary>
        private void ResetAllControls()
        {
            txtTypeName.Clear();
            txtTypeName.Focus();
            IsUpdating = false;
            _ProductTypeID = 0;
        }

        /// <summary>
        /// Method to valid form data.
        /// </summary>
        /// <returns></returns>
        private bool ValidFormData()
        {
            if (string.IsNullOrEmpty(txtTypeName.Text))
            {
                MessageBoxHelper.ShowInformation("You need to enter type name");
                txtTypeName.Focus();
                return false;
            }

            if (IsDuplicate())
            {
                MessageBoxHelper.ShowInformation(SystemMessages.DUPLICATE_PRODUCT_TYPE);
                txtTypeName.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Method to build Product type table.
        /// codes by foyjul bary
        /// </summary>
        /// <returns></returns>
        private DataTable BuildProductTypeTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("TypeID");
            table.Columns.Add("Category Name");

            return table;
        }

        /// <summary>
        /// Method to load Product type information by companyCode.
        /// codes by foyjul bary
        /// </summary>
        /// <param name="inventoryName"></param>
        private void LoadProductTypeInformation()
        {
            DataTable ProductTypeTable = BuildProductTypeTable();
          //  List<ProductType> lstProductType = new ProductManager().GetAllProductType().Cast<ProductType>().Where(p => p.BranchID == MTBFConstants.AppConstants.BranchID && p.OrganizationID == MTBFConstants.AppConstants.OrganizationID).ToList();
            List<ProductType> lstProductType = new ProductManager().GetAllProductType().Cast<ProductType>().ToList();


            if (lstProductType.Count > 0)
            {
                foreach (ProductType productModel in lstProductType)
                {
                    DataRow row = ProductTypeTable.NewRow();
                    row["TypeID"] = productModel.ProductTypeID;
                    row["Category Name"] = productModel.TypeName;
                    ProductTypeTable.Rows.Add(row);
                }
            }
            grvProductType.DataSource = ProductTypeTable;
            grvProductType.DisplayLayout.Bands[0].Columns["TypeID"].Hidden = true;
        }

        /// <summary>
        /// Method to update product type
        /// codes by foyjul bary
        /// </summary>
        /// <returns></returns>
        private int UpdateProductType()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            ProductType productType = new ProductManager().GetProductTypeByID(_ProductTypeID);
            productType.TypeName = txtTypeName.Text;
            //productType.BranchID = MTBFConstants.AppConstants.BranchID;
            //productType.OrganizationID = MTBFConstants.AppConstants.OrganizationID;

            result = new ProductManager().UpdateProductType(productType);

            return result;
        }

        /// <summary>
        /// Method to insert product type
        /// modification by foyjul bary
        /// </summary>
        /// <returns></returns>
        private int InsertProductType()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            ProductType productType = new ProductType();
            productType.TypeName = txtTypeName.Text;
           // productType.BranchID = MTBFConstants.AppConstants.BranchID;
           // productType.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            result = new ProductManager().InsertProductType(productType);

            return result;
        }

        /// <summary>
        /// Method to check duplicate record.
        /// modification by foyjul bary
        /// </summary>
        /// <returns></returns>
        private bool IsDuplicate()
        {
            ProductType productType = new ProductManager().GetAllProductType().Cast<ProductType>().Where(p => p.TypeName.ToUpper().Trim() == txtTypeName.Text.ToUpper().Trim()).ToList().FirstOrDefault();

            if (productType != null && productType.ProductTypeID != _ProductTypeID)
            {
                return true;
            }
            return false;
        }


        #endregion

    }
}
