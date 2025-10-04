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

namespace Tiles_Inventory
{
    public partial class frmViewTransferInformation : BaseForm
    {
        public frmViewTransferInformation()
        {
            DataAccessProxy = new FacadeManager();
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (cmbSalesCenter.Value != null && Convert.ToInt32(cmbSalesCenter.Value) != MTBFConstants.DataField.COMBO_DEFAULT_ID)
            {
                LoadProductTransferInformationBySalesCenterNameAndDate(Convert.ToInt32(cmbSalesCenter.Value), dtpFromDate.Value.ToString("yyyy/MM/dd"), dtpToDate.Value.ToString("yyyy/MM/dd"));
            }
            else
            {
                MessageBoxHelper.ShowInformation("You need to select sales center.");
            }
        }

        private DataTable BuildTransferTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Transfer Number");
            table.Columns.Add("Transfer Date");
            table.Columns.Add("Transfered By");
            table.Columns.Add("Is Edited");
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

        private void LoadProductTransferInformationBySalesCenterNameAndDate(int salesCenterID, string fromDate, string toDate)
        {
            DataTable transferTable = BuildTransferTable();
            foreach (Transfer transfer in DataAccessProxy.GetTransferBySalesCenterAndDate(salesCenterID, fromDate, toDate).Where(t => t.BranchID == MTBFConstants.AppConstants.BranchID && t.OrganizationID == MTBFConstants.AppConstants.OrganizationID))
            {
                Users user = DataAccessProxy.GetUserByUserID(transfer.TransferBy);
                Employee employee = DataAccessProxy.GetEmployeeByID(user.EmployeeID);
                DataRow row = transferTable.NewRow();
                row["Transfer Number"] = transfer.TransferID;
                row["Transfer Date"] = transfer.TransferDate.ToShortDateString();
                row["Transfered By"] = (employee != null) ? employee.EmployeeName : string.Empty;
                row["Is Edited"] = string.IsNullOrEmpty(transfer.UpdatedBy) ? false : true;
                transferTable.Rows.Add(row);
            }
            grvTransferInfo.DataSource = transferTable;
            if (transferTable.Rows.Count == 0)
            {
                MessageBoxHelper.ShowInformation("No data found for this combination");
            }
        }


        private void LoadTransferDetailInformation(int transferID)
        {

            DataTable transferDetailTable = BuildTransferDetailTable();
            foreach (TransferDetail tDetail in DataAccessProxy.GetAllTransferDetailByTransferID(transferID))
            {
                DataRow row = transferDetailTable.NewRow();
                row["Product Name"] = tDetail.ProductName;
                row["Quantity"] = tDetail.Quantity;
                row["Price"] = tDetail.Price;
                row["Total"] = (tDetail.Price * tDetail.Quantity).ToString("F2");
                transferDetailTable.Rows.Add(row);
            }

            grvTransferDetailInfo.DataSource = transferDetailTable;
        }

        private void frmViewTransferInformation_Load(object sender, EventArgs e)
        {
            LoadSalesCenterCombo();
            grvTransferInfo.DataSource = BuildTransferTable();
            grvTransferDetailInfo.DataSource = BuildTransferDetailTable();
        }

        /// <summary>
        /// Method to load sales center combo.
        /// </summary>
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
            cmbSalesCenter.DataSource = table;
            cmbSalesCenter.DisplayMember = "Name";
            cmbSalesCenter.ValueMember = "ID";
            cmbSalesCenter.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
        }

        private void btnDistribute_Click(object sender, EventArgs e)
        {
            frmProductTransfer frm = new frmProductTransfer();
            frm.ShowDialog();
        }

        private void grvTransferInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grvTransferInfo.SelectedRows.Count > 0)
            {
                int transferID = Convert.ToInt32(grvTransferInfo.SelectedRows[0].Cells["Transfer Number"].Value);
                LoadTransferDetailInformation(transferID);
            }
        }

        private void grvTransferInfo_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (grvTransferInfo.SelectedRows.Count > 0 && !grvTransferInfo.SelectedRows[0].IsNewRow)
            {
                int transferID = Convert.ToInt32(grvTransferInfo.SelectedRows[0].Cells["Transfer Number"].Value);
                LoadTransferDetailInformation(transferID);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (grvTransferInfo.SelectedRows.Count > 0 && !grvTransferInfo.SelectedRows[0].IsNewRow)
            {
                int transferID = Convert.ToInt32(grvTransferInfo.SelectedRows[0].Cells["Transfer Number"].Value);
                frmProductTransfer frm = new frmProductTransfer(transferID, true);
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
            if (cmbSalesCenter.Value != null && Convert.ToInt32(cmbSalesCenter.Value) != MTBFConstants.DataField.COMBO_DEFAULT_ID)
            {
                LoadProductTransferInformationBySalesCenterNameAndDate(Convert.ToInt32(cmbSalesCenter.Value), dtpFromDate.Value.ToShortDateString(), dtpToDate.Value.ToShortDateString());
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (grvTransferInfo.SelectedRows.Count > 0 && !grvTransferInfo.SelectedRows[0].IsNewRow)
            {
                int transferID = Convert.ToInt32(grvTransferInfo.SelectedRows[0].Cells["Transfer Number"].Value);
                PrintTrnasferInformation(transferID);
            }
            else
            {
                MessageBoxHelper.ShowInformation("You need to select a record.");
            }
        }

        private DataTable BuildReportTable()
        {
            DataTable table = new DataTable();
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
            Organization pharmacy = DataAccessProxy.GetAllPharmacy().Cast<Organization>().ToList().FirstOrDefault();
            if (pharmacy != null)
            {
                PharmacyName = pharmacy.OrganizationName;
                Address = pharmacy.Address + ", " + pharmacy.Phone + ", " + pharmacy.Fax;
            }
        }

        private void PrintTrnasferInformation(int transferID)
        {
            string pharmacyName = string.Empty;
            string pharmacyAddress = string.Empty;
            Transfer transfer = DataAccessProxy.GetTransferByID(transferID);
            DataTable transferTable = BuildReportTable();
            if (transfer != null)
            {
                SalesCenter salesCenter = DataAccessProxy.GetSalesCenterByID(transfer.SalesCenterID);
                rptTransferProduct op = new rptTransferProduct();
                frmSalesReport objmainReport = new frmSalesReport();

                foreach (TransferDetail tDetail in DataAccessProxy.GetAllTransferDetailByTransferID(transferID))
                {
                    Product product = DataAccessProxy.GetProductByID(tDetail.ProductCode);
                    ProductSize pSize = DataAccessProxy.GetProductSizeByID(product.ProductSizeID);
                    DataRow row = transferTable.NewRow();
                    row["Product Name"] = tDetail.ProductName;
                    row["Quantity"] = tDetail.Quantity;
                    row["Price"] = tDetail.Price;
                    row["Total"] = (tDetail.Quantity * tDetail.Price).ToString("F2");

                    transferTable.Rows.Add(row);
                }
                op.DataSource = transferTable;
                objmainReport.Text = "Transfer Report";
                objmainReport.rptViewer.Document = op.Document;
                objmainReport.rptViewer.Dock = DockStyle.Fill;

                GetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
                op.txtPharmacyName.Text = pharmacyName;
                op.txtAddress.Text = pharmacyAddress;
                op.txtTransferID.Text = transfer.TransferID.ToString();
                op.txtSalesCenterName.Text = salesCenter.SalesCenterName;
                op.txtSalesCenterAddress.Text = salesCenter.Address;
                op.txtPhone.Text = salesCenter.Phone;
                Employee employee = DataAccessProxy.GetEmployeeByID(IFMSConstant.LoggedinUserID);

                op.txtServiceMan.Text = employee.EmployeeName;
                // op.txtOrderDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                op.Run();
                objmainReport.MdiParent = this.MdiParent;
                objmainReport.Show();
            }


        }
    }
}