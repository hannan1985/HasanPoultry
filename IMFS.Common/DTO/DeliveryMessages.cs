using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkX.ObjectSpace.Data.Schema;

namespace IMFS.Common.DTO
{

    public class DeliveryMessages
    {
        [PrimaryKey()]
        public int MessageID { get; set; }

        public string Description { get; set; }

   

        public string Sender { get; set; }

        public string SenderName { get; set; }

        public string ModifiedBy { get; set; }

        private DateTime _SendingDate = DateTime.Now;

        public DateTime SendingDate
        {
            get { return _SendingDate; }
            set { _SendingDate = value; }
        }


        private DateTime _ModificationDate = DateTime.Now;

        public DateTime ModificationDate
        {
            get { return _ModificationDate; }
            set { _ModificationDate = value; }
        }



        public int BranchID { get; set; }

        public int OrganizationID { get; set; }
    }
}
