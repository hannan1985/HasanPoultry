using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
     //[Serializable(), TableMap("Stock")]
        public class Stock
        {
            private int _StockID = 0;
            [PrimaryKey()]
            [DataType("int")]
            public int StockID
            {
                get { return _StockID; }
                set { _StockID = value; }
            }


            private DateTime _StockDate = DateTime.Now;
            //[DataType("DateTime")]
            public DateTime StockDate
            {
                get { return _StockDate; }
                set { _StockDate = value; }
            }

            private int _ProductionUnitID = 0;

            //[DataType("int")]
            public int ProductionUnitID
            {
                get { return _ProductionUnitID; }
                set { _ProductionUnitID = value; }
            }

            private decimal _Total = 0;

            //[DataType("decimal")]
            public decimal Total
            {
                get { return _Total; }
                set { _Total = value; }
            }

            private int _BranchID = 0;

            //[DataType("int")]
            public int BranchID
            {
                get { return _BranchID; }
                set { _BranchID = value; }
            }

            private int _OrganizationID = 0;

            //[DataType("int")]
            public int OrganizationID
            {
                get { return _OrganizationID; }
                set { _OrganizationID = value; }
            }



        }
}
