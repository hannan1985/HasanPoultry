using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable, TableMap("ProductSize")]
    public class ProductSize 
    {
        private int _ProductSizeID = 0;

        [PrimaryKey()]
        [DataType("int")]
        public int ProductSizeID
        {
            get { return _ProductSizeID; }
            set { _ProductSizeID = value; }
        }

        private string _Name = string.Empty;

        [DataType("nvarchar")]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _Description = string.Empty;

        [DataType("nvarchar")]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public int OrganizationID { get; set; }

        public int BranchID { get; set; }
    }
}
