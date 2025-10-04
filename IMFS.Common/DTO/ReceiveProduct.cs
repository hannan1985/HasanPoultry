using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    public class ReceiveProduct
    {
        private int _ReceiveProductID;

        [PrimaryKey()]
        public int ReceiveProductID
        {
            get { return _ReceiveProductID; }
            set { _ReceiveProductID = value; }
        }

        private DateTime _ReceiveDate;

        public DateTime ReceiveDate
        {
            get { return _ReceiveDate; }
            set { _ReceiveDate = value; }
        }

        private int _SalesCenterID;

        public int SalesCenterID
        {
            get { return _SalesCenterID; }
            set { _SalesCenterID = value; }
        }

        public bool IsReceiveFromProduction { get; set; }

        public int ProductUsedID { get; set; }

        private string _ReceiveBy;

        public string ReceiveBy
        {
            get { return _ReceiveBy; }
            set { _ReceiveBy = value; }
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

    }
}
