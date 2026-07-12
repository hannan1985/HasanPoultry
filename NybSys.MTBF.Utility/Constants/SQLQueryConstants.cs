using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NybSys.MTBF.Utility.Constants
{
    public static partial class MTBFConstants
    {
        public static class QueryConstants
        {

            public const string EmailValidation = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

            public const string MAX_ACTIVITY_CENTER_CODE = "SELECT isnull(MAX(CAST(SUBSTRING(ActivityCenterCode,3,2)AS INT)),0) FROM ActivityCenter WHERE CompanyCode = '{0}'";
            public const string MAX_DETAIL_ITEM_CODE = "SELECT isnull(MAX(CAST(SUBSTRING(DetailItemCode,4,2)AS INT)),0) FROM DetailItem WHERE ReportingItemCode = '{0}'";
            public const string MAX_RESPONSIBILITY_CENTER_CODE = "SELECT isnull(MAX(CAST(SUBSTRING(RespCenterCode ,5,2)AS INT)),0) FROM ResponsibilityCenter  WHERE ActivityCenterCode  = '{0}'";
            public const string MAX_CONTROL_ITEM_CODE = "Select isnull(MAX(CAST(SUBSTRING(ControlItemCode  ,1,2)AS INT)),0)  from ControlItem";
            public const string MAX_REPORTING_ITEM_CODE = "SELECT isnull(MAX(CAST(SUBSTRING(ReportingItemCode  ,3,1)AS INT)),0) FROM ReportingItem  WHERE ControlItemCode   = '{0}'";
            public const string GET_COMPANY_CELLING = "SELECT SUM({0}) AS Celling FROM Celling WHERE CompanyCode = '{1}' AND CellingLevel = 1 AND FiscalYear = '{2}' ";
            public const string GET_TOTAL_CELLING_AMOUNT_BY_CONTROLITEMCODE = "Select SUM( BaseAmount) as BaseAmount ,SUM(OnceOffAmount) as OnceOffAmount,SUM(RevisedAmount) as RevisedAmount from Celling where controlItemCode='{0}' and FiscalYear ='{1}' group by ControlItemCode";
            public const string GET_TOTAL_CELLING_AMOUNT_BY_REPORTINGITEMCODE = "Select SUM( BaseAmount) as BaseAmount ,SUM(OnceOffAmount) as OnceOffAmount,SUM(RevisedAmount) as RevisedAmount from Celling where ReportingItemCode ='{0}' and FiscalYear ='{1}' group by ReportingItemCode";
            public const string GET_TOTAL_CELLING_AMOUNT_BY_DETAILITEMCODE = "Select SUM( BaseAmount) as BaseAmount ,SUM(OnceOffAmount) as OnceOffAmount,SUM(RevisedAmount) as RevisedAmount from Celling where DetailItemCode ='{0}' and FiscalYear ='{1}' group by DetailItemCode";
            public const string GET_TOTAL_BUDGET_AMOUNT_BY_CONTROLITEMCODE = "Select SUM (Total )as Total from Budget where ControlItemCode ='{0}' and FiscalYear ='{1}' group by ControlItemCode ";
            public const string GET_TOTAL_BUDGET_AMOUNT_BY_REPORTINGITEMCODE = "Select SUM (Total )as Total from Budget where ReportingItemCode  ='{0}' and FiscalYear ='{1}' group by ReportingItemCode  ";
            public const string GET_TOTAL_BUDGET_AMOUNT_BY_DETAILITEMCODE = "Select SUM (Total )as Total from Budget where DetailItemCode   ='{0}' and FiscalYear ='{1}' group by DetailItemCode  ";
            public const string GET_ACTIVITY_CELLING = "SELECT SUM({0}) AS Celling FROM Celling WHERE CompanyCode = '{1}' AND ActivityCenterCode = '{2}' AND CellingLevel = 2 AND FiscalYear = '{3}' ";
            public const string GET_RESPONSIBILITY_CELLING = "SELECT SUM({0}) AS Celling FROM Celling WHERE CompanyCode = '{1}' AND ActivityCenterCode = '{2}' AND RespCenterCode = '{3}' AND CellingLevel = 3 AND FiscalYear = '{4}' ";

            public const string GET_CONTROLITEM_BY_COMPANY_CODE = "Select * from ControlItem where ControlItemCode  in(Select distinct ControlItemCode  from AccountMaster am  where am. CompanyCode='{0}' )";

            public const string GET_CONTROLITEM_BY_COMPANY_AND_ACTIVITY_CODE = @"Select * from ControlItem where ControlItemCode 
                                                                         in(Select distinct ControlItemCode  from AccountMaster am  where am. CompanyCode='{0}' and am.ActivityCenterCode ='{1}' )";

            public const string GET_CONTROLITEM_BY_COMPANY_ACTIVITY_AND_RESP_CODE = @"Select * from ControlItem where ControlItemCode 
                                                                         in(Select distinct ControlItemCode  from AccountMaster am  where am. CompanyCode='{0}' AND am.ActivityCenterCode ='{1}' AND am.RespCenterCode ='{2}')";



            public const string GET_REPORTINGITEM_CHECK_COMBINATION = @"Select * from ReportingItem where ReportingItemCode in(
                                                                        Select distinct am.ReportingItemCode from AccountMaster am
                                                                        left join ControlItem  ci on am.ControlItemCode  = ci.ControlItemCode  
                                                                         where am. CompanyCode='{0}' and am.ActivityCenterCode ='{1}')";

            public const string GET_REPORTINGITEM_BY_COMPANY_ACTIVITY_AND_RESPCENTER = @"Select * from ReportingItem where ReportingItemCode in(
                                                                        Select distinct am.ReportingItemCode from AccountMaster am
                                                                        left join ControlItem  ci on am.ControlItemCode  = ci.ControlItemCode  
                                                                         where am. CompanyCode='{0}' and am.ActivityCenterCode ='{1}' and am.RespCenterCode ='{2}')";

            public const string GET_DETAILITEM_CHECK_COMBINATION = @"Select * from DetailItem where DetailItemCode in( 
                                                                    Select distinct am.DetailItemCode  from AccountMaster am
                                                                    left join DetailItem di on am.DetailItemCode =di.DetailItemCode 
                                                                    Where am.CompanyCode ='{0}' and am.ActivityCenterCode ='{1}' and am.RespCenterCode ='{2}')";


            public const string GetLatestInvoice = @" Declare @MaxInvoiceID int 
                                                   begin 
                                                   select @MaxInvoiceID = Max(InvoiceID) from InvoiceRegister 
                      	                           select * from InvoiceRegister where InvoiceID = @MaxInvoiceID 
                                                    End ";
            public const string GetLatestCreditNotes = @"Declare @MaxNoteNumber varchar(10) 
                                                         begin 
                                                         select @MaxNoteNumber = Max(CreditNoteNo) from CreditNotes 
                                                         select * from CreditNotes where CreditNoteNo = @MaxNoteNumber 
                                                         End ";

            public const string GetLatestRequisition = @"Declare @MaxReqID int begin 
                                                     select @MaxReqID = Max(RequisitionID) from Requisition 
                                                    select * from Requisition where RequisitionID = @MaxReqID 
                                                    End ";

            public const string GET_BUDGET_REPORT = @"Select tbl.*,c.ControlItemName  from (Select tbl .ControlItemCode,tbl .Description ,tbl .ActivityCenterCode,tbl.ActivityCenterName, 
                                                    SUM(tbl.ActualAmount) as ActualAmount,SUM(tbl.PreviousYearAmount) as PreviousYearAmount,
                                                    SUM(tbl.CurrentYearAmount  )as CurrentYearAmount,SUM(tbl .EstimatedYear1Amount ) as EstimatedYear1Amount, 
                                                    SUM(tbl.EstimatedYear2Amount)as EstimatedYear2Amount     from(

                                                    Select b.ControlItemCode,b.Description ,b.ActivityCenterCode ,ac.ActivityCenterName,b.Total  as ActualAmount ,'00'  as PreviousYearAmount, '00' as CurrentYearAmount, '00' as EstimatedYear1Amount, '00' as EstimatedYear2Amount,b.FiscalYear    from Budget b
                                                    join ActivityCenter ac 
                                                    on ac.ActivityCenterCode =b.ActivityCenterCode  
                                                    where FiscalYear =YEAR (GetDate())-2
                                                    union 
                                                    Select b.ControlItemCode,b.Description ,b.ActivityCenterCode ,ac.ActivityCenterName,'00' as ActualAmount ,b.Total  as PreviousYearAmount, '00' as CurrentYearAmount, '00' as EstimatedYear1Amount, '00' as EstimatedYear2Amount,b.FiscalYear    from Budget b
                                                    join ActivityCenter ac 
                                                    on ac.ActivityCenterCode =b.ActivityCenterCode  
                                                    where FiscalYear =YEAR (GetDate())-1
                                                    Union
                                                    Select b.ControlItemCode,b.Description ,b.ActivityCenterCode ,ac.ActivityCenterName,'00' as ActualAmount ,'00'  as PreviousYearAmount, b.Total as CurrentYearAmount, '00' as EstimatedYear1Amount, '00' as EstimatedYear2Amount,b.FiscalYear    from Budget b
                                                    join ActivityCenter ac 
                                                    on ac.ActivityCenterCode =b.ActivityCenterCode  
                                                    where  FiscalYear =YEAR (GetDate())
                                                    union

                                                    Select b.ControlItemCode,b.Description ,b.ActivityCenterCode ,ac.ActivityCenterName,'00' as ActualAmount ,'00'  as PreviousYearAmount,  '00' as CurrentYearAmount, b.Total as EstimatedYear1Amount, '00' as EstimatedYear2Amount,b.FiscalYear    from Budget b
                                                    join ActivityCenter ac 
                                                    on ac.ActivityCenterCode =b.ActivityCenterCode  
                                                    where FiscalYear =YEAR (GetDate())+1
                                                    union

                                                    Select b.ControlItemCode,b.Description ,b.ActivityCenterCode ,ac.ActivityCenterName,'00' as ActualAmount ,'00'  as PreviousYearAmount,'00' as CurrentYearAmount,'00' as EstimatedYear1Amount, b.Total as EstimatedYear2Amount,b.FiscalYear    from Budget b
                                                    join ActivityCenter ac 
                                                    on ac.ActivityCenterCode =b.ActivityCenterCode  
                                                    where FiscalYear =YEAR (GetDate())+2)tbl
                                                    group by ControlItemCode ,tbl .ControlItemCode,tbl .Description ,tbl .ActivityCenterCode,tbl.ActivityCenterName)tbl
                                                    
                                                    join ControlItem c
                                                    on tbl .ControlItemCode =c.ControlItemCode ";
            public const string GETLOGRECORD = "Select * from LogRecord Where userID='{0}' and logTime=( select MAX(LogTime)logTime from LogRecord) ";
            public const string GET_REPORTING_ITEM_BY_ACCOUNT_TYPE = @"Select * from ReportingItem where   ReportingItemCode  in(Select am.ReportingItemCode  from AccountMaster am
                                                                       left join ControlItem ci
                                                                       on am.ControlItemCode =ci.ControlItemCode  Where  ci.AccountTypeID ={0} and RespCenterCode ='{1}')";
            public const string MAX_CONTRACT_ID = @"Select isnull(MAX(ContractID),0)+1 as ContractID  from Contract";

            public const string GetSupplierInfoByInvoiceTypeAndPaymentMethod = @"Select distinct s.SupplierID,s.SupplierName   from Invoice i 
                                                                                  join Supplier s on i.SupplierID =s.SupplierID 
                                                                                where i.PaymentMethodID ={0} and i.InvoiceType ={1} and i.Status ={2}";


            public const string GetAllDetailItemByAccountTypeAndInstitutionalSetup = @"Select am.* from AccountMaster am
                                                                                    left join 
                                                                                    ControlItem ci on am.ControlItemCode =ci.ControlItemCode 
                                                                                    where ci.AccountTypeID ={0} and am.CompanyCode ='{1}' and am.ActivityCenterCode ='{2}' and am.RespCenterCode ='{3}'";

            public const string GetTotalPurchaseOrderAmountByRespCenterAndDetailItemCode = @"Select (pod.Quantity*pod.Price) -isnull(pod.Discount,0) as OrderAmount    from dbo.PurchaseOrder po
                                                                                            join PurchaseOrderItem pod
                                                                                            on po.OrderID =pod.OrderID 
                                                                                            where po.RespCenterCode='{0}' and pod.DetailItemCode ='{1}' and po.FiscalYear ='2013'";


            public const string GetFilterdVoucherInformationForThisContextOnly = @"Select distinct v.* from Voucher v
                                                                                    join VoucherDetail vd
                                                                                    on v.VoucherID =vd.VoucherID 
                                                                                    where vd.InvoiceRegisterID in(
                                                                                    Select distinct inv.InvoiceRegisterID  from Invoice inv
                                                                                    join InvoiceDetail invd
                                                                                    on inv .InvoiceRegisterID =invd.InvoiceRegisterID 
                                                                                    left join DetailItem di on 
                                                                                    invd.DetailItemCode =di.DetailItemCode 
                                                                                    where inv.CompanyCode ='{0}'  and inv.ActivityCenterCode ='{1}' 
                                                                                    and inv .RespCenterCode ='{2}' and di.ReportingItemCode ='{3}')
                                                                                    and v.Status ={4}";

            public const string GetFilterdVoucherInformationForReleatedContextOnly = @"Select distinct v.* from Voucher v
                                                                join VoucherDetail vd
                                                                on v.VoucherID =vd.VoucherID 
                                                                where vd.InvoiceRegisterID in(
                                                                Select distinct inv.InvoiceRegisterID  from Invoice inv
                                                                join InvoiceDetail invd
                                                                on inv .InvoiceRegisterID =invd.InvoiceRegisterID 
                                                                left join DetailItem di on 
                                                                invd.DetailItemCode =di.DetailItemCode 
                                                                where inv.CompanyCode ='{0}'  or inv.ActivityCenterCode ='{1}' 
                                                                or inv .RespCenterCode ='{2}' or di.ReportingItemCode ='{3}')
                                                                and v.Status ={4}";


            public const string GetVoucherInformationByInvoiceNumber = @"Select distinct v.* from Voucher v
                                                                        join VoucherDetail vd
                                                                        on v.VoucherID =vd.VoucherID 
                                                                        where vd.InvoiceRegisterID in(
                                                                        Select distinct inv.InvoiceRegisterID  from Invoice inv
                                                                        join InvoiceDetail invd
                                                                        on inv .InvoiceRegisterID =invd.InvoiceRegisterID                                                                       
                                                                        where inv.InvoiceNumber  ='{0}')";

            public const string GetVoucherInformationByPoNumber = @"Select distinct v.* from Voucher v
                                                                    join VoucherDetail vd
                                                                    on v.VoucherID =vd.VoucherID 
                                                                    where vd.InvoiceRegisterID in(
                                                                    Select distinct inv.InvoiceRegisterID  from Invoice inv
                                                                    join InvoiceDetail invd
                                                                    on inv .InvoiceRegisterID =invd.InvoiceRegisterID 
                                                                    left join PurchaseOrder po
                                                                    on inv.PurchaseOrderID =po.OrderID 
                                                                    where  SUBSTRING(po.OrderNumber,len(po.OrderNumber)-5,6) ='{0}')";

            public const string GetVoucherInformationByContractNumber = @"Select distinct v.* from Voucher v
                                                                            join VoucherDetail vd
                                                                            on v.VoucherID =vd.VoucherID 
                                                                            where vd.InvoiceRegisterID in(
                                                                            Select distinct inv.InvoiceRegisterID  from Invoice inv
                                                                            join InvoiceDetail invd
                                                                            on inv .InvoiceRegisterID =invd.InvoiceRegisterID 
                                                                            left join Contract  c
                                                                            on c.ContractID =inv.ContractID 
                                                                            where  c.ContractNumber   ='{0}')";

            public const string GetFilteredVoucherInformationByInvoiceAndVoucherDate = @"Select distinct * from Voucher v where 
                                                                                            v.VoucherDate between '{0}' and '{1}'";


            public const string GetVoucherInformationForBatch = @"Select * from Voucher where VoucherID in(
                                                                        Select distinct vd.VoucherID  from dbo.VoucherDetail vd
                                                                        left join Invoice i on vd.InvoiceRegisterID =i.InvoiceRegisterID 
                                                                        left join InvoiceDetail invd
                                                                        on i.InvoiceRegisterID =invd.InvoiceRegisterID 
                                                                        left join DetailItem di
                                                                        on invd.DetailItemCode =di.DetailItemCode 
                                                                        left join  ReportingItem ri
                                                                        on di.ReportingItemCode =ri.ReportingItemCode 
                                                                        left join ControlItem ci on ri.ControlItemCode =ci.ControlItemCode 
                                                                        where i.CompanyCode ='{0}' and ActivityCenterCode ='{1}' and ci.AccountTypeID ={2}) and Status ={3}
                                                                        union

                                                                        Select * from Voucher v where v.VoucherID in( 
                                                                        Select distinct cvd .VoucherID  from ClaimVoucherDetail cvd
                                                                        left join Employee e
                                                                        on cvd .ClaimBy =e.EmployeeID
                                                                        left join DetailItem di
                                                                        on cvd.DetailItemCode =di.DetailItemCode 
                                                                        left join ReportingItem ri on 
                                                                        ri.ReportingItemCode =di.ReportingItemCode 
                                                                        left join ControlItem ci
                                                                        on ci.ControlItemCode =ri.ControlItemCode 
                                                                        where e.CompanyCode ='{0}' and e.ActivityCenterCode ='{1}'
                                                                        and ci.AccountTypeID ={2} )
                                                                        and v.Status ={3}";

            public const string GET_BATCH_INFORMATION_BY_CONTRACT_NUMBER = @"Select * from Batch b where b.BatchID in(
                                                                            Select distinct bd.BatchID  from BatchDetail bd where bd.VoucherID in(
                                                                            Select distinct vd.VoucherID from VoucherDetail vd
                                                                            left join Invoice inv
                                                                            on vd.InvoiceRegisterID =inv.InvoiceRegisterID
                                                                            left join Contract con on 
                                                                            inv .ContractID =con.ContractID 
                                                                            Where con.ContractNumber ='{0}'))";

            public const string GET_BATCH_INFORMATION_BY_VOUCHER_NUMBER = @"Select * from Batch b where b.BatchID in(
                                                                            Select distinct bd.BatchID  from BatchDetail bd where bd.VoucherID in(
                                                                            Select distinct v.VoucherID from Voucher v
                                                                            Where v.VoucherNumber  ='{0}'))";

            public const string GET_BATCH_INFORMATION_BY_INVOICE_NUMBER = @"Select * from Batch b where b.BatchID in(
                                                                            Select distinct bd.BatchID  from BatchDetail bd where bd.VoucherID in(
                                                                            Select distinct vd.VoucherID from VoucherDetail vd
                                                                            left join Invoice inv
                                                                            on vd.InvoiceRegisterID =inv.InvoiceRegisterID
                                                                            where inv.InvoiceNumber ='{0}'))";

            public const string GET_BATCH_INFORMATION_BY_PO_NUMBER = @"Select * from Batch b where b.BatchID in(
                                                                        Select distinct bd.BatchID  from BatchDetail bd where bd.VoucherID in(
                                                                        Select distinct vd.VoucherID from VoucherDetail vd
                                                                        left join Invoice inv
                                                                        on vd.InvoiceRegisterID =inv.InvoiceRegisterID
                                                                        left join PurchaseOrder  po on 
                                                                        inv.PurchaseOrderID =po.OrderID 
                                                                        Where po.OrderNumber  ='{0}'))";

            public const string GET_BATCH_INFORMATION_BY_INSTITUTIONAL_SETUP_AND_STATUS = @"Select * from Batch b where b.BatchID in(
                                                                    Select distinct bd.BatchID  from BatchDetail bd where bd.VoucherID in(
                                                                    Select distinct vd.VoucherID from VoucherDetail vd
                                                                    left join Invoice inv
                                                                    on vd.InvoiceRegisterID =inv.InvoiceRegisterID
                                                                    Where inv.CompanyCode ='{0}' and inv.ActivityCenterCode ='{1}' and inv.RespCenterCode ='{2}'))
                                                                    and b.BatchTypeID ={3} and b.Status={4}";

            public const string GET_BATCH_INFORMATION_BY_VOUCHER_AND_INVOICE_DATE = @"Select b.*, v.TotalAmount as VoucherAmount ,inv.Total as InvoiceAmount ,inv.SupplierID   from Batch b 
                                                                                    left join  BatchDetail bd
                                                                                    on b.BatchID =bd.BatchID 
                                                                                    left join Voucher v
                                                                                    on bd.VoucherID =v.VoucherID 
                                                                                    join VoucherDetail vd on 
                                                                                    v.VoucherID =vd.VoucherID 
                                                                                    left join Invoice inv
                                                                                    on inv.InvoiceRegisterID =vd.InvoiceRegisterID 
                                                                                    Where v.VoucherDate  between '{2}' and '{3}'
                                                                                    and b.BatchDate between '{0}' and '{1}'";
            public const string GET_ACCOUNT_MASTER_BY_INSTITUTIONALSETUP_AND_ACCOUNTYPE = @"Select * from dbo.AccountMaster am
                                                                                            left join ControlItem ci
                                                                                            on am.ControlItemCode=ci.ControlItemCode 
                                                                                            Where am.CompanyCode ='{0}'
                                                                                            AND am .ActivityCenterCode='{1}' AND am.RespCenterCode ='{2}' AND ci.AccountTypeID ={3} ";


            //For Bank Deposit
            public const string GET_DEPOSIT_REF_NO_BY_BANKID_AND_BRANCHID = @"SELECT BankDeposit.DepositRefNo
                                                                            FROM dbo.Bank INNER JOIN
                                                                            Branch ON Bank.BankID = Branch.BankID INNER JOIN
                                                                            BankAccount ON Branch.BranchID = BankAccount.BranchID INNER JOIN
                                                                            BankDeposit ON BankAccount.BankAccountID = BankDeposit.BankAccountID
                                                                            WHERE (Branch.BranchID = '{0}') AND (Bank.BankID = '{1}') AND (BankDeposit.DepositRefNo = '{2}')";

            //For Bank Withdraw
            public const string GET_WITHDRAW_REF_NO_BY_BANKID_AND_BRANCHID = @"SELECT BankWithdraw.WithdrawRefNo
                                                                            FROM dbo.Bank INNER JOIN
                                                                            Branch ON Bank.BankID = Branch.BankID INNER JOIN
                                                                            BankAccount ON Branch.BranchID = BankAccount.BranchID INNER JOIN
                                                                            BankWithdraw ON BankAccount.BankAccountID = BankWithdraw.BankAccountID
                                                                            WHERE (Branch.BranchID = '{0}') AND (Bank.BankID = '{1}') AND (BankWithdraw.WithdrawRefNo = '{2}')";
            public const string GET_CONTROLITEM_BY_INSTITUTIONALSETUP = @"Select distinct * from ControlItem ci
                                                                            left join AccountMaster am
                                                                            on ci.ControlItemCode =am.ControlItemCode 
                                                                            Where am.CompanyCode ='{0}' and am.ActivityCenterCode ='{1}' and am.RespCenterCode ='{2}'";

            public const string GET_CONTROLITEM_FROM_CHART_OF_ACCOUNT = @"Select * from ControlItem where ControlItemCode in(
                                                                            Select distinct ControlItemCode  from AccountMaster )";
            public const string GET_REPORTING_ITEM_FROM_CHART_OF_ACCOUNT = @"Select * from ReportingItem  where ReportingItemCode  in(
                                                                             Select distinct ReportingItemCode   from AccountMaster )";
            public const string GET_DETAIL_ITEM_FROM_CHART_OF_ACCOUNT = @"Select * from DetailItem   where DetailItemCode   in(
                                                                            Select distinct DetailItemCode   from AccountMaster )";
            public const string GET_JOURNAL_REFERENCE_NO = "Select ISNULL( MAX(ReferenceNo),0)+1 as RefNo  from Journal";


            public const string IncomeStatement = @" Select incomeStatement.AccountType , di.DetailItemName,incomeStatement.DetailItemCode, SUM( incomeStatement .Balance) as Balance    from (
                                                    Select AccountType ,JournalID ,ControlItemCode ,ReportingItemCode ,DetailItemCode ,(CreditAmount-DebitAmount ) As Balance from (
                                                    Select 'Income' as AccountType, j.JournalID ,j.ControlItemCode , j.ReportingItemCode , j.DetailItemCode, isnull(j.DebitAmount,0) as DebitAmount ,'0' as CreditAmount   from Journal j where j.ControlItemCode in(
                                                    Select ci.ControlItemCode  from ControlItem ci
                                                    left join FAType fat
                                                    on ci.FATypeID =fat.FATypeID
                                                    where fat.FAHeadID =4) and DrCrIndicator =1 and FiscalYear ='{0}'  and j.CompanyCode ='{1}'  
                                                     union
                                                    Select 'Income' as AccountType, j.JournalID ,j.ControlItemCode , j.ReportingItemCode , j.DetailItemCode, '0' as DebitAmount ,isnull(j.CreditAmount ,0) as CreditAmount   from Journal j where j.ControlItemCode in(
                                                    Select ci.ControlItemCode  from ControlItem ci
                                                    left join FAType fat
                                                    on ci.FATypeID =fat.FATypeID
                                                    where fat.FAHeadID =4) and DrCrIndicator =2 and FiscalYear ='{0}'  and j.CompanyCode ='{1}'  )IncomeTable

                                                    union

                                                    --Expense portion
                                                    Select AccountType ,JournalID, ControlItemCode  ,ReportingItemCode ,DetailItemCode ,(DebitAmount -CreditAmount) As Balance from (
                                                    Select 'Expense' as AccountType, j.JournalID ,j.ControlItemCode , j.ReportingItemCode , j.DetailItemCode, isnull(j.DebitAmount,0) as DebitAmount ,'0' as CreditAmount   from Journal j where j.ControlItemCode in(
                                                    Select ci.ControlItemCode  from ControlItem ci
                                                    left join FAType fat
                                                    on ci.FATypeID =fat.FATypeID
                                                    where fat.FAHeadID =5) and DrCrIndicator =1 and FiscalYear ='{0}'  and j.CompanyCode ='{1}' 
                                                     union
                                                    Select 'Expense' as AccountType, j.JournalID ,j.ControlItemCode , j.ReportingItemCode , j.DetailItemCode, '0' as DebitAmount ,isnull(j.CreditAmount ,0) as CreditAmount   from Journal j where j.ControlItemCode in(
                                                    Select ci.ControlItemCode  from ControlItem ci
                                                    left join FAType fat
                                                    on ci.FATypeID =fat.FATypeID
                                                    where fat.FAHeadID =5) and DrCrIndicator =2 and FiscalYear ='{0}'  and j.CompanyCode ='{1}'  )ExpenseTable ) incomeStatement 
                                                    left join ControlItem ci
                                                    on incomeStatement.ControlItemCode=ci.ControlItemCode 
                                                    left join FAType fat on 
                                                    ci.FATypeID =fat.FATypeID 
                                                    left join DetailItem di 
                                                    on di.DetailItemCode =incomeStatement .DetailItemCode 
                                                    group by incomeStatement.AccountType , di.DetailItemName,incomeStatement.DetailItemCode order by incomeStatement.AccountType desc";

            public const string BalanceSheet = @" --Asset
                                                            Select AccountType  ,DetailItemCode ,Sum((CreditAmount-DebitAmount )) As Balance from (
                                                            Select 'Asset' as AccountType, j.JournalID ,j.ControlItemCode , j.ReportingItemCode , j.DetailItemCode, isnull(j.DebitAmount,0) as DebitAmount ,'0' as CreditAmount   from Journal j where j.ControlItemCode in(
                                                            Select ci.ControlItemCode  from ControlItem ci
                                                            left join FAType fat
                                                            on ci.FATypeID =fat.FATypeID
                                                            where fat.FAHeadID =1) and DrCrIndicator =1 and FiscalYear ='{0}'  and j.CompanyCode ='{1}'
                                                             union
                                                            Select 'Asset' as AccountType, j.JournalID ,j.ControlItemCode , j.ReportingItemCode , j.DetailItemCode, '0' as DebitAmount ,isnull(j.CreditAmount ,0) as CreditAmount   from Journal j where j.ControlItemCode in(
                                                            Select ci.ControlItemCode  from ControlItem ci
                                                            left join FAType fat
                                                            on ci.FATypeID =fat.FATypeID
                                                            where fat.FAHeadID =1) and DrCrIndicator =2 and FiscalYear ='{0}'  and j.CompanyCode ='{1}'  )IncomeTable
                                                            Where (CreditAmount-DebitAmount)>0
                                                            group by DetailItemCode,AccountType 

                                                            union
                                                            -- Liabilities - balance goes asset
                                                            Select AccountType  ,DetailItemCode ,Sum((CreditAmount-DebitAmount )*-1) As Balance from (
                                                            Select 'Asset' as AccountType, j.JournalID ,j.ControlItemCode , j.ReportingItemCode , j.DetailItemCode, isnull(j.DebitAmount,0) as DebitAmount ,'0' as CreditAmount   from Journal j where j.ControlItemCode in(
                                                            Select ci.ControlItemCode  from ControlItem ci
                                                            left join FAType fat
                                                            on ci.FATypeID =fat.FATypeID
                                                            where fat.FAHeadID =2) and DrCrIndicator =1 and FiscalYear ='{0}'  and j.CompanyCode ='{1}' 
                                                             union
                                                            Select 'Asset' as AccountType, j.JournalID ,j.ControlItemCode , j.ReportingItemCode , j.DetailItemCode, '0' as DebitAmount ,isnull(j.CreditAmount ,0) as CreditAmount   from Journal j where j.ControlItemCode in(
                                                            Select ci.ControlItemCode  from ControlItem ci
                                                            left join FAType fat
                                                            on ci.FATypeID =fat.FATypeID
                                                            where fat.FAHeadID =2) and DrCrIndicator =2 and FiscalYear ='{0}'  and j.CompanyCode ='{1}'  )IncomeTable
                                                            Where (CreditAmount-DebitAmount )<0
                                                            group by DetailItemCode,AccountType 

                                                            union

                                                            -- Asset - balance goes Liabilities

                                                            Select AccountType  ,DetailItemCode ,Sum((CreditAmount-DebitAmount )*-1) As Balance from (
                                                            Select 'Liabilities' as AccountType, j.JournalID ,j.ControlItemCode , j.ReportingItemCode , j.DetailItemCode, isnull(j.DebitAmount,0) as DebitAmount ,'0' as CreditAmount   from Journal j where j.ControlItemCode in(
                                                            Select ci.ControlItemCode  from ControlItem ci
                                                            left join FAType fat
                                                            on ci.FATypeID =fat.FATypeID
                                                            where fat.FAHeadID =1) and DrCrIndicator =1 and FiscalYear ='{0}'  and j.CompanyCode ='{1}' 
                                                             union
                                                            Select 'Liabilities' as AccountType, j.JournalID ,j.ControlItemCode , j.ReportingItemCode , j.DetailItemCode, '0' as DebitAmount ,isnull(j.CreditAmount ,0) as CreditAmount   from Journal j where j.ControlItemCode in(
                                                            Select ci.ControlItemCode  from ControlItem ci
                                                            left join FAType fat
                                                            on ci.FATypeID =fat.FATypeID
                                                            where fat.FAHeadID =1) and DrCrIndicator =2 and FiscalYear ='{0}'  and j.CompanyCode ='{1}'  )IncomeTable
                                                            Where (CreditAmount-DebitAmount)<0
                                                            group by DetailItemCode,AccountType 

                                                            union

                                                            Select AccountType  ,DetailItemCode ,Sum((CreditAmount-DebitAmount )) As Balance from (
                                                            Select 'Liabilities' as AccountType, j.JournalID ,j.ControlItemCode , j.ReportingItemCode , j.DetailItemCode, isnull(j.DebitAmount,0) as DebitAmount ,'0' as CreditAmount   from Journal j where j.ControlItemCode in(
                                                            Select ci.ControlItemCode  from ControlItem ci
                                                            left join FAType fat
                                                            on ci.FATypeID =fat.FATypeID
                                                            where fat.FAHeadID =2) and DrCrIndicator =1 and FiscalYear ='{0}'  and j.CompanyCode ='{1}'  
                                                             union
                                                            Select 'Liabilities' as AccountType, j.JournalID ,j.ControlItemCode , j.ReportingItemCode , j.DetailItemCode, '0' as DebitAmount ,isnull(j.CreditAmount ,0) as CreditAmount   from Journal j where j.ControlItemCode in(
                                                            Select ci.ControlItemCode  from ControlItem ci
                                                            left join FAType fat
                                                            on ci.FATypeID =fat.FATypeID
                                                            where fat.FAHeadID =2) and DrCrIndicator =2 and FiscalYear ='{0}'  and j.CompanyCode ='{1}'  )IncomeTable
                                                            Where (CreditAmount-DebitAmount )>0
                                                            group by DetailItemCode,AccountType 

                                                            union

                                                            --Capital
                                                            Select AccountType  ,DetailItemCode ,Sum((CreditAmount-DebitAmount )) As Balance from (
                                                            Select 'Capital' as AccountType, j.JournalID ,j.ControlItemCode , j.ReportingItemCode , j.DetailItemCode, isnull(j.DebitAmount,0) as DebitAmount ,'0' as CreditAmount   from Journal j where j.ControlItemCode in(
                                                            Select ci.ControlItemCode  from ControlItem ci
                                                            left join FAType fat
                                                            on ci.FATypeID =fat.FATypeID
                                                            where fat.FAHeadID =3) and DrCrIndicator =1 and FiscalYear ='{0}'  and j.CompanyCode ='{1}'  
                                                             union
                                                            Select 'Capital' as AccountType, j.JournalID ,j.ControlItemCode , j.ReportingItemCode , j.DetailItemCode, '0' as DebitAmount ,isnull(j.CreditAmount ,0) as CreditAmount   from Journal j where j.ControlItemCode in(
                                                            Select ci.ControlItemCode  from ControlItem ci
                                                            left join FAType fat
                                                            on ci.FATypeID =fat.FATypeID
                                                            where fat.FAHeadID =3) and DrCrIndicator =2 and FiscalYear ='{0}'  and j.CompanyCode ='{1}'  )IncomeTable
                                                            group by DetailItemCode,AccountType ";
            public const string GET_CONTROL_ITEM_BY_COMPANY_DEPARTMENT_AND_SECTION = @"Select * from ControlItem where ControlItemCode in (
                                                                                    Select distinct am.ControlItemCode  from AccountMaster am
                                                                                    Where am.CompanyCode ='{0}' and am.ActivityCenterCode ='{1}' and am.RespCenterCode ='{2}')";

            public const string GET_PRODUCT_BY_NAME = "Ltrim( UPPER(ProductName))= LTRIM ( UPPER ('{0}'))";


            public const string GetSupplierStatement = @"Select tbl.*, c.CompanyName, s.SupplierName from (
                                                        Select SupplierID, BranchID, PurchaseDate Date, 'Purchase' Description, PurchaseAmount DrAmount ,'0' CrAmount, '1' SequesceNo from PurchaseOrder
                                                        union
                                                        Select SupplierID, BranchID, PurchaseDate Date, 'Payments' Description, '0' DrAmount, PaidAmount CrAmount, '2' SequesceNo from PurchaseOrder where PaidAmount>0
                                                        union
                                                        Select SupplierID, BranchID, PaymentDate Date, 'Payments' Description, '0' DrAmount, Amount + isnull(Discount,0) CrAmount , '3' SequesceNo from Payment

                                                        union
                                                        Select SupplierID, BranchID, ReturnDate Date ,'Purchase Return' Description , '0' DrAmount, Total CrAmount , '4' SequesceNo from PurchaseReturn
                                                        union
                                                        Select SupplierID, BranchID, ReturnDate Date ,'Payments for Purchase Return ' Description , ReceiveAmount DrAmount, '0' CrAmount , '4' SequesceNo from PurchaseReturn
                                                        union
                                                        Select SupplierID, BranchID, DueDate Date, 'Previous Due' Description, Amount DrAmount,'0' CrAmount, '0' SequesceNo  from CustomerPreviousDue
                                                        union all
                                                        Select ca.ReferenceID SupplierID, jv.BranchID, jv.Date, jv.Description ,  0 Debit ,jv.Amount Credit,'3' SequesceNo   from JournalVoucher jv
                                                        left outer join ChildAccount ca
                                                        on jv.DebitChildAccountID=ca.ChildAccountID or jv.CreditChildAccountID=ca.ChildAccountID
                                                        where ca.ReferenceType={0} and ca.ReferenceID=1 and DebitAccountID>0
                                                        ) as tbl
                                                        left outer join Suppliers s on s.SupplierID=tbl.supplierID
                                                        left outer join CompanyInformation c on c.CompanyID=s.CompanyID Where tbl.SupplierID={0}  and Date between '{1}' and '{2}' order by Date, tbl.SequesceNo";


        }
    }
}
