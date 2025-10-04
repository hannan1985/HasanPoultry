using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tiles_Inventory
{
    public partial class frmAssignAccessRight : BaseForm 
    {
        public frmAssignAccessRight()
        {
            InitializeComponent();
        }

        private void btnUserRole_Click(object sender, EventArgs e)
        {
            frmAddRole frm = new frmAddRole();
            frm.ShowDialog();
        }

        private void frmAssignAccessRight_Load(object sender, EventArgs e)
        {
            base.CheckAction(this);
        }

        private void btnRoleModule_Click(object sender, EventArgs e)
        {
            frmAssignModule frm = new frmAssignModule();
            frm.ShowDialog();
        }

        private void btnRoleAction_Click(object sender, EventArgs e)
        {
            frmAssignAction frm = new frmAssignAction();
            frm.ShowDialog();
        }

        private void btnRolePermission_Click(object sender, EventArgs e)
        {
            frmAssignPermission frm = new frmAssignPermission();
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
