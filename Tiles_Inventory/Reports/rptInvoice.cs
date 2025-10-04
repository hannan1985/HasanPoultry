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
    /// Summary description for rptInvoice.
    /// </summary>
    public partial class rptInvoice : DataDynamics.ActiveReports.ActiveReport
    {

        public rptInvoice()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
            //txtDate.Text = DateTime.Now.ToShortDateString();
            //txtTime.Text = DateTime.Now.ToShortTimeString();
        }
    }
}
