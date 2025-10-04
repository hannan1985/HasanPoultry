namespace Tiles_Inventory.Reports
{
    /// <summary>
    /// Summary description for rptTrialBalanceCreditItem.
    /// </summary>
    partial class rptTrialBalanceCreditItem
    {
        private DataDynamics.ActiveReports.Detail detail;

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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(rptTrialBalanceCreditItem));
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.txtDescription = new DataDynamics.ActiveReports.TextBox();
            this.txtAmount = new DataDynamics.ActiveReports.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.txtDescription,
            this.txtAmount});
            this.detail.Height = 0.6041667F;
            this.detail.Name = "detail";
            // 
            // txtDescription
            // 
            this.txtDescription.DataField = "AccountName";
            this.txtDescription.Height = 0.2F;
            this.txtDescription.Left = 3.594F;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Top = 0.053F;
            this.txtDescription.Width = 2.229001F;
            // 
            // txtAmount
            // 
            this.txtAmount.DataField = "Amount";
            this.txtAmount.Height = 0.2F;
            this.txtAmount.Left = 5.908F;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Top = 0.053F;
            this.txtAmount.Width = 1F;
            // 
            // rptTrialBalanceCreditItem
            // 
            this.MasterReport = false;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.PrintWidth = 6.989583F;
            this.Sections.Add(this.detail);
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" +
                        "l; font-size: 10pt; color: Black", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold", "Heading1", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" +
                        "lic", "Heading2", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold", "Heading3", "Normal"));
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.TextBox txtDescription;
        private DataDynamics.ActiveReports.TextBox txtAmount;
    }
}
