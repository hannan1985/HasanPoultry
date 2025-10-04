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
using IMFS.Common.Utility;
using NybSys.MTBF.Utility.Helper;
using NybSys.MTBF.Utility.Constants;
using NybSys.MTBF.Utility.Enums;
using IFMS.BLL;

namespace Tiles_Inventory
{
    public partial class frmCustomer : BaseForm
    {
        private int _CustomerID = 0;
        public delegate void LodaEventHandaler(int customerID);
        public event LodaEventHandaler OnLoadCustomerInformation;
        Customer _customer = new Customer();

        public frmCustomer(int customerID, bool isEdit)
        {
            if (isEdit)
            {
                _CustomerID = customerID;
                IsUpdating = isEdit;
            }
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        /// <summary>
        /// Method to insert customer information.
        /// </summary>
        /// <returns></returns>
        private int SaveCustomerInfo()
        {
            int result = 0;
            decimal discountPercent = 0;
            decimal.TryParse(txtDiscountPercent.Text, out discountPercent);


            _customer.CustomerName = txtCustomer.Text;
            _customer.Address = txtAddress.Text;
            _customer.Phone = txtCustomerPhone.Text;
            _customer.CustomerCode = txtCustomerCode.Text;
            _customer.BranchID = MTBFConstants.AppConstants.BranchID;
            _customer.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            _customer.ZoneID = Convert.ToInt32(cmbZone.Value);
            _customer.Proprietor = txtProprietor.Text;
            _customer.DiscountPercentage = discountPercent;
            _customer.CustomerType = Convert.ToInt32(cmbCustomerType.Value);
            _customer.CreditLimit = (string.IsNullOrEmpty(txtCreditLimit.Text)) ? AppHelper.GetDefaultCreditLimit() : Convert.ToDecimal(txtCreditLimit.Text);
            _customer.ApplyCreditLimit = cbApplyCreditLimit.Checked;
            result = new CustomerManager().SaveCustomerInformation(_customer);
            return result;
        }



        private void LoadExistingCustomer()
        {
            _customer = DataAccessProxy.GetCustomerByID(_CustomerID);
            txtCustomer.Text = _customer.CustomerName;
            txtAddress.Text = _customer.Address;
            txtCustomerCode.Text = _customer.CustomerCode;
            txtCustomerPhone.Text = _customer.Phone;
            cmbZone.Value = _customer.ZoneID;
            txtProprietor.Text = _customer.Proprietor;
            cmbCustomerType.Value = _customer.CustomerType;
            txtDiscountPercent.Text = _customer.DiscountPercentage.ToString();
            txtCreditLimit.Text = _customer.CreditLimit.ToString();
            cbApplyCreditLimit.Checked = _customer.ApplyCreditLimit;
        }


        private void ResetAllControls()
        {
            txtCustomer.Clear();
            txtAddress.Clear();
            txtCustomerPhone.Clear();
            txtDiscountPercent.Clear();
            txtProprietor.Clear();
            txtCreditLimit.Clear();
            cbApplyCreditLimit.Checked = false;
            txtCustomerCode.Clear();
            cmbZone.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            txtCustomerCode.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (IsValidFormData())
            {

                int customerID = SaveCustomerInfo();
                if (customerID > 0)
                {
                    MessageBoxHelper.ShowInformation("Information saved successfully.");
                    ResetAllControls();
                    IsUpdating = false;
                    if (OnLoadCustomerInformation != null)
                        OnLoadCustomerInformation(customerID);
                }
                else
                {
                    MessageBoxHelper.ShowInformation("Failed to save information.");
                }

            }
        }

        private bool IsValidFormData()
        {
            if (string.IsNullOrEmpty(txtCustomer.Text))
            {
                MessageBoxHelper.ShowInformation("You need to enter customer name.");
                txtCustomer.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtCustomerPhone.Text))
            {
                MessageBoxHelper.ShowInformation("You need to enter phone number.");
                txtCustomerPhone.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                MessageBoxHelper.ShowInformation("You need to enter address.");
                txtAddress.Focus();
                return false;
            }

            if (!string.IsNullOrEmpty(txtCustomerCode.Text))
            {
                string fitler = string.Format("{0}='{1}'", "CustomerCode", txtCustomerCode.Text.Trim());

                Customer customer = new CustomerManager().GetFilteredCustomer(fitler).FirstOrDefault();
                if (customer != null && customer.CustomerID != _CustomerID)
                {
                    MessageBoxHelper.ShowInformation("Duplicate customer code!");
                    txtCustomerCode.Focus();
                    return false;
                }
            }

            if (cmbCustomerType.Value == null)
            {
                MessageBoxHelper.ShowInformation("Please select customer type");
                cmbCustomerType.Focus();
                return false;
            }
            //if (Convert.ToInt32(cmbZone.Value) == MTBFConstants.DataField.COMBO_DEFAULT_ID)
            //{
            //    MessageBoxHelper.ShowInformation("You need to select customer type.");
            //    cmbZone.Focus();
            //    return false;
            //}


            return true;
        }


        /// <summary>
        /// Method to check duplicate child account.
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        private bool IsDuplicateChildAccount(int customerID)
        {
            ChildAccount childAccount = DataAccessProxy.GetChildAccountByCustomerID(customerID);
            if (childAccount != null)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Method to insert child account information.
        /// </summary>
        /// <returns></returns>
        private int InsertChildAccount(int customerID)
        {
            int result = 0;

            ChildAccount childAccount = new ChildAccount();
            childAccount.AccountID = IFMSConstant.AccountReceivableID;
            childAccount.Description = txtCustomer.Text;
            Employee employee = DataAccessProxy.GetEmployeeByID(IFMSConstant.LoggedinUserID);

            childAccount.CustomerID = customerID;
            childAccount.CreatedBy = employee.EmployeeName;
            childAccount.CreatedDate = DateTime.Now;
            childAccount.Status = 1;
            DataAccessProxy.InsertChildAccount(childAccount);
            result = 1;

            return result;

        }

        private void LoadCustoemrZoneCombo()
        {
            Dictionary<string, int> list = new Dictionary<string, int>();
            list.Add(MTBFConstants.DataField.COMBO_DEFAULT_NAME, MTBFConstants.DataField.COMBO_DEFAULT_ID);

            List<Zone> lstZone = new List<Zone>();
            lstZone = new CustomerManager().GetAllZone();
            Zone emptyZone = new Zone();
            emptyZone.ZoneID = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            emptyZone.ZoneName = MTBFConstants.DataField.COMBO_DEFAULT_NAME;
            lstZone.Insert(0, emptyZone);

            UIHelper.SetComboBoxData(cmbZone, lstZone, "ZoneName", "ZoneID");

            //cmbZone.DisplayMember = "ZoneName";
            //cmbZone.ValueMember = "ZoneID";
            //cmbZone.DataSource = lstZone;
            //cmbZone.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
        }



        private void frmCustomer_Load(object sender, EventArgs e)
        {
            LoadCustoemrZoneCombo();
            LoadCustoemrTypeCombo();
            if (IsUpdating)
            {
                LoadExistingCustomer();
            }
        }

        private void LoadCustoemrTypeCombo()
        {
            Dictionary<string, int> list = new Dictionary<string, int>();
            list.Add(MTBFConstants.DataField.COMBO_DEFAULT_NAME, MTBFConstants.DataField.COMBO_DEFAULT_ID);

            foreach (int enumValue in Enum.GetValues(typeof(MTBFEnums.CustomerType)))
            {
                list.Add(Enum.GetName(typeof(MTBFEnums.CustomerType), enumValue), enumValue);
            }
            cmbCustomerType.DisplayMember = "Key";
            cmbCustomerType.ValueMember = "Value";
            cmbCustomerType.DataSource = list.ToList();
            cmbCustomerType.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
        }

        private void btnCustomerClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnWholeSales_Click(object sender, EventArgs e)
        {
            ResetAllControls();
        }

        private void btnAddProductColor_Click(object sender, EventArgs e)
        {
            frmCustomerZone frm = new frmCustomerZone();
            frm.LoadZoneInfo += new frmCustomerZone.LoadProductTypeEventHandler(frm_LoadZoneInfo);
            frm.ShowDialog();
        }

        void frm_LoadZoneInfo()
        {
            LoadCustoemrZoneCombo();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            List<Customer> lstCustomer = new List<Customer>();
            lstCustomer = new CustomerManager().GetAllCustomerByBranchID(MTBFConstants.AppConstants.BranchID);
            if (new CustomerManager().CopyAllCustomer(lstCustomer, 2) == (int)MTBFEnums.ReturnResult.SUCCESS)
            {
                MessageBoxHelper.ShowInformation("Successfully");
            }
            else
            {
                MessageBoxHelper.ShowInformation("Failed");
            }
        }


    }
}
