using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.View
{
   public class CustomerOurstanding
    {

        private string _CustomerName = string.Empty;

        [DataType("varchar")]
        public string CustomerName
        {
            get { return _CustomerName; }
            set { _CustomerName = value; }
        }

        private string _Address = string.Empty;

        [DataType("varchar")]
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        private string _Phone = string.Empty;

        [DataType("varchar")]
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }


        private int _SalesID = 0;

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


        private decimal _DueAmount = 0;

        [DataType("decimal")]
        public decimal DueAmount
        {
            get { return _DueAmount; }
            set { _DueAmount = value; }
        }


    }
}
