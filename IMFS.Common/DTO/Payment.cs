using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("Payment")]
    public class Payment
    {
        private int _PaymentID = 0;

        [PrimaryKey()]
        [DataType("int")]
        public int PaymentID
        {
            get { return _PaymentID; }
            set { _PaymentID = value; }
        }


        private DateTime _PaymentDate = DateTime.Now;

        [DataType("datetime")]
        public DateTime PaymentDate
        {
            get { return _PaymentDate; }
            set { _PaymentDate = value; }
        }

        private int _CompanyID = 0;

        [DataType("int")]
        public int CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }

        private int _SupplierID = 0;

        [DataType("int")]
        public int SupplierID
        {
            get { return _SupplierID; }
            set { _SupplierID = value; }
        }

        private decimal _Amount = 0;

        [DataType("decimal")]
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        public bool IsAdvance { get; set; }

        public int BranchID { get; set; }

        public int OrganizationID { get; set; }

        public int PaymentMethod { get; set; }

        public decimal CashAmount { get; set; }

        public decimal BankAmount { get; set; }

        public string BankReference { get; set; }

        public int Status { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public int BankAccountID { get; set; }

        public decimal  Discount { get; set; }


        public string BillRefNumber { get; set; }

        public int VoucherNumber { get; set; }
    }
}
