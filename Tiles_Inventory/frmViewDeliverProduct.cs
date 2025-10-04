//Modified By: Md. Foyjul Bary.
// Modification Date :23-07-2014
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;

using NybSys.MTBF.Utility.Helper;
using NybSys.MTBF.Utility.Resources;
using NybSys.MTBF.Utility.Constants;
using Tiles_Inventory.Reports;
using System.IO;
using NybSys.MTBF.Utility.Enums;
using IMFS.Common.DTO;

namespace Tiles_Inventory
{
    public partial class frmViewDeliverProduct : BaseForm
    {
        private List<DeliveryProduct> lstDeliveryProduct = new List<DeliveryProduct>();

        public frmViewDeliverProduct()
        {
            DataAccessProxy = new IFMS.Facade.FacadeManager();
            InitializeComponent();
        }

        private void btFilterSearch_Click(object sender, EventArgs e)
        {
            LoadFilteredProductReceiveInformation(dtpFromDate.Value.ToString("yyyy/MM/dd"), dtpToDate.Value.ToString("yyyy/MM/dd") + " 23:59:59");
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmDeliveryProduct frm = new frmDeliveryProduct();
            frm.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int deliveryProductID = 0;
            if (grvDeliveryProduct.Selected.Rows.Count > 0)
            {
                int.TryParse(grvDeliveryProduct.Selected.Rows[0].Cells["Delivery No"].Value.ToString(), out deliveryProductID);

                frmDeliveryProduct frm = new frmDeliveryProduct(true, deliveryProductID);
                frm.ShowDialog();
            }
            else
            {
                MessageBoxHelper.ShowInformation(SystemMessages.SELECT_ROW);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void grvDeliveryProduct_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {

        }

        //private void btnPrint_Click(object sender, EventArgs e)
        //{
        //    if (grvDeliveryProduct.Rows.Count > 0)
        //    {

        //        this.PrintDeliveryProductReport();

        //    }
        //    else
        //    {
        //        MessageBoxHelper.ShowInformation(SystemMessages.BLANK_GRID_CAN_NOT_BE_PRINTED);
        //    }
        //}

        //private void btnExport_Click(object sender, EventArgs e)
        //{
        //    if (grvDeliveryProduct.Rows.Count > 0)
        //    {
        //        ExportDeliveryProductTable(grvDeliveryProduct, @"\DeliveryProduct.csv");
        //    }
        //    else
        //    {
        //        MessageBoxHelper.ShowInformation(SystemMessages.BLANK_GRID_CAN_NOT_BE_EXPORTED);
        //    }
        //}

        //private void btnSerchOrderNo_Click(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(txtSalesOrderNo.Text))
        //    {
        //        LoadDeliveryProductInformationBySalesOrderNo("SO-" + txtSalesOrderNo.Text);
        //    }
        //    else
        //    {
        //        MessageBoxHelper.ShowInformation(SystemMessages.YOU_NEED_TO_ENTER_SALES_ORDER_NO);
        //        txtSalesOrderNo.Focus();
        //    }
        //}

        //private void txtDeliveryNoteNo_KeyPress(object sender, KeyPressEventArgs e)
        //{

        //    if ("1234567890".IndexOf(e.KeyChar) > -1 | e.KeyChar == Convert.ToChar(8))
        //    {
        //        e.Handled = false;
        //    }
        //    else
        //    {
        //        e.Handled = true;
        //    }
        //}

        //private void txtProductCode_KeyPress(object sender, KeyPressEventArgs e)
        //{

        //    if ("1234567890".IndexOf(e.KeyChar) > -1 | e.KeyChar == Convert.ToChar(8))
        //    {
        //        e.Handled = false;
        //    }
        //    else
        //    {
        //        e.Handled = true;
        //    }
        //}

        //private void txtSalesOrderNo_KeyPress(object sender, KeyPressEventArgs e)
        //{

        //    if ("1234567890".IndexOf(e.KeyChar) > -1 | e.KeyChar == Convert.ToChar(8))
        //    {
        //        e.Handled = false;
        //    }
        //    else
        //    {
        //        e.Handled = true;
        //    }
        //}

        //private void btnLoad_Click(object sender, EventArgs e)
        //{
        //    if (ValidLoadData())
        //    {
        //        gbSearch.Enabled = true;
        //        gbDeliveryInfo.Enabled = true;
        //        gbDeliveryDetail.Enabled = true;
        //    }
        //}

        //#endregion

        //#region "Private Methods"

        ///// <summary>
        ///// Method to load company inforamtion in combo box.
        ///// </summary>
        //private void LoadCompanyCombo()
        //{
        //    List<Company> lstCompany = new List<Company>();

        //    Company company = new Company();
        //    company.CompanyCode = MTBFConstants.DataField.COMBO_DEFULT_CODE;
        //    company.CompanyName = MTBFConstants.DataField.COMBO_DEFULT_NAME;

        //    lstCompany = new OrganizationSetupManager().GetAllCompany().Cast<Company>().ToList();
        //    lstCompany.Insert(0, company);

        //    UIHelper.SetComboBoxData(cmbCompany, lstCompany, MTBFConstants.DataField.COMPANY_NAME, MTBFConstants.DataField.COMPANY_CODE);
        //}

        /// <summary>
        /// Method to get filtered product receive information.
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="fromAmount"></param>
        /// <param name="toAmount"></param>
        private void LoadFilteredProductReceiveInformation(string fromDate, string toDate)
        {
            lstDeliveryProduct = DataAccessProxy.GetDeliveryProductByDate(fromDate, toDate).Cast<DeliveryProduct>().ToList();

            DataTable deliveryTable = BuildDeliveryProductTable();
            if (lstDeliveryProduct.Count > 0)
            {
                foreach (DeliveryProduct deliveryProduct in lstDeliveryProduct)
                {
                    DataRow row = deliveryTable.NewRow();
                    row["Delivery No"] = deliveryProduct.DeliveryID;
                    row["Deliery Date"] = deliveryProduct.DeliveryDate.ToString("dd/MM/yyyy");
                    row["Customer Name"] = deliveryProduct.CustomerName;
                    row["Custoemr Phone"] = deliveryProduct.Phone;
                    row["Sales Order No"] = deliveryProduct.SalesID;
                    deliveryTable.Rows.Add(row);
                }
            }
            else
            {
                MessageBoxHelper.ShowInformation(SystemMessages.NO_DATA_FOUND);
            }
            grvDeliveryProduct.DataSource = deliveryTable;

            grvDeliveryProductDetail.DataSource = BuildDeliveryProductDetailTable();

            grvDeliveryProduct.DisplayLayout.Override.ActiveCellAppearance.Reset();
            grvDeliveryProduct.DisplayLayout.Override.ActiveRowAppearance.Reset();
        }

        /// <summary>
        /// Method to build receive product table.
        /// </summary>
        /// <returns></returns>
        private DataTable BuildDeliveryProductTable()
        {
            DataTable table = new DataTable();

            table.Columns.Add("Delivery No");
            table.Columns.Add("Deliery Date");
            table.Columns.Add("Customer Name");
            table.Columns.Add("Custoemr Phone");
            table.Columns.Add("Sales Order No");

            return table;
        }

        ///// <summary>
        ///// Method to build deliveryproduct table for export.
        ///// </summary>
        ///// <returns></returns>
        //private DataTable BuildDeliveryProductTableForExport()
        //{
        //    DataTable table = new DataTable();
        //    table.Columns.Add("DelieryProductID");
        //    table.Columns.Add("Deliery Date");
        //    table.Columns.Add("Customer Name");
        //    table.Columns.Add(MTBFConstants.GridHeader.PHONE);
        //    table.Columns.Add(MTBFConstants.GridHeader.TOTAL);

        //    return table;
        //}

        ///// <summary>
        ///// Method to fill data in deliveryproduct table.
        ///// </summary>
        ///// <returns></returns>
        //private DataTable FillDataInDeliveryProductTable()
        //{
        //    DataTable deliveryProductTable = BuildDeliveryProductTable();

        //    foreach (UltraGridRow gridRow in grvDeliveryProduct.Rows)
        //    {
        //        DataRow row = deliveryProductTable.NewRow();

        //        row[MTBFConstants.GridHeader.DELIVERY_NOTE_NO] = gridRow.Cells[MTBFConstants.GridHeader.DELIVERY_NOTE_NO].Text;
        //        row[MTBFConstants.GridHeader.DELIVERY_DATE] = gridRow.Cells[MTBFConstants.GridHeader.DELIVERY_DATE].Text;
        //        row[MTBFConstants.GridHeader.CUSTOMER_NAME] = gridRow.Cells[MTBFConstants.GridHeader.CUSTOMER_NAME].Text;
        //        row[MTBFConstants.GridHeader.PHONE] = gridRow.Cells[MTBFConstants.GridHeader.PHONE].Text;
        //        row[MTBFConstants.GridHeader.TOTAL] = gridRow.Cells[MTBFConstants.GridHeader.TOTAL].Text;

        //        deliveryProductTable.Rows.Add(row);
        //    }

        //    return deliveryProductTable;
        //}

        ///// <summary>
        ///// Method to fill data in deliveryproduct table for export.
        ///// </summary>
        ///// <returns></returns>
        //private DataTable FillDataInDeliveryProductTableForExport()
        //{
        //    DataTable deliveryProductTable = BuildDeliveryProductTableForExport();

        //    foreach (UltraGridRow gridRow in grvDeliveryProduct.Rows)
        //    {
        //        DataRow row = deliveryProductTable.NewRow();

        //        row[MTBFConstants.GridHeader.DELIVERY_NOTE_NO] = gridRow.Cells[MTBFConstants.GridHeader.DELIVERY_NOTE_NO].Text;
        //        row[MTBFConstants.GridHeader.DELIVERY_DATE] = gridRow.Cells[MTBFConstants.GridHeader.DELIVERY_DATE].Text;
        //        row[MTBFConstants.GridHeader.CUSTOMER_NAME] = gridRow.Cells[MTBFConstants.GridHeader.CUSTOMER_NAME].Text;
        //        row[MTBFConstants.GridHeader.PHONE] = gridRow.Cells[MTBFConstants.GridHeader.PHONE].Text;
        //        row[MTBFConstants.GridHeader.TOTAL] = gridRow.Cells[MTBFConstants.GridHeader.TOTAL].Text;

        //        deliveryProductTable.Rows.Add(row);
        //    }

        //    return deliveryProductTable;
        //}

        ///// <summary>
        ///// Method to export deliveryproduct information in csv file.
        ///// </summary>
        ///// <param name="grid"></param>
        ///// <param name="fileName"></param>
        //private void ExportDeliveryProductTable(UltraGrid grid, string fileName)
        //{
        //    string location = "";
        //    DataTable table = new DataTable();
        //    FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
        //    DialogResult result = folderBrowser.ShowDialog();
        //    if (result == DialogResult.OK)
        //    {
        //        table = FillDataInDeliveryProductTableForExport();
        //        location = folderBrowser.SelectedPath + fileName;
        //        CSVCreator.CreateCSVFile(table, location);
        //        MessageBoxHelper.ShowInformation(SystemMessages.EXPORT_COMPLETED);
        //    }
        //}

        /// <summary>
        /// Method to build receive product table.
        /// </summary>
        /// <returns></returns>
        private DataTable BuildDeliveryProductDetailTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Product Name");
            table.Columns.Add("Sales Quantity");
            table.Columns.Add("Delivered Quantity");
            table.Columns.Add("Quantity");
            table.Columns.Add("Due");
            return table;
        }

        ///// <summary>
        ///// Method to load delivery product information in grid  by delivery note no.
        ///// </summary>
        ///// <param name="deliveryNoteNo"></param>
        //private void LoadDeliveryProductInformationByDeliveryNoteNo(string deliveryNoteNo)
        //{
        //    DataTable deliveryTable = BuildDeliveryProductTable();

        //    DeliveryProduct deliveryProduct = new DeliveryProduct();
        //    deliveryProduct = new ProductDeliveryManager().GetDeliveryProductByDeliveryNoteNo(deliveryNoteNo);
        //    if (deliveryProduct != null && deliveryProduct.CompanyCode == cmbCompany.Value.ToString() && deliveryProduct.DeliveryType == (int)MTBFEnums.DeliveryType.RegularDelivery)
        //    {
        //        Customer customer = new CustomerManager().GetCustomerByID(deliveryProduct.CustomerID);
        //        DataRow row = deliveryTable.NewRow();
        //        row[MTBFConstants.GridHeader.DELIVERY_NOTE_NO] = deliveryProduct.DeliveryNoteNo;
        //        row[MTBFConstants.GridHeader.SALES_ORDER_NO] = deliveryProduct.SalesOrderNumber;
        //        row[MTBFConstants.GridHeader.DELIVERY_DATE] = deliveryProduct.DeliveryDate.ToShortDateString();
        //        row[MTBFConstants.GridHeader.CUSTOMER_NAME] = (customer != null) ? customer.CustomerName : string.Empty;
        //        row[MTBFConstants.GridHeader.PHONE] = (customer != null) ? customer.Phone : string.Empty;
        //        row[MTBFConstants.GridHeader.TOTAL] = deliveryProduct.Total.ToString();
        //        row[MTBFConstants.GridHeader.RECEIVE_PRODUCT_ID] = deliveryProduct.DeliveryProductID;
        //        deliveryTable.Rows.Add(row);
        //    }
        //    else
        //    {
        //        MessageBoxHelper.ShowInformation(SystemMessages.NO_DATA_FOUND);
        //    }

        //    grvDeliveryProduct.DataSource = deliveryTable;
        //    grvDeliveryProduct.DisplayLayout.Bands[0].Columns[MTBFConstants.GridHeader.RECEIVE_PRODUCT_ID].Hidden = true;
        //    grvDeliveryProductDetail.DataSource = BuildDeliveryProductDetailTable();

        //    grvDeliveryProduct.DisplayLayout.Override.ActiveCellAppearance.Reset();
        //    grvDeliveryProduct.DisplayLayout.Override.ActiveRowAppearance.Reset();
        //}

        ////private void LoadDeliveryProductInformationByOrganizationSetup(string companyCode, string departmentCode, string sectionCode)
        ////{
        ////    DataTable deliveryTable = BuildDeliveryProductTable();

        ////    List<DeliveryProduct> lstDeliveryProduct = new List<DeliveryProduct>();
        ////    lstDeliveryProduct = new InventoryManager().GetDeliveryProductByDeliveryOrganizationSetup(companyCode, departmentCode, sectionCode).Cast<DeliveryProduct>().ToList();
        ////    if (lstDeliveryProduct.Count > 0)
        ////    {
        ////        foreach (DeliveryProduct deliveryProduct in lstDeliveryProduct)
        ////        {
        ////            Customer customer = _SalesProxy.GetCustomerByID(deliveryProduct.CustomerID);
        ////            DataRow row = deliveryTable.NewRow();
        ////            row[MTBFConstants.GridHeader.DELIVERY_NOTE_NO] = deliveryProduct.DeliveryNoteNo;
        ////            row[MTBFConstants.GridHeader.SALES_ORDER_NO] = deliveryProduct.SalesOrderNumber;
        ////            row[MTBFConstants.GridHeader.DELIVERY_DATE] = deliveryProduct.DeliveryDate.ToShortDateString();
        ////            row[MTBFConstants.GridHeader.CUSTOMER_NAME] = (customer != null) ? customer.CustomerName : string.Empty;
        ////            row[MTBFConstants.GridHeader.PHONE] = (customer != null) ? customer.Phone : string.Empty;
        ////            row[MTBFConstants.GridHeader.TOTAL] = deliveryProduct.Total.ToString();
        ////            row[MTBFConstants.GridHeader.RECEIVE_PRODUCT_ID] = deliveryProduct.DeliveryProductID;
        ////            deliveryTable.Rows.Add(row);
        ////        }
        ////    }
        ////    else
        ////    {
        ////        MessageBoxHelper.ShowInformation(SystemMessages.NO_DATA_FOUND);
        ////    }

        ////    grvDeliveryProduct.DataSource = deliveryTable;
        ////    grvDeliveryProduct.DisplayLayout.Bands[0].Columns[MTBFConstants.GridHeader.RECEIVE_PRODUCT_ID].Hidden = true;

        ////    grvDeliveryProduct.DisplayLayout.Override.ActiveCellAppearance.Reset();
        ////    grvDeliveryProduct.DisplayLayout.Override.ActiveRowAppearance.Reset();
        ////}


        ///// <summary>
        ///// Method to load delivery product information in grid  by delivery note no.
        ///// </summary>
        ///// <param name="salesOrderNo"></param>
        //private void LoadDeliveryProductInformationBySalesOrderNo(string salesOrderNo)
        //{
        //    DataTable deliveryTable = BuildDeliveryProductTable();
        //    DeliveryProduct deliveryProduct = new DeliveryProduct();

        //    deliveryProduct = new ProductDeliveryManager().GetDeliveryProductBySalesOrderNumber(salesOrderNo).FirstOrDefault();

        //    if (deliveryProduct != null && deliveryProduct.CompanyCode == cmbCompany.Value.ToString() && deliveryProduct.DeliveryType == (int)MTBFEnums.DeliveryType.RegularDelivery)
        //    {
        //        Customer customer = new CustomerManager().GetCustomerByID(deliveryProduct.CustomerID);
        //        DataRow row = deliveryTable.NewRow();
        //        row[MTBFConstants.GridHeader.DELIVERY_NOTE_NO] = deliveryProduct.DeliveryNoteNo;
        //        row[MTBFConstants.GridHeader.SALES_ORDER_NO] = deliveryProduct.SalesOrderNumber;
        //        row[MTBFConstants.GridHeader.DELIVERY_DATE] = deliveryProduct.DeliveryDate.ToShortDateString();
        //        row[MTBFConstants.GridHeader.CUSTOMER_NAME] = (customer != null) ? customer.CustomerName : string.Empty;
        //        row[MTBFConstants.GridHeader.PHONE] = (customer != null) ? customer.Phone : string.Empty;
        //        row[MTBFConstants.GridHeader.TOTAL] = deliveryProduct.Total.ToString();
        //        row[MTBFConstants.GridHeader.RECEIVE_PRODUCT_ID] = deliveryProduct.DeliveryProductID;
        //        deliveryTable.Rows.Add(row);
        //    }

        //    else
        //    {
        //        MessageBoxHelper.ShowInformation(SystemMessages.NO_DATA_FOUND);
        //    }

        //    grvDeliveryProduct.DataSource = deliveryTable;
        //    grvDeliveryProduct.DisplayLayout.Bands[0].Columns[MTBFConstants.GridHeader.RECEIVE_PRODUCT_ID].Hidden = true;
        //    grvDeliveryProductDetail.DataSource = BuildDeliveryProductDetailTable();

        //    grvDeliveryProduct.DisplayLayout.Override.ActiveCellAppearance.Reset();
        //    grvDeliveryProduct.DisplayLayout.Override.ActiveRowAppearance.Reset();
        //}

        ///// <summary>
        ///// Method to load delivery product informatin in grid by product code
        ///// </summary>
        ///// <param name="deliveryNoteNo"></param>
        //private void LoadDeliveryProductInformationByProductCode(string productCode)
        //{
        //    DataTable deliveryTable = BuildDeliveryProductTable();

        //    List<DeliveryProduct> lstDeliveryProduct = new List<DeliveryProduct>();
        //    lstDeliveryProduct = new ProductDeliveryManager().GetDeliveryProductByProductCode(productCode).Cast<DeliveryProduct>().Where(d => d.CompanyCode == cmbCompany.Value.ToString() && d.DeliveryType == (int)MTBFEnums.DeliveryType.RegularDelivery).ToList();
        //    if (lstDeliveryProduct.Count > 0)
        //    {
        //        foreach (DeliveryProduct deliveryProduct in lstDeliveryProduct)
        //        {
        //            Customer customer = new CustomerManager().GetCustomerByID(deliveryProduct.CustomerID);
        //            DataRow row = deliveryTable.NewRow();
        //            row[MTBFConstants.GridHeader.DELIVERY_NOTE_NO] = deliveryProduct.DeliveryNoteNo;
        //            row[MTBFConstants.GridHeader.SALES_ORDER_NO] = deliveryProduct.SalesOrderNumber;
        //            row[MTBFConstants.GridHeader.DELIVERY_DATE] = deliveryProduct.DeliveryDate.ToShortDateString();
        //            row[MTBFConstants.GridHeader.CUSTOMER_NAME] = (customer != null) ? customer.CustomerName : string.Empty;
        //            row[MTBFConstants.GridHeader.PHONE] = (customer != null) ? customer.Phone : string.Empty;
        //            row[MTBFConstants.GridHeader.TOTAL] = deliveryProduct.Total.ToString();
        //            row[MTBFConstants.GridHeader.RECEIVE_PRODUCT_ID] = deliveryProduct.DeliveryProductID;
        //            deliveryTable.Rows.Add(row);
        //        }
        //    }
        //    else
        //    {
        //        MessageBoxHelper.ShowInformation(SystemMessages.NO_DATA_FOUND);
        //    }

        //    grvDeliveryProduct.DataSource = deliveryTable;
        //    grvDeliveryProduct.DisplayLayout.Bands[0].Columns[MTBFConstants.GridHeader.RECEIVE_PRODUCT_ID].Hidden = true;
        //    grvDeliveryProductDetail.DataSource = BuildDeliveryProductDetailTable();

        //    grvDeliveryProduct.DisplayLayout.Override.ActiveCellAppearance.Reset();
        //    grvDeliveryProduct.DisplayLayout.Override.ActiveRowAppearance.Reset();
        //}

        /// <summary>
        /// Method to load delivery product detail in grid.
        /// </summary>
        /// <param name="deliveryProductID"></param>
        private void LoadDeliveryProductDetail(int deliveryProductID)
        {
            DataTable deliveryProductTable = BuildDeliveryProductDetailTable();
            List<DeliveryProductDetail> lstDeliveryProductDetail = new List<DeliveryProductDetail>();
            lstDeliveryProductDetail = DataAccessProxy.GetAllDeliveryProductDetailByDeliveryID(deliveryProductID).Cast<DeliveryProductDetail>().ToList();
            foreach (DeliveryProductDetail deliveryProductDetail in lstDeliveryProductDetail)
            {
                DataRow row = deliveryProductTable.NewRow();

                row["Product Name"] = deliveryProductDetail.ProductName;
                row["Sales Quantity"] = deliveryProductDetail.SalesQuantity;
                row["Delivered Quantity"] = deliveryProductDetail.SalesQuantity - deliveryProductDetail.Due;
                row["Quantity"] = deliveryProductDetail.Quantity;
                row["Due"] = deliveryProductDetail.Due;
                deliveryProductTable.Rows.Add(row);
            }
            grvDeliveryProductDetail.DataSource = deliveryProductTable;
            grvDeliveryProductDetail.DisplayLayout.Override.ActiveCellAppearance.Reset();
            grvDeliveryProductDetail.DisplayLayout.Override.ActiveRowAppearance.Reset();

        }

        private void grvDeliveryProduct_AfterSelectChange(object sender, ClickCellEventArgs e)
        {
            int deliveryProductID = 0;
            if (grvDeliveryProduct.Selected.Rows.Count > 0)
            {
                int.TryParse(grvDeliveryProduct.Selected.Rows[0].Cells["Delivery No"].Value.ToString(), out deliveryProductID);
                LoadDeliveryProductDetail(deliveryProductID);
            }
        }

        private void frmViewDeliverProduct_Load(object sender, EventArgs e)
        {
            LoadSalesOrderCombo();
            base.CheckAction(this);
        }

        private void LoadSalesOrderCombo()
        {
            List<SalesOrder> lstSalesOrder = new List<SalesOrder>();
            lstSalesOrder = DataAccessProxy.GetAllSalesOrderByBranchAndOrganization(MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<SalesOrder>().ToList();
            if (lstSalesOrder.Count > 0)
                UIHelper.SetComboBoxData(cmbSoNumber, lstSalesOrder, "SalesID", "SalesID");
        }

        private void cmbSoNumber_ValueChanged(object sender, EventArgs e)
        {
            if (cmbSoNumber.Value != null)
            {

                List<DeliveryProduct> lstDProduct = lstDeliveryProduct.Where(d => d.SalesID == Convert.ToInt32(cmbSoNumber.Value)).Cast<DeliveryProduct>().ToList();

                DataTable deliveryTable = BuildDeliveryProductTable();
                if (lstDProduct.Count > 0)
                {
                    foreach (DeliveryProduct deliveryProduct in lstDProduct)
                    {
                        DataRow row = deliveryTable.NewRow();
                        row["Delivery No"] = deliveryProduct.DeliveryID;
                        row["Deliery Date"] = deliveryProduct.DeliveryDate.ToString("dd/MM/yyyy");
                        row["Customer Name"] = deliveryProduct.CustomerName;
                        row["Custoemr Phone"] = deliveryProduct.Phone;
                        row["Sales Order No"] = deliveryProduct.SalesID;
                        deliveryTable.Rows.Add(row);
                    }
                }
                else
                {
                    MessageBoxHelper.ShowInformation(SystemMessages.NO_DATA_FOUND);
                }
                grvDeliveryProduct.DataSource = deliveryTable;

                grvDeliveryProductDetail.DataSource = BuildDeliveryProductDetailTable();

                grvDeliveryProduct.DisplayLayout.Override.ActiveCellAppearance.Reset();
                grvDeliveryProduct.DisplayLayout.Override.ActiveRowAppearance.Reset();


            }
        }

        ///// <summary>
        ///// Method to print delivery product information.
        ///// </summary>
        //private void PrintDeliveryProductReport()
        //{
        //    Users user = MTBFConstants.AppConstants.LoggedinUser;
        //    Company company = new OrganizationSetupManager().GetCompanyByCode(user.CompanyCode);
        //    Employee employee = new EmployeeManager().GetEmployeeByID(user.EmployeeID);
        //    rptDeliveryProduct deliveryProductReport = new rptDeliveryProduct();

        //    deliveryProductReport.lblCompanyName.Text = company.CompanyName;
        //    deliveryProductReport.txtRegistrationNo.Text = company.RegistrationNo;
        //    deliveryProductReport.txtCompanyAddress.Text = company.CompanyAddress;
        //    deliveryProductReport.txtCompanyEmail.Text = company.Email;
        //    deliveryProductReport.txtFaxNumber.Text = company.FaxNumber;
        //    deliveryProductReport.txtTelephoneNo.Text = company.TelephoneNumber;
        //    deliveryProductReport.txtCreatedBy.Text = employee.EmployeeName;

        //    if (company.CompanyLogo != null)
        //    {
        //        MemoryStream ms = new MemoryStream(company.CompanyLogo);
        //        Image returnImage = Image.FromStream(ms);
        //        deliveryProductReport.picLogo.Image = returnImage;
        //    }

        //    frmViewReport frm = new frmViewReport();
        //    frm.MdiParent = this.MdiParent;
        //    deliveryProductReport.DataSource = FillDataInDeliveryProductTable();
        //    frm.rptViewer.Document = deliveryProductReport.Document;
        //    frm.rptViewer.Dock = DockStyle.Fill;
        //    deliveryProductReport.Run();
        //    frm.Show();
        //}




    }
}
