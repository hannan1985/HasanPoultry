namespace Tiles_Inventory.Reports
{
    /// <summary>
    /// Summary description for rptSupplierOutstanding.
    /// </summary>
    partial class rptSupplierOutstanding
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(rptSupplierOutstanding));
            this.pageHeader = new DataDynamics.ActiveReports.PageHeader();
            this.Shape1 = new DataDynamics.ActiveReports.Shape();
            this.Label2 = new DataDynamics.ActiveReports.Label();
            this.Label3 = new DataDynamics.ActiveReports.Label();
            this.TextBox7 = new DataDynamics.ActiveReports.TextBox();
            this.txtPharmacyName = new DataDynamics.ActiveReports.TextBox();
            this.txtAddress = new DataDynamics.ActiveReports.TextBox();
            this.Label1 = new DataDynamics.ActiveReports.Label();
            this.ReportInfo1 = new DataDynamics.ActiveReports.ReportInfo();
            this.Label5 = new DataDynamics.ActiveReports.Label();
            this.label6 = new DataDynamics.ActiveReports.Label();
            this.label7 = new DataDynamics.ActiveReports.Label();
            this.label8 = new DataDynamics.ActiveReports.Label();
            this.TextBox1 = new DataDynamics.ActiveReports.TextBox();
            this.TextBox2 = new DataDynamics.ActiveReports.TextBox();
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.TextBox3 = new DataDynamics.ActiveReports.TextBox();
            this.TextBox5 = new DataDynamics.ActiveReports.TextBox();
            this.txtPaymentDate = new DataDynamics.ActiveReports.TextBox();
            this.txtPaymentAmount = new DataDynamics.ActiveReports.TextBox();
            this.textBox9 = new DataDynamics.ActiveReports.TextBox();
            this.pageFooter = new DataDynamics.ActiveReports.PageFooter();
            this.groupHeader1 = new DataDynamics.ActiveReports.GroupHeader();
            this.groupFooter1 = new DataDynamics.ActiveReports.GroupFooter();
            ((System.ComponentModel.ISupportInitialize)(this.Label2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPharmacyName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportInfo1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPaymentDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPaymentAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.Shape1,
            this.Label2,
            this.Label3,
            this.TextBox7,
            this.txtPharmacyName,
            this.txtAddress,
            this.Label1,
            this.ReportInfo1,
            this.Label5,
            this.label6,
            this.label7,
            this.label8,
            this.TextBox1,
            this.TextBox2});
            this.pageHeader.Height = 1.677417F;
            this.pageHeader.Name = "pageHeader";
            // 
            // Shape1
            // 
            this.Shape1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.Shape1.Height = 0.23F;
            this.Shape1.Left = 0.05F;
            this.Shape1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Shape1.Name = "Shape1";
            this.Shape1.RoundingRadius = 9.999999F;
            this.Shape1.Top = 1.412F;
            this.Shape1.Width = 6.382F;
            // 
            // Label2
            // 
            this.Label2.Height = 0.2F;
            this.Label2.HyperLink = null;
            this.Label2.Left = 0.06500006F;
            this.Label2.Name = "Label2";
            this.Label2.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; ddo-char-set: 0";
            this.Label2.Text = "Supplier Name";
            this.Label2.Top = 1.089F;
            this.Label2.Width = 1.03F;
            // 
            // Label3
            // 
            this.Label3.Height = 0.2F;
            this.Label3.HyperLink = null;
            this.Label3.Left = 1.097F;
            this.Label3.Name = "Label3";
            this.Label3.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: bold; text-align: left; ddo-" +
    "char-set: 1";
            this.Label3.Text = "Description";
            this.Label3.Top = 1.427F;
            this.Label3.Width = 2.135F;
            // 
            // TextBox7
            // 
            this.TextBox7.Height = 0.2F;
            this.TextBox7.Left = 1.956F;
            this.TextBox7.Name = "TextBox7";
            this.TextBox7.Style = "font-family: Tahoma; font-size: 12pt; font-weight: normal; text-align: center; dd" +
    "o-char-set: 0";
            this.TextBox7.Text = "Supplier Due Information.";
            this.TextBox7.Top = 0.02F;
            this.TextBox7.Width = 2.84F;
            // 
            // txtPharmacyName
            // 
            this.txtPharmacyName.Height = 0.2F;
            this.txtPharmacyName.Left = 1.955688F;
            this.txtPharmacyName.Name = "txtPharmacyName";
            this.txtPharmacyName.Style = "font-family: Tahoma; font-size: 11.25pt; text-align: center";
            this.txtPharmacyName.Text = null;
            this.txtPharmacyName.Top = 0.2199999F;
            this.txtPharmacyName.Width = 2.84F;
            // 
            // txtAddress
            // 
            this.txtAddress.Height = 0.2F;
            this.txtAddress.Left = 1.394343F;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Style = "font-family: Tahoma; font-size: 9.75pt; text-align: center";
            this.txtAddress.Text = null;
            this.txtAddress.Top = 0.484F;
            this.txtAddress.Width = 3.961313F;
            // 
            // Label1
            // 
            this.Label1.Height = 0.2F;
            this.Label1.HyperLink = null;
            this.Label1.Left = 0.06500006F;
            this.Label1.Name = "Label1";
            this.Label1.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; ddo-char-set: 0";
            this.Label1.Text = "Company Name ";
            this.Label1.Top = 0.837F;
            this.Label1.Width = 1.03F;
            // 
            // ReportInfo1
            // 
            this.ReportInfo1.FormatString = "{RunDateTime:dd-MM-yyyy h:mm tt}";
            this.ReportInfo1.Height = 0.15625F;
            this.ReportInfo1.Left = 5.122F;
            this.ReportInfo1.Name = "ReportInfo1";
            this.ReportInfo1.Style = "font-family: Tahoma; font-size: 8.25pt; text-align: right; ddo-char-set: 0";
            this.ReportInfo1.Top = 0.02F;
            this.ReportInfo1.Width = 1.262F;
            // 
            // Label5
            // 
            this.Label5.Height = 0.2F;
            this.Label5.HyperLink = null;
            this.Label5.Left = 0.09099997F;
            this.Label5.Name = "Label5";
            this.Label5.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: bold; ddo-char-set: 1";
            this.Label5.Text = "Date";
            this.Label5.Top = 1.427F;
            this.Label5.Width = 0.96F;
            // 
            // label6
            // 
            this.label6.Height = 0.2F;
            this.label6.HyperLink = null;
            this.label6.Left = 3.283F;
            this.label6.Name = "label6";
            this.label6.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: bold; text-align: right; ddo" +
    "-char-set: 1";
            this.label6.Text = "Debit";
            this.label6.Top = 1.427F;
            this.label6.Width = 1.03F;
            // 
            // label7
            // 
            this.label7.Height = 0.2F;
            this.label7.HyperLink = null;
            this.label7.Left = 4.373997F;
            this.label7.Name = "label7";
            this.label7.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: bold; text-align: right; ddo" +
    "-char-set: 1";
            this.label7.Text = "Credit";
            this.label7.Top = 1.427F;
            this.label7.Width = 1.04F;
            // 
            // label8
            // 
            this.label8.Height = 0.2F;
            this.label8.HyperLink = null;
            this.label8.Left = 5.454994F;
            this.label8.Name = "label8";
            this.label8.Style = "font-family: Tahoma; font-size: 8.25pt; font-weight: bold; text-align: right; ddo" +
    "-char-set: 1";
            this.label8.Text = "Due Amount";
            this.label8.Top = 1.427F;
            this.label8.Width = 0.9410001F;
            // 
            // TextBox1
            // 
            this.TextBox1.DataField = "CompanyName";
            this.TextBox1.Height = 0.2F;
            this.TextBox1.Left = 1.135F;
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Style = "font-family: Tahoma; font-size: 9pt; ddo-char-set: 0";
            this.TextBox1.Text = "TextBox1";
            this.TextBox1.Top = 0.837F;
            this.TextBox1.Width = 2.53F;
            // 
            // TextBox2
            // 
            this.TextBox2.DataField = "SupplierName";
            this.TextBox2.Height = 0.2F;
            this.TextBox2.Left = 1.135F;
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.Style = "font-family: Tahoma; font-size: 9pt; ddo-char-set: 0";
            this.TextBox2.Text = null;
            this.TextBox2.Top = 1.089F;
            this.TextBox2.Width = 2.53F;
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.TextBox3,
            this.TextBox5,
            this.txtPaymentDate,
            this.txtPaymentAmount,
            this.textBox9});
            this.detail.Height = 0.236F;
            this.detail.Name = "detail";
            this.detail.Format += new System.EventHandler(this.detail_Format);
            // 
            // TextBox3
            // 
            this.TextBox3.DataField = "Description";
            this.TextBox3.Height = 0.2F;
            this.TextBox3.Left = 1.097F;
            this.TextBox3.Name = "TextBox3";
            this.TextBox3.OutputFormat = resources.GetString("TextBox3.OutputFormat");
            this.TextBox3.Style = "font-family: Tahoma; font-size: 8.25pt; text-align: left; ddo-char-set: 0";
            this.TextBox3.Text = null;
            this.TextBox3.Top = 0.018F;
            this.TextBox3.Width = 2.135F;
            // 
            // TextBox5
            // 
            this.TextBox5.DataField = "Date";
            this.TextBox5.Height = 0.2F;
            this.TextBox5.Left = 0.09099997F;
            this.TextBox5.Name = "TextBox5";
            this.TextBox5.OutputFormat = resources.GetString("TextBox5.OutputFormat");
            this.TextBox5.Style = "font-family: Tahoma; font-size: 8.25pt; ddo-char-set: 0";
            this.TextBox5.Text = null;
            this.TextBox5.Top = 0.018F;
            this.TextBox5.Width = 0.96F;
            // 
            // txtPaymentDate
            // 
            this.txtPaymentDate.DataField = "DrAmount";
            this.txtPaymentDate.Height = 0.2F;
            this.txtPaymentDate.Left = 3.283F;
            this.txtPaymentDate.Name = "txtPaymentDate";
            this.txtPaymentDate.OutputFormat = resources.GetString("txtPaymentDate.OutputFormat");
            this.txtPaymentDate.Style = "font-family: Tahoma; font-size: 8.25pt; text-align: right; ddo-char-set: 0";
            this.txtPaymentDate.Text = null;
            this.txtPaymentDate.Top = 0.018F;
            this.txtPaymentDate.Width = 1.03F;
            // 
            // txtPaymentAmount
            // 
            this.txtPaymentAmount.DataField = "CrAmount";
            this.txtPaymentAmount.Height = 0.2F;
            this.txtPaymentAmount.Left = 4.374F;
            this.txtPaymentAmount.Name = "txtPaymentAmount";
            this.txtPaymentAmount.OutputFormat = resources.GetString("txtPaymentAmount.OutputFormat");
            this.txtPaymentAmount.Style = "font-family: Tahoma; font-size: 8.25pt; text-align: right; ddo-char-set: 0";
            this.txtPaymentAmount.Text = null;
            this.txtPaymentAmount.Top = 0.018F;
            this.txtPaymentAmount.Width = 1.04F;
            // 
            // textBox9
            // 
            this.textBox9.DataField = "Balance";
            this.textBox9.Height = 0.2F;
            this.textBox9.Left = 5.454994F;
            this.textBox9.Name = "textBox9";
            this.textBox9.OutputFormat = resources.GetString("textBox9.OutputFormat");
            this.textBox9.Style = "font-family: Tahoma; font-size: 8.25pt; text-align: right; ddo-char-set: 0";
            this.textBox9.Text = null;
            this.textBox9.Top = 0.018F;
            this.textBox9.Width = 0.9410001F;
            // 
            // pageFooter
            // 
            this.pageFooter.Height = 0.4270833F;
            this.pageFooter.Name = "pageFooter";
            // 
            // groupHeader1
            // 
            this.groupHeader1.Height = 0F;
            this.groupHeader1.Name = "groupHeader1";
            // 
            // groupFooter1
            // 
            this.groupFooter1.Height = 0F;
            this.groupFooter1.Name = "groupFooter1";
            // 
            // rptSupplierOutstanding
            // 
            this.MasterReport = false;
            this.PageSettings.Margins.Left = 0.5F;
            this.PageSettings.Margins.Right = 0.5F;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.PrintWidth = 6.75F;
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
            ((System.ComponentModel.ISupportInitialize)(this.Label2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPharmacyName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportInfo1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPaymentDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPaymentAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.Label Label2;
        private DataDynamics.ActiveReports.Label Label3;
        private DataDynamics.ActiveReports.TextBox TextBox7;
        private DataDynamics.ActiveReports.Label Label1;
        private DataDynamics.ActiveReports.ReportInfo ReportInfo1;
        private DataDynamics.ActiveReports.Label Label5;
        private DataDynamics.ActiveReports.TextBox TextBox1;
        private DataDynamics.ActiveReports.TextBox TextBox2;
        private DataDynamics.ActiveReports.TextBox TextBox3;
        private DataDynamics.ActiveReports.TextBox TextBox5;
        public DataDynamics.ActiveReports.TextBox txtPharmacyName;
        public DataDynamics.ActiveReports.TextBox txtAddress;
        private DataDynamics.ActiveReports.GroupHeader groupHeader1;
        private DataDynamics.ActiveReports.GroupFooter groupFooter1;
        private DataDynamics.ActiveReports.Label label6;
        private DataDynamics.ActiveReports.TextBox txtPaymentDate;
        private DataDynamics.ActiveReports.Label label7;
        private DataDynamics.ActiveReports.Label label8;
        private DataDynamics.ActiveReports.TextBox txtPaymentAmount;
        private DataDynamics.ActiveReports.TextBox textBox9;
        private DataDynamics.ActiveReports.Shape Shape1;
    }
}
