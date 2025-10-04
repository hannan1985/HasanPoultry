using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    public class PartyReceive
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


        private int _PartyID = 0;

        [DataType("int")]
        public int PartyID
        {
            get { return _PartyID; }
            set { _PartyID = value; }
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



        public int BranchID { get; set; }

        public int OrganizationID { get; set; }

    }
}
