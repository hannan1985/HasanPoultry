using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMFS.Common.View
{
    public class ProductTypeWiseSales
    {

        public DateTime SalesDate { get; set; }

        public string MemoNumber { get; set; }

        public int CustomerType { get; set; }

        public string CustomerName { get; set; }

        public decimal SalesAmount { get; set; }

        public decimal Discount { get; set; }

        public string TypeName { get; set; }

        public decimal CarryingLoading { get; set; }

        public decimal CuttingCharge { get; set; }

        public decimal OtherCharge { get; set; }
    }
}
