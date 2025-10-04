using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Enums;

namespace IFMS.BLL
{
    public class EmployeeTypeManager : BllBase
    {

        public int SaveEmployeeType(EmployeeType employeeType)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            try
            {
                if (employeeType.EmployeeTypeID > 0)
                {
                    base.DataAccessManager.Update<EmployeeType>(employeeType);
                }
                else
                {
                    base.DataAccessManager.Insert<EmployeeType>(employeeType);
                }

            }
            catch (Exception)
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }


            return result;
        }

        public List<EmployeeType> GetAllEmployeeType()
        {
            return base.DataAccessManager.GetAll<EmployeeType>().Cast<EmployeeType>().ToList();
        }

    }
}
