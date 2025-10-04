using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    public class PartyPayment
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


        public int BranchID { get; set; }

        public int OrganizationID { get; set; }


    }
}
