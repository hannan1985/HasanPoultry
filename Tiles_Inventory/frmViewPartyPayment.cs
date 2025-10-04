using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IFMS.Facade;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Constants;
using IFMS.BLL;

namespace Tiles_Inventory
{
    public partial class frmViewPartyPayment : BaseForm
    {
        private int _cashReceiveID = 0;

        public frmViewPartyPayment()
        {
            InitializeComponent();
        }

        private void btnView_Click(System.Object sender, System.EventArgs e)
        {
            LoadCashReceiveInformation();
        }

        private void btnAdd_Click(System.Object sender, System.EventArgs e)
        {
            frmMaterialPayment frm = new frmMaterialPayment(false, _cashReceiveID);
            frm.OnPartyPaymentInformationLoad += OnReceiveInforamtionUpdated;
            frm.ShowDialog();
        }

        private void OnReceiveInforamtionUpdated()
        {
            LoadCashReceiveInformation();
        }

        private void btnEdit_Click(System.Object sender, System.EventArgs e)
        {
            if (grvCashReceiveInformation.SelectedRows.Count > 0 && !grvCashReceiveInformation.SelectedRows[0].IsNewRow)
            {
                _cashReceiveID = Convert.ToInt32(grvCashReceiveInformation.SelectedRows[0].Cells[0].Value);
                frmMaterialPayment frm = new frmMaterialPayment(true, _cashReceiveID);
                frm.OnPartyPaymentInformationLoad += OnReceiveInforamtionUpdated;
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a row", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void btnClose_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Method to load cash receive information in grid.
        /// </summary>
        /// <remarks></remarks>
        private void LoadCashReceiveInformation()
        {
            grvCashReceiveInformation.Rows.Clear();

            List<PartyPayment> lstCashReceive = new List<PartyPayment>();

            string fromDate = dtpFromDate.Value.ToString("yyyy/MM/dd");
            string toDate = dtpToDate.Value.AddDays(1).ToString("yyyy/MM/dd");
            string filter = string.Format("{0} between '{1}' and '{2}' and {3}={4} and {5}={6}", "PaymentDate", fromDate, toDate, "BranchID", MTBFConstants.AppConstants.BranchID, "OrganizationID", MTBFConstants.AppConstants.OrganizationID);


            lstCashReceive = new PartyPaymentManager().GetFilteredPartyPayment(filter).Cast<PartyPayment>().OrderBy(c => c.SupplierID).ToList();

            if (lstCashReceive.Count > 0)
            {
                foreach (PartyPayment partyPayment in lstCashReceive)
                {
                    MaterialSupplier customer = new MaterialSupplierManager().GetMaterialSupplierByID(partyPayment.SupplierID);
                    grvCashReceiveInformation.Rows.Add(partyPayment.PaymentID, customer.SupplierName, partyPayment.PaymentDate.ToShortDateString(), partyPayment.Amount);
                }
            }
            else
            {
                MessageBox.Show("No data found for this combination.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void frmViewCashReceive_Load(System.Object sender, System.EventArgs e)
        {
            //  LoadCashReceiveInformation()
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCashReceiveInformation();
        }

        private void btnPrit_Click(object sender, EventArgs e)
        {

        }


    }
}
