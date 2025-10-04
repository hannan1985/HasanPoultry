using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Enums;

namespace IFMS.BLL
{
    public class BranchManager : BllBase
    {
        public int InsertBranch(Branch branch)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                base.DataAccessManager.Insert<Branch>(branch);

            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }


        public int UpdateBranch(Branch branch)
        {

            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                base.DataAccessManager.Update<Branch>(branch);

            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }


        public Branch GetBranchByID(int _branchID)
        {
            Branch Branch = base.DataAccessManager.GetByID<Branch>(_branchID, "BranchID");
            return Branch;
        }

        public List<Branch> GetAllBranch()
        {
            return base.DataAccessManager.GetAll<Branch>().Cast<Branch>().ToList();
        }


        public List<Branch> GetAllBranchByOrganizationID(int organizationID)
        {
            string filter = string.Format("{0}={1}", "OrganizationID", organizationID);
            return base.DataAccessManager.GetFilteredData<Branch>(filter).Cast<Branch>().ToList();

        }
    }
}
