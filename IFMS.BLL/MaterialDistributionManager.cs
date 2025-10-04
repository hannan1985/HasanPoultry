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
    public class MaterialDistributionManager : BllBase
    {

        public int InsertDistribution(Distribution distribution, List<DistributeDetails> lstDistribution)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            int distributionID = 0;

            try
            {
                base.DataAccessManager.BeginTransaction();

                distributionID = base.DataAccessManager.Insert<Distribution>(distribution);
                distribution.DistributeID = distributionID;

                InsertDistributionDetail(distribution, lstDistribution);

                InsertJournalInformationForGoodsDelivery(distribution, lstDistribution);

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

        private decimal CalculateTotalDistributionAmount(List<DistributeDetails> lstDistribution)
        {
            decimal total = 0;

            foreach (DistributeDetails dDetail in lstDistribution)
            {
                total = total + (dDetail.Price * dDetail.Quantity);
            }
            return total;
        }

        public int GetJournalReferenceID()
        {
            int referenceID = 0;
            referenceID = Convert.ToInt32(base.DataAccessManager.ExecuteScalar("Select isnull(max(ReferenceNo),0)+1  from Journal "));
            return referenceID;
        }

        /// <summary>
        /// Method to insert journal information for credit sales.
        /// </summary>
        /// <returns></returns>
        public int InsertJournalInformationForGoodsDelivery(Distribution distribution, List<DistributeDetails> lstDistribution)
        {
            int result = 0;
            int referenceID = 1;
            decimal amount = CalculateTotalDistributionAmount(lstDistribution);

            referenceID = GetJournalReferenceID();
            for (int i = 0; i <= 1; i++)
            {
                IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();

                if (i == 0)
                {
                    journal.DrCrIndecator = "Dr";
                    journal.AccountID = IFMSConstant.Workinprocessinventory;
                }
                else
                {
                    journal.DrCrIndecator = "Cr";
                    journal.AccountID = IFMSConstant.RawMaterialInventory;
                }

                journal.MaterialDistributeID = distribution.DistributeID;
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


        //public int InsertJournalInformationForGoodsSendToProductionUnit(Distribution distribution, List<DistributeDetails> lstDistribution)
        //{
        //    int result = 0;
        //    int referenceID = 1;
        //    decimal amount = CalculateTotalDistributionAmount(lstDistribution);

        //    referenceID = GetJournalReferenceID();
        //    for (int i = 0; i <= 1; i++)
        //    {
        //        IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();

        //        if (i == 0)
        //        {
        //            journal.DrCrIndecator = "Dr";
        //            journal.AccountID = IFMSConstant.CostOfGoodsSoldAccountID;
        //        }
        //        else
        //        {
        //            journal.DrCrIndecator = "Cr";
        //            journal.AccountID = IFMSConstant.InventoryAccountID;
        //        }

        //        journal.MaterialDistributeID = distribution.DistributeID;
        //        journal.JAccountID = 0;
        //        journal.Amount = amount;
        //        journal.ReferenceNo = referenceID;
        //        journal.BranchID = MTBFConstants.AppConstants.BranchID;
        //        journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
        //        base.DataAccessManager.Insert<Journal>(journal);
        //    }

        //    return result;
        //}


        private void InsertDistributionDetail(Distribution distribution, List<DistributeDetails> lstDistribution)
        {
            foreach (DistributeDetails distributeDetail in lstDistribution)
            {
                distributeDetail.DistributeID = distribution.DistributeID;
                base.DataAccessManager.Insert<DistributeDetails>(distributeDetail);
            }
        }

        public int UpdateDistribution(Distribution distribution, List<DistributeDetails> lstDistribution)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            try
            {
                base.DataAccessManager.BeginTransaction();

                base.DataAccessManager.Update<Distribution>(distribution);

                DeleteDistributionDetail(distribution);


                List<Journal> lstJournal = new List<Journal>();

                lstJournal = GetAllJournalByMaterialID(distribution);

                DeleteAllJournal(lstJournal);

                InsertJournalInformationForGoodsDelivery(distribution, lstDistribution);

                InsertDistributionDetail(distribution, lstDistribution);

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

        private void DeleteAllJournal(List<Journal> lstJournal)
        {
            foreach (Journal journal in lstJournal)
            {
                base.DataAccessManager.Delete<Journal>(journal.JournalID);
            }
        }


        private List<Journal> GetAllJournalByMaterialID(Distribution distribution)
        {
            string filter = string.Format("{0}={1}", "MaterialDistributeID", distribution.DistributeID);
            return base.DataAccessManager.GetFilteredData<Journal>(filter).Cast<Journal>().ToList();

        }


        private void DeleteDistributionDetail(Distribution distribution)
        {
            List<DistributeDetails> lstDistributionDetail = GetAllDistributionDetailByDistributionID(distribution.DistributeID);

            foreach (DistributeDetails dDetail in lstDistributionDetail)
            {
                base.DataAccessManager.Delete<DistributeDetails>(dDetail.DistributeDetailID);
            }
        }

        public Distribution GetDistributionByID(int _distributionID)
        {
            Distribution Distribution = base.DataAccessManager.GetByID<Distribution>(_distributionID, "DistributeID");
            return Distribution;
        }

        public List<Distribution> GetAllDistribution()
        {
            return base.DataAccessManager.GetAll<Distribution>().Cast<Distribution>().ToList();
        }

        public List<Distribution> GetAllDistributionByOrganizationID(int organizationID)
        {
            string filter = string.Format("{0}={1}", "OrganizationID", organizationID);
            return base.DataAccessManager.GetFilteredData<Distribution>(filter).Cast<Distribution>().ToList();

        }

        public List<Distribution> GetFilteredDistribution(string filter)
        {
            return base.DataAccessManager.GetFilteredData<Distribution>(filter).Cast<Distribution>().ToList();
        }

        public List<DistributeDetails> GetAllDistributionDetailByDistributionID(int distributionID)
        {
            string filter = string.Format("{0}={1}", "DistributeID", distributionID);
            return base.DataAccessManager.GetFilteredData<DistributeDetails>(filter).Cast<DistributeDetails>().ToList();
        }



        public List<DistributeDetails> GetAllDistributionDetailByMaterialID(int materialID)
        {
            string filter = string.Format("{0}={1}", "MaterialID", materialID);
            return base.DataAccessManager.GetFilteredData<DistributeDetails>(filter).Cast<DistributeDetails>().ToList();
        }
    }
}
