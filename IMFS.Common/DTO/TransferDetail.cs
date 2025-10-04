using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    public class TransferDetail
    {
        private int _TransferDetailID;

        [PrimaryKey()]
        public int TransferDetailID
        {
            get { return _TransferDetailID; }
            set { _TransferDetailID = value; }
        }

        private int _TransferID;

        public int TransferID
        {
            get { return _TransferID; }
            set { _TransferID = value; }
        }

        private string _ProductCode;

        public string ProductCode
        {
            get { return _ProductCode; }
            set { _ProductCode = value; }
        }


        private string  _ProductName;

        public string  ProductName
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

        private decimal _Price;

        public decimal Price
        {
            get { return _Price; }
            set { _Price = value; }
        }




    }
}
