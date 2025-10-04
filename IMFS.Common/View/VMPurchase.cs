using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;

namespace IMFS.Common.View
{
  public   class VMPurchase
    {
        public PurchaseOrder PurchaseOrder { get; set; }

        public List<PurchaseOrderDetail> lstPurchaseOrderDetail { get; set; }

        public VMPurchase()
        {
            PurchaseOrder = new PurchaseOrder();
            lstPurchaseOrderDetail = new List<PurchaseOrderDetail>();
        }
    }
}
