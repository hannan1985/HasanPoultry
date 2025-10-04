using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;
namespace IMFS.Common.View
{
    public class VMProductUsed
    {
        public ProductUsed ProductUsed { get; set; }

        public List<ProductUsedDetail> lstProductUsedDetail { get; set; }
        public VMReceiveProduct VMReceiveProduct = new VMReceiveProduct();

        public VMProductUsed()
        {
            ProductUsed = new ProductUsed();
            lstProductUsedDetail = new List<ProductUsedDetail>();
            VMReceiveProduct = new VMReceiveProduct();
        }
    }
}
