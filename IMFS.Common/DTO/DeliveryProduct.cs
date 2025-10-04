using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    //[Serializable, TableMap("DeliveryProduct")]
    public class DeliveryProduct 
    {
        private int _DeliveryID = 0;

        [PrimaryKey()]
        [DataType("int")]
        public int DeliveryID
        {
            get { return _DeliveryID; }
            set { _DeliveryID = value; }
        }

     

        private int _SalesID = 0;

        [DataType("int")]
        public int SalesID
        {
            get { return _SalesID; }
            set { _SalesID = value; }
        }


        private DateTime _DeliveryDate = DateTime.Now;

        [DataType("date")]
        public DateTime DeliveryDate
        {
            get { return _DeliveryDate; }
            set { _DeliveryDate = value; }
        }

        private int _CustomerID = 0;

        [DataType("int")]
        public int CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }


        private string _Description = string.Empty;

        [DataType("nvarchar")]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        private decimal _Total = 0;

        [DataType("numeric")]
        public decimal Total
        {
            get { return _Total; }
            set { _Total = value; }
        }

        public int Status { get; set; }



        public string CustomerName { get; set; }

        public string Phone { get; set; }
    }
}
