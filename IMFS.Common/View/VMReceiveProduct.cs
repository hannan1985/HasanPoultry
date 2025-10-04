using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;

namespace IMFS.Common.View
{
    public class VMReceiveProduct
    {
        public ReceiveProduct ReceiveProduct { get; set; }

        public List<ReceiveProductDetail> lstRecevieProductDetail { get; set; }

        public VMReceiveProduct()
        {
            ReceiveProduct = new ReceiveProduct();
            lstRecevieProductDetail = new List<ReceiveProductDetail>();
        }
    }
}
