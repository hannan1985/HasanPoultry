using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    public class DistributeSample
    {
        private int _DistributionID;


        [PrimaryKey()]
        public int DistributionID
        {
            get { return _DistributionID; }
            set { _DistributionID = value; }
        }

        private int _SellerID;

        public int SellerID
        {
            get { return _SellerID; }
            set { _SellerID = value; }
        }

        private string _ProductID;

        public string ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }


        private DateTime _DistributeDate = DateTime.Now;

        public DateTime DistributeDate
        {
            get { return _DistributeDate; }
            set { _DistributeDate = value; }
        }

        private DateTime _ReceiveDate = DateTime.Now;

        public DateTime ReceiveDate
        {
            get { return _ReceiveDate; }
            set { _ReceiveDate = value; }
        }


        private decimal _GivenQuantity = 0;

        public decimal GivenQuantity
        {
            get { return _GivenQuantity; }
            set { _GivenQuantity = value; }
        }

        private decimal _ReceiveQuantity = 0;

        public decimal ReceiveQuantity
        {
            get { return _ReceiveQuantity; }
            set { _ReceiveQuantity = value; }
        }




        public int BranchID { get; set; }

        public int OrganizationID { get; set; }
    }
}
