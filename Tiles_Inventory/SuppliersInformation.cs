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
using IMFS.Common.Utility;
using NybSys.MTBF.Utility.Helper;
using NybSys.MTBF.Utility.Constants;
using IFMS.BLL;
using NybSys.MTBF.Utility.Enums;

namespace Tiles_Inventory
{
    public partial class SuppliersInformation : BaseForm
    {
        private int _supplierID = 0;
        public delegate void LodaEventHandaler();
        public event LodaEventHandaler OnLoadSupplierInformation;
        Supplier _supplier = new Supplier();


        public SuppliersInformation(int supplierID, bool isEdit)
        {
            if (isEdit)
            {
                IsUpdating = isEdit;
                _supplierID = supplierID;
            }
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        private void SuppliersInformation_Load(System.Object sender, System.EventArgs e)
        {           
            if (IsUpdating)
            {
                LoadSupplierInformation();
            }
        }

        private bool ValidSaveData()
        {           

            if (string.IsNullOrEmpty(txtSupplier.Text.Trim()))
            {
                MessageBoxHelper.ShowInformation("You need to enter supplier name.");
                txtSupplier.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtSupplierPhone.Text.Trim()))
            {
                MessageBoxHelper.ShowInformation("You need to enter supplier phone.");
                txtSupplierPhone.Focus();
                return false;
            }

            return true;
        }

        private void btnSupplierSave_Click(System.Object sender, System.EventArgs e)
        {
            if (ValidSaveData())
            {
                if (SaveSupplier() == (int)MTBFEnums.ReturnResult.SUCCESS)
                {
                    MessageBoxHelper.ShowInformation("Supplier information  saved successfully.");
                    this.ResetAllControls();
                    if (OnLoadSupplierInformation != null)
                    {
                        OnLoadSupplierInformation();
                    }
                }
                else
                {
                    MessageBoxHelper.ShowInformation("Failed to save information.");
                }
            }


        }

        /// <summary>
        /// Method to check duplicate child account.
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        private bool IsDuplicateChildAccount(int supplierID)
        {
            ChildAccount childAccount = DataAccessProxy.GetChildAccountBySupplierID(supplierID);
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
        private int InsertChildAccount(int supplierID)
        {
            int result = 0;
            try
            {
                ChildAccount childAccount = new ChildAccount();
                childAccount.AccountID = IFMSConstant.AccountPayableID;
                childAccount.Description = txtSupplier.Text;
                Employee employee = DataAccessProxy.GetEmployeeByID(IFMSConstant.LoggedinUserID);
                childAccount.SupplierID = supplierID;
                childAccount.CreatedBy = (employee != null) ? employee.EmployeeName : string.Empty;
                childAccount.CreatedDate = DateTime.Now;
                childAccount.Status = 1;
                result = DataAccessProxy.InsertChildAccount(childAccount);

            }
            catch (Exception ex)
            {
                MessageBoxHelper.ShowInformation("Failed to save child account information");
                result = 0;
            }
            return result;
        }




     

        /// <summary>
        /// Method to insert supplier information.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private int SaveSupplier()
        {
            _supplier.CompanyID =1;
            _supplier.SupplierName = txtSupplier.Text;
            _supplier.PhoneNumber = txtSupplierPhone.Text;
            _supplier.Discontinued = cbDiscontinued.Checked;
            _supplier.Address = txtAddress.Text;
            _supplier.Email = txtEmail.Text;
            _supplier.BranchID = MTBFConstants.AppConstants.BranchID;
            _supplier.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            return new SupplierManager().SaveSupplierInformation(_supplier);
        }

     
        /// <summary>
        /// Method to reset all control .
        /// </summary>
        /// <remarks></remarks>
        private void ResetAllControls()
        {
            txtSupplier.Clear();
            txtSupplierPhone.Clear();
            txtAddress.Clear();
            txtEmail.Clear();
            cbDiscontinued.Checked = false;
            IsUpdating = false;
            txtSupplier.Focus();
        }

        /// <summary>
        /// Method to load supplier information for update.
        /// </summary>
        /// <remarks></remarks>
        private void LoadSupplierInformation()
        {
            _supplier = DataAccessProxy.GetSupplierByID(_supplierID);          
            txtSupplier.Text = _supplier.SupplierName;
            txtAddress.Text = _supplier.Address;
            txtEmail.Text = _supplier.Email;
            txtSupplierPhone.Text = _supplier.PhoneNumber;
            cbDiscontinued.Checked = _supplier.Discontinued;

        }

        private void btnSupplierClose_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

     

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetAllControls();
        }

    }
}
