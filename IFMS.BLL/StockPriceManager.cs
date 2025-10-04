using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Enums;

namespace IFMS.BLL
{
    public class StockPriceManager : BllBase
    {

        public int InsertStockPrice(StockPrice stockPrice)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                StockPrice existing = GetStockPriceByProductID(stockPrice.ProductID);
                if (existing != null)
                {
                    existing.Price = stockPrice.Price;
                    existing.OldPrice = stockPrice.OldPrice;
                    existing.UpdateDate = DateTime.Now;
                    existing.UpdatedBy = stockPrice.UpdatedBy;
                    base.DataAccessManager.Update<StockPrice>(existing);
                }
                else
                {
                    base.DataAccessManager.Insert<StockPrice>(stockPrice);
                }
            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }

        private StockPrice GetStockPriceByProductID(string productID)
        {
            string filter = string.Format("{0}='{1}'", "ProductID", productID);
            return base.DataAccessManager.GetFilteredData<StockPrice>(filter).Cast<StockPrice>().FirstOrDefault();
        }


        public int UpdateStockPrice(StockPrice StockPrice)
        {

            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                base.DataAccessManager.Update<StockPrice>(StockPrice);

            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }

        public StockPrice GetStockPriceByID(int _StockPriceID)
        {
            StockPrice StockPrice = base.DataAccessManager.GetByID<StockPrice>(_StockPriceID, "StockPriceID");
            return StockPrice;
        }

        public List<StockPrice> GetAllStockPrice()
        {
            return base.DataAccessManager.GetAll<StockPrice>().Cast<StockPrice>().ToList();
        }

        public List<StockPrice> GetAllStockPriceByOrganizationID(int organizationID)
        {
            string filter = string.Format("{0}={1}", "OrganizationID", organizationID);
            return base.DataAccessManager.GetFilteredData<StockPrice>(filter).Cast<StockPrice>().ToList();

        }


        public List<StockPrice> GetFilteredStockPrice(string filter)
        {
            return base.DataAccessManager.GetFilteredData<StockPrice>(filter).Cast<StockPrice>().ToList();
        }
    }
}
