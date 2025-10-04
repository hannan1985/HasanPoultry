using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Enums;
using NybSys.MTBF.Utility.Constants;

namespace IFMS.BLL
{
    public class SalaryManager:BllBase
    {

        public int SaveSalary(List<Salary> lstSalary)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            try
            {
                base.DataAccessManager.BeginTransaction();

                foreach (Salary salary in lstSalary)
                {
                    if (salary.SalaryID > 0)
                    {
                        salary.UpdatedBy = MTBFConstants.AppConstants.LoggedinUserID;
                        salary.UpdatedDate = DateTime.Now;
                        base.DataAccessManager.Update<Salary>(salary);
                    }
                    else
                    {
                        salary.CreatedBy = MTBFConstants.AppConstants.LoggedinUserID;
                        salary.CreatedDate = DateTime.Now;
                        salary.UpdatedDate = Convert.ToDateTime(MTBFConstants.DefauldDate);
                        salary.SalaryDate = DateTime.Now;
                        base.DataAccessManager.Insert<Salary>(salary);
                    }
                }

                List<Salary> lstExistingSalary = new List<Salary>();
                string filter = string.Format("{0}={1} and {2}={3}", "Year", lstSalary[0].Year, "Month", lstSalary[0].Month);

                lstExistingSalary = GetFilteredSalary(filter);
                foreach (Salary salary in lstExistingSalary)
                {
                    Salary deletedSalary = lstSalary.Where(s => s.EmployeeID == salary.EmployeeID).FirstOrDefault();
                    if (deletedSalary == null)
                    {
                        base.DataAccessManager.Delete<Salary>(salary.SalaryID);
                    }
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


        public List<Salary> GetFilteredSalary(string filter)
        {
            return base.DataAccessManager.GetFilteredData<Salary>(filter).Cast<Salary>().ToList();

        }

    }
}
