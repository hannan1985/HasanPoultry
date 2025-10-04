using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NybSys.MTBF.Utility.Enums;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Helper;
using IFMS.Facade;
using NybSys.MTBF.Utility.Constants;
using IFMS.BLL;

namespace Tiles_Inventory
{
    public partial class frmSalesCenter : BaseForm
    {
        private int _salesCenterID = 0;
        SalesCenter _salesCenter = new SalesCenter();
        public frmSalesCenter()
        {
            DataAccessProxy = new FacadeManager();
            InitializeComponent();
        }


        private bool ValidFormData()
        {
            if (string.IsNullOrEmpty(txtSalesCenterName.Text))
            {
                MessageBoxHelper.ShowInformation("You need to enter SalesCenter name.");
                txtSalesCenterName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                MessageBoxHelper.ShowInformation("You need to enter SalesCenter address.");
                txtAddress.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtPhone.Text))
            {
                MessageBoxHelper.ShowInformation("You need to enter SalesCenter phone.");
                txtPhone.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtResponsiblePerson.Text))
            {
                MessageBoxHelper.ShowInformation("You need to enter responsible person.");
                txtResponsiblePerson.Focus();
                return false;
            }


            return true;
        }


        /// <summary>
        /// Method to load SalesCenter information in grid.
        /// </summary>
        private void LoadSalesCenterInformation()
        {
            DataTable SalesCenterTable = BuildSalesCenterTable();
            foreach (SalesCenter SalesCenter in DataAccessProxy.GetAllSalesCenter().Where(s => s.BranchID == MTBFConstants.AppConstants.BranchID && s.OrganizationID == MTBFConstants.AppConstants.OrganizationID))
            {
                DataRow row = SalesCenterTable.NewRow();
                row["SalesCenterID"] = SalesCenter.SalesCenterID;
                row["SalesCenter Name"] = SalesCenter.SalesCenterName;
                row["Address"] = SalesCenter.Address;
                row["Phone"] = SalesCenter.Phone;
                row["Responsible Person"] = SalesCenter.ResponsiblePerson;
                SalesCenterTable.Rows.Add(row);
            }
            grvSellesCenterInformaiton.DataSource = SalesCenterTable;
            grvSellesCenterInformaiton.Columns["SalesCenterID"].Visible = false;
        }

        /// <summary>
        /// Method to build SalesCenter information table.
        /// </summary>
        /// <returns></returns>
        private DataTable BuildSalesCenterTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("SalesCenterID");
            table.Columns.Add("SalesCenter Name");
            table.Columns.Add("Address");
            table.Columns.Add("Phone");
            table.Columns.Add("Responsible Person");

            return table;
        }

        /// <summary>
        /// Method to insert SalesCenter information.
        /// </summary>
        /// <returns></returns>
        private int InsertSalesCenterInformation()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            _salesCenter.SalesCenterName = txtSalesCenterName.Text;
            _salesCenter.Address = txtAddress.Text;
            _salesCenter.Phone = txtPhone.Text;
            _salesCenter.BranchID = MTBFConstants.AppConstants.BranchID;
            _salesCenter.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            _salesCenter.ResponsiblePerson = txtResponsiblePerson.Text;
            result =new SalesCenterManager().SaveSalesCenterInformation(_salesCenter);

            return result;
        }

        /// <summary>
        /// Method to update SalesCenter information.
        /// </summary>
        /// <returns></returns>
        private int UpdateSalesCenterInformation()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            SalesCenter salesCenter = DataAccessProxy.GetSalesCenterByID(_salesCenterID);
            salesCenter.SalesCenterName = txtSalesCenterName.Text;
            salesCenter.Address = txtAddress.Text;
            salesCenter.Phone = txtPhone.Text;
            salesCenter.ResponsiblePerson = txtResponsiblePerson.Text;
            result = DataAccessProxy.UpdateSalesCenter(salesCenter);

            return result;
        }

        private void ResetAllControls()
        {
            txtSalesCenterName.Clear();
            txtAddress.Clear();
            txtPhone.Clear();
            txtResponsiblePerson.Clear();
        }

        private void grvSellesCenterInformaiton_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grvSellesCenterInformaiton.SelectedRows.Count > 0)
            {
                _salesCenterID = Convert.ToInt32(grvSellesCenterInformaiton.SelectedRows[0].Cells["SalesCenterID"].Value);

                 _salesCenter = DataAccessProxy.GetSalesCenterByID(_salesCenterID);
                 if (_salesCenter != null)
                {
                    txtSalesCenterName.Text = _salesCenter.SalesCenterName;
                    txtAddress.Text = _salesCenter.Address;
                    txtPhone.Text = _salesCenter.Phone;
                    txtResponsiblePerson.Text = _salesCenter.ResponsiblePerson;
                    IsUpdating = true;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidFormData())
            {
                if (InsertSalesCenterInformation() == (int)MTBFEnums.ReturnResult.SUCCESS)
                {
                    MessageBoxHelper.ShowInformation("Sales center information saved successfully.");
                    ResetAllControls();
                    LoadSalesCenterInformation();
                }
                else
                {
                    MessageBoxHelper.ShowInformation("Failed to save sales center information.");
                }

            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetAllControls();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSalesCenter_Load(object sender, EventArgs e)
        {
            LoadSalesCenterInformation();
        }

        private void grvSellesCenterInformaiton_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (grvSellesCenterInformaiton.SelectedRows.Count > 0)
            {
                _salesCenterID = Convert.ToInt32(grvSellesCenterInformaiton.SelectedRows[0].Cells["SalesCenterID"].Value);

                SalesCenter sellesCenter = DataAccessProxy.GetSalesCenterByID(_salesCenterID);
                if (sellesCenter != null)
                {
                    txtSalesCenterName.Text = sellesCenter.SalesCenterName;
                    txtAddress.Text = sellesCenter.Address;
                    txtPhone.Text = sellesCenter.Phone;
                    txtResponsiblePerson.Text = sellesCenter.ResponsiblePerson;
                    IsUpdating = true;
                }
            }
        }


    }
}
