using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMFS.Common.View
{
  public   class SupplierStatement
    {
        public string  Description { get; set; }

        public string  SupplierName { get; set; }

        public string CompanyName { get; set; }

        public string  Phone { get; set; }

        public string Address { get; set; }

        public DateTime  Date { get; set; }

        public decimal  DrAmount { get; set; }

        public decimal  CrAmount { get; set; }

        public decimal  Balance { get; set; }

    }
}
