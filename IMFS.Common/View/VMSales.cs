using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;

namespace IMFS.Common.View
{
    public class VMSales
    {
        public SalesOrder SalesOrder { get; set; }

        public List<SalesOrderDetail> lstSalesOrderDetail { get; set; }

        public List<ProductInformationForSales> lstProductInformationForSales { get; set; }

        public VMSales()
        {
            SalesOrder = new SalesOrder();
            lstSalesOrderDetail = new List<SalesOrderDetail>();
            lstProductInformationForSales = new List<ProductInformationForSales>();
        }

    }
}
