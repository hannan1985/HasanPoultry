namespace Tiles_Inventory
{
    partial class frmViewSalesReturnInformation
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
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            this.UltraGroupBox3 = new Infragistics.Win.Misc.UltraGroupBox();
            this.btRefresh = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btPrint = new System.Windows.Forms.Button();
            this.btEdit = new System.Windows.Forms.Button();
            this.btDistribute = new System.Windows.Forms.Button();
            this.ultraGroupBox4 = new Infragistics.Win.Misc.UltraGroupBox();
            this.grvTransferDetailInfo = new System.Windows.Forms.DataGridView();
            this.UltraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            this.grvTransferInfo = new System.Windows.Forms.DataGridView();
            this.UltraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.Label3 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.Label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox3)).BeginInit();
            this.UltraGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox4)).BeginInit();
            this.ultraGroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvTransferDetailInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox2)).BeginInit();
            this.UltraGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvTransferInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox1)).BeginInit();
            this.UltraGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // UltraGroupBox3
            // 
            this.UltraGroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            appearance1.BackColor = System.Drawing.Color.LightBlue;
            this.UltraGroupBox3.Appearance = appearance1;
            this.UltraGroupBox3.Controls.Add(this.btRefresh);
            this.UltraGroupBox3.Controls.Add(this.btnClose);
            this.UltraGroupBox3.Controls.Add(this.btPrint);
            this.UltraGroupBox3.Controls.Add(this.btEdit);
            this.UltraGroupBox3.Controls.Add(this.btDistribute);
            this.UltraGroupBox3.Location = new System.Drawing.Point(4, 256);
            this.UltraGroupBox3.Name = "UltraGroupBox3";
            this.UltraGroupBox3.Size = new System.Drawing.Size(1005, 51);
            this.UltraGroupBox3.TabIndex = 10;
            // 
            // btRefresh
            // 
            this.btRefresh.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btRefresh.Location = new System.Drawing.Point(464, 14);
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
            this.btnClose.Location = new System.Drawing.Point(626, 14);
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
            this.btPrint.Location = new System.Drawing.Point(545, 14);
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
            this.btEdit.Location = new System.Drawing.Point(382, 14);
            this.btEdit.Name = "btEdit";
            this.btEdit.Size = new System.Drawing.Size(75, 23);
            this.btEdit.TabIndex = 0;
            this.btEdit.Text = "Edit";
            this.btEdit.UseVisualStyleBackColor = true;
            this.btEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btDistribute
            // 
            this.btDistribute.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btDistribute.Location = new System.Drawing.Point(304, 14);
            this.btDistribute.Name = "btDistribute";
            this.btDistribute.Size = new System.Drawing.Size(75, 23);
            this.btDistribute.TabIndex = 0;
            this.btDistribute.Text = "&Add New";
            this.btDistribute.UseVisualStyleBackColor = true;
            this.btDistribute.Click += new System.EventHandler(this.btnDistribute_Click);
            // 
            // ultraGroupBox4
            // 
            this.ultraGroupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            appearance2.BackColor = System.Drawing.Color.LightBlue;
            this.ultraGroupBox4.Appearance = appearance2;
            this.ultraGroupBox4.Controls.Add(this.grvTransferDetailInfo);
            this.ultraGroupBox4.Location = new System.Drawing.Point(4, 309);
            this.ultraGroupBox4.Name = "ultraGroupBox4";
            this.ultraGroupBox4.Size = new System.Drawing.Size(1005, 176);
            this.ultraGroupBox4.TabIndex = 11;
            // 
            // grvTransferDetailInfo
            // 
            this.grvTransferDetailInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grvTransferDetailInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grvTransferDetailInfo.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.grvTransferDetailInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvTransferDetailInfo.Location = new System.Drawing.Point(7, 5);
            this.grvTransferDetailInfo.Name = "grvTransferDetailInfo";
            this.grvTransferDetailInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvTransferDetailInfo.Size = new System.Drawing.Size(991, 165);
            this.grvTransferDetailInfo.TabIndex = 2;
            // 
            // UltraGroupBox2
            // 
            this.UltraGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            appearance4.BackColor = System.Drawing.Color.LightBlue;
            this.UltraGroupBox2.Appearance = appearance4;
            this.UltraGroupBox2.Controls.Add(this.grvTransferInfo);
            this.UltraGroupBox2.Location = new System.Drawing.Point(4, 66);
            this.UltraGroupBox2.Name = "UltraGroupBox2";
            this.UltraGroupBox2.Size = new System.Drawing.Size(1005, 188);
            this.UltraGroupBox2.TabIndex = 11;
            // 
            // grvTransferInfo
            // 
            this.grvTransferInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grvTransferInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grvTransferInfo.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.grvTransferInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvTransferInfo.Location = new System.Drawing.Point(7, 5);
            this.grvTransferInfo.Name = "grvTransferInfo";
            this.grvTransferInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvTransferInfo.Size = new System.Drawing.Size(991, 177);
            this.grvTransferInfo.TabIndex = 2;
            this.grvTransferInfo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grvTransferInfo_CellContentClick);
            this.grvTransferInfo.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grvTransferInfo_RowHeaderMouseClick);
            // 
            // UltraGroupBox1
            // 
            this.UltraGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            appearance3.BackColor = System.Drawing.Color.LightBlue;
            this.UltraGroupBox1.Appearance = appearance3;
            this.UltraGroupBox1.Controls.Add(this.btnLoad);
            this.UltraGroupBox1.Controls.Add(this.dtpToDate);
            this.UltraGroupBox1.Controls.Add(this.Label3);
            this.UltraGroupBox1.Controls.Add(this.dtpFromDate);
            this.UltraGroupBox1.Controls.Add(this.Label2);
            this.UltraGroupBox1.Location = new System.Drawing.Point(4, 3);
            this.UltraGroupBox1.Name = "UltraGroupBox1";
            this.UltraGroupBox1.Size = new System.Drawing.Size(1005, 61);
            this.UltraGroupBox1.TabIndex = 12;
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnLoad.Location = new System.Drawing.Point(604, 24);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 3;
            this.btnLoad.Text = "Load Data";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // dtpToDate
            // 
            this.dtpToDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(498, 24);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(103, 22);
            this.dtpToDate.TabIndex = 2;
            // 
            // Label3
            // 
            this.Label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Label3.AutoSize = true;
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.Location = new System.Drawing.Point(472, 28);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(22, 14);
            this.Label3.TabIndex = 1;
            this.Label3.Text = "To";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(364, 24);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(103, 22);
            this.dtpFromDate.TabIndex = 2;
            // 
            // Label2
            // 
            this.Label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Location = new System.Drawing.Point(326, 28);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(34, 14);
            this.Label2.TabIndex = 1;
            this.Label2.Text = "From";
            // 
            // frmViewSalesReturnInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 488);
            this.Controls.Add(this.UltraGroupBox3);
            this.Controls.Add(this.ultraGroupBox4);
            this.Controls.Add(this.UltraGroupBox2);
            this.Controls.Add(this.UltraGroupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmViewSalesReturnInformation";
            this.Text = "View Sales Return Information";
            this.Load += new System.EventHandler(this.frmViewTransferInformation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox3)).EndInit();
            this.UltraGroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox4)).EndInit();
            this.ultraGroupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvTransferDetailInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox2)).EndInit();
            this.UltraGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvTransferInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox1)).EndInit();
            this.UltraGroupBox1.ResumeLayout(false);
            this.UltraGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Infragistics.Win.Misc.UltraGroupBox UltraGroupBox3;
        internal System.Windows.Forms.Button btRefresh;
        internal System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.Button btEdit;
        internal System.Windows.Forms.Button btDistribute;
        internal Infragistics.Win.Misc.UltraGroupBox UltraGroupBox2;
        internal System.Windows.Forms.DataGridView grvTransferInfo;
        internal Infragistics.Win.Misc.UltraGroupBox UltraGroupBox1;
        internal System.Windows.Forms.Button btnLoad;
        internal System.Windows.Forms.DateTimePicker dtpToDate;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.DateTimePicker dtpFromDate;
        internal System.Windows.Forms.Label Label2;
        internal Infragistics.Win.Misc.UltraGroupBox ultraGroupBox4;
        internal System.Windows.Forms.DataGridView grvTransferDetailInfo;
        internal System.Windows.Forms.Button btPrint;
    }
}