using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;
namespace IMFS.Common.View
{
    public class VMPurchaseReturn
    {
        public PurchaseReturn purchaseReturn { get; set; }

        public List<PurchaseReturnDetail> lstPurchaseReturnDetail { get; set; }

        public VMPurchaseReturn()
        {
            purchaseReturn = new PurchaseReturn();
            lstPurchaseReturnDetail = new List<PurchaseReturnDetail>();
        }

    }
}
