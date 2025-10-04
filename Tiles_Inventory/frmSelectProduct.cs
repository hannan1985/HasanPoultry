using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IMFS.Common.DTO;
using IFMS.Facade;
using NybSys.MTBF.Utility.Constants;
using NybSys.MTBF.Utility.Helper;

namespace Tiles_Inventory
{
    public partial class frmSelectProduct : BaseForm
    {
        private List<Product> _lstProductInfo = new List<Product>();

        public event LoadProductInfoEventHandler LoadProductInfo;
        public delegate void LoadProductInfoEventHandler(string productID);
      

        public frmSelectProduct()
        {
         
            DataAccessProxy = new FacadeManager();
            InitializeComponent();
        }

        private void frmSelectProduct_Load(object sender, EventArgs e)
        {
            LoadProductTypeCombo();
            LoadProductInformationCombo();
        }



        private void LoadProductInformationCombo()
        {
            _lstProductInfo = DataAccessProxy.GetAllProduct(MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<Product>().ToList();
        }



        private void txtProductModel_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtProductModel.Text))
            {
                LoadProductInformationByModel(txtProductModel.Text);
            }
        }

        private void LoadProductInformationByModel(string productModel)
        {
            try
            {
                List<Product> lstProduct = new List<Product>();
                lstProduct = _lstProductInfo.Where(p => p.ProductModelName.ToLower().StartsWith(productModel.ToLower())).Cast<Product>().ToList();
                LoadProductInformationInGrid(lstProduct);
            }
            catch (Exception ex)
            {
                MessageBoxHelper.ShowInformation("Product information load fail.");
            }

        }


        /// <summary>
        /// Method to load product type information.
        /// </summary>
        private void LoadProductTypeCombo()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("Name");
            List<ProductType> lstProductType = new List<ProductType>();
            lstProductType = DataAccessProxy.GetAllProductType().Cast<ProductType>().ToList();


            DataRow emptyRow = table.NewRow();
            emptyRow[0] = -1;
            emptyRow[1] = "-Select-";
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
            cmbProductType.Value = -1;
        }



        private void LoadProductInformationInGrid(List<Product> lstProduct)
        {
            DataTable pTable = BuildProductTable();
            foreach (Product product in lstProduct)
            {
                DataRow row = pTable.NewRow();

                row["Product ID"] = product.ProductID;
                row["Product Name"] = product.ProductName;
                row["Product Type"] = product.TypeName;
                row["Product Model"] = product.ShelfNumber;
                row["Supplier Name"] = product.SupplierName;
                row["Size"] = product.SizeName;
                row["Product Color"] = product.ColorName;
                pTable.Rows.Add(row);
            }
            grvProductInformation.DataSource = pTable;
            grvProductInformation.DisplayLayout.Bands[0].Columns["Product ID"].Hidden = true;

        }

        private DataTable BuildProductTable()
        {
            DataTable pTable = new DataTable();
            pTable.Columns.Add("Product ID");
            pTable.Columns.Add("Product Name");
            pTable.Columns.Add("Product Type");
            pTable.Columns.Add("Product Model");
            pTable.Columns.Add("Size");
            pTable.Columns.Add("Supplier Name");
            pTable.Columns.Add("Product Color");
            return pTable;
        }

        /// <summary>
        /// Method to load product name in combo box.
        /// </summary>
        /// <remarks></remarks>
        private void LoadPrductInformaitonByProductType(int productTypeID)
        {
            List<Product> lstproduct = new List<Product>();//DataAccessProxy.GetAllProductInformationByProductTypeID(productTypeID);
            lstproduct = _lstProductInfo.Where(p => p.ProductTypeID == productTypeID).Cast<Product>().ToList();
            LoadProductInformationInGrid(lstproduct);
        }

        private void cmbProductType_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbProductType.Value) != -1)
            {
                LoadPrductInformaitonByProductType(Convert.ToInt32(cmbProductType.Value));
            }

        }

        private void grvProductInformation_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grvProductInformation_DoubleClickCell(object sender, Infragistics.Win.UltraWinGrid.DoubleClickCellEventArgs e)
        {
            if (grvProductInformation.Selected.Rows.Count > 0)
            {
                if (LoadProductInfo != null)
                    LoadProductInfo(grvProductInformation.Selected.Rows[0].Cells["Product ID"].Value.ToString());

            }
        }
    }
}
