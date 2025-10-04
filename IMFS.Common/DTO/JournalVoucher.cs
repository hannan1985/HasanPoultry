using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    public class JournalVoucher
    {
        [PrimaryKey()]
        public Int64 JournalVoucherID { get; set; }

        public DateTime Date { get; set; }

        public Int64 VoucherNumber { get; set; }

        public int DebitAccountID { get; set; }

        public int DebitChildAccountID { get; set; }

        public string DebitAccountName { get; set; }

        public string DebitChildAccountName { get; set; }

        public int CreditAccountID { get; set; }

        public int CreditChildAccountID { get; set; }

        public string CreditAccountName { get; set; }

        public string CreditChildAccountName { get; set; }

        public decimal Amount { get; set; }

        public int ReferenceNo { get; set; }

        public string Description { get; set; }

        public int BranchID { get; set; }

        public int OrganizationID { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }


    }
}
