using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using IMFS.Common.DTO;

namespace NybSys.MTBF.Utility.Constants
{
    public static partial class MTBFConstants
    {
        public const int ADMIN_ROLE_ID = 1;
        public const string ADMIN_PERMISSION_CODE = "000000";
        public const string ADMIN_PERMISSION_GROUP_CODE = "0000";
        public const string DEFULT_UOM = "N/A";
        public const string CODE = "Code";
        public const string NAME = "Name";
        public const string ID = "ID";
        public const int CAPITAL_ACCOUNT_TYPE = 3;
        public const string DAY_START_TIME = " 00:00:00";
        public const string DAY_END_TIME = " 23:59:59";
        public const string DateFormat="yyyy/MM/dd";
        public const string DefauldDate = "1900/01/01";
        public const string currencyFormat = "#,##0.00";

        public static class DataField
        {

            //For BankDeposit enum
            public const string BANK_DEPOSIT_TYPE_VALUE = "BankDepositTypeValue";
            public const string BANK_DEPOSIT_TYPE_NAME = "BankDepositTypeName";

            //For bank account
            public const string BANK_ACCOUNT_ID = "BankAccountID";
            public const string BANK_ACCOUNT_NO = "BankAccountNo";

            //For contact persons
            public const string CONTACT_PERSON_ID = "ContactPersonID";
            public const string NAME = "Name";
            public const string EMAIL = "Email";

            //For Account Type
            public const string BANK_ACCOUNT_TYPE_VALUE = "BankAccountTypeValue";
            public const string BANK_ACCOUNT_TYPE_NAME = "BankAccountTypeName";

            //For country
            public const string COUNTRY_NAME = "CountryName";
            public const string COUNTRY_ID = "CountryID";

            //For Bank
            public const string BANK_ID = "BankID";
            public const string BANK_NAME = "BankName";
            public const string BANK_CODE = "BankCode";

            //For Branch 
            public const string BRANCH_ID = "BranchID";
            public const string BRANCH_NAME = "BranchName";
            public const string BRANCH_CODE = "BranchCode";

            //For Bank Account form
            public const string TRANSACTION_HISTORY = "Transaction History";
            public const string COMPANY_CODE = "CompanyCode";
            public const string COMPANY_NAME = "CompanyName";

            public const string ACTIVITY_CENTER_NAME = "ActivityCenterName";
            public const string ACTIVITY_CENTER_CODE = "ActivityCenterCode";

            public const string RESP_CENTER_NAME = "RespCenterName";
            public const string RESP_CENTER_CODE = "RespCenterCode";

            public const string TASK_CODE = "TaskCode";
            public const string TASK_NAME = "TaskName";

            public const string TASK_TYPE_ID = "TaskTypeID";
            public const string TASK_TYPE_NAME = "TaskTypeName";

            public const string USERS_ID = "UserID";
            public const string USERS_NAME = "Name";

            public const string CONTROL_ITEM_CODE = "ControlItemCode";
            public const string CONTROL_ITEM_NAME = "ControlItemName";

            public const string REPORTING_ITEM_CODE = "ReportingItemCode";
            public const string REPORTING_ITEM_NAME = "ReportingItemName";

            public const string DETAIL_ITEM_CODE = "DetailItemCode";
            public const string DETAIL_ITEM_NAME = "DetailItemName";

            public const string ACC_MASTER_CODE = "AccMasterCode";

            public const string CURRENCY_NAME = "CurrencyName";

            public const string CURRENCY_ID = "CurrencyID";
            public const string CURRENCY_SHORT_NAME = "ShortName";

            public const string UOM_ID = "UOMID";
            public const string UOM_NAME = "UOMName";

            public const string ISACTIVE = "IsActive";
            public const string FISCAL_YEAR = "FiscalYear";

            public const string USER_ID = "UserID";
            public const string PASSWORD = "Password";

            public const string COMPANY_DISPLAY_MEMBER = "CompanyDisplayMember";
            public const string ACTIVITY_CENTER_DISPLAY_MEMBER = "ActivityCenterDisplayMember";

            public const string CONTROL_ITEM_DISPLAY_MEMBER = "ControlItemDisplayMember";
            public const string REPORTING_ITEM_DISPLAY_MEMBER = "ReportingItemDisplayMember";

            public const string DETAIL_ITEM_DISPLAY_MEMBER = "DetailItemDisplayMember";
            public const string RESP_CENTER_DISPLAY_MEMBER = "RespCenterDisplayMember";

            public const string ACCOUNT_TYPE_DISPLAY_MEMBER = "AccountTypeDisplayMeber";
            public const string TASK_TYPE_DISPLAY_MEMBER = "TaskTypeDisplayMember";

            public const string OBJECTIVE_ID = "ObjectiveID";
            public const string OUTPUT_ID = "OutputID";
            public const string DESCRIPTION = "Description";
            public const string REFERENCE_CODE = "ReferenceCode";

            public const string ACCOUNT_TYPE_NAME = "AccountTypeName";
            public const string ACCOUNT_TYPE_ID = "AccountTypeID";

            public const string CELLING_LEVEL = "CellingLevel";
            public const string TASK_DISPLAY_MEMBER = "taskDisplayMember";

            public const string CELLING_BASE_AMOUNT = "BaseAmount";
            public const string CELLING_ONCEOFF_AMOUNT = "OnceOffAmount";
            public const string CELLING_REVISED_AMOUNT = "RevisedAmount";


            public const int COMBO_DEFAULT_ID = 0;
            public const string COMBO_DEFULT_CODE = "00";
            public const string COMBO_DEFAULT_NAME = "-Select-";
            public const string COMBO_DEFULT_TASK_TYPE = "All";
            public const string BUDGET_CODE = "BudgetCode";
            public const string YEAR = "Year";
            public const string CELLING_CODE = "CellingCode";
            public const string ROLE_ID = "RoleID";
            public const string ROLE_NAME = "RoleName";
            public const string IS_DELETED = "IsDeleted";
            public const string CURRENCY_CODE = "CurrencyCode";
            public const string ISLOCKED = "IsLocked";
            public const string ISBASE_CURRENCY = "IsBaseCurrency";
            public const string BUDGET_RELEASE_ID = "BudgetReleaseID";
            public const string BUDGET_RELEASE_REQUEST_ID = "BudgetRequestID";
            public const string ORDER_ID = "OrderID";
            public const string SUPPLIER_ID = "SupplierID";
            public const string SUPPLIER_DISPLAY_MEMBER = "SupplierName";
            public const string PURCHASE_ORDER_MEMBER = "OrderNumber";
            public const string PURCHASE_ORDER_ID = "PurchaseOrderID";
            public const string PAYMENT_METHOD_DISPLAY_MEMBER = "PaymentMethodName";
            public const string PAYMENT_METHOD_ID = "PaymentMethodID";
            public const string CONTRACT_TYPE_DISPLAY_MEMBER = "Description";

            public const string SHIPPING_AGENCY_ID = "ShippingAgencyID";
            public const string SHIPPING_AGENCY_DISPLAY_MEMBER = "ShippingAgencyName";

            public const string SHIPPING_METHOD_ID = "ShippingMethodID";
            public const string SHIPPING_METHOD_DISPLAY_MEMBER = "ShippingMethodName";
            public const string CNF_PROVIDER_DISPLAY_MEMBER = "CNFProviderName";
            public const string CNF_PROVIDER_ID = "CNFProviderID";
            public const string CONTRACT_TYPE_ID = "ContractTypeID";
            public const string PAYEE_NAME = "PayeeName";
            public const string PAYEE_ID = "PayeeID";
            public const string CONTRACT_ID = "ContractID";
            public const string CONTRACT_NUMBER = "ContractNumber";
            public const string INVOICE_TYPE = "InvoiceType";
            public const string STATUS = "Status";
            //  public const string SUPPLIER_ID = "SupplierID";
            public const string INVOICE_ID = "InvoiceRegisterID";
            public const string INVOICE_NUMBER = "InvoiceNumber";
            public const string CREDIT_NOTE_EXIST = "CreditNoteExists";
            public const string VOURCER_TYPE = "VoucherType";
            public const string SUPPLIER_NAME = "SupplierName";
            public const string EMPLOYEE_NAME = "EmployeeName";
            public const string EMPLOYEE_ID = "EmployeeID";
            public const string SD_INDICATOR = "SDIndicator";
            public const string JOURNAL_VOUCHER_ID = "JournalVoucherID";
            public const string DEBIT_AMOUNT = "DebitAmount";
            public const string CREDIT_AMOUNT = "CreditAmount";
            public const string VOURCER_NUMBER = "VoucherNumber";
            public const string VOURCER_DATE = "VoucherDate";
            public const string AMOUNT = "Amount";
            public const string VOUCHER_ID = "VoucherID";
            public const string TOTAL_AMOUNT = "TotalAmount";
            public const string INVOICE_DATE = "InvoiceDate";
            public const string TOTAL = "Total";
            public const string BATCH_NUMBER = "BatchNumber";
            public const string BATCH_ID = "BatchID";
            public const string BATCH_DETAIL_ID = "BatchDetailID";
            public const string CLAIM_VOUCHER_DETAIL_ID = "ClaimVoucherDetailID";
            public const string JOURNAL_VOUCHER_DETAIL_ID = "JournalVoucherDetailID";
            //  public const string FIN_YEAR = "FinYear";
            public const string PROJECT_NAME = "ProjectName";
            public const string PROJECT_CODE = "ProjectCode";
            public const string FA_TYPE_NAME = "FATypeName";
            public const string FA_TYPE_ID = "FATypeID";

            public const string FA_HEAD_ID = "FAHeadID";
            public const string REFERENCE_NO = "ReferenceNo";
            public const string FIN_YEAR_ID = "FinYearID";
            public const string NOT_APPLICABLE = "N/A";
            public const string REQUISITION_ID = "RequisitionID";
            public const string ORDER_DATE = "OrderDate";
            public const string REQUISITION_DATE = "DateOfRequisition";
            public const string TYPE = "Type";
            public const string TYPE_DESCRIPTION = "TypeDescription";
            public const string FA_HEAD_NAME = "FAHeadName";
            public const string BATCH_DATE = "BatchDate";
            public const string CREATED_DATE = "CratedDate";
            public const string CUSTOMER_TYPE_ID = "CustomerTypeID";
            public const string CUSTOMER_ID = "CustomerID";
            public const string CUSTOMER_NAME = "CustomerName";
            public const string PAYMENT_TERM_ID = "PaymentTermID";
            public const string DELIVERY_TERM_ID = "DeliveryTermID";
            public const string INVOICE_TERM_ID = "InvoiceTermID";
            public const string INVENTORY_NAME = "Name";
            public const string INVONTORY_ID = "InventoryID";
            public const string PRODUCT_TYPE_ID = "ProductTypeID";
            public const string PRODUCT_MODEL_ID = "ProductModelID";
            public const string PRODUCT_COLOR_ID = "ProductColorID";
            public const string PRODUCT_SIZE_ID = "ProductSizeID";
            public const string PRODUCT_ID = "ProductID";
            public const string PRODUCT_CODE = "ProductCode";
            public const string RECEIVE_PRODUCT_ID = "ReceiveProductID";
            public const string RECEIVE_PRODUCT_DETAIL_ID = "ReceiveProductDetailID";
            public const string PRODUCT_NAME = "ProductName";

            public const string MODULE_NAME = "ModuleName";
            public const string PERMISSION_ID = "PermissionID";
            public const string MODULE_ID = "ModuleID";
            public const string ACTION_ID = "ActionID";
            public const string ROLE_PERMISSION_MAP_ID = "RolePermissionID";
            public const string ROLE_ACTION_MAP_ID = "RoleActionMappingID";
            public const string ROLE_MODULE_MAP_ID = "RoleModuleMappingID";
            public const string ACTION_NAME = "ActionName";
            public const string PERMISSION_NAME = "PermissionName";



        }

        public static class QueryString
        {
            public const string IsActive = "IsActive = 1";
        }

        public static class AppConstants
        {
            public static string LoggedinUserID = string.Empty;
            public static Users LoggedinUser;

            public static bool IsAdminUser = false;
            public static DateTime LoginTime = DateTime.MinValue;
            public static string traveledPath = string.Empty;
            public static int OrganizationID;
            public static int BranchID;
        }

        public static class UIConstants
        {
            public const int MAX_COA_LENGTH = 10;
            public const int RESULT_SUCCESS = 0;
            public const int RESULT_FAILED = -1;

            public static Color INVALID_ROW_COLOR = Color.OrangeRed;
            public static Color ERROR_ROW_COLOR = Color.Red;
            public static Color NORMAL_ROW_COLOR = Color.White;
            public static Color READONLY_ROW_COLOR = Color.LightGray;
        }

        public static class SqlErrorCode
        {
            public const int PRIMAY_KEY_VIOLATION = 2627;
        }


        public static class GridHeader
        {

            //For bank account
            public const string BANK_ACCOUNT_ID = "Bank Account ID";

            //For employee
            public const string EMPLOYEE_NAME = "Employee Name";
            public const string EMPLOYEE_ID = "Employee ID";

            //For contact persons
            public const string CONTACT_PERSON_ID = "Contact Person ID";
            public const string NAME = "Name";
            public const string EMAIL = "Email";

            //For Bank Account form
            public const string ACCOUNT_NUMBER = "Account Number";
            public const string BANK_NAME = "Bank Name";
            public const string BRANCH_NAME = "Branch Name";
            public const string CURRENT_BALANCE = "Current Balance";
            public const string SPACE_CHARACTER = " ";

            public const string CONTRACTOR_NAME = "Contractor Name";
            public const string TYPE_OF_SUPPLY = "Type of Supply";
            public const string DETAIL_ITEM = "Detail Item";
            public const string QUANTITY = "Quantity";
            public const string PRICE = "Price";
            public const string CURRENCY = "Currency";
            public const string DISCOUNT = "Discount";
            public const string TOTAL = "Total";
            public const string PreviousRow = "Previous Row";
            public const string PAYEE_NAME = "Payee Name";
            public const string STATUS = "Status";
            public const string AMOUNT = "Amount";
            public const string CONTRACT_NUMBER = "Contract Number";
            public const string CONTRACT_TYPE = "Contract Type";
            public const string START_DATE = "Start Date";
            public const string END_DATE = "End Date";
            public const string INVOICE_ID = "Invoice ID";
            public const string INVOICE_NUMBER = "Invoice Number";
            public const string SUPPLIER_NAME = "Supplier Name";
            public const string DATE = "Date";
            public const string VAT = "VAT";
            public const string TYPE = "Type";
            public const string PONUMBER_AND_CONTRACTNUMBER = "PO Number/Contract Number";
            public const string MODE_OF_PAYMENT = "Mode of Payment";
            public const string INVOICE_TYPE = "Invoice Type";
            public const string PAYMENT_METHOD = "Payment Method";
            public const string ADDRESS = "Address";
            public const string PHONE = "Phone";
            public const string ACCOUNT_TYPE = "Account Type";
            public const string COMPANY = "Company";
            public const string DEPARTMENT = "Department";
            public const string SECTION = "Section";
            public const string ITEM_NAME = "Item Name";
            public const string DEBIT_AMOUNT = "Debit Amount";
            public const string CREDIT_AMOUNT = "Credit Amount";
            public const string BUDGET_BALANCE = "Budget Balance";
            public const string VOUCHER_NUMBER = "Voucher Number";
            public const string TOTAL_SOURCE_DEBIT = "Total Source Debit";
            public const string TOTAL_SOURCE_CREDIT = "Total Source Credit";
            public const string TOTAL_DEST_DEBIT = "Total Dest. Debit";
            public const string TOTAL_DEST_CREDIT = "Total Dest. Credit";
            public const string SOURCE_ACCOUNT = "Source Account";
            public const string SDEBIT = "SDebit";
            public const string SCREDIT = "SCredit";
            public const string DEST_ACCOUNT = "Destination Account";
            public const string DDEBIT = "DDebit";
            public const string DCREDIT = "DCredit";
            public const string CLAIM_BY = "Claim By";
            public const string UNIT_PRICE = "Unit Price";
            public const string BANK_CODE = "Bank Code";
            public const string BANK = "Bank";
            public const string DESCRIPTION = "Description";
            public const string BATCH_NUMBER = "Batch Number";
            public const string VIEW_DETAIL = "View Detail";
            public const string PO_NUMBER = "PO Number";
            public const string ORDERED_QUANTITY = "Ordered Quantity";
            public const string DUE_QUANTITY = "Due Quantity";
            public const string CONTRACT_DETAIL_ID = "Contract Detail ID";
            public const string JOURNAL_DATE = "Journal Date";
            public const string BALANCE = "Balance";
            public const string REFERENCE_NUMBER = "Reference Number";
            public const string ACCOUNT_ID = "Account ID";
            public const string BRANCH_ID = "Branch ID";
            public const string ACCOUNT_NO = "Account No";
            public const string CHART_OF_ACCOUNT_CODE = "Chart of Account Code";
            public const string FA_TYPE_NAME = "FA Type Name";
            public const string DETAIL_ITEM_NAME = "Detail Item Name";
            public const string DATE_OF_REQUISITION = "Date of Requisition";
            public const string REQUISITION_ITEM_ID = "Requisition Item ID";
            public const string REQUISITION_ID = "Requisition ID";
            public const string RESP_CENTER_CODE = "Resp Center Code";
            public const string PROJECT_CODE = "Project Code";
            public const string REPORTING_ITEM_CODE = "Reporting Item Code";
            public const string REQ_DATE = "Req. Date";
            public const string USER_ID = "User ID";
            public const string ADD_TO_ORDER = "Add to Order";
            public const string REPORTING_ITEM = "Reporting Item";
            public const string PROJECT = "Project";
            public const string ORDERED_VALUE = "Ordered Value";
            public const string ORDERED_DATE = "Ordered Date";
            public const string ORDERED_TYPE = "Order Type";
            public const string PROCESS_STAGE = "Process Stage";
            public const string STATE = "State";
            public const string ORDER_ID = "Order ID";
            public const string DESIGNATION = "Designation";
            public const string PRODUCT_NAME = "Product Name";
            public const string PRODUCT_CODE = "Product Code";
            public const string REORDER_LEVEL = "Reorder Level";
            public const string EXISTING_QTY = "Existing QTY";
            public const string PURCHASE_PRICE = "Purchase Price";
            public const string SALES_PRICE = "Sales Price";

            public const string ROLE_NAME = "Role Name";
            public const string PERMISSION_NAME = "Permission Name";
            public const string ACTION_NAME = "Action Name";
            public const string MODULE_NAME = "Module Name";
            public static string AVAILABLE_QUANTITY;



        }

        // Query constants was here
    }
}
