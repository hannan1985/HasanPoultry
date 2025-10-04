namespace Tiles_Inventory.Reports
{
    /// <summary>
    /// Summary description for rptTrialBalance.
    /// </summary>
    partial class rptTrialBalance
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(rptTrialBalance));
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.txtAccountName = new DataDynamics.ActiveReports.TextBox();
            this.txtCreditAmount = new DataDynamics.ActiveReports.TextBox();
            this.txtDebitAmount = new DataDynamics.ActiveReports.TextBox();
            this.line3 = new DataDynamics.ActiveReports.Line();
            this.line4 = new DataDynamics.ActiveReports.Line();
            this.line5 = new DataDynamics.ActiveReports.Line();
            this.line6 = new DataDynamics.ActiveReports.Line();
            this.line7 = new DataDynamics.ActiveReports.Line();
            this.pageHeader = new DataDynamics.ActiveReports.PageHeader();
            this.label4 = new DataDynamics.ActiveReports.Label();
            this.label5 = new DataDynamics.ActiveReports.Label();
            this.label6 = new DataDynamics.ActiveReports.Label();
            this.label7 = new DataDynamics.ActiveReports.Label();
            this.line1 = new DataDynamics.ActiveReports.Line();
            this.txtPharmacyName = new DataDynamics.ActiveReports.TextBox();
            this.txtAddress = new DataDynamics.ActiveReports.TextBox();
            this.line8 = new DataDynamics.ActiveReports.Line();
            this.line9 = new DataDynamics.ActiveReports.Line();
            this.line10 = new DataDynamics.ActiveReports.Line();
            this.line11 = new DataDynamics.ActiveReports.Line();
            this.line12 = new DataDynamics.ActiveReports.Line();
            this.txtFromDate = new DataDynamics.ActiveReports.TextBox();
            this.txtToDate = new DataDynamics.ActiveReports.TextBox();
            this.lblFrom = new DataDynamics.ActiveReports.Label();
            this.lblTo = new DataDynamics.ActiveReports.Label();
            this.pageFooter = new DataDynamics.ActiveReports.PageFooter();
            this.textBox2 = new DataDynamics.ActiveReports.TextBox();
            this.textBox3 = new DataDynamics.ActiveReports.TextBox();
            this.line2 = new DataDynamics.ActiveReports.Line();
            this.reportInfo2 = new DataDynamics.ActiveReports.ReportInfo();
            this.groupHeader1 = new DataDynamics.ActiveReports.GroupHeader();
            this.groupFooter1 = new DataDynamics.ActiveReports.GroupFooter();
            this.textBox4 = new DataDynamics.ActiveReports.TextBox();
            this.textBox5 = new DataDynamics.ActiveReports.TextBox();
            this.line13 = new DataDynamics.ActiveReports.Line();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccountName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreditAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDebitAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPharmacyName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportInfo2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            this.detail.CanShrink = true;
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.txtAccountName,
            this.txtCreditAmount,
            this.txtDebitAmount,
            this.line3,
            this.line4,
            this.line5,
            this.line6,
            this.line7});
            this.detail.Height = 0.307F;
            this.detail.Name = "detail";
            // 
            // txtAccountName
            // 
            this.txtAccountName.DataField = "AccountName";
            this.txtAccountName.Height = 0.2F;
            this.txtAccountName.Left = 0.135F;
            this.txtAccountName.Name = "txtAccountName";
            this.txtAccountName.Style = "font-family: Tahoma; font-size: 9pt; ddo-char-set: 0";
            this.txtAccountName.Text = null;
            this.txtAccountName.Top = 0.015F;
            this.txtAccountName.Width = 3.135F;
            // 
            // txtCreditAmount
            // 
            this.txtCreditAmount.DataField = "CreditAmount";
            this.txtCreditAmount.Height = 0.2F;
            this.txtCreditAmount.Left = 5.031F;
            this.txtCreditAmount.Name = "txtCreditAmount";
            this.txtCreditAmount.OutputFormat = resources.GetString("txtCreditAmount.OutputFormat");
            this.txtCreditAmount.Style = "font-family: Tahoma; font-size: 9pt; text-align: right; ddo-char-set: 0";
            this.txtCreditAmount.Text = null;
            this.txtCreditAmount.Top = 0.015F;
            this.txtCreditAmount.Width = 1.344F;
            // 
            // txtDebitAmount
            // 
            this.txtDebitAmount.DataField = "DebitAmount";
            this.txtDebitAmount.Height = 0.2F;
            this.txtDebitAmount.Left = 3.55F;
            this.txtDebitAmount.Name = "txtDebitAmount";
            this.txtDebitAmount.OutputFormat = resources.GetString("txtDebitAmount.OutputFormat");
            this.txtDebitAmount.Style = "font-family: Tahoma; font-size: 9pt; text-align: right; ddo-char-set: 0";
            this.txtDebitAmount.Text = null;
            this.txtDebitAmount.Top = 0.015F;
            this.txtDebitAmount.Width = 1.344F;
            // 
            // line3
            // 
            this.line3.AnchorBottom = true;
            this.line3.Height = 0F;
            this.line3.Left = 0.021F;
            this.line3.LineStyle = DataDynamics.ActiveReports.LineStyle.Dash;
            this.line3.LineWeight = 1F;
            this.line3.Name = "line3";
            this.line3.Top = 0.225F;
            this.line3.Width = 6.424713F;
            this.line3.X1 = 0.021F;
            this.line3.X2 = 6.445713F;
            this.line3.Y1 = 0.225F;
            this.line3.Y2 = 0.225F;
            // 
            // line4
            // 
            this.line4.AnchorBottom = true;
            this.line4.Height = 0.232F;
            this.line4.Left = 0.021F;
            this.line4.LineWeight = 1F;
            this.line4.Name = "line4";
            this.line4.Top = 0F;
            this.line4.Width = 0F;
            this.line4.X1 = 0.021F;
            this.line4.X2 = 0.021F;
            this.line4.Y1 = 0F;
            this.line4.Y2 = 0.232F;
            // 
            // line5
            // 
            this.line5.AnchorBottom = true;
            this.line5.Height = 0.232F;
            this.line5.Left = 3.385F;
            this.line5.LineWeight = 1F;
            this.line5.Name = "line5";
            this.line5.Top = 0F;
            this.line5.Width = 0F;
            this.line5.X1 = 3.385F;
            this.line5.X2 = 3.385F;
            this.line5.Y1 = 0F;
            this.line5.Y2 = 0.232F;
            // 
            // line6
            // 
            this.line6.AnchorBottom = true;
            this.line6.Height = 0.232F;
            this.line6.Left = 4.958F;
            this.line6.LineWeight = 1F;
            this.line6.Name = "line6";
            this.line6.Top = 0F;
            this.line6.Width = 0F;
            this.line6.X1 = 4.958F;
            this.line6.X2 = 4.958F;
            this.line6.Y1 = 0F;
            this.line6.Y2 = 0.232F;
            // 
            // line7
            // 
            this.line7.AnchorBottom = true;
            this.line7.Height = 0.232F;
            this.line7.Left = 6.436F;
            this.line7.LineWeight = 1F;
            this.line7.Name = "line7";
            this.line7.Top = 0F;
            this.line7.Width = 0F;
            this.line7.X1 = 6.436F;
            this.line7.X2 = 6.436F;
            this.line7.Y1 = 0F;
            this.line7.Y2 = 0.232F;
            // 
            // pageHeader
            // 
            this.pageHeader.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.label4,
            this.label5,
            this.label6,
            this.label7,
            this.line1,
            this.txtPharmacyName,
            this.txtAddress,
            this.line8,
            this.line9,
            this.line10,
            this.line11,
            this.line12,
            this.txtFromDate,
            this.txtToDate,
            this.lblFrom,
            this.lblTo});
            this.pageHeader.Height = 1.613945F;
            this.pageHeader.Name = "pageHeader";
            // 
            // label4
            // 
            this.label4.Height = 0.2F;
            this.label4.HyperLink = null;
            this.label4.Left = 0.135F;
            this.label4.Name = "label4";
            this.label4.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; ddo-char-set: 1";
            this.label4.Text = "Description";
            this.label4.Top = 1.354F;
            this.label4.Width = 1F;
            // 
            // label5
            // 
            this.label5.Height = 0.2F;
            this.label5.HyperLink = null;
            this.label5.Left = 3.55F;
            this.label5.Name = "label5";
            this.label5.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; text-align: right; ddo-ch" +
    "ar-set: 1";
            this.label5.Text = "Debit";
            this.label5.Top = 1.354F;
            this.label5.Width = 1.344F;
            // 
            // label6
            // 
            this.label6.Height = 0.2F;
            this.label6.HyperLink = null;
            this.label6.Left = 5.031F;
            this.label6.Name = "label6";
            this.label6.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; text-align: right; ddo-ch" +
    "ar-set: 1";
            this.label6.Text = "Credit";
            this.label6.Top = 1.354F;
            this.label6.Width = 1.344F;
            // 
            // label7
            // 
            this.label7.Height = 0.261F;
            this.label7.HyperLink = null;
            this.label7.Left = 2.521F;
            this.label7.Name = "label7";
            this.label7.Style = "font-family: Tahoma; font-size: 14.25pt; font-weight: bold; text-align: center; d" +
    "do-char-set: 0";
            this.label7.Text = "Trial Balance";
            this.label7.Top = 0.5790001F;
            this.label7.Width = 1.428F;
            // 
            // line1
            // 
            this.line1.Height = 0.000666976F;
            this.line1.Left = 0.021F;
            this.line1.LineWeight = 1F;
            this.line1.Name = "line1";
            this.line1.Top = 1.586333F;
            this.line1.Width = 6.414713F;
            this.line1.X1 = 0.021F;
            this.line1.X2 = 6.435713F;
            this.line1.Y1 = 1.587F;
            this.line1.Y2 = 1.586333F;
            // 
            // txtPharmacyName
            // 
            this.txtPharmacyName.Height = 0.2F;
            this.txtPharmacyName.Left = 1.81457F;
            this.txtPharmacyName.Name = "txtPharmacyName";
            this.txtPharmacyName.Style = "font-family: Tahoma; font-size: 12pt; text-align: center; ddo-char-set: 0";
            this.txtPharmacyName.Text = null;
            this.txtPharmacyName.Top = 0F;
            this.txtPharmacyName.Width = 2.84F;
            // 
            // txtAddress
            // 
            this.txtAddress.Height = 0.2700001F;
            this.txtAddress.Left = 1.58957F;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Style = "font-family: Tahoma; font-size: 9.75pt; text-align: center; ddo-char-set: 0";
            this.txtAddress.Text = null;
            this.txtAddress.Top = 0.2239999F;
            this.txtAddress.Width = 3.290708F;
            // 
            // line8
            // 
            this.line8.Height = 0.000666976F;
            this.line8.Left = 0.021F;
            this.line8.LineWeight = 1F;
            this.line8.Name = "line8";
            this.line8.Top = 1.333333F;
            this.line8.Width = 6.414713F;
            this.line8.X1 = 0.021F;
            this.line8.X2 = 6.435713F;
            this.line8.Y1 = 1.334F;
            this.line8.Y2 = 1.333333F;
            // 
            // line9
            // 
            this.line9.Height = 0.26F;
            this.line9.Left = 3.385F;
            this.line9.LineWeight = 1F;
            this.line9.Name = "line9";
            this.line9.Top = 1.347F;
            this.line9.Width = 0F;
            this.line9.X1 = 3.385F;
            this.line9.X2 = 3.385F;
            this.line9.Y1 = 1.347F;
            this.line9.Y2 = 1.607F;
            // 
            // line10
            // 
            this.line10.Height = 0.26F;
            this.line10.Left = 4.958F;
            this.line10.LineWeight = 1F;
            this.line10.Name = "line10";
            this.line10.Top = 1.334F;
            this.line10.Width = 0F;
            this.line10.X1 = 4.958F;
            this.line10.X2 = 4.958F;
            this.line10.Y1 = 1.334F;
            this.line10.Y2 = 1.594F;
            // 
            // line11
            // 
            this.line11.Height = 0.26F;
            this.line11.Left = 6.436F;
            this.line11.LineWeight = 1F;
            this.line11.Name = "line11";
            this.line11.Top = 1.337F;
            this.line11.Width = 0F;
            this.line11.X1 = 6.436F;
            this.line11.X2 = 6.436F;
            this.line11.Y1 = 1.337F;
            this.line11.Y2 = 1.597F;
            // 
            // line12
            // 
            this.line12.Height = 0.26F;
            this.line12.Left = 0.021F;
            this.line12.LineWeight = 1F;
            this.line12.Name = "line12";
            this.line12.Top = 1.334F;
            this.line12.Width = 0F;
            this.line12.X1 = 0.021F;
            this.line12.X2 = 0.021F;
            this.line12.Y1 = 1.334F;
            this.line12.Y2 = 1.594F;
            // 
            // txtFromDate
            // 
            this.txtFromDate.Height = 0.2F;
            this.txtFromDate.Left = 2.309F;
            this.txtFromDate.Name = "txtFromDate";
            this.txtFromDate.OutputFormat = resources.GetString("txtFromDate.OutputFormat");
            this.txtFromDate.Style = "font-family: Tahoma; font-size: 9pt; font-weight: normal; ddo-char-set: 0";
            this.txtFromDate.Text = null;
            this.txtFromDate.Top = 0.8980001F;
            this.txtFromDate.Width = 1F;
            // 
            // txtToDate
            // 
            this.txtToDate.Height = 0.2F;
            this.txtToDate.Left = 3.660916F;
            this.txtToDate.Name = "txtToDate";
            this.txtToDate.OutputFormat = resources.GetString("txtToDate.OutputFormat");
            this.txtToDate.Style = "font-family: Tahoma; font-size: 9pt; font-weight: normal; ddo-char-set: 0";
            this.txtToDate.Text = null;
            this.txtToDate.Top = 0.8980001F;
            this.txtToDate.Width = 1F;
            // 
            // lblFrom
            // 
            this.lblFrom.Height = 0.2F;
            this.lblFrom.HyperLink = null;
            this.lblFrom.Left = 1.809083F;
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Style = "font-family: Tahoma; font-size: 9pt; font-weight: normal; ddo-char-set: 0";
            this.lblFrom.Text = "From :";
            this.lblFrom.Top = 0.8980001F;
            this.lblFrom.Width = 0.4599169F;
            // 
            // lblTo
            // 
            this.lblTo.Height = 0.2F;
            this.lblTo.HyperLink = null;
            this.lblTo.Left = 3.310999F;
            this.lblTo.Name = "lblTo";
            this.lblTo.Style = "font-family: Tahoma; font-size: 9pt; font-weight: normal; ddo-char-set: 0";
            this.lblTo.Text = "To  :";
            this.lblTo.Top = 0.8980001F;
            this.lblTo.Width = 0.3299164F;
            // 
            // pageFooter
            // 
            this.pageFooter.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.textBox2,
            this.textBox3,
            this.line2,
            this.reportInfo2});
            this.pageFooter.Height = 0.8266667F;
            this.pageFooter.Name = "pageFooter";
            // 
            // textBox2
            // 
            this.textBox2.DataField = "DebitAmount";
            this.textBox2.Height = 0.2F;
            this.textBox2.Left = 3.55F;
            this.textBox2.Name = "textBox2";
            this.textBox2.OutputFormat = resources.GetString("textBox2.OutputFormat");
            this.textBox2.Style = "font-family: Tahoma; font-size: 9pt; font-weight: normal; text-align: right; ddo-" +
    "char-set: 0";
            this.textBox2.SummaryRunning = DataDynamics.ActiveReports.SummaryRunning.All;
            this.textBox2.SummaryType = DataDynamics.ActiveReports.SummaryType.GrandTotal;
            this.textBox2.Text = null;
            this.textBox2.Top = 0.21F;
            this.textBox2.Width = 1.344F;
            // 
            // textBox3
            // 
            this.textBox3.DataField = "CreditAmount";
            this.textBox3.Height = 0.2F;
            this.textBox3.Left = 5.031F;
            this.textBox3.Name = "textBox3";
            this.textBox3.OutputFormat = resources.GetString("textBox3.OutputFormat");
            this.textBox3.Style = "font-family: Tahoma; font-size: 9pt; font-weight: normal; text-align: right; ddo-" +
    "char-set: 0";
            this.textBox3.SummaryRunning = DataDynamics.ActiveReports.SummaryRunning.All;
            this.textBox3.SummaryType = DataDynamics.ActiveReports.SummaryType.GrandTotal;
            this.textBox3.Text = null;
            this.textBox3.Top = 0.21F;
            this.textBox3.Width = 1.343999F;
            // 
            // line2
            // 
            this.line2.Height = 0F;
            this.line2.Left = 3.385F;
            this.line2.LineWeight = 1F;
            this.line2.Name = "line2";
            this.line2.Top = 0.17F;
            this.line2.Width = 3.054847F;
            this.line2.X1 = 3.385F;
            this.line2.X2 = 6.439847F;
            this.line2.Y1 = 0.17F;
            this.line2.Y2 = 0.17F;
            // 
            // reportInfo2
            // 
            this.reportInfo2.FormatString = "Page {PageNumber} of {PageCount}";
            this.reportInfo2.Height = 0.15625F;
            this.reportInfo2.Left = 2.538924F;
            this.reportInfo2.Name = "reportInfo2";
            this.reportInfo2.Style = "font-family: Tahoma; font-size: 8.25pt; text-align: center; ddo-char-set: 0";
            this.reportInfo2.Top = 0.67F;
            this.reportInfo2.Width = 1.392F;
            // 
            // groupHeader1
            // 
            this.groupHeader1.Height = 0F;
            this.groupHeader1.Name = "groupHeader1";
            // 
            // groupFooter1
            // 
            this.groupFooter1.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.textBox4,
            this.textBox5,
            this.line13});
            this.groupFooter1.Height = 0.2916667F;
            this.groupFooter1.Name = "groupFooter1";
            // 
            // textBox4
            // 
            this.textBox4.DataField = "CreditAmount";
            this.textBox4.Height = 0.2F;
            this.textBox4.Left = 5.031F;
            this.textBox4.Name = "textBox4";
            this.textBox4.OutputFormat = resources.GetString("textBox4.OutputFormat");
            this.textBox4.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; text-align: right; ddo-ch" +
    "ar-set: 1";
            this.textBox4.SummaryRunning = DataDynamics.ActiveReports.SummaryRunning.All;
            this.textBox4.SummaryType = DataDynamics.ActiveReports.SummaryType.GrandTotal;
            this.textBox4.Text = null;
            this.textBox4.Top = 0.08000001F;
            this.textBox4.Width = 1.343999F;
            // 
            // textBox5
            // 
            this.textBox5.DataField = "DebitAmount";
            this.textBox5.Height = 0.2F;
            this.textBox5.Left = 3.55F;
            this.textBox5.Name = "textBox5";
            this.textBox5.OutputFormat = resources.GetString("textBox5.OutputFormat");
            this.textBox5.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; text-align: right; ddo-ch" +
    "ar-set: 1";
            this.textBox5.SummaryRunning = DataDynamics.ActiveReports.SummaryRunning.All;
            this.textBox5.SummaryType = DataDynamics.ActiveReports.SummaryType.GrandTotal;
            this.textBox5.Text = null;
            this.textBox5.Top = 0.08000001F;
            this.textBox5.Width = 1.344F;
            // 
            // line13
            // 
            this.line13.Height = 0F;
            this.line13.Left = 3.391F;
            this.line13.LineWeight = 1F;
            this.line13.Name = "line13";
            this.line13.Top = 0.06F;
            this.line13.Width = 3.054847F;
            this.line13.X1 = 3.391F;
            this.line13.X2 = 6.445847F;
            this.line13.Y1 = 0.06F;
            this.line13.Y2 = 0.06F;
            // 
            // rptTrialBalance
            // 
            this.MasterReport = false;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.PrintWidth = 6.469848F;
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
            ((System.ComponentModel.ISupportInitialize)(this.txtAccountName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreditAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDebitAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPharmacyName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportInfo2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.TextBox txtDescription;
        private DataDynamics.ActiveReports.TextBox txtAmount;
        private DataDynamics.ActiveReports.PageHeader pageHeader;
        private DataDynamics.ActiveReports.Label label1;
        private DataDynamics.ActiveReports.Label label2;
        private DataDynamics.ActiveReports.PageFooter pageFooter;
        private DataDynamics.ActiveReports.Label label3;
        private DataDynamics.ActiveReports.TextBox textBox1;
        private DataDynamics.ActiveReports.TextBox txtAccountName;
        private DataDynamics.ActiveReports.TextBox txtDebitAmount;
        private DataDynamics.ActiveReports.TextBox txtCreditAmount;
        private DataDynamics.ActiveReports.Label label4;
        private DataDynamics.ActiveReports.Label label5;
        private DataDynamics.ActiveReports.Label label6;
        private DataDynamics.ActiveReports.Label label7;
        private DataDynamics.ActiveReports.TextBox textBox2;
        private DataDynamics.ActiveReports.TextBox textBox3;
        private DataDynamics.ActiveReports.Line line1;
        private DataDynamics.ActiveReports.Line line2;
        public DataDynamics.ActiveReports.TextBox txtPharmacyName;
        public DataDynamics.ActiveReports.TextBox txtAddress;
        private DataDynamics.ActiveReports.Line line3;
        private DataDynamics.ActiveReports.Line line4;
        private DataDynamics.ActiveReports.Line line5;
        private DataDynamics.ActiveReports.Line line6;
        private DataDynamics.ActiveReports.Line line7;
        private DataDynamics.ActiveReports.Line line8;
        private DataDynamics.ActiveReports.Line line9;
        private DataDynamics.ActiveReports.Line line10;
        private DataDynamics.ActiveReports.Line line11;
        private DataDynamics.ActiveReports.Line line12;
        private DataDynamics.ActiveReports.GroupHeader groupHeader1;
        private DataDynamics.ActiveReports.GroupFooter groupFooter1;
        private DataDynamics.ActiveReports.TextBox textBox4;
        private DataDynamics.ActiveReports.TextBox textBox5;
        private DataDynamics.ActiveReports.Line line13;
        private DataDynamics.ActiveReports.ReportInfo reportInfo2;
        private DataDynamics.ActiveReports.Label lblFrom;
        private DataDynamics.ActiveReports.Label lblTo;
        public DataDynamics.ActiveReports.TextBox txtFromDate;
        public DataDynamics.ActiveReports.TextBox txtToDate;
    }
}
