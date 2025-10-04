using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Enums;
using NybSys.MTBF.Utility.Helper;
using IFMS.Facade;
using NybSys.MTBF.Utility.Constants;

namespace Tiles_Inventory
{
    public partial class frmSellerInformation : BaseForm
    {

        private int _sellerID = 0;
        private bool isUpdate = false;

        public frmSellerInformation()
        {
            DataAccessProxy = new FacadeManager();
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidFormData())
            {
                if (isUpdate)
                {
                    if (UpdateSellerInformation() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Seller information saved successfully.");
                        ResetAllControls();
                        isUpdate = false;
                        _sellerID = 0;
                        LoadSellerInformation();
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save seller information.");
                    }
                }
                else
                {
                    if (InsertSellerInformation() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Seller information saved successfully.");
                        ResetAllControls();
                        LoadSellerInformation();
                    }

                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save seller information.");
                    }
                }
            }
        }

        private void frmSellerInformation_Load(object sender, EventArgs e)
        {
            LoadSellerInformation();
        }

        private bool ValidFormData()
        {
            if (string.IsNullOrEmpty(txtSellerName.Text))
            {
                MessageBoxHelper.ShowInformation("You need to enter seller name.");
                txtSellerName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                MessageBoxHelper.ShowInformation("You need to enter seller address.");
                txtAddress.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtPhone.Text))
            {
                MessageBoxHelper.ShowInformation("You need to enter seller phone.");
                txtPhone.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtContactPerson.Text))
            {
                MessageBoxHelper.ShowInformation("You need to enter seller contact person.");
                txtContactPerson.Focus();
                return false;
            }


            return true;
        }


        /// <summary>
        /// Method to load seller information in grid.
        /// </summary>
        private void LoadSellerInformation()
        {
            DataTable sellerTable = BuildSellerTable();
            foreach (Seller seller in DataAccessProxy.GetAllSeller().Where(s => s.BranchID == MTBFConstants.AppConstants.BranchID && s.OrganizationID == MTBFConstants.AppConstants.OrganizationID))
            {
                DataRow row = sellerTable.NewRow();
                row["SellerID"] = seller.SellerID;
                row["Seller Name"] = seller.SellerName;
                row["Address"] = seller.Address;
                row["Phone"] = seller.Phone;
                row["Contact Person"] = seller.ContactPerson;
                sellerTable.Rows.Add(row);
            }
            grvSellerInformaiton.DataSource = sellerTable;
        }

        /// <summary>
        /// Method to build seller information table.
        /// </summary>
        /// <returns></returns>
        private DataTable BuildSellerTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("SellerID");
            table.Columns.Add("Seller Name");
            table.Columns.Add("Address");
            table.Columns.Add("Phone");
            table.Columns.Add("Contact Person");

            return table;
        }

        /// <summary>
        /// Method to insert seller information.
        /// </summary>
        /// <returns></returns>
        private int InsertSellerInformation()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            Seller seller = new Seller();
            seller.SellerName = txtSellerName.Text;
            seller.Address = txtAddress.Text;
            seller.Phone = txtPhone.Text;
            seller.ContactPerson = txtContactPerson.Text;
            seller.BranchID = MTBFConstants.AppConstants.BranchID;
            seller.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            result = DataAccessProxy.InsertSeller(seller);

            return result;
        }

        /// <summary>
        /// Method to update seller information.
        /// </summary>
        /// <returns></returns>
        private int UpdateSellerInformation()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            Seller seller = DataAccessProxy.GetSellerByID(_sellerID);
            seller.SellerName = txtSellerName.Text;
            seller.Address = txtAddress.Text;
            seller.Phone = txtPhone.Text;
            seller.ContactPerson = txtContactPerson.Text;
            result = DataAccessProxy.UpdateSeller(seller);

            return result;
        }

        private void ResetAllControls()
        {
            txtSellerName.Clear();
            txtAddress.Clear();
            txtPhone.Clear();
            txtContactPerson.Clear();
        }

        private void grvSellerInformaiton_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grvSellerInformaiton.SelectedRows.Count > 0)
            {
                _sellerID = Convert.ToInt32(grvSellerInformaiton.SelectedRows[0].Cells["SellerID"].Value);

                Seller seller = DataAccessProxy.GetSellerByID(_sellerID);
                if (seller != null)
                {
                    txtSellerName.Text = seller.SellerName;
                    txtAddress.Text = seller.Address;
                    txtPhone.Text = seller.Phone;
                    txtContactPerson.Text = seller.ContactPerson;
                    isUpdate = true;
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
