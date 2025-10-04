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
using IMFS.Common.View;
using IMFS.Common.Utility;
using NybSys.MTBF.Utility.Constants;
using IFMS.BLL;
using NybSys.MTBF.Utility.Enums;
using NybSys.MTBF.Utility.Helper;

namespace Tiles_Inventory
{
    public partial class frmMaterialPayment : BaseForm
    {
        private bool _isUpdate = false;
        private int _PartyPaymentID = 0;
        private decimal AmountForUpdate = 0;

        public event OnPartyPaymentInformationLoadEventHandler OnPartyPaymentInformationLoad;
        public delegate void OnPartyPaymentInformationLoadEventHandler();

        public frmMaterialPayment(bool isEdit, int sl)
        {
            if (isEdit)
            {
                _isUpdate = isEdit;
                _PartyPaymentID = sl;
            }
            InitializeComponent();

        }


        /// <summary>
        /// Method to reset all controls.
        /// </summary>
        private void ResetAllControls()
        {
            cmbSupplierName.Value = -1;
            txtAmount.Clear();
            cmbSupplierName.Focus();
        }



        /// <summary>
        /// Method to load MaterialSupplier information in combo box.
        /// </summary>
        private void LoadMaterialSupplierCombo()
        {
            List<MaterialSupplier> lstMaterialSupplier = new List<MaterialSupplier>();
            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("Name");

            lstMaterialSupplier = new MaterialSupplierManager().GetAllMaterialSupplier().Where(m => m.BranchID == MTBFConstants.AppConstants.BranchID && m.OrganizationID == MTBFConstants.AppConstants.OrganizationID).Cast<MaterialSupplier>().ToList();

            DataRow emptyrow = table.NewRow();
            emptyrow[0] = -1;
            emptyrow[1] = "-Select-";
            table.Rows.Add(emptyrow);

            if (lstMaterialSupplier.Count > 0)
            {
                foreach (MaterialSupplier materialSupplier in lstMaterialSupplier)
                {
                    DataRow row = table.NewRow();
                    row[0] = materialSupplier.SupplierID;
                    row[1] = materialSupplier.SupplierName;
                    table.Rows.Add(row);
                }

                cmbSupplierName.ValueMember = "ID";
                cmbSupplierName.DisplayMember = "Name";
                cmbSupplierName.DataSource = table;
                cmbSupplierName.Value = -1;
            }

        }

        /// <summary>
        /// Method to load existing data for update.
        /// </summary>
        /// <remarks></remarks>
        private void LoadExistingData()
        {
            PartyPayment PartyPayment = new PartyPaymentManager().GetPartyPaymentByID(_PartyPaymentID);
            if (PartyPayment != null)
            {
                AmountForUpdate = PartyPayment.Amount;

                cmbSupplierName.Value = PartyPayment.SupplierID;
                txtAmount.Text = AmountForUpdate.ToString();

            }
        }

        /// <summary>
        /// Method to update PartyPayment information.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private int UpdatePartyPaymentInformation()
        {
            int result = 0;
            decimal DueAmount = Convert.ToDecimal(Label4.Text);
            decimal checkAmount = 0;
            PartyPayment PartyPayment = new PartyPaymentManager().GetPartyPaymentByID(_PartyPaymentID);

            if (AmountForUpdate <= Convert.ToDecimal(txtAmount.Text))
            {
                decimal ActualAmount = Convert.ToDecimal(txtAmount.Text) - AmountForUpdate;

                if (ActualAmount > DueAmount)
                {
                    MessageBox.Show("Please check your amount field", "Pharmacy Manage", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    txtAmount.Focus();
                    return result;
                }
            }


            PartyPayment.SupplierID = Convert.ToInt32(cmbSupplierName.Value);
            PartyPayment.PaymentDate = dtpPaymentDate.Value;
            PartyPayment.Amount = Convert.ToDecimal(txtAmount.Text);
            result = new PartyPaymentManager().UpdatePartyPayment(PartyPayment);

            checkAmount = Convert.ToDecimal(txtAmount.Text);

            if (AmountForUpdate > checkAmount)
            {
                AmountForUpdate = AmountForUpdate - checkAmount;
                Label4.Text = (DueAmount + AmountForUpdate).ToString();
            }
            else if (AmountForUpdate < checkAmount)
            {
                AmountForUpdate = checkAmount - AmountForUpdate;
                Label4.Text = (DueAmount - AmountForUpdate).ToString();
            }
            else
            {
                AmountForUpdate = 0;
                Label4.Text = Label4.Text;
            }

            btnRefresh.Enabled = false;

            return result;
        }

        /// <summary>
        /// Method to insert PartyPayment information.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private int InsertPartyPaymentInformation()
        {
            int functionReturnValue = 0;
            int result = 0;
            decimal DueAmount = Convert.ToDecimal(Label4.Text);
            decimal ReceiveAmount = Convert.ToDecimal(txtAmount.Text);

            if (ReceiveAmount > DueAmount)
            {
                MessageBox.Show("Please check your amount field", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return functionReturnValue;
            }
            else
            {
                PartyPayment partyPayment = new PartyPayment();

                partyPayment.SupplierID = Convert.ToInt32(cmbSupplierName.Value);
                partyPayment.PaymentDate = dtpPaymentDate.Value;
                partyPayment.Amount = Convert.ToDecimal(txtAmount.Text);
                partyPayment.BranchID = MTBFConstants.AppConstants.BranchID;
                partyPayment.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                result = new PartyPaymentManager().InsertPartyPayment(partyPayment);
                Label4.Text = (DueAmount - ReceiveAmount).ToString();
            }

            return result;
        }

        /// <summary>
        /// Method to validate form data.
        /// </summary>
        /// <param name="vehicleNumber"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool ValidFormData()
        {
            if (Convert.ToInt32(cmbSupplierName.Value) == -1)
            {
                cmbSupplierName.Focus();
                MessageBox.Show("Please enter all required data.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (txtAmount.Text.Trim() == string.Empty)
            {
                txtAmount.Focus();
                MessageBox.Show("Please enter all required data.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (decimal.Parse(txtAmount.Text) == 0)
            {
                txtAmount.Focus();
                MessageBox.Show("Please enter all required data.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Method to get actual due  amount which need to be paid.
        /// </summary>
        /// <remarks></remarks>
        private void GetTotalDueAmount(int supplierID)
        {
            decimal dueAmount = default(decimal);

            if (supplierID > 0)
            {
                dueAmount = new PartyPaymentManager().GetDueAmountByMaterialSupplierID(supplierID);

                Label4.Text = dueAmount.ToString();

                if (!_isUpdate)
                {
                    if (Label4.Text == "0.00")
                    {
                        txtAmount.Enabled = false;
                    }
                    else
                    {
                        txtAmount.Enabled = true;
                        txtAmount.Focus();
                    }
                }
            }

        }

        /// <summary>
        /// Method to insert journal information after cash receive
        /// </summary>
        /// <returns></returns>
        public int InsertJournalInformationForCashPartyPayment(int PartyPaymentID)
        {
            int result = 0;
            int referenceID = 1;
            decimal amount = Convert.ToDecimal(txtAmount.Text);

            MaterialSupplier materialSupplier = new MaterialSupplierManager().GetMaterialSupplierByID(Convert.ToInt32(cmbSupplierName.Value));
            //  UserSettings userSetting = new PartyPaymentManager().GetUserSettingsByEmployeeID(IFMSConstant.LoggedinUserID);

            referenceID = DataAccessProxy.GetJournalReferenceID();

            for (int i = 0; i <= 1; i++)
            {
                IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();
                ChildAccount childAccount = new ChildAccountManager().GetChildAccountByMaterialSupplierID(materialSupplier.SupplierID);

                if (i == 0)
                {
                    journal.DrCrIndecator = "Dr";
                    journal.AccountID = childAccount.AccountID;
                    journal.ChildAccountID = childAccount.ChildAccountID;
                }
                else
                {
                    journal.DrCrIndecator = "Cr";
                    journal.AccountID = IFMSConstant.CashAccountID;

                }

                journal.JAccountID = 0;
                journal.Amount = amount;
                journal.ReferenceNo = referenceID;
                journal.PaymentID = PartyPaymentID;
                journal.BranchID = MTBFConstants.AppConstants.BranchID;
                journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                result = DataAccessProxy.InsertJournal(journal);
            }

            return result;
        }

        /// <summary>
        /// Method to update journal information.
        /// </summary>
        /// <param name="PartyPaymentID"></param>
        /// <returns></returns>
        public int UpdateJournalInformationForCashPartyPayment(int PartyPaymentID)
        {
            int result = 0;
            string indicator = String.Empty;
            List<IMFS.Common.DTO.Journal> lstJournalInformation = new List<IMFS.Common.DTO.Journal>();
            try
            {
                lstJournalInformation = new JournalManager().GetJournalByPartyPaymentID(PartyPaymentID).Cast<IMFS.Common.DTO.Journal>().ToList();

                foreach (IMFS.Common.DTO.Journal journal in lstJournalInformation)
                {
                    journal.Amount = Convert.ToDecimal(txtAmount.Text);
                    result = DataAccessProxy.UpdateJournal(journal);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save information.", "INFORMATION ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                result = 0;
            }

            return result;
        }

        private void PartyPayment_Load(System.Object sender, System.EventArgs e)
        {
            btnRefresh.Enabled = false;
            LoadMaterialSupplierCombo();

            if (_isUpdate)
            {
                LoadExistingData();
            }

        }

        private void btnSave_Click(System.Object sender, System.EventArgs e)
        {
            if (ValidFormData())
            {
                if (_isUpdate)
                {
                    if (UpdatePartyPaymentInformation() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        _isUpdate = false;

                        MessageBoxHelper.ShowInformation("Payment information saved successfully.");
                        ResetAllControls();

                        if (OnPartyPaymentInformationLoad != null)
                        {
                            OnPartyPaymentInformationLoad();
                        }
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save payment information.");
                    }
                }
                else
                {
                    if (InsertPartyPaymentInformation() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Payment information saved successfully.");
                        ResetAllControls();

                        if (OnPartyPaymentInformationLoad != null)
                        {
                            OnPartyPaymentInformationLoad();
                        }
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save payment information.");
                    }
                }
            }
        }

        private void btnClose_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void cmbSupplierName_SelectedValueChanged(System.Object sender, System.EventArgs e)
        {
            if (Convert.ToInt32(cmbSupplierName.Value) != -1)
            {
                GetTotalDueAmount(Convert.ToInt32(cmbSupplierName.Value));
            }
        }



        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ResetAllControls();
        }

    }
}
