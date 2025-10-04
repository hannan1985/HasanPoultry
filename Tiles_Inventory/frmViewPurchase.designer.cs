namespace Tiles_Inventory
{
    partial class frmViewPurchase
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
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
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
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            this.UltraGroupBox3 = new Infragistics.Win.Misc.UltraGroupBox();
            this.btPurchaseHistory = new System.Windows.Forms.Button();
            this.btnAddCosting = new System.Windows.Forms.Button();
            this.btnApprove = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btRefresh = new System.Windows.Forms.Button();
            this.btEdit = new System.Windows.Forms.Button();
            this.btAdd = new System.Windows.Forms.Button();
            this.UltraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            this.txtInvoiceNumber = new System.Windows.Forms.TextBox();
            this.btViewByBillNumber = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btView = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.ultraGroupBox4 = new Infragistics.Win.Misc.UltraGroupBox();
            this.grvPurchaseOrderDetail = new System.Windows.Forms.DataGridView();
            this.btAddExpireDate = new System.Windows.Forms.Button();
            this.UltraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.grvPurchaseOrder = new Infragistics.Win.UltraWinGrid.UltraGrid();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox3)).BeginInit();
            this.UltraGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox2)).BeginInit();
            this.UltraGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox4)).BeginInit();
            this.ultraGroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvPurchaseOrderDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox1)).BeginInit();
            this.UltraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvPurchaseOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // UltraGroupBox3
            // 
            this.UltraGroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance15.BackColor = System.Drawing.Color.LightBlue;
            this.UltraGroupBox3.Appearance = appearance15;
            this.UltraGroupBox3.Controls.Add(this.btPurchaseHistory);
            this.UltraGroupBox3.Controls.Add(this.btnAddCosting);
            this.UltraGroupBox3.Controls.Add(this.btnApprove);
            this.UltraGroupBox3.Controls.Add(this.btnCancel);
            this.UltraGroupBox3.Controls.Add(this.btnClose);
            this.UltraGroupBox3.Controls.Add(this.btnPrint);
            this.UltraGroupBox3.Controls.Add(this.btRefresh);
            this.UltraGroupBox3.Controls.Add(this.btEdit);
            this.UltraGroupBox3.Controls.Add(this.btAdd);
            this.UltraGroupBox3.Location = new System.Drawing.Point(1, 277);
            this.UltraGroupBox3.Name = "UltraGroupBox3";
            this.UltraGroupBox3.Size = new System.Drawing.Size(1031, 50);
            this.UltraGroupBox3.TabIndex = 7;
            // 
            // btPurchaseHistory
            // 
            this.btPurchaseHistory.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btPurchaseHistory.Location = new System.Drawing.Point(672, 14);
            this.btPurchaseHistory.Name = "btPurchaseHistory";
            this.btPurchaseHistory.Size = new System.Drawing.Size(151, 23);
            this.btPurchaseHistory.TabIndex = 8;
            this.btPurchaseHistory.Text = "Product purchase history";
            this.btPurchaseHistory.UseVisualStyleBackColor = true;
            this.btPurchaseHistory.Click += new System.EventHandler(this.btnPurchaseHistory_Click);
            // 
            // btnAddCosting
            // 
            this.btnAddCosting.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAddCosting.Location = new System.Drawing.Point(434, 14);
            this.btnAddCosting.Name = "btnAddCosting";
            this.btnAddCosting.Size = new System.Drawing.Size(82, 23);
            this.btnAddCosting.TabIndex = 1;
            this.btnAddCosting.Text = "Add Costing";
            this.btnAddCosting.UseVisualStyleBackColor = true;
            this.btnAddCosting.Click += new System.EventHandler(this.btnAddCosting_Click);
            // 
            // btnApprove
            // 
            this.btnApprove.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnApprove.Location = new System.Drawing.Point(280, 14);
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.Size = new System.Drawing.Size(75, 23);
            this.btnApprove.TabIndex = 1;
            this.btnApprove.Text = "Approve";
            this.btnApprove.UseVisualStyleBackColor = true;
            this.btnApprove.Click += new System.EventHandler(this.btnApprove_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancel.Location = new System.Drawing.Point(357, 14);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClose.Location = new System.Drawing.Point(825, 14);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPrint.Location = new System.Drawing.Point(518, 14);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btRefresh
            // 
            this.btRefresh.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btRefresh.Location = new System.Drawing.Point(595, 14);
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(75, 23);
            this.btRefresh.TabIndex = 0;
            this.btRefresh.Text = "Refresh";
            this.btRefresh.UseVisualStyleBackColor = true;
            this.btRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btEdit
            // 
            this.btEdit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btEdit.Location = new System.Drawing.Point(203, 14);
            this.btEdit.Name = "btEdit";
            this.btEdit.Size = new System.Drawing.Size(75, 23);
            this.btEdit.TabIndex = 0;
            this.btEdit.Text = "Edit";
            this.btEdit.UseVisualStyleBackColor = true;
            this.btEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btAdd
            // 
            this.btAdd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btAdd.Location = new System.Drawing.Point(126, 14);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(75, 23);
            this.btAdd.TabIndex = 0;
            this.btAdd.Text = "Add New";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // UltraGroupBox2
            // 
            this.UltraGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance16.BackColor = System.Drawing.Color.LightBlue;
            this.UltraGroupBox2.Appearance = appearance16;
            this.UltraGroupBox2.Controls.Add(this.txtInvoiceNumber);
            this.UltraGroupBox2.Controls.Add(this.btViewByBillNumber);
            this.UltraGroupBox2.Controls.Add(this.label3);
            this.UltraGroupBox2.Controls.Add(this.btView);
            this.UltraGroupBox2.Controls.Add(this.Label2);
            this.UltraGroupBox2.Controls.Add(this.Label1);
            this.UltraGroupBox2.Controls.Add(this.dtpToDate);
            this.UltraGroupBox2.Controls.Add(this.dtpFromDate);
            this.UltraGroupBox2.Location = new System.Drawing.Point(2, 3);
            this.UltraGroupBox2.Name = "UltraGroupBox2";
            this.UltraGroupBox2.Size = new System.Drawing.Size(1031, 54);
            this.UltraGroupBox2.TabIndex = 5;
            // 
            // txtInvoiceNumber
            // 
            this.txtInvoiceNumber.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtInvoiceNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtInvoiceNumber.Location = new System.Drawing.Point(253, 16);
            this.txtInvoiceNumber.Name = "txtInvoiceNumber";
            this.txtInvoiceNumber.Size = new System.Drawing.Size(137, 22);
            this.txtInvoiceNumber.TabIndex = 8;
            // 
            // btViewByBillNumber
            // 
            this.btViewByBillNumber.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btViewByBillNumber.Location = new System.Drawing.Point(396, 16);
            this.btViewByBillNumber.Name = "btViewByBillNumber";
            this.btViewByBillNumber.Size = new System.Drawing.Size(75, 22);
            this.btViewByBillNumber.TabIndex = 7;
            this.btViewByBillNumber.Text = "View";
            this.btViewByBillNumber.UseVisualStyleBackColor = true;
            this.btViewByBillNumber.Click += new System.EventHandler(this.btViewByBillNumber_Click);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(182, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "Bill Number";
            // 
            // btView
            // 
            this.btView.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btView.Location = new System.Drawing.Point(773, 16);
            this.btView.Name = "btView";
            this.btView.Size = new System.Drawing.Size(75, 23);
            this.btView.TabIndex = 3;
            this.btView.Text = "View";
            this.btView.UseVisualStyleBackColor = true;
            this.btView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // Label2
            // 
            this.Label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Location = new System.Drawing.Point(637, 20);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(22, 14);
            this.Label2.TabIndex = 2;
            this.Label2.Text = "To";
            // 
            // Label1
            // 
            this.Label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Location = new System.Drawing.Point(483, 20);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(38, 14);
            this.Label1.TabIndex = 2;
            this.Label1.Text = "From ";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(663, 16);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(103, 22);
            this.dtpToDate.TabIndex = 1;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(525, 16);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(103, 22);
            this.dtpFromDate.TabIndex = 0;
            // 
            // ultraGroupBox4
            // 
            this.ultraGroupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance17.BackColor = System.Drawing.Color.LightBlue;
            this.ultraGroupBox4.Appearance = appearance17;
            this.ultraGroupBox4.Controls.Add(this.grvPurchaseOrderDetail);
            this.ultraGroupBox4.Controls.Add(this.btAddExpireDate);
            this.ultraGroupBox4.Location = new System.Drawing.Point(2, 330);
            this.ultraGroupBox4.Name = "ultraGroupBox4";
            this.ultraGroupBox4.Size = new System.Drawing.Size(1031, 169);
            this.ultraGroupBox4.TabIndex = 6;
            // 
            // grvPurchaseOrderDetail
            // 
            this.grvPurchaseOrderDetail.AllowUserToDeleteRows = false;
            this.grvPurchaseOrderDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grvPurchaseOrderDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grvPurchaseOrderDetail.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.grvPurchaseOrderDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvPurchaseOrderDetail.Location = new System.Drawing.Point(5, 26);
            this.grvPurchaseOrderDetail.Name = "grvPurchaseOrderDetail";
            this.grvPurchaseOrderDetail.ReadOnly = true;
            this.grvPurchaseOrderDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvPurchaseOrderDetail.Size = new System.Drawing.Size(1020, 137);
            this.grvPurchaseOrderDetail.TabIndex = 2;
            // 
            // btAddExpireDate
            // 
            this.btAddExpireDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btAddExpireDate.Location = new System.Drawing.Point(903, 2);
            this.btAddExpireDate.Name = "btAddExpireDate";
            this.btAddExpireDate.Size = new System.Drawing.Size(122, 23);
            this.btAddExpireDate.TabIndex = 1;
            this.btAddExpireDate.Text = "Add Expire Date";
            this.btAddExpireDate.UseVisualStyleBackColor = true;
            this.btAddExpireDate.Click += new System.EventHandler(this.btAddExpireDate_Click);
            // 
            // UltraGroupBox1
            // 
            this.UltraGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance18.BackColor = System.Drawing.Color.LightBlue;
            this.UltraGroupBox1.Appearance = appearance18;
            this.UltraGroupBox1.Controls.Add(this.grvPurchaseOrder);
            this.UltraGroupBox1.Location = new System.Drawing.Point(2, 59);
            this.UltraGroupBox1.Name = "UltraGroupBox1";
            this.UltraGroupBox1.Size = new System.Drawing.Size(1031, 215);
            this.UltraGroupBox1.TabIndex = 6;
            // 
            // grvPurchaseOrder
            // 
            this.grvPurchaseOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.grvPurchaseOrder.DisplayLayout.Appearance = appearance1;
            this.grvPurchaseOrder.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns;
            this.grvPurchaseOrder.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance2.BackColor = System.Drawing.Color.White;
            appearance2.BackColor2 = System.Drawing.Color.White;
            this.grvPurchaseOrder.DisplayLayout.CaptionAppearance = appearance2;
            appearance3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance3.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance3.BorderColor = System.Drawing.SystemColors.Window;
            this.grvPurchaseOrder.DisplayLayout.GroupByBox.Appearance = appearance3;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grvPurchaseOrder.DisplayLayout.GroupByBox.BandLabelAppearance = appearance4;
            this.grvPurchaseOrder.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance5.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance5.BackColor2 = System.Drawing.SystemColors.Control;
            appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grvPurchaseOrder.DisplayLayout.GroupByBox.PromptAppearance = appearance5;
            this.grvPurchaseOrder.DisplayLayout.MaxColScrollRegions = 1;
            this.grvPurchaseOrder.DisplayLayout.MaxRowScrollRegions = 1;
            appearance6.BackColor = System.Drawing.SystemColors.Window;
            appearance6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grvPurchaseOrder.DisplayLayout.Override.ActiveCellAppearance = appearance6;
            appearance7.BackColor = System.Drawing.SystemColors.Highlight;
            appearance7.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.grvPurchaseOrder.DisplayLayout.Override.ActiveRowAppearance = appearance7;
            this.grvPurchaseOrder.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.grvPurchaseOrder.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance8.BackColor = System.Drawing.SystemColors.Window;
            this.grvPurchaseOrder.DisplayLayout.Override.CardAreaAppearance = appearance8;
            appearance9.BorderColor = System.Drawing.Color.Silver;
            appearance9.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.grvPurchaseOrder.DisplayLayout.Override.CellAppearance = appearance9;
            this.grvPurchaseOrder.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            this.grvPurchaseOrder.DisplayLayout.Override.CellPadding = 0;
            appearance10.BackColor = System.Drawing.SystemColors.Control;
            appearance10.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance10.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance10.BorderColor = System.Drawing.SystemColors.Window;
            this.grvPurchaseOrder.DisplayLayout.Override.GroupByRowAppearance = appearance10;
            appearance11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(135)))), ((int)(((byte)(214)))));
            appearance11.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(59)))), ((int)(((byte)(150)))));
            appearance11.FontData.BoldAsString = "True";
            appearance11.ForeColor = System.Drawing.Color.White;
            appearance11.TextHAlignAsString = "Left";
            this.grvPurchaseOrder.DisplayLayout.Override.HeaderAppearance = appearance11;
            this.grvPurchaseOrder.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.grvPurchaseOrder.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance12.BackColor = System.Drawing.SystemColors.Window;
            appearance12.BorderColor = System.Drawing.Color.Silver;
            this.grvPurchaseOrder.DisplayLayout.Override.RowAppearance = appearance12;
            this.grvPurchaseOrder.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(135)))), ((int)(((byte)(214)))));
            appearance13.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(135)))), ((int)(((byte)(214)))));
            this.grvPurchaseOrder.DisplayLayout.Override.SelectedRowAppearance = appearance13;
            this.grvPurchaseOrder.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grvPurchaseOrder.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            appearance14.BackColor = System.Drawing.SystemColors.ControlLight;
            this.grvPurchaseOrder.DisplayLayout.Override.TemplateAddRowAppearance = appearance14;
            this.grvPurchaseOrder.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grvPurchaseOrder.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grvPurchaseOrder.Location = new System.Drawing.Point(5, 4);
            this.grvPurchaseOrder.Name = "grvPurchaseOrder";
            this.grvPurchaseOrder.Size = new System.Drawing.Size(1021, 207);
            this.grvPurchaseOrder.TabIndex = 158;
            this.grvPurchaseOrder.ClickCell += new Infragistics.Win.UltraWinGrid.ClickCellEventHandler(this.grvPurchaseOrder_ClickCell);
            // 
            // frmViewPurchase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 501);
            this.Controls.Add(this.UltraGroupBox3);
            this.Controls.Add(this.UltraGroupBox2);
            this.Controls.Add(this.ultraGroupBox4);
            this.Controls.Add(this.UltraGroupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmViewPurchase";
            this.Text = "View Purchase";
            this.Load += new System.EventHandler(this.frmViewPurchase_Load);
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox3)).EndInit();
            this.UltraGroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox2)).EndInit();
            this.UltraGroupBox2.ResumeLayout(false);
            this.UltraGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox4)).EndInit();
            this.ultraGroupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvPurchaseOrderDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox1)).EndInit();
            this.UltraGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvPurchaseOrder)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button btView;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.DateTimePicker dtpToDate;
        internal System.Windows.Forms.DateTimePicker dtpFromDate;
        internal Infragistics.Win.Misc.UltraGroupBox UltraGroupBox2;
        internal Infragistics.Win.Misc.UltraGroupBox UltraGroupBox1;
        internal Infragistics.Win.Misc.UltraGroupBox ultraGroupBox4;
        internal System.Windows.Forms.DataGridView grvPurchaseOrderDetail;
        internal Infragistics.Win.Misc.UltraGroupBox UltraGroupBox3;
        internal System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.Button btRefresh;
        internal System.Windows.Forms.Button btEdit;
        internal System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button btnApprove;
        private System.Windows.Forms.Button btPurchaseHistory;
        private System.Windows.Forms.Button btnAddCosting;
        internal System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtInvoiceNumber;
        internal System.Windows.Forms.Button btViewByBillNumber;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btAddExpireDate;
        private Infragistics.Win.UltraWinGrid.UltraGrid grvPurchaseOrder;
    }
}