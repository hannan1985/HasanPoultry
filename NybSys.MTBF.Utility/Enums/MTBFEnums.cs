using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Reflection;

namespace NybSys.MTBF.Utility.Enums
{
    public class MTBFEnums
    {
        public enum DepositAndWithdrawType
        {
            Cash = 1,
            Cheque = 2
        }

        public enum CashReceieStatus
        {
            Received = 1,
            Cancel = 2
        }

        public enum DueType
        {
            Customer = 1,
            Supplier = 2
        }

        public enum JournalType
        {
            Inventory = 1,
            Production = 2
        }


        public enum ChildAccountType
        {
            Supplier = 1,
            Customer = 2,
            SalesParty = 3,
            MaterialSupplier = 4,
            Bank = 5,
            Expese = 6,
            OtherParty = 7,
            SalesCenter=8,
        }

        public enum MaterialReciveStatus
        {
            Issued = 1,
            Approved = 2
        }

        public enum DeliveryStatus
        {
            Delivered = 2,
            NotDelivered = 1
        }

        public enum ReturnResult
        {
            FAILED = -1,
            SUCCESS = 0,
            DUPLICATE = -2,
            NOTALLOWED = -3
        }

        public enum InstitutionalCellingLevel
        {
            COMPANY = 1,
            ACTIVITY = 2,
            RESPONSIBILITY = 3
        }

        public enum CustomerType
        {
            Permanent = 1,
            Temporary = 2,
            Other = 3
        }


        public enum POState
        {
            Opened = 0,
            Closed = 1,
        }

        public enum PurchseOrderStatus
        {
            Issued = 1,
            Checked = 2,
            Approved = 3,
            Canceled = 4
        }

        public enum BudgetRequestStatus
        {
            Requested = 1,
            Released = 2
        }
        public enum PurchseOrderType
        {
            Manually = 1,
            FromRequisition = 2
        }

        public enum PurchseOrderStage
        {
            Ordered = 1,
            Invoiced = 2,
            Vouchered = 3,
            Batched = 4,
            Paid = 5
        }

        public enum ContractProcessStage
        {
            Invoiced = 1,
            Vouchered = 2,
            Batched = 3,
            Paid = 4
        }

        public enum ObjectiveLevel
        {
            COMPANY = 1,
            ACTIVITY = 2,
            RESPONSIBILITY = 3,
            TASK = 4
        }

        public enum TaskType
        {
            Standard = 1,
            OnceOff = 2,
            Addtional = 3
        }

        public enum BankAccountType
        {
            Loan = 1,
            Current = 2,
            Deposit = 3
        }

        public enum BudgetStatus
        {
            Created = 1,
            Submitted = 2,
            Approved = 3,
            DisApproved = 4
        }

        public enum BudgetRequestedStatus
        {
            Requested = 1,
            Released = 2
        }


        public enum CostCenterCellingLevel
        {
            CONTROL_ITEM = 1,
            REPORTING_ITEM = 2,
            DETAIL_ITEM = 3
        }

        public enum TaskPriority
        {
            Low = 1,
            Medium = 2,
            High = 3
        }
        public enum RegistryStatus
        {
            Active = 2,
            Deleted = 3
        }

        public enum CostItemType
        {
            Revenue = 1,
            Expense = 2,
            Suspens = 3
        }

        public enum UserStatus
        {
            Active = 1,
            Inactive = 2,
            Deleted = 3
        }


        public enum UserType
        {
            Admin = 1,
            Other = 2,
        }


        public enum Application
        {
            Remote_Deduction = 1,
            Web_services = 2,
            Support_Module = 3,
            Government_Applicaiton = 4,
            Security = 5,
            MTBF = 6,
            CRA = 7,
            PIM = 8,
            Data_Import_Tools = 9
        }

        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            Modified = 4,
            Accessed = 5,
            Reviewed = 6,
            Approved = 7,
            Rejected = 8,
            Loggedout = 9,
            LoggedIn = 10,
            Settle = 11
        }

        public enum BudgetRequestType
        {
            Approval = 1,
            Release = 2
        }

        public enum InvoiceType
        {
            Supported = 1,
            Unsupported = 2
        }

        public enum VoucherType
        {
            Supported = 1,
            Unsupported = 2,
            Claim = 3
        }


        public enum VoucherRejectReason
        {
            Reason1 = 1,
            Reason2 = 2,
            Reason3 = 3
        }

        public enum InvoiceStatus
        {
            All = 0,
            Registered = 1,
            Vouchered = 2,
            Batched = 3,
            Paid = 4
        }

        public enum VoucherStatus
        {
            All = 0,
            Issued = 1,
            Checked = 2,
            Approved = 3,
            Rejected = 4,
            Batched = 5,
            Paid = 6
        }

        public enum BatchStatus
        {
            All = 0,
            Issued = 1,
            Paid = 2
        }

        public enum ClaimType
        {
            Overtime = 1,
            TravelAllowance = 2,
            LunchBill = 3,
            Conveyance = 4,
            Others = 5
        }

        public enum SDIndicator
        {
            Source = 1,
            Destination = 2
        }

        public enum DrCrIndicator
        {
            Debit = 1,
            Credit = 2
        }

        public enum FAHead
        {
            Asset = 1,
            Liabilities = 2,
            OE = 3,
            Income = 4,
            Expense = 5
        }


        public enum VoucherSerchOption
        {
            ContextOnly = 1,
            ReletedContext = 2
        }


        public enum BudgetReleaseType
        {
            [Description("Periodic Release")]
            PRelease = 0,
            [Description("PO Based Release")]
            POBasedRelease = 1,
            [Description("Contract Based Release")]
            CBasedRelease = 2
        }


        public enum CallerProject
        {
            MTBF = -1,
            BudgetRelease = -2,
            Payment = -3
        }

        public enum CheckingOpt
        {
            Insert = 1,
            Update = 2,
            Delete = 3
        }

        public enum CommonOpt
        {
            Insert = 1,
            Update = 2,
            Delete = 3
        }


        public enum RequisitionStatus
        {
            Issued = 1,
            Approved = 2,
            Canceled = 3,
            Ordered = 4
        }

        public enum DamageStatus
        {
            Issued = 1,
            Approved = 2,
            Cancelled = 3
        }


        public enum DeliveryMethods
        {
            Average = 1,
            FIFO = 2,
            LIFO = 3
        }

        public enum Month
        {
            January = 1,
            February = 2,
            March = 3,
            April = 4,
            May = 5,
            June = 6,
            July = 7,
            August = 8,
            September = 9,
            October = 10,
            November = 11,
            December = 12
        }


        //public static string GetDescription(Enum currentEnum)
        //{

        //    string description = string.Empty;
        //    DescriptionAttribute da = default(DescriptionAttribute);

        //    FieldInfo fi = currentEnum.GetType().GetField(currentEnum.ToString());
        //    da = (DescriptionAttribute)Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute));
        //    if (da != null)
        //    {
        //        description = da.Description;
        //    }
        //    else
        //    {
        //        description = currentEnum.ToString();
        //    }

        //    return description;

        //}

    }
}
