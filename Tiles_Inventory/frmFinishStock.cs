using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IMFS.Common.DTO;
using IFMS.BLL;
using NybSys.MTBF.Utility.Helper;
using NybSys.MTBF.Utility.Constants;

namespace Tiles_Inventory
{
    public partial class frmFinishStock : BaseForm
    {
        private List<ProductSock> lstProductSock = new List<ProductSock>();

        public frmFinishStock()
        {
            InitializeComponent();
        }

        private void frmMaterialStock_Load(object sender, EventArgs e)
        {
            LoadProductInformationCombo();
        }


        private class ProductSock
        {
            public string ProductName { get; set; }
            public decimal Quantity { get; set; }
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

        private void btCreditSave_Click(object sender, EventArgs e)
        {
            ExportInvoiceInformation(@"\MaterialStock.csv");
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
            stockTable.Columns.Add("Quantity");

            foreach (DataGridViewRow grow in grvProductInformation.Rows)
            {
                DataRow row = stockTable.NewRow();
                row["Product Name"] = grow.Cells["ProductName"].Value;
                row["Quantity"] = grow.Cells["Quantity"].Value;
                stockTable.Rows.Add(row);
            }

            return stockTable;
        }


        private void LoadProductInformationCombo()
        {
            List<StockDetails> lstStockDetail = new List<StockDetails>();

            lstStockDetail = new StockManager().GetAllFinishProductStock(MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID);

            foreach (StockDetails sDetail in lstStockDetail)
            {
                ProductSock productSock = new ProductSock();
                Product product = new ProductManager().GetProductByID(sDetail.ProductID);
                productSock.ProductName = product.ProductName;
                productSock.Quantity = sDetail.Quantity;
                lstProductSock.Add(productSock);
            }
            grvProductInformation.DataSource = lstProductSock;
        }

    }
}
