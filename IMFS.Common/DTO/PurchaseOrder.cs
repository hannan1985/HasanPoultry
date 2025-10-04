using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("PurchaseOrder")]
    public class PurchaseOrder
    {
        private int _PurchaseID = 0;

        [PrimaryKey()]
        [DataType("int")]
        public int PurchaseID
        {
            get { return _PurchaseID; }
            set { _PurchaseID = value; }
        }


        private DateTime _PurchaseDate;

        [DataType("datetime")]
        public DateTime PurchaseDate
        {
            get { return _PurchaseDate; }
            set { _PurchaseDate = value; }
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

        private decimal _PurchaseAmount = 0;

        [DataType("decimal")]
        public decimal PurchaseAmount
        {
            get { return _PurchaseAmount; }
            set { _PurchaseAmount = value; }
        }


        private string  _SalesmenName = string.Empty ;

        [DataType("varchar")]
        public string SalesmenName
        {
            get { return _SalesmenName; }
            set { _SalesmenName = value; }
        }

        private string _InvoiceNumber = string.Empty;

        [DataType("varchar")]
        public string InvoiceNumber
        {
            get { return _InvoiceNumber; }
            set { _InvoiceNumber = value; }
        }


        private int _SalesID = 0;

        [DataType("int")]
        public int SalesID
        {
            get { return _SalesID; }
            set { _SalesID = value; }
        }


        private decimal _Discount = 0;

        [DataType("decimal")]
        public decimal Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
        }

        private bool _IsSalesReturn = false ;

        [DataType("bool")]
        public bool IsSalesReturn
        {
            get { return _IsSalesReturn; }
            set { _IsSalesReturn = value; }
        }


        private decimal _PaidAmount = 0;

        [DataType("decimal")]
        public decimal PaidAmount
        {
            get { return _PaidAmount; }
            set { _PaidAmount = value; }
        }

        public int Status { get; set; }

        public int BranchID { get; set; }

        public int OrganizationID { get; set; }


        public int ImportTrip { get; set; }

        public string  CompanyName { get; set; }

        public string  SupplierName { get; set; }




    }
}
