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
using NybSys.MTBF.Utility.Constants;
using IMFS.Common.View;

namespace Tiles_Inventory
{
    public partial class frmVeiwPurchaseReturn : BaseForm
    {
        List<VMPurchaseReturn> lstVMPurchaseReturn = new List<VMPurchaseReturn>();
        public frmVeiwPurchaseReturn()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadPurchaseReturnInformation();
        }

        private void LoadPurchaseReturnInformation()
        {
            string filter = string.Empty;

            filter = string.Format("ReturnDate between '{0}' And '{1}' and BranchID={2}", dtpFromDate.Value.ToString("yyyy/MM/dd") + MTBFConstants.DAY_START_TIME, dtpToDate.Value.ToString("yyyy/MM/dd") + MTBFConstants.DAY_END_TIME, "BranchID", MTBFConstants.AppConstants.BranchID);
            lstVMPurchaseReturn = new PurchaseReturnManager().GetFilteredPurchaseReturn(filter);

            //int[] companyID = lstVMPurchaseReturn.Select(vmp => vmp.purchaseReturn.CompanyID).Distinct().ToArray();
            //int[] supplierIDs = lstVMPurchaseReturn.Select(vmp => vmp.purchaseReturn.SupplierID).Distinct().ToArray();

            //filter = String.Format("{0} IN ({1})", "CompanyID", String.Join(",", companyID));

            //lstCompanyInformation = new CompanyManager().GetFilteredCompany(filter).Cast<Company>().Where(c => c.BranchID == MTBFConstants.AppConstants.BranchID).ToList();

            //filter = String.Format("{0} IN ({1})", "SupplierID", String.Join(",", supplierIDs));
            //lstSupplier = new SupplierManager().GetFilteredSuppler(filter);

            LoadDataInGrid(lstVMPurchaseReturn);


        }


        private void LoadDataInGrid(List<VMPurchaseReturn> lstvmPurchaseReturn)
        {
            List<Company> lstCompanyInformation = new List<Company>();
            List<Supplier> lstSupplier = new List<Supplier>();


            int[] companyID = lstvmPurchaseReturn.Select(vmp => vmp.purchaseReturn.CompanyID).Distinct().ToArray();
            int[] supplierIDs = lstvmPurchaseReturn.Select(vmp => vmp.purchaseReturn.SupplierID).Distinct().ToArray();

            string filter = String.Format("{0} IN ({1})", "CompanyID", String.Join(",", companyID));
            if (companyID.Length > 0)
            {
                lstCompanyInformation = new CompanyManager().GetFilteredCompany(filter).Cast<Company>().Where(c => c.BranchID == MTBFConstants.AppConstants.BranchID).ToList();
            }
            if (supplierIDs.Length > 0)
            {
                filter = String.Format("{0} IN ({1})", "SupplierID", String.Join(",", supplierIDs));
                lstSupplier = new SupplierManager().GetFilteredSuppler(filter);
            }



            DataTable dt = BuildPurchaseReturnTable();
            foreach (VMPurchaseReturn vmPRetun in lstvmPurchaseReturn)
            {
                Supplier supplier = lstSupplier.Where(s => s.SupplierID == vmPRetun.purchaseReturn.SupplierID).FirstOrDefault();
                Company company = lstCompanyInformation.Where(c => c.CompanyID == vmPRetun.purchaseReturn.CompanyID).FirstOrDefault();
                DataRow row = dt.NewRow();
                row["PurchaseReturnID"] = vmPRetun.purchaseReturn.PurchaseReturnID;
                row["Return Date"] = vmPRetun.purchaseReturn.ReturnDate.ToString("dd/MM/yyyy");
                row["PO Number"] = vmPRetun.purchaseReturn.PONumber;
                row["Company"] = (company != null) ? company.CompanyName : string.Empty;
                row["Supplier"] = (supplier != null) ? supplier.SupplierName : string.Empty;
                row["Total"] = vmPRetun.purchaseReturn.Total;
                row["Receive Amount"] = vmPRetun.purchaseReturn.ReceiveAmount;
                dt.Rows.Add(row);
            }

            grvTransferInfo.DataSource = dt;
            grvTransferInfo.DisplayLayout.Bands[0].Columns["PurchaseReturnID"].Hidden = true;

        }


        private DataTable BuildPurchaseReturnTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PurchaseReturnID");
            dt.Columns.Add("Return Date");
            dt.Columns.Add("PO Number");
            dt.Columns.Add("Company");
            dt.Columns.Add("Supplier");
            dt.Columns.Add("Total");
            dt.Columns.Add("Receive Amount");
            return dt;
        }

        private void grvTransferInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void LoadPurchaseReturnDetail(List<PurchaseReturnDetail> lstReturnDetail)
        {
            grvTransferDetailInfo.DataSource = lstReturnDetail;
            grvTransferDetailInfo.Columns["PurchaseReturnDetailID"].Visible = false;
            grvTransferDetailInfo.Columns["PurchaseReturnID"].Visible = false;
        }

        private void btDistribute_Click(object sender, EventArgs e)
        {
            frmPurchaseReturn frm = new frmPurchaseReturn();
            frm.ShowDialog();
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            if (grvTransferInfo.Selected.Rows.Count > 0)
            {
                int returnID = Convert.ToInt32(grvTransferInfo.Selected.Rows[0].Cells["PurchaseReturnID"].Value);
                List<PurchaseReturnDetail> lstReturnDetail = new List<PurchaseReturnDetail>();
                VMPurchaseReturn vmPurchaseReturn = lstVMPurchaseReturn.Where(vmp => vmp.purchaseReturn.PurchaseReturnID == returnID).FirstOrDefault();
                frmPurchaseReturn frm = new frmPurchaseReturn(vmPurchaseReturn);
                frm.ShowDialog();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvTransferInfo_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            if (grvTransferInfo.Selected.Rows.Count > 0)
            {
                int returnID = Convert.ToInt32(grvTransferInfo.Selected.Rows[0].Cells["PurchaseReturnID"].Value);
                List<PurchaseReturnDetail> lstReturnDetail = new List<PurchaseReturnDetail>();
                lstReturnDetail = lstVMPurchaseReturn.Where(vmp => vmp.purchaseReturn.PurchaseReturnID == returnID).FirstOrDefault().lstPurchaseReturnDetail;
                LoadPurchaseReturnDetail(lstReturnDetail);
            }
        }

    }
}
