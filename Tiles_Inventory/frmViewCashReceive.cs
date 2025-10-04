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
using Tiles_Inventory.Reports;
using NybSys.MTBF.Utility.Helper;
using IMFS.Common.View;
using NybSys.MTBF.Utility.Enums;
using Infragistics.Win.UltraWinGrid;

namespace Tiles_Inventory
{
    public partial class frmViewCashReceive : BaseForm
    {
        private int _cashReceiveID = 0;
        List<CashReceive> lstCashReceive = new List<CashReceive>();
        public frmViewCashReceive()
        {
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        private void btnView_Click(System.Object sender, System.EventArgs e)
        {
            LoadCashReceiveInformation();
        }

        private void btnAdd_Click(System.Object sender, System.EventArgs e)
        {
            frmCashReceive frm = new frmCashReceive(false, _cashReceiveID);
            frm.OnCashReceiveInformationLoad += OnReceiveInforamtionUpdated;
            frm.ShowDialog();
        }

        private void OnReceiveInforamtionUpdated()
        {
            LoadCashReceiveInformation();
        }

        private void btnEdit_Click(System.Object sender, System.EventArgs e)
        {
            if (grvCashReceiveInformation.Selected.Rows.Count > 0)
            {
                _cashReceiveID = Convert.ToInt32(grvCashReceiveInformation.Selected.Rows[0].Cells[0].Value);
                frmCashReceive frm = new frmCashReceive(true, _cashReceiveID);
                frm.OnCashReceiveInformationLoad += OnReceiveInforamtionUpdated;
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a row", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void btnClose_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }


        private DataTable BuildCashReceiveTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Cash Receive ID");
            dt.Columns.Add("Memo Number");
            dt.Columns.Add("Customer Name");
            dt.Columns.Add("Receive Date");
            dt.Columns.Add("Cash Amount");
            dt.Columns.Add("Bank Amount");
            dt.Columns.Add("Receive Amount");
            dt.Columns.Add("Bill Reference");
            dt.Columns.Add("Employee");
          //  dt.Columns.Add("Status");
            return dt;
        }

        private void LoadDataInGrid(List<CashReceive> lstCashReceive)
        {
            lblTotalRecord.Text = lstCashReceive.Count.ToString();
            DataTable dt = BuildCashReceiveTable();
            List<Customer> lstCustomer = new CustomerManager().GetAllCustomerByBranchID(MTBFConstants.AppConstants.BranchID);
            foreach (CashReceive cashReceive in lstCashReceive)
            {
                Customer customer = lstCustomer.Where(c => c.CustomerID == cashReceive.CustomerID).FirstOrDefault();
                DataRow row = dt.NewRow();
                row["Cash Receive ID"] = cashReceive.CashReceiveID;
                row["Memo Number"] = cashReceive.ReferenceNo;
                row["Customer Name"] = (customer != null) ? customer.CustomerName : string.Empty;
                row["Receive Date"] = cashReceive.ReceiveDate.ToString("dd/MM/yyyy");
                row["Cash Amount"] = cashReceive.CashAmount;
                row["Bank Amount"] = cashReceive.BankAmount;
                row["Receive Amount"] = cashReceive.Amount;
                row["Bill Reference"] = cashReceive.BillRefNumber;
                row["Employee"] = cashReceive.CreatedBy;
               // row["Status"] = Enum.GetName(typeof(MTBFEnums.CashReceieStatus), cashReceive.Status);
                dt.Rows.Add(row);
            }
            grvCashReceiveInformation.DataSource = dt;

            int count = 0;

            foreach (CashReceive cashReceive in lstCashReceive)
            {
                if (cashReceive.Status == (int)MTBFEnums.CashReceieStatus.Cancel)
                {
                    UIHelper.MarkDataGridRowAsInvalid(grvCashReceiveInformation.Rows[count]);
                }
                else
                {
                    UIHelper.MarkDataGridRowAsNormal(grvCashReceiveInformation.Rows[count]);
                }
                count++;
            }




        }
        /// <summary>
        /// Method to load cash receive information in grid.
        /// </summary>
        /// <remarks></remarks>
        private void LoadCashReceiveInformation()
        {
            string fromDate = string.Empty;
            string toDate = string.Empty;
            fromDate = dtpFromDate.Value.ToString("yyyy/MM/dd") + " 00:00:01";
            toDate = dtpToDate.Value.ToString("yyyy/MM/dd") + " 23:59:59";

            lstCashReceive = DataAccessProxy.GetAllCashReceiveInformationByDateRange(fromDate, toDate).Cast<CashReceive>().OrderBy(c => c.CustomerID).ToList();
            lstCashReceive = lstCashReceive.Where(c => c.BranchID == MTBFConstants.AppConstants.BranchID && c.OrganizationID == MTBFConstants.AppConstants.OrganizationID).Cast<CashReceive>().ToList();

            List<Customer> lstCustomer = new CustomerManager().GetAllCustomerByBranchID(MTBFConstants.AppConstants.BranchID);
            if (lstCashReceive.Count > 0)
            {
                LoadDataInGrid(lstCashReceive);
            }
            else
            {
                MessageBox.Show("No data found for this combination.", "Transport Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void frmViewCashReceive_Load(System.Object sender, System.EventArgs e)
        {
            //  LoadCashReceiveInformation()
            grvCashReceiveInformation.DataSource = BuildCashReceiveTable();
            LoadCustomerCombo();
        }

        private DataTable BuildCustomerTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("CustomerID");
            table.Columns.Add("Customer Name");
            table.Columns.Add("Address");
            table.Columns.Add("Phone");
            table.Columns.Add("Customer Type");
            return table;
        }

        private void LoadCustomerCombo()
        {

            List<Customer> lstCustomer = new CustomerManager().GetAllCustomerByBranchID(MTBFConstants.AppConstants.BranchID).ToList();
            DataTable table = BuildCustomerTable();

            foreach (Customer customer in lstCustomer)
            {
                DataRow row = table.NewRow();
                row["CustomerID"] = customer.CustomerID;
                row["Customer Name"] = customer.CustomerName;
                row["Address"] = customer.Address;
                row["Phone"] = customer.Phone;
                row["Customer Type"] = Enum.GetName(typeof(MTBFEnums.CustomerType), customer.CustomerType);
                table.Rows.Add(row);

            }

            cmbCustomerName.DisplayMember = "Customer Name";
            cmbCustomerName.ValueMember = "CustomerID";
            cmbCustomerName.DataSource = table;

            cmbCustomerName.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
            cmbCustomerName.DisplayLayout.PerformAutoResizeColumns(false, PerformAutoSizeType.AllRowsInBand, true);
            cmbCustomerName.DisplayLayout.Bands[0].Columns["CustomerID"].Hidden = true;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCashReceiveInformation();
        }

        private void btnPrit_Click(object sender, EventArgs e)
        {
            if (grvCashReceiveInformation.Selected.Rows.Count > 0)
            {
                int cashReceiveID = Convert.ToInt32(grvCashReceiveInformation.Selected.Rows[0].Cells[0].Value);
                PrintMoneyReceipt(cashReceiveID);
            }
        }

        private void PrintMoneyReceipt(int cashReceiveID)
        {
            MoneyReceipt moneReceipt = new MoneyReceipt();
            moneReceipt = new CashReceiveManager().GetMoneyReceiveData(cashReceiveID, MTBFConstants.AppConstants.BranchID);

            if (moneReceipt.SalesDate == "1900-01-01")
            {
                moneReceipt.SalesDate = string.Empty;
            }


            string strAmount = moneReceipt.Amount.ToString("F2");
            string amountWord = string.Empty;
            string amountInCent = string.Empty;
            if (strAmount.Contains("."))
            {
                strAmount = strAmount.Substring(strAmount.LastIndexOf(".") + 1);
                if (strAmount != "00")
                {
                    amountInCent = UIHelper.NumberToWords(Convert.ToInt32(strAmount));
                }
                strAmount = moneReceipt.Amount.ToString("F2");
                strAmount = strAmount.Substring(0, strAmount.LastIndexOf("."));
                amountWord = UIHelper.NumberToWords(Convert.ToInt32(strAmount));
                if (string.IsNullOrEmpty(amountInCent))
                {
                    amountWord = amountWord + " " + "Taka";
                }
                else
                {
                    amountWord = amountWord + " " + "Taka and " + amountInCent + " poysha";
                }
            }
            else
            {
                amountWord = UIHelper.NumberToWords(Convert.ToInt32(moneReceipt.Amount));
                amountWord = amountWord + " " + "Taka.";
            }

            moneReceipt.AmountInWord = char.ToUpper(amountWord[0]) + amountWord.Substring(1);


            List<MoneyReceipt> lstMoneReceipt = new List<MoneyReceipt>();
            lstMoneReceipt.Add(moneReceipt);

            rptMoneReceipt op = new rptMoneReceipt();
            frmSalesReport objmainReport = new frmSalesReport();
            op.DataSource = lstMoneReceipt;
            objmainReport.rptViewer.Document = op.Document;
            objmainReport.rptViewer.Dock = DockStyle.Fill;
            op.Run();
            objmainReport.ShowDialog();



        }

        private void btSearchByMemo_Click(object sender, EventArgs e)
        {
            string filter = string.Format("{0}='{1}' and {2}={3}", "ReferenceNo", txtMemoNumber.Text, "BranchID", MTBFConstants.AppConstants.BranchID);

            CashReceive cashReceive = new CashReceiveManager().GetFilteredCashReceive(filter).FirstOrDefault();
            lstCashReceive = new List<CashReceive>();
            if (cashReceive != null)
            {
                lstCashReceive.Add(cashReceive);
            }
            else
            {
                MessageBoxHelper.ShowInformation("No data found.");
            }

            LoadDataInGrid(lstCashReceive);


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (grvCashReceiveInformation.Selected.Rows.Count > 0)
            {
                _cashReceiveID = Convert.ToInt32(grvCashReceiveInformation.Selected.Rows[0].Cells[0].Value);
                DialogResult mresult = MessageBoxHelper.ShowConfirmation("Do you want to cancel this order?");
                if (mresult == System.Windows.Forms.DialogResult.Yes)
                {
                    if (new CashReceiveManager().CancelCashReceiveInformation(_cashReceiveID) == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Canceled successfully.");
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to cancel information.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtEmployee_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmployee.Text))
            {
                List<CashReceive> lstFilteredSalesOrder = new List<CashReceive>();
                lstFilteredSalesOrder = lstCashReceive.Where(s => s.CreatedBy.Trim().ToUpper().StartsWith(txtEmployee.Text.ToUpper().Trim())).ToList();
                LoadDataInGrid(lstFilteredSalesOrder);
            }
            else
            {
                LoadDataInGrid(lstCashReceive);
            }
        }

        private void btSearchByCustomer_Click(object sender, EventArgs e)
        {
            if (cmbCustomerName.Value != null)
            {
                string filter = string.Format("{0}='{1}'", "CustomerID", Convert.ToInt32(cmbCustomerName.Value));

                lstCashReceive = new CashReceiveManager().GetFilteredCashReceive(filter).ToList();
                // lstCashReceive = new List<CashReceive>();
                if (lstCashReceive.Count == 0)
                {
                    MessageBoxHelper.ShowInformation("No data found.");
                }

                LoadDataInGrid(lstCashReceive);
            }

        }
    }
}
