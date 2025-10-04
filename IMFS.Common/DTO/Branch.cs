using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("Branch")]
    public class Branch
    {
        private int _BranchID = 0;
        [PrimaryKey()]

        public int BranchID
        {
            get { return _BranchID; }
            set { _BranchID = value; }
        }


        private string _BranchName = string.Empty;


        public string BranchName
        {
            get { return _BranchName; }
            set { _BranchName = value; }
        }


        private string _ContactPerson = string.Empty;


        public string ContactPerson
        {
            get { return _ContactPerson; }
            set { _ContactPerson = value; }
        }

        private string _Email = string.Empty;


        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }


        private string _Phone = string.Empty;


        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }

        private string _Address = string.Empty;


        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        private string _Fax = string.Empty;


        public string Fax
        {
            get { return _Fax; }
            set { _Fax = value; }
        }

        private int _Status = 0;


        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }


        public byte[] CompanyLogo { get; set; }

        public string RegistrationNumber { get; set; }


        private int _OrganizationID;

        public int OrganizationID
        {
            get { return _OrganizationID; }
            set { _OrganizationID = value; }
        }

    }
}
