using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Resources;
using NybSys.MTBF.Utility.Helper;
using IFMS.Facade;
using NybSys.MTBF.Utility.Constants;
using IFMS.BLL;
using NybSys.MTBF.Utility.Enums;

namespace Tiles_Inventory
{
    public partial class frmSubCategory : BaseForm
    {
        public event LoadProductTypeEventHandler LoadSubCategory;
        public delegate void LoadProductTypeEventHandler();
        private SubCategory _subCategory = new SubCategory();
        List<ProductType> lstProductType = new List<ProductType>();

        int modelID = 0;
        public frmSubCategory()
        {
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        private void btnSave_Click(System.Object sender, System.EventArgs e)
        {
            if (IsValidFormData())
            {
                if (SaveSubCategory() == (int)MTBFEnums.ReturnResult.SUCCESS)
                {
                    MessageBoxHelper.ShowInformation("Product model saved successfully");
                    if (LoadSubCategory != null)
                    {
                        LoadSubCategory();
                    }
                    LoadSubcategoryInformation();
                    txtModelName.Clear();
                    txtModelName.Focus();
                    IsUpdating = false;
                    _subCategory = new SubCategory();
                }
                else
                {
                    MessageBoxHelper.ShowInformation("Failed to save product model information.");
                }


            }
        }

        private void LoadProductTypeInformation()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("Name");

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

        private int SaveSubCategory()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            _subCategory.CategoryID = Convert.ToInt32(cmbProductType.Value);
            _subCategory.SubCategoryName = txtModelName.Text.Trim();
            result = new CategoryManager().SaveSupCategory(_subCategory);

            return result;
        }

        private int UpdateModel()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            ProductModel productModel = new ProductManager().GetPrductModelByID(modelID);
            productModel.Name = txtModelName.Text;
            // productModel.BranchID = MTBFConstants.AppConstants.BranchID;
            // productModel.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            result = DataAccessProxy.UpdateProductModel(productModel);

            return result;
        }

        private void btnClose_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Method to check duplicate record.
        /// </summary>
        /// <returns></returns>
        private bool IsValidFormData()
        {

            int categoryID = 0;

            if (cmbProductType.Value == null || Convert.ToInt32(cmbProductType.Value) == MTBFConstants.DataField.COMBO_DEFAULT_ID)
            {
                MessageBoxHelper.ShowInformation("You need to select category");
                return false;
            }


            if (string.IsNullOrEmpty(txtModelName.Text))
            {
                MessageBoxHelper.ShowInformation("You need to enter subcategory name");
                return false;
            }

            string filter = string.Format("{0}={1}", "SubCategoryID", modelID);
            SubCategory productModel = new CategoryManager().GetFilteredSubCategory(filter).FirstOrDefault();

            SubCategory subCategory = new CategoryManager().GetFilteredSubCategory(filter).Where(s => s.SubCategoryName.ToUpper().Trim() == txtModelName.Text.ToUpper().Trim()).ToList().FirstOrDefault();

            if (subCategory != null && subCategory.SubCategoryID != modelID)
            {
                MessageBoxHelper.ShowInformation("Duplicate subcategory information");
                return false;
            }
            return true;
        }

        private DataTable BuildModelTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("CategoryID");
            table.Columns.Add("Category Name");
            table.Columns.Add("Sub Category Name");
            return table;
        }

        private void frmProductModel_Load(object sender, EventArgs e)
        {
            LoadProductTypeInformation();
            LoadSubcategoryInformation();
        }

        private void LoadSubcategoryInformation()
        {
            List<SubCategory> lstSubCategory = new List<SubCategory>();
            // lstProductModel = new ProductManager().GetAllProductModel().Where(m => m.BranchID == MTBFConstants.AppConstants.BranchID && m.OrganizationID == MTBFConstants.AppConstants.OrganizationID).Cast<ProductModel>().ToList();
            lstSubCategory = new SubCategoryManager().GetAllSubCategory().Cast<SubCategory>().OrderBy(p => p.SubCategoryName).ToList();

            DataTable table = BuildModelTable();

            foreach (SubCategory subcategory in lstSubCategory)
            {
                ProductType productType = lstProductType.Where(p => p.ProductTypeID == subcategory.CategoryID).FirstOrDefault();
                DataRow row = table.NewRow();
                row["CategoryID"] = subcategory.CategoryID;
                row["Category Name"] = (productType != null) ? productType.TypeName : string.Empty;
                row["Sub Category Name"] = subcategory.SubCategoryName;
                table.Rows.Add(row);
            }

            grvModelInfo.DataSource = table;
            grvModelInfo.DisplayLayout.Bands[0].Columns["CategoryID"].Hidden = true;
        }

        private void grvModelInfo_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            if (grvModelInfo.Selected.Rows.Count > 0)
            {
                modelID = Convert.ToInt32(grvModelInfo.Selected.Rows[0].Cells["CategoryID"].Value);
                string filter = string.Format("{0}={1}", "SubCategoryID", modelID);
                SubCategory productModel = new CategoryManager().GetFilteredSubCategory(filter).FirstOrDefault();
                if (productModel != null)
                {
                    txtModelName.Text = productModel.SubCategoryName;
                    IsUpdating = true;
                }


            }
        }
    }
}
