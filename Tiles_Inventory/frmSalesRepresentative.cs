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
using NybSys.MTBF.Utility.Helper;
using NybSys.MTBF.Utility.Constants;
using NybSys.MTBF.Utility.Enums;
using Infragistics.Win.UltraWinGrid;

namespace Tiles_Inventory
{
    public partial class frmSalesRepresentative : BaseForm
    {
        SalesRepresentative _salesRepresentative = new SalesRepresentative();
        List<Designation> lstDesignation = new List<Designation>();
        public frmSalesRepresentative()
        {
            InitializeComponent();
        }

        private void frmSalesRepresentative_Load(object sender, EventArgs e)
        {
            LoadDesignationCombo();
            SalesRepresentativeInformationLoad();
        }


        private void SalesRepresentativeInformationLoad()
        {
            grvEmployeeDetails.DataSource = BuildEmployeeTable();
            List<SalesRepresentative> lstSalesRepresentative = new List<SalesRepresentative>();
            string filter = string.Format("{0}={1}", "BranchID", MTBFConstants.AppConstants.BranchID);

            lstSalesRepresentative = new SalesRepresentativeManager().GetFilteredSalesRepresentative(filter).Cast<SalesRepresentative>().ToList();

            foreach (SalesRepresentative employee in lstSalesRepresentative)
            {
                UltraGridRow row = grvEmployeeDetails.DisplayLayout.Bands[0].AddNew();

                Designation designation = lstDesignation.Where(d => d.DesignationID == employee.Designation).FirstOrDefault();

                row.Cells["SalesRepresentativeID"].Value = employee.SalesRepresentativeID;
                row.Cells["Name"].Value = employee.Name;
                row.Cells["Designation"].Value = (designation != null) ? designation.DesignationName : string.Empty;
                row.Cells["Address"].Value = employee.Address;
                row.Cells["Phone"].Value = employee.Phone;
                row.Cells["Join Date"].Value = employee.JoinDate.ToString("yyyy/MM/dd");
                row.Cells["Is Deleted"].Value = employee.IsDeleted;
                row.Cells["Email"].Value = employee.Email;
            }

            grvEmployeeDetails.DisplayLayout.Bands[0].Columns["SalesRepresentativeID"].Hidden = true;
        }

        private DataTable BuildEmployeeTable()
        {
            DataTable table = new DataTable();

            table.Columns.Add("SalesRepresentativeID");
            table.Columns.Add("Name");
            table.Columns.Add("Designation");
            table.Columns.Add("Address");
            table.Columns.Add("Phone");
            table.Columns.Add("Join Date");
            table.Columns.Add("Is Deleted");
            table.Columns.Add("Email");

            return table;
        }
        private void LoadDesignationCombo()
        {


            Designation designation = new Designation();
            designation.DesignationID = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            designation.DesignationName = MTBFConstants.DataField.COMBO_DEFAULT_NAME;

            lstDesignation.Insert(0, designation);

            lstDesignation = new EmployeeManager().GetAllDesignation().Cast<Designation>().ToList();

            UIHelper.SetComboBoxData(cmbDesignation, lstDesignation, "DesignationName", "DesignationID");
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidFormData())
            {
                if (SaveSalesRepresentative() == (int)MTBFEnums.ReturnResult.SUCCESS)
                {
                    MessageBoxHelper.ShowInformation("Information saved successfully.");
                    ResetAllControls();
                    SalesRepresentativeInformationLoad();
                }
                else
                {
                    MessageBoxHelper.ShowInformation("Failed to save information.");
                }
            }
        }

        private void ResetAllControls()
        {
            txtEmployeeName.Clear();
            txtAddress.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            cmbDesignation.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            cbIsDeleted.Checked = false;
            dtpJoinDate.Value = DateTime.Now;
            txtEmployeeName.Focus();
            _salesRepresentative = new SalesRepresentative();
        }
        private bool ValidFormData()
        {

            if (txtEmployeeName.Text.Trim() == string.Empty)
            {
                MessageBoxHelper.ShowInformation("You need to enter employee name.");
                txtEmployeeName.Focus();
                return false;
            }

            int designationID = 0;

            if (cmbDesignation.Value == null)
            {
                MessageBoxHelper.ShowInformation("You need to select designation");
                cmbDesignation.Focus();
                return false;
            }

            int.TryParse(cmbDesignation.Value.ToString(), out designationID);
            Designation designation = lstDesignation.Where(d => d.DesignationID == designationID).FirstOrDefault();
            if (designation == null)
            {
                MessageBoxHelper.ShowInformation("Invalid designation select.");
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

        private int SaveSalesRepresentative()
        {
            _salesRepresentative.Name = txtEmployeeName.Text;
            _salesRepresentative.Address = txtAddress.Text;
            _salesRepresentative.Phone = txtPhone.Text;
            _salesRepresentative.Email = txtEmail.Text;
            _salesRepresentative.Designation = Convert.ToInt32(cmbDesignation.Value);
            _salesRepresentative.JoinDate = dtpJoinDate.Value;
            _salesRepresentative.IsDeleted = cbIsDeleted.Checked;
            _salesRepresentative.BranchID = MTBFConstants.AppConstants.BranchID;
            _salesRepresentative.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            return new SalesRepresentativeManager().SaveSalesRepresentative(_salesRepresentative);
        }

        private void grvEmployeeDetails_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            if (grvEmployeeDetails.Selected.Rows.Count > 0)
            {
                int salesRepresentativeID = Convert.ToInt32(grvEmployeeDetails.Selected.Rows[0].Cells["SalesRepresentativeID"].Value);
                string filter = string.Format("{0}={1}", "SalesRepresentativeID", salesRepresentativeID);

                _salesRepresentative = new SalesRepresentativeManager().GetFilteredSalesRepresentative(filter).FirstOrDefault();
                if (_salesRepresentative != null)
                {
                    txtEmployeeName.Text = _salesRepresentative.Name;
                    cmbDesignation.Value = _salesRepresentative.Designation;
                    txtAddress.Text = _salesRepresentative.Address;
                    dtpJoinDate.Value = _salesRepresentative.JoinDate;
                    txtPhone.Text = _salesRepresentative.Phone;
                    txtEmail.Text = _salesRepresentative.Email;
                    cbIsDeleted.Checked = _salesRepresentative.IsDeleted;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetAllControls();
        }
    }
}
