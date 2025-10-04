using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    public class ProductUsedDetail
    {
        [PrimaryKey()]
        public int ProductUsedDetailID { get; set; }

        public int ProductUsedID { get; set; }

        public string ProductID { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Total { get; set; }
    }
}
