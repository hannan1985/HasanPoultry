using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IFMS.DataAccess;

namespace IFMS.BLL
{
   public class BllBase
    {
        private IDataAccessManager _dataAccessmanager = DataAccessFactory.GetDataAccessManager();      
        protected IDataAccessManager DataAccessManager { get { return _dataAccessmanager; } }

    }
}
