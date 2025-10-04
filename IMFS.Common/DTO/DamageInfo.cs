using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    public class DamageInfo
    {
        private int _DamageInfoID;

        [PrimaryKey()]
        public int DamageInfoID
        {
            get { return _DamageInfoID; }
            set { _DamageInfoID = value; }
        }


        private string  _DamageReason;

        public string  DamageReason
        {
            get { return _DamageReason; }
            set { _DamageReason = value; }
        }
        

        private DateTime _DamageDate;

        public DateTime DamageDate
        {
            get { return _DamageDate; }
            set { _DamageDate = value; }
        }

        
        private string _CreatedBy;

        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        private int _Status;

        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        private string _UpdatedBy;

        public string UpdatedBy
        {
            get { return _UpdatedBy; }
            set { _UpdatedBy = value; }
        }


        public int BranchID { get; set; }

        public int OrganizationID { get; set; }
    }
}
