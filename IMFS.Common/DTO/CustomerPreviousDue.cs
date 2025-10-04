using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    public class CustomerPreviousDue
    {
        [PrimaryKey()]
        public int PreviousDueID { get; set; }
        public int DueType { get; set; }
        public DateTime DueDate { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public decimal Amount { get; set; }
        public int BranchID { get; set; }
        public int OrganizationID { get; set; }
        public string  CustomerAddress { get; set; }


    }
}
