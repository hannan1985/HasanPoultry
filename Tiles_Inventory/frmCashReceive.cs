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
using NybSys.MTBF.Utility.Helper;
using Infragistics.Win.UltraWinGrid;
using NybSys.MTBF.Utility.Enums;

namespace Tiles_Inventory
{
    public partial class frmCashReceive : BaseForm
    {
        private int _cashReceiveID = 0;
        private decimal PreviousAmount = 0;
        private decimal updateAmount = 0;
        private bool _isUpdate = false;

        public event OnCashReceiveInformationLoadEventHandler OnCashReceiveInformationLoad;
        public delegate void OnCashReceiveInformationLoadEventHandler();

        public frmCashReceive(bool isEdit, int sl)
        {
            if (isEdit)
            {
                _isUpdate = isEdit;
                _cashReceiveID = sl;
            }
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        private void frmCashReceiveInformation_Load(System.Object sender, System.EventArgs e)
        {
            this.LoadCustomerInformation();
            LoadPaymentMethod();
            LoadBankAccountCombo();
            cmbCustomerName.Enabled = true;
            dtpReceiveDate.Value = DateTime.Now;
            if (_isUpdate)
            {
                LoadExistingCashReceiveInformation();
            }
        }

        private void LoadPaymentMethod()
        {
            Dictionary<int, string> lstPaymentMethod = new Dictionary<int, string>();

            foreach (IFMSEnum.PaymentMethod enumValue in Enum.GetValues(typeof(IFMSEnum.PaymentMethod)))
            {
                lstPaymentMethod.Add((int)enumValue, enumValue.ToString());
            }

            cmbPaymentMethod.DisplayMember = "Value";
            cmbPaymentMethod.ValueMember = "Key";
            cmbPaymentMethod.DataSource = lstPaymentMethod;

            cmbPaymentMethod.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
            cmbPaymentMethod.DisplayLayout.PerformAutoResizeColumns(false, PerformAutoSizeType.AllRowsInBand, true);

        }

        private void cmbCustomerName_ValueChanged(System.Object sender, System.EventArgs e)
        {
            int customerID = 0;
            if (cmbCustomerName.Value != null)
            {
                int.TryParse(cmbCustomerName.Value.ToString(), out customerID);

                lblTotalAmount.Text = Math.Round(GetDueAmountByCustomerID(customerID)).ToString();
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
                    // txtReceiveAmount.Focus();
                    btnSave.Enabled = true;
                }
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

                    if (UpdateCashReceiveInformation() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        lblTotalAmount.Text = GetDueAmountByCustomerID(Convert.ToInt32(cmbCustomerName.Value)).ToString();
                        MessageBox.Show("Successfully saved.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetAllControls();
                        _isUpdate = false;

                        if (OnCashReceiveInformationLoad != null)
                        {
                            OnCashReceiveInformationLoad();
                        }
                        this.Close();
                    }
                }
                else
                {

                    if (InsertCashReceiveInformation() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        lblTotalAmount.Text = GetDueAmountByCustomerID(Convert.ToInt32(cmbCustomerName.Value)).ToString();
                        MessageBox.Show("Successfully saved.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetAllControls();
                        if (OnCashReceiveInformationLoad != null)
                        {
                            OnCashReceiveInformationLoad();
                        }
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
        public int InsertJournalInformationForCashReceive(int cashReceiveID)
        {
            int result = 0;
            int referenceID = 1;
            decimal amount = Convert.ToDecimal(txtReceiveAmount.Text);

            Customer customer = DataAccessProxy.GetCustomerByID(Convert.ToInt32(cmbCustomerName.Value));
            referenceID = DataAccessProxy.GetJournalReferenceID();

            for (int i = 0; i <= 1; i++)
            {
                IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();
                ChildAccount childAccount = DataAccessProxy.GetChildAccountByCustomerID(customer.CustomerID);

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
                journal.CashReceiveID = cashReceiveID;
                journal.BranchID = MTBFConstants.AppConstants.BranchID;
                journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                result = DataAccessProxy.InsertJournal(journal);
            }

            return result;
        }

        /// <summary>
        /// Method to update journal information.
        /// </summary>
        /// <param name="cashReceiveID"></param>
        /// <returns></returns>
        public int UpdateJournalInformationForCashReceive(int cashReceiveID)
        {
            int result = 0;
            List<IMFS.Common.DTO.Journal> lstJournalInformation = new List<IMFS.Common.DTO.Journal>();
            lstJournalInformation = DataAccessProxy.GetJournalByCashReceiveID(_cashReceiveID).Cast<IMFS.Common.DTO.Journal>().ToList();

            foreach (IMFS.Common.DTO.Journal journal in lstJournalInformation)
            {
                journal.Amount = Convert.ToDecimal(txtReceiveAmount.Text);
                result = DataAccessProxy.UpdateJournal(journal);
            }

            return result;
        }

        /// <summary>
        /// Method to load existing cash receive information.
        /// </summary>
        private void LoadExistingCashReceiveInformation()
        {
            CashReceive cashReceive = DataAccessProxy.GetCashReceiveByID(_cashReceiveID);

            dtpReceiveDate.Value = cashReceive.ReceiveDate;
            txtDiscount.Text = cashReceive.Discount.ToString();
            cmbCustomerName.Value = cashReceive.CustomerID;
            //   cmbCustomerName.Enabled = false;
            cmbBankAccount.Value = cashReceive.BankAccountID;
            cbIsAdvance.Checked = cashReceive.IsAdvance;
            txtReferenceNo.Text = cashReceive.ReferenceNo;
            cmbPaymentMethod.Value = cashReceive.PaymentMethod;
            txtCashAmount.Text = cashReceive.CashAmount.ToString();
            txtBankAmount.Text = cashReceive.BankAmount.ToString();
            txtBankReferenceNo.Text = cashReceive.BankReference;
            txtBillRefNumber.Text = cashReceive.BillRefNumber;
            updateAmount = cashReceive.Amount;
            txtReceiveAmount.Text = cashReceive.Amount.ToString();

        }

        /// <summary>
        /// Method to load coustomer information in combo box.
        /// </summary>
        /// <remarks></remarks>
        private void LoadCustomerInformation()
        {
            DataTable table = new DataTable();

            table.Columns.Add("ID");
            table.Columns.Add("Name");
            table.Columns.Add("Address");
            table.Columns.Add("Phone");
            List<Customer> lstCustomer = new List<Customer>();
            lstCustomer = new CustomerManager().GetAllCustomerByBranchID(MTBFConstants.AppConstants.BranchID).ToList();

            foreach (Customer customer in lstCustomer)
            {
                DataRow row = table.NewRow();
                row["ID"] = customer.CustomerID;
                row["Name"] = customer.CustomerName;
                row["Address"] = customer.Address;
                row["Phone"] = customer.Phone;
                table.Rows.Add(row);
            }

            cmbCustomerName.DisplayMember = "Name";
            cmbCustomerName.ValueMember = "ID";
            cmbCustomerName.DataSource = table;
            cmbCustomerName.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
            cmbCustomerName.DisplayLayout.PerformAutoResizeColumns(false, PerformAutoSizeType.AllRowsInBand, true);

        }


        private void LoadBankAccountCombo()
        {
            DataTable table = new DataTable();

            table.Columns.Add("ID");
            table.Columns.Add("Name");
            table.Columns.Add("Branch");

            List<BankAccount> lstBankAccount = new List<BankAccount>();
            lstBankAccount = new BankAccountManager().GetAllBankAccount().ToList();

            DataRow erow = table.NewRow();
            erow["ID"] = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            erow["Name"] = MTBFConstants.DataField.COMBO_DEFAULT_NAME;
            erow["Branch"] = string.Empty;
            table.Rows.Add(erow);

            foreach (BankAccount bankAccount in lstBankAccount)
            {
                DataRow row = table.NewRow();
                row["ID"] = bankAccount.BankAccountID;
                row["Name"] = bankAccount.BankName + " " + bankAccount.BankAccountNumber;
                row["Branch"] = bankAccount.Branch;
                table.Rows.Add(row);
            }

            cmbBankAccount.DisplayMember = "Name";
            cmbBankAccount.ValueMember = "ID";
            cmbBankAccount.DataSource = table;
            cmbBankAccount.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
            cmbBankAccount.DisplayLayout.PerformAutoResizeColumns(false, PerformAutoSizeType.AllRowsInBand, true);

        }

        /// <summary>
        /// Method to insert cash receive information.
        /// </summary>
        /// <returns></returns>
        private int InsertCashReceiveInformation()
        {
            int result = 0;
            decimal cashAmount = 0;
            decimal bankAmount = 0;

            decimal.TryParse(txtCashAmount.Text, out cashAmount);
            decimal.TryParse(txtBankAmount.Text, out bankAmount);
            int bankAccountID = 0;
            if (cmbBankAccount.Value != null)
            {
                int.TryParse(cmbBankAccount.Value.ToString(), out bankAccountID);
            }


            CashReceive cashReceive = new CashReceive();
            cashReceive.ReceiveDate = dtpReceiveDate.Value;
            cashReceive.Amount = Convert.ToDecimal(txtReceiveAmount.Text);
            cashReceive.CustomerID = Convert.ToInt32(cmbCustomerName.Value);
            cashReceive.Discount = string.IsNullOrEmpty(txtDiscount.Text) ? 0 : Convert.ToDecimal(txtDiscount.Text);
            cashReceive.ReferenceNo = txtReferenceNo.Text.Trim();
            cashReceive.BillRefNumber = txtBillRefNumber.Text;
            cashReceive.IsAdvance = cbIsAdvance.Checked;
            cashReceive.CashAmount = cashAmount;
            cashReceive.BankAmount = bankAmount;
            cashReceive.BankAccountID = bankAccountID;
            cashReceive.BankReference = txtBankReferenceNo.Text;
            cashReceive.PaymentMethod = Convert.ToInt32(cmbPaymentMethod.Value);
            cashReceive.BranchID = MTBFConstants.AppConstants.BranchID;
            cashReceive.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            result = DataAccessProxy.InsertCashReceive(cashReceive);
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
            decimal bankAmount = 0;
            actualAmount = GetActualReceiveAmount();

            decimal recevieAmount = 0;
            decimal cashAmount = 0;

            decimal.TryParse(txtReceiveAmount.Text, out recevieAmount);
            decimal.TryParse(txtBankAmount.Text, out bankAmount);

            decimal.TryParse(txtCashAmount.Text, out cashAmount);

            if (Convert.ToInt32(cmbPaymentMethod.Value) == (int)IFMSEnum.PaymentMethod.Bank)
            {
                if (bankAmount == 0)
                {
                    MessageBoxHelper.ShowInformation("You need to enter bank bank amount.");
                    txtBankAmount.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(txtBankReferenceNo.Text))
                {
                    MessageBoxHelper.ShowInformation("You need to enter bank rerence no.");
                    txtBankReferenceNo.Focus();
                    return false;
                }
            }
            else if (Convert.ToInt32(cmbPaymentMethod.Value) == (int)IFMSEnum.PaymentMethod.Both)
            {
                if (cashAmount == 0)
                {
                    MessageBoxHelper.ShowInformation("You need to enter cash amount.");
                    txtBankAmount.Focus();
                    return false;
                }

                if (bankAmount == 0)
                {
                    MessageBoxHelper.ShowInformation("You need to enter bank amount.");
                    txtBankAmount.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(txtBankReferenceNo.Text))
                {
                    MessageBoxHelper.ShowInformation("You need to enter bank rerence no.");
                    txtBankReferenceNo.Focus();
                    return false;
                }
            }

            if (recevieAmount == 0)
            {
                MessageBox.Show("Please check receive amount.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtReceiveAmount.Focus();
                return false;
            }

            if (!cbIsAdvance.Checked)
            {
                if (actualAmount > PreviousAmount)
                {
                    MessageBox.Show("Please check receive amount.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtReceiveAmount.Focus();
                    return false;
                }
            }


            if (txtReferenceNo.Text.Trim() == string.Empty)
            {
                MessageBox.Show("You need to enter memo number", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtReferenceNo.Focus();
                return false;
            }

            string filter = string.Format("{0}='{1}' and {2}={3}", "ReferenceNo", txtReferenceNo.Text, "BranchID", MTBFConstants.AppConstants.BranchID);

            CashReceive cashReceive = new CashReceiveManager().GetFilteredCashReceive(filter).FirstOrDefault();

            if (cashReceive != null && cashReceive.CashReceiveID != _cashReceiveID)
            {
                MessageBoxHelper.ShowInformation("Duplicate memo number!");
                txtReferenceNo.Focus();
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
        private int UpdateCashReceiveInformation()
        {
            int result = 0;
            decimal cashAmount = 0;
            decimal bankAmount = 0;
            int bankAccountID = 0;
            int.TryParse(cmbBankAccount.Value.ToString(), out bankAccountID);
            decimal.TryParse(txtCashAmount.Text, out cashAmount);
            decimal.TryParse(txtBankAmount.Text, out bankAmount);

            CashReceive cashReceive = DataAccessProxy.GetCashReceiveByID(_cashReceiveID);
            cashReceive.ReceiveDate = dtpReceiveDate.Value;

            cashReceive.CustomerID = Convert.ToInt32(cmbCustomerName.Value);
            cashReceive.Discount = string.IsNullOrEmpty(txtDiscount.Text) ? 0 : Convert.ToDecimal(txtDiscount.Text);
            cashReceive.ReferenceNo = txtReferenceNo.Text.Trim();
            cashReceive.BillRefNumber = txtBillRefNumber.Text;
            cashReceive.IsAdvance = cbIsAdvance.Checked;
            cashReceive.CashAmount = cashAmount;
            cashReceive.BankAmount = bankAmount;
            cashReceive.Amount = Convert.ToDecimal(txtReceiveAmount.Text);
            cashReceive.BankAccountID = bankAccountID;
            cashReceive.BankReference = txtBankReferenceNo.Text;
            cashReceive.PaymentMethod = Convert.ToInt32(cmbPaymentMethod.Value);
            result = DataAccessProxy.UpdateCashReceive(cashReceive);
            return result;
        }

        /// <summary>
        /// Method to reset all controls.
        /// </summary>
        private void ResetAllControls()
        {
            txtCashAmount.Clear();
            txtBankAmount.Clear();
            txtReceiveAmount.Clear();
            txtBankReferenceNo.Clear();
            txtDiscount.Clear();
            txtBillRefNumber.Clear();
            cmbPaymentMethod.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            cmbBankAccount.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
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
            dueAmount = DataAccessProxy.GetCustomerDueAmountByCustomerID(customerId, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID);
            return dueAmount;
        }

        private void txtCashAmount_TextChanged(object sender, EventArgs e)
        {
            CalculateReceiveAmount();
        }


        private void CalculateReceiveAmount()
        {
            decimal cashAmount = 0;
            decimal bankAmount = 0;

            decimal.TryParse(txtCashAmount.Text, out cashAmount);
            decimal.TryParse(txtBankAmount.Text, out bankAmount);

            txtReceiveAmount.Text = (cashAmount + bankAmount).ToString("F2");


        }

        private void txtBankAmount_TextChanged(object sender, EventArgs e)
        {
            CalculateReceiveAmount();
        }

        private void cmbPaymentMethod_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbPaymentMethod.Value) == (int)IFMSEnum.PaymentMethod.Cash)
            {
                txtBankAmount.Enabled = false;
                txtBankReferenceNo.Enabled = false;
                txtCashAmount.Enabled = true;
                cmbBankAccount.Enabled = false;
                txtBankAmount.Clear();
                txtBankReferenceNo.Clear();
            }
            else if (Convert.ToInt32(cmbPaymentMethod.Value) == (int)IFMSEnum.PaymentMethod.Bank)
            {
                txtBankAmount.Enabled = true;
                txtBankReferenceNo.Enabled = true;
                cmbBankAccount.Enabled = true;
                txtCashAmount.Enabled = false;
                txtCashAmount.Clear();

            }
            else if (Convert.ToInt32(cmbPaymentMethod.Value) == (int)IFMSEnum.PaymentMethod.Both)
            {
                txtBankAmount.Enabled = true;
                txtBankReferenceNo.Enabled = true;
                cmbBankAccount.Enabled = true;
                txtCashAmount.Enabled = true;
            }
        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

    }
}
