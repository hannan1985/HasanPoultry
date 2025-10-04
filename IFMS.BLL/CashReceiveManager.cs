using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;
using IMFS.Common.Utility;
using IMFS.Common.View;
using NybSys.MTBF.Utility.Constants;
using NybSys.MTBF.Utility.Enums;

namespace IFMS.BLL
{
    public class CashReceiveManager : BllBase
    {
        public int InsertCashReceive(CashReceive cashReceive)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            int receiveID = 0;
            try
            {
                base.DataAccessManager.BeginTransaction();
                cashReceive.Status = (int)MTBFEnums.CashReceieStatus.Received;
                cashReceive.CreatedBy = MTBFConstants.AppConstants.LoggedinUserID;
                cashReceive.CreatedDate = DateTime.Now;
                cashReceive.UpdatedDate = Convert.ToDateTime(MTBFConstants.DefauldDate);
                receiveID = base.DataAccessManager.Insert<CashReceive>(cashReceive);
                cashReceive.CashReceiveID = receiveID;

                InsertJournalInformationForCashReceive(cashReceive);
                if (cashReceive.Discount > 0)
                {
                    InsertJournalInformationForDiscount(cashReceive);
                }

                base.DataAccessManager.CommitTransaction();
            }
            catch
            {

                base.DataAccessManager.Rollback();
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            finally
            {
                base.DataAccessManager.EndTransaction();
            }

            return result;
        }

        public int InsertJournalInformationForCashReceive(CashReceive cashRecive)
        {
            int result = 0;
            int referenceID = 1;
            decimal amount = cashRecive.Amount;
            referenceID = new JournalManager().GetJournalReferenceID();

            string filter = string.Format("{0}='{1}' and {2}={3}", "ReferenceType", (int)MTBFEnums.ChildAccountType.Customer, "ReferenceID", cashRecive.CustomerID);
            ChildAccount customerChildAccount = base.DataAccessManager.GetFilteredData<ChildAccount>(filter).Cast<ChildAccount>().ToList().FirstOrDefault();

            filter = string.Format("{0}='{1}' and {2}={3}", "ReferenceType", (int)MTBFEnums.ChildAccountType.Bank, "ReferenceID", cashRecive.BankAccountID);
            ChildAccount bankChildAccount = base.DataAccessManager.GetFilteredData<ChildAccount>(filter).Cast<ChildAccount>().ToList().FirstOrDefault();

            if (cashRecive.CashAmount > 0)
            {
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
                        journal.AccountID = customerChildAccount.AccountID;
                        journal.ChildAccountID = customerChildAccount.ChildAccountID;
                    }

                    journal.JAccountID = 0;
                    journal.Amount = cashRecive.CashAmount;
                    journal.ReferenceNo = referenceID;
                    journal.CashReceiveID = cashRecive.CashReceiveID;
                    journal.JournalType = (int)MTBFEnums.JournalType.Inventory;
                    journal.BranchID = MTBFConstants.AppConstants.BranchID;
                    journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                    base.DataAccessManager.Insert<Journal>(journal);
                }
            }

            if (cashRecive.BankAmount > 0)
            {
                for (int i = 0; i <= 1; i++)
                {
                    IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();

                    if (i == 0)
                    {
                        journal.DrCrIndecator = "Dr";
                        journal.AccountID = bankChildAccount.AccountID;
                        journal.ChildAccountID = bankChildAccount.ChildAccountID;
                    }
                    else
                    {
                        journal.DrCrIndecator = "Cr";
                        journal.AccountID = customerChildAccount.AccountID;
                        journal.ChildAccountID = customerChildAccount.ChildAccountID;
                    }
                    journal.Description = "Receive from Customer";
                    journal.JAccountID = 0;
                    journal.Amount = cashRecive.BankAmount;
                    journal.ReferenceNo = referenceID;
                    journal.CashReceiveID = cashRecive.CashReceiveID;
                    journal.JournalType = (int)MTBFEnums.JournalType.Inventory;
                    journal.BranchID = MTBFConstants.AppConstants.BranchID;
                    journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                    base.DataAccessManager.Insert<Journal>(journal);
                }
            }

            return result;
        }

        public int InsertJournalInformationForDiscount(CashReceive cashRecive)
        {
            int result = 0;
            int referenceID = 1;
            decimal amount = cashRecive.Discount;
            referenceID = new JournalManager().GetJournalReferenceID();

            string filter = string.Format("{0}='{1}' and {2}={3}", "ReferenceType", (int)MTBFEnums.ChildAccountType.Customer, "ReferenceID", cashRecive.CustomerID);
            ChildAccount childAccount = base.DataAccessManager.GetFilteredData<ChildAccount>(filter).Cast<ChildAccount>().ToList().FirstOrDefault();


            for (int i = 0; i <= 1; i++)
            {
                IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();

                if (i == 0)
                {
                    journal.DrCrIndecator = "Dr";
                    journal.AccountID = IFMSConstant.DiscountGiven;

                }
                else
                {
                    journal.DrCrIndecator = "Cr";
                    journal.AccountID = childAccount.AccountID;
                    journal.ChildAccountID = childAccount.ChildAccountID;
                }

                journal.JAccountID = 0;
                journal.Amount = amount;
                journal.ReferenceNo = referenceID;
                journal.CashReceiveID = cashRecive.CashReceiveID;
                journal.JournalType = (int)MTBFEnums.JournalType.Inventory;
                journal.BranchID = MTBFConstants.AppConstants.BranchID;
                journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                base.DataAccessManager.Insert<Journal>(journal);
            }

            return result;
        }

        public int UpdateCashReceive(CashReceive cashReceive)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            List<Journal> lstJournal = new List<Journal>();
            try
            {
                base.DataAccessManager.BeginTransaction();

                cashReceive.Status = (int)MTBFEnums.CashReceieStatus.Received;
                cashReceive.UpdatedBy = MTBFConstants.AppConstants.LoggedinUserID;
                cashReceive.UpdatedDate = DateTime.Now;
                base.DataAccessManager.Update<CashReceive>(cashReceive);

                lstJournal = GetAllJournalByCashReceiveID(cashReceive);

                DeleteAllJournal(lstJournal);

                InsertJournalInformationForCashReceive(cashReceive);
                if (cashReceive.Discount > 0)
                {
                    InsertJournalInformationForDiscount(cashReceive);
                }

                base.DataAccessManager.CommitTransaction();
            }
            catch
            {

                base.DataAccessManager.Rollback();
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            finally
            {
                base.DataAccessManager.EndTransaction();
            }

            return result;
        }

        private void DeleteAllJournal(List<Journal> lstJournal)
        {
            foreach (Journal journal in lstJournal)
            {
                base.DataAccessManager.Delete<Journal>(journal.JournalID);
            }
        }


        private List<Journal> GetAllJournalByCashReceiveID(CashReceive cashReceive)
        {
            string filter = string.Format("{0}={1}", "CashReceiveID", cashReceive.CashReceiveID);
            return base.DataAccessManager.GetFilteredData<Journal>(filter).Cast<Journal>().ToList();

        }


        public CashReceive GetCashReceiveByID(int _CashReceiveID)
        {
            CashReceive cashReceive = base.DataAccessManager.GetByID<CashReceive>(_CashReceiveID, "CashReceiveID");
            return cashReceive;
        }

        public List<CashReceive> GetAllCashReceive()
        {
            return base.DataAccessManager.GetAll<CashReceive>().Cast<CashReceive>().ToList();
        }


        public List<CashReceive> GetAllCashReceiveInformationByDateRange(string fromDate, string toDate)
        {
            string filter = string.Format("{0} between '{1}' and '{2}'", "ReceiveDate", fromDate, toDate);
            return base.DataAccessManager.GetFilteredData<CashReceive>(filter).Cast<CashReceive>().ToList();
        }

        public decimal GetCustomerDueAmountByCustomerID(int customerId, int branchID, int organizationID)
        {
            string filter = string.Format(IFMSConstant.QueryConstants.CustomerDueAmountByCustomerID, customerId);
            return Convert.ToDecimal(base.DataAccessManager.ExecuteScalar(filter));
        }

        public List<CashReceive> GetAllCashReceiveInformationByDate(string fromDate, string toDate)
        {
            string filter = string.Format(IFMSConstant.QueryConstants.GET_ALL_DAILY_CASH_RECEIVE_INFORMATION_DATE, fromDate, toDate);
            return base.DataAccessManager.ExecuteSQL<CashReceive>(filter).Cast<CashReceive>().ToList();

        }


        public List<CashReceiveInformation> GetAllCashReceiveInformationByDate(string _fromDate, string _toDate, int branchID, int organizationID)
        {
            string filter = string.Format(IFMSConstant.QueryConstants.GET_TOTAL_CASH_RECEIVE_INFORMATION__BY_DATE, _fromDate, _toDate, branchID, organizationID);
            return base.DataAccessManager.ExecuteSQL<CashReceiveInformation>(filter).Cast<CashReceiveInformation>().ToList();

        }

        public List<CashReceive> GetFilteredCashReceive(string filter)
        {
            return base.DataAccessManager.GetFilteredData<CashReceive>(filter).Cast<CashReceive>().ToList();
        }



        public int InsertOtherReceive(OtherReceive otherReceive)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                base.DataAccessManager.BeginTransaction();
                otherReceive.CreatedBy = MTBFConstants.AppConstants.LoggedinUserID;
                otherReceive.CreatedDate = DateTime.Now;
                otherReceive.UpdatedDate = Convert.ToDateTime(MTBFConstants.DefauldDate);
                base.DataAccessManager.Insert<OtherReceive>(otherReceive);

                InsertJournalInformationForOtherReceive(otherReceive);

                base.DataAccessManager.CommitTransaction();
            }
            catch (Exception)
            {
                base.DataAccessManager.Rollback();
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            finally
            {
                base.DataAccessManager.EndTransaction();
            }
            return result;
        }

        public int InsertJournalInformationForOtherReceive(OtherReceive otherReceive)
        {
            int result = 0;
            int referenceID = 1;
            decimal amount = otherReceive.ReceiveAmount;
            referenceID = new JournalManager().GetJournalReferenceID();

            string filter = string.Format("{0}='{1}' and {2}={3}", "ReferenceType", (int)MTBFEnums.ChildAccountType.OtherParty, "ReferenceID", otherReceive.OtherPartyID);
            ChildAccount customerChildAccount = base.DataAccessManager.GetFilteredData<ChildAccount>(filter).Cast<ChildAccount>().ToList().FirstOrDefault();


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
                    journal.AccountID = customerChildAccount.AccountID;
                    journal.ChildAccountID = customerChildAccount.ChildAccountID;
                }

                journal.JAccountID = 0;
                journal.Amount = otherReceive.ReceiveAmount;
                journal.ReferenceNo = referenceID;
                journal.OtherReceiveID = otherReceive.ReceiveID;
                journal.JournalType = (int)MTBFEnums.JournalType.Inventory;
                journal.BranchID = MTBFConstants.AppConstants.BranchID;
                journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                base.DataAccessManager.Insert<Journal>(journal);
            }





            return result;
        }



        public int UpdateOtherReceive(OtherReceive otherReceive)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                base.DataAccessManager.BeginTransaction();
                otherReceive.UpdatedBy = MTBFConstants.AppConstants.LoggedinUserID;
                otherReceive.UpdatedDate = DateTime.Now;

                base.DataAccessManager.Update<OtherReceive>(otherReceive);

                List<Journal> lstJournal = new List<Journal>();
                string filter = string.Format("{0}={1}", "OtherReceiveID", otherReceive.ReceiveID);
                lstJournal = base.DataAccessManager.GetFilteredData<Journal>(filter).Cast<Journal>().ToList();
                DeleteAllJournal(lstJournal);
                InsertJournalInformationForOtherReceive(otherReceive);
                base.DataAccessManager.CommitTransaction();

            }
            catch (Exception)
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
                base.DataAccessManager.Rollback();
            }
            finally
            {
                base.DataAccessManager.EndTransaction();
            }
            return result;
        }

        public List<OtherReceive> GetAllOtherReceive()
        {
            return base.DataAccessManager.GetAll<OtherReceive>().Cast<OtherReceive>().ToList();
        }

        public List<OtherReceive> GetFilteredOtherReceive(string filter)
        {
            return base.DataAccessManager.GetFilteredData<OtherReceive>(filter).Cast<OtherReceive>().ToList();
        }


        public List<CashBookReceive> GetAllDailyCashReceiveInformationByDate(string fromDate, string toDate, int branchID)
        {
            string filter = @"Select * from (
                       Select BranchID, ReceiveDate, 'Cash Receive' Description, Amount Amount from CashReceive 
                            
                             union all
                      	Select BranchID, SalesDate ReceiveDate, 'Receive from Sales' Description, ReceiveAmount Amount from CashSales where ReceiveAmount >0
                             union all
                        Select BranchID, WithdrawDate ReceiveDate, 'Bank Withdraw' Description, WithdrawAmount Amount from BankWithdraw 
                        union all
                        Select BranchID,  ReceiveDate, 'Other Receive' Description, ReceiveAmount Amount from OtherReceive 

                        )tbl Where tbl.ReceiveDate Between '{0}' and '{1}' and BranchID={2}";

            filter = string.Format(filter, fromDate, toDate, branchID);

            return base.DataAccessManager.ExecuteSQL<CashBookReceive>(filter).Cast<CashBookReceive>().ToList();

        }


        public List<CashBookPayment> GetAllDailyCashPaymentInformationByDate(string fromDate, string toDate, int branchID)
        {
            string filter = @"Select * from (
                        Select e.BranchID, e.ExpenseDate, et.ExpenseTypeName Description, e.Amount Amount from Expense e 
                        left outer join ExpenseType et on e.ExpenseType=et.ExpenseTypeID 
                        union all
                        Select BranchID, DepositDate ExpenseDate, 'Bank Deposit' Description ,DepositAmount Amount from BankDeposit
                        union all
                        Select BranchID, PaymentDate ExpenseDate, 'Payment' Description, Amount EAmount  from Payment 
                        union all
                        Select BranchID, ReturnDate ExpenseDate, 'Paid for Sales Return' Description , PaidAmount Amount from SalesReturn
                        union all
                        Select BranchID, PurchaseDate ExpenseDate, 'Paid to Supplier for purchase' Description , PaidAmount Amount from PurchaseOrder where PaidAmount>0
                        union all
                        Select BranchID, PaymentDate ExpenseDate, 'Paid to other party as payment' Description , Amount  from OtherPayment 
                        )tbl where tbl.ExpenseDate Between '{0}' and '{1}' and BranchID={2}";

            filter = string.Format(filter, fromDate, toDate, branchID);

            return base.DataAccessManager.ExecuteSQL<CashBookPayment>(filter).Cast<CashBookPayment>().ToList();

        }

        public decimal GetPreviousBalanceByDate(string filterDate, int branchID)
        {
            string filter = @"
                    Select sum(IAmount)-sum(EAmount) from (
                    Select 'Expense' Description,'0' IAmount, sum(Amount) EAmount from (
                    Select  sum(e.Amount) Amount from Expense e 
                    left outer join ExpenseType et on e.ExpenseType=et.ExpenseTypeID  where e.ExpenseDate < '{0}' and e.BranchID={1}

                    union
                    Select  sum(DepositAmount) Amount from BankDeposit where DepositDate < '{0}' and BranchID={1}
                    union
                    Select  sum( Amount) EAmount  from Payment  where PaymentDate < '{0}' and BranchID={1}
                    union all
                    Select  isnull(sum(Amount),0) EAmount  from OtherPayment  where PaymentDate < '{0}' and BranchID={1}
                    union
                    Select sum( PaidAmount) EAmount from PurchaseOrder where PaidAmount>0
                    and PurchaseDate < '{0}' and BranchID={1} and Status <>3
                    union
                    Select  sum( PaidAmount) Amount from SalesReturn where ReturnDate < '{0}' and BranchID={1}
                    )tbl 
                    union
                    Select 'Income' Description, sum (Amount) IAmount,'0' EAmount from (
                    Select  sum( Amount) Amount from CashReceive where ReceiveDate  < '{0}' and BranchID={1}
                    union
                    Select  sum( ReceiveAmount) Amount from OtherReceive where ReceiveDate  < '{0}' and BranchID={1}
                    union
                    Select  sum(ReceiveAmount) Amount from CashSales where SalesDate  < '{0}' and BranchID={1}
                    union
                    Select sum( WithdrawAmount) Amount from BankWithdraw where WithdrawDate  < '{0}' and BranchID={1}
                    )tbl 
                    )tbl1 ";

            filter = string.Format(filter, filterDate, branchID);

            return Convert.ToDecimal(base.DataAccessManager.ExecuteScalar(filter));

        }


        public MoneyReceipt GetMoneyReceiveData(int cashReceiveID, int branchID)
        {
            string sql = @"Select cr.CashReceiveID, isnull(cr.ReferenceNo,'') MemoNumber, cr.ReceiveDate, cr.Amount, c.CustomerName, cr.BillRefNumber ,isnull( cr.BankReference,'')ChequeNo from CashReceive cr
                            left outer join Customer c on cr.CustomerID=c.CustomerID
                            Where cr.CashReceiveID=" + cashReceiveID + " and cr.BranchID=" + branchID;

            return base.DataAccessManager.ExecuteSQL<MoneyReceipt>(sql).Cast<MoneyReceipt>().FirstOrDefault();
        }

        public int CancelCashReceiveInformation(int _cashReceiveID)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                CashReceive cashReceive = new CashReceive();
                cashReceive = GetCashReceiveByID(_cashReceiveID);
                if (cashReceive != null)
                {
                    cashReceive.Status = (int)MTBFEnums.CashReceieStatus.Cancel;
                    cashReceive.Amount = 0;
                    base.DataAccessManager.Update<CashReceive>(cashReceive);
                }
            }
            catch (Exception)
            {

                result = (int)MTBFEnums.ReturnResult.FAILED;
            }

            return result;
        }



        public List<CashBookReceive> GetAllCashBookJournal(int cashAccountID, string fromDate, string toDate, int branchID)
        {
            //            string sql = @"Select * from (
            //                        Select ReferenceNo, DrCrIndecator, BranchID,  JournalDate, Description, Amount Debit, 0 Credit from Journal where AccountID=1  and DrCrIndecator='Dr'  
            //           
            //                        union all
            //                        Select  ReferenceNo, DrCrIndecator, BranchID,JournalDate, Description, 0 Debit, Amount Credit from Journal where AccountID=1 and DrCrIndecator='Cr'                      
            //                        union all
            //                        Select  ReferenceNo, DrCrIndecator, BranchID,JournalDate, Description, 0 Debit, Amount Credit from Journal where AccountID=11 and DrCrIndecator='Cr'                   
            //
            //                          
            //                        )tbl
            //                        where tbl.JournalDate between '" + fromDate + "' and '" + toDate + "' and tbl.BranchID=" + branchID;

            string sql = @"Select * from (
                        Select 0 Indicator, AccountID, ReferenceNo, DrCrIndecator, BranchID,JournalDate, Description, Amount Debit,  0 Credit from Journal where AccountID=1 and DrCrIndecator='Dr'             
                        union all
                        Select 0 Indicator, AccountID, ReferenceNo, DrCrIndecator, BranchID,JournalDate, Description, 0 Debit, Amount Credit from Journal where AccountID=1 and DrCrIndecator='Cr'   
                        union all
                        Select 1 Indicator, AccountID, ReferenceNo, DrCrIndecator, BranchID,JournalDate, Description, 0 Debit, Amount Credit 
                        from Journal where AccountID=11 and DrCrIndecator='Cr'   
                        and  ReferenceNo in(
                        Select ReferenceNo from Journal where ReferenceNo in (
                        Select  ReferenceNo
                        from Journal where AccountID=11 and DrCrIndecator='Cr') and  DrCrIndecator='Dr'
                        And AccountID=7))tbl
                        where tbl.JournalDate between '" + fromDate + "' and '" + toDate + "' and tbl.BranchID=" + branchID;

            return base.DataAccessManager.ExecuteSQL<CashBookReceive>(sql).Cast<CashBookReceive>().ToList();
        }

    }
}
