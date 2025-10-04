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
using IMFS.Common.View;

namespace Tiles_Inventory
{
    public partial class frmFinishStockReceive : BaseForm
    {
        int _materialReceiveID = 0;

        public frmFinishStockReceive()
        {
            InitializeComponent();
        }

        public frmFinishStockReceive(int materialReceiveID, bool isEdit)
        {
            _materialReceiveID = materialReceiveID;
            IsUpdating = isEdit;
            InitializeComponent();
        }

        private void frmMaterialReceive_Load(object sender, EventArgs e)
        {
            grvProductDetails.DataSource = BuildMaterialTable();
            grvProductDetails.DisplayLayout.Bands[0].Columns["ProductID"].Hidden = true;
            LoadProductionUnitCombo();
            LoadProductInformationCombo();
            if (IsUpdating)
            {
                LoadExistingStockInformation();
                LoadExistingStockDetailInformation();
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
        }


        private void LoadExistingStockDetailInformation()
        {
            foreach (StockDetails dDetail in new StockManager().GetAllStockDetailByStockID(_materialReceiveID))
            {
                UltraGridRow row = grvProductDetails.DisplayLayout.Bands[0].AddNew();
                Product material = new ProductManager().GetProductByID(dDetail.ProductID);
                row.Cells["ProductID"].Value = dDetail.ProductID;
                row.Cells["Material Name"].Value = (material != null) ? material.ProductName : string.Empty;
                row.Cells["Quantity"].Value = dDetail.Quantity;
                row.Cells["Price"].Value = dDetail.Price;
                row.Cells["Total"].Value = dDetail.Total;
                row.Cells["Delete"].Value = "Delete";
            }
        }

        private void LoadExistingStockInformation()
        {
            Stock stock = new StockManager().GetStockByID(_materialReceiveID);
            if (stock != null)
            {
                dtpDistributionDate.Value = stock.StockDate;
                cmbProductionUnit.Value = stock.ProductionUnitID;
                txtTotal.Text = stock.Total.ToString();
            }
        }

        private void LoadProductInformationCombo()
        {
            DataTable productTable = new DataTable();
            productTable.Columns.Add("ProductID");
            productTable.Columns.Add("Product Name");
            productTable.Columns.Add("Type");

            List<Product> lstProductInformation = new List<Product>();
            lstProductInformation = new ProductManager().GetAllProduct().Cast<Product>().Where(p => p.BranchID == MTBFConstants.AppConstants.BranchID && p.OrganizationID == MTBFConstants.AppConstants.OrganizationID).ToList();

            foreach (Product product in lstProductInformation)
            {
                DataRow row = productTable.NewRow();
                row["ProductID"] = product.ProductID;
                row["Product Name"] = product.ProductName;
                row["Type"] = product.TypeName;
                productTable.Rows.Add(row);
            }

            cmbProductInformation.DisplayMember = "Product Name";
            cmbProductInformation.ValueMember = "ProductID";
            cmbProductInformation.DataSource = productTable;
            cmbProductInformation.DisplayLayout.Bands[0].Columns["ProductID"].Hidden = true;
        }


        private DataTable BuildMaterialTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ProductID");
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
            row.Cells["ProductID"].Value = cmbProductInformation.Value.ToString();
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
                        MessageBoxHelper.ShowInformation("Stock information saved successfully.");
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
                        MessageBoxHelper.ShowInformation("Stock information saved successfully.");
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
            List<StockDetails> lstStockDetail = new List<StockDetails>();
            Stock stock = new Stock();
        

            stock.StockDate = dtpDistributionDate.Value;
            stock.Total = Convert.ToDecimal(txtTotal.Text);
            stock.ProductionUnitID = Convert.ToInt32(cmbProductionUnit.Value);
            stock.BranchID = MTBFConstants.AppConstants.BranchID;
            stock.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            lstStockDetail = GetAllStockDetailInformation();

            result = new StockManager().InsertStock(stock, lstStockDetail);
            return result;
        }


        private int UpdateMaterialReceiveInformation()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            List<StockDetails> lstStockDetail = new List<StockDetails>();
            Stock stock = new StockManager().GetStockByID(_materialReceiveID);
            stock.Total = Convert.ToDecimal(txtTotal.Text);
            stock.StockDate = dtpDistributionDate.Value;
            stock.ProductionUnitID = Convert.ToInt32(cmbProductionUnit.Value);
            stock.BranchID = MTBFConstants.AppConstants.BranchID;
            stock.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            lstStockDetail = GetAllStockDetailInformation();

            result = new StockManager().UpdateStock(stock, lstStockDetail);
            return result;
        }

        private List<StockDetails> GetAllStockDetailInformation()
        {
            List<StockDetails> lstStockDetail = new List<StockDetails>();

            foreach (UltraGridRow row in grvProductDetails.Rows)
            {
                StockDetails distributionDetail = new StockDetails();
                distributionDetail.ProductID = row.Cells["ProductID"].Value.ToString();
                distributionDetail.Quantity = Convert.ToDecimal(row.Cells["Quantity"].Text);
                distributionDetail.Price = Convert.ToDecimal(row.Cells["Price"].Value);
                distributionDetail.Total = Convert.ToDecimal(row.Cells["Total"].Text);
                lstStockDetail.Add(distributionDetail);
            }

            return lstStockDetail;
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

        private void btAddPUnit_Click(object sender, EventArgs e)
        {

        }

        private void grvProductDetails_ClickCell(object sender, ClickCellEventArgs e)
        {
            if (e.Cell.Column.Header.Caption == "Delete")
            {
                grvProductDetails.Rows[e.Cell.Row.Index].Delete();
                CalculateTotalAmount();
            }
        }



    }
}
