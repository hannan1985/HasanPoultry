using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable, TableMap("ProductModel")]
    public class ProductModel 
    {

        private int _ProductModelId = 0;

        [PrimaryKey()]
        [DataType("int")]
        public int ProductModelID
        {
            get { return _ProductModelId; }
            set { _ProductModelId = value; }
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

        public int BranchID { get; set; }

        public int OrganizationID { get; set; }

    }
}
