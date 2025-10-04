using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    //[Serializable(), TableMap("MeterialsReceiveDetails")]
    public class MaterialsReceiveDetails
    {
        private int _MaterialRceiveDetailID = 0;
        [PrimaryKey()]
        [DataType("int")]
        public int MaterialRceiveDetailID
        {
            get { return _MaterialRceiveDetailID; }
            set { _MaterialRceiveDetailID = value; }
        }


        private int _MaterialReceiveID =0;
        //[DataType("int")]
        public int MaterialReceiveID
        {
            get { return _MaterialReceiveID; }
            set { _MaterialReceiveID = value; }
        }

        private int _MaterialID = 0;

        //[DataType("int")]
        public int MaterialID
        {
            get { return _MaterialID; }
            set { _MaterialID = value; }
        }

        private decimal _Price = 0;

        //[DataType("decimal")]
        public decimal Price
        {
            get { return _Price; }
            set { _Price = value; }
        }

        private decimal _Quantity = 0;
        //[DataType("decimal")]
        public decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
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
