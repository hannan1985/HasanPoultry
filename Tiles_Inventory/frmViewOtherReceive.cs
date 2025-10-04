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
    public partial class frmViewOtherReceive : BaseForm
    {
        public frmViewOtherReceive()
        {
            InitializeComponent();
        }

        private void btView_Click(object sender, EventArgs e)
        {
            LoadOtherReceiveInformation();
        }

        private void LoadOtherReceiveInformation()
        {
            grvCashReceiveInformation.Rows.Clear();

            string fromDate = dtpFromDate.Value.ToString("yyyy/MM/dd") + MTBFConstants.DAY_START_TIME;
            string toDate = dtpToDate.Value.ToString("yyyy/MM/dd") + MTBFConstants.DAY_END_TIME;

            string filter = string.Format("{0} between '{1}' and '{2}'", "ReceiveDate", fromDate, toDate);

            List<OtherReceive> lstOtherReceive = new List<OtherReceive>();
            lstOtherReceive = new CashReceiveManager().GetFilteredOtherReceive(filter).Cast<OtherReceive>().OrderBy(c => c.ReceiveDate).ToList();
            lstOtherReceive = lstOtherReceive.Where(c => c.BranchID == MTBFConstants.AppConstants.BranchID && c.OrganizationID == MTBFConstants.AppConstants.OrganizationID).Cast<OtherReceive>().ToList();
            if (lstOtherReceive.Count > 0)
            {
                foreach (OtherReceive cashReceive in lstOtherReceive)
                {
                    grvCashReceiveInformation.Rows.Add(cashReceive.ReceiveID, cashReceive.ReceiveDate.ToShortDateString(), cashReceive.PartyName, cashReceive.ShortNote, cashReceive.ReceiveAmount);
                }
            }
            else
            {
                MessageBoxHelper.ShowInformation("No data found for this combination.");
            }


        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            frmOtherReceive frm = new frmOtherReceive();
            frm.ShowDialog();
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            if (grvCashReceiveInformation.SelectedRows.Count > 0)
            {
                int receiveID = Convert.ToInt32(grvCashReceiveInformation.SelectedRows[0].Cells["ReceiveID"].Value);
                frmOtherReceive frm = new frmOtherReceive(receiveID, true);
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
