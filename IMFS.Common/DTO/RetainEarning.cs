using System;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("RetainEarning")]
    public class RetainEarning
    {
        private int _RetaineEarningID = 0;
        [PrimaryKey()]
        [DataType("int")]
        public int RetaineEarningID
        {
            get { return _RetaineEarningID; }
            set { _RetaineEarningID = value; }
        }

        private DateTime  _EarningDate = DateTime .Now ;


        [DataType("date")]
        public DateTime EarningDate
        {
            get { return _EarningDate; }
            set { _EarningDate = value; }
        }

        private decimal _EarningAmount =0;

        [DataType("decimal")]
        public decimal  EarningAmount
        {
            get { return _EarningAmount; }
            set { _EarningAmount = value; }
        }
        private int  _FiscalYear = 0 ;

        [DataType("int")]
        public int FiscalYear
        {
            get { return _FiscalYear; }
            set { _FiscalYear = value; }
        }

    }
}
