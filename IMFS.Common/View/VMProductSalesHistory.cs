using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMFS.Common.View
{
    public class VMProductSalesHistory
    {
        public int SalesID { get; set; }

        public string BillNumber { get; set; }

        public DateTime SalesDate { get; set; }

        public string CustomerName { get; set; }

        public string ProductName { get; set; }

        public string ProductID { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public int CustomerID { get; set; }
    }
}
