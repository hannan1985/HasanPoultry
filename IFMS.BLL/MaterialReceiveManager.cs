using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Enums;
using IMFS.Common.Utility;

namespace IFMS.BLL
{
    public class MaterialReceiveManager : BllBase
    {

        public int InsertMaterialsReceive(MaterialsReceive materialsReceive, List<MaterialsReceiveDetails> lstMaterialsReceiveDetail)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            int materialsReceiveID = 0;

            try
            {
                base.DataAccessManager.BeginTransaction();

                materialsReceiveID = base.DataAccessManager.Insert<MaterialsReceive>(materialsReceive);
                materialsReceive.MaterialReceiveID = materialsReceiveID;

                InsertMaterialsReceiveDetail(materialsReceive, lstMaterialsReceiveDetail);

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

        

        public int GetJournalReferenceID()
        {
            int referenceID = 0;
            referenceID = Convert.ToInt32(base.DataAccessManager.ExecuteScalar("Select isnull(max(ReferenceNo),0)+1  from Journal "));
            return referenceID;
        }


        private void InsertJournalInformationForCashPayment(MaterialsReceive materialsReceive)
        {
            int referenceID = 1;
            ChildAccount supplierChildAccount = new ChildAccount();
            string filter = string.Format("{0}={1}", "MaterailSupplierID", materialsReceive.SupplierID);
            supplierChildAccount = DataAccessManager.GetFilteredData<ChildAccount>(filter).Cast<ChildAccount>().ToList().FirstOrDefault();

            referenceID = GetJournalReferenceID();

            for (int i = 0; i <= 1; i++)
            {
                IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();

                if (i == 0)
                {
                    journal.DrCrIndecator = "Dr";
                    journal.ChildAccountID = supplierChildAccount.ChildAccountID;
                    journal.AccountID = supplierChildAccount.AccountID;
                }
                else
                {
                    journal.DrCrIndecator = "Cr";
                    journal.AccountID = IFMSConstant.CashAccountID;

                }

                journal.JAccountID = 0;

                journal.Amount = materialsReceive.PaidAmount;
                journal.ReferenceNo = referenceID;
                journal.MaterialReceiveID = materialsReceive.MaterialReceiveID;
                journal.JournalType = (int)MTBFEnums.JournalType.Production;
                journal.BranchID = materialsReceive.BranchID;
                journal.OrganizationID = materialsReceive.OrganizationID;
                base.DataAccessManager.Insert<Journal>(journal);
            }
        }

        private void InsertJournalInformationForGoodsReceive(MaterialsReceive materialsReceive)
        {
            int referenceID = GetJournalReferenceID();
            ChildAccount supplierChildAccount = new ChildAccount();
            string filter = string.Format("{0}={1}", "MaterailSupplierID", materialsReceive.SupplierID);
            supplierChildAccount = DataAccessManager.GetFilteredData<ChildAccount>(filter).Cast<ChildAccount>().ToList().FirstOrDefault();

            for (int i = 0; i <= 1; i++)
            {
                Journal journal = new Journal();

                if (i == 0)
                {
                    journal.DrCrIndecator = "Dr";
                    journal.AccountID = IFMSConstant.RawMaterialInventory;
                }
                else
                {
                    journal.DrCrIndecator = "Cr";
                    journal.AccountID = supplierChildAccount.AccountID;
                    journal.ChildAccountID = supplierChildAccount.ChildAccountID;
                }

                journal.JAccountID = 0;
                journal.Amount = materialsReceive.Total;
                journal.ReferenceNo = referenceID;
                journal.MaterialReceiveID = materialsReceive.MaterialReceiveID;
                journal.JournalType = (int)MTBFEnums.JournalType.Production;
                journal.BranchID = materialsReceive.BranchID;
                journal.OrganizationID = materialsReceive.OrganizationID;
                base.DataAccessManager.Insert<Journal>(journal);
            }
        }
        

        private void InsertMaterialsReceiveDetail(MaterialsReceive materialsReceive, List<MaterialsReceiveDetails> lstMaterialsReceiveDetail)
        {
            foreach (MaterialsReceiveDetails distributeDetail in lstMaterialsReceiveDetail)
            {
                distributeDetail.MaterialReceiveID = materialsReceive.MaterialReceiveID;
                base.DataAccessManager.Insert<MaterialsReceiveDetails>(distributeDetail);
            }
        }

        public int UpdateMaterialsReceive(MaterialsReceive MaterialsReceive, List<MaterialsReceiveDetails> lstMaterialsReceive)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            try
            {
                base.DataAccessManager.BeginTransaction();

                base.DataAccessManager.Update<MaterialsReceive>(MaterialsReceive);

                DeleteMaterialsReceiveDetail(MaterialsReceive);

                InsertMaterialsReceiveDetail(MaterialsReceive, lstMaterialsReceive);

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


        public int ApproveMaterialReceive(MaterialsReceive materialsReceive)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;     

            try
            {
                base.DataAccessManager.BeginTransaction();

                materialsReceive.Status = (int)MTBFEnums.MaterialReciveStatus.Approved;

                base.DataAccessManager.Update<MaterialsReceive>(materialsReceive);               

                if (materialsReceive.PaidAmount > 0)
                {
                    InsertJournalInformationForCashPayment(materialsReceive);
                }
                InsertJournalInformationForGoodsReceive(materialsReceive);

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


        private void DeleteMaterialsReceiveDetail(MaterialsReceive materialsReceive)
        {
            List<MaterialsReceiveDetails> lstMaterialsReceiveDetail = GetAllMaterialsReceiveDetailByMaterialsReceiveID(materialsReceive.MaterialReceiveID);

            foreach (MaterialsReceiveDetails dDetail in lstMaterialsReceiveDetail)
            {
                base.DataAccessManager.Delete<MaterialsReceiveDetails>(dDetail.MaterialRceiveDetailID);
            }
        }

        public MaterialsReceive GetMaterialsReceiveByID(int _materialsReceiveID)
        {
            MaterialsReceive MaterialsReceive = base.DataAccessManager.GetByID<MaterialsReceive>(_materialsReceiveID, "MaterialReceiveID");
            return MaterialsReceive;
        }

        public List<MaterialsReceive> GetAllMaterialsReceive()
        {
            return base.DataAccessManager.GetAll<MaterialsReceive>().Cast<MaterialsReceive>().ToList();
        }

        public List<MaterialsReceive> GetAllMaterialsReceiveByOrganizationID(int organizationID)
        {
            string filter = string.Format("{0}={1}", "OrganizationID", organizationID);
            return base.DataAccessManager.GetFilteredData<MaterialsReceive>(filter).Cast<MaterialsReceive>().ToList();

        }

        public List<MaterialsReceive> GetFilteredMaterial(string filter)
        {
            return base.DataAccessManager.GetFilteredData<MaterialsReceive>(filter).Cast<MaterialsReceive>().ToList();
        }

        public List<MaterialsReceiveDetails> GetAllMaterialsReceiveDetailByMaterialsReceiveID(int materialsReceiveID)
        {
            string filter = string.Format("{0}={1}", "MaterialReceiveID", materialsReceiveID);
            return base.DataAccessManager.GetFilteredData<MaterialsReceiveDetails>(filter).Cast<MaterialsReceiveDetails>().ToList();
        }

        public List<MaterialsReceiveDetails> GetAllMaterialStock(int branchID, int organizationID)
        {
            string filter = @"Select  MaterialID ,(Quantity -SalesQuantity ) as Quantity from (
                            Select BranchID ,OrganizationID,MaterialID  ,SUM(Quantity ) as Quantity, SUM(SalesQuantity ) as SalesQuantity from (
                            Select mr.BranchID, mr.OrganizationID,mrd.MaterialID  , SUM (mrd.Quantity) as Quantity , '0' as SalesQuantity From MaterialsReceive mr join MaterialsReceiveDetails mrd on mr.MaterialReceiveID =mrd.MaterialReceiveID 
                             group by MaterialID ,BranchID, OrganizationID 
                             union
                             Select d.BranchID, d.OrganizationID, dd.MaterialID ,'0' as Quantity,  SUM(Quantity) as SalesQuantity from Distribution d join DistributeDetails dd on d.DistributeID =dd.DistributeID 
                             group by MaterialID,BranchID, OrganizationID )tbl
                             group by MaterialID,BranchID, OrganizationID )tbl2
                             where BranchID={0} and OrganizationID={1}";
            filter = string.Format(filter, branchID, organizationID);

            return base.DataAccessManager.ExecuteSQL<MaterialsReceiveDetails>(filter).Cast<MaterialsReceiveDetails>().ToList();
        }

        public List<MaterialsReceiveDetails> GetAllMaterialsReceiveDetailByMaterialID(int materialID)
        {
            string filter = string.Format("{0}={1}", "MaterialID", materialID);
            return base.DataAccessManager.GetFilteredData<MaterialsReceiveDetails>(filter).Cast<MaterialsReceiveDetails>().ToList();
        }
    }
}
