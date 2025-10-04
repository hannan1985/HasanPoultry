using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Enums;
using NybSys.MTBF.Utility.Constants;
using IMFS.Common.Utility;

namespace IFMS.BLL
{
    public class ChildAccountManager : BllBase
    {

        public int InsertChildAccount(ChildAccount childAccount)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                base.DataAccessManager.BeginTransaction();
                childAccount.BranchID = MTBFConstants.AppConstants.BranchID;
                childAccount.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                childAccount.ChildAccountID = base.DataAccessManager.Insert<ChildAccount>(childAccount);


                if (childAccount.AccountID == IFMSConstant.AccountPayableID)
                {
                    Supplier supplier = new Supplier();
                    supplier.SupplierName = childAccount.Description;
                    supplier.BranchID = MTBFConstants.AppConstants.BranchID;
                    supplier.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                    supplier.CreatedDate = DateTime.Now;
                    supplier.CreatedBy = MTBFConstants.AppConstants.LoggedinUserID;
                    supplier.UpdatedDate = Convert.ToDateTime(MTBFConstants.DefauldDate);
                    supplier.SupplierID = base.DataAccessManager.Insert<Supplier>(supplier);
                    childAccount.ReferenceType = (int)MTBFEnums.ChildAccountType.Supplier;
                    childAccount.ReferenceID = supplier.SupplierID;

                    base.DataAccessManager.Update<ChildAccount>(childAccount);
                }
                else if (childAccount.AccountID == IFMSConstant.AccountReceivableID)
                {
                    Customer customer = new Customer();
                    customer.CustomerName = childAccount.Description;
                    customer.BranchID = MTBFConstants.AppConstants.BranchID;
                    customer.OrganizationID = MTBFConstants.AppConstants.OrganizationID;

                    customer.CreatedDate = DateTime.Now;
                    customer.CreatedBy = MTBFConstants.AppConstants.LoggedinUserID;
                    customer.UpdatedDate = Convert.ToDateTime(MTBFConstants.DefauldDate);
                    customer.CustomerID = base.DataAccessManager.Insert<Customer>(customer);
                    childAccount.ReferenceType = (int)MTBFEnums.ChildAccountType.Customer;
                    childAccount.ReferenceID = customer.CustomerID;

                    base.DataAccessManager.Update<ChildAccount>(childAccount);
                }
                else if (childAccount.AccountID == IFMSConstant.Bank)
                {
                    BankAccount bankAccount = new BankAccount();
                    bankAccount.BankName = childAccount.Description;
                    bankAccount.BranchID = MTBFConstants.AppConstants.BranchID;
                    bankAccount.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                    bankAccount.OpeningDate = DateTime.Now;
                    bankAccount.CreatedDate = DateTime.Now;
                    bankAccount.CreatedBy = MTBFConstants.AppConstants.LoggedinUserID;
                    bankAccount.UpdatedDate = Convert.ToDateTime(MTBFConstants.DefauldDate);
                    bankAccount.BankAccountID = base.DataAccessManager.Insert<BankAccount>(bankAccount);

                    childAccount.ReferenceType = (int)MTBFEnums.ChildAccountType.Bank;
                    childAccount.ReferenceID = bankAccount.BankAccountID;
                    base.DataAccessManager.Update<ChildAccount>(childAccount);
                }
                else if (childAccount.AccountID == IFMSConstant.OperatingExpense)
                {
                    ExpenseType expenseType = new ExpenseType();
                    expenseType.ExpenseTypeName = childAccount.Description;
                    expenseType.BranchID = MTBFConstants.AppConstants.BranchID;
                    expenseType.OrganizationID = MTBFConstants.AppConstants.OrganizationID;                  
                    expenseType.CreatedDate = DateTime.Now;
                    expenseType.CreatedBy = MTBFConstants.AppConstants.LoggedinUserID;
                    expenseType.UpdatedDate = Convert.ToDateTime(MTBFConstants.DefauldDate);
                    expenseType.ExpenseTypeID = base.DataAccessManager.Insert<ExpenseType>(expenseType);

                    childAccount.ReferenceType = (int)MTBFEnums.ChildAccountType.Expese;
                    childAccount.ReferenceID = expenseType.ExpenseTypeID;
                    base.DataAccessManager.Update<ChildAccount>(childAccount);
                }


                base.DataAccessManager.CommitTransaction();

            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
                base.DataAccessManager.Rollback();
            }
            finally
            {
                base.DataAccessManager.EndTransaction();
            }

            return result;
        }

        public int UpdateChildAccount(ChildAccount childAccount)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                childAccount.BranchID = MTBFConstants.AppConstants.BranchID;
                childAccount.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                base.DataAccessManager.Update<ChildAccount>(childAccount);
            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }

        public System.Collections.IList GetAllChildAccount()
        {
            return base.DataAccessManager.GetAll<ChildAccount>();
        }

        public ChildAccount GetChildAccountByID(int _childAccountID)
        {
            return base.DataAccessManager.GetByID<ChildAccount>(_childAccountID, "ChildAccountID");
        }

        public ChildAccount GetChildAccountByCustomerID(int customerID)
        {
            string filter = string.Format("{0}='{1}' and {2}={3}", "ReferenceType", (int)MTBFEnums.ChildAccountType.Customer, "ReferenceID", customerID);
            return base.DataAccessManager.GetFilteredData<ChildAccount>(filter).Cast<ChildAccount>().ToList().FirstOrDefault();

        }

        public ChildAccount GetChildAccountByPartyID(int partyID)
        {
            string filter = string.Format("{0}='{1}'", "SalesPartyID", partyID);
            return base.DataAccessManager.GetFilteredData<ChildAccount>(filter).Cast<ChildAccount>().ToList().FirstOrDefault();

        }

        public ChildAccount GetChildAccountBySupplierID(int supplierID)
        {
            string filter = string.Format("{0}='{1}' and {2}={3}", "ReferenceType", (int)MTBFEnums.ChildAccountType.Supplier, "ReferenceID", supplierID);
            return base.DataAccessManager.GetFilteredData<ChildAccount>(filter).Cast<ChildAccount>().ToList().FirstOrDefault();

        }

        public ChildAccount GetChildAccountByParentID(int parentAccountID)
        {
            string filter = string.Format("{0}='{1}'", "AccountID", parentAccountID);
            return base.DataAccessManager.GetFilteredData<ChildAccount>(filter).Cast<ChildAccount>().ToList().FirstOrDefault();
        }

        public ChildAccount GetChildAccountByMaterialSupplierID(int supplierID)
        {
            string filter = string.Format("{0}='{1}'", "MaterailSupplierID", supplierID);
            return base.DataAccessManager.GetFilteredData<ChildAccount>(filter).Cast<ChildAccount>().ToList().FirstOrDefault();
        }


        public List<ChildAccount> GetAllChildAccountByParentID(int parentAccountID)
        {
            string filter = string.Format("{0}='{1}'", "AccountID", parentAccountID);
            return base.DataAccessManager.GetFilteredData<ChildAccount>(filter).Cast<ChildAccount>().ToList();
        }

        public List<ChildAccount> GetAllChildAccountByBranchID(int branchID)
        {
            string filter = string.Format("{0}='{1}'", "BranchID", branchID);
            return base.DataAccessManager.GetFilteredData<ChildAccount>(filter).Cast<ChildAccount>().ToList();
        }

        public List<ChildAccount> GetFilteredChildAccount(string filter)
        {
            return base.DataAccessManager.GetFilteredData<ChildAccount>(filter).Cast<ChildAccount>().ToList();
        }
    }
}
