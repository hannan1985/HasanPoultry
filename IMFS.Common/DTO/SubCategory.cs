using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("SubCategory")]
    public class SubCategory
    {

        private int _SubCategoryID = 0;

        [PrimaryKey()]
        [DataType("int")]
        public int SubCategoryID
        {
            get { return _SubCategoryID; }
            set { _SubCategoryID = value; }
        }

        private int _CategoryID = 0;


        [DataType("int")]
        public int CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; }
        }

        private string _SubCategoryName = string.Empty;

        [DataType("varchar")]
        public string SubCategoryName
        {
            get { return _SubCategoryName; }
            set { _SubCategoryName = value; }
        }


    }
}
