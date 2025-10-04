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
    /// Summary description for rptBarcode.
    /// </summary>
    public partial class rptBarcode : DataDynamics.ActiveReports.ActiveReport
    {
        Dictionary<string, Image> _barcodeDic = new Dictionary<string, Image>();
        List<Image> lstBarcodeImage = new List<Image>();

        public rptBarcode()
        {

            InitializeComponent();
        }
        public rptBarcode(Dictionary<string, Image> barcodeDic)
        {
            //
            // Required for Windows Form Designer support
            //
            _barcodeDic = barcodeDic;
            foreach (string productName in _barcodeDic.Keys)
            {
                lstBarcodeImage.Add(_barcodeDic[productName]);
            }

            InitializeComponent();
        }
        int count = 0;
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

            //    if (lstBarcodeImage[count + 2] != null)
            //    {

            //        pbBarcode3.Image = lstBarcodeImage[count + 2];
            //    }

            //    if (lstBarcodeImage[count + 3] != null)
            //    {

            //        pbBarcode4.Image = lstBarcodeImage[count + 3];
            //    }

            //    if (lstBarcodeImage[count + 4] != null)
            //    {
            //        pbBarcode5.Image = lstBarcodeImage[count + 4];
            //    }

            //    count = count + 5;             

            //}
            //catch (Exception)
            //{
            //    //if (string.IsNullOrEmpty(txtProductName1.Text))
            //    //{
            //    //    pbBarcode1.Visible = false;
            //    //}
            //    //if (string.IsNullOrEmpty(txtProductName2.Text))
            //    //{
            //    //    pbBarcode2.Image = null; ;
            //    //}
            //    //if (string.IsNullOrEmpty(txtProductName3.Text))
            //    //{
            //    //    pbBarcode3.Image = null;
            //    //}
            //    //if (string.IsNullOrEmpty(txtProductName4.Text))
            //    //{
            //    //    pbBarcode4.Image = null;
            //    //}
            //    //if (string.IsNullOrEmpty(txtProductName5.Text))
            //    //{
            //    //    pbBarcode5.Image = null; ;
            //    //}

            //}
        }
    }
}
