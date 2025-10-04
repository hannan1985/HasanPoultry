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
using NybSys.MTBF.Utility.Enums;
using Infragistics.Win.UltraWinGrid;
using IMFS.Common.Utility;



namespace Tiles_Inventory
{
    public partial class frmExpense : BaseForm
    {
        private int _expenseID;
        private bool _isUpdate = false;

        public event OnExpenseInformationLoadEventHandler OnExpenseInformationLoad;
        public delegate void OnExpenseInformationLoadEventHandler();
        List<ChildAccount> lstChildAccount = new List<ChildAccount>();

        public frmExpense(bool isEdit, int sl)
        {
            if (isEdit)
            {
                _isUpdate = isEdit;
                _expenseID = sl;
            }
            InitializeComponent();
        }

        /// <summary>
        /// Method to load employee infromation in combo box.
        /// </summary>
        /// <remarks></remarks>
        private void LoadEmployeeInformation()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("Name");
            List<Employee> lstEmployee = new List<Employee>();
            lstEmployee = new EmployeeManager().GetAllEmployee().Cast<Employee>().ToList();

            DataRow dtRow = table.NewRow();
            dtRow[0] = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            dtRow[1] = MTBFConstants.DataField.COMBO_DEFAULT_NAME;
            table.Rows.Add(dtRow);

            foreach (Employee employee in lstEmployee)
            {
                DataRow Row = table.NewRow();
                Row[0] = employee.EmployeeID;
                Row[1] = employee.EmployeeName;
                table.Rows.Add(Row);
            }


            cmbEmployeeName.DisplayMember = "Name";
            cmbEmployeeName.ValueMember = "ID";
            cmbEmployeeName.DataSource = table;
            cmbEmployeeName.Value = -1;
            cmbEmployeeName.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
            cmbEmployeeName.DisplayLayout.PerformAutoResizeColumns(false, PerformAutoSizeType.AllRowsInBand, true);
        }



        private void LoadExpenseTypeCombo()
        {
            List<ExpenseType> lstExpenseType = new ExpenseManager().GetAllExpenseType();
            ExpenseType expenseType = new ExpenseType();
            expenseType.ExpenseTypeID = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            expenseType.ExpenseTypeName = MTBFConstants.DataField.COMBO_DEFAULT_NAME;
            lstExpenseType.Add(expenseType);

            UIHelper.SetComboBoxData(cmbExpenseType, lstExpenseType, "ExpenseTypeName", "ExpenseTypeID");


        }

        /// <summary>
        /// Method to load expense information.
        /// </summary>
        /// <remarks></remarks>
        private void LoadExpenseInformation()
        {
            Expense expense = new ExpenseManager().GetExpenseByID(_expenseID);

            cmbPaymentMethod.Value = expense.PaymentMethod;
            txtCashAmount.Text = expense.CashAmount.ToString();
            txtBankAmount.Text = expense.BankAmount.ToString();
            txtBankReferenceNo.Text = expense.BankReference;
            cmbBankAccount.Value = expense.BankAccountID;
            cmbCashAccount.Value = expense.CashAccountID;
            dtpDate.Value = expense.ExpenseDate;
            cmbEmployeeName.Value = expense.EmployeeID;
            txtExpenseDescription.Text = expense.Description;
            cmbExpenseType.Value = expense.ExpenseType;
            txtAmount.Text = expense.Amount.ToString();
        }

        /// <summary>
        /// Method to valid form data.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool ValidFormData()
        {

            if (Convert.ToInt32(cmbEmployeeName.Value) == -1 || cmbEmployeeName.Text.Trim() == string.Empty)
            {
                MessageBoxHelper.ShowInformation("You need to select employee name.");
                cmbEmployeeName.Focus();
                return false;
            }
            if (cmbExpenseType.Value == null || Convert.ToInt32(cmbExpenseType.Value) == MTBFConstants.DataField.COMBO_DEFAULT_ID)
            {
                MessageBoxHelper.ShowInformation("You need to select expense type");
                cmbExpenseType.Focus();
                return false;
            }
            if (txtAmount.Text == string.Empty)
            {
                MessageBoxHelper.ShowInformation("You need to enter amount.");
                txtAmount.Focus();
                return false;
            }

            if (Convert.ToDecimal(txtAmount.Text) == 0)
            {
                MessageBoxHelper.ShowInformation("Amount can't be zero.");
                txtAmount.Focus();
                return false;
            }


            return true;
        }

        /// <summary>
        /// Method to insert expense information.
        /// </summary>
        /// <remarks></remarks>
        private int InsertExpenseInformation()
        {
            int result = 0;

            try
            {
                int bankAccountID = 0;
                int cashAccountID = 0;
                int paymentMethod = 0;
                decimal cashAmount = 0;
                decimal bankAmount = 0;
                decimal.TryParse(txtCashAmount.Text, out cashAmount);
                decimal.TryParse(txtBankAmount.Text, out bankAmount);
                int.TryParse(cmbPaymentMethod.Value.ToString(), out paymentMethod);
                int.TryParse(cmbCashAccount.Value.ToString(), out cashAccountID);
                if (cmbBankAccount.Value != null)
                {
                    int.TryParse(cmbBankAccount.Value.ToString(), out bankAccountID);
                }
                Expense expense = new Expense();

                expense.EmployeeID = Convert.ToInt32(cmbEmployeeName.Value);
                expense.ExpenseType = Convert.ToInt32(cmbExpenseType.Value);
                expense.Description = txtExpenseDescription.Text;
                expense.CashAmount = cashAmount;
                expense.BankAmount = bankAmount;
                expense.CashAccountID = cashAccountID;
                expense.PaymentMethod = paymentMethod;
                expense.BankAccountID = bankAccountID;
                expense.BankReference = txtBankReferenceNo.Text;
                expense.Amount = Convert.ToDecimal(txtAmount.Text);
                expense.BranchID = MTBFConstants.AppConstants.BranchID;
                expense.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                expense.ExpenseDate = dtpDate.Value;
                expense.ApprovedDate = DateTime.Now;

                result = new ExpenseManager().InsertExpense(expense);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save information.", "Transport Management ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return result;
            }
            return result;
        }

        /// <summary>
        /// Method to update expense information.
        /// </summary>
        /// <returns></returns>
        private int UpdateExpenseInformation()
        {
            int result = 0;

            try
            {
                int bankAccountID = 0;
                int cashAccountID = 0;
                int paymentMethod = 0;
                decimal cashAmount = 0;
                decimal bankAmount = 0;
                decimal.TryParse(txtCashAmount.Text, out cashAmount);
                decimal.TryParse(txtBankAmount.Text, out bankAmount);
                int.TryParse(cmbPaymentMethod.Value.ToString(), out paymentMethod);
                int.TryParse(cmbCashAccount.Value.ToString(), out cashAccountID);
                if (cmbBankAccount.Value != null)
                {
                    int.TryParse(cmbBankAccount.Value.ToString(), out bankAccountID);
                }

                Expense expense = new ExpenseManager().GetExpenseByID(_expenseID);

                expense.EmployeeID = Convert.ToInt32(cmbEmployeeName.Value);
                expense.ExpenseType = Convert.ToInt32(cmbExpenseType.Value);
                expense.Description = txtExpenseDescription.Text;
                expense.CashAmount = cashAmount;
                expense.BankAmount = bankAmount;
                expense.CashAccountID = cashAccountID;
                expense.PaymentMethod = paymentMethod;
                expense.BankAccountID = bankAccountID;
                expense.BankReference = txtBankReferenceNo.Text;
                expense.Amount = Convert.ToDecimal(txtAmount.Text);
                expense.ExpenseDate = dtpDate.Value;
                expense.ApprovedDate = DateTime.Now;

                result = new ExpenseManager().UpdateExpense(expense);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save information.", "Transport Management ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return result;
            }
            return result;
        }

        /// <summary>
        /// Method to reset all controls.
        /// </summary>
        /// <remarks></remarks>
        private void ResetAllControls()
        {
            cmbEmployeeName.Text = "";
            cmbEmployeeName.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            txtExpenseDescription.Text = "";
            cmbBankAccount.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            cmbCashAccount.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            txtAmount.Text = "";
            txtCashAmount.Clear();
            txtBankAmount.Clear();
            cmbExpenseType.Focus();

        }

        private void Expenses_Load(System.Object sender, System.EventArgs e)
        {
            LoadEmployeeInformation();
            LoadPaymentMethod();
            LoadExpenseTypeCombo();
            LoadBankAccountCombo();
            LoadChildAccount();
            if (_isUpdate)
            {
                LoadExpenseInformation();
            }
        }

        private void LoadChildAccount()
        {

            lstChildAccount = new ChildAccountManager().GetAllChildAccountByParentID(IFMSConstant.CashAccountID);

            DataTable table = new DataTable();

            table.Columns.Add("ID");
            table.Columns.Add("Name");

            DataRow tableDR = table.NewRow();
            tableDR[0] = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            tableDR[1] = MTBFConstants.DataField.COMBO_DEFAULT_NAME;
            table.Rows.Add(tableDR);



            foreach (ChildAccount childAccount in lstChildAccount)
            {
                DataRow row = table.NewRow();
                row[0] = childAccount.ChildAccountID;
                row[1] = childAccount.Description;
                table.Rows.Add(row);

            }
            cmbCashAccount.DataSource = table;
            cmbCashAccount.DisplayMember = "Name";
            cmbCashAccount.ValueMember = "ID";

            cmbCashAccount.Value = (lstChildAccount.Count > 0) ? lstChildAccount[0].ChildAccountID : MTBFConstants.DataField.COMBO_DEFAULT_ID;
            cmbCashAccount.DisplayLayout.Bands[0].Columns["ID"].Hidden = true;
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


        private void btnSave_Click(System.Object sender, System.EventArgs e)
        {
            if (ValidFormData())
            {
                if (_isUpdate)
                {
                    if (UpdateExpenseInformation() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Successfully saved.");
                        _isUpdate = false;
                        ResetAllControls();
                    }

                }
                else
                {
                    if (InsertExpenseInformation() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Successfully saved.");
                        ResetAllControls();
                    }

                }

                if (OnExpenseInformationLoad != null)
                {
                    OnExpenseInformationLoad();
                }
            }

        }

        private void btnClose_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(System.Object sender, System.EventArgs e)
        {
            ResetAllControls();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmExpenseType frm = new frmExpenseType();
            frm.LoadExpenseType += new frmExpenseType.LoadExpenseTypeEventHandler(frm_LoadExpenseType);
            frm.ShowDialog();
        }

        void frm_LoadExpenseType()
        {
            LoadExpenseTypeCombo();
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

        private void CalculateReceiveAmount()
        {
            decimal cashAmount = 0;
            decimal bankAmount = 0;

            decimal.TryParse(txtCashAmount.Text, out cashAmount);
            decimal.TryParse(txtBankAmount.Text, out bankAmount);

            txtAmount.Text = (cashAmount + bankAmount).ToString("F2");


        }

        private void txtBankAmount_TextChanged(object sender, EventArgs e)
        {
            CalculateReceiveAmount();
        }


    }
}
