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
using Infragistics.Win.UltraWinGrid;
using NybSys.MTBF.Utility.Helper;
using NybSys.MTBF.Utility.Constants;

namespace Tiles_Inventory
{
    public partial class frmViewCompany : BaseForm
    {
        private int _companyID = 0;

        public frmViewCompany()
        {
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }


        private void frmViewCompany_Load(System.Object sender, System.EventArgs e)
        {
            LoadCompanyName();
            LoadCompanyInformation();
        }


        /// <summary>
        /// Method to load company informaiton in combo box.
        /// </summary>
        private void LoadCompanyName()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("Name");

            DataRow emptyrow = table.NewRow();
            emptyrow[0] = -1;
            emptyrow[1] = "All";
            table.Rows.Add(emptyrow);


            List<Company> lstCompany = new List<Company>();
            lstCompany = DataAccessProxy.GetAllCompany().Cast<Company>().ToList(); //.Where(s => s.BranchID == MTBFConstants.AppConstants.BranchID && s.OrganizationID == MTBFConstants.AppConstants.OrganizationID)

            foreach (Company company in lstCompany)
            {
                DataRow row = table.NewRow();
                row[0] = company.CompanyID;
                row[1] = company.CompanyName;
                table.Rows.Add(row);
            }

            cmbCompanyName.DisplayMember = "Name";
            cmbCompanyName.ValueMember = "ID";
            cmbCompanyName.DataSource = table;
            cmbCompanyName.Value = -1;
        }


        /// <summary>
        /// Method to load supplier information in gid.
        /// </summary>
        /// <param name="companyId"></param>
        /// <remarks></remarks>

        private void LoadCompanyInformationByID(int companyId)
        {
            List<Company> lstCompany = new List<Company>();

            lstCompany = companyId == -1 ? DataAccessProxy.GetAllCompany().Cast<Company>().ToList() : DataAccessProxy.GetAllCompany().Cast<Company>().Where(c => c.CompanyID == Convert.ToInt32(cmbCompanyName.Value)).ToList();


            foreach (Company company in lstCompany)
            {
                grvCompanyInformation.Rows.Add(company.CompanyID, company.CompanyName, company.ContactPerson, company.Phone, company.Email, company.CompanyAddress);
            }

            grvCompanyInformation.Columns[0].Visible = false;
            grvCompanyInformation.ClearSelection();

        }


        /// <summary>
        /// Method to load company information in grid.
        /// </summary>
        private void LoadCompanyInformation()
        {
            grvCompanyInformation.Rows.Clear();
            List<Company> lstCompany = DataAccessProxy.GetAllCompany().Cast<Company>().ToList();//.Cast<Company>().Where(s => s.BranchID == MTBFConstants.AppConstants.BranchID && s.OrganizationID == MTBFConstants.AppConstants.OrganizationID)

            foreach (Company company in lstCompany)
            {
                grvCompanyInformation.Rows.Add(company.CompanyID, company.CompanyName, company.ContactPerson, company.Phone, company.Email, company.CompanyAddress);
            }

            grvCompanyInformation.Columns[0].Visible = false;
            grvCompanyInformation.ClearSelection();
        }


        private void btnEdit_Click(System.Object sender, System.EventArgs e)
        {
            if (grvCompanyInformation.SelectedRows.Count > 0 && !grvCompanyInformation.SelectedRows[0].IsNewRow && grvCompanyInformation.SelectedRows[0].Cells[0].Value != null)
            {
                _companyID = Convert.ToInt32(grvCompanyInformation.SelectedRows[0].Cells[0].Value);

                CompanyInformation frm = new CompanyInformation(_companyID, true);
                frm.OnLoadCompanyInformation += OnCompanyInformationLoad;
                frm.ShowDialog();

            }
            else
            {
                MessageBoxHelper.ShowInformation("Please select a row");

            }
        }

        private void OnCompanyInformationLoad()
        {
            grvCompanyInformation.Rows.Clear();
            LoadCompanyInformationByID(Convert.ToInt32(cmbCompanyName.Value));
        }

        private void btnRefresh_Click(System.Object sender, System.EventArgs e)
        {
            grvCompanyInformation.Rows.Clear();
            LoadCompanyInformation();
        }

        private void btnClose_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(System.Object sender, System.EventArgs e)
        {
            CompanyInformation frm = new CompanyInformation(_companyID, false);
            frm.OnLoadCompanyInformation += OnCompanyNameLoad;
            frm.ShowDialog();
        }

        private void OnCompanyNameLoad()
        {
            grvCompanyInformation.Rows.Clear();
            LoadCompanyInformation();
        }

        private void cmbCompanyName_ValueChanged(object sender, EventArgs e)
        {
            if ((cmbCompanyName.Value != null))
            {
                grvCompanyInformation.Rows.Clear();
                LoadCompanyInformationByID(Convert.ToInt32(cmbCompanyName.Value));
            }
        }

        private void cmbCompanyName_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridLayout layout = e.Layout;
            layout.PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
        }



    }
}
