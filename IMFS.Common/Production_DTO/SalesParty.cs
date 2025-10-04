using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
   public  class SalesParty
    {

        private int _SalesPartyID = 0;
        [PrimaryKey()]
        [DataType("int")]
        public int SalesPartyID
        {
            get { return _SalesPartyID; }
            set { _SalesPartyID = value; }
        }


        private string _PartyName = string.Empty;

        [DataType("varchar")]
        public string PartyName
        {
            get { return _PartyName; }
            set { _PartyName = value; }
        }

        private string _Address = string.Empty;

        [DataType("varchar")]
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        private string _Phone = string.Empty;

        [DataType("varchar")]
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }


        public int BranchID { get; set; }

        public int OrganizationID { get; set; }
            

    }
}
