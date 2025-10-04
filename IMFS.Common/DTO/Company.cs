using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable, TableMap("CompanyInformation")]
    public class Company
    {
        private int _CompanyID = 0;
        [PrimaryKey ()]
        [DataType("int")]
        public int CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }


        private string _CompanyName = string.Empty;

        [DataType("varchar")]
        public string CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }

        private string _CompanyAddress = string.Empty;

        [DataType("varchar")]
        public string CompanyAddress
        {
            get { return _CompanyAddress; }
            set { _CompanyAddress = value; }
        }

        private string _ContactPerson = string.Empty;

        [DataType("varchar")]
        public string ContactPerson
        {
            get { return _ContactPerson; }
            set { _ContactPerson = value; }
        }

        private string _Phone = string.Empty;

        [DataType("varchar")]
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }

        private string _Email = string.Empty;

        [DataType("varchar")]
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }


        public int OrganizationID { get; set; }

        public int BranchID { get; set; }
    }
}
