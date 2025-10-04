using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;

namespace IMFS.Common.View
{
    public class VMSalesInformation
    {
        public SalesOrder SalesOrder { get; set; }
        public List<SalesOrderDetail> lstSalesOrderDetail { get; set; }
        public Expense Expense { get; set; }

        public VMSalesInformation()
        {
            SalesOrder = new SalesOrder();
            lstSalesOrderDetail = new List<SalesOrderDetail>();
            Expense = new Expense();
        }

    }
}
