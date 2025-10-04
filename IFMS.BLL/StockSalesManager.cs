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
    public class StockSalesManager : BllBase
    {

        public int InsertStockSales(StockSales stockSales, List<StockSalesDetails> lstStockSales)
        {

            int stockSalesID = 0;

            try
            {
                base.DataAccessManager.BeginTransaction();

                stockSalesID = base.DataAccessManager.Insert<StockSales>(stockSales);
                stockSales.StockSalesID = stockSalesID;

                InsertStockSalesDetail(stockSales, lstStockSales);

                InsertJournalInformationForCreditSales(stockSales);
                InsertJournalInformationForGoodsDelivery(stockSales, lstStockSales);

                if (stockSales.ReceiveAmount > 0)
                {
                    InsertJournalInformationForCashReceive(stockSales);
                }

                base.DataAccessManager.CommitTransaction();
            }
            catch
            {
                base.DataAccessManager.Rollback();
                stockSalesID = 0;
            }
            finally
            {
                base.DataAccessManager.EndTransaction();
            }
            return stockSalesID;
        }


        public int GetJournalReferenceID()
        {
            int referenceID = 0;
            referenceID = Convert.ToInt32(base.DataAccessManager.ExecuteScalar("Select isnull(max(ReferenceNo),0)+1  from Journal "));
            return referenceID;
        }

        private int InsertJournalInformationForCreditSales(StockSales stockSales)
        {
            int result = 0;
            int referenceID = 1;
            decimal amount = stockSales.Total;

            referenceID = GetJournalReferenceID();
            ChildAccount childAccount = new ChildAccountManager().GetChildAccountByPartyID(stockSales.PartyID);

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
                    journal.AccountID = IFMSConstant.GoodsSalesAccountID;
                }

                journal.StockSalesID = stockSales.StockSalesID;
                journal.JAccountID = 0;
                journal.Amount = amount;
                journal.ReferenceNo = referenceID;
                journal.JournalType = (int)MTBFEnums.JournalType.Production;
                journal.BranchID = MTBFConstants.AppConstants.BranchID;
                journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                base.DataAccessManager.Insert<Journal>(journal);
            }

            return result;
        }

        private int InsertJournalInformationForCashReceive(StockSales stockSales)
        {
            int result = 0;
            int referenceID = 1;
            decimal amount = stockSales.ReceiveAmount;

            referenceID = GetJournalReferenceID();

            for (int i = 0; i <= 1; i++)
            {
                IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();

                if (i == 0)
                {
                    journal.DrCrIndecator = "Dr";
                    journal.AccountID = IFMSConstant.CashAccountID;

                }
                else
                {
                    journal.DrCrIndecator = "Cr";
                    journal.AccountID = IFMSConstant.GoodsSalesAccountID;
                }

                journal.JAccountID = 0;
                journal.Amount = amount;
                journal.ReferenceNo = referenceID;
                journal.StockSalesID = stockSales.StockSalesID;
                journal.JournalType = (int)MTBFEnums.JournalType.Production;
                journal.BranchID = MTBFConstants.AppConstants.BranchID;
                journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                base.DataAccessManager.Insert<Journal>(journal);
            }

            return result;
        }

        private decimal CalculatePurchasePrice(List<StockSalesDetails> lstStockSales)
        {
            string productID = string.Empty;

            decimal quantity = 0;

            decimal purchasePrice = 0;
            foreach (StockSalesDetails stockDetail in lstStockSales)
            {
                purchasePrice = purchasePrice + (CalculatePriceByProductID(stockDetail.ProductID) * quantity);
            }

            return purchasePrice;
        }

        private decimal CalculatePriceByProductID(string productID)
        {
            decimal purchasePrice = 0;
            decimal totalQuantity = 0;
            decimal totalPrice = 0;

            string filter = string.Format("{0}='{1}'", "ProductID", productID);
            List<StockDetails> lstSockDetials = new StockManager().GetFilteredStockDetail(filter).Cast<StockDetails>().ToList();

            foreach (StockDetails orderDetail in lstSockDetials)
            {
                totalQuantity = totalQuantity + orderDetail.Quantity;
                totalPrice = totalPrice + (orderDetail.Price * orderDetail.Quantity);
            }

            purchasePrice = totalPrice / totalQuantity;
            return purchasePrice;

        }

        /// <summary>
        /// Method to insert journal information for credit sales.
        /// </summary>
        /// <returns></returns>
        private int InsertJournalInformationForGoodsDelivery(StockSales stockSales, List<StockSalesDetails> lstStockSales)
        {
            int result = 0;
            int referenceID = 1;
            decimal amount = CalculatePurchasePrice(lstStockSales);

            referenceID = GetJournalReferenceID();
            for (int i = 0; i <= 1; i++)
            {
                IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();

                if (i == 0)
                {
                    journal.DrCrIndecator = "Dr";
                    journal.AccountID = IFMSConstant.CostOfGoodsSoldAccountID;
                }
                else
                {
                    journal.DrCrIndecator = "Cr";
                    journal.AccountID = IFMSConstant.FinishProductInventory;
                }

                journal.JAccountID = 0;
                journal.Amount = amount;
                journal.ReferenceNo = referenceID;
                journal.StockSalesID = stockSales.StockSalesID;
                journal.JournalType = (int)MTBFEnums.JournalType.Production;
                journal.BranchID = MTBFConstants.AppConstants.BranchID;
                journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                base.DataAccessManager.Insert<Journal>(journal);
            }
            return result;
        }

        private void InsertStockSalesDetail(StockSales stockSales, List<StockSalesDetails> lstStockSales)
        {
            foreach (StockSalesDetails stockSalesDetail in lstStockSales)
            {
                stockSalesDetail.SotckSalesID = stockSales.StockSalesID;
                base.DataAccessManager.Insert<StockSalesDetails>(stockSalesDetail);
            }
        }

        public int UpdateStockSales(StockSales stockSales, List<StockSalesDetails> lstStockSales)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            try
            {
                base.DataAccessManager.BeginTransaction();

                base.DataAccessManager.Update<StockSales>(stockSales);

                DeleteStockSalesDetail(stockSales);

                InsertStockSalesDetail(stockSales, lstStockSales);

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

        private void DeleteStockSalesDetail(StockSales stockSales)
        {
            List<StockSalesDetails> lstStockSalesDetail = GetAllStockSalesDetailByStockSalesID(stockSales.StockSalesID);

            foreach (StockSalesDetails dDetail in lstStockSalesDetail)
            {
                base.DataAccessManager.Delete<StockSalesDetails>(dDetail.StockSalesDetailID);
            }
        }

        public StockSales GetStockSalesByID(int _stockSalesID)
        {
            StockSales StockSales = base.DataAccessManager.GetByID<StockSales>(_stockSalesID, "StockSalesID");
            return StockSales;
        }

        public List<StockSales> GetAllStockSales()
        {
            return base.DataAccessManager.GetAll<StockSales>().Cast<StockSales>().ToList();
        }

        public List<StockSales> GetAllStockSalesByOrganizationID(int organizationID)
        {
            string filter = string.Format("{0}={1}", "OrganizationID", organizationID);
            return base.DataAccessManager.GetFilteredData<StockSales>(filter).Cast<StockSales>().ToList();

        }

        public List<StockSales> GetFilteredStockSales(string filter)
        {
            return base.DataAccessManager.GetFilteredData<StockSales>(filter).Cast<StockSales>().ToList();
        }

        public List<StockSalesDetails> GetAllStockSalesDetailByStockSalesID(int stockSalesID)
        {
            string filter = string.Format("{0}={1}", "SotckSalesID", stockSalesID);
            return base.DataAccessManager.GetFilteredData<StockSalesDetails>(filter).Cast<StockSalesDetails>().ToList();
        }




        public int InsertSalesParty(SalesParty salesParty)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            int partyID = 0;
            try
            {
                base.DataAccessManager.BeginTransaction();

                partyID = base.DataAccessManager.Insert<SalesParty>(salesParty);
                salesParty.SalesPartyID = partyID;
                InsertChildAccount(salesParty);

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


        private bool DuplicateChildAccount(int partyID)
        {
            string filter = string.Format("{0}='{1}'", "SalesPartyID", partyID);

            ChildAccount childAccount = new ChildAccount();
            childAccount = base.DataAccessManager.GetFilteredData<ChildAccount>(filter).Cast<ChildAccount>().ToList().FirstOrDefault();
            if (childAccount != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void InsertChildAccount(SalesParty party)
        {
            ChildAccount childAccount = new ChildAccount();
            childAccount.AccountID = IFMSConstant.AccountReceivableID;
            childAccount.Description = party.PartyName;

            childAccount.SalesPartyID = party.SalesPartyID;

            childAccount.CreatedDate = DateTime.Now;
            childAccount.Status = 1;
            base.DataAccessManager.Insert<ChildAccount>(childAccount);

        }



        public int UpdateSalesParty(SalesParty salesParty)
        {

            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            try
            {
                base.DataAccessManager.BeginTransaction();

                base.DataAccessManager.Update<SalesParty>(salesParty);

                if (!DuplicateChildAccount(salesParty.SalesPartyID))
                {
                    InsertChildAccount(salesParty);
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


        public SalesParty GetSalesPartyByID(int _meterialsID)
        {
            SalesParty Meterials = base.DataAccessManager.GetByID<SalesParty>(_meterialsID, "SalesPartyID");
            return Meterials;
        }

        public List<SalesParty> GetAllSalesParty()
        {
            return base.DataAccessManager.GetAll<SalesParty>().Cast<SalesParty>().ToList();
        }




        public List<SalesParty> GetFilteredSalesParty(string filter)
        {
            return base.DataAccessManager.GetFilteredData<SalesParty>(filter).Cast<SalesParty>().ToList();
        }




    }
}
