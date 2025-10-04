using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    public class DamageDetail
    {
        private int _DamageDetailID;

        [PrimaryKey()]
        public int DamageDetailID
        {
            get { return _DamageDetailID; }
            set { _DamageDetailID = value; }
        }

        private int _DamageInfoID;

        public int DamageInfoID
        {
            get { return _DamageInfoID; }
            set { _DamageInfoID = value; }
        }


        private decimal _Quantity;

        public decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }

        private string _ProductCode;

        public string ProductCode
        {
            get { return _ProductCode; }
            set { _ProductCode = value; }
        }

        private string _ProductName;

        public string ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value; }
        }


        private decimal _SquareFeet;

        public decimal SquareFeet
        {
            get { return _SquareFeet; }
            set { _SquareFeet = value; }
        }





    }
}
