using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("Organization")]
    public class Organization
    {
        private int _OrganizationID = 0;
        [PrimaryKey()]
        [DataType("int")]
        public int OrganizationID
        {
            get { return _OrganizationID; }
            set { _OrganizationID = value; }
        }


        private string _OrganizationName = string.Empty;

        [DataType("varchar")]
        public string OrganizationName
        {
            get { return _OrganizationName; }
            set { _OrganizationName = value; }
        }


        private string _ContactPerson = string.Empty;

        [DataType("varchar")]
        public string ContactPerson
        {
            get { return _ContactPerson; }
            set { _ContactPerson = value; }
        }

        private string _Email = string.Empty;

        [DataType("varchar")]
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }


        private string _Phone = string.Empty;

        [DataType("varchar")]
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }

        private string _Address = string.Empty;

        [DataType("varchar")]
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        private string _Fax = string.Empty;

        [DataType("varchar")]
        public string Fax
        {
            get { return _Fax; }
            set { _Fax = value; }
        }

        private bool _Status = false;

        [DataType("bool")]
        public bool Status
        {
            get { return _Status; }
            set { _Status = value; }
        }


        public byte[] OrganizationLogo { get; set; }

        public string RegistrationNumber { get; set; }
    }
}
