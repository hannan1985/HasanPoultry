using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.ComponentModel;

namespace IMFS.Common.Utility
{
    public static class IFMSEnum
    {


        public enum PaymentMethod
        {
            Cash = 1,
            Bank = 2,
            Both = 3
        }


        public enum AccountHead
        {
            Asset = 1,
            Liabilities = 2,
            Expense = 3,
            Income = 4,
            OwnerEquity = 5
        }

        public enum FinancialReportName
        {
            [Description("Trial Balance")]
            TrialBalance = 1,
            [Description("Income Statement")]
            IncomeStatement = 2,
            [Description("Balance Sheet")]
            BalanceSheet = 3,
            [Description("Detail Balance Sheet")]
            BalanceSheetDetail = 4
        }

        public enum AccountType
        {
            [Description("Fixed Asset")]
            FixedAsset = 1,
            [Description("Current Asset")]
            CurrentAsset = 2,
            [Description("Long term Liabilities")]
            LognTermLiabilities = 3,
            [Description("Short term Liabilities")]
            ShortTermLiabilities = 4,
            [Description("Operating Expense")]
            OperatingExpense = 5,
            [Description("Non-Operating Expense")]
            NonOperatingExpense = 6,
            [Description("Cost of Good Sold")]
            CostofGoodSold = 7,
            [Description("Operating Income")]
            OperatingIncome = 8,
            [Description("Non-Operating Income")]
            NonOperatingIncome = 9,
            [Description("Capital")]
            Capital = 10
        }

        public enum ReturnResult
        {
            Success = 1,
            Failed = 0
        }


        public enum PurchaseOrderStatus
        {
            Issued = 1,
            Approved = 2,
            Cancel = 3
        }



        public enum ReportName
        {

            [Description("Purchase Detail According to Date")]
            PurchaseDetial = 1,
            [Description("Sales Detail According to Date")]
            SalesDetail = 2,
            [Description("Inventory Detail")]
            InventoryDetail = 3,
            [Description("Customer Outstanding  According to Customer")]
            CustomerOutstanding = 4,
            [Description("Supplier Outstanding  According to Supplier")]
            SupplierOutstanding = 5,
            [Description("Profit Report According to Date")]
            ProfitReport = 6,
            [Description("All customer outstanding according to date")]
            AllCustomerOutstanding = 7,
            [Description("Cash Receive Information")]
            CashReceiveAccordingToDate = 8,
            [Description("Cash Payment Information")]
            CashPaymentAccordingToDate = 9,
            [Description("Stock Detail")]
            StockDetail = 10,
            [Description("Zone wise sales report")]
            ZoneWiseSalesReport = 11,
            [Description("Customer wise sales details")]
            CustomerSalesDetail = 12,
            [Description("Salesman wise sales report")]
            SalesmanwiseSalesReport = 13,
            [Description("Type wise inventory report")]
            TypeWiseInventory = 14,
            [Description("Date wise sales return report")]
            SalesReturnHistory = 15,
            [Description("Customer Transaction Detail")]
            CustomerTransaction = 16,
            [Description("Sales Representative Sales Report")]
            SalesRepresentativeSales = 17,
            [Description("Date and zone wise due.")]
            DateAndZoneWiseDue = 18,
            [Description("Product Used and Receive Report")]
            ProductUsedAndReceivedReport = 19,
            [Description("All supplier outstanding according to date")]
            AllSupplierOutstanding = 20,
        }


        public static string GetDescription(this Enum currentEnum)
        {
            string description = String.Empty;
            DescriptionAttribute da;

            FieldInfo fi = currentEnum.GetType().GetField(currentEnum.ToString());
            da = (DescriptionAttribute)Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute));
            description = da != null ? da.Description : currentEnum.ToString();
            return description;
        }

    }
}
