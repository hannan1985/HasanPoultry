using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("TypeInformation")]
    public class ProductType
    {
        private int _ProductTypeID = 0;
        [PrimaryKey()]
        [DataType("int")]
        public int ProductTypeID
        {
            get { return _ProductTypeID; }
            set { _ProductTypeID = value; }
        }

     
        private string _TypeName = string.Empty;

        [DataType("varchar")]
        public string TypeName
        {
            get { return _TypeName; }
            set { _TypeName = value; }
        }

        public int BranchID { get; set; }

        public int OrganizationID { get; set; }



    }
}
