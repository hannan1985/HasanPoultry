namespace Tiles_Inventory
{
    partial class frmAssignAccessRight
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
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.btnClose = new Infragistics.Win.Misc.UltraButton();
            this.ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            this.btRoleModule = new Infragistics.Win.Misc.UltraButton();
            this.btUserRole = new Infragistics.Win.Misc.UltraButton();
            this.btRolePermission = new Infragistics.Win.Misc.UltraButton();
            this.btRoleAction = new Infragistics.Win.Misc.UltraButton();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).BeginInit();
            this.ultraGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.Controls.Add(this.btnClose);
            this.ultraGroupBox1.Controls.Add(this.ultraGroupBox2);
            this.ultraGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(752, 593);
            this.ultraGroupBox1.TabIndex = 0;
            this.ultraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(665, 562);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(81, 26);
            this.btnClose.TabIndex = 60;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ultraGroupBox2
            // 
            this.ultraGroupBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ultraGroupBox2.Controls.Add(this.btRoleModule);
            this.ultraGroupBox2.Controls.Add(this.btUserRole);
            this.ultraGroupBox2.Controls.Add(this.btRolePermission);
            this.ultraGroupBox2.Controls.Add(this.btRoleAction);
            this.ultraGroupBox2.Location = new System.Drawing.Point(98, 170);
            this.ultraGroupBox2.Name = "ultraGroupBox2";
            this.ultraGroupBox2.Size = new System.Drawing.Size(545, 253);
            this.ultraGroupBox2.TabIndex = 0;
            this.ultraGroupBox2.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // btRoleModule
            // 
            this.btRoleModule.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btRoleModule.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btRoleModule.Location = new System.Drawing.Point(342, 22);
            this.btRoleModule.Name = "btRoleModule";
            this.btRoleModule.Size = new System.Drawing.Size(192, 86);
            this.btRoleModule.TabIndex = 1;
            this.btRoleModule.Text = "Role Module";
            this.btRoleModule.Click += new System.EventHandler(this.btnRoleModule_Click);
            // 
            // btUserRole
            // 
            this.btUserRole.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btUserRole.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btUserRole.Location = new System.Drawing.Point(11, 22);
            this.btUserRole.Name = "btUserRole";
            this.btUserRole.Size = new System.Drawing.Size(192, 86);
            this.btUserRole.TabIndex = 0;
            this.btUserRole.Text = "User Role";
            this.btUserRole.Click += new System.EventHandler(this.btnUserRole_Click);
            // 
            // btRolePermission
            // 
            this.btRolePermission.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btRolePermission.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btRolePermission.Location = new System.Drawing.Point(11, 145);
            this.btRolePermission.Name = "btRolePermission";
            this.btRolePermission.Size = new System.Drawing.Size(192, 86);
            this.btRolePermission.TabIndex = 2;
            this.btRolePermission.Text = "Role Permission";
            this.btRolePermission.Click += new System.EventHandler(this.btnRolePermission_Click);
            // 
            // btRoleAction
            // 
            this.btRoleAction.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btRoleAction.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btRoleAction.Location = new System.Drawing.Point(341, 145);
            this.btRoleAction.Name = "btRoleAction";
            this.btRoleAction.Size = new System.Drawing.Size(192, 86);
            this.btRoleAction.TabIndex = 3;
            this.btRoleAction.Text = "Role Action";
            this.btRoleAction.Click += new System.EventHandler(this.btnRoleAction_Click);
            // 
            // frmAssignAccessRight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 593);
            this.Controls.Add(this.ultraGroupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmAssignAccessRight";
            this.Text = "Assign Access Right";
            this.Load += new System.EventHandler(this.frmAssignAccessRight_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).EndInit();
            this.ultraGroupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private Infragistics.Win.Misc.UltraButton btRolePermission;
        private Infragistics.Win.Misc.UltraButton btRoleAction;
        private Infragistics.Win.Misc.UltraButton btRoleModule;
        private Infragistics.Win.Misc.UltraButton btUserRole;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox2;
        private Infragistics.Win.Misc.UltraButton btnClose;
    }
}