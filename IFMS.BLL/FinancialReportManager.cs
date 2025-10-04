using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;
using System.Collections;
using IMFS.Common.Utility;
using IMFS.Common.View;
using NybSys.MTBF.Utility.Constants;

namespace IFMS.BLL
{
    public class FinancialReportManager : BllBase
    {
        public List<BalanceSheet> GetAllBalanceSheet(string _fromDate, string _toDate)
        {
            string filter = string.Format(IFMSConstant.QueryConstants.BalanceSheet, _fromDate, _toDate);
            return base.DataAccessManager.ExecuteSQL<BalanceSheet>(filter).Cast<BalanceSheet>().ToList();
        }

        public List<BalanceSheet> GetAllBalanceSheetDetail(string _fromDate, string _toDate)
        {
            string filter = string.Format(IFMSConstant.QueryConstants.BalanceSheetDetail, MTBFConstants.AppConstants.BranchID, _fromDate, _toDate);
            return base.DataAccessManager.ExecuteSQL<BalanceSheet>(filter).Cast<BalanceSheet>().ToList();
        }


        public decimal GetProfitOrLoss(string fromDate, string toDate, int branchID, int organizationID)
        {
            string filter = string.Format(IFMSConstant.QueryConstants.GetProfitOrLoss, fromDate, toDate, branchID, organizationID);
            return Convert.ToDecimal(base.DataAccessManager.ExecuteScalar(filter));
        }

        public IList GetAllIncomeStatement(string _fromDate, string _toDate)
        {
            string filter = string.Format(IFMSConstant.QueryConstants.IncomeStatement, _fromDate, _toDate);
            return base.DataAccessManager.ExecuteSQL<IncomeStatement>(filter).Cast<IncomeStatement>().ToList();
        }

        public int InsertRetainEarning(RetainEarning retainEarning)
        {
            RetainEarning existingRetainEarning = GetRetainEarningByFiscalYear(retainEarning.FiscalYear);
            if (existingRetainEarning != null)
            {
                existingRetainEarning.EarningAmount = retainEarning.EarningAmount;
                return base.DataAccessManager.Update<RetainEarning>(existingRetainEarning);
            }
            else
            {
                return base.DataAccessManager.Insert<RetainEarning>(retainEarning);
            }

        }

        public RetainEarning GetRetainEarningByFiscalYear(int fiscalYear)
        {
            string filter = string.Format("{0}={1}", "FiscalYear", fiscalYear);
            return base.DataAccessManager.GetFilteredData<RetainEarning>(filter).Cast<RetainEarning>().ToList().FirstOrDefault();
        }

        public IList GetAllRetainEarning()
        {
            return base.DataAccessManager.GetAll<RetainEarning>();
        }

        public int InsertBalanceSheetBackup(List<BalanceSheetBackup> lstBalanceSheetBackup)
        {
            int result = 0;
            foreach (BalanceSheetBackup balanceSheetBackup in lstBalanceSheetBackup)
            {
                result = base.DataAccessManager.Insert<BalanceSheetBackup>(balanceSheetBackup);
            }
            return result;
        }

        public IList GetBalanceSheetBackUpByFiscalYear(string fiscalYear)
        {
            string filter = string.Format("{0}={1}", "FiscalYear", fiscalYear);
            return base.DataAccessManager.GetFilteredData<BalanceSheetBackup>(filter);
        }

        public int DeleteBalanceSheetBackup(List<BalanceSheetBackup> lstBalanceSheetBackup)
        {
            int result = 0;
            foreach (BalanceSheetBackup balanceSheetBackup in lstBalanceSheetBackup)
            {
                result = base.DataAccessManager.Delete<BalanceSheetBackup>(balanceSheetBackup.BalanceSheetID);
            }
            return result;
        }


        public List<TrialBalance> GetTrialBalance(int branchID, string fromDate, string toDate)
        {
            string sql = @"Select * from (
                            Select 1 Head,BranchID, AccountID, AccountsName,ChildAccountID, Description, (sum(DrAmount)-sum(CrAmount))Balance from (
                            Select j.BranchID, j.AccountID,pa.AccountsName, isnull(j.ChildAccountID,0)ChildAccountID,isnull(ca.Description,pa.AccountsName)Description, Sum(Amount) DrAmount,0 CrAmount  from Journal j
                            left outer join ParentAccount pa
                            on j.AccountID=pa.AccountID
                            left outer join ChildAccount ca
                            on j.ChildAccountID=ca.ChildAccountID
                            where pa.AccountHeadID=1 and DrCrIndecator='Dr'
                            and j.JournalDate between '{1}' and '{2}'
                            group by j.BranchID, j.AccountID,pa.AccountsName, j.ChildAccountID,ca.Description
                            union
                            Select j.BranchID, j.AccountID,pa.AccountsName, isnull(j.ChildAccountID,0)ChildAccountID,isnull(ca.Description,pa.AccountsName)Description, 0 DrAmount,Sum(Amount) CrAmount  from Journal j
                            left outer join ParentAccount pa
                            on j.AccountID=pa.AccountID
                            left outer join ChildAccount ca
                            on j.ChildAccountID=ca.ChildAccountID
                            where pa.AccountHeadID=1 and DrCrIndecator='Cr'
                            and j.JournalDate between '{1}' and '{2}'
                            group by j.BranchID, j.AccountID,pa.AccountsName, j.ChildAccountID,ca.Description)tbl
                            group by BranchID, AccountID, AccountsName,ChildAccountID, Description
                            union all
                            --Liabilities

                            Select 2 Head,BranchID, AccountID, AccountsName,ChildAccountID, Description, (sum(CrAmount)-sum(DrAmount))Balance from (
                            Select j.BranchID, j.AccountID,pa.AccountsName, isnull(j.ChildAccountID,0)ChildAccountID,isnull(ca.Description,pa.AccountsName)Description, Sum(Amount) DrAmount,0 CrAmount  from Journal j
                            left outer join ParentAccount pa
                            on j.AccountID=pa.AccountID
                            left outer join ChildAccount ca
                            on j.ChildAccountID=ca.ChildAccountID
                            where pa.AccountHeadID=2 and DrCrIndecator='Dr'
                            and j.JournalDate between '{1}' and '{2}'
                            group by j.BranchID, j.AccountID,pa.AccountsName, j.ChildAccountID,ca.Description
                            union
                            Select j.BranchID, j.AccountID,pa.AccountsName, isnull(j.ChildAccountID,0)ChildAccountID,isnull(ca.Description,pa.AccountsName)Description, 0 DrAmount,Sum(Amount) CrAmount  from Journal j
                            left outer join ParentAccount pa
                            on j.AccountID=pa.AccountID
                            left outer join ChildAccount ca
                            on j.ChildAccountID=ca.ChildAccountID
                            where pa.AccountHeadID=2 and DrCrIndecator='Cr'
                            and j.JournalDate between '{1}' and '{2}'
                            group by j.BranchID, j.AccountID,pa.AccountsName, j.ChildAccountID,ca.Description)tbl
                            group by BranchID, AccountID, AccountsName,ChildAccountID, Description
                            union all
                            --Expense
                            Select 3 Head,BranchID, AccountID, AccountsName,ChildAccountID, Description, (sum(DrAmount)-sum(CrAmount))Balance from (
                            Select j.BranchID, j.AccountID,pa.AccountsName, isnull(j.ChildAccountID,0)ChildAccountID,isnull(ca.Description,pa.AccountsName)Description, Sum(Amount) DrAmount,0 CrAmount  from Journal j
                            left outer join ParentAccount pa
                            on j.AccountID=pa.AccountID
                            left outer join ChildAccount ca
                            on j.ChildAccountID=ca.ChildAccountID
                            where pa.AccountHeadID=3 and DrCrIndecator='Dr'
                            and j.JournalDate between '{1}' and '{2}'
                            group by j.BranchID, j.AccountID,pa.AccountsName, j.ChildAccountID,ca.Description
                            union
                            Select j.BranchID, j.AccountID,pa.AccountsName, isnull(j.ChildAccountID,0)ChildAccountID,isnull(ca.Description,pa.AccountsName)Description, 0 DrAmount,Sum(Amount) CrAmount  from Journal j
                            left outer join ParentAccount pa
                            on j.AccountID=pa.AccountID
                            left outer join ChildAccount ca
                            on j.ChildAccountID=ca.ChildAccountID
                            where pa.AccountHeadID=3 and DrCrIndecator='Cr'
                            and j.JournalDate between '{1}' and '{2}'
                            group by j.BranchID, j.AccountID,pa.AccountsName, j.ChildAccountID,ca.Description)tbl
                            group by BranchID, AccountID, AccountsName,ChildAccountID, Description
                            union all
                            --Income
                            Select 4 Head,BranchID, AccountID, AccountsName,ChildAccountID, Description, (sum(CrAmount)-sum(DrAmount))Balance from (
                            Select j.BranchID, j.AccountID,pa.AccountsName, isnull(j.ChildAccountID,0)ChildAccountID,isnull(ca.Description,pa.AccountsName)Description, Sum(Amount) DrAmount,0 CrAmount  from Journal j
                            left outer join ParentAccount pa
                            on j.AccountID=pa.AccountID
                            left outer join ChildAccount ca
                            on j.ChildAccountID=ca.ChildAccountID
                            where pa.AccountHeadID=4 and DrCrIndecator='Dr'
                            and j.JournalDate between '{1}' and '{2}'
                            group by j.BranchID, j.AccountID,pa.AccountsName, j.ChildAccountID,ca.Description
                            union
                            Select j.BranchID, j.AccountID,pa.AccountsName, isnull(j.ChildAccountID,0)ChildAccountID,isnull(ca.Description,pa.AccountsName)Description, 0 DrAmount,Sum(Amount) CrAmount  from Journal j
                            left outer join ParentAccount pa
                            on j.AccountID=pa.AccountID
                            left outer join ChildAccount ca
                            on j.ChildAccountID=ca.ChildAccountID
                            where pa.AccountHeadID=4 and DrCrIndecator='Cr'
                            and j.JournalDate between '{1}' and '{2}'
                            group by j.BranchID, j.AccountID,pa.AccountsName, j.ChildAccountID,ca.Description)tbl
                            group by BranchID, AccountID, AccountsName,ChildAccountID, Description
                            union all
                            --Owner Equities
                            Select 5 Head,BranchID, AccountID, AccountsName,ChildAccountID, Description, (sum(CrAmount)-sum(DrAmount))Balance from (
                            Select j.BranchID, j.AccountID,pa.AccountsName, isnull(j.ChildAccountID,0)ChildAccountID,isnull(ca.Description,pa.AccountsName)Description, Sum(Amount) DrAmount,0 CrAmount  from Journal j
                            left outer join ParentAccount pa
                            on j.AccountID=pa.AccountID
                            left outer join ChildAccount ca
                            on j.ChildAccountID=ca.ChildAccountID
                            where pa.AccountHeadID=5 and DrCrIndecator='Dr'
                            and j.JournalDate between '{1}' and '{2}'
                            group by j.BranchID, j.AccountID,pa.AccountsName, j.ChildAccountID,ca.Description
                            union
                            Select j.BranchID, j.AccountID,pa.AccountsName, isnull(j.ChildAccountID,0)ChildAccountID,isnull(ca.Description,pa.AccountsName)Description, 0 DrAmount,Sum(Amount) CrAmount  from Journal j
                            left outer join ParentAccount pa
                            on j.AccountID=pa.AccountID
                            left outer join ChildAccount ca
                            on j.ChildAccountID=ca.ChildAccountID
                            where pa.AccountHeadID=5 and DrCrIndecator='Cr'
                            and j.JournalDate between '{1}' and '{2}'
                            group by j.BranchID, j.AccountID,pa.AccountsName, j.ChildAccountID,ca.Description)tbl
                            group by BranchID, AccountID, AccountsName,ChildAccountID, Description

                            )tbl where BranchID={0}";

            sql = string.Format(sql, branchID, fromDate, toDate);
            return base.DataAccessManager.ExecuteSQL<TrialBalance>(sql).Cast<TrialBalance>().ToList();


        }


    }
}
