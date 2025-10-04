namespace Tiles_Inventory
{
    partial class frmSearchSalesInformation
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
            this.cmbSalesId = new System.Windows.Forms.ComboBox();
            this.grvSearchSalesInformation = new System.Windows.Forms.DataGridView();
            this.Label1 = new System.Windows.Forms.Label();
            this.btnShowInvoice = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.UltraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            this.UltraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.grvSearchSalesInformation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox2)).BeginInit();
            this.UltraGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox1)).BeginInit();
            this.UltraGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbSalesId
            // 
            this.cmbSalesId.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbSalesId.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSalesId.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmbSalesId.FormattingEnabled = true;
            this.cmbSalesId.Location = new System.Drawing.Point(72, 18);
            this.cmbSalesId.Name = "cmbSalesId";
            this.cmbSalesId.Size = new System.Drawing.Size(200, 22);
            this.cmbSalesId.TabIndex = 76;
            this.cmbSalesId.SelectedIndexChanged += new System.EventHandler(this.cmbSalesId_SelectedIndexChanged);
            // 
            // grvSearchSalesInformation
            // 
            this.grvSearchSalesInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grvSearchSalesInformation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grvSearchSalesInformation.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.grvSearchSalesInformation.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.grvSearchSalesInformation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvSearchSalesInformation.Location = new System.Drawing.Point(8, 47);
            this.grvSearchSalesInformation.Name = "grvSearchSalesInformation";
            this.grvSearchSalesInformation.ReadOnly = true;
            this.grvSearchSalesInformation.Size = new System.Drawing.Size(707, 172);
            this.grvSearchSalesInformation.TabIndex = 74;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(11, 22);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(57, 14);
            this.Label1.TabIndex = 75;
            this.Label1.Text = "Order No";
            // 
            // btnShowInvoice
            // 
            this.btnShowInvoice.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnShowInvoice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnShowInvoice.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnShowInvoice.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowInvoice.Location = new System.Drawing.Point(283, 15);
            this.btnShowInvoice.Name = "btnShowInvoice";
            this.btnShowInvoice.Size = new System.Drawing.Size(75, 23);
            this.btnShowInvoice.TabIndex = 77;
            this.btnShowInvoice.Text = "Print";
            this.btnShowInvoice.UseVisualStyleBackColor = false;
            this.btnShowInvoice.Click += new System.EventHandler(this.btnShowInvoice_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(363, 15);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 78;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // UltraGroupBox2
            // 
            this.UltraGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            appearance1.BackColor = System.Drawing.Color.LightBlue;
            this.UltraGroupBox2.Appearance = appearance1;
            this.UltraGroupBox2.Controls.Add(this.btnShowInvoice);
            this.UltraGroupBox2.Controls.Add(this.btnClose);
            this.UltraGroupBox2.Location = new System.Drawing.Point(0, 234);
            this.UltraGroupBox2.Name = "UltraGroupBox2";
            this.UltraGroupBox2.Size = new System.Drawing.Size(721, 53);
            this.UltraGroupBox2.TabIndex = 82;
            // 
            // UltraGroupBox1
            // 
            this.UltraGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            appearance2.BackColor = System.Drawing.Color.LightBlue;
            this.UltraGroupBox1.Appearance = appearance2;
            this.UltraGroupBox1.Controls.Add(this.cmbSalesId);
            this.UltraGroupBox1.Controls.Add(this.grvSearchSalesInformation);
            this.UltraGroupBox1.Controls.Add(this.Label1);
            this.UltraGroupBox1.Location = new System.Drawing.Point(0, 3);
            this.UltraGroupBox1.Name = "UltraGroupBox1";
            this.UltraGroupBox1.Size = new System.Drawing.Size(721, 229);
            this.UltraGroupBox1.TabIndex = 81;
            // 
            // frmSearchSalesInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 291);
            this.Controls.Add(this.UltraGroupBox2);
            this.Controls.Add(this.UltraGroupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSearchSalesInformation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sales Information";
            this.Load += new System.EventHandler(this.frmSearchSalesInformation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grvSearchSalesInformation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox2)).EndInit();
            this.UltraGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox1)).EndInit();
            this.UltraGroupBox1.ResumeLayout(false);
            this.UltraGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ComboBox cmbSalesId;
        internal System.Windows.Forms.DataGridView grvSearchSalesInformation;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button btnShowInvoice;
        internal System.Windows.Forms.Button btnClose;
        internal Infragistics.Win.Misc.UltraGroupBox UltraGroupBox2;
        internal Infragistics.Win.Misc.UltraGroupBox UltraGroupBox1;
    }
}