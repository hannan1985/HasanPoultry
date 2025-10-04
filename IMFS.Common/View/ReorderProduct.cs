using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.View
{
    public class ReorderProduct
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

        private string _PackSize = string.Empty;

        [DataType("varchar")]
        public string PackSize
        {
            get { return _PackSize; }
            set { _PackSize = value; }
        }

        private int _Quantity = 0;

        [DataType("int")]
        public int Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }


        private int _ReorderQuantity = 0;

        [DataType("int")]
        public int ReorderQuantity
        {
            get { return _ReorderQuantity; }
            set { _ReorderQuantity = value; }
        }

        //private string _CompanyName = string.Empty;

        //[DataType("varchar")]
        //public string CompanyName
        //{
        //    get { return _CompanyName; }
        //    set { _CompanyName = value; }
        //}

        public string  SupplierName { get; set; }

    }
}
