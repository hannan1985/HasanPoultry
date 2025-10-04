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
using IMFS.Common.Utility;
using Infragistics.Win.UltraWinGrid;
using NybSys.MTBF.Utility.Helper;
using NybSys.MTBF.Utility.Constants;
using Infragistics.Excel;
using System.IO;
using IFMS.BLL;

namespace Tiles_Inventory
{
    public partial class frmViewProduct : BaseForm
    {
        string filtered = string.Empty;
        private int _serialNo = 0;
        private List<Product> lstProductInfo = new List<Product>();

        public frmViewProduct()
        {
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        private void frmViewProduct_Load(System.Object sender, System.EventArgs e)
        {
           
            LoadProductTypeCombo();
            grvProductInformation.DataSource = BuildProductTable();
            grvProductInformation.DisplayLayout.Bands[0].Columns[0].Hidden = true;
            grvProductInformation.DisplayLayout.Bands[0].Columns[1].Hidden = true;
            LoadProductInformationCombo();
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

        /// <summary>
        /// Method to load product information 
        /// </summary>
        private void LoadProductInformationWithLike(string productName)
        {


            DataTable productTable = new DataTable();
            productTable.Columns.Add("ID");
            productTable.Columns.Add("Name");

            lstProductInfo = DataAccessProxy.GetAllProduct().Cast<Product>().Where(p => p.ProductName.ToLower().StartsWith(productName.ToLower())).ToList();
            foreach (Product product in lstProductInfo)
            {
                DataRow row = productTable.NewRow();
                row[0] = product.ProductID;
                row[1] = product.ProductName;
                productTable.Rows.Add(row);

            }
            cmbProductInformation.DataSource = productTable;
            cmbProductInformation.DisplayMember = "Name";
            cmbProductInformation.ValueMember = "ID";

        }



        private void LoadProductInformationCombo()
        {

            DataTable productTable = new DataTable();
            productTable.Columns.Add("ID");
            productTable.Columns.Add("Name");
            List<ProductType> lstProductType = new List<ProductType>();
            List<ProductModel> lstProductModel = new List<ProductModel>();

            lstProductInfo = DataAccessProxy.GetAllProduct(MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<Product>().ToList();
            if (lstProductInfo.Count > 0)
            {
                int[] productTypeID = lstProductInfo.Select(d => d.ProductTypeID).Distinct().ToArray();
                string filter = String.Format("{0} IN ({1})", "ProductTypeID", String.Join(",", productTypeID));
                lstProductType = DataAccessProxy.GetFilteedProductType(filter);

                int[] productModelID = lstProductInfo.Select(d => d.ProductModelID).Distinct().ToArray();
                filter = String.Format("{0} IN ({1})", "ProductModelID", String.Join(",", productModelID));
                lstProductModel = DataAccessProxy.GetFilteedProductModel(filter);


                foreach (Product product in lstProductInfo)
                {
                    DataRow row = productTable.NewRow();
                    row[0] = product.ProductID;
                    row[1] = product.ProductName;
                    productTable.Rows.Add(row);

                }
            }

            cmbProductInformation.DataSource = productTable;
            cmbProductInformation.DisplayMember = "Name";
            cmbProductInformation.ValueMember = "ID";

            LoadProductInformationInGrid(lstProductInfo);

        }

        /// <summary>
        /// Method to load product name in combo box.
        /// </summary>
        /// <remarks></remarks>
        private void LoadPrductInformaitonByProductID(string productID)
        {
            List<Product> lstProduct = new List<Product>();
            Product product = DataAccessProxy.GetProductInformationByID(productID);
            if (product != null)
            {
                lstProduct.Add(product);
                LoadProductInformationInGrid(lstProduct);
            }
            else
            {
                MessageBoxHelper.ShowInformation("No data found for this combination.");
            }
        }

        /// <summary>
        /// Method to load product information in grid.
        /// </summary>
        /// <param name="filteredString"></param>
        /// <remarks></remarks>
        private void LoadProductInformation(string filteredString)
        {
            try
            {
                List<Product> lstProduct = new List<Product>();
                lstProduct = DataAccessProxy.GetAllProduct().Cast<Product>().Where(p => p.ProductName.ToLower().StartsWith(filteredString.ToLower()) && p.BranchID == MTBFConstants.AppConstants.BranchID).ToList();
                LoadProductInformationInGrid(lstProduct);
                filtered = filteredString;
            }
            catch (Exception ex)
            {
                MessageBoxHelper.ShowInformation("Product information load fail.");
            }
        }

        private void LoadProductInformationByModel(string productModel)
        {
            try
            {
                List<Product> lstProduct = new List<Product>();
                // lstProduct = DataAccessProxy.GetAllProductByProductModel().Cast<Product>().Where(p => p.ProductModelName.ToLower().StartsWith(productModel.ToLower())).ToList();
                lstProduct = lstProductInfo.Where(p => p.ProductModelName.ToLower().StartsWith(productModel.ToLower())).Cast<Product>().ToList();

                LoadProductInformationInGrid(lstProduct);
            }
            catch (Exception ex)
            {
                MessageBoxHelper.ShowInformation("Product information load fail.");
            }

        }

        private void LoadProductInformationByProductName(string productName)
        {
            try
            {
                List<Product> lstProduct = new List<Product>();
                lstProduct = lstProductInfo.Where(p => p.ProductName.ToLower().StartsWith(productName.ToLower())).Cast<Product>().ToList();
                LoadProductInformationInGrid(lstProduct);
            }
            catch (Exception ex)
            {
                MessageBoxHelper.ShowInformation("Product information load fail.");
            }

        }



        private void LoadProductInformationInGrid(List<Product> lstProduct)
        {
            DataTable pTable = BuildProductTable();
            foreach (Product product in lstProduct)
            {
                DataRow row = pTable.NewRow();
                row["Serial No"] = product.SerialNo;
                row["Product ID"] = product.ProductID;
                row["Product Name"] = product.ProductName;
                row["Product Type"] = product.TypeName;
                row["Shelf Number"] = product.ShelfNumber;
                row["Supplier Name"] = product.SupplierName;
                row["Discontinued"] = product.Discountinued;
              //  row["Company Name"] = product.CompanyName;
                row["Model"] = product.ProductModelName;
                row["Pack Size"] = product.PackSize;
                row["Unit"] = product.Unit;
                row["Barcode"] = product.BarCode;
                //row["Carton Size"] = product.CartonSize;
                pTable.Rows.Add(row);
            }
            grvProductInformation.DataSource = pTable;

            grvProductInformation.DisplayLayout.Bands[0].Columns[0].Hidden = true;
            grvProductInformation.DisplayLayout.Bands[0].Columns[1].Hidden = true;
            grvProductInformation.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
            grvProductInformation.DisplayLayout.PerformAutoResizeColumns(false, PerformAutoSizeType.AllRowsInBand, true);
        }

        private DataTable BuildProductTable()
        {
            DataTable pTable = new DataTable();
            pTable.Columns.Add("Serial No");
            pTable.Columns.Add("Product ID");
            pTable.Columns.Add("Product Name");
            pTable.Columns.Add("Product Type");
            pTable.Columns.Add("Shelf Number");
            pTable.Columns.Add("Supplier Name");
            pTable.Columns.Add("Discontinued");
           // pTable.Columns.Add("Company Name");
            pTable.Columns.Add("Model");
            pTable.Columns.Add("Pack Size");
            pTable.Columns.Add("Unit");
            pTable.Columns.Add("Barcode");
            return pTable;
        }

        private void btnView_Click(System.Object sender, System.EventArgs e)
        {
            if ((cmbProductInformation.Value != null))
            {
                LoadPrductInformaitonByProductID(cmbProductInformation.Value.ToString());
            }

        }

        private void btnA_Click(System.Object sender, System.EventArgs e)
        {
            LoadProductInformation("A");
        }

        private void btnB_Click(System.Object sender, System.EventArgs e)
        {
            LoadProductInformation("B");
        }

        private void btnC_Click(System.Object sender, System.EventArgs e)
        {
            LoadProductInformation("C");
        }

        private void btnD_Click(System.Object sender, System.EventArgs e)
        {
            LoadProductInformation("D");
        }

        private void btnE_Click(System.Object sender, System.EventArgs e)
        {
            LoadProductInformation("E");
        }

        private void btnF_Click(System.Object sender, System.EventArgs e)
        {
            LoadProductInformation("F");
        }

        private void btnG_Click(System.Object sender, System.EventArgs e)
        {
            LoadProductInformation("G");
        }

        private void btnH_Click(System.Object sender, System.EventArgs e)
        {
            LoadProductInformation("H");
        }

        private void btnI_Click(System.Object sender, System.EventArgs e)
        {
            LoadProductInformation("I");
        }

        private void btnJ_Click(System.Object sender, System.EventArgs e)
        {
            LoadProductInformation("J");
        }

        private void btnK_Click(System.Object sender, System.EventArgs e)
        {
            LoadProductInformation("K");
        }

        private void btnL_Click(System.Object sender, System.EventArgs e)
        {
            LoadProductInformation("L");
        }

        private void btnM_Click(System.Object sender, System.EventArgs e)
        {
            LoadProductInformation("M");
        }

        private void btnN_Click(System.Object sender, System.EventArgs e)
        {
            LoadProductInformation("N");
        }

        private void btnO_Click(System.Object sender, System.EventArgs e)
        {
            LoadProductInformation("O");
        }

        private void btnP_Click(System.Object sender, System.EventArgs e)
        {
            LoadProductInformation("P");
        }

        private void btnQ_Click(System.Object sender, System.EventArgs e)
        {
            LoadProductInformation("Q");
        }

        private void btnR_Click(System.Object sender, System.EventArgs e)
        {
            LoadProductInformation("R");
        }

        private void btnS_Click(System.Object sender, System.EventArgs e)
        {
            LoadProductInformation("S");
        }

        private void btnT_Click(System.Object sender, System.EventArgs e)
        {
            LoadProductInformation("T");
        }

        private void btnU_Click(System.Object sender, System.EventArgs e)
        {
            LoadProductInformation("U");
        }

        private void btnV_Click(System.Object sender, System.EventArgs e)
        {
            LoadProductInformation("V");
        }

        private void btnW_Click(System.Object sender, System.EventArgs e)
        {
            LoadProductInformation("W");
        }

        private void btnX_Click(System.Object sender, System.EventArgs e)
        {
            LoadProductInformation("X");
        }

        private void btnY_Click(System.Object sender, System.EventArgs e)
        {
            LoadProductInformation("Y");
        }

        private void btnZ_Click(System.Object sender, System.EventArgs e)
        {
            LoadProductInformation("Z");
        }

        private void btnAdd_Click(System.Object sender, System.EventArgs e)
        {
            frmProduct frm = new frmProduct(_serialNo, false, string.Empty);
            frm.Load_Product_Type += OnProductInformationLoad;
            frm.ShowDialog();
        }

        private void OnProductInformationLoad()
        {
            if (filtered != string.Empty)
            {
                LoadProductInformation(filtered);
            }
        }

        private void btnEdit_Click(System.Object sender, System.EventArgs e)
        {
            string productId = null;

            if (grvProductInformation.Selected.Rows.Count > 0)
            {
                _serialNo = Convert.ToInt32(grvProductInformation.Selected.Rows[0].Cells[0].Value);
                productId = grvProductInformation.Selected.Rows[0].Cells[1].Value.ToString();
                frmProduct frm = new frmProduct(_serialNo, true, productId);
                frm.Load_Product_Type += OnProductInformationLoad;
                frm.ShowDialog();
            }
            else
            {
                MessageBoxHelper.ShowInformation("Please select a row");
            }
        }

        private void btnRefresh_Click(System.Object sender, System.EventArgs e)
        {
            LoadProductInformationCombo();
            LoadProductTypeCombo();

        }

        private void btnClose_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void cmbProductInformation_TextChanged(object sender, EventArgs e)
        {
            //string filter = cmbProductInformation.Text;
            //LoadProductInformationWithLike(filter);
        }

        private void btnProductType_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbProductType.Value) != -1)
            {
                LoadPrductInformaitonByProductType(Convert.ToInt32(cmbProductType.Value));
            }
            else
            {
                MessageBoxHelper.ShowInformation("You need to select product type.");
                cmbProductType.Focus();
            }
        }

        private void btnProductModel_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Method to load product name in combo box.
        /// </summary>
        /// <remarks></remarks>
        private void LoadPrductInformaitonByProductType(int productTypeID)
        {
            List<Product> lstproduct = new List<Product>();//DataAccessProxy.GetAllProductInformationByProductTypeID(productTypeID);
            lstproduct = lstProductInfo.Where(p => p.ProductTypeID == productTypeID).Cast<Product>().ToList();
            LoadProductInformationInGrid(lstproduct);
        }

        private void txtProductModel_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtProductModel.Text))
            {
                LoadProductInformationByModel(txtProductModel.Text);
            }
        }

        private void cmbProductInformation_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridLayout layout = e.Layout;
            layout.PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
        }

        private void txtProductName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtProductName.Text))
            {
                LoadProductInformationByProductName(txtProductName.Text);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (grvProductInformation.Rows.Count > 0)
            {
                ExportData();
            }
            else
            {
                MessageBoxHelper.ShowInformation("No data for export.");
            }
        }

        private void ExportData()
        {
            string fileLoacation = "";
            DataTable table = new DataTable();
            Branch branch = new BranchManager().GetBranchByID(MTBFConstants.AppConstants.BranchID);
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            DialogResult result = folderBrowser.ShowDialog();


            if (result == DialogResult.OK)
            {
                string nameOfFile = "Product Infromation " + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                fileLoacation = string.Format("{0}\\{1}", folderBrowser.SelectedPath, nameOfFile);

                // Check if file already exists. If yes, delete it. 
                if (File.Exists(fileLoacation))
                {
                    File.Delete(fileLoacation);
                }

                Workbook WB = new Workbook();
                WB.Worksheets.Add("Product Infromation");

                // add Title
                Worksheet sheet = WB.Worksheets["Product Infromation"];
                sheet.Rows[0].Cells[0].Value = (branch != null) ? branch.BranchName : string.Empty; // Title
                sheet.Rows[0].Cells[0].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;

                sheet.Rows[1].Cells[0].Value = "Product Infromation";
                sheet.Rows[1].Cells[0].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;


                ultraGridExcelExporter1.Export(grvProductInformation, WB.Worksheets["Product Infromation"], 6, 0);



                BIFF8Writer.WriteWorkbookToFile(WB, fileLoacation);
                WB.Save(fileLoacation);

                MessageBoxHelper.ShowInformation("Exported Successfully");
            }

        }

        private void txtBarcode_KeyUp(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 13)
            {
                List<Product> lstProduct = new List<Product>();              
                lstProduct = lstProductInfo.Where(p => p.BarCode==txtBarcode.Text.Trim()).Cast<Product>().ToList();
                LoadProductInformationInGrid(lstProduct);
            }
        }

    }
}
