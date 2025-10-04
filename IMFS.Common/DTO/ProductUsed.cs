using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    public class ProductUsed
    {
        [PrimaryKey()]
        public int ProductUsedID { get; set; }

        public DateTime UsedDate { get; set; }

        public int PartyID { get; set; }

        public decimal Total { get; set; }

        public int BranchID { get; set; }

        public int OrganizationID { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
