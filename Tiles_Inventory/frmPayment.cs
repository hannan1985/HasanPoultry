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
using NybSys.MTBF.Utility.Enums;
using NybSys.MTBF.Utility.Helper;
using IFMS.BLL;
using Infragistics.Win.UltraWinGrid;

namespace Tiles_Inventory
{
    public partial class frmPayment : BaseForm
    {
        private bool _isUpdate = false;
        private int _paymentID = 0;
        private decimal AmountForUpdate = 0;

        public event OnPaymentInformationLoadEventHandler OnPaymentInformationLoad;
        public delegate void OnPaymentInformationLoadEventHandler();

        public frmPayment(bool isEdit, int sl)
        {
            if (isEdit)
            {
                _isUpdate = isEdit;
                _paymentID = sl;
            }
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }


        /// <summary>
        /// Method to reset all controls.
        /// </summary>
        private void ResetAllControls()
        {
            cmbSupplierName.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            txtAmount.Clear();
            cmbSupplierName.Focus();
            txtCashAmount.Clear();
            txtBankAmount.Clear();
            txtBankReferenceNo.Clear();
            txtDiscount.Clear();
            txtBillRefNumber.Clear();
            cmbPaymentMethod.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            cmbBankAccount.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            btnSave.Enabled = true;
            _isUpdate = false;
        }


        /// <summary>
        /// Method to load supplier information in combo box.
        /// </summary>
        private void LoadSupplierCombo()
        {
            List<Supplier> lstSupplier = new List<Supplier>();
            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("Name");


            lstSupplier = new SupplierManager().GetAllSupplierByBranchID(MTBFConstants.AppConstants.BranchID).Cast<Supplier>().ToList();

            DataRow emptyrow = table.NewRow();
            emptyrow[0] = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            emptyrow[1] = MTBFConstants.DataField.COMBO_DEFAULT_NAME;
            table.Rows.Add(emptyrow);

            if (lstSupplier.Count > 0)
            {
                foreach (Supplier supplier in lstSupplier)
                {
                    DataRow row = table.NewRow();
                    row[0] = supplier.SupplierID;
                    row[1] = supplier.SupplierName;
                    table.Rows.Add(row);
                }

                cmbSupplierName.ValueMember = "ID";
                cmbSupplierName.DisplayMember = "Name";
                cmbSupplierName.DataSource = table;
                cmbSupplierName.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;

            }

        }

        /// <summary>
        /// Method to load existing data for update.
        /// </summary>
        /// <remarks></remarks>
        private void LoadExistingData()
        {
            Payment payment = DataAccessProxy.GetPaymentInformationByID(_paymentID);
            if (payment != null)
            {
                AmountForUpdate = payment.Amount;

                cmbSupplierName.Value = payment.SupplierID;
                txtAmount.Text = AmountForUpdate.ToString();
                txtVoucherNumber.Text = payment.VoucherNumber.ToString();              
                dtpPaymentDate.Value = payment.PaymentDate;
                txtDiscount.Text = payment.Discount.ToString();
                cmbSupplierName.Value = payment.SupplierID;
                cmbBankAccount.Value = payment.BankAccountID;
                cbIsAdvance.Checked = payment.IsAdvance;
                cmbPaymentMethod.Value = payment.PaymentMethod;
                txtCashAmount.Text = payment.CashAmount.ToString();
                txtBankAmount.Text = payment.BankAmount.ToString();
                txtBankReferenceNo.Text = payment.BankReference;
                txtBillRefNumber.Text = payment.BillRefNumber;
            }
        }

        /// <summary>
        /// Method to update payment information.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private int UpdatePaymentInformation()
        {
            int result = 0;
            decimal DueAmount = Convert.ToDecimal(Label4.Text);
            decimal checkAmount = 0;
            Payment payment = DataAccessProxy.GetPaymentInformationByID(_paymentID);
            decimal discount = 0;
            decimal amount = 0;
            decimal cashAmount = 0;
            decimal bankAmount = 0;
            int bankAccountID = 0;

            int.TryParse(cmbBankAccount.Value.ToString(), out bankAccountID);
            decimal.TryParse(txtCashAmount.Text, out cashAmount);
            decimal.TryParse(txtBankAmount.Text, out bankAmount);
            decimal.TryParse(txtAmount.Text, out amount);
            decimal.TryParse(txtDiscount.Text, out discount);

            payment.CompanyID = 1;
            payment.SupplierID = Convert.ToInt32(cmbSupplierName.Value);
            payment.PaymentDate = dtpPaymentDate.Value;
            payment.VoucherNumber = Convert.ToInt32(txtVoucherNumber.Text);
            payment.IsAdvance = cbIsAdvance.Checked;
            payment.CashAmount = cashAmount;
            payment.BankAmount = bankAmount;
            payment.Amount = amount;
            payment.Discount = discount;
            payment.BankAccountID = bankAccountID;
            payment.BankReference = txtBankReferenceNo.Text;
            payment.PaymentMethod = Convert.ToInt32(cmbPaymentMethod.Value);

            result = DataAccessProxy.UpdatePayment(payment);

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

   
            return result;
        }

        /// <summary>
        /// Method to insert payment information.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private int InsertPaymentInformation()
        {
            int result = 0;
            decimal DueAmount = Convert.ToDecimal(Label4.Text);
            decimal ReceiveAmount = Convert.ToDecimal(txtAmount.Text);
            decimal cashAmount = 0;
            decimal bankAmount = 0;
            int bankAccountID = 0;
            decimal discount = 0;

            if (cmbBankAccount.Value != null)
            {
                int.TryParse(cmbBankAccount.Value.ToString(), out bankAccountID);
            }


            decimal.TryParse(txtCashAmount.Text, out cashAmount);
            decimal.TryParse(txtBankAmount.Text, out bankAmount);
            decimal.TryParse(txtDiscount.Text, out discount);
            Payment payment = new Payment();
            payment.VoucherNumber = Convert.ToInt32(txtVoucherNumber.Text);
            payment.CompanyID = 1; //Convert.ToInt32(cmbCompanyName.Value);
            payment.SupplierID = Convert.ToInt32(cmbSupplierName.Value);
            payment.PaymentDate = dtpPaymentDate.Value;

            payment.BranchID = MTBFConstants.AppConstants.BranchID;
            payment.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            payment.IsAdvance = cbIsAdvance.Checked;
            payment.CashAmount = cashAmount;
            payment.BankAmount = bankAmount;
            payment.Discount = discount;
            payment.Amount = Convert.ToDecimal(txtAmount.Text);
            payment.BankAccountID = bankAccountID;
            payment.BankReference = txtBankReferenceNo.Text;
            payment.PaymentMethod = Convert.ToInt32(cmbPaymentMethod.Value);


            result = DataAccessProxy.InsertPayment(payment);
            Label4.Text = (DueAmount - ReceiveAmount).ToString();


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
            if (string.IsNullOrEmpty(txtVoucherNumber.Text))
            {
                MessageBoxHelper.ShowInformation("You need to enter voucher number.");
                txtVoucherNumber.Focus();
                return false;
            }

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


            decimal DueAmount = Convert.ToDecimal(Label4.Text);
            decimal ReceiveAmount = Convert.ToDecimal(txtAmount.Text);
            if (!cbIsAdvance.Checked)
            {
                if (ReceiveAmount > DueAmount)
                {
                    MessageBox.Show("Please check your amount field", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
           

            return true;
        }

        /// <summary>
        /// Method to get actual due  amount which need to be paid.
        /// </summary>
        /// <remarks></remarks>
        private void GetTotalDueAmount(int companyID, int SupplierID)
        {
            decimal dueAmount = default(decimal);

            if (SupplierID > 0)
            {
                dueAmount = DataAccessProxy.GetDueAmountByCompanyAndSupplierID(companyID, SupplierID, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID);

                CustomerPreviousDue previousDue = new PreviousDueManager().GetSupplierPreviousDueBySupplierID(SupplierID);
                if (previousDue != null)
                {
                    dueAmount = dueAmount + previousDue.Amount;
                }

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



        private void Payment_Load(System.Object sender, System.EventArgs e)
        {
           
            LoadPaymentMethod();
            LoadSupplierCombo();
            LoadBankAccountCombo();
            if (_isUpdate)
            {
                LoadExistingData();
            }

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

        private void btnSave_Click(System.Object sender, System.EventArgs e)
        {
            if (ValidFormData())
            {
                if (_isUpdate)
                {
                    if (UpdatePaymentInformation() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        _isUpdate = false;

                        MessageBox.Show("Successfully Saved", "Pharmacy Manage", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetAllControls();

                        if (OnPaymentInformationLoad != null)
                        {
                            OnPaymentInformationLoad();
                        }
                    }

                    else
                    {
                        MessageBoxHelper.ShowError("Failed to save payment information.");
                    }
                }
                else
                {

                    if (InsertPaymentInformation() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBox.Show("Successfully Saved", "Pharmacy Manage", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetAllControls();

                        if (OnPaymentInformationLoad != null)
                        {
                            OnPaymentInformationLoad();
                        }
                    }
                    else
                    {
                        MessageBoxHelper.ShowError("Failed to save payment information.");
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
            int companyID = 0;
            int supplierID = 0;
            if (Convert.ToInt32(cmbSupplierName.Value) != MTBFConstants.DataField.COMBO_DEFAULT_ID)
            {
                int.TryParse(cmbSupplierName.Value.ToString(), out supplierID);
                if (supplierID > 0)
                {
                    GetTotalDueAmount(companyID, supplierID);
                }
                else
                {
                    MessageBoxHelper.ShowInformation("Invalid supplier selection.");
                    cmbSupplierName.Focus();
                }
            }
        }



        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ("1234567890.".IndexOf(e.KeyChar) > -1 | e.KeyChar == Convert.ToChar(8))
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

        private void UltraGroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void CalculateReceiveAmount()
        {
            decimal cashAmount = 0;
            decimal bankAmount = 0;

            decimal.TryParse(txtCashAmount.Text, out cashAmount);
            decimal.TryParse(txtBankAmount.Text, out bankAmount);

            txtAmount.Text = (cashAmount + bankAmount).ToString("F2");


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

        private void txtCashAmount_TextChanged(object sender, EventArgs e)
        {
            CalculateReceiveAmount();
        }

        private void txtBankAmount_TextChanged(object sender, EventArgs e)
        {
            CalculateReceiveAmount();
        }

    }
}
