using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("Module")]
    public class Module
    {
        private int _ModuleID = 0;

        [DataType("int")]
        public int ModuleID
        {
            get { return _ModuleID; }
            set { _ModuleID = value; }
        }


        private string _ModuleName = string.Empty;

        [DataType("varchar")]
        public string ModuleName
        {
            get { return _ModuleName; }
            set { _ModuleName = value; }
        }

        private string _Description = string.Empty;

        [DataType("varchar")]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

    }
}
