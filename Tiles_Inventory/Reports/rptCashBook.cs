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
    /// Summary description for rptCashBook.
    /// </summary>
    public partial class rptCashBook : DataDynamics.ActiveReports.ActiveReport
    {

        public rptCashBook()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            decimal exAmount = 0;
            decimal eAmount = 0;

            decimal.TryParse(txtExpenseAmount.Text, out exAmount);

            decimal.TryParse(txtReceiveAmount.Text, out eAmount);

            //if (exAmount == 0)
            //{
            //    txtExpenseAmount.Visible = false;
            //}

            //if (eAmount  == 0)
            //{
            //    txtReceiveAmount.Visible = false;
            //}

        }
    }
}
