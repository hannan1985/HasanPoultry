namespace Tiles_Inventory
{
    partial class frmJournalView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Refrence No", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, true);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Account Name");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Debit Amount");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Credit Amount");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn5 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Refrence No");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn6 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Account Name");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn7 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Debit Amount");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn8 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Credit Amount");
            this.gbJournalDetail = new Infragistics.Win.Misc.UltraGroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbExpandAll = new System.Windows.Forms.RadioButton();
            this.rbCollapseAll = new System.Windows.Forms.RadioButton();
            this.grvJournalInformaiton = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraDataSource1 = new Infragistics.Win.UltraWinDataSource.UltraDataSource(this.components);
            this.ultraGroupBox6 = new Infragistics.Win.Misc.UltraGroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.btLoad = new Infragistics.Win.Misc.UltraButton();
            this.btnClose = new Infragistics.Win.Misc.UltraButton();
            ((System.ComponentModel.ISupportInitialize)(this.gbJournalDetail)).BeginInit();
            this.gbJournalDetail.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvJournalInformaiton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox6)).BeginInit();
            this.ultraGroupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbJournalDetail
            // 
            this.gbJournalDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbJournalDetail.Controls.Add(this.panel1);
            this.gbJournalDetail.Controls.Add(this.grvJournalInformaiton);
            this.gbJournalDetail.Location = new System.Drawing.Point(6, 73);
            this.gbJournalDetail.Name = "gbJournalDetail";
            this.gbJournalDetail.Size = new System.Drawing.Size(949, 357);
            this.gbJournalDetail.TabIndex = 4;
            this.gbJournalDetail.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.rbExpandAll);
            this.panel1.Controls.Add(this.rbCollapseAll);
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(190, 31);
            this.panel1.TabIndex = 3;
            // 
            // rbExpandAll
            // 
            this.rbExpandAll.AutoSize = true;
            this.rbExpandAll.BackColor = System.Drawing.Color.Transparent;
            this.rbExpandAll.Location = new System.Drawing.Point(5, 6);
            this.rbExpandAll.Name = "rbExpandAll";
            this.rbExpandAll.Size = new System.Drawing.Size(81, 18);
            this.rbExpandAll.TabIndex = 0;
            this.rbExpandAll.Text = "Expand All";
            this.rbExpandAll.UseVisualStyleBackColor = false;
            this.rbExpandAll.CheckedChanged += new System.EventHandler(this.rbExpandAll_CheckedChanged);
            // 
            // rbCollapseAll
            // 
            this.rbCollapseAll.AutoSize = true;
            this.rbCollapseAll.BackColor = System.Drawing.Color.Transparent;
            this.rbCollapseAll.Checked = true;
            this.rbCollapseAll.Location = new System.Drawing.Point(92, 6);
            this.rbCollapseAll.Name = "rbCollapseAll";
            this.rbCollapseAll.Size = new System.Drawing.Size(84, 18);
            this.rbCollapseAll.TabIndex = 0;
            this.rbCollapseAll.TabStop = true;
            this.rbCollapseAll.Text = "Collapse All";
            this.rbCollapseAll.UseVisualStyleBackColor = false;
            this.rbCollapseAll.CheckedChanged += new System.EventHandler(this.rbCollapseAll_CheckedChanged);
            // 
            // grvJournalInformaiton
            // 
            this.grvJournalInformaiton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grvJournalInformaiton.DataSource = this.ultraDataSource1;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.grvJournalInformaiton.DisplayLayout.Appearance = appearance1;
            this.grvJournalInformaiton.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Width = 219;
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Width = 304;
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Width = 294;
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn4.Width = 304;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4});
            this.grvJournalInformaiton.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.grvJournalInformaiton.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grvJournalInformaiton.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.grvJournalInformaiton.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grvJournalInformaiton.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.grvJournalInformaiton.DisplayLayout.MaxColScrollRegions = 1;
            this.grvJournalInformaiton.DisplayLayout.MaxRowScrollRegions = 1;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grvJournalInformaiton.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.grvJournalInformaiton.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.grvJournalInformaiton.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.grvJournalInformaiton.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.grvJournalInformaiton.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.grvJournalInformaiton.DisplayLayout.Override.CellAppearance = appearance8;
            this.grvJournalInformaiton.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            this.grvJournalInformaiton.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.grvJournalInformaiton.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(135)))), ((int)(((byte)(214)))));
            appearance10.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(59)))), ((int)(((byte)(150)))));
            appearance10.FontData.BoldAsString = "True";
            appearance10.ForeColor = System.Drawing.Color.White;
            appearance10.TextHAlignAsString = "Left";
            this.grvJournalInformaiton.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.grvJournalInformaiton.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.grvJournalInformaiton.DisplayLayout.Override.RowAppearance = appearance11;
            appearance12.BackColor = System.Drawing.Color.Transparent;
            appearance12.BackColor2 = System.Drawing.Color.Transparent;
            this.grvJournalInformaiton.DisplayLayout.Override.SelectedRowAppearance = appearance12;
            this.grvJournalInformaiton.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grvJournalInformaiton.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            appearance13.BackColor = System.Drawing.SystemColors.ControlLight;
            this.grvJournalInformaiton.DisplayLayout.Override.TemplateAddRowAppearance = appearance13;
            this.grvJournalInformaiton.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grvJournalInformaiton.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grvJournalInformaiton.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.grvJournalInformaiton.Location = new System.Drawing.Point(5, 43);
            this.grvJournalInformaiton.Name = "grvJournalInformaiton";
            this.grvJournalInformaiton.Size = new System.Drawing.Size(938, 307);
            this.grvJournalInformaiton.TabIndex = 0;
            // 
            // ultraDataSource1
            // 
            this.ultraDataSource1.Band.Columns.AddRange(new object[] {
            ultraDataColumn5,
            ultraDataColumn6,
            ultraDataColumn7,
            ultraDataColumn8});
            // 
            // ultraGroupBox6
            // 
            this.ultraGroupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ultraGroupBox6.Controls.Add(this.label1);
            this.ultraGroupBox6.Controls.Add(this.label9);
            this.ultraGroupBox6.Controls.Add(this.dtpToDate);
            this.ultraGroupBox6.Controls.Add(this.dtpFromDate);
            this.ultraGroupBox6.Controls.Add(this.btnClose);
            this.ultraGroupBox6.Controls.Add(this.btLoad);
            this.ultraGroupBox6.Location = new System.Drawing.Point(6, 4);
            this.ultraGroupBox6.Name = "ultraGroupBox6";
            this.ultraGroupBox6.Size = new System.Drawing.Size(949, 67);
            this.ultraGroupBox6.TabIndex = 5;
            this.ultraGroupBox6.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(440, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 14);
            this.label1.TabIndex = 44;
            this.label1.Text = "To";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(290, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 14);
            this.label9.TabIndex = 44;
            this.label9.Text = "From";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(471, 22);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(98, 22);
            this.dtpToDate.TabIndex = 6;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(333, 22);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(98, 22);
            this.dtpFromDate.TabIndex = 6;
            // 
            // btLoad
            // 
            this.btLoad.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btLoad.Location = new System.Drawing.Point(578, 20);
            this.btLoad.Name = "btLoad";
            this.btLoad.Size = new System.Drawing.Size(81, 26);
            this.btLoad.TabIndex = 5;
            this.btLoad.Text = "Load";
            this.btLoad.Click += new System.EventHandler(this.btLoad_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnClose.Location = new System.Drawing.Point(665, 20);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(81, 26);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmJournalView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 432);
            this.Controls.Add(this.ultraGroupBox6);
            this.Controls.Add(this.gbJournalDetail);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmJournalView";
            this.Text = "Journal View";
            ((System.ComponentModel.ISupportInitialize)(this.gbJournalDetail)).EndInit();
            this.gbJournalDetail.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvJournalInformaiton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox6)).EndInit();
            this.ultraGroupBox6.ResumeLayout(false);
            this.ultraGroupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraGroupBox gbJournalDetail;
        private Infragistics.Win.UltraWinDataSource.UltraDataSource ultraDataSource1;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox6;
        private Infragistics.Win.Misc.UltraButton btLoad;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private Infragistics.Win.UltraWinGrid.UltraGrid grvJournalInformaiton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbExpandAll;
        private System.Windows.Forms.RadioButton rbCollapseAll;
        private Infragistics.Win.Misc.UltraButton btnClose;
    }
}