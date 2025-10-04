using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    public class Zone
    {
        [PrimaryKey()]
        public int ZoneID { get; set; }

        public string  ZoneName { get; set; }

        public string Description { get; set; }

        public int BranchID { get; set; }

        public int OrganizationID { get; set; }

    }
}
