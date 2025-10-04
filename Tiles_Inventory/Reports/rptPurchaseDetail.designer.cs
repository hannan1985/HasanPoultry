namespace Tiles_Inventory.Reports
{
    /// <summary>
    /// Summary description for rptPurchaseDetail.
    /// </summary>
    partial class rptPurchaseDetail
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(rptPurchaseDetail));
            this.pageHeader = new DataDynamics.ActiveReports.PageHeader();
            this.TextBox7 = new DataDynamics.ActiveReports.TextBox();
            this.txtPharmacyName = new DataDynamics.ActiveReports.TextBox();
            this.txtAddress = new DataDynamics.ActiveReports.TextBox();
            this.ReportInfo1 = new DataDynamics.ActiveReports.ReportInfo();
            this.Label1 = new DataDynamics.ActiveReports.Label();
            this.Label2 = new DataDynamics.ActiveReports.Label();
            this.Label3 = new DataDynamics.ActiveReports.Label();
            this.Label6 = new DataDynamics.ActiveReports.Label();
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.TextBox3 = new DataDynamics.ActiveReports.TextBox();
            this.TextBox6 = new DataDynamics.ActiveReports.TextBox();
            this.TextBox2 = new DataDynamics.ActiveReports.TextBox();
            this.pageFooter = new DataDynamics.ActiveReports.PageFooter();
            this.TextBox9 = new DataDynamics.ActiveReports.TextBox();
            this.Label8 = new DataDynamics.ActiveReports.Label();
            this.groupHeader1 = new DataDynamics.ActiveReports.GroupHeader();
            this.TextBox1 = new DataDynamics.ActiveReports.TextBox();
            this.groupFooter1 = new DataDynamics.ActiveReports.GroupFooter();
            this.Line1 = new DataDynamics.ActiveReports.Line();
            this.textBox8 = new DataDynamics.ActiveReports.TextBox();
            this.label7 = new DataDynamics.ActiveReports.Label();
            this.groupHeader2 = new DataDynamics.ActiveReports.GroupHeader();
            this.groupFooter2 = new DataDynamics.ActiveReports.GroupFooter();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPharmacyName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportInfo1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label7)).BeginInit();
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
            this.Label2,
            this.Label3,
            this.Label6});
            this.pageHeader.Height = 1.177F;
            this.pageHeader.Name = "pageHeader";
            // 
            // TextBox7
            // 
            this.TextBox7.Height = 0.2F;
            this.TextBox7.Left = 1.830157F;
            this.TextBox7.Name = "TextBox7";
            this.TextBox7.Style = "font-family: Tahoma; font-size: 12pt; font-weight: normal; text-align: center; dd" +
    "o-char-set: 0";
            this.TextBox7.Text = "Purchase Information";
            this.TextBox7.Top = 0.02F;
            this.TextBox7.Width = 2.84F;
            // 
            // txtPharmacyName
            // 
            this.txtPharmacyName.Height = 0.2F;
            this.txtPharmacyName.Left = 1.829844F;
            this.txtPharmacyName.Name = "txtPharmacyName";
            this.txtPharmacyName.Style = "font-family: Tahoma; font-size: 11.25pt; text-align: center";
            this.txtPharmacyName.Text = null;
            this.txtPharmacyName.Top = 0.2199999F;
            this.txtPharmacyName.Width = 2.84F;
            // 
            // txtAddress
            // 
            this.txtAddress.Height = 0.2F;
            this.txtAddress.Left = 1.269343F;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Style = "font-family: Tahoma; font-size: 9.75pt; text-align: center";
            this.txtAddress.Text = null;
            this.txtAddress.Top = 0.484F;
            this.txtAddress.Width = 3.961313F;
            // 
            // ReportInfo1
            // 
            this.ReportInfo1.FormatString = "{RunDateTime:dd-MM-yyyy h:mm tt}";
            this.ReportInfo1.Height = 0.15625F;
            this.ReportInfo1.Left = 4.629F;
            this.ReportInfo1.Name = "ReportInfo1";
            this.ReportInfo1.Style = "font-family: Tahoma; font-size: 9pt; text-align: right; ddo-char-set: 0";
            this.ReportInfo1.Top = 0F;
            this.ReportInfo1.Width = 1.392F;
            // 
            // Label1
            // 
            this.Label1.Height = 0.16F;
            this.Label1.HyperLink = null;
            this.Label1.Left = 0.493F;
            this.Label1.Name = "Label1";
            this.Label1.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: bold; ddo-char-set: 0";
            this.Label1.Text = "Purchase Date";
            this.Label1.Top = 0.9770001F;
            this.Label1.Width = 1F;
            // 
            // Label2
            // 
            this.Label2.Height = 0.16F;
            this.Label2.HyperLink = null;
            this.Label2.Left = 1.539F;
            this.Label2.Name = "Label2";
            this.Label2.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: bold; ddo-char-set: 0";
            this.Label2.Text = "Invoice Number";
            this.Label2.Top = 0.9770001F;
            this.Label2.Width = 0.97F;
            // 
            // Label3
            // 
            this.Label3.Height = 0.16F;
            this.Label3.HyperLink = null;
            this.Label3.Left = 2.567F;
            this.Label3.Name = "Label3";
            this.Label3.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: bold; ddo-char-set: 0";
            this.Label3.Text = "Suppler Name";
            this.Label3.Top = 0.9770001F;
            this.Label3.Width = 2.12F;
            // 
            // Label6
            // 
            this.Label6.Height = 0.16F;
            this.Label6.HyperLink = null;
            this.Label6.Left = 4.781667F;
            this.Label6.Name = "Label6";
            this.Label6.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: bold; text-align: right; ddo" +
    "-char-set: 0";
            this.Label6.Text = "Total";
            this.Label6.Top = 0.9770001F;
            this.Label6.Width = 0.9409998F;
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.TextBox3,
            this.TextBox6,
            this.TextBox2});
            this.detail.Height = 0.2083333F;
            this.detail.Name = "detail";
            // 
            // TextBox3
            // 
            this.TextBox3.DataField = "SupplierName";
            this.TextBox3.Height = 0.16F;
            this.TextBox3.Left = 2.567F;
            this.TextBox3.Name = "TextBox3";
            this.TextBox3.Style = "font-family: Tahoma; font-size: 8.25pt; ddo-char-set: 0";
            this.TextBox3.Text = null;
            this.TextBox3.Top = 0.02416655F;
            this.TextBox3.Width = 2.12F;
            // 
            // TextBox6
            // 
            this.TextBox6.DataField = "Total";
            this.TextBox6.Height = 0.16F;
            this.TextBox6.Left = 4.781667F;
            this.TextBox6.Name = "TextBox6";
            this.TextBox6.Style = "font-family: Tahoma; font-size: 8.25pt; text-align: right; ddo-char-set: 0";
            this.TextBox6.Text = null;
            this.TextBox6.Top = 0.02416666F;
            this.TextBox6.Width = 0.9409998F;
            // 
            // TextBox2
            // 
            this.TextBox2.DataField = "InvoiceNumber";
            this.TextBox2.Height = 0.16F;
            this.TextBox2.Left = 1.539F;
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.Style = "font-family: Tahoma; font-size: 8.25pt; ddo-char-set: 0";
            this.TextBox2.Text = null;
            this.TextBox2.Top = 0.024F;
            this.TextBox2.Width = 0.97F;
            // 
            // pageFooter
            // 
            this.pageFooter.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.TextBox9,
            this.Label8});
            this.pageFooter.Height = 0.2083333F;
            this.pageFooter.Name = "pageFooter";
            // 
            // TextBox9
            // 
            this.TextBox9.DataField = "Total";
            this.TextBox9.Height = 0.16F;
            this.TextBox9.Left = 4.781667F;
            this.TextBox9.Name = "TextBox9";
            this.TextBox9.OutputFormat = resources.GetString("TextBox9.OutputFormat");
            this.TextBox9.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: bold; text-align: right; ddo" +
    "-char-set: 0";
            this.TextBox9.SummaryRunning = DataDynamics.ActiveReports.SummaryRunning.All;
            this.TextBox9.SummaryType = DataDynamics.ActiveReports.SummaryType.GrandTotal;
            this.TextBox9.Text = null;
            this.TextBox9.Top = 0.02500006F;
            this.TextBox9.Width = 0.9409998F;
            // 
            // Label8
            // 
            this.Label8.Height = 0.16F;
            this.Label8.HyperLink = null;
            this.Label8.Left = 3.998666F;
            this.Label8.Name = "Label8";
            this.Label8.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: bold; ddo-char-set: 0";
            this.Label8.Text = "Grand Total";
            this.Label8.Top = 0.02500006F;
            this.Label8.Width = 0.7699995F;
            // 
            // groupHeader1
            // 
            this.groupHeader1.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.TextBox1});
            this.groupHeader1.DataField = "PurchaseDate";
            this.groupHeader1.Height = 0.1770833F;
            this.groupHeader1.Name = "groupHeader1";
            // 
            // TextBox1
            // 
            this.TextBox1.DataField = "PurchaseDate";
            this.TextBox1.Height = 0.16F;
            this.TextBox1.Left = 0.493F;
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.OutputFormat = resources.GetString("TextBox1.OutputFormat");
            this.TextBox1.Style = "font-family: Tahoma; font-size: 8.25pt; ddo-char-set: 0";
            this.TextBox1.Text = null;
            this.TextBox1.Top = 0.008541709F;
            this.TextBox1.Width = 1F;
            // 
            // groupFooter1
            // 
            this.groupFooter1.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.Line1,
            this.textBox8,
            this.label7});
            this.groupFooter1.Height = 0.25F;
            this.groupFooter1.Name = "groupFooter1";
            // 
            // Line1
            // 
            this.Line1.Height = 0F;
            this.Line1.Left = 3.922F;
            this.Line1.LineWeight = 1F;
            this.Line1.Name = "Line1";
            this.Line1.Top = 0.23F;
            this.Line1.Width = 1.93F;
            this.Line1.X1 = 5.852F;
            this.Line1.X2 = 3.922F;
            this.Line1.Y1 = 0.23F;
            this.Line1.Y2 = 0.23F;
            // 
            // textBox8
            // 
            this.textBox8.DataField = "Total";
            this.textBox8.Height = 0.16F;
            this.textBox8.Left = 4.781667F;
            this.textBox8.Name = "textBox8";
            this.textBox8.OutputFormat = resources.GetString("textBox8.OutputFormat");
            this.textBox8.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: bold; text-align: right; ddo" +
    "-char-set: 0";
            this.textBox8.SummaryGroup = "groupHeader1";
            this.textBox8.SummaryRunning = DataDynamics.ActiveReports.SummaryRunning.Group;
            this.textBox8.SummaryType = DataDynamics.ActiveReports.SummaryType.SubTotal;
            this.textBox8.Text = "textBox8";
            this.textBox8.Top = 0.02000006F;
            this.textBox8.Width = 0.9409998F;
            // 
            // label7
            // 
            this.label7.Height = 0.16F;
            this.label7.HyperLink = null;
            this.label7.Left = 3.978666F;
            this.label7.Name = "label7";
            this.label7.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: bold; text-align: right; ddo" +
    "-char-set: 0";
            this.label7.Text = "Total";
            this.label7.Top = 0.02000006F;
            this.label7.Width = 0.7706674F;
            // 
            // groupHeader2
            // 
            this.groupHeader2.DataField = "PurchaseID";
            this.groupHeader2.Height = 0F;
            this.groupHeader2.Name = "groupHeader2";
            // 
            // groupFooter2
            // 
            this.groupFooter2.Height = 0F;
            this.groupFooter2.Name = "groupFooter2";
            // 
            // rptPurchaseDetail
            // 
            this.MasterReport = false;
            this.PageSettings.Margins.Left = 0.5F;
            this.PageSettings.Margins.Right = 0.5F;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.Sections.Add(this.pageHeader);
            this.Sections.Add(this.groupHeader1);
            this.Sections.Add(this.groupHeader2);
            this.Sections.Add(this.detail);
            this.Sections.Add(this.groupFooter2);
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
            ((System.ComponentModel.ISupportInitialize)(this.Label2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.TextBox TextBox7;
        private DataDynamics.ActiveReports.ReportInfo ReportInfo1;
        private DataDynamics.ActiveReports.Label Label1;
        private DataDynamics.ActiveReports.Label Label2;
        private DataDynamics.ActiveReports.Label Label3;
        private DataDynamics.ActiveReports.Label Label6;
        private DataDynamics.ActiveReports.TextBox TextBox3;
        private DataDynamics.ActiveReports.TextBox TextBox6;
        private DataDynamics.ActiveReports.GroupHeader groupHeader1;
        private DataDynamics.ActiveReports.TextBox TextBox1;
        private DataDynamics.ActiveReports.GroupFooter groupFooter1;
        private DataDynamics.ActiveReports.Line Line1;
        private DataDynamics.ActiveReports.GroupHeader groupHeader2;
        private DataDynamics.ActiveReports.TextBox TextBox2;
        private DataDynamics.ActiveReports.GroupFooter groupFooter2;
        private DataDynamics.ActiveReports.TextBox TextBox9;
        private DataDynamics.ActiveReports.Label Label8;
        public DataDynamics.ActiveReports.TextBox txtPharmacyName;
        public DataDynamics.ActiveReports.TextBox txtAddress;
        private DataDynamics.ActiveReports.TextBox textBox8;
        private DataDynamics.ActiveReports.Label label7;
    }
}
