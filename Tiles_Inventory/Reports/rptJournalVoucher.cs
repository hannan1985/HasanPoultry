using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;

namespace Tiles_Inventory
{
    /// <summary>
    /// Summary description for AllLedger.
    /// </summary>
    public partial class rptJournalVoucher : DataDynamics.ActiveReports.ActiveReport
    {

        public rptJournalVoucher()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {

        }
    }
}
