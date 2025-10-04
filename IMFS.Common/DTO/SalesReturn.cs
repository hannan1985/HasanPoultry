using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    public class SalesReturn
    {
        private int _SalesReturnID;

        [PrimaryKey()]
        public int SalesReturnID
        {
            get { return _SalesReturnID; }
            set { _SalesReturnID = value; }
        }

        private DateTime _ReturnDate;

        public DateTime ReturnDate
        {
            get { return _ReturnDate; }
            set { _ReturnDate = value; }
        }

        public int CustomerID { get; set; }

        public string  CustomerName { get; set; }

        public string  Phone { get; set; }

        public string  ShortNote { get; set; }


        private string _CreatedBy;

        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        private string _UpdatedBy;

        public string UpdatedBy
        {
            get { return _UpdatedBy; }
            set { _UpdatedBy = value; }
        }

        private DateTime _UpdatedDate = DateTime.Now;

        public DateTime UpdatedDate
        {
            get { return _UpdatedDate; }
            set { _UpdatedDate = value; }
        }



        public int BranchID { get; set; }

        public int OrganizationID { get; set; }


        public decimal Total { get; set; }

        public decimal  Discount { get; set; }
        public decimal  PaidAmount { get; set; }
    }
}
