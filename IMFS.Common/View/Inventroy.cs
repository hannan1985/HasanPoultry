using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.View
{
    public class Inventroy
    {

        private string _ProductName = string.Empty;

        [DataType("varchar")]
        public string ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value; }
        }

        public decimal TransferQty { get; set; }
        public decimal UsedQty { get; set; }

        private string _GenericName = string.Empty;

        [DataType("varchar")]
        public string GenericName
        {
            get { return _GenericName; }
            set { _GenericName = value; }
        }

        private int _Quantity = 0;

        [DataType("int")]
        public int Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }

        private decimal _PurchasePrice = 0;

        [DataType("decimal")]
        public decimal PurchasePrice
        {
            get { return _PurchasePrice; }
            set { _PurchasePrice = value; }
        }

        private decimal _Total = 0;

        [DataType("decimal")]
        public decimal Total
        {
            get { return _Total; }
            set { _Total = value; }
        }

        private string _CompanyName = string.Empty;

        [DataType("varchar")]
        public string CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }

        public decimal  CartonSize { get; set; }

        public decimal  Carton { get; set; }

        public string  ProductID { get; set; }

        public string  Size { get; set; }

        public string  ModelName { get; set; }

        public string  ProductType { get; set; }

        public int ProductModelID { get; set; }

        public int ProductTypeID { get; set; }

    }
}
