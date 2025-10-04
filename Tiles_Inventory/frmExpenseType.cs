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
using NybSys.MTBF.Utility.Helper;
using NybSys.MTBF.Utility.Enums;



namespace Tiles_Inventory
{
    public partial class frmExpenseType : BaseForm
    {
        public event LoadExpenseTypeEventHandler LoadExpenseType;
        public delegate void LoadExpenseTypeEventHandler();
        ExpenseType _expenseType = new ExpenseType();
        private int _expenseTypeID = 0;

        public frmExpenseType()
        {
            InitializeComponent();
        }

        #region "Private Events"


        private bool ValidFormData()
        {
            if (string.IsNullOrEmpty(txtExpenseType.Text))
            {
                MessageBoxHelper.ShowInformation("You need to enter expense type name.");
                txtExpenseType.Focus();
                return false;
            }

            List<ExpenseType> lstExpenseType = new ExpenseManager().GetAllExpenseType().Cast<ExpenseType>().Where(r => r.ExpenseTypeName.ToLower() == txtExpenseType.Text.ToLower()).ToList();

            if (lstExpenseType.Count > 0 && lstExpenseType[0].ExpenseTypeID != _expenseTypeID)
            {
                MessageBoxHelper.ShowInformation("This expenseType is already exists");
                txtExpenseType.Focus();
                return false;
            }

            return true;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {

            if (ValidFormData())
            {
                if (InsertUserExpenseType() == (int)MTBFEnums.ReturnResult.SUCCESS)
                {
                    MessageBoxHelper.ShowInformation("ExpenseType information saved successfully");
                    LoadAllExpenseType();
                    txtExpenseType.Clear();
                    if (LoadExpenseType != null)
                        LoadExpenseType();
                }
                else
                {
                    MessageBoxHelper.ShowInformation("Failed to save expense type information");
                }
            }

        }




        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }


        private void frmAddExpenseType_Load(object sender, EventArgs e)
        {
            LoadAllExpenseType();
        }

        #endregion

        #region "Private Methods"

        /// <summary>
        /// Method to check duplicate expenseType name.
        /// </summary>
        /// <returns></returns>
        private bool IsDuplicateExpenseType()
        {
            List<ExpenseType> lstExpenseType = new ExpenseManager().GetAllExpenseType().Cast<ExpenseType>().Where(r => r.ExpenseTypeName.ToLower() == txtExpenseType.Text.ToLower()).ToList();

            if (lstExpenseType.Count > 0 && lstExpenseType[0].ExpenseTypeID != _expenseTypeID)
            {
                MessageBoxHelper.ShowInformation("This expenseType is already exists");
                txtExpenseType.Focus();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method to insert 
        /// </summary>
        /// <returns></returns>
        private int InsertUserExpenseType()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            _expenseType.ExpenseTypeName = txtExpenseType.Text;
            result = new ExpenseManager().SaveExpenseType(_expenseType);
            return result;
        }

        /// <summary>
        /// Method to update expenseType
        /// </summary>
        /// <returns></returns>
        private int UpdateUserExpenseType()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            ExpenseType expenseType = new ExpenseManager().GetExpenseTypeByID(_expenseTypeID);
            expenseType.ExpenseTypeName = txtExpenseType.Text;
            result = new ExpenseManager().UpdateExpenseType(expenseType);
            return result;
        }

        /// <summary>
        /// Method to load all expenseType in grid.
        /// </summary>
        private void LoadAllExpenseType()
        {
            DataTable expenseTypeTable = new DataTable();
            expenseTypeTable.Columns.Add("ExpenseTypeID");
            expenseTypeTable.Columns.Add("Type Name");
            foreach (ExpenseType expenseType in new ExpenseManager().GetAllExpenseType())
            {
                DataRow row = expenseTypeTable.NewRow();
                row["ExpenseTypeID"] = expenseType.ExpenseTypeID;
                row["Type Name"] = expenseType.ExpenseTypeName;
                expenseTypeTable.Rows.Add(row);
            }
            grvExpenseTypeInfo.DataSource = expenseTypeTable;

            grvExpenseTypeInfo.DisplayLayout.Bands[0].Columns["ExpenseTypeID"].Hidden = true;
        }

        #endregion

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtExpenseType.Clear();
        }

        private void grvExpenseTypeInfo_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            if (grvExpenseTypeInfo.Selected.Rows.Count > 0)
            {
                _expenseTypeID = Convert.ToInt32(grvExpenseTypeInfo.Selected.Rows[0].Cells["ExpenseTypeID"].Value);

                _expenseType = new ExpenseManager().GetExpenseTypeByID(_expenseTypeID);
                if (_expenseType != null)
                {
                    txtExpenseType.Text = _expenseType.ExpenseTypeName;                   
                    base.IsUpdating = true;
                }
            }
        }






    }
}
