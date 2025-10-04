using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("RoleActionMapping")]
    public class RoleActionMapping 
    {
        private int _RoleActionMappingID;

        [PrimaryKey]
        [DataType("int")]
        public int RoleActionMappingID
        {
            get { return _RoleActionMappingID; }
            set { _RoleActionMappingID = value; }
        }

        private int _RoleID;

        [DataType("int")]
        public int RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }

        private int _ActionID;

        [DataType("int")]
        public int ActionID
        {
            get { return _ActionID; }
            set { _ActionID = value; }
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
