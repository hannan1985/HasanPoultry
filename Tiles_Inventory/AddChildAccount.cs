using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IMFS.Common.DTO;
using IFMS.Facade;
using NybSys.MTBF.Utility.Constants;
using NybSys.MTBF.Utility.Enums;
using NybSys.MTBF.Utility.Helper;
using IFMS.BLL;

namespace Tiles_Inventory
{
    public partial class AddChildAccount : BaseForm
    {
        private int _childAccountID = 0;

        public delegate void LodaEventHandaler();
        public event LodaEventHandaler OnChildAccountAddad;
        List<ParentAccount> lstParentAccount = new List<ParentAccount>();
        List<ChildAccount> lstChildAccount = new List<ChildAccount>();
        private int pAAccountID = 0;

        public AddChildAccount()
        {
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        public AddChildAccount(int parenetAccountID)
        {
            pAAccountID = parenetAccountID;
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        private void AddChildAccount_Load(object sender, EventArgs e)
        {
            LoadParentAccount();
            LoadAllChildAccount();
        }


        private void LoadAllChildAccount()
        {
            lstChildAccount = new ChildAccountManager().GetAllChildAccount().Cast<ChildAccount>().ToList();
            int parenetAccountID = 0;
            //if (cmbParentAccount.Value != null)
            //{
            //    int.TryParse(cmbParentAccount.Value.ToString(), out parenetAccountID);
            //}
            List<ChildAccount> lstFilteredChildAccount = new List<ChildAccount>();
            if (parenetAccountID > 0)
            {
                lstFilteredChildAccount = lstChildAccount.Where(c => c.AccountID == parenetAccountID).ToList();
            }
            else
            {
                lstFilteredChildAccount = lstChildAccount;
            }

            LoadDataInGrid(lstFilteredChildAccount);
        }


        private void LoadDataInGrid(List<ChildAccount> lstCAccount)
        {
            DataTable table = new DataTable();
            table.Columns.Add("AccountID");
            table.Columns.Add("Account Head");
            table.Columns.Add("Account Name");

            foreach (ChildAccount childAccount in lstCAccount)
            {
                ParentAccount parentAccount = lstParentAccount.Where(p => p.AccountID == childAccount.AccountID).FirstOrDefault();
                DataRow row = table.NewRow();
                row["AccountID"] = childAccount.ChildAccountID;
                row["Account Name"] = childAccount.Description;
                row["Account Head"] = (parentAccount != null) ? parentAccount.AccountsName : string.Empty;
                table.Rows.Add(row);
            }
            grvRoleInfo.DataSource = table;
            grvRoleInfo.DisplayLayout.Bands[0].Columns["AccountID"].Hidden = true;

        }

        /// <summary>
        /// Method to load parent account info in combo box.
        /// </summary>
        private void LoadParentAccount()
        {

            ParentAccount parentAccount = new ParentAccount();

            parentAccount.AccountID = MTBFConstants.DataField.COMBO_DEFAULT_ID;
            parentAccount.AccountsName = MTBFConstants.DataField.COMBO_DEFAULT_NAME;

            lstParentAccount = DataAccessProxy.GetAllParentAccount().Cast<ParentAccount>().ToList();
            lstParentAccount.Insert(0, parentAccount);

            UIHelper.SetComboBoxData(cmbParentAccount, lstParentAccount, "AccountsName", "AccountID");
        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (VaildFormData())
            {
                if (IsUpdating)
                {
                    if (UpdateChildAccount() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Child account informaiton saved successfully.");
                        IsUpdating = false;
                        LoadAllChildAccount();
                        txtAccountName.Clear();
                        txtAccountName.Focus();
                        IsUpdating = false;
                        if (OnChildAccountAddad != null)
                            OnChildAccountAddad();
                    }
                }
                else
                {
                    if (InsertChildAccount() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Child account informaiton saved successfully.");
                        LoadAllChildAccount();
                        txtAccountName.Clear();
                        txtAccountName.Focus();
                        if (OnChildAccountAddad != null)
                            OnChildAccountAddad();
                    }
                }
            }

           
        }

        private bool VaildFormData()
        {
            int accountID = 0;
            if (cmbParentAccount.Value == null)
            {
                MessageBoxHelper.ShowInformation("Please select account type.");
                cmbParentAccount.Focus();
                return false;
            }


            List<ChildAccount> lstChildaccount = new List<ChildAccount>();
           
            int.TryParse(cmbParentAccount.Value.ToString(), out accountID);

            if (accountID == 0)
            {
                MessageBoxHelper.ShowInformation("Please select account type.");
                cmbParentAccount.Focus();
                return false;
            }

            lstChildaccount = new ChildAccountManager().GetAllChildAccountByBranchID(MTBFConstants.AppConstants.BranchID);

            ChildAccount existingChildAccount = lstChildaccount.Where(c => c.Description.ToLower().Trim() == txtAccountName.Text.ToLower().Trim()).FirstOrDefault();

            if (existingChildAccount != null && existingChildAccount.ChildAccountID != _childAccountID)
            {
                MessageBoxHelper.ShowInformation("Duplicate chart of account.");
                txtAccountName.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Method to insert child account information.
        /// </summary>
        /// <returns></returns>
        private int InsertChildAccount()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            ChildAccount childAccount = new ChildAccount();
            childAccount.AccountID = Convert.ToInt32(cmbParentAccount.Value);
            childAccount.Description = txtAccountName.Text.Trim();
            childAccount.CreatedBy = "Hannan";
            childAccount.CreatedDate = DateTime.Now;
            childAccount.Status = 1;
            result = DataAccessProxy.InsertChildAccount(childAccount);
            return result;

        }


        private int UpdateChildAccount()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            ChildAccount childAccount = DataAccessProxy.GetChildAccountByID(_childAccountID);
            childAccount.AccountID = Convert.ToInt32(cmbParentAccount.Value);
            childAccount.Description = txtAccountName.Text.Trim();
            result = DataAccessProxy.UpdateChildAccount(childAccount);
            return result;

        }

        private void grvRoleInfo_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            if (grvRoleInfo.Selected.Rows.Count > 0)
            {
                _childAccountID = Convert.ToInt32(grvRoleInfo.Selected.Rows[0].Cells["AccountID"].Value);
                ChildAccount childAccount = DataAccessProxy.GetChildAccountByID(_childAccountID);
                txtAccountName.Text = childAccount.Description;
                cmbParentAccount.Value = childAccount.AccountID;
                IsUpdating = true;
            }
        }

        private void cmbParentAccount_ValueChanged(object sender, EventArgs e)
        {
            //int parenetAccountID = 0;
            //if (cmbParentAccount.Value != null)
            //{
            //    int.TryParse(cmbParentAccount.Value.ToString(), out parenetAccountID);
            //    List<ChildAccount> lstFilteredChildAccount = lstChildAccount.Where(c => c.AccountID == parenetAccountID).ToList();
            //    LoadDataInGrid(lstFilteredChildAccount);

            //}
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddParentAccount frm = new AddParentAccount();
            frm.OnParentAccountAddad += new AddParentAccount.LodaEventHandaler(frm_OnParentAccountAddad);
            frm.ShowDialog();
        }

        void frm_OnParentAccountAddad()
        {
            LoadParentAccount();
        }

    }
}
