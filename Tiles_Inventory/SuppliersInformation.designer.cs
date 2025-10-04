namespace Tiles_Inventory
{
    partial class SuppliersInformation
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
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            this.btnSupplierClose = new System.Windows.Forms.Button();
            this.btnSupplierSave = new System.Windows.Forms.Button();
            this.txtSupplier = new System.Windows.Forms.TextBox();
            this.txtSupplierPhone = new System.Windows.Forms.TextBox();
            this.Label23 = new System.Windows.Forms.Label();
            this.Label21 = new System.Windows.Forms.Label();
            this.UltraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.cbDiscontinued = new System.Windows.Forms.CheckBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.UltraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox1)).BeginInit();
            this.UltraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox2)).BeginInit();
            this.UltraGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSupplierClose
            // 
            this.btnSupplierClose.Location = new System.Drawing.Point(219, 22);
            this.btnSupplierClose.Name = "btnSupplierClose";
            this.btnSupplierClose.Size = new System.Drawing.Size(75, 23);
            this.btnSupplierClose.TabIndex = 2;
            this.btnSupplierClose.Text = "Close";
            this.btnSupplierClose.UseVisualStyleBackColor = true;
            this.btnSupplierClose.Click += new System.EventHandler(this.btnSupplierClose_Click);
            // 
            // btnSupplierSave
            // 
            this.btnSupplierSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSupplierSave.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSupplierSave.Location = new System.Drawing.Point(63, 22);
            this.btnSupplierSave.Name = "btnSupplierSave";
            this.btnSupplierSave.Size = new System.Drawing.Size(75, 23);
            this.btnSupplierSave.TabIndex = 0;
            this.btnSupplierSave.Text = "Save";
            this.btnSupplierSave.UseVisualStyleBackColor = true;
            this.btnSupplierSave.Click += new System.EventHandler(this.btnSupplierSave_Click);
            // 
            // txtSupplier
            // 
            this.txtSupplier.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSupplier.Location = new System.Drawing.Point(111, 22);
            this.txtSupplier.Name = "txtSupplier";
            this.txtSupplier.Size = new System.Drawing.Size(207, 22);
            this.txtSupplier.TabIndex = 0;
            // 
            // txtSupplierPhone
            // 
            this.txtSupplierPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSupplierPhone.Location = new System.Drawing.Point(111, 128);
            this.txtSupplierPhone.Name = "txtSupplierPhone";
            this.txtSupplierPhone.Size = new System.Drawing.Size(207, 22);
            this.txtSupplierPhone.TabIndex = 3;
            // 
            // Label23
            // 
            this.Label23.AutoSize = true;
            this.Label23.BackColor = System.Drawing.Color.Transparent;
            this.Label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label23.Location = new System.Drawing.Point(23, 26);
            this.Label23.Name = "Label23";
            this.Label23.Size = new System.Drawing.Size(85, 14);
            this.Label23.TabIndex = 162;
            this.Label23.Text = "Supplier Name";
            // 
            // Label21
            // 
            this.Label21.AutoSize = true;
            this.Label21.BackColor = System.Drawing.Color.Transparent;
            this.Label21.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label21.Location = new System.Drawing.Point(19, 132);
            this.Label21.Name = "Label21";
            this.Label21.Size = new System.Drawing.Size(89, 14);
            this.Label21.TabIndex = 164;
            this.Label21.Text = "Phone Number";
            // 
            // UltraGroupBox1
            // 
            this.UltraGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance14.BackColor = System.Drawing.Color.LightBlue;
            this.UltraGroupBox1.Appearance = appearance14;
            this.UltraGroupBox1.Controls.Add(this.cbDiscontinued);
            this.UltraGroupBox1.Controls.Add(this.txtAddress);
            this.UltraGroupBox1.Controls.Add(this.txtSupplier);
            this.UltraGroupBox1.Controls.Add(this.label1);
            this.UltraGroupBox1.Controls.Add(this.txtEmail);
            this.UltraGroupBox1.Controls.Add(this.txtSupplierPhone);
            this.UltraGroupBox1.Controls.Add(this.label2);
            this.UltraGroupBox1.Controls.Add(this.Label23);
            this.UltraGroupBox1.Controls.Add(this.Label21);
            this.UltraGroupBox1.Location = new System.Drawing.Point(2, 1);
            this.UltraGroupBox1.Name = "UltraGroupBox1";
            this.UltraGroupBox1.Size = new System.Drawing.Size(356, 203);
            this.UltraGroupBox1.TabIndex = 0;
            // 
            // cbDiscontinued
            // 
            this.cbDiscontinued.AutoSize = true;
            this.cbDiscontinued.BackColor = System.Drawing.Color.Transparent;
            this.cbDiscontinued.Location = new System.Drawing.Point(111, 155);
            this.cbDiscontinued.Name = "cbDiscontinued";
            this.cbDiscontinued.Size = new System.Drawing.Size(96, 18);
            this.cbDiscontinued.TabIndex = 4;
            this.cbDiscontinued.Text = "Discontinued";
            this.cbDiscontinued.UseVisualStyleBackColor = false;
            // 
            // txtAddress
            // 
            this.txtAddress.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.Location = new System.Drawing.Point(111, 49);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(207, 47);
            this.txtAddress.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 14);
            this.label1.TabIndex = 162;
            this.label1.Text = "Supplier Address";
            // 
            // UltraGroupBox2
            // 
            this.UltraGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance13.BackColor = System.Drawing.Color.LightBlue;
            this.UltraGroupBox2.Appearance = appearance13;
            this.UltraGroupBox2.Controls.Add(this.btnReset);
            this.UltraGroupBox2.Controls.Add(this.btnSupplierClose);
            this.UltraGroupBox2.Controls.Add(this.btnSupplierSave);
            this.UltraGroupBox2.Location = new System.Drawing.Point(2, 205);
            this.UltraGroupBox2.Name = "UltraGroupBox2";
            this.UltraGroupBox2.Size = new System.Drawing.Size(356, 67);
            this.UltraGroupBox2.TabIndex = 1;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(141, 22);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 1;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(74, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 14);
            this.label2.TabIndex = 164;
            this.label2.Text = "Email";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(111, 101);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(207, 22);
            this.txtEmail.TabIndex = 2;
            // 
            // SuppliersInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 273);
            this.Controls.Add(this.UltraGroupBox1);
            this.Controls.Add(this.UltraGroupBox2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SuppliersInformation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Supplier  Information";
            this.Load += new System.EventHandler(this.SuppliersInformation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox1)).EndInit();
            this.UltraGroupBox1.ResumeLayout(false);
            this.UltraGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox2)).EndInit();
            this.UltraGroupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button btnSupplierClose;
        internal System.Windows.Forms.Button btnSupplierSave;
        internal System.Windows.Forms.TextBox txtSupplier;
        internal System.Windows.Forms.TextBox txtSupplierPhone;
        internal System.Windows.Forms.Label Label23;
        internal System.Windows.Forms.Label Label21;
        internal Infragistics.Win.Misc.UltraGroupBox UltraGroupBox1;
        internal System.Windows.Forms.CheckBox cbDiscontinued;
        internal Infragistics.Win.Misc.UltraGroupBox UltraGroupBox2;
        private System.Windows.Forms.Button btnReset;
        internal System.Windows.Forms.TextBox txtAddress;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox txtEmail;
        internal System.Windows.Forms.Label label2;
    }
}