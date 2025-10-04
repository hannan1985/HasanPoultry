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
using NybSys.MTBF.Utility.Helper;
using NybSys.MTBF.Utility.Constants;
using IMFS.Common.View;
using IFMS.BLL;
using Infragistics.Win.UltraWinGrid;
using System.IO;
using Infragistics.Excel;

namespace Tiles_Inventory
{
    public partial class frmProductSalesHistory : BaseForm
    {
        List<Customer> lstCustomer = new List<Customer>();
        public frmProductSalesHistory()
        {
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        private void frmProductSalesHistory_Load(object sender, EventArgs e)
        {
            LoadCustomerInformation();
            LoadProductInformation();
            grvCreditProductDetails.DataSource = BuildProductSalesHistoryTable();
        }


        private void LoadCustomerInformation()
        {
            DataTable table = new DataTable();

            table.Columns.Add("ID");
            table.Columns.Add("Name");
            table.Columns.Add("Address");
            table.Columns.Add("Phone");

            Customer defcustomer = new Customer();
            defcustomer.CustomerID = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            defcustomer.CustomerName = MTBFConstants.DataField.COMBO_DEFULT_TASK_TYPE;

            lstCustomer = new CustomerManager().GetAllCustomerByBranchID(MTBFConstants.AppConstants.BranchID).ToList();
            lstCustomer.Insert(0, defcustomer);
            foreach (Customer customer in lstCustomer)
            {
                DataRow row = table.NewRow();
                row["ID"] = customer.CustomerID;
                row["Name"] = customer.CustomerName;
                row["Address"] = customer.Address;
                row["Phone"] = customer.Phone;
                table.Rows.Add(row);
            }

            cmbCustomerName.DisplayMember = "Name";
            cmbCustomerName.ValueMember = "ID";
            cmbCustomerName.DataSource = table;
            cmbCustomerName.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            cmbCustomerName.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
            cmbCustomerName.DisplayLayout.PerformAutoResizeColumns(false, PerformAutoSizeType.AllRowsInBand, true);

        }

        /// <summary>
        /// Method to load product information combo.
        /// </summary>
        private void LoadProductInformation()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Product ID");
            table.Columns.Add("Product Name");

            foreach (Product product in DataAccessProxy.GetAllProduct().Cast<Product>().Where(p => p.Discountinued == false))
            {
                DataRow row = table.NewRow();
                row["Product ID"] = product.ProductID;
                row["Product Name"] = product.ProductName;
                table.Rows.Add(row);
            }
            cmbProductInformation.DataSource = table;
            cmbProductInformation.DisplayMember = "Product Name";
            cmbProductInformation.ValueMember = "Product ID";
            cmbProductInformation.DisplayLayout.Bands[0].Columns["Product ID"].Hidden = true;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (cmbProductInformation.Value != null)
            {
                LoadProductSalesHisotry();
            }
            else
            {
                MessageBoxHelper.ShowInformation("Plese select a product name.");
            }
        }

        private DataTable BuildProductSalesHistoryTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Customer Name");
            table.Columns.Add("Sales Number");
            table.Columns.Add("Bill Number");
            table.Columns.Add("Sales Date");
            table.Columns.Add("Product Name");
            table.Columns.Add("Quantity");
            return table;
        }

        private void LoadProductSalesHisotry()
        {
            int customerID = 0;
            DataTable productHistoryTable = BuildProductSalesHistoryTable();
            decimal totalQty = 0;
            decimal totalSquareFeet = 0;
            int.TryParse(cmbCustomerName.Value.ToString(), out customerID);

            List<VMProductSalesHistory> lstSalesHistory = new List<VMProductSalesHistory>();
            lstSalesHistory = new SalesManager().GetProductSalesHistoryByProductAndBranchID(cmbProductInformation.Value.ToString().Trim(), MTBFConstants.AppConstants.BranchID);

            lstSalesHistory = lstSalesHistory.Where(v => v.SalesDate >= dtpFromDate.Value.AddDays(-1) && v.SalesDate <= dtpToDate.Value).ToList();

            if (customerID > 0)
            {
                lstSalesHistory = lstSalesHistory.Where(s => s.CustomerID == customerID).ToList();
            }

            foreach (VMProductSalesHistory salesOrderDetail in lstSalesHistory)
            {
                Customer customer = lstCustomer.Where(c => c.CustomerID == salesOrderDetail.CustomerID).FirstOrDefault();

                DataRow row = productHistoryTable.NewRow();
                row["Customer Name"] = (customer != null) ? customer.CustomerName : string.Empty;
                row["Sales Number"] = salesOrderDetail.SalesID;
                row["Bill Number"] = salesOrderDetail.BillNumber;
                row["Sales Date"] = salesOrderDetail.SalesDate.ToShortDateString();
                row["Product Name"] = salesOrderDetail.ProductName;
                row["Quantity"] = salesOrderDetail.Quantity;
                productHistoryTable.Rows.Add(row);
                totalQty = totalQty + salesOrderDetail.Quantity;

            }
            grvCreditProductDetails.DataSource = productHistoryTable;
            txtTotalQuantity.Text = totalQty.ToString();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportGridData();
        }


        private void ExportGridData()
        {
            Branch branch = new BranchManager().GetBranchByID(MTBFConstants.AppConstants.BranchID);
            try
            {
                if (grvCreditProductDetails.DataSource == null)
                {
                    MessageBoxHelper.ShowInformation("No information foud for export.");
                }
                else
                {
                    string fileLoacation = "";
                    FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                    DialogResult result = folderBrowser.ShowDialog();

                    try
                    {
                        if (result == DialogResult.OK)
                        {
                            string nameOfFile = cmbCustomerName.Text + " Sales" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                            fileLoacation = string.Format("{0}\\{1}", folderBrowser.SelectedPath, nameOfFile);

                            // Check if file already exists. If yes, delete it. 
                            if (File.Exists(fileLoacation))
                            {
                                File.Delete(fileLoacation);
                            }

                            Workbook WB = new Workbook();
                            WB.Worksheets.Add("Product Sales Report");

                            // add Title
                            Worksheet sheet = WB.Worksheets["Product Sales Report"];
                            sheet.Rows[0].Cells[0].Value = (branch != null) ? branch.BranchName : string.Empty;
                            sheet.Rows[0].Cells[0].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;

                            sheet.Rows[1].Cells[0].Value = cmbCustomerName.Text + " Sales";
                            sheet.Rows[1].Cells[0].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;

                            sheet.Rows[2].Cells[0].Value = "From :";
                            sheet.Rows[2].Cells[0].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
                            sheet.Rows[2].Cells[1].Value = dtpFromDate.Value.ToString("dd MMM yyyy");
                            sheet.Rows[2].Cells[1].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
                            sheet.Rows[3].Cells[0].Value = "To :";
                            sheet.Rows[3].Cells[0].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
                            sheet.Rows[3].Cells[1].Value = dtpToDate.Value.ToString("dd MMM yyyy");
                            sheet.Rows[3].Cells[1].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;

                            ultraGridExcelExporter1.Export(grvCreditProductDetails, WB.Worksheets["Product Sales Report"], 6, 0);
                            int count = 8;
                            //decimal total = 0;
                            //foreach (UltraGridRow row in grvSales.Rows)
                            //{
                            //    count++;
                            //    total = total + (Convert.ToDecimal(row.Cells["Sales Amount"].Value) + Convert.ToDecimal(row.Cells["O. Charge"].Value) + Convert.ToDecimal(row.Cells["C. Charge"].Value) + Convert.ToDecimal(row.Cells["C.L.Charge"].Value) - Convert.ToDecimal(row.Cells["Discount"].Value));
                            //}
                            //int total = Convert.ToInt32(lblTotalRecord.Text) + 8;
                            //sheet.Rows[total].Cells[4].Value = "Total :";
                            //sheet.Rows[total].Cells[4].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;

                            //sheet.Rows[total].Cells[5].ApplyFormula("=sum(F8:" + "F" + total + ")");
                            //sheet.Rows[total].Cells[5].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;


                            BIFF8Writer.WriteWorkbookToFile(WB, fileLoacation);
                            WB.Save(fileLoacation);

                            MessageBoxHelper.ShowInformation("Exported Successfully");
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBoxHelper.ShowInformation(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxHelper.ShowInformation(ex.Message);
            }
        }

    }
}
