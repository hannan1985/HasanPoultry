using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMFS.Common.View
{
    public class CashReceiveInformation
    {
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public decimal ReceiveAmount { get; set; }
        public decimal Discount { get; set; }
        public string  BillRefNumber { get; set; }
        public DateTime ReceiveDate { get; set; }
        public string  Address { get; set; }
    }
}
