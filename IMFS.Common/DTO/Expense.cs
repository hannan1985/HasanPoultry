using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("Expense")]
    public class Expense
    {

        private int _ExpenseID = 0;
        [PrimaryKey()]
        [DataType("int")]
        public int ExpenseID
        {
            get { return _ExpenseID; }
            set { _ExpenseID = value; }
        }

        private DateTime _ExpenseDate = DateTime.Now;

        [DataType("datetime")]
        public DateTime ExpenseDate
        {
            get { return _ExpenseDate; }
            set { _ExpenseDate = value; }
        }

        private int _EmployeeID = 0;

        [DataType("int")]
        public int EmployeeID
        {
            get { return _EmployeeID; }
            set { _EmployeeID = value; }
        }

        public int ExpenseType { get; set; }

        public int PaymentMethod { get; set; }

        public int CashAccountID { get; set; }

        public decimal CashAmount { get; set; }

        public int BankAccountID { get; set; }

        public decimal BankAmount { get; set; }

        public string BankReference { get; set; }

        private decimal _Amount = 0;

        [DataType("decimal")]
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        public int BranchID { get; set; }

        public int OrganizationID { get; set; }

        public string Description { get; set; }

        public DateTime ApprovedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
