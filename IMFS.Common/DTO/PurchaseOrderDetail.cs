using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("PurchaseOrderDetails")]
    public class PurchaseOrderDetail
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

        private int _PurchaseID = 0;

        [DataType("int")]
        public int PurchaseID
        {
            get { return _PurchaseID; }
            set { _PurchaseID = value; }
        }


        private DateTime _ExpireDate = DateTime.Now;

        [DataType("date")]
        public DateTime ExpireDate
        {
            get { return _ExpireDate; }
            set { _ExpireDate = value; }
        }

        private decimal _PurchasePrice = 0;

        [DataType("decimal")]
        public decimal PurchasePrice
        {
            get { return _PurchasePrice; }
            set { _PurchasePrice = value; }
        }

        private decimal _Quantity = 0;

        [DataType("decimal")]
        public decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }


        private decimal _SalesPrice = 0;

        [DataType("decimal")]
        public decimal SalesPrice
        {
            get { return _SalesPrice; }
            set { _SalesPrice = value; }
        }

        public decimal  Carton { get; set; }

        public string Barcode { get; set; }

        public decimal SquareFeet { get; set; }

        public string  ProductName { get; set; }

        public decimal Total { get; set; }

        public bool IsFree { get; set; }
    }
}
