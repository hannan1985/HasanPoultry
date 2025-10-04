using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    public class StockPrice
    {
        [PrimaryKey()]
        public int StockPriceID { get; set; }

        public string ProductID { get; set; }

        public string ProductName { get; set; }

        public decimal OldPrice { get; set; }

        public decimal Price { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdateDate { get; set; }

        public int BranchID { get; set; }

        public int OrganizationID { get; set; }
    }
}
