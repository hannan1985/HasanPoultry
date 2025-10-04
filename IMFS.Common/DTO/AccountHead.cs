using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("AccountHead")]
    public class AccountHead
    {
        private int _AccountHeadID = 0;

        [DataType("int")]
        public int AccountHeadID
        {
            get { return _AccountHeadID; }
            set { _AccountHeadID = value; }
        }

        private string _Description = string.Empty;

        [DataType("varchar")]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
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
