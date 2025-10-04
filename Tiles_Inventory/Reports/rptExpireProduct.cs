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
    /// Summary description for rptExpireProduct.
    /// </summary>
    public partial class rptExpireProduct : DataDynamics.ActiveReports.ActiveReport
    {

        public rptExpireProduct()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {

        }

        private void detail_Format(object sender, EventArgs e)
        {
            DateTime expireDate = Convert.ToDateTime(txtExpireDate.Text);
            string date = DateTime.Now.ToShortDateString();
            DateTime currentDate =Convert.ToDateTime (date);

            if (currentDate >= expireDate)
            {
                txtExpireDate.BackColor = Color.Red;
            }
            else if (currentDate <= expireDate.AddDays(-60))
            {
                txtExpireDate.BackColor = Color.Yellow;
            }
            else if (currentDate <= expireDate.AddDays(-30))
            {
                txtExpireDate.BackColor = Color.DarkOrange;
            }

        }
    }
}
