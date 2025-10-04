using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    public class SalesRepresentative
    {
        [PrimaryKey()]
        public int SalesRepresentativeID { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public int Designation { get; set; }

        public int BranchID { get; set; }

        public int OrganizationID { get; set; }

        public string Email { get; set; }

        public DateTime  JoinDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
