using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Enums;
using IMFS.Common.Utility;
using IMFS.Common.View;
using NybSys.MTBF.Utility.Constants;

namespace IFMS.BLL
{
    public class SalesReturnManager : BllBase
    {

        #region "SalesReturn"

        public int InsertSalesReturnInformation(SalesReturn salesReturn, List<SalesReturnDetail> lstSalesReturnDetail)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            int salesReturnID = 0;
            try
            {
                base.DataAccessManager.BeginTransaction();
                salesReturnID = base.DataAccessManager.Insert<SalesReturn>(salesReturn);
                salesReturn.SalesReturnID = salesReturnID;
                InsertSalesReturnDetail(salesReturn, lstSalesReturnDetail);

                string filter = string.Format("{0}='{1}' and {2}={3}", "ReferenceType", (int)MTBFEnums.ChildAccountType.Customer, "ReferenceID", salesReturn.CustomerID);
                ChildAccount customerChildAccount = base.DataAccessManager.GetFilteredData<ChildAccount>(filter).Cast<ChildAccount>().ToList().FirstOrDefault();

                InsertJournalInformationForGoodsReceive(salesReturn,customerChildAccount);
                if (salesReturn.PaidAmount > 0)
                {
                    InsertJournalInformationForCashPayment(salesReturn,customerChildAccount);
                }
                base.DataAccessManager.CommitTransaction();
            }
            catch
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

        public int UpdateSalesReturnInformation(SalesReturn salesReturn, List<SalesReturnDetail> lstSalesReturnDetail)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            try
            {
                base.DataAccessManager.BeginTransaction();
                base.DataAccessManager.Update<SalesReturn>(salesReturn);
                DeleteAllTransferDetail(salesReturn);
                InsertSalesReturnDetail(salesReturn, lstSalesReturnDetail);
                string filter = string.Format("{0}='{1}' and {2}={3}", "ReferenceType", (int)MTBFEnums.ChildAccountType.Customer, "ReferenceID", salesReturn.CustomerID);
                ChildAccount customerChildAccount = base.DataAccessManager.GetFilteredData<ChildAccount>(filter).Cast<ChildAccount>().ToList().FirstOrDefault();

                DeleteJournalInformation(salesReturn);
                InsertJournalInformationForGoodsReceive(salesReturn, customerChildAccount);
                if (salesReturn.PaidAmount > 0)
                {
                    InsertJournalInformationForCashPayment(salesReturn, customerChildAccount);
                }
                base.DataAccessManager.CommitTransaction();
            }
            catch
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



        private void DeleteJournalInformation(SalesReturn salesReturn)
        {
            string filter = string.Format("{0}={1}", "SalesReturnID", salesReturn.SalesReturnID);

            foreach (Journal journal in base.DataAccessManager.GetFilteredData<Journal>(filter))
            {
                base.DataAccessManager.Delete<Journal>(journal.JournalID);
            }
        }


        public int GetJournalReferenceID()
        {
            int referenceID = 0;
            referenceID = Convert.ToInt32(base.DataAccessManager.ExecuteScalar("Select isnull(max(ReferenceNo),0)+1  from Journal "));
            return referenceID;
        }

        public int InsertJournalInformationForCashPayment(SalesReturn salesReturn, ChildAccount customerChildAccount)
        {
            int result = 0;
            int referenceID = 1;
            decimal amount = salesReturn.PaidAmount;

                
            referenceID = new JournalManager().GetJournalReferenceID();

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
                journal.Amount = amount;
                journal.ReferenceNo = referenceID;
                journal.SalesReturnID = salesReturn.SalesReturnID;
             
                journal.BranchID = MTBFConstants.AppConstants.BranchID;
                journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                base.DataAccessManager.Insert<Journal>(journal);
            }

            return result;
        }

        private void InsertJournalInformationForGoodsReceive(SalesReturn salesReturn, ChildAccount customerChildAccount)
        {
           

            int referenceID = GetJournalReferenceID();

            for (int i = 0; i <= 1; i++)
            {
                Journal journal = new Journal();

                if (i == 0)
                {
                    journal.DrCrIndecator = "Dr";
                    journal.AccountID = IFMSConstant.InventoryAccountID;
                }
                else
                {
                    journal.DrCrIndecator = "Cr";
                    journal.ChildAccountID = customerChildAccount.ChildAccountID;
                    journal.AccountID = customerChildAccount.AccountID;
                }

                journal.JAccountID = 0;
                journal.Amount = salesReturn.Total;
                journal.ReferenceNo = referenceID;
                journal.SalesReturnID = salesReturn.SalesReturnID;
                journal.BranchID = salesReturn.BranchID;
                journal.OrganizationID = salesReturn.OrganizationID;
                base.DataAccessManager.Insert<Journal>(journal);
            }
        }



        private void DeleteAllTransferDetail(SalesReturn salesReturn)
        {
            foreach (SalesReturnDetail transferDetail in GetAllSalesReturnDetailBySalesID(salesReturn.SalesReturnID))
            {
                base.DataAccessManager.Delete<SalesReturnDetail>(transferDetail.SalesReturnDetailID);
            }
        }


        public SalesReturn GetSalesReturnByID(int _TransferID)
        {
            SalesReturn SalesReturn = base.DataAccessManager.GetByID<SalesReturn>(_TransferID, "SalesReturnID");
            return SalesReturn;
        }

        public List<SalesReturn> GetAllSalesReturn()
        {
            return base.DataAccessManager.GetAll<SalesReturn>().Cast<SalesReturn>().ToList();
        }

        #endregion

        #region "SalesReturn Detail"

        private void InsertSalesReturnDetail(SalesReturn transfer, List<SalesReturnDetail> lstTransferDetail)
        {
            foreach (SalesReturnDetail salesReturnDetail in lstTransferDetail)
            {
                salesReturnDetail.SalesReturnID = transfer.SalesReturnID;
                base.DataAccessManager.Insert<SalesReturnDetail>(salesReturnDetail);

            }
        }

        public int UpdateSalesReturnDetail(SalesReturnDetail SalesReturnDetail)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            try
            {
                base.DataAccessManager.Update<SalesReturnDetail>(SalesReturnDetail);

            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }

        public SalesReturnDetail GetSalesReturnDetailByID(int _TransferDetailID)
        {
            SalesReturnDetail SalesReturnDetail = base.DataAccessManager.GetByID<SalesReturnDetail>(_TransferDetailID, "TransferDetailID");
            return SalesReturnDetail;
        }

        public List<SalesReturnDetail> GetAllSalesReturnDetail()
        {
            return base.DataAccessManager.GetAll<SalesReturnDetail>().Cast<SalesReturnDetail>().ToList();
        }

        #endregion


        public List<SalesReturnDetail> GetAllSalesReturnDetailBySalesID(int salesReturnID)
        {
            string filter = string.Format("{0}={1}", "SalesReturnID", salesReturnID);
            return base.DataAccessManager.GetFilteredData<SalesReturnDetail>(filter).Cast<SalesReturnDetail>().ToList();
        }

        public List<SalesReturnDetail> GetAllSalesReturnDetailProductByProductIDandDAate(int branchID, int organizationID, string fromDate, string toDate)
        {
            string filter = string.Format(IFMSConstant.QueryConstants.GET_ALL_TRANSFER_PRODUCT_BY_PRODUCTID_DATE, branchID, organizationID, fromDate, toDate);
            return base.DataAccessManager.ExecuteSQL<SalesReturnDetail>(filter).Cast<SalesReturnDetail>().ToList();
        }




        public List<SalesReturn> GetAllSalesReturnByDate(int branchID, int organizationID, string fromDate, string toDate)
        {
            string filter = string.Format("{0}={1} And {2}={3} And ReturnDate between '{4}' and '{5}'", "BranchID", branchID, "OrganizationID", organizationID, fromDate, toDate);

            return base.DataAccessManager.GetFilteredData<SalesReturn>(filter).Cast<SalesReturn>().ToList();
        }

        public List<SalesReturn> GetAllSalesReturnByCustomerID(int customerID)
        {
            string filter = string.Format("{0}={1}", "CustomerID", customerID);
            return base.DataAccessManager.GetFilteredData<SalesReturn>(filter).Cast<SalesReturn>().ToList();
        }

        public List<SalesReturn> GetFilteredSalesReturn(string filter)
        {
            return base.DataAccessManager.GetFilteredData<SalesReturn>(filter).Cast<SalesReturn>().ToList();
        }

        public List<SalesReturnReport> GetSalesReturnReportByDate(string _fromDate, string _toDate, int branchID, int organizationID)
        {
            string filter = string.Format(IFMSConstant.QueryConstants.GET_SALES_RETURN_HISTORY_BY_DATE_AND_ORGANIZATION, _fromDate ,_toDate ,branchID ,organizationID);
            return base.DataAccessManager.ExecuteSQL<SalesReturnReport>(filter).Cast<SalesReturnReport>().ToList();
        }

        public List<SalesReturnDetail> GetAllSalesReturnProductByProductIDandDAate(int branchID, int organizationID, string fromDate, string toDate)
        {
            string filter = string.Format(IFMSConstant.QueryConstants.GET_ALL_SALES_RETURN_PRODUCT_BY_PRODUCTID_DATE, branchID, organizationID, fromDate, toDate);
            return base.DataAccessManager.ExecuteSQL<SalesReturnDetail>(filter).Cast<SalesReturnDetail>().ToList();
        }



        public List<PurchaseReturnDetail> GetAllPurchaseReturnProductByDaate(int branchID, int organizationID, string fromDate, string toDate)
        {
            string filter = string.Format(IFMSConstant.QueryConstants.GET_ALL_PURCHASE_RETURN_PRODUCT_BY_PRODUCTID_DATE, branchID, organizationID, fromDate, toDate);
            return base.DataAccessManager.ExecuteSQL<PurchaseReturnDetail>(filter).Cast<PurchaseReturnDetail>().ToList();      
        }
    }
}
