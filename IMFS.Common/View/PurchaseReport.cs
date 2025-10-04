using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMFS.Common.View
{
   public  class PurchaseReport
    {

        public int PurchaseID { get; set; }

        public string InvoiceNumber { get; set; }

        public string  ProductName { get; set; }

        public DateTime PurchaseDate { get; set; }

        public decimal  Total { get; set; }

        public string CartonSize { get; set; }

        public decimal  Quantity { get; set; }

        public decimal  Carton { get; set; }

        public decimal  PurchasePrice { get; set; }

        public string  Unit { get; set; }

        public string  Model { get; set; }

        public decimal  PaidAmount { get; set; }

        public decimal  Due { get; set; }

    }
}
