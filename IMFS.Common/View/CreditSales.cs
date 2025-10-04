using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.View
{
    [Serializable(), TableMap("CreditSalesRep")]
    public class CreditSales
    {

        private int _SalesID = 0;
        [DataType("int")]
        public int SalesID
        {
            get { return _SalesID; }
            set { _SalesID = value; }
        }

        private DateTime _SalesDate;

        [DataType("datetime")]
        public DateTime SalesDate
        {
            get { return _SalesDate; }
            set { _SalesDate = value; }
        }

        private string _ProductName = string.Empty;
        [DataType("varchar")]
        public string ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value; }
        }


        private decimal _Price = 0;

        [DataType("decimal")]
        public decimal Price
        {
            get { return _Price; }
            set { _Price = value; }
        }

        private decimal _Quantity = 0;

        [DataType("decimal")]
        public decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }

        private int _CustomerID = 0;
        [DataType("int")]
        public int CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }

        public decimal VAT { get; set; }
        public string SquareFeet { get; set; }
        public string Size { get; set; }
        public decimal  Total { get; set; }
        public decimal  ReceiveAmount { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public decimal  DueAmount { get; set; }

    }
}
