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
    public partial class frmProductModel : BaseForm
    {


        public event LoadProductTypeEventHandler LoadProductModel;
        public delegate void LoadProductTypeEventHandler();
        int modelID = 0;
        public frmProductModel()
        {
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        private void btnSave_Click(System.Object sender, System.EventArgs e)
        {
            if (IsValidFormData())
            {
                if (IsUpdating)
                {
                    if (UpdateModel() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Product model saved successfully");
                        if (LoadProductModel != null)
                        {
                            LoadProductModel();
                        }
                        LoadModelInformation();
                        txtModelName.Clear();
                        txtModelName.Focus();
                        IsUpdating = false;
                        modelID = 0;
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save product model information.");
                    }
                }
                else
                {
                    if (InsertModel() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Product model saved successfully");
                        if (LoadProductModel != null)
                        {
                            LoadProductModel();
                        }
                        LoadModelInformation();
                        txtModelName.Clear();
                        txtModelName.Focus();
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save product model information.");
                    }
                }
            }
        }



        private int InsertModel()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            ProductModel productModel = new ProductModel();
            productModel.Name = txtModelName.Text.Trim();
            //  productModel.BranchID = MTBFConstants.AppConstants.BranchID;
            //  productModel.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            result = DataAccessProxy.InsertProductModel(productModel);

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

            if (string.IsNullOrEmpty(txtModelName.Text))
            {
                MessageBoxHelper.ShowInformation("You need to enter model name");
                return false;
            }

            ProductModel productModel = DataAccessProxy.GetAllProductModel().Cast<ProductModel>().Where(s => s.Name.ToUpper().Trim() == txtModelName.Text.ToUpper().Trim()).ToList().FirstOrDefault();

            if (productModel != null && productModel.ProductModelID != modelID)
            {
                MessageBoxHelper.ShowInformation("Duplicate model information");
                return false;
            }
            return true;
        }

        private DataTable BuildModelTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ModelID");
            table.Columns.Add("Model Name");
            return table;
        }

        private void frmProductModel_Load(object sender, EventArgs e)
        {
            LoadModelInformation();
        }

        private void LoadModelInformation()
        {
            List<ProductModel> lstProductModel = new List<ProductModel>();
            // lstProductModel = new ProductManager().GetAllProductModel().Where(m => m.BranchID == MTBFConstants.AppConstants.BranchID && m.OrganizationID == MTBFConstants.AppConstants.OrganizationID).Cast<ProductModel>().ToList();
            lstProductModel = new ProductManager().GetAllProductModel().Cast<ProductModel>().OrderBy(p => p.Name).ToList();

            DataTable table = BuildModelTable();

            foreach (ProductModel productModel in lstProductModel)
            {
                DataRow row = table.NewRow();
                row["ModelID"] = productModel.ProductModelID;
                row["Model Name"] = productModel.Name;
                table.Rows.Add(row);
            }

            grvModelInfo.DataSource = table;
            grvModelInfo.DisplayLayout.Bands[0].Columns["ModelID"].Hidden = true;
        }

        private void grvModelInfo_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            if (grvModelInfo.Selected.Rows.Count > 0)
            {
                modelID = Convert.ToInt32(grvModelInfo.Selected.Rows[0].Cells["ModelID"].Value);
                ProductModel productModel = new ProductManager().GetPrductModelByID(modelID);
                txtModelName.Text = productModel.Name;
                IsUpdating = true;

            }
        }
    }
}
