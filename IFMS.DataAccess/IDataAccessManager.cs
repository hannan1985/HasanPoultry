using System.Collections.Generic;
using System.Collections;

namespace IFMS.DataAccess
{
    public interface IDataAccessManager
    {
        int Insert<T>(T obj);
        int Update<T>(T obj);
        int Delete<T>(object primary);
        IList GetAll<T>();
        IList GetFilteredData<T>(string sqlFilter);
        T GetByID<T>(int ID, string columName);
        T GetByCode<T>(string code, string columName);
        object ExecuteScalar(string sql);
        void BeginTransaction();
        void Rollback();
        void CommitTransaction();
        void EndTransaction();
        IList<T> ExecuteSQL<T>(string sql);
    }
}
