using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    public class SalesQuotationDetail 
    {
        private int _SalesQuotationDetailID = 0;
        [PrimaryKey()]
        [DataType("int")]
        public int SalesQuotationDetailID
        {
            get { return _SalesQuotationDetailID; }
            set { _SalesQuotationDetailID = value; }
        }


        private int _SalesQuotationID = 0;
        [DataType("int")]
        public int SalesQuotationID
        {
            get { return _SalesQuotationID; }
            set { _SalesQuotationID = value; }
        }

        private string _ItemSpec = string.Empty;

        [DataType("nvarchar")]
        public string ItemSpec
        {
            get { return _ItemSpec; }
            set { _ItemSpec = value; }
        }

        private string _ProductCode = string.Empty;

        [DataType("nvarchar")]
        public string ProductCode
        {
            get { return _ProductCode; }
            set { _ProductCode = value; }
        }

        private decimal _Quantity = 0;
        [DataType("decimal")]
        public decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }

        private decimal _FreeQuantity = 0;
        [DataType("decimal")]
        public decimal FreeQuantity
        {
            get { return _FreeQuantity; }
            set { _FreeQuantity = value; }
        }

        private decimal _Discount = 0;
        [DataType("decimal")]
        public decimal Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
        }


        private int _CurrencyID = 0;
        [DataType("int")]
        public int CurrencyID
        {
            get { return _CurrencyID; }
            set { _CurrencyID = value; }
        }

        private decimal _Price = 0;
        [DataType("decimal")]
        public decimal Price
        {
            get { return _Price; }
            set { _Price = value; }
        }

        private decimal _Total = 0;
        [DataType("decimal")]
        public decimal Total
        {
            get { return _Total; }
            set { _Total = value; }
        }

        private int _Status = 0;
        [DataType("int")]
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

    }
}
