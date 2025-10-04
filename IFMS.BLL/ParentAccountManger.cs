using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;

namespace IFMS.BLL
{
    public class ParentAccountManger : BllBase
    {

        public int InsertParentAccouont(ParentAccount parentAccount)
        {
            try
            {
                int result = base.DataAccessManager.Insert<ParentAccount>(parentAccount);
                return result;
            }
            catch
            {
                throw;
            }
        }


        public int UpdateParentAccouont(ParentAccount parentAccount)
        {
            try
            {
                int result = base.DataAccessManager.Update<ParentAccount>(parentAccount);
                return result;
            }
            catch
            {
                throw;
            }
        }


        public ParentAccount GetParentAccountByID(int _parentAccountID)
        {
            ParentAccount parentAccount = base.DataAccessManager.GetByID<ParentAccount>(_parentAccountID, "AccountID");
            return parentAccount;
        }

        public System.Collections.IList GetAllParentAccount()
        {
            return base.DataAccessManager.GetAll<ParentAccount>();
        }

        public System.Collections.IList GetAllAccountType()
        {
            return base.DataAccessManager.GetAll<AccountType>();
        }
    }
}
