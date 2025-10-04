using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMFS.Common.View
{
  public   class CashBookReceive
    {
        public DateTime ReceiveDate { get; set; }

        public int  Indicator { get; set; }

        public int  ReferenceNo { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        public DateTime JournalDate { get; set; }

        public int AccountID { get; set; }

        public decimal  Debit { get; set; }

        public decimal  Credit { get; set; }

        public string DrCrIndecator { get; set; }
    }
}
