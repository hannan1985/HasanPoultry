namespace Tiles_Inventory.Reports
{
    /// <summary>
    /// Summary description for rptCashBook.
    /// </summary>
    partial class rptCashBook
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(rptCashBook));
            this.pageHeader = new DataDynamics.ActiveReports.PageHeader();
            this.Shape1 = new DataDynamics.ActiveReports.Shape();
            this.label3 = new DataDynamics.ActiveReports.Label();
            this.TextBox7 = new DataDynamics.ActiveReports.TextBox();
            this.txtPharmacyName = new DataDynamics.ActiveReports.TextBox();
            this.txtAddress = new DataDynamics.ActiveReports.TextBox();
            this.txtFromDate = new DataDynamics.ActiveReports.TextBox();
            this.txtToDate = new DataDynamics.ActiveReports.TextBox();
            this.Label4 = new DataDynamics.ActiveReports.Label();
            this.Label5 = new DataDynamics.ActiveReports.Label();
            this.ReportInfo1 = new DataDynamics.ActiveReports.ReportInfo();
            this.label8 = new DataDynamics.ActiveReports.Label();
            this.Label1 = new DataDynamics.ActiveReports.Label();
            this.label6 = new DataDynamics.ActiveReports.Label();
            this.label7 = new DataDynamics.ActiveReports.Label();
            this.label2 = new DataDynamics.ActiveReports.Label();
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.textBox1 = new DataDynamics.ActiveReports.TextBox();
            this.textBox2 = new DataDynamics.ActiveReports.TextBox();
            this.txtReceiveAmount = new DataDynamics.ActiveReports.TextBox();
            this.textBox5 = new DataDynamics.ActiveReports.TextBox();
            this.txtExpenseAmount = new DataDynamics.ActiveReports.TextBox();
            this.line1 = new DataDynamics.ActiveReports.Line();
            this.textBox3 = new DataDynamics.ActiveReports.TextBox();
            this.pageFooter = new DataDynamics.ActiveReports.PageFooter();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPharmacyName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportInfo1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiveAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExpenseAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.Shape1,
            this.label3,
            this.TextBox7,
            this.txtPharmacyName,
            this.txtAddress,
            this.txtFromDate,
            this.txtToDate,
            this.Label4,
            this.Label5,
            this.ReportInfo1,
            this.label8,
            this.Label1,
            this.label6,
            this.label7,
            this.label2});
            this.pageHeader.Height = 1.82F;
            this.pageHeader.Name = "pageHeader";
            // 
            // Shape1
            // 
            this.Shape1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.Shape1.Height = 0.2390001F;
            this.Shape1.Left = 0.08760388F;
            this.Shape1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Shape1.Name = "Shape1";
            this.Shape1.RoundingRadius = 9.999999F;
            this.Shape1.Top = 1.57F;
            this.Shape1.Width = 7.498001F;
            // 
            // label3
            // 
            this.label3.Height = 0.2F;
            this.label3.HyperLink = null;
            this.label3.Left = 2.463F;
            this.label3.Name = "label3";
            this.label3.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; text-align: left; ddo-cha" +
    "r-set: 0";
            this.label3.Text = "Description";
            this.label3.Top = 1.598F;
            this.label3.Width = 1.913396F;
            // 
            // TextBox7
            // 
            this.TextBox7.Height = 0.2F;
            this.TextBox7.Left = 2.42375F;
            this.TextBox7.Name = "TextBox7";
            this.TextBox7.Style = "font-family: Tahoma; font-size: 12pt; font-weight: normal; text-align: center; dd" +
    "o-char-set: 0";
            this.TextBox7.Text = "Cash Book";
            this.TextBox7.Top = 0.08F;
            this.TextBox7.Width = 2.84F;
            // 
            // txtPharmacyName
            // 
            this.txtPharmacyName.Height = 0.2F;
            this.txtPharmacyName.Left = 2.42375F;
            this.txtPharmacyName.Name = "txtPharmacyName";
            this.txtPharmacyName.Style = "font-family: Tahoma; font-size: 11.25pt; text-align: center";
            this.txtPharmacyName.Text = null;
            this.txtPharmacyName.Top = 0.3F;
            this.txtPharmacyName.Width = 2.84F;
            // 
            // txtAddress
            // 
            this.txtAddress.Height = 0.2F;
            this.txtAddress.Left = 1.918437F;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Style = "font-family: Tahoma; font-size: 9.75pt; text-align: center";
            this.txtAddress.Text = null;
            this.txtAddress.Top = 0.5640001F;
            this.txtAddress.Width = 3.850625F;
            // 
            // txtFromDate
            // 
            this.txtFromDate.Height = 0.2F;
            this.txtFromDate.Left = 2.902292F;
            this.txtFromDate.Name = "txtFromDate";
            this.txtFromDate.OutputFormat = resources.GetString("txtFromDate.OutputFormat");
            this.txtFromDate.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; ddo-char-set: 0";
            this.txtFromDate.Text = null;
            this.txtFromDate.Top = 0.868F;
            this.txtFromDate.Width = 1F;
            // 
            // txtToDate
            // 
            this.txtToDate.Height = 0.2F;
            this.txtToDate.Left = 4.27425F;
            this.txtToDate.Name = "txtToDate";
            this.txtToDate.OutputFormat = resources.GetString("txtToDate.OutputFormat");
            this.txtToDate.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; ddo-char-set: 0";
            this.txtToDate.Text = "TextBox1";
            this.txtToDate.Top = 0.868F;
            this.txtToDate.Width = 1F;
            // 
            // Label4
            // 
            this.Label4.Height = 0.2F;
            this.Label4.HyperLink = null;
            this.Label4.Left = 2.413249F;
            this.Label4.Name = "Label4";
            this.Label4.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; ddo-char-set: 0";
            this.Label4.Text = "From :";
            this.Label4.Top = 0.868F;
            this.Label4.Width = 0.4599169F;
            // 
            // Label5
            // 
            this.Label5.Height = 0.2F;
            this.Label5.HyperLink = null;
            this.Label5.Left = 3.915165F;
            this.Label5.Name = "Label5";
            this.Label5.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; ddo-char-set: 0";
            this.Label5.Text = "To  :";
            this.Label5.Top = 0.868F;
            this.Label5.Width = 0.3299164F;
            // 
            // ReportInfo1
            // 
            this.ReportInfo1.FormatString = "{RunDateTime:dd-MM-yyyy h:mm tt}";
            this.ReportInfo1.Height = 0.15625F;
            this.ReportInfo1.Left = 5.720583F;
            this.ReportInfo1.Name = "ReportInfo1";
            this.ReportInfo1.Style = "font-family: Tahoma; font-size: 9pt; text-align: right; ddo-char-set: 0";
            this.ReportInfo1.Top = 0F;
            this.ReportInfo1.Width = 1.392F;
            // 
            // label8
            // 
            this.label8.Height = 0.2F;
            this.label8.HyperLink = null;
            this.label8.Left = 6.545812F;
            this.label8.Name = "label8";
            this.label8.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; text-align: right; ddo-ch" +
    "ar-set: 0";
            this.label8.Text = "Balance";
            this.label8.Top = 1.598F;
            this.label8.Width = 1.029417F;
            // 
            // Label1
            // 
            this.Label1.Height = 0.2F;
            this.Label1.HyperLink = null;
            this.Label1.Left = 5.446854F;
            this.Label1.Name = "Label1";
            this.Label1.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; text-align: right; ddo-ch" +
    "ar-set: 0";
            this.Label1.Text = "Payment";
            this.Label1.Top = 1.598F;
            this.Label1.Width = 1.070042F;
            // 
            // label6
            // 
            this.label6.Height = 0.2F;
            this.label6.HyperLink = null;
            this.label6.Left = 0.1076039F;
            this.label6.Name = "label6";
            this.label6.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; text-align: left; ddo-cha" +
    "r-set: 0";
            this.label6.Text = "Date";
            this.label6.Top = 1.598F;
            this.label6.Width = 0.86F;
            // 
            // label7
            // 
            this.label7.Height = 0.2F;
            this.label7.HyperLink = null;
            this.label7.Left = 4.426853F;
            this.label7.Name = "label7";
            this.label7.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; text-align: right; ddo-ch" +
    "ar-set: 0";
            this.label7.Text = "Received";
            this.label7.Top = 1.598F;
            this.label7.Width = 0.99F;
            // 
            // label2
            // 
            this.label2.Height = 0.2F;
            this.label2.HyperLink = null;
            this.label2.Left = 1.017604F;
            this.label2.Name = "label2";
            this.label2.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; text-align: left; ddo-cha" +
    "r-set: 0";
            this.label2.Text = "Account Name";
            this.label2.Top = 1.598F;
            this.label2.Width = 1.380396F;
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.textBox1,
            this.textBox2,
            this.txtReceiveAmount,
            this.textBox5,
            this.txtExpenseAmount,
            this.line1,
            this.textBox3});
            this.detail.Height = 0.2F;
            this.detail.Name = "detail";
            this.detail.Format += new System.EventHandler(this.detail_Format);
            // 
            // textBox1
            // 
            this.textBox1.DataField = "Date";
            this.textBox1.Height = 0.16F;
            this.textBox1.Left = 0.1076039F;
            this.textBox1.Name = "textBox1";
            this.textBox1.OutputFormat = resources.GetString("textBox1.OutputFormat");
            this.textBox1.Style = "font-family: Tahoma; font-size: 8.25pt; text-align: left; ddo-char-set: 0";
            this.textBox1.Text = null;
            this.textBox1.Top = 0F;
            this.textBox1.Width = 0.86F;
            // 
            // textBox2
            // 
            this.textBox2.DataField = "Description";
            this.textBox2.Height = 0.16F;
            this.textBox2.Left = 2.463F;
            this.textBox2.Name = "textBox2";
            this.textBox2.Style = "font-family: Tahoma; font-size: 8.25pt; text-align: left; ddo-char-set: 0";
            this.textBox2.Text = null;
            this.textBox2.Top = 0F;
            this.textBox2.Width = 1.913396F;
            // 
            // txtReceiveAmount
            // 
            this.txtReceiveAmount.DataField = "Received";
            this.txtReceiveAmount.Height = 0.16F;
            this.txtReceiveAmount.Left = 4.426853F;
            this.txtReceiveAmount.Name = "txtReceiveAmount";
            this.txtReceiveAmount.Style = "font-family: Tahoma; font-size: 8.25pt; text-align: right; ddo-char-set: 0";
            this.txtReceiveAmount.Text = null;
            this.txtReceiveAmount.Top = 1.192093E-07F;
            this.txtReceiveAmount.Width = 0.99F;
            // 
            // textBox5
            // 
            this.textBox5.DataField = "Payment";
            this.textBox5.Height = 0.16F;
            this.textBox5.Left = 5.446854F;
            this.textBox5.Name = "textBox5";
            this.textBox5.Style = "font-family: Tahoma; font-size: 8.25pt; text-align: right; ddo-char-set: 0";
            this.textBox5.Text = null;
            this.textBox5.Top = 1.192093E-07F;
            this.textBox5.Width = 1.070042F;
            // 
            // txtExpenseAmount
            // 
            this.txtExpenseAmount.DataField = "Balance";
            this.txtExpenseAmount.Height = 0.16F;
            this.txtExpenseAmount.Left = 6.545812F;
            this.txtExpenseAmount.Name = "txtExpenseAmount";
            this.txtExpenseAmount.OutputFormat = resources.GetString("txtExpenseAmount.OutputFormat");
            this.txtExpenseAmount.Style = "font-family: Tahoma; font-size: 8.25pt; text-align: right; ddo-char-set: 0";
            this.txtExpenseAmount.Text = null;
            this.txtExpenseAmount.Top = 0F;
            this.txtExpenseAmount.Width = 1.029417F;
            // 
            // line1
            // 
            this.line1.Height = 0F;
            this.line1.Left = 0.1076039F;
            this.line1.LineStyle = DataDynamics.ActiveReports.LineStyle.Dash;
            this.line1.LineWeight = 1F;
            this.line1.Name = "line1";
            this.line1.Top = 0.18F;
            this.line1.Width = 7.492292F;
            this.line1.X1 = 0.1076039F;
            this.line1.X2 = 7.599896F;
            this.line1.Y1 = 0.18F;
            this.line1.Y2 = 0.18F;
            // 
            // textBox3
            // 
            this.textBox3.DataField = "AccountName";
            this.textBox3.Height = 0.16F;
            this.textBox3.Left = 1.017604F;
            this.textBox3.Name = "textBox3";
            this.textBox3.Style = "font-family: Tahoma; font-size: 8.25pt; text-align: left; ddo-char-set: 0";
            this.textBox3.Text = null;
            this.textBox3.Top = 0F;
            this.textBox3.Width = 1.380396F;
            // 
            // pageFooter
            // 
            this.pageFooter.Height = 0F;
            this.pageFooter.Name = "pageFooter";
            // 
            // rptCashBook
            // 
            this.MasterReport = false;
            this.PageSettings.Margins.Left = 0.25F;
            this.PageSettings.Margins.Right = 0.25F;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.PrintWidth = 7.6875F;
            this.Sections.Add(this.pageHeader);
            this.Sections.Add(this.detail);
            this.Sections.Add(this.pageFooter);
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" +
            "l; font-size: 10pt; color: Black", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold", "Heading1", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" +
            "lic", "Heading2", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold", "Heading3", "Normal"));
            ((System.ComponentModel.ISupportInitialize)(this.label3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPharmacyName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportInfo1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiveAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExpenseAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.TextBox TextBox7;
        private DataDynamics.ActiveReports.Label Label4;
        private DataDynamics.ActiveReports.Label Label5;
        private DataDynamics.ActiveReports.ReportInfo ReportInfo1;
        public DataDynamics.ActiveReports.TextBox txtPharmacyName;
        public DataDynamics.ActiveReports.TextBox txtAddress;
        public DataDynamics.ActiveReports.TextBox txtFromDate;
        public DataDynamics.ActiveReports.TextBox txtToDate;
        private DataDynamics.ActiveReports.Shape Shape1;
        private DataDynamics.ActiveReports.Label label8;
        private DataDynamics.ActiveReports.Label label3;
        private DataDynamics.ActiveReports.Label Label1;
        private DataDynamics.ActiveReports.Label label6;
        private DataDynamics.ActiveReports.Label label7;
        private DataDynamics.ActiveReports.TextBox textBox1;
        private DataDynamics.ActiveReports.TextBox textBox2;
        private DataDynamics.ActiveReports.TextBox txtReceiveAmount;
        private DataDynamics.ActiveReports.TextBox textBox5;
        private DataDynamics.ActiveReports.TextBox txtExpenseAmount;
        private DataDynamics.ActiveReports.Line line1;
        private DataDynamics.ActiveReports.Label label2;
        private DataDynamics.ActiveReports.TextBox textBox3;
    }
}
