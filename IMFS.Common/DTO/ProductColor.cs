using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable, TableMap("ProductColor")]
    public class ProductColor 
    {
               
        private int _ProductColorID = 0;

        [PrimaryKey()]
        [DataType("int")]
        public int ProductColorID
        {
            get { return _ProductColorID; }
            set { _ProductColorID = value; }
        }

        private string _Name = string.Empty;

        [DataType("nvarchar")]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
    }
}
