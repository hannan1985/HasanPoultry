using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.View
{
   public class CashSalesInvoice
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

        private decimal _SalesAmount = 0;

        [DataType("decimal")]
        public decimal SalesAmount
        {
            get { return _SalesAmount; }
            set { _SalesAmount = value; }
        }
             


        private string _ProductName = string.Empty;
        [DataType("varchar")]
        public string ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value; }
        }


        private int _EmployeeId = 0;
        [DataType("int")]
        public int EmployeeId
        {
            get { return _EmployeeId; }
            set { _EmployeeId = value; }
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

        private decimal _Total = 0;

        [DataType("decimal")]
        public decimal Total
        {
            get { return _Total; }
            set { _Total = value; }
        }


        private decimal _Discount = 0;

        [DataType("decimal")]
        public decimal Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
        }

        private decimal _NetTotal = 0;

        [DataType("decimal")]
        public decimal NetTotal
        {
            get { return _NetTotal; }
            set { _NetTotal = value; }
        }            

    }
}
