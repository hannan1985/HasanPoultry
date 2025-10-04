using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.View
{
    public class ExpireProduct
    {
        private int _PurchaseID = 0;

        [DataType("int")]
        public int PurchaseID
        {
            get { return _PurchaseID; }
            set { _PurchaseID = value; }
        }
        private string _ProductID = string.Empty;

        [DataType("varchar")]
        public string ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }

        private string _ProductName = string.Empty;

        [DataType("varchar")]
        public string ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value; }
        }

        private string _CompanyName = string.Empty;

        [DataType("varchar")]
        public string CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
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


        private DateTime _ExpireDate = DateTime.Now;

        [DataType("date")]
        public DateTime ExpireDate
        {
            get { return _ExpireDate; }
            set { _ExpireDate = value; }
        }


        private decimal _Total = 0;

        [DataType("decimal")]
        public decimal Total
        {
            get { return _Total; }
            set { _Total = value; }
        }



    }
}
