using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;

namespace Tiles_Inventory.Reports
{
    /// <summary>
    /// Summary description for rptTrialBalance.
    /// </summary>
    public partial class rptTrialBalance : DataDynamics.ActiveReports.ActiveReport
    {
        private SqlConnection con =
         new SqlConnection("Data Source=LINWIN-PC\\SQLEXPRESS;Initial Catalog=AccountingDB;Integrated Security=True");

        private SqlCommand cmd = null;


        public rptTrialBalance()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

 

       
    }
}
