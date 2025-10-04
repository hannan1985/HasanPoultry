using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    public class BankWithdraw
    {
        [PrimaryKey()]
        public int WithdrawID { get; set; }
        public DateTime WithdrawDate { get; set; }
        public int BankAccountID { get; set; }
        public decimal WithdrawAmount { get; set; }
        public string ShortNote { get; set; }
        public string BankAccountNumber { get; set; }
        public int BranchID { get; set; }
        public int OrganizationID { get; set; }


    }
}
