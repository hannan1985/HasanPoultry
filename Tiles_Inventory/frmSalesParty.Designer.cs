namespace Tiles_Inventory
{
    partial class frmSalesParty
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
            this.UltraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            this.btnSupplierClose = new System.Windows.Forms.Button();
            this.btnSupplierSave = new System.Windows.Forms.Button();
            this.btnSupplierUpdate = new System.Windows.Forms.Button();
            this.UltraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.txtSalesPartyName = new System.Windows.Forms.TextBox();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.grvSalesPartyInformation = new System.Windows.Forms.DataGridView();
            this.Label33 = new System.Windows.Forms.Label();
            this.Label34 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox2)).BeginInit();
            this.UltraGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox1)).BeginInit();
            this.UltraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvSalesPartyInformation)).BeginInit();
            this.SuspendLayout();
            // 
            // UltraGroupBox2
            // 
            this.UltraGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            appearance1.BackColor = System.Drawing.Color.LightBlue;
            this.UltraGroupBox2.Appearance = appearance1;
            this.UltraGroupBox2.Controls.Add(this.btnSupplierClose);
            this.UltraGroupBox2.Controls.Add(this.btnSupplierSave);
            this.UltraGroupBox2.Controls.Add(this.btnSupplierUpdate);
            this.UltraGroupBox2.Location = new System.Drawing.Point(3, 405);
            this.UltraGroupBox2.Name = "UltraGroupBox2";
            this.UltraGroupBox2.Size = new System.Drawing.Size(711, 64);
            this.UltraGroupBox2.TabIndex = 1;
            // 
            // btnSupplierClose
            // 
            this.btnSupplierClose.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSupplierClose.Location = new System.Drawing.Point(399, 16);
            this.btnSupplierClose.Name = "btnSupplierClose";
            this.btnSupplierClose.Size = new System.Drawing.Size(75, 23);
            this.btnSupplierClose.TabIndex = 2;
            this.btnSupplierClose.Text = "Close";
            this.btnSupplierClose.UseVisualStyleBackColor = true;
            this.btnSupplierClose.Click += new System.EventHandler(this.btnSupplierClose_Click);
            // 
            // btnSupplierSave
            // 
            this.btnSupplierSave.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSupplierSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSupplierSave.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSupplierSave.Location = new System.Drawing.Point(237, 16);
            this.btnSupplierSave.Name = "btnSupplierSave";
            this.btnSupplierSave.Size = new System.Drawing.Size(75, 23);
            this.btnSupplierSave.TabIndex = 0;
            this.btnSupplierSave.Text = "Save";
            this.btnSupplierSave.UseVisualStyleBackColor = true;
            this.btnSupplierSave.Click += new System.EventHandler(this.btnSupplierSave_Click);
            // 
            // btnSupplierUpdate
            // 
            this.btnSupplierUpdate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSupplierUpdate.Enabled = false;
            this.btnSupplierUpdate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSupplierUpdate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSupplierUpdate.Location = new System.Drawing.Point(318, 16);
            this.btnSupplierUpdate.Name = "btnSupplierUpdate";
            this.btnSupplierUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnSupplierUpdate.TabIndex = 1;
            this.btnSupplierUpdate.Text = "Update";
            this.btnSupplierUpdate.UseVisualStyleBackColor = true;
            this.btnSupplierUpdate.Click += new System.EventHandler(this.btnSupplierUpdate_Click);
            // 
            // UltraGroupBox1
            // 
            this.UltraGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            appearance2.BackColor = System.Drawing.Color.LightBlue;
            this.UltraGroupBox1.Appearance = appearance2;
            this.UltraGroupBox1.Controls.Add(this.txtSalesPartyName);
            this.UltraGroupBox1.Controls.Add(this.txtPhoneNumber);
            this.UltraGroupBox1.Controls.Add(this.grvSalesPartyInformation);
            this.UltraGroupBox1.Controls.Add(this.Label33);
            this.UltraGroupBox1.Controls.Add(this.Label34);
            this.UltraGroupBox1.Controls.Add(this.txtAddress);
            this.UltraGroupBox1.Controls.Add(this.label5);
            this.UltraGroupBox1.Location = new System.Drawing.Point(3, 2);
            this.UltraGroupBox1.Name = "UltraGroupBox1";
            this.UltraGroupBox1.Size = new System.Drawing.Size(711, 401);
            this.UltraGroupBox1.TabIndex = 0;
            // 
            // txtSalesPartyName
            // 
            this.txtSalesPartyName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSalesPartyName.Location = new System.Drawing.Point(102, 18);
            this.txtSalesPartyName.Name = "txtSalesPartyName";
            this.txtSalesPartyName.Size = new System.Drawing.Size(207, 22);
            this.txtSalesPartyName.TabIndex = 0;
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhoneNumber.Location = new System.Drawing.Point(102, 111);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(207, 22);
            this.txtPhoneNumber.TabIndex = 2;
            // 
            // grvSalesPartyInformation
            // 
            this.grvSalesPartyInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grvSalesPartyInformation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grvSalesPartyInformation.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.grvSalesPartyInformation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvSalesPartyInformation.Location = new System.Drawing.Point(6, 184);
            this.grvSalesPartyInformation.Name = "grvSalesPartyInformation";
            this.grvSalesPartyInformation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvSalesPartyInformation.Size = new System.Drawing.Size(698, 207);
            this.grvSalesPartyInformation.TabIndex = 3;
            this.grvSalesPartyInformation.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grvSalesPartyInformation_CellClick);
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
            // frmSalesParty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 471);
            this.Controls.Add(this.UltraGroupBox2);
            this.Controls.Add(this.UltraGroupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmSalesParty";
            this.Text = "Party Information";
            this.Load += new System.EventHandler(this.frmSalesParty_Load);
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox2)).EndInit();
            this.UltraGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox1)).EndInit();
            this.UltraGroupBox1.ResumeLayout(false);
            this.UltraGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvSalesPartyInformation)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.TextBox txtSalesPartyName;
        internal System.Windows.Forms.TextBox txtPhoneNumber;
        internal System.Windows.Forms.DataGridView grvSalesPartyInformation;
        internal System.Windows.Forms.Label Label33;
        internal System.Windows.Forms.Label Label34;
        internal System.Windows.Forms.TextBox txtAddress;
        internal Infragistics.Win.Misc.UltraGroupBox UltraGroupBox2;
        internal System.Windows.Forms.Button btnSupplierClose;
        internal System.Windows.Forms.Button btnSupplierSave;
        internal System.Windows.Forms.Button btnSupplierUpdate;
        internal System.Windows.Forms.Label label5;
        internal Infragistics.Win.Misc.UltraGroupBox UltraGroupBox1;
    }
}