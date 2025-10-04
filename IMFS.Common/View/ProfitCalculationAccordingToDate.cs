using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.View
{
   public class ProfitCalculationAccordingToDate
    {
        private DateTime _SalesDate;

        [DataType("datetime")]
        public DateTime SalesDate
        {
            get { return _SalesDate; }
            set { _SalesDate = value; }
        }

        private decimal _TotalSales = 0;

        [DataType("decimal")]
        public decimal TotalSales
        {
            get { return _TotalSales; }
            set { _TotalSales = value; }
        }

        private decimal _Discount = 0;

        [DataType("decimal")]
        public decimal Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
        }


        private decimal _TotalPurchase = 0;

        [DataType("decimal")]
        public decimal TotalPurchase
        {
            get { return _TotalPurchase; }
            set { _TotalPurchase = value; }
        }

        private decimal _Profit = 0;

        [DataType("decimal")]
        public decimal Profit
        {
            get { return _Profit; }
            set { _Profit = value; }
        }

        public string  ProductID { get; set; }

        public int Quantity { get; set; }

    }
}
