using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMFS.Common.View
{
    public class MoneyReceipt
    {
        public int CashReceiveID { get; set; }

        public DateTime ReceiveDate { get; set; }

        public string CustomerName { get; set; }

        public string  MemoNumber { get; set; }

        public decimal Amount { get; set; }

        public string BillRefNumber { get; set; }

        public string ChequeNo { get; set; }

        public string SalesDate { get; set; }

        public string AmountInWord { get; set; }
    }
}
