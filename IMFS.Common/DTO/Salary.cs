using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    public class Salary
    {
        [PrimaryKey()]
        public int SalaryID { get; set; }

        public DateTime SalaryDate { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public int EmployeeID { get; set; }

        public string EmployeeName { get; set; }

        public string  Address { get; set; }

        public string Designation { get; set; }

        public decimal MonthlySalary { get; set; }

        public int WorkingDay { get; set; }

        public decimal Deduction { get; set; }

        public decimal Incentive { get; set; }

        public decimal Bonus { get; set; }

        public decimal NetSalary { get; set; }

        public decimal PaidAmount { get; set; }

        public decimal Due { get; set; }

        public string  CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string  UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public decimal Advance { get; set; }

        public string  TypeName { get; set; }
    }
}
