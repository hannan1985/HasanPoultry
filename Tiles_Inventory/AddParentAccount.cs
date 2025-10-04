using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using IFMS.Facade;
using IMFS.Common.DTO;
using System.Collections.Generic;
using System.Linq;
using IMFS.Common.Utility;
using NybSys.MTBF.Utility.Constants;
using NybSys.MTBF.Utility.Helper;
using NybSys.MTBF.Utility.Enums;
using IFMS.BLL;

namespace Tiles_Inventory
{
    public partial class AddParentAccount : BaseForm
    {

        private int _parentAccountID = 0;
        private bool _isEdit = false;

        public delegate void LodaEventHandaler();
        public event LodaEventHandaler OnParentAccountAddad;
        List<AccountType> lstAccountType = new List<AccountType>();
        List<ParentAccount> lstParentAccount = new List<ParentAccount>();

        public AddParentAccount()
        {
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }


        public AddParentAccount(string aTCode, int parentAccountID, bool IsEdit)
        {
            if (IsEdit)
            {
                _isEdit = IsEdit;
                _parentAccountID = parentAccountID;
            }
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        private void AddParentAccount_Load(object sender, EventArgs e)
        {
            LoadAccountType();
            cmbAccountType.Enabled = true;
            LoadAllParentAccount();
            if (_isEdit)
            {
                LoadExistingData();
                cmbAccountType.Enabled = false;
            }

            btnSave.Enabled = (MTBFConstants.AppConstants.LoggedinUser.UserType == (int)MTBFEnums.UserType.Admin) ? true : false;

        }


        private void LoadExistingData()
        {
            ParentAccount parentAccount = new ParentAccount();
            parentAccount = DataAccessProxy.GetParentAccountByID(_parentAccountID);
            cmbAccountType.Value = parentAccount.AccountTypeID;
            txtAccountName.Text = parentAccount.AccountsName;
        }

        private void LoadAllParentAccount()
        {
            lstParentAccount = new ParentAccountManger().GetAllParentAccount().Cast<ParentAccount>().ToList();
            LoadDataInGrid(lstParentAccount);
        }

        private void LoadDataInGrid(List<ParentAccount> lstPAccount)
        {
            DataTable table = new DataTable();
            table.Columns.Add("AccountID");
            table.Columns.Add("Account Type");
            table.Columns.Add("Account Name");


            foreach (ParentAccount parentAccount in lstPAccount)
            {
                AccountType accountType = lstAccountType.Where(a => a.AccountTypeID == parentAccount.AccountTypeID).FirstOrDefault();
                DataRow row = table.NewRow();
                row["AccountID"] = parentAccount.AccountID;
                row["Account Name"] = parentAccount.AccountsName;
                row["Account Type"] = (accountType != null) ? accountType.AccountTypeName : string.Empty;
                table.Rows.Add(row);

            }

            grvRoleInfo.DataSource = table;
            grvRoleInfo.DisplayLayout.Bands[0].Columns["AccountID"].Hidden = true;
        }


        /// <summary>
        /// Method load account type code in combo box.
        /// </summary>
        private void LoadAccountType()
        {

            AccountType accountType = new AccountType();

            accountType.AccountTypeID = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            accountType.AccountTypeName = MTBFConstants.DataField.COMBO_DEFAULT_NAME;
            lstAccountType = DataAccessProxy.GetAllAccountType().Cast<AccountType>().ToList();
            lstAccountType.Insert(0, accountType);
            UIHelper.SetComboBoxData(cmbAccountType, lstAccountType, "AccountTypeName", "AccountTypeID");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidFormData())
            {
                if (_isEdit)
                {
                    if (UpdateParentAccount() > 0)
                    {
                        MessageBox.Show("Successfully Saved", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _isEdit = false;
                        txtAccountName.Clear();
                        txtAccountName.Focus();
                        LoadAllParentAccount();
                        if (OnParentAccountAddad != null)
                            OnParentAccountAddad();
                    }
                }
                else
                {
                    if (InsertParentAccount() > 0)
                    {
                        MessageBox.Show("Successfully Saved", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmbAccountType.Enabled = true;
                        txtAccountName.Clear();
                        txtAccountName.Focus();
                        LoadAllParentAccount();
                        if (OnParentAccountAddad != null)
                            OnParentAccountAddad();
                    }
                }

            }

        }


        /// <summary>
        /// Method to check valid form data.
        /// </summary>
        /// <returns></returns>
        private bool ValidFormData()
        {

            if (cmbAccountType.Value == null || Convert.ToInt32(cmbAccountType.Value) == MTBFConstants.DataField.COMBO_DEFAULT_ID)
            {
                MessageBoxHelper.ShowInformation("Please select account type.");
                cmbAccountType.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtAccountName.Text.Trim()))
            {
                MessageBoxHelper.ShowInformation("Please enter account name");
                txtAccountName.Focus();
                return false;
            }

            return true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbAccountHead_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LoadAccountType();
        }

        /// <summary>
        /// Method to insert parent account info.
        /// </summary>
        /// <returns></returns>
        private int InsertParentAccount()
        {
            int result = 0;
            ParentAccount parentAccount = new ParentAccount();
            try
            {
                parentAccount.AccountTypeID = Convert.ToInt32(cmbAccountType.Value);
                parentAccount.AccountsName = txtAccountName.Text.Trim() ;

                if (parentAccount.AccountTypeID == (int)IFMSEnum.AccountType.FixedAsset)
                {
                    parentAccount.IsBalanceSheetItem = true;
                }
                else if (parentAccount.AccountTypeID == (int)IFMSEnum.AccountType.CurrentAsset)
                {
                    parentAccount.IsBalanceSheetItem = true;
                }
                else if (parentAccount.AccountTypeID == (int)IFMSEnum.AccountType.LognTermLiabilities)
                {
                    parentAccount.IsBalanceSheetItem = true;
                }
                else if (parentAccount.AccountTypeID == (int)IFMSEnum.AccountType.ShortTermLiabilities)
                {
                    parentAccount.IsBalanceSheetItem = true;
                }
                else if (parentAccount.AccountTypeID == (int)IFMSEnum.AccountType.OperatingExpense)
                {
                    parentAccount.IsIncomeStatementItem = true;
                }
                else if (parentAccount.AccountTypeID == (int)IFMSEnum.AccountType.NonOperatingExpense)
                {

                }
                else if (parentAccount.AccountTypeID == (int)IFMSEnum.AccountType.OperatingIncome)
                {
                    parentAccount.IsIncomeStatementItem = true;
                }
                else if (parentAccount.AccountTypeID == (int)IFMSEnum.AccountType.NonOperatingIncome)
                {
                    parentAccount.IsIncomeStatementItem = true;
                }

                else if (parentAccount.AccountTypeID == (int)IFMSEnum.AccountType.CostofGoodSold)
                {
                    parentAccount.IsIncomeStatementItem = true;
                }
                else if (parentAccount.AccountTypeID == (int)IFMSEnum.AccountType.Capital)
                {
                    parentAccount.IsBalanceSheetItem = true;
                }


                AccountType accountType = lstAccountType.Where(a => a.AccountTypeID == parentAccount.AccountTypeID).FirstOrDefault();
                if (accountType != null)
                {
                    parentAccount.AccountHeadID = accountType.AccountHeadID;
                }
                parentAccount.IsDefault = false;
                parentAccount.CreatedBy = "Hannan";
                parentAccount.CreatedDate = DateTime.Now;
                parentAccount.Status = 1;
                result = DataAccessProxy.InsertParentAccount(parentAccount);

            }
            catch (Exception ex)
            {
                result = 0;
            }

            return result;
        }

        /// <summary>
        /// Method to update account info .
        /// </summary>
        /// <returns></returns>
        private int UpdateParentAccount()
        {
            int result = 0;

            ParentAccount parentAccount = DataAccessProxy.GetParentAccountByID(_parentAccountID);
            try
            {
                parentAccount.AccountTypeID = Convert.ToInt32(cmbAccountType.Value);
                parentAccount.AccountsName = txtAccountName.Text;
                if (parentAccount.AccountTypeID == (int)IFMSEnum.AccountType.FixedAsset)
                {
                    parentAccount.IsBalanceSheetItem = true;
                }
                else if (parentAccount.AccountTypeID == (int)IFMSEnum.AccountType.CurrentAsset)
                {
                    parentAccount.IsBalanceSheetItem = true;
                }
                else if (parentAccount.AccountTypeID == (int)IFMSEnum.AccountType.LognTermLiabilities)
                {
                    parentAccount.IsBalanceSheetItem = true;
                }
                else if (parentAccount.AccountTypeID == (int)IFMSEnum.AccountType.ShortTermLiabilities)
                {
                    parentAccount.IsBalanceSheetItem = true;
                }
                else if (parentAccount.AccountTypeID == (int)IFMSEnum.AccountType.OperatingExpense)
                {
                    parentAccount.IsIncomeStatementItem = true;
                }
                else if (parentAccount.AccountTypeID == (int)IFMSEnum.AccountType.NonOperatingExpense)
                {

                }
                else if (parentAccount.AccountTypeID == (int)IFMSEnum.AccountType.OperatingIncome)
                {
                    parentAccount.IsIncomeStatementItem = true;
                }
                else if (parentAccount.AccountTypeID == (int)IFMSEnum.AccountType.NonOperatingIncome)
                {
                    parentAccount.IsIncomeStatementItem = true;
                }

                else if (parentAccount.AccountTypeID == (int)IFMSEnum.AccountType.CostofGoodSold)
                {
                    parentAccount.IsIncomeStatementItem = true;
                }
                else if (parentAccount.AccountTypeID == (int)IFMSEnum.AccountType.Capital)
                {
                    parentAccount.IsBalanceSheetItem = true;
                }
                DataAccessProxy.UpdateParentAccount(parentAccount);

                result = 1;
            }
            catch (Exception ex)
            {
                result = 0;
            }

            return result;
        }

        private void grvRoleInfo_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            if (grvRoleInfo.Selected.Rows.Count > 0)
            {
                _parentAccountID = Convert.ToInt32(grvRoleInfo.Selected.Rows[0].Cells["AccountID"].Value);
                ParentAccount parentAccount = DataAccessProxy.GetParentAccountByID(_parentAccountID);
                txtAccountName.Text = parentAccount.AccountsName;
                cmbAccountType.Value = parentAccount.AccountTypeID;
                _isEdit = true;
            }
        }

        private void cmbAccountType_ValueChanged(object sender, EventArgs e)
        {
            //int accountTypeID = 0;
            //if (cmbAccountType.Value != null)
            //{
            //    int.TryParse(cmbAccountType.Value.ToString(), out accountTypeID);
            //    List<ParentAccount> lstFilteredParentAccount = new List<ParentAccount>();
            //    lstFilteredParentAccount = lstParentAccount.Where(p => p.AccountTypeID == accountTypeID).ToList();
            //    LoadDataInGrid(lstFilteredParentAccount);
            //}
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddParentAccount frm = new AddParentAccount();

            frm.ShowDialog();
        }


    }
}
