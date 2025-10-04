using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.View
{

    public class CreditInvoice
    {

        private int _SalesID = 0;
        [DataType("int")]
        public int SalesID
        {
            get { return _SalesID; }
            set { _SalesID = value; }
        }

        public string ShortNote { get; set; }

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

        private decimal _ReceiveAmount = 0;

        [DataType("decimal")]
        public decimal ReceiveAmount
        {
            get { return _ReceiveAmount; }
            set { _ReceiveAmount = value; }
        }

        private decimal _DueAmount = 0;

        [DataType("decimal")]
        public decimal DueAmount
        {
            get { return _DueAmount; }
            set { _DueAmount = value; }
        }

        private string _CustomerName = string.Empty;
        [DataType("varchar")]
        public string CustomerName
        {
            get { return _CustomerName; }
            set { _CustomerName = value; }
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


        private string _Address = string.Empty;
        [DataType("varchar")]
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        private string _Phone = string.Empty;
        [DataType("varchar")]
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
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


        private decimal _CartonSize = 0;

        [DataType("decimal")]
        public decimal CartonSize
        {
            get { return _CartonSize; }
            set { _CartonSize =value; }
        }


        private decimal _Discount = 0;

        [DataType("decimal")]
        public decimal Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
        }

        public decimal SquareFeet { get; set; }

        private string _Size;

        public string Size
        {
            get { return _Size; }
            set { _Size = value; }
        }

        public decimal CarryingLoading { get; set; }

        public decimal GrandTotal { get; set; }

        public int CustomerID { get; set; }

        public string Model { get; set; }

        public string TypeName { get; set; }

        public int SL { get; set; }

        public string  Unit { get; set; }

        public string  BillNumber { get; set; }

        public string  PackSize { get; set; }

        public string  EmployeeName { get; set; }

        public string  Proprietor { get; set; }

    }
}
