namespace Tiles_Inventory.Reports
{
    /// <summary>
    /// Summary description for rptExpireProduct.
    /// </summary>
    partial class rptExpireProduct
    {
        private DataDynamics.ActiveReports.PageHeader pageHeader;
        private DataDynamics.ActiveReports.Detail detail;
        private DataDynamics.ActiveReports.PageFooter pageFooter;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }

        #region ActiveReport Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(rptExpireProduct));
            this.pageHeader = new DataDynamics.ActiveReports.PageHeader();
            this.label1 = new DataDynamics.ActiveReports.Label();
            this.label2 = new DataDynamics.ActiveReports.Label();
            this.label3 = new DataDynamics.ActiveReports.Label();
            this.label4 = new DataDynamics.ActiveReports.Label();
            this.label5 = new DataDynamics.ActiveReports.Label();
            this.label6 = new DataDynamics.ActiveReports.Label();
            this.label7 = new DataDynamics.ActiveReports.Label();
            this.TextBox7 = new DataDynamics.ActiveReports.TextBox();
            this.txtPharmacyName = new DataDynamics.ActiveReports.TextBox();
            this.txtAddress = new DataDynamics.ActiveReports.TextBox();
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.textBox2 = new DataDynamics.ActiveReports.TextBox();
            this.textBox3 = new DataDynamics.ActiveReports.TextBox();
            this.textBox4 = new DataDynamics.ActiveReports.TextBox();
            this.txtExpireDate = new DataDynamics.ActiveReports.TextBox();
            this.textBox6 = new DataDynamics.ActiveReports.TextBox();
            this.textBox5 = new DataDynamics.ActiveReports.TextBox();
            this.pageFooter = new DataDynamics.ActiveReports.PageFooter();
            this.textBox1 = new DataDynamics.ActiveReports.TextBox();
            this.groupHeader1 = new DataDynamics.ActiveReports.GroupHeader();
            this.groupFooter1 = new DataDynamics.ActiveReports.GroupFooter();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPharmacyName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExpireDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.label1,
            this.label2,
            this.label3,
            this.label4,
            this.label5,
            this.label6,
            this.label7,
            this.TextBox7,
            this.txtPharmacyName,
            this.txtAddress});
            this.pageHeader.Height = 1.333333F;
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Format += new System.EventHandler(this.pageHeader_Format);
            // 
            // label1
            // 
            this.label1.Height = 0.1499999F;
            this.label1.HyperLink = null;
            this.label1.Left = 0.082F;
            this.label1.Name = "label1";
            this.label1.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: normal; ddo-char-set: 0";
            this.label1.Text = "Company Name";
            this.label1.Top = 1.143F;
            this.label1.Width = 1.02F;
            // 
            // label2
            // 
            this.label2.Height = 0.1499999F;
            this.label2.HyperLink = null;
            this.label2.Left = 2.09F;
            this.label2.Name = "label2";
            this.label2.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: normal; ddo-char-set: 0";
            this.label2.Text = "Quantity";
            this.label2.Top = 1.143F;
            this.label2.Width = 0.5992001F;
            // 
            // label3
            // 
            this.label3.Height = 0.1499999F;
            this.label3.HyperLink = null;
            this.label3.Left = 2.729F;
            this.label3.Name = "label3";
            this.label3.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: normal; ddo-char-set: 0";
            this.label3.Text = "Price";
            this.label3.Top = 1.143F;
            this.label3.Width = 0.55F;
            // 
            // label4
            // 
            this.label4.Height = 0.1499999F;
            this.label4.HyperLink = null;
            this.label4.Left = 3.297F;
            this.label4.Name = "label4";
            this.label4.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: normal; ddo-char-set: 0";
            this.label4.Text = "Product Name";
            this.label4.Top = 1.143F;
            this.label4.Width = 1.680001F;
            // 
            // label5
            // 
            this.label5.Height = 0.1499999F;
            this.label5.HyperLink = null;
            this.label5.Left = 5.007017F;
            this.label5.Name = "label5";
            this.label5.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: normal; ddo-char-set: 0";
            this.label5.Text = "Expire Date";
            this.label5.Top = 1.143F;
            this.label5.Width = 1F;
            // 
            // label6
            // 
            this.label6.Height = 0.1499999F;
            this.label6.HyperLink = null;
            this.label6.Left = 6.035816F;
            this.label6.Name = "label6";
            this.label6.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: normal; ddo-char-set: 0";
            this.label6.Text = "Total";
            this.label6.Top = 1.143F;
            this.label6.Width = 0.6791968F;
            // 
            // label7
            // 
            this.label7.Height = 0.1499999F;
            this.label7.HyperLink = null;
            this.label7.Left = 1.401F;
            this.label7.Name = "label7";
            this.label7.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: normal; ddo-char-set: 0";
            this.label7.Text = "PID";
            this.label7.Top = 1.143F;
            this.label7.Width = 0.5089999F;
            // 
            // TextBox7
            // 
            this.TextBox7.Height = 0.2F;
            this.TextBox7.Left = 1.985792F;
            this.TextBox7.Name = "TextBox7";
            this.TextBox7.Style = "font-family: Tahoma; font-size: 12pt; font-weight: normal; text-align: center; dd" +
                "o-char-set: 0";
            this.TextBox7.Text = "Expire Product Information";
            this.TextBox7.Top = 0F;
            this.TextBox7.Width = 2.84F;
            // 
            // txtPharmacyName
            // 
            this.txtPharmacyName.Height = 0.2F;
            this.txtPharmacyName.Left = 1.98625F;
            this.txtPharmacyName.Name = "txtPharmacyName";
            this.txtPharmacyName.Style = "font-family: Tahoma; font-size: 11.25pt; text-align: center";
            this.txtPharmacyName.Text = null;
            this.txtPharmacyName.Top = 0.24F;
            this.txtPharmacyName.Width = 2.84F;
            // 
            // txtAddress
            // 
            this.txtAddress.Height = 0.2F;
            this.txtAddress.Left = 1.480479F;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Style = "font-family: Tahoma; font-size: 9.75pt; text-align: center";
            this.txtAddress.Text = null;
            this.txtAddress.Top = 0.464F;
            this.txtAddress.Width = 3.850625F;
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.textBox2,
            this.textBox3,
            this.textBox4,
            this.txtExpireDate,
            this.textBox6,
            this.textBox5});
            this.detail.Height = 0.2083333F;
            this.detail.Name = "detail";
            this.detail.Format += new System.EventHandler(this.detail_Format);
            // 
            // textBox2
            // 
            this.textBox2.DataField = "Quantity";
            this.textBox2.Height = 0.1499999F;
            this.textBox2.Left = 2.0612F;
            this.textBox2.Name = "textBox2";
            this.textBox2.Style = "font-family: Tahoma; font-size: 8.25pt; ddo-char-set: 0";
            this.textBox2.Text = null;
            this.textBox2.Top = 0.02897924F;
            this.textBox2.Width = 0.5992001F;
            // 
            // textBox3
            // 
            this.textBox3.DataField = "PurchasePrice";
            this.textBox3.Height = 0.1499999F;
            this.textBox3.Left = 2.700399F;
            this.textBox3.Name = "textBox3";
            this.textBox3.Style = "font-family: Tahoma; font-size: 8.25pt; ddo-char-set: 0";
            this.textBox3.Text = null;
            this.textBox3.Top = 0.02897924F;
            this.textBox3.Width = 0.55F;
            // 
            // textBox4
            // 
            this.textBox4.DataField = "ProductName";
            this.textBox4.Height = 0.1499999F;
            this.textBox4.Left = 3.267601F;
            this.textBox4.Name = "textBox4";
            this.textBox4.Style = "font-family: Tahoma; font-size: 8.25pt; ddo-char-set: 0";
            this.textBox4.Text = null;
            this.textBox4.Top = 0.02897918F;
            this.textBox4.Width = 1.680001F;
            // 
            // txtExpireDate
            // 
            this.txtExpireDate.DataField = "ExpireDate";
            this.txtExpireDate.Height = 0.1499999F;
            this.txtExpireDate.Left = 4.977818F;
            this.txtExpireDate.Name = "txtExpireDate";
            this.txtExpireDate.OutputFormat = resources.GetString("txtExpireDate.OutputFormat");
            this.txtExpireDate.Style = "font-family: Tahoma; font-size: 8.25pt; ddo-char-set: 0";
            this.txtExpireDate.Text = null;
            this.txtExpireDate.Top = 0.02897918F;
            this.txtExpireDate.Width = 1F;
            // 
            // textBox6
            // 
            this.textBox6.DataField = "Total";
            this.textBox6.Height = 0.1499999F;
            this.textBox6.Left = 6.006817F;
            this.textBox6.Name = "textBox6";
            this.textBox6.OutputFormat = resources.GetString("textBox6.OutputFormat");
            this.textBox6.Style = "font-family: Tahoma; font-size: 8.25pt; ddo-char-set: 0";
            this.textBox6.Text = null;
            this.textBox6.Top = 0.02897918F;
            this.textBox6.Width = 0.6791968F;
            // 
            // textBox5
            // 
            this.textBox5.DataField = "PurchaseID";
            this.textBox5.Height = 0.1499999F;
            this.textBox5.Left = 1.372F;
            this.textBox5.Name = "textBox5";
            this.textBox5.Style = "font-family: Tahoma; font-size: 8.25pt; ddo-char-set: 0";
            this.textBox5.Text = null;
            this.textBox5.Top = 0.02935424F;
            this.textBox5.Width = 0.5089999F;
            // 
            // pageFooter
            // 
            this.pageFooter.Height = 0F;
            this.pageFooter.Name = "pageFooter";
            // 
            // textBox1
            // 
            this.textBox1.DataField = "CompanyName";
            this.textBox1.Height = 0.1499999F;
            this.textBox1.Left = 0.053F;
            this.textBox1.Name = "textBox1";
            this.textBox1.Style = "font-family: Tahoma; font-size: 8.25pt; ddo-char-set: 0";
            this.textBox1.Text = null;
            this.textBox1.Top = 0.02395837F;
            this.textBox1.Width = 1.25F;
            // 
            // groupHeader1
            // 
            this.groupHeader1.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.textBox1});
            this.groupHeader1.DataField = "CompanyName";
            this.groupHeader1.Height = 0.1979167F;
            this.groupHeader1.Name = "groupHeader1";
            // 
            // groupFooter1
            // 
            this.groupFooter1.Height = 0.01041667F;
            this.groupFooter1.Name = "groupFooter1";
            // 
            // rptExpireProduct
            // 
            this.MasterReport = false;
            this.PageSettings.DefaultPaperSize = false;
            this.PageSettings.Margins.Left = 0.5F;
            this.PageSettings.Margins.Right = 0.5F;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.PrintWidth = 6.811584F;
            this.Sections.Add(this.pageHeader);
            this.Sections.Add(this.groupHeader1);
            this.Sections.Add(this.detail);
            this.Sections.Add(this.groupFooter1);
            this.Sections.Add(this.pageFooter);
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" +
                        "l; font-size: 10pt; color: Black", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold", "Heading1", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" +
                        "lic", "Heading2", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold", "Heading3", "Normal"));
            ((System.ComponentModel.ISupportInitialize)(this.label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPharmacyName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExpireDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.Label label2;
        private DataDynamics.ActiveReports.Label label3;
        private DataDynamics.ActiveReports.Label label4;
        private DataDynamics.ActiveReports.Label label5;
        private DataDynamics.ActiveReports.Label label6;
        private DataDynamics.ActiveReports.TextBox textBox2;
        private DataDynamics.ActiveReports.TextBox textBox3;
        private DataDynamics.ActiveReports.TextBox textBox4;
        private DataDynamics.ActiveReports.TextBox txtExpireDate;
        private DataDynamics.ActiveReports.TextBox textBox6;
        private DataDynamics.ActiveReports.Label label1;
        private DataDynamics.ActiveReports.TextBox textBox1;
        private DataDynamics.ActiveReports.GroupHeader groupHeader1;
        private DataDynamics.ActiveReports.GroupFooter groupFooter1;
        private DataDynamics.ActiveReports.Label label7;
        private DataDynamics.ActiveReports.TextBox textBox5;
        private DataDynamics.ActiveReports.TextBox TextBox7;
        public DataDynamics.ActiveReports.TextBox txtPharmacyName;
        public DataDynamics.ActiveReports.TextBox txtAddress;
    }
}
