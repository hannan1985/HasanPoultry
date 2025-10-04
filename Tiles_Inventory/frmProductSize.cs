using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NybSys.MTBF.Utility.Resources;
using NybSys.MTBF.Utility.Helper;
using IMFS.Common.DTO;
using IFMS.Facade;
using NybSys.MTBF.Utility.Constants;
using IFMS.BLL;
using NybSys.MTBF.Utility.Enums;


namespace Tiles_Inventory
{
    public partial class frmProductSize : BaseForm
    {

        public event LoadProductTypeEventHandler LoadProductSize;
        public delegate void LoadProductTypeEventHandler();
        int productSizeID = 0;
        public frmProductSize()
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
                    if (UpdateProductSize() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Product size saved successfully");
                        if (LoadProductSize != null)
                        {
                            LoadProductSize();
                        }
                        LoadProductSizeInformation();
                        IsUpdating = false;
                        productSizeID = 0;
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save product type information");
                    }
                }
                else
                {
                    if (InsertProductSize() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Product size saved successfully");
                        if (LoadProductSize != null)
                        {
                            LoadProductSize();
                        }
                        LoadProductSizeInformation();
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save product type information");
                    }
                }


            }
        }


        private int InsertProductSize()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            ProductSize productSize = new ProductSize();
            productSize.Name = txtProductSize.Text;
           // productSize.BranchID = MTBFConstants.AppConstants.BranchID;
          //  productSize.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            result = DataAccessProxy.InsertProductSize(productSize);
            return result;
        }


        private int UpdateProductSize()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            ProductSize productSize = new ProductManager().GetProductSizeByID(productSizeID);
            productSize.Name = txtProductSize.Text;
            //productSize.BranchID = MTBFConstants.AppConstants.BranchID;
           // productSize.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            DataAccessProxy.UpdateProductSize(productSize);
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

            if (string.IsNullOrEmpty(txtProductSize.Text.Trim()))
            {
                MessageBoxHelper.ShowInformation("You need to enter product size.");
                txtProductSize.Focus();
                return false;
            }

            ProductSize productSize = DataAccessProxy.GetAllProductSize().Cast<ProductSize>().Where(s => s.Name.ToUpper().Trim() == txtProductSize.Text.ToUpper().Trim()).ToList().FirstOrDefault();

            if (productSize != null)
            {
                MessageBoxHelper.ShowInformation("Duplicate product size informaiton.");
                return false;
            }
            return true;
        }

        private DataTable BuildSizeTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("SizeID");
            table.Columns.Add("Size");
            return table;
        }

        private void LoadProductSizeInformation()
        {
            List<ProductSize> lstProductSize = new List<ProductSize>();
           // lstProductSize = new ProductManager().GetAllProductSize().Where(s => s.BranchID == MTBFConstants.AppConstants.BranchID && s.OrganizationID == MTBFConstants.AppConstants.OrganizationID).Cast<ProductSize>().ToList();
            lstProductSize = new ProductManager().GetAllProductSize().Cast<ProductSize>().ToList();

            DataTable table = BuildSizeTable();
            foreach (ProductSize pSize in lstProductSize)
            {
                DataRow row = table.NewRow();
                row["SizeID"] = pSize.ProductSizeID;
                row["Size"] = pSize.Name;
                table.Rows.Add(row);
            }
            grvProductType.DataSource = table;
            grvProductType.DisplayLayout.Bands[0].Columns["SizeID"].Hidden = true;
        }


        private void txtProductSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (".1234567890".IndexOf(e.KeyChar) > -1 | e.KeyChar == Convert.ToChar(8))
            //{
            //    e.Handled = false;
            //}
            //else
            //{
            //    e.Handled = true;
            //}
        }

        private void frmProductSize_Load(object sender, EventArgs e)
        {
            LoadProductSizeInformation();            
        }

        private void grvProductType_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            if (grvProductType.Selected.Rows.Count > 0)
            {
                int pSizeID = Convert.ToInt32(grvProductType.Selected.Rows[0].Cells["SizeID"].Value);
                ProductSize productSize = new ProductManager().GetProductSizeByID(pSizeID);
                txtProductSize.Text = productSize.Name;
                IsUpdating = true;
                productSizeID = productSize.ProductSizeID;
            }
        }
    }
}
