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

namespace Tiles_Inventory
{
    public partial class frmViewProductionUnitExpense : BaseForm
    {
        private int _expenseID;

        public frmViewProductionUnitExpense()
        {
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        /// <summary>
        /// Method to laod expense information in grid.
        /// </summary>
        /// <remarks></remarks>
        private void LoadExpenseInformation(int unitID, string fromDate, string toDate)
        {
            grvExpenseInformation.Rows.Clear();
            List<ProductionUnitExpense> lstExpense = new List<ProductionUnitExpense>();
            string filter = string.Format("{0} between '{1}' and '{2}' And {3}={4}", "ExpenseDate", fromDate, toDate, "UnitID", unitID);
            lstExpense = new ExpenseManager().GetFilteredProductionUnitExpense(filter).Cast<ProductionUnitExpense>().ToList();

            if (lstExpense.Count > 0)
            {
                foreach (ProductionUnitExpense expense in lstExpense.Where(e => e.BranchID == MTBFConstants.AppConstants.BranchID && e.OrganizationID == MTBFConstants.AppConstants.OrganizationID))
                {
                    Employee employee = DataAccessProxy.GetEmployeeByID(expense.EmployeeID);

                    grvExpenseInformation.Rows.Add(expense.ProductionUnitExpenseID, employee.EmployeeName, expense.ExpenseDate.ToShortDateString(), expense.Description, expense.Amount);
                }
            }
            else
            {
                MessageBox.Show("No data found for this combination.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnView_Click(System.Object sender, System.EventArgs e)
        {
            LoadExpenseInformation(Convert.ToInt32(cmbProductionUnit.Value), dtpFromDate.Value.ToString("yyyy/MM/dd"), dtpToDate.Value.ToString("yyyy/MM/dd"));
        }

        private void btnEdit_Click(System.Object sender, System.EventArgs e)
        {
            if (grvExpenseInformation.SelectedRows[0].IsNewRow)
            {
                MessageBox.Show("Please select a row", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (!grvExpenseInformation.SelectedRows[0].IsNewRow & grvExpenseInformation.SelectedRows.Count > 0)
            {
                _expenseID = Convert.ToInt32(grvExpenseInformation.SelectedRows[0].Cells[0].Value);
                frmProductionUnitExpense frm = new frmProductionUnitExpense(true, _expenseID);
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
            table.Columns.Add("Description");
            table.Columns.Add("Amount");
            return table;
        }


        private void OnExpenseInforamtionUpdated()
        {
            LoadExpenseInformation(Convert.ToInt32(cmbProductionUnit.Value), dtpFromDate.Value.ToString("yyyy/MM/dd"), dtpToDate.Value.ToString("yyyy/MM/dd"));
        }

        private void btnAdd_Click(System.Object sender, System.EventArgs e)
        {
            frmProductionUnitExpense frm = new frmProductionUnitExpense(false, _expenseID);
            frm.OnExpenseInformationLoad += OnExpenseInforamtionUpdated;
            frm.ShowDialog();
        }

        private void btnRefresh_Click(System.Object sender, System.EventArgs e)
        {
            grvExpenseInformation.Rows.Clear();
        }

        private void btnClose_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void frmViewExpense_Load(System.Object sender, System.EventArgs e)
        {
            base.CheckAction(this);
            LoadProductionUnitCombo();
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


        private void PrintExpenseReport()
        {
            string InventoryName = string.Empty;
            string InventoryAddress = string.Empty;

            int productionUnitID = (cmbProductionUnit.Value != null) ? Convert.ToInt32(cmbProductionUnit.Value) : 0;
            ProductionUnit pUnit = new ProductionUnitManager().GetProductionUnitByID(productionUnitID);

            DataTable expenseTable = BuildExpenseTable();
            foreach (DataGridViewRow row in grvExpenseInformation.Rows)
            {
                DataRow dRow = expenseTable.NewRow();
                dRow[0] = row.Cells[1].Value;
                dRow[1] = row.Cells[2].Value;
                dRow[2] = row.Cells[3].Value;
                dRow[3] = row.Cells[4].Value;
                expenseTable.Rows.Add(dRow);
            }

            rptProductionUnitExpenseReport op = new rptProductionUnitExpenseReport();
            frmSalesReport objmainReport = new frmSalesReport();
            op.DataSource = expenseTable;
            objmainReport.rptViewer.Document = op.Document;
            op.txtUnitName.Text = (pUnit != null) ? pUnit.UnitName : string.Empty;
            op.txtResponsible.Text = (pUnit != null) ? pUnit.Responsible : string.Empty;
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
