using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.View
{
    public class SalesAndPurchaseReport
    {
        private int _PurchaseID = 0;

        [DataType("int")]
        public int PurchaseID
        {
            get { return _PurchaseID; }
            set { _PurchaseID = value; }
        }

        private int _SalesID = 0;

        [DataType("int")]
        public int SalesID
        {
            get { return _SalesID; }
            set { _SalesID = value; }
        }

        public int SalesRepresentativeID { get; set; }

        private DateTime _PurchaseDate;

        [DataType("datetime")]
        public DateTime PurchaseDate
        {
            get { return _PurchaseDate; }
            set { _PurchaseDate = value; }
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

        private int _Quantity = 0;

        [DataType("int")]
        public int Quantity
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

        public int EmployeeID { get; set; }

        public decimal  SalesAmount { get; set; }

        public decimal  Discount { get; set; }
       
        public decimal  ReceiveAmount { get; set; }

        public int CustomerID { get; set; }

        public string  Address { get; set; }

        public decimal  CarryingLoading { get; set; }

        public string ZoneName { get; set; }

        public string CustomerName { get; set; }

        public int ZoneID { get; set; }

        public string InvoiceNumber { get; set; }

        public string  CompanyName { get; set; }

        public decimal SquareFeet { get; set; }

        public string  BillNumber { get; set; }

        public string  RepresentativeName { get; set; }

        public string SupplierName { get; set; }

      

    }
}
