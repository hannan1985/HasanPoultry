namespace Tiles_Inventory
{
    partial class frmNewAccessright
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
            Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance40 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance41 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance42 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance43 = new Infragistics.Win.Appearance();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.btnNewPermission = new Infragistics.Win.Misc.UltraButton();
            this.btnNewAction = new Infragistics.Win.Misc.UltraButton();
            this.btUserRole = new Infragistics.Win.Misc.UltraButton();
            this.cmbRoleName = new Infragistics.Win.UltraWinGrid.UltraCombo();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRefresh = new Infragistics.Win.Misc.UltraButton();
            this.btnSave = new Infragistics.Win.Misc.UltraButton();
            this.btnClose = new Infragistics.Win.Misc.UltraButton();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).BeginInit();
            this.ultraGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbRoleName)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.Controls.Add(this.ultraGroupBox2);
            this.ultraGroupBox1.Controls.Add(this.btnNewPermission);
            this.ultraGroupBox1.Controls.Add(this.btnNewAction);
            this.ultraGroupBox1.Controls.Add(this.btUserRole);
            this.ultraGroupBox1.Controls.Add(this.cmbRoleName);
            this.ultraGroupBox1.Controls.Add(this.label1);
            this.ultraGroupBox1.Controls.Add(this.btnRefresh);
            this.ultraGroupBox1.Controls.Add(this.btnSave);
            this.ultraGroupBox1.Controls.Add(this.btnClose);
            this.ultraGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(643, 491);
            this.ultraGroupBox1.TabIndex = 0;
            this.ultraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // ultraGroupBox2
            // 
            this.ultraGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            appearance1.BackColor = System.Drawing.Color.Transparent;
            this.ultraGroupBox2.Appearance = appearance1;
            this.ultraGroupBox2.Controls.Add(this.treeView1);
            this.ultraGroupBox2.Location = new System.Drawing.Point(80, 42);
            this.ultraGroupBox2.Name = "ultraGroupBox2";
            this.ultraGroupBox2.Size = new System.Drawing.Size(262, 402);
            this.ultraGroupBox2.TabIndex = 2;
            this.ultraGroupBox2.Text = "Permission List";
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.CheckBoxes = true;
            this.treeView1.Location = new System.Drawing.Point(1, 23);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(254, 380);
            this.treeView1.TabIndex = 0;
            // 
            // btnNewPermission
            // 
            this.btnNewPermission.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewPermission.Location = new System.Drawing.Point(343, 42);
            this.btnNewPermission.Name = "btnNewPermission";
            this.btnNewPermission.Size = new System.Drawing.Size(130, 26);
            this.btnNewPermission.TabIndex = 6;
            this.btnNewPermission.Text = "Add New Permission";
            this.btnNewPermission.Click += new System.EventHandler(this.btnNewPermission_Click);
            // 
            // btnNewAction
            // 
            this.btnNewAction.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewAction.Location = new System.Drawing.Point(343, 74);
            this.btnNewAction.Name = "btnNewAction";
            this.btnNewAction.Size = new System.Drawing.Size(130, 26);
            this.btnNewAction.TabIndex = 7;
            this.btnNewAction.Text = "Add New Action";
            this.btnNewAction.Click += new System.EventHandler(this.btnNewAction_Click);
            // 
            // btUserRole
            // 
            this.btUserRole.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btUserRole.Location = new System.Drawing.Point(343, 10);
            this.btUserRole.Name = "btUserRole";
            this.btUserRole.Size = new System.Drawing.Size(46, 26);
            this.btUserRole.TabIndex = 1;
            this.btUserRole.Text = "Add";
            this.btUserRole.Click += new System.EventHandler(this.btUserRole_Click);
            // 
            // cmbRoleName
            // 
            this.cmbRoleName.CheckedListSettings.CheckStateMember = "";
            appearance32.BackColor = System.Drawing.SystemColors.Window;
            appearance32.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.cmbRoleName.DisplayLayout.Appearance = appearance32;
            this.cmbRoleName.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns;
            this.cmbRoleName.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.cmbRoleName.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance33.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance33.BorderColor = System.Drawing.SystemColors.Window;
            this.cmbRoleName.DisplayLayout.GroupByBox.Appearance = appearance33;
            appearance34.ForeColor = System.Drawing.SystemColors.GrayText;
            this.cmbRoleName.DisplayLayout.GroupByBox.BandLabelAppearance = appearance34;
            this.cmbRoleName.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance35.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance35.BackColor2 = System.Drawing.SystemColors.Control;
            appearance35.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance35.ForeColor = System.Drawing.SystemColors.GrayText;
            this.cmbRoleName.DisplayLayout.GroupByBox.PromptAppearance = appearance35;
            this.cmbRoleName.DisplayLayout.MaxColScrollRegions = 1;
            this.cmbRoleName.DisplayLayout.MaxRowScrollRegions = 1;
            appearance36.BackColor = System.Drawing.SystemColors.Window;
            appearance36.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmbRoleName.DisplayLayout.Override.ActiveCellAppearance = appearance36;
            appearance37.BackColor = System.Drawing.SystemColors.Highlight;
            appearance37.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.cmbRoleName.DisplayLayout.Override.ActiveRowAppearance = appearance37;
            this.cmbRoleName.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.cmbRoleName.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance38.BackColor = System.Drawing.SystemColors.Window;
            this.cmbRoleName.DisplayLayout.Override.CardAreaAppearance = appearance38;
            appearance39.BorderColor = System.Drawing.Color.Silver;
            appearance39.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.cmbRoleName.DisplayLayout.Override.CellAppearance = appearance39;
            this.cmbRoleName.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.cmbRoleName.DisplayLayout.Override.CellPadding = 0;
            appearance40.BackColor = System.Drawing.SystemColors.Control;
            appearance40.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance40.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance40.BorderColor = System.Drawing.SystemColors.Window;
            this.cmbRoleName.DisplayLayout.Override.GroupByRowAppearance = appearance40;
            appearance41.TextHAlignAsString = "Left";
            this.cmbRoleName.DisplayLayout.Override.HeaderAppearance = appearance41;
            this.cmbRoleName.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.cmbRoleName.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance42.BackColor = System.Drawing.SystemColors.Window;
            appearance42.BorderColor = System.Drawing.Color.Silver;
            this.cmbRoleName.DisplayLayout.Override.RowAppearance = appearance42;
            this.cmbRoleName.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance43.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cmbRoleName.DisplayLayout.Override.TemplateAddRowAppearance = appearance43;
            this.cmbRoleName.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.cmbRoleName.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.cmbRoleName.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.cmbRoleName.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
            this.cmbRoleName.Location = new System.Drawing.Point(80, 12);
            this.cmbRoleName.Name = "cmbRoleName";
            this.cmbRoleName.PreferredDropDownSize = new System.Drawing.Size(0, 0);
            this.cmbRoleName.Size = new System.Drawing.Size(254, 24);
            this.cmbRoleName.TabIndex = 0;
            this.cmbRoleName.ValueChanged += new System.EventHandler(this.cmbRoleName_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 14);
            this.label1.TabIndex = 63;
            this.label1.Text = "Role Name";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Location = new System.Drawing.Point(166, 459);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(81, 26);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(80, 459);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(81, 26);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.Location = new System.Drawing.Point(253, 459);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(81, 26);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmNewAccessright
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 491);
            this.Controls.Add(this.ultraGroupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmNewAccessright";
            this.Text = "Access right";
            this.Load += new System.EventHandler(this.frmNewAccessright_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            this.ultraGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).EndInit();
            this.ultraGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbRoleName)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private Infragistics.Win.Misc.UltraButton btUserRole;
        private Infragistics.Win.UltraWinGrid.UltraCombo cmbRoleName;
        private System.Windows.Forms.Label label1;
        private Infragistics.Win.Misc.UltraButton btnSave;
        private Infragistics.Win.Misc.UltraButton btnClose;
        private System.Windows.Forms.TreeView treeView1;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox2;
        private Infragistics.Win.Misc.UltraButton btnNewAction;
        private Infragistics.Win.Misc.UltraButton btnNewPermission;
        private Infragistics.Win.Misc.UltraButton btnRefresh;
    }
}