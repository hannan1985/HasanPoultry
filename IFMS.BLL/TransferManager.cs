using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Enums;
using IMFS.Common.Utility;
using IMFS.Common.View;

namespace IFMS.BLL
{
    public class TransferManager : BllBase
    {
        #region "Transfer"

        public int InsertTransferInformation(Transfer transfer, List<TransferDetail> lstTransferDetail)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            int transferID = 0;
            try
            {
                base.DataAccessManager.BeginTransaction();

                transferID = base.DataAccessManager.Insert<Transfer>(transfer);
                transfer.TransferID = transferID;
                InsertTransferDetail(transfer, lstTransferDetail);


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

        public int UpdateTransferInformation(Transfer transfer, List<TransferDetail> lstTransferDetail)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            try
            {
                base.DataAccessManager.BeginTransaction();
                base.DataAccessManager.Update<Transfer>(transfer);
                DeleteAllTransferDetail(transfer);
                InsertTransferDetail(transfer, lstTransferDetail);
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


        private void DeleteAllTransferDetail(Transfer transfer)
        {
            foreach (TransferDetail transferDetail in GetAllTransferDetailByTransferID(transfer.TransferID))
            {
                base.DataAccessManager.Delete<TransferDetail>(transferDetail.TransferDetailID);
            }
        }


        public Transfer GetTransferByID(int _TransferID)
        {
            Transfer Transfer = base.DataAccessManager.GetByID<Transfer>(_TransferID, "TransferID");
            return Transfer;
        }

        public List<Transfer> GetAllTransfer()
        {
            return base.DataAccessManager.GetAll<Transfer>().Cast<Transfer>().ToList();
        }

        #endregion

        #region "Transfer Detail"

        private void InsertTransferDetail(Transfer transfer, List<TransferDetail> lstTransferDetail)
        {
            foreach (TransferDetail transferDetail in lstTransferDetail)
            {
                transferDetail.TransferID = transfer.TransferID;
                base.DataAccessManager.Insert<TransferDetail>(transferDetail);

            }
        }

        public int UpdateTransferDetail(TransferDetail TransferDetail)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            try
            {
                base.DataAccessManager.Update<TransferDetail>(TransferDetail);

            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }

        public TransferDetail GetTransferDetailByID(int _TransferDetailID)
        {
            TransferDetail TransferDetail = base.DataAccessManager.GetByID<TransferDetail>(_TransferDetailID, "TransferDetailID");
            return TransferDetail;
        }

        public List<TransferDetail> GetAllTransferDetail()
        {
            return base.DataAccessManager.GetAll<TransferDetail>().Cast<TransferDetail>().ToList();
        }

        #endregion



        public List<TransferDetail> GetAllTransferDetailByTransferID(int transferID)
        {
            string filter = string.Format("{0}={1}", "TransferID", transferID);
            return base.DataAccessManager.GetFilteredData<TransferDetail>(filter).Cast<TransferDetail>().ToList();
        }

        public List<Transfer> GetTransferBySalesCenterAndDate(int salesCenterID, string fromDate, string toDate)
        {
            string filter = string.Format("{0}={1} And {2} between '{3}' and '{4}'", "SalesCenterID", salesCenterID, "TransferDate", fromDate, toDate);
            return base.DataAccessManager.GetFilteredData<Transfer>(filter).Cast<Transfer>().ToList();
        }

        public decimal GetAllTransferProductByProudctCode(string productCode, int branchID, int organizationID)
        {
            string filter = string.Format(IFMSConstant.QueryConstants.GET_TRANSFER_PRODUCT_BY_PRODUCTID, productCode, branchID, organizationID);
            return Convert.ToDecimal(base.DataAccessManager.ExecuteScalar(filter));
        }

        public List<TransferDetail> GetAllTransterProductByProductIDandDAate(int branchID, int organizationID, string fromDate, string toDate)
        {
            string filter = string.Format(IFMSConstant.QueryConstants.GET_ALL_TRANSFER_PRODUCT_BY_PRODUCTID_DATE, branchID, organizationID, fromDate, toDate);
            return base.DataAccessManager.ExecuteSQL<TransferDetail>(filter).Cast<TransferDetail>().ToList();
        }





        public List<TransferProductPrice> GetAllTransferProductPrice()
        {
            string sql = @"Select p.ProductID,p.ProductName, sum(td.Quantity)Quantity,  avg(td.Price)Price from dbo.TransferDetail td
                        left outer join ProductInformation p
                        on td.ProductCode=p.ProductID
                        group by p.ProductID,p.ProductName";

            return base.DataAccessManager.ExecuteSQL<TransferProductPrice>(sql).Cast<TransferProductPrice>().ToList();
        }
    }
}
