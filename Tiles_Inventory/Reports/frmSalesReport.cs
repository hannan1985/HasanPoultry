using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IFMS.Facade;
using IMFS.Common.View;

namespace Tiles_Inventory.Reports
{
    public partial class frmSalesReport : Form
    {
        public frmSalesReport()
        {
            InitializeComponent();

        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                string location = string.Empty;
                FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                DialogResult result = folderBrowser.ShowDialog();
                location = folderBrowser.SelectedPath;
                DataDynamics.ActiveReports.Export.Pdf.PdfExport pdf = new DataDynamics.ActiveReports.Export.Pdf.PdfExport();
                pdf.Export(this.rptViewer.Document, location + "\\" + "Sales Invoice" + ".pdf");
                MessageBox.Show("Downloaded Successfully.");
            }
            catch (Exception)
            {
                MessageBox.Show("Download Failed.");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
