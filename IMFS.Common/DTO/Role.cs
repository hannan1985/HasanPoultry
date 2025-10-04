using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("Role")]
    public class Role
    {
        private int _roleID = -1;

        [PrimaryKey()]
        [DataType("int")]
        public int RoleID
        {
            get { return _roleID; }
            set { _roleID = value; }
        }


        private string _roleName = string.Empty;

        [DataType("varchar")]
        public string RoleName
        {
            get { return _roleName; }
            set { _roleName = value; }
        }


        private string _Description = string.Empty;

        [DataType("varchar")]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        private bool _isActive = false;

         [DataType("bit")]
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
    }
}
