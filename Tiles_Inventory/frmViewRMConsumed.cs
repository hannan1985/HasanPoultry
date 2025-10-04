using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Helper;
using IFMS.Facade;
using NybSys.MTBF.Utility.Constants;
using IFMS.BLL;
using Tiles_Inventory.Reports;
using IMFS.Common.View;
using NybSys.MTBF.Utility.Enums;

namespace Tiles_Inventory
{
    public partial class frmViewRMConsumed : BaseForm
    {
        private List<VMProductUsed> lstVMProductUsed = new List<VMProductUsed>();
        public frmViewRMConsumed()
        {
            DataAccessProxy = new FacadeManager();
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadDamageInformation();
        }

        private void btnDistribute_Click(object sender, EventArgs e)
        {
            frmProductUsed frm = new frmProductUsed();
            frm.ShowDialog();
        }

        private DataTable BuildDamageTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ProductUsedID");
            table.Columns.Add("Use Date");
            table.Columns.Add("Party Name");
            return table;
        }


        private DataTable BuildDamageDetailTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Product Name");
            table.Columns.Add("Quantity");
            //  table.Columns.Add("Square Feet");
            return table;
        }

        private void LoadDamageInformation()
        {
            DataTable damageTable = BuildDamageTable();
            string fromDate = dtpFromDate.Value.ToString("yyyy/MM/dd") + MTBFConstants.DAY_START_TIME;
            string toDate = dtpToDate.Value.ToString("yyyy/MM/dd") + MTBFConstants.DAY_END_TIME;
            string filter = string.Format("{0} between '{1}' and '{2}'", "UsedDate", fromDate, toDate);
            lstVMProductUsed = new ProductUsedManager().GetFilteredProductUsed(filter);
            List<SalesCenter> lstSalesCenter = new List<SalesCenter>();
            lstSalesCenter = new SalesCenterManager().GetAllSalesCenter().Where(s => s.BranchID == MTBFConstants.AppConstants.BranchID && s.OrganizationID == MTBFConstants.AppConstants.OrganizationID).ToList();


            foreach (VMProductUsed vmProductUsed in lstVMProductUsed)
            {
                SalesCenter salesCenter = lstSalesCenter.Where(s => s.SalesCenterID == vmProductUsed.ProductUsed.PartyID).FirstOrDefault();
                DataRow row = damageTable.NewRow();
                row["ProductUsedID"] = vmProductUsed.ProductUsed.ProductUsedID;
                row["Use Date"] = vmProductUsed.ProductUsed.UsedDate.ToString("dd/MM/yyyy");
                row["Party Name"] = (salesCenter != null) ? salesCenter.SalesCenterName : string.Empty;
                damageTable.Rows.Add(row);
            }

            grvTransferInfo.DataSource = damageTable;
            if (grvTransferInfo.Rows.Count == 0)
            {
                MessageBoxHelper.ShowInformation("No data found for this combination");
            }
        }

        private void grvTransferInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void LoadDamageInformationDetail(int damageInfoID)
        {
            List<ProductUsedDetail> lstProductUsedDetail = new List<ProductUsedDetail>();

            VMProductUsed vmProductUsed = new VMProductUsed();
            vmProductUsed = lstVMProductUsed.Where(c => c.ProductUsed.ProductUsedID == damageInfoID).FirstOrDefault();

            List<Product> lstProduct = new List<Product>();
            lstProduct = new ProductManager().GetAllProductByBranchAndOrganizationID(MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID);
            DataTable damageTable = BuildDamageDetailTable();
            if (vmProductUsed != null)
            {
                foreach (ProductUsedDetail damageDetail in vmProductUsed.lstProductUsedDetail)
                {
                    Product product = lstProduct.Where(p => p.ProductID == damageDetail.ProductID).FirstOrDefault();
                    DataRow row = damageTable.NewRow();
                    row["Product Name"] = (product != null) ? product.ProductName : string.Empty;
                    row["Quantity"] = damageDetail.Quantity;
                    damageTable.Rows.Add(row);
                }
            }


            grvTransferDetailInfo.DataSource = damageTable;
            if (damageTable.Rows.Count == 0)
            {
                MessageBoxHelper.ShowInformation("No data found for this combination");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (grvTransferInfo.Selected.Rows.Count > 0)
            {
                int damageInfoID = Convert.ToInt32(grvTransferInfo.Selected.Rows[0].Cells["ProductUsedID"].Value);
                VMProductUsed vmProductUsed = new VMProductUsed();
                vmProductUsed = lstVMProductUsed.Where(c => c.ProductUsed.ProductUsedID == damageInfoID).FirstOrDefault();
                frmProductUsed frm = new frmProductUsed(vmProductUsed,true);
                frm.ShowDialog();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDamageInformation();
        }

        private void PrintDamageReport()
        {
            string fromDate = dtpFromDate.Value.ToString("yyyy/MM/dd") + MTBFConstants.DAY_START_TIME;
            string toDate = dtpToDate.Value.ToString("yyyy/MM/dd") + MTBFConstants.DAY_END_TIME;
            List<DamageReport> lstDamageDetail = new List<DamageReport>();
            List<PurchaseOrderDetail> lstPurcahseOrder = new List<PurchaseOrderDetail>();

            lstDamageDetail = new DamageManager().GetDamageReport(fromDate, toDate, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).OrderBy(d => d.DamageDate).ToList();

            List<StockPrice> lstStockPrice = new List<StockPrice>();

            string[] productIDs = lstDamageDetail.Select(i => i.ProductCode).Distinct().ToArray();
            string filter = string.Empty;
            if (productIDs.Length > 0)
            {
                for (int i = 0; i < productIDs.Length; i++)
                {
                    if (filter != string.Empty) filter = filter + ",";
                    filter = filter + "'" + productIDs[i] + "'";
                }
                filter = String.Format("{0} IN ({1})", "ProductID", filter);
                lstStockPrice = new StockPriceManager().GetFilteredStockPrice(filter).Cast<StockPrice>().ToList();
                lstPurcahseOrder = DataAccessProxy.GetPurcahseOrderDetailFiltered(filter).Cast<PurchaseOrderDetail>().ToList();
            }

            DataTable dt = BuildDamageReportTable();
            foreach (DamageReport dDetail in lstDamageDetail)
            {
                StockPrice stockPrice = lstStockPrice.Where(s => s.ProductID == dDetail.ProductCode).FirstOrDefault();
                PurchaseOrderDetail PDetail = lstPurcahseOrder.Where(p => p.ProductID == dDetail.ProductCode).Cast<PurchaseOrderDetail>().FirstOrDefault();

                decimal purchasePrice = (PDetail != null) ? PDetail.PurchasePrice : 0;

                DataRow row = dt.NewRow();
                row["Date"] = dDetail.DamageDate.ToString("dd-MMM-yyyy");
                row["Reason"] = dDetail.DamageReason;
                row["Product Name"] = dDetail.ProductName;
                row["Quantity"] = dDetail.Quantity;
                row["Price"] = (stockPrice != null) ? stockPrice.Price : purchasePrice;
                row["Total"] = (stockPrice != null) ? (stockPrice.Price * dDetail.Quantity).ToString("F2") : (dDetail.Quantity * purchasePrice).ToString("F2");
                dt.Rows.Add(row);
            }

            string pharmacyName = string.Empty;
            string pharmacyAddress = string.Empty;
            Organization pharmacy = new Organization();
            rptDamageReport op = new rptDamageReport();
            frmSalesReport frm = new frmSalesReport();
            op.DataSource = dt;
            frm.rptViewer.Document = op.Document;
            op.txtFromDate.Text = dtpFromDate.Value.ToString("dd/MM/yyyy");
            op.txtToDate.Text = dtpToDate.Value.ToString("dd/MM/yyyy");
            frm.rptViewer.Dock = DockStyle.Fill;
            SetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress, ref pharmacy);
            op.txtPharmacyName.Text = pharmacyName;
            op.txtAddress.Text = pharmacyAddress;
            frm.Text = "Damage Report";
            op.Run();
            frm.ShowDialog();
        }

        private void PrintSingleDamageReport()
        {
            string fromDate = dtpFromDate.Value.ToString("yyyy/MM/dd") + MTBFConstants.DAY_START_TIME;
            string toDate = dtpToDate.Value.ToString("yyyy/MM/dd") + MTBFConstants.DAY_END_TIME;
            List<DamageReport> lstDamageDetail = new List<DamageReport>();
            List<PurchaseOrderDetail> lstPurcahseOrder = new List<PurchaseOrderDetail>();

            int damageInfoID = Convert.ToInt32(grvTransferInfo.Selected.Rows[0].Cells["Damage Number"].Value);
            lstDamageDetail = new DamageManager().GetDamageReport(fromDate, toDate, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).OrderBy(d => d.DamageDate).ToList();
            if (damageInfoID > 0)
            {
                lstDamageDetail = lstDamageDetail.Where(d => d.DamageInfoID == damageInfoID).ToList();
            }
            List<StockPrice> lstStockPrice = new List<StockPrice>();

            string[] productIDs = lstDamageDetail.Select(i => i.ProductCode).Distinct().ToArray();
            string filter = string.Empty;
            if (productIDs.Length > 0)
            {
                for (int i = 0; i < productIDs.Length; i++)
                {
                    if (filter != string.Empty) filter = filter + ",";
                    filter = filter + "'" + productIDs[i] + "'";
                }
                filter = String.Format("{0} IN ({1})", "ProductID", filter);
                lstStockPrice = new StockPriceManager().GetFilteredStockPrice(filter).Cast<StockPrice>().ToList();
                lstPurcahseOrder = DataAccessProxy.GetPurcahseOrderDetailFiltered(filter).Cast<PurchaseOrderDetail>().ToList();
            }

            DataTable dt = BuildDamageReportTable();
            foreach (DamageReport dDetail in lstDamageDetail)
            {
                StockPrice stockPrice = lstStockPrice.Where(s => s.ProductID == dDetail.ProductCode).FirstOrDefault();
                PurchaseOrderDetail PDetail = lstPurcahseOrder.Where(p => p.ProductID == dDetail.ProductCode).Cast<PurchaseOrderDetail>().FirstOrDefault();

                decimal purchasePrice = (PDetail != null) ? PDetail.PurchasePrice : 0;

                DataRow row = dt.NewRow();
                row["Date"] = dDetail.DamageDate.ToString("dd-MMM-yyyy");
                row["Reason"] = dDetail.DamageReason;
                row["Product Name"] = dDetail.ProductName;
                row["Quantity"] = dDetail.Quantity;
                row["Price"] = (stockPrice != null) ? stockPrice.Price : purchasePrice;
                row["Total"] = (stockPrice != null) ? (stockPrice.Price * dDetail.Quantity).ToString("F2") : (dDetail.Quantity * purchasePrice).ToString("F2");
                dt.Rows.Add(row);
            }

            string pharmacyName = string.Empty;
            string pharmacyAddress = string.Empty;
            Organization pharmacy = new Organization();
            rptDamageReport op = new rptDamageReport();
            frmSalesReport frm = new frmSalesReport();
            op.DataSource = dt;
            frm.rptViewer.Document = op.Document;
            op.txtFromDate.Text = dtpFromDate.Value.ToString("dd/MM/yyyy");
            op.txtToDate.Text = dtpToDate.Value.ToString("dd/MM/yyyy");
            frm.rptViewer.Dock = DockStyle.Fill;
            SetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress, ref pharmacy);
            op.txtPharmacyName.Text = pharmacyName;
            op.txtAddress.Text = pharmacyAddress;
            frm.Text = "Damage Report";
            op.Run();
            frm.ShowDialog();
        }

        private void SetPharmachyInforamation(ref string PharmacyName, ref string Address, ref Organization pharmacy)
        {
            pharmacy = DataAccessProxy.GetAllPharmacy().Cast<Organization>().ToList().FirstOrDefault();
            Branch branch = DataAccessProxy.GetBracnhByID(MTBFConstants.AppConstants.BranchID);
            if (branch != null)
            {
                PharmacyName = branch.BranchName;
                Address = branch.Address + ", " + branch.Phone + ", " + branch.Fax;
            }
        }

        private DataTable BuildDamageReportTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Date");
            dt.Columns.Add("Reason");
            dt.Columns.Add("Product Name");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Price");
            dt.Columns.Add("Total");
            return dt;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (grvTransferInfo.Selected.Rows.Count > 0)
            {
                int damageInfoID = Convert.ToInt32(grvTransferInfo.Selected.Rows[0].Cells["Damage Number"].Value);

                DamageInfo damageInfo = new DamageManager().GetDamageInfoByID(damageInfoID);
                if (damageInfo.Status != (int)MTBFEnums.DamageStatus.Cancelled)
                {
                    if (MessageBoxHelper.ShowConfirmation("Are you sure to cancel this record?") == DialogResult.Yes)
                    {
                        if (new DamageManager().CanceleDamage(damageInfoID) == (int)MTBFEnums.ReturnResult.SUCCESS)
                        {
                            MessageBoxHelper.ShowInformation("Damage information cancel successfully.");
                        }
                        else
                        {
                            MessageBoxHelper.ShowInformation("Failed to cancel damage information.");
                        }
                    }
                }
                else
                {
                    MessageBoxHelper.ShowInformation("This record is already cancelled.");
                }

            }
        }

        private void frmViewDamage_Load(object sender, EventArgs e)
        {

        }

        private void grvTransferInfo_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            if (grvTransferInfo.Selected.Rows.Count > 0)
            {
                int productUseID = Convert.ToInt32(grvTransferInfo.Selected.Rows[0].Cells["ProductUsedID"].Value);
                LoadDamageInformationDetail(productUseID);
            }
        }
    }
}
