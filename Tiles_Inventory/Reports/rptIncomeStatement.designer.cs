namespace Tiles_Inventory.Reports
{
    /// <summary>
    /// Summary description for IncomeItem.
    /// </summary>
    partial class rptIncomeStatement
    {
        private DataDynamics.ActiveReports.Detail detail;

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
            DataDynamics.ActiveReports.DataSources.OleDBDataSource oleDBDataSource1 = new DataDynamics.ActiveReports.DataSources.OleDBDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptIncomeStatement));
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.textBox1 = new DataDynamics.ActiveReports.TextBox();
            this.textBox2 = new DataDynamics.ActiveReports.TextBox();
            this.groupHeader1 = new DataDynamics.ActiveReports.GroupHeader();
            this.textBox3 = new DataDynamics.ActiveReports.TextBox();
            this.groupFooter1 = new DataDynamics.ActiveReports.GroupFooter();
            this.txtTotalAmount = new DataDynamics.ActiveReports.TextBox();
            this.pageHeader1 = new DataDynamics.ActiveReports.PageHeader();
            this.TextBox7 = new DataDynamics.ActiveReports.TextBox();
            this.txtPharmacyName = new DataDynamics.ActiveReports.TextBox();
            this.txtAddress = new DataDynamics.ActiveReports.TextBox();
            this.ReportInfo1 = new DataDynamics.ActiveReports.ReportInfo();
            this.txtReportCaption = new DataDynamics.ActiveReports.TextBox();
            this.pageFooter1 = new DataDynamics.ActiveReports.PageFooter();
            this.txtProfitOrLoss = new DataDynamics.ActiveReports.TextBox();
            this.label1 = new DataDynamics.ActiveReports.Label();
            this.groupHeader2 = new DataDynamics.ActiveReports.GroupHeader();
            this.textBox4 = new DataDynamics.ActiveReports.TextBox();
            this.groupFooter2 = new DataDynamics.ActiveReports.GroupFooter();
            this.reportInfo2 = new DataDynamics.ActiveReports.ReportInfo();
            this.txtFromDate = new DataDynamics.ActiveReports.TextBox();
            this.txtToDate = new DataDynamics.ActiveReports.TextBox();
            this.lblFrom = new DataDynamics.ActiveReports.Label();
            this.lblTo = new DataDynamics.ActiveReports.Label();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPharmacyName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportInfo1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReportCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProfitOrLoss)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportInfo2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            this.detail.CanShrink = true;
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.textBox1,
            this.textBox2});
            this.detail.Height = 0.25F;
            this.detail.Name = "detail";
            this.detail.Format += new System.EventHandler(this.detail_Format);
            // 
            // textBox1
            // 
            this.textBox1.DataField = "AccountsName";
            this.textBox1.Height = 0.2F;
            this.textBox1.Left = 1.12F;
            this.textBox1.Name = "textBox1";
            this.textBox1.Style = "font-family: Tahoma; font-size: 9pt; ddo-char-set: 0";
            this.textBox1.Text = null;
            this.textBox1.Top = 0.02500001F;
            this.textBox1.Width = 2.14F;
            // 
            // textBox2
            // 
            this.textBox2.DataField = "Amount";
            this.textBox2.Height = 0.2F;
            this.textBox2.Left = 3.792F;
            this.textBox2.Name = "textBox2";
            this.textBox2.Style = "font-family: Tahoma; font-size: 9pt; text-align: right; ddo-char-set: 0";
            this.textBox2.Text = null;
            this.textBox2.Top = 0.02500001F;
            this.textBox2.Width = 1F;
            // 
            // groupHeader1
            // 
            this.groupHeader1.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.textBox3});
            this.groupHeader1.DataField = "AccountType";
            this.groupHeader1.Height = 0.2604166F;
            this.groupHeader1.Name = "groupHeader1";
            // 
            // textBox3
            // 
            this.textBox3.DataField = "AccountType";
            this.textBox3.Height = 0.2F;
            this.textBox3.Left = 1.12F;
            this.textBox3.Name = "textBox3";
            this.textBox3.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: bold; text-decoration: under" +
    "line; ddo-char-set: 0";
            this.textBox3.Text = "Name";
            this.textBox3.Top = 0.0302083F;
            this.textBox3.Width = 2.14F;
            // 
            // groupFooter1
            // 
            this.groupFooter1.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.txtTotalAmount});
            this.groupFooter1.Height = 0.25F;
            this.groupFooter1.Name = "groupFooter1";
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.DataField = "Amount";
            this.txtTotalAmount.Height = 0.2F;
            this.txtTotalAmount.Left = 4.857019F;
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.OutputFormat = resources.GetString("txtTotalAmount.OutputFormat");
            this.txtTotalAmount.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: bold; text-align: right; ddo" +
    "-char-set: 0";
            this.txtTotalAmount.SummaryGroup = "groupHeader1";
            this.txtTotalAmount.SummaryRunning = DataDynamics.ActiveReports.SummaryRunning.Group;
            this.txtTotalAmount.SummaryType = DataDynamics.ActiveReports.SummaryType.SubTotal;
            this.txtTotalAmount.Text = null;
            this.txtTotalAmount.Top = 0.025F;
            this.txtTotalAmount.Width = 1.319982F;
            // 
            // pageHeader1
            // 
            this.pageHeader1.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.TextBox7,
            this.txtPharmacyName,
            this.txtAddress,
            this.ReportInfo1,
            this.txtReportCaption,
            this.txtFromDate,
            this.txtToDate,
            this.lblFrom,
            this.lblTo});
            this.pageHeader1.Height = 1.364584F;
            this.pageHeader1.Name = "pageHeader1";
            // 
            // TextBox7
            // 
            this.TextBox7.Height = 0.2F;
            this.TextBox7.Left = 1.829823F;
            this.TextBox7.Name = "TextBox7";
            this.TextBox7.Style = "font-family: Tahoma; font-size: 11.25pt; font-weight: normal; text-align: center;" +
    " ddo-char-set: 0";
            this.TextBox7.Text = "Income Statement";
            this.TextBox7.Top = 0.524F;
            this.TextBox7.Width = 2.84F;
            // 
            // txtPharmacyName
            // 
            this.txtPharmacyName.Height = 0.2F;
            this.txtPharmacyName.Left = 1.829469F;
            this.txtPharmacyName.Name = "txtPharmacyName";
            this.txtPharmacyName.Style = "font-family: Tahoma; font-size: 9.75pt; text-align: center; ddo-char-set: 0";
            this.txtPharmacyName.Text = null;
            this.txtPharmacyName.Top = 0.06999999F;
            this.txtPharmacyName.Width = 2.84F;
            // 
            // txtAddress
            // 
            this.txtAddress.Height = 0.2F;
            this.txtAddress.Left = 1.604469F;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Style = "font-family: Tahoma; font-size: 8.25pt; text-align: center; ddo-char-set: 0";
            this.txtAddress.Text = null;
            this.txtAddress.Top = 0.2939999F;
            this.txtAddress.Width = 3.290708F;
            // 
            // ReportInfo1
            // 
            this.ReportInfo1.FormatString = "{RunDateTime:dd-MM-yyyy h:mm tt}";
            this.ReportInfo1.Height = 0.15625F;
            this.ReportInfo1.Left = 5.027F;
            this.ReportInfo1.Name = "ReportInfo1";
            this.ReportInfo1.Style = "font-family: Tahoma; font-size: 9pt; text-align: right; ddo-char-set: 0";
            this.ReportInfo1.Top = 0.062F;
            this.ReportInfo1.Width = 1.392F;
            // 
            // txtReportCaption
            // 
            this.txtReportCaption.Height = 0.2F;
            this.txtReportCaption.Left = 1.604823F;
            this.txtReportCaption.Name = "txtReportCaption";
            this.txtReportCaption.Style = "font-family: Tahoma; font-size: 12pt; text-align: center; ddo-char-set: 0";
            this.txtReportCaption.Text = null;
            this.txtReportCaption.Top = 0.744F;
            this.txtReportCaption.Width = 3.290708F;
            // 
            // pageFooter1
            // 
            this.pageFooter1.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.txtProfitOrLoss,
            this.label1,
            this.reportInfo2});
            this.pageFooter1.Height = 0.77125F;
            this.pageFooter1.Name = "pageFooter1";
            // 
            // txtProfitOrLoss
            // 
            this.txtProfitOrLoss.Height = 0.2F;
            this.txtProfitOrLoss.Left = 4.857F;
            this.txtProfitOrLoss.Name = "txtProfitOrLoss";
            this.txtProfitOrLoss.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: bold; text-align: right; ddo" +
    "-char-set: 0";
            this.txtProfitOrLoss.Text = null;
            this.txtProfitOrLoss.Top = 0F;
            this.txtProfitOrLoss.Width = 1.319982F;
            // 
            // label1
            // 
            this.label1.Height = 0.2F;
            this.label1.HyperLink = null;
            this.label1.Left = 1.12F;
            this.label1.Name = "label1";
            this.label1.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: bold; ddo-char-set: 0";
            this.label1.Text = "Net Income (Loss)";
            this.label1.Top = 0F;
            this.label1.Width = 1.47F;
            // 
            // groupHeader2
            // 
            this.groupHeader2.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.textBox4});
            this.groupHeader2.Height = 0.25F;
            this.groupHeader2.Name = "groupHeader2";
            // 
            // textBox4
            // 
            this.textBox4.DataField = "AccountTypeName";
            this.textBox4.Height = 0.2F;
            this.textBox4.Left = 1.12F;
            this.textBox4.Name = "textBox4";
            this.textBox4.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; ddo-char-set: 0";
            this.textBox4.Text = null;
            this.textBox4.Top = 0.025F;
            this.textBox4.Width = 2.140001F;
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
            this.reportInfo2.Left = 2.554F;
            this.reportInfo2.Name = "reportInfo2";
            this.reportInfo2.Style = "font-family: Tahoma; font-size: 8.25pt; text-align: center; ddo-char-set: 0";
            this.reportInfo2.Top = 0.615F;
            this.reportInfo2.Width = 1.392F;
            // 
            // txtFromDate
            // 
            this.txtFromDate.Height = 0.2F;
            this.txtFromDate.Left = 2.324F;
            this.txtFromDate.Name = "txtFromDate";
            this.txtFromDate.OutputFormat = resources.GetString("txtFromDate.OutputFormat");
            this.txtFromDate.Style = "font-family: Tahoma; font-size: 9pt; font-weight: normal; ddo-char-set: 0";
            this.txtFromDate.Text = null;
            this.txtFromDate.Top = 0.9910001F;
            this.txtFromDate.Width = 1F;
            // 
            // txtToDate
            // 
            this.txtToDate.Height = 0.2F;
            this.txtToDate.Left = 3.675916F;
            this.txtToDate.Name = "txtToDate";
            this.txtToDate.OutputFormat = resources.GetString("txtToDate.OutputFormat");
            this.txtToDate.Style = "font-family: Tahoma; font-size: 9pt; font-weight: normal; ddo-char-set: 0";
            this.txtToDate.Text = null;
            this.txtToDate.Top = 0.9910001F;
            this.txtToDate.Width = 1F;
            // 
            // lblFrom
            // 
            this.lblFrom.Height = 0.2F;
            this.lblFrom.HyperLink = null;
            this.lblFrom.Left = 1.824083F;
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Style = "font-family: Tahoma; font-size: 9pt; font-weight: normal; ddo-char-set: 0";
            this.lblFrom.Text = "From :";
            this.lblFrom.Top = 0.9910001F;
            this.lblFrom.Width = 0.4599169F;
            // 
            // lblTo
            // 
            this.lblTo.Height = 0.2F;
            this.lblTo.HyperLink = null;
            this.lblTo.Left = 3.325999F;
            this.lblTo.Name = "lblTo";
            this.lblTo.Style = "font-family: Tahoma; font-size: 9pt; font-weight: normal; ddo-char-set: 0";
            this.lblTo.Text = "To  :";
            this.lblTo.Top = 0.9910001F;
            this.lblTo.Width = 0.3299164F;
            // 
            // rptIncomeStatement
            // 
            this.MasterReport = false;
            oleDBDataSource1.ConnectionString = "Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;Initial " +
    "Catalog=Transport;Data Source=LINWIN-PC\\SQLEXPRESS";
            oleDBDataSource1.SQL = resources.GetString("oleDBDataSource1.SQL");
            this.DataSource = oleDBDataSource1;
            this.PageSettings.Margins.Left = 0.5F;
            this.PageSettings.Margins.Right = 0.5F;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.Sections.Add(this.pageHeader1);
            this.Sections.Add(this.groupHeader1);
            this.Sections.Add(this.groupHeader2);
            this.Sections.Add(this.detail);
            this.Sections.Add(this.groupFooter2);
            this.Sections.Add(this.groupFooter1);
            this.Sections.Add(this.pageFooter1);
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" +
            "l; font-size: 10pt; color: Black", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold", "Heading1", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" +
            "lic", "Heading2", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold", "Heading3", "Normal"));
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPharmacyName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportInfo1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReportCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProfitOrLoss)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox4)).EndInit();
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
        private DataDynamics.ActiveReports.GroupFooter groupFooter1;
        private DataDynamics.ActiveReports.TextBox textBox3;
        private DataDynamics.ActiveReports.PageHeader pageHeader1;
        private DataDynamics.ActiveReports.PageFooter pageFooter1;
        private DataDynamics.ActiveReports.TextBox TextBox7;
        private DataDynamics.ActiveReports.ReportInfo ReportInfo1;
        private DataDynamics.ActiveReports.GroupHeader groupHeader2;
        private DataDynamics.ActiveReports.TextBox textBox4;
        private DataDynamics.ActiveReports.GroupFooter groupFooter2;
        public DataDynamics.ActiveReports.TextBox txtPharmacyName;
        public DataDynamics.ActiveReports.TextBox txtAddress;
        private DataDynamics.ActiveReports.TextBox txtTotalAmount;
        private DataDynamics.ActiveReports.Label label1;
        public DataDynamics.ActiveReports.TextBox txtProfitOrLoss;
        public DataDynamics.ActiveReports.TextBox txtReportCaption;
        private DataDynamics.ActiveReports.ReportInfo reportInfo2;
        public DataDynamics.ActiveReports.TextBox txtFromDate;
        public DataDynamics.ActiveReports.TextBox txtToDate;
        private DataDynamics.ActiveReports.Label lblFrom;
        private DataDynamics.ActiveReports.Label lblTo;
    }
}
