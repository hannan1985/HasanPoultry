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
    public partial class frmSalesParty : BaseForm
    {
        private int _branchID = 0;

        public frmSalesParty()
        {

            InitializeComponent();
        }

        private void frmSalesParty_Load(object sender, EventArgs e)
        {
            LoadSalesPartyInformation();
        }

        private void btnSupplierSave_Click(object sender, EventArgs e)
        {
            if (ValidFormData())
            {
                if (InsertSalesPartyInfo() == (int)MTBFEnums.ReturnResult.SUCCESS)
                {
                    MessageBoxHelper.ShowInformation("Successfully saved");

                    LoadSalesPartyInformation();

                    ResetAllControls();
                }
            }
        }

        private void btnSupplierUpdate_Click(object sender, EventArgs e)
        {
            if (ValidFormData())
            {
                if (UpdateSalesPartyInfo() == (int)MTBFEnums.ReturnResult.SUCCESS)
                {
                    MessageBoxHelper.ShowInformation("Successfully saved");

                    LoadSalesPartyInformation();

                    ResetAllControls();
                }
            }
        }

        private void grvSalesPartyInformation_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grvSalesPartyInformation.SelectedRows.Count > 0 && !grvSalesPartyInformation.SelectedRows[0].IsNewRow)
            {
                _branchID = Convert.ToInt32(grvSalesPartyInformation.CurrentRow.Cells["SalesPartyID"].Value);

                SalesParty pUnit = new StockSalesManager().GetSalesPartyByID(_branchID);
                if (pUnit != null)
                {
                    txtSalesPartyName.Text = pUnit.PartyName;
                    txtAddress.Text = pUnit.Address;
                    txtPhoneNumber.Text = pUnit.Phone;

                }
                btnSupplierUpdate.Enabled = true;
                btnSupplierSave.Enabled = false;
            }
        }

        private bool ValidFormData()
        {
            if (txtSalesPartyName.Text.Trim() == string.Empty)
            {
                MessageBoxHelper.ShowInformation("You need to enter name.");
                txtSalesPartyName.Focus();
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
        /// Method to insert SalesParty information.
        /// </summary>
        /// <returns></returns>
        private int InsertSalesPartyInfo()
        {
            int result = 0;

            SalesParty pUnit = new SalesParty();

            pUnit.PartyName = txtSalesPartyName.Text;
            pUnit.Address = txtAddress.Text;
            pUnit.Phone = txtPhoneNumber.Text;          
            pUnit.BranchID = MTBFConstants.AppConstants.BranchID;
            pUnit.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            result = new StockSalesManager().InsertSalesParty(pUnit);


            return result;

        }

        /// <summary>
        /// Method to update SalesParty information.
        /// </summary>
        /// <returns></returns>
        private int UpdateSalesPartyInfo()
        {
            int result = 0;

            SalesParty pUnit = new StockSalesManager().GetSalesPartyByID(_branchID);

            pUnit.PartyName = txtSalesPartyName.Text;
            pUnit.Address = txtAddress.Text;
            pUnit.Phone = txtPhoneNumber.Text;
            pUnit.Address = txtAddress.Text;
            pUnit.BranchID = MTBFConstants.AppConstants.BranchID;
            pUnit.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            result = new StockSalesManager().UpdateSalesParty(pUnit);


            return result;
        }

        /// <summary>
        /// Method to load SalesParty information.
        /// </summary>
        private void LoadSalesPartyInformation()
        {
            List<SalesParty> lstSalesParty = new List<SalesParty>();         
            lstSalesParty = new StockSalesManager().GetAllSalesParty().Cast<SalesParty>().Where(c => c.BranchID == MTBFConstants.AppConstants.BranchID && c.OrganizationID == MTBFConstants.AppConstants.OrganizationID).ToList();

            DataTable orgTable = BuildOrganizationTable();
            foreach (SalesParty pUnit in lstSalesParty)
            {
                DataRow row = orgTable.NewRow();
                row["SalesPartyID"] = pUnit.SalesPartyID;
                row["Party Name"] = pUnit.PartyName;
                row["Phone"] = pUnit.Phone;               
                row["Address"] = pUnit.Address;
                orgTable.Rows.Add(row);
            }

            grvSalesPartyInformation.DataSource = orgTable;
            grvSalesPartyInformation.Columns["SalesPartyID"].Visible = false;

        }

        private DataTable BuildOrganizationTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("SalesPartyID");
            table.Columns.Add("Party Name");           
            table.Columns.Add("Phone");
            table.Columns.Add("Address");
            return table;
        }

        private void ResetAllControls()
        {
            txtSalesPartyName.Clear();
            txtAddress.Clear();
            txtPhoneNumber.Clear();
 
        }

        private void btnSupplierClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSalesParty_Load_1(object sender, EventArgs e)
        {

        }

        
     

    }
}
