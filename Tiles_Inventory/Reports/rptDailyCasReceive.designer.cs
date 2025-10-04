namespace Tiles_Inventory.Reports
{
    /// <summary>
    /// Summary description for rptDailyCasReceive.
    /// </summary>
    partial class rptDailyCasReceive
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(rptDailyCasReceive));
            this.pageHeader = new DataDynamics.ActiveReports.PageHeader();
            this.TextBox7 = new DataDynamics.ActiveReports.TextBox();
            this.txtPharmacyName = new DataDynamics.ActiveReports.TextBox();
            this.txtAddress = new DataDynamics.ActiveReports.TextBox();
            this.ReportInfo1 = new DataDynamics.ActiveReports.ReportInfo();
            this.Shape1 = new DataDynamics.ActiveReports.Shape();
            this.Label4 = new DataDynamics.ActiveReports.Label();
            this.Label5 = new DataDynamics.ActiveReports.Label();
            this.Label6 = new DataDynamics.ActiveReports.Label();
            this.label9 = new DataDynamics.ActiveReports.Label();
            this.label2 = new DataDynamics.ActiveReports.Label();
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.textBox2 = new DataDynamics.ActiveReports.TextBox();
            this.textBox3 = new DataDynamics.ActiveReports.TextBox();
            this.textBox4 = new DataDynamics.ActiveReports.TextBox();
            this.textBox6 = new DataDynamics.ActiveReports.TextBox();
            this.textBox1 = new DataDynamics.ActiveReports.TextBox();
            this.pageFooter = new DataDynamics.ActiveReports.PageFooter();
            this.textBox5 = new DataDynamics.ActiveReports.TextBox();
            this.label1 = new DataDynamics.ActiveReports.Label();
            this.groupHeader1 = new DataDynamics.ActiveReports.GroupHeader();
            this.groupFooter1 = new DataDynamics.ActiveReports.GroupFooter();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPharmacyName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportInfo1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).BeginInit();
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
            this.Label6,
            this.label9,
            this.label2});
            this.pageHeader.Height = 1.416666F;
            this.pageHeader.Name = "pageHeader";
            // 
            // TextBox7
            // 
            this.TextBox7.Height = 0.2F;
            this.TextBox7.Left = 1.928958F;
            this.TextBox7.Name = "TextBox7";
            this.TextBox7.Style = "font-family: Tahoma; font-size: 12pt; font-weight: normal; text-align: center; dd" +
                "o-char-set: 0";
            this.TextBox7.Text = "Cash Receive Information";
            this.TextBox7.Top = 0.016F;
            this.TextBox7.Width = 2.84F;
            // 
            // txtPharmacyName
            // 
            this.txtPharmacyName.Height = 0.2F;
            this.txtPharmacyName.Left = 1.928958F;
            this.txtPharmacyName.Name = "txtPharmacyName";
            this.txtPharmacyName.Style = "font-family: Tahoma; font-size: 11.25pt; text-align: center";
            this.txtPharmacyName.Text = null;
            this.txtPharmacyName.Top = 0.2360002F;
            this.txtPharmacyName.Width = 2.84F;
            // 
            // txtAddress
            // 
            this.txtAddress.Height = 0.2F;
            this.txtAddress.Left = 1.423646F;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Style = "font-family: Tahoma; font-size: 9.75pt; text-align: center";
            this.txtAddress.Text = null;
            this.txtAddress.Top = 0.4800001F;
            this.txtAddress.Width = 3.850625F;
            // 
            // ReportInfo1
            // 
            this.ReportInfo1.FormatString = "{RunDateTime:dd-MM-yyyy h:mm tt}";
            this.ReportInfo1.Height = 0.15625F;
            this.ReportInfo1.Left = 5.225F;
            this.ReportInfo1.Name = "ReportInfo1";
            this.ReportInfo1.Style = "text-align: left";
            this.ReportInfo1.Top = 0F;
            this.ReportInfo1.Width = 1.392F;
            // 
            // Shape1
            // 
            this.Shape1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.Shape1.Height = 0.23F;
            this.Shape1.Left = 0.148F;
            this.Shape1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Shape1.Name = "Shape1";
            this.Shape1.RoundingRadius = 9.999999F;
            this.Shape1.Top = 1.17F;
            this.Shape1.Width = 6.522F;
            // 
            // Label4
            // 
            this.Label4.Height = 0.2F;
            this.Label4.HyperLink = null;
            this.Label4.Left = 2.322333F;
            this.Label4.Name = "Label4";
            this.Label4.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: normal; ddo-char-set: 0";
            this.Label4.Text = "Customer Name";
            this.Label4.Top = 1.18F;
            this.Label4.Width = 2.24F;
            // 
            // Label5
            // 
            this.Label5.Height = 0.2F;
            this.Label5.HyperLink = null;
            this.Label5.Left = 0.238F;
            this.Label5.Name = "Label5";
            this.Label5.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: normal; ddo-char-set: 0";
            this.Label5.Text = "Receive Date";
            this.Label5.Top = 1.18F;
            this.Label5.Width = 1F;
            // 
            // Label6
            // 
            this.Label6.Height = 0.2F;
            this.Label6.HyperLink = null;
            this.Label6.Left = 5.645333F;
            this.Label6.Name = "Label6";
            this.Label6.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: normal; text-align: right; d" +
                "do-char-set: 0";
            this.Label6.Text = "Due Amount";
            this.Label6.Top = 1.18F;
            this.Label6.Width = 1F;
            // 
            // label9
            // 
            this.label9.Height = 0.2F;
            this.label9.HyperLink = null;
            this.label9.Left = 4.602333F;
            this.label9.Name = "label9";
            this.label9.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: normal; text-align: right; d" +
                "do-char-set: 0";
            this.label9.Text = "Receive Amount";
            this.label9.Top = 1.18F;
            this.label9.Width = 0.9999999F;
            // 
            // label2
            // 
            this.label2.Height = 0.2F;
            this.label2.HyperLink = null;
            this.label2.Left = 1.3F;
            this.label2.Name = "label2";
            this.label2.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: normal; ddo-char-set: 0";
            this.label2.Text = "Receive No";
            this.label2.Top = 1.18F;
            this.label2.Width = 1F;
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.textBox2,
            this.textBox3,
            this.textBox4,
            this.textBox6});
            this.detail.Height = 0.2291666F;
            this.detail.Name = "detail";
            // 
            // textBox2
            // 
            this.textBox2.DataField = "Customer Name";
            this.textBox2.Height = 0.2F;
            this.textBox2.Left = 2.3F;
            this.textBox2.Name = "textBox2";
            this.textBox2.Style = "font-family: Tahoma; font-size: 8.25pt; ddo-char-set: 0";
            this.textBox2.Text = null;
            this.textBox2.Top = 0F;
            this.textBox2.Width = 2.24F;
            // 
            // textBox3
            // 
            this.textBox3.DataField = "Receive Amount";
            this.textBox3.Height = 0.2F;
            this.textBox3.Left = 4.58F;
            this.textBox3.Name = "textBox3";
            this.textBox3.Style = "font-family: Tahoma; font-size: 8.25pt; text-align: right; ddo-char-set: 0";
            this.textBox3.Text = null;
            this.textBox3.Top = 0F;
            this.textBox3.Width = 0.9999999F;
            // 
            // textBox4
            // 
            this.textBox4.DataField = "Due Amount";
            this.textBox4.Height = 0.2F;
            this.textBox4.Left = 5.623F;
            this.textBox4.Name = "textBox4";
            this.textBox4.Style = "font-family: Tahoma; font-size: 8.25pt; text-align: right; ddo-char-set: 0";
            this.textBox4.Text = null;
            this.textBox4.Top = 0F;
            this.textBox4.Width = 1F;
            // 
            // textBox6
            // 
            this.textBox6.DataField = "ReferenceNo";
            this.textBox6.Height = 0.2F;
            this.textBox6.Left = 1.3F;
            this.textBox6.Name = "textBox6";
            this.textBox6.Style = "font-family: Tahoma; font-size: 8.25pt; ddo-char-set: 0";
            this.textBox6.Text = null;
            this.textBox6.Top = 0F;
            this.textBox6.Width = 1F;
            // 
            // textBox1
            // 
            this.textBox1.DataField = "Receive Date";
            this.textBox1.Height = 0.2F;
            this.textBox1.Left = 0.2381664F;
            this.textBox1.Name = "textBox1";
            this.textBox1.Style = "font-family: Tahoma; font-size: 8.25pt; ddo-char-set: 0";
            this.textBox1.Text = null;
            this.textBox1.Top = 0.02000012F;
            this.textBox1.Width = 1F;
            // 
            // pageFooter
            // 
            this.pageFooter.Height = 0.02083333F;
            this.pageFooter.Name = "pageFooter";
            // 
            // textBox5
            // 
            this.textBox5.DataField = "Due Amount";
            this.textBox5.Height = 0.2F;
            this.textBox5.Left = 5.623333F;
            this.textBox5.Name = "textBox5";
            this.textBox5.OutputFormat = resources.GetString("textBox5.OutputFormat");
            this.textBox5.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: bold; text-align: right; ddo" +
                "-char-set: 0";
            this.textBox5.SummaryRunning = DataDynamics.ActiveReports.SummaryRunning.All;
            this.textBox5.SummaryType = DataDynamics.ActiveReports.SummaryType.PageTotal;
            this.textBox5.Text = null;
            this.textBox5.Top = 0.03F;
            this.textBox5.Width = 1F;
            // 
            // label1
            // 
            this.label1.Height = 0.2F;
            this.label1.HyperLink = null;
            this.label1.Left = 4.580333F;
            this.label1.Name = "label1";
            this.label1.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: bold; text-align: right; ddo" +
                "-char-set: 0";
            this.label1.Text = "Total";
            this.label1.Top = 0.03F;
            this.label1.Width = 1F;
            // 
            // groupHeader1
            // 
            this.groupHeader1.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.textBox1});
            this.groupHeader1.DataField = "Receive Date";
            this.groupHeader1.Height = 0.25F;
            this.groupHeader1.Name = "groupHeader1";
            // 
            // groupFooter1
            // 
            this.groupFooter1.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.textBox5,
            this.label1});
            this.groupFooter1.Height = 0.25F;
            this.groupFooter1.Name = "groupFooter1";
            // 
            // rptDailyCasReceive
            // 
            this.MasterReport = false;
            this.PageSettings.Margins.Left = 0.5F;
            this.PageSettings.Margins.Right = 0.5F;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.PrintWidth = 6.697917F;
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
            ((System.ComponentModel.ISupportInitialize)(this.Label4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.TextBox TextBox7;
        private DataDynamics.ActiveReports.ReportInfo ReportInfo1;
        private DataDynamics.ActiveReports.Shape Shape1;
        private DataDynamics.ActiveReports.Label Label4;
        private DataDynamics.ActiveReports.Label Label5;
        private DataDynamics.ActiveReports.Label Label6;
        private DataDynamics.ActiveReports.Label label9;
        private DataDynamics.ActiveReports.TextBox textBox1;
        private DataDynamics.ActiveReports.TextBox textBox2;
        private DataDynamics.ActiveReports.TextBox textBox3;
        private DataDynamics.ActiveReports.TextBox textBox4;
        public DataDynamics.ActiveReports.TextBox txtPharmacyName;
        public DataDynamics.ActiveReports.TextBox txtAddress;
        private DataDynamics.ActiveReports.TextBox textBox5;
        private DataDynamics.ActiveReports.Label label1;
        private DataDynamics.ActiveReports.GroupHeader groupHeader1;
        private DataDynamics.ActiveReports.GroupFooter groupFooter1;
        private DataDynamics.ActiveReports.Label label2;
        private DataDynamics.ActiveReports.TextBox textBox6;
    }
}
