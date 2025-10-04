using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IMFS.Common.DTO;
using IFMS.Facade;
using NybSys.MTBF.Utility.Helper;
using IMFS.Common.Utility;
using NybSys.MTBF.Utility.Constants;
using NybSys.MTBF.Utility.Enums;
using IFMS.BLL;
using IMFS.Common.View;
using Tiles_Inventory.Reports;

namespace Tiles_Inventory
{
    public partial class frmViewPurchase : BaseForm
    {
        private string pharmacyName;
        private string pharmacyAddress;
        private List<PurchaseOrderDetail> _lstPurcahseOrderDetail = new List<PurchaseOrderDetail>();
        private List<Product> lstProduct = new List<Product>();
        public frmViewPurchase()
        {
            DataAccessProxy = new FacadeManager();
            InitializeComponent();

        }

        private void btnView_Click(object sender, EventArgs e)
        {
            LoadPurchaseOrderInformationByDate(dtpFromDate.Value.ToString("yyyy/MM/dd"), dtpToDate.Value.ToString("yyyy/MM/dd"));
        }

        /// <summary>
        /// Method to build sales detail table
        /// </summary>
        /// <returns></returns>
        //private DataTable BuildPurchaseDetailTable()
        //{
        //    DataTable table = new DataTable();
        //    table.Columns.Add("Product Name");
        //    table.Columns.Add("Quantity");
        //    table.Columns.Add("Square Feet");
        //    table.Columns.Add("Price");
        //    table.Columns.Add("Bar Code");
        //    table.Columns.Add("Total");
        //    return table;
        //}


        private DataTable BuildPurchaseDetailTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("PurcahseOrderDetailID");
            table.Columns.Add("Product Name");
            table.Columns.Add("Type");
            table.Columns.Add("Size");
            table.Columns.Add("Color");
            table.Columns.Add("Quantity");
            // table.Columns.Add("Square Feet");
            table.Columns.Add("Price");
            table.Columns.Add("Bar Code");
            table.Columns.Add("Total");
            return table;
        }


        private DataTable BuildPurchaseTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("PurchaseID");
            table.Columns.Add("PI Number");
            table.Columns.Add("Purchase Date");
            table.Columns.Add("Supplier Name");
            table.Columns.Add("Company Name");
            table.Columns.Add("Total");
            table.Columns.Add("Paid Amount");
            table.Columns.Add("Status");         
            return table;
        }

        /// <summary>
        /// Method to load purchase order infromation in grid
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        private void LoadPurchaseOrderInformationByDate(string fromDate, string toDate)
        {

            fromDate = fromDate + " 00:00:01";
            toDate = toDate + " 23:59:59";
            List<PurchaseOrder> lstPurchaseOrder = DataAccessProxy.GetAllPurchaseOrderByDate(fromDate, toDate, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<PurchaseOrder>().ToList();
            LoadDataInGrid(lstPurchaseOrder);
        }

        private void LoadDataInGrid(List<PurchaseOrder> lstPurchaseOrder)
        {
            DataTable purchaseTable = BuildPurchaseTable();

            foreach (PurchaseOrder purchaseOrder in lstPurchaseOrder)
            {
                //Supplier supplier = DataAccessProxy.GetSupplierByID(purchaseOrder.SupplierID);
                //Company company = DataAccessProxy.GetCompanyByID(purchaseOrder.CompanyID);

                DataRow row = purchaseTable.NewRow();
                row["PurchaseID"] = purchaseOrder.PurchaseID;
                row["PI Number"] = purchaseOrder.InvoiceNumber;
                row["Purchase Date"] = purchaseOrder.PurchaseDate.ToShortDateString();
                row["Supplier Name"] = purchaseOrder.SupplierName;
                row["Company Name"] = purchaseOrder.CompanyName;
                row["Total"] = purchaseOrder.PurchaseAmount;
                row["Status"] = Enum.GetName(typeof(IFMSEnum.PurchaseOrderStatus), purchaseOrder.Status);
                row["Paid Amount"] = purchaseOrder.PaidAmount;

                purchaseTable.Rows.Add(row);
            }
            if (purchaseTable.Rows.Count == 0)
            {
                MessageBoxHelper.ShowInformation("No data found for this combination.");
            }
            grvPurchaseOrder.DataSource = purchaseTable;
            grvPurchaseOrder.DisplayLayout.Bands[0].Columns["PurchaseID"].Hidden = true;
            grvPurchaseOrderDetail.DataSource = BuildPurchaseDetailTable();
        }


        private void grvPurchaseOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }


        private void LoadPurchaseOrderDetail(int purchaseOrderID)
        {
            DataTable purchaseDetailtable = BuildPurchaseDetailTable();
            _lstPurcahseOrderDetail = DataAccessProxy.GetAllPurchaseOrderDetailByPurchaseID(purchaseOrderID);

            foreach (PurchaseOrderDetail orderDetail in _lstPurcahseOrderDetail)
            {

                DataRow row = purchaseDetailtable.NewRow();
                Product product = lstProduct.Where(p => p.ProductID == orderDetail.ProductID).Cast<Product>().ToList().FirstOrDefault();
                if (product != null)
                {
                    row["PurcahseOrderDetailID"] = orderDetail.SerialNo;
                    row["Product Name"] = product.ProductName;
                    row["Type"] = product.TypeName;
                    row["Size"] = product.SizeName;
                    row["Color"] = product.ColorName;
                    row["Quantity"] = orderDetail.Quantity;
                    //   row["Square Feet"] = orderDetail.SquareFeet;
                    row["Price"] = (MTBFConstants.AppConstants.LoggedinUser.UserType == (int)MTBFEnums.UserType.Admin) ? orderDetail.PurchasePrice : 1;
                    row["Bar Code"] = product.BarCode;
                    row["Total"] = (MTBFConstants.AppConstants.LoggedinUser.UserType == (int)MTBFEnums.UserType.Admin) ? (orderDetail.PurchasePrice * orderDetail.Quantity).ToString("00.00") : (1 * orderDetail.Quantity).ToString("00.00");
                    purchaseDetailtable.Rows.Add(row);
                }

            }
            grvPurchaseOrderDetail.DataSource = purchaseDetailtable;
            grvPurchaseOrderDetail.AutoResizeColumns();
            grvPurchaseOrderDetail.Columns["PurcahseOrderDetailID"].Visible = false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (grvPurchaseOrder.Selected.Rows.Count > 0)
            {
                int _purchaseID = Convert.ToInt32(grvPurchaseOrder.Selected.Rows[0].Cells["PurchaseID"].Value);

                PurchaseOrder purchaseOrder = DataAccessProxy.GetPurchaseOrderByID(_purchaseID);
                if (purchaseOrder.Status == (int)IFMSEnum.PurchaseOrderStatus.Issued)
                {
                    frmPurchase frm = new frmPurchase(purchaseOrder, true, _lstPurcahseOrderDetail);
                    frm.OnLoadPurchaseInformation += new frmPurchase.LoadEventHandaler(frm_OnLoadPurchaseInformation);
                    frm.Show();
                }
                else
                {
                    MessageBoxHelper.ShowInformation("Only issued purchase order can be edit.");
                }
            }
        }

        void frm_OnLoadPurchaseInformation()
        {
            string fromDate = dtpFromDate.Value.ToString("yyyy/MM/dd");
            string toDate = dtpToDate.Value.ToString("yyyy/MM/dd");

            LoadPurchaseOrderInformationByDate(fromDate, toDate);
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (grvPurchaseOrder.Selected.Rows.Count > 0)
            {
                int _purchaseID = Convert.ToInt32(grvPurchaseOrder.Selected.Rows[0].Cells["PurchaseID"].Value);



                PurchaseOrder purchaseOrder = DataAccessProxy.GetPurchaseOrderByID(_purchaseID);
                if (purchaseOrder.Status != (int)IFMSEnum.PurchaseOrderStatus.Approved)
                {
                    DialogResult mresult = MessageBoxHelper.ShowConfirmation("Do you want to approve this order?");
                    if (mresult == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (ApprovePurchaseOrder(purchaseOrder) == (int)IFMSEnum.ReturnResult.Success)
                        {
                            MessageBoxHelper.ShowInformation("Purchase order approved successfully.");
                            LoadPurchaseOrderInformationByDate(dtpFromDate.Value.ToString("yyyy/MM/dd"), dtpToDate.Value.ToString("yyyy/MM/dd"));
                        }
                        else
                        {
                            MessageBoxHelper.ShowInformation("Failed to approve purchase order.");
                        }
                    }
                }
                else if (purchaseOrder.Status == (int)IFMSEnum.PurchaseOrderStatus.Approved)
                {
                    MessageBoxHelper.ShowInformation("This purchase order is alrady approved.");
                }

            }
            else
            {
                MessageBoxHelper.ShowInformation("You need to select a row.");
            }

        }

        private int ApprovePurchaseOrder(PurchaseOrder purchaseOrder)
        {
            purchaseOrder.Status = (int)IFMSEnum.PurchaseOrderStatus.Approved;
            return DataAccessProxy.ApprovePurchaseOrder(purchaseOrder);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmPurchase frm = new frmPurchase();
            frm.OnLoadPurchaseInformation += new frmPurchase.LoadEventHandaler(frm_OnLoadPurchaseInformation);
            frm.ShowDialog();
        }

        private void frmViewPurchase_Load(object sender, EventArgs e)
        {
            grvPurchaseOrder.DataSource = BuildPurchaseTable();
            grvPurchaseOrderDetail.DataSource = BuildPurchaseDetailTable();
            // grvPurchaseOrder.Columns["PurchaseID"].Visible = false;
            lstProduct = DataAccessProxy.GetAllProductByBranchAndOrganizationID(MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID);
            base.CheckAction(this);
            grvPurchaseOrder.DisplayLayout.Bands[0].Columns["PurchaseID"].Hidden = true;

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadPurchaseOrderInformationByDate(dtpFromDate.Value.ToShortDateString(), dtpToDate.Value.ToShortDateString());
        }

        private void btnPurchaseHistory_Click(object sender, EventArgs e)
        {
            frmProductPurchaseHistory frm = new frmProductPurchaseHistory();
            frm.ShowDialog();
        }

        private void btnAddCosting_Click(object sender, EventArgs e)
        {
            if (grvPurchaseOrder.Selected.Rows.Count > 0)
            {
                int _purchaseID = Convert.ToInt32(grvPurchaseOrder.Selected.Rows[0].Cells["PurchaseID"].Value);

                PurchaseOrder purchaseOrder = DataAccessProxy.GetPurchaseOrderByID(_purchaseID);
                frmPurchase frm = new frmPurchase(purchaseOrder, true, _lstPurcahseOrderDetail);
                frm.OnLoadPurchaseInformation += new frmPurchase.LoadEventHandaler(frm_OnLoadPurchaseInformation);
                frm.Show();
            }
            else
            {
                MessageBoxHelper.ShowInformation("Please select a row.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (grvPurchaseOrder.Selected.Rows.Count > 0)
            {
                DialogResult mresult = MessageBoxHelper.ShowConfirmation("Do you want to cancel this order?");
                if (mresult == System.Windows.Forms.DialogResult.Yes)
                {
                    int _purchaseID = Convert.ToInt32(grvPurchaseOrder.Selected.Rows[0].Cells["PurchaseID"].Value);

                    PurchaseOrder purchaseOrder = DataAccessProxy.GetPurchaseOrderByID(_purchaseID);
                    if (purchaseOrder.Status == (int)IFMSEnum.PurchaseOrderStatus.Issued)
                    {
                        if (CancelPurcahseOrder(purchaseOrder) == (int)IFMSEnum.ReturnResult.Success)
                        {
                            MessageBoxHelper.ShowInformation("Purchase order cancel successfully.");
                        }
                        else
                        {
                            MessageBoxHelper.ShowInformation("Failed to cancel purchase order.");
                        }

                    }
                    else if (purchaseOrder.Status == (int)IFMSEnum.PurchaseOrderStatus.Approved)
                    {
                        MessageBoxHelper.ShowInformation("Approved purcahse order can't be canceled.");
                    }

                }




            }
            else
            {
                MessageBoxHelper.ShowInformation("You need to select a row.");
            }
        }

        private int CancelPurcahseOrder(PurchaseOrder purchaseOrder)
        {
            purchaseOrder.Status = (int)IFMSEnum.PurchaseOrderStatus.Cancel;
            return DataAccessProxy.CancelPurchaseOrder(purchaseOrder);
        }

        private void btViewByBillNumber_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtInvoiceNumber.Text))
            {
                string filter = string.Format("{0}='{1}'", "InvoiceNumber", txtInvoiceNumber.Text.Trim());
                List<PurchaseOrder> lstPurchaseOrder = new List<PurchaseOrder>();
                lstPurchaseOrder = new PurchaseManager().GetFilteredPurchaseOrder(filter);
                LoadDataInGrid(lstPurchaseOrder);
            }
            else
            {
                MessageBoxHelper.ShowInformation("Please enter invoice number");
                txtInvoiceNumber.Focus();
            }
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

        private void btnPrint_Click(object sender, EventArgs e)
        {

            if (grvPurchaseOrder.Selected.Rows.Count > 0)
            {
                Organization pharmacy = new Organization();
                int _purchaseID = Convert.ToInt32(grvPurchaseOrder.Selected.Rows[0].Cells["PurchaseID"].Value);
                List<PurchaseReport> lstPurchaseReport = new List<PurchaseReport>();
                lstPurchaseReport = new PurchaseManager().GetPurchaseInformationByID(_purchaseID);

                //foreach (PurchaseReport purcahseReport in lstPurchaseReport)
                //{
                //    purcahseReport.Carton = purcahseReport.Quantity / Convert.ToDecimal(purcahseReport.CartonSize);
                //}

                rptPurchaseReport op = new rptPurchaseReport();
                frmSalesReport objmainReport = new frmSalesReport();
                op.DataSource = lstPurchaseReport;
                objmainReport.rptViewer.Document = op.Document;
                objmainReport.rptViewer.Dock = DockStyle.Fill;

                SetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress, ref pharmacy);

                op.txtPharmacyName.Text = pharmacyName;
                op.txtAddress.Text = pharmacyAddress;
                //op.txtCompanyName.Text = pharmacy.OrganizationName;
                //employee = DataAccessProxy.GetEmployeeByID(IFMSConstant.LoggedinUserID);
                //if (pharmacy.OrganizationLogo != null)
                //{
                //    MemoryStream ms = new MemoryStream(pharmacy.OrganizationLogo);
                //    Image returnImage = Image.FromStream(ms);
                //    op.picLogo.Image = returnImage;
                //}

                //op.txtServiceMan.Text = (employee != null) ? employee.EmployeeName : "Super Admin";
                op.Run();
                objmainReport.MdiParent = this.MdiParent;
                objmainReport.Show();
            }
            else
            {
                MessageBoxHelper.ShowInformation("Please select a record");
            }
        }

        private void btAddExpireDate_Click(object sender, EventArgs e)
        {
            if (grvPurchaseOrder.Selected.Rows.Count > 0 )
            {
                int purcahseOrderDetailID = Convert.ToInt32(grvPurchaseOrderDetail.SelectedRows[0].Cells["PurcahseOrderDetailID"].Value);
                string productName = grvPurchaseOrderDetail.SelectedRows[0].Cells["Product Name"].Value.ToString();
                frmAddExpireDate frm = new frmAddExpireDate(purcahseOrderDetailID, productName);
                frm.ShowDialog();
            }
            else
            {
                MessageBoxHelper.ShowInformation("Please select a product.");
            }
        }

        private void grvPurchaseOrder_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            if (grvPurchaseOrder.Selected.Rows.Count > 0)
            {
                int purchaseID = Convert.ToInt32(grvPurchaseOrder.Selected.Rows[0].Cells["PurchaseID"].Value);
                LoadPurchaseOrderDetail(purchaseID);
            }
        }


    }
}
