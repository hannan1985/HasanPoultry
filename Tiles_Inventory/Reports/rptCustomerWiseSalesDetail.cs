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
    /// Summary description for rptSalesDetail.
    /// </summary>
    public partial class rptCustomerWiseSalesDetail : DataDynamics.ActiveReports.ActiveReport
    {
        private decimal totalCarringAndLoading = 0;
        private decimal totalSalesTotal = 0;

        public rptCustomerWiseSalesDetail()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void groupFooter2_Format(object sender, EventArgs e)
        {
            decimal carringAndLoading = 0;
            decimal salesTotal = 0;
            decimal.TryParse(txtCarrignAndLoading.Text, out carringAndLoading);
            decimal.TryParse(txtSalesTotal.Text, out salesTotal);

            txtSalesTotal.Text = (carringAndLoading + salesTotal).ToString();

            totalCarringAndLoading = totalCarringAndLoading + carringAndLoading;
            totalSalesTotal = totalSalesTotal + salesTotal;

        }

        private void groupFooter1_Format(object sender, EventArgs e)
        {
            txtDatetotal.Text = (totalCarringAndLoading + totalSalesTotal).ToString();
        }

        private void pageFooter_Format(object sender, EventArgs e)
        {

        }

        private void reportFooter1_Format(object sender, EventArgs e)
        {
            txtGrandTotal.Text = (totalCarringAndLoading + totalSalesTotal).ToString();
        }
    }
}
