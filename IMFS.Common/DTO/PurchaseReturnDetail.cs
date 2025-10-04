using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    public class PurchaseReturnDetail
    {
        [PrimaryKey()]
        public int PurchaseReturnDetailID { get; set; }

        public int PurchaseReturnID { get; set; }

        public string ProductID { get; set; }

        public string ProductName { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
