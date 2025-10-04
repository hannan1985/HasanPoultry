using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Enums;

namespace IFMS.BLL
{
    public class SalesRepresentativeManager : BllBase
    {
        public int SaveSalesRepresentative(SalesRepresentative salesRepresentative)
        {

            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                if (salesRepresentative.SalesRepresentativeID > 0)
                {
                    base.DataAccessManager.Update<SalesRepresentative>(salesRepresentative);
                }
                else
                {
                    base.DataAccessManager.Insert<SalesRepresentative>(salesRepresentative);
                }
            }
            catch (Exception)
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }

            return result;
        }


        public List<SalesRepresentative> GetFilteredSalesRepresentative(string filter)
        {
            return base.DataAccessManager.GetFilteredData<SalesRepresentative>(filter).Cast<SalesRepresentative>().ToList();
        }

    }
}
