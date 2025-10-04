using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("AccountType")]
    public class AccountType
    {

        private int _AccountTypeID = 0;
        [PrimaryKey()]
        [DataType("int")]
        public int AccountTypeID
        {
            get { return _AccountTypeID; }
            set { _AccountTypeID = value; }
        }

        private int _AccountHeadID = 0;

        [DataType("int")]
        public int AccountHeadID
        {
            get { return _AccountHeadID; }
            set { _AccountHeadID = value; }
        }

        private string _AccountTypeName = string.Empty;

        [DataType("varchar")]
        public string AccountTypeName
        {
            get { return _AccountTypeName; }
            set { _AccountTypeName = value; }
        }


        private string _CreatedBy = string.Empty;

        [DataType("varchar")]
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        private DateTime _CreatedDate = DateTime.Now;

        [DataType("DateTime")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }

        private int _Status = 0;

        [DataType("int")]
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
    }
}
