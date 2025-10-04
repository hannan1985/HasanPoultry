using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    //[Serializable(), TableMap("StockDetails")]
    public class StockDetails
    {
        private int _StockDetailID = 0;
        [PrimaryKey()]
        [DataType("int")]
        public int StockDetailID
        {
            get { return _StockDetailID; }
            set { _StockDetailID = value; }
        }


        private int _StockID = 0;
        //[DataType("int")]
        public int StockID
        {
            get { return _StockID; }
            set { _StockID = value; }
        }

        private string _ProductID = string.Empty;

        //[DataType("int")]
        public string ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }

        private decimal _Quantity = 0;

        //[DataType("decimal")]
        public decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }

        private decimal _Price = 0;

        //[DataType("decimal")]
        public decimal Price
        {
            get { return _Price; }
            set { _Price = value; }
        }

        private decimal _SalesPrice = 0;

        //[DataType("decimal")]
        public decimal SalesPrice
        {
            get { return _SalesPrice; }
            set { _SalesPrice = value; }
        }


        private decimal _Total = 0;

        //[DataType("decimal")]
        public decimal Total
        {
            get { return _Total; }
            set { _Total = value; }
        }


    }
}
