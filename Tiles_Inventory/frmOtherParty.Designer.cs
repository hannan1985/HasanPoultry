namespace Tiles_Inventory
{
    partial class frmOtherParty
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
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("", -1);
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            this.UltraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.grvSalesPartyInformation = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.btnSupplierClose = new System.Windows.Forms.Button();
            this.txtSalesPartyName = new System.Windows.Forms.TextBox();
            this.btnSupplierSave = new System.Windows.Forms.Button();
            this.btnSupplierUpdate = new System.Windows.Forms.Button();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.Label33 = new System.Windows.Forms.Label();
            this.Label34 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox1)).BeginInit();
            this.UltraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvSalesPartyInformation)).BeginInit();
            this.SuspendLayout();
            // 
            // UltraGroupBox1
            // 
            this.UltraGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance2.BackColor = System.Drawing.Color.LightBlue;
            this.UltraGroupBox1.Appearance = appearance2;
            this.UltraGroupBox1.Controls.Add(this.grvSalesPartyInformation);
            this.UltraGroupBox1.Controls.Add(this.btnSupplierClose);
            this.UltraGroupBox1.Controls.Add(this.txtSalesPartyName);
            this.UltraGroupBox1.Controls.Add(this.btnSupplierSave);
            this.UltraGroupBox1.Controls.Add(this.btnSupplierUpdate);
            this.UltraGroupBox1.Controls.Add(this.txtPhoneNumber);
            this.UltraGroupBox1.Controls.Add(this.Label33);
            this.UltraGroupBox1.Controls.Add(this.Label34);
            this.UltraGroupBox1.Controls.Add(this.txtAddress);
            this.UltraGroupBox1.Controls.Add(this.label5);
            this.UltraGroupBox1.Location = new System.Drawing.Point(3, 2);
            this.UltraGroupBox1.Name = "UltraGroupBox1";
            this.UltraGroupBox1.Size = new System.Drawing.Size(711, 466);
            this.UltraGroupBox1.TabIndex = 0;
            // 
            // grvSalesPartyInformation
            // 
            this.grvSalesPartyInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.grvSalesPartyInformation.DisplayLayout.Appearance = appearance5;
            this.grvSalesPartyInformation.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns;
            ultraGridBand1.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            this.grvSalesPartyInformation.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grvSalesPartyInformation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.grvSalesPartyInformation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance6.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance6.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance6.BorderColor = System.Drawing.SystemColors.Window;
            this.grvSalesPartyInformation.DisplayLayout.GroupByBox.Appearance = appearance6;
            appearance7.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grvSalesPartyInformation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance7;
            this.grvSalesPartyInformation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance8.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance8.BackColor2 = System.Drawing.SystemColors.Control;
            appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance8.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grvSalesPartyInformation.DisplayLayout.GroupByBox.PromptAppearance = appearance8;
            this.grvSalesPartyInformation.DisplayLayout.MaxColScrollRegions = 1;
            this.grvSalesPartyInformation.DisplayLayout.MaxRowScrollRegions = 1;
            appearance9.BackColor = System.Drawing.SystemColors.Window;
            appearance9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grvSalesPartyInformation.DisplayLayout.Override.ActiveCellAppearance = appearance9;
            appearance10.BackColor = System.Drawing.SystemColors.Highlight;
            appearance10.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.grvSalesPartyInformation.DisplayLayout.Override.ActiveRowAppearance = appearance10;
            this.grvSalesPartyInformation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.grvSalesPartyInformation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            this.grvSalesPartyInformation.DisplayLayout.Override.CardAreaAppearance = appearance11;
            appearance12.BorderColor = System.Drawing.Color.Silver;
            appearance12.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.grvSalesPartyInformation.DisplayLayout.Override.CellAppearance = appearance12;
            this.grvSalesPartyInformation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.grvSalesPartyInformation.DisplayLayout.Override.CellPadding = 0;
            appearance13.BackColor = System.Drawing.SystemColors.Control;
            appearance13.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance13.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance13.BorderColor = System.Drawing.SystemColors.Window;
            this.grvSalesPartyInformation.DisplayLayout.Override.GroupByRowAppearance = appearance13;
            appearance14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(135)))), ((int)(((byte)(214)))));
            appearance14.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(59)))), ((int)(((byte)(150)))));
            appearance14.ForeColor = System.Drawing.Color.White;
            appearance14.TextHAlignAsString = "Left";
            this.grvSalesPartyInformation.DisplayLayout.Override.HeaderAppearance = appearance14;
            this.grvSalesPartyInformation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.grvSalesPartyInformation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance15.BackColor = System.Drawing.SystemColors.Window;
            appearance15.BorderColor = System.Drawing.Color.Silver;
            this.grvSalesPartyInformation.DisplayLayout.Override.RowAppearance = appearance15;
            this.grvSalesPartyInformation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance16.BackColor = System.Drawing.SystemColors.ControlLight;
            this.grvSalesPartyInformation.DisplayLayout.Override.TemplateAddRowAppearance = appearance16;
            this.grvSalesPartyInformation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grvSalesPartyInformation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grvSalesPartyInformation.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grvSalesPartyInformation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.Horizontal;
            this.grvSalesPartyInformation.Location = new System.Drawing.Point(6, 200);
            this.grvSalesPartyInformation.Name = "grvSalesPartyInformation";
            this.grvSalesPartyInformation.Size = new System.Drawing.Size(699, 259);
            this.grvSalesPartyInformation.TabIndex = 6;
            this.grvSalesPartyInformation.ClickCell += new Infragistics.Win.UltraWinGrid.ClickCellEventHandler(this.grvSalesPartyInformation_ClickCell);
            // 
            // btnSupplierClose
            // 
            this.btnSupplierClose.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSupplierClose.Location = new System.Drawing.Point(262, 164);
            this.btnSupplierClose.Name = "btnSupplierClose";
            this.btnSupplierClose.Size = new System.Drawing.Size(75, 23);
            this.btnSupplierClose.TabIndex = 5;
            this.btnSupplierClose.Text = "Close";
            this.btnSupplierClose.UseVisualStyleBackColor = true;
            this.btnSupplierClose.Click += new System.EventHandler(this.btnSupplierClose_Click);
            // 
            // txtSalesPartyName
            // 
            this.txtSalesPartyName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSalesPartyName.Location = new System.Drawing.Point(102, 18);
            this.txtSalesPartyName.Name = "txtSalesPartyName";
            this.txtSalesPartyName.Size = new System.Drawing.Size(207, 22);
            this.txtSalesPartyName.TabIndex = 0;
            // 
            // btnSupplierSave
            // 
            this.btnSupplierSave.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSupplierSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSupplierSave.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSupplierSave.Location = new System.Drawing.Point(102, 164);
            this.btnSupplierSave.Name = "btnSupplierSave";
            this.btnSupplierSave.Size = new System.Drawing.Size(75, 23);
            this.btnSupplierSave.TabIndex = 3;
            this.btnSupplierSave.Text = "Save";
            this.btnSupplierSave.UseVisualStyleBackColor = true;
            this.btnSupplierSave.Click += new System.EventHandler(this.btnSupplierSave_Click);
            // 
            // btnSupplierUpdate
            // 
            this.btnSupplierUpdate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSupplierUpdate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSupplierUpdate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSupplierUpdate.Location = new System.Drawing.Point(181, 164);
            this.btnSupplierUpdate.Name = "btnSupplierUpdate";
            this.btnSupplierUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnSupplierUpdate.TabIndex = 4;
            this.btnSupplierUpdate.Text = "Rest";
            this.btnSupplierUpdate.UseVisualStyleBackColor = true;
            this.btnSupplierUpdate.Click += new System.EventHandler(this.btnSupplierUpdate_Click);
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhoneNumber.Location = new System.Drawing.Point(102, 111);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(207, 22);
            this.txtPhoneNumber.TabIndex = 2;
            // 
            // Label33
            // 
            this.Label33.AutoSize = true;
            this.Label33.BackColor = System.Drawing.Color.Transparent;
            this.Label33.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label33.Location = new System.Drawing.Point(49, 50);
            this.Label33.Name = "Label33";
            this.Label33.Size = new System.Drawing.Size(50, 14);
            this.Label33.TabIndex = 186;
            this.Label33.Text = "Address";
            // 
            // Label34
            // 
            this.Label34.AutoSize = true;
            this.Label34.BackColor = System.Drawing.Color.Transparent;
            this.Label34.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label34.Location = new System.Drawing.Point(29, 21);
            this.Label34.Name = "Label34";
            this.Label34.Size = new System.Drawing.Size(70, 14);
            this.Label34.TabIndex = 185;
            this.Label34.Text = "Party Name";
            // 
            // txtAddress
            // 
            this.txtAddress.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.Location = new System.Drawing.Point(102, 46);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(268, 59);
            this.txtAddress.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(10, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 14);
            this.label5.TabIndex = 184;
            this.label5.Text = "Phone Number";
            // 
            // frmOtherParty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 471);
            this.Controls.Add(this.UltraGroupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmOtherParty";
            this.Text = "Other Party Information";
            this.Load += new System.EventHandler(this.frmSalesParty_Load);
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox1)).EndInit();
            this.UltraGroupBox1.ResumeLayout(false);
            this.UltraGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvSalesPartyInformation)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.TextBox txtSalesPartyName;
        internal System.Windows.Forms.TextBox txtPhoneNumber;
        internal System.Windows.Forms.Label Label33;
        internal System.Windows.Forms.Label Label34;
        internal System.Windows.Forms.TextBox txtAddress;
        internal System.Windows.Forms.Button btnSupplierClose;
        internal System.Windows.Forms.Button btnSupplierSave;
        internal System.Windows.Forms.Button btnSupplierUpdate;
        internal System.Windows.Forms.Label label5;
        internal Infragistics.Win.Misc.UltraGroupBox UltraGroupBox1;
        private Infragistics.Win.UltraWinGrid.UltraGrid grvSalesPartyInformation;
    }
}