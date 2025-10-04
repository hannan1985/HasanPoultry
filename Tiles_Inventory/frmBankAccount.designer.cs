namespace Tiles_Inventory
{
    partial class frmBankAccount
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
            this.UltraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.UltraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.cmbAccountType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpOpenDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtManagerName = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtBankName = new System.Windows.Forms.TextBox();
            this.txtResponsiblePerson = new System.Windows.Forms.TextBox();
            this.txtAccountNumber = new System.Windows.Forms.TextBox();
            this.txtOpeaningBalance = new System.Windows.Forms.TextBox();
            this.txtBranch = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox2)).BeginInit();
            this.UltraGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox1)).BeginInit();
            this.UltraGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // UltraGroupBox2
            // 
            this.UltraGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UltraGroupBox2.Controls.Add(this.btnSave);
            this.UltraGroupBox2.Controls.Add(this.btnRefresh);
            this.UltraGroupBox2.Controls.Add(this.btnClose);
            this.UltraGroupBox2.Location = new System.Drawing.Point(4, 349);
            this.UltraGroupBox2.Name = "UltraGroupBox2";
            this.UltraGroupBox2.Size = new System.Drawing.Size(477, 63);
            this.UltraGroupBox2.TabIndex = 1;
            this.UltraGroupBox2.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
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
            // UltraGroupBox1
            // 
            this.UltraGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UltraGroupBox1.Controls.Add(this.cmbAccountType);
            this.UltraGroupBox1.Controls.Add(this.label7);
            this.UltraGroupBox1.Controls.Add(this.dtpOpenDate);
            this.UltraGroupBox1.Controls.Add(this.label5);
            this.UltraGroupBox1.Controls.Add(this.label8);
            this.UltraGroupBox1.Controls.Add(this.label9);
            this.UltraGroupBox1.Controls.Add(this.label4);
            this.UltraGroupBox1.Controls.Add(this.label6);
            this.UltraGroupBox1.Controls.Add(this.label2);
            this.UltraGroupBox1.Controls.Add(this.Label1);
            this.UltraGroupBox1.Controls.Add(this.txtManagerName);
            this.UltraGroupBox1.Controls.Add(this.Label3);
            this.UltraGroupBox1.Controls.Add(this.txtAddress);
            this.UltraGroupBox1.Controls.Add(this.txtBranch);
            this.UltraGroupBox1.Controls.Add(this.txtBankName);
            this.UltraGroupBox1.Controls.Add(this.txtResponsiblePerson);
            this.UltraGroupBox1.Controls.Add(this.txtAccountNumber);
            this.UltraGroupBox1.Controls.Add(this.txtOpeaningBalance);
            this.UltraGroupBox1.Location = new System.Drawing.Point(4, 4);
            this.UltraGroupBox1.Name = "UltraGroupBox1";
            this.UltraGroupBox1.Size = new System.Drawing.Size(477, 342);
            this.UltraGroupBox1.TabIndex = 0;
            this.UltraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // cmbAccountType
            // 
            this.cmbAccountType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAccountType.FormattingEnabled = true;
            this.cmbAccountType.Items.AddRange(new object[] {
            "Savings",
            "Current",
            "LC",
            "CC(Hypo)",
            "FDR"});
            this.cmbAccountType.Location = new System.Drawing.Point(121, 211);
            this.cmbAccountType.Name = "cmbAccountType";
            this.cmbAccountType.Size = new System.Drawing.Size(160, 22);
            this.cmbAccountType.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(33, 215);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 14);
            this.label7.TabIndex = 103;
            this.label7.Text = "Account Type";
            // 
            // dtpOpenDate
            // 
            this.dtpOpenDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpOpenDate.Location = new System.Drawing.Point(121, 268);
            this.dtpOpenDate.Name = "dtpOpenDate";
            this.dtpOpenDate.Size = new System.Drawing.Size(103, 22);
            this.dtpOpenDate.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(30, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 14);
            this.label5.TabIndex = 101;
            this.label5.Text = "Manager Name";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(38, 70);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 14);
            this.label8.TabIndex = 101;
            this.label8.Text = "Bank Address";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(50, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 14);
            this.label4.TabIndex = 101;
            this.label4.Text = "Bank Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(26, 183);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 14);
            this.label6.TabIndex = 101;
            this.label6.Text = "Account Holder";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(18, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 14);
            this.label2.TabIndex = 101;
            this.label2.Text = "Account Number";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Location = new System.Drawing.Point(17, 244);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(101, 14);
            this.Label1.TabIndex = 101;
            this.Label1.Text = "Opening Amount";
            // 
            // txtManagerName
            // 
            this.txtManagerName.Location = new System.Drawing.Point(121, 123);
            this.txtManagerName.Name = "txtManagerName";
            this.txtManagerName.Size = new System.Drawing.Size(210, 22);
            this.txtManagerName.TabIndex = 3;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.Location = new System.Drawing.Point(51, 272);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(67, 14);
            this.Label3.TabIndex = 101;
            this.Label3.Text = "Open Date";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(121, 70);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(210, 47);
            this.txtAddress.TabIndex = 2;
            // 
            // txtBankName
            // 
            this.txtBankName.Location = new System.Drawing.Point(121, 15);
            this.txtBankName.Name = "txtBankName";
            this.txtBankName.Size = new System.Drawing.Size(210, 22);
            this.txtBankName.TabIndex = 0;
            // 
            // txtResponsiblePerson
            // 
            this.txtResponsiblePerson.Location = new System.Drawing.Point(121, 179);
            this.txtResponsiblePerson.Name = "txtResponsiblePerson";
            this.txtResponsiblePerson.Size = new System.Drawing.Size(210, 22);
            this.txtResponsiblePerson.TabIndex = 5;
            // 
            // txtAccountNumber
            // 
            this.txtAccountNumber.Location = new System.Drawing.Point(121, 151);
            this.txtAccountNumber.Name = "txtAccountNumber";
            this.txtAccountNumber.Size = new System.Drawing.Size(210, 22);
            this.txtAccountNumber.TabIndex = 4;
            // 
            // txtOpeaningBalance
            // 
            this.txtOpeaningBalance.Location = new System.Drawing.Point(121, 240);
            this.txtOpeaningBalance.Name = "txtOpeaningBalance";
            this.txtOpeaningBalance.Size = new System.Drawing.Size(210, 22);
            this.txtOpeaningBalance.TabIndex = 7;
            this.txtOpeaningBalance.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOpeaningBalance_KeyPress);
            // 
            // txtBranch
            // 
            this.txtBranch.Location = new System.Drawing.Point(121, 42);
            this.txtBranch.Name = "txtBranch";
            this.txtBranch.Size = new System.Drawing.Size(210, 22);
            this.txtBranch.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(39, 46);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 14);
            this.label9.TabIndex = 101;
            this.label9.Text = "Branch Name";
            // 
            // frmBankAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 415);
            this.Controls.Add(this.UltraGroupBox2);
            this.Controls.Add(this.UltraGroupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmBankAccount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bank Account";
            this.Load += new System.EventHandler(this.frmBankAccount_Load);
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox2)).EndInit();
            this.UltraGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UltraGroupBox1)).EndInit();
            this.UltraGroupBox1.ResumeLayout(false);
            this.UltraGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Infragistics.Win.Misc.UltraGroupBox UltraGroupBox2;
        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.Button btnRefresh;
        internal System.Windows.Forms.Button btnClose;
        internal Infragistics.Win.Misc.UltraGroupBox UltraGroupBox1;
        internal System.Windows.Forms.DateTimePicker dtpOpenDate;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox txtManagerName;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox txtOpeaningBalance;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox txtBankName;
        internal System.Windows.Forms.TextBox txtAccountNumber;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.Label label8;
        internal System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.ComboBox cmbAccountType;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.TextBox txtResponsiblePerson;
        internal System.Windows.Forms.Label label9;
        internal System.Windows.Forms.TextBox txtBranch;
    }
}