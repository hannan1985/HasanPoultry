namespace Tiles_Inventory.Reports
{
    /// <summary>
    /// Summary description for rptOrder.
    /// </summary>
    partial class rptOrder
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(rptOrder));
            this.pageHeader = new DataDynamics.ActiveReports.PageHeader();
            this.txtOrderDate = new DataDynamics.ActiveReports.TextBox();
            this.Label2 = new DataDynamics.ActiveReports.Label();
            this.txtOrderBy = new DataDynamics.ActiveReports.TextBox();
            this.Label3 = new DataDynamics.ActiveReports.Label();
            this.Label4 = new DataDynamics.ActiveReports.Label();
            this.Label5 = new DataDynamics.ActiveReports.Label();
            this.Label6 = new DataDynamics.ActiveReports.Label();
            this.TextBox7 = new DataDynamics.ActiveReports.TextBox();
            this.txtPharmacyName = new DataDynamics.ActiveReports.TextBox();
            this.txtAddress = new DataDynamics.ActiveReports.TextBox();
            this.label1 = new DataDynamics.ActiveReports.Label();
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.txtProductName = new DataDynamics.ActiveReports.TextBox();
            this.TextBox5 = new DataDynamics.ActiveReports.TextBox();
            this.TextBox6 = new DataDynamics.ActiveReports.TextBox();
            this.Line1 = new DataDynamics.ActiveReports.Line();
            this.textBox1 = new DataDynamics.ActiveReports.TextBox();
            this.pageFooter = new DataDynamics.ActiveReports.PageFooter();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderBy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPharmacyName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.txtOrderDate,
            this.Label2,
            this.txtOrderBy,
            this.Label3,
            this.Label4,
            this.Label5,
            this.Label6,
            this.TextBox7,
            this.txtPharmacyName,
            this.txtAddress,
            this.label1});
            this.pageHeader.Height = 1.62525F;
            this.pageHeader.Name = "pageHeader";
            // 
            // txtOrderDate
            // 
            this.txtOrderDate.Height = 0.1599999F;
            this.txtOrderDate.Left = 1.740334F;
            this.txtOrderDate.Name = "txtOrderDate";
            this.txtOrderDate.Style = "font-family: Tahoma; font-size: 9pt; ddo-char-set: 0";
            this.txtOrderDate.Text = "TextBox1";
            this.txtOrderDate.Top = 0.794F;
            this.txtOrderDate.Width = 2.05F;
            // 
            // Label2
            // 
            this.Label2.Height = 0.1599999F;
            this.Label2.HyperLink = null;
            this.Label2.Left = 0.9903336F;
            this.Label2.Name = "Label2";
            this.Label2.Style = "font-family: Tahoma; font-size: 9pt; ddo-char-set: 0";
            this.Label2.Text = "Order Date :";
            this.Label2.Top = 0.794F;
            this.Label2.Width = 0.73F;
            // 
            // txtOrderBy
            // 
            this.txtOrderBy.Height = 0.1599999F;
            this.txtOrderBy.Left = 1.740334F;
            this.txtOrderBy.Name = "txtOrderBy";
            this.txtOrderBy.Style = "font-family: Tahoma; font-size: 9pt; ddo-char-set: 0";
            this.txtOrderBy.Text = "TextBox1";
            this.txtOrderBy.Top = 1.044F;
            this.txtOrderBy.Width = 2.05F;
            // 
            // Label3
            // 
            this.Label3.Height = 0.1599999F;
            this.Label3.HyperLink = null;
            this.Label3.Left = 0.9903336F;
            this.Label3.Name = "Label3";
            this.Label3.Style = "font-family: Tahoma; font-size: 9pt; ddo-char-set: 0";
            this.Label3.Text = "Order By :";
            this.Label3.Top = 1.044F;
            this.Label3.Width = 0.73F;
            // 
            // Label4
            // 
            this.Label4.Height = 0.1500001F;
            this.Label4.HyperLink = null;
            this.Label4.Left = 0.9803336F;
            this.Label4.Name = "Label4";
            this.Label4.Style = "font-family: Tahoma; font-size: 8.25pt; ddo-char-set: 0";
            this.Label4.Text = "Product Name";
            this.Label4.Top = 1.429F;
            this.Label4.Width = 1.39F;
            // 
            // Label5
            // 
            this.Label5.Height = 0.1500001F;
            this.Label5.HyperLink = null;
            this.Label5.Left = 3.872334F;
            this.Label5.Name = "Label5";
            this.Label5.Style = "font-family: Tahoma; font-size: 8.25pt; text-align: center; ddo-char-set: 0";
            this.Label5.Text = "Quantity in Box";
            this.Label5.Top = 1.429F;
            this.Label5.Width = 0.8300002F;
            // 
            // Label6
            // 
            this.Label6.Height = 0.1500001F;
            this.Label6.HyperLink = null;
            this.Label6.Left = 4.729334F;
            this.Label6.Name = "Label6";
            this.Label6.Style = "font-family: Tahoma; font-size: 8.25pt; text-align: center; ddo-char-set: 0";
            this.Label6.Text = "Quantity in Pieces ";
            this.Label6.Top = 1.429F;
            this.Label6.Width = 0.9400004F;
            // 
            // TextBox7
            // 
            this.TextBox7.Height = 0.2F;
            this.TextBox7.Left = 1.923907F;
            this.TextBox7.Name = "TextBox7";
            this.TextBox7.Style = "font-family: Tahoma; font-size: 12pt; font-weight: normal; text-align: center; dd" +
    "o-char-set: 0";
            this.TextBox7.Text = "Order Information";
            this.TextBox7.Top = 0.08000001F;
            this.TextBox7.Width = 2.84F;
            // 
            // txtPharmacyName
            // 
            this.txtPharmacyName.Height = 0.2F;
            this.txtPharmacyName.Left = 1.923595F;
            this.txtPharmacyName.Name = "txtPharmacyName";
            this.txtPharmacyName.Style = "font-family: Tahoma; font-size: 11.25pt; text-align: center";
            this.txtPharmacyName.Text = null;
            this.txtPharmacyName.Top = 0.2799997F;
            this.txtPharmacyName.Width = 2.84F;
            // 
            // txtAddress
            // 
            this.txtAddress.Height = 0.2F;
            this.txtAddress.Left = 1.52825F;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Style = "font-family: Tahoma; font-size: 9.75pt; text-align: center";
            this.txtAddress.Text = null;
            this.txtAddress.Top = 0.5239999F;
            this.txtAddress.Width = 3.631F;
            // 
            // label1
            // 
            this.label1.Height = 0.1500001F;
            this.label1.HyperLink = null;
            this.label1.Left = 2.842334F;
            this.label1.Name = "label1";
            this.label1.Style = "font-family: Tahoma; font-size: 8.25pt; text-align: center; ddo-char-set: 0";
            this.label1.Text = "Prodcut Type";
            this.label1.Top = 1.429F;
            this.label1.Width = 0.9699998F;
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.txtProductName,
            this.TextBox5,
            this.TextBox6,
            this.Line1,
            this.textBox1});
            this.detail.Height = 0.22875F;
            this.detail.Name = "detail";
            // 
            // txtProductName
            // 
            this.txtProductName.DataField = "Product Name";
            this.txtProductName.Height = 0.1500001F;
            this.txtProductName.Left = 0.9803336F;
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Style = "font-family: Tahoma; font-size: 8.25pt; ddo-char-set: 0";
            this.txtProductName.Text = null;
            this.txtProductName.Top = 0.01979166F;
            this.txtProductName.Width = 1.8F;
            // 
            // TextBox5
            // 
            this.TextBox5.DataField = "Box";
            this.TextBox5.Height = 0.1500001F;
            this.TextBox5.Left = 3.872334F;
            this.TextBox5.Name = "TextBox5";
            this.TextBox5.Style = "font-family: Tahoma; font-size: 8.25pt; text-align: right; ddo-char-set: 0";
            this.TextBox5.Text = null;
            this.TextBox5.Top = 0.01979166F;
            this.TextBox5.Width = 0.8300002F;
            // 
            // TextBox6
            // 
            this.TextBox6.DataField = "Pieces";
            this.TextBox6.Height = 0.1500001F;
            this.TextBox6.Left = 4.729334F;
            this.TextBox6.Name = "TextBox6";
            this.TextBox6.Style = "font-family: Tahoma; font-size: 8.25pt; text-align: right; ddo-char-set: 0";
            this.TextBox6.Text = null;
            this.TextBox6.Top = 0.01979166F;
            this.TextBox6.Width = 0.9400004F;
            // 
            // Line1
            // 
            this.Line1.Height = 0F;
            this.Line1.Left = 0.9803336F;
            this.Line1.LineWeight = 1F;
            this.Line1.Name = "Line1";
            this.Line1.Top = 0.2F;
            this.Line1.Width = 4.726833F;
            this.Line1.X1 = 0.9803336F;
            this.Line1.X2 = 5.707167F;
            this.Line1.Y1 = 0.2F;
            this.Line1.Y2 = 0.2F;
            // 
            // textBox1
            // 
            this.textBox1.DataField = "Product Type";
            this.textBox1.Height = 0.1500001F;
            this.textBox1.Left = 2.842334F;
            this.textBox1.Name = "textBox1";
            this.textBox1.Style = "font-family: Tahoma; font-size: 8.25pt; text-align: right; ddo-char-set: 0";
            this.textBox1.Text = null;
            this.textBox1.Top = 0.02F;
            this.textBox1.Width = 0.9699998F;
            // 
            // pageFooter
            // 
            this.pageFooter.Height = 0.21875F;
            this.pageFooter.Name = "pageFooter";
            // 
            // rptOrder
            // 
            this.MasterReport = false;
            this.PageSettings.Margins.Right = 0.5F;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.PrintWidth = 6.6875F;
            this.Sections.Add(this.pageHeader);
            this.Sections.Add(this.detail);
            this.Sections.Add(this.pageFooter);
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" +
            "l; font-size: 10pt; color: Black", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold", "Heading1", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" +
            "lic", "Heading2", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold", "Heading3", "Normal"));
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderBy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPharmacyName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.Label Label2;
        private DataDynamics.ActiveReports.Label Label3;
        private DataDynamics.ActiveReports.Label Label4;
        private DataDynamics.ActiveReports.Label Label5;
        private DataDynamics.ActiveReports.Label Label6;
        private DataDynamics.ActiveReports.TextBox TextBox7;
        private DataDynamics.ActiveReports.TextBox txtProductName;
        private DataDynamics.ActiveReports.TextBox TextBox5;
        private DataDynamics.ActiveReports.TextBox TextBox6;
        private DataDynamics.ActiveReports.Line Line1;
        public DataDynamics.ActiveReports.TextBox txtPharmacyName;
        public DataDynamics.ActiveReports.TextBox txtAddress;
        public DataDynamics.ActiveReports.TextBox txtOrderDate;
        public DataDynamics.ActiveReports.TextBox txtOrderBy;
        private DataDynamics.ActiveReports.Label label1;
        private DataDynamics.ActiveReports.TextBox textBox1;
    }
}
