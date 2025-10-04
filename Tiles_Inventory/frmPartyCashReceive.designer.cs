namespace Tiles_Inventory
{
    partial class frmCashPartyReceive
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
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.cmbCustomerName = new Infragistics.Win.UltraWinGrid.UltraCombo();
            this.dtpReceiveDate = new System.Windows.Forms.DateTimePicker();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtReceiveAmount = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.Label4 = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.UltraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            this.UltraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCustomerName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox2)).BeginInit();
            this.UltraGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox1)).BeginInit();
            this.UltraGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(126, 20);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(286, 20);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cmbCustomerName
            // 
            this.cmbCustomerName.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.SuggestAppend;
            this.cmbCustomerName.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
            this.cmbCustomerName.CheckedListSettings.CheckStateMember = "";
            Appearance1.BackColor = System.Drawing.SystemColors.Window;
            Appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.cmbCustomerName.DisplayLayout.Appearance = Appearance1;
            this.cmbCustomerName.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.cmbCustomerName.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            Appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            Appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            Appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            Appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.cmbCustomerName.DisplayLayout.GroupByBox.Appearance = Appearance2;
            Appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.cmbCustomerName.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance3;
            this.cmbCustomerName.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            Appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            Appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            Appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            Appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.cmbCustomerName.DisplayLayout.GroupByBox.PromptAppearance = Appearance4;
            this.cmbCustomerName.DisplayLayout.MaxColScrollRegions = 1;
            this.cmbCustomerName.DisplayLayout.MaxRowScrollRegions = 1;
            Appearance5.BackColor = System.Drawing.SystemColors.Window;
            Appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmbCustomerName.DisplayLayout.Override.ActiveCellAppearance = Appearance5;
            Appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            Appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.cmbCustomerName.DisplayLayout.Override.ActiveRowAppearance = Appearance6;
            this.cmbCustomerName.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.cmbCustomerName.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            Appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.cmbCustomerName.DisplayLayout.Override.CardAreaAppearance = Appearance7;
            Appearance8.BorderColor = System.Drawing.Color.Silver;
            Appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.cmbCustomerName.DisplayLayout.Override.CellAppearance = Appearance8;
            this.cmbCustomerName.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.cmbCustomerName.DisplayLayout.Override.CellPadding = 0;
            Appearance9.BackColor = System.Drawing.SystemColors.Control;
            Appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            Appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            Appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            Appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.cmbCustomerName.DisplayLayout.Override.GroupByRowAppearance = Appearance9;
            Appearance10.TextHAlignAsString = "Left";
            this.cmbCustomerName.DisplayLayout.Override.HeaderAppearance = Appearance10;
            this.cmbCustomerName.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.cmbCustomerName.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            Appearance11.BackColor = System.Drawing.SystemColors.Window;
            Appearance11.BorderColor = System.Drawing.Color.Silver;
            this.cmbCustomerName.DisplayLayout.Override.RowAppearance = Appearance11;
            this.cmbCustomerName.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            Appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cmbCustomerName.DisplayLayout.Override.TemplateAddRowAppearance = Appearance12;
            this.cmbCustomerName.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.cmbCustomerName.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.cmbCustomerName.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.cmbCustomerName.Location = new System.Drawing.Point(121, 41);
            this.cmbCustomerName.Name = "cmbCustomerName";
            this.cmbCustomerName.PreferredDropDownSize = new System.Drawing.Size(0, 0);
            this.cmbCustomerName.Size = new System.Drawing.Size(210, 24);
            this.cmbCustomerName.TabIndex = 0;
            this.cmbCustomerName.ValueChanged += new System.EventHandler(this.cmbCustomerName_ValueChanged);
            // 
            // dtpReceiveDate
            // 
            this.dtpReceiveDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpReceiveDate.Location = new System.Drawing.Point(121, 155);
            this.dtpReceiveDate.Name = "dtpReceiveDate";
            this.dtpReceiveDate.Size = new System.Drawing.Size(103, 22);
            this.dtpReceiveDate.TabIndex = 4;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Location = new System.Drawing.Point(21, 75);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(97, 14);
            this.Label1.TabIndex = 101;
            this.Label1.Text = "Receive Amount";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.Location = new System.Drawing.Point(40, 159);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(79, 14);
            this.Label3.TabIndex = 101;
            this.Label3.Text = "Receive Date";
            // 
            // txtReceiveAmount
            // 
            this.txtReceiveAmount.Location = new System.Drawing.Point(121, 71);
            this.txtReceiveAmount.Name = "txtReceiveAmount";
            this.txtReceiveAmount.Size = new System.Drawing.Size(210, 22);
            this.txtReceiveAmount.TabIndex = 1;
            this.txtReceiveAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtReceiveAmount_KeyPress);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Location = new System.Drawing.Point(48, 46);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(70, 14);
            this.Label2.TabIndex = 101;
            this.Label2.Text = "Party Name";
            // 
            // btnRefresh
            // 
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnRefresh.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(206, 20);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnWholeSales_Click);
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.BackColor = System.Drawing.Color.Transparent;
            this.Label4.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(351, 44);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(96, 18);
            this.Label4.TabIndex = 101;
            this.Label4.Text = "Due amount";
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.ForeColor = System.Drawing.Color.Red;
            this.lblTotalAmount.Location = new System.Drawing.Point(348, 70);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(131, 39);
            this.lblTotalAmount.TabIndex = 101;
            this.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UltraGroupBox2
            // 
            appearance2.BackColor = System.Drawing.Color.LightBlue;
            this.UltraGroupBox2.Appearance = appearance2;
            this.UltraGroupBox2.Controls.Add(this.btnSave);
            this.UltraGroupBox2.Controls.Add(this.btnRefresh);
            this.UltraGroupBox2.Controls.Add(this.btnClose);
            this.UltraGroupBox2.Location = new System.Drawing.Point(1, 206);
            this.UltraGroupBox2.Name = "UltraGroupBox2";
            this.UltraGroupBox2.Size = new System.Drawing.Size(487, 63);
            this.UltraGroupBox2.TabIndex = 1;
            // 
            // UltraGroupBox1
            // 
            appearance1.BackColor = System.Drawing.Color.LightBlue;
            this.UltraGroupBox1.Appearance = appearance1;
            this.UltraGroupBox1.Controls.Add(this.cmbCustomerName);
            this.UltraGroupBox1.Controls.Add(this.Label4);
            this.UltraGroupBox1.Controls.Add(this.lblTotalAmount);
            this.UltraGroupBox1.Controls.Add(this.dtpReceiveDate);
            this.UltraGroupBox1.Controls.Add(this.label5);
            this.UltraGroupBox1.Controls.Add(this.label6);
            this.UltraGroupBox1.Controls.Add(this.Label1);
            this.UltraGroupBox1.Controls.Add(this.txtReferenceNo);
            this.UltraGroupBox1.Controls.Add(this.Label3);
            this.UltraGroupBox1.Controls.Add(this.txtDiscount);
            this.UltraGroupBox1.Controls.Add(this.txtReceiveAmount);
            this.UltraGroupBox1.Controls.Add(this.Label2);
            this.UltraGroupBox1.Location = new System.Drawing.Point(1, 2);
            this.UltraGroupBox1.Name = "UltraGroupBox1";
            this.UltraGroupBox1.Size = new System.Drawing.Size(487, 200);
            this.UltraGroupBox1.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(8, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 14);
            this.label5.TabIndex = 101;
            this.label5.Text = "Reference Number";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(64, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 14);
            this.label6.TabIndex = 101;
            this.label6.Text = "Discount";
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.Location = new System.Drawing.Point(121, 126);
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(210, 22);
            this.txtReferenceNo.TabIndex = 3;
            this.txtReferenceNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtReceiveAmount_KeyPress);
            // 
            // txtDiscount
            // 
            this.txtDiscount.Location = new System.Drawing.Point(121, 98);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(210, 22);
            this.txtDiscount.TabIndex = 2;
            this.txtDiscount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtReceiveAmount_KeyPress);
            // 
            // frmCashPartyReceive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 272);
            this.Controls.Add(this.UltraGroupBox2);
            this.Controls.Add(this.UltraGroupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmCashPartyReceive";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Party Receive";
            this.Load += new System.EventHandler(this.frmCashPartyReceive_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbCustomerName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox2)).EndInit();
            this.UltraGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox1)).EndInit();
            this.UltraGroupBox1.ResumeLayout(false);
            this.UltraGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.Button btnClose;
        internal Infragistics.Win.UltraWinGrid.UltraCombo cmbCustomerName;
        internal System.Windows.Forms.DateTimePicker dtpReceiveDate;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox txtReceiveAmount;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Button btnRefresh;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label lblTotalAmount;
        internal Infragistics.Win.Misc.UltraGroupBox UltraGroupBox2;
        internal Infragistics.Win.Misc.UltraGroupBox UltraGroupBox1;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.TextBox txtReferenceNo;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.TextBox txtDiscount;
    }
}