using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    //[Serializable(), TableMap("Meterials")]
    public class Materials
    {
        private int _MaterialID = 0;
        [PrimaryKey()]
        [DataType("int")]
        public int MaterialID
        {
            get { return _MaterialID; }
            set { _MaterialID = value; }
        }


        private string _MaterialName = string.Empty;
        //[DataType("nvarchar(50)")]
        public string MaterialName
        {
            get { return _MaterialName; }
            set { _MaterialName = value; }
        }


        public string Size { get; set; }
        public string Origin { get; set; }
        public string VendorName { get; set; }
        public int ReorderQuantity { get; set; }
        public byte[] ReceipeCost { get; set; }




        private int _BranchID = 0;

        //[DataType("int")]
        public int BranchID
        {
            get { return _BranchID; }
            set { _BranchID = value; }
        }

        private int _OrgaizationID = 0;

        //[DataType("int")]
        public int OrganizationID
        {
            get { return _OrgaizationID; }
            set { _OrgaizationID = value; }
        }




    }
}
