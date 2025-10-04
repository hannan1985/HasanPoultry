using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IMFS.Common.DTO;
using IFMS.BLL;
using NybSys.MTBF.Utility.Constants;
using NybSys.MTBF.Utility.Helper;
using System.IO;

namespace Tiles_Inventory
{
    public partial class frmViewMaterials : BaseForm
    {
        List<Materials> lstMaterials = new List<Materials>();

        public frmViewMaterials()
        {
            InitializeComponent();
        }

        private void frmViewMaterials_Load(object sender, EventArgs e)
        {
            LoadAllMaterials();
        }

        /// <summary>
        /// Method to load all Materials in grid.
        /// </summary>
        private void LoadAllMaterials()
        {
            DataTable MaterialsTable = new DataTable();
            MaterialsTable.Columns.Add("MaterialID");
            MaterialsTable.Columns.Add("Material Name");
            MaterialsTable.Columns.Add("Size");
            MaterialsTable.Columns.Add("Orgin");
            MaterialsTable.Columns.Add("Vendor Name");
            MaterialsTable.Columns.Add("Reorder Quantity");

            lstMaterials = new MaterialManager().GetAllMeterials().Where(c => c.BranchID == MTBFConstants.AppConstants.BranchID && c.OrganizationID == MTBFConstants.AppConstants.OrganizationID).Cast<Materials>().ToList();

            foreach (Materials material in lstMaterials)
            {
                DataRow row = MaterialsTable.NewRow();
                row["MaterialID"] = material.MaterialID;
                row["Material Name"] = material.MaterialName;
                row["Size"] = material.Size;
                row["Orgin"] = material.Origin;
                row["Vendor Name"] = material.VendorName;
                row["Reorder Quantity"] = material.ReorderQuantity;
                MaterialsTable.Rows.Add(row);
            }
            grvMaterialsInfo.DataSource = MaterialsTable;

            grvMaterialsInfo.DisplayLayout.Bands[0].Columns["MaterialID"].Hidden = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            frmMaterial frm = new frmMaterial();
            frm.ShowDialog();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (grvMaterialsInfo.Selected.Rows.Count > 0)
            {
                int materialId = Convert.ToInt32(grvMaterialsInfo.Selected.Rows[0].Cells["MaterialID"].Value);

                frmMaterial frm = new frmMaterial(materialId, true);
                frm.ShowDialog();

            }
        }

       

        private void txtMaterialsName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMaterialsName.Text))
            {
                LaodMaterialInfoByName(txtMaterialsName.Text);
            }
            else
            {
                LoadAllMaterials();
            }
        }

        private void LaodMaterialInfoByName(string materialName)
        {
            DataTable MaterialsTable = new DataTable();
            MaterialsTable.Columns.Add("MaterialID");
            MaterialsTable.Columns.Add("Material Name");
            MaterialsTable.Columns.Add("Size");
            MaterialsTable.Columns.Add("Orgin");
            MaterialsTable.Columns.Add("Vendor Name");
            MaterialsTable.Columns.Add("Reorder Quantity");
            List<Materials> lstMaterialsByName = new List<Materials>();
            lstMaterialsByName = lstMaterials.Where(c => c.MaterialName.ToLower().StartsWith(materialName)).Cast<Materials>().ToList();

            foreach (Materials material in lstMaterialsByName)
            {
                DataRow row = MaterialsTable.NewRow();
                row["MaterialID"] = material.MaterialID;
                row["Material Name"] = material.MaterialName;
                row["Size"] = material.Size;
                row["Orgin"] = material.Origin;
                row["Vendor Name"] = material.VendorName;
                row["Reorder Quantity"] = material.ReorderQuantity;
                MaterialsTable.Rows.Add(row);
            }
            grvMaterialsInfo.DataSource = MaterialsTable;

            grvMaterialsInfo.DisplayLayout.Bands[0].Columns["MaterialID"].Hidden = true;
        }

        private void btnRefresh_Click_1(object sender, EventArgs e)
        {
            LoadAllMaterials();
        }

    }
}
