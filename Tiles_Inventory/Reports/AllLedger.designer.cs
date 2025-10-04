namespace Tiles_Inventory
{
    /// <summary>
    /// Summary description for AllLedger.
    /// </summary>
    partial class AllLedger
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AllLedger));
            DataDynamics.ActiveReports.DataSources.OleDBDataSource oleDBDataSource1 = new DataDynamics.ActiveReports.DataSources.OleDBDataSource();
            this.pageHeader = new DataDynamics.ActiveReports.PageHeader();
            this.label2 = new DataDynamics.ActiveReports.Label();
            this.label1 = new DataDynamics.ActiveReports.Label();
            this.label3 = new DataDynamics.ActiveReports.Label();
            this.label4 = new DataDynamics.ActiveReports.Label();
            this.label5 = new DataDynamics.ActiveReports.Label();
            this.label6 = new DataDynamics.ActiveReports.Label();
            this.lblAccountName = new DataDynamics.ActiveReports.Label();
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.textBox2 = new DataDynamics.ActiveReports.TextBox();
            this.textBox3 = new DataDynamics.ActiveReports.TextBox();
            this.textBox6 = new DataDynamics.ActiveReports.TextBox();
            this.line1 = new DataDynamics.ActiveReports.Line();
            this.textBox4 = new DataDynamics.ActiveReports.TextBox();
            this.textBox1 = new DataDynamics.ActiveReports.TextBox();
            this.textBox5 = new DataDynamics.ActiveReports.TextBox();
            this.pageFooter = new DataDynamics.ActiveReports.PageFooter();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAccountName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.label2,
            this.label1,
            this.label3,
            this.label4,
            this.label5,
            this.label6,
            this.lblAccountName});
            this.pageHeader.Height = 0.9101667F;
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Format += new System.EventHandler(this.pageHeader_Format);
            // 
            // label2
            // 
            this.label2.Height = 0.2F;
            this.label2.HyperLink = null;
            this.label2.Left = 4.705F;
            this.label2.Name = "label2";
            this.label2.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: bold; ddo-char-set: 0";
            this.label2.Text = "Debit";
            this.label2.Top = 0.686F;
            this.label2.Width = 0.9700003F;
            // 
            // label1
            // 
            this.label1.Height = 0.2F;
            this.label1.HyperLink = null;
            this.label1.Left = 0.8137481F;
            this.label1.Name = "label1";
            this.label1.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: bold; ddo-char-set: 0";
            this.label1.Text = "Accounts Name";
            this.label1.Top = 0.686F;
            this.label1.Width = 1.858748F;
            // 
            // label3
            // 
            this.label3.Height = 0.2F;
            this.label3.HyperLink = null;
            this.label3.Left = 5.706752F;
            this.label3.Name = "label3";
            this.label3.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: bold; ddo-char-set: 0";
            this.label3.Text = "Credit";
            this.label3.Top = 0.686F;
            this.label3.Width = 0.9700003F;
            // 
            // label4
            // 
            this.label4.Height = 0.2F;
            this.label4.HyperLink = null;
            this.label4.Left = 6.708504F;
            this.label4.Name = "label4";
            this.label4.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: bold; ddo-char-set: 0";
            this.label4.Text = "Balance";
            this.label4.Top = 0.686F;
            this.label4.Width = 1F;
            // 
            // label5
            // 
            this.label5.Height = 0.2F;
            this.label5.HyperLink = null;
            this.label5.Left = 0.07199621F;
            this.label5.Name = "label5";
            this.label5.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: bold; ddo-char-set: 0";
            this.label5.Text = "Date";
            this.label5.Top = 0.686F;
            this.label5.Width = 0.71F;
            // 
            // label6
            // 
            this.label6.Height = 0.2F;
            this.label6.HyperLink = null;
            this.label6.Left = 2.704248F;
            this.label6.Name = "label6";
            this.label6.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: bold; ddo-char-set: 0";
            this.label6.Text = "Description";
            this.label6.Top = 0.686F;
            this.label6.Width = 1.969F;
            // 
            // lblAccountName
            // 
            this.lblAccountName.Height = 0.23F;
            this.lblAccountName.HyperLink = null;
            this.lblAccountName.Left = 2.262938F;
            this.lblAccountName.Name = "lblAccountName";
            this.lblAccountName.Style = "font-family: Tahoma; font-size: 12pt; font-weight: bold; text-align: center; text" +
    "-decoration: underline; ddo-char-set: 1";
            this.lblAccountName.Text = "Description";
            this.lblAccountName.Top = 0.02F;
            this.lblAccountName.Width = 3.229042F;
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.textBox2,
            this.textBox3,
            this.textBox6,
            this.line1,
            this.textBox4,
            this.textBox1,
            this.textBox5});
            this.detail.Height = 0.2789445F;
            this.detail.Name = "detail";
            // 
            // textBox2
            // 
            this.textBox2.DataField = "DebitAmount";
            this.textBox2.Height = 0.2F;
            this.textBox2.Left = 4.705004F;
            this.textBox2.Name = "textBox2";
            this.textBox2.OutputFormat = resources.GetString("textBox2.OutputFormat");
            this.textBox2.Style = "font-size: 9pt; ddo-char-set: 0";
            this.textBox2.Text = null;
            this.textBox2.Top = 0.01888891F;
            this.textBox2.Width = 0.9700003F;
            // 
            // textBox3
            // 
            this.textBox3.DataField = "CreditAmount";
            this.textBox3.Height = 0.2F;
            this.textBox3.Left = 5.706756F;
            this.textBox3.Name = "textBox3";
            this.textBox3.Style = "font-size: 9pt; ddo-char-set: 0";
            this.textBox3.Text = null;
            this.textBox3.Top = 0.01888891F;
            this.textBox3.Width = 0.9700003F;
            // 
            // textBox6
            // 
            this.textBox6.DataField = "Balance";
            this.textBox6.Height = 0.2F;
            this.textBox6.Left = 6.708508F;
            this.textBox6.Name = "textBox6";
            this.textBox6.OutputFormat = resources.GetString("textBox6.OutputFormat");
            this.textBox6.Style = "font-size: 9pt; ddo-char-set: 0";
            this.textBox6.Text = null;
            this.textBox6.Top = 0.01888891F;
            this.textBox6.Width = 1F;
            // 
            // line1
            // 
            this.line1.Height = 0F;
            this.line1.Left = 0.072F;
            this.line1.LineStyle = DataDynamics.ActiveReports.LineStyle.Dash;
            this.line1.LineWeight = 1F;
            this.line1.Name = "line1";
            this.line1.Top = 0.26F;
            this.line1.Width = 7.64602F;
            this.line1.X1 = 0.072F;
            this.line1.X2 = 7.71802F;
            this.line1.Y1 = 0.26F;
            this.line1.Y2 = 0.26F;
            // 
            // textBox4
            // 
            this.textBox4.DataField = "Date";
            this.textBox4.Height = 0.2F;
            this.textBox4.Left = 0.07200003F;
            this.textBox4.Name = "textBox4";
            this.textBox4.OutputFormat = resources.GetString("textBox4.OutputFormat");
            this.textBox4.Style = "font-size: 9pt; ddo-char-set: 0";
            this.textBox4.Text = null;
            this.textBox4.Top = 0.01888891F;
            this.textBox4.Width = 0.71F;
            // 
            // textBox1
            // 
            this.textBox1.DataField = "AccountName";
            this.textBox1.Height = 0.2F;
            this.textBox1.Left = 0.8137519F;
            this.textBox1.Name = "textBox1";
            this.textBox1.Style = "font-size: 9pt; ddo-char-set: 0";
            this.textBox1.Text = null;
            this.textBox1.Top = 0.01888891F;
            this.textBox1.Width = 1.858748F;
            // 
            // textBox5
            // 
            this.textBox5.DataField = "Description";
            this.textBox5.Height = 0.2F;
            this.textBox5.Left = 2.704252F;
            this.textBox5.Name = "textBox5";
            this.textBox5.Style = "font-size: 9pt; ddo-char-set: 0";
            this.textBox5.Text = null;
            this.textBox5.Top = 0.01888891F;
            this.textBox5.Width = 1.969F;
            // 
            // pageFooter
            // 
            this.pageFooter.Height = 0.05416667F;
            this.pageFooter.Name = "pageFooter";
            // 
            // AllLedger
            // 
            this.MasterReport = false;
            oleDBDataSource1.ConnectionString = "Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;Initial " +
    "Catalog=Transport;Data Source=LINWIN-PC\\SQLEXPRESS";
            oleDBDataSource1.SQL = "";
            this.DataSource = oleDBDataSource1;
            this.PageSettings.Margins.Left = 0.25F;
            this.PageSettings.Margins.Right = 0.25F;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.PrintWidth = 7.754917F;
            this.Sections.Add(this.pageHeader);
            this.Sections.Add(this.detail);
            this.Sections.Add(this.pageFooter);
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" +
            "l; font-size: 10pt; color: Black", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold", "Heading1", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" +
            "lic", "Heading2", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold", "Heading3", "Normal"));
            ((System.ComponentModel.ISupportInitialize)(this.label2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAccountName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.TextBox textBox1;
        private DataDynamics.ActiveReports.TextBox textBox2;
        private DataDynamics.ActiveReports.TextBox textBox3;
        private DataDynamics.ActiveReports.Label label1;
        private DataDynamics.ActiveReports.Label label2;
        private DataDynamics.ActiveReports.Label label3;
        private DataDynamics.ActiveReports.Label label4;
        private DataDynamics.ActiveReports.TextBox textBox6;
        private DataDynamics.ActiveReports.Line line1;
        private DataDynamics.ActiveReports.Label label5;
        private DataDynamics.ActiveReports.TextBox textBox4;
        private DataDynamics.ActiveReports.Label label6;
        private DataDynamics.ActiveReports.TextBox textBox5;
        public DataDynamics.ActiveReports.Label lblAccountName;
    }
}
