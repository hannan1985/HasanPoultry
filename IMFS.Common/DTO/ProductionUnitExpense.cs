using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    public class ProductionUnitExpense
    {
        public DateTime ApprovedDate;
        [PrimaryKey()]
        public int ProductionUnitExpenseID { get; set; }

        public DateTime ExpenseDate { get; set; }

        public int EmployeeID { get; set; }

        public int ExpenseType { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        public int BranchID { get; set; }

        public int OrganizationID { get; set; }

        public int UnitID { get; set; }

    }
}
