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



namespace Tiles_Inventory
{
    public partial class frmProductionUnitExpense : BaseForm
    {
        private int _expenseID;
        private bool _isUpdate = false;

        public event OnExpenseInformationLoadEventHandler OnExpenseInformationLoad;
        public delegate void OnExpenseInformationLoadEventHandler();

        public frmProductionUnitExpense(bool isEdit, int sl)
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
            dtRow[0] = -1;
            dtRow[1] = "-Select-";
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
            ProductionUnitExpense expense = new ExpenseManager().GetProductionUnitExpenseByID(_expenseID);
            cmbProductionUnit.Value = expense.UnitID;
            dtpDate.Value = expense.ExpenseDate;
            cmbEmployeeName.Value = expense.EmployeeID;
            cmbExpenseType.Value = expense.ExpenseType;
            txtExpenseDescription.Text = expense.Description;
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
                ProductionUnitExpense expense = new ProductionUnitExpense();

                expense.EmployeeID = Convert.ToInt32(cmbEmployeeName.Value);
                expense.ExpenseType = Convert.ToInt32(cmbExpenseType.Value);
                expense.Description = txtExpenseDescription.Text;
                expense.Amount = Convert.ToDecimal(txtAmount.Text);
                expense.UnitID = Convert.ToInt32(cmbProductionUnit.Value);
                expense.BranchID = MTBFConstants.AppConstants.BranchID;
                expense.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                expense.ExpenseDate = dtpDate.Value;
                expense.ApprovedDate = DateTime.Now;

                result = new ExpenseManager().InsertProductionUnitExpense(expense);
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
                ProductionUnitExpense expense = new ExpenseManager().GetProductionUnitExpenseByID(_expenseID);

                expense.EmployeeID = Convert.ToInt32(cmbEmployeeName.Value);
                expense.ExpenseType = Convert.ToInt32(cmbExpenseType.Value);
                expense.Description = txtExpenseDescription.Text;
                expense.Amount = Convert.ToDecimal(txtAmount.Text);
                expense.UnitID = Convert.ToInt32(cmbProductionUnit.Value);
                expense.ExpenseDate = dtpDate.Value;
                expense.ApprovedDate = DateTime.Now;

                result = new ExpenseManager().UpdateProductionUnitExpense(expense);
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
            cmbEmployeeName.SelectedText = "--Select--";
            txtExpenseDescription.Text = "";
            txtAmount.Text = "";


        }

        private void Expenses_Load(System.Object sender, System.EventArgs e)
        {
            LoadEmployeeInformation();
            LoadExpenseTypeCombo();
            LoadProductionUnitCombo();

            if (_isUpdate)
            {
                LoadExpenseInformation();
            }
        }


        private void LoadProductionUnitCombo()
        {
            DataTable MaterialsTable = new DataTable();
            MaterialsTable.Columns.Add("UnitID");
            MaterialsTable.Columns.Add("Unit Name");
            foreach (ProductionUnit material in new ProductionUnitManager().GetAllProductionUnit())
            {
                DataRow row = MaterialsTable.NewRow();
                row["UnitID"] = material.UnitID;
                row["Unit Name"] = material.UnitName;
                MaterialsTable.Rows.Add(row);
            }
            cmbProductionUnit.DataSource = MaterialsTable;
            cmbProductionUnit.DisplayMember = "Unit Name";
            cmbProductionUnit.ValueMember = "UnitID";
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



    }
}
