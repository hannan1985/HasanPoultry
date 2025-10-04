using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMFS.Common.View
{
    public class CashPaymentInformation
    {
        public DateTime  PaymentDate { get; set; }
        public string SupplierName { get; set; }
        public string CustomerName { get; set; }
        public decimal PaymentAmount { get; set; }
    }
}
