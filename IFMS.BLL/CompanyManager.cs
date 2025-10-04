using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;

namespace IFMS.BLL
{
    public class CompanyManager:BllBase 
    {
        public int InsertCompany(Company company)
        {
            try
            {
                int result = base.DataAccessManager.Insert<Company>(company);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public int UpdateCompany(Company company)
        {
            try
            {
                int result = base.DataAccessManager.Update<Company>(company);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public System.Collections.IList GetAllCompany()
        {
            return base.DataAccessManager.GetAll<Company>();
        }

        public Company GetCompanyByID(int _CompanyID)
        {
            return base.DataAccessManager.GetByID<Company>(_CompanyID, "CompanyID");
        }

        public List<Company> GetFilteredCompany(string filter)
        {
            return base.DataAccessManager.GetFilteredData<Company>(filter).Cast<Company>().ToList();
        }
    }
}
