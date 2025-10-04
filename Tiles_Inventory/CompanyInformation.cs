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

namespace Tiles_Inventory
{
    public partial class CompanyInformation : BaseForm
    {
        public delegate void LodaEventHandaler();
        public event LodaEventHandaler OnLoadCompanyInformation;
        private int _CompanyID = 0;

        public CompanyInformation(int companyID, bool isEdit)
        {
            _CompanyID = companyID;
            IsUpdating = isEdit;
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }


        /// <summary>
        /// Method to update company information.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private int UpdateCompanyInformation()
        {
            int result = 0;
            Company company = DataAccessProxy.GetCompanyByID(_CompanyID);
            try
            {
                company.CompanyName = txtCompanyName.Text;
                company.CompanyAddress = txtCompanyAddress.Text;
                company.Phone = txtPhone.Text;
                company.ContactPerson = txtContactPerson.Text;
                company.Email = txtEmail.Text;
                company.BranchID = MTBFConstants.AppConstants.BranchID;
                company.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                DataAccessProxy.UpdateCompany(company);
                result = 1;

                this.ResetAllControls();

            }
            catch (Exception ex)
            {
                result = 0;
                MessageBoxHelper.ShowInformation("Operation faild please try again.");
            }
            return result;
        }

        /// <summary>
        /// Method to insert company information.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private int InsertCompanyInformation()
        {
            int result = 0;
            Company company = new Company();

            try
            {
                company.CompanyName = txtCompanyName.Text;
                company.CompanyAddress = txtCompanyAddress.Text;
                company.Phone = txtPhone.Text;
                company.ContactPerson = txtContactPerson.Text;
                company.Email = txtEmail.Text;
                company .BranchID = MTBFConstants.AppConstants.BranchID;
               company .OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                DataAccessProxy.InsertCompany(company);
                result = 1;
                this.ResetAllControls();

            }
            catch (Exception ex)
            {
                result = 0;
                MessageBoxHelper.ShowInformation("Operation faild please try again.");
            }

            return result;
        }

        /// <summary>
        /// Method to load company information for update.
        /// </summary>
        /// <remarks></remarks>
        private void LoadCompanyInformation()
        {
            Company company = DataAccessProxy.GetCompanyByID(_CompanyID);
            txtCompanyName.Text = company.CompanyName;
            txtContactPerson.Text = company.ContactPerson;
            txtCompanyAddress.Text = company.CompanyAddress;
            txtPhone.Text = company.Phone;
            txtEmail.Text = company.Email;
        }

        private void ResetAllControls()
        {
            txtCompanyAddress.Text = string.Empty;
            txtCompanyName.Text = string.Empty;
            txtContactPerson.Text = string.Empty;
            txtPhone.Clear();
            txtEmail.Clear();
            txtCompanyName.Focus();
        }

        private bool ValidFormData()
        {
            if (txtCompanyName.Text == string.Empty)
            {
                MessageBoxHelper.ShowInformation("You need to enter company name.");
                txtCompanyName.Focus();
                return false;
            }
            if (txtContactPerson.Text == string.Empty)
            {
                MessageBoxHelper.ShowInformation("You need to enter contract person name.");
                txtContactPerson.Focus();
                return false;
            }

            if (txtPhone.Text == string.Empty)
            {
                MessageBoxHelper.ShowInformation("You need to enter phone number.");
                txtPhone.Focus();
                return false;
            }

            return true;
        }

        private void btnSave_Click(System.Object sender, System.EventArgs e)
        {
            if (ValidFormData())
            {
                if (IsUpdating)
                {
                    if (UpdateCompanyInformation() > 0)
                    {
                        MessageBoxHelper.ShowInformation("Company information  saved successfully.");
                        if (OnLoadCompanyInformation != null)
                        {
                            OnLoadCompanyInformation();
                        }
                        IsUpdating = false;
                    }
                }
                else
                {
                    if (InsertCompanyInformation() > 0)
                    {
                        MessageBoxHelper.ShowInformation("Company information  saved successfully.");
                        if (OnLoadCompanyInformation != null)
                        {
                            OnLoadCompanyInformation();
                        }
                    }
                }
            }
        }

        private void CompanyInformation_Load(System.Object sender, System.EventArgs e)
        {
            if (IsUpdating)
            {
                LoadCompanyInformation();
            }
        }

        private void btnClose_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnAddNewSupplier_Click(System.Object sender, System.EventArgs e)
        {
            SuppliersInformation frm = new SuppliersInformation(0, false);
            frm.ShowDialog();
        }


    }
}
