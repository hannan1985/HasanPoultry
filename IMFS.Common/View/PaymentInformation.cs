using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.View
{
    public class PaymentInformation
    {
        private int _PaymentID = 0;

        [DataType("int")]
        public int PaymentID
        {
            get { return _PaymentID; }
            set { _PaymentID = value; }
        }


        private DateTime _PaymentDate = DateTime.Now;

        [DataType("datetime")]
        public DateTime PaymentDate
        {
            get { return _PaymentDate; }
            set { _PaymentDate = value; }
        }

        private int _CompanyID = 0;

        [DataType("int")]
        public int CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }


        private string _CompanyName = string.Empty;

        [DataType("varchar")]
        public string CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }


        private int _SupplierID = 0;

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


        private decimal  _Amount = 0;

        [DataType("decimal")]
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        public int BranchID { get; set; }

        public int OrganizationID { get; set; }
    }
}
