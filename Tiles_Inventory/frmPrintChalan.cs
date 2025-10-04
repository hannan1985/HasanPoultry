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
using Tiles_Inventory.Reports;
using IFMS.BLL;
using NybSys.MTBF.Utility.Constants;
using System.IO;

namespace Tiles_Inventory
{
    public partial class frmPrintChalan : BaseForm
    {
        int _salesID = 0;

        public frmPrintChalan(int salesID)
        {
            _salesID = salesID;
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        private void btnCustomerClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPrintChalan_Load(object sender, EventArgs e)
        {

        }

        private void SetPharmachyInforamation(ref string PharmacyName, ref string Address, ref Organization pharmacy)
        {
            pharmacy = DataAccessProxy.GetAllPharmacy().Cast<Organization>().ToList().FirstOrDefault();
            Branch branch = DataAccessProxy.GetBracnhByID(MTBFConstants.AppConstants.BranchID);
            if (pharmacy != null)
            {
                PharmacyName = branch.BranchName;
                if (!string.IsNullOrEmpty(branch.Address))
                {
                    Address = branch.Address;
                }
                if (!string.IsNullOrEmpty(branch.Phone))
                {
                    Address = Address + ", " + branch.Phone;
                }
                if (!string.IsNullOrEmpty(branch.Fax))
                {
                    Address = Address + ", " + branch.Fax;
                }


            }
        }

        /// <summary>
        /// Method to insert print invoice.
        /// </summary>
        /// <param name="salesId"></param>
        private void PrintChalan(int salesId)
        {
            string pharmacyName = string.Empty;
            string pharmacyAddress = string.Empty;
            SalesOrder salesOrder = new SalesManager().GetSalesOrderByID(salesId);
            Employee employee = new Employee();

            DataTable salesReport = new DataTable();

            Organization pharmacy = new Organization();
            salesReport = DataAccessProxy.GetCreditSalesReport(salesId);
            salesReport.Columns.Add("Box");

            rptChalan op = new rptChalan();
            frmSalesReport objmainReport = new frmSalesReport();
            salesReport.Columns["Box"].ReadOnly = false;

            op.DataSource = salesReport;
            objmainReport.rptViewer.Document = op.Document;
            objmainReport.rptViewer.Dock = DockStyle.Fill;

            SetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress, ref pharmacy);
            op.txtPharmacyName.Text = pharmacyName;
            op.txtAddress.Text = pharmacyAddress;
            op.txtDriverName.Text = txtDriverName.Text;
            op.txtDestination.Text = txtDestination.Text;
            op.txtVehicleNo.Text = txtVehicleNo.Text;
            op.txtTransport.Text = txtTransport.Text;
            op.txtNumberOfCopy.Text = "First Copy Customer";
            objmainReport.Text = "Gate Pass";
            op.txtComment.Text = txtComment.Text;
            op.txtSalesDate.Text = salesOrder.SalesDate.ToString("dd/MM/yyyy");
            if (pharmacy.OrganizationLogo != null)
            {
                MemoryStream ms = new MemoryStream(pharmacy.OrganizationLogo);
                Image returnImage = Image.FromStream(ms);
                op.picLogo.Image = returnImage;
            }
            employee = DataAccessProxy.GetEmployeeByID(salesOrder.EmployeeID);

            // op.txtNetTotal.Text = DataAccessProxy.GetTotalSalesAmountWithOutDiscount(salesId).ToString();

            //op.txtServiceMan.Text = employee.EmployeeName;
            op.Run();
            objmainReport.ShowDialog();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            GatePass gatePass = new GatePass();
            gatePass.SalesID = _salesID;
            gatePass.DriverName = txtDriverName.Text;
            gatePass.Destination = txtDestination.Text;
            gatePass.VehicleNumber = txtVehicleNo.Text;
            gatePass.Transport = txtTransport.Text;
            gatePass.Comment = txtComment.Text;
            gatePass.CreatedDate = DateTime.Now;

            new SalesManager().InsertGatePass(gatePass);


            PrintChalan(_salesID);
        }

        private void btnWholeSales_Click(object sender, EventArgs e)
        {
            txtDriverName.Clear();
            txtDestination.Clear();
            txtVehicleNo.Clear();
            txtTransport.Clear();
        }


    }
}
