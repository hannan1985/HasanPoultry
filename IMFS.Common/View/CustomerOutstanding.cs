using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMFS.Common.View
{
    public class CustomerOutstanding
    {
        public DateTime SalesDate { get; set; }

        public string BillNumber { get; set; }

        public string Description { get; set; }

        public decimal Debit { get; set; }

        public decimal Credit { get; set; }

        public decimal  Balance { get; set; }

        public int BranchID { get; set; }

        public int CustomerID { get; set; }

    }
}
