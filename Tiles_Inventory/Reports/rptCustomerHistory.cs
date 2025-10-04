using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;

namespace Tiles_Inventory.Reports
{
    /// <summary>
    /// Summary description for rptCustomerDue.
    /// </summary>
    public partial class rptCustomerHistory : DataDynamics.ActiveReports.ActiveReport
    {

        public rptCustomerHistory()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            //decimal totalDue = 0;
            //decimal totalReceive = 0;
            //decimal.TryParse(txtTotalDue.Text, out totalDue);
            //decimal.TryParse(txtTotalReceive.Text, out totalReceive);
            //txtActualDue.Text = (totalDue - totalReceive).ToString();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            //decimal receieAmount = 0;
            //decimal salesAmount = 0;
            //decimal.TryParse(txtReceiveAmount.Text, out receieAmount);
            //decimal.TryParse(txtSalesAmount.Text, out salesAmount );
            //if (receieAmount == 0)
            //{
            //    txtReceiveAmount.Visible = false;
            //    txtReceiveDate.Visible = false;
            //    txtReceiveNo.Visible = false;
            //}
            //else
            //{
            //    txtReceiveAmount.Visible = true ;
            //    txtReceiveDate.Visible = true ;
            //    txtReceiveNo.Visible = true ;
            //}
            //if (salesAmount  == 0)
            //{
            //    txtSalesDate.Visible = false;
            //    txtSalesAmount.Visible = false;
            //    txtDueAmount.Visible = false;
            //}
            //else
            //{
            //    txtSalesDate.Visible = true  ;
            //    txtSalesAmount.Visible = true ;
            //    txtDueAmount.Visible = true ;
            //}

        }
    }
}
