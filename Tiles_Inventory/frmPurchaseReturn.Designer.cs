namespace Tiles_Inventory
{
    partial class frmPurchaseReturn
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
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
            Infragistics.Win.Appearance Appearance49 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance50 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance51 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance52 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance53 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance54 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance55 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance56 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance57 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance58 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance59 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance Appearance60 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            this.txtQuantity = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
            this.btnSelectProduct = new System.Windows.Forms.Button();
            this.txtPurchasePrice = new System.Windows.Forms.TextBox();
            this.txtProductSize = new System.Windows.Forms.TextBox();
            this.txtPackSize = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtInvoiceNumber = new System.Windows.Forms.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.txtReceiveAmount = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Label7 = new System.Windows.Forms.Label();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.grvPurchaseDetails = new System.Windows.Forms.DataGridView();
            this.ProductID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Delete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btAdd = new System.Windows.Forms.Button();
            this.Label4 = new System.Windows.Forms.Label();
            this.cmbProductName = new Infragistics.Win.UltraWinGrid.UltraCombo();
            this.CmbSupplierName = new Infragistics.Win.UltraWinGrid.UltraCombo();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.btRefresh = new System.Windows.Forms.Button();
            this.UltraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.dtpReturnDate = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.Label29 = new System.Windows.Forms.Label();
            this.btEdit = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.UltraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvPurchaseDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProductName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbSupplierName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox1)).BeginInit();
            this.UltraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox2)).BeginInit();
            this.UltraGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(253, 125);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
            this.txtQuantity.PromptChar = ' ';
            this.txtQuantity.Size = new System.Drawing.Size(71, 23);
            this.txtQuantity.TabIndex = 5;
            this.txtQuantity.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtQuantity_KeyUp);
            // 
            // btnSelectProduct
            // 
            this.btnSelectProduct.Location = new System.Drawing.Point(109, 154);
            this.btnSelectProduct.Name = "btnSelectProduct";
            this.btnSelectProduct.Size = new System.Drawing.Size(215, 23);
            this.btnSelectProduct.TabIndex = 9;
            this.btnSelectProduct.Text = "Select Product";
            this.btnSelectProduct.UseVisualStyleBackColor = true;
            // 
            // txtPurchasePrice
            // 
            this.txtPurchasePrice.Location = new System.Drawing.Point(109, 126);
            this.txtPurchasePrice.Name = "txtPurchasePrice";
            this.txtPurchasePrice.Size = new System.Drawing.Size(71, 22);
            this.txtPurchasePrice.TabIndex = 4;
            // 
            // txtProductSize
            // 
            this.txtProductSize.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProductSize.Location = new System.Drawing.Point(587, 125);
            this.txtProductSize.Name = "txtProductSize";
            this.txtProductSize.ReadOnly = true;
            this.txtProductSize.Size = new System.Drawing.Size(36, 22);
            this.txtProductSize.TabIndex = 8;
            this.txtProductSize.TabStop = false;
            // 
            // txtPackSize
            // 
            this.txtPackSize.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPackSize.Location = new System.Drawing.Point(429, 125);
            this.txtPackSize.Name = "txtPackSize";
            this.txtPackSize.ReadOnly = true;
            this.txtPackSize.Size = new System.Drawing.Size(76, 22);
            this.txtPackSize.TabIndex = 7;
            this.txtPackSize.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(509, 129);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(75, 14);
            this.label11.TabIndex = 86;
            this.label11.Text = "Product Size";
            // 
            // txtInvoiceNumber
            // 
            this.txtInvoiceNumber.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInvoiceNumber.Location = new System.Drawing.Point(109, 39);
            this.txtInvoiceNumber.Name = "txtInvoiceNumber";
            this.txtInvoiceNumber.Size = new System.Drawing.Size(215, 22);
            this.txtInvoiceNumber.TabIndex = 1;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.BackColor = System.Drawing.Color.Transparent;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(369, 129);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(57, 14);
            this.Label6.TabIndex = 86;
            this.Label6.Text = "Pack Size";
            // 
            // Label8
            // 
            this.Label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label8.AutoSize = true;
            this.Label8.BackColor = System.Drawing.Color.Transparent;
            this.Label8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label8.Location = new System.Drawing.Point(690, 426);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(100, 16);
            this.Label8.TabIndex = 91;
            this.Label8.Text = "Receive Amount";
            // 
            // txtReceiveAmount
            // 
            this.txtReceiveAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReceiveAmount.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReceiveAmount.Location = new System.Drawing.Point(791, 423);
            this.txtReceiveAmount.Name = "txtReceiveAmount";
            this.txtReceiveAmount.Size = new System.Drawing.Size(191, 23);
            this.txtReceiveAmount.TabIndex = 12;
            this.txtReceiveAmount.Text = "0";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(37, 43);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(70, 14);
            this.Label1.TabIndex = 86;
            this.Label1.Text = "PO Number";
            // 
            // Label7
            // 
            this.Label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label7.AutoSize = true;
            this.Label7.BackColor = System.Drawing.Color.Transparent;
            this.Label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.Location = new System.Drawing.Point(753, 389);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(37, 16);
            this.Label7.TabIndex = 90;
            this.Label7.Text = "Total";
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalAmount.Enabled = false;
            this.txtTotalAmount.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalAmount.Location = new System.Drawing.Point(791, 386);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.Size = new System.Drawing.Size(191, 23);
            this.txtTotalAmount.TabIndex = 11;
            // 
            // grvPurchaseDetails
            // 
            this.grvPurchaseDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grvPurchaseDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grvPurchaseDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProductID,
            this.ProductName,
            this.Quantity,
            this.UnitPrice,
            this.Total,
            this.Delete});
            this.grvPurchaseDetails.Location = new System.Drawing.Point(23, 187);
            this.grvPurchaseDetails.Name = "grvPurchaseDetails";
            this.grvPurchaseDetails.Size = new System.Drawing.Size(960, 191);
            this.grvPurchaseDetails.TabIndex = 10;
            this.grvPurchaseDetails.TabStop = false;
            this.grvPurchaseDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grvPurchaseDetails_CellContentClick);
            // 
            // ProductID
            // 
            this.ProductID.HeaderText = "ProductID";
            this.ProductID.Name = "ProductID";
            this.ProductID.Visible = false;
            // 
            // ProductName
            // 
            this.ProductName.HeaderText = "Product Name";
            this.ProductName.Name = "ProductName";
            this.ProductName.ReadOnly = true;
            this.ProductName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Quantity
            // 
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.Name = "Quantity";
            // 
            // UnitPrice
            // 
            this.UnitPrice.HeaderText = "Purchase Price";
            this.UnitPrice.Name = "UnitPrice";
            // 
            // Total
            // 
            this.Total.HeaderText = "Total";
            this.Total.Name = "Total";
            // 
            // Delete
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = "Delete";
            this.Delete.DefaultCellStyle = dataGridViewCellStyle1;
            this.Delete.HeaderText = "";
            this.Delete.Name = "Delete";
            this.Delete.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Delete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Delete.Text = "Delete";
            this.Delete.ToolTipText = "Delete";
            // 
            // btAdd
            // 
            this.btAdd.BackColor = System.Drawing.Color.Transparent;
            this.btAdd.BackgroundImage = global::Tiles_Inventory.Properties.Resources.Add;
            this.btAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btAdd.Location = new System.Drawing.Point(326, 125);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(31, 22);
            this.btAdd.TabIndex = 6;
            this.btAdd.UseVisualStyleBackColor = false;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.BackColor = System.Drawing.Color.Transparent;
            this.Label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(21, 72);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(85, 14);
            this.Label4.TabIndex = 86;
            this.Label4.Text = "Supplier Name";
            // 
            // cmbProductName
            // 
            this.cmbProductName.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
            this.cmbProductName.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
            this.cmbProductName.CheckedListSettings.CheckStateMember = "";
            Appearance13.BackColor = System.Drawing.SystemColors.Window;
            Appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.cmbProductName.DisplayLayout.Appearance = Appearance13;
            this.cmbProductName.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn;
            this.cmbProductName.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.cmbProductName.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            Appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
            Appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
            Appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            Appearance14.BorderColor = System.Drawing.SystemColors.Window;
            this.cmbProductName.DisplayLayout.GroupByBox.Appearance = Appearance14;
            Appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
            this.cmbProductName.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance15;
            this.cmbProductName.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            Appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
            Appearance16.BackColor2 = System.Drawing.SystemColors.Control;
            Appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            Appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
            this.cmbProductName.DisplayLayout.GroupByBox.PromptAppearance = Appearance16;
            this.cmbProductName.DisplayLayout.MaxColScrollRegions = 1;
            this.cmbProductName.DisplayLayout.MaxRowScrollRegions = 1;
            Appearance17.BackColor = System.Drawing.SystemColors.Window;
            Appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmbProductName.DisplayLayout.Override.ActiveCellAppearance = Appearance17;
            Appearance18.BackColor = System.Drawing.SystemColors.Highlight;
            Appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.cmbProductName.DisplayLayout.Override.ActiveRowAppearance = Appearance18;
            this.cmbProductName.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.cmbProductName.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            Appearance19.BackColor = System.Drawing.SystemColors.Window;
            this.cmbProductName.DisplayLayout.Override.CardAreaAppearance = Appearance19;
            Appearance20.BorderColor = System.Drawing.Color.Silver;
            Appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.cmbProductName.DisplayLayout.Override.CellAppearance = Appearance20;
            this.cmbProductName.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.cmbProductName.DisplayLayout.Override.CellPadding = 0;
            Appearance21.BackColor = System.Drawing.SystemColors.Control;
            Appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
            Appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            Appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            Appearance21.BorderColor = System.Drawing.SystemColors.Window;
            this.cmbProductName.DisplayLayout.Override.GroupByRowAppearance = Appearance21;
            Appearance22.TextHAlignAsString = "Left";
            this.cmbProductName.DisplayLayout.Override.HeaderAppearance = Appearance22;
            this.cmbProductName.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.cmbProductName.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            Appearance23.BackColor = System.Drawing.SystemColors.Window;
            Appearance23.BorderColor = System.Drawing.Color.Silver;
            this.cmbProductName.DisplayLayout.Override.RowAppearance = Appearance23;
            this.cmbProductName.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            Appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cmbProductName.DisplayLayout.Override.TemplateAddRowAppearance = Appearance24;
            this.cmbProductName.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.cmbProductName.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.cmbProductName.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.cmbProductName.Location = new System.Drawing.Point(109, 95);
            this.cmbProductName.Name = "cmbProductName";
            this.cmbProductName.PreferredDropDownSize = new System.Drawing.Size(0, 0);
            this.cmbProductName.Size = new System.Drawing.Size(215, 24);
            this.cmbProductName.TabIndex = 3;
            this.cmbProductName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbProductName_KeyUp);
            // 
            // CmbSupplierName
            // 
            this.CmbSupplierName.CheckedListSettings.CheckStateMember = "";
            Appearance49.BackColor = System.Drawing.SystemColors.Window;
            Appearance49.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.CmbSupplierName.DisplayLayout.Appearance = Appearance49;
            this.CmbSupplierName.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.CmbSupplierName.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            Appearance50.BackColor = System.Drawing.SystemColors.ActiveBorder;
            Appearance50.BackColor2 = System.Drawing.SystemColors.ControlDark;
            Appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            Appearance50.BorderColor = System.Drawing.SystemColors.Window;
            this.CmbSupplierName.DisplayLayout.GroupByBox.Appearance = Appearance50;
            Appearance51.ForeColor = System.Drawing.SystemColors.GrayText;
            this.CmbSupplierName.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance51;
            this.CmbSupplierName.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            Appearance52.BackColor = System.Drawing.SystemColors.ControlLightLight;
            Appearance52.BackColor2 = System.Drawing.SystemColors.Control;
            Appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            Appearance52.ForeColor = System.Drawing.SystemColors.GrayText;
            this.CmbSupplierName.DisplayLayout.GroupByBox.PromptAppearance = Appearance52;
            this.CmbSupplierName.DisplayLayout.MaxColScrollRegions = 1;
            this.CmbSupplierName.DisplayLayout.MaxRowScrollRegions = 1;
            Appearance53.BackColor = System.Drawing.SystemColors.Window;
            Appearance53.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CmbSupplierName.DisplayLayout.Override.ActiveCellAppearance = Appearance53;
            Appearance54.BackColor = System.Drawing.SystemColors.Highlight;
            Appearance54.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.CmbSupplierName.DisplayLayout.Override.ActiveRowAppearance = Appearance54;
            this.CmbSupplierName.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.CmbSupplierName.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            Appearance55.BackColor = System.Drawing.SystemColors.Window;
            this.CmbSupplierName.DisplayLayout.Override.CardAreaAppearance = Appearance55;
            Appearance56.BorderColor = System.Drawing.Color.Silver;
            Appearance56.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.CmbSupplierName.DisplayLayout.Override.CellAppearance = Appearance56;
            this.CmbSupplierName.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.CmbSupplierName.DisplayLayout.Override.CellPadding = 0;
            Appearance57.BackColor = System.Drawing.SystemColors.Control;
            Appearance57.BackColor2 = System.Drawing.SystemColors.ControlDark;
            Appearance57.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            Appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            Appearance57.BorderColor = System.Drawing.SystemColors.Window;
            this.CmbSupplierName.DisplayLayout.Override.GroupByRowAppearance = Appearance57;
            Appearance58.TextHAlignAsString = "Left";
            this.CmbSupplierName.DisplayLayout.Override.HeaderAppearance = Appearance58;
            this.CmbSupplierName.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.CmbSupplierName.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            Appearance59.BackColor = System.Drawing.SystemColors.Window;
            Appearance59.BorderColor = System.Drawing.Color.Silver;
            this.CmbSupplierName.DisplayLayout.Override.RowAppearance = Appearance59;
            this.CmbSupplierName.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            Appearance60.BackColor = System.Drawing.SystemColors.ControlLight;
            this.CmbSupplierName.DisplayLayout.Override.TemplateAddRowAppearance = Appearance60;
            this.CmbSupplierName.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.CmbSupplierName.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.CmbSupplierName.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.CmbSupplierName.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
            this.CmbSupplierName.Location = new System.Drawing.Point(109, 67);
            this.CmbSupplierName.Name = "CmbSupplierName";
            this.CmbSupplierName.PreferredDropDownSize = new System.Drawing.Size(0, 0);
            this.CmbSupplierName.Size = new System.Drawing.Size(215, 24);
            this.CmbSupplierName.TabIndex = 2;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(21, 100);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(85, 14);
            this.Label2.TabIndex = 87;
            this.Label2.Text = "Product Name";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(20, 130);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(86, 14);
            this.Label3.TabIndex = 87;
            this.Label3.Text = "Purchase Price";
            // 
            // btRefresh
            // 
            this.btRefresh.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btRefresh.Location = new System.Drawing.Point(419, 18);
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(75, 23);
            this.btRefresh.TabIndex = 1;
            this.btRefresh.Text = "Refresh";
            this.btRefresh.UseVisualStyleBackColor = true;
            // 
            // UltraGroupBox1
            // 
            this.UltraGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance2.BackColor = System.Drawing.Color.LightBlue;
            this.UltraGroupBox1.Appearance = appearance2;
            this.UltraGroupBox1.Controls.Add(this.dtpReturnDate);
            this.UltraGroupBox1.Controls.Add(this.txtQuantity);
            this.UltraGroupBox1.Controls.Add(this.btnSelectProduct);
            this.UltraGroupBox1.Controls.Add(this.txtPurchasePrice);
            this.UltraGroupBox1.Controls.Add(this.txtProductSize);
            this.UltraGroupBox1.Controls.Add(this.txtPackSize);
            this.UltraGroupBox1.Controls.Add(this.label11);
            this.UltraGroupBox1.Controls.Add(this.txtInvoiceNumber);
            this.UltraGroupBox1.Controls.Add(this.label9);
            this.UltraGroupBox1.Controls.Add(this.Label6);
            this.UltraGroupBox1.Controls.Add(this.Label8);
            this.UltraGroupBox1.Controls.Add(this.Label1);
            this.UltraGroupBox1.Controls.Add(this.txtReceiveAmount);
            this.UltraGroupBox1.Controls.Add(this.Label7);
            this.UltraGroupBox1.Controls.Add(this.txtTotalAmount);
            this.UltraGroupBox1.Controls.Add(this.grvPurchaseDetails);
            this.UltraGroupBox1.Controls.Add(this.btAdd);
            this.UltraGroupBox1.Controls.Add(this.Label4);
            this.UltraGroupBox1.Controls.Add(this.cmbProductName);
            this.UltraGroupBox1.Controls.Add(this.CmbSupplierName);
            this.UltraGroupBox1.Controls.Add(this.Label2);
            this.UltraGroupBox1.Controls.Add(this.Label3);
            this.UltraGroupBox1.Controls.Add(this.Label29);
            this.UltraGroupBox1.Location = new System.Drawing.Point(3, 2);
            this.UltraGroupBox1.Name = "UltraGroupBox1";
            this.UltraGroupBox1.Size = new System.Drawing.Size(993, 456);
            this.UltraGroupBox1.TabIndex = 0;
            // 
            // dtpReturnDate
            // 
            this.dtpReturnDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpReturnDate.Location = new System.Drawing.Point(109, 11);
            this.dtpReturnDate.Name = "dtpReturnDate";
            this.dtpReturnDate.Size = new System.Drawing.Size(106, 22);
            this.dtpReturnDate.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(32, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 14);
            this.label9.TabIndex = 86;
            this.label9.Text = "Return Date";
            // 
            // Label29
            // 
            this.Label29.AutoSize = true;
            this.Label29.BackColor = System.Drawing.Color.Transparent;
            this.Label29.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label29.Location = new System.Drawing.Point(195, 129);
            this.Label29.Name = "Label29";
            this.Label29.Size = new System.Drawing.Size(54, 14);
            this.Label29.TabIndex = 87;
            this.Label29.Text = "Quantity";
            // 
            // btEdit
            // 
            this.btEdit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btEdit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btEdit.Location = new System.Drawing.Point(578, 18);
            this.btEdit.Name = "btEdit";
            this.btEdit.Size = new System.Drawing.Size(75, 23);
            this.btEdit.TabIndex = 3;
            this.btEdit.Text = "Modify ";
            this.btEdit.UseVisualStyleBackColor = false;
            this.btEdit.Visible = false;
            // 
            // btSave
            // 
            this.btSave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btSave.Location = new System.Drawing.Point(340, 18);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(75, 23);
            this.btSave.TabIndex = 0;
            this.btSave.Text = "Save";
            this.btSave.UseVisualStyleBackColor = false;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btClose
            // 
            this.btClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btClose.Location = new System.Drawing.Point(497, 18);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(75, 23);
            this.btClose.TabIndex = 2;
            this.btClose.Text = "Close";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // UltraGroupBox2
            // 
            this.UltraGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance1.BackColor = System.Drawing.Color.LightBlue;
            this.UltraGroupBox2.Appearance = appearance1;
            this.UltraGroupBox2.Controls.Add(this.btRefresh);
            this.UltraGroupBox2.Controls.Add(this.btEdit);
            this.UltraGroupBox2.Controls.Add(this.btSave);
            this.UltraGroupBox2.Controls.Add(this.btClose);
            this.UltraGroupBox2.Location = new System.Drawing.Point(3, 461);
            this.UltraGroupBox2.Name = "UltraGroupBox2";
            this.UltraGroupBox2.Size = new System.Drawing.Size(993, 59);
            this.UltraGroupBox2.TabIndex = 1;
            // 
            // frmPurchaseReturn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 523);
            this.Controls.Add(this.UltraGroupBox1);
            this.Controls.Add(this.UltraGroupBox2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmPurchaseReturn";
            this.Text = "Purchase Return";
            this.Load += new System.EventHandler(this.frmPurchaseReturn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvPurchaseDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProductName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbSupplierName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox1)).EndInit();
            this.UltraGroupBox1.ResumeLayout(false);
            this.UltraGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox2)).EndInit();
            this.UltraGroupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal Infragistics.Win.UltraWinEditors.UltraNumericEditor txtQuantity;
        private System.Windows.Forms.Button btnSelectProduct;
        internal System.Windows.Forms.TextBox txtPurchasePrice;
        internal System.Windows.Forms.TextBox txtProductSize;
        internal System.Windows.Forms.TextBox txtPackSize;
        internal System.Windows.Forms.Label label11;
        internal System.Windows.Forms.TextBox txtInvoiceNumber;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.TextBox txtReceiveAmount;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Timer timer1;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.TextBox txtTotalAmount;
        internal System.Windows.Forms.DataGridView grvPurchaseDetails;
        internal System.Windows.Forms.Button btAdd;
        internal System.Windows.Forms.Label Label4;
        internal Infragistics.Win.UltraWinGrid.UltraCombo cmbProductName;
        internal Infragistics.Win.UltraWinGrid.UltraCombo CmbSupplierName;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Button btRefresh;
        internal Infragistics.Win.Misc.UltraGroupBox UltraGroupBox1;
        internal System.Windows.Forms.Label Label29;
        internal System.Windows.Forms.Button btEdit;
        internal System.Windows.Forms.Button btSave;
        internal System.Windows.Forms.Button btClose;
        internal Infragistics.Win.Misc.UltraGroupBox UltraGroupBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.DataGridViewButtonColumn Delete;
        private System.Windows.Forms.DateTimePicker dtpReturnDate;
        internal System.Windows.Forms.Label label9;
    }
}