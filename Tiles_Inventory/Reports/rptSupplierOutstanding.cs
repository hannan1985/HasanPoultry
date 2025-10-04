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
    /// Summary description for rptSupplierOutstanding.
    /// </summary>
    public partial class rptSupplierOutstanding : DataDynamics.ActiveReports.ActiveReport
    {

        public rptSupplierOutstanding()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            //decimal paymentAmount = 0;
            //decimal.TryParse(txtPaymentAmount.Text, out paymentAmount);
            //if (paymentAmount == 0)
            //{
            //    txtPaymentDate.Visible = false;
            //}
        }
    }
}
