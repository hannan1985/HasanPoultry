using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMFS.Common.View
{
    public class StockDetailInformation
    {
        public string ProductName { get; set; }
        public string ProductID { get; set; }
        public int PreviousQuantity { get; set; }
        public int ReceiveQuantity { get; set; }
        public int SalesReturnQuantity { get; set; }
        public int SalesQuantity { get; set; }
        public int TransferQuantity { get; set; }
        public int SampleQuantity { get; set; }
        public int DamageQuantity { get; set; }
        public int PReturnQuantity { get; set; }
        public int ClosingStock { get; set; }


    }
}
