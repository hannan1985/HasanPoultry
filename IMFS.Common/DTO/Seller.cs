using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [TableMap("Seller")]
    public class Seller
    {
        
        private int _SellerID;

        [PrimaryKey()]
        public int SellerID
        {
            get { return _SellerID; }
            set { _SellerID = value; }
        }


        private string _SellerName;

        public string SellerName
        {
            get { return _SellerName; }
            set { _SellerName = value; }
        }

        private string _Address;

        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        private string _Phone;

        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }

        private string _ContactPerson;

        public string ContactPerson
        {
            get { return _ContactPerson; }
            set { _ContactPerson = value; }
        }




        public int BranchID { get; set; }

        public int OrganizationID { get; set; }
    }
}
