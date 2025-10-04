using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    public class BankDeposit
    {
        [PrimaryKey()]
        public int BankDepositID { get; set; }
        public DateTime DepositDate { get; set; }
        public int BankAccountID { get; set; }
        public decimal DepositAmount { get; set; }
        public string ShortNote { get; set; }
        public string BankAccountNumber { get; set; }
        public int BranchID { get; set; }
        public int OrganizationID { get; set; }
    }
}
