using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Enums;
using IMFS.Common.Utility;

namespace IFMS.BLL
{
    public class DistributionManager : BllBase
    {
        #region "DistributeSample"

        public int InsertDistributeSample(DistributeSample distributeSample)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                base.DataAccessManager.Insert<DistributeSample>(distributeSample);

            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }

        public int UpdateDistributeSample(DistributeSample distributeSample)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                base.DataAccessManager.Update<DistributeSample>(distributeSample);

            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }

        public List<DistributeSample> GetAllDistributeSample()
        {
            return base.DataAccessManager.GetAll<DistributeSample>().Cast<DistributeSample>().ToList();
        }

        public DistributeSample GetDistributeSampleByID(int distributeSampleID)
        {
            return base.DataAccessManager.GetByID<DistributeSample>(distributeSampleID, "DistributionID");
        }

        #endregion


        #region "Seller"
        public int InsertSeller(Seller seller)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                base.DataAccessManager.Insert<Seller>(seller);

            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }

        public int UpdateSeller(Seller seller)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                base.DataAccessManager.Update<Seller>(seller);
            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }

        public List<Seller> GetAllSeller()
        {
            return base.DataAccessManager.GetAll<Seller>().Cast<Seller>().ToList();
        }

        public Seller GetSellerByID(int _SellerID)
        {
            return base.DataAccessManager.GetByID<Seller>(_SellerID, "SellerID");
        }

        #endregion




        public List<DistributeSample> GetDistributeSampleBySellerIDAndDate(int sellerID, string fromDate, string toDate)
        {
            string filter = string.Format("{0}={1} and {2} between '{3}' and '{4}'", "SellerID", sellerID, "DistributeDate", fromDate, toDate);
            return base.DataAccessManager.GetFilteredData<DistributeSample>(filter).Cast<DistributeSample>().ToList();
        }

        public decimal GetAllGeivenSampleByProductID(string productID, int branchID, int organizationID)
        {
            string filter = string.Format(IFMSConstant.QueryConstants.GET_GIVEN_SAMPLE_BY_PRODUCTID, productID, branchID, organizationID);
            return Convert.ToDecimal(base.DataAccessManager.ExecuteScalar(filter));
        }

        public List<DistributeSample> GetAllSampleDistributionByProductIDAndDate(int branchID, int organizationID, string fromDate, string toDate)
        {
            string filter = string.Format(IFMSConstant.QueryConstants.GET_GIVEN_SAMPLE_BY_PRODUCTID_AND_DATE, branchID, organizationID,  fromDate, toDate);
            return base.DataAccessManager.ExecuteSQL<DistributeSample>(filter).Cast<DistributeSample>().ToList();

        }
    }
}
