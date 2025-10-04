namespace Tiles_Inventory
{
    partial class frmViewDistributedSample
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance61 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance62 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance63 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance64 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance65 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance66 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance67 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance68 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance69 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance70 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance71 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance72 = new Infragistics.Win.Appearance();
            this.UltraGroupBox3 = new Infragistics.Win.Misc.UltraGroupBox();
            this.btRefresh = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btPrint = new System.Windows.Forms.Button();
            this.btEdit = new System.Windows.Forms.Button();
            this.btReceive = new System.Windows.Forms.Button();
            this.btDistribute = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.grvDistributionInfo = new System.Windows.Forms.DataGridView();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.UltraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.UltraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.cmbSellerName = new Infragistics.Win.UltraWinGrid.UltraCombo();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox3)).BeginInit();
            this.UltraGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvDistributionInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox2)).BeginInit();
            this.UltraGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox1)).BeginInit();
            this.UltraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSellerName)).BeginInit();
            this.SuspendLayout();
            // 
            // UltraGroupBox3
            // 
            this.UltraGroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            appearance1.BackColor = System.Drawing.Color.LightBlue;
            this.UltraGroupBox3.Appearance = appearance1;
            this.UltraGroupBox3.Controls.Add(this.btRefresh);
            this.UltraGroupBox3.Controls.Add(this.btnClose);
            this.UltraGroupBox3.Controls.Add(this.btPrint);
            this.UltraGroupBox3.Controls.Add(this.btEdit);
            this.UltraGroupBox3.Controls.Add(this.btReceive);
            this.UltraGroupBox3.Controls.Add(this.btDistribute);
            this.UltraGroupBox3.Location = new System.Drawing.Point(4, 416);
            this.UltraGroupBox3.Name = "UltraGroupBox3";
            this.UltraGroupBox3.Size = new System.Drawing.Size(1005, 65);
            this.UltraGroupBox3.TabIndex = 7;
            // 
            // btRefresh
            // 
            this.btRefresh.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btRefresh.Location = new System.Drawing.Point(588, 21);
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(75, 23);
            this.btRefresh.TabIndex = 0;
            this.btRefresh.Text = "Refresh";
            this.btRefresh.UseVisualStyleBackColor = true;
            this.btRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClose.Location = new System.Drawing.Point(670, 21);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btPrint
            // 
            this.btPrint.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btPrint.Location = new System.Drawing.Point(506, 21);
            this.btPrint.Name = "btPrint";
            this.btPrint.Size = new System.Drawing.Size(75, 23);
            this.btPrint.TabIndex = 0;
            this.btPrint.Text = "Print";
            this.btPrint.UseVisualStyleBackColor = true;
            this.btPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btEdit
            // 
            this.btEdit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btEdit.Location = new System.Drawing.Point(424, 21);
            this.btEdit.Name = "btEdit";
            this.btEdit.Size = new System.Drawing.Size(75, 23);
            this.btEdit.TabIndex = 0;
            this.btEdit.Text = "Edit";
            this.btEdit.UseVisualStyleBackColor = true;
            this.btEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btReceive
            // 
            this.btReceive.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btReceive.Location = new System.Drawing.Point(342, 21);
            this.btReceive.Name = "btReceive";
            this.btReceive.Size = new System.Drawing.Size(75, 23);
            this.btReceive.TabIndex = 0;
            this.btReceive.Text = "&Receive";
            this.btReceive.UseVisualStyleBackColor = true;
            this.btReceive.Click += new System.EventHandler(this.btnReceive_Click);
            // 
            // btDistribute
            // 
            this.btDistribute.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btDistribute.Location = new System.Drawing.Point(260, 21);
            this.btDistribute.Name = "btDistribute";
            this.btDistribute.Size = new System.Drawing.Size(75, 23);
            this.btDistribute.TabIndex = 0;
            this.btDistribute.Text = "&Distribute";
            this.btDistribute.UseVisualStyleBackColor = true;
            this.btDistribute.Click += new System.EventHandler(this.btnDistribute_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnLoad.Location = new System.Drawing.Point(737, 24);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 3;
            this.btnLoad.Text = "Load Data";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // grvDistributionInfo
            // 
            this.grvDistributionInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grvDistributionInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grvDistributionInfo.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.grvDistributionInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvDistributionInfo.Location = new System.Drawing.Point(7, 5);
            this.grvDistributionInfo.Name = "grvDistributionInfo";
            this.grvDistributionInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvDistributionInfo.Size = new System.Drawing.Size(991, 338);
            this.grvDistributionInfo.TabIndex = 2;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(631, 24);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(103, 22);
            this.dtpToDate.TabIndex = 2;
            // 
            // UltraGroupBox2
            // 
            this.UltraGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            appearance2.BackColor = System.Drawing.Color.LightBlue;
            this.UltraGroupBox2.Appearance = appearance2;
            this.UltraGroupBox2.Controls.Add(this.grvDistributionInfo);
            this.UltraGroupBox2.Location = new System.Drawing.Point(4, 65);
            this.UltraGroupBox2.Name = "UltraGroupBox2";
            this.UltraGroupBox2.Size = new System.Drawing.Size(1005, 349);
            this.UltraGroupBox2.TabIndex = 8;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(497, 24);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(103, 22);
            this.dtpFromDate.TabIndex = 2;
            // 
            // UltraGroupBox1
            // 
            this.UltraGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            appearance3.BackColor = System.Drawing.Color.LightBlue;
            this.UltraGroupBox1.Appearance = appearance3;
            this.UltraGroupBox1.Controls.Add(this.cmbSellerName);
            this.UltraGroupBox1.Controls.Add(this.Label1);
            this.UltraGroupBox1.Controls.Add(this.btnLoad);
            this.UltraGroupBox1.Controls.Add(this.dtpToDate);
            this.UltraGroupBox1.Controls.Add(this.Label3);
            this.UltraGroupBox1.Controls.Add(this.dtpFromDate);
            this.UltraGroupBox1.Controls.Add(this.Label2);
            this.UltraGroupBox1.Location = new System.Drawing.Point(4, 3);
            this.UltraGroupBox1.Name = "UltraGroupBox1";
            this.UltraGroupBox1.Size = new System.Drawing.Size(1005, 61);
            this.UltraGroupBox1.TabIndex = 9;
            // 
            // cmbSellerName
            // 
            this.cmbSellerName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmbSellerName.CheckedListSettings.CheckStateMember = "";
            Appearance61.BackColor = System.Drawing.SystemColors.Window;
            Appearance61.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.cmbSellerName.DisplayLayout.Appearance = Appearance61;
            this.cmbSellerName.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.cmbSellerName.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            Appearance62.BackColor = System.Drawing.SystemColors.ActiveBorder;
            Appearance62.BackColor2 = System.Drawing.SystemColors.ControlDark;
            Appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            Appearance62.BorderColor = System.Drawing.SystemColors.Window;
            this.cmbSellerName.DisplayLayout.GroupByBox.Appearance = Appearance62;
            Appearance63.ForeColor = System.Drawing.SystemColors.GrayText;
            this.cmbSellerName.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance63;
            this.cmbSellerName.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            Appearance64.BackColor = System.Drawing.SystemColors.ControlLightLight;
            Appearance64.BackColor2 = System.Drawing.SystemColors.Control;
            Appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            Appearance64.ForeColor = System.Drawing.SystemColors.GrayText;
            this.cmbSellerName.DisplayLayout.GroupByBox.PromptAppearance = Appearance64;
            this.cmbSellerName.DisplayLayout.MaxColScrollRegions = 1;
            this.cmbSellerName.DisplayLayout.MaxRowScrollRegions = 1;
            Appearance65.BackColor = System.Drawing.SystemColors.Window;
            Appearance65.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmbSellerName.DisplayLayout.Override.ActiveCellAppearance = Appearance65;
            Appearance66.BackColor = System.Drawing.SystemColors.Highlight;
            Appearance66.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.cmbSellerName.DisplayLayout.Override.ActiveRowAppearance = Appearance66;
            this.cmbSellerName.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.cmbSellerName.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            Appearance67.BackColor = System.Drawing.SystemColors.Window;
            this.cmbSellerName.DisplayLayout.Override.CardAreaAppearance = Appearance67;
            Appearance68.BorderColor = System.Drawing.Color.Silver;
            Appearance68.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.cmbSellerName.DisplayLayout.Override.CellAppearance = Appearance68;
            this.cmbSellerName.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.cmbSellerName.DisplayLayout.Override.CellPadding = 0;
            Appearance69.BackColor = System.Drawing.SystemColors.Control;
            Appearance69.BackColor2 = System.Drawing.SystemColors.ControlDark;
            Appearance69.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            Appearance69.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            Appearance69.BorderColor = System.Drawing.SystemColors.Window;
            this.cmbSellerName.DisplayLayout.Override.GroupByRowAppearance = Appearance69;
            Appearance70.TextHAlignAsString = "Left";
            this.cmbSellerName.DisplayLayout.Override.HeaderAppearance = Appearance70;
            this.cmbSellerName.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.cmbSellerName.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            Appearance71.BackColor = System.Drawing.SystemColors.Window;
            Appearance71.BorderColor = System.Drawing.Color.Silver;
            this.cmbSellerName.DisplayLayout.Override.RowAppearance = Appearance71;
            this.cmbSellerName.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            Appearance72.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cmbSellerName.DisplayLayout.Override.TemplateAddRowAppearance = Appearance72;
            this.cmbSellerName.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.cmbSellerName.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.cmbSellerName.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.cmbSellerName.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
            this.cmbSellerName.Location = new System.Drawing.Point(233, 22);
            this.cmbSellerName.Name = "cmbSellerName";
            this.cmbSellerName.PreferredDropDownSize = new System.Drawing.Size(0, 0);
            this.cmbSellerName.Size = new System.Drawing.Size(220, 24);
            this.cmbSellerName.TabIndex = 22;
            // 
            // Label1
            // 
            this.Label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Location = new System.Drawing.Point(158, 27);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(71, 14);
            this.Label1.TabIndex = 21;
            this.Label1.Text = "Seller Name";
            // 
            // Label3
            // 
            this.Label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Label3.AutoSize = true;
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.Location = new System.Drawing.Point(605, 28);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(22, 14);
            this.Label3.TabIndex = 1;
            this.Label3.Text = "To";
            // 
            // Label2
            // 
            this.Label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Location = new System.Drawing.Point(459, 28);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(34, 14);
            this.Label2.TabIndex = 1;
            this.Label2.Text = "From";
            // 
            // frmViewDistributedSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 484);
            this.Controls.Add(this.UltraGroupBox3);
            this.Controls.Add(this.UltraGroupBox2);
            this.Controls.Add(this.UltraGroupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmViewDistributedSample";
            this.Text = "View Distributed Sample";
            this.Load += new System.EventHandler(this.frmViewDistributedSample_Load);
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox3)).EndInit();
            this.UltraGroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvDistributionInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox2)).EndInit();
            this.UltraGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox1)).EndInit();
            this.UltraGroupBox1.ResumeLayout(false);
            this.UltraGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSellerName)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal Infragistics.Win.Misc.UltraGroupBox UltraGroupBox3;
        internal System.Windows.Forms.Button btRefresh;
        internal System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.Button btEdit;
        internal System.Windows.Forms.Button btDistribute;
        internal System.Windows.Forms.Button btnLoad;
        internal System.Windows.Forms.DataGridView grvDistributionInfo;
        internal System.Windows.Forms.DateTimePicker dtpToDate;
        internal Infragistics.Win.Misc.UltraGroupBox UltraGroupBox2;
        internal System.Windows.Forms.DateTimePicker dtpFromDate;
        internal Infragistics.Win.Misc.UltraGroupBox UltraGroupBox1;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal Infragistics.Win.UltraWinGrid.UltraCombo cmbSellerName;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button btReceive;
        internal System.Windows.Forms.Button btPrint;
    }
}