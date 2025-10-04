using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IMFS.Common.DTO;

namespace Tiles_Inventory
{
    public partial class frmShowMessage : BaseForm 
    {
        int _messageID = 0;
        public frmShowMessage(int messageID)
        {
            _messageID = messageID;
            DataAccessProxy = new IFMS.Facade.FacadeManager();
            InitializeComponent();
        }

        private void frmShowMessage_Load(object sender, EventArgs e)
        {
            DeliveryMessages deliveryMessage = DataAccessProxy.GetDeliveryMessagesByID(_messageID);
            if (deliveryMessage != null)
            {
                txtMessageDescription.Text = deliveryMessage.Description;
            }
        }
    }
}
