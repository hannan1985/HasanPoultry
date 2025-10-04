using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{
    [Serializable(), TableMap("SalesCenter")]
    public class SalesCenter
    {

        private int _SalesCetnerID;

        [PrimaryKey()]
        public int SalesCenterID
        {
            get { return _SalesCetnerID; }
            set { _SalesCetnerID = value; }
        }

        private string _SalesCenterName;

        public string SalesCenterName
        {
            get { return _SalesCenterName; }
            set { _SalesCenterName = value; }
        }

        private string _Address;

        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }


        private string _Phone;

        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }



        private string _ResponsiblePerson;

        public string ResponsiblePerson
        {
            get { return _ResponsiblePerson; }
            set { _ResponsiblePerson = value; }
        }

        public int OrganizationID { get; set; }

        public int BranchID { get; set; }
    }
}
