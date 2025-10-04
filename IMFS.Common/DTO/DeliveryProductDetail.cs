using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    //[Serializable, TableMap("DeliveryProductDetail")]
    public class DeliveryProductDetail 
    {
        private int _DeliveryProductDetailID = 0;

        [PrimaryKey()]
        [DataType("int")]
        public int DeliveryProductDetailID
        {
            get { return _DeliveryProductDetailID; }
            set { _DeliveryProductDetailID = value; }
        }
        
        private int _DeliveryProductID = 0;

        [DataType("int")]
        public int DeliveryProductID
        {
            get { return _DeliveryProductID; }
            set { _DeliveryProductID = value; }
        }


        private string _ProductName = string.Empty;

        [DataType("varchar")]
        public string ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value; }
        }

      
        private string _ProductID = string.Empty;

        [DataType("varchar")]
        public string ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }
        
        private decimal _Quantity = 0;

        [DataType("numeric")]
        public decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }


        public decimal  SalesQuantity { get; set; }

        public decimal  DeliveredQuantity { get; set; }

        public decimal  Due { get; set; }

    }
}
