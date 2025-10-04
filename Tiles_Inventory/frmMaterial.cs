using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NybSys.MTBF.Utility.Constants;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Enums;
using NybSys.MTBF.Utility.Helper;
using IFMS.BLL;

namespace Tiles_Inventory
{
    public partial class frmMaterial : BaseForm
    {
        private int _materialsID = 0;
        byte[] _selectedFile = null;

        public frmMaterial()
        {
            InitializeComponent();
        }

        public frmMaterial(int materialID, bool isEdit)
        {
            IsUpdating = isEdit;
            _materialsID = materialID;
            InitializeComponent();
        }

        private void frmMaterial_Load(object sender, EventArgs e)
        {           
            if (IsUpdating)
            {
                LoadExistingMaterialInfo();
            }
        }

        private void LoadExistingMaterialInfo()
        {
            Materials materials = new MaterialManager().GetMeterialsByID(_materialsID);
            if (materials != null)
            {
                txtMaterialsName.Text = materials.MaterialName;
                txtSize.Text = materials.Size;
                txtOrigin.Text = materials.Origin;
                txtReorderQuantity.Text = materials.ReorderQuantity.ToString();
                txtVendorName.Text = materials.VendorName;               
            }
        }

        #region "Private Events"

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidFormData())
            {

                if (base.IsUpdating)
                {
                    if (UpdateUserMaterials() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {

                        MessageBoxHelper.ShowInformation("Materials information saved successfully");                      
                        ResetAllControls();
                        txtMaterialsName.Focus();
                        IsUpdating = false;
                        _materialsID = 0;
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save materials information");
                    }
                }
                else
                {
                    if (InsertUserMaterials() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Materials information saved successfully");                     
                        ResetAllControls();
                        txtMaterialsName.Focus();
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save materials information");
                    }
                }
            }
        }

        private void ResetAllControls()
        {
            txtMaterialsName.Clear();
            txtSize.Clear();
            txtOrigin.Clear();
            txtReorderQuantity.Clear();
        
            txtVendorName.Clear();
            _selectedFile = null;
        }

        private bool ValidFormData()
        {
            if (string.IsNullOrEmpty(txtMaterialsName.Text))
            {
                MessageBoxHelper.ShowInformation("You need to enter materials name.");
                txtMaterialsName.Focus();
                return false;
            }

            if (IsDuplicateMaterials())
            {
                txtMaterialsName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtSize.Text))
            {
                MessageBoxHelper.ShowInformation("You need to enter size.");
                txtSize.Focus();
                return false;
            }


            if (string.IsNullOrEmpty(txtVendorName.Text))
            {
                MessageBoxHelper.ShowInformation("You need to enter vendor name.");
                txtVendorName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtReorderQuantity.Text))
            {
                MessageBoxHelper.ShowInformation("You need to enter reorder quantity.");
                txtReorderQuantity.Focus();
                return false;
            }


            return true;
        }




        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

      



        #endregion

        #region "Private Methods"

        /// <summary>
        /// Method to check duplicate Materials name.
        /// </summary>
        /// <returns></returns>
        private bool IsDuplicateMaterials()
        {
            List<Materials> lstMaterials = new MaterialManager().GetAllMeterials().Cast<Materials>().Where(r => r.MaterialName.ToLower() == txtMaterialsName.Text.ToLower()).ToList();

            if (lstMaterials.Count > 0 && lstMaterials[0].MaterialID != _materialsID)
            {
                MessageBoxHelper.ShowInformation("This materials is already exists");
                txtMaterialsName.Focus();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method to insert 
        /// </summary>
        /// <returns></returns>
        private int InsertUserMaterials()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            Materials materials = new Materials();
            materials.MaterialName = txtMaterialsName.Text;
            materials.Size = txtSize.Text;
            materials.Origin = txtOrigin.Text;
            materials.VendorName = txtVendorName.Text;
            materials.ReorderQuantity = Convert.ToInt32(txtReorderQuantity.Text);
            materials.ReceipeCost = _selectedFile;

            materials.BranchID = MTBFConstants.AppConstants.BranchID;
            materials.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            result = new MaterialManager().InsertMeterials(materials);
            return result;
        }

        /// <summary>
        /// Method to update Materials
        /// </summary>
        /// <returns></returns>
        private int UpdateUserMaterials()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            Materials materials = new MaterialManager().GetMeterialsByID(_materialsID);
            materials.MaterialName = txtMaterialsName.Text;
            materials.Size = txtSize.Text;
            materials.Origin = txtOrigin.Text;
            materials.VendorName = txtVendorName.Text;
            materials.ReorderQuantity = Convert.ToInt32(txtReorderQuantity.Text);
            materials.ReceipeCost = _selectedFile;
            materials.BranchID = MTBFConstants.AppConstants.BranchID;
            materials.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            result = new MaterialManager().UpdateMeterials(materials);
            return result;
        }

     

        #endregion

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtMaterialsName.Clear();
        }
    

        private bool IsValidFileFormate(string fileName)
        {
            string extenstion = fileName.Substring(fileName.LastIndexOf(".") + 1);

            if (extenstion.ToUpper() == "PDF")
            {
                return true;
            }
            else
            {
                MessageBoxHelper.ShowInformation("You can upload only pdf file");
            }
            return false;
        }

      
    }
}
