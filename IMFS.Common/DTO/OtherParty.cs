using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    public class OtherParty
    {
        [PrimaryKey()]
        public int OtherPartyID { get; set; }

        public string PartyName { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public int BranchID { get; set; }

        public int OrganizationID { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
