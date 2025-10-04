using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using IFMS.BLL;
using IMFS.Common.DTO;
using IMFS.Common.View;


// Please change the two gos if you are using for different database

namespace IFMS.Facade
{
    public class FacadeManager
    {

        #region "Instruction

        #endregion

        //#region "Facade Codes for TaskType"

        //// Here returns for Insert Statements
        //public int InsertTaskType(TaskType taskType)
        //{
        //    return new LookupManager().InsertTaskType(taskType);
        //}

        //// Here goes codes for Update Statements
        //public int UpdateTaskType(TaskType taskType)
        //{
        //    return new LookupManager().UpdateTaskType(taskType);
        //}

        //// Here goes codes for All Records call for the table &TaskType
        //public IList GetAllTaskType()
        //{
        //    return new LookupManager().GetAllTaskType();
        //}

        //// Here goes codes for Filtered Records call for the table &TaskType
        //public IList GetTaskTypeByName(string taskTypeName)
        //{
        //    return new LookupManager().GetTaskTypeByName(taskTypeName);
        //}

        ////This is an pice of code for Specific Records of a table
        //public TaskType GetTaskTypeByID(int ID, string primary)
        //{
        //    return new LookupManager().GetTaskTypeByID(ID, primary);
        //}

        //#endregion


        #region "Transaction"

        public void BegainTransaction()
        {
            new TransactionManager().BeginTransaction();
        }

        public void Rollback()
        {
            new TransactionManager().Rollback();
        }

        public void CommitTransaction()
        {
            new TransactionManager().CommitTransaction();
        }

        public void EndTransaction()
        {
            new TransactionManager().EndTransaction();
        }

        #endregion

        #region "Product Size"

        public int InsertProductSize(ProductSize productSize)
        {
            return new ProductManager().InsertProductSize(productSize);
        }

        public List<ProductSize> GetAllProductSize()
        {
            return new ProductManager().GetAllProductSize();
        }

        public int UpdateProductSize(ProductSize productSize)
        {
            return new ProductManager().UpdateProductSize(productSize);
        }

        public ProductSize GetProductSizeByID(int productSizeID)
        {
            return new ProductManager().GetProductSizeByID(productSizeID);
        }

        #endregion

        #region "Product Model"

        public int InsertProductModel(ProductModel productModel)
        {
            return new ProductManager().InsertProductModel(productModel);
        }

        public List<ProductModel> GetAllProductModel()
        {
            return new ProductManager().GetAllProductModel();
        }

        public int UpdateProductModel(ProductModel productModel)
        {
            return new ProductManager().UpdateProductModel(productModel);
        }

        public ProductModel GetProductModelByID(int productModelID)
        {
            return new ProductManager().GetPrductModelByID(productModelID);
        }

        #endregion

        #region "Product Color"

        public int InsertProductColor(ProductColor productColor)
        {
            return new ProductManager().InsertProductColor(productColor);
        }

        public List<ProductColor> GetAllProductColor()
        {
            return new ProductManager().GetAllProductColor();
        }

        public int UpdateProductColor(ProductColor productColor)
        {
            return new ProductManager().UpdateProductColor(productColor);
        }

        public ProductColor GetProductColorByID(int productColorID)
        {
            return new ProductManager().GetProductColorByID(productColorID);
        }

        #endregion


        public int InsertAccountHead(AccountHead accountHead)
        {
            return new AccountHeadManager().InsertAccountHead(accountHead);
        }

        public int UpdateAccountHead(AccountHead accountHead)
        {
            return new AccountHeadManager().UpdateAccountHead(accountHead);
        }


        public int InsertAccountType(AccountType accountType)
        {
            return new AccountTypeManager().InsertAccountType(accountType);
        }

        public int UpdateAccountType(AccountType accountType)
        {
            return new AccountTypeManager().UpdateAccountType(accountType);
        }


        public int InsertParentAccount(ParentAccount parentAccount)
        {
            return new ParentAccountManger().InsertParentAccouont(parentAccount);
        }

        public int UpdateParentAccount(ParentAccount parentAccount)
        {
            return new ParentAccountManger().UpdateParentAccouont(parentAccount);
        }


        public int InsertChildAccount(ChildAccount childAccount)
        {
            return new ChildAccountManager().InsertChildAccount(childAccount);
        }

        public int UpdateChildAccount(ChildAccount childAccount)
        {
            return new ChildAccountManager().UpdateChildAccount(childAccount);
        }

        public int InsertJournalAccount(JournalAccountsInformation journalAccount)
        {
            return new JournalAccountsManager().InsertJournalAccount(journalAccount);
        }

        public int UpdateJournalAccount(JournalAccountsInformation journalAccount)
        {
            return new JournalAccountsManager().UpdateJournalAccount(journalAccount);
        }


        public ParentAccount GetParentAccountByID(int _parentAccountID)
        {
            return new ParentAccountManger().GetParentAccountByID(_parentAccountID);
        }

        public ChildAccount GetChildAccountByID(int _childAccountID)
        {
            return new ChildAccountManager().GetChildAccountByID(_childAccountID);
        }

        public IList GetAllParentAccount()
        {
            return new ParentAccountManger().GetAllParentAccount();
        }

        public IList GetAllAccountType()
        {
            return new ParentAccountManger().GetAllAccountType();
        }

        public IList GetAllAccountHead()
        {
            return new AccountHeadManager().GetAllAccountHead();
        }

        public IList GetAllChildAccount()
        {
            return new ChildAccountManager().GetAllChildAccount();
        }

        public IList GetAllJournalAccount()
        {
            return new JournalAccountsManager().GetAllJournalAccount();
        }

        public AccountType GetAccountTypeByID(int accountTypeID)
        {
            return new AccountTypeManager().GetAccountTypeByID(accountTypeID);
        }

        public int GetJournalReferenceID()
        {
            return new JournalAccountsManager().GetJournalReferenceID();
        }

        public int InsertJournal(Journal journal)
        {
            return new JournalAccountsManager().InsertJournal(journal);
        }

        public IList GetAllItems()
        {
            return new ItemManager().GetAllItem();
        }

        public IList GetAllCategory()
        {
            return new CategoryManager().GetAllCategory();
        }

        public IList GetAllSubCategory()
        {
            return new SubCategoryManager().GetAllSubCategory();
        }

        public IList GetAllProduct()
        {
            return new ProductManager().GetAllProduct();
        }

        public IList GetAllProduct(int branchID, int organizationID)
        {
            return new ProductManager().GetAllProduct(branchID, organizationID);
        }
        public List<Product> GetProductByName(string productName)
        {
            return new ProductManager().GetProductByName(productName);
        }

        public IList GetAllCompany()
        {
            return new CompanyManager().GetAllCompany();
        }

        public IList GetAllSupplier()
        {
            return new SupplierManager().GetAllSullier();
        }


        public Supplier GetSupplierByID(int _supplierID)
        {
            return new SupplierManager().GetSupplierByID(_supplierID);
        }

       

        public Company GetCompanyByID(int _CompanyID)
        {
            return new CompanyManager().GetCompanyByID(_CompanyID);
        }

        public int InsertCompany(Company company)
        {
            return new CompanyManager().InsertCompany(company);
        }

        public int UpdateCompany(Company company)
        {
            return new CompanyManager().UpdateCompany(company);
        }

        public int InsertCustomer(Customer customer)
        {
            return new CustomerManager().SaveCustomerInformation(customer);
        }

        public Customer GetCustomerByID(int _CustomerID)
        {
            return new CustomerManager().GetCustomerByID(_CustomerID);
        }

       

        public IList GetAllCustomer()
        {
            return new CustomerManager().GetAllCustomer();
        }

        public List<ProductInformationForSales> GetAllProductForSales(int branchID, int organizationID)
        {
            return new ProductManager().GetAllProductForSales(branchID, organizationID);
        }

        public decimal GetSalesPriceByProductID(string productID)
        {
            return new ProductManager().GetSalesPriceByProductID(productID);
        }

        public int InsertSalesOrderDetail(List<SalesOrderDetail> lstSalesOrderDetail)
        {
            return new SalesManager().InsertSalesOrderDetail(lstSalesOrderDetail);
        }

        public int InsertSalesOrderDetail(SalesOrder salesOrder)
        {
            return new SalesManager().InsertSalesOrder(salesOrder);
        }

        public Customer GetCustomerByPhoneNumber(string phoneNumber, int branchID, int organizationID)
        {
            return new CustomerManager().GetCustomerByPhoneNumber(phoneNumber, branchID, organizationID);
        }

        public Employee GetEmployeeByID(int employeeID)
        {
            return new EmployeeManager().GetEmployeeByID(employeeID);
        }

        public IList GetAllCreditSales()
        {
            return new SalesManager().GetAllCreditSales();
        }

        public decimal GetTotalSalesAmountWithOutDiscount(int salesID)
        {
            return new SalesManager().GetTotalSalesAmountWithOutDiscount(salesID);
        }

        public DataTable GetSalesReport(int salesId)
        {
            return new SalesManager().GetSalesReport(salesId);
        }

        public DataTable GetCreditSalesReport(int salesId)
        {
            return new SalesManager().GetCreditSalesReport(salesId);
        }

        public List<CreditSales> GetSalesInformationByID(int salesID)
        {
            return new SalesManager().GetSalesInformationByID(salesID);
        }

        public List<SalesOrder> GetAllSalesOrder()
        {
            return new SalesManager().GetAllSalesOrder();
        }

        public List<SalesOrder> GetAllSalesOrderByBranchAndOrganization(int branchID, int organizationID)
        {
            return new SalesManager().GetAllSalesOrderByBranchAndOrganization(branchID, organizationID);
        }

        public IList GetSupplierByCompanyID(int companyID)
        {
            return new SupplierManager().GetSupplierByCompanyID(companyID);
        }

        public int InsertPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            return new PurchaseManager().InsertPurchaseOrder(purchaseOrder);
        }


        public int InsertPurchaseOrderInformation(PurchaseOrder purchaseOrder, List<PurchaseOrderDetail> lstPurchaseOrderDetail, string userID, ChildAccount supplierChildAccount)
        {
            return new PurchaseManager().InsertPurchaseOrderInformation(purchaseOrder, lstPurchaseOrderDetail, userID, supplierChildAccount);
        }


        public List<PurchaseOrder> GetAllPurchaseOrder()
        {
            return new PurchaseManager().GetAllPurchaseOrder();
        }

        public int InsertPurchaseOrderDetails(List<PurchaseOrderDetail> lstPurchaseOrderDetails)
        {
            return new PurchaseManager().InsertPurchaseOrderDetail(lstPurchaseOrderDetails);
        }

        public PriceInformation GetPriceInformationByProductID(string productID)
        {
            return new PurchaseManager().GetPriceInformationByProductID(productID);
        }

        public int InsertPriceInformation(PriceInformation priceInformation)
        {
            return new PurchaseManager().InsertPriceInformation(priceInformation);
        }

        public int UpdatePriceInformation(PriceInformation priceInformation)
        {
            return new PurchaseManager().UpdatePriceInformation(priceInformation);
        }

        public Product GetProductInformationByID(string productID)
        {
            return new ProductManager().GetProductInformationByID(productID);
        }

        public List<ProductInformationForPurchase> GetProductInformationBySupplierID(int supplierID)
        {
            return new ProductManager().GetProductInformationforPurchase(supplierID);
        }

        public IList GetAllProductType()
        {
            return new ProductManager().GetAllProductType();
        }

        public int GetMaxProductSerialNumber()
        {
            return new ProductManager().GetMaxProductSerialNumber();
        }

        public int InsertProduct(Product product)
        {
            return new ProductManager().InsertProduct(product);
        }

        public int UpdateProduct(Product product)
        {
            return new ProductManager().UpdateProduct(product);
        }

        public ProductType GetProductTypeByName(string productTypeName)
        {
            return new ProductManager().GetProductTypeByName(productTypeName);
        }

        public int InsertProductType(ProductType productType)
        {
            return new ProductManager().InsertProductType(productType);
        }

        public ProductType GetProductTypeByID(int productTypeID)
        {
            return new ProductManager().GetProductTypeByID(productTypeID);
        }

        public IList GetPaymentInformationByCompanyAndDate(int companyID, string fromDate, string toDate)
        {
            return new PaymentManager().GetPaymentInformationByCompanyAndDate(companyID, fromDate, toDate);
        }

        public Payment GetPaymentInformationByID(int _paymentID)
        {
            return new PaymentManager().GetPaymentByID(_paymentID);
        }

        public int UpdatePayment(Payment payment)
        {
            return new PaymentManager().UpdatePayment(payment);
        }

        public int InsertPayment(Payment payment)
        {
            return new PaymentManager().InsertPayment(payment);
        }

        public decimal GetDueAmountByCompanyAndSupplierID(int companyID, int SupplierID, int branchID, int organizationID)
        {
            return new PaymentManager().GetDueAmountByCompanyAndSupplierID(companyID, SupplierID, branchID, organizationID);
        }

        public decimal GetCustomerDueAmountByCustomerID(int customerId, int branchID, int organizationID)
        {
            return new CashReceiveManager().GetCustomerDueAmountByCustomerID(customerId, branchID, organizationID);
        }

        public CashReceive GetCashReceiveByID(int _cashReceiveID)
        {
            return new CashReceiveManager().GetCashReceiveByID(_cashReceiveID);
        }

        public int UpdateCashReceive(CashReceive cashReceive)
        {
            return new CashReceiveManager().UpdateCashReceive(cashReceive);
        }

        public int InsertCashReceive(CashReceive cashReceive)
        {
            return new CashReceiveManager().InsertCashReceive(cashReceive);
        }

        public List<CashReceive> GetAllCashReceive()
        {
            return new CashReceiveManager().GetAllCashReceive();
        }

        public List<CashReceive> GetAllCashReceiveInformationByDate(string fromDate, string toDate)
        {
            return new CashReceiveManager().GetAllCashReceiveInformationByDate(fromDate, toDate);
        }

        public List<CashReceive> GetAllCashReceiveInformationByDateRange(string fromDate, string toDate)
        {
            return new CashReceiveManager().GetAllCashReceiveInformationByDateRange(fromDate, toDate);
        }

        public IList GetStockDifferences()
        {
            return new ProductManager().GetStockDifferences();
        }

        public IList GetAllEmployee()
        {
            return new EmployeeManager().GetAllEmployee();
        }

        public Expense GetExpenseByID(int _expenseID)
        {
            return new ExpenseManager().GetExpenseByID(_expenseID);
        }

        public int InsertExpense(Expense expense)
        {
            return new ExpenseManager().InsertExpense(expense);
        }

        public int UpdateExpense(Expense expense)
        {
            return new ExpenseManager().UpdateExpense(expense);
        }

        public List<Expense> GetAllExpense()
        {
            return new ExpenseManager().GetAllExpense();
        }

        public IList GetAllExpenseByDate(string fromDate, string toDate)
        {
            return new ExpenseManager().GetAllExpenseByDate(fromDate, toDate);
        }

        public IList GetOrderableProductByCompanyID(int companyID)
        {
            return new PurchaseManager().GetOrderableProductByCompanyID(companyID);
        }

        public IList GetAllPharmacy()
        {
            return new PharmacyManager().GetAllPharmacy();
        }

        public IList GetAllExpireProduct()
        {
            return new ProductManager().GetAllExpireProduct();
        }

        public IList GetAllPurchaseInformationAccordingToDate(string _fromDate, string _toDate, int branchID, int organizaitonID)
        {
            return new PurchaseManager().GetAllPurchaseInformationAccordingToDate(_fromDate, _toDate, branchID, organizaitonID);
        }

        public IList GetAllSalesInformationAccordingToDate(string _fromDate, string _toDate, int branchID, int organizationID)
        {
            return new SalesManager().GetAllSalesInformationAccordingToDate(_fromDate, _toDate, branchID, organizationID);
        }

        public List<SalesAndPurchaseReport> GetAllZoneWiseSalesInformationAccordingToDate(string _fromDate, string _toDate, int branchID, int zoneID)
        {
            return new SalesManager().GetAllZoneWiseSalesInformationAccordingToDate(_fromDate, _toDate, branchID, zoneID);
        }
        public List<Inventroy> GetAllInventroyInformation(int branchID, int organizationID)
        {
            return new ProductManager().GetAllInventroyInformation(branchID, organizationID);
        }

        public IList GetCustomerOutstandingInformationByCustomerID(int customerID)
        {
            return new CustomerManager().GetCustomerOutstandingInformationByCustomerID(customerID);
        }

        public IList GetSalesDetailByCustomerID(int customerID)
        {
            return new SalesManager().GetSalesDetailByCustomerID(customerID);
        }

        public IList GetCashReceiveByCustomerID(int customerID)
        {
            return new SalesManager().GetCashReceiveByCustomerID(customerID);
        }

        public IList GetProfitInformationAccordingToDate(string _fromDate, string _toDate, int branchID, int organizationID)
        {
            return new SalesManager().GetProfitInformationAccordingToDate(_fromDate, _toDate, branchID, organizationID);
        }

        public List<Expense> GetTotalExpenseAmountByDate(string _fromDate, string _toDate, int branchID, int organizationID)
        {
            return new ExpenseManager().GetAllExpenseAmountByDate(_fromDate, _toDate, branchID, organizationID);


        }

        public IList GetPurchaseOrderBySupplierID(int supplierID)
        {
            return new PurchaseManager().GetPurchaseOrderBySupplierID(supplierID);
        }

        public IList GetPaymentInformationBySupplierID(int supplierID)
        {
            return new PaymentManager().GetPaymentInformationBySupplierID(supplierID);
        }

        public TrialPeriodInformation GetAllTrialPeriod()
        {
            return new LoginManager().GetTrialPeriod();
        }

        public int UpdateTrialPeriod(TrialPeriodInformation objtrialPeriod)
        {
            return new LoginManager().UpdateTrialPeriod(objtrialPeriod);
        }


        #region "Users"

        public Users GetUserByUserNameAndPassword(string username, string password)
        {
            return new LoginManager().GetUserByUserNameAndPassword(username, password);
        }

        public int CreateDataBaseBackUp(string backupDir)
        {
            return new LoginManager().CreateDataBaseBackUp(backupDir);
        }

        public Users GetUserByUserName(string userId)
        {
            return new LoginManager().GetUserByUserName(userId);
        }

        public int InsertUsers(Users user)
        {
            return new LoginManager().InsertUsers(user);
        }

        public int InsertEmployee(Employee employee)
        {
            return new EmployeeManager().InsertEmployee(employee);
        }

        public int UpdateEmployee(Employee employee)
        {
            return new EmployeeManager().UpdateEmployee(employee);
        }

        public Users GetUserByEmployeeID(int _employeeID)
        {
            return new LoginManager().GetUserByEmployeeID(_employeeID);
        }

        public int UpdateUser(Users user)
        {
            return new LoginManager().UpdateUsers(user);
        }


        #endregion




        public int InsertPharmacy(Organization pharmacy)
        {
            return new PharmacyManager().InsertOrganization(pharmacy);
        }

        public Organization GetPharmachyByID(int _pharmacyID)
        {
            return new PharmacyManager().GetOrganizationByID(_pharmacyID);
        }

        public int UpdatePharmacy(Organization pharmacy)
        {
            return new PharmacyManager().UpdateOrganization(pharmacy);
        }

        public ChildAccount GetChildAccountByCustomerID(int customerID)
        {
            return new ChildAccountManager().GetChildAccountByCustomerID(customerID);
        }

        public ChildAccount GetChildAccountBySupplierID(int supplierID)
        {
            return new ChildAccountManager().GetChildAccountBySupplierID(supplierID);
        }

        public ChildAccount GetChildAccountByParentID(int parentAccountID)
        {
            return new ChildAccountManager().GetChildAccountByParentID(parentAccountID);
        }

        //public UserSettings GetUserSettingsByEmployeeID(int employeeID)
        //{
        //    return new LoginManager().GetUserSettingsByEmployeeID(employeeID);
        //}

        public IList GetJournalByCashReceiveID(int _cashReceiveID)
        {
            return new JournalManager().GetJournalByCashReceiveID(_cashReceiveID);
        }

        public int UpdateJournal(Journal journal)
        {
            return new JournalManager().UpdateJournal(journal);
        }

        public IList GetJournalByCashPaymentID(int paymentID)
        {
            return new JournalManager().GetJournalByCashPaymentID(paymentID);
        }

        public PurchaseOrderDetail GetPurchaseOrderDetailByProductAndPurchaseID(string productID, int _purchaseID)
        {
            return new PurchaseManager().GetPurchaseOrderDetailByProductAndPurchaseID(productID, _purchaseID);
        }

        public IList GetAllPurchaseOrderDetailProductByProductAndPurchaseID(string productID, int _purchaseID)
        {
            return new PurchaseManager().GetAllPurchaseOrderDetailProductByProductAndPurchaseID(productID, _purchaseID);
        }

        public IList GetAllSalesProductByProductAndPurchaseID(string productID, int _purchaseID)
        {
            return new PurchaseManager().GetAllSalesProductByProductAndPurchaseID(productID, _purchaseID);
        }

        public IList GetAllBalanceSheet(string fromDate, string toDate)
        {
            return new FinancialReportManager().GetAllBalanceSheet(fromDate, toDate);
        }

        public decimal GetProfitOrLoss(string fromDate, string toDate, int branchID, int organizationID)
        {
            return new FinancialReportManager().GetProfitOrLoss(fromDate, toDate, branchID, organizationID);
        }

        public IList GetAllIncomeStatement(string _fromDate, string _toDate)
        {
            return new FinancialReportManager().GetAllIncomeStatement(_fromDate, _toDate);
        }

        public int InsertRetainEarning(RetainEarning retainEarning)
        {
            return new FinancialReportManager().InsertRetainEarning(retainEarning);
        }

        public RetainEarning GetRetainEarningByFiscalYear(int fiscalYear)
        {
            return new FinancialReportManager().GetRetainEarningByFiscalYear(fiscalYear);
        }

        public IList GetAllRetainEarning()
        {
            return new FinancialReportManager().GetAllRetainEarning();
        }

        public int InsertBalanceSheetBackup(List<BalanceSheetBackup> lstBalanceSheetBackup)
        {
            return new FinancialReportManager().InsertBalanceSheetBackup(lstBalanceSheetBackup);
        }

        public IList GetBalanceSheetBackUpByFiscalYear(string fiscalYear)
        {
            return new FinancialReportManager().GetBalanceSheetBackUpByFiscalYear(fiscalYear);
        }

        public int DeleteBalanceSheetBackup(List<BalanceSheetBackup> lstBalanceSheetBackup)
        {
            return new FinancialReportManager().DeleteBalanceSheetBackup(lstBalanceSheetBackup);
        }

        public int DeleteEmployee(Employee employee)
        {
            return new EmployeeManager().DeleteEmployee(employee);
        }

        public TrialPeriodInformation GetTrialPeriod()
        {
            return new EmployeeManager().GetTrialPeriod();
        }

        public int InsertTrialPeriod(TrialPeriodInformation trialPeriod)
        {
            return new EmployeeManager().InsertTrialPeriod(trialPeriod);
        }

        public List<Product> GetAllProductInformationByProductTypeID(int productTypeID)
        {
            return new ProductManager().GetAllProductByProductTypeID(productTypeID);
        }

        public List<Product> GetAllProductByProductModel()
        {
            return new ProductManager().GetAllProductByProductModel();
        }

        public SalesOrder GetSalesOrderByID(int salesOrderID)
        {
            return new SalesManager().GetSalesOrderByID(salesOrderID);
        }


        #region "Security"

        #region  "Module"

        public Module GetModuleByID(int moduleID)
        {
            return new SecurityManager().GetModuleByID(moduleID);
        }

        public Module GetModuleByName(string moduleName)
        {
            return new SecurityManager().GetModuleByName(moduleName);
        }

        public List<Module> GetAllModule()
        {
            return new SecurityManager().GetAllModule();
        }

        public List<Module> GetAllModuleByRoleID(int roleID)
        {
            return new SecurityManager().GetAllModuleByRoleID(roleID);
        }

        #endregion

        #region "Permission"

        public Permission GetPermissionByID(int permissionID)
        {
            return new SecurityManager().GetPermissionByID(permissionID);
        }

        public List<Permission> GetAllPermission()
        {
            return new SecurityManager().GetAllPermission();
        }

        public List<Permission> GetAllPermissionByRoleID(int roleID)
        {
            return new SecurityManager().GetAllPermissionByRoleID(roleID);
        }


        public List<Permission> GetAllPermissionByModuleID(int moduleID)
        {
            return new SecurityManager().GetAllPermissionByModuleID(moduleID);
        }

        public Permission GetPermissionByName(string permissionName)
        {
            return new SecurityManager().GetPermissionByName(permissionName);
        }

        #endregion

        #region "Action"

        public IMFS.Common.DTO.Action GetActionByID(int actionID)
        {
            return new SecurityManager().GetActionByID(actionID);
        }

        public List<IMFS.Common.DTO.Action> GetAllAction()
        {
            return new SecurityManager().GetAllAction();
        }

        public List<IMFS.Common.DTO.Action> GetAllActionByPermissionID(int permissionID)
        {
            return new SecurityManager().GetAllActionByPermissionID(permissionID);
        }

        #endregion

        #region "Role"

        public int InsertRole(Role role)
        {
            return new SecurityManager().InsertRole(role);
        }

        public int UpdateRole(Role role)
        {
            return new SecurityManager().UpdateRole(role);
        }

        public List<Role> GetAllRole()
        {
            return new SecurityManager().GetAllRole();
        }

        public Role GetRoleByID(int roleID)
        {
            return new SecurityManager().GetRoleByID(roleID);
        }

        public List<Role> GetAllRoleByUserID(string userID)
        {
            return new SecurityManager().GetAllRoleByUserID(userID);
        }

        #endregion

        #region "User Role"

        public int InsertuserRole(UserRole userRole)
        {
            return new SecurityManager().InsertUserRole(userRole);
        }

        public int UpdateUserRole(UserRole userRole)
        {
            return new SecurityManager().UpdateUserRole(userRole);
        }

        public List<UserRole> GetAllUserRole()
        {
            return new SecurityManager().GetAllUserRole();
        }

        public List<UserRole> GetAllUserRoleByUserID(string userID)
        {
            return new SecurityManager().GetAllUserRoleByUserID(userID);
        }

        #endregion

        #region "Role Module Mapping"

        public int InsertRoleModuleMapping(RoleModuleMapping roleModuleMapping)
        {
            return new SecurityManager().InsertRoleModuleMapping(roleModuleMapping);
        }

        public int UpdateRoleModuleMapping(RoleModuleMapping roleModuleMapping)
        {
            return new SecurityManager().UpdateRoleModuleMapping(roleModuleMapping);
        }

        public List<RoleModuleMapping> GetAllRoleModuleMapping()
        {
            return new SecurityManager().GetAllRoleModuleMapping();
        }

        public List<RoleModuleMapping> GetAllRoleModuleMappingByUserID(string userID)
        {
            return new SecurityManager().GetAllRoleModuleMappingByUserID(userID);
        }

        public RoleModuleMapping GetRoleModuleMapByID(int roleModuleMapID)
        {
            return new SecurityManager().GetRoleModuleMapByID(roleModuleMapID);
        }

        public RoleModuleMapping GetRoleModuleMapByRoleAndModuleID(int roleID, int moduleID)
        {
            return new SecurityManager().GetRoleModuleMapByRoleAndModuleID(roleID, moduleID);
        }

        #endregion

        #region "Role Permission Mapping"

        public int InsertRolePermissionMapping(RolePermissionMapping rolePermissionMapping)
        {
            return new SecurityManager().InsertRolePermissionMapping(rolePermissionMapping);
        }

        public int UpdateRolePermissionMapping(RolePermissionMapping rolePermissionMapping)
        {
            return new SecurityManager().UpdateRolePermissionMapping(rolePermissionMapping);
        }

        public List<RolePermissionMapping> GetAllRolePermissionMapping()
        {
            return new SecurityManager().GetAllRolePermissionMapping();
        }

        public List<RolePermissionMapping> GetAllRolePermissionMappingByUserAndModuleID(string userID, int moduleID)
        {
            return new SecurityManager().GetAllRolePermissionMappingByUserAndModuleID(userID, moduleID);
        }

        public RolePermissionMapping GetRolePermissionMapByID(int rolePermissionID)
        {
            return new SecurityManager().GetRolePermissionMappingByID(rolePermissionID);
        }

        public RolePermissionMapping GetRolePermissionMapByRolePermissionAndModuleID(int roleID, int moduleID, int permissionID)
        {
            return new SecurityManager().GetRolePermissionMapByRolePermissionAndModuleID(roleID, moduleID, permissionID);
        }


        #endregion

        #region "Role Action Mapping"

        public int InsertRoleActionMapping(RoleActionMapping roleActionMapping)
        {
            return new SecurityManager().InsertRoleActionMapping(roleActionMapping);
        }

        public int UpdateRoleActionMapping(RoleActionMapping roleActionMapping)
        {
            return new SecurityManager().UpdateRoleActionMapping(roleActionMapping);
        }

        public List<RoleActionMapping> GetAllRoleActionMapping()
        {
            return new SecurityManager().GetAllRoleActionMapping();
        }

        public List<RoleActionMapping> GetAllRoleActionMappingByUserID(string userID)
        {
            return new SecurityManager().GetAllRoleActionMappingByUserID(userID);
        }

        public RoleActionMapping GetRoleActionMappByID(int roleActionMapID)
        {
            return new SecurityManager().GetRoleActionMappingByID(roleActionMapID);
        }

        public RoleActionMapping GetRoleActionMapByRolePermissionAndActionID(int roleID, int permissionID, int actionID)
        {
            return new SecurityManager().GetRoleActionMapByRolePermissionAndActionID(roleID, permissionID, actionID);
        }

        public List<RoleActionMapping> GetAllRoleActionMappingByPermissionID(int permissionID)
        {
            return new SecurityManager().GetAllRoleActionMappingByPermissionID(permissionID);
        }

        #endregion

        #endregion


        public int UpdateUsers(Users users, List<Role> lstrole)
        {
            return new LoginManager().UpdateUsers(users, lstrole);
        }

        public Users GetUserByUserID(string _usersID)
        {
            return new LoginManager().GetUsersByUserID(_usersID);
        }

        public List<Users> GetAllUses()
        {
            return new LoginManager().GetAllUsers();
        }

        public int InsertUsers(Users users, List<Role> lstRole)
        {
            return new LoginManager().InsertUsers(users, lstRole);
        }

        public List<PurchaseOrderDetail> GetAllPurchaseOrderDetailByPurchaseID(int purchaseOrderID)
        {
            return new PurchaseManager().GetAllPurchaseOrderDetailByPurchaseID(purchaseOrderID);
        }

        public PurchaseOrder GetPurchaseOrderByID(int _purchaseOrderID)
        {
            return new PurchaseManager().GetPurchaseOrderByID(_purchaseOrderID);
        }

        public Product GetProductByID(string productID)
        {
            return new ProductManager().GetProductByID(productID);
        }

        public int UpdatePurchaseOrderInformation(PurchaseOrder purchaseOrder, List<PurchaseOrderDetail> lstPurchaseOrderDetail, string userID, ChildAccount supplierChildAccount)
        {
            return new PurchaseManager().UpdatePurchaseOrderInformation(purchaseOrder, lstPurchaseOrderDetail, userID, supplierChildAccount);
        }

        public int ApprovePurchaseOrder(PurchaseOrder purchaseOrder)
        {
            return new PurchaseManager().ApprovePurchaseOrder(purchaseOrder);
        }

        public List<SalesOrderDetail> GetAllSalesDetailBySalesID(int salesID)
        {
            return new SalesManager().GetAllSalesOrderDetailBySalesID(salesID);
        }

        public List<AllCustomerOutstanding> GetAllCustomerOutstandingByDate(string _fromDate, string _toDate, int branchID, int organizationID)
        {
            return new CustomerManager().GetAllCustomerOutstandingByDate(_fromDate, _toDate, branchID, organizationID);
        }

        public PurchaseOrder GetPurchaseOrderBySalesID(int salesId)
        {
            return new PurchaseManager().GetPurchaseOrderBySalesID(salesId);
        }

        public List<CashReceiveInformation> GetTotalCashReceiveInformationByDate(string _fromDate, string _toDate, int branchID, int organizationID)
        {
            return new CashReceiveManager().GetAllCashReceiveInformationByDate(_fromDate, _toDate, branchID, organizationID);
        }


        public List<CashPaymentInformation> GetTotalCashPaymentInformationByDate(string _fromDate, string _toDate, int branchID, int organizationID)
        {
            return new PaymentManager().GetTotalCashPaymentInformationByDate(_fromDate, _toDate, branchID, organizationID);
        }

        public int UpdateSalesOrderInformation(SalesOrder salesOrder, List<SalesOrderDetail> lstSalesOrderDetail)
        {
            return new SalesManager().UpdateSalesOrderInformation(salesOrder, lstSalesOrderDetail);
        }

        public SalesOrderDetail GetSalesDetailByID(int salesOrderDtailID)
        {
            return new SalesManager().GetSalesDetailByID(salesOrderDtailID);
        }

        public List<SalesOrder> GetAllSalesOrderByDate(string fromDate, string toDate, int branchID, int organizationID)
        {
            return new SalesManager().GetAllSalesOrderByDate(fromDate, toDate, branchID, organizationID);
        }

        public List<SalesOrderDetail> GetSalesOrderDetailByProductID(string productID, int branchID, int organizationID)
        {
            return new SalesManager().GetSalesOrderDetailByProductID(productID, branchID, organizationID);
        }

        public List<PurchaseOrderDetail> GetPurchaseOrderDetailByProductID(string productID, int branchID, int organizationID)
        {
            return new PurchaseManager().GetPurchaseOrderDetailByProductID(productID, branchID, organizationID);
        }



        #region "DistributeSample"

        public int InsertDistributeSample(DistributeSample distributeSample)
        {
            return new DistributionManager().InsertDistributeSample(distributeSample);
        }

        public int UpdateDistributeSample(DistributeSample distributeSample)
        {
            return new DistributionManager().UpdateDistributeSample(distributeSample);
        }

        public List<DistributeSample> GetAllDistributeSample()
        {
            return new DistributionManager().GetAllDistributeSample();
        }

        public DistributeSample GetDistributeSampleByID(int distributeSampleID)
        {
            return new DistributionManager().GetDistributeSampleByID(distributeSampleID);
        }

        public List<DistributeSample> GetDistributeSampleBySellerIDAndDate(int sellerID, string fromDate, string toDate)
        {
            return new DistributionManager().GetDistributeSampleBySellerIDAndDate(sellerID, fromDate, toDate);
        }

        public decimal GetAllGivenSampleByProductID(string productID, int branchID, int organizationID)
        {
            return new DistributionManager().GetAllGeivenSampleByProductID(productID, branchID, organizationID);
        }

        #endregion


        #region "Seller"

        public int InsertSeller(Seller seller)
        {
            return new DistributionManager().InsertSeller(seller);
        }

        public int UpdateSeller(Seller seller)
        {
            return new DistributionManager().UpdateSeller(seller);
        }

        public List<Seller> GetAllSeller()
        {
            return new DistributionManager().GetAllSeller();
        }

        public Seller GetSellerByID(int _SellerID)
        {
            return new DistributionManager().GetSellerByID(_SellerID);
        }

        #endregion

        #region "Sales Center"

        public List<SalesCenter> GetAllSalesCenter()
        {
            return new SalesCenterManager().GetAllSalesCenter();
        }

        public int InsertSalesCenter(SalesCenter salesCenter)
        {
            return new SalesCenterManager().InsertSalesCenter(salesCenter);
        }

        public int UpdateSalesCenter(SalesCenter salesCenter)
        {
            return new SalesCenterManager().UpdateSalesCenter(salesCenter);
        }

        public SalesCenter GetSalesCenterByID(int salesCeterID)
        {
            return new SalesCenterManager().GetSalesCenterByID(salesCeterID);
        }

        #endregion


        #region "Transfer"

        public int InsertTransferInformation(Transfer transfer, List<TransferDetail> lstTransferDetail)
        {
            return new TransferManager().InsertTransferInformation(transfer, lstTransferDetail);
        }

        public int UpdateTransferInformation(Transfer transfer, List<TransferDetail> lstTransferDetail)
        {
            return new TransferManager().UpdateTransferInformation(transfer, lstTransferDetail);
        }

        public Transfer GetTransferByID(int _TransferID)
        {
            return new TransferManager().GetTransferByID(_TransferID);
        }

        public List<Transfer> GetAllTransfer()
        {
            return new TransferManager().GetAllTransfer();
        }

        #endregion

        #region "Transfer"

        //public int InsertReceiveProductInformation(ReceiveProduct receiveProduct, List<ReceiveProductDetail> lstReceiveProductDetail)
        //{
        //    return new ReceiveProductManager().SaveReceiveProductInformation(receiveProduct, lstReceiveProductDetail);
        //}

        public int UpdateReceiveProductInformation(ReceiveProduct receiveProduct, List<ReceiveProductDetail> lstReceiveProductDetail)
        {
            return new ReceiveProductManager().UpdateReceiveProductInformation(receiveProduct, lstReceiveProductDetail);
        }

        public ReceiveProduct GetReceiveProductByID(int _ReceiveProductID)
        {
            return new ReceiveProductManager().GetReceiveProductByID(_ReceiveProductID);
        }

        public List<ReceiveProduct> GetAllReceiveProductByBranchAndOrganization(int branchID, int organizationID)
        {
            return new ReceiveProductManager().GetAllReceiveProductByBranchAndOrganization(branchID, organizationID);
        }

        #endregion


        #region "Transfer Detail"

        public int UpdateTransferDetail(TransferDetail transferDetail)
        {
            return new TransferManager().UpdateTransferDetail(transferDetail);
        }

        public TransferDetail GetTransferDetailByID(int _TransferDetailID)
        {
            return new TransferManager().GetTransferDetailByID(_TransferDetailID);
        }

        public List<TransferDetail> GetAllTransferDetail()
        {
            return new TransferManager().GetAllTransferDetail();
        }

        public List<TransferDetail> GetAllTransferDetailByTransferID(int transferID)
        {
            return new TransferManager().GetAllTransferDetailByTransferID(transferID);
        }

        public decimal GetAllTransferProductByProudctCode(string productCode, int branchID, int organizationID)
        {
            return new TransferManager().GetAllTransferProductByProudctCode(productCode, branchID, organizationID);
        }


        public List<Transfer> GetTransferBySalesCenterAndDate(int salesCenterID, string fromDate, string toDate)
        {
            return new TransferManager().GetTransferBySalesCenterAndDate(salesCenterID, fromDate, toDate);
        }

        #endregion

        #region "DamageInfo"

        

      


        public DamageInfo GetDamageInfoByID(int damageInfoID)
        {
            return new DamageManager().GetDamageInfoByID(damageInfoID);
        }

        public List<DamageInfo> GetAllDamageInfo()
        {
            return new DamageManager().GetAllDamageInfo();
        }

        public List<DamageInfo> GetAllDamageInfoByDate(string fromDate, string toDate)
        {
            return new DamageManager().GetAllDamageInfoByDate(fromDate, toDate);
        }


        public List<DamageDetail> GetAllDamageDetailbyDamageInfoID(int damageInfoID)
        {
            return new DamageManager().GetAllDamageDetailByDamageInfoID(damageInfoID);
        }

        public decimal GetAllDamageProductByProudctCode(string productCode, int branchID, int organizationID)
        {
            return new DamageManager().GetAllDamageProductByProudctCode(productCode, branchID, organizationID);
        }

        #endregion




        public List<StockDetailInformation> GetAllStockDetailInformationByDate(string stockDate, int branchID, int organizationID)
        {
            return new ProductManager().GetAllStockDetailInformationByDate(stockDate, branchID, organizationID);
        }

        public List<DistributeSample> GetAllSampleDistributionByProductIDAndDate(int branchID, int organizationID, string fromDate, string toDate)
        {
            return new DistributionManager().GetAllSampleDistributionByProductIDAndDate(branchID, organizationID, fromDate, toDate);
        }

        public List<DamageDetail> GetAllDamageProductByProductCodeAndDate(int branchID, int organizationID, string fromDate, string toDate)
        {
            return new DamageManager().GetAllDamageProductByProductCodeAndDate(branchID, organizationID, fromDate, toDate);
        }

        public List<SalesOrderDetail> GetAllSalesProductByProductIDandDate(int branchID, int organizationID, string fromDate, string toDate)
        {
            return new SalesManager().GetAllSalesProductByProductIDandDate(branchID, organizationID, fromDate, toDate);
        }

        public List<TransferDetail> GetAllTransterProductByProductIDandDAate(int branchID, int organizationID, string fromDate, string toDate)
        {
            return new TransferManager().GetAllTransterProductByProductIDandDAate(branchID, organizationID, fromDate, toDate);
        }

        public List<ReceiveProductDetail> GetAllReceiveProductByProductIDAndDate(int branchID, int organizationID, string fromDate, string toDate)
        {
            return new PurchaseManager().GetAllReceiveProductByProductIDAndDate(branchID, organizationID, fromDate, toDate);
        }

        public List<Branch> GetAllBranch()
        {
            return new BranchManager().GetAllBranch();
        }

        public int InsertBranch(Branch branch)
        {
            return new BranchManager().InsertBranch(branch);
        }

        public Branch GetBracnhByID(int _branchID)
        {
            return new BranchManager().GetBranchByID(_branchID);
        }

        public int UpdateBranch(Branch branch)
        {
            return new BranchManager().UpdateBranch(branch);
        }

        public List<Payment> GetAllPayment()
        {
            return new PaymentManager().GetAllPayment();
        }

        public int UpdateSalesOrder(SalesOrder salesOrder)
        {
            return new SalesManager().UpdateSalesOrder(salesOrder);
        }


        public List<Journal> GetAllJournalBySalesID(int salesID)
        {
            return new JournalManager().GetAllJournalBySalesID(salesID);
        }

        public void DeleteJournal(Journal journal)
        {
            new JournalManager().DeleteJournal(journal);
        }


        public void DeleteSalesDetail(SalesOrderDetail salesDetail)
        {
            new SalesManager().DeleteSalesDetail(salesDetail);
        }

        public List<RolePermissionMapping> GetAllRolePermissionMappingByRoleID(int roleID)
        {
            return new SecurityManager().GetAllRolePermissionMappingByRoleID(roleID);
        }



        #region "Sales Quotation"



        public SalesQuotationDetail GetSalesQuotationDetailByID(int salesQuotationDetailID)
        {
            return new SalesQuotationManager().GetSalesQuotationDetailByID(salesQuotationDetailID);
        }


        /// <summary>
        /// Method to insert sales quotation information.
        /// </summary>
        /// <param name="salesQuotation"></param>
        /// <param name="lstSalesQuotationDetail"></param>
        /// <returns></returns>
        public int InsertSalesQuotationInformation(SalesQuotation salesQuotation, List<SalesQuotationDetail> lstSalesQuotationDetail)
        {
            return new SalesQuotationManager().InsertSalesQuotationInformation(salesQuotation, lstSalesQuotationDetail);
        }



        /// <summary>
        /// Method to update sales quotation information.
        /// </summary>
        /// <param name="salesQuotation"></param>
        /// <param name="lstSalesQuotationDetail"></param>
        /// <returns></returns>
        public int UpdateSalesQuotationInformation(SalesQuotation salesQuotation, List<SalesQuotationDetail> lstSalesQuotationDetail)
        {
            return new SalesQuotationManager().UpdateSalesQuotationInformation(salesQuotation, lstSalesQuotationDetail);
        }



        public List<SalesQuotationDetail> GetAllSalesQuotationDetailBySalesQuotationID(int salesQuotationID)
        {
            return new SalesQuotationManager().GetAllSalesQuotationDetailBySalesQuotationID(salesQuotationID);
        }

        public List<SalesQuotation> GetAllSalesQuotationByCustomerID(int customerID)
        {
            return new SalesQuotationManager().GetAllSalesQuotationByCustomerID(customerID);
        }



        public SalesQuotation GetSalesQuotationByID(int quotationID)
        {
            return new SalesQuotationManager().GetSalesQuotationByID(quotationID);
        }


        #endregion


        #region "Delivery Product"

        /// <summary>
        /// Method to insert product type.
        /// </summary>
        /// <param name="productDelivery"></param>
        /// <returns></returns>
        public int InsertDeliveryProduct(DeliveryProduct productDelivery, List<DeliveryProductDetail> lstDeliveryProductDetail)
        {
            return new ProductDeliveryMnager().InsertDeliveryProduct(productDelivery, lstDeliveryProductDetail);
        }

        public List<DeliveryProduct> GetDeliveryProductBySalesID(int salesID)
        {
            return new ProductDeliveryMnager().GetDeliveryProductBySalesID(salesID);
        }

        /// <summary>
        /// Method to update product type.
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public int UpdateDeliveryProduct(DeliveryProduct productDelivery, List<DeliveryProductDetail> lstDeliveryProductDetail)
        {
            return new ProductDeliveryMnager().UpdateDeliveryProduct(productDelivery, lstDeliveryProductDetail);
        }

        /// <summary>
        /// Method to get product type by id.
        /// </summary>
        /// <param name="productModelID"></param>
        /// <returns></returns>
        public DeliveryProduct GetDeliveryProductByID(int productDeliveryID)
        {
            return new ProductDeliveryMnager().GetDeliveryProductByID(productDeliveryID);
        }


        /// <summary>
        /// Method to get all product Delivery information by product code.
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        public List<DeliveryProduct> GetDeliveryProductByProductCode(string productCode)
        {
            return new ProductDeliveryMnager().GetDeliveryProductByProductCode(productCode);
        }

        #endregion

        #region "Delivery Product Detail"





        /// <summary>
        /// Method to get all product type information.
        /// </summary>
        /// <returns></returns>
        public List<DeliveryProductDetail> GetAllDeliveryProductDetailByDeliveryID(int productDeliveryID)
        {
            return new ProductDeliveryMnager().GetAllDeliveryProductDetailByDeliveryID(productDeliveryID);
        }

        /// <summary>
        /// Method to get product type by id.
        /// </summary>
        /// <param name="productModelID"></param>
        /// <returns></returns>
        public DeliveryProductDetail GetDeliveryProductDetailByID(int productDeliveryDetailID)
        {
            return new ProductDeliveryMnager().GetDeliveryProductDetailByID(productDeliveryDetailID);
        }

        public List<DeliveryProductDetail> GetAllDeliveryProductDetailByDeliveryIDAndProductCode(int deliveryProductID, string productCode)
        {
            return new ProductDeliveryMnager().GetAllDeliveryProductDetailByDeliveryIDAndProductCode(deliveryProductID, productCode);
        }

        #endregion

        #region "Delivery Message"
        public int InsertDeliveryMessages(DeliveryMessages deliveryMessages)
        {
            return new DeliveryMessageManager().InsertDeliveryMessages(deliveryMessages);
        }


        public int UpdateDeliveryMessages(DeliveryMessages deliveryMessages)
        {
            return new DeliveryMessageManager().UpdateDeliveryMessages(deliveryMessages);
        }


        public DeliveryMessages GetDeliveryMessagesByID(int deliveryMessagesID)
        {
            return new DeliveryMessageManager().GetDeliveryMessagesByID(deliveryMessagesID);
        }

        public List<DeliveryMessages> GetAllDeliveryMessages()
        {
            return new DeliveryMessageManager().GetAllDeliveryMessages();
        }

        #endregion

        public List<DeliveryProduct> GetDeliveryProductByDate(string fromDate, string toDate)
        {
            return new ProductDeliveryMnager().GetDeliveryProductByDate(fromDate, toDate);
        }

        public List<ProductType> GetFilteedProductType(string filter)
        {
            return new ProductManager().GetFilteedProductType(filter);
        }

        public List<ProductModel> GetFilteedProductModel(string filter)
        {
            return new ProductManager().GetFilteedProductModel(filter);
        }

        public List<PurchaseOrderDetail> GetPurcahseOrderDetailFiltered(string filter)
        {
            return new PurchaseManager().GetPurcahseOrderDetailFiltered(filter);
        }



        public List<Product> GetFilteredProduct(string filter)
        {
            return new ProductManager().GetFilteredProduct(filter);
        }

        public List<PurchaseOrder> GetAllPurchaseOrderByDate(string fromDate, string toDate, int branchID, int organizationID)
        {
            return new PurchaseManager().GetAllPurchaseOrderByDate(fromDate, toDate, branchID, organizationID);
        }

        public List<SalesOrder> GetAllDeveloperSalesOrderByDate(string fromDate, string toDate, int branchID, int organizationID)
        {
            return new SalesManager().GetAllDeveloperSalesOrderByDate(fromDate, toDate, branchID, organizationID);
        }

        public List<DeliveryMessages> GetAllDeliveryMessagesByDateBranchAndOrganization(string fromDate, string toDate, int branchID, int organizationID)
        {
            return new DeliveryMessageManager().GetAllDeliveryMessagesByDateBranchAndOrganization(fromDate, toDate, branchID, organizationID);
        }

        public List<ReceiveProductDetail> GetAllReceiveProductDetailByReceiveProductID(int _receiveID)
        {
            return new ReceiveProductManager().GetAllReceiveProductDetailByReceiveProductID(_receiveID);
        }

        public List<ReceiveProduct> GetAllReceiveProductByDate(string fromDate, string toDate)
        {
            return new ReceiveProductManager().GetAllReceiveProductByDate(fromDate, toDate);
        }

        public List<SalesCenter> GetFilteredSalesCenterInformation(string filter)
        {
            return new SalesCenterManager().GetFilteredSalesCenterInformation(filter);
        }

        public List<Users> GetFilterUser(string filter)
        {
            return new LoginManager().GetFilteredUser(filter);
        }

        public List<Product> GetAllProductByBranchAndOrganizationID(int branchID, int organizationID)
        {
            return new ProductManager().GetAllProductByBranchAndOrganizationID(branchID, organizationID);
        }

        public List<PurchaseOrderDetail> GetFilteredPurchaseOrderDetail(string filter)
        {
            return new PurchaseManager().GetFilteredPurchaseOrderDetail(filter);
        }

        public int CancelPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            return new PurchaseManager().CancelPurchaseOrder(purchaseOrder);
        }

        public Product GetProductByBarCode(string barCode)
        {
            return new ProductManager().GetProductByBarCode(barCode);
        }

       
    }
}
