namespace Tiles_Inventory.Reports
{
    /// <summary>
    /// Summary description for rptProductReceive.
    /// </summary>
    partial class rptProductReceive
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(rptProductReceive));
            this.pageHeader = new DataDynamics.ActiveReports.PageHeader();
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.pageFooter = new DataDynamics.ActiveReports.PageFooter();
            this.Shape1 = new DataDynamics.ActiveReports.Shape();
            this.label3 = new DataDynamics.ActiveReports.Label();
            this.Label1 = new DataDynamics.ActiveReports.Label();
            this.label6 = new DataDynamics.ActiveReports.Label();
            this.label7 = new DataDynamics.ActiveReports.Label();
            this.textBox1 = new DataDynamics.ActiveReports.TextBox();
            this.textBox2 = new DataDynamics.ActiveReports.TextBox();
            this.txtReceiveAmount = new DataDynamics.ActiveReports.TextBox();
            this.textBox5 = new DataDynamics.ActiveReports.TextBox();
            this.line1 = new DataDynamics.ActiveReports.Line();
            this.TextBox7 = new DataDynamics.ActiveReports.TextBox();
            this.txtPharmacyName = new DataDynamics.ActiveReports.TextBox();
            this.txtAddress = new DataDynamics.ActiveReports.TextBox();
            this.txtFromDate = new DataDynamics.ActiveReports.TextBox();
            this.txtToDate = new DataDynamics.ActiveReports.TextBox();
            this.Label4 = new DataDynamics.ActiveReports.Label();
            this.Label5 = new DataDynamics.ActiveReports.Label();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiveAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPharmacyName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.Shape1,
            this.label3,
            this.Label1,
            this.label6,
            this.label7,
            this.TextBox7,
            this.txtPharmacyName,
            this.txtAddress,
            this.txtFromDate,
            this.txtToDate,
            this.Label4,
            this.Label5});
            this.pageHeader.Height = 1.6875F;
            this.pageHeader.Name = "pageHeader";
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.textBox1,
            this.textBox2,
            this.txtReceiveAmount,
            this.textBox5,
            this.line1});
            this.detail.Height = 0.1869445F;
            this.detail.Name = "detail";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = 0.25F;
            this.pageFooter.Name = "pageFooter";
            // 
            // Shape1
            // 
            this.Shape1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.Shape1.Height = 0.2390001F;
            this.Shape1.Left = 0.598F;
            this.Shape1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Shape1.Name = "Shape1";
            this.Shape1.RoundingRadius = 9.999999F;
            this.Shape1.Top = 1.44F;
            this.Shape1.Width = 5.981958F;
            // 
            // label3
            // 
            this.label3.Height = 0.2F;
            this.label3.HyperLink = null;
            this.label3.Left = 1.583723F;
            this.label3.Name = "label3";
            this.label3.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; text-align: left; ddo-cha" +
    "r-set: 0";
            this.label3.Text = "Product Name";
            this.label3.Top = 1.4495F;
            this.label3.Width = 2.584242F;
            // 
            // Label1
            // 
            this.Label1.Height = 0.2F;
            this.Label1.HyperLink = null;
            this.Label1.Left = 5.241563F;
            this.Label1.Name = "Label1";
            this.Label1.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; text-align: center; ddo-c" +
    "har-set: 0";
            this.Label1.Text = "Receive Quantity";
            this.Label1.Top = 1.4495F;
            this.Label1.Width = 1.267917F;
            // 
            // label6
            // 
            this.label6.Height = 0.2F;
            this.label6.HyperLink = null;
            this.label6.Left = 0.6819247F;
            this.label6.Name = "label6";
            this.label6.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; text-align: left; ddo-cha" +
    "r-set: 0";
            this.label6.Text = "Date";
            this.label6.Top = 1.4495F;
            this.label6.Width = 0.86F;
            // 
            // label7
            // 
            this.label7.Height = 0.2F;
            this.label7.HyperLink = null;
            this.label7.Left = 4.209764F;
            this.label7.Name = "label7";
            this.label7.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; text-align: center; ddo-c" +
    "har-set: 0";
            this.label7.Text = "Used Quantity";
            this.label7.Top = 1.4495F;
            this.label7.Width = 0.99F;
            // 
            // textBox1
            // 
            this.textBox1.DataField = "Date";
            this.textBox1.Height = 0.16F;
            this.textBox1.Left = 0.6819247F;
            this.textBox1.Name = "textBox1";
            this.textBox1.Style = "font-family: Tahoma; font-size: 8.25pt; text-align: left; ddo-char-set: 0";
            this.textBox1.Text = null;
            this.textBox1.Top = 0F;
            this.textBox1.Width = 0.86F;
            // 
            // textBox2
            // 
            this.textBox2.DataField = "ProuctName";
            this.textBox2.Height = 0.16F;
            this.textBox2.Left = 1.583723F;
            this.textBox2.Name = "textBox2";
            this.textBox2.Style = "font-family: Tahoma; font-size: 8.25pt; text-align: left; ddo-char-set: 0";
            this.textBox2.Text = null;
            this.textBox2.Top = 0F;
            this.textBox2.Width = 2.584242F;
            // 
            // txtReceiveAmount
            // 
            this.txtReceiveAmount.DataField = "UsedQty";
            this.txtReceiveAmount.Height = 0.16F;
            this.txtReceiveAmount.Left = 4.209764F;
            this.txtReceiveAmount.Name = "txtReceiveAmount";
            this.txtReceiveAmount.Style = "font-family: Tahoma; font-size: 8.25pt; text-align: right; ddo-char-set: 0";
            this.txtReceiveAmount.Text = null;
            this.txtReceiveAmount.Top = 1.192093E-07F;
            this.txtReceiveAmount.Width = 0.99F;
            // 
            // textBox5
            // 
            this.textBox5.DataField = "ReceivedQty";
            this.textBox5.Height = 0.16F;
            this.textBox5.Left = 5.241563F;
            this.textBox5.Name = "textBox5";
            this.textBox5.Style = "font-family: Tahoma; font-size: 8.25pt; text-align: right; ddo-char-set: 0";
            this.textBox5.Text = null;
            this.textBox5.Top = 1.192093E-07F;
            this.textBox5.Width = 1.267917F;
            // 
            // line1
            // 
            this.line1.Height = 0F;
            this.line1.Left = 0.632F;
            this.line1.LineStyle = DataDynamics.ActiveReports.LineStyle.Dash;
            this.line1.LineWeight = 1F;
            this.line1.Name = "line1";
            this.line1.Top = 0.18F;
            this.line1.Width = 5.929005F;
            this.line1.X1 = 0.632F;
            this.line1.X2 = 6.561005F;
            this.line1.Y1 = 0.18F;
            this.line1.Y2 = 0.18F;
            // 
            // TextBox7
            // 
            this.TextBox7.Height = 0.2F;
            this.TextBox7.Left = 1.98625F;
            this.TextBox7.Name = "TextBox7";
            this.TextBox7.Style = "font-family: Tahoma; font-size: 12pt; font-weight: normal; text-align: center; dd" +
    "o-char-set: 0";
            this.TextBox7.Text = "Rrceive And Consumed";
            this.TextBox7.Top = 0F;
            this.TextBox7.Width = 2.84F;
            // 
            // txtPharmacyName
            // 
            this.txtPharmacyName.Height = 0.2F;
            this.txtPharmacyName.Left = 1.98625F;
            this.txtPharmacyName.Name = "txtPharmacyName";
            this.txtPharmacyName.Style = "font-family: Tahoma; font-size: 11.25pt; text-align: center";
            this.txtPharmacyName.Text = null;
            this.txtPharmacyName.Top = 0.22F;
            this.txtPharmacyName.Width = 2.84F;
            // 
            // txtAddress
            // 
            this.txtAddress.Height = 0.2F;
            this.txtAddress.Left = 1.480937F;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Style = "font-family: Tahoma; font-size: 9.75pt; text-align: center";
            this.txtAddress.Text = null;
            this.txtAddress.Top = 0.4840001F;
            this.txtAddress.Width = 3.850625F;
            // 
            // txtFromDate
            // 
            this.txtFromDate.Height = 0.2F;
            this.txtFromDate.Left = 2.464792F;
            this.txtFromDate.Name = "txtFromDate";
            this.txtFromDate.OutputFormat = resources.GetString("txtFromDate.OutputFormat");
            this.txtFromDate.Style = "font-weight: bold; ddo-char-set: 1";
            this.txtFromDate.Text = null;
            this.txtFromDate.Top = 0.788F;
            this.txtFromDate.Width = 1F;
            // 
            // txtToDate
            // 
            this.txtToDate.Height = 0.2F;
            this.txtToDate.Left = 3.836751F;
            this.txtToDate.Name = "txtToDate";
            this.txtToDate.OutputFormat = resources.GetString("txtToDate.OutputFormat");
            this.txtToDate.Style = "font-weight: bold; ddo-char-set: 1";
            this.txtToDate.Text = "TextBox1";
            this.txtToDate.Top = 0.788F;
            this.txtToDate.Width = 1F;
            // 
            // Label4
            // 
            this.Label4.Height = 0.2F;
            this.Label4.HyperLink = null;
            this.Label4.Left = 1.975749F;
            this.Label4.Name = "Label4";
            this.Label4.Style = "font-weight: bold; ddo-char-set: 1";
            this.Label4.Text = "From :";
            this.Label4.Top = 0.788F;
            this.Label4.Width = 0.4599169F;
            // 
            // Label5
            // 
            this.Label5.Height = 0.2F;
            this.Label5.HyperLink = null;
            this.Label5.Left = 3.477666F;
            this.Label5.Name = "Label5";
            this.Label5.Style = "font-weight: bold; ddo-char-set: 1";
            this.Label5.Text = "To  :";
            this.Label5.Top = 0.788F;
            this.Label5.Width = 0.3299164F;
            // 
            // rptProductReceive
            // 
            this.MasterReport = false;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.PrintWidth = 6.8125F;
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
            ((System.ComponentModel.ISupportInitialize)(this.Label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiveAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPharmacyName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.Shape Shape1;
        private DataDynamics.ActiveReports.Label label3;
        private DataDynamics.ActiveReports.Label Label1;
        private DataDynamics.ActiveReports.Label label6;
        private DataDynamics.ActiveReports.Label label7;
        private DataDynamics.ActiveReports.TextBox textBox1;
        private DataDynamics.ActiveReports.TextBox textBox2;
        private DataDynamics.ActiveReports.TextBox txtReceiveAmount;
        private DataDynamics.ActiveReports.TextBox textBox5;
        private DataDynamics.ActiveReports.Line line1;
        private DataDynamics.ActiveReports.TextBox TextBox7;
        public DataDynamics.ActiveReports.TextBox txtFromDate;
        public DataDynamics.ActiveReports.TextBox txtToDate;
        private DataDynamics.ActiveReports.Label Label4;
        private DataDynamics.ActiveReports.Label Label5;
        public DataDynamics.ActiveReports.TextBox txtPharmacyName;
        public DataDynamics.ActiveReports.TextBox txtAddress;
    }
}
