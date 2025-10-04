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
    public class PreviousDueManager : BllBase
    {

        #region "CustomerPreviousDue"


        public int InsertCustomerPreviousDue(CustomerPreviousDue customerPreviousDue)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            
            try
            {

                base.DataAccessManager.BeginTransaction();

                base.DataAccessManager.Insert<CustomerPreviousDue>(customerPreviousDue);
               
                InsertJournalInformationForCashReceive(customerPreviousDue);


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

        public int UpdateCustomerPreviousDue(CustomerPreviousDue customerPreviousDue)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {

                base.DataAccessManager.BeginTransaction();

                base.DataAccessManager.Update<CustomerPreviousDue>(customerPreviousDue);
              
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


        public int InsertJournalInformationForCashReceive(CustomerPreviousDue previousDue)
        {
            int result = 0;
            int referenceID = 1;
            decimal amount = previousDue.Amount; ;


            referenceID = new JournalManager().GetJournalReferenceID();

            for (int i = 0; i <= 1; i++)
            {
                IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();
            
                if (i == 0)
                {
                    journal.DrCrIndecator = "Dr";
                    journal.AccountID = (previousDue.DueType == (int)MTBFEnums.DueType.Customer) ? IFMSConstant.AccountReceivableID : IFMSConstant.Purchase;

                }
                else
                {
                    journal.DrCrIndecator = "Cr";
                    journal.AccountID = (previousDue.DueType == (int)MTBFEnums.DueType.Supplier) ? IFMSConstant.AccountPayableID : IFMSConstant.GoodsSalesAccountID;
                }

                journal.JAccountID = 0;
                journal.Amount = amount;
                journal.ReferenceNo = referenceID;            
                journal.BranchID = MTBFConstants.AppConstants.BranchID;
                journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                result = base.DataAccessManager.Insert<Journal>(journal);
            }

            return result;
        }



        public List<CustomerPreviousDue> GetAllCustomerPreviousDue()
        {
            return base.DataAccessManager.GetAll<CustomerPreviousDue>().Cast<CustomerPreviousDue>().ToList();
        }

        public CustomerPreviousDue GetCustomerPreviousDueByID(int customerPreviousDueID)
        {
            return base.DataAccessManager.GetByID<CustomerPreviousDue>(customerPreviousDueID, "PreviousDueID");
        }

        public CustomerPreviousDue GetCustomerPreviousDueByCustomerID(int customerID)
        {
            string filter = string.Format("{0}={1}", "CustomerID", customerID);
            return base.DataAccessManager.GetFilteredData<CustomerPreviousDue>(filter).Cast<CustomerPreviousDue>().ToList().FirstOrDefault();
        }

        public CustomerPreviousDue GetSupplierPreviousDueBySupplierID(int supplierID)
        {
            string filter = string.Format("{0}={1}", "SupplierID", supplierID);
            return base.DataAccessManager.GetFilteredData<CustomerPreviousDue>(filter).Cast<CustomerPreviousDue>().ToList().FirstOrDefault();
        }

        #endregion



        public List<CustomerPreviousDue> GetFilteredCustomerDue(string filter)
        {
            return base.DataAccessManager.GetFilteredData<CustomerPreviousDue>(filter).Cast<CustomerPreviousDue>().ToList();
        }
    }
}
