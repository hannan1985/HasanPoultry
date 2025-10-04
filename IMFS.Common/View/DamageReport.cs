using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMFS.Common.View
{
    public class DamageReport
    {

        public int DamageInfoID { get; set; }
        public DateTime DamageDate { get; set; }

        public string DamageReason { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }

        public decimal Price { get; set; }
        public decimal Total { get; set; }

        public string ProductCode { get; set; }
    }
}
