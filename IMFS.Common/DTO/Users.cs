using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("Users")]
    public class Users
    {
        private string _UserID = string.Empty;

        [PrimaryKey()]
        [DataType("nvarchar")]
        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        private string _password = string.Empty;

        [DataType("nvarchar")]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }


        private string _Name = string.Empty;

        [DataType("nvarchar")]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private int _EmployeeID = 0;
        [DataType("int")]
        public int EmployeeID
        {
            get { return _EmployeeID; }
            set { _EmployeeID = value; }
        }

        private string _Surname = string.Empty;

        [DataType("nvarchar")]
        public string Surname
        {
            get { return _Surname; }
            set { _Surname = value; }
        }


        private int _AccessRight = 0;

        [DataType("int")]
        public int AccessRight
        {
            get { return _AccessRight; }
            set { _AccessRight = value; }
        }


        private string _Designation = string.Empty;

        [DataType("varchar")]
        public string Designation
        {
            get { return _Designation; }
            set { _Designation = value; }
        }


        private int _FacilityID = 0;

        [DataType("int")]
        public int FacilityID
        {
            get { return _FacilityID; }
            set { _FacilityID = value; }
        }


        private string _Telephone = string.Empty;

        [DataType("nvarchar")]
        public string Telephone
        {
            get { return _Telephone; }
            set { _Telephone = value; }
        }


        private string _CellPhone = string.Empty;

        [DataType("nvarchar")]
        public string CellPhone
        {
            get { return _CellPhone; }
            set { _CellPhone = value; }
        }


        private string _Email = string.Empty;

        [DataType("nvarchar")]
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }


        private string _IM = string.Empty;

        [DataType("nvarchar")]
        public string IM
        {
            get { return _IM; }
            set { _IM = value; }
        }


        private System.DateTime _RegistryDate = DateTime.Now;

        [DataType("datetime")]
        public System.DateTime RegistryDate
        {
            get { return _RegistryDate; }
            set { _RegistryDate = value; }
        }


        private int _Status = 0;

        [DataType("int")]
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }


        private string _Network = string.Empty;

        [DataType("nvarchar")]
        public string Network
        {
            get { return _Network; }
            set { _Network = value; }
        }


        private string _MACAddress = string.Empty;

        [DataType("nvarchar")]
        public string MACAddress
        {
            get { return _MACAddress; }
            set { _MACAddress = value; }
        }


        private int _TimeSlot = 0;

        [DataType("int")]
        public int TimeSlot
        {
            get { return _TimeSlot; }
            set { _TimeSlot = value; }
        }

        private bool _isSuperAdmin = false;

        [DataType("bit")]
        public bool IsSuperAdmin
        {
            get { return _isSuperAdmin; }
            set { _isSuperAdmin = value; }
        }

        private bool _IsDeleted = false;

        public bool IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }



        public int UserType { get; set; }

        public int BranchID { get; set; }

        public int OrganizationID { get; set; }
    }
}
