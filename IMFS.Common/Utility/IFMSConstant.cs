using System;

namespace IMFS.Common.Utility
{
    public class IFMSConstant
    {
        public static int LoggedinUserID = 0;

        public static DateTime LoggedinTime = DateTime.Now;

        /// <summary>
        /// Parent account constant
        /// </summary>
        public const int CashAccountID = 1;
        public const int AccountPayableID = 2;
        public const int AccountReceivableID = 3;
        public const int InventoryAccountID = 4;
        public const int GoodsSalesAccountID = 5;
        public const int CostOfGoodsSoldAccountID = 6;
        public const int OperatingExpense = 7;
        public const int DiscountGiven = 8;
        public const int DiscountEarn = 9;
        public const int Purchase = 10;
        public const int Bank = 11;
        public const int Capital = 12;
        public const int SalesReturn = 13;
        public const int RawMaterialInventory = 14;
        public const int Workinprocessinventory = 15;
        public const int OtherProductionCost = 16;
        public const int FinishProductInventory = 17;

        //***************************************************//

        public const string PRODUCT_TYPE_ID = "ProductTypeID";


        public class QueryConstants
        {
            public const string GetSalesPrice = @"Select SalesPrice  from PurchaseOrderDetails Where ProductID ='{0}' and SerialNo =(
                                                Select MAX (SerialNo ) from PurchaseOrderDetails Where ProductID ='{0}')";

            public const string GetTotalAmountWithoutDiscount = @"Select SalesId,(SUM(Total) -SUM(Discount )) as Total from (Select cs.SalesId, sum(cs.Price *cs.Quantity) as Total,'00' as Discount   from CashSalesOrderDetails cs where cs.SalesId ={0} group by cs.SalesId union Select c.SalesId,'00' as Total, c.Discount   from CashSales c
                                                                where c.SalesId ={1})tbl
                                                                group by SalesId";


            public const string AllSupplierOutstandingByDate = @"Select * from (Select tbl2.BranchID ,tbl2.OrganizationID , c.SupplierName ,c.Address, c.PhoneNumber Phone  , tbl2.SupplierID , SUM(PurchaseAmount) -SUM(PaidAmount) as DueAmount from (
                                Select * from( 
                                Select cs.BranchID , cs.OrganizationID , cs.SupplierID, SUM ((PurchaseAmount) ) as PurchaseAmount, ISNULL( SUM (PaidAmount),0) as PaidAmount from PurchaseOrder cs
                                where cs.PurchaseDate between '{0}' and '{1}' and BranchID ='{2}' and OrganizationID ='{3}' 

                                group by cs.SupplierID, cs.BranchID , cs.OrganizationID 
                                union

                                Select BranchID, OrganizationID, SupplierID,Amount as PurchaseAmount, '0' as PaidAmount from CustomerPreviousDue 
                                where  BranchID ='{2}' and OrganizationID ='{3}'  and SupplierID <>0
                                )tbl
                                union
                                Select BranchID , OrganizationID, SupplierID  ,'0' as SalesAmount, sum(Amount + isnull(Discount,0) ) as PaidAmount from Payment 
                                group by BranchID ,OrganizationID , SupplierID)tbl2
                                left join Suppliers c on c.SupplierID =tbl2 .SupplierID 
                                group by tbl2.BranchID ,tbl2.OrganizationID , tbl2.SupplierID,c.Address ,c.SupplierName,c.PhoneNumber )tbl3";// where tbl3.DueAmount>0 



            public const string SalesReport = @"Select csd.SalesID, p.ProductName , csd.Price ,csd.Quantity,csd.VAT ,csd.Price *csd.Quantity as Total, cs.Discount, (cs.SalesAmount-cs.Discount) as NetTotal,  cs.SalesAmount     from CashSalesOrderDetails csd
                                               left join CashSales cs
                                               on csd.SalesID =cs.SalesID 
                                               left join ProductInformation p
                                               on csd .ProductID =p.ProductId        
                                               where csd.SalesID ={0}";

            public const string CreditSalesReport = @"Select Distinct  ROW_NUMBER() OVER (ORDER BY PI.ProductName) AS SL,
                                                    PI.Unit,cs.BillNumber, cs.ShortNote, cs.SalesId ,cs.SalesDate,cs.SalesAmount, cs.CarryingLoading ,
                                                    cs.ReceiveAmount,(cs.SalesAmount+cs.CarryingLoading )-cs.Discount as GrandTotal,
                                                    (cs.SalesAmount+cs.CarryingLoading ) -(cs.ReceiveAmount+cs.Discount ) as DueAmount ,c.CustomerID
                                                    ,c.CustomerName,PI.ProductName,cs.EmployeeId  ,c.Address,c.Phone,csd.Price ,csd.Quantity,  c.Proprietor,
                                                    csd.SquareFeet , csd.Price *csd.Quantity  as Total,CS.Discount ,pm.Name  as Model ,tp.TypeName,
                                                    PI.PackSize, e.EmployeeName
                                                    from CashSales CS left outer  join Customer C on cs.CustomerId =c.CustomerID 
                                                    inner join CashSalesOrderDetails CSD on cs.SalesId =csd.SalesId 
                                                    inner join ProductInformation PI on csd.ProductId =PI.ProductId
                                                    left join ProductModel  pm on PI.ProductModelID  =pm.ProductModelID  
                                                    left join TypeInformation tp on PI.ProductTypeID =tp.ProductTypeID 
                                                    left outer join Employee e on cs.EmployeeID=e.EmployeeID
                                                        where cs.SalesId ={0}";

            public const string ViewSalesInformation = @"SELECT     pm.Name as Model, dbo.CashSales.SalesID, dbo.CashSales.SalesDate, dbo.ProductInformation.ProductName, dbo.CashSalesOrderDetails.Price, CashSalesOrderDetails.VAT,
                                                        dbo.CashSalesOrderDetails.Quantity,  dbo.CashSales.CustomerID FROM dbo.CashSales INNER JOIN
                                                        dbo.CashSalesOrderDetails ON dbo.CashSales.SalesID = dbo.CashSalesOrderDetails.SalesID INNER JOIN
                                                        dbo.ProductInformation ON dbo.CashSalesOrderDetails.ProductID = dbo.ProductInformation.ProductId
                                                        join ProductModel pm on pm.ProductModelID=ProductInformation.ProductModelID
                                                       WHERE (dbo.CashSales.SalesID = {0})   ";


            public const string GetProductInformationBySupplier = @"Select ProductId ,ProductName,pm.Name as Model,t.TypeName ,p.BranchID ,p.OrganizationID   from ProductInformation p
	                                                                    join TypeInformation t
	                                                                    on t.ProductTypeID =p.ProductTypeId
	                                                                   left outer join ProductModel pm on pm.ProductModelID =p.ProductModelID 
                                                                        Where p.SupplierID ={0} and p.Discountinued='false'";



            public const string GetProductInformationForReturn = @"Select ProductId ,ProductName,pm.Name as Model,t.TypeName ,p.BranchID ,p.OrganizationID   from ProductInformation p
                                                                        join TypeInformation t
                                                                        on t.ProductTypeID =p.ProductTypeId
                                                                        join ProductModel pm on pm.ProductModelID =p.ProductModelID 
                                                                        where ProductID in(
                                                                        Select distinct ProductID from PurchaseOrderDetails)
                                                                        and p.CompanyID ={0} and p.Discountinued='false'";

            public const string GetMaxProductSerialNumber = @"select MAX(serialno)+1 from ProductInformation ";

            public const string GetPaymentInformationByCompanyIDAndDate = @"Select p.PaymentID,  p.CompanyID,c.CompanyName  ,p.SupplierID,s.SupplierName ,p.PaymentDate,p.Amount    from Payment p
                                                                            join CompanyInformation c
                                                                            on c.CompanyId =p.CompanyID 
                                                                            join Suppliers s
                                                                            on s.SupplierId =p.SupplierID 
                                                                            Where p.CompanyID ={0} and p.PaymentDate between '{1}' and '{2}'";
            public const string GetPaymentInformationByDate = @"Select p.PaymentID,  p.CompanyID,c.CompanyName  ,p.SupplierID,s.SupplierName ,p.PaymentDate,p.Amount    from Payment p
                                                                            join CompanyInformation c
                                                                            on c.CompanyId =p.CompanyID 
                                                                            join Suppliers s
                                                                            on s.SupplierId =p.SupplierID 
                                                                            Where p.PaymentDate between '{0}' and '{1}'";

            public const string SupplierDueAmountByCompanyAndSupplier = @"Select sum(tbl.DueAmount)-SUM(tbl.PaidAmount) as DueAmount     from(
                                                                        select po.BranchID ,po.OrganizationID ,  po.SupplierID,sum(00 ) as PaidAmount, isnull(sum(po.PurchaseAmount),0) -
                                                                        Isnull(sum(po.PaidAmount),0) as DueAmount   from PurchaseOrder po
                                                                        left outer join CompanyInformation c
                                                                        on c.CompanyId =po.CompanyID
                                                                        join Suppliers s
                                                                        on s.SupplierId =po.SupplierID 
                                                                        group by  po.SupplierID, po.BranchID ,po.OrganizationID 
                                                                        union

                                                                        Select p.BranchID ,p.OrganizationID  ,p.SupplierID,isnull(sum(p.Amount+ isnull(p.Discount,0)),0) as PaidAmount,sum(00 ) as DueAmount   from Payment p
                                                                        left outer join CompanyInformation c
                                                                        on c.CompanyId =p.CompanyID 
                                                                        left outer join Suppliers s
                                                                        on s.SupplierId =p.SupplierID 
                                                                        group by p.SupplierID,p.BranchID ,p.OrganizationID 
                                                                        union
                                                                        Select pr.BranchID ,pr.OrganizationID, pr.SupplierID ,isnull(sum(pr.Total),0) as PaidAmount,sum(pr.ReceiveAmount) as DueAmount   from PurchaseReturn pr
                                                                        left outer join CompanyInformation c
                                                                        on c.CompanyId =pr.CompanyID 
                                                                        left outer join Suppliers s
                                                                        on s.SupplierId =pr.SupplierID 
                                                                        group by pr.SupplierID,pr.BranchID ,pr.OrganizationID 
                                                                        union all
                                                                        Select jv.BranchID, jv.OrganizationID, ca.ReferenceID SupplierID,  isnull( sum(jv.Amount),0) PaidAmount ,0 DueAmount  from JournalVoucher jv
                                                                        left outer join ChildAccount ca
                                                                        on jv.DebitChildAccountID=ca.ChildAccountID or jv.CreditChildAccountID=ca.ChildAccountID
                                                                        where ca.ReferenceType=1 and ca.ReferenceID=1 and DebitAccountID>0
                                                                        group by jv.BranchID, jv.OrganizationID,  ca.ReferenceID)tbl
                                                                            where tbl.SupplierID ='{1}' and tbl .BranchID ='{2}' and tbl .OrganizationID ='{3}'
                                                                            group by tbl.SupplierID";

            public const string CustomerDueAmountByCustomerID = @"Select SUM(Debit)- SUM (Credit) as DueAmount from (
                                                                Select cs.SalesDate , cs.BillNumber as BillNumber, 'Goods Sales' as Description, ((cs.SalesAmount +cs.CarryingLoading) -(cs.Discount +cs.ReceiveAmount )) as Debit,'0' as Credit ,cs.BranchID,cs.CustomerID  from CashSales cs Where (cs.SalesAmount -(cs.Discount +cs.ReceiveAmount ))>0
                                                                union
                                                                Select sr.ReturnDate,  CONVERT(varchar(10), sr.SalesReturnID)  as BillNumber, 'Return' as Description, '0' as Debit ,(sr.Total -(sr.Discount +sr.PaidAmount )) as Credit,sr.BranchID,sr.CustomerID   from SalesReturn sr where (sr.Total -(sr.Discount +sr.PaidAmount ))>0
                                                                union
                                                                Select cr.ReceiveDate , CONVERT(varchar(10), cr.CashReceiveID)  as BillNumber, 'Received' as Description ,'0' as Debit, (cr.Amount+cr.Discount ) as Credit ,cr.BranchID ,cr.CustomerID  from dbo.CashReceive cr where (cr.Amount+cr.Discount )>0
                                                                union
                                                                Select cd.DueDate, CONVERT(varchar(10),  cd.PreviousDueID)as BillNumber, 'Previous Due' as Description , cd.Amount as Debit ,'0' as Credit, cd.BranchID ,cd.CustomerID from CustomerPreviousDue cd where cd.Amount>0 
                                                                union all
                                                                Select jv.Date, jv.VoucherNumber  BillNumber, jv.Description ,  0 Debit ,jv.Amount Credit, jv.BranchID ,ca.ReferenceID CustomerID from JournalVoucher jv
                                                                left outer join ChildAccount ca
                                                                on jv.DebitChildAccountID=ca.ChildAccountID or jv.CreditChildAccountID=ca.ChildAccountID
                                                                where ca.ReferenceType=2 and ca.ReferenceID=1 and CreditAccountID>0
                                                                )tbl
                                                                where tbl.CustomerId={0}  group by tbl.CustomerID";

            public const string PartyDueAmountByPartyID = @"Select isnull((tbl.SalesAmount -(tbl.SRAmount +isnull(tbl1.ReceiveAmount,0))),0) as DueAmount  from (
                                                            Select cs.BranchID,cs.OrganizationID , PartyID ,sum(Total ) as SalesAmount ,sum(ReceiveAmount ) as SRAmount  from StockSales cs Where PartyID ={0}  group by PartyID , cs.BranchID ,cs.OrganizationID   ) tbl
                                                            left join
                                                            (
                                                            Select cr.BranchID ,cr.OrganizationID , PartyID ,isnull(sum(Amount),0) + isnull(sum(Discount),0) as ReceiveAmount   from PartyReceive cr Where PartyID ={0} group by PartyID , cr.BranchID ,cr.OrganizationID  )tbl1
                                                            on tbl.PartyID  =tbl1.PartyID 
                                                                where tbl.PartyID='{0}' and tbl.BranchID ='{1}' and tbl .OrganizationID ='{2}'";


            public const string AllCustomerOutstandingByDate = @"Select * from (
                                                                Select tbl.CustomerID , c.CustomerType, c.CustomerName, c.Phone, c.Address, SUM(Debit)- SUM (Credit) as DueAmount from (
                                                                Select cs.SalesDate Date, cs.BillNumber as BillNumber, 'Goods Sales' as Description, ((cs.SalesAmount +cs.CarryingLoading) -(cs.Discount +cs.ReceiveAmount )) as Debit,'0' as Credit ,cs.BranchID,cs.CustomerID  from CashSales cs Where (cs.SalesAmount -(cs.Discount +cs.ReceiveAmount ))>0
                                                                union all
                                                                Select sr.ReturnDate Date,  CONVERT(varchar(10), sr.SalesReturnID)  as BillNumber, 'Return' as Description, '0' as Debit ,(sr.Total -(sr.Discount +sr.PaidAmount )) as Credit,sr.BranchID,sr.CustomerID   from SalesReturn sr where (sr.Total -(sr.Discount +sr.PaidAmount ))>0
                                                                union all
                                                                Select cr.ReceiveDate Date , CONVERT(varchar(10), cr.CashReceiveID)  as BillNumber, 'Received' as Description ,'0' as Debit, (cr.Amount+cr.Discount ) as Credit ,cr.BranchID ,cr.CustomerID  from dbo.CashReceive cr where (cr.Amount+cr.Discount )>0
                                                                union all
                                                                Select cd.DueDate Date, CONVERT(varchar(10),  cd.PreviousDueID)as BillNumber, 'Previous Due' as Description , cd.Amount as Debit ,'0' as Credit, cd.BranchID ,cd.CustomerID from CustomerPreviousDue cd where cd.Amount>0 
                                                                and cd.SupplierID=0
                                                                )tbl
                                                                left outer join Customer c on tbl.CustomerID=c.CustomerID
                                                                where  tbl.Date  between '{0}' and '{1}' and tbl.BranchID={2}  group by tbl.CustomerID, c.CustomerType, c.CustomerName, c.Phone, c.Address
                                                                )tbl where tbl.DueAmount<>0";




            public const string StockDifference = @"Select tbl.ProductId as ProductID,tbl.ProductName ,tbl.Quantity *-1 as Quantity  from (
                                                    Select p .ProductID,p.ProductName  ,isnull(tbl3 .PurchaseQuantity,0) -isnull(tbl3 .SalesQuantity,0)  as Quantity from (
                                                    Select tbl.ProductID,tbl .PurchaseQuantity,isnull(sum(tbl2 .SalesQuantity),0) as SalesQuantity    from 
                                                    (Select ProductID,sum(Quantity) as PurchaseQuantity   from PurchaseOrderDetails group by ProductID) tbl
                                                    left join (
                                                    Select ProductId,Sum(Quantity) as SalesQuantity   from CashSalesOrderDetails group by ProductId )tbl2
                                                    on tbl .ProductID =tbl2 .ProductId group by tbl .ProductID,tbl .PurchaseQuantity   )tbl3
                                                    left join ProductInformation p on tbl3 .ProductID = p.ProductId  )tbl where tbl .Quantity <0";

            public const string GetReorderProduct = @"Select distinct tbl1.ProductID  ,p.ProductName , t.TypeName ,p.PackSize,tbl1 .Quantity,p. ReorderQuantity,s.SupplierName   from (
Select tbl1 .ProductID ,tbl1.Quantity -ISNULL(tbl2.SQuantity,0) as Quantity  from (
Select ProductID ,SUM(Quantity ) as Quantity from PurchaseOrderDetails  group by ProductID )tbl1
left join
(
Select ProductId, isnull(SUM(Quantity),0) as SQuantity  from CashSalesOrderDetails group by ProductId )tbl2
on tbl1 .ProductID =tbl2.ProductId ) tbl1
left join

ProductInformation p
on tbl1 .ProductID =p.ProductId
left join TypeInformation t
on p.ProductTypeId =t.ProductTypeID 
left join Suppliers s
on p.SupplierID =s.SupplierID 
where  tbl1 .Quantity < p.ReorderQuantity  and p.SupplierID={0}";


            public const string ExpireProduct = @"Select * from (Select c.CompanyName ,  tbl3 .ProductID,p.ProductName+' '+ t.TypeName as ProductName ,tbl3 .Quantity -isnull(tbl3 .SalesQuantity,0) as Quantity,tbl3 .PurchasePrice,tbl3.PurchasePrice*(tbl3 .Quantity -isnull(tbl3 .SalesQuantity,0)) as Total  ,tbl3 .PurchaseID,tbl3.ExpireDate     from (
                                                                Select * from
                                                                (
                                                                Select ProductID,PurchaseID ,ExpireDate,Quantity, PurchasePrice,SalesPrice      from PurchaseOrderDetails)tbl1 
                                                                left join
                                                                (
                                                                Select ProductID as PID,PurchaseID as SPID, Sum(Quantity) as SalesQuantity   from CashSalesOrderDetails 
                                                                group by ProductID ,PurchaseID)tbl2 on tbl1 .PurchaseID =tbl2 .SPID  and tbl1 .ProductID =tbl2 .PID ) tbl3
                                                                left join 
                                                                ProductInformation p on tbl3 .ProductID =p.ProductID 
                                                                left join TypeInformation t on p.ProductTypeID =t.ProductTypeID 
                                                                left join CompanyInformation c on p.CompanyID =c.CompanyID ) tbl4 where DATEDIFF(day,SYSDATETIME(),tbl4 .ExpireDate)<=90 order by CompanyName";

            public const string PurchaseReportAccordingToDate = @"Select PurchaseID,ProductName,PurchaseDate ,Quantity,Price ,(Quantity * price)-Commission  as Total   from(
                                                                    Select p.BranchID , p.OrganizationID,  pod.PurchaseID,p.ProductName,po.PurchaseDate  ,pod.Quantity ,pod.PurchasePrice as Price , isnull(pod.PurchaseCommission,0) as Commission   
                                                                    from PurchaseOrderDetails pod
                                                                    join PurchaseOrder po on pod.PurchaseID =po.PurchaseID 
                                                                    left join ProductInformation p on
                                                                    pod.ProductID =p.ProductID )tbl Where PurchaseDate between '{0}' and '{1}' and BranchID ='{2}'  and OrganizationID ='{3}'";

            public const string SalesReportAccordingToDate = @"select * from(select Distinct p.BranchID ,p.OrganizationID , p.ProductName,cs.SalesDate, cs.CustomerID, cs.SalesID,csd.ProductID,csd.Price,csd.Quantity,cs.CarryingLoading, (csd.Quantity*csd.Price) as Total,cs.EmployeeID  
                                                                from CashSales cs   left join CashSalesOrderDetails csd on cs.SalesId =csd.SalesId 
                                                                inner join ProductInformation p on csd.ProductId =p.ProductId where cs.IsCanceled ='false')tbl 
                                                                where (tbl.SalesDate Between '{0}' AND '{1}' and tbl.BranchID ='{2}' and tbl .OrganizationID ='{3}') order by SalesID ";


            public const string ZoneSalesReportAccordingToDate = @"Select z.ZoneName, z.ZoneID  , c.CustomerName ,c.Address , cs.CustomerID, Sum( cs.SalesAmount) as SalesAmount , sum(cs.ReceiveAmount) as ReceiveAmount, sum(cs.Discount) as Discount, sum(cs.CarryingLoading) as CarryingLoading  from CashSales cs 
                                                                    left outer join Customer c on c.CustomerID=cs.CustomerID
                                                                    left outer join Zone z on z.ZoneID =c.ZoneID 
                                                                    where  cs.SalesDate between '{0}' and '{1}' and cs.BranchID ={2}
                                                                    and z.ZoneID={3}
                                                                    group by  cs.CustomerID ,c.CustomerName ,z.ZoneName, z.ZoneID ,c.Address  ";//cs.CustomerID in (Select CustomerID from Customer where ZoneID =1) and
            public const string AllZoneSalesReportAccordingToDate = @"Select z.ZoneName, z.ZoneID  , c.CustomerName ,c.Address , cs.CustomerID, Sum( cs.SalesAmount) as SalesAmount , sum(cs.ReceiveAmount) as ReceiveAmount, sum(cs.Discount) as Discount, sum(cs.CarryingLoading) as CarryingLoading  from CashSales cs 
                                                                    left outer join Customer c on c.CustomerID=cs.CustomerID
                                                                    left outer join Zone z on z.ZoneID =c.ZoneID 
                                                                    where  cs.SalesDate between '{0}' and '{1}' and cs.BranchID ={2}                                                                   
                                                                    group by  cs.CustomerID ,c.CustomerName ,z.ZoneName, z.ZoneID ,c.Address  ";//cs.CustomerID in (Select CustomerID from Customer where ZoneID =1) and


            public const string InventoryReport = @"Select distinct tt.ProductID ,tt.ProductName,tt.Quantity  ,'0' as PurchasePrice, ps.Name as Size , tt.ProductModelID ,tt.ProductTypeID   from (
                                                    SELECT  tbl3.BranchID ,tbl3 .OrganizationID ,  tbl3.ProductID,p.ProductModelID, p.ProductName,p.ProductTypeID,  tbl3.Quantity
                                                    FROM         (

                                                    SELECT  BranchID ,OrganizationID ,   ProductID, SUM(PurchaseQuantity + ReturnQuantity) - SUM(SalesQuantity) AS Quantity
                                                    FROM (

                                                    SELECT  po.BranchID ,po.OrganizationID ,   pod.ProductID, '0'  AS ReturnQuantity, SUM(pod.Quantity) AS PurchaseQuantity, '0' AS SalesQuantity
                                                    FROM          dbo.PurchaseOrderDetails AS pod LEFT OUTER JOIN
                                                    dbo.PurchaseOrder AS po ON pod.PurchaseID = po.PurchaseID
                                                    WHERE      (po.Status = 2  And BranchID='{0}') 
                                                    GROUP BY pod.ProductID,po.BranchID ,po.OrganizationID 
                                                    UNION
                                                    SELECT BranchID ,OrganizationID ,    rd.ProductCode AS ProductId, SUM(rd.Quantity) AS ReturnQuantity,'0'  AS PurchaseQuantity  ,'0' as SalesQuantity
                                                    FROM         dbo.SalesReturnDetail AS rd INNER JOIN
                                                    dbo.SalesReturn AS r ON r.SalesReturnID  = rd.SalesReturnID 
                                                    Where  BranchID='{0}' 
                                                    GROUP BY rd.ProductCode ,BranchID ,OrganizationID  


                                                    UNION
                                                    SELECT BranchID ,OrganizationID ,    rd.ProductCode AS ProductId, '0' AS ReturnQuantity, SUM(rd.Quantity) AS PurchaseQuantity ,'0' as SalesQuantity
                                                    FROM         dbo.ReceiveProductDetail AS rd INNER JOIN
                                                    dbo.ReceiveProduct AS r ON r.ReceiveProductID = rd.ReceiveProductID 
                                                    Where  BranchID='{0}'
                                                    GROUP BY rd.ProductCode ,BranchID ,OrganizationID 

                                                    UNION


                                                    SELECT BranchID, OrganizationID , ProductID,'0' AS ReturnQuantity, '0' AS PurchaseQuantity, SUM(Quantity + DQuanitity +DisQuantity +TQuantity ) AS SalesQuantity
                                                    FROM         (

                                                    SELECT  cs.BranchID ,cs.OrganizationID ,   csd.ProductID, SUM(csd.Quantity) AS Quantity , '0' as DQuanitity ,'0' as DisQuantity,  '0' as TQuantity
                                                    FROM          dbo.CashSalesOrderDetails AS csd INNER JOIN
                                                    dbo.CashSales AS cs ON csd.SalesID = cs.SalesID 
                                                    Where  BranchID='{0}' 
                                                    GROUP BY csd.ProductID,cs.BranchID ,cs.OrganizationID 

                                                    UNION
                                                    SELECT BranchID ,OrganizationID ,    td.ProductCode AS ProductId ,'0' AS Quantity , '0' as DQuantity,'0' as DisQuantity, SUM(td.Quantity) as TQuantity
                                                    FROM         dbo.TransferDetail AS td INNER JOIN
                                                    dbo.Transfer AS t ON t.TransferID = td.TransferID 
                                                    Where  BranchID='{0}' 
                                                    GROUP BY td.ProductCode ,BranchID ,OrganizationID                                              


                                                    UNION
                                                    SELECT  BranchID ,OrganizationID ,   dd.ProductCode AS ProductID , '0' as Quantity, SUM(dd.Quantity) AS DQuantity , '0' as DisQuantity, '0' as TQuantity
                                                    FROM         dbo.DamageInfo LEFT OUTER JOIN
                                                    dbo.DamageDetail AS dd ON dbo.DamageInfo.DamageInfoID = dd.DamageInfoID 
                                                    Where  BranchID='{0}'
                                                    GROUP BY dd.ProductCode,BranchID ,OrganizationID 

                                                    union

                                                    Select  pr.BranchID ,pr.OrganizationID ,   prd.ProductID AS ProductID , '0' as Quantity, SUM(prd.Quantity) AS DQuantity , '0' as DisQuantity, '0' as TQuantity from PurchaseReturnDetail prd
                                                    left outer join PurchaseReturn pr
                                                    on pr.PurchaseReturnID=prd.PurchaseReturnID
                                                    Where  BranchID='{0}'
                                                    GROUP BY prd.ProductID,pr.BranchID ,pr.OrganizationID 

                                                    UNION
                                                    SELECT BranchID ,OrganizationID , ProductID , '0' as Quantity, '0'  AS DQuantity, SUM(GivenQuantity) - SUM(ReceiveQuantity) AS DisQuantity,  '0' as TQuantity
                                                    FROM         dbo.DistributeSample 
                                                    Where BranchID='{0}'
                                                    GROUP BY ProductID ,BranchID ,OrganizationID  

                                                    ) AS tbl
                                                    GROUP BY ProductID,BranchID, OrganizationID
                                                    ) AS tbl2
                                                    GROUP BY ProductID,BranchID ,OrganizationID ) AS tbl3 INNER JOIN
                                                    dbo.ProductInformation AS p ON tbl3.ProductID = p.ProductID
                                                    )as tt  
                                                   left outer join  ProductInformation p on p.ProductID =tt.ProductID
                                                   left outer join ProductSize  ps on ps.ProductSizeID =p.ProductSizeID";  //tt.Quantity >0 

            public const string GetCustomerOutstandingByCustomerID = @"Select c.CustomerName,c.Address ,c.Phone,tab1 .SalesId,tab1 .SalesDate,tab1 .DueAmount      from (
                                                                        Select SalesId,SalesDate ,CustomerId, SUM(SalesAmount )-SUM(ReceiveAmount+Discount ) as DueAmount   from CashSales
                                                                        Where CustomerId>0  group by CustomerId,SalesId,SalesDate )  tab1
                                                                        join Customer c 
                                                                        on c.CustomerID =tab1 .CustomerId 
                                                                        Where tab1 .DueAmount >0 and tab1 .CustomerId ={0}";


            public const string ProfitAccordingToDate = @"select SalesDate, ProductID, TotalSales, Discount , sum(PurchasePrice * Quantity) as PurchasePrice from (
                                                        select SalesDate, ProductID,sum(totalSales) as TotalSales ,sum(Discount) as Discount, sum(PurchasePrice) as PurchasePrice ,sum(Quantity ) as Quantity from (
                                                        Select cs.SalesDate,csd.ProductID ,sum(csd.Quantity *csd.Price) as totalSales , sum(cs.Discount) as Discount , '0' as PurchasePrice, sum(csd.Quantity ) as Quantity  from CashSalesOrderDetails csd join CashSales cs on csd.SalesID=cs.SalesID 
                                                        where cs.SalesDate between '{0}' and '{1}' and BranchID ='{2}' and OrganizationID ='{3}'
                                                        group by csd.ProductID,cs.SalesDate
                                                        union

                                                        Select '' as SalesDate, pod.ProductID,'0' as totalSales,'0' as Discount,  Avg(pod.PurchasePrice) as PurchasePrice ,'0' as Quantity  
                                                        from dbo.PurchaseOrderDetails pod Where pod.ProductID in(
                                                        Select  csd.ProductID  from CashSalesOrderDetails csd join CashSales cs on csd.SalesID=cs.SalesID 
                                                        where cs.SalesDate between '{0}' and '{1}' and BranchID ='{2}' and OrganizationID ='{3}') group by pod.ProductID)tbl 
                                                        group by ProductID ,SalesDate
                                                        )tbl2 where TotalSales>0 group by ProductID,TotalSales, Discount,SalesDate";





            public const string GET_TOTAL_EXPENSE_BY_DATE = @"Select ExpenseDate,   isnull(sum(Amount),0) as Amount from Expense  Where ExpenseDate between '{0}' and '{1}' and BranchID ='{2}' and OrganizationID ='{3}' group by ExpenseDate";



            public const string DatabaseBackUp = @"BACKUP DATABASE {2} TO DISK = '{0}\{1}.BAK'";


            public const string GetProfitOrLoss = @"Select  sum(IncomeAmount) -SUM(ExpAmount)  as Balance  from (
                                                    Select 'accountType' as AccountType, '0' as IncomeAmount,sum(j.Amount) as ExpAmount  from Journal j
                                                    left join ParentAccount pa
                                                    on j.AccountID =pa.AccountID
                                                    left join AccountType at
                                                    on pa.AccountTypeID =at.AccountTypeID 
                                                    where at.AccountHeadID =3 and j.DrCrIndecator ='Dr' and 
                                                    j.JournalDate between '{0}' and '{1}' and j.BranchID ='{2}' and j.OrganizationID ='{3}'
                                                    group by pa.AccountID 
                                                    union
                                                    Select 'accountType' as AccountType, sum(j.Amount) as IncomeAmount,'0' as ExpAmount  from Journal j
                                                    left join ParentAccount pa
                                                    on j.AccountID =pa.AccountID
                                                    left join AccountType at
                                                    on pa.AccountTypeID =at.AccountTypeID 
                                                    where at.AccountHeadID =4 and j.DrCrIndecator ='Cr' and 
                                                    j.JournalDate between '{0}' and '{1}' and j.BranchID ='{2}' and j.OrganizationID ='{3}'
                                                    group by pa.AccountID )tblIncome group by AccountType";


            public const string IncomeStatement = @"SELECT BranchID, OrganizationID, 'Expense' AS AccountType, AccountsName, AccountTypeName, Amount FROM (SELECT     *
                                                    FROM  (SELECT BranchID ,OrganizationID , AccountsName, AccountTypeName, ISNULL(sum(DrAmount), 0) - ISNULL(sum(CrAmount), 0) AS Amount
                                                    FROM (SELECT j.BranchID, j.OrganizationID, pa.AccountsName, at.AccountTypeName, SUM(j.Amount) AS DrAmount, '0' AS CrAmount
                                                    FROM  Journal j LEFT JOIN ParentAccount pa ON pa.AccountID = j.AccountID 
                                                    LEFT JOIN AccountType at ON pa.AccountTypeID = at.AccountTypeID
                                                    WHERE  j.DrCrIndecator = 'Dr' AND at.AccountHeadID = 3 AND j.JournalDate BETWEEN '{0}' AND '{1}'
                                                    GROUP BY pa.AccountsName, at.AccountTypeName,j.BranchID, j.OrganizationID
                                                    UNION
                                                    SELECT j.BranchID, j.OrganizationID ,  pa.AccountsName, at.AccountTypeName, '0' AS DrAmount, SUM(j.Amount) AS CrAmount
                                                    FROM Journal j LEFT JOIN ParentAccount pa ON pa.AccountID = j.AccountID LEFT JOIN
                                                    AccountType at ON pa.AccountTypeID = at.AccountTypeID
                                                    WHERE     j.DrCrIndecator = 'Cr' AND at.AccountHeadID = 3 AND j.JournalDate BETWEEN '{0}' AND '{1}'
                                                    GROUP BY pa.AccountsName, at.AccountTypeName,j.BranchID, j.OrganizationID ) tbl
                                                    GROUP BY AccountsName, AccountTypeName, BranchID ,OrganizationID ) tblAsset
                                                    WHERE      Amount > 0) tblExpense
                                                                                                        UNION
                                                    SELECT BranchID,OrganizationID, 'Expense' AS AccountType, AccountsName, AccountTypeName, Amount
                                                    FROM (SELECT * FROM (SELECT BranchID ,OrganizationID , AccountsName, AccountTypeName, ISNULL(sum(DrAmount), 0) - ISNULL(sum(CrAmount), 0) AS Amount
                                                    FROM (SELECT j.BranchID ,j.OrganizationID ,    pa.AccountsName, at.AccountTypeName, SUM(j.Amount) AS DrAmount, '0' AS CrAmount
                                                    FROM  Journal j LEFT JOIN  ParentAccount pa ON pa.AccountID = j.AccountID LEFT JOIN
                                                    AccountType at ON pa.AccountTypeID = at.AccountTypeID
                                                    WHERE    j.DrCrIndecator = 'Dr' AND at.AccountHeadID = 4 AND j.JournalDate BETWEEN '{0}' AND '{1}'
                                                    GROUP BY pa.AccountsName, at.AccountTypeName,j.BranchID ,j.OrganizationID
                                                    UNION
                                                    SELECT j.BranchID ,j.OrganizationID, pa.AccountsName, at.AccountTypeName, '0' AS DrAmount, SUM(j.Amount) AS CrAmount
                                                    FROM Journal j LEFT JOIN ParentAccount pa ON pa.AccountID = j.AccountID LEFT JOIN
                                                    AccountType at ON pa.AccountTypeID = at.AccountTypeID
                                                    WHERE     j.DrCrIndecator = 'Cr' AND at.AccountHeadID = 4 AND j.JournalDate BETWEEN '{0}' AND '{1}'
                                                    GROUP BY pa.AccountsName, at.AccountTypeName,j.BranchID ,j.OrganizationID) tbl
                                                    GROUP BY AccountsName, AccountTypeName,BranchID ,OrganizationID ) tblAsset
                                                    WHERE Amount > 0) tblIncome
                                                                                                        UNION
                                                    SELECT BranchID,OrganizationID,  'Income' AS AccountType, AccountsName, AccountTypeName, Amount * - 1 AS Amount
                                                    FROM  (SELECT *  FROM   (SELECT BranchID ,OrganizationID ,    AccountsName, AccountTypeName, ISNULL(sum(DrAmount), 0) - ISNULL(sum(CrAmount), 0) AS Amount
                                                    FROM  (SELECT   j.BranchID ,j.OrganizationID,  pa.AccountsName, at.AccountTypeName, SUM(j.Amount) AS DrAmount, '0' AS CrAmount
                                                    FROM Journal j LEFT JOIN  ParentAccount pa ON pa.AccountID = j.AccountID LEFT JOIN
                                                    AccountType at ON pa.AccountTypeID = at.AccountTypeID
                                                    WHERE      j.DrCrIndecator = 'Dr' AND at.AccountHeadID = 4 AND j.JournalDate BETWEEN '{0}' AND '{1}'
                                                    GROUP BY pa.AccountsName, at.AccountTypeName,j.BranchID ,j.OrganizationID 
                                                    UNION
                                                    SELECT  j.BranchID ,j.OrganizationID,   pa.AccountsName, at.AccountTypeName, '0' AS DrAmount, SUM(j.Amount) AS CrAmount
                                                    FROM  Journal j LEFT JOIN ParentAccount pa ON pa.AccountID = j.AccountID LEFT JOIN
                                                    AccountType at ON pa.AccountTypeID = at.AccountTypeID
                                                    WHERE     j.DrCrIndecator = 'Cr' AND at.AccountHeadID = 4 AND j.JournalDate BETWEEN '{0}' AND '{1}'
                                                    GROUP BY pa.AccountsName, at.AccountTypeName,j.BranchID ,j.OrganizationID) tbl
                                                    GROUP BY AccountsName, AccountTypeName,BranchID ,OrganizationID ) tblAsset
                                                    WHERE      Amount < 0) tblIncome
                                                                                                       UNION
                                                    SELECT BranchID, OrganizationID,  'Income' AS AccountType, AccountsName, AccountTypeName, Amount * - 1 AS Amount
                                                    FROM (SELECT  *  FROM (SELECT BranchID ,OrganizationID ,    AccountsName, AccountTypeName, ISNULL(sum(DrAmount), 0) - ISNULL(sum(CrAmount), 0) AS Amount
                                                    FROM  (SELECT  j.BranchID ,j.OrganizationID,   pa.AccountsName, at.AccountTypeName, SUM(j.Amount) AS DrAmount, '0' AS CrAmount
                                                    FROM Journal j LEFT JOIN  ParentAccount pa ON pa.AccountID = j.AccountID LEFT JOIN
                                                    AccountType at ON pa.AccountTypeID = at.AccountTypeID
                                                    WHERE      j.DrCrIndecator = 'Dr' AND at.AccountHeadID = 3 AND j.JournalDate BETWEEN '{0}' AND '{1}'
                                                    GROUP BY pa.AccountsName, at.AccountTypeName,j.BranchID ,j.OrganizationID 
                                                    UNION
                                                    SELECT j.BranchID ,j.OrganizationID, pa.AccountsName, at.AccountTypeName, '0' AS DrAmount, SUM(j.Amount) AS CrAmount
                                                    FROM  Journal j LEFT JOIN  ParentAccount pa ON pa.AccountID = j.AccountID LEFT JOIN
                                                    AccountType at ON pa.AccountTypeID = at.AccountTypeID
                                                    WHERE     j.DrCrIndecator = 'Cr' AND at.AccountHeadID = 3 AND j.JournalDate BETWEEN '{0}' AND '{1}'
                                                    GROUP BY pa.AccountsName, at.AccountTypeName,j.BranchID ,j.OrganizationID) tbl
                                                    GROUP BY AccountsName, AccountTypeName,BranchID ,OrganizationID ) tblAsset
                                                    WHERE  Amount < 0) tblExpense1";



            public const string BalanceSheet = @"SELECT     BranchID, OrganizationID, 'Asset' AS AccountType, AccountsName, AccountTypeName, Amount
                                                FROM         (SELECT     *
                                                FROM          (SELECT     BranchID, OrganizationID, AccountsName, AccountTypeName, ISNULL(sum(DrAmount), 0) - ISNULL(sum(CrAmount), 0) AS Amount
                                                FROM          (SELECT     j.BranchID, j.OrganizationID, pa.AccountsName, at.AccountTypeName, SUM(j.Amount) AS DrAmount, '0' AS CrAmount
                                                FROM          Journal j LEFT JOIN
                                                ParentAccount pa ON pa.AccountID = j.AccountID LEFT JOIN
                                                AccountType at ON pa.AccountTypeID = at.AccountTypeID
                                                WHERE      j.DrCrIndecator = 'Dr' AND at.AccountHeadID = 1 and j.JournalDate between '{0}' and '{1}'
                                                GROUP BY pa.AccountsName, at.AccountTypeName, j.BranchID, j.OrganizationID
                                                UNION
                                                SELECT     j.BranchID, j.OrganizationID, pa.AccountsName, at.AccountTypeName, '0' AS DrAmount, SUM(j.Amount) AS CrAmount
                                                FROM         Journal j LEFT JOIN
                                                ParentAccount pa ON pa.AccountID = j.AccountID LEFT JOIN
                                                AccountType at ON pa.AccountTypeID = at.AccountTypeID
                                                WHERE     j.DrCrIndecator = 'Cr' AND at.AccountHeadID = 1 and j.JournalDate between '{0}' and '{1}'
                                                GROUP BY pa.AccountsName, at.AccountTypeName, j.BranchID, j.OrganizationID) tbl
                                                GROUP BY AccountsName, AccountTypeName, BranchID, OrganizationID) tblAsset
                                                WHERE      Amount > 0
                                                UNION
                                                /*--------------------------------------Liability Debit balance Asset*/ SELECT BranchID, OrganizationID, AccountsName, AccountTypeName, Amount AS Amount
                                                FROM         (SELECT     BranchID, OrganizationID, AccountsName, AccountTypeName, ISNULL(sum(DrAmount), 0) - ISNULL(sum(CrAmount), 0) AS Amount
                                                FROM          (SELECT     j.BranchID, j.OrganizationID, pa.AccountsName, at.AccountTypeName, SUM(j.Amount) AS DrAmount, '0' AS CrAmount
                                                FROM          Journal j LEFT JOIN
                                                ParentAccount pa ON pa.AccountID = j.AccountID LEFT JOIN
                                                AccountType at ON pa.AccountTypeID = at.AccountTypeID
                                                WHERE      j.DrCrIndecator = 'Dr' AND at.AccountHeadID = 2 and j.JournalDate between '{0}' and '{1}'
                                                GROUP BY pa.AccountsName, at.AccountTypeName, j.BranchID, j.OrganizationID
                                                UNION
                                                SELECT     j.BranchID, j.OrganizationID, pa.AccountsName, at.AccountTypeName, '0' AS DrAmount, SUM(j.Amount) AS CrAmount
                                                FROM         Journal j LEFT JOIN
                                                ParentAccount pa ON pa.AccountID = j.AccountID LEFT JOIN
                                                AccountType at ON pa.AccountTypeID = at.AccountTypeID
                                                WHERE     j.DrCrIndecator = 'Cr' AND at.AccountHeadID = 2 and j.JournalDate between '{0}' and '{1}'
                                                GROUP BY pa.AccountsName, at.AccountTypeName, j.BranchID, j.OrganizationID) tbl
                                                GROUP BY AccountsName, AccountTypeName, BranchID, OrganizationID) tblAsset
                                                WHERE     Amount > 0
                                                UNION
                                                /*------------------------------Owner Equity*/ SELECT BranchID, OrganizationID, AccountsName, AccountTypeName, Amount AS Amount
                                                FROM         (SELECT     BranchID, OrganizationID, AccountsName, AccountTypeName, ISNULL(sum(DrAmount), 0) - ISNULL(sum(CrAmount), 0) AS Amount
                                                FROM          (SELECT     j.BranchID, j.OrganizationID, pa.AccountsName, at.AccountTypeName, SUM(j.Amount) AS DrAmount, '0' AS CrAmount
                                                FROM          Journal j LEFT JOIN
                                                ParentAccount pa ON pa.AccountID = j.AccountID LEFT JOIN
                                                AccountType at ON pa.AccountTypeID = at.AccountTypeID
                                                WHERE      j.DrCrIndecator = 'Dr' AND at.AccountHeadID = 5 and j.JournalDate between '{0}' and '{1}'
                                                GROUP BY pa.AccountsName, at.AccountTypeName, BranchID, OrganizationID
                                                UNION
                                                SELECT     j.BranchID, j.OrganizationID, pa.AccountsName, at.AccountTypeName, '0' AS DrAmount, SUM(j.Amount) AS CrAmount
                                                FROM         Journal j LEFT JOIN
                                                ParentAccount pa ON pa.AccountID = j.AccountID LEFT JOIN
                                                AccountType at ON pa.AccountTypeID = at.AccountTypeID
                                                WHERE     j.DrCrIndecator = 'Cr' AND at.AccountHeadID = 5 and j.JournalDate between '{0}' and '{1}'
                                                GROUP BY pa.AccountsName, at.AccountTypeName, j.BranchID, j.OrganizationID) tbl
                                                GROUP BY AccountsName, AccountTypeName, BranchID, OrganizationID) tblAsset
                                                WHERE     Amount > 0) tblAsset
                                                UNION
                                                /*--------------------------------------- Asset Side Credit balace Liability*/ SELECT BranchID, OrganizationID, 'Liability' AS Liability, AccountsName, AccountTypeName, Amount
                                                FROM         (SELECT     BranchID, OrganizationID, AccountsName, AccountTypeName, Amount * - 1 AS Amount
                                                FROM          (SELECT     BranchID, OrganizationID, AccountsName, AccountTypeName, ISNULL(sum(DrAmount), 0) - ISNULL(sum(CrAmount), 0) AS Amount
                                                FROM          (SELECT     j.BranchID, j.OrganizationID, pa.AccountsName, at.AccountTypeName, SUM(j.Amount) AS DrAmount, '0' AS CrAmount
                                                FROM          Journal j LEFT JOIN
                                                ParentAccount pa ON pa.AccountID = j.AccountID LEFT JOIN
                                                AccountType at ON pa.AccountTypeID = at.AccountTypeID
                                                WHERE      j.DrCrIndecator = 'Dr' AND at.AccountHeadID = 1 and j.JournalDate between '{0}' and '{1}'
                                                GROUP BY pa.AccountsName, at.AccountTypeName, j.BranchID, j.OrganizationID
                                                UNION
                                                SELECT     j.BranchID, j.OrganizationID, pa.AccountsName, at.AccountTypeName, '0' AS DrAmount, SUM(j.Amount) AS CrAmount
                                                FROM         Journal j LEFT JOIN
                                                ParentAccount pa ON pa.AccountID = j.AccountID LEFT JOIN
                                                AccountType at ON pa.AccountTypeID = at.AccountTypeID
                                                WHERE     j.DrCrIndecator = 'Cr' AND at.AccountHeadID = 1 and j.JournalDate between '{0}' and '{1}'
                                                GROUP BY pa.AccountsName, at.AccountTypeName, j.BranchID, j.OrganizationID) tbl
                                                GROUP BY AccountsName, AccountTypeName, BranchID, OrganizationID) tblAsset
                                                WHERE      Amount < 0
                                                UNION
                                                /*-------------------------------------------Liability*/ SELECT BranchID, OrganizationID, AccountsName, AccountTypeName, Amount * - 1 AS Amount
                                                FROM         (SELECT     BranchID, OrganizationID, AccountsName, AccountTypeName, ISNULL(sum(DrAmount), 0) - ISNULL(sum(CrAmount), 0) AS Amount
                                                FROM          (SELECT     j.BranchID, j.OrganizationID, pa.AccountsName, at.AccountTypeName, SUM(j.Amount) AS DrAmount, '0' AS CrAmount
                                                FROM          Journal j LEFT JOIN
                                                ParentAccount pa ON pa.AccountID = j.AccountID LEFT JOIN
                                                AccountType at ON pa.AccountTypeID = at.AccountTypeID
                                                WHERE      j.DrCrIndecator = 'Dr' AND at.AccountHeadID = 2 and j.JournalDate between '{0}' and '{1}'
                                                GROUP BY pa.AccountsName, at.AccountTypeName, j.BranchID, j.OrganizationID
                                                UNION
                                                SELECT     j.BranchID, j.OrganizationID, pa.AccountsName, at.AccountTypeName, '0' AS DrAmount, SUM(j.Amount) AS CrAmount
                                                FROM         Journal j LEFT JOIN
                                                ParentAccount pa ON pa.AccountID = j.AccountID LEFT JOIN
                                                AccountType at ON pa.AccountTypeID = at.AccountTypeID
                                                WHERE     j.DrCrIndecator = 'Cr' AND at.AccountHeadID = 2 and j.JournalDate between '{0}' and '{1}'
                                                GROUP BY pa.AccountsName, at.AccountTypeName, j.BranchID, j.OrganizationID) tbl
                                                GROUP BY AccountsName, AccountTypeName, BranchID, OrganizationID) tblAsset
                                                WHERE     Amount < 0
                                                UNION
                                                /*---------------------------------Owner equity */ SELECT BranchID, OrganizationID, AccountsName, AccountTypeName, Amount * - 1 AS Amount
                                                FROM         (SELECT     BranchID, OrganizationID, AccountsName, AccountTypeName, ISNULL(sum(DrAmount), 0) - ISNULL(sum(CrAmount), 0) AS Amount
                                                FROM          (SELECT     j.BranchID, j.OrganizationID, pa.AccountsName, at.AccountTypeName, SUM(j.Amount) AS DrAmount, '0' AS CrAmount
                                                FROM          Journal j LEFT JOIN
                                                ParentAccount pa ON pa.AccountID = j.AccountID LEFT JOIN
                                                AccountType at ON pa.AccountTypeID = at.AccountTypeID
                                                WHERE      j.DrCrIndecator = 'Dr' AND at.AccountHeadID = 5 and j.JournalDate between '{0}' and '{1}'
                                                GROUP BY pa.AccountsName, at.AccountTypeName, j.BranchID, j.OrganizationID
                                                UNION
                                                SELECT     j.BranchID, j.OrganizationID, pa.AccountsName, at.AccountTypeName, '0' AS DrAmount, SUM(j.Amount) AS CrAmount
                                                FROM         Journal j LEFT JOIN
                                                ParentAccount pa ON pa.AccountID = j.AccountID LEFT JOIN
                                                AccountType at ON pa.AccountTypeID = at.AccountTypeID
                                                WHERE     j.DrCrIndecator = 'Cr' AND at.AccountHeadID = 5 and j.JournalDate between '{0}' and '{1}'
                                                GROUP BY pa.AccountsName, at.AccountTypeName, j.BranchID, j.OrganizationID) tbl
                                                GROUP BY AccountsName, AccountTypeName, BranchID, OrganizationID) tblAsset
                                                WHERE     Amount < 0) tblLiability";

            public const string BalanceSheetDetail = @"SELECT     BranchID, OrganizationID, 'Asset' AS AccountType, AccountsName, AccountTypeName, Amount
                                                    FROM         (
                                                    SELECT     *
                                                    FROM          (
                                                    SELECT     BranchID, OrganizationID, AccountsName, AccountTypeName, ISNULL(sum(DrAmount), 0) - ISNULL(sum(CrAmount), 0) AS Amount
                                                    FROM          (
                                                    SELECT     j.BranchID, j.OrganizationID, isnull(ca.Description, pa.AccountsName) AccountsName, at.AccountTypeName, SUM(j.Amount) AS DrAmount, '0' AS CrAmount
                                                    FROM          Journal j LEFT JOIN
                                                    ParentAccount pa ON pa.AccountID = j.AccountID LEFT JOIN
                                                    AccountType at ON pa.AccountTypeID = at.AccountTypeID
                                                    left outer join ChildAccount ca on  j.ChildAccountID=ca.ChildAccountID
                                                    WHERE      j.DrCrIndecator = 'Dr' AND at.AccountHeadID = 1 
                                                    and j.JournalDate between '{1}' and '{2}' and j.BranchID={0}
                                                    GROUP BY ca.Description,AccountsName ,at.AccountTypeName, j.BranchID, j.OrganizationID
                                                    UNION
                                                    SELECT     j.BranchID, j.OrganizationID, isnull(ca.Description, pa.AccountsName) AccountsName, at.AccountTypeName, '0' AS DrAmount, SUM(j.Amount) AS CrAmount
                                                    FROM         Journal j LEFT JOIN
                                                    ParentAccount pa ON pa.AccountID = j.AccountID LEFT JOIN
                                                    AccountType at ON pa.AccountTypeID = at.AccountTypeID
                                                    left outer join ChildAccount ca on j.ChildAccountID=ca.ChildAccountID
                                                    WHERE     j.DrCrIndecator = 'Cr' AND at.AccountHeadID = 1
                                                     and j.JournalDate between '{1}' and '{2}' and j.BranchID={0}
                                                    GROUP BY ca.Description, pa.AccountsName, at.AccountTypeName, j.BranchID, j.OrganizationID) tbl
                                                    GROUP BY AccountsName, AccountTypeName, BranchID, OrganizationID) tblAsset
                                                    WHERE      Amount > 0
                                                    UNION
                                                    /*--------------------------------------Liability Debit balance Asset*/ 
                                                    SELECT BranchID, OrganizationID, AccountsName, AccountTypeName, Amount AS Amount
                                                    FROM         (SELECT     BranchID, OrganizationID, AccountsName, AccountTypeName, ISNULL(sum(DrAmount), 0) - ISNULL(sum(CrAmount), 0) AS Amount
                                                    FROM          (
                                                    SELECT     j.BranchID, j.OrganizationID, isnull(ca.Description, pa.AccountsName) AccountsName, at.AccountTypeName, SUM(j.Amount) AS DrAmount, '0' AS CrAmount
                                                    FROM          Journal j LEFT JOIN
                                                    ParentAccount pa ON pa.AccountID = j.AccountID LEFT JOIN
                                                    AccountType at ON pa.AccountTypeID = at.AccountTypeID
                                                    left outer join ChildAccount ca on j.ChildAccountID=ca.ChildAccountID
                                                    WHERE      j.DrCrIndecator = 'Dr' AND at.AccountHeadID = 2 
                                                    and j.JournalDate between '{1}' and '{2}' and j.BranchID={0}
                                                    GROUP BY ca.Description, pa.AccountsName, at.AccountTypeName, j.BranchID, j.OrganizationID
                                                    UNION
                                                    SELECT     j.BranchID, j.OrganizationID, isnull(ca.Description, pa.AccountsName) AccountsName, at.AccountTypeName, '0' AS DrAmount, SUM(j.Amount) AS CrAmount
                                                    FROM         Journal j LEFT JOIN
                                                    ParentAccount pa ON pa.AccountID = j.AccountID LEFT JOIN
                                                    AccountType at ON pa.AccountTypeID = at.AccountTypeID
                                                    left outer join ChildAccount ca on j.ChildAccountID=ca.ChildAccountID
                                                    WHERE     j.DrCrIndecator = 'Cr' AND at.AccountHeadID = 2 
                                                    and j.JournalDate between '{1}' and '{2}' and j.BranchID={0}
                                                    GROUP BY ca.Description, pa.AccountsName, at.AccountTypeName, j.BranchID, j.OrganizationID) tbl
                                                    GROUP BY AccountsName, AccountTypeName, BranchID, OrganizationID) tblAsset
                                                    WHERE     Amount > 0
                                                    UNION
                                                    /*------------------------------Owner Equity*/ 
                                                    SELECT BranchID, OrganizationID, AccountsName, AccountTypeName, Amount AS Amount
                                                    FROM         (SELECT     BranchID, OrganizationID, AccountsName, AccountTypeName, ISNULL(sum(DrAmount), 0) - ISNULL(sum(CrAmount), 0) AS Amount
                                                    FROM          (
                                                    SELECT     j.BranchID, j.OrganizationID, isnull(ca.Description, pa.AccountsName) AccountsName, at.AccountTypeName, SUM(j.Amount) AS DrAmount, '0' AS CrAmount
                                                    FROM          Journal j LEFT JOIN
                                                    ParentAccount pa ON pa.AccountID = j.AccountID LEFT JOIN
                                                    AccountType at ON pa.AccountTypeID = at.AccountTypeID
                                                    left outer join ChildAccount ca on j.ChildAccountID=ca.ChildAccountID
                                                    WHERE      j.DrCrIndecator = 'Dr' AND at.AccountHeadID = 5 
                                                    and j.JournalDate between '{1}' and '{2}' and j.BranchID={0}
                                                    GROUP BY ca.Description, pa.AccountsName, at.AccountTypeName, j.BranchID, j.OrganizationID
                                                    UNION
                                                    SELECT     j.BranchID, j.OrganizationID,isnull(ca.Description, pa.AccountsName) AccountsName, at.AccountTypeName, '0' AS DrAmount, SUM(j.Amount) AS CrAmount
                                                    FROM         Journal j LEFT JOIN
                                                    ParentAccount pa ON pa.AccountID = j.AccountID LEFT JOIN
                                                    AccountType at ON pa.AccountTypeID = at.AccountTypeID
                                                    left outer join ChildAccount ca on j.ChildAccountID=ca.ChildAccountID
                                                    WHERE     j.DrCrIndecator = 'Cr' AND at.AccountHeadID = 5
                                                     and j.JournalDate between '{1}' and '{2}' and j.BranchID={0}
                                                    GROUP BY ca.Description, pa.AccountsName, at.AccountTypeName, j.BranchID, j.OrganizationID) tbl
                                                    GROUP BY AccountsName, AccountTypeName, BranchID, OrganizationID) tblAsset
                                                    WHERE     Amount > 0) tblAsset
                                                    UNION
                                                    /*--------------------------------------- Asset Side Credit balace Liability*/ 
                                                    SELECT BranchID, OrganizationID, 'Liability' AS Liability, AccountsName, AccountTypeName, Amount
                                                    FROM         (SELECT     BranchID, OrganizationID, AccountsName, AccountTypeName, Amount * - 1 AS Amount
                                                    FROM          (SELECT     BranchID, OrganizationID, AccountsName, AccountTypeName, ISNULL(sum(DrAmount), 0) - ISNULL(sum(CrAmount), 0) AS Amount
                                                    FROM          (
                                                    SELECT     j.BranchID, j.OrganizationID, isnull(ca.Description, pa.AccountsName) AccountsName, at.AccountTypeName, SUM(j.Amount) AS DrAmount, '0' AS CrAmount
                                                    FROM          Journal j LEFT JOIN
                                                    ParentAccount pa ON pa.AccountID = j.AccountID LEFT JOIN
                                                    AccountType at ON pa.AccountTypeID = at.AccountTypeID
                                                    left outer join ChildAccount ca on j.ChildAccountID=ca.ChildAccountID
                                                    WHERE      j.DrCrIndecator = 'Dr' AND at.AccountHeadID = 1 
                                                    and j.JournalDate between '{1}' and '{2}' and j.BranchID={0}
                                                    GROUP BY ca.Description, pa.AccountsName, at.AccountTypeName, j.BranchID, j.OrganizationID
                                                    UNION
                                                    SELECT     j.BranchID, j.OrganizationID,  isnull(ca.Description, pa.AccountsName) AccountsName, at.AccountTypeName, '0' AS DrAmount, SUM(j.Amount) AS CrAmount
                                                    FROM         Journal j LEFT JOIN
                                                    ParentAccount pa ON pa.AccountID = j.AccountID LEFT JOIN
                                                    AccountType at ON pa.AccountTypeID = at.AccountTypeID
                                                    left outer join ChildAccount ca on j.ChildAccountID=ca.ChildAccountID
                                                    WHERE     j.DrCrIndecator = 'Cr' AND at.AccountHeadID = 1 
                                                    and j.JournalDate between '{1}' and '{2}' and j.BranchID={0}
                                                    GROUP BY ca.Description,pa.AccountsName, at.AccountTypeName, j.BranchID, j.OrganizationID) tbl
                                                    GROUP BY AccountsName, AccountTypeName, BranchID, OrganizationID) tblAsset
                                                    WHERE      Amount < 0
                                                    UNION
                                                    /*-------------------------------------------Liability*/ SELECT BranchID, OrganizationID, AccountsName, AccountTypeName, Amount * - 1 AS Amount
                                                    FROM         (SELECT     BranchID, OrganizationID, AccountsName, AccountTypeName, ISNULL(sum(DrAmount), 0) - ISNULL(sum(CrAmount), 0) AS Amount
                                                    FROM          (
                                                    SELECT     j.BranchID, j.OrganizationID, isnull(ca.Description, pa.AccountsName) AccountsName, at.AccountTypeName, SUM(j.Amount) AS DrAmount, '0' AS CrAmount
                                                    FROM          Journal j LEFT JOIN
                                                    ParentAccount pa ON pa.AccountID = j.AccountID LEFT JOIN
                                                    AccountType at ON pa.AccountTypeID = at.AccountTypeID
                                                    left outer join ChildAccount ca on j.ChildAccountID=ca.ChildAccountID
                                                    WHERE      j.DrCrIndecator = 'Dr' AND at.AccountHeadID = 2 
                                                    and j.JournalDate between '{1}' and '{2}' and j.BranchID={0}
                                                    GROUP BY ca.Description, pa.AccountsName, at.AccountTypeName, j.BranchID, j.OrganizationID
                                                    UNION
                                                    SELECT     j.BranchID, j.OrganizationID, isnull(ca.Description, pa.AccountsName) AccountsName, at.AccountTypeName, '0' AS DrAmount, SUM(j.Amount) AS CrAmount
                                                    FROM         Journal j LEFT JOIN
                                                    ParentAccount pa ON pa.AccountID = j.AccountID LEFT JOIN
                                                    AccountType at ON pa.AccountTypeID = at.AccountTypeID
                                                    left outer join ChildAccount ca on j.ChildAccountID=ca.ChildAccountID
                                                    WHERE     j.DrCrIndecator = 'Cr' AND at.AccountHeadID = 2 
                                                    and j.JournalDate between '{1}' and '{2}' and j.BranchID={0}
                                                    GROUP BY ca.Description, pa.AccountsName, at.AccountTypeName, j.BranchID, j.OrganizationID) tbl
                                                    GROUP BY AccountsName, AccountTypeName, BranchID, OrganizationID) tblAsset
                                                    WHERE     Amount < 0
                                                    UNION
                                                    /*---------------------------------Owner equity */ SELECT BranchID, OrganizationID, AccountsName, AccountTypeName, Amount * - 1 AS Amount
                                                    FROM         (SELECT     BranchID, OrganizationID, AccountsName, AccountTypeName, ISNULL(sum(DrAmount), 0) - ISNULL(sum(CrAmount), 0) AS Amount
                                                    FROM          (
                                                    SELECT     j.BranchID, j.OrganizationID, isnull(ca.Description, pa.AccountsName) AccountsName, at.AccountTypeName, SUM(j.Amount) AS DrAmount, '0' AS CrAmount
                                                    FROM          Journal j LEFT JOIN
                                                    ParentAccount pa ON pa.AccountID = j.AccountID LEFT JOIN
                                                    AccountType at ON pa.AccountTypeID = at.AccountTypeID
                                                    left outer join ChildAccount ca on j.ChildAccountID=ca.ChildAccountID
                                                    WHERE      j.DrCrIndecator = 'Dr' AND at.AccountHeadID = 5 
                                                    and j.JournalDate between '{1}' and '{2}' and j.BranchID={0}
                                                    GROUP BY ca.Description, pa.AccountsName, at.AccountTypeName, j.BranchID, j.OrganizationID
                                                    UNION
                                                    SELECT     j.BranchID, j.OrganizationID, isnull(ca.Description, pa.AccountsName) AccountsName, at.AccountTypeName, '0' AS DrAmount, SUM(j.Amount) AS CrAmount
                                                    FROM         Journal j LEFT JOIN
                                                    ParentAccount pa ON pa.AccountID = j.AccountID LEFT JOIN
                                                    AccountType at ON pa.AccountTypeID = at.AccountTypeID
                                                    left outer join ChildAccount ca on j.ChildAccountID=ca.ChildAccountID
                                                    WHERE     j.DrCrIndecator = 'Cr' AND at.AccountHeadID = 5 
                                                    and j.JournalDate between '{1}' and '{2}' and j.BranchID={0}
                                                    GROUP BY ca.Description, pa.AccountsName, at.AccountTypeName, j.BranchID, j.OrganizationID) tbl
                                                    GROUP BY AccountsName, AccountTypeName, BranchID, OrganizationID) tblAsset
                                                    WHERE     Amount < 0) tblLiability";

            public const string GET_ALL_PRODUCT_BY_MODEL = @"Select p.*,pm.Name as ProductModelName  from ProductInformation p
                                                            join ProductModel pm on 
                                                            p.ProductModelID=pm.ProductModelID ";

            public const string GET_ALL_DAILY_CASH_RECEIVE_INFORMATION_DATE = @"Select ReferenceNo, ReceiveDate,  sum(Amount) as Amount, CustomerID from (
                                                                                    Select ReferenceNo, CONVERT(CHAR(10), ReceiveDate, 121) as ReceiveDate,  SUM(Amount) as Amount, CustomerID  from CashReceive
                                                                                    group by CustomerID,ReceiveDate,ReferenceNo 
                                                                                    ) tbl  Where tbl. ReceiveDate between '{0}' and '{1}' 
                                                                                    group by tbl.CustomerID,tbl.ReceiveDate ,tbl.ReferenceNo   ";


            public const string GET_TOTAL_CASH_RECEIVE_INFORMATION__BY_DATE = @"select c.CustomerName, c.Address ,c.Phone ,tbl.ReceiveAmount,tbl .Discount  ,tbl.ReceiveDate ,tbl.BillRefNumber  from(
                                                                                Select SUM (ReceiveAmount) as ReceiveAmount, SUM(Discount) as Discount, ReceiveDate ,CustomerID,BillRefNumber  from (
                                                                                Select cs.BranchID ,cs.OrganizationID , cs.Discount ,cs.ReceiveAmount,'N/A' BillRefNumber, cs.SalesDate as ReceiveDate , cs.CustomerID   from CashSales cs
                                                                                union
                                                                                Select cr.BranchID , cr.OrganizationID , cr.Discount ,cr.Amount as ReceiveAmount, cr.BillRefNumber, cr.ReceiveDate,  cr.CustomerID  from CashReceive cr )tbl 
                                                                                where ReceiveDate between  '{0}' and '{1}' and BranchID ='{2}' and OrganizationID ='{3}'
                                                                                group by 
                                                                                CustomerID , ReceiveDate,BillRefNumber )tbl
                                                                                left join 
                                                                                Customer c on tbl.CustomerID =c.CustomerID  ";



            public const string GET_TOTAL_CASH_PAYMENT_INFORMATION__BY_DATE = @"Select tbl1 .PaymentDate,s.SupplierName, cu.CustomerName ,tbl1.PaymentAmount   from (
                                                                                Select tbl.PaymentDate,  tbl.SupplierID, tbl.SalesID, SUM(tbl.PaymentAmount) as PaymentAmount from(
                                                                                Select BranchID, OrganizationID, PurchaseDate as PaymentDate, SupplierID,SalesID,  SUM(isnull(PaidAmount,0)) as PaymentAmount  from PurchaseOrder 
                                                                                where PurchaseDate between '{0}' and '{1}' and BranchID ='{2}' and OrganizationID ='{3}'
                                                                                group by SupplierID, SalesID,PurchaseDate, BranchID ,OrganizationID 
                                                                                union
                                                                                Select p.BranchID ,p.OrganizationID , p.PaymentDate, p.SupplierID,'0' as SalesID, SUM(isnull(p.Amount,0))as PaymentAmount from Payment p
                                                                                where PaymentDate between '{0}' and '{1}' and BranchID ='{2}' and OrganizationID ='{3}'
                                                                                group by p.SupplierID,PaymentDate, BranchID ,OrganizationID )tbl
                                                                                group by tbl .SupplierID,tbl.SalesID,tbl.PaymentDate )tbl1
                                                                                left join CashSales cs on tbl1 .SalesID=cs.SalesID
                                                                                left join Customer cu on cu.CustomerID=cs.CustomerID
                                                                                left join Suppliers s on s.SupplierID=tbl1.SupplierID 
                                                                                where tbl1 .PaymentAmount >0 ";

            public const string GET_GIVEN_SAMPLE_BY_PRODUCTID = @"Select  ISNULL( SUM(GivenQuantity),0)- ISNULL( SUM(ReceiveQuantity),0) as DistributeQuantity  from dbo.DistributeSample
                                                                where ProductID ='{0}' and BranchID ='{1}' and OrganizationID ='{2}'
                                                                group by ProductID ";

            public const string GET_GIVEN_SAMPLE_BY_PRODUCTID_AND_DATE = @"Select ProductID ,  ISNULL( SUM(GivenQuantity),0)- ISNULL( SUM(ReceiveQuantity),0) as GivenQuantity  from dbo.DistributeSample
                                                                            where BranchID ={0} And OrganizationID ={1}  and DistributeDate between '{2}' and '{3}'                                                          
                                                                            group by ProductID ";


            public const string GET_TRANSFER_PRODUCT_BY_PRODUCTID = @"Select SUM (Quantity ) as Quantity from TransferDetail 
                                                                    left join Transfer t on t.TransferID =TransferDetail .TransferID 
                                                                    where ProductCode ='{0}' and t.BranchID='{1}' and t.OrganizationID='{2}'  group by ProductCode ";

            public const string GET_RECEIVE_PRODUCT_BY_PRODUCTID = @"Select SUM (Quantity ) as Quantity from ReceiveProductDetail rd
                                                                    left join ReceiveProduct t on t.ReceiveProductID  =rd.ReceiveProductID 
                                                                    where ProductCode ='{0}' and t.BranchID='{1}' and t.OrganizationID='{2}'  group by ProductCode";


            public const string GET_DAMAGE_PRODUCT_BY_PRODUCTID = @"Select SUM (Quantity ) as Quantity from DamageDetail 
                                                                    left join DamageInfo d on DamageDetail .DamageInfoID =d.DamageInfoID 
                                                                    where ProductCode ='{0}' and d.BranchID ='{1}' and d.OrganizationID ='{2}'   group by ProductCode ";


            public const string GET_DAMAGE_PRODUCT_BY_PRODUCTID_AND_DATE = @"Select ProductCode, SUM (Quantity ) as Quantity from DamageDetail dd
                                                                            join DamageInfo d on d.DamageInfoID =dd.DamageInfoID    
                                                                            where d.BranchID ={0} and d.OrganizationID ={1} and d.DamageDate between '{2}' and '{3}'  
                                                                            group by ProductCode";

            public const string GET_DAMAGE_PRODUCT_HISTORY_AND_DATE = @"Select d.DamageInfoID, d.DamageDate, d.DamageReason, dd.ProductCode, p.ProductName , dd.Quantity  from dbo.DamageDetail dd
                                                                            join dbo.DamageInfo d on d.DamageInfoID=dd.DamageInfoID
                                                                            left Join ProductInformation p on p.ProductID =dd.ProductCode 
                                                                            Where d.DamageDate between '{0}' and '{1}' and d.BranchID ={2} and d.OrganizationID={3}
                                                                            order by p.ProductName";


            public const string PREVIOUS_STOCK_QUANTITY = @"	Select tbl3.ProductID, p.ProductName, tbl3.PreviousQuantity from (
	Select tbl2.ProductID ,SUM(tbl2.PurchaseQuantity ) - SUM(tbl2.SalesQuantity) as PreviousQuantity from (
	
	Select p.ProductID,sum(isnull(Quantity,0)) as PurchaseQuantity,'0' as SalesQuantity   
	from	ProductInformation p left outer join 
	PurchaseOrderDetails pod  on p.ProductID=pod.ProductID
	left join PurchaseOrder po
	on pod.PurchaseID =po.PurchaseID
	where po.Status =2 and po.PurchaseDate <'{0}' and po.BranchID ={1} and po.OrganizationID={2}
	group by p.ProductID
	
	union
	Select p.ProductID  as ProductID ,sum( isnull( rpd.Quantity,0)) as PurchaseQuantity,'0' as SalesQuantity 
	from ProductInformation p left outer join ReceiveProductDetail rpd
	on p.ProductID=rpd.ProductCode
	left outer join ReceiveProduct  po
	on rpd.ReceiveProductID  =po.ReceiveProductID  
	where po.ReceiveDate <'{0}' 	and
	  po.BranchID ={1} and po.OrganizationID={2} 
	group by p.ProductID 
	union
	Select srd.ProductCode as ProductID , SUM(srd.Quantity) as PurchaseQuantity ,0 SalesQuantity from SalesReturn sr 
	left join SalesReturnDetail srd
	on sr.SalesReturnID =srd.SalesReturnID 
	where sr.ReturnDate <'{0}' and BranchID ={1} and OrganizationID={2}
	group by srd.Productcode 
	union
	Select p.ProductID,'0' as PurchaseQuantity,'0' as SalesQuantity   from
	ProductInformation p left outer join 
	PurchaseOrderDetails pod  on p.ProductID=pod.ProductID
	left join PurchaseOrder po
	on pod.PurchaseID =po.PurchaseID where po.Status =2 and po.BranchID ={1} and po.OrganizationID={2}
	group by p.ProductID
	union
	Select tbl.ProductID, '0' as PurchaseQuantity , SUM(tbl.Quantity) as SalesQuantity from (
	Select ProductId, Sum(Quantity) as Quantity   from CashSalesOrderDetails csd
	join CashSales cs on csd.SalesID =cs.SalesID  where cs.SalesDate <'{0}' and cs.BranchID ={1} and cs.OrganizationID={2}
	group by ProductId  

	union

	Select td.ProductCode as ProductId , SUM (Quantity ) as Quantity from TransferDetail  td
	join Transfer t on t.TransferID =td.TransferID 
	where TransferDate <'{0}' and t.BranchID ={1} and t.OrganizationID={2} group by td.ProductCode 

	union
	Select dd.ProductCode as ProductID , SUM(dd.Quantity) as Quantity  from DamageInfo left join DamageDetail dd
	on DamageInfo.DamageInfoID =dd.DamageInfoID where DamageInfo.DamageDate <'{0}' and BranchID ={1} and OrganizationID={2}
	group by dd.ProductCode 
	union 
	Select srd.ProductCode as ProductID , SUM(srd.Quantity) as Quantity  from SalesReturn sr 
	left join SalesReturnDetail srd
	on sr.SalesReturnID =srd.SalesReturnID 
	where sr.ReturnDate <'{0}' and BranchID ={1} and OrganizationID={2}
	group by srd.Productcode 

	union
	Select ProductID ,SUM(GivenQuantity) - SUM (ReceiveQuantity) as Quantity from DistributeSample 
	where DistributeDate< '{0}' and BranchID ={1} and OrganizationID={2} group by ProductID )tbl
	group by tbl.ProductID )tbl2 group by tbl2 .ProductID )tbl3
	join ProductInformation p on tbl3.ProductID=p.ProductID order by P.ProductName";

            public const string GET_ALL_SALES_PRODUCT_BY_PRODUCTID_DATE = @"Select csd.ProductID ,Sum(Quantity) as Quantity   from CashSalesOrderDetails csd
                                                                            join CashSales cs on csd.SalesID =cs.SalesID  where BranchID ={0} And OrganizationID ={1}  and cs.SalesDate  between '{2}' and '{3}'

                                                                            group by ProductId ";

            public const string GET_ALL_TRANSFER_PRODUCT_BY_PRODUCTID_DATE = @"Select ProductCode ,SUM (Quantity ) as Quantity from TransferDetail  td
                                                                                join Transfer t on t.TransferID =td.TransferID 
                                                                                where BranchID ={0} And OrganizationID ={1}  and TransferDate between '{2}' and '{3}'
                                                                                group by ProductCode ";
            public const string GET_ALL_SALES_RETURN_PRODUCT_BY_PRODUCTID_DATE = @"Select srd.ProductCode ,SUM (Quantity ) as Quantity from SalesReturnDetail  srd
	                                                                            join SalesReturn sr on sr.SalesReturnID  =srd.SalesReturnID 
	                                                                            where BranchID ={0} And OrganizationID ={1}  and ReturnDate between '{2}' and '{3}'
	                                                                            group by ProductCode";

            public const string GET_ALL_PURCHASE_RETURN_PRODUCT_BY_PRODUCTID_DATE = @"Select prd.ProductID ,SUM (Quantity ) as Quantity from PurchaseReturnDetail  prd
                                                                                    join PurchaseReturn pr on pr.PurchaseReturnID  =prd.PurchaseReturnID                
	                                                                                where BranchID ={0} And OrganizationID ={1}  and ReturnDate between '{2}' and '{3}'
	                                                                                group by ProductID";


            public const string GET_ALL_RECEIVE_PRODUCT_BY_PRODUCTID_DATE = @"Select ProductID as ProductCode ,Sum(Quantity) as Quantity from (  
                                                                                    Select BranchID ,OrganizationID , ProductID, PurchaseDate , SUM(PurchaseQuantity ) as Quantity from (
                                                                                    Select po.BranchID ,po.OrganizationID , pod.ProductID ,po.PurchaseDate,sum(Quantity) as PurchaseQuantity     from PurchaseOrderDetails pod left join PurchaseOrder po
                                                                                    on pod.PurchaseID =po.PurchaseID where Po.Status=2 group by pod.ProductID, po.PurchaseDate ,po.BranchID, po.OrganizationID 
                                                                                    UNION
                                                                                    SELECT BranchID ,OrganizationID ,    rd.ProductCode AS ProductId, r.ReceiveDate as PurchaseDate , SUM(rd.Quantity) AS PurchaseQuantity 
                                                                                    FROM         dbo.ReceiveProductDetail AS rd INNER JOIN
                                                                                    dbo.ReceiveProduct AS r ON r.ReceiveProductID = rd.ReceiveProductID 
                                                                                    GROUP BY rd.ProductCode,r.ReceiveDate  ,BranchID ,OrganizationID  )tbl
                                                                                    Group by ProductID,PurchaseDate,BranchID ,OrganizationID  )tbl2
                                                                                    where  BranchID={0} And OrganizationID ={1} And PurchaseDate between '{2}' and '{3}' group by ProductID";

            public const string PRODUCT_INFORMATION_FOR_SALES = @"Select *  from (
	                                                                SELECT  tbl3.BranchID ,tbl3 .OrganizationID ,  tbl3.ProductID, p.ProductName,p.TypeName, p.Barcode, p.ProductModelName as Model,  tbl3.Quantity
	                                                                FROM         (
	
	                                                                SELECT  BranchID ,OrganizationID ,   ProductID, SUM(PurchaseQuantity) - SUM(SalesQuantity) AS Quantity
	                                                                FROM (
	
	                                                                SELECT  po.BranchID ,po.OrganizationID ,   pod.ProductID, SUM(pod.Quantity) AS PurchaseQuantity, '0' AS SalesQuantity
	                                                                FROM          dbo.PurchaseOrderDetails AS pod LEFT OUTER JOIN
	                                                                dbo.PurchaseOrder AS po ON pod.PurchaseID = po.PurchaseID
	                                                                WHERE      (po.Status = 2)
	                                                                GROUP BY pod.ProductID,po.BranchID ,po.OrganizationID 
	
	                                                                UNION
	                                                                SELECT BranchID ,OrganizationID ,    pod.ProductID, '0' AS PurchaseQuantity, '0' AS SalesQuantity
	                                                                FROM         dbo.PurchaseOrderDetails AS pod LEFT OUTER JOIN
	                                                                dbo.PurchaseOrder AS po ON pod.PurchaseID = po.PurchaseID
	                                                                WHERE     (po.Status = 2)
	                                                                GROUP BY pod.ProductID,BranchID ,OrganizationID 
	
                                                                    UNION
                                                                    SELECT BranchID ,OrganizationID ,    rd.ProductCode AS ProductId, SUM(rd.Quantity) AS PurchaseQuantity ,'0' as SalesQuantity
                                                                    FROM         dbo.ReceiveProductDetail AS rd INNER JOIN
                                                                    dbo.ReceiveProduct AS r ON r.ReceiveProductID = rd.ReceiveProductID 
                                                                    GROUP BY rd.ProductCode ,BranchID ,OrganizationID 

                                                                    UNION
                                                                    SELECT BranchID ,OrganizationID ,    rd.ProductCode AS ProductId, SUM(rd.Quantity) AS PurchaseQuantity ,'0' as SalesQuantity
                                                                    FROM         dbo.SalesReturnDetail AS rd INNER JOIN
                                                                    dbo.SalesReturn AS r ON r.SalesReturnID  = rd.SalesReturnID  
                                                                    GROUP BY rd.ProductCode ,BranchID ,OrganizationID 


	                                                                UNION
	
	
	                                                                SELECT BranchID, OrganizationID , ProductID, '0' AS PurchaseQuantity, SUM(Quantity) AS SalesQuantity
	                                                                FROM         (
	
	                                                                SELECT  cs.BranchID ,cs.OrganizationID ,   csd.ProductID, SUM(csd.Quantity) AS Quantity
	                                                                FROM          dbo.CashSalesOrderDetails AS csd INNER JOIN
	                                                                dbo.CashSales AS cs ON csd.SalesID = cs.SalesID
	                                                                GROUP BY csd.ProductID,cs.BranchID ,cs.OrganizationID 
	
	                                                                UNION
	                                                                SELECT BranchID ,OrganizationID ,    td.ProductCode AS ProductId, SUM(td.Quantity) AS Quantity
	                                                                FROM         dbo.TransferDetail AS td INNER JOIN
	                                                                dbo.Transfer AS t ON t.TransferID = td.TransferID
	                                                                GROUP BY td.ProductCode ,BranchID ,OrganizationID 
	
                                                                   
	                                                                UNION
	                                                                SELECT  BranchID ,OrganizationID ,   dd.ProductCode AS ProductID, SUM(dd.Quantity) AS Quantity
	                                                                FROM         dbo.DamageInfo LEFT OUTER JOIN
	                                                                dbo.DamageDetail AS dd ON dbo.DamageInfo.DamageInfoID = dd.DamageInfoID
	                                                                GROUP BY dd.ProductCode,BranchID ,OrganizationID 
	
	                                                                UNION
	                                                                SELECT BranchID ,OrganizationID , ProductID, SUM(GivenQuantity) - SUM(ReceiveQuantity) AS Quantity
	                                                                FROM         dbo.DistributeSample
	                                                                GROUP BY ProductID ,BranchID ,OrganizationID 
	
	                                                                ) AS tbl
	                                                                GROUP BY ProductID,BranchID, OrganizationID
	                                                                ) AS tbl2
	                                                                GROUP BY ProductID,BranchID ,OrganizationID ) AS tbl3 INNER JOIN
	                                                                dbo.ProductInformation AS p ON tbl3.ProductID = p.ProductID
	                                                                )as tt  where tt.BranchID={0} and OrganizationID={1} ORDER BY ProductName";


            public const string GET_SALES_RETURN_HISTORY_BY_DATE_AND_ORGANIZATION = @"Select sr.SalesReturnID, sr.ReturnDate, sr.CustomerName, srd.ProductName, srd.Quantity, srd.Price,srd.Quantity*srd.Price as Total from dbo.SalesReturnDetail srd
                                                                                        join dbo.SalesReturn sr on srd.SalesReturnID= sr.SalesReturnID
                                                                                        Where sr.ReturnDate between '{0}' and '{1}' and sr.BranchID={2} and 
                                                                                        sr.OrganizationID={3}";

            public const string CUSTOMER_DUE_REPORT_BY_CUSTOEMRID_AND_BRANCHID = @"Select * from (
                                                                                Select cs.SalesDate , cs.BillNumber, 'Goods Sales' as Description, ((cs.SalesAmount +cs.CarryingLoading) -(cs.Discount +cs.ReceiveAmount )) as Debit,'0' as Credit ,cs.BranchID,cs.CustomerID , '2' SequenceNo from CashSales cs 
                                                                                where (cs.SalesAmount -(cs.Discount +cs.ReceiveAmount ))>0
                                                                                and cs.SalesDate between '{2}' and '{3}' 
                                                                                union
                                                                                Select sr.ReturnDate,  CONVERT(varchar(10), sr.SalesReturnID)  as BillNumber, 'Return' as Description, '0' as Debit ,(sr.Total -(sr.Discount +sr.PaidAmount )) as Credit,sr.BranchID,sr.CustomerID , '4' SequenceNo  from SalesReturn sr 
                                                                                where (sr.Total -(sr.Discount +sr.PaidAmount ))>0
                                                                                and sr.ReturnDate between '{2}' and '{3}' 
                                                                                union
                                                                                Select cr.ReceiveDate , CONVERT(varchar(10), cr.CashReceiveID)  as BillNumber, 'Received ( ' +BillRefNumber+' )' as Description ,'0' as Debit, (cr.Amount+cr.Discount ) as Credit ,cr.BranchID ,cr.CustomerID , '3' SequenceNo from dbo.CashReceive cr 
                                                                                where (cr.Amount+cr.Discount )>0
                                                                                and cr.ReceiveDate between '{2}' and '{3}' 
                                                                                union
                                                                                Select cd.DueDate, CONVERT(varchar(10),  cd.PreviousDueID)as BillNumber, 'Previous Due' as Description , cd.Amount as Debit ,'0' as Credit, cd.BranchID ,cd.CustomerID, '1' SequenceNo from CustomerPreviousDue cd 
                                                                                where Amount >0
                                                                                and cd.DueDate between '{2}' and '{3}' 
                                                                                union all
                                                                                Select jv.Date, jv.VoucherNumber  BillNumber, jv.Description ,  0 Debit ,jv.Amount Credit, jv.BranchID ,ca.ReferenceID CustomerID, '2' SequenceNumber from JournalVoucher jv
                                                                                left outer join ChildAccount ca
                                                                                on jv.DebitChildAccountID=ca.ChildAccountID or jv.CreditChildAccountID=ca.ChildAccountID
                                                                                where ca.ReferenceType=2 and ca.ReferenceID=1 and CreditAccountID>0
                                                                                and jv.Date between '{2}' and '{3}' 

                                                                                )tbl
                                                                                where tbl.CustomerID ={0} and tbl .BranchID ={1} order by tbl.SequenceNo";



            public const string CUSTOMER_TRANSACTION_REPORT_BY_CUSTOEMRID_AND_BRANCHID = @"Select * from (
                                                    Select cs.SalesDate , cs.BillNumber, 'Goods Sales' as Description,  (cs.SalesAmount + cs.CarryingLoading) as Debit,'0' as Credit ,cs.BranchID,cs.CustomerID ,'1' SequenceNumber   from CashSales cs 
                                                    union
                                                    Select cs.SalesDate , cs.BillNumber , 'Cash Receive' as Description,  '0' as Debit, (cs.ReceiveAmount ) as Credit ,cs.BranchID,cs.CustomerID ,'1' SequenceNumber   from CashSales cs where cs.ReceiveAmount>0

                                                    union
                                                    Select cs.SalesDate , cs.BillNumber, 'Sales Discount' as Description,  '0' as Debit, (cs.Discount ) as Credit ,cs.BranchID,cs.CustomerID ,'1' SequenceNumber   from CashSales cs where cs.Discount>0

                                                    union
                                                    Select sr.ReturnDate,  CONVERT(varchar(10), sr.SalesReturnID)  as BillNumber, 'Paid for Return' as Description,  sr.PaidAmount as Debit ,'0' as Credit,sr.BranchID,sr.CustomerID ,'3' SequenceNumber   from SalesReturn sr where sr.PaidAmount>0
                                                    union
                                                    Select sr.ReturnDate,  CONVERT(varchar(10), sr.SalesReturnID)  as BillNumber, 'Return' as Description, '0' as Debit ,(sr.Total ) as Credit,sr.BranchID,sr.CustomerID ,'2' SequenceNumber    from SalesReturn sr 
                                                    union
                                                    Select sr.ReturnDate,  CONVERT(varchar(10), sr.SalesReturnID)  as BillNumber, 'Return Discount' as Description, (sr.Discount ) as Debit ,'0' as Credit,sr.BranchID,sr.CustomerID ,'3' SequenceNumber    from SalesReturn sr
                                                    union
                                                    Select cr.ReceiveDate , CONVERT(varchar(10), cr.CashReceiveID)  as BillNumber, 'Received Discount' as Description  ,'0' as Debit, (cr.Discount) as Credit ,cr.BranchID ,cr.CustomerID ,'2' SequenceNumber   from dbo.CashReceive cr Where cr.Discount>0
                                                    union
                                                    Select cr.ReceiveDate , CONVERT(varchar(10), cr.CashReceiveID)  as BillNumber, 'Received'as Description ,'0' as Debit, (cr.Amount ) as Credit ,cr.BranchID ,cr.CustomerID ,'2' SequenceNumber    from dbo.CashReceive cr
                                                    union all
                                                    Select jv.Date, jv.VoucherNumber  BillNumber, jv.Description ,  0 Debit ,jv.Amount Credit, jv.BranchID ,ca.ReferenceID CustomerID, '2' SequenceNumber from JournalVoucher jv
                                                    left outer join ChildAccount ca
                                                    on jv.DebitChildAccountID=ca.ChildAccountID or jv.CreditChildAccountID=ca.ChildAccountID
                                                    where ca.ReferenceType=2 and ca.ReferenceID=1 and CreditAccountID>0

                                                    union all
                                                    Select jv.Date, jv.VoucherNumber  BillNumber, jv.Description ,  jv.Amount Debit ,0 Credit, jv.BranchID ,ca.ReferenceID CustomerID, '3' SequenceNumber from JournalVoucher jv
                                                    left outer join ChildAccount ca
                                                    on jv.DebitChildAccountID=ca.ChildAccountID or jv.CreditChildAccountID=ca.ChildAccountID
                                                    where ca.ReferenceType=2 and ca.ReferenceID={0} and DebitAccountID>0

                                                    union

                                                    Select cd.DueDate, CONVERT(varchar(10),  cd.PreviousDueID)as BillNumber, 'Previous Due' as Description ,  cd.Amount as Debit ,'0' as Credit, cd.BranchID ,cd.CustomerID, '0' SequenceNumber   from CustomerPreviousDue cd )tbl
                                                    where tbl.CustomerId='{0}' Order by  SequenceNumber";


            public const string CUSTOMER_TRANSACTION_REPORT_BY_CUSTOEMRID_AND_Date = @"Select * from (
                                                        Select cs.SalesDate , cs.BillNumber, 'Goods Sales' as Description,  (cs.SalesAmount + cs.CarryingLoading) as Debit,'0' as Credit ,cs.BranchID,cs.CustomerID ,'1' SequenceNumber   from CashSales cs 
                                                         where cs.SalesDate between '{1}' and '{2}' 
                                                        union
                                                        Select cs.SalesDate , cs.BillNumber , 'Cash Receive' as Description,  '0' as Debit, (cs.ReceiveAmount ) as Credit ,cs.BranchID,cs.CustomerID ,'2' SequenceNumber   from CashSales cs where cs.ReceiveAmount>0
                                                        and cs.SalesDate between '{1}' and '{2}' 
                                                        union
                                                        Select cs.SalesDate , cs.BillNumber, 'Sales Discount' as Description,  '0' as Debit, (cs.Discount ) as Credit ,cs.BranchID,cs.CustomerID ,'3' SequenceNumber   from CashSales cs where cs.Discount>0
                                                        and cs.SalesDate between '{1}' and '{2}' 
                                                        union
                                                        Select sr.ReturnDate,  CONVERT(varchar(10), sr.SalesReturnID)  as BillNumber, 'Paid for Return' as Description,  sr.PaidAmount as Debit ,'0' as Credit,sr.BranchID,sr.CustomerID ,'3' SequenceNumber   from SalesReturn sr where sr.PaidAmount>0
                                                        and sr.ReturnDate between '{1}' and '{2}' 
                                                        union
                                                        Select sr.ReturnDate,  CONVERT(varchar(10), sr.SalesReturnID)  as BillNumber, 'Return' as Description, '0' as Debit ,(sr.Total ) as Credit,sr.BranchID,sr.CustomerID ,'2' SequenceNumber    from SalesReturn sr 
                                                        where sr.ReturnDate between '{1}' and '{2}' 
                                                        union
                                                        Select sr.ReturnDate,  CONVERT(varchar(10), sr.SalesReturnID)  as BillNumber, 'Return Discount' as Description, (sr.Discount ) as Debit ,'0' as Credit,sr.BranchID,sr.CustomerID ,'4' SequenceNumber    from SalesReturn sr
                                                        where sr.ReturnDate between '{1}' and '{2}' 
                                                        union
                                                        Select cr.ReceiveDate , CONVERT(varchar(10), cr.CashReceiveID)  as BillNumber, 'Received Discount' as Description  ,'0' as Debit, (cr.Discount) as Credit ,cr.BranchID ,cr.CustomerID ,'2' SequenceNumber   from dbo.CashReceive cr Where cr.Discount>0
                                                        and cr.ReceiveDate between '{1}' and '{2}' 
                                                        union
                                                        Select cr.ReceiveDate , CONVERT(varchar(10), cr.CashReceiveID)  as BillNumber, 'Received'as Description ,'0' as Debit, (cr.Amount ) as Credit ,cr.BranchID ,cr.CustomerID ,'3' SequenceNumber    from dbo.CashReceive cr
                                                        where cr.ReceiveDate between '{1}' and '{2}' 
                                                        union all
                                                        Select jv.Date, jv.VoucherNumber  BillNumber, jv.Description ,  0 Debit ,jv.Amount Credit, jv.BranchID ,ca.ReferenceID CustomerID, '2' SequenceNumber from JournalVoucher jv
                                                        left outer join ChildAccount ca
                                                        on jv.DebitChildAccountID=ca.ChildAccountID or jv.CreditChildAccountID=ca.ChildAccountID
                                                        where ca.ReferenceType=2 and ca.ReferenceID={0} and CreditAccountID>0
                                                        and jv.Date between '{1}' and '{2}' 
                                                        union all
                                                        Select jv.Date, jv.VoucherNumber  BillNumber, jv.Description ,  jv.Amount Debit ,0 Credit, jv.BranchID ,ca.ReferenceID CustomerID, '4' SequenceNumber from JournalVoucher jv
                                                        left outer join ChildAccount ca
                                                        on jv.DebitChildAccountID=ca.ChildAccountID or jv.CreditChildAccountID=ca.ChildAccountID
                                                        where ca.ReferenceType=2 and ca.ReferenceID={0} and DebitAccountID>0
                                                        and jv.Date between '{1}' and '{2}' 
                                                        and jv.ReferenceNo in(
                                                        Select ReferenceNo from JournalVoucher jv
                                                        left outer join ParentAccount pa
                                                        on jv.DebitAccountID =pa.AccountID or jv.CreditAccountID=pa.AccountID
                                                        where pa.AccountHeadID=2)
                                                        union

                                                        Select cd.DueDate, CONVERT(varchar(10),  cd.PreviousDueID)as BillNumber, 'Previous Due' as Description ,  cd.Amount as Debit ,'0' as Credit, cd.BranchID ,cd.CustomerID, '0' SequenceNumber   from CustomerPreviousDue cd 
                                                        where cd.DueDate between '{1}' and '{2}' 
                                                        )tbl
                                                        where tbl.CustomerId='{0}' Order by  SequenceNumber";


        }



    }
}
