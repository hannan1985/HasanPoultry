namespace Tiles_Inventory.Reports
{
    /// <summary>
    /// Summary description for rptSalesDetail.
    /// </summary>
    partial class rptZoneWiseSalesDetail
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(rptZoneWiseSalesDetail));
            this.pageHeader = new DataDynamics.ActiveReports.PageHeader();
            this.TextBox7 = new DataDynamics.ActiveReports.TextBox();
            this.txtPharmacyName = new DataDynamics.ActiveReports.TextBox();
            this.txtAddress = new DataDynamics.ActiveReports.TextBox();
            this.ReportInfo1 = new DataDynamics.ActiveReports.ReportInfo();
            this.Label1 = new DataDynamics.ActiveReports.Label();
            this.Label3 = new DataDynamics.ActiveReports.Label();
            this.Label4 = new DataDynamics.ActiveReports.Label();
            this.label2 = new DataDynamics.ActiveReports.Label();
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.TextBox3 = new DataDynamics.ActiveReports.TextBox();
            this.TextBox4 = new DataDynamics.ActiveReports.TextBox();
            this.textBox2 = new DataDynamics.ActiveReports.TextBox();
            this.pageFooter = new DataDynamics.ActiveReports.PageFooter();
            this.Label8 = new DataDynamics.ActiveReports.Label();
            this.TextBox9 = new DataDynamics.ActiveReports.TextBox();
            this.groupHeader1 = new DataDynamics.ActiveReports.GroupHeader();
            this.TextBox1 = new DataDynamics.ActiveReports.TextBox();
            this.groupFooter1 = new DataDynamics.ActiveReports.GroupFooter();
            this.Line1 = new DataDynamics.ActiveReports.Line();
            this.textBox8 = new DataDynamics.ActiveReports.TextBox();
            this.label7 = new DataDynamics.ActiveReports.Label();
            this.label5 = new DataDynamics.ActiveReports.Label();
            this.textBox5 = new DataDynamics.ActiveReports.TextBox();
            this.textBox6 = new DataDynamics.ActiveReports.TextBox();
            this.textBox10 = new DataDynamics.ActiveReports.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPharmacyName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportInfo1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.TextBox7,
            this.txtPharmacyName,
            this.txtAddress,
            this.ReportInfo1,
            this.Label1,
            this.Label3,
            this.Label4,
            this.label2,
            this.label5});
            this.pageHeader.Height = 1.208F;
            this.pageHeader.Name = "pageHeader";
            // 
            // TextBox7
            // 
            this.TextBox7.Height = 0.2F;
            this.TextBox7.Left = 1.83F;
            this.TextBox7.Name = "TextBox7";
            this.TextBox7.Style = "font-family: Tahoma; font-size: 12pt; font-weight: normal; text-align: center; dd" +
    "o-char-set: 0";
            this.TextBox7.Text = "Sales Information";
            this.TextBox7.Top = 0.19F;
            this.TextBox7.Width = 2.84F;
            // 
            // txtPharmacyName
            // 
            this.txtPharmacyName.Height = 0.2F;
            this.txtPharmacyName.Left = 1.829687F;
            this.txtPharmacyName.Name = "txtPharmacyName";
            this.txtPharmacyName.Style = "font-family: Tahoma; font-size: 11.25pt; text-align: center";
            this.txtPharmacyName.Text = null;
            this.txtPharmacyName.Top = 0.3899998F;
            this.txtPharmacyName.Width = 2.84F;
            // 
            // txtAddress
            // 
            this.txtAddress.Height = 0.2F;
            this.txtAddress.Left = 1.269186F;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Style = "font-family: Tahoma; font-size: 9.75pt; text-align: center";
            this.txtAddress.Text = null;
            this.txtAddress.Top = 0.6539999F;
            this.txtAddress.Width = 3.961313F;
            // 
            // ReportInfo1
            // 
            this.ReportInfo1.FormatString = "{RunDateTime:dd-MM-yyyy h:mm tt}";
            this.ReportInfo1.Height = 0.15625F;
            this.ReportInfo1.Left = 4.683668F;
            this.ReportInfo1.Name = "ReportInfo1";
            this.ReportInfo1.Style = "font-family: Tahoma; font-size: 8.25pt; text-align: right; ddo-char-set: 0";
            this.ReportInfo1.Top = 0F;
            this.ReportInfo1.Width = 1.392F;
            // 
            // Label1
            // 
            this.Label1.Height = 0.1500001F;
            this.Label1.HyperLink = null;
            this.Label1.Left = 0.103F;
            this.Label1.Name = "Label1";
            this.Label1.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: bold; ddo-char-set: 0";
            this.Label1.Text = "Zone Name";
            this.Label1.Top = 1.027F;
            this.Label1.Width = 1.146F;
            // 
            // Label3
            // 
            this.Label3.Height = 0.1500001F;
            this.Label3.HyperLink = null;
            this.Label3.Left = 1.278F;
            this.Label3.Name = "Label3";
            this.Label3.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: bold; ddo-char-set: 0";
            this.Label3.Text = "Customer Name";
            this.Label3.Top = 1.027F;
            this.Label3.Width = 1.733F;
            // 
            // Label4
            // 
            this.Label4.Height = 0.1500001F;
            this.Label4.HyperLink = null;
            this.Label4.Left = 5.134F;
            this.Label4.Name = "Label4";
            this.Label4.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: bold; text-align: right; ddo" +
    "-char-set: 0";
            this.Label4.Text = "Sales Amount";
            this.Label4.Top = 1.027F;
            this.Label4.Width = 1.001F;
            // 
            // label2
            // 
            this.label2.Height = 0.1500001F;
            this.label2.HyperLink = null;
            this.label2.Left = 3.057F;
            this.label2.Name = "label2";
            this.label2.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: bold; ddo-char-set: 0";
            this.label2.Text = "Customer Address";
            this.label2.Top = 1.027F;
            this.label2.Width = 2.034F;
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.TextBox3,
            this.TextBox4,
            this.textBox2,
            this.textBox5});
            this.detail.Height = 0.1770833F;
            this.detail.Name = "detail";
            this.detail.Format += new System.EventHandler(this.detail_Format);
            // 
            // TextBox3
            // 
            this.TextBox3.DataField = "CustomerName";
            this.TextBox3.Height = 0.1500001F;
            this.TextBox3.Left = 1.284155F;
            this.TextBox3.Name = "TextBox3";
            this.TextBox3.Style = "font-family: Tahoma; font-size: 8.25pt; ddo-char-set: 0";
            this.TextBox3.Text = null;
            this.TextBox3.Top = 0.009000322F;
            this.TextBox3.Width = 1.733F;
            // 
            // TextBox4
            // 
            this.TextBox4.DataField = "SalesAmount";
            this.TextBox4.Height = 0.1500001F;
            this.TextBox4.Left = 5.138617F;
            this.TextBox4.Name = "TextBox4";
            this.TextBox4.OutputFormat = resources.GetString("TextBox4.OutputFormat");
            this.TextBox4.Style = "font-family: Tahoma; font-size: 8.25pt; text-align: right; ddo-char-set: 0";
            this.TextBox4.Text = null;
            this.TextBox4.Top = 0.009000322F;
            this.TextBox4.Width = 1.001F;
            // 
            // textBox2
            // 
            this.textBox2.DataField = "Address";
            this.textBox2.Height = 0.1500001F;
            this.textBox2.Left = 3.063155F;
            this.textBox2.Name = "textBox2";
            this.textBox2.Style = "font-family: Tahoma; font-size: 8.25pt; ddo-char-set: 0";
            this.textBox2.Text = null;
            this.textBox2.Top = 0.009000329F;
            this.textBox2.Width = 2.034F;
            // 
            // pageFooter
            // 
            this.pageFooter.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.Label8,
            this.TextBox9,
            this.textBox10});
            this.pageFooter.Height = 0.2083333F;
            this.pageFooter.Name = "pageFooter";
            // 
            // Label8
            // 
            this.Label8.Height = 0.1500001F;
            this.Label8.HyperLink = null;
            this.Label8.Left = 4.313F;
            this.Label8.Name = "Label8";
            this.Label8.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: bold; text-align: right; ddo" +
    "-char-set: 0";
            this.Label8.Text = "Grand Total";
            this.Label8.Top = 0.008F;
            this.Label8.Width = 0.7706674F;
            // 
            // TextBox9
            // 
            this.TextBox9.DataField = "SalesAmount";
            this.TextBox9.Height = 0.1500001F;
            this.TextBox9.Left = 5.138617F;
            this.TextBox9.Name = "TextBox9";
            this.TextBox9.OutputFormat = resources.GetString("TextBox9.OutputFormat");
            this.TextBox9.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: bold; text-align: right; ddo" +
    "-char-set: 0";
            this.TextBox9.SummaryRunning = DataDynamics.ActiveReports.SummaryRunning.All;
            this.TextBox9.SummaryType = DataDynamics.ActiveReports.SummaryType.GrandTotal;
            this.TextBox9.Text = null;
            this.TextBox9.Top = 0.008083302F;
            this.TextBox9.Width = 1.131F;
            // 
            // groupHeader1
            // 
            this.groupHeader1.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.TextBox1});
            this.groupHeader1.DataField = "ZoneName";
            this.groupHeader1.Height = 0.1750002F;
            this.groupHeader1.Name = "groupHeader1";
            // 
            // TextBox1
            // 
            this.TextBox1.DataField = "ZoneName";
            this.TextBox1.Height = 0.1500001F;
            this.TextBox1.Left = 0.1034996F;
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Style = "font-family: Tahoma; font-size: 8.25pt; ddo-char-set: 0";
            this.TextBox1.Text = null;
            this.TextBox1.Top = 0.02500012F;
            this.TextBox1.Width = 1.146F;
            // 
            // groupFooter1
            // 
            this.groupFooter1.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.Line1,
            this.textBox8,
            this.label7,
            this.textBox6});
            this.groupFooter1.Height = 0.2398334F;
            this.groupFooter1.Name = "groupFooter1";
            // 
            // Line1
            // 
            this.Line1.Height = 0.001249984F;
            this.Line1.Left = 0.3809171F;
            this.Line1.LineWeight = 1F;
            this.Line1.Name = "Line1";
            this.Line1.Top = 0.219F;
            this.Line1.Width = 6.766083F;
            this.Line1.X1 = 7.147F;
            this.Line1.X2 = 0.3809171F;
            this.Line1.Y1 = 0.219F;
            this.Line1.Y2 = 0.22025F;
            // 
            // textBox8
            // 
            this.textBox8.DataField = "SalesAmount";
            this.textBox8.Height = 0.1500001F;
            this.textBox8.Left = 5.138617F;
            this.textBox8.Name = "textBox8";
            this.textBox8.OutputFormat = resources.GetString("textBox8.OutputFormat");
            this.textBox8.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: bold; text-align: right; ddo" +
    "-char-set: 0";
            this.textBox8.SummaryGroup = "groupHeader1";
            this.textBox8.SummaryRunning = DataDynamics.ActiveReports.SummaryRunning.Group;
            this.textBox8.SummaryType = DataDynamics.ActiveReports.SummaryType.SubTotal;
            this.textBox8.Text = "textBox8";
            this.textBox8.Top = 0.0540833F;
            this.textBox8.Width = 1.001F;
            // 
            // label7
            // 
            this.label7.Height = 0.1500001F;
            this.label7.HyperLink = null;
            this.label7.Left = 4.313F;
            this.label7.Name = "label7";
            this.label7.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: bold; text-align: right; ddo" +
    "-char-set: 0";
            this.label7.Text = "Total";
            this.label7.Top = 0.054F;
            this.label7.Width = 0.7706674F;
            // 
            // label5
            // 
            this.label5.Height = 0.1500001F;
            this.label5.HyperLink = null;
            this.label5.Left = 6.196F;
            this.label5.Name = "label5";
            this.label5.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: bold; text-align: right; ddo" +
    "-char-set: 0";
            this.label5.Text = "Discount";
            this.label5.Top = 1.027F;
            this.label5.Width = 0.9510008F;
            // 
            // textBox5
            // 
            this.textBox5.DataField = "Discount";
            this.textBox5.Height = 0.1500001F;
            this.textBox5.Left = 6.200617F;
            this.textBox5.Name = "textBox5";
            this.textBox5.OutputFormat = resources.GetString("textBox5.OutputFormat");
            this.textBox5.Style = "font-family: Tahoma; font-size: 8.25pt; text-align: right; ddo-char-set: 0";
            this.textBox5.Text = null;
            this.textBox5.Top = 0.009000329F;
            this.textBox5.Width = 0.9510008F;
            // 
            // textBox6
            // 
            this.textBox6.DataField = "Discount";
            this.textBox6.Height = 0.1500001F;
            this.textBox6.Left = 6.200617F;
            this.textBox6.Name = "textBox6";
            this.textBox6.OutputFormat = resources.GetString("textBox6.OutputFormat");
            this.textBox6.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: bold; text-align: right; ddo" +
    "-char-set: 0";
            this.textBox6.SummaryGroup = "groupHeader1";
            this.textBox6.SummaryRunning = DataDynamics.ActiveReports.SummaryRunning.Group;
            this.textBox6.SummaryType = DataDynamics.ActiveReports.SummaryType.SubTotal;
            this.textBox6.Text = "textBox8";
            this.textBox6.Top = 0.05408331F;
            this.textBox6.Width = 0.9510008F;
            // 
            // textBox10
            // 
            this.textBox10.DataField = "Discount";
            this.textBox10.Height = 0.1500001F;
            this.textBox10.Left = 6.200617F;
            this.textBox10.Name = "textBox10";
            this.textBox10.OutputFormat = resources.GetString("textBox10.OutputFormat");
            this.textBox10.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: bold; text-align: right; ddo" +
    "-char-set: 0";
            this.textBox10.SummaryRunning = DataDynamics.ActiveReports.SummaryRunning.All;
            this.textBox10.SummaryType = DataDynamics.ActiveReports.SummaryType.GrandTotal;
            this.textBox10.Text = null;
            this.textBox10.Top = 0.007999998F;
            this.textBox10.Width = 0.9510008F;
            // 
            // rptZoneWiseSalesDetail
            // 
            this.MasterReport = false;
            this.PageSettings.Margins.Left = 0.5F;
            this.PageSettings.Margins.Right = 0.5F;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.PrintWidth = 7.21875F;
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
            ((System.ComponentModel.ISupportInitialize)(this.TextBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPharmacyName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportInfo1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.TextBox TextBox7;
        private DataDynamics.ActiveReports.ReportInfo ReportInfo1;
        private DataDynamics.ActiveReports.Label Label1;
        private DataDynamics.ActiveReports.Label Label3;
        private DataDynamics.ActiveReports.Label Label4;
        private DataDynamics.ActiveReports.GroupHeader groupHeader1;
        private DataDynamics.ActiveReports.GroupFooter groupFooter1;
        private DataDynamics.ActiveReports.TextBox TextBox3;
        private DataDynamics.ActiveReports.TextBox TextBox4;
        private DataDynamics.ActiveReports.Label Label8;
        private DataDynamics.ActiveReports.TextBox TextBox9;
        private DataDynamics.ActiveReports.Line Line1;
        public DataDynamics.ActiveReports.TextBox txtPharmacyName;
        public DataDynamics.ActiveReports.TextBox txtAddress;
        private DataDynamics.ActiveReports.TextBox textBox8;
        private DataDynamics.ActiveReports.Label label7;
        private DataDynamics.ActiveReports.TextBox TextBox1;
        private DataDynamics.ActiveReports.Label label2;
        private DataDynamics.ActiveReports.TextBox textBox2;
        private DataDynamics.ActiveReports.Label label5;
        private DataDynamics.ActiveReports.TextBox textBox5;
        private DataDynamics.ActiveReports.TextBox textBox10;
        private DataDynamics.ActiveReports.TextBox textBox6;
    }
}
