using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    public class Transfer
    {
        private int _TransferID;

        [PrimaryKey()]
        public int TransferID
        {
            get { return _TransferID; }
            set { _TransferID = value; }
        }

        private DateTime _TransferDate;

        public DateTime TransferDate
        {
            get { return _TransferDate; }
            set { _TransferDate = value; }
        }

        private int _SalesCenterID;

        public int SalesCenterID
        {
            get { return _SalesCenterID; }
            set { _SalesCenterID = value; }
        }


        private string _TransferBy;

        public string TransferBy
        {
            get { return _TransferBy; }
            set { _TransferBy = value; }
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

        public decimal  Total { get; set; }

        public int BranchID { get; set; }

        public int OrganizationID { get; set; }
    }
}
