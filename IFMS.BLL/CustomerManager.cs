using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;
using IMFS.Common.Utility;
using IMFS.Common.View;
using NybSys.MTBF.Utility.Enums;
using NybSys.MTBF.Utility.Constants;

namespace IFMS.BLL
{
    public class CustomerManager : BllBase
    {
       

        public System.Collections.IList GetAllCustomer()
        {
            return base.DataAccessManager.GetAll<Customer>();
        }

        public Customer GetCustomerByID(int _CustomerID)
        {
            return base.DataAccessManager.GetByID<Customer>(_CustomerID, "CustomerID");
        }


        public Customer GetCustomerByPhoneNumber(string phoneNumber, int branchID, int organizationID)
        {
            string filter = string.Format("{0}='{1}' and {2}={3} and {4}={5}", "Phone", phoneNumber, "BranchID", branchID, "OrganizationID", organizationID);
            return base.DataAccessManager.GetFilteredData<Customer>(filter).Cast<Customer>().ToList().FirstOrDefault();
        }

        public System.Collections.IList GetCustomerOutstandingInformationByCustomerID(int customerID)
        {
            string filter = string.Format(IFMSConstant.QueryConstants.CustomerDueAmountByCustomerID, customerID);
            return base.DataAccessManager.ExecuteSQL<CustomerOurstanding>(filter).Cast<CustomerOurstanding>().ToList();
        }

        public List<AllCustomerOutstanding> GetAllCustomerOutstandingByDate(string _fromDate, string _toDate, int branchID, int organizationID)
        {
            string filter = string.Format(IFMSConstant.QueryConstants.AllCustomerOutstandingByDate, _fromDate, _toDate, branchID, organizationID);
            return base.DataAccessManager.ExecuteSQL<AllCustomerOutstanding>(filter).Cast<AllCustomerOutstanding>().ToList();

        }


        #region "Zone"


        public int InsertZone(Zone zone)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                base.DataAccessManager.Insert<Zone>(zone);
            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }

        public int UpdateZone(Zone zone)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                base.DataAccessManager.Update<Zone>(zone);
            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }

        public List<Zone> GetAllZone()
        {
            return base.DataAccessManager.GetAll<Zone>().Cast<Zone>().ToList();
        }

        public Zone GetZoneByID(int zoneID)
        {
            return base.DataAccessManager.GetByID<Zone>(zoneID, "ZoneID");
        }

        #endregion

   
        public List<Customer> GetFilteredCustomer(string fitler)
        {
            return base.DataAccessManager.GetFilteredData<Customer>(fitler).Cast<Customer>().ToList();
        }

        public List<Customer> GetAllCustomerByBranchID(int branchID)
        {
            string filter = string.Format("{0}={1}", "BranchID", branchID);
            return base.DataAccessManager.GetFilteredData<Customer>(filter).Cast<Customer>().ToList();
        }

        public int CopyAllCustomer(List<Customer> lstCustomer, int branchID)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            try
            {
                base.DataAccessManager.BeginTransaction();

                foreach (Customer customer in lstCustomer)
                {
                    customer.BranchID = branchID;
                    customer.CustomerID = base.DataAccessManager.Insert<Customer>(customer);
                    InsertCustomerChildAccount(customer);
                }


                base.DataAccessManager.CommitTransaction();

            }
            catch (Exception)
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

        private void InsertCustomerChildAccount(Customer customer)
        {
            ChildAccount childAccount = new ChildAccount();
            childAccount.AccountID = IFMSConstant.AccountReceivableID;
            childAccount.Description = customer.CustomerName;
            childAccount.ReferenceType = (int)MTBFEnums.ChildAccountType.Customer;
            childAccount.ReferenceID = customer.CustomerID;
            childAccount.CreatedBy = MTBFConstants.AppConstants.LoggedinUser.UserID;
            childAccount.CreatedDate = DateTime.Now;
            childAccount.Status = 1;
            base.DataAccessManager.Insert<ChildAccount>(childAccount);
        }

        public int SaveCustomerInformation(Customer customer)
        {
           

            try
            {
                base.DataAccessManager.BeginTransaction();

                if (customer.CustomerID > 0)
                {
                    
                    customer.UpdatedBy = MTBFConstants.AppConstants.LoggedinUserID;
                    customer.UpdatedDate = DateTime.Now;
                    base.DataAccessManager.Update<Customer>(customer);

                    string filter = string.Format("{0}='{1}' and {2}={3}", "ReferenceType", (int)MTBFEnums.ChildAccountType.Customer, "ReferenceID", customer.CustomerID);
                    ChildAccount childAccount = base.DataAccessManager.GetFilteredData<ChildAccount>(filter).Cast<ChildAccount>().ToList().FirstOrDefault();
                    if (childAccount != null)
                    {
                        childAccount.Description = customer.CustomerName;
                        base.DataAccessManager.Update<ChildAccount>(childAccount);
                    }
                    else
                    {
                        childAccount = new ChildAccount();
                        childAccount.AccountID = IFMSConstant.AccountReceivableID;
                        childAccount.Description = customer.CustomerName;
                        childAccount.ReferenceType = (int)MTBFEnums.ChildAccountType.Customer;
                        childAccount.ReferenceID = customer.CustomerID;
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
                    customer.CreatedDate = DateTime.Now;
                    customer.CreatedBy = MTBFConstants.AppConstants.LoggedinUserID;
                    customer.UpdatedDate = Convert.ToDateTime(MTBFConstants.DefauldDate);
                    customer.CustomerID = base.DataAccessManager.Insert<Customer>(customer);
                    ChildAccount childAccount = new ChildAccount();
                    childAccount.AccountID = IFMSConstant.AccountReceivableID;
                    childAccount.Description = customer.CustomerName;
                    childAccount.ReferenceType = (int)MTBFEnums.ChildAccountType.Customer;
                    childAccount.ReferenceID = customer.CustomerID;
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
                customer.CustomerID = 0;
            }
            finally
            {
                base.DataAccessManager.EndTransaction();
            }

            return customer.CustomerID;
        }

    }
}
