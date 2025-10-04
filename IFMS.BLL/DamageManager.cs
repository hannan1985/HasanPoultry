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
    public class DamageManager : BllBase
    {
        public int SaveDamageInformation(VMDamage vmDamage)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            try
            {
                base.DataAccessManager.BeginTransaction();

                if (vmDamage.DamageInfo.DamageInfoID > 0)
                {
                    base.DataAccessManager.Update<DamageInfo>(vmDamage.DamageInfo);
                    DeleteAllPreviousDetail(vmDamage.DamageInfo);
                    InsertDamageDetailInformation(vmDamage);

                    List<Journal> lstExistingJournal = new List<Journal>();
                    string filter = string.Format("{0}={1}", "DamageInfoID", vmDamage.DamageInfo.DamageInfoID);
                    lstExistingJournal = base.DataAccessManager.GetFilteredData<Journal>(filter).Cast<Journal>().ToList();
                    DeleteJournal(lstExistingJournal);
                }
                else
                {
                    vmDamage.DamageInfo.DamageInfoID = base.DataAccessManager.Insert<DamageInfo>(vmDamage.DamageInfo);
                    InsertDamageDetailInformation(vmDamage);
                }

                InsertJournalInformationForGoodsDamage(vmDamage);

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

        private void DeleteJournal(List<Journal> lstExistingJournal)
        {
            foreach (Journal journal in lstExistingJournal)
            {
                base.DataAccessManager.Delete<Journal>(journal.JournalID);
            }
        }


        public int InsertJournalInformationForGoodsDamage(VMDamage vmDamage)
        {
            int result = 0;
            int referenceID = 1;
            decimal amount = CalculatePurchasePrice(vmDamage.lstDamageDetail);

            referenceID = new JournalManager().GetJournalReferenceID();
            for (int i = 0; i <= 1; i++)
            {
                IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();

                if (i == 0)
                {
                    journal.DrCrIndecator = "Dr";
                    journal.AccountID = IFMSConstant.OperatingExpense;
                }
                else
                {
                    journal.DrCrIndecator = "Cr";
                    journal.AccountID = IFMSConstant.InventoryAccountID;
                }

                journal.DamageInfoID = vmDamage.DamageInfo.DamageInfoID;
                journal.JAccountID = 0;
                journal.Amount = amount;
                journal.ReferenceNo = referenceID;
                journal.BranchID = MTBFConstants.AppConstants.BranchID;
                journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                base.DataAccessManager.Insert<Journal>(journal);
            }

            return result;
        }

        private decimal CalculatePurchasePrice(List<DamageDetail> lstDamageDetail)
        {
            decimal total = 0;
            List<PurchaseOrderDetail> lstPurchaseOrderDetail = new List<PurchaseOrderDetail>();
            string[] productID = lstDamageDetail.Select(i => i.ProductCode).Distinct().ToArray();
            string filter = string.Empty;


            if (productID.Length > 0)
            {
                for (int i = 0; i < productID.Length; i++)
                {
                    if (filter != string.Empty) filter = filter + ",";
                    filter = filter + "'" + productID[i] + "'";
                }

                filter = String.Format("{0} IN ({1})", "ProductID", filter);
                lstPurchaseOrderDetail = base.DataAccessManager.GetFilteredData<PurchaseOrderDetail>(filter).Cast<PurchaseOrderDetail>().ToList();

            }

            foreach (DamageDetail damageDetail in lstDamageDetail)
            {
                decimal price = CalculateAveragePrice(lstPurchaseOrderDetail.Where(p => p.ProductID == damageDetail.ProductCode).Cast<PurchaseOrderDetail>().ToList());


                total += damageDetail.Quantity * price;
            }

            return total;
        }

        private decimal CalculateAveragePrice(List<PurchaseOrderDetail> lstPurchaseOrderDetail)
        {
            decimal price = 0;
            decimal total = 0;


            foreach (PurchaseOrderDetail detail in lstPurchaseOrderDetail)
            {
                total += detail.PurchasePrice;
            }

            price = (lstPurchaseOrderDetail.Count > 0) ? (total / lstPurchaseOrderDetail.Count) : 0;

            return price;
        }


        private void InsertDamageDetailInformation(VMDamage vmDamage)
        {
            foreach (DamageDetail damageDetail in vmDamage.lstDamageDetail)
            {
                damageDetail.DamageInfoID = vmDamage.DamageInfo.DamageInfoID;
                base.DataAccessManager.Insert<DamageDetail>(damageDetail);

            }
        }



        private void DeleteAllPreviousDetail(DamageInfo damageInfo)
        {
            foreach (DamageDetail damageDetail in GetAllDamageDetailByDamageInfoID(damageInfo.DamageInfoID))
            {
                base.DataAccessManager.Delete<DamageDetail>(damageDetail.DamageDetailID);
            }
        }


        public List<DamageDetail> GetAllDamageDetailByDamageInfoID(int damageInfoID)
        {
            string filter = string.Format("{0}={1}", "DamageInfoID", damageInfoID);
            return base.DataAccessManager.GetFilteredData<DamageDetail>(filter).Cast<DamageDetail>().ToList();

        }

        public DamageInfo GetDamageInfoByID(int damageInfoID)
        {
            return base.DataAccessManager.GetByID<DamageInfo>(damageInfoID, "DamageInfoID");
        }

        public List<DamageInfo> GetAllDamageInfo()
        {
            return base.DataAccessManager.GetAll<DamageInfo>().Cast<DamageInfo>().ToList();
        }

        public List<DamageInfo> GetAllDamageInfoByDate(string fromDate, string toDate)
        {
            string filter = string.Format("{0} between '{1}' and '{2}'", "DamageDate", fromDate, toDate);
            return base.DataAccessManager.GetFilteredData<DamageInfo>(filter).Cast<DamageInfo>().ToList();
        }

        public decimal GetAllDamageProductByProudctCode(string productCode, int branchID, int organizationID)
        {
            string filter = string.Format(IFMSConstant.QueryConstants.GET_DAMAGE_PRODUCT_BY_PRODUCTID, productCode, branchID, organizationID);
            return Convert.ToDecimal(base.DataAccessManager.ExecuteScalar(filter));
        }


        public List<DamageDetail> GetAllDamageProductByProductCodeAndDate(int branchID, int organizationId, string fromDate, string toDate)
        {
            string filter = string.Format(IFMSConstant.QueryConstants.GET_DAMAGE_PRODUCT_BY_PRODUCTID_AND_DATE, branchID, organizationId, fromDate, toDate);
            return base.DataAccessManager.ExecuteSQL<DamageDetail>(filter).Cast<DamageDetail>().ToList();
        }

        public List<DamageDetail> GetFilteredDamageDetail(string filter)
        {
            return base.DataAccessManager.GetFilteredData<DamageDetail>(filter).Cast<DamageDetail>().ToList();
        }


        public List<DamageDetail> GetAllDamageDetailByDateFilter(string filter)
        {
            List<DamageDetail> lstDamageDetail = new List<DamageDetail>();

            foreach (DamageInfo damageInfo in base.DataAccessManager.GetFilteredData<DamageInfo>(filter))
            {
                lstDamageDetail.AddRange(GetAllDamageDetailByDamageInfoID(damageInfo.DamageInfoID));
            }

            return lstDamageDetail;
        }

        public List<DamageInfo> GetFilteredDamageInfo(string filter)
        {
            return base.DataAccessManager.GetFilteredData<DamageInfo>(filter).Cast<DamageInfo>().ToList();
        }


        public List<DamageReport> GetDamageReport(string fromDate, string toDate, int branchID, int organizationID)
        {
            string filter = string.Format(IFMSConstant.QueryConstants.GET_DAMAGE_PRODUCT_HISTORY_AND_DATE, fromDate, toDate, branchID, organizationID);
            return base.DataAccessManager.ExecuteSQL<DamageReport>(filter).Cast<DamageReport>().ToList();
        }

        public int CanceleDamage(int damageInfoID)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                DamageInfo damageInfo = GetDamageInfoByID(damageInfoID);

                List<DamageDetail> lstDamageDetial = GetAllDamageDetailByDamageInfoID(damageInfoID);

                damageInfo.Status = (int)MTBFEnums.DamageStatus.Cancelled;
                base.DataAccessManager.Update<DamageInfo>(damageInfo);

                foreach (DamageDetail dDetail in lstDamageDetial)
                {
                    dDetail.Quantity = 0;
                    base.DataAccessManager.Update<DamageDetail>(dDetail);
                }

            }
            catch (Exception)
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }

            return result;
        }
    }
}
