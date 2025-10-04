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
    /// Summary description for rptBarcodeForLabel.
    /// </summary>
    public partial class rptBarcodeForLabel : DataDynamics.ActiveReports.ActiveReport
    {
        Dictionary<string, Image> _barcodeDic = new Dictionary<string, Image>();
        List<Image> lstBarcodeImage = new List<Image>();
        int count = 0;
        public rptBarcodeForLabel()
        {
            //
            // Required for Windows Form Designer support
            //
            //Dictionary<string, Image> barcodeDic
            //_barcodeDic = barcodeDic;
            //foreach (string productName in _barcodeDic.Keys)
            //{
            //    lstBarcodeImage.Add(_barcodeDic[productName]);
            //}
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            //try
            //{
            //    if (lstBarcodeImage[count] != null)
            //    {
            //        pbBarcode1.Image = lstBarcodeImage[count];
            //    }

            //    if (lstBarcodeImage[count + 1] != null)
            //    {
            //        pbBarcode2.Image = lstBarcodeImage[count + 1];
            //    }            

            //    count = count + 2;

            //}
            //catch (Exception)
            //{ 
            //}
        }
    }
}
