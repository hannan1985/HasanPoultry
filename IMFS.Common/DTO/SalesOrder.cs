using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("CashSales")]
    public class SalesOrder
    {
        private int _SalesID = 0;

        [PrimaryKey()]
        [DataType("int")]
        public int SalesID
        {
            get { return _SalesID; }
            set { _SalesID = value; }
        }


        private DateTime _SalesDate;

        [DataType("datetime")]
        public DateTime SalesDate
        {
            get { return _SalesDate; }
            set { _SalesDate = value; }
        }

        private decimal _SalesAmount = 0;

        [DataType("decimal")]
        public decimal SalesAmount
        {
            get { return _SalesAmount; }
            set { _SalesAmount = value; }
        }

        private int _EmployeeID = 0;
        [DataType("int")]
        public int EmployeeID
        {
            get { return _EmployeeID; }
            set { _EmployeeID = value; }
        }

        private int _CustomerID = 0;
        [DataType("int")]
        public int CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }


        private decimal _Discount = 0;

        [DataType("decimal")]
        public decimal Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
        }


        private decimal _ReveiveAmount = 0;

        [DataType("decimal")]
        public decimal ReceiveAmount
        {
            get { return _ReveiveAmount; }
            set { _ReveiveAmount = value; }
        }

        private decimal _CarryingLoading = 0;

        [DataType("decimal")]
        public decimal CarryingLoading
        {
            get { return _CarryingLoading; }
            set { _CarryingLoading = value; }
        }

        private bool _IsCanceled = false;

        public bool IsCanceled
        {
            get { return _IsCanceled; }
            set { _IsCanceled = value; }
        }

        public string CustomerName { get; set; }

        public string CustomerPhone { get; set; }


        public int BranchID { get; set; }

        public int OrganizationID { get; set; }

        public int Status { get; set; }

        public string  BillNumber { get; set; }

        public int SalesRepresentativeID { get; set; }

        public string ShortNote { get; set; }
    }
}
