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
    /// Summary description for rptPartyOutstanding.
    /// </summary>
    public partial class rptPartyOutstanding : DataDynamics.ActiveReports.ActiveReport
    {

        public rptPartyOutstanding()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            decimal receieAmount = 0;
            decimal.TryParse(txtReceiveAmount.Text, out receieAmount);
            if (receieAmount == 0)
            {
                txtReceiveAmount.Visible = false;
                txtReceiveDate.Visible = false;
                txtReceiveNo.Visible = false;
            }
        }
    }
}
