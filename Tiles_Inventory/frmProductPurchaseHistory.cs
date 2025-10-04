using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IFMS.Facade;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Constants;
using IFMS.BLL;
using NybSys.MTBF.Utility.Helper;

namespace Tiles_Inventory
{
    public partial class frmProductPurchaseHistory : BaseForm
    {
        List<Product> _lstProduct = new List<Product>();
        public frmProductPurchaseHistory()
        {
            DataAccessProxy = new FacadeManager();
            InitializeComponent();
        }

        private void frmProductPurchaseHistory_Load(object sender, EventArgs e)
        {
            LoadProductInformation();
            grvCreditProductDetails.DataSource = BuildProductSalesHistoryTable();
        }


        /// <summary>
        /// Method to load product information combo.
        /// </summary>
        private void LoadProductInformation()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Product ID");
            table.Columns.Add("Product Name");
            _lstProduct = new ProductManager().GetAllProductByBranchAndOrganizationID(MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID);
            foreach (Product product in _lstProduct)
            {
                DataRow row = table.NewRow();
                row["Product ID"] = product.ProductID;
                row["Product Name"] = product.ProductName;
                table.Rows.Add(row);
            }
            cmbProductInformation.DataSource = table;
            cmbProductInformation.DisplayMember = "Product Name";
            cmbProductInformation.ValueMember = "Product ID";
            cmbProductInformation.DisplayLayout.Bands[0].Columns["Product ID"].Hidden = true;
        }



        private DataTable BuildProductSalesHistoryTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Purchase Number");
            table.Columns.Add("PI Number");
            table.Columns.Add("Purchase Date");
            table.Columns.Add("Product Name");
            table.Columns.Add("Quantity");

            return table;
        }

        private void LoadProductPurchaseHisotry()
        {
            if (cmbProductInformation.Value != null)
            {
                DataTable productHistoryTable = BuildProductSalesHistoryTable();
                decimal totalQty = 0;

                //string filter = string.Format("{0}='{1}' and Status=2", "ProductID", cmbProductInformation.Value.ToString().Trim());
                //List<PurchaseOrderDetail> lstPurchaseOrderdetail = new List<PurchaseOrderDetail>();
                //lstPurchaseOrderdetail = new PurchaseManager().GetFilteredPurchaseOrderDetail(filter);

                foreach (PurchaseOrderDetail purchaseOrderDetail in DataAccessProxy.GetPurchaseOrderDetailByProductID(cmbProductInformation.Value.ToString(), MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID))
                {
                    DataRow row = productHistoryTable.NewRow();
                    PurchaseOrder purchaseOrder = DataAccessProxy.GetPurchaseOrderByID(purchaseOrderDetail.PurchaseID);
                    if (purchaseOrder.PurchaseDate >= dtpFromDate.Value.AddDays(-1) && purchaseOrder.PurchaseDate <= dtpToDate.Value)
                    {
                        Product product = _lstProduct.Where(p => p.ProductID == purchaseOrderDetail.ProductID).FirstOrDefault();
                        row["Purchase Number"] = purchaseOrderDetail.PurchaseID;
                        row["PI Number"] = purchaseOrder.InvoiceNumber;
                        row["Purchase Date"] = purchaseOrder.PurchaseDate.ToShortDateString();
                        row["Product Name"] = product.ProductName;
                        row["Quantity"] = purchaseOrderDetail.Quantity;

                        productHistoryTable.Rows.Add(row);
                        totalQty = totalQty + purchaseOrderDetail.Quantity;

                    }
                }
                grvCreditProductDetails.DataSource = productHistoryTable;
                txtTotalQuantity.Text = totalQty.ToString();
            }
            else
            {
                MessageBoxHelper.ShowInformation("Please select product name.");
                cmbProductInformation.Focus();
            }
           

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadProductPurchaseHisotry();
        }
    }
}
