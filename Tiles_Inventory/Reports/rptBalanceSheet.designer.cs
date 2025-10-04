namespace Tiles_Inventory.Reports
{
    /// <summary>
    /// Summary description for BalanceSheet.
    /// </summary>
    partial class rptBalanceSheet
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(rptBalanceSheet));
            this.pageHeader = new DataDynamics.ActiveReports.PageHeader();
            this.TextBox7 = new DataDynamics.ActiveReports.TextBox();
            this.txtPharmacyName = new DataDynamics.ActiveReports.TextBox();
            this.txtAddress = new DataDynamics.ActiveReports.TextBox();
            this.ReportInfo1 = new DataDynamics.ActiveReports.ReportInfo();
            this.txtReportCaption = new DataDynamics.ActiveReports.TextBox();
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.textBox2 = new DataDynamics.ActiveReports.TextBox();
            this.textBox1 = new DataDynamics.ActiveReports.TextBox();
            this.pageFooter = new DataDynamics.ActiveReports.PageFooter();
            this.groupHeader1 = new DataDynamics.ActiveReports.GroupHeader();
            this.label1 = new DataDynamics.ActiveReports.Label();
            this.groupFooter1 = new DataDynamics.ActiveReports.GroupFooter();
            this.textBox3 = new DataDynamics.ActiveReports.TextBox();
            this.groupHeader2 = new DataDynamics.ActiveReports.GroupHeader();
            this.label5 = new DataDynamics.ActiveReports.Label();
            this.groupFooter2 = new DataDynamics.ActiveReports.GroupFooter();
            this.reportInfo2 = new DataDynamics.ActiveReports.ReportInfo();
            this.txtFromDate = new DataDynamics.ActiveReports.TextBox();
            this.txtToDate = new DataDynamics.ActiveReports.TextBox();
            this.lblFrom = new DataDynamics.ActiveReports.Label();
            this.lblTo = new DataDynamics.ActiveReports.Label();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPharmacyName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportInfo1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReportCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportInfo2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.TextBox7,
            this.txtPharmacyName,
            this.txtAddress,
            this.ReportInfo1,
            this.txtReportCaption,
            this.txtFromDate,
            this.txtToDate,
            this.lblFrom,
            this.lblTo});
            this.pageHeader.Height = 1.403666F;
            this.pageHeader.Name = "pageHeader";
            // 
            // TextBox7
            // 
            this.TextBox7.Height = 0.2F;
            this.TextBox7.Left = 1.778938F;
            this.TextBox7.Name = "TextBox7";
            this.TextBox7.Style = "font-family: Tahoma; font-size: 12pt; font-weight: normal; text-align: center; dd" +
    "o-char-set: 0";
            this.TextBox7.Text = "Balance Sheet";
            this.TextBox7.Top = 0.532F;
            this.TextBox7.Width = 2.84F;
            // 
            // txtPharmacyName
            // 
            this.txtPharmacyName.Height = 0.2F;
            this.txtPharmacyName.Left = 1.778938F;
            this.txtPharmacyName.Name = "txtPharmacyName";
            this.txtPharmacyName.Style = "font-family: Tahoma; font-size: 12pt; text-align: center; ddo-char-set: 0";
            this.txtPharmacyName.Text = null;
            this.txtPharmacyName.Top = 0.01800001F;
            this.txtPharmacyName.Width = 2.84F;
            // 
            // txtAddress
            // 
            this.txtAddress.Height = 0.2F;
            this.txtAddress.Left = 1.553938F;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Style = "font-family: Tahoma; font-size: 9.75pt; text-align: center; ddo-char-set: 0";
            this.txtAddress.Text = null;
            this.txtAddress.Top = 0.2419999F;
            this.txtAddress.Width = 3.290708F;
            // 
            // ReportInfo1
            // 
            this.ReportInfo1.FormatString = "{RunDateTime:dd-MM-yyyy h:mm tt}";
            this.ReportInfo1.Height = 0.15625F;
            this.ReportInfo1.Left = 4.937355F;
            this.ReportInfo1.Name = "ReportInfo1";
            this.ReportInfo1.Style = "font-family: Tahoma; font-size: 8.25pt; text-align: right; ddo-char-set: 0";
            this.ReportInfo1.Top = 0.03987501F;
            this.ReportInfo1.Width = 1.392F;
            // 
            // txtReportCaption
            // 
            this.txtReportCaption.Height = 0.2F;
            this.txtReportCaption.Left = 1.626938F;
            this.txtReportCaption.Name = "txtReportCaption";
            this.txtReportCaption.Style = "font-family: Tahoma; font-size: 12pt; text-align: center; ddo-char-set: 0";
            this.txtReportCaption.Text = null;
            this.txtReportCaption.Top = 0.752F;
            this.txtReportCaption.Width = 3.290708F;
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.textBox2,
            this.textBox1});
            this.detail.Height = 0.23125F;
            this.detail.Name = "detail";
            this.detail.Format += new System.EventHandler(this.detail_Format);
            // 
            // textBox2
            // 
            this.textBox2.DataField = "Amount";
            this.textBox2.Height = 0.2F;
            this.textBox2.Left = 3.053F;
            this.textBox2.Name = "textBox2";
            this.textBox2.Style = "font-family: Tahoma; font-size: 9pt; text-align: right; ddo-char-set: 0";
            this.textBox2.Text = "textBox1";
            this.textBox2.Top = 0.01562506F;
            this.textBox2.Width = 1.21F;
            // 
            // textBox1
            // 
            this.textBox1.DataField = "AccountsName";
            this.textBox1.Height = 0.2F;
            this.textBox1.Left = 1.172F;
            this.textBox1.Name = "textBox1";
            this.textBox1.Style = "font-family: Tahoma; font-size: 9pt; ddo-char-set: 0";
            this.textBox1.Text = "textBox1";
            this.textBox1.Top = 0.01562494F;
            this.textBox1.Width = 1.583F;
            // 
            // pageFooter
            // 
            this.pageFooter.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.reportInfo2});
            this.pageFooter.Height = 0.5416667F;
            this.pageFooter.Name = "pageFooter";
            // 
            // groupHeader1
            // 
            this.groupHeader1.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.label1});
            this.groupHeader1.DataField = "AccountType";
            this.groupHeader1.Height = 0.2604167F;
            this.groupHeader1.Name = "groupHeader1";
            // 
            // label1
            // 
            this.label1.DataField = "AccountType";
            this.label1.Height = 0.2F;
            this.label1.HyperLink = null;
            this.label1.Left = 1.172F;
            this.label1.Name = "label1";
            this.label1.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: bold; text-decoration: under" +
    "line; ddo-char-set: 0";
            this.label1.Text = "label1";
            this.label1.Top = 0.02F;
            this.label1.Width = 1.583F;
            // 
            // groupFooter1
            // 
            this.groupFooter1.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.textBox3});
            this.groupFooter1.Height = 0.229F;
            this.groupFooter1.Name = "groupFooter1";
            // 
            // textBox3
            // 
            this.textBox3.DataField = "Amount";
            this.textBox3.Height = 0.2F;
            this.textBox3.Left = 4.328F;
            this.textBox3.Name = "textBox3";
            this.textBox3.OutputFormat = resources.GetString("textBox3.OutputFormat");
            this.textBox3.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; text-align: right; ddo-ch" +
    "ar-set: 1";
            this.textBox3.SummaryGroup = "groupHeader1";
            this.textBox3.SummaryRunning = DataDynamics.ActiveReports.SummaryRunning.Group;
            this.textBox3.SummaryType = DataDynamics.ActiveReports.SummaryType.SubTotal;
            this.textBox3.Text = "textBox1";
            this.textBox3.Top = 0.0145F;
            this.textBox3.Width = 1.42F;
            // 
            // groupHeader2
            // 
            this.groupHeader2.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.label5});
            this.groupHeader2.DataField = "AccountTypeName";
            this.groupHeader2.Height = 0.25F;
            this.groupHeader2.Name = "groupHeader2";
            // 
            // label5
            // 
            this.label5.DataField = "AccountTypeName";
            this.label5.Height = 0.2F;
            this.label5.HyperLink = null;
            this.label5.Left = 1.172F;
            this.label5.Name = "label5";
            this.label5.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; text-decoration: underlin" +
    "e; ddo-char-set: 1";
            this.label5.Text = "label1";
            this.label5.Top = 0.025F;
            this.label5.Width = 1.583F;
            // 
            // groupFooter2
            // 
            this.groupFooter2.Height = 0F;
            this.groupFooter2.Name = "groupFooter2";
            // 
            // reportInfo2
            // 
            this.reportInfo2.FormatString = "Page {PageNumber} of {PageCount}";
            this.reportInfo2.Height = 0.15625F;
            this.reportInfo2.Left = 2.604F;
            this.reportInfo2.Name = "reportInfo2";
            this.reportInfo2.Style = "font-family: Tahoma; font-size: 8.25pt; text-align: center; ddo-char-set: 0";
            this.reportInfo2.Top = 0.355F;
            this.reportInfo2.Width = 1.392F;
            // 
            // txtFromDate
            // 
            this.txtFromDate.Height = 0.2F;
            this.txtFromDate.Left = 2.309793F;
            this.txtFromDate.Name = "txtFromDate";
            this.txtFromDate.OutputFormat = resources.GetString("txtFromDate.OutputFormat");
            this.txtFromDate.Style = "font-family: Tahoma; font-size: 9pt; font-weight: normal; ddo-char-set: 0";
            this.txtFromDate.Text = null;
            this.txtFromDate.Top = 1.01F;
            this.txtFromDate.Width = 1F;
            // 
            // txtToDate
            // 
            this.txtToDate.Height = 0.2F;
            this.txtToDate.Left = 3.661709F;
            this.txtToDate.Name = "txtToDate";
            this.txtToDate.OutputFormat = resources.GetString("txtToDate.OutputFormat");
            this.txtToDate.Style = "font-family: Tahoma; font-size: 9pt; font-weight: normal; ddo-char-set: 0";
            this.txtToDate.Text = null;
            this.txtToDate.Top = 1.01F;
            this.txtToDate.Width = 1F;
            // 
            // lblFrom
            // 
            this.lblFrom.Height = 0.2F;
            this.lblFrom.HyperLink = null;
            this.lblFrom.Left = 1.809875F;
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Style = "font-family: Tahoma; font-size: 9pt; font-weight: normal; ddo-char-set: 0";
            this.lblFrom.Text = "From :";
            this.lblFrom.Top = 1.01F;
            this.lblFrom.Width = 0.4599169F;
            // 
            // lblTo
            // 
            this.lblTo.Height = 0.2F;
            this.lblTo.HyperLink = null;
            this.lblTo.Left = 3.311792F;
            this.lblTo.Name = "lblTo";
            this.lblTo.Style = "font-family: Tahoma; font-size: 9pt; font-weight: normal; ddo-char-set: 0";
            this.lblTo.Text = "To  :";
            this.lblTo.Top = 1.01F;
            this.lblTo.Width = 0.3299164F;
            // 
            // rptBalanceSheet
            // 
            this.MasterReport = false;
            this.PageSettings.Margins.Left = 0.5F;
            this.PageSettings.Margins.Right = 0.5F;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.PrintWidth = 6.471584F;
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
            ((System.ComponentModel.ISupportInitialize)(this.txtReportCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportInfo2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.TextBox textBox1;
        private DataDynamics.ActiveReports.TextBox textBox2;
        private DataDynamics.ActiveReports.GroupHeader groupHeader1;
        private DataDynamics.ActiveReports.Label label1;
        private DataDynamics.ActiveReports.GroupFooter groupFooter1;
        private DataDynamics.ActiveReports.TextBox textBox3;
        private DataDynamics.ActiveReports.GroupHeader groupHeader2;
        private DataDynamics.ActiveReports.Label label5;
        private DataDynamics.ActiveReports.GroupFooter groupFooter2;
        private DataDynamics.ActiveReports.TextBox TextBox7;
        private DataDynamics.ActiveReports.ReportInfo ReportInfo1;
        public DataDynamics.ActiveReports.TextBox txtPharmacyName;
        public DataDynamics.ActiveReports.TextBox txtAddress;
        public DataDynamics.ActiveReports.TextBox txtReportCaption;
        private DataDynamics.ActiveReports.ReportInfo reportInfo2;
        public DataDynamics.ActiveReports.TextBox txtFromDate;
        public DataDynamics.ActiveReports.TextBox txtToDate;
        private DataDynamics.ActiveReports.Label lblFrom;
        private DataDynamics.ActiveReports.Label lblTo;
    }
}
