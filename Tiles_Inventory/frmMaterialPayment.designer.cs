namespace Tiles_Inventory
{
    partial class frmMaterialPayment
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
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
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
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            this.btnClose = new System.Windows.Forms.Button();
            this.UltraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.cmbSupplierName = new Infragistics.Win.UltraWinGrid.UltraCombo();
            this.dtpPaymentDate = new System.Windows.Forms.DateTimePicker();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.UltraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox1)).BeginInit();
            this.UltraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSupplierName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox2)).BeginInit();
            this.UltraGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(286, 17);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // UltraGroupBox1
            // 
            appearance13.BackColor = System.Drawing.Color.LightBlue;
            this.UltraGroupBox1.Appearance = appearance13;
            this.UltraGroupBox1.Controls.Add(this.cmbSupplierName);
            this.UltraGroupBox1.Controls.Add(this.dtpPaymentDate);
            this.UltraGroupBox1.Controls.Add(this.Label5);
            this.UltraGroupBox1.Controls.Add(this.Label2);
            this.UltraGroupBox1.Controls.Add(this.Label4);
            this.UltraGroupBox1.Controls.Add(this.Label6);
            this.UltraGroupBox1.Controls.Add(this.Label3);
            this.UltraGroupBox1.Controls.Add(this.txtAmount);
            this.UltraGroupBox1.Location = new System.Drawing.Point(2, 2);
            this.UltraGroupBox1.Name = "UltraGroupBox1";
            this.UltraGroupBox1.Size = new System.Drawing.Size(484, 169);
            this.UltraGroupBox1.TabIndex = 0;
            // 
            // cmbSupplierName
            // 
            this.cmbSupplierName.CheckedListSettings.CheckStateMember = "";
            Appearance61.BackColor = System.Drawing.SystemColors.Window;
            Appearance61.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.cmbSupplierName.DisplayLayout.Appearance = Appearance61;
            this.cmbSupplierName.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.cmbSupplierName.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            Appearance62.BackColor = System.Drawing.SystemColors.ActiveBorder;
            Appearance62.BackColor2 = System.Drawing.SystemColors.ControlDark;
            Appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            Appearance62.BorderColor = System.Drawing.SystemColors.Window;
            this.cmbSupplierName.DisplayLayout.GroupByBox.Appearance = Appearance62;
            Appearance63.ForeColor = System.Drawing.SystemColors.GrayText;
            this.cmbSupplierName.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance63;
            this.cmbSupplierName.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            Appearance64.BackColor = System.Drawing.SystemColors.ControlLightLight;
            Appearance64.BackColor2 = System.Drawing.SystemColors.Control;
            Appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            Appearance64.ForeColor = System.Drawing.SystemColors.GrayText;
            this.cmbSupplierName.DisplayLayout.GroupByBox.PromptAppearance = Appearance64;
            this.cmbSupplierName.DisplayLayout.MaxColScrollRegions = 1;
            this.cmbSupplierName.DisplayLayout.MaxRowScrollRegions = 1;
            Appearance65.BackColor = System.Drawing.SystemColors.Window;
            Appearance65.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmbSupplierName.DisplayLayout.Override.ActiveCellAppearance = Appearance65;
            Appearance66.BackColor = System.Drawing.SystemColors.Highlight;
            Appearance66.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.cmbSupplierName.DisplayLayout.Override.ActiveRowAppearance = Appearance66;
            this.cmbSupplierName.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.cmbSupplierName.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            Appearance67.BackColor = System.Drawing.SystemColors.Window;
            this.cmbSupplierName.DisplayLayout.Override.CardAreaAppearance = Appearance67;
            Appearance68.BorderColor = System.Drawing.Color.Silver;
            Appearance68.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.cmbSupplierName.DisplayLayout.Override.CellAppearance = Appearance68;
            this.cmbSupplierName.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.cmbSupplierName.DisplayLayout.Override.CellPadding = 0;
            Appearance69.BackColor = System.Drawing.SystemColors.Control;
            Appearance69.BackColor2 = System.Drawing.SystemColors.ControlDark;
            Appearance69.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            Appearance69.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            Appearance69.BorderColor = System.Drawing.SystemColors.Window;
            this.cmbSupplierName.DisplayLayout.Override.GroupByRowAppearance = Appearance69;
            Appearance70.TextHAlignAsString = "Left";
            this.cmbSupplierName.DisplayLayout.Override.HeaderAppearance = Appearance70;
            this.cmbSupplierName.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.cmbSupplierName.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            Appearance71.BackColor = System.Drawing.SystemColors.Window;
            Appearance71.BorderColor = System.Drawing.Color.Silver;
            this.cmbSupplierName.DisplayLayout.Override.RowAppearance = Appearance71;
            this.cmbSupplierName.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            Appearance72.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cmbSupplierName.DisplayLayout.Override.TemplateAddRowAppearance = Appearance72;
            this.cmbSupplierName.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.cmbSupplierName.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.cmbSupplierName.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.cmbSupplierName.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
            this.cmbSupplierName.Location = new System.Drawing.Point(106, 35);
            this.cmbSupplierName.Name = "cmbSupplierName";
            this.cmbSupplierName.PreferredDropDownSize = new System.Drawing.Size(0, 0);
            this.cmbSupplierName.Size = new System.Drawing.Size(220, 24);
            this.cmbSupplierName.TabIndex = 1;
            this.cmbSupplierName.ValueChanged += new System.EventHandler(this.cmbSupplierName_SelectedValueChanged);
            // 
            // dtpPaymentDate
            // 
            this.dtpPaymentDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPaymentDate.Location = new System.Drawing.Point(106, 107);
            this.dtpPaymentDate.Name = "dtpPaymentDate";
            this.dtpPaymentDate.Size = new System.Drawing.Size(103, 22);
            this.dtpPaymentDate.TabIndex = 3;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.BackColor = System.Drawing.Color.Transparent;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.Location = new System.Drawing.Point(343, 34);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(101, 18);
            this.Label5.TabIndex = 17;
            this.Label5.Text = "Due Amount ";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(18, 40);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(85, 14);
            this.Label2.TabIndex = 11;
            this.Label2.Text = "Supplier Name";
            // 
            // Label4
            // 
            this.Label4.BackColor = System.Drawing.Color.Transparent;
            this.Label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(343, 56);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(127, 42);
            this.Label4.TabIndex = 16;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.BackColor = System.Drawing.Color.Transparent;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(18, 111);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(85, 14);
            this.Label6.TabIndex = 13;
            this.Label6.Text = "Payment Date";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(52, 75);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(51, 14);
            this.Label3.TabIndex = 13;
            this.Label3.Text = "Amount";
            // 
            // txtAmount
            // 
            this.txtAmount.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(106, 71);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(220, 23);
            this.txtAmount.TabIndex = 2;
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnRefresh.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(205, 17);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(124, 17);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // UltraGroupBox2
            // 
            appearance14.BackColor = System.Drawing.Color.LightBlue;
            this.UltraGroupBox2.Appearance = appearance14;
            this.UltraGroupBox2.Controls.Add(this.btnClose);
            this.UltraGroupBox2.Controls.Add(this.btnRefresh);
            this.UltraGroupBox2.Controls.Add(this.btnSave);
            this.UltraGroupBox2.Location = new System.Drawing.Point(2, 173);
            this.UltraGroupBox2.Name = "UltraGroupBox2";
            this.UltraGroupBox2.Size = new System.Drawing.Size(484, 56);
            this.UltraGroupBox2.TabIndex = 1;
            // 
            // frmMaterialPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 231);
            this.Controls.Add(this.UltraGroupBox1);
            this.Controls.Add(this.UltraGroupBox2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmMaterialPayment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Payment";
            this.Load += new System.EventHandler(this.PartyPayment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox1)).EndInit();
            this.UltraGroupBox1.ResumeLayout(false);
            this.UltraGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSupplierName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox2)).EndInit();
            this.UltraGroupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button btnClose;
        internal Infragistics.Win.Misc.UltraGroupBox UltraGroupBox1;
        internal System.Windows.Forms.DateTimePicker dtpPaymentDate;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox txtAmount;
        internal System.Windows.Forms.Button btnRefresh;
        internal System.Windows.Forms.Button btnSave;
        internal Infragistics.Win.Misc.UltraGroupBox UltraGroupBox2;
        internal Infragistics.Win.UltraWinGrid.UltraCombo cmbSupplierName;
    }
}