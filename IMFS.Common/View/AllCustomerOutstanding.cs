using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMFS.Common.View
{
    public class AllCustomerOutstanding
    {
        public int CustomerID { get; set; }

        public string SupplierName { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public decimal DueAmount { get; set; }

    }
}
