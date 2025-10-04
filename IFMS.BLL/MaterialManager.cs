using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NybSys.MTBF.Utility.Enums;
using IMFS.Common.DTO;

namespace IFMS.BLL
{
    public class MaterialManager : BllBase
    {

        public int InsertMeterials(Materials meterials)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                base.DataAccessManager.Insert<Materials>(meterials);

            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }


        public int UpdateMeterials(Materials meterials)
        {

            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                base.DataAccessManager.Update<Materials>(meterials);

            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }


        public Materials GetMeterialsByID(int _meterialsID)
        {
            Materials Meterials = base.DataAccessManager.GetByID<Materials>(_meterialsID, "MaterialID");
            return Meterials;
        }

        public List<Materials> GetAllMeterials()
        {
            return base.DataAccessManager.GetAll<Materials>().Cast<Materials>().ToList();
        }


        public List<Materials> GetAllMeterialsByOrganizationID(int organizationID)
        {
            string filter = string.Format("{0}={1}", "OrganizationID", organizationID);
            return base.DataAccessManager.GetFilteredData<Materials>(filter).Cast<Materials>().ToList();

        }

        public List<Materials> GetFilteredMaterial(string filter)
        {          
            return base.DataAccessManager.GetFilteredData<Materials>(filter).Cast<Materials>().ToList();
        }

    }
}
