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

namespace Tiles_Inventory
{
    public partial class frmProductionUnit : BaseForm
    {
        private int _branchID = 0;

        public frmProductionUnit()
        {

            InitializeComponent();
        }

        private void frmProductionUnit_Load(object sender, EventArgs e)
        {
            LoadProductionUnitInformation();
        }

        private void btnSupplierSave_Click(object sender, EventArgs e)
        {
            if (ValidFormData())
            {
                if (InsertProductionUnitInfo() == (int)MTBFEnums.ReturnResult.SUCCESS)
                {
                    MessageBoxHelper.ShowInformation("Successfully saved");

                    LoadProductionUnitInformation();

                    ResetAllControls();
                }
            }
        }

        private void btnSupplierUpdate_Click(object sender, EventArgs e)
        {
            if (ValidFormData())
            {
                if (UpdateProductionUnitInfo() == (int)MTBFEnums.ReturnResult.SUCCESS)
                {
                    MessageBoxHelper.ShowInformation("Successfully saved");

                    LoadProductionUnitInformation();

                    ResetAllControls();
                    btnSupplierUpdate.Enabled = false;
                    btnSupplierSave.Enabled = true;
                }
            }
        }

        private void grvProductionUnitInformation_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (grvProductionUnitInformation.SelectedRows.Count > 0 && !grvProductionUnitInformation.SelectedRows[0].IsNewRow)
            {
                _branchID = Convert.ToInt32(grvProductionUnitInformation.CurrentRow.Cells["ProductionUnitID"].Value);

                ProductionUnit pUnit = new ProductionUnitManager().GetProductionUnitByID(_branchID);
                if (pUnit != null)
                {
                    txtProductionUnitName.Text = pUnit.UnitName;
                    txtContactPerson.Text = pUnit.Responsible;

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
        /// Method to insert ProductionUnit information.
        /// </summary>
        /// <returns></returns>
        private int InsertProductionUnitInfo()
        {
            int result = 0;

            ProductionUnit pUnit = new ProductionUnit();

            pUnit.UnitName = txtProductionUnitName.Text;
            pUnit.Address = txtAddress.Text;
            pUnit.Responsible = txtContactPerson.Text;
            pUnit.PhoneNumber = txtPhoneNumber.Text;
            result = new ProductionUnitManager().InsertProductionUnit(pUnit);


            return result;

        }

        /// <summary>
        /// Method to update ProductionUnit information.
        /// </summary>
        /// <returns></returns>
        private int UpdateProductionUnitInfo()
        {
            int result = 0;

            ProductionUnit pUnit = new ProductionUnitManager().GetProductionUnitByID(_branchID);

            pUnit.UnitName = txtProductionUnitName.Text;
            pUnit.Address = txtAddress.Text;
            pUnit.Responsible = txtContactPerson.Text;
            pUnit.PhoneNumber = txtPhoneNumber.Text;
            result = new ProductionUnitManager().UpdateProductionUnit(pUnit);


            return result;
        }

        /// <summary>
        /// Method to load ProductionUnit information.
        /// </summary>
        private void LoadProductionUnitInformation()
        {
            List<ProductionUnit> lstProductionUnit = new List<ProductionUnit>();
            lstProductionUnit = new ProductionUnitManager().GetAllProductionUnit().Cast<ProductionUnit>().ToList();
            DataTable orgTable = BuildOrganizationTable();
            foreach (ProductionUnit pUnit in lstProductionUnit)
            {
                DataRow row = orgTable.NewRow();
                row["ProductionUnitID"] = pUnit.UnitID;
                row["Unit Name"] = pUnit.UnitName;
                row["Contact Person"] = pUnit.Responsible;
                row["Phone"] = pUnit.PhoneNumber;
                orgTable.Rows.Add(row);
            }

            grvProductionUnitInformation.DataSource = orgTable;
            grvProductionUnitInformation.Columns["ProductionUnitID"].Visible = false;

        }

        private DataTable BuildOrganizationTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ProductionUnitID");
            table.Columns.Add("Unit Name");
            table.Columns.Add("Contact Person");
            table.Columns.Add("Phone");
            return table;
        }

        private void ResetAllControls()
        {
            txtProductionUnitName.Clear();
            txtAddress.Clear();
            txtPhoneNumber.Clear();
            txtContactPerson.Clear();
        }

        private void btnSupplierClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmProductionUnit_Load_1(object sender, EventArgs e)
        {

        }

    }
}
