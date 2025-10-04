using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    //[Serializable(), TableMap("StockDetails")]
    public class StockSales
    {
        private int _StockSalesID = 0;
        [PrimaryKey()]
        [DataType("int")]
        public int StockSalesID
        {
            get { return _StockSalesID; }
            set { _StockSalesID = value; }
        }


        private DateTime _SalesDate = DateTime.Now;
        //[DataType("int")]
        public DateTime SalesDate
        {
            get { return _SalesDate; }
            set { _SalesDate = value; }
        }

        private int _PartyID = 0;

        //[DataType("int")]
        public int PartyID
        {
            get { return _PartyID; }
            set { _PartyID = value; }
        }

        private decimal _Total = 0;

        //[DataType("decimal")]
        public decimal Total
        {
            get { return _Total; }
            set { _Total = value; }
        }

        private string _SoldBy = string.Empty;

        //[DataType("nvarchar(50)")]
        public string SoldBy
        {
            get { return _SoldBy; }
            set { _SoldBy = value; }
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

        public decimal ReceiveAmount { get; set; }
    }
}
