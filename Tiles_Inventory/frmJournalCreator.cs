using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IMFS.Common.DTO;
using IFMS.Facade;
using IMFS.Common.Utility;
using NybSys.MTBF.Utility.Constants;
using NybSys.MTBF.Utility.Helper;

namespace Tiles_Inventory
{
    public partial class frmJournalCreator : BaseForm
    {

        public frmJournalCreator()
        {
            DataAccessProxy = new FacadeManager();
            InitializeComponent();
        }

        private void btnShowReport_Click(object sender, EventArgs e)
        {
            if (cmbReportName.Text == "Sales")
            {
                CreateJournalForSalesInformation();
            }
            else if (cmbReportName.Text == "Purchase")
            {
                CreateJournalForPurchaseInformation();
            }

            else if (cmbReportName.Text == "Cash Receive")
            {
                CreateJournalForCashReceiveInformation();
            }
            else if (cmbReportName.Text == "Cash Payment")
            {
                CreateJournalForPaymentInformation();
            }
            else if (cmbReportName.Text == "Expense")
            {
                CreateJournalForExpensetInformation();
            }
        }

        private void CreateJournalForSalesInformation()
        {
            try
            {
                base.DataAccessProxy.BegainTransaction();
                foreach (SalesOrder salesOrder in DataAccessProxy.GetAllSalesOrder().Where(p => p.BranchID == MTBFConstants.AppConstants.BranchID && p.OrganizationID == MTBFConstants.AppConstants.OrganizationID && p.IsCanceled == false))
                {
                    InsertJournalInformationForCreditSales(salesOrder);
                    InsertJournalInformationForGoodsDelivery(salesOrder);
                    if (salesOrder.ReceiveAmount > 0)
                    {
                        InsertJournalInformationForCashReceive(salesOrder);
                    }
                }
                base.DataAccessProxy.CommitTransaction();
                MessageBoxHelper.ShowInformation("Journal information created successuflly.");
            }
            catch (Exception)
            {
                base.DataAccessProxy.Rollback();
                MessageBoxHelper.ShowInformation("Failed to create journal information for sales.");
            }
            finally
            {
                base.DataAccessProxy.EndTransaction();
            }

        }

        /// <summary>
        /// Method to insert journal information for credit sales.
        /// </summary>
        /// <returns></returns>
        public int InsertJournalInformationForCreditSales(SalesOrder salesOrder)
        {
            int result = 0;
            int referenceID = 1;
            decimal amount = salesOrder.SalesAmount;

            referenceID = DataAccessProxy.GetJournalReferenceID();

            Customer customer = DataAccessProxy.GetCustomerByID(salesOrder.CustomerID);

            for (int i = 0; i <= 1; i++)
            {
                IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();
                ChildAccount childAccount = DataAccessProxy.GetChildAccountByCustomerID(customer.CustomerID);

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

                journal.SalesID = salesOrder.SalesID;
                journal.JAccountID = 0;
                journal.JournalDate = salesOrder.SalesDate;
                journal.Amount = amount;
                journal.ReferenceNo = referenceID;
                journal.BranchID = MTBFConstants.AppConstants.BranchID;
                journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                result = DataAccessProxy.InsertJournal(journal);
            }

            return result;
        }

        /// <summary>
        /// Method to calculate purchase price of product.
        /// </summary>
        /// <returns></returns>
        private decimal CalculatePurchasePrice(SalesOrder salesOrder)
        {
            string productID = string.Empty;

            decimal purchasePrice = 0;
            foreach (SalesOrderDetail sDetail in DataAccessProxy.GetAllSalesDetailBySalesID(salesOrder.SalesID))
            {
                purchasePrice = purchasePrice + (CalculatePriceByProductID(sDetail.ProductID) * sDetail.SquareFeet);
            }

            return purchasePrice;
        }

        private decimal CalculatePriceByProductID(string productID)
        {
            decimal purchasePrice = 0;
            decimal totalSquareFeet = 0;
            decimal totalPrice = 0;
            foreach (PurchaseOrderDetail orderDetail in DataAccessProxy.GetPurchaseOrderDetailByProductID(productID, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID))
            {
                totalSquareFeet = totalSquareFeet + orderDetail.SquareFeet;
                totalPrice = totalPrice + (orderDetail.PurchasePrice * orderDetail.SquareFeet);
            }

            purchasePrice = totalPrice / totalSquareFeet;
            return purchasePrice;

        }

        /// <summary>
        /// Method to insert journal information for credit sales.
        /// </summary>
        /// <returns></returns>
        public int InsertJournalInformationForGoodsDelivery(SalesOrder salesOrder)
        {
            int result = 0;
            int referenceID = 1;
            decimal amount = CalculatePurchasePrice(salesOrder);

            referenceID = DataAccessProxy.GetJournalReferenceID();
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

                journal.SalesID = salesOrder.SalesID;
                journal.JAccountID = 0;
                journal.JournalDate = salesOrder.SalesDate;
                journal.Amount = amount;
                journal.ReferenceNo = referenceID;
                journal.BranchID = MTBFConstants.AppConstants.BranchID;
                journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                result = DataAccessProxy.InsertJournal(journal);
            }

            return result;
        }

        /// <summary>
        /// Method to insert journal information after cash receive
        /// </summary>
        /// <returns></returns>
        public int InsertJournalInformationForCashReceive(SalesOrder salesOrder)
        {
            int result = 0;
            int referenceID = 1;
            decimal amount = salesOrder.ReceiveAmount;
            referenceID = DataAccessProxy.GetJournalReferenceID();
            Customer customer = DataAccessProxy.GetCustomerByID(salesOrder.CustomerID);
            // UserSettings userSetting = DataAccessProxy.GetUserSettingsByEmployeeID(IFMSConstant.LoggedinUserID);

            for (int i = 0; i <= 1; i++)
            {
                IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();
                ChildAccount childAccount = DataAccessProxy.GetChildAccountByCustomerID(customer.CustomerID);

                if (i == 0)
                {
                    journal.DrCrIndecator = "Dr";
                    journal.AccountID = IFMSConstant.CashAccountID;

                }
                else
                {
                    journal.DrCrIndecator = "Cr";
                    journal.AccountID = childAccount.AccountID;
                    journal.ChildAccountID = childAccount.ChildAccountID;
                }

                journal.SalesID = salesOrder.SalesID;
                journal.JAccountID = 0;
                journal.JournalDate = salesOrder.SalesDate;
                journal.Amount = amount;
                journal.ReferenceNo = referenceID;
                journal.BranchID = MTBFConstants.AppConstants.BranchID;
                journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                result = DataAccessProxy.InsertJournal(journal);
            }

            return result;
        }


        private void CreateJournalForPurchaseInformation()
        {
            try
            {
                base.DataAccessProxy.BegainTransaction();
                foreach (PurchaseOrder purchaseOrder in DataAccessProxy.GetAllPurchaseOrder().Where(p => p.BranchID == MTBFConstants.AppConstants.BranchID && p.OrganizationID == MTBFConstants.AppConstants.OrganizationID && p.Status == (int)IFMSEnum.PurchaseOrderStatus.Approved))
                {
                    ChildAccount childAccount = DataAccessProxy.GetChildAccountBySupplierID(purchaseOrder.SupplierID);
                    InsertJournalInformationForCashPayment(purchaseOrder, childAccount);
                    if (purchaseOrder.PaidAmount > 0)
                        InsertJournalInformationForGoodsReceive(purchaseOrder, childAccount);
                }
                base.DataAccessProxy.CommitTransaction();
                MessageBoxHelper.ShowInformation("Journal information created successfully.");
            }
            catch (Exception)
            {
                base.DataAccessProxy.Rollback();
                MessageBoxHelper.ShowInformation("Failed to create journal information for purchase.");
            }
            finally
            {
                base.DataAccessProxy.EndTransaction();
            }


        }

        private void InsertJournalInformationForCashPayment(PurchaseOrder purchaseOrder, ChildAccount supplierChildAccount)
        {
            int referenceID = 1;

            // UserSettings userSetting = DataAccessProxy.GetUserSettingsByEmployeeID(MTBFConstants.AppConstants.LoggedinUser.EmployeeID);

            referenceID = DataAccessProxy.GetJournalReferenceID();

            for (int i = 0; i <= 1; i++)
            {
                IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();

                if (i == 0)
                {
                    journal.DrCrIndecator = "Dr";
                    journal.ChildAccountID = supplierChildAccount.ChildAccountID;
                    journal.AccountID = supplierChildAccount.AccountID;
                }
                else
                {
                    journal.DrCrIndecator = "Cr";
                    journal.AccountID = IFMSConstant.CashAccountID;

                }

                journal.JAccountID = 0;
                journal.Amount = purchaseOrder.PaidAmount;
                journal.ReferenceNo = referenceID;
                journal.JournalDate = purchaseOrder.PurchaseDate;
                journal.PurchaseID = purchaseOrder.PurchaseID;
                journal.BranchID = purchaseOrder.BranchID;
                journal.OrganizationID = purchaseOrder.OrganizationID;
                DataAccessProxy.InsertJournal(journal);
            }
        }

        private void InsertJournalInformationForGoodsReceive(PurchaseOrder purchaseOrder, ChildAccount supplierChildAccount)
        {
            int referenceID = DataAccessProxy.GetJournalReferenceID();

            for (int i = 0; i <= 1; i++)
            {
                Journal journal = new Journal();

                if (i == 0)
                {
                    journal.DrCrIndecator = "Dr";
                    journal.AccountID = IFMSConstant.InventoryAccountID;
                }
                else
                {
                    journal.DrCrIndecator = "Cr";
                    journal.AccountID = supplierChildAccount.AccountID;
                    journal.ChildAccountID = supplierChildAccount.ChildAccountID;
                }

                journal.JAccountID = 0;
                journal.Amount = purchaseOrder.PurchaseAmount;
                journal.ReferenceNo = referenceID;
                journal.JournalDate = purchaseOrder.PurchaseDate;
                journal.PurchaseID = purchaseOrder.PurchaseID;
                journal.BranchID = purchaseOrder.BranchID;
                journal.OrganizationID = purchaseOrder.OrganizationID;
                DataAccessProxy.InsertJournal(journal);
            }
        }

        private void CreateJournalForCashReceiveInformation()
        {
            try
            {
                base.DataAccessProxy.BegainTransaction();
                foreach (CashReceive cashReceive in DataAccessProxy.GetAllCashReceive().Where(p => p.BranchID == MTBFConstants.AppConstants.BranchID && p.OrganizationID == MTBFConstants.AppConstants.OrganizationID))
                {
                    InsertJournalInformationForCashReceive(cashReceive);
                    if (cashReceive.Discount > 0)
                        InsertJournalInformationForDiscountGiven(cashReceive);
                }
                base.DataAccessProxy.CommitTransaction();
                MessageBoxHelper.ShowInformation("Journal Informaition created successfully.");
            }
            catch (Exception)
            {
                base.DataAccessProxy.Rollback();
                MessageBoxHelper.ShowInformation("Failed to create journal Informaition.");
            }
            finally
            {
                base.DataAccessProxy.EndTransaction();
            }
        }

        private void InsertJournalInformationForDiscountGiven(CashReceive cashReceive)
        {

            int referenceID = 1;
            decimal amount = cashReceive.Discount;

            Customer customer = DataAccessProxy.GetCustomerByID(cashReceive.CustomerID);

            referenceID = DataAccessProxy.GetJournalReferenceID();

            for (int i = 0; i <= 1; i++)
            {
                IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();
                ChildAccount childAccount = DataAccessProxy.GetChildAccountByCustomerID(customer.CustomerID);

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

                journal.JAccountID = 0;
                journal.Amount = amount;
                journal.ReferenceNo = referenceID;
                journal.JournalDate = cashReceive.ReceiveDate;
                journal.CashReceiveID = cashReceive.CashReceiveID;
                journal.BranchID = MTBFConstants.AppConstants.BranchID;
                journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                DataAccessProxy.InsertJournal(journal);
            }


        }

        public int InsertJournalInformationForCashReceive(CashReceive cashReceive)
        {
            int result = 0;
            int referenceID = 1;
            decimal amount = cashReceive.Amount;

            Customer customer = DataAccessProxy.GetCustomerByID(cashReceive.CustomerID);

            referenceID = DataAccessProxy.GetJournalReferenceID();

            for (int i = 0; i <= 1; i++)
            {
                IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();
                ChildAccount childAccount = DataAccessProxy.GetChildAccountByCustomerID(customer.CustomerID);

                if (i == 0)
                {
                    journal.DrCrIndecator = "Dr";

                    journal.AccountID = IFMSConstant.CashAccountID;

                }
                else
                {
                    journal.DrCrIndecator = "Cr";
                    journal.AccountID = childAccount.AccountID;
                    journal.ChildAccountID = childAccount.ChildAccountID;
                }

                journal.JAccountID = 0;
                journal.Amount = amount;
                journal.JournalDate = cashReceive.ReceiveDate;
                journal.ReferenceNo = referenceID;
                journal.CashReceiveID = cashReceive.CashReceiveID;
                journal.BranchID = MTBFConstants.AppConstants.BranchID;
                journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                result = DataAccessProxy.InsertJournal(journal);
            }

            return result;
        }

        private void CreateJournalForPaymentInformation()
        {
            try
            {
                base.DataAccessProxy.BegainTransaction();
                foreach (Payment payment in DataAccessProxy.GetAllPayment().Where(p => p.BranchID == MTBFConstants.AppConstants.BranchID && p.OrganizationID == MTBFConstants.AppConstants.OrganizationID))
                {
                    InsertJournalInformationForCashPayment(payment);
                }
                base.DataAccessProxy.CommitTransaction();
                MessageBoxHelper.ShowInformation("Journal Informaition created successfully.");
            }
            catch (Exception)
            {
                base.DataAccessProxy.Rollback();
                MessageBoxHelper.ShowInformation("Failed to create journal Informaition.");
            }
            finally
            {
                base.DataAccessProxy.EndTransaction();
            }
        }



        public int InsertJournalInformationForCashPayment(Payment payment)
        {
            int result = 0;
            int referenceID = 1;
            decimal amount = payment.Amount;

            Supplier supplier = DataAccessProxy.GetSupplierByID(payment.SupplierID);

            referenceID = DataAccessProxy.GetJournalReferenceID();

            for (int i = 0; i <= 1; i++)
            {
                IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();
                ChildAccount childAccount = DataAccessProxy.GetChildAccountBySupplierID(supplier.SupplierID);

                if (i == 0)
                {
                    journal.DrCrIndecator = "Dr";
                    journal.AccountID = childAccount.AccountID;
                    journal.ChildAccountID = childAccount.ChildAccountID;
                }
                else
                {
                    journal.DrCrIndecator = "Cr";
                    journal.AccountID = IFMSConstant.CashAccountID;

                }

                journal.JAccountID = 0;
                journal.Amount = amount;
                journal.JournalDate = payment.PaymentDate;
                journal.ReferenceNo = referenceID;
                journal.PaymentID = payment.PaymentID;
                journal.BranchID = MTBFConstants.AppConstants.BranchID;
                journal.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                result = DataAccessProxy.InsertJournal(journal);
            }

            return result;
        }



        private void CreateJournalForExpensetInformation()
        {
            try
            {
                base.DataAccessProxy.BegainTransaction();
                foreach (Expense expense in DataAccessProxy.GetAllExpense().Where(p => p.BranchID == MTBFConstants.AppConstants.BranchID && p.OrganizationID == MTBFConstants.AppConstants.OrganizationID))
                {
                    InsertJournalInformationForCashReceive(expense);
                }
                base.DataAccessProxy.CommitTransaction();
                MessageBoxHelper.ShowInformation("Journal Informaition created successfully.");
            }
            catch (Exception)
            {
                base.DataAccessProxy.Rollback();
                MessageBoxHelper.ShowInformation("Failed to create journal Informaition.");
            }
            finally
            {
                base.DataAccessProxy.EndTransaction();
            }
        }

        public int InsertJournalInformationForCashReceive(Expense expense)
        {
            int result = 0;
            int referenceID = 1;
            referenceID = DataAccessProxy.GetJournalReferenceID();

            for (int i = 0; i <= 1; i++)
            {
                IMFS.Common.DTO.Journal journal = new IMFS.Common.DTO.Journal();
                if (i == 0)
                {
                    journal.DrCrIndecator = "Dr";
                    journal.AccountID = IFMSConstant.OperatingExpense;

                }
                else
                {
                    journal.DrCrIndecator = "Cr";
                    journal.AccountID = IFMSConstant.CashAccountID;
                }


                journal.Amount = expense.Amount;
                journal.ReferenceNo = referenceID;
                journal.ExpenseID = expense.ExpenseID;
                journal.JournalDate = expense.ExpenseDate;
                journal.BranchID = expense.BranchID;
                journal.OrganizationID = expense.OrganizationID;
                DataAccessProxy.InsertJournal(journal);
            }

            return result;
        }


        private void frmJournalCreator_Load(object sender, EventArgs e)
        {
            //MTBFConstants.AppConstants.BranchID = 1; ;
            //MTBFConstants.AppConstants.OrganizationID = 1;
        }





    }
}
