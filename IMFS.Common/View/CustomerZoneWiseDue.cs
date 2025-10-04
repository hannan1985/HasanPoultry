using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMFS.Common.View
{
    public class CustomerZoneWiseDue
    {
        public int CustomerID { get; set; }

        public string OrganizationName { get; set; }

        public string ZoneName { get; set; }

        public int ZoneID { get; set; }

        public string Proprietor { get; set; }

        public decimal SalesAmount { get; set; }

        public decimal ReceiveAmount { get; set; }

        public decimal PresentDue { get; set; }

        public decimal ReturnAmount { get; set; }

        public decimal PreviousDue { get; set; }

        public decimal ActualDue { get; set; }

        public string  Address { get; set; }
    }
}
