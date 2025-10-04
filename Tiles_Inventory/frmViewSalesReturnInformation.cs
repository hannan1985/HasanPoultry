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
using NybSys.MTBF.Utility.Helper;

using IMFS.Common.Utility;
using IFMS.Facade;
using Tiles_Inventory.Reports;
using System.IO;
using IFMS.BLL;

namespace Tiles_Inventory
{
    public partial class frmViewSalesReturnInformation : BaseForm
    {
        public frmViewSalesReturnInformation()
        {
            DataAccessProxy = new FacadeManager();
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadSalesReturnByDate(dtpFromDate.Value.ToString("yyyy/MM/dd"), dtpToDate.Value.ToString("yyyy/MM/dd"));
        }

        private DataTable BuildTransferTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("SalesReturnID");
            table.Columns.Add("Return Date");
            table.Columns.Add("Customer Name");
            table.Columns.Add("Short Note");
            table.Columns.Add("Phone");
            table.Columns.Add("Total");
            table.Columns.Add("Discount");
            table.Columns.Add("Paid Amount");
            return table;

        }

        private DataTable BuildTransferDetailTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Product Name");
            table.Columns.Add("Price");
            table.Columns.Add("Quantity");
            table.Columns.Add("Total");

            return table;

        }

        private void LoadSalesReturnByDate(string fromDate, string toDate)
        {
            DataTable transferTable = BuildTransferTable();
            foreach (SalesReturn transfer in new SalesReturnManager().GetAllSalesReturnByDate(MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID, fromDate, toDate))
            {

                DataRow row = transferTable.NewRow();
                row["SalesReturnID"] = transfer.SalesReturnID;
                row["Return Date"] = transfer.ReturnDate.ToShortDateString();
                row["Customer Name"] = transfer.CustomerName;
                row["Phone"] = transfer.Phone;
                row["Short Note"] = transfer.ShortNote;
                row["Total"] = transfer.Total;
                row["Discount"] = transfer.Discount;
                row["Paid Amount"] = transfer.PaidAmount;
                transferTable.Rows.Add(row);
            }
            grvTransferInfo.DataSource = transferTable;
            if (transferTable.Rows.Count == 0)
            {
                MessageBoxHelper.ShowInformation("No data found for this combination");
            }
        }


        private void LoadTransferDetailInformation(int transferID)
        {

            DataTable transferDetailTable = BuildTransferDetailTable();
            foreach (SalesReturnDetail tDetail in new SalesReturnManager().GetAllSalesReturnDetailBySalesID(transferID))
            {
                DataRow row = transferDetailTable.NewRow();
                row["Product Name"] = tDetail.ProductName;
                row["Price"] = tDetail.Price;
                row["Quantity"] = tDetail.Quantity;
                row["Total"] = (tDetail.Quantity * tDetail.Price).ToString("F2");
                transferDetailTable.Rows.Add(row);
            }

            grvTransferDetailInfo.DataSource = transferDetailTable;
        }

        private void frmViewTransferInformation_Load(object sender, EventArgs e)
        {
            grvTransferInfo.DataSource = BuildTransferTable();
            grvTransferDetailInfo.DataSource = BuildTransferDetailTable();
        }


        private void btnDistribute_Click(object sender, EventArgs e)
        {
            frmCusSalesReturn frm = new frmCusSalesReturn();
            frm.ShowDialog();
        }

        private void grvTransferInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grvTransferInfo.SelectedRows.Count > 0)
            {
                int transferID = Convert.ToInt32(grvTransferInfo.SelectedRows[0].Cells["SalesReturnID"].Value);
                LoadTransferDetailInformation(transferID);
            }
        }

        private void grvTransferInfo_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (grvTransferInfo.SelectedRows.Count > 0 && !grvTransferInfo.SelectedRows[0].IsNewRow)
            {
                int transferID = Convert.ToInt32(grvTransferInfo.SelectedRows[0].Cells["SalesReturnID"].Value);
                LoadTransferDetailInformation(transferID);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (grvTransferInfo.SelectedRows.Count > 0 && !grvTransferInfo.SelectedRows[0].IsNewRow)
            {
                int transferID = Convert.ToInt32(grvTransferInfo.SelectedRows[0].Cells["SalesReturnID"].Value);
                frmCusSalesReturn frm = new frmCusSalesReturn(transferID, true);
                frm.ShowDialog();
            }
            else
            {
                MessageBoxHelper.ShowInformation("You need to select a record.");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadSalesReturnByDate(dtpFromDate.Value.ToShortDateString(), dtpToDate.Value.ToShortDateString());

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (grvTransferInfo.SelectedRows.Count > 0 && !grvTransferInfo.SelectedRows[0].IsNewRow)
            {
                int salesReturnID = Convert.ToInt32(grvTransferInfo.SelectedRows[0].Cells["SalesReturnID"].Value);
                PrintSalesReturnInformation(salesReturnID);
            }
            else
            {
                MessageBoxHelper.ShowInformation("You need to select a record.");
            }
        }

        private DataTable BuildReportTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("SL");
            table.Columns.Add("Model");
            table.Columns.Add("ProductName");
            table.Columns.Add("TypeName");
            table.Columns.Add("Quantity");
            table.Columns.Add("Price");
            table.Columns.Add("Total");
            return table;
        }



        private void SetPharmachyInforamation(ref string PharmacyName, ref string Address, ref Organization pharmacy)
        {
            pharmacy = DataAccessProxy.GetAllPharmacy().Cast<Organization>().ToList().FirstOrDefault();
            Branch branch = DataAccessProxy.GetBracnhByID(MTBFConstants.AppConstants.BranchID);
            if (branch != null)
            {
                PharmacyName = branch.BranchName;
                Address = branch.Address + ", " + branch.Phone + ", " + branch.Fax;
            }
        }


        private void PrintSalesReturnInformation(int salesReturnID)
        {
            string pharmacyName = string.Empty;
            string pharmacyAddress = string.Empty;
            Organization pharmacy = new Organization();

            SalesReturn salesReturn = new SalesReturnManager().GetSalesReturnByID(salesReturnID);
            DataTable transferTable = BuildReportTable();
            if (salesReturn != null)
            {
                Customer customer = DataAccessProxy.GetCustomerByID(salesReturn.CustomerID);

                frmSalesReport objmainReport = new frmSalesReport();
                rptCusSalesReturn op = new rptCusSalesReturn();
                int count = 1;


                List<SalesReturnDetail> lstSalesReturnDetail = new List<SalesReturnDetail>();
                List<Product> lstProduct = new List<Product>();
                List<ProductModel> lstProductModel = new List<ProductModel>();
                List<ProductType> lstProductType = new List<ProductType>();

                lstSalesReturnDetail = new SalesReturnManager().GetAllSalesReturnDetailBySalesID(salesReturnID);
                string[] productID = lstSalesReturnDetail.Select(i => i.ProductCode).Distinct().ToArray();
                string filter = string.Empty;


                if (productID.Length > 0)
                {
                    for (int i = 0; i < productID.Length; i++)
                    {
                        if (filter != string.Empty) filter = filter + ",";
                        filter = filter + "'" + productID[i] + "'";
                    }

                    filter = String.Format("{0} IN ({1})", "ProductID", filter);
                    lstProduct = DataAccessProxy.GetFilteredProduct(filter);
                }
                int[] productModelID = lstProduct.Select(p => p.ProductModelID).Distinct().ToArray();
                if (productModelID.Length > 0)
                {
                    string modelfilter = String.Format("{0} IN ({1})", "ProductModelID", String.Join(",", productModelID));
                    lstProductModel = DataAccessProxy.GetFilteedProductModel(modelfilter);
                }


                int[] productTypeID = lstProduct.Select(p => p.ProductTypeID).Distinct().ToArray();
                if (productTypeID.Length > 0)
                {
                    string modelfilter = String.Format("{0} IN ({1})", "ProductTypeID", String.Join(",", productTypeID));
                    lstProductType = new ProductManager().GetFilteedProductType(modelfilter);
                }


                foreach (SalesReturnDetail returnDetail in lstSalesReturnDetail)
                {
                    Product product = lstProduct.Where(p => p.ProductID == returnDetail.ProductCode).FirstOrDefault();
                    ProductModel model = lstProductModel.Where(m => m.ProductModelID == product.ProductModelID).FirstOrDefault();
                    ProductType type = lstProductType.Where(t => t.ProductTypeID == product.ProductTypeID).FirstOrDefault();

                    DataRow row = transferTable.NewRow();
                    row["SL"] = count;
                    row["Model"] = (model != null) ? model.Name : string.Empty;
                    row["ProductName"] = returnDetail.ProductName;
                    row["TypeName"] = (type != null) ? type.TypeName : string.Empty;
                    row["Quantity"] = returnDetail.Quantity;
                    row["Price"] = returnDetail.Price;
                    row["Total"] = returnDetail.Quantity * returnDetail.Price;
                    transferTable.Rows.Add(row);
                    count++;
                }
                op.DataSource = transferTable;
                objmainReport.rptViewer.Document = op.Document;
                objmainReport.rptViewer.Dock = DockStyle.Fill;

                SetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress, ref pharmacy);
                op.txtPharmacyName.Text = pharmacyName;
                op.txtAddress.Text = pharmacyAddress;
                op.txtCompanyName.Text = pharmacy.OrganizationName;
                op.txtShortNote.Text = salesReturn.ShortNote;
                op.txtGrandTotal.Text = (salesReturn.Total - salesReturn.Discount).ToString("F2");
                if (pharmacy.OrganizationLogo != null)
                {
                    MemoryStream ms = new MemoryStream(pharmacy.OrganizationLogo);
                    Image returnImage = Image.FromStream(ms);
                    op.picLogo.Image = returnImage;
                }
                op.txtReturnID.Text = salesReturn.SalesReturnID.ToString();
                op.txtCustomerName.Text = customer.CustomerName;
                op.txtCustomerAddress.Text = customer.Address;
                op.txtPhone.Text = salesReturn.Phone;
                op.txtTotal.Text = salesReturn.Total.ToString();
                op.txtDiscount.Text = salesReturn.Discount.ToString();
                op.txtReceiveAmount.Text = salesReturn.PaidAmount.ToString();

                // op.txtOrderDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

                op.Run();
                objmainReport.MdiParent = this.MdiParent;
                objmainReport.Show();
            }


        }
    }
}