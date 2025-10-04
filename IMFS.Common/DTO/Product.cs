using System;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("ProductInformation")]
    public class Product
    {
        private int _SerialNo;

        [PrimaryKey()]
        [DataType("int")]
        public int SerialNo
        {
            get { return _SerialNo; }
            set { _SerialNo = value; }
        }


        private string _ProductID = string.Empty;

        public int SubCategoryID { get; set; }

        [DataType("varchar")]
        public string ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }

        private string _ProductName = string.Empty;

        [DataType("varchar")]
        public string ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value; }
        }


        private int _ProductTypeID = 0;

        [DataType("int")]
        public int ProductTypeID
        {
            get { return _ProductTypeID; }
            set { _ProductTypeID = value; }
        }

        public string TypeName { get; set; }

        private int _ProductSizeID = 0;

        [DataType("int")]
        public int ProductSizeID
        {
            get { return _ProductSizeID; }
            set { _ProductSizeID = value; }
        }

        public string ProductSize { get; set; }

        private int _ProductModelID = 0;

        [DataType("int")]
        public int ProductModelID
        {
            get { return _ProductModelID; }
            set { _ProductModelID = value; }
        }

        public string ProductModel { get; set; }

        private int _ProductColorID = 0;

        [DataType("int")]
        public int ProductColorID
        {
            get { return _ProductColorID; }
            set { _ProductColorID = value; }
        }

        public string ColorName { get; set; }

        private string _GenericName = string.Empty;

        [DataType("varchar")]
        public string GenericName
        {
            get { return _GenericName; }
            set { _GenericName = value; }
        }

        private string _PackSize = string.Empty;

        [DataType("varchar")]
        public string PackSize
        {
            get { return _PackSize; }
            set { _PackSize = value; }
        }


        private string _ShelfNumber = string.Empty;

        [DataType("varchar")]
        public string ShelfNumber
        {
            get { return _ShelfNumber; }
            set { _ShelfNumber = value; }
        }


        private int _CompanyID = 0;

        [DataType("int")]
        public int CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }

        public string CompanyName { get; set; }

        private int _SupplierID = 0;

        [DataType("int")]
        public int SupplierID
        {
            get { return _SupplierID; }
            set { _SupplierID = value; }
        }


        public string SupplierName { get; set; }

        private bool _Discountinued = false;

        [DataType("bit")]
        public bool Discountinued
        {
            get { return _Discountinued; }
            set { _Discountinued = value; }
        }

        private int _ReorderQuanitity = 0;

        [DataType("int")]
        public int ReorderQuantity
        {
            get { return _ReorderQuanitity; }
            set { _ReorderQuanitity = value; }
        }

        private string _ProductModelName = string.Empty;

        [DataType("varchar")]
        public string ProductModelName
        {
            get { return _ProductModelName; }
            set { _ProductModelName = value; }
        }
        public decimal VAT { get; set; }


        public int BranchID { get; set; }

        public int OrganizationID { get; set; }

        public string SizeName { get; set; }

        public string  BarCode { get; set; }

        public string  Unit { get; set; }

        public decimal  CartonSize { get; set; }
    }
}
