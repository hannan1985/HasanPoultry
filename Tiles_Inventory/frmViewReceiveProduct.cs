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
using IFMS.Facade;
using NybSys.MTBF.Utility.Helper;
using IMFS.Common.View;
using IFMS.BLL;
using System.IO;
using Bytescout.Spreadsheet;
using NybSys.MTBF.Utility.Enums;

namespace Tiles_Inventory
{
    public partial class frmViewReceiveProduct : BaseForm
    {
        List<VMReceiveProduct> lstVMReceiveProduct = new List<VMReceiveProduct>();
        public frmViewReceiveProduct()
        {
            DataAccessProxy = new FacadeManager();
            InitializeComponent();
        }

        private void frmViewReceiveProduct_Load(object sender, EventArgs e)
        {

        }


        private DataTable BuildTransferTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Receive Number");
            table.Columns.Add("Receive Date");
            table.Columns.Add("Sales Center");
            table.Columns.Add("Received By");
            table.Columns.Add("Is Edited");
            return table;

        }

        private DataTable BuildTransferDetailTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Product Name");
            table.Columns.Add("Quantity");
            return table;

        }


        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadReceiveProductInformationByDate(dtpFromDate.Value.ToString("yyyy/MM/dd") + MTBFConstants.DAY_START_TIME, dtpToDate.Value.ToString("yyyy/MM/dd") + MTBFConstants.DAY_END_TIME);

        }

        private void LoadReceiveProductInformationByDate(string fromDate, string toDate)
        {
            DataTable transferTable = BuildTransferTable();
            List<Users> lstUsers = new List<Users>();
            List<SalesCenter> lstSalesCenter = new List<SalesCenter>();

            string filter = string.Format("{0} between '{1}' and '{2}'", "ReceiveDate", fromDate, toDate);

            lstVMReceiveProduct = new ReceiveProductManager().GetFilteredReceiveProduct(filter);

            // List<ReceiveProduct> lstReceiveProduct = DataAccessProxy.GetAllReceiveProductByDate(fromDate, toDate).Where(t => t.BranchID == MTBFConstants.AppConstants.BranchID && t.OrganizationID == MTBFConstants.AppConstants.OrganizationID).Cast<ReceiveProduct>().ToList();

            int[] salesCenerIDs = lstVMReceiveProduct.Select(i => i.ReceiveProduct.SalesCenterID).Distinct().ToArray();
            filter = string.Empty;

            string[] userIDs = lstVMReceiveProduct.Select(i => i.ReceiveProduct.ReceiveBy).Distinct().ToArray();



            if (userIDs.Length > 0)
            {
                for (int i = 0; i <= userIDs.Length - 1; i++)
                {
                    if (filter != string.Empty) filter = filter + ",";
                    filter = filter + "'" + userIDs[i] + "'";
                }

                filter = String.Format("{0} IN ({1})", "UserID", filter);
                lstUsers = DataAccessProxy.GetFilterUser(filter).Cast<Users>().ToList();
            }


            if (salesCenerIDs.Length > 0)
            {
                filter = String.Format("{0} IN ({1})", "SalesCenterID", String.Join(",", salesCenerIDs));
                lstSalesCenter = DataAccessProxy.GetFilteredSalesCenterInformation(filter);

            }

            foreach (VMReceiveProduct vmReceiveProduct in lstVMReceiveProduct)
            {
                Users user = lstUsers.Where(u => u.UserID == vmReceiveProduct.ReceiveProduct.ReceiveBy).Cast<Users>().ToList().FirstOrDefault();
                SalesCenter salesCenter = lstSalesCenter.Where(s => s.SalesCenterID == vmReceiveProduct.ReceiveProduct.SalesCenterID).Cast<SalesCenter>().ToList().FirstOrDefault();
                Employee employee = DataAccessProxy.GetEmployeeByID(user.EmployeeID);
                DataRow row = transferTable.NewRow();
                row["Receive Number"] = vmReceiveProduct.ReceiveProduct.ReceiveProductID;
                row["Sales Center"] = (salesCenter != null) ? salesCenter.SalesCenterName : string.Empty;
                row["Receive Date"] = vmReceiveProduct.ReceiveProduct.ReceiveDate.ToShortDateString();
                row["Received By"] = (employee != null) ? employee.EmployeeName : string.Empty;
                row["Is Edited"] = string.IsNullOrEmpty(vmReceiveProduct.ReceiveProduct.UpdatedBy) ? false : true;
                transferTable.Rows.Add(row);
            }
            grvTransferInfo.DataSource = transferTable;
            if (transferTable.Rows.Count == 0)
            {
                MessageBoxHelper.ShowInformation("No data found for this combination");
            }
        }

        private void btDistribute_Click(object sender, EventArgs e)
        {
            frmBranchReceive frm = new frmBranchReceive();
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            LoadReceiveProductInformationByDate(dtpFromDate.Value.ToString("yyyy/MM/dd"), dtpToDate.Value.ToString("yyyy/MM/dd"));
        }

        private void grvTransferInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void LoadTransferDetailInformation(List<ReceiveProductDetail> lstReceiveProductDetial)
        {

            DataTable transferDetailTable = BuildTransferDetailTable();
            foreach (ReceiveProductDetail tDetail in lstReceiveProductDetial)
            {
                DataRow row = transferDetailTable.NewRow();
                row["Product Name"] = tDetail.ProductName;
                row["Quantity"] = tDetail.Quantity;
                transferDetailTable.Rows.Add(row);
            }

            grvTransferDetailInfo.DataSource = transferDetailTable;
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            if (grvTransferInfo.Selected.Rows.Count > 0)
            {
                int transferID = Convert.ToInt32(grvTransferInfo.Selected.Rows[0].Cells["Receive Number"].Value);
                VMReceiveProduct vmReceiveProduct = lstVMReceiveProduct.Where(v => v.ReceiveProduct.ReceiveProductID == transferID).FirstOrDefault();
                if (vmReceiveProduct != null)
                {
                    frmBranchReceive frm = new frmBranchReceive(0, vmReceiveProduct, true);
                    frm.ShowDialog();
                }
            }
            else
            {
                MessageBoxHelper.ShowInformation("You need to select a row.");
            }

        }

        private void grvTransferInfo_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            if (grvTransferInfo.Selected.Rows.Count > 0)
            {
                int transferID = Convert.ToInt32(grvTransferInfo.Selected.Rows[0].Cells["Receive Number"].Value);

                VMReceiveProduct vmReceiveProduct = lstVMReceiveProduct.Where(v => v.ReceiveProduct.ReceiveProductID == transferID).FirstOrDefault();
                if (vmReceiveProduct != null)
                {
                    LoadTransferDetailInformation(vmReceiveProduct.lstRecevieProductDetail);
                }
            }
        }

        private void btPrint_Click(object sender, EventArgs e)
        {

        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            string folderPath = "";
            OpenFileDialog folderBrowserDialog1 = new OpenFileDialog();



            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                folderPath = folderBrowserDialog1.FileName;

                if (ImportProduct(folderPath) == (int)MTBFEnums.ReturnResult.SUCCESS)
                {
                    MessageBoxHelper.ShowInformation("Product imported successfully");
                }
                else
                {
                    MessageBoxHelper.ShowInformation("Failed to import product");
                }
            }
        }




        private int ImportProduct(string folderPath)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            VMProductImport vmProductImport = new VMProductImport();
            byte[] fileByte = File.ReadAllBytes(folderPath);

            int productNameIndex = 0;
            int productTypeIndex = 1;
            int productBrandIndex = 2;
            int orginIndex = 3;
            int sizeIndex = 4;
            int quantityIndex = 5;
            int barcodeIndex = 6;
            int rateIndex = 7;
            int packSizeIndex = 8;
            int unitIndex = 9;

            Infragistics.Excel.Workbook workbook = Infragistics.Excel.Workbook.Load(folderPath);


            List<StockPrice> lstStockPrice = new List<StockPrice>();

            List<Product> lstProduct = new List<Product>();
            List<ProductType> lstProductType = new List<ProductType>();
            List<ProductSize> lstProductSize = new List<ProductSize>();
            List<ProductModel> lstProductBrand = new List<ProductModel>();
            List<ProductColor> lstProductOrigin = new List<ProductColor>();
            // List<Unit> lstUnit = new List<Unit>();
            List<ReceiveProductDetail> lstReceiveProductDetail = new List<ReceiveProductDetail>();

            List<ReceiveProductDetail> lstReceiveProductDetailWithZeroQty = new List<ReceiveProductDetail>();


            Dictionary<string, int> earnLeaveDic = new Dictionary<string, int>();
            // Check dates
            string filter = string.Empty;
            for (int i = 1; i < 10000; i++)
            {
                if (workbook.Worksheets[0].Rows[i].Cells[productNameIndex].Value != null && !string.IsNullOrEmpty(workbook.Worksheets[0].Rows[i].Cells[productNameIndex].Value.ToString()))
                {

                    Product product = new Product();
                    ProductType productType = new ProductType();
                    ProductModel productBrand = new ProductModel();
                    ProductSize productSize = new ProductSize();
                    // Unit unit = new Unit();
                    ProductColor productOrigin = new ProductColor();

                    StockPrice stockPrice = new StockPrice();

                    ReceiveProductDetail receiveDetail = new ReceiveProductDetail();

                    product.ProductName = workbook.Worksheets[0].Rows[i].Cells[productNameIndex].Value.ToString().Trim();
                    product.TypeName = (workbook.Worksheets[0].Rows[i].Cells[productTypeIndex].Value != null) ? workbook.Worksheets[0].Rows[i].Cells[productTypeIndex].Value.ToString().Trim() : string.Empty;
                    product.SizeName = (workbook.Worksheets[0].Rows[i].Cells[sizeIndex].Value != null) ? workbook.Worksheets[0].Rows[i].Cells[sizeIndex].Value.ToString().Trim() : string.Empty;
                    product.ColorName = (workbook.Worksheets[0].Rows[i].Cells[orginIndex].Value != null) ? workbook.Worksheets[0].Rows[i].Cells[orginIndex].Value.ToString().Trim() : string.Empty;
                    product.Unit = (workbook.Worksheets[0].Rows[i].Cells[unitIndex].Value != null) ? workbook.Worksheets[0].Rows[i].Cells[unitIndex].Value.ToString().Trim() : string.Empty;
                    product.ProductModelName = (workbook.Worksheets[0].Rows[i].Cells[productBrandIndex].Value != null) ? workbook.Worksheets[0].Rows[i].Cells[productBrandIndex].Value.ToString().Trim() : string.Empty;
                    product.BarCode = (workbook.Worksheets[0].Rows[i].Cells[barcodeIndex].Value != null) ? workbook.Worksheets[0].Rows[i].Cells[barcodeIndex].Value.ToString().Trim() : string.Empty;
                    product.PackSize = (workbook.Worksheets[0].Rows[i].Cells[packSizeIndex].Value != null) ? workbook.Worksheets[0].Rows[i].Cells[packSizeIndex].Value.ToString().Trim() : "1";

                    productType.TypeName = (workbook.Worksheets[0].Rows[i].Cells[productTypeIndex].Value != null) ? workbook.Worksheets[0].Rows[i].Cells[productTypeIndex].Value.ToString().Trim() : string.Empty;


                    productSize.Name = (workbook.Worksheets[0].Rows[i].Cells[sizeIndex].Value != null) ? workbook.Worksheets[0].Rows[i].Cells[sizeIndex].Value.ToString().Trim() : string.Empty;

                    productBrand.Name = (workbook.Worksheets[0].Rows[i].Cells[productBrandIndex].Value != null) ? workbook.Worksheets[0].Rows[i].Cells[productBrandIndex].Value.ToString().Trim() : string.Empty;

                    productOrigin.Name = (workbook.Worksheets[0].Rows[i].Cells[orginIndex].Value != null) ? workbook.Worksheets[0].Rows[i].Cells[orginIndex].Value.ToString().Trim() : string.Empty;

                    //  unit.UnitName = (workbook.Worksheets[0].Rows[i].Cells[unitIndex].Value != null) ? workbook.Worksheets[0].Rows[i].Cells[unitIndex].Value.ToString().Trim() : string.Empty;


                    decimal quantity = 0;
                    if (workbook.Worksheets[0].Rows[i].Cells[quantityIndex].Value != null)
                    {
                        decimal.TryParse(workbook.Worksheets[0].Rows[i].Cells[quantityIndex].Value.ToString(), out quantity);
                        receiveDetail.Quantity = quantity;

                    }
                    receiveDetail.ProductName = product.ProductName;


                    Product existingProdcut = lstProduct.Where(p => p.ProductName.ToLower().Trim() == product.ProductName.ToLower().Trim()
                    && p.BarCode.Trim() == product.BarCode.Trim()).FirstOrDefault();
                    if (existingProdcut == null)
                    {
                        lstProduct.Add(product);
                        stockPrice.ProductName = product.ProductName;
                        decimal price = 0;
                        if (workbook.Worksheets[0].Rows[i].Cells[rateIndex].Value != null)
                        {
                            decimal.TryParse(workbook.Worksheets[0].Rows[i].Cells[rateIndex].Value.ToString().Trim(), out price);
                        }

                        stockPrice.Price = price;
                        lstStockPrice.Add(stockPrice);

                    }

                    if (existingProdcut == null && quantity > 0)
                    {
                        lstReceiveProductDetail.Add(receiveDetail);
                    }
                    else
                    {
                        lstReceiveProductDetailWithZeroQty.Add(receiveDetail);
                    }


                    ProductType existing = lstProductType.Where(t => t.TypeName.ToLower().Trim() == productType.TypeName.ToLower().Trim()).FirstOrDefault();
                    if (existing == null && !string.IsNullOrEmpty(productType.TypeName))
                    {
                        lstProductType.Add(productType);
                    }

                    ProductSize pSize = lstProductSize.Where(s => s.Name.ToLower().Trim() == productSize.Name.ToLower().Trim()).FirstOrDefault();
                    if (pSize == null && !string.IsNullOrEmpty(productSize.Name))
                    {
                        lstProductSize.Add(productSize);
                    }

                    ProductModel pModel = lstProductBrand.Where(m => m.Name.ToLower().Trim() == productBrand.Name.ToLower().Trim()).FirstOrDefault();
                    if (pModel == null && !string.IsNullOrEmpty(productBrand.Name))
                    {
                        lstProductBrand.Add(productBrand);
                    }

                    ProductColor pOrgin = lstProductOrigin.Where(m => m.Name.ToLower().Trim() == productOrigin.Name.ToLower().Trim()).FirstOrDefault();
                    if (pOrgin == null && !string.IsNullOrEmpty(productOrigin.Name))
                    {
                        lstProductOrigin.Add(productOrigin);
                    }

                    //Unit exUnit = lstUnit.Where(m => m.UnitName.ToLower().Trim() == unit.UnitName.ToLower().Trim()).FirstOrDefault();
                    //if (exUnit == null && !string.IsNullOrEmpty(unit.UnitName))
                    //{
                    //    lstUnit.Add(unit);
                    //}

                }
            }

            // document.Close();

            vmProductImport.lstProduct = lstProduct;
            vmProductImport.lstProductType = lstProductType;
            vmProductImport.lstProductSize = lstProductSize;
            vmProductImport.lstProductModel = lstProductBrand;
            vmProductImport.lstReceiveProductDetail = lstReceiveProductDetail;
            vmProductImport.lstStockPrice = lstStockPrice;
            //   vmProductImport.lstProducOrigin = lstProductOrigin;
            //  vmProductImport.lstUnit = lstUnit;

            result = new ReceiveProductManager().ImportProduct(vmProductImport);

            return result;
        }

    }
}
