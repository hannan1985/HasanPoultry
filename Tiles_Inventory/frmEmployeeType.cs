using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IMFS.Common.DTO;
using IFMS.BLL;
using NybSys.MTBF.Utility.Enums;
using NybSys.MTBF.Utility.Helper;

namespace Tiles_Inventory
{
    public partial class frmEmployeeType : BaseForm
    {
        EmployeeType _employeeType = new EmployeeType();
        List<EmployeeType> _lstEmployeeType = new List<EmployeeType>();
        public event OnRoleInformationLoadEventHandler OnLoadEmployeeType;
        public delegate void OnRoleInformationLoadEventHandler();

        public frmEmployeeType()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadEmployeeType()
        {
            _lstEmployeeType = new EmployeeTypeManager().GetAllEmployeeType();
            grvRoleInfo.DataSource = _lstEmployeeType;
            grvRoleInfo.DisplayLayout.Bands[0].Columns["EmployeeTypeID"].Hidden = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveEmployeeType() == (int)MTBFEnums.ReturnResult.SUCCESS)
            {
                MessageBoxHelper.ShowInformation("Information saved successfully.");
                txtTypeName.Clear();
                txtTypeName.Focus();
                LoadEmployeeType();
                if (OnLoadEmployeeType != null)
                    OnLoadEmployeeType();
            }
            else
            {
                MessageBoxHelper.ShowInformation("Failed to save information.");
            }
        }

        private int SaveEmployeeType()
        {
            _employeeType.TypeName = txtTypeName.Text.Trim();
            return new EmployeeTypeManager().SaveEmployeeType(_employeeType);
        }


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtTypeName.Clear();
            txtTypeName.Focus();
        }

        private void frmEmployeeType_Load(object sender, EventArgs e)
        {
            LoadEmployeeType();
        }
    }
}
