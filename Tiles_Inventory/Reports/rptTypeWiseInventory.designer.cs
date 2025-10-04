namespace Tiles_Inventory.Reports
{
    /// <summary>
    /// Summary description for rptInventory.
    /// </summary>
    partial class rptTypeWiseInventory
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(rptTypeWiseInventory));
            this.pageHeader = new DataDynamics.ActiveReports.PageHeader();
            this.shape1 = new DataDynamics.ActiveReports.Shape();
            this.TextBox7 = new DataDynamics.ActiveReports.TextBox();
            this.txtPharmacyName = new DataDynamics.ActiveReports.TextBox();
            this.txtAddress = new DataDynamics.ActiveReports.TextBox();
            this.Label1 = new DataDynamics.ActiveReports.Label();
            this.Label4 = new DataDynamics.ActiveReports.Label();
            this.ReportInfo1 = new DataDynamics.ActiveReports.ReportInfo();
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.TextBox1 = new DataDynamics.ActiveReports.TextBox();
            this.TextBox4 = new DataDynamics.ActiveReports.TextBox();
            this.pageFooter = new DataDynamics.ActiveReports.PageFooter();
            this.groupHeader1 = new DataDynamics.ActiveReports.GroupHeader();
            this.groupFooter1 = new DataDynamics.ActiveReports.GroupFooter();
            this.line1 = new DataDynamics.ActiveReports.Line();
            this.line2 = new DataDynamics.ActiveReports.Line();
            this.line3 = new DataDynamics.ActiveReports.Line();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPharmacyName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportInfo1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.shape1,
            this.TextBox7,
            this.txtPharmacyName,
            this.txtAddress,
            this.Label1,
            this.Label4,
            this.ReportInfo1});
            this.pageHeader.Height = 1.4645F;
            this.pageHeader.Name = "pageHeader";
            // 
            // shape1
            // 
            this.shape1.BackColor = System.Drawing.Color.Empty;
            this.shape1.Height = 0.1805F;
            this.shape1.Left = 0.8499999F;
            this.shape1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.shape1.Name = "shape1";
            this.shape1.RoundingRadius = 9.999999F;
            this.shape1.Top = 1.284F;
            this.shape1.Width = 5.216667F;
            // 
            // TextBox7
            // 
            this.TextBox7.Height = 0.2F;
            this.TextBox7.Left = 2.037979F;
            this.TextBox7.Name = "TextBox7";
            this.TextBox7.Style = "font-family: Tahoma; font-size: 12pt; font-weight: normal; text-align: center; dd" +
                "o-char-set: 0";
            this.TextBox7.Text = "Inventory Information";
            this.TextBox7.Top = 0.05F;
            this.TextBox7.Width = 2.84F;
            // 
            // txtPharmacyName
            // 
            this.txtPharmacyName.Height = 0.2F;
            this.txtPharmacyName.Left = 2.038F;
            this.txtPharmacyName.Name = "txtPharmacyName";
            this.txtPharmacyName.Style = "font-family: Tahoma; font-size: 11.25pt; text-align: center";
            this.txtPharmacyName.Text = null;
            this.txtPharmacyName.Top = 0.29F;
            this.txtPharmacyName.Width = 2.84F;
            // 
            // txtAddress
            // 
            this.txtAddress.Height = 0.2F;
            this.txtAddress.Left = 1.812979F;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Style = "font-family: Tahoma; font-size: 9pt; text-align: center";
            this.txtAddress.Text = null;
            this.txtAddress.Top = 0.544F;
            this.txtAddress.Width = 3.290708F;
            // 
            // Label1
            // 
            this.Label1.Height = 0.1599999F;
            this.Label1.HyperLink = null;
            this.Label1.Left = 1.131667F;
            this.Label1.Name = "Label1";
            this.Label1.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: bold; ddo-char-set: 1";
            this.Label1.Text = "Product Type";
            this.Label1.Top = 1.295F;
            this.Label1.Width = 2.38F;
            // 
            // Label4
            // 
            this.Label4.Height = 0.1599999F;
            this.Label4.HyperLink = null;
            this.Label4.Left = 5.112667F;
            this.Label4.Name = "Label4";
            this.Label4.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: bold; text-align: center; dd" +
                "o-char-set: 1";
            this.Label4.Text = "Quantity";
            this.Label4.Top = 1.295F;
            this.Label4.Width = 0.6700001F;
            // 
            // ReportInfo1
            // 
            this.ReportInfo1.FormatString = "{RunDateTime:dd-MM-yyyy h:mm tt}";
            this.ReportInfo1.Height = 0.15625F;
            this.ReportInfo1.Left = 4.932011F;
            this.ReportInfo1.Name = "ReportInfo1";
            this.ReportInfo1.Style = "font-family: Tahoma; font-size: 8.25pt; text-align: right; ddo-char-set: 0";
            this.ReportInfo1.Top = 0.072F;
            this.ReportInfo1.Width = 1.392F;
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Height = 0F;
            this.detail.Name = "detail";
            // 
            // TextBox1
            // 
            this.TextBox1.DataField = "ProductType";
            this.TextBox1.Height = 0.1599999F;
            this.TextBox1.Left = 1.132F;
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Style = "font-family: Tahoma; font-size: 8.25pt; ddo-char-set: 0";
            this.TextBox1.Text = null;
            this.TextBox1.Top = 0.007F;
            this.TextBox1.Width = 2.38F;
            // 
            // TextBox4
            // 
            this.TextBox4.DataField = "Quantity";
            this.TextBox4.Height = 0.1599999F;
            this.TextBox4.Left = 5.113F;
            this.TextBox4.Name = "TextBox4";
            this.TextBox4.Style = "font-family: Tahoma; font-size: 8.25pt; text-align: center; ddo-char-set: 0";
            this.TextBox4.SummaryGroup = "groupHeader1";
            this.TextBox4.SummaryType = DataDynamics.ActiveReports.SummaryType.SubTotal;
            this.TextBox4.Text = null;
            this.TextBox4.Top = 0.006999881F;
            this.TextBox4.Width = 0.6700001F;
            // 
            // pageFooter
            // 
            this.pageFooter.Height = 0.25F;
            this.pageFooter.Name = "pageFooter";
            // 
            // groupHeader1
            // 
            this.groupHeader1.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.TextBox1,
            this.TextBox4,
            this.line1,
            this.line2,
            this.line3});
            this.groupHeader1.DataField = "ProductType";
            this.groupHeader1.Height = 0.2545278F;
            this.groupHeader1.Name = "groupHeader1";
            // 
            // groupFooter1
            // 
            this.groupFooter1.Height = 0.01041667F;
            this.groupFooter1.Name = "groupFooter1";
            // 
            // line1
            // 
            this.line1.Height = 0F;
            this.line1.Left = 0.85F;
            this.line1.LineStyle = DataDynamics.ActiveReports.LineStyle.Dash;
            this.line1.LineWeight = 1F;
            this.line1.Name = "line1";
            this.line1.Top = 0.23F;
            this.line1.Width = 5.216667F;
            this.line1.X1 = 0.85F;
            this.line1.X2 = 6.066667F;
            this.line1.Y1 = 0.23F;
            this.line1.Y2 = 0.23F;
            // 
            // line2
            // 
            this.line2.AnchorBottom = true;
            this.line2.Height = 0.2360001F;
            this.line2.Left = 0.8500001F;
            this.line2.LineWeight = 1F;
            this.line2.Name = "line2";
            this.line2.Top = -0.01800002F;
            this.line2.Width = 0F;
            this.line2.X1 = 0.8500001F;
            this.line2.X2 = 0.8500001F;
            this.line2.Y1 = -0.01800002F;
            this.line2.Y2 = 0.2180001F;
            // 
            // line3
            // 
            this.line3.AnchorBottom = true;
            this.line3.Height = 0.2180001F;
            this.line3.Left = 6.067F;
            this.line3.LineWeight = 1F;
            this.line3.Name = "line3";
            this.line3.Top = 3.72529E-09F;
            this.line3.Width = 0F;
            this.line3.X1 = 6.067F;
            this.line3.X2 = 6.067F;
            this.line3.Y1 = 3.72529E-09F;
            this.line3.Y2 = 0.2180001F;
            // 
            // rptTypeWiseInventory
            // 
            this.MasterReport = false;
            this.PageSettings.Margins.Left = 0.5F;
            this.PageSettings.Margins.Right = 0.5F;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.PrintWidth = 6.916667F;
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
            ((System.ComponentModel.ISupportInitialize)(this.Label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportInfo1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.TextBox TextBox7;
        private DataDynamics.ActiveReports.Label Label1;
        private DataDynamics.ActiveReports.Label Label4;
        private DataDynamics.ActiveReports.ReportInfo ReportInfo1;
        private DataDynamics.ActiveReports.TextBox TextBox1;
        private DataDynamics.ActiveReports.TextBox TextBox4;
        public DataDynamics.ActiveReports.TextBox txtPharmacyName;
        public DataDynamics.ActiveReports.TextBox txtAddress;
        private DataDynamics.ActiveReports.GroupHeader groupHeader1;
        private DataDynamics.ActiveReports.GroupFooter groupFooter1;
        private DataDynamics.ActiveReports.Shape shape1;
        private DataDynamics.ActiveReports.Line line1;
        private DataDynamics.ActiveReports.Line line2;
        private DataDynamics.ActiveReports.Line line3;
    }
}
