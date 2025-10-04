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
using IFMS.BLL;

namespace Tiles_Inventory
{
    public partial class frmPartyStock : BaseForm
    {
        private List<ProductSock> lstProductSock = new List<ProductSock>();
        List<ProductType> lstProductType = new List<ProductType>();

        public frmPartyStock()
        {
            DataAccessProxy = new IFMS.Facade.FacadeManager();
            InitializeComponent();
        }

        private void frmViewStock_Load(object sender, EventArgs e)
        {
            timer1.Start();        
            LoadSalesCenterCombo();
        }


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
            cmbPartyName.DataSource = table;
            cmbPartyName.DisplayMember = "Name";
            cmbPartyName.ValueMember = "ID";
            cmbPartyName.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
        }
      

        private void LoadProductInformationCombo()
        {
            lstProductSock = new List<ProductSock>();
         
            lstProductType = new ProductManager().GetAllProductType();
            int partyID = 0;
            int.TryParse(cmbPartyName.Value.ToString(), out partyID);
            List<Inventroy> lstInventory = new List<Inventroy>();

            List<Product> lstProduct = new List<Product>();
            List<ProductModel> lstProductModel = new List<ProductModel>();

            List<PurchaseOrderDetail> lstPurcahseOrder = new List<PurchaseOrderDetail>();
            lstInventory = new ProductUsedManager().GetPartyStock(partyID).Cast<Inventroy>().ToList();

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
                productSock.TransferQty = inventory.TransferQty;
                productSock.UsedQty = inventory.UsedQty;
                ProductType pType = lstProductType.Where(t => t.ProductTypeID == inventory.ProductTypeID).FirstOrDefault();
                productSock.Type = (pType != null) ? pType.TypeName : string.Empty;
                lstProductSock.Add(productSock);
            }
            grvProductInformation.DataSource = lstProductSock;
            grvProductInformation.Columns["Model"].Visible = false;
        }

        private class ProductSock
        {
            public string ProductName { get; set; }
            public string Model { get; set; }
            public string Type { get; set; }
            public decimal TransferQty { get; set; }
            public decimal UsedQty { get; set; }
            public decimal Quantity { get; set; }
          

        }

       

        private void LoadProductInformationByModel(string productModel)
        {
            List<ProductSock> lstStockByModel = new List<ProductSock>();

            lstStockByModel = lstProductSock.Where(p => p.Model.ToLower().StartsWith(productModel.ToLower())).Cast<ProductSock>().ToList();
            grvProductInformation.DataSource = lstStockByModel;
        }

      

        private void LoadProductInformationByProductName(string productName)
        {
            List<ProductSock> lstStockByName = new List<ProductSock>();
            lstStockByName = lstProductSock.Where(p => p.ProductName.ToLower().StartsWith(productName.ToLower())).Cast<ProductSock>().ToList();
            grvProductInformation.DataSource = lstStockByName;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
          
         
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
            stockTable.Columns.Add("Type");
           // stockTable.Columns.Add("Carton Size");
          //  stockTable.Columns.Add("Carton");

            stockTable.Columns.Add("Quantity");

            foreach (DataGridViewRow grow in grvProductInformation.Rows)
            {
                DataRow row = stockTable.NewRow();
                row["Product Name"] = grow.Cells["ProductName"].Value;
                row["Type"] = grow.Cells["Type"].Value;
              //  row["Carton Size"] = grow.Cells["CartonSize"].Value;
              //  row["Carton"] = grow.Cells["Carton"].Value;
                row["Quantity"] = grow.Cells["Quantity"].Value;
                stockTable.Rows.Add(row);
            }

            return stockTable;
        }

        private void cmbProductType_ValueChanged(object sender, EventArgs e)
        {
            if (cmbPartyName.Value != null)
            {
                LoadProductInformationCombo();
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
