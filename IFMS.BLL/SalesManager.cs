using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.Utility;
using IMFS.Common.DTO;
using IMFS.Common.View;
using System.Data;
using System.Reflection;
using NybSys.MTBF.Utility.Constants;
using NybSys.MTBF.Utility.Enums;

namespace IFMS.BLL
{
    public class SalesManager : BllBase
    {

        public int GetSaleMemoNumber()
        {
            string sql = "Select isnull(Max(BillNumber),100)+ 1 from CashSales where BranchID=" + MTBFConstants.AppConstants.BranchID;
            return Convert.ToInt32(base.DataAccessManager.ExecuteScalar(sql));
        }

        public int InsertSalesOrder(SalesOrder salesOrder)
        {
            try
            {
                int result = base.DataAccessManager.Insert<SalesOrder>(salesOrder);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public int UpdateSalesOrder(SalesOrder salesOrder)
        {
            try
            {
                int result = base.DataAccessManager.Update<SalesOrder>(salesOrder);
                return result;
            }
            catch
            {
                throw;
            }
        }


        public List<SalesOrder> GetAllSalesOrder()
        {
            List<SalesOrder> lstSalesOrder = new List<SalesOrder>();

            lstSalesOrder = base.DataAccessManager.GetAll<SalesOrder>().Cast<SalesOrder>().ToList();

            return lstSalesOrder;
        }

        public List<SalesOrder> GetAllSalesOrderByBranchAndOrganization(int branchID, int organizationID)
        {
            List<SalesOrder> lstSalesOrder = new List<SalesOrder>();
            string filter = string.Format("{0}={1} and {2}={3}", "BranchID", branchID, "OrganizationID", organizationID);
            lstSalesOrder = base.DataAccessManager.GetFilteredData<SalesOrder>(filter).Cast<SalesOrder>().ToList();

            return lstSalesOrder;
        }

        //* SalesOrderDetail*//


        public int InsertSalesOrderDetail(List<SalesOrderDetail> lstSalesOrderDetail)
        {
            try
            {

                foreach (SalesOrderDetail salesOrderDetail in lstSalesOrderDetail)
                {
                    base.DataAccessManager.Insert<SalesOrderDetail>(salesOrderDetail);
                }

                return 1;
            }
            catch
            {
                throw;
            }
        }

        public int UpdateSalesOrderDetail(SalesOrderDetail salesOrderDetail)
        {
            try
            {
                int result = base.DataAccessManager.Update<SalesOrderDetail>(salesOrderDetail);
                return result;
            }
            catch
            {
                throw;
            }
        }


        public System.Collections.IList GetAllSalesOrderDetail()
        {
            return base.DataAccessManager.GetAll<SalesOrderDetail>();
        }


        public System.Collections.IList GetAllCreditSales()
        {
            return base.DataAccessManager.GetAll<CreditSales>();
        }

        public decimal GetTotalSalesAmountWithOutDiscount(int salesID)
        {
            decimal netAmount = 0;
            string filter = string.Format(IFMSConstant.QueryConstants.GetTotalAmountWithoutDiscount, salesID, salesID);

            netAmount = Convert.ToDecimal(base.DataAccessManager.ExecuteScalar(filter));
            return netAmount;
        }

        public System.Data.DataTable GetSalesReport(int salesId)
        {
            List<CashSalesInvoice> cashSales = new List<CashSalesInvoice>();
            string filter = string.Format(IFMSConstant.QueryConstants.SalesReport, salesId);
            cashSales = base.DataAccessManager.ExecuteSQL<CashSalesInvoice>(filter).Cast<CashSalesInvoice>().ToList();
            return ListToDataTable(cashSales);
        }

        public DataTable GetCreditSalesReport(int salesId)
        {
            List<CreditInvoice> creditSales = new List<CreditInvoice>();
            DataTable reportTable = new DataTable();
            string filter = string.Format(IFMSConstant.QueryConstants.CreditSalesReport, salesId);
            // reportTable = (DataTable)base.DataAccessManager.ExecuteScalar(filter);
            creditSales = base.DataAccessManager.ExecuteSQL<CreditInvoice>(filter).Cast<CreditInvoice>().ToList();
            return ListToDataTable(creditSales);

        }

        public List<CreditSales> GetSalesInformationByID(int salesID)
        {
            List<CreditSales> reportTable = new List<CreditSales>();
            string filter = string.Format(IFMSConstant.QueryConstants.ViewSalesInformation, salesID);
            reportTable = base.DataAccessManager.ExecuteSQL<CreditSales>(filter).Cast<CreditSales>().ToList();
            return reportTable;
        }


        public DataTable ListToDataTable<T>(List<T> list)
        {
            DataTable dt = new DataTable();

            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                dt.Columns.Add(new DataColumn(info.Name, info.PropertyType));
            }
            foreach (T t in list)
            {
                DataRow row = dt.NewRow();
                foreach (PropertyInfo info in typeof(T).GetProperties())
                {
                    row[info.Name] = info.GetValue(t, null);
                }
                dt.Rows.Add(row);
            }
            return dt;
        }


        //public List<SalesAndPurchaseReport> GetAllSalesInformationAccordingToDate(string _fromDate, string _toDate, int branchID, int organizatonID)
        //{
        //    string filter = string.Format(IFMSConstant.QueryConstants.SalesReportAccordingToDate, _fromDate, _toDate, branchID, organizatonID);
        //    return base.DataAccessManager.ExecuteSQL<SalesAndPurchaseReport>(filter).Cast<SalesAndPurchaseReport>().ToList();
        //}
        public List<SalesAndPurchaseReport> GetAllSalesInformationAccordingToDate(string _fromDate, string _toDate, int branchID, int organizatonID)
        {
            string sql = @"	Select cs.SalesID,c.CustomerID, z.ZoneName, BillNumber, SalesDate, isnull( c.CustomerName,'Cash Sales') as CustomerName , SalesAmount, Discount , (SalesAmount -Discount) Total from CashSales cs
	                        left outer join Customer c on cs.CustomerID =c.CustomerID 
	                        left outer join Zone z on z.ZoneID=c.ZoneID
                        Where cs.SalesDate between '{0}' and '{1}' and cs.BranchID={2}";
            string filter = string.Format(sql, _fromDate, _toDate, branchID);
            return base.DataAccessManager.ExecuteSQL<SalesAndPurchaseReport>(filter).Cast<SalesAndPurchaseReport>().ToList();
        }

        public List<SalesAndPurchaseReport> GetAllZoneWiseSalesInformationAccordingToDate(string _fromDate, string _toDate, int branchID, int zoneID)
        {
            string filter = string.Empty;
            if (zoneID > 0)
            {
                filter = string.Format(IFMSConstant.QueryConstants.ZoneSalesReportAccordingToDate, _fromDate, _toDate, branchID, zoneID);
            }
            else
            {
                filter = string.Format(IFMSConstant.QueryConstants.AllZoneSalesReportAccordingToDate, _fromDate, _toDate, branchID);
            }
            return base.DataAccessManager.ExecuteSQL<SalesAndPurchaseReport>(filter).Cast<SalesAndPurchaseReport>().ToList();
        }

        public System.Collections.IList GetSalesDetailByCustomerID(int customerID)
        {
            string filter = string.Format("{0}={1} And SalesAmount > (Discount +ReceiveAmount )", "CustomerID", customerID);
            return base.DataAccessManager.GetFilteredData<SalesOrder>(filter);
        }

        public System.Collections.IList GetCashReceiveByCustomerID(int customerID)
        {

            string filter = string.Format("{0}={1}", "CustomerID", customerID);
            return base.DataAccessManager.GetFilteredData<CashReceive>(filter);
        }

        public System.Collections.IList GetProfitInformationAccordingToDate(string _fromDate, string _toDate, int branchID, int organizationID)
        {
            string filter = string.Format(IFMSConstant.QueryConstants.ProfitAccordingToDate, _fromDate, _toDate, branchID, organizationID);
            return base.DataAccessManager.ExecuteSQL<ProfitCalculationAccordingToDate>(filter).Cast<ProfitCalculationAccordingToDate>().ToList();
        }

        public List<ProfitCalculationAccordingToDate> GetFilteredProfitInformationAccordingToDate(string filter)
        {
            return base.DataAccessManager.ExecuteSQL<ProfitCalculationAccordingToDate>(filter).Cast<ProfitCalculationAccordingToDate>().ToList();
        }

        public SalesOrder GetSalesOrderByID(int salesOrderID)
        {
            return base.DataAccessManager.GetByID<SalesOrder>(salesOrderID, "SalesID");
        }

        public List<SalesOrderDetail> GetAllSalesOrderDetailBySalesID(int salesID)
        {
            string filter = string.Format("{0}={1}", "SalesID", salesID);
            return base.DataAccessManager.GetFilteredData<SalesOrderDetail>(filter).Cast<SalesOrderDetail>().ToList();
        }

        public int UpdateSalesOrderInformation(SalesOrder salesOrder, List<SalesOrderDetail> lstSalesOrderDetail)
        {
            int result = (int)IFMSEnum.ReturnResult.Success;
            base.DataAccessManager.BeginTransaction();
            try
            {
                base.DataAccessManager.Update<SalesOrder>(salesOrder);
                foreach (SalesOrderDetail salesOrderDetail in lstSalesOrderDetail)
                {
                    base.DataAccessManager.Update<SalesOrderDetail>(salesOrderDetail);
                }

                string filter = string.Format("{0}={1}", "SalesID", salesOrder.SalesID);

                foreach (Journal journal in base.DataAccessManager.GetFilteredData<Journal>(filter))
                {
                    journal.Amount = salesOrder.SalesAmount;
                    base.DataAccessManager.Update<Journal>(journal);
                }
                base.DataAccessManager.CommitTransaction();
            }
            catch (Exception)
            {
                result = (int)IFMSEnum.ReturnResult.Failed;
                base.DataAccessManager.Rollback();
            }
            finally
            {
                base.DataAccessManager.EndTransaction();
            }
            return result;
        }




        public SalesOrderDetail GetSalesDetailByID(int salesOrderDtailID)
        {
            return base.DataAccessManager.GetByID<SalesOrderDetail>(salesOrderDtailID, "SerialNo");
        }

        public List<SalesOrder> GetAllSalesOrderByDate(string fromDate, string toDate, int branchID, int organizationId)
        {
            string filter = string.Format("{0} between '{1}' AND '{2}' And BranchID={3} and OrganizationID={4}", "SalesDate", fromDate, toDate, branchID, organizationId);
            return base.DataAccessManager.GetFilteredData<SalesOrder>(filter).Cast<SalesOrder>().ToList();
        }

        public List<SalesOrderDetail> GetSalesOrderDetailByProductID(string productID, int branchID, int organizationID)
        {
            string filter = string.Format("{0} ='{1}'", "ProductID", productID);
            List<SalesOrderDetail> lstSalesOrderDetail = new List<SalesOrderDetail>();

            foreach (SalesOrderDetail sDetail in base.DataAccessManager.GetFilteredData<SalesOrderDetail>(filter).Cast<SalesOrderDetail>().ToList())
            {
                SalesOrder salesOrder = GetSalesOrderByID(sDetail.SalesID);
                if (salesOrder.BranchID == branchID && salesOrder.OrganizationID == organizationID)
                {
                    lstSalesOrderDetail.Add(sDetail);
                }
            }
            return lstSalesOrderDetail;
        }

        public List<VMProductSalesHistory> GetProductSalesHistoryByProductAndBranchID(string productID, int branchID)
        {
            string sql = @"Select c.CustomerID, cs.SalesID, cs.BillNumber, cs.SalesDate, c.CustomerName,csd.ProductID, csd.ProductName, csd.Quantity, csd.Price from CashSalesOrderDetails csd
                        left outer join CashSales cs
                        on csd.SalesID=cs.SalesID
                        left outer join Customer c on
                        cs.CustomerID=c.CustomerID
                        where ProductID='" + productID + "' and cs.BranchID=" + branchID + " and cs.IsCanceled='false' order by cs.SalesDate";

            return base.DataAccessManager.ExecuteSQL<VMProductSalesHistory>(sql).Cast<VMProductSalesHistory>().ToList();
        }



        public List<SalesOrderDetail> GetAllSalesProductByProductIDandDate(int branchID, int organizationID, string fromDate, string toDate)
        {
            string filter = string.Format(IFMSConstant.QueryConstants.GET_ALL_SALES_PRODUCT_BY_PRODUCTID_DATE, branchID, organizationID, fromDate, toDate);
            return base.DataAccessManager.ExecuteSQL<SalesOrderDetail>(filter).Cast<SalesOrderDetail>().ToList();
        }

        public void DeleteSalesDetail(SalesOrderDetail salesDetail)
        {
            base.DataAccessManager.Delete<SalesOrderDetail>(salesDetail.SerialNo);
        }

        public List<SalesOrder> GetAllDeveloperSalesOrderByDate(string fromDate, string toDate, int branchID, int organizationID)
        {
            string filter = string.Format("{0} between '{1}' AND '{2}' And BranchID={3} and OrganizationID={4} and CustomerID in (Select customerID from Customer where CustomerType =1)", "SalesDate", fromDate, toDate, branchID, organizationID);
            return base.DataAccessManager.GetFilteredData<SalesOrder>(filter).Cast<SalesOrder>().ToList();
        }

        public List<SalesOrder> GetAllDeveloperSalesOrderByDate(string fromDate, string toDate, int branchID, int organizationID, int customerType)
        {
            string filter = string.Format("{0} between '{1}' AND '{2}' And BranchID={3} and OrganizationID={4} and CustomerID in (Select customerID from Customer where CustomerType ={5})", "SalesDate", fromDate, toDate, branchID, organizationID, customerType);
            return base.DataAccessManager.GetFilteredData<SalesOrder>(filter).Cast<SalesOrder>().ToList();
        }

        public List<SalesOrder> GetFilteredSalesInformation(string filter)
        {
            return base.DataAccessManager.GetFilteredData<SalesOrder>(filter).Cast<SalesOrder>().ToList();
        }


        public List<SalesAndPurchaseReport> GetFilteredSalesRepresentativeSalesInformation(string fromDate, string toDate, int branchID)
        {

            string sql = @"Select cs.SalesID,cs.SalesRepresentativeID ,sr.Address ,c.CustomerID, z.ZoneName, BillNumber, 
                        SalesDate, isnull( c.CustomerName,'Cash Sales') as CustomerName , SalesAmount, Discount, cs.ReceiveAmount , (SalesAmount -Discount) Total 
                        from CashSales cs
                        left outer join Customer c on cs.CustomerID =c.CustomerID 
                        left outer join Zone z on z.ZoneID=c.ZoneID
                        left outer join SalesRepresentative sr
                        on cs.SalesRepresentativeID=sr.SalesRepresentativeID
                        Where cs.SalesDate between '{0}' and '{1}' and cs.BranchID={2}";
            string filter = string.Format(sql, fromDate, toDate, branchID);
            return base.DataAccessManager.ExecuteSQL<SalesAndPurchaseReport>(filter).Cast<SalesAndPurchaseReport>().ToList();
        }

        public void InsertGatePass(GatePass gatePass)
        {
            GatePass existingGatePass = GetGatePassBySalesID(gatePass.SalesID);

            if (existingGatePass == null)
            {
                base.DataAccessManager.Insert<GatePass>(gatePass);
            }
            else
            {
                base.DataAccessManager.Update<GatePass>(existingGatePass);
            }
        }

        private GatePass GetGatePassBySalesID(int salesID)
        {
            string filter = string.Format("{0}={1}", "SalesID", salesID);
            return base.DataAccessManager.GetFilteredData<GatePass>(filter).Cast<GatePass>().ToList().FirstOrDefault();
        }


        public decimal GetFilteredSalesSalesDiscount(string salesFilter)
        {
            return Convert.ToDecimal(base.DataAccessManager.ExecuteScalar(salesFilter));
        }

        public List<CustomerOutstanding> GetCustomerOutStandingByCustomerID(int customerID, int branchID, string fromDate, string toDate)
        {
            List<CustomerOutstanding> lstCustomerOutstanding = new List<CustomerOutstanding>();
            string filter = string.Format(IFMSConstant.QueryConstants.CUSTOMER_DUE_REPORT_BY_CUSTOEMRID_AND_BRANCHID, customerID, branchID, fromDate, toDate);
            lstCustomerOutstanding = base.DataAccessManager.ExecuteSQL<CustomerOutstanding>(filter).Cast<CustomerOutstanding>().ToList();
            return lstCustomerOutstanding;
        }

        public List<CustomerOutstanding> GetCustomerTransactionByCustomerID(int customerID)
        {
            List<CustomerOutstanding> lstCustomerOutstanding = new List<CustomerOutstanding>();
            string filter = string.Format(IFMSConstant.QueryConstants.CUSTOMER_TRANSACTION_REPORT_BY_CUSTOEMRID_AND_BRANCHID, customerID);
            lstCustomerOutstanding = base.DataAccessManager.ExecuteSQL<CustomerOutstanding>(filter).Cast<CustomerOutstanding>().ToList();
            return lstCustomerOutstanding;
        }

        public List<CustomerOutstanding> GetCustomerTransactionByCustomerID(int customerID, string fromDate, string toDate)
        {
            List<CustomerOutstanding> lstCustomerOutstanding = new List<CustomerOutstanding>();
            string filter = string.Format(IFMSConstant.QueryConstants.CUSTOMER_TRANSACTION_REPORT_BY_CUSTOEMRID_AND_Date, customerID, fromDate, toDate);
            lstCustomerOutstanding = base.DataAccessManager.ExecuteSQL<CustomerOutstanding>(filter).Cast<CustomerOutstanding>().ToList();
            return lstCustomerOutstanding;
        }
        public decimal GetTotalDicountByCustomerID(int customerID)
        {
            string sql = @"Select sum(Amount) Amount from (
                            Select  'Sales Discount' as Description,  (cs.Discount ) as Amount ,cs.BranchID,cs.CustomerID ,'1' SequenceNumber   from CashSales cs where cs.Discount>0
                            union
                            Select  'Received Discount' as Description  , (cr.Discount) as Amount ,cr.BranchID ,cr.CustomerID ,'2' SequenceNumber   from dbo.CashReceive cr Where cr.Discount>0
                            )tbl Where tbl.CustomerID=" + customerID;

            return Convert.ToDecimal(base.DataAccessManager.ExecuteScalar(sql));
        }

        public List<ProductTypeWiseSales> BranchAndProductTypeWiseReportByCustomerType(string fromDate, string toDate, int branchID, int customerType)
        {
            string sql = @"Select tbl.SalesDate , isnull(tbl.BillNumber,'') as BillNumber , tbl.CustomerName , sum(tbl.SalesAmount) SalesAmount, isnull(SUM(tbl.Discount),0) as Discount, tbl.TypeName   from (
                           Select cs.SalesDate, cs.BillNumber, c.CustomerName , cs.BranchID, csd.ProductID, sum((cs.SalesAmount +cs.CarryingLoading )) SalesAmount, cs.Discount  ,t.TypeName   from CashSales cs
                            join CashSalesOrderDetails csd on cs.SalesID =csd.SalesID 
                            join ProductInformation p on csd.ProductID =p.ProductID 
                            join TypeInformation t on p.ProductTypeID =t.ProductTypeID
                            join Customer c on c.CustomerID =cs.CustomerID where c.CustomerType={0} )tbl                            
                            Where tbl.SalesDate between '" + fromDate + "' and '" + toDate + @"' and tbl.BranchID =" + branchID + " group by tbl.SalesDate,tbl.BillNumber , tbl.CustomerName ,tbl.TypeName  order by tbl.TypeName";
            sql = string.Format(sql, customerType);

            return base.DataAccessManager.ExecuteSQL<ProductTypeWiseSales>(sql).ToList();
        }

        public List<ProductTypeWiseSales> BranchAndProductTypeWiseReport(string fromDate, string toDate, int branchID)
        {
            string sql = @"Select tbl.SalesDate , isnull(tbl.BillNumber,'') as BillNumber , tbl.CustomerName , sum(tbl.SalesAmount) SalesAmount, isnull(SUM(tbl.Discount),0) as Discount, tbl.TypeName   from (
                            Select cs.SalesDate, cs.BillNumber, c.CustomerName , cs.BranchID, csd.ProductID, (csd.Price *csd.SquareFeet) as SalesAmount, cs.Discount  ,t.TypeName   from CashSales cs
                            join CashSalesOrderDetails csd on cs.SalesID =csd.SalesID 
                            join ProductInformation p on csd.ProductID =p.ProductID 
                            join TypeInformation t on p.ProductTypeID =t.ProductTypeID
                            join Customer c on c.CustomerID =cs.CustomerID  )tbl                            
                            Where tbl.SalesDate between '" + fromDate + "' and '" + toDate + @"' and tbl.BranchID =" + branchID + " group by tbl.SalesDate,tbl.BillNumber , tbl.CustomerName ,tbl.TypeName  order by tbl.TypeName";
            return base.DataAccessManager.ExecuteSQL<ProductTypeWiseSales>(sql).ToList();
        }



        public List<ProductTypeWiseSales> BranchAndProductTypeWiseDetailReport(string fromDate, string toDate, int branchID)
        {
            string sql = @"Select tbl.CustomerType, tbl.TypeName, sum(Total) SalesAmount from (                            
                            Select c.Customertype,cs.SalesDate, t.ProductTypeID , t.TypeName, ((csd.Quantity * csd.Price))  Total  from [dbo].[CashSalesOrderDetails] csd
                            left outer join CashSales cs
                            on csd.SalesID=cs.SalesID
                            left outer join ProductInformation p
                            on csd.ProductID=p.ProductID
                            left outer join TypeInformation t on 
                            p.ProductTypeID=t.ProductTypeID
                            left outer join Customer c on 
                            cs.CustomerID=c.CustomerID
                            Where cs.BranchID={0} and csd.SquareFeet = 0 and cs.IsCanceled='false')tbl 
                            where tbl.SalesDate between '{1}' and '{2}'
                            group by tbl.CustomerType, tbl.TypeName";
            sql = string.Format(sql, branchID, fromDate, toDate);

            return base.DataAccessManager.ExecuteSQL<ProductTypeWiseSales>(sql).ToList();
        }



        public List<ProductTypeWiseSales> BranchAndProductTypeWiseDiscountReport(string fromDate, string toDate, int branchID)
        {
            string sql = @"Select c.CustomerType, sum (Discount) Discount from CashSales cs
                        left outer join Customer c on cs.CustomerID=c.CustomerID
                         where cs.BranchID={0} and cs.SalesDate between '{1}' and '{2}'
                         group by CustomerType";
            sql = string.Format(sql, branchID, fromDate, toDate);

            return base.DataAccessManager.ExecuteSQL<ProductTypeWiseSales>(sql).ToList();


        }

        public List<ProductTypeWiseSales> BranchAndProductTypeWiseOtherReceiveReport(string fromDate, string toDate, int branchID)
        {
            string sql = @"Select c.CustomerType, Sum(cs.CarryingLoading) CarryingLoading from CashSales cs
                            left outer join Customer c on cs.CustomerID=c.CustomerID
                            where cs.BranchID={0} and cs.SalesDate between '{1}' and '{2}'
                            group by CustomerType";
            sql = string.Format(sql, branchID, fromDate, toDate);

            return base.DataAccessManager.ExecuteSQL<ProductTypeWiseSales>(sql).ToList();

        }

        public List<SalesOrder> GetSalesOrderByBillNumber(string billNumber, int branchID)
        {
            string filter = string.Format("{0}='{1}' and {2}={3}", "BillNumber", billNumber, "BranchID", branchID);
            return base.DataAccessManager.GetFilteredData<SalesOrder>(filter).Cast<SalesOrder>().ToList();
        }

        public int SaveSalesInformation(VMSales vmSales)
        {
            try
            {
                base.DataAccessManager.BeginTransaction();

                if (vmSales.SalesOrder.SalesID > 0)
                {

                    base.DataAccessManager.Update<SalesOrder>(vmSales.SalesOrder);

                    List<Journal> lstExistingJournal = new List<Journal>();
                    string filter = string.Format("{0}={1}", "SalesID", vmSales.SalesOrder.SalesID);
                    lstExistingJournal = base.DataAccessManager.GetFilteredData<Journal>(filter).Cast<Journal>().ToList();
                    foreach (Journal journal in lstExistingJournal)
                    {
                        base.DataAccessManager.Delete<Journal>(journal.JournalID);
                    }

                    filter = string.Format("{0}={1}", "SalesID", vmSales.SalesOrder.SalesID);
                    List<SalesOrderDetail> lstExSalesOrderDetail = new List<SalesOrderDetail>();

                    lstExSalesOrderDetail = base.DataAccessManager.GetFilteredData<SalesOrderDetail>(filter).Cast<SalesOrderDetail>().ToList();

                    foreach (SalesOrderDetail exDetail in lstExSalesOrderDetail)
                    {
                        base.DataAccessManager.Delete<SalesOrderDetail>(exDetail.SerialNo);
                    }

                    foreach (SalesOrderDetail sDetail in vmSales.lstSalesOrderDetail)
                    {
                        sDetail.SalesID = vmSales.SalesOrder.SalesID;
                        base.DataAccessManager.Insert<SalesOrderDetail>(sDetail);
                    }

                    InsertJournalInformationForCreditSales(vmSales);
                    InsertJournalInformationForGoodsDelivery(vmSales);
                    if (vmSales.SalesOrder.ReceiveAmount > 0)
                    {
                        InsertJournalInformationForCashReceive(vmSales);
                    }

                    if (vmSales.SalesOrder.Discount > 0)
                    {
                        InsertJournalInformationForDiscount(vmSales);
                    }
                }
                else
                {

                    vmSales.SalesOrder.SalesID = base.DataAccessManager.Insert<SalesOrder>(vmSales.SalesOrder);

                    foreach (SalesOrderDetail sDetail in vmSales.lstSalesOrderDetail)
                    {
                        sDetail.SalesID = vmSales.SalesOrder.SalesID;
                        base.DataAccessManager.Insert<SalesOrderDetail>(sDetail);
                    }

                    if (vmSales.SalesOrder.CustomerID > 0)
                    {
                        InsertJournalInformationForCreditSales(vmSales);
                    }
                   
                    InsertJournalInformationForGoodsDelivery(vmSales);
                    if (vmSales.SalesOrder.ReceiveAmount > 0)
                    {
                        InsertJournalInformationForCashReceive(vmSales);
                    }

                    if (vmSales.SalesOrder.Discount > 0)
                    {
                        InsertJournalInformationForDiscount(vmSales);
                    }
                }

                base.DataAccessManager.CommitTransaction();

            }
            catch (Exception)
            {
                vmSales.SalesOrder.SalesID = 0;
            }
            finally
            {
                base.DataAccessManager.EndTransaction();
            }


            return vmSales.SalesOrder.SalesID;
        }

        private decimal CalculatePurchasePrice(VMSales vmSales)
        {
            string productID = string.Empty;

            decimal purchasePrice = 0;
            foreach (SalesOrderDetail sDetail in vmSales.lstSalesOrderDetail)
            {

                purchasePrice = purchasePrice + (CalculatePriceByProductID(sDetail.ProductID) * sDetail.Quantity);

            }
            return purchasePrice;
        }

        private decimal CalculatePriceByProductID(string productID)
        {
            decimal purchasePrice = 0;
            decimal totalQty = 0;
            decimal totalPrice = 0;
            List<PurchaseOrderDetail> lstPurchaseOrderDetail = new List<PurchaseOrderDetail>();
            lstPurchaseOrderDetail = new PurchaseManager().GetPurchaseOrderDetailByProductID(productID, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID);
            foreach (PurchaseOrderDetail orderDetail in lstPurchaseOrderDetail)
            {
                totalQty = totalQty + orderDetail.Quantity;
                totalPrice = totalPrice + (orderDetail.Quantity * orderDetail.PurchasePrice);
            }

            purchasePrice = (totalPrice > 0) ? totalPrice / totalQty : 0;
            return purchasePrice;

        }

        public int InsertJournalInformationForCreditSales(VMSales vmSales)
        {
            int result = 0;
            int referenceID = 1;
            decimal amount = vmSales.SalesOrder.SalesAmount + vmSales.SalesOrder.CarryingLoading;

            referenceID = new JournalManager().GetJournalReferenceID();



            for (int i = 0; i <= 1; i++)
            {
                IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();
                ChildAccount childAccount = new ChildAccountManager().GetChildAccountByCustomerID(vmSales.SalesOrder.CustomerID);

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

                journal.SalesID = vmSales.SalesOrder.SalesID;
                journal.JAccountID = 0;
                journal.Amount = amount;
                journal.ReferenceNo = referenceID;
                journal.BranchID = MTBFConstants.AppConstants.BranchID;
                journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                base.DataAccessManager.Insert<Journal>(journal);
            }

            return result;
        }

        public int InsertJournalInformationForGoodsDelivery(VMSales vmSales)
        {
            int result = 0;
            int referenceID = 1;
            decimal amount = CalculatePurchasePrice(vmSales);

            referenceID = new JournalManager().GetJournalReferenceID();
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
                    journal.AccountID = IFMSConstant.InventoryAccountID;
                }

                journal.SalesID = vmSales.SalesOrder.SalesID;
                journal.JAccountID = 0;
                journal.Amount = amount;
                journal.ReferenceNo = referenceID;
                journal.BranchID = MTBFConstants.AppConstants.BranchID;
                journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                base.DataAccessManager.Insert<Journal>(journal);
            }

            return result;
        }

        public int InsertJournalInformationForCashReceive(VMSales vmSales)
        {
            int result = 0;
            int referenceID = 1;
            decimal amount = vmSales.SalesOrder.ReceiveAmount;

            referenceID = new JournalManager().GetJournalReferenceID();

            for (int i = 0; i <= 1; i++)
            {
                IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();
                ChildAccount childAccount = new ChildAccountManager().GetChildAccountByCustomerID(vmSales.SalesOrder.CustomerID);

                if (i == 0)
                {
                    journal.DrCrIndecator = "Dr";
                    journal.AccountID = IFMSConstant.CashAccountID;
                }
                else
                {
                    if (childAccount != null)
                    {
                        journal.DrCrIndecator = "Cr";
                        journal.AccountID = childAccount.AccountID;
                        journal.ChildAccountID = childAccount.ChildAccountID;
                    }
                    else
                    {
                        journal.DrCrIndecator = "Cr";
                        journal.AccountID = IFMSConstant.GoodsSalesAccountID;                     
                    }
                  
                }

                journal.SalesID = vmSales.SalesOrder.SalesID;
                //  journal.ReferenceID = vmSales.SalesOrder.SalesID;
                // journal.ReferenceType = (int)MTBFEnums.JournalReferenceType.Sales;
                journal.JAccountID = 0;
                journal.Amount = amount;
                journal.ReferenceNo = referenceID;
                journal.BranchID = MTBFConstants.AppConstants.BranchID;
                journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                base.DataAccessManager.Insert<Journal>(journal);
            }

            return result;
        }

        public int InsertJournalInformationForDiscount(VMSales vmSales)
        {
            int result = 0;
            int referenceID = 1;
            decimal amount = vmSales.SalesOrder.Discount;

            referenceID = new JournalManager().GetJournalReferenceID();

            for (int i = 0; i <= 1; i++)
            {
                IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();
                ChildAccount childAccount = new ChildAccountManager().GetChildAccountByCustomerID(vmSales.SalesOrder.CustomerID);

                if (i == 0)
                {
                    journal.DrCrIndecator = "Dr";
                    journal.AccountID = IFMSConstant.DiscountGiven;
                }
                else
                {
                    journal.DrCrIndecator = "Cr";
                    journal.AccountID = childAccount.AccountID;
                    journal.ChildAccountID = childAccount.ChildAccountID;
                }

                journal.SalesID = vmSales.SalesOrder.SalesID;
                //    journal.ReferenceID = vmSales.SalesOrder.SalesID;
                //  journal.ReferenceType = (int)MTBFEnums.JournalReferenceType.Sales;
                journal.JAccountID = 0;
                journal.Amount = amount;
                journal.ReferenceNo = referenceID;
                journal.BranchID = MTBFConstants.AppConstants.BranchID;
                journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                base.DataAccessManager.Insert<Journal>(journal);
            }

            return result;
        }


        public List<CustomerZoneWiseDue> GetDateAndZoneWiseDue(string _fromDate, string _toDate, int branchID)
        {
            string filter = @"Select c.ZoneID, tbl.CustomerID, c.CustomerName OrganizationName,c.Address, c.Proprietor,z.ZoneName, sum(tbl.SalesAmount) SalesAmount ,sum(tbl.ReceiveAmount) ReceiveAmount,(sum(tbl.SalesAmount) -sum(tbl.ReceiveAmount) ) PresentDue, sum(tbl.ReturnAmount)ReturnAmount from (
                            Select cs.CustomerID, sum((cs.SalesAmount+ cs.CarryingLoading)- cs.Discount) SalesAmount, Sum(cs.ReceiveAmount) ReceiveAmount,'0' ReturnAmount from Cashsales cs
                            Where cs.SalesDate between '{0}'  and '{1}' and cs.BranchID={2}
                            group by cs.CustomerID
                            union all
                            Select cr.CustomerID, '0' SalesAmount, Sum(cr.Amount) ReceiveAmount,'0' ReturnAmount from CashReceive cr
                            Where cr.ReceiveDate between '{0}'  and '{1}' and cr.BranchID={2}
                            group by cr.CustomerID
                            union all
                            Select sr.CustomerID, '0' SalesAmount,'0'ReceiveAmount , sum(Total-(PaidAmount+Discount)) ReturnAmount from SalesReturn sr 
                            Where sr.ReturnDate between '{0}'  and '{1}' and sr.BranchID={2}
                            group by sr.CustomerID
	                        union all
	                        Select cp.CustomerID, sum(cp.Amount) SalesAmount, '0' ReceiveAmount, '0' ReturnAmount from CustomerPreviousDue cp
	                        Where cp.DueDate between '{0}'  and '{1}' and cp.BranchID={2} and CustomerID>0
	                        group by cp.CustomerID

)tbl
                            left outer join Customer c on
                            tbl.CustomerID=c.CustomerID
                            left outer join Zone z on
                            c.ZoneID=z.ZoneID
                            group by c.ZoneID, tbl.CustomerID,c.CustomerName, c.Proprietor,z.ZoneName,c.Address";

            filter = string.Format(filter, _fromDate, _toDate, branchID);


            return base.DataAccessManager.ExecuteSQL<CustomerZoneWiseDue>(filter).Cast<CustomerZoneWiseDue>().ToList();
        }





        public decimal GetConsumedProductAmount(string salesFilter)
        {
            return Convert.ToDecimal(base.DataAccessManager.ExecuteScalar(salesFilter));
        }
    }
}
