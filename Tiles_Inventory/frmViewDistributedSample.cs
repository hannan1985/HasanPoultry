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

using IMFS.Common.Utility;
using IFMS.Facade;
using Tiles_Inventory.Reports;
using NybSys.MTBF.Utility.Constants;

namespace Tiles_Inventory
{
    public partial class frmViewDistributedSample : BaseForm
    {
        public frmViewDistributedSample()
        {
            DataAccessProxy = new FacadeManager();
            InitializeComponent();
        }

        private void btnDistribute_Click(object sender, EventArgs e)
        {
            frmDistributeSample frm = new frmDistributeSample();
            frm.ShowDialog();
        }

        private void btnReceive_Click(object sender, EventArgs e)
        {
            if (grvDistributionInfo.SelectedRows.Count > 0)
            {
                int distributionID = Convert.ToInt32(grvDistributionInfo.SelectedRows[0].Cells["DistributionID"].Value);

                DistributeSample distributeSample = DataAccessProxy.GetDistributeSampleByID(distributionID);
                if (distributeSample.GivenQuantity > distributeSample.ReceiveQuantity)
                {
                    frmReceiveSample frm = new frmReceiveSample(distributionID, true);
                    frm.ShowDialog();
                }
                else if (distributeSample.GivenQuantity == distributeSample.ReceiveQuantity)
                {
                    MessageBoxHelper.ShowInformation("You already receive all samples.");
                }
            }
            else
            {
                MessageBoxHelper.ShowInformation("You need to select a record for receive.");
            }

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string fromDate = dtpFromDate.Value.ToString("yyyy/MM/dd");
            string toDate = dtpToDate.Value.ToString("yyyy/MM/dd");
            if (cmbSellerName.Value != null)
            {

                LoadSampleDistributionInformationBySellerIDandDate(Convert.ToInt32(cmbSellerName.Value), fromDate, toDate);
            }
            else
            {
                MessageBoxHelper.ShowInformation("You need to select seller name.");
            }
        }

        private void LoadSampleDistributionInformationBySellerIDandDate(int sellerID, string fromDate, string toDate)
        {
            DataTable distributeTable = BuildSampleDistributionTable();
            List<DistributeSample> lstDistributeSample = DataAccessProxy.GetDistributeSampleBySellerIDAndDate(sellerID, fromDate, toDate).OrderBy(d => d.ProductID).Cast<DistributeSample>().ToList();
            foreach (DistributeSample distributeSample in lstDistributeSample.Where(p => p.BranchID == MTBFConstants.AppConstants.BranchID && p.OrganizationID == MTBFConstants.AppConstants.OrganizationID))
            {
                Product product = DataAccessProxy.GetProductByID(distributeSample.ProductID);
                ProductSize pSize = DataAccessProxy.GetProductSizeByID(product.ProductSizeID);
                ProductType pType = DataAccessProxy.GetProductTypeByID(product.ProductTypeID);

                DataRow row = distributeTable.NewRow();
                row["DistributionID"] = distributeSample.DistributionID;
                row["Product Name"] = product.ProductName;
                row["Size"] = (pSize != null) ? pSize.Name : string.Empty;
                row["Type"] = (pType != null) ? pType.TypeName : string.Empty;
                row["Distribute Date"] = distributeSample.DistributeDate.ToShortDateString();
                row["Receive Date"] = distributeSample.ReceiveDate.ToShortDateString();
                row["Given Quantity"] = distributeSample.GivenQuantity;
                row["Receive Quantity"] = distributeSample.ReceiveQuantity;

                distributeTable.Rows.Add(row);
            }
            grvDistributionInfo.DataSource = distributeTable;
            
        }

        /// <summary>
        /// Method to load seller information in combo box.
        /// </summary>
        private void LoadSellerInformation()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Seller ID");
            table.Columns.Add("Seller Name");

            foreach (Seller seller in DataAccessProxy.GetAllSeller().Where(s => s.BranchID == MTBFConstants.AppConstants.BranchID && s.OrganizationID == MTBFConstants.AppConstants.OrganizationID))
            {
                DataRow row = table.NewRow();
                row["Seller ID"] = seller.SellerID;
                row["Seller Name"] = seller.SellerName;
                table.Rows.Add(row);
            }

            cmbSellerName.DataSource = table;
            cmbSellerName.DisplayMember = "Seller Name";
            cmbSellerName.ValueMember = "Seller ID";

        }


        private DataTable BuildSampleDistributionTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("DistributionID");
            table.Columns.Add("Product Name");
            table.Columns.Add("Size");
            table.Columns.Add("Type");
            table.Columns.Add("Distribute Date");
            table.Columns.Add("Receive Date");
            table.Columns.Add("Given Quantity");
            table.Columns.Add("Receive Quantity");

            return table;
        }

        private void frmViewDistributedSample_Load(object sender, EventArgs e)
        {
            grvDistributionInfo.DataSource = BuildSampleDistributionTable();
            LoadSellerInformation();
            base.CheckAction(this);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            string fromDate = dtpFromDate.Value.ToString("yyyy/MM/dd");
            string toDate = dtpToDate.Value.ToString("yyyy/MM/dd");
            if (cmbSellerName.Value != null)
            {

                LoadSampleDistributionInformationBySellerIDandDate(Convert.ToInt32(cmbSellerName.Value), fromDate, toDate);
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (grvDistributionInfo.SelectedRows.Count > 0)
            {
                int distributionID = Convert.ToInt32(grvDistributionInfo.SelectedRows[0].Cells["DistributionID"].Value);

                DistributeSample distributeSample = DataAccessProxy.GetDistributeSampleByID(distributionID);
                if (distributeSample.ReceiveQuantity == 0)
                {
                    frmDistributeSample frm = new frmDistributeSample(distributionID, true);
                    frm.ShowDialog();
                }
                else if (distributeSample.ReceiveQuantity > 0)
                {
                    MessageBoxHelper.ShowInformation("You can edit only those records which receive quantity is zero.");
                }
            }
            else
            {
                MessageBoxHelper.ShowInformation("You need to select a record for receive.");
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintDistributionInformation();
        }


        private DataTable BuildDistributionTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Product Name");
            table.Columns.Add("Size");
            table.Columns.Add("Type");
            table.Columns.Add("Distribute Date");
            table.Columns.Add("Quantity");
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

        private void PrintDistributionInformation()
        {
            string pharmacyName = string.Empty;
            string pharmacyAddress = string.Empty;
            DataTable table = BuildDistributionTable();
            int distributeID = 0;
            List<DistributionReport> lstDistributionReport = new List<DistributionReport>();
            foreach (DataGridViewRow row in grvDistributionInfo.Rows)
            {               
                if (row.Cells["Product Name"].Value != null)
                {
                    distributeID = Convert.ToInt32(row.Cells["DistributionID"].Value);
                    DistributionReport disReport = new DistributionReport();
                    disReport.ProductName = row.Cells["Product Name"].Value.ToString();
                    disReport.Type = row.Cells["Type"].Value.ToString();
                    disReport.Size = row.Cells["Size"].Value.ToString();
                    disReport.Quantity = row.Cells["Given Quantity"].Value.ToString();
                    lstDistributionReport.Add(disReport);
                }
            }
            lstDistributionReport = lstDistributionReport.OrderBy(d => d.ProductName).Cast<DistributionReport>().ToList();
            if (lstDistributionReport.Count > 0)
            {
                rptDistributeSample op = new rptDistributeSample();
                frmSalesReport objmainReport = new frmSalesReport();
                DistributeSample distributeSample = DataAccessProxy.GetDistributeSampleByID(distributeID);

                Seller seller = DataAccessProxy.GetSellerByID(distributeSample.SellerID);
                op.DataSource = lstDistributionReport;
                objmainReport.rptViewer.Document = op.Document;
                objmainReport.rptViewer.Dock = DockStyle.Fill;

                GetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
                op.txtPharmacyName.Text = pharmacyName;
                op.txtAddress.Text = pharmacyAddress;
                if (seller != null)
                {
                    op.txtSalesCenterName.Text = seller.SellerName;
                    op.txtSalesCenterAddress.Text = seller.Address;
                    op.txtPhone.Text = seller.Phone;
                }

                Employee employee = DataAccessProxy.GetEmployeeByID(IFMSConstant.LoggedinUserID);

                op.txtServiceMan.Text = employee.EmployeeName;
                // op.txtOrderDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                op.Run();
                objmainReport.MdiParent = this.MdiParent;
                objmainReport.Show();
            }
            else
            {
                MessageBoxHelper.ShowInformation("Blank grid can't be print.");
            }
        }


        private class DistributionReport
        {
            public string  ProductName { get; set; }
            public string  Type { get; set; }
            public string Size { get; set; }
            public string  Quantity { get; set; }
        }

    }
}


