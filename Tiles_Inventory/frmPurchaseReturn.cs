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
using IFMS.BLL;
using Infragistics.Win.UltraWinGrid;
using IMFS.Common.View;
using NybSys.MTBF.Utility.Helper;
using NybSys.MTBF.Utility.Enums;

namespace Tiles_Inventory
{
    public partial class frmPurchaseReturn : BaseForm
    {
        List<Product> lstProductInformation = new List<Product>();
        private VMPurchaseReturn _vmPurchaseReturn = new VMPurchaseReturn();

        public frmPurchaseReturn()
        {
            InitializeComponent();
        }

        public frmPurchaseReturn(VMPurchaseReturn vmPurchaseReturn)
        {
            _vmPurchaseReturn = vmPurchaseReturn;
            base.IsUpdating = true;
            InitializeComponent();
        }
        private void frmPurchaseReturn_Load(object sender, EventArgs e)
        {
            LoadSupplierNameByCompanyID();
            LoadAllProductInformation();
            if (base.IsUpdating)
            {

                LoadExistingReturnData();
            }
        }

        private void LoadAllProductInformation()
        {

            lstProductInformation = new ProductManager().GetAllProductByBranchAndOrganizationID(MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<Product>().ToList();

            DataTable dt = new DataTable();
            dt.Columns.Add("ProductID");
            dt.Columns.Add("ProductName");      

            foreach (Product product in lstProductInformation)
            {
                DataRow row = dt.NewRow();
                row["ProductID"] = product.ProductID;
                row["ProductName"] = product.ProductName;           
                dt.Rows.Add(row);
            }

            cmbProductName.DataSource = dt;
            cmbProductName.DisplayMember = "ProductName";
            cmbProductName.ValueMember = "ProductID";
            cmbProductName.DisplayLayout.Bands[0].Columns["ProductID"].Hidden = true;
            cmbProductName.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
            cmbProductName.DisplayLayout.PerformAutoResizeColumns(false, PerformAutoSizeType.AllRowsInBand, true);
        }

        private void LoadExistingReturnData()
        {
            txtInvoiceNumber.Text = _vmPurchaseReturn.purchaseReturn.PONumber;
            // cmbCompanyName.Value = _vmPurchaseReturn.purchaseReturn.CompanyID;
            CmbSupplierName.Value = _vmPurchaseReturn.purchaseReturn.SupplierID;
            dtpReturnDate.Value = _vmPurchaseReturn.purchaseReturn.ReturnDate;
            txtReceiveAmount.Text = _vmPurchaseReturn.purchaseReturn.ReceiveAmount.ToString();



            foreach (PurchaseReturnDetail rDetail in _vmPurchaseReturn.lstPurchaseReturnDetail)
            {
                DataGridViewRow row = grvPurchaseDetails.Rows[0].Clone() as DataGridViewRow;
                int index = grvPurchaseDetails.Rows.Add(row);
                grvPurchaseDetails.Rows[index].Cells["ProductID"].Value = rDetail.ProductID;
                grvPurchaseDetails.Rows[index].Cells["ProductName"].Value = rDetail.ProductName;
                grvPurchaseDetails.Rows[index].Cells["Quantity"].Value = rDetail.Quantity.ToString();
                grvPurchaseDetails.Rows[index].Cells["UnitPrice"].Value = rDetail.Price.ToString();
                grvPurchaseDetails.Rows[index].Cells["Total"].Value = (rDetail.Price * rDetail.Quantity).ToString("F2");
            }
            txtTotalAmount.Text = CalculateTotalPurchaseCost().ToString("F2");
        }


        /// <summary>
        /// Method to load company informaiton in combo box.
        /// </summary>
        //private void LoadCompanyName()
        //{
        //    DataTable table = new DataTable();
        //    table.Columns.Add("ID");
        //    table.Columns.Add("Name");

        //    DataRow emptyRow = table.NewRow();
        //    emptyRow[0] = MTBFConstants.DataField.COMBO_DEFAULT_ID;
        //    emptyRow[1] = "-Select-";
        //    table.Rows.Add(emptyRow);

        //    List<Company> lstCompany = new List<Company>();
        //    lstCompany = new CompanyManager().GetAllCompany().Cast<Company>().Where(c => c.BranchID == MTBFConstants.AppConstants.BranchID && c.OrganizationID == MTBFConstants.AppConstants.OrganizationID).ToList();

        //    foreach (Company company in lstCompany)
        //    {
        //        DataRow row = table.NewRow();
        //        row[0] = company.CompanyID;
        //        row[1] = company.CompanyName;
        //        table.Rows.Add(row);
        //    }

        //    cmbCompanyName.DisplayMember = "Name";
        //    cmbCompanyName.ValueMember = "ID";
        //    cmbCompanyName.DataSource = table;
        //    cmbCompanyName.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
        //    cmbCompanyName.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
        //    cmbCompanyName.DisplayLayout.PerformAutoResizeColumns(false, PerformAutoSizeType.AllRowsInBand, true);
        //}


        /// <summary>
        /// Method to load supplier information in combo box.
        /// </summary>
        private void LoadSupplierNameByCompanyID()
        {
            List<Supplier> lstSupplier = new List<Supplier>();
            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("Name");

            DataRow emptyRow = table.NewRow();
            emptyRow[0] = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            emptyRow[1] = "-Select-";
            table.Rows.Add(emptyRow);

            lstSupplier = new SupplierManager().GetAllSupplierByBranchID(MTBFConstants.AppConstants.BranchID);

            if (lstSupplier.Count > 0)
            {
                foreach (Supplier supplier in lstSupplier)
                {
                    DataRow row = table.NewRow();
                    row[0] = supplier.SupplierID;
                    row[1] = supplier.SupplierName;
                    table.Rows.Add(row);
                }

                CmbSupplierName.ValueMember = "ID";
                CmbSupplierName.DisplayMember = "Name";
                CmbSupplierName.DataSource = table;
                CmbSupplierName.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
                CmbSupplierName.Enabled = true;
            }
        }

        private void cmbCompanyName_ValueChanged(object sender, EventArgs e)
        {
            //if (cmbCompanyName.Value != null && !string.IsNullOrEmpty(cmbCompanyName.Text))
            //{
            //    LoadSupplierNameByCompanyID(Convert.ToInt32(cmbCompanyName.Value));
            //    LoadAllProductInformationByCompanyID(Convert.ToInt32(cmbCompanyName.Value));
            //}
        }

        //private void LoadAllProductInformationByCompanyID(int companyID)
        //{

        //    lstProductInformation = new ProductManager().GetProductInformationForReturnByCompanyID(companyID).Cast<ProductInformationForPurchase>().ToList();
        //    cmbProductName.DataSource = lstProductInformation;
        //    cmbProductName.DisplayMember = "Model";
        //    cmbProductName.ValueMember = "ProductID";
        //}

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (ValidFormData())
            {
                AddProductInGrid();
            }
        }


        private bool ValidFormData()
        {

            if (txtInvoiceNumber.Text == string.Empty)
            {
                MessageBoxHelper.ShowInformation("You need to enter invoice number.");
                txtInvoiceNumber.Focus();
                return false;
            }



            //if ((Convert.ToInt32(cmbCompanyName.Value) == MTBFConstants.DataField.COMBO_DEFAULT_ID))
            //{
            //    MessageBoxHelper.ShowInformation("You need to select company name.");
            //    cmbCompanyName.Focus();
            //    return false;
            //}

            if ((Convert.ToInt32(CmbSupplierName.Value) == MTBFConstants.DataField.COMBO_DEFAULT_ID))
            {
                MessageBoxHelper.ShowInformation("You need to select supplier name.");
                CmbSupplierName.Focus();
                return false;
            }

            if ((cmbProductName.Value == null))
            {
                MessageBoxHelper.ShowInformation("You need to select product name.");
                cmbProductName.Focus();
                return false;
            }

            if (txtPurchasePrice.Text == string.Empty)
            {
                MessageBoxHelper.ShowInformation("You need to enter purchase price."); ;
                txtPurchasePrice.Focus();
                return false;
            }



            if (txtQuantity.Text == string.Empty)
            {
                MessageBoxHelper.ShowInformation("You need to enter quantity.");
                txtQuantity.Focus();
                return false;
            }

            if (Convert.ToDecimal(txtPurchasePrice.Text) == 0)
            {
                MessageBoxHelper.ShowInformation("Purchase price can't be zero.");
                txtPurchasePrice.Focus();
                return false;
            }



            if (Convert.ToDecimal(txtQuantity.Text) == 0)
            {
                MessageBoxHelper.ShowInformation("Quantity can't be zero.");
                txtQuantity.Focus();
                return false;
            }



            return true;
        }

        private void AddProductInGrid()
        {
            //   int intBoxQuantity = 0;
            decimal Quantity = 0;
            decimal PurchasePrice = 0;

            Quantity = Convert.ToDecimal(txtQuantity.Text);
            PurchasePrice = Convert.ToDecimal(txtPurchasePrice.Text);



            DataGridViewRow row = grvPurchaseDetails.Rows[0].Clone() as DataGridViewRow;
            int index = grvPurchaseDetails.Rows.Add(row);
            Product product = lstProductInformation.Where(p => p.ProductID == cmbProductName.Value.ToString()).FirstOrDefault(); //.Where(p => p.BranchID == MTBFConstants.AppConstants.BranchID && p.OrganizationID == MTBFConstants.AppConstants.OrganizationID).Cast<Product>().FirstOrDefault()

            grvPurchaseDetails.Rows[index].Cells["ProductID"].Value = product.ProductID;
            grvPurchaseDetails.Rows[index].Cells["ProductName"].Value = product.ProductName;
            grvPurchaseDetails.Rows[index].Cells["Quantity"].Value = Quantity.ToString();
            grvPurchaseDetails.Rows[index].Cells["UnitPrice"].Value = PurchasePrice.ToString();
            grvPurchaseDetails.Rows[index].Cells["Total"].Value = (PurchasePrice * Quantity).ToString("F2");
            txtTotalAmount.Text = CalculateTotalPurchaseCost().ToString("F2");


            PurchaseReturnDetail purchaseReturnDetail = new PurchaseReturnDetail();
            purchaseReturnDetail.ProductID = product.ProductID;
            purchaseReturnDetail.ProductName = product.ProductName;
            purchaseReturnDetail.Quantity = Quantity;
            purchaseReturnDetail.Price = PurchasePrice;
            _vmPurchaseReturn.lstPurchaseReturnDetail.Add(purchaseReturnDetail);
            ResetControl();
            grvPurchaseDetails.AutoResizeColumns();
        }

        private decimal CalculateTotalPurchaseCost()
        {
            decimal sum = 0;
            decimal total = 0;
            decimal mrp = 0;
            decimal quantity = 0;
            for (int i = 0; i <= grvPurchaseDetails.Rows.Count - 2; i++)
            {
                if (grvPurchaseDetails.Rows[i].Cells["UnitPrice"].Value != null)
                {
                    decimal.TryParse(grvPurchaseDetails.Rows[i].Cells["UnitPrice"].Value.ToString(), out mrp);
                }
                if (grvPurchaseDetails.Rows[i].Cells["Quantity"].Value != null)
                {
                    decimal.TryParse(grvPurchaseDetails.Rows[i].Cells["Quantity"].Value.ToString(), out quantity);
                }

                total = quantity * mrp;
                grvPurchaseDetails.Rows[i].Cells["Total"].Value = total.ToString("F2");
                sum = sum + total;
            }

            return sum;
        }

        private void ResetControl()
        {
            cmbProductName.Text = string.Empty;
            txtQuantity.Value = 0;
            txtPurchasePrice.Clear();
            cmbProductName.Focus();
            grvPurchaseDetails.Columns[0].Visible = false;
        }

        private void txtQuantity_KeyUp(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 13)
            {
                if (ValidFormData())
                {
                    AddProductInGrid();
                    cmbProductName.Focus();
                }
            }
        }

        private void cmbProductName_KeyUp(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 13)
            {
                txtPurchasePrice.Focus();
            }
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (grvPurchaseDetails.Rows.Count < 2)
            {
                MessageBoxHelper.ShowInformation("Please add data in grid.");
                return;
            }
            if (SavePurchaseReturn() == (int)MTBFEnums.ReturnResult.SUCCESS)
            {
                MessageBoxHelper.ShowInformation("Information Saved Successfully.");
            }
            else
            {
                MessageBoxHelper.ShowInformation("Failed to save information.");
            }
        }

        private int SavePurchaseReturn()
        {

            decimal receiveAmount = 0;
            decimal.TryParse(txtReceiveAmount.Text, out receiveAmount);
            _vmPurchaseReturn.purchaseReturn.CompanyID = 1;// Convert.ToInt32(cmbCompanyName.Value);
            _vmPurchaseReturn.purchaseReturn.SupplierID = Convert.ToInt32(CmbSupplierName.Value);
            _vmPurchaseReturn.purchaseReturn.CreatedBy = MTBFConstants.AppConstants.LoggedinUserID;
            _vmPurchaseReturn.purchaseReturn.BranchID = MTBFConstants.AppConstants.BranchID;
            _vmPurchaseReturn.purchaseReturn.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            _vmPurchaseReturn.purchaseReturn.Total = Convert.ToDecimal(txtTotalAmount.Text);
            _vmPurchaseReturn.purchaseReturn.ReceiveAmount = receiveAmount;
            _vmPurchaseReturn.purchaseReturn.ReturnDate = dtpReturnDate.Value;
            _vmPurchaseReturn.purchaseReturn.PONumber = txtInvoiceNumber.Text;
            return new PurchaseReturnManager().SavePurcahseReturn(_vmPurchaseReturn);
        }

        private void grvPurchaseDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string deleteString = grvPurchaseDetails.CurrentCell.OwningColumn.ToolTipText;
            if (!grvPurchaseDetails.CurrentRow.IsNewRow)
            {
                if (deleteString == "Delete")
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure want to delete this record ?", "Conformation Message", MessageBoxButtons.YesNo);
                    if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                    {
                        grvPurchaseDetails.Rows.Remove(grvPurchaseDetails.CurrentRow);
                        txtTotalAmount.Text = CalculateTotalPurchaseCost().ToString();
                        _vmPurchaseReturn.lstPurchaseReturnDetail.RemoveAt(e.RowIndex);
                    }
                }
            }
        }

    }
}
