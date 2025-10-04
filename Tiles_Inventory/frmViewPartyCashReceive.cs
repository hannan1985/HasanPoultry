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
    public partial class frmViewPartyCashReceive : BaseForm
    {
        private int _cashReceiveID = 0;

        public frmViewPartyCashReceive()
        {
            InitializeComponent();
           
        }

        private void btnView_Click(System.Object sender, System.EventArgs e)
        {
            LoadCashReceiveInformation();
        }

        private void btnAdd_Click(System.Object sender, System.EventArgs e)
        {
            frmCashPartyReceive frm = new frmCashPartyReceive(false, _cashReceiveID);
            frm.OnPartyReceiveInformationLoad += OnReceiveInforamtionUpdated;
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
                frmCashPartyReceive frm = new frmCashPartyReceive(true, _cashReceiveID);
                frm.OnPartyReceiveInformationLoad += OnReceiveInforamtionUpdated;
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

            List<PartyReceive> lstCashReceive = new List<PartyReceive>();
            lstCashReceive = new PartyReceiveManager().GetTotalPartyReceiveInformationByDate(dtpFromDate.Value.ToString("yyyy/MM/dd"), dtpToDate.Value.AddDays(1).ToString("yyyy/MM/dd"), MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<PartyReceive>().OrderBy(c => c.PartyID).ToList();

            if (lstCashReceive.Count > 0)
            {
                foreach (PartyReceive cashReceive in lstCashReceive)
                {
                    SalesParty customer = new StockSalesManager().GetSalesPartyByID(cashReceive.PartyID);
                    grvCashReceiveInformation.Rows.Add(cashReceive.CashReceiveID, customer.PartyName, cashReceive.ReceiveDate.ToShortDateString(), cashReceive.Amount);
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
