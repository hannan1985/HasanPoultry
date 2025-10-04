using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Enums;
using IMFS.Common.Utility;
using NybSys.MTBF.Utility.Constants;

namespace IFMS.BLL
{
    public class SalesCenterManager : BllBase
    {
        public int InsertSalesCenter(SalesCenter salesCenter)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            try
            {
                base.DataAccessManager.Insert<SalesCenter>(salesCenter);

            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }


        public int UpdateSalesCenter(SalesCenter salesCenter)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            try
            {
                base.DataAccessManager.Update<SalesCenter>(salesCenter);

            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }


        public int SaveSalesCenterInformation(SalesCenter salesCenter)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            try
            {
                base.DataAccessManager.BeginTransaction();

                if (salesCenter.SalesCenterID > 0)
                {

                    
                    base.DataAccessManager.Update<SalesCenter>(salesCenter);

                    string filter = string.Format("{0}='{1}' and {2}={3}", "ReferenceType", (int)MTBFEnums.ChildAccountType.SalesCenter, "ReferenceID", salesCenter.SalesCenterID);
                    ChildAccount childAccount = base.DataAccessManager.GetFilteredData<ChildAccount>(filter).Cast<ChildAccount>().ToList().FirstOrDefault();
                    if (childAccount != null)
                    {
                        childAccount.Description = salesCenter.SalesCenterName;
                        base.DataAccessManager.Update<ChildAccount>(childAccount);
                    }
                    else
                    {
                        childAccount = new ChildAccount();
                        childAccount.AccountID = IFMSConstant.AccountPayableID;
                        childAccount.Description = salesCenter.SalesCenterName;
                        childAccount.ReferenceType = (int)MTBFEnums.ChildAccountType.SalesCenter;
                        childAccount.ReferenceID = salesCenter.SalesCenterID;
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

                    salesCenter.SalesCenterID = base.DataAccessManager.Insert<SalesCenter>(salesCenter);
                    ChildAccount childAccount = new ChildAccount();
                    childAccount.AccountID = IFMSConstant.AccountPayableID;
                    childAccount.Description = salesCenter.SalesCenterName;
                    childAccount.ReferenceType = (int)MTBFEnums.ChildAccountType.SalesCenter;
                    childAccount.ReferenceID = salesCenter.SalesCenterID;
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



        public SalesCenter GetSalesCenterByID(int _SalesCenterID)
        {
            SalesCenter SalesCenter = base.DataAccessManager.GetByID<SalesCenter>(_SalesCenterID, "SalesCenterID");
            return SalesCenter;
        }

        public List<SalesCenter> GetAllSalesCenter()
        {
            return base.DataAccessManager.GetAll<SalesCenter>().Cast<SalesCenter>().ToList();
        }


        public List<SalesCenter> GetFilteredSalesCenterInformation(string filter)
        {
            return base.DataAccessManager.GetFilteredData<SalesCenter>(filter).Cast<SalesCenter>().ToList();
        }
    }
}
