using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMFS.Common.View
{
    public class SalesReturnReport
    {
        public DateTime ReturnDate { get; set; }


        private int _SalesReturnDetailID;


        public int SalesReturnDetailID
        {
            get { return _SalesReturnDetailID; }
            set { _SalesReturnDetailID = value; }
        }

        private int _SalesReturnID;

        public int SalesReturnID
        {
            get { return _SalesReturnID; }
            set { _SalesReturnID = value; }
        }

        private string _ProductCode;

        public string ProductCode
        {
            get { return _ProductCode; }
            set { _ProductCode = value; }
        }


        private string _ProductName;

        public string ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value; }
        }

        public string  CustomerName { get; set; }

        private decimal _Quantity;

        public decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }

        private decimal _SquareFeet;

        public decimal SquareFeet
        {
            get { return _SquareFeet; }
            set { _SquareFeet = value; }
        }

        public decimal Price { get; set; }

        public decimal  Total { get; set; }

    }
}
