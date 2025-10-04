using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    //   [Serializable(), TableMap("SalesQuotation")]
    public class SalesQuotation
    {
        private int _SalesQuotationID = 0;
        [PrimaryKey()]
        [DataType("int")]
        public int SalesQuotationID
        {
            get { return _SalesQuotationID; }
            set { _SalesQuotationID = value; }
        }


        private DateTime _QuotationDate = DateTime.Now;
        [DataType("date")]
        public DateTime QuotationDate
        {
            get { return _QuotationDate; }
            set { _QuotationDate = value; }
        }

        private int _CustomerID = 0;
        [DataType("int")]
        public int CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }



        private DateTime _ValidUntil = DateTime.Now;
        [DataType("date")]
        public DateTime ValidUntil
        {
            get { return _ValidUntil; }
            set { _ValidUntil = value; }
        }
        
        private string _ProductName = string.Empty;

        [DataType("varchar")]
        public string ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value; }
        }


        private string _ProductID = string.Empty;

        [DataType("varchar")]
        public string ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }

        private decimal _Quantity = 0;

        public decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }


        private decimal  _Price;

        public decimal  Price
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


        public int Status { get; set; }
    }
}
