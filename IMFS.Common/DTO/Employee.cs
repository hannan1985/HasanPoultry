using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("Employee")]
    public class Employee
    {
        private int _EmployeeID = 0;
        [PrimaryKey()]
        [DataType("int")]
        public int EmployeeID
        {
            get { return _EmployeeID; }
            set { _EmployeeID = value; }
        }


        private string _EmployeeName = string.Empty;

        [DataType("varchar")]
        public string EmployeeName
        {
            get { return _EmployeeName; }
            set { _EmployeeName = value; }
        }

        private int _DesignationID = 0;

        [DataType("int")]
        public int DesignationID
        {
            get { return _DesignationID; }
            set { _DesignationID = value; }
        }

        private string _Address = string.Empty;

        [DataType("varchar")]
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        private DateTime _JoinDate = DateTime.Now;

        [DataType("DateTime")]
        public DateTime JoinDate
        {
            get { return _JoinDate; }
            set { _JoinDate = value; }
        }

        private decimal _Salary = 0;

        [DataType("decimal")]
        public decimal Salary
        {
            get { return _Salary; }
            set { _Salary = value; }
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

        private bool _IsDeleted = false;
        [DataType("bool")]
        public bool IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }


        public int EmployeeTypeID { get; set; }




        public int BranchID { get; set; }

        public int OrganizationID { get; set; }
    }
}
