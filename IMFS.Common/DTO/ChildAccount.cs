using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("ChildAccount")]
    public class ChildAccount
    {
        private int _ChildAccountID = 0;
        [PrimaryKey()]
        [DataType("int")]
        public int ChildAccountID
        {
            get { return _ChildAccountID; }
            set { _ChildAccountID = value; }
        }

        private int _AccountID = 0;

        [DataType("int")]
        public int AccountID
        {
            get { return _AccountID; }
            set { _AccountID = value; }
        }

        private string _Description = string.Empty;

        [DataType("varchar")]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        private int _CustomerID = 0;
        [DataType("int")]
        public int CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }

        private int _CompanyID = 0;
        [DataType("int")]
        public int CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }


        private int _SupplierID = 0;
        [DataType("int")]
        public int SupplierID
        {
            get { return _SupplierID; }
            set { _SupplierID = value; }
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

        public int BranchID { get; set; }

        public int OrganizationID { get; set; }

        public int SalesPartyID { get; set; }

        public int MaterailSupplierID { get; set; }

        public int ReferenceType { get; set; }

        public int ReferenceID { get; set; }
    }
}
