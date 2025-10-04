using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;
using System.Collections;
using NybSys.MTBF.Utility.Enums;

namespace IFMS.BLL
{
    public class EmployeeManager : BllBase
    {

        public int InsertEmployee(Employee employee)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                base.DataAccessManager.Insert<Employee>(employee);
            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }

        public int UpdateEmployee(Employee employee)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                base.DataAccessManager.Update<Employee>(employee);
            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }

        public Employee GetEmployeeByID(int employeeID)
        {
            return base.DataAccessManager.GetByID<Employee>(employeeID, "EmployeeID");
        }


        public IList GetAllEmployee()
        {
            return base.DataAccessManager.GetAll<Employee>().Cast<Employee>().ToList();
        }

        public int DeleteEmployee(Employee employee)
        {
            return base.DataAccessManager.Delete<Employee>(employee.EmployeeID);
        }


        #region "Designation"

        public int InsertDesignation(Designation designation)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            try
            {
                base.DataAccessManager.Insert<Designation>(designation);
            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }

            return result;
        }

        public int UpdateDesignation(Designation designation)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            try
            {
                base.DataAccessManager.Update<Designation>(designation);
            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }

            return result;
        }

        public List<Designation> GetAllDesignation()
        {
            return base.DataAccessManager.GetAll<Designation>().Cast<Designation>().ToList();
        }

        public Designation GetDesignationByID(int designationID)
        {
            return base.DataAccessManager.GetByID<Designation>(designationID, "DesignationID");
        }



        #endregion




        public TrialPeriodInformation GetTrialPeriod()
        {
            return base.DataAccessManager.GetAll<TrialPeriodInformation>().Cast<TrialPeriodInformation>().ToList().FirstOrDefault();
        }

        public int InsertTrialPeriod(TrialPeriodInformation trialPeriod)
        {
            return base.DataAccessManager.Insert<TrialPeriodInformation>(trialPeriod);
        }

        public int UpdateTrialPeriod(TrialPeriodInformation trialPeriod)
        {
            return base.DataAccessManager.Update<TrialPeriodInformation>(trialPeriod);
        }

        public List<Employee> GetAllEmployeeByBranchID(int branchID)
        {
            string filter = string.Format("{0}={1}", "BranchID", branchID);
            return base.DataAccessManager.GetFilteredData<Employee>(filter).Cast<Employee>().ToList();
        }
    }
}
