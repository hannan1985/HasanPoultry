namespace Tiles_Inventory
{
    partial class frmViewCompany
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
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            this.btRefresh = new System.Windows.Forms.Button();
            this.UltraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.cmbCompanyName = new Infragistics.Win.UltraWinGrid.UltraCombo();
            this.Label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.grvCompanyInformation = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UltraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            this.btAdd = new System.Windows.Forms.Button();
            this.UltraGroupBox3 = new Infragistics.Win.Misc.UltraGroupBox();
            this.btEdit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox1)).BeginInit();
            this.UltraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompanyName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCompanyInformation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox2)).BeginInit();
            this.UltraGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox3)).BeginInit();
            this.UltraGroupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btRefresh
            // 
            this.btRefresh.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btRefresh.Location = new System.Drawing.Point(391, 17);
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(75, 23);
            this.btRefresh.TabIndex = 0;
            this.btRefresh.Text = "Refresh";
            this.btRefresh.UseVisualStyleBackColor = true;
            this.btRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // UltraGroupBox1
            // 
            this.UltraGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            appearance3.BackColor = System.Drawing.Color.LightBlue;
            this.UltraGroupBox1.Appearance = appearance3;
            this.UltraGroupBox1.Controls.Add(this.cmbCompanyName);
            this.UltraGroupBox1.Controls.Add(this.Label1);
            this.UltraGroupBox1.Location = new System.Drawing.Point(1, 2);
            this.UltraGroupBox1.Name = "UltraGroupBox1";
            this.UltraGroupBox1.Size = new System.Drawing.Size(772, 78);
            this.UltraGroupBox1.TabIndex = 9;
            // 
            // cmbCompanyName
            // 
            this.cmbCompanyName.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
            this.cmbCompanyName.CheckedListSettings.CheckStateMember = "";
            Appearance1.BackColor = System.Drawing.SystemColors.Window;
            Appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.cmbCompanyName.DisplayLayout.Appearance = Appearance1;
            this.cmbCompanyName.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns;
            this.cmbCompanyName.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.cmbCompanyName.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            Appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            Appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            Appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            Appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.cmbCompanyName.DisplayLayout.GroupByBox.Appearance = Appearance2;
            Appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.cmbCompanyName.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance3;
            this.cmbCompanyName.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            Appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            Appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            Appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            Appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.cmbCompanyName.DisplayLayout.GroupByBox.PromptAppearance = Appearance4;
            this.cmbCompanyName.DisplayLayout.MaxColScrollRegions = 1;
            this.cmbCompanyName.DisplayLayout.MaxRowScrollRegions = 1;
            Appearance5.BackColor = System.Drawing.SystemColors.Window;
            Appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmbCompanyName.DisplayLayout.Override.ActiveCellAppearance = Appearance5;
            Appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            Appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.cmbCompanyName.DisplayLayout.Override.ActiveRowAppearance = Appearance6;
            this.cmbCompanyName.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.cmbCompanyName.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            Appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.cmbCompanyName.DisplayLayout.Override.CardAreaAppearance = Appearance7;
            Appearance8.BorderColor = System.Drawing.Color.Silver;
            Appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.cmbCompanyName.DisplayLayout.Override.CellAppearance = Appearance8;
            this.cmbCompanyName.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.cmbCompanyName.DisplayLayout.Override.CellPadding = 0;
            Appearance9.BackColor = System.Drawing.SystemColors.Control;
            Appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            Appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            Appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            Appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.cmbCompanyName.DisplayLayout.Override.GroupByRowAppearance = Appearance9;
            Appearance10.TextHAlignAsString = "Left";
            this.cmbCompanyName.DisplayLayout.Override.HeaderAppearance = Appearance10;
            this.cmbCompanyName.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.cmbCompanyName.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            Appearance11.BackColor = System.Drawing.SystemColors.Window;
            Appearance11.BorderColor = System.Drawing.Color.Silver;
            this.cmbCompanyName.DisplayLayout.Override.RowAppearance = Appearance11;
            this.cmbCompanyName.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            Appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cmbCompanyName.DisplayLayout.Override.TemplateAddRowAppearance = Appearance12;
            this.cmbCompanyName.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.cmbCompanyName.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.cmbCompanyName.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.cmbCompanyName.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
            this.cmbCompanyName.Location = new System.Drawing.Point(101, 21);
            this.cmbCompanyName.Name = "cmbCompanyName";
            this.cmbCompanyName.PreferredDropDownSize = new System.Drawing.Size(0, 0);
            this.cmbCompanyName.Size = new System.Drawing.Size(312, 24);
            this.cmbCompanyName.TabIndex = 3;
            this.cmbCompanyName.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.cmbCompanyName_InitializeLayout);
            this.cmbCompanyName.ValueChanged += new System.EventHandler(this.cmbCompanyName_ValueChanged);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Location = new System.Drawing.Point(5, 26);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(92, 14);
            this.Label1.TabIndex = 2;
            this.Label1.Text = "Company Name";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClose.Location = new System.Drawing.Point(475, 17);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // grvCompanyInformation
            // 
            this.grvCompanyInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grvCompanyInformation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grvCompanyInformation.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.grvCompanyInformation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvCompanyInformation.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column1,
            this.Column5,
            this.Address});
            this.grvCompanyInformation.Location = new System.Drawing.Point(6, 5);
            this.grvCompanyInformation.Name = "grvCompanyInformation";
            this.grvCompanyInformation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvCompanyInformation.Size = new System.Drawing.Size(760, 326);
            this.grvCompanyInformation.TabIndex = 0;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "CompanyID";
            this.Column2.Name = "Column2";
            this.Column2.Visible = false;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Company Name";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Contact Person";
            this.Column4.Name = "Column4";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Phone";
            this.Column1.Name = "Column1";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Email";
            this.Column5.Name = "Column5";
            // 
            // Address
            // 
            this.Address.HeaderText = "Address";
            this.Address.Name = "Address";
            // 
            // UltraGroupBox2
            // 
            this.UltraGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            appearance2.BackColor = System.Drawing.Color.LightBlue;
            this.UltraGroupBox2.Appearance = appearance2;
            this.UltraGroupBox2.Controls.Add(this.grvCompanyInformation);
            this.UltraGroupBox2.Location = new System.Drawing.Point(1, 82);
            this.UltraGroupBox2.Name = "UltraGroupBox2";
            this.UltraGroupBox2.Size = new System.Drawing.Size(772, 337);
            this.UltraGroupBox2.TabIndex = 7;
            // 
            // btAdd
            // 
            this.btAdd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btAdd.Location = new System.Drawing.Point(223, 17);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(75, 23);
            this.btAdd.TabIndex = 0;
            this.btAdd.Text = "Add New";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // UltraGroupBox3
            // 
            this.UltraGroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            appearance1.BackColor = System.Drawing.Color.LightBlue;
            this.UltraGroupBox3.Appearance = appearance1;
            this.UltraGroupBox3.Controls.Add(this.btnClose);
            this.UltraGroupBox3.Controls.Add(this.btRefresh);
            this.UltraGroupBox3.Controls.Add(this.btEdit);
            this.UltraGroupBox3.Controls.Add(this.btAdd);
            this.UltraGroupBox3.Location = new System.Drawing.Point(1, 421);
            this.UltraGroupBox3.Name = "UltraGroupBox3";
            this.UltraGroupBox3.Size = new System.Drawing.Size(772, 57);
            this.UltraGroupBox3.TabIndex = 8;
            // 
            // btEdit
            // 
            this.btEdit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btEdit.Location = new System.Drawing.Point(307, 17);
            this.btEdit.Name = "btEdit";
            this.btEdit.Size = new System.Drawing.Size(75, 23);
            this.btEdit.TabIndex = 0;
            this.btEdit.Text = "Edit";
            this.btEdit.UseVisualStyleBackColor = true;
            this.btEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // frmViewCompany
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 481);
            this.Controls.Add(this.UltraGroupBox1);
            this.Controls.Add(this.UltraGroupBox2);
            this.Controls.Add(this.UltraGroupBox3);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmViewCompany";
            this.Text = "View Company";
            this.Load += new System.EventHandler(this.frmViewCompany_Load);
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox1)).EndInit();
            this.UltraGroupBox1.ResumeLayout(false);
            this.UltraGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompanyName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCompanyInformation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox2)).EndInit();
            this.UltraGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox3)).EndInit();
            this.UltraGroupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button btRefresh;
        internal Infragistics.Win.Misc.UltraGroupBox UltraGroupBox1;
        internal Infragistics.Win.UltraWinGrid.UltraCombo cmbCompanyName;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.DataGridView grvCompanyInformation;
        internal Infragistics.Win.Misc.UltraGroupBox UltraGroupBox2;
        internal System.Windows.Forms.Button btAdd;
        internal Infragistics.Win.Misc.UltraGroupBox UltraGroupBox3;
        internal System.Windows.Forms.Button btEdit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
    }
}