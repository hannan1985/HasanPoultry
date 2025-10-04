using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IFMS.BLL
{
    public class TransactionManager : BllBase
    {
        /// <summary>
        /// Method to start a transaction scop
        /// </summary>
        /// <remarks></remarks>
        public void BeginTransaction()
        {
            DataAccessManager.BeginTransaction();
        }

        /// <summary>
        /// Method to rollback a transaction.
        /// </summary>
        /// <remarks></remarks>
        public void Rollback()
        {
            DataAccessManager.Rollback();
        }

        /// <summary>
        /// Method to commit transaction.
        /// </summary>
        /// <remarks></remarks>
        public void CommitTransaction()
        {
            DataAccessManager.CommitTransaction();
        }

        /// <summary>
        /// Method to end transaction.
        /// </summary>
        /// <remarks></remarks>
        public void EndTransaction()
        {
            DataAccessManager.EndTransaction();
        }

    }
}
