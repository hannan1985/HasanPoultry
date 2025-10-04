using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMFS.Common.View
{
    public class TrialBalance
    {
        public string AccountType { get; set; }

        public int Head { get; set; }

        public string AccountsName { get; set; }

        public string AccountTypeName { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        public decimal Balance { get; set; }

    }
}
