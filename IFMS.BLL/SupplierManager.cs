using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;
using IMFS.Common.View;
using NybSys.MTBF.Utility.Constants;
using NybSys.MTBF.Utility.Enums;
using IMFS.Common.Utility;

namespace IFMS.BLL
{
    public class SupplierManager : BllBase
    {
      

        public Supplier GetSupplierByID(int supplierID)
        {
            return base.DataAccessManager.GetByID<Supplier>(supplierID, "SupplierID");
        }


        public System.Collections.IList GetAllSullier()
        {
            return base.DataAccessManager.GetAll<Supplier>();
        }

        public System.Collections.IList GetSupplierByCompanyID(int companyID)
        {
            string filter = string.Format("{0}={1}", "CompanyID", companyID);
            return base.DataAccessManager.GetFilteredData<Supplier>(filter);
        }

        public List<Supplier> GetFilteredSuppler(string filter)
        {
            return base.DataAccessManager.GetFilteredData<Supplier>(filter).Cast<Supplier>().ToList();
        }

        public List<SupplierStatement> GetSupplierStatementSupplierID(int supplierID, string fromDate, string toDate)
        {
            string filter = String.Format(MTBFConstants.QueryConstants.GetSupplierStatement, supplierID, fromDate, toDate);
            return base.DataAccessManager.ExecuteSQL<SupplierStatement>(filter).Cast<SupplierStatement>().ToList();
        }

        public List<Supplier> GetAllSupplierByBranchID(int branchID)
        {
            string filter = string.Format("{0}={1}", "BranchID", branchID);
            return base.DataAccessManager.GetFilteredData<Supplier>(filter).Cast<Supplier>().ToList();
        }


        public int SaveSupplierInformation(Supplier supplier)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            try
            {
                base.DataAccessManager.BeginTransaction();

                if (supplier.SupplierID > 0)
                {
                    
                    supplier.UpdatedBy = MTBFConstants.AppConstants.LoggedinUserID;
                    supplier.UpdatedDate = DateTime.Now;
                    base.DataAccessManager.Update<Supplier>(supplier);

                    string filter = string.Format("{0}='{1}' and {2}={3}", "ReferenceType", (int)MTBFEnums.ChildAccountType.Supplier, "ReferenceID", supplier.SupplierID);
                    ChildAccount childAccount = base.DataAccessManager.GetFilteredData<ChildAccount>(filter).Cast<ChildAccount>().ToList().FirstOrDefault();
                    if (childAccount != null)
                    {
                        childAccount.Description = supplier.SupplierName;
                        base.DataAccessManager.Update<ChildAccount>(childAccount);
                    }
                    else
                    {
                        childAccount = new ChildAccount();
                        childAccount.AccountID = IFMSConstant.AccountPayableID;
                        childAccount.Description = supplier.SupplierName;
                        childAccount.ReferenceType = (int)MTBFEnums.ChildAccountType.Supplier;
                        childAccount.ReferenceID = supplier.SupplierID;
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
                    supplier.CreatedDate = DateTime.Now;
                    supplier.CreatedBy = MTBFConstants.AppConstants.LoggedinUserID;
                    supplier.UpdatedDate = Convert.ToDateTime(MTBFConstants.DefauldDate);
                    supplier.SupplierID = base.DataAccessManager.Insert<Supplier>(supplier);
                    ChildAccount childAccount = new ChildAccount();
                    childAccount.AccountID = IFMSConstant.AccountPayableID;
                    childAccount.Description = supplier.SupplierName;
                    childAccount.ReferenceType = (int)MTBFEnums.ChildAccountType.Supplier;
                    childAccount.ReferenceID = supplier.SupplierID;
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

        public List<AllCustomerOutstanding> GetAllSupplierOutstandingByDate(string _fromDate, string _toDate, int branchID, int organizationID)
        {
            string filter = string.Format(IFMSConstant.QueryConstants.AllSupplierOutstandingByDate, _fromDate, _toDate, branchID, organizationID);
            return base.DataAccessManager.ExecuteSQL<AllCustomerOutstanding>(filter).Cast<AllCustomerOutstanding>().ToList();        }
    }
}
