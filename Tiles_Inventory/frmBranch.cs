using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IMFS.Common.DTO;
using IFMS.Facade;
using NybSys.MTBF.Utility.Helper;
using NybSys.MTBF.Utility.Enums;

namespace Tiles_Inventory
{
    public partial class frmBranch : BaseForm
    {
        private int _branchID = 0;

        public frmBranch()
        {
            DataAccessProxy = new FacadeManager();
            InitializeComponent();
        }

        private void frmBranch_Load(object sender, EventArgs e)
        {
            LoadBranchInformation();
        }

        private void btnSupplierSave_Click(object sender, EventArgs e)
        {
            if (ValidFormData())
            {
                if (InsertBranchInfo() == (int)MTBFEnums.ReturnResult.SUCCESS)
                {
                    MessageBoxHelper.ShowInformation("Successfully saved");

                    LoadBranchInformation();

                    ResetAllControls();
                }
            }
        }

        private void btnSupplierUpdate_Click(object sender, EventArgs e)
        {
            if (ValidFormData())
            {
                if (UpdateBranchInfo() == (int)MTBFEnums.ReturnResult.SUCCESS)
                {
                    MessageBoxHelper.ShowInformation("Successfully saved");

                    LoadBranchInformation();

                    ResetAllControls();
                }
            }
        }

        private void grvBranchInformation_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (grvBranchInformation.SelectedRows.Count > 0 && !grvBranchInformation.SelectedRows[0].IsNewRow)
            {
                _branchID = Convert.ToInt32(grvBranchInformation.CurrentRow.Cells["BranchID"].Value);

                Branch branch = DataAccessProxy.GetBracnhByID(_branchID);
                if (branch != null)
                {
                    txtBranchName.Text = branch.BranchName;
                    txtContactPerson.Text = branch.ContactPerson;
                    txtEmail.Text = branch.Email;
                    txtPhone.Text = branch.Phone;
                    txtFax.Text = branch.Fax;
                    txtPharmacyAddress.Text = branch.Address;
                    txtRegistratinNumber.Text = branch.RegistrationNumber;
                    cbActive.Checked = (branch.Status == 1) ? true : false;
                }
                btnSupplierUpdate.Enabled = true;
                btnSupplierSave.Enabled = false;
            }
        }

        private bool ValidFormData()
        {
            if (txtBranchName.Text.Trim() == string.Empty)
            {
                MessageBoxHelper.ShowInformation("You need to enter branch name.");
                txtBranchName.Focus();
                return false;
            }

            if (txtPharmacyAddress.Text == string.Empty)
            {
                MessageBoxHelper.ShowInformation("You need to enter branch address.");
                txtPharmacyAddress.Focus();
                return false;
            }

            if (txtPhone.Text == string.Empty)
            {
                MessageBoxHelper.ShowInformation("You need to enter  contact number.");
                txtPhone.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Method to insert Branch information.
        /// </summary>
        /// <returns></returns>
        private int InsertBranchInfo()
        {
            int result = 0;
            try
            {
                Branch Branch = new Branch();

                Branch.BranchName = txtBranchName.Text;
                Branch.Email = txtEmail.Text;
                Branch.ContactPerson = txtContactPerson.Text;
                Branch.Phone = txtPhone.Text;
                Branch.Fax = txtFax.Text;
                Branch.RegistrationNumber = txtRegistratinNumber.Text;
                Branch.Address = txtPharmacyAddress.Text;
                Branch.Status = (cbActive.Checked) ? 1 : 2;

                result = DataAccessProxy.InsertBranch(Branch);
            }
            catch (Exception)
            {

                MessageBoxHelper.ShowInformation("Operation fail please try again");
            }

            return result;

        }

        /// <summary>
        /// Method to update Branch information.
        /// </summary>
        /// <returns></returns>
        private int UpdateBranchInfo()
        {
            int result = 0;
            try
            {


                Branch branch = DataAccessProxy.GetBracnhByID(_branchID);

                branch.BranchName = txtBranchName.Text;
                branch.Email = txtEmail.Text;
                branch.ContactPerson = txtContactPerson.Text;
                branch.Phone = txtPhone.Text;

                branch.Fax = txtFax.Text;
                branch.RegistrationNumber = txtRegistratinNumber.Text;
                branch.Address = txtPharmacyAddress.Text;
                branch.Status = (cbActive.Checked) ? 1 : 2;

                result = DataAccessProxy.UpdateBranch(branch);
            }
            catch (Exception)
            {

                MessageBoxHelper.ShowInformation("Operation fail please try again");
            }

            return result;
        }

        /// <summary>
        /// Method to load Branch information.
        /// </summary>
        private void LoadBranchInformation()
        {
            List<Branch> lstBranch = new List<Branch>();
            lstBranch = DataAccessProxy.GetAllBranch().Cast<Branch>().ToList();
            DataTable orgTable = BuildOrganizationTable();
            foreach (Branch Branch in lstBranch)
            {
                DataRow row = orgTable.NewRow();
                row["BranchID"] = Branch.BranchID;
                row["Branch Name"] = Branch.BranchName;
                row["Contact Person"] = Branch.ContactPerson;
                row["Phone"] = Branch.Phone;
                row["Email"] = Branch.Email;
                row["Address"] = Branch.Address;
                orgTable.Rows.Add(row);
            }

            grvBranchInformation.DataSource = orgTable;
            grvBranchInformation.Columns["BranchID"].Visible = false;

        }

        private DataTable BuildOrganizationTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("BranchID");
            table.Columns.Add("Branch Name");
            table.Columns.Add("Contact Person");
            table.Columns.Add("Phone");
            table.Columns.Add("Email");
            table.Columns.Add("Address");
            return table;
        }

        private void ResetAllControls()
        {
            txtBranchName.Clear();
            txtEmail.Clear();
            txtContactPerson.Clear();
            txtPhone.Clear();
            txtFax.Clear();
            txtPharmacyAddress.Clear();
            cbActive.Checked = false;
            txtRegistratinNumber.Clear();
        }

        private void btnSupplierClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
