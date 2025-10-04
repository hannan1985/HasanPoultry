using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    public class Designation
    {
        [PrimaryKey()]
        public int DesignationID { get; set; }

        public string  DesignationName { get; set; }

    }
}
