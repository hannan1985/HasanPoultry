using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("Customer")]
    public class Customer
    {
        private int _CustomerID = 0;
        [PrimaryKey()]
        [DataType("int")]
        public int CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }


        private string _CustomerName = string.Empty;

        [DataType("varchar")]
        public string CustomerName
        {
            get { return _CustomerName; }
            set { _CustomerName = value; }
        }

        private string _CustomerAddress = string.Empty;

        [DataType("varchar")]
        public string Address
        {
            get { return _CustomerAddress; }
            set { _CustomerAddress = value; }
        }

        private string _Phone = string.Empty;

        [DataType("varchar")]
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }


        public decimal DiscountPercentage { get; set; }

        public decimal CreditLimit { get; set; }

        public bool ApplyCreditLimit { get; set; }

        public int BranchID { get; set; }

        public int OrganizationID { get; set; }

        public int CustomerType { get; set; }

        public int ZoneID { get; set; }

        public string CustomerCode { get; set; }

        public string  Proprietor  { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
