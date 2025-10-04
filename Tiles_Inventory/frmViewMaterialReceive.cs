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
using NybSys.MTBF.Utility.Enums;

namespace Tiles_Inventory
{
    public partial class frmViewMaterialReceive : BaseForm
    {

        private List<MaterialsReceive> lstMaterialReceive = new List<MaterialsReceive>();

        public frmViewMaterialReceive()
        {
            DataAccessProxy = new FacadeManager();
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadProductTransferInformationBySalesCenterNameAndDate(dtpFromDate.Value.ToString("yyyy/MM/dd"), dtpToDate.Value.ToString("yyyy/MM/dd"));
        }

        private DataTable BuildTransferTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("MaterialReceiveID");
            table.Columns.Add("Receive Date");
            table.Columns.Add("Supplier Name");
            table.Columns.Add("Total");
            table.Columns.Add("Paid Amount");
            table.Columns.Add("Status");
            return table;

        }

        private DataTable BuildTransferDetailTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Material Name");
            table.Columns.Add("Quantity");
            table.Columns.Add("Price");
            table.Columns.Add("Total");
            return table;

        }

        private void LoadProductTransferInformationBySalesCenterNameAndDate(string fromDate, string toDate)
        {
            fromDate = fromDate + MTBFConstants.DAY_START_TIME;
            toDate = toDate + MTBFConstants.DAY_END_TIME;
            DataTable transferTable = BuildTransferTable();
            string filter = string.Format("{0} between '{1}' and '{2}'", "ReceiveDate", fromDate, toDate);

            lstMaterialReceive = new MaterialReceiveManager().GetFilteredMaterial(filter).Where(t => t.BranchID == MTBFConstants.AppConstants.BranchID && t.OrganizationID == MTBFConstants.AppConstants.OrganizationID).Cast<MaterialsReceive>().ToList();
            foreach (MaterialsReceive transfer in lstMaterialReceive)
            {
                MaterialSupplier supplier = new MaterialSupplierManager().GetMaterialSupplierByID(transfer.SupplierID);
                DataRow row = transferTable.NewRow();
                row["MaterialReceiveID"] = transfer.MaterialReceiveID;
                row["Receive Date"] = transfer.ReceiveDate.ToShortDateString();
                row["Supplier Name"] = (supplier != null) ? supplier.SupplierName : string.Empty;
                row["Total"] = transfer.Total;
                row["Paid Amount"] = transfer.PaidAmount;
                row["Status"] = Enum.GetName(typeof(MTBFEnums.MaterialReciveStatus), transfer.Status);
                transferTable.Rows.Add(row);
            }
            grvTransferInfo.DataSource = transferTable;
            if (transferTable.Rows.Count == 0)
            {
                MessageBoxHelper.ShowInformation("No data found for this combination");
            }
        }


        private void LoadTransferDetailInformation(int materialReceiveID)
        {

            DataTable transferDetailTable = BuildTransferDetailTable();
            foreach (MaterialsReceiveDetails tDetail in new MaterialReceiveManager().GetAllMaterialsReceiveDetailByMaterialsReceiveID(materialReceiveID))
            {
                Materials material = new MaterialManager().GetMeterialsByID(tDetail.MaterialID);
                DataRow row = transferDetailTable.NewRow();
                row["Material Name"] = (material != null) ? material.MaterialName : string.Empty;
                row["Quantity"] = tDetail.Quantity;
                row["Price"] = tDetail.Price;
                row["Price"] = tDetail.Price;
                row["Total"] = tDetail.Total;

                transferDetailTable.Rows.Add(row);
            }

            grvTransferDetailInfo.DataSource = transferDetailTable;
        }

        private void frmViewTransferInformation_Load(object sender, EventArgs e)
        {
            grvTransferInfo.DataSource = BuildTransferTable();
            grvTransferDetailInfo.DataSource = BuildTransferDetailTable();
        }



        private void btnDistribute_Click(object sender, EventArgs e)
        {
            frmMaterialReceive frm = new frmMaterialReceive();
            frm.ShowDialog();
        }

        private void grvTransferInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grvTransferInfo.SelectedRows.Count > 0)
            {
                int transferID = Convert.ToInt32(grvTransferInfo.SelectedRows[0].Cells["MaterialReceiveID"].Value);
                LoadTransferDetailInformation(transferID);
            }
        }

        private void grvTransferInfo_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (grvTransferInfo.SelectedRows.Count > 0 && !grvTransferInfo.SelectedRows[0].IsNewRow)
            {
                int transferID = Convert.ToInt32(grvTransferInfo.SelectedRows[0].Cells["MaterialReceiveID"].Value);
                LoadTransferDetailInformation(transferID);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (grvTransferInfo.SelectedRows.Count > 0 && !grvTransferInfo.SelectedRows[0].IsNewRow)
            {
                int transferID = Convert.ToInt32(grvTransferInfo.SelectedRows[0].Cells["MaterialReceiveID"].Value);
                MaterialsReceive materialReceive = new MaterialsReceive();
                materialReceive = new MaterialReceiveManager().GetMaterialsReceiveByID(transferID);
                if (materialReceive.Status == (int)MTBFEnums.MaterialReciveStatus.Approved)
                {
                    frmMaterialReceive frm = new frmMaterialReceive(transferID, true);
                    frm.ShowDialog();
                }
                else
                {
                    MessageBoxHelper.ShowInformation("You can edit only issued receive.");
                }
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

            LoadProductTransferInformationBySalesCenterNameAndDate(dtpFromDate.Value.ToString("yyyy/MM/dd"), dtpToDate.Value.ToString("yyyy/MM/dd"));

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (grvTransferInfo.SelectedRows.Count > 0 && !grvTransferInfo.SelectedRows[0].IsNewRow)
            {
                int transferID = Convert.ToInt32(grvTransferInfo.SelectedRows[0].Cells["MaterialReceiveID"].Value);
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
            table.Columns.Add("Supplier Name");
            table.Columns.Add("Phone");
            table.Columns.Add("Receive Date");
            table.Columns.Add("Material Name");
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



            foreach (MaterialsReceive materialReceive in lstMaterialReceive)
            {
                MaterialSupplier supplier = new MaterialSupplierManager().GetMaterialSupplierByID(materialReceive.SupplierID);
                foreach (MaterialsReceiveDetails tDetail in new MaterialReceiveManager().GetAllMaterialsReceiveDetailByMaterialsReceiveID(materialReceive.MaterialReceiveID))
                {
                    Materials product = new MaterialManager().GetMeterialsByID(tDetail.MaterialID);

                    DataRow row = transferTable.NewRow();
                    row["Supplier Name"] = supplier.SupplierName;
                    row["Phone"] = supplier.PhoneNumber;
                    row["Receive Date"] = materialReceive.ReceiveDate.ToString("dd/MM/yyyy");
                    row["Material Name"] = product.MaterialName;
                    row["Quantity"] = tDetail.Quantity;
                    row["Price"] = tDetail.Price;
                    row["Total"] = tDetail.Total;
                    transferTable.Rows.Add(row);
                }
            }

            rptReceiveMaterial op = new rptReceiveMaterial();
            frmSalesReport objmainReport = new frmSalesReport();
            op.DataSource = transferTable;
            objmainReport.rptViewer.Document = op.Document;
            objmainReport.rptViewer.Dock = DockStyle.Fill;
            op.txtFromDate.Text = dtpFromDate.Value.ToString("dd/MM/yyyy");
            op.txtToDate.Text = dtpToDate.Value.ToString("dd/MM/yyyy");
            GetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
            op.txtPharmacyName.Text = pharmacyName;
            op.txtAddress.Text = pharmacyAddress;

            op.Run();
            objmainReport.MdiParent = this.MdiParent;
            objmainReport.Show();
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (grvTransferInfo.SelectedRows.Count > 0 && !grvTransferInfo.SelectedRows[0].IsNewRow)
            {
                int transferID = Convert.ToInt32(grvTransferInfo.SelectedRows[0].Cells["MaterialReceiveID"].Value);

                MaterialsReceive materialReceive = new MaterialsReceive();
                materialReceive = new MaterialReceiveManager().GetMaterialsReceiveByID(transferID);

                if (materialReceive.Status != (int)MTBFEnums.MaterialReciveStatus.Approved)
                {
                    if (new MaterialReceiveManager().ApproveMaterialReceive(materialReceive) == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Approved successfully.");
                    }
                    else
                    {
                        MessageBoxHelper.ShowError("Approve failed.");
                    }
                }
                else
                {
                    MessageBoxHelper.ShowInformation("This receive is already approved.");
                }
            }
            else
            {
                MessageBoxHelper.ShowInformation("You need to select a record.");
            }
        }



    }
}