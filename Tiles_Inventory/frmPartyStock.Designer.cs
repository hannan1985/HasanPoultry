namespace Tiles_Inventory
{
    partial class frmPartyStock
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
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance18 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance19 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance20 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance21 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance22 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance23 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance24 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            this.UltraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.cmbPartyName = new Infragistics.Win.UltraWinGrid.UltraCombo();
            this.label2 = new System.Windows.Forms.Label();
            this.UltraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btCreditSave = new System.Windows.Forms.Button();
            this.grvProductInformation = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox1)).BeginInit();
            this.UltraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPartyName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox2)).BeginInit();
            this.UltraGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvProductInformation)).BeginInit();
            this.SuspendLayout();
            // 
            // UltraGroupBox1
            // 
            this.UltraGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance3.BackColor = System.Drawing.Color.LightBlue;
            this.UltraGroupBox1.Appearance = appearance3;
            this.UltraGroupBox1.Controls.Add(this.cmbPartyName);
            this.UltraGroupBox1.Controls.Add(this.label2);
            this.UltraGroupBox1.Location = new System.Drawing.Point(1, 2);
            this.UltraGroupBox1.Name = "UltraGroupBox1";
            this.UltraGroupBox1.Size = new System.Drawing.Size(987, 58);
            this.UltraGroupBox1.TabIndex = 3;
            // 
            // cmbPartyName
            // 
            this.cmbPartyName.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
            this.cmbPartyName.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
            this.cmbPartyName.CheckedListSettings.CheckStateMember = "";
            Appearance13.BackColor = System.Drawing.SystemColors.Window;
            Appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.cmbPartyName.DisplayLayout.Appearance = Appearance13;
            this.cmbPartyName.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.cmbPartyName.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            Appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
            Appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
            Appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            Appearance14.BorderColor = System.Drawing.SystemColors.Window;
            this.cmbPartyName.DisplayLayout.GroupByBox.Appearance = Appearance14;
            Appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
            this.cmbPartyName.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance15;
            this.cmbPartyName.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            Appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
            Appearance16.BackColor2 = System.Drawing.SystemColors.Control;
            Appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            Appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
            this.cmbPartyName.DisplayLayout.GroupByBox.PromptAppearance = Appearance16;
            this.cmbPartyName.DisplayLayout.MaxColScrollRegions = 1;
            this.cmbPartyName.DisplayLayout.MaxRowScrollRegions = 1;
            Appearance17.BackColor = System.Drawing.SystemColors.Window;
            Appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmbPartyName.DisplayLayout.Override.ActiveCellAppearance = Appearance17;
            Appearance18.BackColor = System.Drawing.SystemColors.Highlight;
            Appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.cmbPartyName.DisplayLayout.Override.ActiveRowAppearance = Appearance18;
            this.cmbPartyName.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.cmbPartyName.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            Appearance19.BackColor = System.Drawing.SystemColors.Window;
            this.cmbPartyName.DisplayLayout.Override.CardAreaAppearance = Appearance19;
            Appearance20.BorderColor = System.Drawing.Color.Silver;
            Appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.cmbPartyName.DisplayLayout.Override.CellAppearance = Appearance20;
            this.cmbPartyName.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.cmbPartyName.DisplayLayout.Override.CellPadding = 0;
            Appearance21.BackColor = System.Drawing.SystemColors.Control;
            Appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
            Appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            Appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            Appearance21.BorderColor = System.Drawing.SystemColors.Window;
            this.cmbPartyName.DisplayLayout.Override.GroupByRowAppearance = Appearance21;
            Appearance22.TextHAlignAsString = "Left";
            this.cmbPartyName.DisplayLayout.Override.HeaderAppearance = Appearance22;
            this.cmbPartyName.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.cmbPartyName.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            Appearance23.BackColor = System.Drawing.SystemColors.Window;
            Appearance23.BorderColor = System.Drawing.Color.Silver;
            this.cmbPartyName.DisplayLayout.Override.RowAppearance = Appearance23;
            this.cmbPartyName.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            Appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cmbPartyName.DisplayLayout.Override.TemplateAddRowAppearance = Appearance24;
            this.cmbPartyName.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.cmbPartyName.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.cmbPartyName.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.cmbPartyName.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
            this.cmbPartyName.Location = new System.Drawing.Point(410, 17);
            this.cmbPartyName.Name = "cmbPartyName";
            this.cmbPartyName.PreferredDropDownSize = new System.Drawing.Size(0, 0);
            this.cmbPartyName.Size = new System.Drawing.Size(240, 24);
            this.cmbPartyName.TabIndex = 6;
            this.cmbPartyName.ValueChanged += new System.EventHandler(this.cmbProductType_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(336, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 14);
            this.label2.TabIndex = 5;
            this.label2.Text = "Party Name";
            // 
            // UltraGroupBox2
            // 
            this.UltraGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance2.BackColor = System.Drawing.Color.LightBlue;
            this.UltraGroupBox2.Appearance = appearance2;
            this.UltraGroupBox2.Controls.Add(this.btnClose);
            this.UltraGroupBox2.Controls.Add(this.btnRefresh);
            this.UltraGroupBox2.Controls.Add(this.btCreditSave);
            this.UltraGroupBox2.Controls.Add(this.grvProductInformation);
            this.UltraGroupBox2.Location = new System.Drawing.Point(1, 66);
            this.UltraGroupBox2.Name = "UltraGroupBox2";
            this.UltraGroupBox2.Size = new System.Drawing.Size(987, 412);
            this.UltraGroupBox2.TabIndex = 4;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(744, 385);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnRefresh.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(825, 385);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btCreditSave
            // 
            this.btCreditSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCreditSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btCreditSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btCreditSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCreditSave.Location = new System.Drawing.Point(906, 385);
            this.btCreditSave.Name = "btCreditSave";
            this.btCreditSave.Size = new System.Drawing.Size(75, 23);
            this.btCreditSave.TabIndex = 3;
            this.btCreditSave.Text = "Export";
            this.btCreditSave.UseVisualStyleBackColor = false;
            this.btCreditSave.Click += new System.EventHandler(this.btCreditSave_Click);
            // 
            // grvProductInformation
            // 
            this.grvProductInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grvProductInformation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grvProductInformation.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.grvProductInformation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvProductInformation.Location = new System.Drawing.Point(6, 5);
            this.grvProductInformation.Name = "grvProductInformation";
            this.grvProductInformation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvProductInformation.Size = new System.Drawing.Size(975, 376);
            this.grvProductInformation.TabIndex = 0;
            this.grvProductInformation.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmPartyStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 481);
            this.Controls.Add(this.UltraGroupBox1);
            this.Controls.Add(this.UltraGroupBox2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmPartyStock";
            this.Text = "Party Stock";
            this.Load += new System.EventHandler(this.frmViewStock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox1)).EndInit();
            this.UltraGroupBox1.ResumeLayout(false);
            this.UltraGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPartyName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox2)).EndInit();
            this.UltraGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvProductInformation)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal Infragistics.Win.Misc.UltraGroupBox UltraGroupBox1;
        internal Infragistics.Win.Misc.UltraGroupBox UltraGroupBox2;
        internal System.Windows.Forms.DataGridView grvProductInformation;
        private System.Windows.Forms.Timer timer1;
        internal System.Windows.Forms.Button btCreditSave;
        internal Infragistics.Win.UltraWinGrid.UltraCombo cmbPartyName;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.Button btnRefresh;
    }
}