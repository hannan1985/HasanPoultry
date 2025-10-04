using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NybSys.MTBF.Utility.Constants;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Helper;

using IMFS.Common.Utility;
using IFMS.Facade;
using Tiles_Inventory.Reports;
using IFMS.BLL;

namespace Tiles_Inventory
{
    public partial class frmViewFinishGoodsReceive : BaseForm
    {

        private List<Stock> lstStock = new List<Stock>();

        public frmViewFinishGoodsReceive()
        {
            DataAccessProxy = new FacadeManager();
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadMaterialDistributionByPUnitAndDate(Convert.ToInt32(cmbProductionUnit.Value), dtpFromDate.Value.ToString("yyyy/MM/dd"), dtpToDate.Value.ToString("yyyy/MM/dd"));
        }

        private DataTable BuildTransferTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("StockID");
            table.Columns.Add("Receive Date");
            table.Columns.Add("Supplier Name");
            table.Columns.Add("Total");
            return table;

        }

        private DataTable BuildTransferDetailTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Product Name");
            table.Columns.Add("Quantity");
            table.Columns.Add("Price");
            table.Columns.Add("Total");
            return table;

        }

        private void LoadMaterialDistributionByPUnitAndDate(int unitID, string fromDate, string toDate)
        {
            fromDate = fromDate + MTBFConstants.DAY_START_TIME;
            toDate = toDate + MTBFConstants.DAY_END_TIME;
            DataTable transferTable = BuildTransferTable();
            string filter = string.Format("{0} between '{1}' and '{2}' and {3}={4}", "StockDate", fromDate, toDate, "ProductionUnitID", unitID);
            lstStock = new StockManager().GetFilteredStock(filter).Where(t => t.BranchID == MTBFConstants.AppConstants.BranchID && t.OrganizationID == MTBFConstants.AppConstants.OrganizationID).Cast<Stock>().ToList();
            foreach (Stock transfer in lstStock)
            {
                ProductionUnit pUnit = new ProductionUnitManager().GetProductionUnitByID(transfer.ProductionUnitID);
                DataRow row = transferTable.NewRow();
                row["StockID"] = transfer.StockID;
                row["Receive Date"] = transfer.StockDate.ToShortDateString();
                row["Supplier Name"] = (pUnit != null) ? pUnit.UnitName : string.Empty;
                row["Total"] = transfer.Total;
                transferTable.Rows.Add(row);
            }
            grvTransferInfo.DataSource = transferTable;
            if (transferTable.Rows.Count == 0)
            {
                MessageBoxHelper.ShowInformation("No data found for this combination");
            }
        }


        private void LoadTransferDetailInformation(int stockID)
        {

            DataTable transferDetailTable = BuildTransferDetailTable();
            foreach (StockDetails distribution in new StockManager().GetAllStockDetailByStockID(stockID))
            {
                Product product = new ProductManager().GetProductByID(distribution.ProductID);
                DataRow row = transferDetailTable.NewRow();
                row["Product Name"] = (product != null) ? product.ProductName : string.Empty;
                row["Quantity"] = distribution.Quantity;
                row["Price"] = distribution.Price;
                row["Price"] = distribution.Price;
                row["Total"] = distribution.Total;

                transferDetailTable.Rows.Add(row);
            }

            grvTransferDetailInfo.DataSource = transferDetailTable;
        }

        private void frmViewTransferInformation_Load(object sender, EventArgs e)
        {
            LoadProductionUnitCombo();
            grvTransferInfo.DataSource = BuildTransferTable();
            grvTransferDetailInfo.DataSource = BuildTransferDetailTable();
        }

        private void LoadProductionUnitCombo()
        {
            DataTable MaterialsTable = new DataTable();
            MaterialsTable.Columns.Add("UnitID");
            MaterialsTable.Columns.Add("Unit Name");
            foreach (ProductionUnit material in new ProductionUnitManager().GetAllProductionUnit())
            {
                DataRow row = MaterialsTable.NewRow();
                row["UnitID"] = material.UnitID;
                row["Unit Name"] = material.UnitName;
                MaterialsTable.Rows.Add(row);
            }
            cmbProductionUnit.DataSource = MaterialsTable;
            cmbProductionUnit.DisplayMember = "Unit Name";
            cmbProductionUnit.ValueMember = "UnitID";
        }


        private void btnDistribute_Click(object sender, EventArgs e)
        {
            frmFinishStockReceive frm = new frmFinishStockReceive();
            frm.ShowDialog();
        }

        private void grvTransferInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grvTransferInfo.SelectedRows.Count > 0)
            {
                int distributionID = Convert.ToInt32(grvTransferInfo.SelectedRows[0].Cells["StockID"].Value);
                LoadTransferDetailInformation(distributionID);
            }
        }

        private void grvTransferInfo_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (grvTransferInfo.SelectedRows.Count > 0 && !grvTransferInfo.SelectedRows[0].IsNewRow)
            {
                int distributionID = Convert.ToInt32(grvTransferInfo.SelectedRows[0].Cells["StockID"].Value);
                LoadTransferDetailInformation(distributionID);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (grvTransferInfo.SelectedRows.Count > 0 && !grvTransferInfo.SelectedRows[0].IsNewRow)
            {
                int transferID = Convert.ToInt32(grvTransferInfo.SelectedRows[0].Cells["StockID"].Value);
                frmFinishStockReceive frm = new frmFinishStockReceive(transferID, true);
                frm.ShowDialog();
            }
            else
            {
                MessageBoxHelper.ShowInformation("You need to select a record.");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (cmbProductionUnit.Value != null)
                LoadMaterialDistributionByPUnitAndDate(Convert.ToInt32(cmbProductionUnit.Value), dtpFromDate.Value.ToString("yyyy/MM/dd"), dtpToDate.Value.ToString("yyyy/MM/dd"));

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (grvTransferInfo.SelectedRows.Count > 0 && !grvTransferInfo.SelectedRows[0].IsNewRow)
            {

                PrintTrnasferInformation();
            }
            else
            {
                MessageBoxHelper.ShowInformation("You need to select a record.");
            }
        }

        private DataTable BuildReportTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Receive Date");
            table.Columns.Add("Product Name");
            table.Columns.Add("Quantity");
            table.Columns.Add("Price");
            table.Columns.Add("Total");
            return table;
        }


        /// <summary>
        /// Method to Get pharmacy information 
        /// </summary>
        /// <param name="PharmacyName"></param>
        /// <param name="Address"></param>
        private void GetPharmachyInforamation(ref string PharmacyName, ref string Address)
        {
            Branch pharmacy = new BranchManager().GetBranchByID(MTBFConstants.AppConstants.BranchID);
            if (pharmacy != null)
            {
                PharmacyName = pharmacy.BranchName;
                Address = pharmacy.Address + ", " + pharmacy.Phone + ", " + pharmacy.Fax;
            }
        }

        private void PrintTrnasferInformation()
        {
            string pharmacyName = string.Empty;
            string pharmacyAddress = string.Empty;

            DataTable transferTable = BuildReportTable();

            ProductionUnit pUnit = new ProductionUnitManager().GetProductionUnitByID(lstStock[0].ProductionUnitID);

            foreach (Stock stock in lstStock)
            {
                foreach (StockDetails tDetail in new StockManager().GetAllStockDetailByStockID(stock.StockID))
                {
                    Product product = new ProductManager().GetProductByID(tDetail.ProductID);

                    DataRow row = transferTable.NewRow();
                    row["Receive Date"] = stock.StockDate.ToString("dd/MM/yyyy");
                    row["Product Name"] = product.ProductName;
                    row["Quantity"] = tDetail.Quantity;
                    row["Price"] = tDetail.Price;
                    row["Total"] = tDetail.Total;
                    transferTable.Rows.Add(row);
                }
            }

            rptStockReceive op = new rptStockReceive();
            frmSalesReport objmainReport = new frmSalesReport();
            op.DataSource = transferTable;
            objmainReport.rptViewer.Document = op.Document;
            objmainReport.rptViewer.Dock = DockStyle.Fill;
            op.txtFromDate.Text = dtpFromDate.Value.ToString("dd/MM/yyyy");
            op.txtToDate.Text = dtpToDate.Value.ToString("dd/MM/yyyy");
            GetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
            op.txtPharmacyName.Text = pharmacyName;
            op.txtAddress.Text = pharmacyAddress;
            op.txtUnitName.Text = pUnit.UnitName;
            op.txtResponsible.Text = pUnit.Responsible;

            op.Run();
            objmainReport.MdiParent = this.MdiParent;
            objmainReport.Show();

        }



    }
}