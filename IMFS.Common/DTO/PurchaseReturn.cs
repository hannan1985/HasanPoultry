using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    public class PurchaseReturn
    {
        [PrimaryKey()]
        public int PurchaseReturnID { get; set; }

        public DateTime ReturnDate { get; set; }

        public string  PONumber { get; set; }

        public int CompanyID { get; set; }

        public int SupplierID { get; set; }

        public decimal Total { get; set; }

        public decimal  ReceiveAmount { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int BranchID { get; set; }

        public int OrganizationID { get; set; }
    }
}
