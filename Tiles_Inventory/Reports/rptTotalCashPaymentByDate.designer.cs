namespace Tiles_Inventory.Reports
{
    /// <summary>
    /// Summary description for rptTotalCashPaymentByDate.
    /// </summary>
    partial class rptTotalCashPaymentByDate
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(rptTotalCashPaymentByDate));
            this.pageHeader = new DataDynamics.ActiveReports.PageHeader();
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.pageFooter = new DataDynamics.ActiveReports.PageFooter();
            this.TextBox7 = new DataDynamics.ActiveReports.TextBox();
            this.txtPharmacyName = new DataDynamics.ActiveReports.TextBox();
            this.txtAddress = new DataDynamics.ActiveReports.TextBox();
            this.ReportInfo1 = new DataDynamics.ActiveReports.ReportInfo();
            this.Shape1 = new DataDynamics.ActiveReports.Shape();
            this.Label4 = new DataDynamics.ActiveReports.Label();
            this.Label5 = new DataDynamics.ActiveReports.Label();
            this.label9 = new DataDynamics.ActiveReports.Label();
            this.label1 = new DataDynamics.ActiveReports.Label();
            this.textBox1 = new DataDynamics.ActiveReports.TextBox();
            this.textBox2 = new DataDynamics.ActiveReports.TextBox();
            this.textBox3 = new DataDynamics.ActiveReports.TextBox();
            this.textBox4 = new DataDynamics.ActiveReports.TextBox();
            this.label3 = new DataDynamics.ActiveReports.Label();
            this.textBox5 = new DataDynamics.ActiveReports.TextBox();
            this.line1 = new DataDynamics.ActiveReports.Line();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPharmacyName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportInfo1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.TextBox7,
            this.txtPharmacyName,
            this.txtAddress,
            this.ReportInfo1,
            this.Shape1,
            this.Label4,
            this.Label5,
            this.label9,
            this.label1});
            this.pageHeader.Height = 1.4F;
            this.pageHeader.Name = "pageHeader";
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.textBox1,
            this.textBox2,
            this.textBox3,
            this.textBox4,
            this.line1});
            this.detail.Height = 0.2379445F;
            this.detail.Name = "detail";
            // 
            // pageFooter
            // 
            this.pageFooter.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.label3,
            this.textBox5});
            this.pageFooter.Height = 0.25F;
            this.pageFooter.Name = "pageFooter";
            // 
            // TextBox7
            // 
            this.TextBox7.Height = 0.2F;
            this.TextBox7.Left = 1.579F;
            this.TextBox7.Name = "TextBox7";
            this.TextBox7.Style = "font-family: Tahoma; font-size: 12pt; font-weight: normal; text-align: center; dd" +
                "o-char-set: 0";
            this.TextBox7.Text = "Cash Payment Information";
            this.TextBox7.Top = 0.016F;
            this.TextBox7.Width = 2.84F;
            // 
            // txtPharmacyName
            // 
            this.txtPharmacyName.Height = 0.2F;
            this.txtPharmacyName.Left = 1.579F;
            this.txtPharmacyName.Name = "txtPharmacyName";
            this.txtPharmacyName.Style = "font-family: Tahoma; font-size: 11.25pt; text-align: center";
            this.txtPharmacyName.Text = null;
            this.txtPharmacyName.Top = 0.2360002F;
            this.txtPharmacyName.Width = 2.84F;
            // 
            // txtAddress
            // 
            this.txtAddress.Height = 0.2F;
            this.txtAddress.Left = 1.073687F;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Style = "font-family: Tahoma; font-size: 9.75pt; text-align: center";
            this.txtAddress.Text = null;
            this.txtAddress.Top = 0.48F;
            this.txtAddress.Width = 3.850625F;
            // 
            // ReportInfo1
            // 
            this.ReportInfo1.FormatString = "{RunDateTime:dd-MM-yyyy h:mm tt}";
            this.ReportInfo1.Height = 0.15625F;
            this.ReportInfo1.Left = 4.695F;
            this.ReportInfo1.Name = "ReportInfo1";
            this.ReportInfo1.Style = "text-align: left";
            this.ReportInfo1.Top = 0F;
            this.ReportInfo1.Width = 1.392F;
            // 
            // Shape1
            // 
            this.Shape1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.Shape1.Height = 0.23F;
            this.Shape1.Left = 0.4914999F;
            this.Shape1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Shape1.Name = "Shape1";
            this.Shape1.RoundingRadius = 9.999999F;
            this.Shape1.Top = 1.17F;
            this.Shape1.Width = 5.392F;
            // 
            // Label4
            // 
            this.Label4.Height = 0.2F;
            this.Label4.HyperLink = null;
            this.Label4.Left = 3.154167F;
            this.Label4.Name = "Label4";
            this.Label4.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: normal; ddo-char-set: 0";
            this.Label4.Text = "Customer Name";
            this.Label4.Top = 1.18F;
            this.Label4.Width = 1.659F;
            // 
            // Label5
            // 
            this.Label5.Height = 0.2F;
            this.Label5.HyperLink = null;
            this.Label5.Left = 0.5314996F;
            this.Label5.Name = "Label5";
            this.Label5.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: normal; ddo-char-set: 0";
            this.Label5.Text = "Payment Date";
            this.Label5.Top = 1.18F;
            this.Label5.Width = 1F;
            // 
            // label9
            // 
            this.label9.Height = 0.2F;
            this.label9.HyperLink = null;
            this.label9.Left = 4.8595F;
            this.label9.Name = "label9";
            this.label9.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: normal; text-align: right; d" +
                "do-char-set: 0";
            this.label9.Text = "Payment Amount";
            this.label9.Top = 1.18F;
            this.label9.Width = 1F;
            // 
            // label1
            // 
            this.label1.Height = 0.2F;
            this.label1.HyperLink = null;
            this.label1.Left = 1.577833F;
            this.label1.Name = "label1";
            this.label1.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: normal; ddo-char-set: 0";
            this.label1.Text = "Supplier Name";
            this.label1.Top = 1.18F;
            this.label1.Width = 1.53F;
            // 
            // textBox1
            // 
            this.textBox1.DataField = "PaymentDate";
            this.textBox1.Height = 0.2F;
            this.textBox1.Left = 0.5235001F;
            this.textBox1.Name = "textBox1";
            this.textBox1.OutputFormat = resources.GetString("textBox1.OutputFormat");
            this.textBox1.Style = "font-family: Tahoma; font-size: 9pt; ddo-char-set: 0";
            this.textBox1.Text = "textBox1";
            this.textBox1.Top = 0F;
            this.textBox1.Width = 1F;
            // 
            // textBox2
            // 
            this.textBox2.DataField = "SupplierName";
            this.textBox2.Height = 0.2F;
            this.textBox2.Left = 1.569833F;
            this.textBox2.Name = "textBox2";
            this.textBox2.Style = "font-family: Tahoma; font-size: 9pt; ddo-char-set: 0";
            this.textBox2.Text = "textBox1";
            this.textBox2.Top = 0F;
            this.textBox2.Width = 1.53F;
            // 
            // textBox3
            // 
            this.textBox3.DataField = "CustomerName";
            this.textBox3.Height = 0.2F;
            this.textBox3.Left = 3.146167F;
            this.textBox3.Name = "textBox3";
            this.textBox3.Style = "font-family: Tahoma; font-size: 9pt; ddo-char-set: 0";
            this.textBox3.Text = "textBox1";
            this.textBox3.Top = 0F;
            this.textBox3.Width = 1.659F;
            // 
            // textBox4
            // 
            this.textBox4.DataField = "PaymentAmount";
            this.textBox4.Height = 0.2F;
            this.textBox4.Left = 4.8515F;
            this.textBox4.Name = "textBox4";
            this.textBox4.Style = "font-family: Tahoma; font-size: 9pt; text-align: right; ddo-char-set: 0";
            this.textBox4.Text = "textBox1";
            this.textBox4.Top = 0F;
            this.textBox4.Width = 1F;
            // 
            // label3
            // 
            this.label3.Height = 0.2F;
            this.label3.HyperLink = null;
            this.label3.Left = 3.807F;
            this.label3.Name = "label3";
            this.label3.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: bold; ddo-char-set: 1";
            this.label3.Text = "Total Amount";
            this.label3.Top = 0F;
            this.label3.Width = 1F;
            // 
            // textBox5
            // 
            this.textBox5.DataField = "PaymentAmount";
            this.textBox5.Height = 0.2F;
            this.textBox5.Left = 4.8515F;
            this.textBox5.Name = "textBox5";
            this.textBox5.OutputFormat = resources.GetString("textBox5.OutputFormat");
            this.textBox5.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: bold; text-align: right; ddo" +
                "-char-set: 1";
            this.textBox5.SummaryRunning = DataDynamics.ActiveReports.SummaryRunning.All;
            this.textBox5.SummaryType = DataDynamics.ActiveReports.SummaryType.PageTotal;
            this.textBox5.Text = null;
            this.textBox5.Top = 0F;
            this.textBox5.Width = 1F;
            // 
            // line1
            // 
            this.line1.Height = 0.0008333325F;
            this.line1.Left = 0.5239863F;
            this.line1.LineStyle = DataDynamics.ActiveReports.LineStyle.Dash;
            this.line1.LineWeight = 1F;
            this.line1.Name = "line1";
            this.line1.Top = 0.2301667F;
            this.line1.Width = 5.327514F;
            this.line1.X1 = 0.5239863F;
            this.line1.X2 = 5.8515F;
            this.line1.Y1 = 0.231F;
            this.line1.Y2 = 0.2301667F;
            // 
            // rptTotalCashPaymentByDate
            // 
            this.MasterReport = false;
            this.PageSettings.Margins.Right = 0.5F;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.PrintWidth = 6.375F;
            this.Sections.Add(this.pageHeader);
            this.Sections.Add(this.detail);
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
            ((System.ComponentModel.ISupportInitialize)(this.Label4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.TextBox TextBox7;
        private DataDynamics.ActiveReports.ReportInfo ReportInfo1;
        private DataDynamics.ActiveReports.Shape Shape1;
        private DataDynamics.ActiveReports.Label Label4;
        private DataDynamics.ActiveReports.Label Label5;
        private DataDynamics.ActiveReports.Label label9;
        private DataDynamics.ActiveReports.Label label1;
        private DataDynamics.ActiveReports.TextBox textBox1;
        private DataDynamics.ActiveReports.TextBox textBox2;
        private DataDynamics.ActiveReports.TextBox textBox3;
        private DataDynamics.ActiveReports.TextBox textBox4;
        private DataDynamics.ActiveReports.Label label3;
        private DataDynamics.ActiveReports.TextBox textBox5;
        public DataDynamics.ActiveReports.TextBox txtPharmacyName;
        public DataDynamics.ActiveReports.TextBox txtAddress;
        private DataDynamics.ActiveReports.Line line1;
    }
}
