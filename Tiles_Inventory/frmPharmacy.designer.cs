namespace Tiles_Inventory
{
    partial class frmPharmacy
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
            this.btnBrowse = new Infragistics.Win.Misc.UltraButton();
            this.pbCompanyLogo = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbActive = new System.Windows.Forms.CheckBox();
            this.txtPharmacyName = new System.Windows.Forms.TextBox();
            this.txtContactPerson = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtRegistratinNumber = new System.Windows.Forms.TextBox();
            this.txtFax = new System.Windows.Forms.TextBox();
            this.grvPharmacyInformation = new System.Windows.Forms.DataGridView();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.Label33 = new System.Windows.Forms.Label();
            this.Label34 = new System.Windows.Forms.Label();
            this.txtPharmacyAddress = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblContactPerson = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox2)).BeginInit();
            this.UltraGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox1)).BeginInit();
            this.UltraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCompanyLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvPharmacyInformation)).BeginInit();
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
            this.UltraGroupBox1.Controls.Add(this.btnBrowse);
            this.UltraGroupBox1.Controls.Add(this.pbCompanyLogo);
            this.UltraGroupBox1.Controls.Add(this.label4);
            this.UltraGroupBox1.Controls.Add(this.cbActive);
            this.UltraGroupBox1.Controls.Add(this.txtPharmacyName);
            this.UltraGroupBox1.Controls.Add(this.txtContactPerson);
            this.UltraGroupBox1.Controls.Add(this.txtPhone);
            this.UltraGroupBox1.Controls.Add(this.txtRegistratinNumber);
            this.UltraGroupBox1.Controls.Add(this.txtFax);
            this.UltraGroupBox1.Controls.Add(this.grvPharmacyInformation);
            this.UltraGroupBox1.Controls.Add(this.txtEmail);
            this.UltraGroupBox1.Controls.Add(this.Label33);
            this.UltraGroupBox1.Controls.Add(this.Label34);
            this.UltraGroupBox1.Controls.Add(this.txtPharmacyAddress);
            this.UltraGroupBox1.Controls.Add(this.Label3);
            this.UltraGroupBox1.Controls.Add(this.label5);
            this.UltraGroupBox1.Controls.Add(this.lblContactPerson);
            this.UltraGroupBox1.Controls.Add(this.Label2);
            this.UltraGroupBox1.Controls.Add(this.Label1);
            this.UltraGroupBox1.Location = new System.Drawing.Point(3, 2);
            this.UltraGroupBox1.Name = "UltraGroupBox1";
            this.UltraGroupBox1.Size = new System.Drawing.Size(711, 401);
            this.UltraGroupBox1.TabIndex = 0;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(523, 102);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(70, 22);
            this.btnBrowse.TabIndex = 8;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // pbCompanyLogo
            // 
            this.pbCompanyLogo.BackColor = System.Drawing.Color.Transparent;
            this.pbCompanyLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbCompanyLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbCompanyLogo.Location = new System.Drawing.Point(523, 15);
            this.pbCompanyLogo.Name = "pbCompanyLogo";
            this.pbCompanyLogo.Size = new System.Drawing.Size(150, 83);
            this.pbCompanyLogo.TabIndex = 191;
            this.pbCompanyLogo.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(433, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 14);
            this.label4.TabIndex = 190;
            this.label4.Text = "Company Logo";
            // 
            // cbActive
            // 
            this.cbActive.AutoSize = true;
            this.cbActive.BackColor = System.Drawing.Color.Transparent;
            this.cbActive.Location = new System.Drawing.Point(144, 269);
            this.cbActive.Name = "cbActive";
            this.cbActive.Size = new System.Drawing.Size(60, 18);
            this.cbActive.TabIndex = 7;
            this.cbActive.Text = "Active";
            this.cbActive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbActive.UseVisualStyleBackColor = false;
            // 
            // txtPharmacyName
            // 
            this.txtPharmacyName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPharmacyName.Location = new System.Drawing.Point(144, 11);
            this.txtPharmacyName.Name = "txtPharmacyName";
            this.txtPharmacyName.Size = new System.Drawing.Size(207, 22);
            this.txtPharmacyName.TabIndex = 0;
            // 
            // txtContactPerson
            // 
            this.txtContactPerson.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContactPerson.Location = new System.Drawing.Point(144, 43);
            this.txtContactPerson.Name = "txtContactPerson";
            this.txtContactPerson.Size = new System.Drawing.Size(207, 22);
            this.txtContactPerson.TabIndex = 1;
            // 
            // txtPhone
            // 
            this.txtPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhone.Location = new System.Drawing.Point(144, 75);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(207, 22);
            this.txtPhone.TabIndex = 2;
            // 
            // txtRegistratinNumber
            // 
            this.txtRegistratinNumber.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRegistratinNumber.Location = new System.Drawing.Point(144, 236);
            this.txtRegistratinNumber.Name = "txtRegistratinNumber";
            this.txtRegistratinNumber.Size = new System.Drawing.Size(207, 22);
            this.txtRegistratinNumber.TabIndex = 6;
            // 
            // txtFax
            // 
            this.txtFax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFax.Location = new System.Drawing.Point(144, 107);
            this.txtFax.Name = "txtFax";
            this.txtFax.Size = new System.Drawing.Size(207, 22);
            this.txtFax.TabIndex = 3;
            // 
            // grvPharmacyInformation
            // 
            this.grvPharmacyInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grvPharmacyInformation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grvPharmacyInformation.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.grvPharmacyInformation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvPharmacyInformation.Location = new System.Drawing.Point(21, 311);
            this.grvPharmacyInformation.Name = "grvPharmacyInformation";
            this.grvPharmacyInformation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvPharmacyInformation.Size = new System.Drawing.Size(668, 80);
            this.grvPharmacyInformation.TabIndex = 9;
            this.grvPharmacyInformation.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grvPharmacyInformation_CellClick);
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(144, 139);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(207, 22);
            this.txtEmail.TabIndex = 4;
            // 
            // Label33
            // 
            this.Label33.AutoSize = true;
            this.Label33.BackColor = System.Drawing.Color.Transparent;
            this.Label33.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label33.Location = new System.Drawing.Point(21, 175);
            this.Label33.Name = "Label33";
            this.Label33.Size = new System.Drawing.Size(121, 14);
            this.Label33.TabIndex = 186;
            this.Label33.Text = "Organization Address";
            // 
            // Label34
            // 
            this.Label34.AutoSize = true;
            this.Label34.BackColor = System.Drawing.Color.Transparent;
            this.Label34.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label34.Location = new System.Drawing.Point(33, 14);
            this.Label34.Name = "Label34";
            this.Label34.Size = new System.Drawing.Size(109, 14);
            this.Label34.TabIndex = 185;
            this.Label34.Text = "Organization Name";
            // 
            // txtPharmacyAddress
            // 
            this.txtPharmacyAddress.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPharmacyAddress.Location = new System.Drawing.Point(144, 171);
            this.txtPharmacyAddress.Multiline = true;
            this.txtPharmacyAddress.Name = "txtPharmacyAddress";
            this.txtPharmacyAddress.Size = new System.Drawing.Size(275, 59);
            this.txtPharmacyAddress.TabIndex = 5;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(107, 142);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(34, 14);
            this.Label3.TabIndex = 184;
            this.Label3.Text = "Email";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(23, 239);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 14);
            this.label5.TabIndex = 184;
            this.label5.Text = "Registration Number";
            // 
            // lblContactPerson
            // 
            this.lblContactPerson.AutoSize = true;
            this.lblContactPerson.BackColor = System.Drawing.Color.Transparent;
            this.lblContactPerson.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContactPerson.Location = new System.Drawing.Point(51, 46);
            this.lblContactPerson.Name = "lblContactPerson";
            this.lblContactPerson.Size = new System.Drawing.Size(91, 14);
            this.lblContactPerson.TabIndex = 184;
            this.lblContactPerson.Text = "Contact Person";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(116, 110);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(25, 14);
            this.Label2.TabIndex = 184;
            this.Label2.Text = "Fax";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(44, 78);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(97, 14);
            this.Label1.TabIndex = 184;
            this.Label1.Text = "Contact Number";
            // 
            // frmPharmacy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 471);
            this.Controls.Add(this.UltraGroupBox2);
            this.Controls.Add(this.UltraGroupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmPharmacy";
            this.Text = "Organization Information";
            this.Load += new System.EventHandler(this.frmPharmacy_Load);
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox2)).EndInit();
            this.UltraGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox1)).EndInit();
            this.UltraGroupBox1.ResumeLayout(false);
            this.UltraGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCompanyLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvPharmacyInformation)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal Infragistics.Win.Misc.UltraGroupBox UltraGroupBox2;
        internal System.Windows.Forms.Button btnSupplierClose;
        internal System.Windows.Forms.Button btnSupplierSave;
        internal System.Windows.Forms.Button btnSupplierUpdate;
        internal Infragistics.Win.Misc.UltraGroupBox UltraGroupBox1;
        internal System.Windows.Forms.CheckBox cbActive;
        internal System.Windows.Forms.TextBox txtPharmacyName;
        internal System.Windows.Forms.TextBox txtContactPerson;
        internal System.Windows.Forms.TextBox txtPhone;
        internal System.Windows.Forms.TextBox txtFax;
        internal System.Windows.Forms.DataGridView grvPharmacyInformation;
        internal System.Windows.Forms.TextBox txtEmail;
        internal System.Windows.Forms.Label Label33;
        internal System.Windows.Forms.Label Label34;
        internal System.Windows.Forms.TextBox txtPharmacyAddress;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label lblContactPerson;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        private Infragistics.Win.Misc.UltraButton btnBrowse;
        private System.Windows.Forms.PictureBox pbCompanyLogo;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox txtRegistratinNumber;
        internal System.Windows.Forms.Label label5;
    }
}