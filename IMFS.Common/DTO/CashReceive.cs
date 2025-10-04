using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("CashReceive")]
    public class CashReceive
    {
        private int _CashReceiveID = 0;

        [PrimaryKey()]
        [DataType("int")]
        public int CashReceiveID
        {
            get { return _CashReceiveID; }
            set { _CashReceiveID = value; }
        }

        private DateTime _ReceiveDate = DateTime.Now;

        [DataType("datetime")]
        public DateTime ReceiveDate
        {
            get { return _ReceiveDate; }
            set { _ReceiveDate = value; }
        }


        private int _CustomerID = 0;

        [DataType("int")]
        public int CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }

        private string _ReferenceNo = string.Empty;

        [DataType("varchar")]
        public string ReferenceNo
        {
            get { return _ReferenceNo; }
            set { _ReferenceNo = value; }
        }

        private decimal _Amount = 0;

        [DataType("decimal")]
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        private decimal _Discount;

        public decimal Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
        }

        public string BillRefNumber { get; set; }

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
    }
}
