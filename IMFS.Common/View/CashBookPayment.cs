using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMFS.Common.View
{
   public  class CashBookPayment
    {
       public DateTime ExpenseDate { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }
    }
}
