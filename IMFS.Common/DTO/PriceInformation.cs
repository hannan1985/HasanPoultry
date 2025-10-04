using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("PriceInformation")]
    public class PriceInformation
    {
        private int _ProductPriceID = 0;
        [PrimaryKey()]
        [DataType("int")]
        public int ProductPriceID
        {
            get { return _ProductPriceID; }
            set { _ProductPriceID = value; }
        }

        private string _ProductID = string.Empty;
        [DataType("varchar")]
        public string ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }

        private decimal _BoxPrice = 0;
        [DataType("decimal")]
        public decimal BoxPrice
        {
            get { return _BoxPrice; }
            set { _BoxPrice = value; }
        }

        private decimal _BoxMRP = 0;
        [DataType("decimal")]
        public decimal BoxMRP
        {
            get { return _BoxMRP; }
            set { _BoxMRP = value; }
        }

        private decimal _UnitPrice = 0;
        [DataType("decimal")]
        public decimal UnitPrice
        {
            get { return _UnitPrice; }
            set { _UnitPrice = value; }
        }

        private decimal _UnitMRP = 0;
        [DataType("decimal")]
        public decimal UnitMRP
        {
            get { return _UnitMRP; }
            set { _UnitMRP = value; }
        }

        private decimal _VAT = 0;

        [DataType("decimal")]
        public decimal VAT
        {
            get { return _VAT; }
            set { _VAT = value; }
        }

        private DateTime _LastUpdateDate;

        [DataType("datetime")]
        public DateTime LastUpdateDate
        {
            get { return _LastUpdateDate; }
            set { _LastUpdateDate = value; }
        }

        private int _UpdatedBy = 0;

        [DataType("int")]
        public int UpdatedBy
        {
            get { return _UpdatedBy; }
            set { _UpdatedBy = value; }
        }


    }
}
