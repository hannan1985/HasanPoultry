using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("BalanceSheetBackup")]
    public class BalanceSheetBackup
    {

        private int _BalanceSheetID = 0;
        [PrimaryKey()]
        [DataType("int")]
        public int BalanceSheetID
        {
            get { return _BalanceSheetID; }
            set { _BalanceSheetID = value; }
        }

        private int _BalanceSheetYear =0;

        [DataType("int")]
        public int FiscalYear
        {
            get { return _BalanceSheetYear; }
            set { _BalanceSheetYear = value; }
        }
        

        private string _AccountType = string.Empty;

        [DataType("varchar")]
        public string AccountType
        {
            get { return _AccountType; }
            set { _AccountType = value; }
        }


        private string _AccountTypeName = string.Empty;

        [DataType("varchar")]
        public string AccountTypeName
        {
            get { return _AccountTypeName; }
            set { _AccountTypeName = value; }
        }

        private string _AccountsName = string.Empty;

        [DataType("varchar")]
        public string AccountsName
        {
            get { return _AccountsName; }
            set { _AccountsName = value; }
        }

        private decimal _Amount = 0;

        [DataType("decimal")]
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

    }
}
