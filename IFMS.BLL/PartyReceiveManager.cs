using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;
using IMFS.Common.Utility;
using NybSys.MTBF.Utility.Constants;
using NybSys.MTBF.Utility.Enums;

namespace IFMS.BLL
{
    public class PartyReceiveManager : BllBase
    {

        public int InsertPartyReceive(PartyReceive partyReceive)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            int receiveID = 0;
            try
            {
                base.DataAccessManager.BeginTransaction();

                receiveID = base.DataAccessManager.Insert<PartyReceive>(partyReceive);
                partyReceive.CashReceiveID = receiveID;

                InsertJournalInformationForCashReceive(partyReceive);
                if (partyReceive.Discount > 0)
                {
                    InsertJournalInformationForDiscount(partyReceive);
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


        public int InsertJournalInformationForCashReceive(PartyReceive partyReceive)
        {
            int result = 0;
            int referenceID = 1;
            decimal amount = partyReceive.Amount;
            referenceID = new JournalManager().GetJournalReferenceID();
            ChildAccount childAccount = new ChildAccountManager().GetChildAccountByPartyID(partyReceive.PartyID);

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

                journal.JAccountID = 0;
                journal.Amount = amount;
                journal.ReferenceNo = referenceID;
                journal.PartyReceiveID = partyReceive.PartyID;
                journal.JournalType = (int)MTBFEnums.JournalType.Production;
                journal.BranchID = MTBFConstants.AppConstants.BranchID;
                journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                base.DataAccessManager.Insert<Journal>(journal);
            }

            return result;
        }


        public int InsertJournalInformationForDiscount(PartyReceive partyReceive)
        {
            int result = 0;
            int referenceID = 1;
            decimal amount = partyReceive.Discount;
            referenceID = new JournalManager().GetJournalReferenceID();
            ChildAccount childAccount = new ChildAccountManager().GetChildAccountByPartyID(partyReceive.PartyID);

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
                journal.PartyReceiveID = partyReceive.PartyID;
                journal.JournalType = (int)MTBFEnums.JournalType.Production;
                journal.BranchID = MTBFConstants.AppConstants.BranchID;
                journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                base.DataAccessManager.Insert<Journal>(journal);
            }

            return result;
        }


        public int UpdatePartyReceive(PartyReceive partyReceive)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            List<Journal> lstJournal = new List<Journal>();
            try
            {
                base.DataAccessManager.BeginTransaction();

                base.DataAccessManager.Update<PartyReceive>(partyReceive);
                lstJournal = GetAllJournalByPartyReceiveID(partyReceive);
                DeleteAllJournal(lstJournal);

                InsertJournalInformationForCashReceive(partyReceive);
                if (partyReceive.Discount > 0)
                {
                    InsertJournalInformationForDiscount(partyReceive);
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


        private List<Journal> GetAllJournalByPartyReceiveID(PartyReceive cashReceive)
        {
            string filter = string.Format("{0}={1}", "PartyReceiveID", cashReceive.CashReceiveID);
            return base.DataAccessManager.GetFilteredData<Journal>(filter).Cast<Journal>().ToList();

        }



        public PartyReceive GetPartyReceiveByID(int _PartyReceiveID)
        {
            PartyReceive PartyReceive = base.DataAccessManager.GetByID<PartyReceive>(_PartyReceiveID, "CashReceiveID");
            return PartyReceive;
        }

        public List<PartyReceive> GetAllPartyReceive()
        {
            return base.DataAccessManager.GetAll<PartyReceive>().Cast<PartyReceive>().ToList();
        }


        public List<PartyReceive> GetAllPartyReceiveInformationByDateRange(string fromDate, string toDate)
        {
            string filter = string.Format("{0} between '{1}' and '{2}'", "ReceiveDate", fromDate, toDate);
            return base.DataAccessManager.GetFilteredData<PartyReceive>(filter).Cast<PartyReceive>().ToList();
        }

        public decimal GetPartyDueAmountByPartyID(int partyID, int branchID, int organizationID)
        {
            string filter = string.Format(IFMSConstant.QueryConstants.PartyDueAmountByPartyID, partyID, branchID, organizationID);
            return Convert.ToDecimal(base.DataAccessManager.ExecuteScalar(filter));
        }

        public List<PartyReceive> GetAllPartyReceiveInformationByDate(string fromDate, string toDate)
        {
            string filter = string.Format(IFMSConstant.QueryConstants.GET_ALL_DAILY_CASH_RECEIVE_INFORMATION_DATE, fromDate, toDate);
            return base.DataAccessManager.ExecuteSQL<PartyReceive>(filter).Cast<PartyReceive>().ToList();

        }


        public List<PartyReceive> GetTotalPartyReceiveInformationByDate(string fromDate, string toDate, int branchID, int organizationID)
        {
            string filter = string.Format("{0} between '{1}' and '{2}' and {3}={4} and {5}={6}", "ReceiveDate", fromDate, toDate, "BranchID", branchID, "OrganizationID", organizationID);
            return base.DataAccessManager.GetFilteredData<PartyReceive>(filter).Cast<PartyReceive>().ToList();

        }



        public List<PartyReceive> GetFilteredPartyReceive(string filter)
        {
            return base.DataAccessManager.GetFilteredData<PartyReceive>(filter).Cast<PartyReceive>().ToList();
        }
    }
}
