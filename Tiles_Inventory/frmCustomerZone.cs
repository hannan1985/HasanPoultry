using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NybSys.MTBF.Utility.Resources;
using NybSys.MTBF.Utility.Helper;
using IMFS.Common.DTO;
using IFMS.Facade;
using NybSys.MTBF.Utility.Constants;
using IFMS.BLL;
using NybSys.MTBF.Utility.Enums;


namespace Tiles_Inventory
{
    public partial class frmCustomerZone : BaseForm
    {

        public event LoadProductTypeEventHandler LoadZoneInfo;
        public delegate void LoadProductTypeEventHandler();
        int zoneID = 0;

        public frmCustomerZone()
        {
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        private void btnSave_Click(System.Object sender, System.EventArgs e)
        {

            if (IsValidFormData())
            {
                if (IsUpdating)
                {
                    if (UpdateZoneInformation() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Zone informaiton saved successfully.");
                        if (LoadZoneInfo != null)
                        {
                            LoadZoneInfo();
                        }
                        LoadZoneInformation();
                        IsUpdating = false;
                        zoneID = 0;
                        txtZoneName.Clear();
                        txtDescription.Clear();
                        txtZoneName.Focus();
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save zone information");
                    }
                }
                else
                {
                    if (InsertZoneInformation() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Zone informaiton saved successfully.");
                        if (LoadZoneInfo != null)
                        {
                            LoadZoneInfo();
                        }
                        LoadZoneInformation();
                        txtZoneName.Clear();
                        txtDescription.Clear();
                        txtZoneName.Focus();
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save zone information");
                    }
                }
            }
        }


        private int InsertZoneInformation()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            Zone zone = new Zone();
            zone.ZoneName = txtZoneName.Text;
            zone.Description = txtDescription.Text;
            zone.BranchID = MTBFConstants.AppConstants.BranchID;
            zone.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            result = new CustomerManager().InsertZone(zone);
            return result;
        }


        private int UpdateZoneInformation()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            Zone zone = new CustomerManager().GetZoneByID(zoneID);
            zone.ZoneName = txtZoneName.Text;
            zone.Description = txtDescription.Text;
            zone.BranchID = MTBFConstants.AppConstants.BranchID;
            zone.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            result = new CustomerManager().UpdateZone(zone);
            return result;
        }


        private void btnClose_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Method to check duplicate record.
        /// </summary>
        /// <returns></returns>
        private bool IsValidFormData()
        {

            if (string.IsNullOrEmpty(txtZoneName.Text.Trim()))
            {
                MessageBoxHelper.ShowInformation("You need to enter zone name.");
                txtZoneName.Focus();
                return false;
            }

            Zone zone = new CustomerManager().GetAllZone().Cast<Zone>().Where(s => s.ZoneName.ToUpper().Trim() == txtZoneName.Text.ToUpper().Trim()).ToList().FirstOrDefault();

            if (zone != null)
            {
                MessageBoxHelper.ShowInformation("Duplicate zone information.");
                return false;
            }
            return true;
        }

        private DataTable BuildSizeTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ZoneID");
            table.Columns.Add("Zone Name");
            table.Columns.Add("Description");
            return table;
        }

        private void LoadZoneInformation()
        {
            List<Zone> lstZone = new List<Zone>();
            lstZone = new CustomerManager().GetAllZone().Where(s => s.BranchID == MTBFConstants.AppConstants.BranchID && s.OrganizationID == MTBFConstants.AppConstants.OrganizationID).Cast<Zone>().ToList();
            DataTable table = BuildSizeTable();
            foreach (Zone zone in lstZone)
            {
                DataRow row = table.NewRow();
                row["ZoneID"] = zone.ZoneID;
                row["Zone Name"] = zone.ZoneName;
                row["Description"] = zone.Description;
                table.Rows.Add(row);
            }
            grvZoneInformation.DataSource = table;
            grvZoneInformation.DisplayLayout.Bands[0].Columns["ZoneID"].Hidden = true;
        }


        private void txtProductSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (".1234567890".IndexOf(e.KeyChar) > -1 | e.KeyChar == Convert.ToChar(8))
            //{
            //    e.Handled = false;
            //}
            //else
            //{
            //    e.Handled = true;
            //}
        }

        private void frmProductSize_Load(object sender, EventArgs e)
        {
            LoadZoneInformation();
        }

        private void grvProductType_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            if (grvZoneInformation.Selected.Rows.Count > 0)
            {
                int pSizeID = Convert.ToInt32(grvZoneInformation.Selected.Rows[0].Cells["ZoneID"].Value);
                Zone zone = new CustomerManager().GetZoneByID(zoneID);
                if (zone != null)
                {
                    txtZoneName.Text = zone.ZoneName;
                    txtDescription.Text = zone.Description;
                    IsUpdating = true;
                    zoneID = pSizeID;
                }

            }
        }
    }
}
