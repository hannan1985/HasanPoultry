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
    public partial class frmViewCustomer : BaseForm
    {
        private int _CustomerID = 0;
        List<Customer> lstCustomer = new List<Customer>();
        List<Zone> lstZone = new List<Zone>();
        public frmViewCustomer()
        {
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        private void frmViewCustomer_Load(object sender, EventArgs e)
        {
            lstZone = new CustomerManager().GetAllZone(); 
            LoadCustomerInformation();
                     
        }

        /// <summary>
        /// Method to load customer information in grid.
        /// </summary>
        private void LoadCustomerInformation()
        {
            lstCustomer = new CustomerManager().GetAllCustomerByBranchID(MTBFConstants.AppConstants.BranchID).Cast<Customer>().ToList(); 
            LoadDatainGrid(lstCustomer);  
        }

        private void LoadDatainGrid(List<Customer> lstCustomer)
        {
            grvCompanyInformation.DataSource = BuildCustomerTable();
            grvCompanyInformation.Columns[0].Visible = false;

            DataTable dt = BuildCustomerTable();
          
           
            foreach (Customer customer in lstCustomer)
            {
                Zone zone = lstZone.Where(z => z.ZoneID == customer.ZoneID).FirstOrDefault();
                DataRow row = dt.NewRow();
                row["CustomerID"] = customer.CustomerID;
                row["Customer Code"] = customer.CustomerCode;
                row["Customer Name"] = customer.CustomerName;
                row["Phone"] = customer.Phone;
                row["Address"] = customer.Address;
                row["Zone"] = (zone != null) ? zone.ZoneName : string.Empty;
                row["Credit Limit"] = customer.CreditLimit;
                row["Discount Percent"] = customer.DiscountPercentage;
                row["Proprietor"] = customer.Proprietor;
                dt.Rows.Add(row);
            }

            grvCompanyInformation.DataSource = dt;
            grvCompanyInformation.Columns[0].Visible = false;

        }


        private DataTable BuildCustomerTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CustomerID");
            dt.Columns.Add("Customer Code");
            dt.Columns.Add("Customer Name");
            dt.Columns.Add("Phone");
            dt.Columns.Add("Address");
            dt.Columns.Add("Zone");
            dt.Columns.Add("Credit Limit");
            dt.Columns.Add("Discount Percent");
            dt.Columns.Add("Proprietor");

            return dt;
        }

        private void LoadCustomerInformationByPhoneNumber(string customerName)
        {
            
            List<Customer> lstSearchCustomer = new List<Customer>();
            lstSearchCustomer = lstCustomer.Cast<Customer>().Where(c => c.CustomerName.ToLower().StartsWith(customerName.ToLower())).ToList();
            LoadDatainGrid(lstSearchCustomer);
            //foreach (Customer customer in lstSearchCustomer)
            //{
            //    Zone zone = new CustomerManager().GetZoneByID(customer.ZoneID);
            //    string zoneName = (zone != null) ? zone.ZoneName : string.Empty;
            //    grvCompanyInformation.Rows.Add(customer.CustomerID, customer.CustomerCode, customer.CustomerName, customer.Phone, customer.Address, zoneName, customer.CreditLimit, customer.DiscountPercentage);
            //}

            //grvCompanyInformation.Columns[0].Visible = false;
            //grvCompanyInformation.ClearSelection();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmCustomer frm = new frmCustomer(_CustomerID, false);
            frm.OnLoadCustomerInformation += new frmCustomer.LodaEventHandaler(frm_OnLoadCustomerInformation);
            frm.ShowDialog();
        }

        void frm_OnLoadCustomerInformation(int customerID)
        {
            LoadCustomerInformation();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {


            if (grvCompanyInformation.SelectedRows.Count > 0 && !grvCompanyInformation.SelectedRows[0].IsNewRow && grvCompanyInformation.SelectedRows[0].Cells[0].Value != null)
            {
                _CustomerID = Convert.ToInt32(grvCompanyInformation.SelectedRows[0].Cells[0].Value);

                frmCustomer frm = new frmCustomer(_CustomerID, true);
                frm.OnLoadCustomerInformation += new frmCustomer.LodaEventHandaler(frm_OnLoadCustomerInformation);
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
            
            LoadCustomerInformationByPhoneNumber(txtPhone.Text.Trim());
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCustomerInformation();
        }


    }
}
