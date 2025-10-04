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

namespace Tiles_Inventory
{
    public partial class frmViewOtherPayment : BaseForm
    {
        List<OtherPayment> lstOtherPayment = new List<OtherPayment>();

        public frmViewOtherPayment()
        {
            InitializeComponent();
        }

        private void btView_Click(object sender, EventArgs e)
        {
            LoadOtherPaymentInformation();
        }

        private void LoadOtherPaymentInformation()
        {
            grvOtherPaymentInformation.Rows.Clear();

            string fromDate = dtpFromDate.Value.ToString("yyyy/MM/dd") + MTBFConstants.DAY_START_TIME;
            string toDate = dtpToDate.Value.ToString("yyyy/MM/dd") + MTBFConstants.DAY_END_TIME;

            string filter = string.Format("{0} between '{1}' and '{2}'", "PaymentDate", fromDate, toDate);


            lstOtherPayment = new OtherPaymentManager().GetFilteredOtherPayment(filter).Cast<OtherPayment>().OrderBy(c => c.PaymentDate).ToList();
            lstOtherPayment = lstOtherPayment.Where(c => c.BranchID == MTBFConstants.AppConstants.BranchID && c.OrganizationID == MTBFConstants.AppConstants.OrganizationID).Cast<OtherPayment>().ToList();
            if (lstOtherPayment.Count > 0)
            {
                foreach (OtherPayment otherPayment in lstOtherPayment)
                {
                    grvOtherPaymentInformation.Rows.Add(otherPayment.OtherPaymentID, otherPayment.PaymentDate.ToShortDateString(), otherPayment.PartyName, otherPayment.ShortNote, otherPayment.Amount);
                }
            }
            else
            {
                MessageBoxHelper.ShowInformation("No data found for this combination.");
            }


        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            frmOtherPayment frm = new frmOtherPayment();
            frm.ShowDialog();
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            if (grvOtherPaymentInformation.SelectedRows.Count > 0)
            {
                int receiveID = Convert.ToInt32(grvOtherPaymentInformation.SelectedRows[0].Cells["ReceiveID"].Value);
                frmOtherPayment frm = new frmOtherPayment(receiveID, true);
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


    }
}
