using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Enums;

namespace IFMS.BLL
{
    public class JournalAccountsManager : BllBase
    {
        public int InsertJournalAccount(JournalAccountsInformation journalAccount)
        {
            try
            {
                int result = base.DataAccessManager.Insert<JournalAccountsInformation>(journalAccount);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public int UpdateJournalAccount(JournalAccountsInformation journalAccount)
        {
            try
            {
                int result = base.DataAccessManager.Update<JournalAccountsInformation>(journalAccount);
                return result;
            }
            catch
            {
                throw;
            }
        }



        public System.Collections.IList GetAllJournalAccount()
        {
            return base.DataAccessManager.GetAll<JournalAccountsInformation>();
        }

        public int GetJournalReferenceID()
        {
            int referenceID = 0;
            referenceID = Convert.ToInt32(base.DataAccessManager.ExecuteScalar("Select isnull(max(ReferenceNo),0)+1  from Journal "));
            return referenceID;
        }

        public int InsertJournal(Journal journal)
        {
            int result = base.DataAccessManager.Insert<Journal>(journal);
            return result;
        }

        public int SaveJournal(List<Journal> lstJournal, List<JournalVoucher> lstJurnalVoucher, int referenceNo)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                base.DataAccessManager.BeginTransaction();

                List<Journal> lstExistingJournal = new List<Journal>();
                List<JournalVoucher> lstExistingJournalVoucher = new List<JournalVoucher>();
                string filter = string.Format("{0}={1}", "ReferenceNo", referenceNo);

                lstExistingJournalVoucher = base.DataAccessManager.GetFilteredData<JournalVoucher>(filter).Cast<JournalVoucher>().ToList();

                lstExistingJournal = base.DataAccessManager.GetFilteredData<Journal>(filter).Cast<Journal>().ToList();

                foreach (Journal journal in lstExistingJournal)
                {
                    base.DataAccessManager.Delete<Journal>(journal.JournalID);
                }

                foreach (JournalVoucher journalVoucher in lstExistingJournalVoucher)
                {
                    base.DataAccessManager.Delete<JournalVoucher>(journalVoucher.JournalVoucherID);
                }


                foreach (Journal journal in lstJournal)
                {
                    base.DataAccessManager.Insert<Journal>(journal);
                }

                foreach (JournalVoucher journalVoucher in lstJurnalVoucher)
                {
                    journalVoucher.JournalVoucherID = 0;
                    base.DataAccessManager.Insert<JournalVoucher>(journalVoucher);
                }              

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
    }
}
