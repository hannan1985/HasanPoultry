using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;

namespace IFMS.BLL
{
    public class SubCategoryManager : BllBase
    {
        public int InsertSubCategory(SubCategory  subcategory)
        {
            try
            {
                int result = base.DataAccessManager.Insert<SubCategory>(subcategory);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public int UpdateSubCategory(SubCategory subcategory)
        {
            try
            {
                int result = base.DataAccessManager.Update<SubCategory>(subcategory);
                return result;
            }
            catch
            {
                throw;
            }
        }


        public System.Collections.IList GetAllSubCategory()
        {
            return base.DataAccessManager.GetAll<SubCategory>();
        }
    }
}
