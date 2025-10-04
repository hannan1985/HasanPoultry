using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.View
{
    public class ProductInformationForPurchase
    {
        private string _ProductID = string.Empty;

        [DataType("varchar")]
        public string ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }

        private string _ProecutName = string.Empty;

        [DataType("varchar")]
        public string ProductName
        {
            get { return _ProecutName; }
            set { _ProecutName = value; }
        }

        private string _TypeName = string.Empty;

        [DataType("varchar")]
        public string TypeName
        {
            get { return _TypeName; }
            set { _TypeName = value; }
        }

        public decimal  CartonSize { get; set; }

        public string  Model { get; set; }

        public int OrganizationID { get; set; }

        public int BranchID { get; set; }

        public string  Barcode { get; set; }
    }
}
