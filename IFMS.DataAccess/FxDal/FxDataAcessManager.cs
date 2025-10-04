using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using FrameworkX.ObjectSpace;
using NybSys.MTBF.Utility.Encryption;


namespace IFMS.DataAccess
{
    public class FxDataAcessManager : IDataAccessManager
    {
        private string DBConnectionString;
        public System.Data.IDbTransaction transaction = null;
        MTBFEncryption objCrypto = new MTBFEncryption();
        public FxDataAcessManager()
        {
            AppSettingsReader objCnf = new AppSettingsReader();
            string strOutput = String.Empty;
            strOutput = objCnf.GetValue("ConnectionString", strOutput.GetType()).ToString();
            try
            {
                strOutput = objCrypto.Decrypt(strOutput);
            }
            catch (Exception)
            {
            }


            if (string.IsNullOrEmpty(strOutput))
            {
                throw new Exception("Connection string not found in application configuration file.");
            }
            else
            {
                DBConnectionString = strOutput;
            }
        }

        #region "Codes for for Calling Database

        public sealed class GetDB
        {
            public static Database GetFrameworkDB(string Connstring)
            {
                FrameworkX.ObjectSpace.Data.Providers.SqlServer.SqlServerProvider SqlProvider = new FrameworkX.ObjectSpace.Data.Providers.SqlServer.SqlServerProvider();
                Configuration Conf = new Configuration(SqlProvider, Connstring);
                Database db = new Database(Conf, true);
                return db;
            }
        }

        #endregion

        #region "Transaction"

        /// <summary>
        /// Method to start a transaction scop
        /// </summary>
        /// <remarks></remarks>
        public void BeginTransaction()
        {
            this.transaction = GetDB.GetFrameworkDB(DBConnectionString).BeginTransaction(IsolationLevel.ReadUncommitted);
        }

        /// <summary>
        /// Method to rollback a transaction.
        /// </summary>
        /// <remarks></remarks>
        public void Rollback()
        {
            if ((transaction != null))
            {
                transaction.Rollback();
            }
        }

        /// <summary>
        /// Method to commit transaction.
        /// </summary>
        /// <remarks></remarks>
        public void CommitTransaction()
        {
            if ((transaction != null))
            {
                transaction.Commit();
            }
        }

        /// <summary>
        /// Method to end transaction.
        /// </summary>
        /// <remarks></remarks>
        public void EndTransaction()
        {
            if ((transaction != null))
            {
                GetDB.GetFrameworkDB(DBConnectionString).EndTransaction();
            }
        }

        #endregion

        #region "Generic View filter"

        public IList GetFilteredDataFromView<T>(string sqlFilter)
        {
            Database db = GetDB.GetFrameworkDB(DBConnectionString);
            IList lst = (IList)db.SelectBy(typeof(T), sqlFilter, "");
            return lst;
        }

        public IList GetAllDataFromView<T>()
        {
            Database db = GetDB.GetFrameworkDB(DBConnectionString);
            return db.SelectAll(typeof(T));
        }



        #endregion

        #region "Scaler"

        //public T ExecuteScalar<T>(string sql)
        //{
        //    Database db = GetDB.GetFrameworkDB(DBConnectionString);
        //    object result = db.ExecuteScalar(sql);
        //    return (T)result;
        //}

        #endregion

        #region "Generic"

        public int Insert<T>(T obj)
        {
            try
            {
                Database db = GetDB.GetFrameworkDB(DBConnectionString);
                int result = db.Insert(obj);
                return result;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public int Update<T>(T obj)
        {
            try
            {
                Database db = GetDB.GetFrameworkDB(DBConnectionString);
                int result = db.Update(obj);
                return result;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public int Delete<T>(object primary)
        {
            try
            {
                Database db = GetDB.GetFrameworkDB(DBConnectionString);
                var result = db.Delete(typeof(T), primary);
                return 1;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public IList GetAll<T>()
        {
            Database db = GetDB.GetFrameworkDB(DBConnectionString);
            return db.SelectAll(typeof(T));
        }

        public IList GetFilteredData<T>(string sqlFilter)
        {
            Database db = GetDB.GetFrameworkDB(DBConnectionString);
            IList lst = (IList)db.SelectBy(typeof(T), sqlFilter, "");
            return lst;
        }

        public T GetByID<T>(int ID, string columName)
        {
            object obj = null;
            string filter = string.Format("{0} = {1} ", columName, ID);
            IList objs = GetFilteredData<T>(filter);
            if ((objs.Count > 0))
            {
                obj = objs[0];
            }
            return (T)obj;
        }

        public T GetByCode<T>(string code, string columName)
        {

            object obj = null;
            string filter = string.Format("{0} = '{1}' ", columName, code);
            IList objs = GetFilteredData<T>(filter);

            if ((objs.Count > 0))
            {
                obj = objs[0];
            }
            return (T)obj;
        }

        public object ExecuteScalar(string sql)
        {
            object result = null;
            Database db = GetDB.GetFrameworkDB(DBConnectionString);
            result = db.ExecuteScalar(sql);
            if (result == DBNull.Value)
            {
                result = null;
            }
            return result;
        }

        public IList<T> ExecuteSQL<T>(string sql)
        {
            Database db = GetDB.GetFrameworkDB(DBConnectionString);
            return db.ExecuteJoinSQL<T>(sql);
        }

        #endregion
    }
}
