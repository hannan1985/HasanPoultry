using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    public class ReceiveProductDetail
    {

        private int _ReceiveProductDetailID;

        [PrimaryKey()]
        public int ReceiveProductDetailID
        {
            get { return _ReceiveProductDetailID; }
            set { _ReceiveProductDetailID = value; }
        }

        private int _ReceiveProductID;

        public int ReceiveProductID
        {
            get { return _ReceiveProductID; }
            set { _ReceiveProductID = value; }
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



    }
}
