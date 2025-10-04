using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NybSys.MTBF.Utility.Constants;
using NybSys.MTBF.Utility.Enums;
using IMFS.Common.DTO;

namespace IFMS.BLL
{
    public class SalesQuotationManager : BllBase
    {
        public SalesQuotationDetail GetSalesQuotationDetailByID(int salesQuotationDetailID)
        {
            return base.DataAccessManager.GetByID<SalesQuotationDetail>(salesQuotationDetailID, "SalesQuotationDetailID");
        }


        /// <summary>
        /// Method to insert sales quotation information.
        /// </summary>
        /// <param name="salesQuotation"></param>
        /// <param name="lstSalesQuotationDetail"></param>
        /// <returns></returns>
        public int InsertSalesQuotationInformation(SalesQuotation salesQuotation, List<SalesQuotationDetail> lstSalesQuotationDetail)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            int salesQuotationID = 0;
            try
            {
              
                base.DataAccessManager.BeginTransaction();

                salesQuotationID = base.DataAccessManager.Insert<SalesQuotation>(salesQuotation);

                salesQuotation.SalesQuotationID = salesQuotationID;
                InsertSalesQuotationDetail(salesQuotation, lstSalesQuotationDetail);

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

        /// <summary>
        /// Method to insert sales quotation detail.
        /// </summary>
        /// <param name="salesQuotation"></param>
        /// <param name="lstSalesQuotationDetail"></param>
        private void InsertSalesQuotationDetail(SalesQuotation salesQuotation, List<SalesQuotationDetail> lstSalesQuotationDetail)
        {
            foreach (SalesQuotationDetail detail in lstSalesQuotationDetail)
            {
                detail.SalesQuotationID = salesQuotation.SalesQuotationID;
                base.DataAccessManager.Insert<SalesQuotationDetail>(detail);
            }
        }

        /// <summary>
        /// Method to update sales quotation information.
        /// </summary>
        /// <param name="salesQuotation"></param>
        /// <param name="lstSalesQuotationDetail"></param>
        /// <returns></returns>
        public int UpdateSalesQuotationInformation(SalesQuotation salesQuotation, List<SalesQuotationDetail> lstSalesQuotationDetail)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            int salesQuotationID = 0;
            try
            {
                base.DataAccessManager.BeginTransaction();

                salesQuotationID = base.DataAccessManager.Update<SalesQuotation>(salesQuotation);

                List<SalesQuotationDetail> lstDeleteSalesQuotationDetail = GetAllSalesQuotationDetailBySalesQuotationID(salesQuotation.SalesQuotationID);
                DeleteSalesQuotationDetail(lstDeleteSalesQuotationDetail);

                InsertSalesQuotationDetail(salesQuotation, lstSalesQuotationDetail);

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

        /// <summary>
        /// Method to delete sales quotation detail information.
        /// </summary>
        /// <param name="lstSalesQuotationDetail"></param>
        private void DeleteSalesQuotationDetail(List<SalesQuotationDetail> lstSalesQuotationDetail)
        {
            foreach (SalesQuotationDetail detail in lstSalesQuotationDetail)
            {
                base.DataAccessManager.Delete<SalesQuotationDetail>(detail.SalesQuotationDetailID);
            }
        }

  

        public List<SalesQuotationDetail> GetAllSalesQuotationDetailBySalesQuotationID(int salesQuotationID)
        {
            string filter = string.Format("{0}={1}", "SalesQuotationID", salesQuotationID);
            return base.DataAccessManager.GetFilteredData<SalesQuotationDetail>(filter).Cast<SalesQuotationDetail>().ToList();
        }      

        public List<SalesQuotation> GetAllSalesQuotationByCustomerID(int customerID)
        {
            string filter = string.Format("{0}={1}", MTBFConstants.DataField.CUSTOMER_ID, customerID);
            return base.DataAccessManager.GetFilteredData<SalesQuotation>(filter).Cast<SalesQuotation>().ToList();
        }      

     
        public SalesQuotation GetSalesQuotationByID(int quotationID)
        {
            return base.DataAccessManager.GetByID<SalesQuotation>(quotationID, "SalesQuotationID");
        }

       

    }
}
