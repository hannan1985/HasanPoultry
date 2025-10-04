using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("Action")]
    public class Action
    {
        private int _PermissionID;

        [DataType("int")]
        public int PermissionID
        {
            get { return _PermissionID; }
            set { _PermissionID = value; }
        }

        private int _ActionID;

        [DataType("int")]
        public int ActionID
        {
            get { return _ActionID; }
            set { _ActionID = value; }
        }

        private string _ButtonName = string.Empty;

        [DataType("char")]
        public string ButtonName
        {
            get { return _ButtonName; }
            set { _ButtonName = value; }
        }

        private string _ActionName = string.Empty;

        [DataType("varchar")]
        public string ActionName
        {
            get { return _ActionName; }
            set { _ActionName = value; }
        }
    }
}
