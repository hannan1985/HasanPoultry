using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IFMS.Facade;
using IMFS.Common.View;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Helper;
using NybSys.MTBF.Utility.Constants;
using IFMS.BLL;

namespace Tiles_Inventory
{
    public partial class frmViewPayment : BaseForm
    {
        private int _paymentID = 0;
        List<Supplier> lstSupplier = new List<Supplier>();
        public frmViewPayment()
        {
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        #region "Event Handelers"

        private void frmViewPayment_Load(System.Object sender, System.EventArgs e)
        {
            LoadSupplierCombo();
            grvPaymentInformation.DataSource = BuildCashReceiveTable();
            grvPaymentInformation.DisplayLayout.Bands[0].Columns["PaymentID"].Hidden = true;
            base.CheckAction(this);
        }

        private void btnLoad_Click(System.Object sender, System.EventArgs e)
        {


            LoadPaymentInformation(dtpFromDate.Value.ToString("yyyy/MM/dd"), dtpToDate.Value.ToString("yyyy/MM/dd"));

        }

        private void btnClose_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnEdit_Click(System.Object sender, System.EventArgs e)
        {
            if (grvPaymentInformation.Selected.Rows.Count > 0)
            {
                _paymentID = Convert.ToInt32(grvPaymentInformation.Selected.Rows[0].Cells["PaymentID"].Value);
                frmPayment frm = new frmPayment(true, _paymentID);
                frm.OnPaymentInformationLoad += OnPaymentInfoLoad;
                frm.ShowDialog();
            }
            else
            {
                MessageBoxHelper.ShowInformation("Please select a row.");
            }
        }

        private void OnPaymentInfoLoad()
        {

            LoadPaymentInformation(dtpFromDate.Value.ToString("yyyy/MM/dd"), dtpToDate.Value.ToString("yyyy/MM/dd"));

        }

        private void btnAddNew_Click(System.Object sender, System.EventArgs e)
        {
            frmPayment frm = new frmPayment(false, _paymentID);
            frm.OnPaymentInformationLoad += OnPaymentInfoLoad;
            frm.ShowDialog();

        }

        private void btnRefresh_Click(System.Object sender, System.EventArgs e)
        {
            LoadPaymentInformation(dtpFromDate.Value.ToString("yyyy/MM/dd"), dtpToDate.Value.ToString("yyyy/MM/dd"));
        }

        #endregion

        #region "Private Methods"

        /// <summary>
        /// Method to load company informaiton in combo box.
        /// </summary>
        private void LoadSupplierCombo()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("Name");

            lstSupplier = new SupplierManager().GetAllSupplierByBranchID(MTBFConstants.AppConstants.BranchID);
            DataRow emptyrow = table.NewRow();
            emptyrow[0] = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            emptyrow[1] = MTBFConstants.DataField.COMBO_DEFAULT_NAME;
            table.Rows.Add(emptyrow);

            foreach (Supplier company in lstSupplier)
            {
                DataRow row = table.NewRow();
                row[0] = company.SupplierID;
                row[1] = company.SupplierName;
                table.Rows.Add(row);
            }

            cmbSupplierName.DisplayMember = "Name";
            cmbSupplierName.ValueMember = "ID";
            cmbSupplierName.DataSource = table;
            cmbSupplierName.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
        }

        /// <summary>
        /// Method to load payment entry information in grid.
        /// </summary>
        /// <remarks></remarks>

        private void LoadPaymentInformation(string fromDate, string toDate)
        {

            fromDate = fromDate + MTBFConstants.DAY_START_TIME;
            toDate = toDate + MTBFConstants.DAY_END_TIME;

            List<Payment> lstPayment = new List<Payment>();

            string filter = string.Format("{0} between '{1}' and '{2}'", "PaymentDate", fromDate, toDate);

            lstPayment = new PaymentManager().GetFilteredPaymentInformation(filter);

            int supplierID = 0;
            if (cmbSupplierName.Value != null)
            {
                int.TryParse(cmbSupplierName.Value.ToString(), out supplierID);
            }

            if (supplierID > 0)
            {
                lstPayment = lstPayment.Where(p => p.SupplierID == supplierID).ToList();
            }


            if (lstPayment.Count <= 0)
            {
                MessageBoxHelper.ShowInformation("No data available for this combination.");
            }
            else
            {
                LoadDataInGrid(lstPayment);
            }
        }


        private DataTable BuildCashReceiveTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PaymentID");
            dt.Columns.Add("Memo Number");
            dt.Columns.Add("Supplier Name");
            dt.Columns.Add("Payment Date");
            dt.Columns.Add("Cash Amount");
            dt.Columns.Add("Bank Amount");
            dt.Columns.Add("Paid Amount");
            dt.Columns.Add("Discount");
            dt.Columns.Add("Bill Reference");
            dt.Columns.Add("Employee");
            //  dt.Columns.Add("Status");
            return dt;
        }

        private void LoadDataInGrid(List<Payment> lstCashReceive)
        {
            lblTotalRecord.Text = lstCashReceive.Count.ToString();
            DataTable dt = BuildCashReceiveTable();

            foreach (Payment payment in lstCashReceive)
            {
                Supplier supplier = lstSupplier.Where(c => c.SupplierID == payment.SupplierID).FirstOrDefault();
                DataRow row = dt.NewRow();
                row["PaymentID"] = payment.PaymentID;
                row["Memo Number"] = payment.VoucherNumber;
                row["Supplier Name"] = (supplier != null) ? supplier.SupplierName : string.Empty;
                row["Payment Date"] = payment.PaymentDate.ToString("dd/MM/yyyy");
                row["Cash Amount"] = payment.CashAmount;
                row["Bank Amount"] = payment.BankAmount;
                row["Paid Amount"] = payment.Amount;
                row["Discount"] = payment.Discount;
                row["Bill Reference"] = payment.BillRefNumber;
                row["Employee"] = payment.CreatedBy;
                // row["Status"] = Enum.GetName(typeof(MTBFEnums.CashReceieStatus), cashReceive.Status);
                dt.Rows.Add(row);
            }
            grvPaymentInformation.DataSource = dt;
            grvPaymentInformation.DisplayLayout.Bands[0].Columns["PaymentID"].Hidden = true;

        }

        #endregion
    }
}
