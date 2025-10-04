using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    public class BankAccount
    {
        private int _BankAccountID = 0;

        [PrimaryKey()]
        [DataType("int")]
        public int BankAccountID
        {
            get { return _BankAccountID; }
            set { _BankAccountID = value; }
        }

        private string _BankAccountNo = string.Empty;

        [DataType("nvarchar")]
        public string BankAccountNumber
        {
            get { return _BankAccountNo; }
            set { _BankAccountNo = value; }
        }

        private DateTime _OpeningDate;

        [DataType("date")]
        public DateTime OpeningDate
        {
            get { return _OpeningDate; }
            set { _OpeningDate = value; }
        }

        private decimal _OpeningBalance = 0;

        [DataType("numeric")]
        public decimal OpeningBalance
        {
            get { return _OpeningBalance; }
            set { _OpeningBalance = value; }
        }

        private int _Status = 0;

        [DataType("int")]
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public string Address { get; set; }

        public string ManagerName { get; set; }

        public string BankName { get; set; }

        public string AccountType { get; set; }

        public string  ResponsiblePerson { get; set; }

        public string  Branch { get; set; }

        public int BranchID { get; set; }

        public int OrganizationID { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }


    }
}
