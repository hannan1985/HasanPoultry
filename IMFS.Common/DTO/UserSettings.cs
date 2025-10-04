using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("UserSettings")]
    public class UserSettings
    {
        private int _UserSettingID = 0;
        [PrimaryKey()]
        [DataType("int")]
        public int UserSettingID
        {
            get { return _UserSettingID; }
            set { _UserSettingID = value; }
        }

        private int _EmployeeID = 0;

        [DataType("int")]
        public int EmployeeID
        {
            get { return _EmployeeID; }
            set { _EmployeeID = value; }
        }
        private int _SalesTransactionLevel = 0;

        [DataType("int")]
        public int SalesTransactionLevel
        {
            get { return _SalesTransactionLevel; }
            set { _SalesTransactionLevel = value; }
        }

        private int _SalesAccountID = 0;

        [DataType("int")]
        public int SalesAccountID
        {
            get { return _SalesAccountID; }
            set { _SalesAccountID = value; }
        }

        private int _PurchaseTransactionLevel = 0;

        [DataType("int")]
        public int PurchaseTransactionLevel
        {
            get { return _PurchaseTransactionLevel; }
            set { _PurchaseTransactionLevel = value; }
        }

        private int _PurchaseAccountID = 0;

        [DataType("int")]
        public int PurchaseAccountID
        {
            get { return _PurchaseAccountID; }
            set { _PurchaseAccountID = value; }
        }

    }
}
