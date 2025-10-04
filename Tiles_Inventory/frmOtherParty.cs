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
    public partial class frmOtherParty : BaseForm
    {
        private int _branchID = 0;
        private OtherParty _otherParty = new OtherParty();
        List<OtherParty> _lstOtherParty = new List<OtherParty>();
        public frmOtherParty()
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
                if (SaveOtherPartyInfo() == (int)MTBFEnums.ReturnResult.SUCCESS)
                {
                    MessageBoxHelper.ShowInformation("Successfully saved");

                    LoadSalesPartyInformation();

                    ResetAllControls();
                }
            }
        }

        private void btnSupplierUpdate_Click(object sender, EventArgs e)
        {
            ResetAllControls();
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
        private int SaveOtherPartyInfo()
        {
     
            _otherParty.PartyName = txtSalesPartyName.Text;
            _otherParty.Address = txtAddress.Text;
            _otherParty.PhoneNumber = txtPhoneNumber.Text;
            _otherParty.BranchID = MTBFConstants.AppConstants.BranchID;
            _otherParty.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
           
            return new OtherPartyManager().SaveOtherPartyInformation(_otherParty);
        }

      

        /// <summary>
        /// Method to load SalesParty information.
        /// </summary>
        private void LoadSalesPartyInformation()
        {
          
            string filter = string.Format("{0}={1}", "BranchID", MTBFConstants.AppConstants.BranchID);

            _lstOtherParty = new OtherPartyManager().GetFilterdOtherParty(filter);

            DataTable orgTable = BuildOrganizationTable();
            foreach (OtherParty otherParty in _lstOtherParty)
            {
                DataRow row = orgTable.NewRow();
                row["SalesPartyID"] = otherParty.OtherPartyID;
                row["Party Name"] = otherParty.PartyName;
                row["Phone"] = otherParty.PhoneNumber;
                row["Address"] = otherParty.Address;
                orgTable.Rows.Add(row);
            }

            grvSalesPartyInformation.DataSource = orgTable;
            grvSalesPartyInformation.DisplayLayout.Bands[0].Columns["SalesPartyID"].Hidden = true;

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
            _otherParty = new OtherParty();

        }

        private void btnSupplierClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSalesParty_Load_1(object sender, EventArgs e)
        {

        }

        private void grvSalesPartyInformation_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            if (grvSalesPartyInformation.Selected.Rows.Count > 0)
            {
                int salesPartyID = Convert.ToInt32(grvSalesPartyInformation.Selected.Rows[0].Cells["SalesPartyID"].Value);

                _otherParty = _lstOtherParty.Where(o => o.OtherPartyID == salesPartyID).FirstOrDefault();
                if (_otherParty != null)
                {
                    txtSalesPartyName.Text = _otherParty.PartyName;
                    txtAddress.Text = _otherParty.Address;
                    txtPhoneNumber.Text = _otherParty.PhoneNumber;
                }
            }
        }

       



    }
}
