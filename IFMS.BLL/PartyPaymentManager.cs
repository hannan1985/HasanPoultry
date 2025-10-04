using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Enums;
using IMFS.Common.Utility;
using NybSys.MTBF.Utility.Constants;

namespace IFMS.BLL
{
    public class PartyPaymentManager : BllBase
    {

        public int InsertPartyPayment(PartyPayment partyPayment)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            int paymentID = 0;
            try
            {
                base.DataAccessManager.BeginTransaction();

                paymentID = base.DataAccessManager.Insert<PartyPayment>(partyPayment);

                partyPayment.PaymentID = paymentID;

                InsertJournalInformationForCashPayment(partyPayment);

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


        public int InsertJournalInformationForCashPayment(PartyPayment payment)
        {
            int result = 0;
            int referenceID = 1;
            decimal amount = payment.Amount;

            ChildAccount childAccount = new ChildAccountManager().GetChildAccountByMaterialSupplierID(payment.SupplierID);
            referenceID = new JournalManager().GetJournalReferenceID();

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

                journal.JAccountID = 0;
                journal.Amount = amount;
                journal.ReferenceNo = referenceID;
                journal.PartyPaymentID = payment.PaymentID;
                journal.JournalType = (int)MTBFEnums.JournalType.Production;
                journal.BranchID = MTBFConstants.AppConstants.BranchID;
                journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                base.DataAccessManager.Insert<Journal>(journal);
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


        private List<Journal> GetAllJournalByPaymentID(PartyPayment payment)
        {
            string filter = string.Format("{0}={1}", "PartyPaymentID", payment.PaymentID);
            return base.DataAccessManager.GetFilteredData<Journal>(filter).Cast<Journal>().ToList();

        }


        public int UpdatePartyPayment(PartyPayment partyPayment)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            List<Journal> lstJournal = new List<Journal>();
            try
            {
                base.DataAccessManager.BeginTransaction();

                base.DataAccessManager.Update<PartyPayment>(partyPayment);

                lstJournal = GetAllJournalByPaymentID(partyPayment);
                DeleteAllJournal(lstJournal);

                InsertJournalInformationForCashPayment(partyPayment);

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


        public PartyPayment GetPartyPaymentByID(int _PartyPaymentID)
        {
            PartyPayment PartyPayment = base.DataAccessManager.GetByID<PartyPayment>(_PartyPaymentID, "PaymentID");
            return PartyPayment;
        }

        public List<PartyPayment> GetAllPartyPayment()
        {
            return base.DataAccessManager.GetAll<PartyPayment>().Cast<PartyPayment>().ToList();
        }


        public List<PartyPayment> GetFilteredPartyPayment(string filter)
        {
            return base.DataAccessManager.GetFilteredData<PartyPayment>(filter).Cast<PartyPayment>().ToList();
        }



        public decimal GetDueAmountByMaterialSupplierID(int supplierID)
        {
            string filter = @"Select ISNULL ( DueAmount -PaidAmount,0)  as Amount from (
                            Select SupplierID , SUM(DueAmount) as DueAmount, SUM(PaidAmount) as PaidAmount from (
                            Select SupplierID , Total -PaidAmount  As DueAmount,'0' as PaidAmount from MaterialsReceive 
                            union
                            Select SupplierID, '0' as DueAmount, Amount as PaidAmount from PartyPayment)tbl  where SupplierID ={0} group by SupplierID
                            )tbl2";
            filter = string.Format(filter, supplierID);

            return Convert.ToDecimal(base.DataAccessManager.ExecuteScalar(filter));
        }
    }
}
