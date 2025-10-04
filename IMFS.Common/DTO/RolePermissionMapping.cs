using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("RolePermissionMapping")]
    public class RolePermissionMapping 
    {
        private int _RolePermissionMappingID;
        [PrimaryKey]
        [DataType("int")]
        public int RolePermissionID
        {
            get { return _RolePermissionMappingID; }
            set { _RolePermissionMappingID = value; }
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

        private int _PermissionID;

        [DataType("int")]
        public int PermissionID
        {
            get { return _PermissionID; }
            set { _PermissionID = value; }
        }
    }
}
