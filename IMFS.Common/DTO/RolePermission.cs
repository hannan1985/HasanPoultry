using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("RolePermission")]
    public class RolePermission
    {
        private int _roleID = -1;
        [PrimaryKey ]
        [DataType("int")]
        public int RoleID
        {
            get { return _roleID; }
            set { _roleID = value; }
        }


        private string _permissionCode = string.Empty;

        [DataType("varchar")]
        public string PermissionCode
        {
            get { return _permissionCode; }
            set { _permissionCode = value; }
        }
    }
}
