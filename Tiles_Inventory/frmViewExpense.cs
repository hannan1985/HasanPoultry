using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using IFMS.Facade;

using IMFS.Common.DTO;
using System.Data;
using Tiles_Inventory.Reports;
using NybSys.MTBF.Utility.Constants;
using IFMS.BLL;
using NybSys.MTBF.Utility.Helper;
using Infragistics.Win.UltraWinGrid;

namespace Tiles_Inventory
{
    public partial class frmViewExpense : BaseForm
    {
        private int _expenseID;
        List<ExpenseType> lstExpenseType = new List<ExpenseType>();
        public frmViewExpense()
        {
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        /// <summary>
        /// Method to laod expense information in grid.
        /// </summary>
        /// <remarks></remarks>
        private void LoadExpenseInformation()
        {


            string fromDate = dtpFromDate.Value.ToString("yyyy/MM/dd") + MTBFConstants.DAY_START_TIME;
            string toDate = dtpToDate.Value.ToString("yyyy/MM/dd") + MTBFConstants.DAY_END_TIME;


            List<Expense> lstExpense = new List<Expense>();
            lstExpense = DataAccessProxy.GetAllExpenseByDate(fromDate, toDate).Cast<Expense>().ToList();
          
            if (lstExpense.Count > 0)
            {
                LoadDataInGrid(lstExpense);
            }
            else
            {
                MessageBox.Show("No data found for this combination.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private DataTable BuildExpTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ExpenseID");
            table.Columns.Add("Expense Date");
            table.Columns.Add("Employee Name");
            table.Columns.Add("Expense Type");
            table.Columns.Add("Description");
            table.Columns.Add("Cash Amount");
            table.Columns.Add("Bank Amount");
            table.Columns.Add("Amount");
            return table;
        }

        private void LoadDataInGrid(List<Expense> lstFilteredExpense)
        {
            List<Employee> lstEmployee = new List<Employee>();
            lstEmployee = new EmployeeManager().GetAllEmployeeByBranchID(MTBFConstants.AppConstants.BranchID);
            lblTotalRecord.Text = lstFilteredExpense.Count.ToString();
            DataTable dt = BuildExpTable();
            foreach (Expense expense in lstFilteredExpense)
            {
                Employee employee = lstEmployee.Where(e => e.EmployeeID == expense.EmployeeID).FirstOrDefault();
                ExpenseType expenseType = lstExpenseType.Where(e => e.ExpenseTypeID == expense.ExpenseType).FirstOrDefault();
                DataRow row = dt.NewRow();
                row["ExpenseID"] = expense.ExpenseID;
                row["Expense Date"] = expense.ExpenseDate.ToString("dd/MM/yyyy");
                row["Employee Name"] = (employee != null) ? employee.EmployeeName : string.Empty;
                row["Expense Type"] = (expenseType != null) ? expenseType.ExpenseTypeName : string.Empty;
                row["Description"] = expense.Description;
                row["Cash Amount"] = expense.CashAmount;
                row["Bank Amount"] = expense.BankAmount;
                row["Amount"] = expense.Amount;
                dt.Rows.Add(row);
            }

            grvExpenseInformation.DataSource = dt;
            grvExpenseInformation.DisplayLayout.Bands[0].Columns["ExpenseID"].Hidden = true;
        }

        private void btnView_Click(System.Object sender, System.EventArgs e)
        {
            LoadExpenseInformation();
        }

        private void btnEdit_Click(System.Object sender, System.EventArgs e)
        {


            if (grvExpenseInformation.Selected.Rows.Count > 0)
            {
                _expenseID = Convert.ToInt32(grvExpenseInformation.Selected.Rows[0].Cells[0].Value);
                frmExpense frm = new frmExpense(true, _expenseID);
                frm.OnExpenseInformationLoad += OnExpenseInforamtionUpdated;
                frm.ShowDialog();
            }
        }

        /// <summary>
        /// Method to build expense table.
        /// </summary>
        /// <returns></returns>
        private DataTable BuildExpenseTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Employee Name");
            table.Columns.Add("Expense Date");
            table.Columns.Add("Expense Type");
            table.Columns.Add("Description");
            table.Columns.Add("Amount");
            return table;
        }


        private void OnExpenseInforamtionUpdated()
        {
            LoadExpenseInformation();
        }

        private void btnAdd_Click(System.Object sender, System.EventArgs e)
        {
            frmExpense frm = new frmExpense(false, _expenseID);
            frm.OnExpenseInformationLoad += OnExpenseInforamtionUpdated;
            frm.ShowDialog();
        }

        private void btnRefresh_Click(System.Object sender, System.EventArgs e)
        {

        }

        private void btnClose_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void frmViewExpense_Load(System.Object sender, System.EventArgs e)
        {
            base.CheckAction(this);
            LoadExpenseTypeCombo();
            grvExpenseInformation.DataSource = BuildExpTable();
            grvExpenseInformation.DisplayLayout.Bands[0].Columns["ExpenseID"].Hidden = true;
        }

        private void LoadExpenseTypeCombo()
        {
            lstExpenseType = new ExpenseManager().GetAllExpenseType();
            ExpenseType expenseType = new ExpenseType();
            expenseType.ExpenseTypeID = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            expenseType.ExpenseTypeName = MTBFConstants.DataField.COMBO_DEFAULT_NAME;
            lstExpenseType.Add(expenseType);

            UIHelper.SetComboBoxData(cmbExpenseType, lstExpenseType, "ExpenseTypeName", "ExpenseTypeID");


        }
        private void PrintExpenseReport()
        {
            string InventoryName = string.Empty;
            string InventoryAddress = string.Empty;

            DataTable expenseTable = BuildExpenseTable();
            foreach (UltraGridRow row in grvExpenseInformation.Rows)
            {
                DataRow dRow = expenseTable.NewRow();
                dRow["Expense Date"] = row.Cells["Expense Date"].Value;
                dRow["Employee Name"] = row.Cells["Employee Name"].Value;
                dRow["Expense Type"] = row.Cells["Expense Type"].Value;
                dRow["Description"] = row.Cells["Description"].Value;
                dRow["Amount"] = row.Cells["Amount"].Value;
                expenseTable.Rows.Add(dRow);
            }

            rptExpenseReport op = new rptExpenseReport();
            frmSalesReport objmainReport = new frmSalesReport();
            op.DataSource = expenseTable;
            objmainReport.rptViewer.Document = op.Document;
            objmainReport.rptViewer.Dock = DockStyle.Fill;
            SetPharmachyInforamation(ref InventoryName, ref InventoryAddress);
            op.txtPharmacyName.Text = InventoryName;
            op.txtAddress.Text = InventoryAddress;
            op.txtFromDate.Text = dtpFromDate.Value.ToShortDateString();
            op.txtToDate.Text = dtpToDate.Value.ToShortDateString();

            op.Run();
            objmainReport.ShowDialog();
        }
        private void SetPharmachyInforamation(ref string InventoryName, ref string Address)
        {
            Branch Inventory = DataAccessProxy.GetBracnhByID(MTBFConstants.AppConstants.BranchID);
            if (Inventory != null)
            {
                InventoryName = Inventory.BranchName;
                Address = Inventory.Address + ", " + Inventory.Phone + ", " + Inventory.Fax;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintExpenseReport();
        }

    }
}
