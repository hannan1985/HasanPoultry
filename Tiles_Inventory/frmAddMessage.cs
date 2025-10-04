using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NybSys.MTBF.Utility.Enums;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Constants;
using NybSys.MTBF.Utility.Helper;


namespace Tiles_Inventory
{
    public partial class frmAddMessage : BaseForm
    {
        private int _messageID = 0;

        public frmAddMessage()
        {
            DataAccessProxy = new IFMS.Facade.FacadeManager();
            InitializeComponent();
        }

        public frmAddMessage(int messageID, bool isEdit)
        {
            _messageID = messageID;
            IsUpdating = isEdit;
            DataAccessProxy = new IFMS.Facade.FacadeManager();
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsUpdating)
            {
                if (UpdateMessage() == (int)MTBFEnums.ReturnResult.SUCCESS)
                {
                    MessageBoxHelper.ShowInformation("Message successfully sent.");
                    txtMessageDescription.Clear();
                    IsUpdating = false;
                    _messageID = 0;
                }
                else
                {
                    MessageBoxHelper.ShowInformation("Message sending failed.");
                }
            }
            else
            {
                if (InsertMessage () == (int)MTBFEnums.ReturnResult.SUCCESS)
                {
                    MessageBoxHelper.ShowInformation("Message successfully sent.");
                    txtMessageDescription.Clear();
                }
                else
                {
                    MessageBoxHelper.ShowInformation("Message sending failed.");
                }
            }
        }


        private int InsertMessage()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            DeliveryMessages deliveryManages = new DeliveryMessages();

            deliveryManages.Description = txtMessageDescription.Text;
            deliveryManages.SendingDate = DateTime.Now;
            deliveryManages.Sender = MTBFConstants.AppConstants.LoggedinUser.UserID;
            deliveryManages.SenderName = MTBFConstants.AppConstants.LoggedinUser.Name;
            deliveryManages.BranchID = MTBFConstants.AppConstants.BranchID;
            deliveryManages.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            result = DataAccessProxy.InsertDeliveryMessages(deliveryManages);

            return result;
        }


        private int UpdateMessage()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            DeliveryMessages deliveryManages = DataAccessProxy.GetDeliveryMessagesByID(_messageID);

            deliveryManages.Description = txtMessageDescription.Text;
            deliveryManages.ModificationDate = DateTime.Now;
            deliveryManages.ModifiedBy = MTBFConstants.AppConstants.LoggedinUser.Name;
            result = DataAccessProxy.UpdateDeliveryMessages(deliveryManages);

            return result;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtMessageDescription.Clear();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddMessage_Load(object sender, EventArgs e)
        {
            if (IsUpdating)
            {
                DeliveryMessages deliveryMessage = DataAccessProxy.GetDeliveryMessagesByID(_messageID);
                if (deliveryMessage != null)
                {
                    txtMessageDescription.Text = deliveryMessage.Description;
                }
            }
        }




    }
}
