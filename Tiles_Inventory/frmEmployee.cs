using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using NybSys.MTBF.Utility.Constants;
using NybSys.MTBF.Utility.Encryption;
using NybSys.MTBF.Utility.Enums;
using NybSys.MTBF.Utility.Helper;
using IMFS.Common.DTO;
using IFMS.BLL;


namespace Tiles_Inventory
{
    public partial class frmEmployee : BaseForm
    {
        private int _employeeID = 0;
        MTBFEncryption objEncryptDecrypt = new MTBFEncryption();
        List<EmployeeType> lstEmployeeType = new List<EmployeeType>();

        public frmEmployee()
        {
            InitializeComponent();
        }

        private void Employee_Load(System.Object sender, System.EventArgs e)
        {
            grvEmployeeDetails.DataSource = BuildEmployeeTable();
            LoadEmployeeTypeCombo();
            EmployeeInformationLoad();
            txtEmployeeName.Focus();
            LoadDesignationCombo();
         
        }



        private void LoadDesignationCombo()
        {
            List<Designation> lstDesignation = new List<Designation>();

            Designation designation = new Designation();
            designation.DesignationID = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            designation.DesignationName = MTBFConstants.DataField.COMBO_DEFAULT_NAME;

            lstDesignation.Insert(0, designation);

            lstDesignation = new EmployeeManager().GetAllDesignation().Cast<Designation>().ToList();

            UIHelper.SetComboBoxData(cmbDesignation, lstDesignation, "DesignationName", "DesignationID");
        }



        private DataTable BuildEmployeeTable()
        {
            DataTable table = new DataTable();

            table.Columns.Add("EmployeeID");
            table.Columns.Add("Employee Name");
            table.Columns.Add("Designation");
            table.Columns.Add("Address");
            table.Columns.Add("Phone");
            table.Columns.Add("Salary");
            table.Columns.Add("Join Date");
            table.Columns.Add("Is Deleted");
            table.Columns.Add("Email");
            table.Columns.Add("Employee Type");
            return table;
        }

        /// <summary>
        /// Method to load employee information in grid.
        /// </summary>
        /// <remarks></remarks>
        private void EmployeeInformationLoad()
        {
           DataTable dt = BuildEmployeeTable();
            List<Employee> lstEmployee = new List<Employee>();
            lstEmployee = new EmployeeManager().GetAllEmployee().Cast<Employee>().ToList();
            List<Designation> lstDesignation = new List<Designation>();
            lstDesignation = new EmployeeManager().GetAllDesignation();
            foreach (Employee employee in lstEmployee)
            {
               // UltraGridRow row = grvEmployeeDetails.DisplayLayout.Bands[0].AddNew();
                EmployeeType employeeType = lstEmployeeType.Where(e => e.EmployeeTypeID == employee.EmployeeTypeID).FirstOrDefault();
                Designation designation = lstDesignation.Where(d => d.DesignationID == employee.DesignationID).FirstOrDefault();
                DataRow row = dt.NewRow();
                row["EmployeeID"] = employee.EmployeeID;
                row["Employee Name"] = employee.EmployeeName;
                row["Designation"] = (designation != null) ? designation.DesignationName : string.Empty;
                row["Address"] = employee.Address;
                row["Phone"] = employee.Phone;
                row["Salary"] = employee.Salary;
                row["Join Date"] = employee.JoinDate.ToString("yyyy/MM/dd");
                row["Is Deleted"] = employee.IsDeleted;
                row["Email"] = employee.Email;
                row["Employee Type"] = (employeeType != null) ? employeeType.TypeName : string.Empty;
                dt.Rows.Add(row);
            }
            grvEmployeeDetails.DataSource = dt;
            grvEmployeeDetails.DisplayLayout.Bands[0].Columns["EmployeeID"].Hidden = true;
        }

        /// <summary>
        /// Method to validate form data.
        /// </summary>
        /// <returns></returns>
        private bool ValidFormData()
        {

            if (txtEmployeeName.Text.Trim() == string.Empty)
            {
                MessageBoxHelper.ShowInformation("You need to enter employee name.");
                txtEmployeeName.Focus();
                return false;
            }

            if (cmbDesignation.Text == string.Empty)
            {
                MessageBoxHelper.ShowInformation("You need to select designation");
                cmbDesignation.Focus();
                return false;
            }

            if (txtPhone.Text.Trim() == string.Empty)
            {
                MessageBoxHelper.ShowInformation("You need to enter phone number");
                txtPhone.Focus();
                return false;
            }
            return true;
        }


        /// <summary>
        /// Method to insert employee information.
        /// </summary>
        /// <returns></returns>
        private int InsertEmployee()
        {
            int employeeType = 0;
            int.TryParse(cmbEmpoyeeType.Value.ToString(), out employeeType);

            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            Employee employee = new Employee();
            employee.EmployeeName = txtEmployeeName.Text;
            employee.DesignationID = Convert.ToInt32(cmbDesignation.Value);
            employee.Address = txtAddress.Text;
            employee.JoinDate = dtpJoinDate.Value;
            employee.Salary = (txtSalary.Text == string.Empty) ? 0 : Convert.ToDecimal(txtSalary.Text);
            employee.Phone = txtPhone.Text;
            employee.Email = txtEmail.Text;
            employee.IsDeleted = false;
            employee.EmployeeTypeID = employeeType;
            employee.BranchID = MTBFConstants.AppConstants.BranchID;
            employee.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            result = new EmployeeManager().InsertEmployee(employee);
            return result;
        }

        /// <summary>
        /// Method to update employee information.
        /// </summary>
        /// <returns></returns>
        private int UpdateEmployee()
        {
            int employeeType = 0;
            int.TryParse(cmbEmpoyeeType.Value.ToString(), out employeeType);
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            Employee employee = new EmployeeManager().GetEmployeeByID(_employeeID);
            employee.EmployeeName = txtEmployeeName.Text;
            employee.DesignationID = Convert.ToInt32(cmbDesignation.Value);
            employee.Address = txtAddress.Text;
            employee.JoinDate = dtpJoinDate.Value;
            employee.Salary = (txtSalary.Text == string.Empty) ? 0 : Convert.ToDecimal(txtSalary.Text);
            employee.Phone = txtPhone.Text;
            employee.Email = txtEmail.Text;
            employee.IsDeleted = cbIsDeleted.Checked;
            employee.EmployeeTypeID = employeeType;
            result = new EmployeeManager().UpdateEmployee(employee);

            return result;
        }

        /// <summary>
        /// Method to reset all controls.
        /// </summary>
        private void ResetAllControls()
        {
            txtEmployeeName.Clear();
            txtAddress.Clear();
            txtPhone.Clear();
            txtSalary.Clear();
            txtEmail.Clear();
            cmbDesignation.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            cmbEmpoyeeType.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            cbIsDeleted.Checked = false;
            dtpJoinDate.Value = DateTime.Now;
        }

        private void btnClose_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }



        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ResetAllControls();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidFormData())
            {
                if (IsUpdating)
                {
                    if (UpdateEmployee() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Employee information saved successfully.");
                        this.EmployeeInformationLoad();
                        txtEmployeeName.Focus();
                        ResetAllControls();
                        IsUpdating = false;
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save employee information.");
                    }
                }
                else
                {
                    if (InsertEmployee() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Employee information saved successfully.");
                        this.EmployeeInformationLoad();
                        ResetAllControls();
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save employee information.");
                    }
                }

            }
        }

        private void grvEmployeeDetails_ClickCell(object sender, ClickCellEventArgs e)
        {
            if (grvEmployeeDetails.Selected.Rows.Count > 0)
            {
                this.IsUpdating = true;
                _employeeID = Convert.ToInt32(grvEmployeeDetails.Selected.Rows[0].Cells["EmployeeID"].Value);
                Employee employee = new EmployeeManager().GetEmployeeByID(_employeeID);
                txtEmployeeName.Text = employee.EmployeeName;
                cmbDesignation.Value = employee.DesignationID;
                txtAddress.Text = employee.Address;
                dtpJoinDate.Value = employee.JoinDate;
                txtSalary.Text = employee.Salary.ToString();
                txtPhone.Text = employee.Phone;
                txtEmail.Text = employee.Email;
                cmbEmpoyeeType.Value = employee.EmployeeTypeID;
                cbIsDeleted.Checked = employee.IsDeleted;

            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmDesignation frm = new frmDesignation();
            frm.LoadDesignation += new frmDesignation.LoadDesignationEventHandler(frm_LoadDesignation);
            frm.ShowDialog();
        }

        void frm_LoadDesignation()
        {
            LoadDesignationCombo();
        }

        private void btAddEmployeeType_Click(object sender, EventArgs e)
        {
            frmEmployeeType frm = new frmEmployeeType();
            frm.OnLoadEmployeeType += new frmEmployeeType.OnRoleInformationLoadEventHandler(frm_OnLoadEmployeeType);
            frm.ShowDialog();
        }

        void frm_OnLoadEmployeeType()
        {
            LoadEmployeeTypeCombo();
        }

        private void LoadEmployeeTypeCombo()
        {


            EmployeeType employeeType = new EmployeeType();
            employeeType.EmployeeTypeID = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            employeeType.TypeName = MTBFConstants.DataField.COMBO_DEFAULT_NAME;

            lstEmployeeType.Insert(0, employeeType);

            lstEmployeeType = new EmployeeTypeManager().GetAllEmployeeType().Cast<EmployeeType>().ToList();

            UIHelper.SetComboBoxData(cmbEmpoyeeType, lstEmployeeType, "TypeName", "EmployeeTypeID");
        }

    }
}
