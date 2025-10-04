using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IFMS.BLL;
using IMFS.Common.DTO;
using Infragistics.Win.UltraWinGrid;
using NybSys.MTBF.Utility.Helper;
using NybSys.MTBF.Utility.Enums;
using NybSys.MTBF.Utility.Constants;
using IMFS.Common.View;
using System.IO;
using Tiles_Inventory.Reports;
using IMFS.Common.Utility;

namespace Tiles_Inventory
{
    public partial class frmSalesFinishGoods : BaseForm
    {
        int _materialReceiveID = 0;

        public frmSalesFinishGoods()
        {
            InitializeComponent();
        }

        public frmSalesFinishGoods(int materialReceiveID, bool isEdit)
        {
            _materialReceiveID = materialReceiveID;
            IsUpdating = isEdit;
            InitializeComponent();
        }

        private void frmMaterialReceive_Load(object sender, EventArgs e)
        {
            grvProductDetails.DataSource = BuildMaterialTable();
            grvProductDetails.DisplayLayout.Bands[0].Columns["ProductID"].Hidden = true;
            LoadSalesPartyCombo();
            LoadProductInformationCombo();
            if (IsUpdating)
            {
                LoadExistingStockInformation();
                LoadExistingStockDetailInformation();
            }
        }

        private void LoadSalesPartyCombo()
        {
            DataTable materialsTable = new DataTable();
            materialsTable.Columns.Add("PartyID");
            materialsTable.Columns.Add("Party Name");

            DataRow emptyrow = materialsTable.NewRow();
            emptyrow["PartyID"] = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            emptyrow["Party Name"] = MTBFConstants.DataField.COMBO_DEFAULT_NAME;
            materialsTable.Rows.Add(emptyrow);

            foreach (SalesParty material in new StockSalesManager().GetAllSalesParty())
            {
                DataRow row = materialsTable.NewRow();
                row["PartyID"] = material.SalesPartyID;
                row["Party Name"] = material.PartyName;
                materialsTable.Rows.Add(row);
            }
            cmbPartyName.DataSource = materialsTable;
            cmbPartyName.DisplayMember = "Party Name";
            cmbPartyName.ValueMember = "PartyID";
            cmbPartyName.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
        }


        private void LoadExistingStockDetailInformation()
        {
            foreach (StockSalesDetails dDetail in new StockSalesManager().GetAllStockSalesDetailByStockSalesID(_materialReceiveID))
            {
                UltraGridRow row = grvProductDetails.DisplayLayout.Bands[0].AddNew();
                Product material = new ProductManager().GetProductByID(dDetail.ProductID);
                row.Cells["ProductID"].Value = dDetail.ProductID;
                row.Cells["Product Name"].Value = (material != null) ? material.ProductName : string.Empty;
                row.Cells["Quantity"].Value = dDetail.Quantity;
                row.Cells["Price"].Value = dDetail.Price;
                row.Cells["Total"].Value = dDetail.Total;
                row.Cells["Delete"].Value = "Delete";
            }
        }

        private void LoadExistingStockInformation()
        {
            StockSales stock = new StockSalesManager().GetStockSalesByID(_materialReceiveID);
            if (stock != null)
            {
                dtpSalesDate.Value = stock.SalesDate;
                cmbPartyName.Value = stock.PartyID;
                txtTotal.Text = stock.Total.ToString();
                txtReceiveAmount.Text = stock.ReceiveAmount.ToString();
            }
        }

        private void LoadProductInformationCombo()
        {
            DataTable productTable = new DataTable();
            productTable.Columns.Add("ProductID");
            productTable.Columns.Add("Product Name");
            productTable.Columns.Add("Type");

            List<Product> lstProductInformation = new List<Product>();
            lstProductInformation = new ProductManager().GetAllProduct().Cast<Product>().Where(p => p.BranchID == MTBFConstants.AppConstants.BranchID && p.OrganizationID == MTBFConstants.AppConstants.OrganizationID).ToList();

            foreach (Product product in lstProductInformation)
            {
                DataRow row = productTable.NewRow();
                row["ProductID"] = product.ProductID;
                row["Product Name"] = product.ProductName;
                row["Type"] = product.TypeName;
                productTable.Rows.Add(row);
            }

            cmbProductInformation.DisplayMember = "Product Name";
            cmbProductInformation.ValueMember = "ProductID";
            cmbProductInformation.DataSource = productTable;
            cmbProductInformation.DisplayLayout.Bands[0].Columns["ProductID"].Hidden = true;
        }


        private DataTable BuildMaterialTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ProductID");
            table.Columns.Add("Product Name");
            table.Columns.Add("Quantity");
            table.Columns.Add("Price");
            table.Columns.Add("Total");
            table.Columns.Add("Delete");
            return table;

        }


        private void btAdd_Click(object sender, EventArgs e)
        {
            if (ValidAddData())
            {
                AddDataInGrid();
                CalculateTotalAmount();
                cmbProductInformation.Focus();
            }

        }


        private void CalculateTotalAmount()
        {
            decimal total = 0;


            foreach (UltraGridRow row in grvProductDetails.Rows)
            {
                total = total + Convert.ToDecimal(row.Cells["Total"].Value);
            }

            txtTotal.Text = total.ToString("F2");
        }


        private bool ValidAddData()
        {
            int meterialID = 0;


            if (cmbProductInformation.Value != null)
            {
                int.TryParse(cmbProductInformation.Value.ToString(), out meterialID);
                Materials material = new MaterialManager().GetMeterialsByID(meterialID);

                if (material == null)
                {
                    MessageBoxHelper.ShowInformation("Invalid material informaiton.");
                    cmbProductInformation.Focus();
                }

            }

            decimal quantity = 0;
            decimal price = 0;
            decimal.TryParse(txtQuantity.Text, out quantity);
            decimal.TryParse(txtPrice.Text, out price);

            if (quantity == 0)
            {
                MessageBoxHelper.ShowInformation("You need to enter quantity.");
                txtQuantity.Focus();
            }

            if (price == 0)
            {
                MessageBoxHelper.ShowInformation("You need to enter price.");
                txtPrice.Focus();
            }

            return true;

        }


        private void AddDataInGrid()
        {
            UltraGridRow row = grvProductDetails.DisplayLayout.Bands[0].AddNew();
            decimal price = Convert.ToDecimal(txtPrice.Text);
            row.Cells["ProductID"].Value = cmbProductInformation.Value.ToString();
            row.Cells["Product Name"].Value = cmbProductInformation.Text;
            row.Cells["Quantity"].Value = Convert.ToDecimal(txtQuantity.Value);
            row.Cells["Price"].Value = price;
            row.Cells["Total"].Value = Convert.ToDecimal(txtQuantity.Value) * price;
            row.Cells["Delete"].Value = "Delete";
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (ValidFormData())
            {
                if (IsUpdating)
                {
                    if (UpdateStockSalesInformation() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Stock information saved successfully.");
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save distribution information.");
                    }

                }
                else
                {
                    int salesID = InsertStockSalesInformation();

                    if (salesID > 0)
                    {
                        MessageBoxHelper.ShowInformation("Stock information saved successfully.");
                        PrintInvoice(salesID);
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save distribution information.");
                    }
                }
            }
        }

        private DataTable BuildReportTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("SalesID");
            table.Columns.Add("Party Name");
            table.Columns.Add("Phone");
            table.Columns.Add("Address");
            table.Columns.Add("Sales Date");
            table.Columns.Add("Product Name");
            table.Columns.Add("Quantity");
            table.Columns.Add("Price");
            table.Columns.Add("Total");
            table.Columns.Add("Sales Amount");
            table.Columns.Add("Receive Amount");
            table.Columns.Add("Due");
            return table;
        }

        private void PrintInvoice(int salesId)
        {
            string pharmacyName = string.Empty;
            string pharmacyAddress = string.Empty;
            Employee employee = new Employee();
            DataTable salesReport = new DataTable();
            Organization pharmacy = new Organization();
            salesReport = SetReportData(salesId);

            rptPartySalesReport op = new rptPartySalesReport();
            frmSalesReport objmainReport = new frmSalesReport();
            op.DataSource = salesReport;
            objmainReport.rptViewer.Document = op.Document;
            objmainReport.rptViewer.Dock = DockStyle.Fill;

            SetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress, ref pharmacy);

            op.txtPharmacyName.Text = pharmacyName;
            op.txtAddress.Text = pharmacyAddress;
            employee = new EmployeeManager().GetEmployeeByID(IFMSConstant.LoggedinUserID);
            if (pharmacy.OrganizationLogo != null)
            {
                MemoryStream ms = new MemoryStream(pharmacy.OrganizationLogo);
                Image returnImage = Image.FromStream(ms);
                op.picLogo.Image = returnImage;
            }

            op.txtServiceMan.Text = (employee != null) ? employee.EmployeeName : "Super Admin";
            op.Run();
            objmainReport.MdiParent = this.MdiParent;
            objmainReport.Show();

        }

        private DataTable SetReportData(int salesId)
        {

            DataTable table = BuildReportTable();

            StockSales stockSales = new StockSalesManager().GetStockSalesByID(salesId);
            SalesParty party = new StockSalesManager().GetSalesPartyByID(stockSales.PartyID);
            foreach (StockSalesDetails sDetail in new StockSalesManager().GetAllStockSalesDetailByStockSalesID(salesId))
            {
                Product product = new ProductManager().GetProductByID(sDetail.ProductID);
                DataRow row = table.NewRow();
                row["SalesID"] = stockSales.StockSalesID;
                row["Party Name"] = party.PartyName;
                row["Phone"] = party.Phone;
                row["Address"] = party.Address;
                row["Sales Date"] = stockSales.SalesDate.ToString("dd/MM/yyyy");
                row["Product Name"] = product.ProductName;
                row["Quantity"] = sDetail.Quantity;
                row["Price"] = sDetail.Price;
                row["Total"] = sDetail.Total;
                row["Sales Amount"] = stockSales.Total;
                row["Receive Amount"] = stockSales.ReceiveAmount;
                row["Due"] = stockSales.Total - stockSales.ReceiveAmount;
                table.Rows.Add(row);
            }

            return table;

        }

        private void SetPharmachyInforamation(ref string PharmacyName, ref string Address, ref Organization pharmacy)
        {
            pharmacy = new PharmacyManager().GetAllPharmacy().Cast<Organization>().ToList().FirstOrDefault();
            Branch branch = new BranchManager().GetBranchByID(MTBFConstants.AppConstants.BranchID);
            if (pharmacy != null)
            {
                PharmacyName = branch.BranchName;
                Address = branch.Address + ", " + branch.Phone + ", " + pharmacy.Fax;
            }
        }




        private int InsertStockSalesInformation()
        {
            int result = 0;
            List<StockSalesDetails> lstStockDetail = new List<StockSalesDetails>();
            StockSales stock = new StockSales();
            decimal receiveAmount = 0;
            decimal.TryParse(txtReceiveAmount.Text, out receiveAmount);

            stock.SalesDate = dtpSalesDate.Value;
            stock.Total = Convert.ToDecimal(txtTotal.Text);
            stock.PartyID = Convert.ToInt32(cmbPartyName.Value);
            stock.ReceiveAmount = receiveAmount;
            stock.BranchID = MTBFConstants.AppConstants.BranchID;
            stock.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            lstStockDetail = GetAllStockDetailInformation();

            result = new StockSalesManager().InsertStockSales(stock, lstStockDetail);
            return result;
        }


        private int UpdateStockSalesInformation()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            List<StockSalesDetails> lstStockDetail = new List<StockSalesDetails>();
            StockSales stock = new StockSalesManager().GetStockSalesByID(_materialReceiveID);
            decimal receiveAmount = 0;
            decimal.TryParse(txtReceiveAmount.Text, out receiveAmount);
            stock.Total = Convert.ToDecimal(txtTotal.Text);
            stock.SalesDate = dtpSalesDate.Value;
            stock.PartyID = Convert.ToInt32(cmbPartyName.Value);
            stock.ReceiveAmount = receiveAmount;
            stock.BranchID = MTBFConstants.AppConstants.BranchID;
            stock.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            lstStockDetail = GetAllStockDetailInformation();

            result = new StockSalesManager().UpdateStockSales(stock, lstStockDetail);
            return result;
        }

        private List<StockSalesDetails> GetAllStockDetailInformation()
        {
            List<StockSalesDetails> lstStockDetail = new List<StockSalesDetails>();

            foreach (UltraGridRow row in grvProductDetails.Rows)
            {
                StockSalesDetails distributionDetail = new StockSalesDetails();
                distributionDetail.ProductID = row.Cells["ProductID"].Value.ToString();
                distributionDetail.Quantity = Convert.ToDecimal(row.Cells["Quantity"].Text);
                distributionDetail.Price = Convert.ToDecimal(row.Cells["Price"].Value);
                distributionDetail.Total = Convert.ToDecimal(row.Cells["Total"].Text);
                lstStockDetail.Add(distributionDetail);
            }

            return lstStockDetail;
        }


        private bool ValidFormData()
        {
            if (grvProductDetails.Rows.Count == 0)
            {
                MessageBoxHelper.ShowInformation("You need to add data in grid.");
                return false;
            }

            return true;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btAddPUnit_Click(object sender, EventArgs e)
        {

        }

        private void grvProductDetails_ClickCell(object sender, ClickCellEventArgs e)
        {
            if (e.Cell.Column.Header.Caption == "Delete")
            {
                grvProductDetails.Rows[e.Cell.Row.Index].Delete();
                CalculateTotalAmount();
            }
        }

        private void cmbProductInformation_Leave(object sender, EventArgs e)
        {
            txtPrice.Focus();
        }



    }
}
