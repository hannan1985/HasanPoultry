using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("Items")]
    public class Items
    {

        private int _ItemID = 0;

        [PrimaryKey ()]
        [DataType("int")]
        public int ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }

        private string _ItemName = string.Empty;

        [DataType("varchar")]
        public string ItemName
        {
            get { return _ItemName; }
            set { _ItemName = value; }
        }
    }
}
