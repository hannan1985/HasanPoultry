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
using IMFS.Common.View;
using NybSys.MTBF.Utility.Helper;

namespace Tiles_Inventory
{
    public partial class frmViewStock : BaseForm
    {
        private List<ProductSock> lstProductSock = new List<ProductSock>();
        List<ProductType> lstProductType = new List<ProductType>();

        public frmViewStock()
        {
            DataAccessProxy = new IFMS.Facade.FacadeManager();
            InitializeComponent();
        }

        private void frmViewStock_Load(object sender, EventArgs e)
        {
            timer1.Start();
            LoadProductTypeCombo();
        }

        private void LoadProductTypeCombo()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("Name");

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

        private void LoadProductInformationCombo()
        {
            lstProductSock = new List<ProductSock>();
            List<Inventroy> lstInventory = new List<Inventroy>();

            List<Product> lstProduct = new List<Product>();
            List<ProductModel> lstProductModel = new List<ProductModel>();

            List<PurchaseOrderDetail> lstPurcahseOrder = new List<PurchaseOrderDetail>();
            lstInventory = DataAccessProxy.GetAllInventroyInformation(MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<Inventroy>().ToList();

            string[] productID = lstInventory.Select(i => i.ProductID).Distinct().ToArray();
            string filter = string.Empty;


            if (productID.Length > 0)
            {
                for (int i = 0; i < productID.Length; i++)
                {
                    if (filter != string.Empty) filter = filter + ",";
                    filter = filter + "'" + productID[i] + "'";
                }

                filter = String.Format("{0} IN ({1})", "ProductID", filter);
                lstProduct = DataAccessProxy.GetFilteredProduct(filter);
            }
            int[] productModelID = lstProduct.Select(p => p.ProductModelID).Distinct().ToArray();
            if (productModelID.Length > 0)
            {
                string modelfilter = String.Format("{0} IN ({1})", "ProductModelID", String.Join(",", productModelID));
                lstProductModel = DataAccessProxy.GetFilteedProductModel(modelfilter);
            }
            foreach (Inventroy inventory in lstInventory)
            {
                ProductSock productSock = new ProductSock();
                productSock.ProductName = inventory.ProductName;
                ProductModel pModel = lstProductModel.Where(m => m.ProductModelID == inventory.ProductModelID).FirstOrDefault();
                productSock.Model = (pModel != null) ? pModel.Name : string.Empty;
                productSock.Quantity = inventory.Quantity;
                // productSock.CartonSize = inventory.CartonSize.ToString();
                //  productSock.Carton = (inventory.Quantity / inventory.CartonSize).ToString("F2");
                ProductType pType = lstProductType.Where(t => t.ProductTypeID == inventory.ProductTypeID).FirstOrDefault();
                productSock.Type = (pType != null) ? pType.TypeName : string.Empty;
                lstProductSock.Add(productSock);
            }
            grvProductInformation.DataSource = lstProductSock;
        }

        private class ProductSock
        {
            public string ProductName { get; set; }
            public string Model { get; set; }
            //public string Carton { get; set; }
            //public string CartonSize { get; set; }
            public decimal Quantity { get; set; }
            public string Type { get; set; }

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
            List<ProductSock> lstStockByModel = new List<ProductSock>();

            lstStockByModel = lstProductSock.Where(p => p.Model.ToLower().StartsWith(productModel.ToLower())).Cast<ProductSock>().ToList();
            grvProductInformation.DataSource = lstStockByModel;
        }

        private void txtProductName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtProductName.Text))
            {
                LoadProductInformationByProductName(txtProductName.Text);
            }
        }

        private void LoadProductInformationByProductName(string productName)
        {
            List<ProductSock> lstStockByName = new List<ProductSock>();
            lstStockByName = lstProductSock.Where(p => p.ProductName.ToLower().StartsWith(productName.ToLower())).Cast<ProductSock>().ToList();
            grvProductInformation.DataSource = lstStockByName;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            LoadProductInformationCombo();
        }

        private void btCreditSave_Click(object sender, EventArgs e)
        {
            ExportInvoiceInformation(@"\StockDetail.csv");
        }

        private void ExportInvoiceInformation(string fileName)
        {
            string location = "";
            DataTable table = new DataTable();

            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            DialogResult result = folderBrowser.ShowDialog();

            if (result == DialogResult.OK)
            {
                table = BuildTable();
                location = folderBrowser.SelectedPath + fileName;
                CSVCreator.CreateCSVFile(table, location);
                MessageBoxHelper.ShowInformation("Export successfully.");
            }
        }

        /// <summary>
        /// Method to fill  requisition  data in table.
        /// </summary>
        /// <returns></returns>
        private DataTable BuildTable()
        {
            DataTable stockTable = new DataTable();
            stockTable.Columns.Add("Product Name");
            stockTable.Columns.Add("Model");
            // stockTable.Columns.Add("Carton Size");
            //  stockTable.Columns.Add("Carton");

            stockTable.Columns.Add("Quantity");

            foreach (DataGridViewRow grow in grvProductInformation.Rows)
            {
                DataRow row = stockTable.NewRow();
                row["Product Name"] = grow.Cells["ProductName"].Value;
                row["Model"] = grow.Cells["Model"].Value;
                //  row["Carton Size"] = grow.Cells["CartonSize"].Value;
                //  row["Carton"] = grow.Cells["Carton"].Value;
                row["Quantity"] = grow.Cells["Quantity"].Value;
                stockTable.Rows.Add(row);
            }

            return stockTable;
        }

        private void cmbProductType_ValueChanged(object sender, EventArgs e)
        {
            if (cmbProductType.Value != null)
            {
                LoadProductInformationByProductType(cmbProductType.Text);
            }
        }

        private void LoadProductInformationByProductType(string productType)
        {
            if (!string.IsNullOrEmpty(productType))
            {
                List<ProductSock> lstStockByName = new List<ProductSock>();
                lstStockByName = lstProductSock.Where(p => p.Type.ToLower().StartsWith(productType.ToLower())).Cast<ProductSock>().ToList();
                grvProductInformation.DataSource = lstStockByName;
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadProductInformationCombo();
        }

    }
}
