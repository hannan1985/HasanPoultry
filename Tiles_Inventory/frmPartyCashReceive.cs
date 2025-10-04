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
using IMFS.Common.Utility;
using NybSys.MTBF.Utility.Constants;
using IFMS.BLL;
using NybSys.MTBF.Utility.Enums;
using NybSys.MTBF.Utility.Helper;

namespace Tiles_Inventory
{
    public partial class frmCashPartyReceive : BaseForm
    {
        private int _PartyReceiveID = 0;
        private decimal PreviousAmount = 0;
        private decimal updateAmount = 0;
        private bool _isUpdate = false;

        public event OnPartyReceiveInformationLoadEventHandler OnPartyReceiveInformationLoad;
        public delegate void OnPartyReceiveInformationLoadEventHandler();

        public frmCashPartyReceive(bool isEdit, int sl)
        {
            if (isEdit)
            {
                _isUpdate = isEdit;
                _PartyReceiveID = sl;
            }
            DataAccessProxy = new FacadeManager();

            InitializeComponent();
        }



        private void cmbCustomerName_ValueChanged(System.Object sender, System.EventArgs e)
        {
            int customerID = 0;
            int.TryParse(cmbCustomerName.Value.ToString(), out customerID);

            lblTotalAmount.Text = GetDueAmountByCustomerID(customerID).ToString();
            PreviousAmount = Convert.ToDecimal(lblTotalAmount.Text);

            if (lblTotalAmount.Text == "0" | lblTotalAmount.Text == "0.00")
            {
                lblTotalAmount.ForeColor = Color.Green;
                // txtReceiveAmount.Enabled = False
                //btnSave.Enabled = False
            }
            else
            {
                lblTotalAmount.ForeColor = Color.Red;
                txtReceiveAmount.Enabled = true;
                txtReceiveAmount.Focus();
                btnSave.Enabled = true;
            }

        }

        private void btnClose_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(System.Object sender, System.EventArgs e)
        {
            if (ValidFormData())
            {

                if (_isUpdate)
                {
                    if (UpdatePartyReceiveInformation() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        lblTotalAmount.Text = GetDueAmountByCustomerID(Convert.ToInt32(cmbCustomerName.Value)).ToString();
                        MessageBox.Show("Successfully saved.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetAllControls();
                        _isUpdate = false;
                        if (OnPartyReceiveInformationLoad != null)
                        {
                            OnPartyReceiveInformationLoad();
                        }
                    }
                    else
                    {
                        MessageBoxHelper.ShowError("Failed to save receive information.");
                    }
                }
                else
                {
                    if (InsertPartyReceiveInformation() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        lblTotalAmount.Text = GetDueAmountByCustomerID(Convert.ToInt32(cmbCustomerName.Value)).ToString();
                        MessageBox.Show("Successfully saved.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetAllControls();
                        if (OnPartyReceiveInformationLoad != null)
                        {
                            OnPartyReceiveInformationLoad();
                        }
                    }
                    else
                    {
                        MessageBoxHelper.ShowError("Failed to save receive information.");
                    }
                }
            }
        }


        private void btnWholeSales_Click(System.Object sender, System.EventArgs e)
        {
            ResetAllControls();
        }

        private void txtReceiveAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ("1234567890".IndexOf(e.KeyChar) > -1 | e.KeyChar == Convert.ToChar(8))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Method to insert journal information after cash receive
        /// </summary>
        /// <returns></returns>
        public int InsertJournalInformationForPartyReceive(int partyReceiveID)
        {
            int result = 0;
            int referenceID = 1;
            decimal amount = Convert.ToDecimal(txtReceiveAmount.Text);

            SalesParty customer = new StockSalesManager().GetSalesPartyByID(Convert.ToInt32(cmbCustomerName.Value));
            referenceID = new JournalAccountsManager().GetJournalReferenceID();

            for (int i = 0; i <= 1; i++)
            {
                IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();
                ChildAccount childAccount = new ChildAccountManager().GetChildAccountByPartyID(customer.SalesPartyID);

                if (i == 0)
                {
                    journal.DrCrIndecator = "Dr";
                    journal.AccountID = IFMSConstant.CashAccountID;

                }
                else
                {
                    journal.DrCrIndecator = "Cr";
                    journal.AccountID = childAccount.AccountID;
                    journal.ChildAccountID = childAccount.ChildAccountID;
                }

                journal.JAccountID = 0;
                journal.Amount = amount;
                journal.ReferenceNo = referenceID;
                journal.PartyReceiveID = partyReceiveID;
                journal.BranchID = MTBFConstants.AppConstants.BranchID;
                journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                result = new JournalManager().InsertJournal(journal);
            }

            return result;
        }

        /// <summary>
        /// Method to update journal information.
        /// </summary>
        /// <param name="PartyReceiveID"></param>
        /// <returns></returns>
        public int UpdateJournalInformationForPartyReceive(int PartyReceiveID)
        {
            int result = 0;
            List<IMFS.Common.DTO.Journal> lstJournalInformation = new List<IMFS.Common.DTO.Journal>();

            lstJournalInformation = new JournalManager().GetJournalByPartyReceiveID(_PartyReceiveID).Cast<IMFS.Common.DTO.Journal>().ToList();

            foreach (IMFS.Common.DTO.Journal journal in lstJournalInformation)
            {
                journal.Amount = Convert.ToDecimal(txtReceiveAmount.Text);
                result = new JournalManager().UpdateJournal(journal);
            }

            return result;
        }

        /// <summary>
        /// Method to load existing cash receive information.
        /// </summary>
        private void LoadExistingPartyReceiveInformation()
        {
            PartyReceive partyReceive = new PartyReceiveManager().GetPartyReceiveByID(_PartyReceiveID);

            txtReceiveAmount.Text = partyReceive.Amount.ToString();
            dtpReceiveDate.Value = partyReceive.ReceiveDate;
            txtDiscount.Text = partyReceive.Discount.ToString();
            cmbCustomerName.Value = partyReceive.PartyID;
            cmbCustomerName.Enabled = false;
            txtReferenceNo.Text = partyReceive.ReferenceNo;
            updateAmount = partyReceive.Amount;
        }

        /// <summary>
        /// Method to load coustomer information in combo box.
        /// </summary>
        /// <remarks></remarks>
        private void LoadPartyCombo()
        {
            DataTable table = new DataTable();

            table.Columns.Add("ID");
            table.Columns.Add("Name");

            List<SalesParty> lstCustomer = new List<SalesParty>();
            lstCustomer = new StockSalesManager().GetAllSalesParty().Cast<SalesParty>().Where(c => c.BranchID == MTBFConstants.AppConstants.BranchID && c.OrganizationID == MTBFConstants.AppConstants.OrganizationID).ToList();

            foreach (SalesParty customer in lstCustomer)
            {
                DataRow row = table.NewRow();
                row[0] = customer.SalesPartyID;
                row[1] = customer.PartyName;
                table.Rows.Add(row);
            }

            cmbCustomerName.DisplayMember = "Name";
            cmbCustomerName.ValueMember = "ID";
            cmbCustomerName.DataSource = table;
        }

        /// <summary>
        /// Method to insert cash receive information.
        /// </summary>
        /// <returns></returns>
        private int InsertPartyReceiveInformation()
        {
            int result = 0;
            PartyReceive partyReceive = new PartyReceive();
            partyReceive.ReceiveDate = dtpReceiveDate.Value;
            partyReceive.Amount = Convert.ToDecimal(txtReceiveAmount.Text);
            partyReceive.PartyID = Convert.ToInt32(cmbCustomerName.Value);
            partyReceive.ReferenceNo = txtReferenceNo.Text.Trim();
            partyReceive.BranchID = MTBFConstants.AppConstants.BranchID;
            partyReceive.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            result = new PartyReceiveManager().InsertPartyReceive(partyReceive);
            return result;
        }

        /// <summary>
        /// Method to valid form data.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool ValidFormData()
        {
            PreviousAmount = GetDueAmountByCustomerID(Convert.ToInt32(cmbCustomerName.Value));
            decimal actualAmount = 0;
            actualAmount = GetActualReceiveAmount();

            if (txtReceiveAmount.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please check receive amount.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtReceiveAmount.Focus();
                return false;
            }


            if (actualAmount > PreviousAmount)
            {
                MessageBox.Show("Please check receive amount.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtReceiveAmount.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Method to get actual receive amount.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private decimal GetActualReceiveAmount()
        {
            decimal presentAmount = 0;

            decimal.TryParse(txtReceiveAmount.Text.Trim(), out presentAmount);
            decimal actualAmount = 0;

            if (_isUpdate)
            {
                if (updateAmount > presentAmount)
                {
                    actualAmount = 0;
                }
                else if (presentAmount > updateAmount)
                {
                    actualAmount = presentAmount - updateAmount;
                }
                else if (presentAmount == updateAmount)
                {
                    actualAmount = 0;
                }
            }
            else
            {
                actualAmount = presentAmount;
            }

            return actualAmount;
        }

        /// <summary>
        /// Method to update cash receive information.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private int UpdatePartyReceiveInformation()
        {
            int result = 0;
            PartyReceive partyReceive = new PartyReceiveManager().GetPartyReceiveByID(_PartyReceiveID);
            partyReceive.ReceiveDate = dtpReceiveDate.Value;
            partyReceive.Amount = Convert.ToDecimal(txtReceiveAmount.Text);
            partyReceive.PartyID = Convert.ToInt32(cmbCustomerName.Value);
            partyReceive.Discount = string.IsNullOrEmpty(txtDiscount.Text) ? 0 : Convert.ToDecimal(txtDiscount.Text);
            partyReceive.ReferenceNo = txtReferenceNo.Text.Trim();
            result = new PartyReceiveManager().UpdatePartyReceive(partyReceive);
            return result;
        }

        /// <summary>
        /// Method to reset all controls.
        /// </summary>
        private void ResetAllControls()
        {
            cmbCustomerName.Enabled = true;
            txtReceiveAmount.Clear();
            btnSave.Enabled = true;
            _isUpdate = false;
        }

        /// <summary>
        /// Method to get due amount by customer id.
        /// </summary>
        /// <remarks></remarks>
        private decimal GetDueAmountByCustomerID(int customerId)
        {
            decimal dueAmount = default(decimal);
            dueAmount = new PartyReceiveManager().GetPartyDueAmountByPartyID(customerId, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID);
            return dueAmount;
        }

        private void frmCashPartyReceive_Load(object sender, EventArgs e)
        {
            this.LoadPartyCombo();
            cmbCustomerName.Enabled = true;
            if (_isUpdate)
            {
                LoadExistingPartyReceiveInformation();
            }
        }


    }
}
