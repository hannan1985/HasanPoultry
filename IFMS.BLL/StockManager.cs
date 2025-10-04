using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Enums;
using IMFS.Common.Utility;

namespace IFMS.BLL
{
    public class StockManager : BllBase
    {

        public int InsertStock(Stock stock, List<StockDetails> lstStock)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            int stockID = 0;

            try
            {
                base.DataAccessManager.BeginTransaction();

                stockID = base.DataAccessManager.Insert<Stock>(stock);
                stock.StockID = stockID;

                InsertStockDetail(stock, lstStock);

                InsertJournalInformationForGoodsReceive(stock);

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


        private void InsertJournalInformationForGoodsReceive(Stock stock)
        {
            int referenceID = new JournalManager().GetJournalReferenceID();

            for (int i = 0; i <= 1; i++)
            {
                Journal journal = new Journal();

                if (i == 0)
                {
                    journal.DrCrIndecator = "Dr";
                    journal.AccountID = IFMSConstant.FinishProductInventory;
                }
                else
                {
                    journal.DrCrIndecator = "Cr";
                    journal.AccountID = IFMSConstant.Workinprocessinventory;
                }

                journal.JAccountID = 0;
                journal.Amount = stock.Total;
                journal.ReferenceNo = referenceID;
                journal.StockID = stock.StockID;
                journal.JournalType = (int)MTBFEnums.JournalType.Production;
                journal.BranchID = stock.BranchID;
                journal.OrganizationID = stock.OrganizationID;
                base.DataAccessManager.Insert<Journal>(journal);
            }
        }

        private void InsertStockDetail(Stock stock, List<StockDetails> lstStock)
        {
            foreach (StockDetails distributeDetail in lstStock)
            {
                distributeDetail.StockID = stock.StockID;
                base.DataAccessManager.Insert<StockDetails>(distributeDetail);
            }
        }

        public int UpdateStock(Stock stock, List<StockDetails> lstStock)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            try
            {
                base.DataAccessManager.BeginTransaction();

                base.DataAccessManager.Update<Stock>(stock);

                DeleteStockDetail(stock);

                InsertStockDetail(stock, lstStock);

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

        private void DeleteStockDetail(Stock stock)
        {
            List<StockDetails> lstStockDetail = GetAllStockDetailByStockID(stock.StockID);

            foreach (StockDetails dDetail in lstStockDetail)
            {
                base.DataAccessManager.Delete<StockDetails>(dDetail.StockDetailID);
            }
        }

        public Stock GetStockByID(int _stockID)
        {
            Stock Stock = base.DataAccessManager.GetByID<Stock>(_stockID, "StockID");
            return Stock;
        }

        public List<Stock> GetAllStock()
        {
            return base.DataAccessManager.GetAll<Stock>().Cast<Stock>().ToList();
        }

        public List<Stock> GetAllStockByOrganizationID(int organizationID)
        {
            string filter = string.Format("{0}={1}", "OrganizationID", organizationID);
            return base.DataAccessManager.GetFilteredData<Stock>(filter).Cast<Stock>().ToList();

        }

        public List<Stock> GetFilteredStock(string filter)
        {
            return base.DataAccessManager.GetFilteredData<Stock>(filter).Cast<Stock>().ToList();
        }

        public List<StockDetails> GetAllStockDetailByStockID(int stockID)
        {
            string filter = string.Format("{0}={1}", "StockID", stockID);
            return base.DataAccessManager.GetFilteredData<StockDetails>(filter).Cast<StockDetails>().ToList();
        }

        public List<StockDetails> GetAllFinishProductStock(int branchID, int organizationID)
        {
            string filter = @"Select  ProductID ,(Quantity -SalesQuantity ) as Quantity from (
                            Select BranchID ,OrganizationID,ProductID  ,SUM(Quantity ) as Quantity, SUM(SalesQuantity ) as SalesQuantity from (
                            Select s.BranchID, s.OrganizationID,sd.ProductID  , SUM (sd.Quantity ) as Quantity , '0' as SalesQuantity From Stock  s join StockDetails  sd on s.StockID  =sd.StockID  
                                group by sd.ProductID ,s.BranchID, s.OrganizationID 
                                union
                                Select s.BranchID, s.OrganizationID, sd.ProductID  ,'0' as Quantity,  SUM(Quantity) as SalesQuantity from StockSales  s join StockSalesDetails  sd on s.StockSalesID  =sd.SotckSalesID  
                                group by sd.ProductID ,s.BranchID, s.OrganizationID )tbl
                                group by ProductID ,BranchID, OrganizationID )tbl2
                                where BranchID={0} and OrganizationID={1}";
            filter = string.Format(filter, branchID, organizationID);
            return base.DataAccessManager.ExecuteSQL<StockDetails>(filter).Cast<StockDetails>().ToList();
        }

        public List<StockDetails> GetFilteredStockDetail(string filter)
        {
            return base.DataAccessManager.GetFilteredData<StockDetails>(filter).Cast<StockDetails>().ToList();
        }
    }
}
