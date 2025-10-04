using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    //[Serializable(), TableMap("DistributeDetails")]
     public class DistributeDetails
    {
         private int _DistributeDetailID = 0;
         [PrimaryKey()]
         [DataType("int")]
         public int DistributeDetailID 
        {
            get { return _DistributeDetailID; }
            set { _DistributeDetailID = value; }
        }


        private int _DistributeID = 0;
        //[DataType("int")]
        public int DistributeID
        {
            get { return _DistributeID; }
            set { _DistributeID = value; }
        }

        private int _MaterialID = 0;

        //[DataType("int")]
        public int MaterialID
        {
            get { return _MaterialID; }
            set { _MaterialID = value; }
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

        private decimal _Total = 0;
        //[DataType("decimal")]

        public decimal Total
        {
            get { return _Total; }
            set { _Total = value; }
        }


    }
}
