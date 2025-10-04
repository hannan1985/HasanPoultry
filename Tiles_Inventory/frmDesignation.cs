using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NybSys.MTBF.Utility.Enums;
using NybSys.MTBF.Utility.Helper;
using NybSys.MTBF.Utility.Constants;
using IMFS.Common.DTO;
using IFMS.BLL;

namespace Tiles_Inventory
{
    public partial class frmDesignation : BaseForm
    {
        private int _designationID = 0;
        public event LoadDesignationEventHandler LoadDesignation;
        public delegate void LoadDesignationEventHandler();

        public frmDesignation()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDesignationName.Text))
            {
                MessageBoxHelper.ShowInformation("You need to enter Designation name.");
                txtDesignationName.Focus();
                return;
            }

            if (!IsDuplicateDesignation())
            {
                if (base.IsUpdating)
                {
                    if (UpdateUserDesignation() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {

                        MessageBoxHelper.ShowInformation("Designation information saved successfully");
                        LoadAllDesignation();
                        txtDesignationName.Clear();
                        if (LoadDesignation != null)
                            LoadDesignation();
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save Designation information");
                    }
                }
                else
                {
                    if (InsertUserDesignation() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Designation information saved successfully");
                        LoadAllDesignation();
                        txtDesignationName.Clear();
                        if (LoadDesignation != null)
                            LoadDesignation();
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save Designation information");
                    }
                }
            }

        }


        #region "Private Methods"

        /// <summary>
        /// Method to check duplicate Designation name.
        /// </summary>
        /// <returns></returns>
        private bool IsDuplicateDesignation()
        {
            List<Designation> lstDesignation = new EmployeeManager().GetAllDesignation().Cast<Designation>().Where(r => r.DesignationName.ToLower() == txtDesignationName.Text.ToLower()).ToList();

            if (lstDesignation.Count > 0 && lstDesignation[0].DesignationID != _designationID)
            {
                MessageBoxHelper.ShowInformation("This Designation is already exists");
                txtDesignationName.Focus();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method to insert 
        /// </summary>
        /// <returns></returns>
        private int InsertUserDesignation()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            Designation designation = new Designation();
            designation.DesignationName = txtDesignationName.Text;
            result = new EmployeeManager().InsertDesignation(designation);
            return result;
        }

        /// <summary>
        /// Method to update Designation
        /// </summary>
        /// <returns></returns>
        private int UpdateUserDesignation()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            Designation designation = new EmployeeManager().GetDesignationByID(_designationID);
            designation.DesignationName = txtDesignationName.Text;
            result = new EmployeeManager().UpdateDesignation(designation);
            return result;
        }

        /// <summary>
        /// Method to load all Designation in grid.
        /// </summary>
        private void LoadAllDesignation()
        {
            DataTable DesignationTable = new DataTable();
            DesignationTable.Columns.Add("DesignationID");
            DesignationTable.Columns.Add("Designation");
            foreach (Designation Designation in new EmployeeManager().GetAllDesignation())
            {
                DataRow row = DesignationTable.NewRow();
                row["DesignationID"] = Designation.DesignationID;
                row["Designation"] = Designation.DesignationName;
                DesignationTable.Rows.Add(row);
            }
            grvDesignationInfo.DataSource = DesignationTable;

            grvDesignationInfo.DisplayLayout.Bands[0].Columns["DesignationID"].Hidden = true;
        }

        #endregion

        private void grvDesignationInfo_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            if (grvDesignationInfo .Selected.Rows.Count > 0)
            {
                _designationID  = Convert.ToInt32(grvDesignationInfo.Selected.Rows[0].Cells["DesignationID"].Value);

                Designation  designation = new EmployeeManager().GetDesignationByID(_designationID);
                if (designation != null)
                {
                    txtDesignationName.Text = designation.DesignationName ;                 
                    base.IsUpdating = true;
                }
            }
        }

        private void frmDesignation_Load(object sender, EventArgs e)
        {
            LoadAllDesignation();
        }


        
    }
}
