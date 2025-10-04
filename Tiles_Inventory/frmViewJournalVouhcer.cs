using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IMFS.Common.DTO;
using IFMS.BLL;
using NybSys.MTBF.Utility.Constants;
using NybSys.MTBF.Utility.Helper;
using Tiles_Inventory.Reports;

namespace Tiles_Inventory
{
    public partial class frmViewJournalVouhcer : BaseForm
    {
        List<JournalVoucher> lstJournalVoucher = new List<JournalVoucher>();

        public frmViewJournalVouhcer()
        {
            InitializeComponent();
        }

        private void btLoad_Click(object sender, EventArgs e)
        {
            LoadJournalVoucher();
        }


        private void LoadJournalVoucher()
        {
            string fromDate = dtpFromDate.Value.ToString(MTBFConstants.DateFormat) + MTBFConstants.DAY_START_TIME;
            string toDate = dtpToDate.Value.ToString(MTBFConstants.DateFormat) + MTBFConstants.DAY_END_TIME;

            string filter = string.Format("{0} between '{1}' and '{2}'", "Date", fromDate, toDate);
            lstJournalVoucher = new JournalVoucherManager().GetFilteredJournalVoucher(filter);
            LoadDataInGrid(lstJournalVoucher);
        }

        private DataTable BuildJournalTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Voucher Number");
            table.Columns.Add("Account Name");
            table.Columns.Add("Debit Amount");
            table.Columns.Add("Credit Amount");
            return table;

        }


        private void LoadDataInGrid(List<JournalVoucher> lstVoucher)
        {
            DataTable dt = BuildJournalTable();
            foreach (JournalVoucher contraVoucher in lstVoucher)
            {
                DataRow row = dt.NewRow();
                row["Voucher Number"] = contraVoucher.VoucherNumber;
                row["Account Name"] = string.IsNullOrEmpty(contraVoucher.DebitChildAccountName) ? contraVoucher.CreditChildAccountName : contraVoucher.DebitChildAccountName;
                row["Debit Amount"] = string.IsNullOrEmpty(contraVoucher.CreditChildAccountName) ? contraVoucher.Amount : 0;
                row["Credit Amount"] = string.IsNullOrEmpty(contraVoucher.DebitChildAccountName) ? contraVoucher.Amount : 0;
                dt.Rows.Add(row);
            }
            grvJournalInformaiton.DataSource = dt;
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            //frmJournalInformation frm = new frmJournalInformation();
            //frm.ShowDialog();
            frmJournalInformationNew frm = new frmJournalInformationNew();
            frm.ShowDialog();

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (grvJournalInformaiton.Selected.Rows.Count > 0)
            {
                int voucherNumber = Convert.ToInt32(grvJournalInformaiton.Selected.Rows[0].Cells["Voucher Number"].Text);

                List<JournalVoucher> lstEditContraVoucher = lstJournalVoucher.Where(v => v.VoucherNumber == voucherNumber).ToList();
                if (lstEditContraVoucher.Count > 0)
                {
                    //frmJournalInformation frm = new frmJournalInformation(lstEditContraVoucher, true);
                    //frm.ShowDialog();
                    frmJournalInformationNew frm = new frmJournalInformationNew(lstEditContraVoucher, true);
                    frm.ShowDialog();

                }
            }
        }

        private void btSearchByVoucherNo_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtVoucherNo.Text))
            {
                string filter = string.Format("{0}= '{1}'", "VoucherNumber", txtVoucherNo.Text.Trim());
                lstJournalVoucher = new JournalVoucherManager().GetFilteredJournalVoucher(filter);
                LoadDataInGrid(lstJournalVoucher);
            }
            else
            {
                MessageBoxHelper.ShowInformation("You need to enter voucher number.");
                txtVoucherNo.Focus();
            }

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (!cbPrintAll.Checked)
            {
                if (grvJournalInformaiton.Selected.Rows.Count > 0)
                {
                    string filter = string.Format("{0}= '{1}'", "VoucherNumber", grvJournalInformaiton.Selected.Rows[0].Cells["Voucher Number"].Value.ToString());
                    lstJournalVoucher = new JournalVoucherManager().GetFilteredJournalVoucher(filter);
                    rptJournalVoucher op = new rptJournalVoucher();
                    frmSalesReport objmainReport = new frmSalesReport();

                    DataTable Drtable = new DataTable();
                    DataTable dt = new DataTable();

                    dt.Columns.Add("VoucherNo");
                    dt.Columns.Add("AccountName");

                    dt.Columns.Add("Description");
                    dt.Columns.Add("DebitAmount");
                    dt.Columns.Add("CreditAmount");
                    dt.Columns.Add("Balance");

                    foreach (JournalVoucher jVoucher in lstJournalVoucher)
                    {
                        DataRow row = dt.NewRow();
                        row["VoucherNo"] = jVoucher.VoucherNumber;
                        row["AccountName"] = string.IsNullOrEmpty(jVoucher.DebitChildAccountName) ? jVoucher.CreditChildAccountName : jVoucher.DebitChildAccountName;

                        row["Description"] = jVoucher.Description;
                        row["DebitAmount"] = string.IsNullOrEmpty(jVoucher.CreditChildAccountName) ? jVoucher.Amount : 0;
                        row["CreditAmount"] = string.IsNullOrEmpty(jVoucher.DebitChildAccountName) ? jVoucher.Amount : 0;
                        dt.Rows.Add(row);
                    }


                    op.DataSource = dt;
                    objmainReport.rptViewer.Document = op.Document;
                    objmainReport.rptViewer.Dock = DockStyle.Fill;
                    op.txtFromDate.Text = dtpFromDate.Value.ToString("dd/MM/yyyy");
                    op.txtToDate.Text = dtpToDate.Value.ToString("dd/MM/yyyy");
                    op.Run();
                    objmainReport.ShowDialog();

                }
                else
                {
                    MessageBoxHelper.ShowInformation("You need to enter voucher number.");
                    txtVoucherNo.Focus();
                }
            }
            else
            {
                rptJournalVoucher op = new rptJournalVoucher();
                frmSalesReport objmainReport = new frmSalesReport();

                DataTable Drtable = new DataTable();
                DataTable dt = new DataTable();

                dt.Columns.Add("VoucherNo");
                dt.Columns.Add("AccountName");

                dt.Columns.Add("Description");
                dt.Columns.Add("DebitAmount");
                dt.Columns.Add("CreditAmount");
                dt.Columns.Add("Balance");

                foreach (JournalVoucher jVoucher in lstJournalVoucher)
                {
                    DataRow row = dt.NewRow();
                    row["VoucherNo"] = jVoucher.VoucherNumber;
                    row["AccountName"] = string.IsNullOrEmpty(jVoucher.DebitChildAccountName) ? jVoucher.CreditChildAccountName : jVoucher.DebitChildAccountName;

                    row["Description"] = jVoucher.Description;
                    row["DebitAmount"] = string.IsNullOrEmpty(jVoucher.CreditChildAccountName) ? jVoucher.Amount : 0;
                    row["CreditAmount"] = string.IsNullOrEmpty(jVoucher.DebitChildAccountName) ? jVoucher.Amount : 0;
                    dt.Rows.Add(row);
                }


                op.DataSource = dt;
                objmainReport.rptViewer.Document = op.Document;
                objmainReport.rptViewer.Dock = DockStyle.Fill;
                op.txtFromDate.Text = dtpFromDate.Value.ToString("dd/MM/yyyy");
                op.txtToDate.Text = dtpToDate.Value.ToString("dd/MM/yyyy");
                op.Run();
                objmainReport.ShowDialog();


            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }




    }
}
