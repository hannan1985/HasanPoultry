using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;

namespace IFMS.BLL
{
    public class AccountHeadManager : BllBase
    {

        public int InsertAccountHead(AccountHead accountHead)
        {
            try
            {
                int result = base.DataAccessManager.Insert<AccountHead>(accountHead);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public int UpdateAccountHead(AccountHead accountHead)
        {
            try
            {
                int result = base.DataAccessManager.Update<AccountHead>(accountHead);
                return result;
            }
            catch
            {
                throw;
            }
        }


        public System.Collections.IList GetAllAccountHead()
        {
            return base.DataAccessManager.GetAll<AccountHead>();
        }
    }
}
