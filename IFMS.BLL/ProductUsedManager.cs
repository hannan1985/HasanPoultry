using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.View;
using NybSys.MTBF.Utility.Enums;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Constants;

namespace IFMS.BLL
{
    public class ProductUsedManager : BllBase
    {
        public int SaveProductUsed(VMProductUsed vmProductUsed)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                base.DataAccessManager.BeginTransaction();

                if (vmProductUsed.ProductUsed.ProductUsedID > 0)
                {
                    vmProductUsed.ProductUsed.UpdatedBy = MTBFConstants.AppConstants.LoggedinUserID;
                    vmProductUsed.ProductUsed.UpdatedDate = DateTime.Now;
                    base.DataAccessManager.Update<ProductUsed>(vmProductUsed.ProductUsed);

                    string filter = string.Format("{0}={1}", "ProductUsedID", vmProductUsed.ProductUsed.ProductUsedID);
                    List<ProductUsedDetail> lstExistingUseDetail = new List<ProductUsedDetail>();
                    lstExistingUseDetail = base.DataAccessManager.GetFilteredData<ProductUsedDetail>(filter).Cast<ProductUsedDetail>().ToList();
                    foreach (ProductUsedDetail pUsedDetail in lstExistingUseDetail)
                    {
                        base.DataAccessManager.Delete<ProductUsedDetail>(pUsedDetail.ProductUsedDetailID);
                    }

                    foreach (ProductUsedDetail useDetail in vmProductUsed.lstProductUsedDetail)
                    {
                        useDetail.ProductUsedID = vmProductUsed.ProductUsed.ProductUsedID;
                        base.DataAccessManager.Insert<ProductUsedDetail>(useDetail);
                    }
                }
                else
                {
                    vmProductUsed.ProductUsed.CreatedBy = MTBFConstants.AppConstants.LoggedinUserID;
                    vmProductUsed.ProductUsed.CreatedDate = DateTime.Now;
                    vmProductUsed.ProductUsed.UpdatedDate = Convert.ToDateTime(MTBFConstants.DefauldDate);
                    vmProductUsed.ProductUsed.ProductUsedID = base.DataAccessManager.Insert<ProductUsed>(vmProductUsed.ProductUsed);

                    foreach (ProductUsedDetail useDetail in vmProductUsed.lstProductUsedDetail)
                    {
                        useDetail.ProductUsedID = vmProductUsed.ProductUsed.ProductUsedID;
                        base.DataAccessManager.Insert<ProductUsedDetail>(useDetail);
                    }

                   
                }

                vmProductUsed.VMReceiveProduct.ReceiveProduct.ProductUsedID = vmProductUsed.ProductUsed.ProductUsedID;
                vmProductUsed.VMReceiveProduct.ReceiveProduct.IsReceiveFromProduction = true;




                if (vmProductUsed.VMReceiveProduct.ReceiveProduct.ReceiveProductID > 0)
                {
                    base.DataAccessManager.Update<ReceiveProduct>(vmProductUsed.VMReceiveProduct.ReceiveProduct);

                    string filter = string.Format("{0}={1}", "ReceiveProductID", vmProductUsed.VMReceiveProduct.ReceiveProduct.ReceiveProductID);
                    List<ReceiveProductDetail> lstExisting = new List<ReceiveProductDetail>();
                    lstExisting = base.DataAccessManager.GetFilteredData<ReceiveProductDetail>(filter).Cast<ReceiveProductDetail>().ToList();
                    foreach (ReceiveProductDetail rDetail in lstExisting)
                    {
                        base.DataAccessManager.Delete<ReceiveProductDetail>(rDetail.ReceiveProductDetailID);
                    }

                    foreach (ReceiveProductDetail receiveDetail in vmProductUsed.VMReceiveProduct.lstRecevieProductDetail)
                    {
                        receiveDetail.ReceiveProductID = vmProductUsed.VMReceiveProduct.ReceiveProduct.ReceiveProductID;
                        base.DataAccessManager.Insert<ReceiveProductDetail>(receiveDetail);
                    }
                }
                else
                {
                    vmProductUsed.VMReceiveProduct.ReceiveProduct.ReceiveDate = DateTime.Now;
                    vmProductUsed.VMReceiveProduct.ReceiveProduct.ReceiveProductID = base.DataAccessManager.Insert<ReceiveProduct>(vmProductUsed.VMReceiveProduct.ReceiveProduct);

                    foreach (ReceiveProductDetail receiveDetail in vmProductUsed.VMReceiveProduct.lstRecevieProductDetail)
                    {
                        receiveDetail.ReceiveProductID = vmProductUsed.VMReceiveProduct.ReceiveProduct.ReceiveProductID;
                        base.DataAccessManager.Insert<ReceiveProductDetail>(receiveDetail);
                    }
                }




                base.DataAccessManager.CommitTransaction();
            }
            catch (Exception)
            {
                base.DataAccessManager.Rollback();
            }
            finally
            {
                base.DataAccessManager.EndTransaction();
            }


            return result;
        }

        public List<VMProductUsed> GetFilteredProductUsed(string filter)
        {
            List<VMProductUsed> lstVMProductUsed = new List<VMProductUsed>();
            List<ProductUsed> lstProductUsed = new List<ProductUsed>();
            lstProductUsed = base.DataAccessManager.GetFilteredData<ProductUsed>(filter).Cast<ProductUsed>().ToList();

            foreach (ProductUsed productUsed in lstProductUsed)
            {
                VMProductUsed vmProductUsed = new VMProductUsed();
                vmProductUsed.ProductUsed = productUsed;
                filter = string.Format("{0}={1}", "ProductUsedID", vmProductUsed.ProductUsed.ProductUsedID);
                vmProductUsed.lstProductUsedDetail = base.DataAccessManager.GetFilteredData<ProductUsedDetail>(filter).Cast<ProductUsedDetail>().ToList();
                lstVMProductUsed.Add(vmProductUsed);
            }

            return lstVMProductUsed;
        }


        public List<Inventroy> GetPartyStock(int partyID)
        {
            string sql = @"Select tbl.ProductTypeID, tbl.ProductID, tbl.ProductName, sum(tbl.TransferQty)TransferQty, sum(tbl.UsedQty) UsedQty, (sum(tbl.TransferQty)-sum(tbl.UsedQty)) Quantity from (

                    Select tp.ProductTypeID, td.ProductCode ProductID, td.ProductName, sum(td.Quantity) TransferQty, 0 UsedQty from TransferDetail td
                    left outer join Transfer t
                    on td.TransferID=t.TransferID
                    left outer join ProductInformation p
                    on td.ProductCode=p.ProductID
                    left outer join TypeInformation tp
                    on p.ProductTypeID=tp.ProductTypeID
                    where t.SalesCenterID=" + partyID + @"
                    group by tp.ProductTypeID,td.ProductCode , td.ProductName
                    union all

                    Select t.ProductTypeID, pd.ProductID,p.ProductName,0 TransferQty,  sum(pd.Quantity)UsedQty  from ProductUsedDetail pd
                    left outer join dbo.ProductUsed pu
                    on pd.ProductUsedID=pu.ProductUsedID
                    left outer join ProductInformation p
                    on pd.ProductID=p.ProductID
                    left outer join TypeInformation t
                    on p.ProductTypeID=t.ProductTypeID
                    Where pu.PartyID=" + partyID + @"
                    group by t.ProductTypeID,pd.ProductID,p.ProductName

                    union all
                    Select t.ProductTypeID, pd.ProductCode ProductID,p.ProductName,0 TransferQty,  sum(pd.Quantity) UsedQty  
                    from ReceiveProductDetail pd
                    left outer join dbo.ReceiveProduct pu
                    on pd.ReceiveProductID=pu.ReceiveProductID
                    left outer join ProductInformation p
                    on pd.ProductCode=p.ProductID
                    left outer join TypeInformation t
                    on p.ProductTypeID=t.ProductTypeID
                    Where pu.SalesCenterID=" + partyID + @"
                    group by t.ProductTypeID,pd.ProductCode,p.ProductName
                    )tbl

                    group by tbl.ProductTypeID, tbl.ProductID, tbl.ProductName";

            return base.DataAccessManager.ExecuteSQL<Inventroy>(sql).Cast<Inventroy>().ToList();
        }

        public List<ProductConsumtionAndReceive> GetProductUsedAndReceieveInfo(int partyID)
        {
            string sql = @"Select tbl.PartyID, tbl.Date, tbl.ProductID, sum(tbl.UsedQty) UsedQty, sum(tbl.ReceivedQty)ReceivedQty from(
                        Select pu.PartyID, pu.UsedDate Date, pud.ProductID, pud.Quantity UsedQty, 0 ReceivedQty from dbo.ProductUsedDetail pud
                        left outer join dbo.ProductUsed pu
                        on pud.ProductUsedID=pu.ProductUsedID
                        union all

                        Select rp.SalesCenterID PartyID, rp.ReceiveDate Date,  rpd.ProductCode ProductID, 0 UsedQty, rpd.Quantity  ReceivedQty  from dbo.ReceiveProductDetail rpd
                        left outer join dbo.ReceiveProduct rp
                        on rpd.ReceiveProductID=rp.ReceiveProductID)
                        tbl where tbl.PartyID="+partyID+@"
                        group by tbl.PartyID, tbl.Date, tbl.ProductID";

            return base.DataAccessManager.ExecuteSQL<ProductConsumtionAndReceive>(sql).Cast<ProductConsumtionAndReceive>().ToList();
        }
    }
}
