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
    /// Summary description for rptPurchaseReport.
    /// </summary>
    public partial class rptPurchaseReport : DataDynamics.ActiveReports.ActiveReport
    {
        int count = 1;

        public rptPurchaseReport()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            lblSerialNo.Text = count.ToString ();
            count++;
        }
    }
}
