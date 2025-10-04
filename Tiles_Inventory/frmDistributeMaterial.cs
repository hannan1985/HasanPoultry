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
using NybSys.MTBF.Utility.Helper;
using Infragistics.Win.UltraWinGrid;
using NybSys.MTBF.Utility.Enums;
using NybSys.MTBF.Utility.Constants;

namespace Tiles_Inventory
{
    public partial class frmDistributeMaterial : BaseForm
    {
        int _distributionID = 0;
        List<MaterialsReceiveDetails> lstStockDetail = new List<MaterialsReceiveDetails>();

        public frmDistributeMaterial()
        {
            InitializeComponent();
        }

        public frmDistributeMaterial(int distributionID, bool isEdit)
        {
            _distributionID = distributionID;
            IsUpdating = isEdit;
            InitializeComponent();
        }

        private void frmDistributeMaterial_Load(object sender, EventArgs e)
        {
            LoadMaterialCombo();
            grvProductDetails.DataSource = BuildMaterialTable();
            grvProductDetails.DisplayLayout.Bands[0].Columns["MaterialID"].Hidden = true;
            LoadProductionUnitCombo();
            dtpDistributionDate.Value = DateTime.Now;
            if (IsUpdating)
            {
                LoadExistingDistributionInformation();
                LoadExistingDistributionDetailInformation();
            }
        }

        private void LoadProductionUnitCombo()
        {
            DataTable MaterialsTable = new DataTable();
            MaterialsTable.Columns.Add("UnitID");
            MaterialsTable.Columns.Add("Unit Name");
            foreach (ProductionUnit material in new ProductionUnitManager().GetAllProductionUnit())
            {
                DataRow row = MaterialsTable.NewRow();
                row["UnitID"] = material.UnitID;
                row["Unit Name"] = material.UnitName;
                MaterialsTable.Rows.Add(row);
            }
            cmbProductionUnit.DataSource = MaterialsTable;
            cmbProductionUnit.DisplayMember = "Unit Name";
            cmbProductionUnit.ValueMember = "UnitID";
            cmbProductionUnit.DisplayLayout.Bands[0].Columns["UnitID"].Hidden = true;
        }

        private void LoadExistingDistributionDetailInformation()
        {
            foreach (DistributeDetails dDetail in new MaterialDistributionManager().GetAllDistributionDetailByDistributionID(_distributionID))
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

        private void LoadExistingDistributionInformation()
        {
            Distribution distribution = new MaterialDistributionManager().GetDistributionByID(_distributionID);
            if (distribution != null)
            {
                dtpDistributionDate.Value = distribution.DistributeDate;
                cmbProductionUnit.Value = distribution.ProductionUnitID;
                txtTotal.Text = distribution.Total.ToString();
            }
        }

        private void LoadMaterialCombo()
        {
            lstStockDetail = new MaterialReceiveManager().GetAllMaterialStock(MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID);
            lstStockDetail = RemoveGridAddedProductQuantity(lstStockDetail);

            lstStockDetail = RemoveDamageProduct(lstStockDetail);

            DataTable MaterialsTable = new DataTable();
            MaterialsTable.Columns.Add("MaterialID");
            MaterialsTable.Columns.Add("Material Name");
            MaterialsTable.Columns.Add("Quantity");

            foreach (MaterialsReceiveDetails sDetail in lstStockDetail)
            {
                Materials material = new MaterialManager().GetMeterialsByID(sDetail.MaterialID);
                DataRow row = MaterialsTable.NewRow();
                row["MaterialID"] = material.MaterialID;
                row["Material Name"] = material.MaterialName;
                row["Quantity"] = sDetail.Quantity;
                MaterialsTable.Rows.Add(row);
            }
            cmbProductInformation.DataSource = MaterialsTable;
            cmbProductInformation.DisplayMember = "Material Name";
            cmbProductInformation.ValueMember = "MaterialID";
            cmbProductInformation.DisplayLayout.Bands[0].Columns["MaterialID"].Hidden = true;
        }

        private decimal CalculateDamageQuantityByMaterialID(int materialID)
        {
            decimal damageQty = 0;

            List<DamageDetail> lstDamageDetail = new List<DamageDetail>();
            string filter = string.Format("{0}={1}", "MaterialID", materialID);
            lstDamageDetail = new DamageManager().GetFilteredDamageDetail(filter);

            foreach (DamageDetail dDetail in lstDamageDetail)
            {
                damageQty = damageQty + dDetail.Quantity;
            }
            return damageQty;

        }

        /// <summary>
        /// Method to remove damage product.
        /// </summary>
        /// <param name="lstProductInfo"></param>
        /// <returns></returns>
        private List<MaterialsReceiveDetails> RemoveDamageProduct(List<MaterialsReceiveDetails> lstProductInfo)
        {
            List<MaterialsReceiveDetails> lstFilteredProductInfo = new List<MaterialsReceiveDetails>();
            foreach (MaterialsReceiveDetails productifo in lstProductInfo)
            {
                decimal damageQty = CalculateDamageQuantityByMaterialID(productifo.MaterialID);
                productifo.Quantity = (productifo.Quantity - damageQty);
                lstFilteredProductInfo.Add(productifo);
            }

            return lstFilteredProductInfo;
        }



        private List<MaterialsReceiveDetails> RemoveGridAddedProductQuantity(List<MaterialsReceiveDetails> lstProductInfo)
        {
            MaterialsReceiveDetails product = null;

            for (int i = 0; i < grvProductDetails.Rows.Count; i++)
            {
                if (grvProductDetails.Rows[i].Cells["MaterialID"].Value != null)
                {
                    int materialID = Convert.ToInt32(grvProductDetails.Rows[i].Cells["MaterialID"].Value);
                    int quantity = Convert.ToInt32(grvProductDetails.Rows[i].Cells["Quantity"].Value);

                    int listCount = lstProductInfo.Count;


                    for (int j = 0; j < listCount; j++)
                    {
                        product = lstProductInfo[j];
                        if (materialID == product.MaterialID)
                        {
                            if (product.Quantity > quantity)
                            {
                                lstProductInfo.RemoveAt(j);
                                product.Quantity = product.Quantity - quantity;
                                lstProductInfo.Add(product);
                                break;
                            }
                            else if (product.Quantity <= quantity)
                            {
                                lstProductInfo.RemoveAt(j);
                                break;
                            }
                        }
                    }
                }
            }
            return lstProductInfo;
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
                cmbProductInformation.Focus();
                LoadMaterialCombo();
            }

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
                    return false;
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
                return false;
            }

            //if (price == 0)
            //{
            //    MessageBoxHelper.ShowInformation("You need to enter price.");
            //    txtPrice.Focus();
            //    return false;
            //}

            MaterialsReceiveDetails mDetail = lstStockDetail.Where(m => m.MaterialID == Convert.ToInt32(cmbProductInformation.Value)).Cast<MaterialsReceiveDetails>().ToList().FirstOrDefault();

            if (mDetail != null)
            {
                if (quantity > mDetail.Quantity)
                {
                    MessageBoxHelper.ShowWarning("Please check quantity availability.");
                    txtQuantity.Focus();
                    return false;
                }
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
                    if (UpdateDistributionInformation() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Distribution information saved successfully.");
                        grvProductDetails.DataSource = BuildMaterialTable();
                        grvProductDetails.DisplayLayout.Bands[0].Columns["MaterialID"].Hidden = true;
                        IsUpdating = false;
                        CalculateTotalAmount();
                        cmbProductionUnit.Focus();
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save distribution information.");
                    }

                }
                else
                {
                    if (InsertDistributionInformation() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Distribution information saved successfully.");
                        grvProductDetails.DataSource = BuildMaterialTable();
                        grvProductDetails.DisplayLayout.Bands[0].Columns["MaterialID"].Hidden = true;
                        CalculateTotalAmount();
                        cmbProductionUnit.Focus();
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save distribution information.");
                    }
                }
            }
        }


        private int InsertDistributionInformation()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            List<DistributeDetails> lstDistributionDetail = new List<DistributeDetails>();
            Distribution distribution = new Distribution();
            distribution.ProductionUnitID = Convert.ToInt32(cmbProductionUnit.Value);
            distribution.Total = Convert.ToDecimal(txtTotal.Text);
            distribution.DistributeDate = dtpDistributionDate.Value;
            distribution.BranchID = MTBFConstants.AppConstants.BranchID;
            distribution.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            lstDistributionDetail = GetAllDistributionDetailInformation();

            result = new MaterialDistributionManager().InsertDistribution(distribution, lstDistributionDetail);
            return result;
        }


        private int UpdateDistributionInformation()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            List<DistributeDetails> lstDistributionDetail = new List<DistributeDetails>();
            Distribution distribution = new MaterialDistributionManager().GetDistributionByID(_distributionID);
            distribution.ProductionUnitID = Convert.ToInt32(cmbProductionUnit.Value);
            distribution.Total = Convert.ToDecimal(txtTotal.Text);
            distribution.DistributeDate = dtpDistributionDate.Value;
            distribution.BranchID = MTBFConstants.AppConstants.BranchID;
            distribution.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            lstDistributionDetail = GetAllDistributionDetailInformation();

            result = new MaterialDistributionManager().UpdateDistribution(distribution, lstDistributionDetail);
            return result;
        }

        private List<DistributeDetails> GetAllDistributionDetailInformation()
        {
            List<DistributeDetails> lstDistributionDetail = new List<DistributeDetails>();

            foreach (UltraGridRow row in grvProductDetails.Rows)
            {
                DistributeDetails distributionDetail = new DistributeDetails();
                distributionDetail.MaterialID = Convert.ToInt32(row.Cells["MaterialID"].Value);
                distributionDetail.Quantity = Convert.ToDecimal(row.Cells["Quantity"].Text);
                distributionDetail.Price = Convert.ToDecimal(row.Cells["Price"].Value);
                distributionDetail.Total = Convert.ToDecimal(row.Cells["Total"].Text);
                lstDistributionDetail.Add(distributionDetail);
            }

            return lstDistributionDetail;
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



        private void grvProductDetails_ClickCell(object sender, ClickCellEventArgs e)
        {
            if (e.Cell.Column.Header.Caption == "Delete")
            {
                grvProductDetails.Rows[e.Cell.Row.Index].Delete();
                CalculateTotalAmount();
            }
        }

        private void cmbProductInformation_KeyUp(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 13)
            {
                txtQuantity.Focus();
            }
        }

        private void txtPrice_KeyUp(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 13)
            {
                txtQuantity.Focus();
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
                    txtPrice.Value = 0;
                    txtQuantity.Value = 0;
                    cmbProductInformation.Focus();
                    LoadMaterialCombo();
                }
            }
        }

        private void cmbProductInformation_ValueChanged(object sender, EventArgs e)
        {
            if (cmbProductInformation.Value != null)
            {
                LoadPriceInformation(cmbProductInformation.Value.ToString());
            }
        }

        private void LoadPriceInformation(string productID)
        {
            decimal totalQuantity = 0;
            decimal restQty = 0;
            decimal totalDistributeQty = 0;
            int materialID = 0;

            int.TryParse(cmbProductInformation.Value.ToString(), out materialID);
            if (materialID > 0)
            {
                List<MaterialsReceiveDetails> lstMaterialsReceiveDetails = new List<MaterialsReceiveDetails>();
                lstMaterialsReceiveDetails = new MaterialReceiveManager().GetAllMaterialsReceiveDetailByMaterialID(materialID);

                List<DistributeDetails> lstMaterialDistribution = new List<DistributeDetails>();
                lstMaterialDistribution = new MaterialDistributionManager().GetAllDistributionDetailByMaterialID(materialID);
                totalDistributeQty = CalculateTotalDistributeMaterial(lstMaterialDistribution);

                decimal totalPrice = 0;

                foreach (MaterialsReceiveDetails mrDetail in lstMaterialsReceiveDetails)
                {
                    totalQuantity = totalQuantity + mrDetail.Quantity;

                    if (totalQuantity > totalDistributeQty)
                    {
                        restQty = restQty + mrDetail.Quantity;
                        totalPrice = totalPrice + (mrDetail.Price * mrDetail.Quantity);
                    }

                }

                txtPrice.Value = (restQty > 0) ? totalPrice / restQty : 0;
            }
            else
            {
                MessageBoxHelper.ShowWarning("Invalid material!.");
                cmbProductInformation.Focus();
            }


        }

        private decimal CalculateTotalDistributeMaterial(List<DistributeDetails> lstMaterialDistribution)
        {
            decimal totalQty = 0;

            foreach (DistributeDetails item in lstMaterialDistribution)
            {
                totalQty = totalQty + item.Quantity;
            }
            return totalQty;
        }


    }
}
