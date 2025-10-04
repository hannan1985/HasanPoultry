using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    public class EmployeeType
    {
        [PrimaryKey()]
        public int EmployeeTypeID { get; set; }

        public string TypeName { get; set; }
    }
}
