namespace Tiles_Inventory
{
    partial class frmViewJournalVouhcer
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
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn1 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Voucher No");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn2 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Account Name");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn3 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Debit Amount");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn4 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Credit Amount");
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Voucher No", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
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
            this.ultraDataSource1 = new Infragistics.Win.UltraWinDataSource.UltraDataSource(this.components);
            this.ultraGroupBox6 = new Infragistics.Win.Misc.UltraGroupBox();
            this.btSearchByVoucherNo = new Infragistics.Win.Misc.UltraButton();
            this.txtVoucherNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.btnClose = new Infragistics.Win.Misc.UltraButton();
            this.btLoad = new Infragistics.Win.Misc.UltraButton();
            this.gbJournalDetail = new Infragistics.Win.Misc.UltraGroupBox();
            this.cbPrintAll = new System.Windows.Forms.CheckBox();
            this.grvJournalInformaiton = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.btnAddNew = new Infragistics.Win.Misc.UltraButton();
            this.btnEdit = new Infragistics.Win.Misc.UltraButton();
            this.btnPrint = new Infragistics.Win.Misc.UltraButton();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox6)).BeginInit();
            this.ultraGroupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbJournalDetail)).BeginInit();
            this.gbJournalDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvJournalInformaiton)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraDataSource1
            // 
            this.ultraDataSource1.Band.Columns.AddRange(new object[] {
            ultraDataColumn1,
            ultraDataColumn2,
            ultraDataColumn3,
            ultraDataColumn4});
            // 
            // ultraGroupBox6
            // 
            this.ultraGroupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ultraGroupBox6.Controls.Add(this.btSearchByVoucherNo);
            this.ultraGroupBox6.Controls.Add(this.txtVoucherNo);
            this.ultraGroupBox6.Controls.Add(this.label5);
            this.ultraGroupBox6.Controls.Add(this.label1);
            this.ultraGroupBox6.Controls.Add(this.label9);
            this.ultraGroupBox6.Controls.Add(this.dtpToDate);
            this.ultraGroupBox6.Controls.Add(this.dtpFromDate);
            this.ultraGroupBox6.Controls.Add(this.btnClose);
            this.ultraGroupBox6.Controls.Add(this.btLoad);
            this.ultraGroupBox6.Location = new System.Drawing.Point(6, 3);
            this.ultraGroupBox6.Name = "ultraGroupBox6";
            this.ultraGroupBox6.Size = new System.Drawing.Size(949, 67);
            this.ultraGroupBox6.TabIndex = 7;
            this.ultraGroupBox6.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // btSearchByVoucherNo
            // 
            this.btSearchByVoucherNo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btSearchByVoucherNo.Location = new System.Drawing.Point(292, 20);
            this.btSearchByVoucherNo.Name = "btSearchByVoucherNo";
            this.btSearchByVoucherNo.Size = new System.Drawing.Size(81, 26);
            this.btSearchByVoucherNo.TabIndex = 47;
            this.btSearchByVoucherNo.Text = "Load";
            this.btSearchByVoucherNo.Click += new System.EventHandler(this.btSearchByVoucherNo_Click);
            // 
            // txtVoucherNo
            // 
            this.txtVoucherNo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtVoucherNo.Location = new System.Drawing.Point(167, 22);
            this.txtVoucherNo.Name = "txtVoucherNo";
            this.txtVoucherNo.Size = new System.Drawing.Size(119, 22);
            this.txtVoucherNo.TabIndex = 45;
            this.txtVoucherNo.TabStop = false;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(93, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 14);
            this.label5.TabIndex = 46;
            this.label5.Text = "Voucher No";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(549, 26);
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
            this.label9.Location = new System.Drawing.Point(399, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 14);
            this.label9.TabIndex = 44;
            this.label9.Text = "From";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(580, 22);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(98, 22);
            this.dtpToDate.TabIndex = 6;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(442, 22);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(98, 22);
            this.dtpFromDate.TabIndex = 6;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnClose.Location = new System.Drawing.Point(774, 20);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(81, 26);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btLoad
            // 
            this.btLoad.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btLoad.Location = new System.Drawing.Point(687, 20);
            this.btLoad.Name = "btLoad";
            this.btLoad.Size = new System.Drawing.Size(81, 26);
            this.btLoad.TabIndex = 5;
            this.btLoad.Text = "Load";
            this.btLoad.Click += new System.EventHandler(this.btLoad_Click);
            // 
            // gbJournalDetail
            // 
            this.gbJournalDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbJournalDetail.Controls.Add(this.cbPrintAll);
            this.gbJournalDetail.Controls.Add(this.grvJournalInformaiton);
            this.gbJournalDetail.Controls.Add(this.btnAddNew);
            this.gbJournalDetail.Controls.Add(this.btnEdit);
            this.gbJournalDetail.Controls.Add(this.btnPrint);
            this.gbJournalDetail.Location = new System.Drawing.Point(6, 72);
            this.gbJournalDetail.Name = "gbJournalDetail";
            this.gbJournalDetail.Size = new System.Drawing.Size(949, 357);
            this.gbJournalDetail.TabIndex = 6;
            this.gbJournalDetail.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // cbPrintAll
            // 
            this.cbPrintAll.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cbPrintAll.AutoSize = true;
            this.cbPrintAll.BackColor = System.Drawing.Color.Transparent;
            this.cbPrintAll.Location = new System.Drawing.Point(552, 329);
            this.cbPrintAll.Name = "cbPrintAll";
            this.cbPrintAll.Size = new System.Drawing.Size(67, 18);
            this.cbPrintAll.TabIndex = 6;
            this.cbPrintAll.Text = "Print All";
            this.cbPrintAll.UseVisualStyleBackColor = false;
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
            ultraGridColumn1.Width = 168;
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Width = 259;
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Width = 241;
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn4.Width = 249;
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
            this.grvJournalInformaiton.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grvJournalInformaiton.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.Horizontal;
            this.grvJournalInformaiton.Location = new System.Drawing.Point(5, 4);
            this.grvJournalInformaiton.Name = "grvJournalInformaiton";
            this.grvJournalInformaiton.Size = new System.Drawing.Size(938, 313);
            this.grvJournalInformaiton.TabIndex = 0;
            // 
            // btnAddNew
            // 
            this.btnAddNew.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAddNew.Location = new System.Drawing.Point(294, 325);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(81, 26);
            this.btnAddNew.TabIndex = 5;
            this.btnAddNew.Text = "Add New";
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnEdit.Location = new System.Drawing.Point(381, 325);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(81, 26);
            this.btnEdit.TabIndex = 5;
            this.btnEdit.Text = "Edit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnPrint.Location = new System.Drawing.Point(468, 325);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(81, 26);
            this.btnPrint.TabIndex = 5;
            this.btnPrint.Text = "Print";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // frmViewJournalVouhcer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 432);
            this.Controls.Add(this.ultraGroupBox6);
            this.Controls.Add(this.gbJournalDetail);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmViewJournalVouhcer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Journal Vouhcer";
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox6)).EndInit();
            this.ultraGroupBox6.ResumeLayout(false);
            this.ultraGroupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbJournalDetail)).EndInit();
            this.gbJournalDetail.ResumeLayout(false);
            this.gbJournalDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvJournalInformaiton)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox6;
        private Infragistics.Win.Misc.UltraButton btnClose;
        private Infragistics.Win.Misc.UltraButton btLoad;
        private Infragistics.Win.UltraWinDataSource.UltraDataSource ultraDataSource1;
        private Infragistics.Win.UltraWinGrid.UltraGrid grvJournalInformaiton;
        private Infragistics.Win.Misc.UltraGroupBox gbJournalDetail;
        private Infragistics.Win.Misc.UltraButton btnAddNew;
        private Infragistics.Win.Misc.UltraButton btnEdit;
        private Infragistics.Win.Misc.UltraButton btnPrint;
        private Infragistics.Win.Misc.UltraButton btSearchByVoucherNo;
        private System.Windows.Forms.TextBox txtVoucherNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbPrintAll;
    }
}