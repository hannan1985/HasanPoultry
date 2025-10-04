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
    /// Summary description for rptChalan.
    /// </summary>
    public partial class rptChalan : DataDynamics.ActiveReports.ActiveReport
    {

        public rptChalan()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {
          //  txtSalesDate.Text = DateTime.Now.ToShortDateString();
            txtTime.Text = DateTime.Now.ToString();
        }
    }
}
