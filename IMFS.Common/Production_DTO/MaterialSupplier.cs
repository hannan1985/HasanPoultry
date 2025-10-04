using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    public class MaterialSupplier
    {
        private int _SupplierID = 0;
        [PrimaryKey()]
        [DataType("int")]
        public int SupplierID
        {
            get { return _SupplierID; }
            set { _SupplierID = value; }
        }       

        private string _SupplierName = string.Empty;

        [DataType("varchar")]
        public string SupplierName
        {
            get { return _SupplierName; }
            set { _SupplierName = value; }
        }

        private string _PhoneNumber = string.Empty;

        [DataType("varchar")]
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }


        public string  Address { get; set; }


        public int BranchID { get; set; }

        public int OrganizationID { get; set; }
    }
}
