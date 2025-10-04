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
using Tiles_Inventory.Reports;
using NybSys.MTBF.Utility.Helper;

namespace Tiles_Inventory
{
    public partial class frmVeiwSalesPartyHistory : BaseForm
    {
        private string pharmacyName;
        private string pharmacyAddress;

        public frmVeiwSalesPartyHistory()
        {
            InitializeComponent();
        }

        private void frmVeiwSalesPartyHistory_Load(object sender, EventArgs e)
        {
            LoadSalesPartyCombo();
            rptViewer.Dock = DockStyle.Fill;
        }

        private void LoadSalesPartyCombo()
        {
            DataTable materialsTable = new DataTable();
            materialsTable.Columns.Add("PartyID");
            materialsTable.Columns.Add("Party Name");

            DataRow emptyrow = materialsTable.NewRow();
            emptyrow["PartyID"] = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            emptyrow["Party Name"] = MTBFConstants.DataField.COMBO_DEFAULT_NAME;
            materialsTable.Rows.Add(emptyrow);

            foreach (SalesParty material in new StockSalesManager().GetAllSalesParty())
            {
                DataRow row = materialsTable.NewRow();
                row["PartyID"] = material.SalesPartyID;
                row["Party Name"] = material.PartyName;
                materialsTable.Rows.Add(row);
            }
            cmbPartyName.DataSource = materialsTable;
            cmbPartyName.DisplayMember = "Party Name";
            cmbPartyName.ValueMember = "PartyID";
            cmbPartyName.Value = MTBFConstants.DataField.COMBO_DEFAULT_ID;
        }


        private DataTable BuildCustomerOutstandingTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("CustomerID");
            table.Columns.Add("SalesID");
            table.Columns.Add("Sales Date");
            table.Columns.Add("Due Amount");
            table.Columns.Add("Receive Number");
            table.Columns.Add("Receive Date");
            table.Columns.Add("Receive Amount");
            table.Columns.Add("Actual Due");
            return table;
        }

        private DataTable LoadCustomerOutstandingReport(List<StockSales> lstSalesOrder, List<PartyReceive> lstCashReceive)
        {
            DataTable customerOutstandingTable = BuildCustomerOutstandingTable();

            if (lstSalesOrder.Count >= lstCashReceive.Count)
            {
                foreach (StockSales salesOrder in lstSalesOrder)
                {
                    DataRow row = customerOutstandingTable.NewRow();

                    row["CustomerID"] = salesOrder.PartyID;
                    row["SalesID"] = salesOrder.StockSalesID;
                    row["Sales Date"] = salesOrder.SalesDate.ToShortDateString();

                    row["Due Amount"] = (salesOrder.Total - (salesOrder.ReceiveAmount));
                    row["Receive Number"] = 0;
                    row["Receive Date"] = 0;
                    row["Receive Amount"] = 0;
                    row["Actual Due"] = 0;
                    customerOutstandingTable.Rows.Add(row);
                }
            }
            else if (lstCashReceive.Count >= lstSalesOrder.Count)
            {
                foreach (PartyReceive cashReceive in lstCashReceive)
                {
                    DataRow row = customerOutstandingTable.NewRow();
                    row["CustomerID"] = cashReceive.PartyID;
                    row["SalesID"] = 0;
                    row["Sales Date"] = 0;
                    row["Due Amount"] = 0;
                    row["Receive Number"] = cashReceive.CashReceiveID;
                    row["Receive Date"] = cashReceive.ReceiveDate;
                    row["Receive Amount"] = cashReceive.Amount + cashReceive.Discount;
                    row["Actual Due"] = 0;
                    customerOutstandingTable.Rows.Add(row);
                }
            }
            return customerOutstandingTable;
        }

        /// <summary>
        /// Method to print customer outstanding report.
        /// </summary>
        private void CustomerOutStandingReport()
        {
            int partyID = 0;
            int.TryParse(cmbPartyName.Value.ToString(), out partyID);
            List<StockSales> lstSalesOrder = new List<StockSales>();
            string filter = string.Format("{0}={1}", "PartyID", partyID);

            lstSalesOrder = new StockSalesManager().GetFilteredStockSales(filter).Cast<StockSales>().ToList();
            List<PartyReceive> lstCashReceive = new List<PartyReceive>();

            lstCashReceive = new PartyReceiveManager().GetFilteredPartyReceive(filter).Cast<PartyReceive>().ToList();

            DataTable customerOutstandingTable = LoadCustomerOutstandingReport(lstSalesOrder, lstCashReceive);



            if (lstSalesOrder.Count >= lstCashReceive.Count)
            {

                for (int i = 0; i <= customerOutstandingTable.Rows.Count - 1; i++)
                {
                    if (i < lstCashReceive.Count)
                    {
                        PartyReceive cashReceive = lstCashReceive[i];
                        customerOutstandingTable.Rows[i]["Receive Number"] = cashReceive.CashReceiveID;
                        customerOutstandingTable.Rows[i]["Receive Date"] = cashReceive.ReceiveDate.ToShortDateString();
                        customerOutstandingTable.Rows[i]["Receive Amount"] = cashReceive.Amount + cashReceive.Discount;
                    }
                }

            }
            else if (lstCashReceive.Count >= lstSalesOrder.Count)
            {
                for (int i = 0; i <= customerOutstandingTable.Rows.Count - 1; i++)
                {
                    StockSales salesOrder = lstSalesOrder[i];
                    customerOutstandingTable.Rows[i]["CustomerID"] = salesOrder.PartyID;
                    customerOutstandingTable.Rows[i]["SalesID"] = salesOrder.StockSalesID;
                    customerOutstandingTable.Rows[i]["Sales Date"] = salesOrder.SalesDate.ToShortDateString();
                    customerOutstandingTable.Rows[i]["Due Amount"] = salesOrder.Total - (salesOrder.ReceiveAmount);
                }
            }

            if (customerOutstandingTable.Rows.Count > 0)
            {
                rptPartyOutstanding op = new rptPartyOutstanding();
                op.DataSource = customerOutstandingTable;
                rptViewer.Document = op.Document;
                rptViewer.Dock = DockStyle.Fill;
                GetPharmachyInforamation(ref pharmacyName, ref pharmacyAddress);
                op.txtPharmacyName.Text = pharmacyName;
                op.txtAddress.Text = pharmacyAddress;
                op.txtActualDue.Text = CalcualteAcutalDueAmount(customerOutstandingTable).ToString();
                SalesParty customer = new StockSalesManager().GetSalesPartyByID(partyID);
                if (customer != null)
                {
                    op.txtCustomerName.Text = customer.PartyName;
                    op.txtCustomerAddress.Text = customer.Address;
                    op.txtPhone.Text = customer.Phone;
                }

                op.Run();

            }
            else
            {
                MessageBox.Show("No available customer out standing report.", "Inventory Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void GetPharmachyInforamation(ref string PharmacyName, ref string Address)
        {
            Branch branch = new BranchManager().GetBranchByID(MTBFConstants.AppConstants.BranchID);
            if (branch != null)
            {
                PharmacyName = branch.BranchName;
                Address = branch.Address + ", " + branch.Phone + ", " + branch.Fax;
            }
        }

        private decimal CalcualteAcutalDueAmount(DataTable table)
        {
            decimal actualDueAmount = 0;
            decimal receiveAmount = 0;
            decimal dueAmount = 0;
            foreach (DataRow row in table.Rows)
            {
                receiveAmount = receiveAmount + Convert.ToDecimal(row["Receive Amount"]);
                dueAmount = dueAmount + Convert.ToDecimal(row["Due Amount"]);

            }

            actualDueAmount = dueAmount - receiveAmount;
            return actualDueAmount;
        }

        private void btView_Click(object sender, EventArgs e)
        {
            if (cmbPartyName.Value != null)
            {
                CustomerOutStandingReport();
            }
            else
            {
                MessageBoxHelper.ShowInformation("You need to select party name.");
                cmbPartyName.Focus();
            }


        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }




    }
}
