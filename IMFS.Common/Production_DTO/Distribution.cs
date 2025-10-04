using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    //[Serializable(), TableMap("Ditribution")]
    public class Distribution
    {
        private int _DistributeID = 0;
        [PrimaryKey()]
        [DataType("int")]
        public int DistributeID
        {
            get { return _DistributeID; }
            set { _DistributeID = value; }
        }


        private DateTime _DistributeDate = DateTime.Now;
        //[DataType("datetime")]
        public DateTime DistributeDate
        {
            get { return _DistributeDate; }
            set { _DistributeDate = value; }
        }

        private string _DistributBy = string.Empty;

        //[DataType("nvarchar(50)")]
        public string DistributBy
        {
            get { return _DistributBy; }
            set { _DistributBy = value; }
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
