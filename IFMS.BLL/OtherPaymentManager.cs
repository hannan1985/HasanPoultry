using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Enums;
using NybSys.MTBF.Utility.Constants;
using IMFS.Common.Utility;

namespace IFMS.BLL
{
    public class OtherPaymentManager : BllBase
    {

        public int SaveOtherPayment(OtherPayment otherPayment)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                base.DataAccessManager.BeginTransaction();

                if (otherPayment.OtherPaymentID > 0)
                {
                    otherPayment.UpdatedBy = MTBFConstants.AppConstants.LoggedinUserID;
                    otherPayment.UpdatedDate = DateTime.Now;
                    base.DataAccessManager.Update<OtherPayment>(otherPayment);

                    List<Journal> lstJournal = new List<Journal>();
                    string filter = string.Format("{0}={1}", "OtherPaymentID", otherPayment.OtherPaymentID);
                    lstJournal = base.DataAccessManager.GetFilteredData<Journal>(filter).Cast<Journal>().ToList();
                    DeleteAllJournal(lstJournal);
                    InsertJournalInformationForOtherPayment(otherPayment);
                }
                else
                {
                    otherPayment.CreatedBy = MTBFConstants.AppConstants.LoggedinUserID;
                    otherPayment.CreatedDate = DateTime.Now;
                    otherPayment.UpdatedDate = Convert.ToDateTime(MTBFConstants.DefauldDate);
                    base.DataAccessManager.Insert<OtherPayment>(otherPayment);

                    InsertJournalInformationForOtherPayment(otherPayment);
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

        private void DeleteAllJournal(List<Journal> lstJournal)
        {
            foreach (Journal journal in lstJournal)
            {
                base.DataAccessManager.Delete<Journal>(journal.JournalID);
            }
        }

        public int InsertJournalInformationForOtherPayment(OtherPayment otherPayment)
        {
            int result = 0;
            int referenceID = 1;
            decimal amount = otherPayment.Amount;
            referenceID = new JournalManager().GetJournalReferenceID();

            string filter = string.Format("{0}='{1}' and {2}={3}", "ReferenceType", (int)MTBFEnums.ChildAccountType.OtherParty, "ReferenceID", otherPayment.OtherPartyID);
            ChildAccount customerChildAccount = base.DataAccessManager.GetFilteredData<ChildAccount>(filter).Cast<ChildAccount>().ToList().FirstOrDefault();


            for (int i = 0; i <= 1; i++)
            {
                IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();

                if (i == 0)
                {
                    journal.DrCrIndecator = "Dr";
                    journal.AccountID = customerChildAccount.AccountID;
                    journal.ChildAccountID = customerChildAccount.ChildAccountID;

                }
                else
                {
                    journal.DrCrIndecator = "Cr";
                    journal.AccountID = IFMSConstant.CashAccountID;
                }

                journal.JAccountID = 0;
                journal.Amount = otherPayment.Amount;
                journal.ReferenceNo = referenceID;
                journal.OtherPaymentID = otherPayment.OtherPaymentID;
                journal.JournalType = (int)MTBFEnums.JournalType.Inventory;
                journal.BranchID = MTBFConstants.AppConstants.BranchID;
                journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                base.DataAccessManager.Insert<Journal>(journal);
            }





            return result;
        }

        public int UpdateOtherPayment(OtherPayment OtherPayment)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                base.DataAccessManager.BeginTransaction();

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

        public List<OtherPayment> GetFilteredOtherPayment(string filter)
        {
            return base.DataAccessManager.GetFilteredData<OtherPayment>(filter).Cast<OtherPayment>().ToList();
        }

    }
}
