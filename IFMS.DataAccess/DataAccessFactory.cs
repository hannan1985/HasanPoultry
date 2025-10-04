namespace IFMS.DataAccess
{
    public static class DataAccessFactory
    {
        private static IDataAccessManager _dataAccessManager = null;

        public static IDataAccessManager GetDataAccessManager()
        {
            if (_dataAccessManager == null)
            {
                _dataAccessManager = new FxDataAcessManager();
            }

            return _dataAccessManager;
        }
    }
}
