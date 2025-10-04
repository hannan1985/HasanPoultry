namespace Tiles_Inventory
{
    partial class frmProductPurchaseHistory
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
            this.ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.btnLoad = new System.Windows.Forms.Button();
            this.cmbProductInformation = new Infragistics.Win.UltraWinGrid.UltraCombo();
            this.Label27 = new System.Windows.Forms.Label();
            this.txtTotalQuantity = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.grvCreditProductDetails = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).BeginInit();
            this.ultraGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProductInformation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvCreditProductDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraGroupBox2
            // 
            this.ultraGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ultraGroupBox2.Controls.Add(this.label3);
            this.ultraGroupBox2.Controls.Add(this.label4);
            this.ultraGroupBox2.Controls.Add(this.dtpToDate);
            this.ultraGroupBox2.Controls.Add(this.dtpFromDate);
            this.ultraGroupBox2.Controls.Add(this.btnLoad);
            this.ultraGroupBox2.Controls.Add(this.cmbProductInformation);
            this.ultraGroupBox2.Controls.Add(this.Label27);
            this.ultraGroupBox2.Location = new System.Drawing.Point(0, 2);
            this.ultraGroupBox2.Name = "ultraGroupBox2";
            this.ultraGroupBox2.Size = new System.Drawing.Size(883, 71);
            this.ultraGroupBox2.TabIndex = 3;
            this.ultraGroupBox2.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(546, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 14);
            this.label3.TabIndex = 133;
            this.label3.Text = "To";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(392, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 14);
            this.label4.TabIndex = 134;
            this.label4.Text = "From ";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(572, 23);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(103, 22);
            this.dtpToDate.TabIndex = 132;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(434, 23);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(103, 22);
            this.dtpFromDate.TabIndex = 131;
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Location = new System.Drawing.Point(683, 23);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 130;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // cmbProductInformation
            // 
            this.cmbProductInformation.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
            this.cmbProductInformation.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
            this.cmbProductInformation.CheckedListSettings.CheckStateMember = "";
            Appearance13.BackColor = System.Drawing.SystemColors.Window;
            Appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.cmbProductInformation.DisplayLayout.Appearance = Appearance13;
            this.cmbProductInformation.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns;
            this.cmbProductInformation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.cmbProductInformation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            Appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
            Appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
            Appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            Appearance14.BorderColor = System.Drawing.SystemColors.Window;
            this.cmbProductInformation.DisplayLayout.GroupByBox.Appearance = Appearance14;
            Appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
            this.cmbProductInformation.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance15;
            this.cmbProductInformation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            Appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
            Appearance16.BackColor2 = System.Drawing.SystemColors.Control;
            Appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            Appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
            this.cmbProductInformation.DisplayLayout.GroupByBox.PromptAppearance = Appearance16;
            this.cmbProductInformation.DisplayLayout.MaxColScrollRegions = 1;
            this.cmbProductInformation.DisplayLayout.MaxRowScrollRegions = 1;
            Appearance17.BackColor = System.Drawing.SystemColors.Window;
            Appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmbProductInformation.DisplayLayout.Override.ActiveCellAppearance = Appearance17;
            Appearance18.BackColor = System.Drawing.SystemColors.Highlight;
            Appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.cmbProductInformation.DisplayLayout.Override.ActiveRowAppearance = Appearance18;
            this.cmbProductInformation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.cmbProductInformation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            Appearance19.BackColor = System.Drawing.SystemColors.Window;
            this.cmbProductInformation.DisplayLayout.Override.CardAreaAppearance = Appearance19;
            Appearance20.BorderColor = System.Drawing.Color.Silver;
            Appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.cmbProductInformation.DisplayLayout.Override.CellAppearance = Appearance20;
            this.cmbProductInformation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.cmbProductInformation.DisplayLayout.Override.CellPadding = 0;
            Appearance21.BackColor = System.Drawing.SystemColors.Control;
            Appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
            Appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            Appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            Appearance21.BorderColor = System.Drawing.SystemColors.Window;
            this.cmbProductInformation.DisplayLayout.Override.GroupByRowAppearance = Appearance21;
            Appearance22.TextHAlignAsString = "Left";
            this.cmbProductInformation.DisplayLayout.Override.HeaderAppearance = Appearance22;
            this.cmbProductInformation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.cmbProductInformation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            Appearance23.BackColor = System.Drawing.SystemColors.Window;
            Appearance23.BorderColor = System.Drawing.Color.Silver;
            this.cmbProductInformation.DisplayLayout.Override.RowAppearance = Appearance23;
            this.cmbProductInformation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            Appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cmbProductInformation.DisplayLayout.Override.TemplateAddRowAppearance = Appearance24;
            this.cmbProductInformation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.cmbProductInformation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.cmbProductInformation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.cmbProductInformation.Location = new System.Drawing.Point(99, 22);
            this.cmbProductInformation.Name = "cmbProductInformation";
            this.cmbProductInformation.PreferredDropDownSize = new System.Drawing.Size(0, 0);
            this.cmbProductInformation.Size = new System.Drawing.Size(269, 24);
            this.cmbProductInformation.TabIndex = 128;
            // 
            // Label27
            // 
            this.Label27.AutoSize = true;
            this.Label27.BackColor = System.Drawing.Color.Transparent;
            this.Label27.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label27.Location = new System.Drawing.Point(8, 27);
            this.Label27.Name = "Label27";
            this.Label27.Size = new System.Drawing.Size(85, 14);
            this.Label27.TabIndex = 129;
            this.Label27.Text = "Product Name";
            // 
            // txtTotalQuantity
            // 
            this.txtTotalQuantity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalQuantity.Location = new System.Drawing.Point(780, 370);
            this.txtTotalQuantity.Name = "txtTotalQuantity";
            this.txtTotalQuantity.ReadOnly = true;
            this.txtTotalQuantity.Size = new System.Drawing.Size(100, 22);
            this.txtTotalQuantity.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(689, 374);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 14);
            this.label1.TabIndex = 9;
            this.label1.Text = "Total Quantity";
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ultraGroupBox1.Controls.Add(this.txtTotalQuantity);
            this.ultraGroupBox1.Controls.Add(this.label1);
            this.ultraGroupBox1.Controls.Add(this.grvCreditProductDetails);
            this.ultraGroupBox1.Location = new System.Drawing.Point(0, 75);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(883, 398);
            this.ultraGroupBox1.TabIndex = 2;
            this.ultraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // grvCreditProductDetails
            // 
            this.grvCreditProductDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grvCreditProductDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grvCreditProductDetails.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.grvCreditProductDetails.Location = new System.Drawing.Point(3, 6);
            this.grvCreditProductDetails.Name = "grvCreditProductDetails";
            this.grvCreditProductDetails.Size = new System.Drawing.Size(877, 351);
            this.grvCreditProductDetails.TabIndex = 8;
            this.grvCreditProductDetails.TabStop = false;
            // 
            // frmProductPurchaseHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 475);
            this.Controls.Add(this.ultraGroupBox2);
            this.Controls.Add(this.ultraGroupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProductPurchaseHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Product Purchase History";
            this.Load += new System.EventHandler(this.frmProductPurchaseHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).EndInit();
            this.ultraGroupBox2.ResumeLayout(false);
            this.ultraGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProductInformation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            this.ultraGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvCreditProductDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox2;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.DateTimePicker dtpToDate;
        internal System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Button btnLoad;
        internal Infragistics.Win.UltraWinGrid.UltraCombo cmbProductInformation;
        internal System.Windows.Forms.Label Label27;
        private System.Windows.Forms.TextBox txtTotalQuantity;
        private System.Windows.Forms.Label label1;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        internal System.Windows.Forms.DataGridView grvCreditProductDetails;
    }
}