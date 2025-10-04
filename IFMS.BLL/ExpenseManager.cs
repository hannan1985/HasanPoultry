using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;
using System.Collections;
using IMFS.Common.Utility;
using NybSys.MTBF.Utility.Enums;
using NybSys.MTBF.Utility.Constants;

namespace IFMS.BLL
{
    public class ExpenseManager : BllBase
    {
        public int InsertExpense(Expense expense)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            int expenseID = 0;
            try
            {
                base.DataAccessManager.BeginTransaction();
                expense.CreatedDate = DateTime.Now;
                expense.CreatedBy = MTBFConstants.AppConstants.LoggedinUserID;
                expense.UpdatedDate = Convert.ToDateTime(MTBFConstants.DefauldDate);
                expenseID = base.DataAccessManager.Insert<Expense>(expense);
                expense.ExpenseID = expenseID;
                InsertJournalInformationForExpense(expense);

                base.DataAccessManager.CommitTransaction();
            }
            catch
            {
                base.DataAccessManager.Rollback();
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            finally
            {
                base.DataAccessManager.EndTransaction();
            }
            return result;
        }


        public int InsertJournalInformationForExpense(Expense expense)
        {
            int result = 0;
            int referenceID = 1;
            referenceID = new JournalAccountsManager().GetJournalReferenceID();

            string filter = string.Format("{0}='{1}' and {2}={3}", "ReferenceType", (int)MTBFEnums.ChildAccountType.Expese, "ReferenceID", expense.ExpenseType);
            ChildAccount expenseChildAccount = base.DataAccessManager.GetFilteredData<ChildAccount>(filter).Cast<ChildAccount>().ToList().FirstOrDefault();

            filter = string.Format("{0}='{1}' and {2}={3}", "ReferenceType", (int)MTBFEnums.ChildAccountType.Bank, "ReferenceID", expense.BankAccountID);
            ChildAccount bankChildAccount = base.DataAccessManager.GetFilteredData<ChildAccount>(filter).Cast<ChildAccount>().ToList().FirstOrDefault();




            if (expense.CashAmount > 0)
            {
                for (int i = 0; i <= 1; i++)
                {
                    IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();
                    if (i == 0)
                    {
                        journal.DrCrIndecator = "Dr";
                        journal.AccountID = expenseChildAccount.AccountID;
                        journal.ChildAccountID = expenseChildAccount.ChildAccountID;
                    }
                    else
                    {
                        journal.DrCrIndecator = "Cr";
                        journal.ChildAccountID = (expense.CashAccountID > 0) ? expense.CashAccountID : 0;
                        journal.AccountID =  IFMSConstant.CashAccountID;
                    }

                    journal.Description = expense.Description;
                    journal.Amount = expense.CashAmount;
                    journal.ReferenceNo = referenceID;
                    journal.ExpenseID = expense.ExpenseID;
                    journal.JournalType = (int)MTBFEnums.JournalType.Inventory;
                    journal.BranchID = expense.BranchID;
                    journal.OrganizationID = expense.OrganizationID;
                    base.DataAccessManager.Insert<Journal>(journal);
                }

            }

            if (expense.BankAmount > 0)
            {
                for (int i = 0; i <= 1; i++)
                {
                    IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();


                    if (i == 0)
                    {
                        journal.DrCrIndecator = "Dr";
                        journal.AccountID = expenseChildAccount.AccountID;
                        journal.ChildAccountID = expenseChildAccount.ChildAccountID;
                    }
                    else
                    {
                        journal.DrCrIndecator = "Cr";
                        journal.AccountID = bankChildAccount.AccountID;
                        journal.ChildAccountID = bankChildAccount.ChildAccountID;
                    }
                    journal.Description = "Expense paid from bank";
                    journal.JAccountID = 0;
                    journal.Amount = expense.BankAmount;
                    journal.ReferenceNo = referenceID;
                    journal.ExpenseID = expense.ExpenseID;
                    journal.JournalType = (int)MTBFEnums.JournalType.Inventory;
                    journal.BranchID = MTBFConstants.AppConstants.BranchID;
                    journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                    base.DataAccessManager.Insert<Journal>(journal);
                }
            }






            return result;
        }


        public int UpdateExpense(Expense expense)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            try
            {
                base.DataAccessManager.BeginTransaction();
                expense.UpdatedBy = MTBFConstants.AppConstants.LoggedinUserID;
                expense.UpdatedDate = DateTime.Now;
                base.DataAccessManager.Update<Expense>(expense);

                DeletePreviousJournal(expense);

                InsertJournalInformationForExpense(expense);
                base.DataAccessManager.CommitTransaction();
            }
            catch
            {
                base.DataAccessManager.Rollback();
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            finally
            {
                base.DataAccessManager.EndTransaction();
            }
            return result;
        }

        private void DeletePreviousJournal(Expense expense)
        {
            string filter = string.Format("{0}={1}", "ExpenseID", expense.ExpenseID);
            foreach (Journal journal in base.DataAccessManager.GetFilteredData<Journal>(filter))
            {
                base.DataAccessManager.Delete<Journal>(journal.JournalID);
            }
        }

        public Expense GetExpenseByID(int expenseID)
        {
            return base.DataAccessManager.GetByID<Expense>(expenseID, "ExpenseID");
        }

        public List<Expense> GetAllExpense()
        {
            return base.DataAccessManager.GetAll<Expense>().Cast<Expense>().ToList();
        }


        public IList GetAllExpenseByDate(string fromDate, string toDate)
        {
            string filter = string.Format("ExpenseDate between '{0}' AND '{1}'", fromDate, toDate);
            return base.DataAccessManager.GetFilteredData<Expense>(filter);
        }

        public List<Expense> GetAllExpenseAmountByDate(string _fromDate, string _toDate, int branchID, int OrganizationID)
        {
            string filter = string.Format(IFMSConstant.QueryConstants.GET_TOTAL_EXPENSE_BY_DATE, _fromDate, _toDate, branchID, OrganizationID);
            return base.DataAccessManager.ExecuteSQL<Expense>(filter).Cast<Expense>().ToList();
        }



        public int SaveExpenseType(ExpenseType expenseType)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            try
            {
                base.DataAccessManager.BeginTransaction();

                if (expenseType.ExpenseTypeID > 0)
                {
                    expenseType.UpdatedBy = MTBFConstants.AppConstants.LoggedinUserID;
                    expenseType.UpdatedDate = DateTime.Now;
                    base.DataAccessManager.Update<ExpenseType>(expenseType);

                    string filter = string.Format("{0}='{1}' and {2}={3}", "ReferenceType", (int)MTBFEnums.ChildAccountType.Expese, "ReferenceID", expenseType.ExpenseTypeID);
                    ChildAccount childAccount = base.DataAccessManager.GetFilteredData<ChildAccount>(filter).Cast<ChildAccount>().ToList().FirstOrDefault();
                    if (childAccount != null)
                    {
                        childAccount.Description = expenseType.ExpenseTypeName;
                        base.DataAccessManager.Update<ChildAccount>(childAccount);
                    }
                    else
                    {
                        childAccount = new ChildAccount();
                        childAccount.AccountID = IFMSConstant.OperatingExpense;
                        childAccount.Description = expenseType.ExpenseTypeName;
                        childAccount.ReferenceType = (int)MTBFEnums.ChildAccountType.Expese;
                        childAccount.ReferenceID = expenseType.ExpenseTypeID;
                        childAccount.BranchID = MTBFConstants.AppConstants.BranchID;
                        childAccount.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                        childAccount.CreatedBy = MTBFConstants.AppConstants.LoggedinUser.UserID;
                        childAccount.CreatedDate = DateTime.Now;
                        childAccount.Status = 1;
                        base.DataAccessManager.Insert<ChildAccount>(childAccount);
                    }
                }
                else
                {
                    expenseType.BranchID = MTBFConstants.AppConstants.BranchID;
                    expenseType.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                    expenseType.CreatedDate = DateTime.Now;
                    expenseType.CreatedBy = MTBFConstants.AppConstants.LoggedinUserID;
                    expenseType.UpdatedDate = Convert.ToDateTime(MTBFConstants.DefauldDate);
                    expenseType.ExpenseTypeID = base.DataAccessManager.Insert<ExpenseType>(expenseType);
                    ChildAccount childAccount = new ChildAccount();
                    childAccount.AccountID = IFMSConstant.OperatingExpense;
                    childAccount.Description = expenseType.ExpenseTypeName;
                    childAccount.ReferenceType = (int)MTBFEnums.ChildAccountType.Expese;
                    childAccount.ReferenceID = expenseType.ExpenseTypeID;
                    childAccount.BranchID = MTBFConstants.AppConstants.BranchID;
                    childAccount.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                    childAccount.CreatedBy = MTBFConstants.AppConstants.LoggedinUser.UserID;
                    childAccount.CreatedDate = DateTime.Now;
                    childAccount.Status = 1;
                    base.DataAccessManager.Insert<ChildAccount>(childAccount);
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

        public int UpdateExpenseType(ExpenseType expenseType)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                this.DataAccessManager.Update<ExpenseType>(expenseType);

            }
            catch (Exception exp)
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }

            return result;
        }

        public List<ExpenseType> GetAllExpenseType()
        {
            return this.DataAccessManager.GetAll<ExpenseType>().Cast<ExpenseType>().ToList(); ;
        }

        public ExpenseType GetExpenseTypeByID(int expenseTypeID)
        {
            return this.DataAccessManager.GetByID<ExpenseType>(expenseTypeID, "ExpenseTypeID");
        }

        public List<Expense> GetFilteredExpense(string filter)
        {
            return base.DataAccessManager.GetFilteredData<Expense>(filter).Cast<Expense>().ToList();
        }

        //////////////////////////

        private void DeletePreviousJournal(ProductionUnitExpense expense)
        {
            string filter = string.Format("{0}={1}", "ProductionCostID", expense.ProductionUnitExpenseID);
            foreach (Journal journal in base.DataAccessManager.GetFilteredData<Journal>(filter))
            {
                base.DataAccessManager.Delete<Journal>(journal.JournalID);
            }
        }

        public int InsertJournalInformationForProductionExpense(ProductionUnitExpense expense)
        {
            int result = 0;
            int referenceID = 1;
            referenceID = new JournalAccountsManager().GetJournalReferenceID();

            for (int i = 0; i <= 1; i++)
            {
                IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();
                if (i == 0)
                {
                    journal.DrCrIndecator = "Dr";
                    journal.AccountID = IFMSConstant.OtherProductionCost;

                }
                else
                {
                    journal.DrCrIndecator = "Cr";
                    journal.AccountID = IFMSConstant.CashAccountID;
                }


                journal.Amount = expense.Amount;
                journal.ReferenceNo = referenceID;
                journal.ProductionCostID = expense.ProductionUnitExpenseID;
                journal.BranchID = expense.BranchID;
                journal.OrganizationID = expense.OrganizationID;
                base.DataAccessManager.Insert<Journal>(journal);
            }

            return result;
        }


        public int InsertProductionUnitExpense(ProductionUnitExpense productionUnitExpense)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            int expenseID = 0;
            try
            {
                base.DataAccessManager.BeginTransaction();

                expenseID = base.DataAccessManager.Insert<ProductionUnitExpense>(productionUnitExpense);
                productionUnitExpense.ProductionUnitExpenseID = expenseID;
                InsertJournalInformationForProductionExpense(productionUnitExpense);
                base.DataAccessManager.CommitTransaction();
            }
            catch
            {
                base.DataAccessManager.Rollback();
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            finally
            {
                base.DataAccessManager.EndTransaction();
            }
            return result;
        }

        public int UpdateProductionUnitExpense(ProductionUnitExpense productionUnitExpense)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            try
            {
                base.DataAccessManager.BeginTransaction();

                base.DataAccessManager.Update<ProductionUnitExpense>(productionUnitExpense);

                DeletePreviousJournal(productionUnitExpense);

                InsertJournalInformationForProductionExpense(productionUnitExpense);

                base.DataAccessManager.CommitTransaction();
            }
            catch
            {
                base.DataAccessManager.Rollback();
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            finally
            {
                base.DataAccessManager.EndTransaction();
            }
            return result;
        }

        public List<ProductionUnitExpense> GetAllProductionUnitExpense()
        {
            return this.DataAccessManager.GetAll<ProductionUnitExpense>().Cast<ProductionUnitExpense>().ToList(); ;
        }

        public ProductionUnitExpense GetProductionUnitExpenseByID(int ProductionUnitExpenseID)
        {
            return this.DataAccessManager.GetByID<ProductionUnitExpense>(ProductionUnitExpenseID, "ProductionUnitExpenseID");
        }

        public List<ProductionUnitExpense> GetFilteredProductionUnitExpense(string filter)
        {
            return base.DataAccessManager.GetFilteredData<ProductionUnitExpense>(filter).Cast<ProductionUnitExpense>().ToList();
        }





    }
}
