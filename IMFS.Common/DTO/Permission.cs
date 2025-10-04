using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("Permission")]
    public class Permission
    {
        private int _PermissionID ;

        [PrimaryKey()]
        [DataType("int")]
        public int PermissionID
        {
            get { return _PermissionID; }
            set { _PermissionID = value; }
        }

        private int _ModuleID;

        [DataType("int")]
        public int ModuleID
        {
            get { return _ModuleID; }
            set { _ModuleID = value; }
        }

        private string _FormName = string.Empty;

        [DataType("char")]
        public string FormName
        {
            get { return _FormName; }
            set { _FormName = value; }
        }

        private string _PermissionName = string.Empty;

        [DataType("varchar")]
        public string PermissionName
        {
            get { return _PermissionName; }
            set { _PermissionName = value; }
        }


        private bool _isActive = false;
        [DataType("bit")]
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }


        private bool _isDeleted = false;
        [DataType("bit")]
        public bool IsDeleted
        {
            get { return _isDeleted; }
            set { _isDeleted = value; }
        }

        public string PermissionDisplayText
        {
            get { return _PermissionID + " - " + _PermissionName; }
        }
    }
}
