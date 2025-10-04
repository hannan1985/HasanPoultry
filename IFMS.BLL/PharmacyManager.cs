using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;

namespace IFMS.BLL
{
    public class PharmacyManager : BllBase
    {
        public int InsertOrganization(Organization pharmacy)
        {
            try
            {
                int result = base.DataAccessManager.Insert<Organization>(pharmacy);
                return result;
            }
            catch
            {
                throw;
            }
        }


        public int UpdateOrganization(Organization pharmacy)
        {
            try
            {
                int result = base.DataAccessManager.Update<Organization>(pharmacy);
                return result;
            }
            catch
            {
                throw;
            }
        }


        public Organization GetOrganizationByID(int _pharmacyID)
        {
            Organization pharmacy = base.DataAccessManager.GetByID<Organization>(_pharmacyID, "OrganizationID");
            return pharmacy;
        }

        public List<Organization> GetAllPharmacy()
        {
            return base.DataAccessManager.GetAll<Organization>().Cast<Organization>().ToList();
        }

    }
}
