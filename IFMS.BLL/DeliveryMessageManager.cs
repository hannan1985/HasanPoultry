using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Enums;

namespace IFMS.BLL
{
    public class DeliveryMessageManager : BllBase
    {
        public int InsertDeliveryMessages(DeliveryMessages deliveryMessages)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                base.DataAccessManager.Insert<DeliveryMessages>(deliveryMessages);
            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }


        public int UpdateDeliveryMessages(DeliveryMessages deliveryMessages)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                base.DataAccessManager.Update<DeliveryMessages>(deliveryMessages);
            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }


        public DeliveryMessages GetDeliveryMessagesByID(int deliveryMessagesID)
        {
            DeliveryMessages deliveryMessages = base.DataAccessManager.GetByID<DeliveryMessages>(deliveryMessagesID, "MessageID");
            return deliveryMessages;
        }

        public List<DeliveryMessages> GetAllDeliveryMessages()
        {
            return base.DataAccessManager.GetAll<DeliveryMessages>().Cast<DeliveryMessages>().ToList();
        }


        public List<DeliveryMessages> GetAllDeliveryMessagesByDateBranchAndOrganization(string fromDate, string toDate, int branchID, int organizationID)
        {
            string filter = string.Format("{0} between '{1}' AND '{2}' And BranchID={3} and OrganizationID={4}", "SendingDate", fromDate, toDate, branchID, organizationID);
            return base.DataAccessManager.GetFilteredData<DeliveryMessages>(filter).Cast<DeliveryMessages>().ToList();
        }
    }
}
