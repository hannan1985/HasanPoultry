using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Collections;
using NybSys.MTBF.Utility.Helper;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Enums;
using NybSys.MTBF.Utility.Constants;
using IMFS.Common.Utility;


namespace IFMS.BLL
{
    public class BankAccountManager : BllBase
    {
        #region "Codes for BankAccount"


        public int SaveBankAccount(BankAccount bankAccount)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            try
            {
                this.DataAccessManager.BeginTransaction();

                if (bankAccount.BankAccountID > 0)
                {
                   
                    bankAccount.UpdatedBy = MTBFConstants.AppConstants.LoggedinUserID;
                    bankAccount.UpdatedDate = DateTime.Now;
                    this.DataAccessManager.Update<BankAccount>(bankAccount);
                    DeletePreviousJournal(bankAccount);

                    string filter = string.Format("{0}='{1}' and {2}={3}", "ReferenceType", (int)MTBFEnums.ChildAccountType.Bank, "ReferenceID", bankAccount.BankAccountID);
                    ChildAccount childAccount = base.DataAccessManager.GetFilteredData<ChildAccount>(filter).Cast<ChildAccount>().ToList().FirstOrDefault();
                    if (childAccount != null)
                    {
                        childAccount.Description = bankAccount.BankAccountNumber;
                        base.DataAccessManager.Update<ChildAccount>(childAccount);
                    }
                    else
                    {
                        childAccount = new ChildAccount();
                        childAccount.AccountID = IFMSConstant.Bank;
                        childAccount.Description = bankAccount.BankName + " " + bankAccount.BankAccountNumber;
                        childAccount.ReferenceType = (int)MTBFEnums.ChildAccountType.Bank;
                        childAccount.ReferenceID = bankAccount.BankAccountID;
                        childAccount.BranchID = MTBFConstants.AppConstants.BranchID;
                        childAccount.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                        childAccount.CreatedBy = MTBFConstants.AppConstants.LoggedinUser.UserID;
                        childAccount.CreatedDate = DateTime.Now;
                        childAccount.Status = 1;
                        base.DataAccessManager.Insert<ChildAccount>(childAccount);
                    }
                }
                else
                {
                    bankAccount.CreatedDate = DateTime.Now;
                    bankAccount.CreatedBy = MTBFConstants.AppConstants.LoggedinUserID;
                    bankAccount.UpdatedDate = Convert.ToDateTime(MTBFConstants.DefauldDate);
                    bankAccount.BankAccountID = this.DataAccessManager.Insert<BankAccount>(bankAccount);

                    ChildAccount childAccount = new ChildAccount();
                    childAccount.AccountID = IFMSConstant.Bank;
                    childAccount.Description = bankAccount.BankName + " " + bankAccount.BankAccountNumber;
                    childAccount.ReferenceType = (int)MTBFEnums.ChildAccountType.Bank;
                    childAccount.ReferenceID = bankAccount.BankAccountID;
                    childAccount.BranchID = MTBFConstants.AppConstants.BranchID;
                    childAccount.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                    childAccount.CreatedBy = MTBFConstants.AppConstants.LoggedinUser.UserID;
                    childAccount.CreatedDate = DateTime.Now;
                    childAccount.Status = 1;
                    base.DataAccessManager.Insert<ChildAccount>(childAccount);
                }
                if (bankAccount.OpeningBalance > 0)
                {
                    InsertJournalInformationForBankAccount(bankAccount);
                }
          

                this.DataAccessManager.CommitTransaction();

            }
            catch (Exception exp)
            {
                this.DataAccessManager.Rollback();
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            finally
            {
                this.DataAccessManager.EndTransaction();
            }
            return result;


        }


        /// <summary>
        /// Method to insert BankAccount.
        /// </summary>
        /// <param name="bankAccount"></param>
        /// <returns></returns>
        public int InsertBankAccount(BankAccount bankAccount)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            int bankAccountID = 0;
            try
            {
                this.DataAccessManager.BeginTransaction();

                bankAccountID = this.DataAccessManager.Insert<BankAccount>(bankAccount);

                bankAccount.BankAccountID = bankAccountID;



                this.DataAccessManager.CommitTransaction();

            }
            catch (Exception exp)
            {
                this.DataAccessManager.Rollback();
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            finally
            {
                this.DataAccessManager.EndTransaction();
            }
            return result;


        }



        /// <summary>
        /// Method to update BankAccount.
        /// </summary>
        /// <param name="bankAccount"></param>
        /// <returns></returns>
        public int UpdateBankAccount(BankAccount bankAccount)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            try
            {
                this.DataAccessManager.BeginTransaction();

                this.DataAccessManager.Update<BankAccount>(bankAccount);

                DeletePreviousJournal(bankAccount);

                InsertJournalInformationForBankAccount(bankAccount);

                this.DataAccessManager.CommitTransaction();

            }
            catch (Exception exp)
            {
                this.DataAccessManager.Rollback();
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            finally
            {
                this.DataAccessManager.EndTransaction();
            }
            return result;
        }

        private void DeletePreviousJournal(BankAccount bankAccount)
        {
            string filter = string.Format("{0}={1} And BankWithdrawID=0 And BankDepositID=0", "BankAccountID", bankAccount.BankAccountID);

            Journal journal = DataAccessManager.GetFilteredData<Journal>(filter).Cast<Journal>().ToList().FirstOrDefault();
            if (journal != null)
            {
                filter = string.Format("{0}={1}", "ReferenceNo", journal.ReferenceNo);
                foreach (Journal jou in DataAccessManager.GetFilteredData<Journal>(filter))
                {
                    DataAccessManager.Delete<Journal>(jou.JournalID);
                }
            }


        }



        /// <summary>
        /// Method to get all BankAccount.
        /// </summary>
        /// <returns></returns>
        public List<BankAccount> GetAllBankAccount()
        {
            return this.DataAccessManager.GetAll<BankAccount>().Cast<BankAccount>().ToList();
        }

        /// <summary>
        /// Get BankAccount by id
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public BankAccount GetBankAccountByID(int ID)
        {
            string filter = string.Format("{0}={1}", "BankAccountID", ID);
            return base.DataAccessManager.GetFilteredData<BankAccount>(filter).Cast<BankAccount>().ToList().FirstOrDefault();
        }

        /// <summary>
        /// Method to get BankAccount by BankAccount code.
        /// </summary>
        /// <param name="bankAccountNumber"></param>
        /// <returns></returns>
        public BankAccount GetBankAccountByBankAccountNumber(string bankAccountNumber)
        {
            string filter = string.Format("{0}='{1}'", "BankAccountNumber", bankAccountNumber);
            return base.DataAccessManager.GetFilteredData<BankAccount>(filter).Cast<BankAccount>().ToList().FirstOrDefault();
        }



        #endregion


        #region "Codes for BankAccount Deposit"

        /// <summary>
        /// Method to insert BankAccount deposit..
        /// </summary>
        /// <param name="bankDeposit"></param>
        /// <returns></returns>
        public int InsertBankDeposit(BankDeposit bankDeposit)
        {
            int bankDepositID = 0;
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                this.DataAccessManager.BeginTransaction();

                bankDepositID = this.DataAccessManager.Insert<BankDeposit>(bankDeposit);
                bankDeposit.BankDepositID = bankDepositID;
                InsertJournalInformationForBankDeposit(bankDeposit);

                this.DataAccessManager.CommitTransaction();

            }
            catch (Exception exp)
            {
                this.DataAccessManager.Rollback();
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            finally
            {
                this.DataAccessManager.EndTransaction();
            }
            return result;
        }


        /// <summary>
        /// Method to insert journal information.
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        //private void ProcessJournalForBankAccountDeposit(BankDeposit bankDeposit)
        //{

        //    int journalReferenceNo = new JournalManager().GetJournalReferenceID();

        //    InsertJournalInformationForBankDeposit(bankDeposit.DepositAmount, "Dr", bankDeposit.BankAccountID, bankDeposit.BankAccountNumber, journalReferenceNo, IFMSConstant.Bank, bankDeposit.BankDepositID);
        //    InsertJournalInformationForBankDeposit(bankDeposit.DepositAmount, "Cr", 0, bankDeposit.BankAccountNumber, journalReferenceNo, IFMSConstant.CashAccountID, bankDeposit.BankDepositID);
        //}


        private void ProcessJournalForBakWithdraw(BankWithdraw bankWithdraw)
        {
            int referenceID = 1;
            decimal amount = bankWithdraw.WithdrawAmount;
            referenceID = new JournalManager().GetJournalReferenceID();
            string filter = string.Format("{0}='{1}' and {2}={3}", "ReferenceType", (int)MTBFEnums.ChildAccountType.Bank, "ReferenceID", bankWithdraw.BankAccountID);
            ChildAccount childAccount = base.DataAccessManager.GetFilteredData<ChildAccount>(filter).Cast<ChildAccount>().ToList().FirstOrDefault();

            for (int i = 0; i <= 1; i++)
            {
                IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();

                if (i == 0)
                {
                    journal.DrCrIndecator = "Dr";
                    journal.AccountID = IFMSConstant.CashAccountID;
                }
                else
                {
                    journal.DrCrIndecator = "Cr";
                    journal.AccountID = childAccount.AccountID;
                    journal.ChildAccountID = childAccount.ChildAccountID;
                }
                journal.Description = "Withdraw by office";
                journal.BankWithdrawID = bankWithdraw.WithdrawID;
                journal.JAccountID = 0;
                journal.Amount = amount;
                journal.ReferenceNo = referenceID;
                journal.BankAccountID = bankWithdraw.BankAccountID;
                journal.JournalType = (int)MTBFEnums.JournalType.Inventory;
                journal.BranchID = MTBFConstants.AppConstants.BranchID;
                journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                base.DataAccessManager.Insert<Journal>(journal);
            }

           
        }





        /// <summary>
        /// Method to insert journal information.
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="activityCenterCode"></param>
        /// <param name="respCenterCode"></param>
        /// <param name="controlItemCode"></param>
        /// <param name="reportingItemCode"></param>
        /// <param name="detailItemCode"></param>
        /// <param name="amount"></param>
        /// <param name="drCrIndicator"></param>
        private void InsertJournalInformationForBankWithdraw(decimal amount, string drCrIndicator, int bankAccountID, string bankAccountNumber, int referenceNo, int accountID, int bankWithDrawID)
        {
            Journal journal = new Journal();

            journal.ReferenceNo = referenceNo;

            if (drCrIndicator == "Dr")
            {
                journal.Amount = amount;

            }
            else
            {
                journal.Amount = amount;
            }
            journal.Description = "Withdrawn by office";
            journal.BankAccountID = bankAccountID;
            journal.BankWithdrawID = bankWithDrawID;
            journal.BranchID = MTBFConstants.AppConstants.BranchID;
            journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            journal.AccountID = accountID;
            journal.DrCrIndecator = drCrIndicator;


            base.DataAccessManager.Insert<Journal>(journal);
        }


        private void InsertJournalInformationForBankDeposit(BankDeposit bankDeposit)
        {
            int referenceID = 1;
            decimal amount = bankDeposit.DepositAmount;
            referenceID = new JournalManager().GetJournalReferenceID();
            string filter = string.Format("{0}='{1}' and {2}={3}", "ReferenceType", (int)MTBFEnums.ChildAccountType.Bank, "ReferenceID", bankDeposit.BankAccountID);
            ChildAccount childAccount = base.DataAccessManager.GetFilteredData<ChildAccount>(filter).Cast<ChildAccount>().ToList().FirstOrDefault();

            for (int i = 0; i <= 1; i++)
            {
                IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();

                if (i == 0)
                {
                    journal.DrCrIndecator = "Dr";
                    journal.AccountID = childAccount.AccountID;
                    journal.ChildAccountID = childAccount.ChildAccountID;
                }
                else
                {
                    journal.DrCrIndecator = "Cr";
                    journal.AccountID = IFMSConstant.CashAccountID;
                }
                journal.Description = "Deposited by office";
                journal.JAccountID = 0;
                journal.BankDepositID = bankDeposit.BankDepositID;
                journal.Amount = amount;
                journal.ReferenceNo = referenceID;
                journal.BankAccountID = bankDeposit.BankAccountID;
                journal.JournalType = (int)MTBFEnums.JournalType.Inventory;
                journal.BranchID = MTBFConstants.AppConstants.BranchID;
                journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                base.DataAccessManager.Insert<Journal>(journal);
            }
        }



        private void InsertJournalInformationForBankAccount(BankAccount bankAccount)
        {
            int referenceID = 1;
            decimal amount = bankAccount.OpeningBalance;
            referenceID = new JournalManager().GetJournalReferenceID();
            string filter = string.Format("{0}='{1}' and {2}={3}", "ReferenceType", (int)MTBFEnums.ChildAccountType.Bank, "ReferenceID", bankAccount.BankAccountID);
            ChildAccount childAccount = base.DataAccessManager.GetFilteredData<ChildAccount>(filter).Cast<ChildAccount>().ToList().FirstOrDefault();

            for (int i = 0; i <= 1; i++)
            {
                IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();

                if (i == 0)
                {
                    journal.DrCrIndecator = "Dr";
                    journal.AccountID = childAccount.AccountID;
                    journal.ChildAccountID = childAccount.ChildAccountID;
                }
                else
                {
                    journal.DrCrIndecator = "Cr";
                    journal.AccountID = IFMSConstant.Capital;
                }
                journal.Description = "Bank Opening Balance";
                journal.JAccountID = 0;
                journal.Amount = amount;
                journal.ReferenceNo = referenceID;
                journal.BankAccountID = bankAccount.BankAccountID;
                journal.JournalType = (int)MTBFEnums.JournalType.Inventory;
                journal.BranchID = MTBFConstants.AppConstants.BranchID;
                journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                base.DataAccessManager.Insert<Journal>(journal);
            }


        }



        ///// <summary>
        ///// Method to update BankAccount deposit.
        ///// </summary>
        ///// <param name="BankAccountDeposit"></param>
        ///// <returns></returns>
        //public int UpdateBankAccountDeposit(BankAccountDeposit BankAccountDeposit)
        //{
        //    try
        //    {
        //        int result = this.DataAccessManager.Update<BankAccountDeposit>(BankAccountDeposit);
        //        return result;
        //    }
        //    catch (Exception exp)
        //    {
        //        throw exp;
        //    }
        //}

        ///// <summary>
        ///// Method to get all BankAccount deposit.
        ///// </summary>
        ///// <returns></returns>
        //public List<BankAccountDeposit> GetAllBankAccountDeposit(string companyCode)
        //{
        //    string filter = string.Format("{0}='{1}'", MTBFConstants.DataField.COMPANY_CODE, companyCode);
        //    List<BankAccountDeposit> lstBankAccountDeposit = this.DataAccessManager.GetFilteredData<BankAccountDeposit>(filter).Cast<BankAccountDeposit>().ToList();
        //    return lstBankAccountDeposit;
        //}

        ///// <summary>
        ///// Method to get BankAccount deposit by id.
        ///// </summary>
        ///// <param name="ID"></param>
        ///// <returns></returns>
        //public BankAccountDeposit GetBankAccountDepositByID(int ID)
        //{
        //    BankAccountDeposit BankAccountDeposit = this.DataAccessManager.GetByID<BankAccountDeposit>(ID, MTBFConstants.DataField.BankAccount_DEPOSIT_ID);
        //    return BankAccountDeposit;
        //}

        ///// <summary>
        ///// Method to get BankAccount deposit reference no by BankAccount id, branch id and ref no.
        ///// </summary>
        ///// <returns></returns>
        //public IList GetDepositRefNoByBankAccountIdAndBranchIdAndRefNo(int BankAccountID, int branchID, string refNo)
        //{
        //    string filter = string.Format(MTBFConstants.QueryConstants.GET_DEPOSIT_REF_NO_BY_BankAccountID_AND_BRANCHID, BankAccountID, branchID, refNo);
        //    return base.DataAccessManager.ExecuteSQL<BankAccountDeposit>(filter).Cast<BankAccountDeposit>().ToList();
        //}

        ///// <summary>
        ///// Method to get BankAccount deposit by BankAccount account id.
        ///// </summary>
        ///// <param name="BankAccountAccountID"></param>
        ///// <returns></returns>
        //public List<BankAccountDeposit> GetBankAccountDepositByBankAccountAccountID(int BankAccountAccountID)
        //{
        //    string filter = string.Format("{0}={1}", MTBFConstants.DataField.BankAccount_ACCOUNT_ID, BankAccountAccountID);
        //    return base.DataAccessManager.GetFilteredData<BankAccountDeposit>(filter).Cast<BankAccountDeposit>().ToList();
        //}

        #endregion

        //#region "Codes for BankAccount Withdraw"

        /// <summary>
        /// Method to insert BankAccount withdraw.
        /// </summary>
        /// <param name="bankWithdraw"></param>
        /// <returns></returns>
        public int InsertBankWithdraw(BankWithdraw bankWithdraw)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            int withdrawID = 0;
            try
            {
                this.DataAccessManager.BeginTransaction();

                withdrawID = this.DataAccessManager.Insert<BankWithdraw>(bankWithdraw);
                bankWithdraw.WithdrawID = withdrawID;
                ProcessJournalForBakWithdraw(bankWithdraw);

                this.DataAccessManager.CommitTransaction();
            }
            catch (Exception exp)
            {
                this.DataAccessManager.Rollback();
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            finally
            {
                this.DataAccessManager.EndTransaction();
            }
            return result;
        }

        ///// <summary>
        ///// Method to update BankAccount withdraw.
        ///// </summary>
        ///// <param name="BankAccountWithdraw"></param>
        ///// <returns></returns>
        //public int UpdateBankAccountWithdraw(BankAccountWithdraw BankAccountWithdraw)
        //{
        //    try
        //    {
        //        int result = this.DataAccessManager.Update<BankAccountWithdraw>(BankAccountWithdraw);
        //        return result;
        //    }
        //    catch (Exception exp)
        //    {
        //        throw exp;
        //    }
        //}

        ///// <summary>
        ///// Method to get all BankAccount withdraw.
        ///// </summary>
        ///// <returns></returns>
        //public List<BankAccountWithdraw> GetAllBankAccountWithdraw(string companyCode)
        //{
        //    string filter = string.Format("{0}='{1}'", MTBFConstants.DataField.COMPANY_CODE, companyCode);
        //    List<BankAccountWithdraw> lstBankAccountWithdraw = this.DataAccessManager.GetFilteredData<BankAccountWithdraw>(filter).Cast<BankAccountWithdraw>().ToList();
        //    return lstBankAccountWithdraw;
        //}

        ///// <summary>
        ///// Method to get BankAccount withdraw by id.
        ///// </summary>
        ///// <param name="ID"></param>
        ///// <returns></returns>
        //public BankAccountWithdraw GetBankAccountWithdrawByID(int ID)
        //{
        //    BankAccountWithdraw BankAccountWithdraw = this.DataAccessManager.GetByID<BankAccountWithdraw>(ID, MTBFConstants.DataField.BankAccount_WITHDRAW_ID);
        //    return BankAccountWithdraw;
        //}

        ///// <summary>
        ///// Method to get BankAccount withdraw reference no by BankAccount id, branch id and withdraw ref no.
        ///// </summary>
        ///// <returns></returns>
        //public IList GetWithdrawRefNoByBankAccountIdAndBranchIdAndRefNo(int BankAccountID, int branchID, string withdrawRefNo)
        //{
        //    string filter = string.Format(MTBFConstants.QueryConstants.GET_WITHDRAW_REF_NO_BY_BankAccountID_AND_BRANCHID, BankAccountID, branchID, withdrawRefNo);
        //    return base.DataAccessManager.ExecuteSQL<BankAccountWithdraw>(filter).Cast<BankAccountWithdraw>().ToList();
        //}

        ///// <summary>
        ///// Method to get BankAccount withdraw by BankAccount account id.
        ///// </summary>
        ///// <param name="BankAccountAccountID"></param>
        ///// <returns></returns>
        //public List<BankAccountWithdraw> GetBankAccountWithdrawByBankAccountAccountID(int BankAccountAccountID)
        //{
        //    string filter = string.Format("{0}={1}", MTBFConstants.DataField.BankAccount_ACCOUNT_ID, BankAccountAccountID);
        //    return base.DataAccessManager.GetFilteredData<BankAccountWithdraw>(filter).Cast<BankAccountWithdraw>().ToList();
        //}

        //#endregion



        public List<BankDeposit> GetAllBankDepositByDate(string fromDate, string toDate)
        {
            string filter = string.Format("{0} between '{1}' and '{2}' order by DepositDate", "DepositDate", fromDate, toDate);
            List<BankDeposit> lstBankDeposit = base.DataAccessManager.GetFilteredData<BankDeposit>(filter).Cast<BankDeposit>().ToList();
            return lstBankDeposit;
        }


        public List<BankWithdraw> GetAllBankWithdrawByDate(string fromDate, string toDate)
        {
            string filter = string.Format("{0} between '{1}' and '{2}' order by WithdrawDate", "WithdrawDate", fromDate, toDate);
            List<BankWithdraw> lstBankWithdraw = base.DataAccessManager.GetFilteredData<BankWithdraw>(filter).Cast<BankWithdraw>().ToList();
            return lstBankWithdraw;
        }

        public List<BankWithdraw> GetAllBankWithdraw()
        {
            return base.DataAccessManager.GetAll<BankWithdraw>().Cast<BankWithdraw>().ToList();
        }


        public List<BankDeposit> GetAllBankDeposit()
        {
            return base.DataAccessManager.GetAll<BankDeposit>().Cast<BankDeposit>().ToList();
        }

        public BankDeposit GetBankDebositByID(int bankDepositID)
        {
            string filter = string.Format("{0}={1}", "BankDepositID", bankDepositID);
            return base.DataAccessManager.GetFilteredData<BankDeposit>(filter).Cast<BankDeposit>().ToList().FirstOrDefault();
        }

        public BankWithdraw GetBankWithdrawByID(int bankWithdrawID)
        {
            string filter = string.Format("{0}={1}", "WithdrawID", bankWithdrawID);
            return base.DataAccessManager.GetFilteredData<BankWithdraw>(filter).Cast<BankWithdraw>().ToList().FirstOrDefault();
        }

        public List<BankWithdraw> GetFilteredBankWithdraw(string filter)
        {
            return base.DataAccessManager.GetFilteredData<BankWithdraw>(filter).Cast<BankWithdraw>().ToList();
        }

        public List<BankDeposit> GetFilteredBankDeposit(string filter)
        {
            return base.DataAccessManager.GetFilteredData<BankDeposit>(filter).Cast<BankDeposit>().ToList();
        }

        public int UpdateBankDeposit(BankDeposit bankDeposit)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            try
            {
                this.DataAccessManager.BeginTransaction();

                this.DataAccessManager.Update<BankDeposit>(bankDeposit);
                DeleteAllDepositJournal(bankDeposit);
                InsertJournalInformationForBankDeposit(bankDeposit);

                this.DataAccessManager.CommitTransaction();
            }
            catch (Exception exp)
            {
                this.DataAccessManager.Rollback();
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            finally
            {
                this.DataAccessManager.EndTransaction();
            }
            return result;
        }

        public int UpdateBankWithdraw(BankWithdraw bankWithdraw)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            try
            {
                this.DataAccessManager.BeginTransaction();

                this.DataAccessManager.Update<BankWithdraw>(bankWithdraw);

                DeleteAllWithdrawJournal(bankWithdraw);
                ProcessJournalForBakWithdraw(bankWithdraw);
                this.DataAccessManager.CommitTransaction();
            }
            catch (Exception exp)
            {
                this.DataAccessManager.Rollback();
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            finally
            {
                this.DataAccessManager.EndTransaction();
            }
            return result;
        }

        private void DeleteAllWithdrawJournal(BankWithdraw bankWithdraw)
        {
            string filter = string.Format("{0}={1}", "BankWithdrawID", bankWithdraw.WithdrawID);
            List<Journal> lstJournal = new List<Journal>();
            lstJournal = base.DataAccessManager.GetFilteredData<Journal>(filter).Cast<Journal>().ToList();
            foreach (Journal journal in lstJournal)
            {
                base.DataAccessManager.Delete<Journal>(journal.JournalID);
            }
        }

        private void DeleteAllDepositJournal(BankDeposit bankDeposit)
        {
            string filter = string.Format("{0}={1}", "BankDepositID", bankDeposit.BankDepositID);
            List<Journal> lstJournal = new List<Journal>();
            lstJournal = base.DataAccessManager.GetFilteredData<Journal>(filter).Cast<Journal>().ToList();
            foreach (Journal journal in lstJournal)
            {
                base.DataAccessManager.Delete<Journal>(journal.JournalID);
            }
        }
    }
}
