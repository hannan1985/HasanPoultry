using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DataDynamics.ActiveReports;
using IFMS.Facade;
using IMFS.Common.DTO;
using IMFS.Common.Utility;

using IMFS.Common.View;
using NybSys.MTBF.Utility.Helper;
using System.Data;
using Tiles_Inventory.Reports;
using NybSys.MTBF.Utility.Constants;
using Infragistics.Win.UltraWinGrid;

namespace Tiles_Inventory
{
    public partial class frmCreateBarcode : BaseForm
    {
        Bitmap MemoryImage;
        List<Product> lstProductInformation = new List<Product>();

        public frmCreateBarcode()
        {
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            int barcodeQty = 1;

            try
            {
                if (CanCreate())
                {
                   // string code = txtCode.Text.Trim().PadLeft(12, '0');                 
                    int.TryParse(txtQuanity.Text, out barcodeQty);   

                    DataTable table = new DataTable();
                    table.Columns.Add("ProductName1");
                    table.Columns.Add("ProductCode1");                
                    Product product = lstProductInformation.Where(p => p.BarCode == txtCode.Text.Trim()).FirstOrDefault();
                    if (product != null)
                    {
                        for (int i = 0; i < barcodeQty; i++)
                        {
                            DataRow row = table.NewRow();
                            row["ProductName1"] = product.ProductName;
                            row["ProductCode1"] = product.BarCode;                            
                            table.Rows.Add(row);
                            i += 5;
                        }

                        rptBarcodeForLabel report = new rptBarcodeForLabel();
                        frmSalesReport frm = new frmSalesReport();
                        report.DataSource = table;
                        frm.rptViewer.Document = report.Document;
                        frm.rptViewer.Dock = DockStyle.Fill;
                        report.Run();
                        frm.ShowDialog();

                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Invalid barcode");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void PrintBarCode(int numberOfBarcode, Image barcodeImage)
        {
            pnlBarcode.Controls.Clear();
            // rptBarcode barcodeReport = new rptBarcode();

            DataDynamics.ActiveReports.Picture picture = new DataDynamics.ActiveReports.Picture();

            PictureBox pbCode = new PictureBox();

            for (int i = 0; i < numberOfBarcode; i++)
            {
                pbCode = new PictureBox();
                pbCode.Name = i.ToString();
                pbCode.Image = barcodeImage;
                pbCode.Width = 110;
                pbCode.Height = 40;
                pnlBarcode.Controls.Add(pbCode);
            }
            grvProductDetails.Visible = false;
            pnlBarcode.Visible = true;
        }




        private bool CanCreate()
        {
            if (txtCode.Text.Trim() == "")
            {
                MessageBox.Show("Please type code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCode.Focus();
                return false;
            }

            //if (txtCode.Text.Trim().Length != 12)
            //{
            //    MessageBox.Show("Code must be 12 digit long", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtCode.Focus();
            //    return false;
            //}

            return true;
        }

        public void GetPrintArea(Panel pnl)
        {
            MemoryImage = new Bitmap(pnl.Width, pnl.Height);
            pnl.DrawToBitmap(MemoryImage, new Rectangle(0, 0, pnl.Width, pnl.Height));
        }


        public void Print(Panel pnl)
        {

            GetPrintArea(pnl);
            previewdlg.Document = printdoc1;
            previewdlg.ShowDialog();
        }






        private Dictionary<string, Image> GetAllProductData()
        {
            Dictionary<string, Image> barcodeDic = new Dictionary<string, Image>();

            foreach (UltraGridRow row in grvProductDetails.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Select"].Value) == true)
                {
                    string productName = row.Cells["Product Name"].Text.Trim();
                    string barcode = row.Cells["Barcode"].Text.Trim();
                    string code = barcode.Trim().PadLeft(12, '0');
                    Bitmap barcodeImage = BarcodeManager.GetBarcode(code);
                    barcodeDic.Add(productName, (Image)barcodeImage);
                }
            }
            return barcodeDic;
        }


        private DataTable GetProductTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ProductName1");
            table.Columns.Add("ProductCode1");
            table.Columns.Add("ProductName2");
            table.Columns.Add("ProductCode2");
            table.Columns.Add("ProductName3");
            table.Columns.Add("ProductCode3");
            table.Columns.Add("ProductName4");
            table.Columns.Add("ProductCode4");
            table.Columns.Add("ProductName5");
            table.Columns.Add("ProductCode5");

            List<Product> lstProduct = new List<Product>();

            int count = 0;

            foreach (UltraGridRow row in grvProductDetails.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Select"].Value) == true)
                {
                    string productName = row.Cells["Product Name"].Text.Trim();
                    Product product = DataAccessProxy.GetProductByName(productName).Cast<Product>().ToList().FirstOrDefault();
                    lstProduct.Add(product);
                }
            }



            foreach (Product product in lstProduct)
            {
                if (lstProduct.Count > count)
                {
                    DataRow drow = table.NewRow();
                    if ((product != null))
                    {
                        drow["ProductName1"] = lstProduct[count].ProductName;
                        drow["ProductCode1"] = lstProduct[count].BarCode;
                    }
                    else
                    {
                        table.Rows.Add(drow);
                        break;
                    }
                    if (lstProduct.Count > (count + 1) && (lstProduct[count + 1] != null))
                    {
                        drow["ProductName2"] = lstProduct[count + 1].ProductName;
                        drow["ProductCode2"] = lstProduct[count + 1].BarCode;
                    }
                    else
                    {
                        table.Rows.Add(drow);
                        break;
                    }
                    if (lstProduct.Count > (count + 2) && (lstProductInformation[count + 2] != null))
                    {
                        drow["ProductName3"] = lstProduct[count + 2].ProductName;
                        drow["ProductCode3"] = lstProduct[count + 2].BarCode;
                    }
                    else
                    {
                        table.Rows.Add(drow);
                        break;
                    }
                    if (lstProduct.Count > (count + 3) && (lstProduct[count + 3] != null))
                    {
                        drow["ProductName4"] = lstProduct[count + 3].ProductName;
                        drow["ProductCode4"] = lstProduct[count + 3].BarCode;
                    }
                    else
                    {
                        table.Rows.Add(drow);
                        break;
                    }
                    if (lstProduct.Count > (count + 4) && (lstProduct[count + 4] != null))
                    {
                        drow["ProductName5"] = lstProduct[count + 4].ProductName;
                        drow["ProductCode5"] = lstProduct[count + 4].BarCode;
                        count = count + 5;
                    }
                    else
                    {
                        table.Rows.Add(drow);
                        break;
                    }
                    table.Rows.Add(drow);
                }
            }
            return table;

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            // GetProductTable();

            //// Print(this.pnlBarcode);
            //if (cbLabelPrint.Checked)
            //{
            //    rptBarcodeForLabel report = new rptBarcodeForLabel(GetAllProductData());
            //    frmSalesReport frm = new frmSalesReport();
            //    report.DataSource = GetProductTable();
            //    frm.rptViewer.Document = report.Document;
            //    frm.rptViewer.Dock = DockStyle.Fill;
            //    report.Run();
            //    frm.ShowDialog();
            //}
            //else
            //{
            //    rptBarcode report = new rptBarcode(GetAllProductData());
            //    frmSalesReport frm = new frmSalesReport();
            //    report.DataSource = GetProductTable();
            //    frm.rptViewer.Document = report.Document;
            //    frm.rptViewer.Dock = DockStyle.Fill;
            //    report.Run();
            //    frm.ShowDialog();
            //}

            rptBarcode report = new rptBarcode();
            frmSalesReport frm = new frmSalesReport();
            report.DataSource = GetProductTable();
            frm.rptViewer.Document = report.Document;
            frm.rptViewer.Dock = DockStyle.Fill;
            report.Run();
            frm.ShowDialog();



        }

        private void LoadProductInformationCombo()
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("ID");
                table.Columns.Add("Product Name");
                table.Columns.Add("Barcode");
                table.Columns.Add("Delete");
                table.Columns.Add("Select");
                lstProductInformation = DataAccessProxy.GetAllProductByBranchAndOrganizationID(MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<Product>().ToList();
                foreach (Product product in lstProductInformation)
                {
                    DataRow row = table.NewRow();
                    row["ID"] = product.ProductID;
                    row["Product Name"] = product.ProductName;
                    row["Barcode"] = product.BarCode;
                    row["Delete"] = "Delete";
                    row["Select"] = false;
                    table.Rows.Add(row);
                }

                cmbProductName.DisplayMember = "Product Name";
                cmbProductName.ValueMember = "ID";
                cmbProductName.DataSource = table;
                cmbProductName.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
                cmbProductName.DisplayLayout.PerformAutoResizeColumns(false, PerformAutoSizeType.AllRowsInBand, true);


                grvProductDetails.DataSource = table;
                grvProductDetails.DisplayLayout.Bands[0].Columns["ID"].Hidden = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Product information load fail.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void printdoc1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Rectangle pagearea = e.PageBounds;
            e.Graphics.DrawImage(MemoryImage, (pagearea.Width / 2) - (this.pnlBarcode.Width / 2), this.pnlBarcode.Location.Y);
        }

        private void frmCreateBarcode_Load(object sender, EventArgs e)
        {
            LoadProductInformationCombo();
            LoadProductTypeCombo();
            GetProductTable();
        }


        /// <summary>
        /// Method to load product type information.
        /// </summary>
        private void LoadProductTypeCombo()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("Name");
            List<ProductType> lstProductType = new List<ProductType>();
            lstProductType = DataAccessProxy.GetAllProductType().Cast<ProductType>().ToList();


            DataRow emptyRow = table.NewRow();
            emptyRow[0] = -1;
            emptyRow[1] = "-Select-";
            table.Rows.Add(emptyRow);

            foreach (ProductType productType in lstProductType)
            {
                DataRow row = table.NewRow();
                row[0] = productType.ProductTypeID;
                row[1] = productType.TypeName;
                table.Rows.Add(row);
            }


            cmbProductType.DataSource = table;
            cmbProductType.DisplayMember = "Name";
            cmbProductType.ValueMember = "ID";
            cmbProductType.Value = -1;
        }

        private void cmbProductName_TextChanged(object sender, EventArgs e)
        {
        }

        private void cmbProductName_ValueChanged(object sender, EventArgs e)
        {
            if (cmbProductName.Value != null)
            {
                Product product = DataAccessProxy.GetProductByID(cmbProductName.Value.ToString());

                txtCode.Text = product.BarCode;
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            if (grvProductDetails.Rows.Count > 0)
            {
                foreach (UltraGridRow row in grvProductDetails.Rows)
                {
                    row.Cells["Select"].Value = true;
                }
            }
        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            if (grvProductDetails.Rows.Count > 0)
            {
                foreach (UltraGridRow row in grvProductDetails.Rows)
                {
                    row.Cells["Select"].Value = false;
                }
            }
        }

        private void grvProductDetails_ClickCell(object sender, ClickCellEventArgs e)
        {
            bool check = false;
            if (e.Cell.Column.Header.Caption == "Select")
            {
                bool.TryParse(grvProductDetails.Rows[e.Cell.Row.Index].Cells["Select"].Value.ToString(), out check);
                grvProductDetails.Rows[e.Cell.Row.Index].Cells["Select"].Value = (check) ? false : true;
            }

            string celltext = e.Cell.Column.Header.Caption;
            if (celltext == "Delete")
            {
                if (grvProductDetails.Rows[e.Cell.Row.Index].Delete())
                {

                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btProductType_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbProductType.Value) != -1)
            {
                LoadPrductInformaitonByProductType(Convert.ToInt32(cmbProductType.Value));
            }
            else
            {
                MessageBoxHelper.ShowInformation("You need to select product type.");
                cmbProductType.Focus();
            }
        }

        private void LoadPrductInformaitonByProductType(int productType)
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("Product Name");
            table.Columns.Add("Barcode");
            table.Columns.Add("Delete");
            table.Columns.Add("Select");
            List<Product> lstProduct = lstProductInformation.Where(p => p.ProductTypeID == productType).Cast<Product>().ToList();
            foreach (Product product in lstProduct)
            {
                DataRow row = table.NewRow();
                row["ID"] = product.ProductID;
                row["Product Name"] = product.ProductName;
                row["Barcode"] = product.BarCode;
                row["Delete"] = "Delete";
                row["Select"] = false;
                table.Rows.Add(row);
            }

            cmbProductName.DisplayMember = "Product Name";
            cmbProductName.ValueMember = "ID";
            cmbProductName.DataSource = table;
            cmbProductName.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
            cmbProductName.DisplayLayout.PerformAutoResizeColumns(false, PerformAutoSizeType.AllRowsInBand, true);


            grvProductDetails.DataSource = table;
            grvProductDetails.DisplayLayout.Bands[0].Columns["ID"].Hidden = true;
        }




    }
}
