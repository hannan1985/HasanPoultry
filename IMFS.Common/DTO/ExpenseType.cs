using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    public class ExpenseType
    {
        [PrimaryKey()]
        public int ExpenseTypeID { get; set; }

        public string ExpenseTypeName { get; set; }

        public int BranchID { get; set; }

        public int OrganizationID { get; set; }

        public string Description { get; set; }

        public DateTime ApprovedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

    }
}
