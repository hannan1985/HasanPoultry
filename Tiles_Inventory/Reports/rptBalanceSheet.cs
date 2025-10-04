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
    /// Summary description for BalanceSheet.
    /// </summary>
    public partial class rptBalanceSheet : DataDynamics.ActiveReports.ActiveReport
    {
        public rptBalanceSheet()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
        //    DataTable Drtable = new DataTable();
        //    SqlDataAdapter adapter;
        //    int profitOrLoss = 0;
        //    int rowcount = 0;
            
        //    con.Open();
        //    cmd = new SqlCommand("Exec BalanceSheet", con);
        //    adapter = new SqlDataAdapter(cmd);
        //    adapter.Fill(Drtable);
        //    rowcount = Drtable.Rows.Count;

        //    cmd = new SqlCommand("Exec GetProfitOrLoss", con);
        //    dr = cmd.ExecuteReader();
        //    if (dr.Read())
        //    {
        //        profitOrLoss = Convert.ToInt32(dr[0]);
        //    }

        //    if (counter + 1 == rowcount)
        //    {
        //        lblProfitorLoss.Text = "Profit/Loss";
        //        txtNetProfit.Text = profitOrLoss.ToString();
        //    }
        //    dr.Close();
        //    counter++;
        //    con.Close() ;
        }
        }
}
