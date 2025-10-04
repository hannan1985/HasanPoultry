using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    public class GatePass
    {
        [PrimaryKey()]
        public int GatePassID { get; set; }

        public int SalesID { get; set; }

        public DateTime CreatedDate { get; set; }

        public string  DriverName { get; set; }

        public string  VehicleNumber { get; set; }

        public string  Destination { get; set; }

        public string  Transport { get; set; }

        public string  Comment { get; set; }

    }
}
