using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IFMS.Facade;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Helper;
using NybSys.MTBF.Utility.Constants;
using IFMS.BLL;

namespace Tiles_Inventory
{
    public partial class frmViewPreviousDue : BaseForm
    {
        private int _CustomerID = 0;
        List<CustomerPreviousDue> lstCustomerDue = new List<CustomerPreviousDue>();
        public frmViewPreviousDue()
        {
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        private void frmViewCustomer_Load(object sender, EventArgs e)
        {
            LoadPreviousDueInformation();
        }

        /// <summary>
        /// Method to load customer information in grid.
        /// </summary>
        private void LoadPreviousDueInformation()
        {
            lstCustomerDue = new List<CustomerPreviousDue>();
            List<CustomerPreviousDue> lstCustomerPreviousDue = new List<CustomerPreviousDue>();
            lstCustomerPreviousDue = new PreviousDueManager().GetAllCustomerPreviousDue().Cast<CustomerPreviousDue>().ToList();
            lstCustomerPreviousDue = lstCustomerPreviousDue.Where(p => p.BranchID == MTBFConstants.AppConstants.BranchID && p.OrganizationID == MTBFConstants.AppConstants.OrganizationID).ToList();

            List<Customer> lstCustomer = new List<Customer>();
            List<Supplier> lstSupplier = new List<Supplier>();
            lstCustomer = new CustomerManager().GetAllCustomerByBranchID(MTBFConstants.AppConstants.BranchID);
            lstSupplier = new SupplierManager().GetAllSupplierByBranchID(MTBFConstants.AppConstants.BranchID);


            grvCompanyInformation.Rows.Clear();
            foreach (CustomerPreviousDue customerDue in lstCustomerPreviousDue)
            {
                Supplier supplier = lstSupplier.Where(s => s.SupplierID == customerDue.SupplierID).FirstOrDefault();
                Customer customer = lstCustomer.Where(c => c.CustomerID == customerDue.CustomerID).FirstOrDefault();
                customerDue.CustomerName = (customer != null) ? customer.CustomerName : string.Empty;
                customerDue.CustomerAddress = (customer != null) ? customer.Address : string.Empty;
                customerDue.SupplierName = (supplier != null) ? supplier.SupplierName : string.Empty;
                grvCompanyInformation.Rows.Add(customerDue.PreviousDueID, customerDue.DueDate.ToString("dd/MM/yyyy"), customerDue.CustomerName, customerDue.CustomerAddress, customerDue.SupplierName, customerDue.Amount);

                lstCustomerDue.Add(customerDue);
            }

            grvCompanyInformation.Columns[0].Visible = false;
            grvCompanyInformation.ClearSelection();
        }

        private void LoadDueInformationByCustomerName(string customerName)
        {
            List<CustomerPreviousDue> lstCustomer = new List<CustomerPreviousDue>();
            lstCustomer = lstCustomerDue.Where(c => c.CustomerName.ToLower().StartsWith(customerName.ToLower())).ToList();

            foreach (CustomerPreviousDue customerDue in lstCustomer)
            {
                grvCompanyInformation.Rows.Add(customerDue.PreviousDueID, customerDue.DueDate.ToString("dd/MM/yyyy"), customerDue.CustomerName, customerDue.CustomerAddress, customerDue.SupplierName, customerDue.Amount);
            }

            grvCompanyInformation.Columns[0].Visible = false;
            grvCompanyInformation.ClearSelection();
        }


        private void LoadDueInformationBySupplierName(string supplierName)
        {

            List<CustomerPreviousDue> lstCustomer = new List<CustomerPreviousDue>();
            lstCustomer = lstCustomerDue.Where(c => c.SupplierName.ToLower().StartsWith(supplierName.ToLower())).ToList();

            foreach (CustomerPreviousDue customer in lstCustomer)
            {
                grvCompanyInformation.Rows.Add(customer.PreviousDueID, customer.DueDate.ToString("dd/MM/yyyy"), customer.CustomerName, customer.CustomerAddress, customer.SupplierName, customer.Amount);
            }

            grvCompanyInformation.Columns[0].Visible = false;
            grvCompanyInformation.ClearSelection();
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmPreviousDue frm = new frmPreviousDue(false, _CustomerID);
            frm.OnDueInformationLoad += new frmPreviousDue.OnDueInformationLoadEventHandler(frm_OnDueInformationLoad);
            frm.ShowDialog();
        }

        void frm_OnDueInformationLoad()
        {
            LoadPreviousDueInformation();
        }



        private void btnEdit_Click(object sender, EventArgs e)
        {


            if (grvCompanyInformation.SelectedRows.Count > 0 && !grvCompanyInformation.SelectedRows[0].IsNewRow && grvCompanyInformation.SelectedRows[0].Cells[0].Value != null)
            {
                _CustomerID = Convert.ToInt32(grvCompanyInformation.SelectedRows[0].Cells[0].Value);

                frmPreviousDue frm = new frmPreviousDue(true, _CustomerID);
                frm.OnDueInformationLoad += new frmPreviousDue.OnDueInformationLoadEventHandler(frm_OnDueInformationLoad);
                frm.ShowDialog();
            }
            else
            {
                MessageBoxHelper.ShowInformation("Please select a row");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            grvCompanyInformation.Rows.Clear();
            LoadDueInformationByCustomerName(txtPhone.Text.Trim());
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadPreviousDueInformation();
        }

        private void txtSupplierName_TextChanged(object sender, EventArgs e)
        {
            grvCompanyInformation.Rows.Clear();
            LoadDueInformationBySupplierName(txtSupplierName.Text.Trim());
        }


    }
}
