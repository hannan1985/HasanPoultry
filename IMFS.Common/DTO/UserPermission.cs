using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("UserPermission")]
    public class UserPermission
    {
        private string _userID = string.Empty;

        [DataType("varchar")]
        public string UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }


        private int _roleID = -1;

        [DataType("int")]
        public int RoleID
        {
            get { return _roleID; }
            set { _roleID = value; }
        }


        private string _permissionCode = string.Empty;

        [DataType("char")]
        public string PermissionCode
        {
            get { return _permissionCode; }
            set { _permissionCode = value; }
        }
    }
}
