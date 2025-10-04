using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;
using IMFS.Common.Utility;
using IMFS.Common.View;
using System.Collections;
using NybSys.MTBF.Utility.Constants;
using NybSys.MTBF.Utility.Enums;

namespace IFMS.BLL
{
    public class PurchaseManager : BllBase
    {
        private LoginManager loginManger = new LoginManager();
        private ChildAccountManager childAccountManger = new ChildAccountManager();
        public int InsertPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            try
            {
                int result = base.DataAccessManager.Insert<PurchaseOrder>(purchaseOrder);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public int UpdatePurchaseOrder(PurchaseOrder purchaseOrder)
        {
            try
            {
                int result = base.DataAccessManager.Update<PurchaseOrder>(purchaseOrder);
                return result;
            }
            catch
            {
                throw;
            }
        }


        public List<PurchaseOrder> GetAllPurchaseOrder()
        {
            return base.DataAccessManager.GetAll<PurchaseOrder>().Cast<PurchaseOrder>().ToList(); ;
        }


        //* purchaseOrderDetail*//


        public int InsertPurchaseOrderDetail(List<PurchaseOrderDetail> lstPurchaseOrderDetail)
        {
            try
            {

                foreach (PurchaseOrderDetail purchaseOrderDetail in lstPurchaseOrderDetail)
                {
                    base.DataAccessManager.Insert<PurchaseOrderDetail>(purchaseOrderDetail);
                }

                return 1;
            }
            catch
            {
                throw;
            }
        }

        public int UpdatePurchaseOrderDetail(PurchaseOrderDetail purchaseOrderDetail)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                base.DataAccessManager.Update<PurchaseOrderDetail>(purchaseOrderDetail);
            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }


        public System.Collections.IList GetAllPurchaseOrderDetail()
        {
            return base.DataAccessManager.GetAll<PurchaseOrderDetail>();
        }


        /* Price Information*/

        public int InsertPriceInformation(PriceInformation priceInformation)
        {
            try
            {
                int result = base.DataAccessManager.Insert<PriceInformation>(priceInformation);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public int UpdatePriceInformation(PriceInformation priceInformation)
        {
            try
            {
                int result = base.DataAccessManager.Update<PriceInformation>(priceInformation);
                return result;
            }
            catch
            {
                throw;
            }
        }


        public PriceInformation GetPriceInformationByProductID(string productID)
        {
            try
            {
                string filter = string.Format("{0}='{1}'", "ProductID", productID);
                return base.DataAccessManager.GetFilteredData<PriceInformation>(filter).Cast<PriceInformation>().ToList().FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }



        public System.Collections.IList GetOrderableProductByCompanyID(int supplierID)
        {
            string filter = string.Format(IFMSConstant.QueryConstants.GetReorderProduct, supplierID);
            return base.DataAccessManager.ExecuteSQL<ReorderProduct>(filter).Cast<ReorderProduct>().ToList();
        }

        public List<SalesAndPurchaseReport> GetAllPurchaseInformationAccordingToDate(string _fromDate, string _toDate, int branchID, int organizationID)
        {
            string sql = @"Select po.InvoiceNumber ,po.PurchaseDate, s.SupplierName , po.PurchaseAmount as Total from PurchaseOrder po 
                            join Suppliers s on s.SupplierID=po.SupplierID
                        Where po.PurchaseDate between '{0}' and '{1}' and po.BranchID={2}";
            string filter = string.Format(sql, _fromDate, _toDate, branchID);
            return base.DataAccessManager.ExecuteSQL<SalesAndPurchaseReport>(filter).Cast<SalesAndPurchaseReport>().ToList();
        }

        //public List<SalesAndPurchaseReport> GetAllPurchaseInformationAccordingToDate(string _fromDate, string _toDate, int branchID, int organizationID)
        //        {
        //            string filter = string.Format(IFMSConstant.QueryConstants.PurchaseReportAccordingToDate, _fromDate, _toDate, branchID, organizationID);
        //            return base.DataAccessManager.ExecuteSQL<SalesAndPurchaseReport>(filter).Cast<SalesAndPurchaseReport>().ToList();
        //        }
        public System.Collections.IList GetPurchaseOrderBySupplierID(int supplierID)
        {
            string filter = String.Format("{0}={1} and (PurchaseAmount -  PaidAmount)>0 ", "SupplierID", supplierID);
            return base.DataAccessManager.GetFilteredData<PurchaseOrder>(filter).Cast<PurchaseOrder>().ToList();
        }




        public PurchaseOrderDetail GetPurchaseOrderDetailByProductAndPurchaseID(string productID, int _purchaseID)
        {
            string filter = String.Format("{0}='{1}' AND {2}={3}", "ProductID", productID, "PurchaseID", _purchaseID);
            return base.DataAccessManager.GetFilteredData<PurchaseOrderDetail>(filter).Cast<PurchaseOrderDetail>().ToList().FirstOrDefault();
        }

        public IList GetAllPurchaseOrderDetailProductByProductAndPurchaseID(string productID, int _purchaseID)
        {
            string filter = String.Format("{0}='{1}' AND {2}={3}", "ProductID", productID, "PurchaseID", _purchaseID);
            return base.DataAccessManager.GetFilteredData<PurchaseOrderDetail>(filter).Cast<PurchaseOrderDetail>().ToList();
        }


        public List<SalesOrderDetail> GetAllSalesProductByProductAndPurchaseID(string productID, int _purchaseID)
        {
            string filter = String.Format("{0}='{1}' AND {2}={3}", "ProductID", productID, "PurchaseID", _purchaseID);
            return base.DataAccessManager.GetFilteredData<SalesOrderDetail>(filter).Cast<SalesOrderDetail>().ToList();
        }

        public List<PurchaseOrderDetail> GetAllPurchaseOrderDetailByPurchaseID(int purchaseOrderID)
        {
            string filter = String.Format("{0}={1}", "PurchaseID", purchaseOrderID);
            return base.DataAccessManager.GetFilteredData<PurchaseOrderDetail>(filter).Cast<PurchaseOrderDetail>().ToList();
        }

        public int InsertPurchaseOrderInformation(PurchaseOrder purchaseOrder, List<PurchaseOrderDetail> lstPurchaseOrderDetail, string userID, ChildAccount supplierChildAccount)
        {
            int result = (int)IFMSEnum.ReturnResult.Success;
            int purchaseID = 0;
            base.DataAccessManager.BeginTransaction();
            try
            {
                purchaseID = base.DataAccessManager.Insert<PurchaseOrder>(purchaseOrder);
                purchaseOrder.PurchaseID = purchaseID;
                InsertPurchaseOrderDetail(purchaseOrder, lstPurchaseOrderDetail);
                base.DataAccessManager.CommitTransaction();
            }
            catch (Exception)
            {
                base.DataAccessManager.Rollback();
                result = (int)IFMSEnum.ReturnResult.Failed;
            }
            finally
            {
                base.DataAccessManager.EndTransaction();
            }

            return result;
        }


        private void InsertPurchaseOrderDetail(PurchaseOrder purchaseOrder, List<PurchaseOrderDetail> lstPurchaseOrderDetail)
        {
            foreach (PurchaseOrderDetail pDetail in lstPurchaseOrderDetail)
            {
                pDetail.PurchaseID = purchaseOrder.PurchaseID;
                base.DataAccessManager.Insert<PurchaseOrderDetail>(pDetail);
            }
        }


        public int GetJournalReferenceID()
        {
            int referenceID = 0;
            referenceID = Convert.ToInt32(base.DataAccessManager.ExecuteScalar("Select isnull(max(ReferenceNo),0)+1  from Journal "));
            return referenceID;
        }


        private void InsertJournalInformationForCashPayment(PurchaseOrder purchaseOrder, ChildAccount supplierChildAccount, string userID)
        {
            int referenceID = 1;
            Users user = base.DataAccessManager.GetByCode<Users>(userID, "UserID");
            UserSettings userSetting = loginManger.GetUserSettingsByEmployeeID(user.EmployeeID);

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
                    if (userSetting != null)
                    {
                        journal.ChildAccountID = userSetting.PurchaseAccountID;
                    }
                    else
                    {
                        journal.AccountID = IFMSConstant.CashAccountID;
                    }
                }

                journal.JAccountID = 0;
                journal.Amount = purchaseOrder.PaidAmount;
                journal.ReferenceNo = referenceID;
                journal.PurchaseID = purchaseOrder.PurchaseID;
                journal.JournalType = (int)MTBFEnums.JournalType.Inventory;
                journal.BranchID = purchaseOrder.BranchID;
                journal.OrganizationID = purchaseOrder.OrganizationID;
                base.DataAccessManager.Insert<Journal>(journal);
            }
        }

        private void InsertJournalInformationForGoodsReceive(PurchaseOrder purchaseOrder, ChildAccount supplierChildAccount)
        {
            int referenceID = GetJournalReferenceID();

            for (int i = 0; i <= 1; i++)
            {
                Journal journal = new Journal();

                if (i == 0)
                {
                    journal.DrCrIndecator = "Dr";
                    journal.AccountID = IFMSConstant.InventoryAccountID;
                }
                else
                {
                    journal.DrCrIndecator = "Cr";
                    journal.AccountID = supplierChildAccount.AccountID;
                    journal.ChildAccountID = supplierChildAccount.ChildAccountID;
                }

                journal.JAccountID = 0;
                journal.Amount = purchaseOrder.PurchaseAmount;
                journal.ReferenceNo = referenceID;
                journal.PurchaseID = purchaseOrder.PurchaseID;
                journal.JournalType = (int)MTBFEnums.JournalType.Inventory;
                journal.BranchID = purchaseOrder.BranchID;
                journal.OrganizationID = purchaseOrder.OrganizationID;
                base.DataAccessManager.Insert<Journal>(journal);
            }
        }


        public PurchaseOrder GetPurchaseOrderByID(int _purchaseOrderID)
        {
            return base.DataAccessManager.GetByID<PurchaseOrder>(_purchaseOrderID, "PurchaseID");
        }

        private List<Journal> GetAllJournalByPurchaseID(int purchaseID)
        {
            string filter = string.Format("{0}={1}", "PurchaseID", purchaseID);
            return base.DataAccessManager.GetFilteredData<Journal>(filter).Cast<Journal>().ToList();
        }

        public int UpdatePurchaseOrderInformation(PurchaseOrder purchaseOrder, List<PurchaseOrderDetail> lstPurchaseOrderDetail, string userID, ChildAccount supplierChildAccount)
        {
            int result = (int)IFMSEnum.ReturnResult.Success;
            List<PurchaseOrderDetail> lstDeletePurchaseOrderDetail = GetAllPurchaseOrderDetailByPurchaseID(purchaseOrder.PurchaseID).Cast<PurchaseOrderDetail>().ToList();
            base.DataAccessManager.BeginTransaction();
            try
            {
                base.DataAccessManager.Update<PurchaseOrder>(purchaseOrder);
                DeletePurchaseOrderDetailInformation(lstDeletePurchaseOrderDetail);
                InsertPurchaseOrderDetail(purchaseOrder, lstPurchaseOrderDetail);

                base.DataAccessManager.CommitTransaction();
            }
            catch (Exception)
            {
                base.DataAccessManager.Rollback();
                result = (int)IFMSEnum.ReturnResult.Failed;
            }
            finally
            {
                base.DataAccessManager.EndTransaction();
            }

            return result;
        }

        private void DeletePurchaseOrderDetailInformation(List<PurchaseOrderDetail> lstPurchaseOrderDetail)
        {
            foreach (PurchaseOrderDetail pDetail in lstPurchaseOrderDetail)
            {
                base.DataAccessManager.Delete<PurchaseOrderDetail>(pDetail.SerialNo);
            }
        }

        private void DeleteJournalInformation(List<Journal> lstJournal)
        {
            foreach (Journal journal in lstJournal)
            {
                base.DataAccessManager.Delete<Journal>(journal.JournalID);
            }
        }



        public int ApprovePurchaseOrder(PurchaseOrder purchaseOrder)
        {
            int result = (int)IFMSEnum.ReturnResult.Success;
            base.DataAccessManager.BeginTransaction();
            try
            {
                ChildAccount childAccount = new ChildAccount();
                if (purchaseOrder.SupplierID > 0)
                {
                    childAccount = childAccountManger.GetChildAccountBySupplierID(purchaseOrder.SupplierID);
                }
                else
                {
                    SalesOrder salesOrder = new SalesManager().GetSalesOrderByID(purchaseOrder.SalesID);
                    childAccount = childAccountManger.GetChildAccountByCustomerID(salesOrder.CustomerID);
                }

                base.DataAccessManager.Update<PurchaseOrder>(purchaseOrder);
                InsertJournalInformationForGoodsReceive(purchaseOrder, childAccount);
                if (purchaseOrder.PaidAmount > 0)
                {
                    InsertJournalInformationForCashPayment(purchaseOrder, childAccount, MTBFConstants.AppConstants.LoggedinUserID);
                }

                base.DataAccessManager.CommitTransaction();
            }
            catch (Exception)
            {
                base.DataAccessManager.Rollback();
                result = (int)IFMSEnum.ReturnResult.Failed;
            }
            finally
            {
                base.DataAccessManager.EndTransaction();

            }
            return result;
        }

        public PurchaseOrder GetPurchaseOrderBySalesID(int salesId)
        {
            string filter = string.Format("{0}={1}", "SalesID", salesId);
            return base.DataAccessManager.GetFilteredData<PurchaseOrder>(filter).Cast<PurchaseOrder>().ToList().FirstOrDefault();
        }

        public List<PurchaseOrderDetail> GetPurchaseOrderDetailByProductID(string productID, int branchID, int organizationID)
        {
            string filter = string.Format("{0} ='{1}'", "ProductID", productID);
            List<PurchaseOrderDetail> lstPurchaseOrderDetail = new List<PurchaseOrderDetail>();

            foreach (PurchaseOrderDetail pDetail in base.DataAccessManager.GetFilteredData<PurchaseOrderDetail>(filter).Cast<PurchaseOrderDetail>().ToList())
            {
                PurchaseOrder purchaseOrder = GetPurchaseOrderByID(pDetail.PurchaseID);
                if (purchaseOrder.BranchID == branchID && purchaseOrder.OrganizationID == organizationID && purchaseOrder.Status == (int)IFMSEnum.PurchaseOrderStatus.Approved)
                {
                    lstPurchaseOrderDetail.Add(pDetail);
                }
            }
            return lstPurchaseOrderDetail;
        }

        public List<ReceiveProductDetail> GetAllReceiveProductByProductIDAndDate(int branchId, int organizationID, string fromDate, string toDate)
        {
            string filter = string.Format(IFMSConstant.QueryConstants.GET_ALL_RECEIVE_PRODUCT_BY_PRODUCTID_DATE, branchId, organizationID, fromDate, toDate);
            return base.DataAccessManager.ExecuteSQL<ReceiveProductDetail>(filter).Cast<ReceiveProductDetail>().ToList();
        }

        public List<PurchaseOrderDetail> GetPurcahseOrderDetailFiltered(string filter)
        {
            string sql = string.Format(filter + " And SerialNo in(Select MAX(SerialNo) from PurchaseOrderDetails group by  ProductID) order by ProductID");
            return base.DataAccessManager.GetFilteredData<PurchaseOrderDetail>(sql).Cast<PurchaseOrderDetail>().ToList();

        }

        public List<PurchaseOrder> GetAllPurchaseOrderByDate(string fromDate, string toDate, int branchID, int organizationID)
        {
            string filter = string.Format("{0} between '{1}' AND '{2}' And BranchID={3} and OrganizationID={4}", "PurchaseDate", fromDate, toDate, branchID, organizationID);
            return base.DataAccessManager.GetFilteredData<PurchaseOrder>(filter).Cast<PurchaseOrder>().ToList();
        }

        public List<PurchaseOrderDetail> GetFilteredPurchaseOrderDetail(string filter)
        {
            return base.DataAccessManager.GetFilteredData<PurchaseOrderDetail>(filter).Cast<PurchaseOrderDetail>().ToList();
        }

        public int CancelPurchaseOrder(PurchaseOrder purchaseOrder)
        {

            int result = (int)IFMSEnum.ReturnResult.Success;
            List<PurchaseOrderDetail> lstPurchaseOrderDetail = new List<PurchaseOrderDetail>();
            base.DataAccessManager.BeginTransaction();
            try
            {

                lstPurchaseOrderDetail = GetAllPurchaseOrderDetailByPurchaseID(purchaseOrder.PurchaseID);
                foreach (PurchaseOrderDetail pDetail in lstPurchaseOrderDetail)
                {
                    pDetail.Quantity = 0;
                    base.DataAccessManager.Update<PurchaseOrderDetail>(pDetail);
                }

                base.DataAccessManager.Update<PurchaseOrder>(purchaseOrder);

                base.DataAccessManager.CommitTransaction();
            }
            catch (Exception)
            {
                base.DataAccessManager.Rollback();
                result = (int)IFMSEnum.ReturnResult.Failed;
            }
            finally
            {
                base.DataAccessManager.EndTransaction();

            }
            return result;
        }

        public int SavePurchaseOrderInformation(PurchaseOrder purchaseOrder, List<PurchaseOrderDetail> lstPurchaseOrderDetail)
        {
            int result = (int)IFMSEnum.ReturnResult.Success;

            ChildAccount supplierChildAccount = new ChildAccountManager().GetChildAccountBySupplierID(purchaseOrder.SupplierID);
            base.DataAccessManager.BeginTransaction();
            try
            {
                if (purchaseOrder.PurchaseID > 0)
                {
                    base.DataAccessManager.Update<PurchaseOrder>(purchaseOrder);
                }
                else
                {
                    purchaseOrder.PurchaseID = base.DataAccessManager.Insert<PurchaseOrder>(purchaseOrder);
                }

                List<PurchaseOrderDetail> lstExistingDetail = GetAllPurchaseOrderDetailByPurchaseID(purchaseOrder.PurchaseID);
                foreach (PurchaseOrderDetail pDetail in lstExistingDetail)
                {
                    base.DataAccessManager.Delete<PurchaseOrderDetail>(pDetail.SerialNo);
                }

                InsertPurchaseOrderDetail(purchaseOrder, lstPurchaseOrderDetail);
                base.DataAccessManager.CommitTransaction();
            }
            catch (Exception)
            {
                base.DataAccessManager.Rollback();
                result = (int)IFMSEnum.ReturnResult.Failed;
            }
            finally
            {
                base.DataAccessManager.EndTransaction();
            }

            return result;
        }


        public List<PurchaseOrder> GetFilteredPurchaseOrder(string filter)
        {
            return base.DataAccessManager.GetFilteredData<PurchaseOrder>(filter).Cast<PurchaseOrder>().ToList();
        }

        public List<PurchaseReport> GetPurchaseInformationByID(int purchaseID)
        {

            string sql = @"Select po.PurchaseDate,po.InvoiceNumber, p.ProductName,pm.Name Model,p.Unit, isnull(p.CartonSize,1) CartonSize,  po.PurchaseID, po.InvoiceNumber, isnull(p.CartonSize,1) CartonSize, pod.PurchasePrice, pod.Quantity, 
                            po.PaidAmount, (pod.Quantity* pod.PurchasePrice) Total, ( po.PurchaseAmount- po.PaidAmount) Due  from PurchaseOrder po
                            left outer join PurchaseOrderDetails pod
                            on po.PurchaseID=pod.PurchaseID
                            left outer join ProductInformation p 
                            left outer join ProductModel pm 
                            on p.ProductModelID=pm.ProductModelID
                            left outer join TypeInformation pt 
                            on p.ProductTypeID=pt.ProductTypeID
                            on pod.ProductID=p.ProductID
                        Where po.PurchaseID={0}";
            sql = string.Format(sql, purchaseID);

            return base.DataAccessManager.ExecuteSQL<PurchaseReport>(sql).Cast<PurchaseReport>().ToList();
        }

        public PurchaseOrderDetail GetPurchaseOrderDetailByID(int _PurcahseOrderDetailID)
        {
            return base.DataAccessManager.GetByID<PurchaseOrderDetail>(_PurcahseOrderDetailID, "SerialNo");
        }
    }
}
