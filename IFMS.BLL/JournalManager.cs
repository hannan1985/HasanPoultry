using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;

namespace IFMS.BLL
{
    public class JournalManager : BllBase
    {

        public int InsertAccountHead(JournalAccountsInformation accountHead)
        {
            try
            {
                JournalAccountsInformation journalAccount = new JournalAccountsInformation();
                int result = base.DataAccessManager.Insert<JournalAccountsInformation>(journalAccount);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public System.Collections.IList GetJournalByCashReceiveID(int _cashReceiveID)
        {
            string filter = string.Format("{0}={1}", "CashReceiveID", _cashReceiveID);
            return base.DataAccessManager.GetFilteredData<Journal>(filter);
        }

        public int UpdateJournal(Journal journal)
        {
            return base.DataAccessManager.Update<Journal>(journal);
        }


        public int InsertJournal(Journal journal)
        {
            return base.DataAccessManager.Insert<Journal>(journal);
        }

        public System.Collections.IList GetJournalByCashPaymentID(int paymentID)
        {
            string filter = string.Format("{0}='{1}'", "PaymentID", paymentID);
            return base.DataAccessManager.GetFilteredData<Journal>(filter);
        }

        public List<Journal> GetAllJournalBySalesID(int salesID)
        {
            string filter = string.Format("{0}={1}", "SalesID", salesID);
            return base.DataAccessManager.GetFilteredData<Journal>(filter).Cast<Journal>().ToList();
        }

        public void DeleteJournal(Journal journal)
        {
            base.DataAccessManager.Delete<Journal>(journal.JournalID);
        }

        public List<Journal> GetJournalByPartyReceiveID(int _PartyReceiveID)
        {
            string filter = string.Format("{0}='{1}'", "PartyReceiveID", _PartyReceiveID);
            return base.DataAccessManager.GetFilteredData<Journal>(filter).Cast<Journal>().ToList();
        }

        public List<Journal> GetJournalByPartyPaymentID(int partyPaymentID)
        {
            string filter = string.Format("{0}='{1}'", "PartyPaymentID", partyPaymentID);
            return base.DataAccessManager.GetFilteredData<Journal>(filter).Cast<Journal>().ToList();
        }

        public int GetJournalReferenceID()
        {
            int referenceID = 0;
            referenceID = Convert.ToInt32(base.DataAccessManager.ExecuteScalar("Select isnull(max(ReferenceNo),0)+1  from Journal "));
            return referenceID;
        }

        public List<Journal> GetAllJournalByBankAccountID(int bankAccountID)
        {
            string filter = string.Format("{0}='{1}'", "BankAccountID", bankAccountID);
            return base.DataAccessManager.GetFilteredData<Journal>(filter).Cast<Journal>().ToList();
        }

        public ParentAccount GetParentAccountByID(int accountID)
        {
            string filter = string.Format("{0}='{1}'", "AccountID", accountID);
            return base.DataAccessManager.GetFilteredData<ParentAccount>(filter).Cast<ParentAccount>().ToList().FirstOrDefault();
        }

        public List<Journal> GetAllJournalByDate(string fromDate, string toDate)
        {
            string filter = string.Format("{0} between '{1}' And '{2}'", "JournalDate", fromDate, toDate);
            return base.DataAccessManager.GetFilteredData<Journal>(filter).Cast<Journal>().ToList();
        }

        public List<Journal> GetFilteredJournal(string filter)
        {
            return base.DataAccessManager.GetFilteredData<Journal>(filter).Cast<Journal>().ToList();
        }
    }

}
