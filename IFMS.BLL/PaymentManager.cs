using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;
using IMFS.Common.Utility;
using IMFS.Common.View;
using NybSys.MTBF.Utility.Enums;
using NybSys.MTBF.Utility.Constants;

namespace IFMS.BLL
{
    public class PaymentManager : BllBase
    {
        public int InsertPayment(Payment payment)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            int paymentID = 0;
            try
            {
                base.DataAccessManager.BeginTransaction();
                payment.CreatedDate = DateTime.Now;
                payment.CreatedBy = MTBFConstants.AppConstants.LoggedinUserID;
                payment.UpdatedDate = Convert.ToDateTime(MTBFConstants.DefauldDate);
                paymentID = base.DataAccessManager.Insert<Payment>(payment);

                payment.PaymentID = paymentID;

                InsertJournalInformationForCashPayment(payment);

                if (payment.Discount > 0)
                {
                    InsertJournalInformationForDiscount(payment);
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

        public int InsertJournalInformationForDiscount(Payment payment)
        {
            int result = 0;
            int referenceID = 1;
            decimal amount = payment.Discount;
            referenceID = new JournalManager().GetJournalReferenceID();
            string filter = string.Format("{0}='{1}' and {2}={3}", "ReferenceType", (int)MTBFEnums.ChildAccountType.Supplier, "ReferenceID", payment.SupplierID);
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
                    journal.AccountID = IFMSConstant.DiscountEarn;
                }

                journal.JAccountID = 0;
                journal.Amount = amount;
                journal.ReferenceNo = referenceID;
                journal.PaymentID = payment.PaymentID;
                journal.JournalType = (int)MTBFEnums.JournalType.Inventory;
                journal.BranchID = MTBFConstants.AppConstants.BranchID;
                journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                base.DataAccessManager.Insert<Journal>(journal);
            }

            return result;
        }


        public int InsertJournalInformationForCashPayment(Payment payment)
        {
            int result = 0;
            int referenceID = 1;
            decimal amount = payment.Amount;


            referenceID = new JournalManager().GetJournalReferenceID();


            string filter = string.Format("{0}='{1}' and {2}={3}", "ReferenceType", (int)MTBFEnums.ChildAccountType.Supplier, "ReferenceID", payment.SupplierID);
            ChildAccount supplierChildAccount = base.DataAccessManager.GetFilteredData<ChildAccount>(filter).Cast<ChildAccount>().ToList().FirstOrDefault();

            filter = string.Format("{0}='{1}' and {2}={3}", "ReferenceType", (int)MTBFEnums.ChildAccountType.Bank, "ReferenceID", payment.BankAccountID);
            ChildAccount bankChildAccount = base.DataAccessManager.GetFilteredData<ChildAccount>(filter).Cast<ChildAccount>().ToList().FirstOrDefault();



            if (payment.CashAmount > 0)
            {
                for (int i = 0; i <= 1; i++)
                {
                    IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();


                    if (i == 0)
                    {
                        journal.DrCrIndecator = "Dr";
                        journal.AccountID = supplierChildAccount.AccountID;
                        journal.ChildAccountID = supplierChildAccount.ChildAccountID;
                    }
                    else
                    {
                        journal.DrCrIndecator = "Cr";
                        journal.AccountID = IFMSConstant.CashAccountID;

                    }
                    journal.Description = "Cash paid to supplier";

                    journal.JAccountID = 0;
                    journal.Amount = payment.CashAmount;
                    journal.ReferenceNo = referenceID;
                    journal.PaymentID = payment.PaymentID;
                    journal.JournalType = (int)MTBFEnums.JournalType.Inventory;
                    journal.BranchID = MTBFConstants.AppConstants.BranchID;
                    journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                    base.DataAccessManager.Insert<Journal>(journal);
                }
            }

            if (payment.BankAmount > 0)
            {
                for (int i = 0; i <= 1; i++)
                {
                    IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();


                    if (i == 0)
                    {
                        journal.DrCrIndecator = "Dr";
                        journal.AccountID = supplierChildAccount.AccountID;
                        journal.ChildAccountID = supplierChildAccount.ChildAccountID;
                    }
                    else
                    {
                        journal.DrCrIndecator = "Cr";
                        journal.AccountID = bankChildAccount.AccountID;
                        journal.ChildAccountID = bankChildAccount.ChildAccountID;
                    }
                    journal.Description = "Paid to Supplier";
                    journal.JAccountID = 0;
                    journal.Amount = payment.BankAmount;
                    journal.ReferenceNo = referenceID;
                    journal.PaymentID = payment.PaymentID;
                    journal.JournalType = (int)MTBFEnums.JournalType.Inventory;
                    journal.BranchID = MTBFConstants.AppConstants.BranchID;
                    journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                    base.DataAccessManager.Insert<Journal>(journal);
                }
            }


            return result;
        }

        public int UpdatePayment(Payment payment)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            List<Journal> lstJournal = new List<Journal>();
            try
            {
                base.DataAccessManager.BeginTransaction();

                base.DataAccessManager.Update<Payment>(payment);

                lstJournal = GetAllJournalByPaymentID(payment);
                DeleteAllJournal(lstJournal);

                InsertJournalInformationForCashPayment(payment);

                if (payment.Discount > 0)
                {
                    InsertJournalInformationForDiscount(payment);
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

        public Payment GetPaymentByID(int _PaymentID)
        {
            Payment payment = base.DataAccessManager.GetByID<Payment>(_PaymentID, "PaymentID");
            return payment;
        }

        private void DeleteAllJournal(List<Journal> lstJournal)
        {
            foreach (Journal journal in lstJournal)
            {
                base.DataAccessManager.Delete<Journal>(journal.JournalID);
            }
        }

        private List<Journal> GetAllJournalByPaymentID(Payment payment)
        {
            string filter = string.Format("{0}={1}", "PaymentID", payment.PaymentID);
            return base.DataAccessManager.GetFilteredData<Journal>(filter).Cast<Journal>().ToList();

        }

        public System.Collections.IList GetPaymentInformationByCompanyAndDate(int companyID, string fromDate, string toDate)
        {
            string filter = string.Empty;
            filter = companyID == -1 ? string.Format("{0} between '{1}' and '{2}'", "PaymentDate", fromDate, toDate) : string.Format("CompanyID={0} and {1} between '{2}' and '{3}'", companyID, "PaymentDate", fromDate, toDate);

            return base.DataAccessManager.GetFilteredData<Payment>(filter).Cast<Payment>().ToList();
        }


        public decimal GetDueAmountByCompanyAndSupplierID(int companyID, int SupplierID, int branchID, int organizationID)
        {
            string filter = string.Format(IFMSConstant.QueryConstants.SupplierDueAmountByCompanyAndSupplier, companyID, SupplierID, branchID, organizationID);
            return Convert.ToDecimal(base.DataAccessManager.ExecuteScalar(filter));
        }

        public System.Collections.IList GetPaymentInformationBySupplierID(int supplierID)
        {
            string filter = string.Format("{0}={1}", "SupplierID", supplierID);
            return base.DataAccessManager.GetFilteredData<Payment>(filter);
        }

        public List<CashPaymentInformation> GetTotalCashPaymentInformationByDate(string _fromDate, string _toDate, int branchID, int organizationID)
        {
            string filter = string.Format(IFMSConstant.QueryConstants.GET_TOTAL_CASH_PAYMENT_INFORMATION__BY_DATE, _fromDate, _toDate, branchID, organizationID);
            return base.DataAccessManager.ExecuteSQL<CashPaymentInformation>(filter).Cast<CashPaymentInformation>().ToList();

        }


        public List<Payment> GetAllPayment()
        {
            return base.DataAccessManager.GetAll<Payment>().Cast<Payment>().ToList();
        }

        public List<Payment> GetFilteredPaymentInformation(string filter)
        {
            return base.DataAccessManager.GetFilteredData<Payment>(filter).Cast<Payment>().ToList();
        }
    }
}
