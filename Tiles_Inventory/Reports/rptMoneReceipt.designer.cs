namespace Tiles_Inventory.Reports
{
    /// <summary>
    /// Summary description for rptMoneReceipt.
    /// </summary>
    partial class rptMoneReceipt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptMoneReceipt));
            this.pageHeader = new DataDynamics.ActiveReports.PageHeader();
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.picture1 = new DataDynamics.ActiveReports.Picture();
            this.textBox1 = new DataDynamics.ActiveReports.TextBox();
            this.textBox2 = new DataDynamics.ActiveReports.TextBox();
            this.textBox3 = new DataDynamics.ActiveReports.TextBox();
            this.textBox4 = new DataDynamics.ActiveReports.TextBox();
            this.textBox5 = new DataDynamics.ActiveReports.TextBox();
            this.textBox6 = new DataDynamics.ActiveReports.TextBox();
            this.textBox7 = new DataDynamics.ActiveReports.TextBox();
            this.line1 = new DataDynamics.ActiveReports.Line();
            this.line2 = new DataDynamics.ActiveReports.Line();
            this.txtPharmacyName = new DataDynamics.ActiveReports.TextBox();
            this.picLogo = new DataDynamics.ActiveReports.Picture();
            this.pageFooter = new DataDynamics.ActiveReports.PageFooter();
            this.line3 = new DataDynamics.ActiveReports.Line();
            this.line4 = new DataDynamics.ActiveReports.Line();
            ((System.ComponentModel.ISupportInitialize)(this.picture1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPharmacyName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
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
            this.picture1,
            this.textBox1,
            this.textBox2,
            this.textBox3,
            this.textBox4,
            this.textBox5,
            this.textBox6,
            this.textBox7,
            this.line1,
            this.line2,
            this.txtPharmacyName,
            this.picLogo,
            this.line3,
            this.line4});
            this.detail.Height = 5.2615F;
            this.detail.Name = "detail";
            // 
            // picture1
            // 
            this.picture1.Height = 4.497F;
            this.picture1.ImageData = ((System.IO.Stream)(resources.GetObject("picture1.ImageData")));
            this.picture1.Left = 0.06712532F;
            this.picture1.Name = "picture1";
            this.picture1.SizeMode = DataDynamics.ActiveReports.SizeModes.Stretch;
            this.picture1.Top = 0.046F;
            this.picture1.Width = 8.396084F;
            // 
            // textBox1
            // 
            this.textBox1.DataField = "MemoNumber";
            this.textBox1.Height = 0.2F;
            this.textBox1.Left = 0.7622083F;
            this.textBox1.Name = "textBox1";
            this.textBox1.Style = "font-family: Tahoma; font-size: 9pt; ddo-char-set: 0";
            this.textBox1.Text = "textBox1";
            this.textBox1.Top = 1.15F;
            this.textBox1.Width = 1F;
            // 
            // textBox2
            // 
            this.textBox2.DataField = "ReceiveDate";
            this.textBox2.Height = 0.2F;
            this.textBox2.Left = 6.837F;
            this.textBox2.Name = "textBox2";
            this.textBox2.OutputFormat = resources.GetString("textBox2.OutputFormat");
            this.textBox2.Style = "font-family: Tahoma; font-size: 9pt; ddo-char-set: 0";
            this.textBox2.Text = "textBox1";
            this.textBox2.Top = 1.13F;
            this.textBox2.Width = 1F;
            // 
            // textBox3
            // 
            this.textBox3.DataField = "CustomerName";
            this.textBox3.Height = 0.2F;
            this.textBox3.Left = 2.739F;
            this.textBox3.Name = "textBox3";
            this.textBox3.Style = "font-family: Tahoma; font-size: 9pt; ddo-char-set: 0";
            this.textBox3.Text = "textBox1";
            this.textBox3.Top = 1.522F;
            this.textBox3.Width = 4.208F;
            // 
            // textBox4
            // 
            this.textBox4.DataField = "AmountInWord";
            this.textBox4.Height = 0.2F;
            this.textBox4.Left = 2.103F;
            this.textBox4.Name = "textBox4";
            this.textBox4.Style = "font-family: Tahoma; font-size: 9pt; ddo-char-set: 0";
            this.textBox4.Text = "textBox1";
            this.textBox4.Top = 1.842F;
            this.textBox4.Width = 4.738F;
            // 
            // textBox5
            // 
            this.textBox5.DataField = "BillRefNumber";
            this.textBox5.Height = 0.2F;
            this.textBox5.Left = 4.216F;
            this.textBox5.Name = "textBox5";
            this.textBox5.Style = "font-family: Tahoma; font-size: 9pt; ddo-char-set: 0";
            this.textBox5.Text = "textBox1";
            this.textBox5.Top = 2.929F;
            this.textBox5.Width = 1.375F;
            // 
            // textBox6
            // 
            this.textBox6.DataField = "Amount";
            this.textBox6.Height = 0.2F;
            this.textBox6.Left = 1.306F;
            this.textBox6.Name = "textBox6";
            this.textBox6.OutputFormat = resources.GetString("textBox6.OutputFormat");
            this.textBox6.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; text-align: left; ddo-cha" +
    "r-set: 1";
            this.textBox6.Text = "100";
            this.textBox6.Top = 3.639F;
            this.textBox6.Width = 1.219F;
            // 
            // textBox7
            // 
            this.textBox7.DataField = "ChequeNo";
            this.textBox7.Height = 0.2F;
            this.textBox7.Left = 3.192F;
            this.textBox7.Name = "textBox7";
            this.textBox7.Style = "font-family: Tahoma; font-size: 9pt; ddo-char-set: 0";
            this.textBox7.Text = "dfs";
            this.textBox7.Top = 2.36F;
            this.textBox7.Width = 1F;
            // 
            // line1
            // 
            this.line1.Height = 4.501F;
            this.line1.Left = 0.04720831F;
            this.line1.LineWeight = 1F;
            this.line1.Name = "line1";
            this.line1.Top = 0.04200023F;
            this.line1.Width = 0F;
            this.line1.X1 = 0.04720831F;
            this.line1.X2 = 0.04720831F;
            this.line1.Y1 = 4.543F;
            this.line1.Y2 = 0.04200023F;
            // 
            // line2
            // 
            this.line2.Height = 0F;
            this.line2.Left = 0.04720831F;
            this.line2.LineWeight = 1F;
            this.line2.Name = "line2";
            this.line2.Top = 0.046F;
            this.line2.Width = 8.404009F;
            this.line2.X1 = 0.04720831F;
            this.line2.X2 = 8.451218F;
            this.line2.Y1 = 0.046F;
            this.line2.Y2 = 0.046F;
            // 
            // txtPharmacyName
            // 
            this.txtPharmacyName.Height = 0.2F;
            this.txtPharmacyName.Left = 0.352F;
            this.txtPharmacyName.Name = "txtPharmacyName";
            this.txtPharmacyName.Style = "font-family: Tahoma; font-size: 11.25pt; text-align: center; ddo-char-set: 0";
            this.txtPharmacyName.Text = null;
            this.txtPharmacyName.Top = 0.691F;
            this.txtPharmacyName.Width = 2.84F;
            // 
            // picLogo
            // 
            this.picLogo.Height = 0.581F;
            this.picLogo.HyperLink = null;
            this.picLogo.ImageData = null;
            this.picLogo.Left = 0.36F;
            this.picLogo.Name = "picLogo";
            this.picLogo.SizeMode = DataDynamics.ActiveReports.SizeModes.Stretch;
            this.picLogo.Top = 0.07F;
            this.picLogo.Visible = false;
            this.picLogo.Width = 1.87F;
            // 
            // pageFooter
            // 
            this.pageFooter.Height = 0.25F;
            this.pageFooter.Name = "pageFooter";
            // 
            // line3
            // 
            this.line3.Height = 4.500999F;
            this.line3.Left = 8.463001F;
            this.line3.LineWeight = 1F;
            this.line3.Name = "line3";
            this.line3.Top = 0.04200023F;
            this.line3.Width = 0F;
            this.line3.X1 = 8.463001F;
            this.line3.X2 = 8.463001F;
            this.line3.Y1 = 4.543F;
            this.line3.Y2 = 0.04200023F;
            // 
            // line4
            // 
            this.line4.Height = 0F;
            this.line4.Left = 0.04700001F;
            this.line4.LineWeight = 1F;
            this.line4.Name = "line4";
            this.line4.Top = 4.543F;
            this.line4.Width = 8.404008F;
            this.line4.X1 = 0.04700001F;
            this.line4.X2 = 8.451008F;
            this.line4.Y1 = 4.543F;
            this.line4.Y2 = 4.543F;
            // 
            // rptMoneReceipt
            // 
            this.MasterReport = false;
            this.PageSettings.Margins.Left = 0F;
            this.PageSettings.Margins.Right = 0F;
            this.PageSettings.Margins.Top = 0.25F;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.PrintWidth = 8.510417F;
            this.Sections.Add(this.pageHeader);
            this.Sections.Add(this.detail);
            this.Sections.Add(this.pageFooter);
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" +
            "l; font-size: 10pt; color: Black", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold", "Heading1", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" +
            "lic", "Heading2", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold", "Heading3", "Normal"));
            ((System.ComponentModel.ISupportInitialize)(this.picture1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPharmacyName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.Picture picture1;
        private DataDynamics.ActiveReports.TextBox textBox1;
        private DataDynamics.ActiveReports.TextBox textBox2;
        private DataDynamics.ActiveReports.TextBox textBox3;
        private DataDynamics.ActiveReports.TextBox textBox4;
        private DataDynamics.ActiveReports.TextBox textBox5;
        private DataDynamics.ActiveReports.TextBox textBox6;
        private DataDynamics.ActiveReports.TextBox textBox7;
        private DataDynamics.ActiveReports.Line line1;
        private DataDynamics.ActiveReports.Line line2;
        public DataDynamics.ActiveReports.Picture picLogo;
        public DataDynamics.ActiveReports.TextBox txtPharmacyName;
        private DataDynamics.ActiveReports.Line line3;
        private DataDynamics.ActiveReports.Line line4;
    }
}
