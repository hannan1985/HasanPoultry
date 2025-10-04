using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("RoleModuleMapping")]
    public class RoleModuleMapping 
    {
        private int _RoleModuleMappingID;

        [PrimaryKey]
        [DataType("int")]
        public int RoleModuleMappingID
        {
            get { return _RoleModuleMappingID; }
            set { _RoleModuleMappingID = value; }
        }

        private int _RoleID;

        [DataType("int")]
        public int RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }

        private int _ModuleID;

        [DataType("int")]
        public int ModuleID
        {
            get { return _ModuleID; }
            set { _ModuleID = value; }
        }


    }
}
