using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("Category")]
    public class Category
    {

        private int _CategoryID = 0;

        [PrimaryKey()]
        [DataType("int")]
        public int CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; }
        }

        private int _ItemID = 0;


        [DataType("int")]
        public int ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }

        private string _CategoryName = string.Empty;

        [DataType("varchar")]
        public string CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; }
        }
    }
}
