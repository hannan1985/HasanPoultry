using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IMFS.Common.DTO;
using IMFS.Common.View;
using NybSys.MTBF.Utility.Constants;
using IFMS.BLL;
using IFMS.Facade;
using NybSys.MTBF.Utility.Helper;
using Infragistics.Win.UltraWinGrid;

namespace Tiles_Inventory
{
    public partial class frmViewStockPrice : BaseForm
    {
        private List<ProductSock> lstProductSock = new List<ProductSock>();
        List<ProductType> lstProductType = new List<ProductType>();
        List<Inventroy> lstInventory = new List<Inventroy>();

        public frmViewStockPrice()
        {
            DataAccessProxy = new FacadeManager();
            InitializeComponent();
        }

        private void frmViewStockPrice_Load(object sender, EventArgs e)
        {
            LoadProductInformationCombo();

        }



        private void LoadProductInformationCombo()
        {

            List<StockPrice> lstStockPrice = new List<StockPrice>();
            List<Product> lstProduct = new List<Product>();
            List<ProductModel> lstProductModel = new List<ProductModel>();

            List<PurchaseOrderDetail> lstPurcahseOrder = new List<PurchaseOrderDetail>();
            lstInventory = new ProductManager().GetAllInventroyInformation(MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<Inventroy>().ToList();

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
                lstPurcahseOrder = DataAccessProxy.GetPurcahseOrderDetailFiltered(filter).Cast<PurchaseOrderDetail>().ToList();
                lstStockPrice = new StockPriceManager().GetFilteredStockPrice(filter);
            }

            int[] productModelID = lstProduct.Select(p => p.ProductModelID).Distinct().ToArray();
            int[] productTypeID = lstProduct.Select(p => p.ProductTypeID).Distinct().ToArray();
            if (productModelID.Length > 0)
            {
                string modelfilter = String.Format("{0} IN ({1})", "ProductModelID", String.Join(",", productModelID));
                lstProductModel = DataAccessProxy.GetFilteedProductModel(modelfilter);

            }

            if (productTypeID.Length > 0)
            {
                string modelfilter = String.Format("{0} IN ({1})", "ProductTypeID", String.Join(",", productTypeID));
                lstProductType = DataAccessProxy.GetFilteedProductType(modelfilter);

            }

            foreach (Inventroy inventory in lstInventory)
            {
                StockPrice stockPrice = lstStockPrice.Where(s => s.ProductID == inventory.ProductID).FirstOrDefault();

                PurchaseOrderDetail porderDetail = lstPurcahseOrder.Where(p => p.ProductID == inventory.ProductID).Cast<PurchaseOrderDetail>().FirstOrDefault();

                decimal purchasePrice = (porderDetail != null) ? porderDetail.PurchasePrice : 1;

                ProductSock productSock = new ProductSock();
                productSock.ProductID = inventory.ProductID;
                productSock.ProductName = inventory.ProductName;
                ProductModel pModel = lstProductModel.Where(m => m.ProductModelID == inventory.ProductModelID).FirstOrDefault();
                productSock.Model = (pModel != null) ? pModel.Name : string.Empty;
                productSock.Quantity = inventory.Quantity;
                ProductType pType = lstProductType.Where(t => t.ProductTypeID == inventory.ProductTypeID).FirstOrDefault();
                productSock.Type = (pType != null) ? pType.TypeName : string.Empty;
                productSock.Price = (stockPrice != null) ? stockPrice.Price : purchasePrice;
                productSock.OldPrice = (stockPrice != null) ? stockPrice.OldPrice : purchasePrice;
                lstProductSock.Add(productSock);
            }
            grvProductInformation.DataSource = lstProductSock;
        }

        private class ProductSock
        {
            public string ProductID { get; set; }
            public string ProductName { get; set; }
            public string Model { get; set; }
            public decimal Quantity { get; set; }
            public string Type { get; set; }
            public decimal OldPrice { get; set; }
            public decimal Price { get; set; }

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


        /// <summary>
        /// Method to fill  requisition  data in table.
        /// </summary>
        /// <returns></returns>
        private DataTable BuildTable()
        {
            DataTable stockTable = new DataTable();
            stockTable.Columns.Add("Product Name");
            stockTable.Columns.Add("Model");
            stockTable.Columns.Add("Quantity");

            foreach (UltraGridRow grow in grvProductInformation.Rows)
            {
                DataRow row = stockTable.NewRow();
                row["Product Name"] = grow.Cells["ProductName"].Value;
                row["Model"] = grow.Cells["Model"].Value;
                row["Quantity"] = grow.Cells["Quantity"].Value;
                stockTable.Rows.Add(row);
            }

            return stockTable;
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

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (grvProductInformation.Selected.Rows.Count > 0)
            {
                Product product = new ProductManager().GetProductByID(grvProductInformation.Selected.Rows[0].Cells["ProductID"].Value.ToString());
                decimal price = decimal.Parse(grvProductInformation.Selected.Rows[0].Cells["Price"].Value.ToString());

                frmDefineStockPrice frm = new frmDefineStockPrice(product, price);
                frm.LoadStockPrice += new frmDefineStockPrice.LoadDefineStockPriceHandler(frm_LoadStockPrice);
                frm.ShowDialog();
            }

        }

        void frm_LoadStockPrice()
        {
            lstProductSock = new List<ProductSock>();
            LoadProductInformationCombo();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportInvoiceInformation(@"\Price List.csv");
        }

        private DataTable BuildExportTable()
        {
            DataTable stockTable = new DataTable();
            stockTable.Columns.Add("Product Name");
            stockTable.Columns.Add("Model");
            stockTable.Columns.Add("Type");
            stockTable.Columns.Add("Price");

            foreach (UltraGridRow grow in grvProductInformation.Rows)
            {
                DataRow row = stockTable.NewRow();
                row["Product Name"] = grow.Cells["ProductName"].Value;
                row["Model"] = grow.Cells["Model"].Value;
                row["Type"] = grow.Cells["Type"].Value;
                row["Price"] = grow.Cells["Price"].Value;
                stockTable.Rows.Add(row);
            }

            return stockTable;
        }

        private void ExportInvoiceInformation(string fileName)
        {
            string location = "";
            DataTable table = new DataTable();

            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            DialogResult result = folderBrowser.ShowDialog();

            if (result == DialogResult.OK)
            {
                table = BuildExportTable();
                location = folderBrowser.SelectedPath + fileName;
                CSVCreator.CreateCSVFile(table, location);
                MessageBoxHelper.ShowInformation("Export successfully.");
            }
        }

        private void grvProductInformation_ClickCell(object sender, ClickCellEventArgs e)
        {
            if (e.Cell.Column.Header.Caption == "Price")
            {
                grvProductInformation.Rows[e.Cell.Row.Index].Cells["Price"].Activate();
                grvProductInformation.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode);
            }
        }

        private void grvProductInformation_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case (int)Keys.Up:
                    grvProductInformation.PerformAction(UltraGridAction.ExitEditMode, false, false);
                    grvProductInformation.PerformAction(UltraGridAction.AboveCell, false, false);
                    e.Handled = true;
                    grvProductInformation.PerformAction(UltraGridAction.EnterEditMode, false, false);
                    break;
                case (int)Keys.Down:
                    grvProductInformation.PerformAction(UltraGridAction.ExitEditMode, false, false);
                    grvProductInformation.PerformAction(UltraGridAction.BelowCell, false, false);
                    e.Handled = true;
                    grvProductInformation.PerformAction(UltraGridAction.EnterEditMode, false, false);
                    break;
                //case (int)Keys.Right:
                //    grvProductInformation.PerformAction(UltraGridAction.ExitEditMode, false, false);
                //    grvProductInformation.PerformAction(UltraGridAction.NextCellByTab, false, false);
                //    e.Handled = true;
                //    grvProductInformation.PerformAction(UltraGridAction.EnterEditMode, false, false);
                //    break;
                //case (int)Keys.Left:
                //    grvProductInformation.PerformAction(UltraGridAction.ExitEditMode, false, false);
                //    grvProductInformation.PerformAction(UltraGridAction.PrevCellByTab, false, false);
                //    e.Handled = true;
                //    grvProductInformation.PerformAction(UltraGridAction.EnterEditMode, false, false);
                //    break;
            }
        }

    }
}
