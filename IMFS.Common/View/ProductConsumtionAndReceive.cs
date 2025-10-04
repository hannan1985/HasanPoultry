using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMFS.Common.View
{
  public  class ProductConsumtionAndReceive
    {
        public string  ProductID { get; set; }

        public string  ProductName { get; set; }

        public decimal UsedQty { get; set; }

        public decimal  ReceivedQty { get; set; }
    }
}
