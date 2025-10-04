using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("BalanceSheet")]
    public class BalanceSheet
    {


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

        public int OrganizationID { get; set; }

        public int BranchID { get; set; }
    }
}
