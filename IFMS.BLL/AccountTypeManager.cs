using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;

namespace IFMS.BLL
{
   public class AccountTypeManager:BllBase 
    {

       public int InsertAccountType(AccountType accountType)
        {
            try
            {
                int result = base.DataAccessManager.Insert<AccountType>(accountType);
                return result;
            }
            catch
            {
                throw;
            }
        }

       public int UpdateAccountType(AccountType accountType)
        {
            try
            {
                int result = base.DataAccessManager.Update<AccountType>(accountType);
                return result;
            }
            catch
            {
                throw;
            }
        }


       public AccountType GetAccountTypeByID(int accountTypeID)
       {
           return base.DataAccessManager.GetByID<AccountType>(accountTypeID, "AccountTypeID");
       }
    }
}
