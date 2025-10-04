using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;

namespace IFMS.BLL
{
   public class ItemManager:BllBase 
    {
        public int InsertItem(Items  item)
        {
            try
            {
                int result = base.DataAccessManager.Insert<Items>(item);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public int UpdateItem(Items item)
        {
            try
            {
                int result = base.DataAccessManager.Update<Items>(item);
                return result;
            }
            catch
            {
                throw;
            }
        }


        public System.Collections.IList GetAllItem()
        {
            return base.DataAccessManager.GetAll<Items>();
        }

    }
}
