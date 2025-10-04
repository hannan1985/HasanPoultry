using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("Suppliers")]
    public class Supplier
    {
        private int _SupplierID = 0;
        [PrimaryKey()]
        [DataType("int")]
        public int SupplierID
        {
            get { return _SupplierID; }
            set { _SupplierID = value; }
        }


        private int _CompanyID = 0;

        [DataType("int")]
        public int CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }


        private string _SupplierName = string.Empty;

        [DataType("varchar")]
        public string SupplierName
        {
            get { return _SupplierName; }
            set { _SupplierName = value; }
        }


        public string  Address { get; set; }

        private string _PhoneNumber = string.Empty;

        [DataType("varchar")]
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }


        private bool _Discontinued = false;

        [DataType("bool")]
        public bool Discontinued
        {
            get { return _Discontinued; }
            set { _Discontinued = value; }
        }


        public int BranchID { get; set; }

        public int OrganizationID { get; set; }

        public string Email { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
