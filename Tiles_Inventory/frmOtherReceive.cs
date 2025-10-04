using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NybSys.MTBF.Utility.Helper;
using NybSys.MTBF.Utility.Enums;
using IMFS.Common.DTO;
using IFMS.BLL;
using NybSys.MTBF.Utility.Constants;

namespace Tiles_Inventory
{
    public partial class frmOtherReceive : Form
    {
        int _receiveID = 0;
        bool isUpdating = false;
        List<OtherParty> _lstOtherParty = new List<OtherParty>();

        public frmOtherReceive()
        {
            InitializeComponent();
        }

        public frmOtherReceive(int receiveID, bool isEdit)
        {
            _receiveID = receiveID;
            isUpdating = isEdit;
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidFormData())
            {
                if (isUpdating)
                {
                    if (UpdateOtherRecive() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Successfully saved.");
                        ResetAllControls();
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save information.");
                    }

                }
                else
                {
                    if (InsertOtherRecive() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Successfully saved.");
                        ResetAllControls();
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save information.");
                    }

                }
            }
        }

        private int InsertOtherRecive()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            OtherReceive otherReceive = new OtherReceive();
            otherReceive.ReceiveAmount = Convert.ToDecimal(txtReceiveAmount.Text);
            otherReceive.OtherPartyID = Convert.ToInt32(cmbOtherParty.Value);
            otherReceive.ReceiveDate = dtpReceiveDate.Value;
            otherReceive.PartyName = cmbOtherParty.Text;
            otherReceive.ShortNote = txtShortNote.Text;
            otherReceive.BranchID = MTBFConstants.AppConstants.BranchID;
            otherReceive.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            result = new CashReceiveManager().InsertOtherReceive(otherReceive);
            return result;
        }

        private int UpdateOtherRecive()
        {

            string filter = string.Format("{0}={1}", "ReceiveID", _receiveID);
            OtherReceive otherReceive = new CashReceiveManager().GetFilteredOtherReceive(filter).FirstOrDefault();
            otherReceive.ReceiveAmount = Convert.ToDecimal(txtReceiveAmount.Text);
            otherReceive.ReceiveDate = dtpReceiveDate.Value;
            otherReceive.PartyName = cmbOtherParty.Text;
            otherReceive.OtherPartyID = Convert.ToInt32(cmbOtherParty.Value);
            otherReceive.ShortNote = txtShortNote.Text;
            otherReceive.BranchID = MTBFConstants.AppConstants.BranchID;
            otherReceive.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            
            return new CashReceiveManager().UpdateOtherReceive(otherReceive);
        }

        private bool ValidFormData()
        {
            decimal receiveAmount = 0;
            decimal.TryParse(txtReceiveAmount.Text, out receiveAmount);

            int otherPartyID = 0;

            if (cmbOtherParty.Value == null)
            {
                MessageBoxHelper.ShowInformation("You need to select other party.");
                cmbOtherParty.Focus();
                return false;
            }

            int.TryParse(cmbOtherParty.Value.ToString(), out otherPartyID);

            if (otherPartyID == 0)
            {
                MessageBoxHelper.ShowInformation("You need to select other party.");
                cmbOtherParty.Focus();
                return false;
            }

            if (receiveAmount == 0)
            {
                MessageBoxHelper.ShowInformation("You need to enter receive amount.");
                txtReceiveAmount.Focus();
                return false;
            }


            if (string.IsNullOrEmpty(txtShortNote.Text))
            {
                MessageBoxHelper.ShowInformation("You need to enter short note.");
                txtShortNote.Focus();
                return false;
            }


            return true;


        }

        private void ResetAllControls()
        {
            cmbOtherParty.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            txtReceiveAmount.Clear();
            txtShortNote.Clear();
            dtpReceiveDate.Value = DateTime.Now;
            isUpdating = false;
            _receiveID = 0;
        }

        private void frmOtherReceive_Load(object sender, EventArgs e)
        {
            LoadOtherPartyCombo();
            if (isUpdating)
            {
                LoadExistingRecord();
            }
        }

        private void LoadOtherPartyCombo()
        {
            string filter = string.Format("{0}={1}", "BranchID", MTBFConstants.AppConstants.BranchID);
            _lstOtherParty = new OtherPartyManager().GetFilterdOtherParty(filter);
            OtherParty otherParty = new OtherParty();
            otherParty.OtherPartyID = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            otherParty.PartyName = MTBFConstants.DataField.COMBO_DEFAULT_NAME;
            _lstOtherParty.Insert(0, otherParty);
            UIHelper.SetComboBoxData(cmbOtherParty, _lstOtherParty, "PartyName", "OtherPartyID");
        }

        private void LoadExistingRecord()
        {
            string filter = string.Format("{0}={1}", "ReceiveID", _receiveID);
            OtherReceive otherReceive = new CashReceiveManager().GetFilteredOtherReceive(filter).FirstOrDefault();
            if (otherReceive != null)
            {
                txtReceiveAmount.Text = otherReceive.ReceiveAmount.ToString();
                txtShortNote.Text = otherReceive.ShortNote;
                dtpReceiveDate.Value = otherReceive.ReceiveDate;
                cmbOtherParty.Value = otherReceive.OtherPartyID;
                _receiveID = otherReceive.ReceiveID;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ResetAllControls();
        }
    }
}
