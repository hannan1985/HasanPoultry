using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    //[Serializable(), TableMap("MeterialsReceive")]
    public class MaterialsReceive
    {
        private int _MaterialReceiveID = 0;
        [PrimaryKey()]
        [DataType("int")]
        public int MaterialReceiveID
        {
            get { return _MaterialReceiveID; }
            set { _MaterialReceiveID = value; }
        }


        private DateTime _ReceiveDate = DateTime.Now;
        //[DataType("DateTime")]
        public DateTime ReceiveDate
        {
            get { return _ReceiveDate; }
            set { _ReceiveDate = value; }
        }

        private int _SupplierID = 0;

        //[DataType("int")]
        public int SupplierID
        {
            get { return _SupplierID; }
            set { _SupplierID = value; }
        }

        private decimal _Total = 0;

        //[DataType("decimal")]
        public decimal Total
        {
            get { return _Total; }
            set { _Total = value; }
        }

        private decimal _PaidAmount = 0;
        //[DataType("decimal")]
        public decimal PaidAmount
        {
            get { return _PaidAmount; }
            set { _PaidAmount = value; }
        }

        private string _ReeivedBy = string.Empty;

        //[DataType("nvarchar(50)")]
        public string ReeivedBy
        {
            get { return _ReeivedBy; }
            set { _ReeivedBy = value; }
        }

        public int Status { get; set; }

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
