using System;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("GetProductInformationForSales")]
    public class ProductInformationForSales
    {
        private string _ProductID = string.Empty;

        [DataType("varchar")]
        public string ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }

        private string _ProecutName = string.Empty;

        [DataType("varchar")]
        public string ProductName
        {
            get { return _ProecutName; }
            set { _ProecutName = value; }
        }

        private string _TypeName = string.Empty;

        [DataType("varchar")]
        public string TypeName
        {
            get { return _TypeName; }
            set { _TypeName = value; }
        }

        private string _BarCode = string.Empty;

        [DataType("varchar")]
        public string Barcode
        {
            get { return _BarCode; }
            set { _BarCode = value; }
        }

        private decimal _Quantity = 0;

        [DataType("decimal")]
        public decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }

        private decimal _SalesPrice = 0;

        [DataType("decimal")]
        public decimal SalesPrice
        {
            get { return _SalesPrice; }
            set { _SalesPrice = value; }
        }
        public string  Model { get; set; }

        //private DateTime _ExpireDate = DateTime.Now;

        //[DataType("date")]
        //public DateTime ExpireDate
        //{
        //    get { return _ExpireDate; }
        //    set { _ExpireDate = value; }
        //}

        //private int _PurchaseID = 0;

        //[DataType("int")]
        //public int PurchaseID
        //{
        //    get { return _PurchaseID; }
        //    set { _PurchaseID = value; }
        //}


        public int BranchID { get; set; }

        public int OrganizationID { get; set; }


        public string Size { get; set; }

        public decimal VAT { get; set; }
    }
}
