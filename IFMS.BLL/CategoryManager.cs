using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Enums;

namespace IFMS.BLL
{
    public class CategoryManager : BllBase
    {
        public int InsertCategory(Category category)
        {
            try
            {
                int result = base.DataAccessManager.Insert<Category>(category);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public int UpdateCategory(Category category)
        {
            try
            {
                int result = base.DataAccessManager.Update<Category>(category);
                return result;
            }
            catch
            {
                throw;
            }
        }


        public System.Collections.IList GetAllCategory()
        {
            return base.DataAccessManager.GetAll<Category>();
        }


        public int SaveSupCategory(SubCategory subCategory)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                if (subCategory.SubCategoryID > 0)
                {
                    base.DataAccessManager.Update<SubCategory>(subCategory);
                }
                else
                {
                    base.DataAccessManager.Insert<SubCategory>(subCategory);
                }
            }
            catch (Exception)
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }

            return result;
        }


        public List<SubCategory> GetAllSubCategory()
        {
            return base.DataAccessManager.GetAll<SubCategory>().Cast<SubCategory>().ToList();
        }


        public List<SubCategory> GetFilteredSubCategory(string filter)
        {
            return base.DataAccessManager.GetFilteredData<SubCategory>(filter).Cast<SubCategory>().ToList();
        }

    }
}
