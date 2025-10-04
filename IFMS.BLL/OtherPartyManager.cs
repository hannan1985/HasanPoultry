using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Constants;
using NybSys.MTBF.Utility.Enums;
using IMFS.Common.Utility;

namespace IFMS.BLL
{
    public class OtherPartyManager : BllBase
    {

        public int SaveOtherPartyInformation(OtherParty otherParty)
        {
            try
            {
                base.DataAccessManager.BeginTransaction();

                if (otherParty.OtherPartyID > 0)
                {

                    otherParty.UpdatedBy = MTBFConstants.AppConstants.LoggedinUserID;
                    otherParty.UpdatedDate = DateTime.Now;
                    base.DataAccessManager.Update<OtherParty>(otherParty);

                    string filter = string.Format("{0}='{1}' and {2}={3}", "ReferenceType", (int)MTBFEnums.ChildAccountType.OtherParty, "ReferenceID", otherParty.OtherPartyID);
                    ChildAccount childAccount = base.DataAccessManager.GetFilteredData<ChildAccount>(filter).Cast<ChildAccount>().ToList().FirstOrDefault();
                    if (childAccount != null)
                    {
                        childAccount.Description = otherParty.PartyName;
                        base.DataAccessManager.Update<ChildAccount>(childAccount);
                    }
                    else
                    {
                        childAccount = new ChildAccount();
                        childAccount.AccountID = IFMSConstant.AccountReceivableID;
                        childAccount.Description = otherParty.PartyName;
                        childAccount.ReferenceType = (int)MTBFEnums.ChildAccountType.OtherParty;
                        childAccount.ReferenceID = otherParty.OtherPartyID;
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
                    otherParty.CreatedDate = DateTime.Now;
                    otherParty.CreatedBy = MTBFConstants.AppConstants.LoggedinUserID;
                    otherParty.UpdatedDate = Convert.ToDateTime(MTBFConstants.DefauldDate);
                    otherParty.OtherPartyID = base.DataAccessManager.Insert<OtherParty>(otherParty);
                    ChildAccount childAccount = new ChildAccount();
                    childAccount.AccountID = IFMSConstant.AccountReceivableID;
                    childAccount.Description = otherParty.PartyName;
                    childAccount.ReferenceType = (int)MTBFEnums.ChildAccountType.OtherParty;
                    childAccount.ReferenceID = otherParty.OtherPartyID;
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
                otherParty.OtherPartyID = 0;
            }
            finally
            {
                base.DataAccessManager.EndTransaction();
            }

            return otherParty.OtherPartyID;
        }




        public List<OtherParty> GetFilterdOtherParty(string filter)
        {
            return base.DataAccessManager.GetFilteredData<OtherParty>(filter).Cast<OtherParty>().ToList();
        }
    }
}
