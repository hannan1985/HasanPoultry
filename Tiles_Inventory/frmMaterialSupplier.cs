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
using IFMS.BLL;
using NybSys.MTBF.Utility.Constants;

namespace Tiles_Inventory
{
    public partial class frmMaterialSupplier : BaseForm
    {
        private int _branchID = 0;

        public frmMaterialSupplier()
        {

            InitializeComponent();
        }

        private void frmMaterialSupplier_Load(object sender, EventArgs e)
        {
            LoadMaterialSupplierInformation();
        }

        private void btnSupplierSave_Click(object sender, EventArgs e)
        {
            if (ValidFormData())
            {
                if (InsertMaterialSupplierInfo() == (int)MTBFEnums.ReturnResult.SUCCESS)
                {
                    MessageBoxHelper.ShowInformation("Successfully saved");

                    LoadMaterialSupplierInformation();

                    ResetAllControls();
                }
            }
        }

        private void btnSupplierUpdate_Click(object sender, EventArgs e)
        {
            if (ValidFormData())
            {
                if (UpdateMaterialSupplierInfo() == (int)MTBFEnums.ReturnResult.SUCCESS)
                {
                    MessageBoxHelper.ShowInformation("Successfully saved");

                    LoadMaterialSupplierInformation();

                    ResetAllControls();
                    btnSupplierUpdate.Enabled = false;
                    btnSupplierSave.Enabled = true;
                }
            }
        }

        private void grvMaterialSupplierInformation_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (grvProductionUnitInformation.SelectedRows.Count > 0 && !grvProductionUnitInformation.SelectedRows[0].IsNewRow)
            {
                _branchID = Convert.ToInt32(grvProductionUnitInformation.CurrentRow.Cells["MaterialSupplierID"].Value);

                MaterialSupplier pUnit = new MaterialSupplierManager().GetMaterialSupplierByID(_branchID);
                if (pUnit != null)
                {
                    txtProductionUnitName.Text = pUnit.SupplierName;
                    txtAddress.Text = pUnit.Address;
                    txtPhoneNumber.Text = pUnit.PhoneNumber;

                }
                btnSupplierUpdate.Enabled = true;
                btnSupplierSave.Enabled = false;
            }
        }

        private bool ValidFormData()
        {
            if (txtProductionUnitName.Text.Trim() == string.Empty)
            {
                MessageBoxHelper.ShowInformation("You need to enter name.");
                txtProductionUnitName.Focus();
                return false;
            }

            if (txtAddress.Text == string.Empty)
            {
                MessageBoxHelper.ShowInformation("You need to enter address.");
                txtAddress.Focus();
                return false;
            }

            if (txtPhoneNumber.Text == string.Empty)
            {
                MessageBoxHelper.ShowInformation("You need to enter phone number.");
                txtPhoneNumber.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Method to insert MaterialSupplier information.
        /// </summary>
        /// <returns></returns>
        private int InsertMaterialSupplierInfo()
        {
            int result = 0;

            MaterialSupplier pUnit = new MaterialSupplier();

            pUnit.SupplierName = txtProductionUnitName.Text;
            pUnit.Address = txtAddress.Text;
            pUnit.PhoneNumber = txtPhoneNumber.Text;
            pUnit.BranchID = MTBFConstants.AppConstants.BranchID;
            pUnit.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            result = new MaterialSupplierManager().InsertMaterialSupplier(pUnit);


            return result;

        }

        /// <summary>
        /// Method to update MaterialSupplier information.
        /// </summary>
        /// <returns></returns>
        private int UpdateMaterialSupplierInfo()
        {
            int result = 0;

            MaterialSupplier pUnit = new MaterialSupplierManager().GetMaterialSupplierByID(_branchID);

            pUnit.SupplierName = txtProductionUnitName.Text;
            pUnit.Address = txtAddress.Text;

            pUnit.PhoneNumber = txtPhoneNumber.Text;
            result = new MaterialSupplierManager().UpdateMaterialSupplier(pUnit);


            return result;
        }

        /// <summary>
        /// Method to load MaterialSupplier information.
        /// </summary>
        private void LoadMaterialSupplierInformation()
        {
            List<MaterialSupplier> lstMaterialSupplier = new List<MaterialSupplier>();
            lstMaterialSupplier = new MaterialSupplierManager().GetAllMaterialSupplier().Where(m => m.BranchID == MTBFConstants.AppConstants.BranchID && m.OrganizationID == MTBFConstants.AppConstants.OrganizationID).Cast<MaterialSupplier>().ToList();
            DataTable orgTable = BuildOrganizationTable();
            foreach (MaterialSupplier pUnit in lstMaterialSupplier)
            {
                DataRow row = orgTable.NewRow();
                row["MaterialSupplierID"] = pUnit.SupplierID;
                row["Supplier Name"] = pUnit.SupplierName;
                row["Address"] = pUnit.Address;
                row["Phone"] = pUnit.PhoneNumber;
                orgTable.Rows.Add(row);
            }

            grvProductionUnitInformation.DataSource = orgTable;
            grvProductionUnitInformation.Columns["MaterialSupplierID"].Visible = false;

        }

        private DataTable BuildOrganizationTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("MaterialSupplierID");
            table.Columns.Add("Supplier Name");
            table.Columns.Add("Address");
            table.Columns.Add("Phone");
            return table;
        }

        private void ResetAllControls()
        {
            txtProductionUnitName.Clear();
            txtAddress.Clear();
            txtPhoneNumber.Clear();
        }

        private void btnSupplierClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMaterialSupplier_Load_1(object sender, EventArgs e)
        {

        }

        private void grvProductionUnitInformation_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
