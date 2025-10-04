namespace Tiles_Inventory.Reports
{
    /// <summary>
    /// Summary description for rptExpenseReport.
    /// </summary>
    partial class rptExpenseReport
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(rptExpenseReport));
            this.pageHeader = new DataDynamics.ActiveReports.PageHeader();
            this.TextBox7 = new DataDynamics.ActiveReports.TextBox();
            this.txtPharmacyName = new DataDynamics.ActiveReports.TextBox();
            this.txtAddress = new DataDynamics.ActiveReports.TextBox();
            this.txtFromDate = new DataDynamics.ActiveReports.TextBox();
            this.txtToDate = new DataDynamics.ActiveReports.TextBox();
            this.Label4 = new DataDynamics.ActiveReports.Label();
            this.Label5 = new DataDynamics.ActiveReports.Label();
            this.ReportInfo1 = new DataDynamics.ActiveReports.ReportInfo();
            this.Shape1 = new DataDynamics.ActiveReports.Shape();
            this.Label1 = new DataDynamics.ActiveReports.Label();
            this.Label2 = new DataDynamics.ActiveReports.Label();
            this.label8 = new DataDynamics.ActiveReports.Label();
            this.Label7 = new DataDynamics.ActiveReports.Label();
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.textBox1 = new DataDynamics.ActiveReports.TextBox();
            this.textBox3 = new DataDynamics.ActiveReports.TextBox();
            this.textBox4 = new DataDynamics.ActiveReports.TextBox();
            this.textBox2 = new DataDynamics.ActiveReports.TextBox();
            this.pageFooter = new DataDynamics.ActiveReports.PageFooter();
            this.textBox5 = new DataDynamics.ActiveReports.TextBox();
            this.label3 = new DataDynamics.ActiveReports.Label();
            this.groupHeader1 = new DataDynamics.ActiveReports.GroupHeader();
            this.groupFooter1 = new DataDynamics.ActiveReports.GroupFooter();
            this.label6 = new DataDynamics.ActiveReports.Label();
            this.textBox6 = new DataDynamics.ActiveReports.TextBox();
            this.groupHeader2 = new DataDynamics.ActiveReports.GroupHeader();
            this.groupFooter2 = new DataDynamics.ActiveReports.GroupFooter();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPharmacyName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportInfo1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.TextBox7,
            this.txtPharmacyName,
            this.txtAddress,
            this.txtFromDate,
            this.txtToDate,
            this.Label4,
            this.Label5,
            this.ReportInfo1,
            this.Shape1,
            this.Label1,
            this.Label2,
            this.label8,
            this.Label7,
            this.label6});
            this.pageHeader.Height = 1.419F;
            this.pageHeader.Name = "pageHeader";
            // 
            // TextBox7
            // 
            this.TextBox7.Height = 0.2F;
            this.TextBox7.Left = 2.091105F;
            this.TextBox7.Name = "TextBox7";
            this.TextBox7.Style = "font-family: Tahoma; font-size: 12pt; font-weight: normal; text-align: center; dd" +
    "o-char-set: 0";
            this.TextBox7.Text = "Expense Detail";
            this.TextBox7.Top = 0.08000001F;
            this.TextBox7.Width = 2.84F;
            // 
            // txtPharmacyName
            // 
            this.txtPharmacyName.Height = 0.2F;
            this.txtPharmacyName.Left = 2.091105F;
            this.txtPharmacyName.Name = "txtPharmacyName";
            this.txtPharmacyName.Style = "font-family: Tahoma; font-size: 11.25pt; text-align: center";
            this.txtPharmacyName.Text = null;
            this.txtPharmacyName.Top = 0.3F;
            this.txtPharmacyName.Width = 2.84F;
            // 
            // txtAddress
            // 
            this.txtAddress.Height = 0.2F;
            this.txtAddress.Left = 1.585104F;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Style = "font-family: Tahoma; font-size: 9.75pt; text-align: center";
            this.txtAddress.Text = null;
            this.txtAddress.Top = 0.5640001F;
            this.txtAddress.Width = 3.850625F;
            // 
            // txtFromDate
            // 
            this.txtFromDate.Height = 0.2F;
            this.txtFromDate.Left = 2.579417F;
            this.txtFromDate.Name = "txtFromDate";
            this.txtFromDate.OutputFormat = resources.GetString("txtFromDate.OutputFormat");
            this.txtFromDate.Style = "font-weight: bold; ddo-char-set: 1";
            this.txtFromDate.Text = "TextBox1";
            this.txtFromDate.Top = 0.798F;
            this.txtFromDate.Width = 1F;
            // 
            // txtToDate
            // 
            this.txtToDate.Height = 0.2F;
            this.txtToDate.Left = 3.940501F;
            this.txtToDate.Name = "txtToDate";
            this.txtToDate.OutputFormat = resources.GetString("txtToDate.OutputFormat");
            this.txtToDate.Style = "font-weight: bold; ddo-char-set: 1";
            this.txtToDate.Text = "TextBox1";
            this.txtToDate.Top = 0.798F;
            this.txtToDate.Width = 1F;
            // 
            // Label4
            // 
            this.Label4.Height = 0.2F;
            this.Label4.HyperLink = null;
            this.Label4.Left = 2.079499F;
            this.Label4.Name = "Label4";
            this.Label4.Style = "font-weight: bold; ddo-char-set: 1";
            this.Label4.Text = "From :";
            this.Label4.Top = 0.798F;
            this.Label4.Width = 0.4599169F;
            // 
            // Label5
            // 
            this.Label5.Height = 0.2F;
            this.Label5.HyperLink = null;
            this.Label5.Left = 3.581416F;
            this.Label5.Name = "Label5";
            this.Label5.Style = "font-weight: bold; ddo-char-set: 1";
            this.Label5.Text = "To  :";
            this.Label5.Top = 0.798F;
            this.Label5.Width = 0.3299164F;
            // 
            // ReportInfo1
            // 
            this.ReportInfo1.FormatString = "{RunDateTime:dd-MM-yyyy h:mm tt}";
            this.ReportInfo1.Height = 0.15625F;
            this.ReportInfo1.Left = 5.458F;
            this.ReportInfo1.Name = "ReportInfo1";
            this.ReportInfo1.Style = "text-align: right";
            this.ReportInfo1.Top = 0F;
            this.ReportInfo1.Width = 1.392F;
            // 
            // Shape1
            // 
            this.Shape1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.Shape1.Height = 0.2390001F;
            this.Shape1.Left = 0F;
            this.Shape1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Shape1.Name = "Shape1";
            this.Shape1.RoundingRadius = 9.999999F;
            this.Shape1.Top = 1.18F;
            this.Shape1.Width = 6.85F;
            // 
            // Label1
            // 
            this.Label1.Height = 0.2F;
            this.Label1.HyperLink = null;
            this.Label1.Left = 2.055F;
            this.Label1.Name = "Label1";
            this.Label1.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; text-align: left; ddo-cha" +
    "r-set: 0";
            this.Label1.Text = "Employee Name";
            this.Label1.Top = 1.199F;
            this.Label1.Width = 1.55F;
            // 
            // Label2
            // 
            this.Label2.Height = 0.2F;
            this.Label2.HyperLink = null;
            this.Label2.Left = 3.63F;
            this.Label2.Name = "Label2";
            this.Label2.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; text-align: left; ddo-cha" +
    "r-set: 0";
            this.Label2.Text = "Description";
            this.Label2.Top = 1.199F;
            this.Label2.Width = 2.4F;
            // 
            // label8
            // 
            this.label8.Height = 0.2F;
            this.label8.HyperLink = null;
            this.label8.Left = 6.051F;
            this.label8.Name = "label8";
            this.label8.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; text-align: right; ddo-ch" +
    "ar-set: 0";
            this.label8.Text = "Amount";
            this.label8.Top = 1.199F;
            this.label8.Width = 0.7990003F;
            // 
            // Label7
            // 
            this.Label7.Height = 0.2F;
            this.Label7.HyperLink = null;
            this.Label7.Left = 0F;
            this.Label7.Name = "Label7";
            this.Label7.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; text-align: left; ddo-cha" +
    "r-set: 0";
            this.Label7.Text = "Expense Date";
            this.Label7.Top = 1.199F;
            this.Label7.Width = 1.01F;
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.textBox1,
            this.textBox3,
            this.textBox4});
            this.detail.Height = 0.2208335F;
            this.detail.Name = "detail";
            // 
            // textBox1
            // 
            this.textBox1.DataField = "Employee Name";
            this.textBox1.Height = 0.2F;
            this.textBox1.Left = 2.055F;
            this.textBox1.Name = "textBox1";
            this.textBox1.Style = "font-family: Tahoma; font-size: 9pt; text-align: left; ddo-char-set: 0";
            this.textBox1.Text = null;
            this.textBox1.Top = 0F;
            this.textBox1.Width = 1.55F;
            // 
            // textBox3
            // 
            this.textBox3.DataField = "Description";
            this.textBox3.Height = 0.2F;
            this.textBox3.Left = 3.63F;
            this.textBox3.Name = "textBox3";
            this.textBox3.Style = "font-family: Tahoma; font-size: 9pt; text-align: left; ddo-char-set: 0";
            this.textBox3.Text = null;
            this.textBox3.Top = 2.384186E-07F;
            this.textBox3.Width = 2.4F;
            // 
            // textBox4
            // 
            this.textBox4.DataField = "Amount";
            this.textBox4.Height = 0.2F;
            this.textBox4.Left = 6.051F;
            this.textBox4.Name = "textBox4";
            this.textBox4.OutputFormat = resources.GetString("textBox4.OutputFormat");
            this.textBox4.Style = "font-family: Tahoma; font-size: 9pt; text-align: right; ddo-char-set: 0";
            this.textBox4.Text = null;
            this.textBox4.Top = 1.192093E-07F;
            this.textBox4.Width = 0.7990003F;
            // 
            // textBox2
            // 
            this.textBox2.DataField = "Expense Date";
            this.textBox2.Height = 0.2F;
            this.textBox2.Left = 0F;
            this.textBox2.Name = "textBox2";
            this.textBox2.Style = "font-family: Tahoma; font-size: 9pt; text-align: left; ddo-char-set: 0";
            this.textBox2.Text = null;
            this.textBox2.Top = 0F;
            this.textBox2.Width = 1.01F;
            // 
            // pageFooter
            // 
            this.pageFooter.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.textBox5,
            this.label3});
            this.pageFooter.Height = 0.325F;
            this.pageFooter.Name = "pageFooter";
            // 
            // textBox5
            // 
            this.textBox5.DataField = "Amount";
            this.textBox5.Height = 0.2F;
            this.textBox5.Left = 5.821F;
            this.textBox5.Name = "textBox5";
            this.textBox5.OutputFormat = resources.GetString("textBox5.OutputFormat");
            this.textBox5.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; text-align: right; ddo-ch" +
    "ar-set: 1";
            this.textBox5.SummaryGroup = "groupHeader1";
            this.textBox5.SummaryRunning = DataDynamics.ActiveReports.SummaryRunning.All;
            this.textBox5.SummaryType = DataDynamics.ActiveReports.SummaryType.GrandTotal;
            this.textBox5.Text = null;
            this.textBox5.Top = 0F;
            this.textBox5.Width = 1.029417F;
            // 
            // label3
            // 
            this.label3.Height = 0.2F;
            this.label3.HyperLink = null;
            this.label3.Left = 4.620001F;
            this.label3.Name = "label3";
            this.label3.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; text-align: right; ddo-ch" +
    "ar-set: 1";
            this.label3.Text = "Total";
            this.label3.Top = 0F;
            this.label3.Width = 1.16F;
            // 
            // groupHeader1
            // 
            this.groupHeader1.DataField = "Expense Date";
            this.groupHeader1.Height = 0F;
            this.groupHeader1.Name = "groupHeader1";
            // 
            // groupFooter1
            // 
            this.groupFooter1.Height = 0F;
            this.groupFooter1.Name = "groupFooter1";
            // 
            // label6
            // 
            this.label6.Height = 0.2F;
            this.label6.HyperLink = null;
            this.label6.Left = 1.045F;
            this.label6.Name = "label6";
            this.label6.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; text-align: left; ddo-cha" +
    "r-set: 0";
            this.label6.Text = "Expense Type";
            this.label6.Top = 1.199F;
            this.label6.Width = 1.01F;
            // 
            // textBox6
            // 
            this.textBox6.DataField = "Expense Type";
            this.textBox6.Height = 0.2F;
            this.textBox6.Left = 1.045F;
            this.textBox6.Name = "textBox6";
            this.textBox6.Style = "font-family: Tahoma; font-size: 9pt; text-align: left; ddo-char-set: 0";
            this.textBox6.Text = null;
            this.textBox6.Top = 1.192093E-07F;
            this.textBox6.Width = 1.01F;
            // 
            // groupHeader2
            // 
            this.groupHeader2.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.textBox6,
            this.textBox2});
            this.groupHeader2.DataField = "Expense Type";
            this.groupHeader2.Height = 0.25F;
            this.groupHeader2.Name = "groupHeader2";
            // 
            // groupFooter2
            // 
            this.groupFooter2.Height = 0F;
            this.groupFooter2.Name = "groupFooter2";
            // 
            // rptExpenseReport
            // 
            this.MasterReport = false;
            this.PageSettings.Margins.Left = 0.5F;
            this.PageSettings.Margins.Right = 0.5F;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.PrintWidth = 7.020833F;
            this.Sections.Add(this.pageHeader);
            this.Sections.Add(this.groupHeader1);
            this.Sections.Add(this.groupHeader2);
            this.Sections.Add(this.detail);
            this.Sections.Add(this.groupFooter2);
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
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportInfo1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.TextBox TextBox7;
        private DataDynamics.ActiveReports.Label Label4;
        private DataDynamics.ActiveReports.Label Label5;
        private DataDynamics.ActiveReports.ReportInfo ReportInfo1;
        private DataDynamics.ActiveReports.Shape Shape1;
        private DataDynamics.ActiveReports.Label Label1;
        private DataDynamics.ActiveReports.Label Label2;
        private DataDynamics.ActiveReports.Label Label7;
        private DataDynamics.ActiveReports.Label label8;
        private DataDynamics.ActiveReports.TextBox textBox1;
        private DataDynamics.ActiveReports.TextBox textBox2;
        private DataDynamics.ActiveReports.TextBox textBox3;
        private DataDynamics.ActiveReports.TextBox textBox4;
        public DataDynamics.ActiveReports.TextBox txtPharmacyName;
        public DataDynamics.ActiveReports.TextBox txtAddress;
        public DataDynamics.ActiveReports.TextBox txtFromDate;
        public DataDynamics.ActiveReports.TextBox txtToDate;
        private DataDynamics.ActiveReports.GroupHeader groupHeader1;
        private DataDynamics.ActiveReports.GroupFooter groupFooter1;
        private DataDynamics.ActiveReports.TextBox textBox5;
        private DataDynamics.ActiveReports.Label label3;
        private DataDynamics.ActiveReports.Label label6;
        private DataDynamics.ActiveReports.TextBox textBox6;
        private DataDynamics.ActiveReports.GroupHeader groupHeader2;
        private DataDynamics.ActiveReports.GroupFooter groupFooter2;
    }
}
