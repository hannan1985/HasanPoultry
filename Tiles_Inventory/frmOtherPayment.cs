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
    public partial class frmOtherPayment : Form
    {
        int _receiveID = 0;
        bool isUpdating = false;
        List<OtherParty> _lstOtherParty = new List<OtherParty>();
        OtherPayment _otherPayment = new OtherPayment();

        public frmOtherPayment()
        {
            InitializeComponent();
        }

        public frmOtherPayment(int receiveID, bool isEdit)
        {
            _receiveID = receiveID;
            isUpdating = isEdit;
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidFormData())
            {
                if (InsertOtherPayment() == (int)MTBFEnums.ReturnResult.SUCCESS)
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

        private int InsertOtherPayment()
        {
            _otherPayment.Amount = Convert.ToDecimal(txtReceiveAmount.Text);
            _otherPayment.OtherPartyID = Convert.ToInt32(cmbOtherParty.Value);
            _otherPayment.PaymentDate = dtpReceiveDate.Value;
            _otherPayment.PartyName = cmbOtherParty.Text;
            _otherPayment.ShortNote = txtShortNote.Text;
            _otherPayment.BranchID = MTBFConstants.AppConstants.BranchID;
            _otherPayment.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            return new OtherPaymentManager().SaveOtherPayment(_otherPayment);
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
                MessageBoxHelper.ShowInformation("You need to enter payment amount.");
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
            _otherPayment = new OtherPayment();
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
            string filter = string.Format("{0}={1}", "OtherPaymentID", _receiveID);
            _otherPayment = new OtherPaymentManager().GetFilteredOtherPayment(filter).FirstOrDefault();
            if (_otherPayment != null)
            {
                txtReceiveAmount.Text = _otherPayment.Amount.ToString();
                txtShortNote.Text = _otherPayment.ShortNote;
                dtpReceiveDate.Value = _otherPayment.PaymentDate;
                cmbOtherParty.Value = _otherPayment.OtherPartyID;

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
