using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    public class ProductionUnit
    {
        [PrimaryKey()]
        public int UnitID { get; set; }

        public string UnitName { get; set; }

        public string Address { get; set; }

        public string Responsible { get; set; }

        public string  PhoneNumber { get; set; }

    }
}
