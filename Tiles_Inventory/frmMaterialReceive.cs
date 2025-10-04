using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IFMS.BLL;
using IMFS.Common.DTO;
using Infragistics.Win.UltraWinGrid;
using NybSys.MTBF.Utility.Helper;
using NybSys.MTBF.Utility.Enums;
using NybSys.MTBF.Utility.Constants;

namespace Tiles_Inventory
{
    public partial class frmMaterialReceive : BaseForm
    {
        int _materialReceiveID = 0;

        public frmMaterialReceive()
        {
            InitializeComponent();
        }

        public frmMaterialReceive(int materialReceiveID, bool isEdit)
        {
            _materialReceiveID = materialReceiveID;
            IsUpdating = isEdit;
            InitializeComponent();
        }

        private void frmMaterialReceive_Load(object sender, EventArgs e)
        {
            grvProductDetails.DataSource = BuildMaterialTable();
            grvProductDetails.DisplayLayout.Bands[0].Columns["MaterialID"].Hidden = true;
            LoadMaterialCombo();
            LoadSupplierCombo();
            if (IsUpdating)
            {
                LoadExistingMaterialsReceiveInformation();
                LoadExistingMaterialsReceiveDetailInformation();
            }
        }


        private void LoadExistingMaterialsReceiveDetailInformation()
        {
            foreach (MaterialsReceiveDetails dDetail in new MaterialReceiveManager().GetAllMaterialsReceiveDetailByMaterialsReceiveID(_materialReceiveID))
            {
                UltraGridRow row = grvProductDetails.DisplayLayout.Bands[0].AddNew();
                Materials material = new MaterialManager().GetMeterialsByID(dDetail.MaterialID);
                row.Cells["MaterialID"].Value = dDetail.MaterialID;
                row.Cells["Material Name"].Value = (material != null) ? material.MaterialName : string.Empty;
                row.Cells["Quantity"].Value = dDetail.Quantity;
                row.Cells["Price"].Value = dDetail.Price;
                row.Cells["Total"].Value = dDetail.Total;
                row.Cells["Delete"].Value = "Delete";
            }
        }

        private void LoadExistingMaterialsReceiveInformation()
        {
            MaterialsReceive materialReceive = new MaterialReceiveManager().GetMaterialsReceiveByID(_materialReceiveID);
            if (materialReceive != null)
            {
                dtpDistributionDate.Value = materialReceive.ReceiveDate;
                txtPaidAmount.Text = materialReceive.PaidAmount.ToString();
                txtTotal.Text = materialReceive.Total.ToString(); ;
            }
        }

        private void LoadMaterialCombo()
        {
            DataTable MaterialsTable = new DataTable();
            MaterialsTable.Columns.Add("MaterialID");
            MaterialsTable.Columns.Add("Material Name");
            List<Materials> lstMaterials = new List<Materials>();
            lstMaterials = new MaterialManager().GetAllMeterials().Where(c => c.BranchID == MTBFConstants.AppConstants.BranchID && c.OrganizationID == MTBFConstants.AppConstants.OrganizationID).Cast<Materials>().ToList();


            foreach (Materials material in lstMaterials )
            {
                DataRow row = MaterialsTable.NewRow();
                row["MaterialID"] = material.MaterialID;
                row["Material Name"] = material.MaterialName;
                MaterialsTable.Rows.Add(row);
            }
            cmbProductInformation.DataSource = MaterialsTable;
            cmbProductInformation.DisplayMember = "Material Name";
            cmbProductInformation.ValueMember = "MaterialID";
            cmbProductInformation.DisplayLayout.Bands[0].Columns["MaterialID"].Hidden = true;
        }


        private void LoadSupplierCombo()
        {
            DataTable MaterialsTable = new DataTable();
            MaterialsTable.Columns.Add("SupplierID");
            MaterialsTable.Columns.Add("Supplier Name");
            List<MaterialSupplier> lstMaterialSupplier = new List<MaterialSupplier>();
            lstMaterialSupplier = new MaterialSupplierManager().GetAllMaterialSupplier().Where(m => m.BranchID == MTBFConstants.AppConstants.BranchID && m.OrganizationID == MTBFConstants.AppConstants.OrganizationID).Cast<MaterialSupplier>().ToList();


            foreach (MaterialSupplier material in lstMaterialSupplier)
            {
                DataRow row = MaterialsTable.NewRow();
                row["SupplierID"] = material.SupplierID;
                row["Supplier Name"] = material.SupplierName;
                MaterialsTable.Rows.Add(row);
            }
            cmbSupplier.DataSource = MaterialsTable;
            cmbSupplier.DisplayMember = "Supplier Name";
            cmbSupplier.ValueMember = "SupplierID";
            cmbSupplier.DisplayLayout.Bands[0].Columns["SupplierID"].Hidden = true;
        }


        private DataTable BuildMaterialTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("MaterialID");
            table.Columns.Add("Material Name");
            table.Columns.Add("Quantity");
            table.Columns.Add("Price");
            table.Columns.Add("Total");
            table.Columns.Add("Delete");
            return table;

        }


        private void btAdd_Click(object sender, EventArgs e)
        {
            if (ValidAddData())
            {
                AddDataInGrid();
                CalculateTotalAmount();
                txtQuantity.Value = 0;
                txtPrice.Value = 0;
                cmbProductInformation.Focus();
            }

        }

        private bool ValidAddData()
        {
            int meterialID = 0;


            if (cmbProductInformation.Value != null)
            {
                int.TryParse(cmbProductInformation.Value.ToString(), out meterialID);
                Materials material = new MaterialManager().GetMeterialsByID(meterialID);

                if (material == null)
                {
                    MessageBoxHelper.ShowInformation("Invalid material informaiton.");
                    cmbProductInformation.Focus();
                }

            }

            decimal quantity = 0;
            decimal price = 0;
            decimal.TryParse(txtQuantity.Text, out quantity);
            decimal.TryParse(txtPrice.Text, out price);

            if (quantity == 0)
            {
                MessageBoxHelper.ShowInformation("You need to enter quantity.");
                txtQuantity.Focus();
            }

            if (price == 0)
            {
                MessageBoxHelper.ShowInformation("You need to enter price.");
                txtPrice.Focus();
            }

            return true;
        }


        private void AddDataInGrid()
        {
            UltraGridRow row = grvProductDetails.DisplayLayout.Bands[0].AddNew();
            decimal price = Convert.ToDecimal(txtPrice.Text);
            row.Cells["MaterialID"].Value = Convert.ToInt32(cmbProductInformation.Value);
            row.Cells["Material Name"].Value = cmbProductInformation.Text;
            row.Cells["Quantity"].Value = Convert.ToDecimal(txtQuantity.Value);
            row.Cells["Price"].Value = price;
            row.Cells["Total"].Value = Convert.ToDecimal(txtQuantity.Value) * price;
            row.Cells["Delete"].Value = "Delete";
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (ValidFormData())
            {
                if (IsUpdating)
                {
                    if (UpdateMaterialReceiveInformation() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("MaterialsReceive information saved successfully.");
                        IsUpdating = false;
                        _materialReceiveID = 0;
                        grvProductDetails.DataSource = BuildMaterialTable();
                        grvProductDetails.DisplayLayout.Bands[0].Columns["MaterialID"].Hidden = true;
                        CalculateTotalAmount();
                        cmbSupplier.Focus();
                        txtPaidAmount.Text = 0.ToString();
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save distribution information.");
                    }

                }
                else
                {
                    if (InsertMaterialReceiveInformation() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("MaterialsReceive information saved successfully.");
                        grvProductDetails.DataSource = BuildMaterialTable();
                        grvProductDetails.DisplayLayout.Bands[0].Columns["MaterialID"].Hidden = true;
                        CalculateTotalAmount();
                        cmbSupplier.Focus();
                        txtPaidAmount.Text = 0.ToString();

                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save distribution information.");
                    }
                }
            }
        }


        private int InsertMaterialReceiveInformation()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            decimal paidAmount = 0;
            decimal.TryParse(txtPaidAmount.Text, out paidAmount);
            List<MaterialsReceiveDetails> lstMaterialsReceiveDetail = new List<MaterialsReceiveDetails>();
            MaterialsReceive materialReceive = new MaterialsReceive();
            materialReceive.SupplierID = Convert.ToInt32(cmbSupplier.Value);
            materialReceive.Total = Convert.ToDecimal(txtTotal.Text);
            materialReceive.PaidAmount = paidAmount;
            materialReceive.ReceiveDate = dtpDistributionDate.Value;
            materialReceive.Status = (int)MTBFEnums.MaterialReciveStatus.Issued;
            materialReceive.BranchID = MTBFConstants.AppConstants.BranchID;
            materialReceive.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            lstMaterialsReceiveDetail = GetAllMaterialsReceiveDetailInformation();

            result = new MaterialReceiveManager().InsertMaterialsReceive(materialReceive, lstMaterialsReceiveDetail);
            return result;
        }


        private int UpdateMaterialReceiveInformation()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            decimal paidAmount = 0;
            decimal.TryParse(txtPaidAmount.Text, out paidAmount);
            List<MaterialsReceiveDetails> lstMaterialsReceiveDetail = new List<MaterialsReceiveDetails>();
            MaterialsReceive materialReceive = new MaterialReceiveManager().GetMaterialsReceiveByID(_materialReceiveID);
            materialReceive.SupplierID = Convert.ToInt32(cmbSupplier.Value);
            materialReceive.Total = Convert.ToDecimal(txtTotal.Text);
            materialReceive.PaidAmount = paidAmount;
            materialReceive.ReceiveDate = dtpDistributionDate.Value;
            materialReceive.BranchID = MTBFConstants.AppConstants.BranchID;
            materialReceive.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            lstMaterialsReceiveDetail = GetAllMaterialsReceiveDetailInformation();

            result = new MaterialReceiveManager().UpdateMaterialsReceive(materialReceive, lstMaterialsReceiveDetail);
            return result;
        }

        private List<MaterialsReceiveDetails> GetAllMaterialsReceiveDetailInformation()
        {
            List<MaterialsReceiveDetails> lstMaterialsReceiveDetail = new List<MaterialsReceiveDetails>();

            foreach (UltraGridRow row in grvProductDetails.Rows)
            {
                MaterialsReceiveDetails distributionDetail = new MaterialsReceiveDetails();
                distributionDetail.MaterialID = Convert.ToInt32(row.Cells["MaterialID"].Value);
                distributionDetail.Quantity = Convert.ToDecimal(row.Cells["Quantity"].Text);
                distributionDetail.Price = Convert.ToDecimal(row.Cells["Price"].Value);
                distributionDetail.Total = Convert.ToDecimal(row.Cells["Total"].Text);
                lstMaterialsReceiveDetail.Add(distributionDetail);
            }

            return lstMaterialsReceiveDetail;
        }


        private bool ValidFormData()
        {
            if (grvProductDetails.Rows.Count == 0)
            {
                MessageBoxHelper.ShowInformation("You need to add data in grid.");
                return false;
            }

            return true;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvProductDetails_CellChange(object sender, CellEventArgs e)
        {


        }
        private void CalculateTotalAmount()
        {
            decimal total = 0;


            foreach (UltraGridRow row in grvProductDetails.Rows)
            {
                total = total + Convert.ToDecimal(row.Cells["Total"].Value);
            }

            txtTotal.Text = total.ToString("F2");
        }

        private void grvProductDetails_ClickCell(object sender, ClickCellEventArgs e)
        {
            if (e.Cell.Column.Header.Caption == "Delete")
            {
                grvProductDetails.Rows[e.Cell.Row.Index].Delete();
                CalculateTotalAmount();
            }
        }

        private void txtQuantity_KeyUp(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 13)
            {
                if (ValidAddData())
                {
                    AddDataInGrid();
                    CalculateTotalAmount();
                    cmbProductInformation.Focus();
                    txtQuantity.Value = 0;
                    txtPrice.Value = 0;
                }
            }
        }

        private void cmbProductInformation_KeyUp(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 13)
            {
                txtPrice.Focus();
            }
        }

        private void txtPrice_KeyUp(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 13)
            {
                txtQuantity.Focus();
            }
        }



    }
}
