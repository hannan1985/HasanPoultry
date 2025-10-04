using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
   public  class OtherPayment
    {
        [PrimaryKey()]
        public int OtherPaymentID { get; set; }

        public int OtherPartyID { get; set; }

        public string PartyName { get; set; }

        public DateTime PaymentDate { get; set; }

        public string ShortNote { get; set; }

        public decimal Amount { get; set; }   

        public int BranchID { get; set; }

        public int OrganizationID { get; set; }       

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
