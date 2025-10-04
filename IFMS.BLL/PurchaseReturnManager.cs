using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.View;
using NybSys.MTBF.Utility.Enums;
using IMFS.Common.DTO;
using IMFS.Common.Utility;
using NybSys.MTBF.Utility.Constants;

namespace IFMS.BLL
{
    public class PurchaseReturnManager : BllBase
    {
        public int SavePurcahseReturn(VMPurchaseReturn vmPurchaseReturn)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                base.DataAccessManager.BeginTransaction();

                string filter = string.Format("{0}='{1}' and {2}={3}", "ReferenceType", (int)MTBFEnums.ChildAccountType.Supplier, "ReferenceID", vmPurchaseReturn.purchaseReturn.SupplierID);
                ChildAccount supplierChildAccount = base.DataAccessManager.GetFilteredData<ChildAccount>(filter).Cast<ChildAccount>().ToList().FirstOrDefault();


                if (vmPurchaseReturn.purchaseReturn.PurchaseReturnID > 0)
                {
                    base.DataAccessManager.Update<PurchaseReturn>(vmPurchaseReturn.purchaseReturn);

                    List<Journal> lstExistingJournal = new List<Journal>();
                    filter = string.Format("{0}={1}", "PurchaseReturnID", vmPurchaseReturn.purchaseReturn.PurchaseReturnID);
                    lstExistingJournal = base.DataAccessManager.GetFilteredData<Journal>(filter).Cast<Journal>().ToList();
                    DeleteJournal(lstExistingJournal);
                }
                else
                {
                    vmPurchaseReturn.purchaseReturn.CreatedDate = DateTime.Now;
                    vmPurchaseReturn.purchaseReturn.PurchaseReturnID = base.DataAccessManager.Insert<PurchaseReturn>(vmPurchaseReturn.purchaseReturn);
                }


                List<PurchaseReturnDetail> lstExistingPurcahseReturnDetail = new List<PurchaseReturnDetail>();
                lstExistingPurcahseReturnDetail = GetAllPurchaseReturnDetailByPurchaseReturnID(vmPurchaseReturn.purchaseReturn.PurchaseReturnID);

                foreach (PurchaseReturnDetail exPRDetil in lstExistingPurcahseReturnDetail)
                {
                    base.DataAccessManager.Delete<PurchaseReturnDetail>(exPRDetil.PurchaseReturnDetailID);
                }


                foreach (PurchaseReturnDetail prDetail in vmPurchaseReturn.lstPurchaseReturnDetail)
                {
                    if (prDetail.PurchaseReturnDetailID > 0)
                    {
                        base.DataAccessManager.Update<PurchaseReturnDetail>(prDetail);
                    }
                    else
                    {
                        prDetail.PurchaseReturnID = vmPurchaseReturn.purchaseReturn.PurchaseReturnID;
                        base.DataAccessManager.Insert<PurchaseReturnDetail>(prDetail);
                    }
                }

                InsertJournalInformationForGoodsReturn(vmPurchaseReturn, supplierChildAccount);
                if (vmPurchaseReturn.purchaseReturn.ReceiveAmount > 0)
                {
                    InsertJournalInformationForCashReceive(vmPurchaseReturn, supplierChildAccount);
                }

                base.DataAccessManager.CommitTransaction();
            }
            catch (Exception ex)
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

        private void DeleteJournal(List<Journal> lstExistingJournal)
        {
            foreach (Journal journal in lstExistingJournal)
            {
                base.DataAccessManager.Delete<Journal>(journal.JournalID);
            }
        }



        public int InsertJournalInformationForGoodsReturn(VMPurchaseReturn vmPurchaseReturn, ChildAccount supplierChildAccount)
        {
            int result = 0;
            int referenceID = 1;
            decimal amount = CalculatePurchasePrice(vmPurchaseReturn.lstPurchaseReturnDetail);



            referenceID = new JournalManager().GetJournalReferenceID();
            for (int i = 0; i <= 1; i++)
            {
                IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();

                if (i == 0)
                {
                    journal.DrCrIndecator = "Dr";
                    journal.AccountID = supplierChildAccount.AccountID;
                    journal.ChildAccountID = supplierChildAccount.ChildAccountID;
                }
                else
                {
                    journal.DrCrIndecator = "Cr";
                    journal.AccountID = IFMSConstant.InventoryAccountID;
                }

                journal.PurchaseRetrunID = vmPurchaseReturn.purchaseReturn.PurchaseReturnID;
                journal.JAccountID = 0;
                journal.Amount = amount;
                journal.ReferenceNo = referenceID;
                journal.BranchID = MTBFConstants.AppConstants.BranchID;
                journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                base.DataAccessManager.Insert<Journal>(journal);
            }

            return result;
        }


        private void InsertJournalInformationForCashReceive(VMPurchaseReturn vmPurchaseReturn, ChildAccount supplierChildAccount)
        {
            int referenceID = 1;


            referenceID = new JournalManager().GetJournalReferenceID();

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
                    journal.ChildAccountID = supplierChildAccount.ChildAccountID;
                    journal.AccountID = supplierChildAccount.AccountID;

                }

                journal.JAccountID = 0;
                journal.Amount = vmPurchaseReturn.purchaseReturn.ReceiveAmount;
                journal.ReferenceNo = referenceID;
                journal.PurchaseRetrunID = vmPurchaseReturn.purchaseReturn.PurchaseReturnID;
                journal.JournalType = (int)MTBFEnums.JournalType.Inventory;
                journal.BranchID = vmPurchaseReturn.purchaseReturn.BranchID;
                journal.OrganizationID = vmPurchaseReturn.purchaseReturn.OrganizationID;
                base.DataAccessManager.Insert<Journal>(journal);
            }
        }


        private decimal CalculatePurchasePrice(List<PurchaseReturnDetail> list)
        {
            decimal total = 0;
            foreach (PurchaseReturnDetail returnDetail in list)
            {
                total += (returnDetail.Price * returnDetail.Quantity);
            }

            return total;
        }



        public List<VMPurchaseReturn> GetAllPurchaseReturn()
        {
            List<VMPurchaseReturn> lstVMPurchaseReturn = new List<VMPurchaseReturn>();

            List<PurchaseReturn> lstPurcahseReturn = new List<PurchaseReturn>();
            lstPurcahseReturn = base.DataAccessManager.GetAll<PurchaseReturn>().Cast<PurchaseReturn>().ToList();

            foreach (PurchaseReturn pReturn in lstPurcahseReturn)
            {
                List<PurchaseReturnDetail> lstPurchaseRetunDetail = new List<PurchaseReturnDetail>();
                lstPurchaseRetunDetail = GetAllPurchaseReturnDetailByPurchaseReturnID(pReturn.PurchaseReturnID);

                VMPurchaseReturn vmPurchaseReturn = new VMPurchaseReturn();
                vmPurchaseReturn.purchaseReturn = pReturn;
                vmPurchaseReturn.lstPurchaseReturnDetail = lstPurchaseRetunDetail;
                lstVMPurchaseReturn.Add(vmPurchaseReturn);

            }

            return lstVMPurchaseReturn;
        }

        public List<VMPurchaseReturn> GetFilteredPurchaseReturn(string filter)
        {
            List<VMPurchaseReturn> lstVMPurchaseReturn = new List<VMPurchaseReturn>();

            List<PurchaseReturn> lstPurcahseReturn = new List<PurchaseReturn>();
            lstPurcahseReturn = base.DataAccessManager.GetFilteredData<PurchaseReturn>(filter).Cast<PurchaseReturn>().ToList();

            foreach (PurchaseReturn pReturn in lstPurcahseReturn)
            {
                List<PurchaseReturnDetail> lstPurchaseRetunDetail = new List<PurchaseReturnDetail>();
                lstPurchaseRetunDetail = GetAllPurchaseReturnDetailByPurchaseReturnID(pReturn.PurchaseReturnID);

                VMPurchaseReturn vmPurchaseReturn = new VMPurchaseReturn();
                vmPurchaseReturn.purchaseReturn = pReturn;
                vmPurchaseReturn.lstPurchaseReturnDetail = lstPurchaseRetunDetail;
                lstVMPurchaseReturn.Add(vmPurchaseReturn);

            }

            return lstVMPurchaseReturn;
        }

        public VMPurchaseReturn GetPurchaseReturnByID(int purcahseReturnID)
        {
            List<VMPurchaseReturn> lstVMPurchaseReturn = new List<VMPurchaseReturn>();

            PurchaseReturn purcahseReturn = new PurchaseReturn();
            purcahseReturn = base.DataAccessManager.GetByID<PurchaseReturn>(purcahseReturnID, "PurchaseReturnID");
            VMPurchaseReturn vmPurchaseReturn = new VMPurchaseReturn();
            if (purcahseReturn != null)
            {
                List<PurchaseReturnDetail> lstPurchaseRetunDetail = new List<PurchaseReturnDetail>();
                lstPurchaseRetunDetail = GetAllPurchaseReturnDetailByPurchaseReturnID(purcahseReturn.PurchaseReturnID);


                vmPurchaseReturn.purchaseReturn = purcahseReturn;
                vmPurchaseReturn.lstPurchaseReturnDetail = lstPurchaseRetunDetail;


            }

            return vmPurchaseReturn;
        }



        public List<PurchaseReturnDetail> GetAllPurchaseReturnDetailByPurchaseReturnID(int purchaseReturnID)
        {
            List<PurchaseReturnDetail> lstPurcahseReturnDetail = new List<PurchaseReturnDetail>();
            string filter = string.Format("{0}={1}", "PurchaseReturnID", purchaseReturnID);
            lstPurcahseReturnDetail = base.DataAccessManager.GetFilteredData<PurchaseReturnDetail>(filter).Cast<PurchaseReturnDetail>().ToList();
            return lstPurcahseReturnDetail;
        }

    }
}
