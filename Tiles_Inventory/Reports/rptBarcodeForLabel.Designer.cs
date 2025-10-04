namespace Tiles_Inventory.Reports
{
    /// <summary>
    /// Summary description for rptBarcodeForLabel.
    /// </summary>
    partial class rptBarcodeForLabel
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(rptBarcodeForLabel));
            this.pageHeader = new DataDynamics.ActiveReports.PageHeader();
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.txtProductName1 = new DataDynamics.ActiveReports.TextBox();
            this.txtProductName2 = new DataDynamics.ActiveReports.TextBox();
            this.txtProductName3 = new DataDynamics.ActiveReports.TextBox();
            this.txtProductName4 = new DataDynamics.ActiveReports.TextBox();
            this.txtProductName5 = new DataDynamics.ActiveReports.TextBox();
            this.line1 = new DataDynamics.ActiveReports.Line();
            this.barcode1 = new DataDynamics.ActiveReports.Barcode();
            this.barcode2 = new DataDynamics.ActiveReports.Barcode();
            this.barcode3 = new DataDynamics.ActiveReports.Barcode();
            this.barcode4 = new DataDynamics.ActiveReports.Barcode();
            this.barcode5 = new DataDynamics.ActiveReports.Barcode();
            this.pageFooter = new DataDynamics.ActiveReports.PageFooter();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductName1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductName2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductName3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductName4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductName5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = 0F;
            this.pageHeader.Name = "pageHeader";
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.txtProductName1,
            this.txtProductName2,
            this.txtProductName3,
            this.txtProductName4,
            this.txtProductName5,
            this.line1,
            this.barcode1,
            this.barcode2,
            this.barcode3,
            this.barcode4,
            this.barcode5});
            this.detail.Height = 0.9525F;
            this.detail.Name = "detail";
            this.detail.Format += new System.EventHandler(this.detail_Format);
            // 
            // txtProductName1
            // 
            this.txtProductName1.DataField = "ProductName1";
            this.txtProductName1.Height = 0.21F;
            this.txtProductName1.Left = 0.1162086F;
            this.txtProductName1.Name = "txtProductName1";
            this.txtProductName1.Style = "font-family: Tahoma; font-size: 6.75pt; text-align: center; ddo-char-set: 0";
            this.txtProductName1.Text = null;
            this.txtProductName1.Top = 0F;
            this.txtProductName1.Width = 1.469F;
            // 
            // txtProductName2
            // 
            this.txtProductName2.DataField = "ProductName1";
            this.txtProductName2.Height = 0.21F;
            this.txtProductName2.Left = 1.689209F;
            this.txtProductName2.Name = "txtProductName2";
            this.txtProductName2.Style = "font-family: Tahoma; font-size: 6.75pt; text-align: center; ddo-char-set: 0";
            this.txtProductName2.Text = null;
            this.txtProductName2.Top = 0.02F;
            this.txtProductName2.Width = 1.469F;
            // 
            // txtProductName3
            // 
            this.txtProductName3.DataField = "ProductName1";
            this.txtProductName3.Height = 0.21F;
            this.txtProductName3.Left = 3.261209F;
            this.txtProductName3.Name = "txtProductName3";
            this.txtProductName3.Style = "font-family: Tahoma; font-size: 6.75pt; text-align: center; ddo-char-set: 0";
            this.txtProductName3.Text = null;
            this.txtProductName3.Top = 0F;
            this.txtProductName3.Width = 1.469F;
            // 
            // txtProductName4
            // 
            this.txtProductName4.DataField = "ProductName1";
            this.txtProductName4.Height = 0.21F;
            this.txtProductName4.Left = 4.863208F;
            this.txtProductName4.Name = "txtProductName4";
            this.txtProductName4.Style = "font-family: Tahoma; font-size: 6.75pt; text-align: center; ddo-char-set: 0";
            this.txtProductName4.Text = null;
            this.txtProductName4.Top = 0F;
            this.txtProductName4.Width = 1.469F;
            // 
            // txtProductName5
            // 
            this.txtProductName5.DataField = "ProductName1";
            this.txtProductName5.Height = 0.21F;
            this.txtProductName5.Left = 6.425209F;
            this.txtProductName5.Name = "txtProductName5";
            this.txtProductName5.Style = "font-family: Tahoma; font-size: 6.75pt; text-align: center; ddo-char-set: 0";
            this.txtProductName5.Text = null;
            this.txtProductName5.Top = 0F;
            this.txtProductName5.Width = 1.469F;
            // 
            // line1
            // 
            this.line1.Height = 0F;
            this.line1.Left = 0.116F;
            this.line1.LineStyle = DataDynamics.ActiveReports.LineStyle.Dash;
            this.line1.LineWeight = 1F;
            this.line1.Name = "line1";
            this.line1.Top = 0.798F;
            this.line1.Width = 7.763882F;
            this.line1.X1 = 0.116F;
            this.line1.X2 = 7.879882F;
            this.line1.Y1 = 0.798F;
            this.line1.Y2 = 0.798F;
            // 
            // barcode1
            // 
            this.barcode1.CheckSumEnabled = false;
            this.barcode1.DataField = "ProductCode1";
            this.barcode1.Font = new System.Drawing.Font("Courier New", 8F);
            this.barcode1.Height = 0.5F;
            this.barcode1.Left = 0.1162091F;
            this.barcode1.Name = "barcode1";
            this.barcode1.Style = DataDynamics.ActiveReports.BarCodeStyle.Ansi39x;
            this.barcode1.Text = "barcode1";
            this.barcode1.Top = 0.2299999F;
            this.barcode1.Width = 1.469F;
            // 
            // barcode2
            // 
            this.barcode2.CheckSumEnabled = false;
            this.barcode2.DataField = "ProductCode1";
            this.barcode2.Font = new System.Drawing.Font("Courier New", 8F);
            this.barcode2.Height = 0.5F;
            this.barcode2.Left = 1.689209F;
            this.barcode2.Name = "barcode2";
            this.barcode2.Style = DataDynamics.ActiveReports.BarCodeStyle.Ansi39x;
            this.barcode2.Text = "barcode1";
            this.barcode2.Top = 0.23F;
            this.barcode2.Width = 1.469F;
            // 
            // barcode3
            // 
            this.barcode3.CheckSumEnabled = false;
            this.barcode3.DataField = "ProductCode1";
            this.barcode3.Font = new System.Drawing.Font("Courier New", 8F);
            this.barcode3.Height = 0.5F;
            this.barcode3.Left = 3.261209F;
            this.barcode3.Name = "barcode3";
            this.barcode3.Style = DataDynamics.ActiveReports.BarCodeStyle.Ansi39x;
            this.barcode3.Text = "barcode1";
            this.barcode3.Top = 0.2299999F;
            this.barcode3.Width = 1.469F;
            // 
            // barcode4
            // 
            this.barcode4.CheckSumEnabled = false;
            this.barcode4.DataField = "ProductCode1";
            this.barcode4.Font = new System.Drawing.Font("Courier New", 8F);
            this.barcode4.Height = 0.5F;
            this.barcode4.Left = 4.863208F;
            this.barcode4.Name = "barcode4";
            this.barcode4.Style = DataDynamics.ActiveReports.BarCodeStyle.Ansi39x;
            this.barcode4.Text = "barcode1";
            this.barcode4.Top = 0.23F;
            this.barcode4.Width = 1.469F;
            // 
            // barcode5
            // 
            this.barcode5.CheckSumEnabled = false;
            this.barcode5.DataField = "ProductCode1";
            this.barcode5.Font = new System.Drawing.Font("Courier New", 8F);
            this.barcode5.Height = 0.5F;
            this.barcode5.Left = 6.425209F;
            this.barcode5.Name = "barcode5";
            this.barcode5.Style = DataDynamics.ActiveReports.BarCodeStyle.Ansi39x;
            this.barcode5.Text = "barcode1";
            this.barcode5.Top = 0.2299999F;
            this.barcode5.Width = 1.469F;
            // 
            // pageFooter
            // 
            this.pageFooter.Height = 0.1979167F;
            this.pageFooter.Name = "pageFooter";
            // 
            // rptBarcodeForLabel
            // 
            this.MasterReport = false;
            this.PageSettings.Margins.Left = 0.25F;
            this.PageSettings.Margins.Right = 0.25F;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.PrintWidth = 8.010417F;
            this.Sections.Add(this.pageHeader);
            this.Sections.Add(this.detail);
            this.Sections.Add(this.pageFooter);
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" +
            "l; font-size: 10pt; color: Black", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold", "Heading1", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" +
            "lic", "Heading2", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold", "Heading3", "Normal"));
            ((System.ComponentModel.ISupportInitialize)(this.txtProductName1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductName2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductName3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductName4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductName5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.TextBox txtProductName1;
        private DataDynamics.ActiveReports.TextBox txtProductName2;
        private DataDynamics.ActiveReports.TextBox txtProductName3;
        private DataDynamics.ActiveReports.TextBox txtProductName4;
        private DataDynamics.ActiveReports.TextBox txtProductName5;
        private DataDynamics.ActiveReports.Line line1;
        private DataDynamics.ActiveReports.Barcode barcode1;
        private DataDynamics.ActiveReports.Barcode barcode2;
        private DataDynamics.ActiveReports.Barcode barcode3;
        private DataDynamics.ActiveReports.Barcode barcode4;
        private DataDynamics.ActiveReports.Barcode barcode5;
    }
}
