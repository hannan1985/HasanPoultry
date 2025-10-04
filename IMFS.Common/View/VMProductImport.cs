using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;
namespace IMFS.Common.View
{
    public class VMProductImport
    {
        public List<Product> lstProduct { get; set; }
        public List<ProductType> lstProductType { get; set; }
        public List<ProductModel> lstProductModel { get; set; }
        public List<ProductSize> lstProductSize { get; set; }
        public List<ReceiveProductDetail> lstReceiveProductDetail { get; set; }
        public List<StockPrice>  lstStockPrice { get; set; }

        public VMProductImport()
        {
            lstProduct = new List<Product>();
            lstProductType = new List<ProductType>();
            lstProductModel = new List<ProductModel>();
            lstProductSize = new List<ProductSize>();
            lstReceiveProductDetail = new List<ReceiveProductDetail>();
            lstStockPrice = new List<StockPrice>();
        }

    }
}
