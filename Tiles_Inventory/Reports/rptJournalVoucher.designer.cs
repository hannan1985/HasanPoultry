namespace Tiles_Inventory
{
    /// <summary>
    /// Summary description for AllLedger.
    /// </summary>
    partial class rptJournalVoucher
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(rptJournalVoucher));
            DataDynamics.ActiveReports.DataSources.OleDBDataSource oleDBDataSource1 = new DataDynamics.ActiveReports.DataSources.OleDBDataSource();
            this.pageHeader = new DataDynamics.ActiveReports.PageHeader();
            this.label2 = new DataDynamics.ActiveReports.Label();
            this.label1 = new DataDynamics.ActiveReports.Label();
            this.label3 = new DataDynamics.ActiveReports.Label();
            this.label5 = new DataDynamics.ActiveReports.Label();
            this.label6 = new DataDynamics.ActiveReports.Label();
            this.lblAccountName = new DataDynamics.ActiveReports.Label();
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.textBox2 = new DataDynamics.ActiveReports.TextBox();
            this.textBox3 = new DataDynamics.ActiveReports.TextBox();
            this.line1 = new DataDynamics.ActiveReports.Line();
            this.textBox4 = new DataDynamics.ActiveReports.TextBox();
            this.textBox1 = new DataDynamics.ActiveReports.TextBox();
            this.textBox5 = new DataDynamics.ActiveReports.TextBox();
            this.pageFooter = new DataDynamics.ActiveReports.PageFooter();
            this.txtFromDate = new DataDynamics.ActiveReports.TextBox();
            this.txtToDate = new DataDynamics.ActiveReports.TextBox();
            this.label7 = new DataDynamics.ActiveReports.Label();
            this.label8 = new DataDynamics.ActiveReports.Label();
            this.groupHeader1 = new DataDynamics.ActiveReports.GroupHeader();
            this.groupFooter1 = new DataDynamics.ActiveReports.GroupFooter();
            this.line2 = new DataDynamics.ActiveReports.Line();
            this.line3 = new DataDynamics.ActiveReports.Line();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAccountName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.lblAccountName,
            this.txtFromDate,
            this.txtToDate,
            this.label7,
            this.label8});
            this.pageHeader.Height = 0.8794168F;
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Format += new System.EventHandler(this.pageHeader_Format);
            // 
            // label2
            // 
            this.label2.Height = 0.16F;
            this.label2.HyperLink = null;
            this.label2.Left = 3.529F;
            this.label2.Name = "label2";
            this.label2.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; ddo-char-set: 0";
            this.label2.Text = "Debit";
            this.label2.Top = 0.6719999F;
            this.label2.Width = 0.8500004F;
            // 
            // label1
            // 
            this.label1.Height = 0.16F;
            this.label1.HyperLink = null;
            this.label1.Left = 0.9713986F;
            this.label1.Name = "label1";
            this.label1.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; ddo-char-set: 0";
            this.label1.Text = "Accounts Name";
            this.label1.Top = 0.6719999F;
            this.label1.Width = 2.527602F;
            // 
            // label3
            // 
            this.label3.Height = 0.16F;
            this.label3.HyperLink = null;
            this.label3.Left = 4.414282F;
            this.label3.Name = "label3";
            this.label3.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; ddo-char-set: 0";
            this.label3.Text = "Credit";
            this.label3.Top = 0.6719999F;
            this.label3.Width = 0.8500004F;
            // 
            // label5
            // 
            this.label5.Height = 0.16F;
            this.label5.HyperLink = null;
            this.label5.Left = 0.9713986F;
            this.label5.Name = "label5";
            this.label5.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; ddo-char-set: 0";
            this.label5.Text = "Voucher Number";
            this.label5.Top = 0.05000001F;
            this.label5.Width = 1.085F;
            // 
            // label6
            // 
            this.label6.Height = 0.16F;
            this.label6.HyperLink = null;
            this.label6.Left = 0.9710002F;
            this.label6.Name = "label6";
            this.label6.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; ddo-char-set: 0";
            this.label6.Text = "Description";
            this.label6.Top = 0.267F;
            this.label6.Width = 1.085F;
            // 
            // lblAccountName
            // 
            this.lblAccountName.Height = 0.23F;
            this.lblAccountName.HyperLink = null;
            this.lblAccountName.Left = 1.538979F;
            this.lblAccountName.Name = "lblAccountName";
            this.lblAccountName.Style = "font-family: Tahoma; font-size: 12pt; font-weight: bold; text-align: center; text" +
    "-decoration: underline; ddo-char-set: 1";
            this.lblAccountName.Text = "Journal Voucher";
            this.lblAccountName.Top = 0.02F;
            this.lblAccountName.Width = 3.229042F;
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.textBox2,
            this.textBox3,
            this.line1,
            this.textBox1});
            this.detail.Height = 0.2789445F;
            this.detail.Name = "detail";
            // 
            // textBox2
            // 
            this.textBox2.DataField = "DebitAmount";
            this.textBox2.Height = 0.1791111F;
            this.textBox2.Left = 3.56486F;
            this.textBox2.Name = "textBox2";
            this.textBox2.OutputFormat = resources.GetString("textBox2.OutputFormat");
            this.textBox2.Style = "font-family: Tahoma; font-size: 9pt; ddo-char-set: 0";
            this.textBox2.Text = null;
            this.textBox2.Top = 0.01888891F;
            this.textBox2.Width = 0.8500004F;
            // 
            // textBox3
            // 
            this.textBox3.DataField = "CreditAmount";
            this.textBox3.Height = 0.1791111F;
            this.textBox3.Left = 4.450142F;
            this.textBox3.Name = "textBox3";
            this.textBox3.Style = "font-family: Tahoma; font-size: 9pt; ddo-char-set: 0";
            this.textBox3.Text = null;
            this.textBox3.Top = 0.01888891F;
            this.textBox3.Width = 0.8500004F;
            // 
            // line1
            // 
            this.line1.Height = 2.980232E-08F;
            this.line1.Left = 1.006858F;
            this.line1.LineStyle = DataDynamics.ActiveReports.LineStyle.Dash;
            this.line1.LineWeight = 1F;
            this.line1.Name = "line1";
            this.line1.Top = 0.26F;
            this.line1.Width = 4.293284F;
            this.line1.X1 = 1.006858F;
            this.line1.X2 = 5.300142F;
            this.line1.Y1 = 0.26F;
            this.line1.Y2 = 0.26F;
            // 
            // textBox4
            // 
            this.textBox4.DataField = "VoucherNo";
            this.textBox4.Height = 0.16F;
            this.textBox4.Left = 2.076F;
            this.textBox4.Name = "textBox4";
            this.textBox4.OutputFormat = resources.GetString("textBox4.OutputFormat");
            this.textBox4.Style = "font-family: Tahoma; font-size: 9pt; ddo-char-set: 0";
            this.textBox4.Text = null;
            this.textBox4.Top = 0.05000001F;
            this.textBox4.Width = 1.199F;
            // 
            // textBox1
            // 
            this.textBox1.DataField = "AccountName";
            this.textBox1.Height = 0.1791111F;
            this.textBox1.Left = 1.007257F;
            this.textBox1.Name = "textBox1";
            this.textBox1.Style = "font-family: Tahoma; font-size: 9pt; ddo-char-set: 0";
            this.textBox1.Text = null;
            this.textBox1.Top = 0.01888891F;
            this.textBox1.Width = 2.527602F;
            // 
            // textBox5
            // 
            this.textBox5.DataField = "Description";
            this.textBox5.Height = 0.25F;
            this.textBox5.Left = 2.076F;
            this.textBox5.Name = "textBox5";
            this.textBox5.Style = "font-family: Tahoma; font-size: 9pt; ddo-char-set: 0";
            this.textBox5.Text = null;
            this.textBox5.Top = 0.267F;
            this.textBox5.Width = 2.692F;
            // 
            // pageFooter
            // 
            this.pageFooter.Height = 0.05416667F;
            this.pageFooter.Name = "pageFooter";
            // 
            // txtFromDate
            // 
            this.txtFromDate.Height = 0.2F;
            this.txtFromDate.Left = 2.212042F;
            this.txtFromDate.Name = "txtFromDate";
            this.txtFromDate.OutputFormat = resources.GetString("txtFromDate.OutputFormat");
            this.txtFromDate.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; ddo-char-set: 0";
            this.txtFromDate.Text = null;
            this.txtFromDate.Top = 0.375F;
            this.txtFromDate.Width = 1F;
            // 
            // txtToDate
            // 
            this.txtToDate.Height = 0.2F;
            this.txtToDate.Left = 3.584F;
            this.txtToDate.Name = "txtToDate";
            this.txtToDate.OutputFormat = resources.GetString("txtToDate.OutputFormat");
            this.txtToDate.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; ddo-char-set: 0";
            this.txtToDate.Text = "TextBox1";
            this.txtToDate.Top = 0.375F;
            this.txtToDate.Width = 1F;
            // 
            // label7
            // 
            this.label7.Height = 0.2F;
            this.label7.HyperLink = null;
            this.label7.Left = 1.723F;
            this.label7.Name = "label7";
            this.label7.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; ddo-char-set: 0";
            this.label7.Text = "From :";
            this.label7.Top = 0.375F;
            this.label7.Width = 0.4599169F;
            // 
            // label8
            // 
            this.label8.Height = 0.2F;
            this.label8.HyperLink = null;
            this.label8.Left = 3.224916F;
            this.label8.Name = "label8";
            this.label8.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; ddo-char-set: 0";
            this.label8.Text = "To  :";
            this.label8.Top = 0.375F;
            this.label8.Width = 0.3299164F;
            // 
            // groupHeader1
            // 
            this.groupHeader1.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.textBox5,
            this.label2,
            this.label1,
            this.label3,
            this.label5,
            this.label6,
            this.textBox4});
            this.groupHeader1.DataField = "VoucherNo";
            this.groupHeader1.Height = 0.8549167F;
            this.groupHeader1.Name = "groupHeader1";
            // 
            // groupFooter1
            // 
            this.groupFooter1.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.line2,
            this.line3});
            this.groupFooter1.Height = 0.25F;
            this.groupFooter1.Name = "groupFooter1";
            // 
            // line2
            // 
            this.line2.Height = 0F;
            this.line2.Left = 0.9710001F;
            this.line2.LineStyle = DataDynamics.ActiveReports.LineStyle.Dash;
            this.line2.LineWeight = 1F;
            this.line2.Name = "line2";
            this.line2.Top = 0.1035F;
            this.line2.Width = 4.293284F;
            this.line2.X1 = 0.9710001F;
            this.line2.X2 = 5.264284F;
            this.line2.Y1 = 0.1035F;
            this.line2.Y2 = 0.1035F;
            // 
            // line3
            // 
            this.line3.Height = 0F;
            this.line3.Left = 0.971F;
            this.line3.LineStyle = DataDynamics.ActiveReports.LineStyle.Dash;
            this.line3.LineWeight = 1F;
            this.line3.Name = "line3";
            this.line3.Top = 0.1465F;
            this.line3.Width = 4.293284F;
            this.line3.X1 = 0.971F;
            this.line3.X2 = 5.264284F;
            this.line3.Y1 = 0.1465F;
            this.line3.Y2 = 0.1465F;
            // 
            // rptJournalVoucher
            // 
            this.MasterReport = false;
            oleDBDataSource1.ConnectionString = "Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;Initial " +
    "Catalog=Transport;Data Source=LINWIN-PC\\SQLEXPRESS";
            oleDBDataSource1.SQL = "";
            this.DataSource = oleDBDataSource1;
            this.PageSettings.Margins.Left = 0.25F;
            this.PageSettings.Margins.Right = 0.25F;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.PrintWidth = 6.307001F;
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
            ((System.ComponentModel.ISupportInitialize)(this.label2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAccountName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.TextBox textBox1;
        private DataDynamics.ActiveReports.TextBox textBox2;
        private DataDynamics.ActiveReports.TextBox textBox3;
        private DataDynamics.ActiveReports.Label label1;
        private DataDynamics.ActiveReports.Label label2;
        private DataDynamics.ActiveReports.Label label3;
        private DataDynamics.ActiveReports.Line line1;
        private DataDynamics.ActiveReports.Label label5;
        private DataDynamics.ActiveReports.TextBox textBox4;
        private DataDynamics.ActiveReports.Label label6;
        private DataDynamics.ActiveReports.TextBox textBox5;
        public DataDynamics.ActiveReports.Label lblAccountName;
        private DataDynamics.ActiveReports.Label label7;
        private DataDynamics.ActiveReports.Label label8;
        private DataDynamics.ActiveReports.GroupHeader groupHeader1;
        private DataDynamics.ActiveReports.GroupFooter groupFooter1;
        private DataDynamics.ActiveReports.Line line2;
        private DataDynamics.ActiveReports.Line line3;
        public DataDynamics.ActiveReports.TextBox txtFromDate;
        public DataDynamics.ActiveReports.TextBox txtToDate;
    }
}
