using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NybSys.MTBF.Utility.Enums;
using NybSys.MTBF.Utility.Constants;
using IMFS.Common.DTO;
using IFMS.BLL;
using Infragistics.Win.UltraWinGrid;
using NybSys.MTBF.Utility.Helper;
using Infragistics.Excel;
using System.IO;
using Tiles_Inventory.Reports;

namespace Tiles_Inventory
{
    public partial class frmGenereateSalary : BaseForm
    {
        List<Salary> lstSalary = new List<Salary>();
        public frmGenereateSalary()
        {
            InitializeComponent();
        }

        private void frmGenereateSalary_Load(object sender, EventArgs e)
        {
            LoadMonthCombo();
            LoadYearCombo();

            grvEmployee.DataSource = BuildEmployeeTable();
            grvEmployee.DisplayLayout.Bands[0].Columns["EmployeeID"].Hidden = true;
            //   LoadAllEmployee();
        }


        private DataTable BuildEmployeeTable()
        {
            DataTable table = new DataTable();

            table.Columns.Add("EmployeeID");
            table.Columns.Add("Employee Name");
            table.Columns.Add("Designation");
            table.Columns.Add("Monthly Salary", typeof(decimal));
            table.Columns.Add("Working Day", typeof(Int32));
            table.Columns.Add("Deduction", typeof(decimal));
            table.Columns.Add("Advance", typeof(decimal));
            table.Columns.Add("Incentive", typeof(decimal));
            table.Columns.Add("Bonus", typeof(decimal));
            table.Columns.Add("Net Salary", typeof(decimal));
            table.Columns.Add("Remove");
            table.Columns.Add("Employee Type");
            return table;
        }

        private void LoadAllEmployee()
        {

            List<Employee> lstEmployee = new List<Employee>();
            lstEmployee = new EmployeeManager().GetAllEmployeeByBranchID(MTBFConstants.AppConstants.BranchID).Cast<Employee>().ToList();
            List<Designation> lstDesignation = new List<Designation>();
            lstDesignation = new EmployeeManager().GetAllDesignation();

            DataTable dt = BuildEmployeeTable();
            foreach (Employee employee in lstEmployee)
            {
                DataRow row = dt.NewRow();
                Designation designation = lstDesignation.Where(d => d.DesignationID == employee.DesignationID).FirstOrDefault();

                row["EmployeeID"] = employee.EmployeeID;
                row["Employee Name"] = employee.EmployeeName;
                row["Designation"] = (designation != null) ? designation.DesignationName : string.Empty;
                row["Monthly Salary"] = employee.Salary;
                row["Working Day"] = 30;
                row["Deduction"] = 0;
                row["Incentive"] = 0;
                row["Bonus"] = 0;
                row["Net Salary"] = 0;
                row["Remove"] = "Remove";
                dt.Rows.Add(row);
            }

            grvEmployee.DataSource = dt;

            grvEmployee.DisplayLayout.Bands[0].Columns["EmployeeID"].Hidden = true;
            grvEmployee.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
            grvEmployee.DisplayLayout.PerformAutoResizeColumns(false, PerformAutoSizeType.AllRowsInBand, true);
        }


        private void LoadMonthCombo()
        {
            Dictionary<string, int> list = new Dictionary<string, int>();
            list.Add(MTBFConstants.DataField.COMBO_DEFAULT_NAME, MTBFConstants.DataField.COMBO_DEFAULT_ID);

            foreach (int enumValue in Enum.GetValues(typeof(MTBFEnums.Month)))
            {
                list.Add(Enum.GetName(typeof(MTBFEnums.Month), enumValue), enumValue);
            }
            cmbMonth.DisplayMember = "Key";
            cmbMonth.ValueMember = "Value";
            cmbMonth.DataSource = list.ToList();
            cmbMonth.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
        }

        private void LoadYearCombo()
        {
            List<Int32> lstYear = new List<int>();

            for (int i = 2018; i < 2050; i++)
            {
                lstYear.Add(i);
            }

            cmbYear.DataSource = lstYear;
            cmbYear.Value = DateTime.Now.Year;
        }

        private void btCreditClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbMonth_ValueChanged(object sender, EventArgs e)
        {

            if (cmbMonth.Value != null)
            {
                LoadSalaryInformation();
            }
            else
            {
                MessageBoxHelper.ShowInformation("Please select month");
                cmbMonth.Focus();
            }

        }

        private void LoadSalaryInformation()
        {
            int year = 0;
            int month = 0;
            int.TryParse(cmbYear.Text, out year);
            int.TryParse(cmbMonth.Value.ToString(), out month);

            string filter = string.Format("{0}={1} and {2}={3}", "Year", year, "Month", month);

            lstSalary = new SalaryManager().GetFilteredSalary(filter);
            List<Employee> lstEmployee = new List<Employee>();
            List<EmployeeType> lstEmployeeType = new List<EmployeeType>();
            lstEmployeeType = new EmployeeTypeManager().GetAllEmployeeType();
            lstEmployee = new EmployeeManager().GetAllEmployeeByBranchID(MTBFConstants.AppConstants.BranchID).Where(e => e.IsDeleted == false).Cast<Employee>().ToList();
            if (lstSalary.Count == 0)
            {

                List<Designation> lstDesignation = new List<Designation>();
                lstDesignation = new EmployeeManager().GetAllDesignation();
                foreach (Employee employee in lstEmployee)
                {
                    Designation designation = lstDesignation.Where(d => d.DesignationID == employee.DesignationID).FirstOrDefault();
                    EmployeeType empType = lstEmployeeType.Where(e => e.EmployeeTypeID == employee.EmployeeTypeID).FirstOrDefault();
                    Salary salary = new Salary();
                    salary.EmployeeName = employee.EmployeeName;
                    salary.EmployeeID = employee.EmployeeID;
                    salary.Designation = (designation != null) ? designation.DesignationName : string.Empty;
                    salary.Month = month;
                    salary.Year = year;
                    salary.MonthlySalary = employee.Salary;
                    salary.WorkingDay = 30;
                    salary.NetSalary = employee.Salary;
                    salary.TypeName = (empType != null) ? empType.TypeName : string.Empty;
                    salary.Address = employee.Address;
                    lstSalary.Add(salary);
                }

            }
            else
            {
                foreach (Salary salary in lstSalary)
                {
                    Employee employee = lstEmployee.Where(e => e.EmployeeID == salary.EmployeeID).FirstOrDefault();
                    if (employee != null)
                    {
                        EmployeeType empType = lstEmployeeType.Where(e => e.EmployeeTypeID == employee.EmployeeTypeID).FirstOrDefault();
                        salary.TypeName = (empType != null) ? empType.TypeName : string.Empty;
                        salary.Address = employee.Address;
                    }

                }
            }
            LoadDataInGrid();
            CalcualteTotal();

        }

        private void LoadDataInGrid()
        {
            DataTable dt = BuildEmployeeTable();
            foreach (Salary salary in lstSalary)
            {
                DataRow row = dt.NewRow();
                row["EmployeeID"] = salary.EmployeeID;
                row["Employee Name"] = salary.EmployeeName;
                row["Designation"] = salary.Designation;
                row["Monthly Salary"] = salary.MonthlySalary;
                row["Working Day"] = salary.WorkingDay;
                row["Deduction"] = salary.Deduction;
                row["Advance"] = salary.Advance;
                row["Incentive"] = salary.Incentive;
                row["Bonus"] = salary.Bonus;
                row["Net Salary"] = salary.NetSalary;
                row["Employee Type"] = salary.EmployeeName;
                row["Remove"] = "Remove";
                dt.Rows.Add(row);
            }

            grvEmployee.DataSource = dt;

            grvEmployee.DisplayLayout.Bands[0].Columns["Employee Type"].Hidden = true;
            grvEmployee.DisplayLayout.Bands[0].Columns["EmployeeID"].Hidden = true;
            grvEmployee.DisplayLayout.Bands[0].Columns["Remove"].Hidden = true;
            grvEmployee.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
            grvEmployee.DisplayLayout.PerformAutoResizeColumns(false, PerformAutoSizeType.AllRowsInBand, true);
        }

        private void grvEmployee_ClickCell(object sender, ClickCellEventArgs e)
        {



            string celltext = e.Cell.Column.Header.Caption;

            if (celltext == "Remove")
            {
                int rowIndex = e.Cell.Row.Index;
                if (grvEmployee.Rows[e.Cell.Row.Index].Delete())
                {
                    lstSalary.RemoveAt(rowIndex);
                    CalcualteTotal();
                }
            }


            if (celltext == "Monthly Salary")
            {
                grvEmployee.Rows[e.Cell.Row.Index].Cells["Monthly Salary"].Activate();
                grvEmployee.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode);
            }

            if (celltext == "Working Day")
            {
                grvEmployee.Rows[e.Cell.Row.Index].Cells["Working Day"].Activate();
                grvEmployee.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode);
            }

            if (celltext == "Deduction")
            {
                grvEmployee.Rows[e.Cell.Row.Index].Cells["Deduction"].Activate();
                grvEmployee.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode);
            }

            if (celltext == "Advance")
            {
                grvEmployee.Rows[e.Cell.Row.Index].Cells["Advance"].Activate();
                grvEmployee.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode);
            }

            if (celltext == "Incentive")
            {
                grvEmployee.Rows[e.Cell.Row.Index].Cells["Incentive"].Activate();
                grvEmployee.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode);
            }

            if (celltext == "Bonus")
            {
                grvEmployee.Rows[e.Cell.Row.Index].Cells["Bonus"].Activate();
                grvEmployee.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode);
            }
        }

        private void grvEmployee_CellChange(object sender, CellEventArgs e)
        {
            int employeeID = 0;
            decimal monthlySalary = 0;
            int workigDay = 0;
            decimal deduction = 0;
            decimal advance = 0;
            decimal incentive = 0;
            decimal bonus = 0;
            decimal.TryParse(grvEmployee.Rows[e.Cell.Row.Index].Cells["Monthly Salary"].Text, out monthlySalary);
            int.TryParse(grvEmployee.Rows[e.Cell.Row.Index].Cells["Working Day"].Text, out workigDay);
            decimal.TryParse(grvEmployee.Rows[e.Cell.Row.Index].Cells["Deduction"].Text, out deduction);
            decimal.TryParse(grvEmployee.Rows[e.Cell.Row.Index].Cells["Advance"].Text, out advance);
            decimal.TryParse(grvEmployee.Rows[e.Cell.Row.Index].Cells["Incentive"].Text, out incentive);
            decimal.TryParse(grvEmployee.Rows[e.Cell.Row.Index].Cells["Bonus"].Text, out bonus);
            int.TryParse(grvEmployee.Rows[e.Cell.Row.Index].Cells["EmployeeID"].Value.ToString(), out employeeID);
            decimal newMonthlySalary = 0;
            newMonthlySalary = (monthlySalary / 30) * workigDay;

            grvEmployee.Rows[e.Cell.Row.Index].Cells["Net Salary"].Value = ((newMonthlySalary + incentive + bonus) - (deduction + advance)).ToString("F2");

            Salary salary = lstSalary.Where(s => s.EmployeeID == employeeID).FirstOrDefault();


            //lstSalary[e.Cell.Row.Index].MonthlySalary = newMonthlySalary;
            //lstSalary[e.Cell.Row.Index].WorkingDay = workigDay;
            //lstSalary[e.Cell.Row.Index].Deduction = deduction;
            //lstSalary[e.Cell.Row.Index].Incentive = incentive;
            //lstSalary[e.Cell.Row.Index].Bonus = bonus;
            //lstSalary[e.Cell.Row.Index].Advance = advance;
            //lstSalary[e.Cell.Row.Index].NetSalary = ((newMonthlySalary + incentive + bonus) - (deduction + advance));
            salary.MonthlySalary = newMonthlySalary;
            salary.WorkingDay = workigDay;
            salary.Deduction = deduction;
            salary.Incentive = incentive;
            salary.Bonus = bonus;
            salary.Advance = advance;
            salary.NetSalary = ((newMonthlySalary + incentive + bonus) - (deduction + advance));

            CalcualteTotal();
        }


        private void CalcualteTotal()
        {
            decimal total = 0;
            foreach (Salary salary in lstSalary)
            {
                total += salary.NetSalary;
            }
            txtTotal.Text = total.ToString("F2");
        }

        private void btCreditSave_Click(object sender, EventArgs e)
        {
            if (grvEmployee.Rows.Count > 0)
            {
                if (new SalaryManager().SaveSalary(lstSalary) == (int)MTBFEnums.ReturnResult.SUCCESS)
                {
                    MessageBoxHelper.ShowInformation("Information saved successfully.");
                }
                else
                {
                    MessageBoxHelper.ShowInformation("Failed to save information.");
                }
            }
            else
            {
                MessageBoxHelper.ShowInformation("You need to enter data in grid.");
            }


        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ExportGridData();
        }

        private void ExportGridData()
        {
            Branch branch = new BranchManager().GetBranchByID(MTBFConstants.AppConstants.BranchID);
            try
            {
                LoadDataInExportGrid();
                if (grvExport.DataSource == null)
                {
                    MessageBoxHelper.ShowInformation("No information foud for export.");
                }
                else
                {
                    string fileLoacation = "";
                    FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                    DialogResult result = folderBrowser.ShowDialog();

                    try
                    {
                        if (result == DialogResult.OK)
                        {
                            string nameOfFile = " Salary Sheet " + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                            fileLoacation = string.Format("{0}\\{1}", folderBrowser.SelectedPath, nameOfFile);

                            // Check if file already exists. If yes, delete it. 
                            if (File.Exists(fileLoacation))
                            {
                                File.Delete(fileLoacation);
                            }

                            Workbook WB = new Workbook();
                            WB.Worksheets.Add("SalarySheet");

                            // add Title
                            Worksheet sheet = WB.Worksheets["SalarySheet"];
                            sheet.Rows[0].Cells[0].Value = (branch != null) ? branch.BranchName : string.Empty;
                            sheet.Rows[0].Cells[0].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;

                            sheet.Rows[1].Cells[0].Value = "Salary sheet for the Month of  " + cmbMonth.Text + " " + cmbYear.Text;
                            sheet.Rows[1].Cells[0].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;

                            ultraGridExcelExporter1.Export(grvExport, WB.Worksheets["SalarySheet"], 4, 0);
                            int count = 8;
                            //decimal total = 0;
                            //foreach (UltraGridRow row in grvSales.Rows)
                            //{
                            //    count++;
                            //    total = total + (Convert.ToDecimal(row.Cells["Sales Amount"].Value) + Convert.ToDecimal(row.Cells["O. Charge"].Value) + Convert.ToDecimal(row.Cells["C. Charge"].Value) + Convert.ToDecimal(row.Cells["C.L.Charge"].Value) - Convert.ToDecimal(row.Cells["Discount"].Value));
                            //}
                            //int total = Convert.ToInt32(lblTotalRecord.Text) + 8;
                            //sheet.Rows[total].Cells[4].Value = "Total :";
                            //sheet.Rows[total].Cells[4].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;

                            //sheet.Rows[total].Cells[5].ApplyFormula("=sum(F8:" + "F" + total + ")");
                            //sheet.Rows[total].Cells[5].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;


                            BIFF8Writer.WriteWorkbookToFile(WB, fileLoacation);
                            WB.Save(fileLoacation);

                            MessageBoxHelper.ShowInformation("Exported Successfully");
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBoxHelper.ShowInformation(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxHelper.ShowInformation(ex.Message);
            }
        }

        private void LoadDataInExportGrid()
        {
            DataTable dt = BuildEmployeeTable();
            foreach (Salary salary in lstSalary.Where(s => s.NetSalary > 0))
            {
                DataRow row = dt.NewRow();
                row["EmployeeID"] = salary.EmployeeID;
                row["Employee Name"] = salary.EmployeeName;
                row["Designation"] = salary.Designation;
                row["Monthly Salary"] = salary.MonthlySalary;
                row["Working Day"] = salary.WorkingDay;
                row["Deduction"] = salary.Deduction;
                row["Advance"] = salary.Advance;
                row["Incentive"] = salary.Incentive;
                row["Bonus"] = salary.Bonus;
                row["Net Salary"] = salary.NetSalary;
                row["Remove"] = "Remove";
                dt.Rows.Add(row);
            }

            grvExport.DataSource = dt;

            grvExport.DisplayLayout.Bands[0].Columns["EmployeeID"].Hidden = true;
            grvExport.DisplayLayout.Bands[0].Columns["Remove"].Hidden = true;
        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            PrintReport();
        }

        private DataTable BuildReportTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Employee Name");
            table.Columns.Add("Address");
            table.Columns.Add("Designation");
            table.Columns.Add("Join Date");
            table.Columns.Add("Work Place");
            table.Columns.Add("Salary");
            table.Columns.Add("Working Day", typeof(Int32));
            table.Columns.Add("Deduction", typeof(decimal));
            table.Columns.Add("Total Amount");
            table.Columns.Add("Advance");
            table.Columns.Add("Bonus");
            table.Columns.Add("Payable Amount");
            table.Columns.Add("Employee Type");
            return table;
        }

        private void PrintReport()
        {
            List<Employee> lstEmployee = new List<Employee>();
            List<EmployeeType> lstEmployeeType = new List<EmployeeType>();
            lstEmployeeType = new EmployeeTypeManager().GetAllEmployeeType();
            List<Designation> lstDesignation = new List<Designation>();
            lstDesignation = new EmployeeManager().GetAllDesignation();
            lstEmployee = new EmployeeManager().GetAllEmployeeByBranchID(MTBFConstants.AppConstants.BranchID).Where(e => e.IsDeleted == false).Cast<Employee>().ToList();
            DataTable dt = BuildReportTable();
            lstSalary = lstSalary.OrderBy(s => s.TypeName).ToList();

            foreach (Salary salary in lstSalary)
            {
                Employee employee = lstEmployee.Where(e => e.EmployeeID == salary.EmployeeID).FirstOrDefault();
                EmployeeType empType = new EmployeeType();
                Designation designation = new Designation();
                if (employee != null)
                {
                    empType = lstEmployeeType.Where(e => e.EmployeeTypeID == employee.EmployeeTypeID).FirstOrDefault();
                    designation = lstDesignation.Where(d => d.DesignationID == employee.DesignationID).FirstOrDefault();
                }

                DataRow row = dt.NewRow();
                row["Employee Name"] = salary.EmployeeName;
                row["Address"] = salary.Address;
                row["Designation"] = (designation != null) ? designation.DesignationName : string.Empty;
                row["Join Date"] = (employee != null) ? employee.JoinDate.ToString("dd/MM/yyyy") : string.Empty;
                row["Work Place"] = (empType != null) ? empType.TypeName : string.Empty;
                row["Salary"] = salary.MonthlySalary.ToString("F2");
                row["Total Amount"] = (salary.MonthlySalary + salary.Incentive + salary.Bonus).ToString("F2");
                row["Advance"] = salary.Advance.ToString();
                row["Working Day"] = salary.WorkingDay;
                row["Deduction"] = salary.Deduction;
                row["Bonus"] = salary.Bonus;
                row["Payable Amount"] = ((salary.MonthlySalary + salary.Incentive + salary.Bonus) - (salary.Advance + salary.Deduction)).ToString("F2");
                row["Employee Type"] = (empType != null) ? empType.TypeName : string.Empty; ;
                dt.Rows.Add(row);
            }


            rptSalary op = new rptSalary();
            frmSalesReport objmainReport = new frmSalesReport();
            op.DataSource = dt;
            objmainReport.rptViewer.Document = op.Document;
            objmainReport.rptViewer.Dock = DockStyle.Fill;
            op.lblMonthYear.Text = "-" + cmbMonth.Text + " -" + cmbYear.Text;
            //SetPharmachyInforamation(ref InventoryName, ref InventoryAddress);
            //op.txtPharmacyName.Text = InventoryName;
            //op.txtAddress.Text = InventoryAddress;
            //op.txtFromDate.Text = dtpFromDate.Value.ToShortDateString();
            //op.txtToDate.Text = dtpToDate.Value.ToShortDateString();

            op.Run();
            objmainReport.ShowDialog();

        }

    }
}
