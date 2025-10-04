using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("CashSalesOrderDetails")]
    public class SalesOrderDetail
    {
        private int _SerialNo = 0;

        [PrimaryKey()]
        [DataType("int")]
        public int SerialNo
        {
            get { return _SerialNo; }
            set { _SerialNo = value; }
        }

        private string _ProductID = string.Empty;

        [DataType("varchar")]
        public string ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }

        private int _SalesID = 0;

        [DataType("int")]
        public int SalesID
        {
            get { return _SalesID; }
            set { _SalesID = value; }
        }


        private decimal _Price = 0;

        [DataType("decimal")]
        public decimal Price
        {
            get { return _Price; }
            set { _Price = value; }
        }

        private decimal _Quantity = 0;

        [DataType("int")]
        public decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }

        private int _PurchaseID = 0;

        [DataType("int")]
        public int PurchaseID
        {
            get { return _PurchaseID; }
            set { _PurchaseID = value; }
        }

        private decimal _VAT;

        public decimal VAT
        {
            get { return _VAT; }
            set { _VAT = value; }
        }


        private decimal _SquareFeet;

        public decimal SquareFeet
        {
            get { return _SquareFeet; }
            set { _SquareFeet = value; }
        }

        public string ProductName { get; set; }



        private bool _IsFree = false;

        public bool IsFree
        {
            get { return _IsFree; }
            set { _IsFree = value; }
        }

    }
}
