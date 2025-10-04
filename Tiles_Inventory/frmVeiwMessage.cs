using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Constants;
using NybSys.MTBF.Utility.Helper;

namespace Tiles_Inventory
{
    public partial class frmVeiwMessage : BaseForm
    {
        public frmVeiwMessage()
        {
            DataAccessProxy = new IFMS.Facade.FacadeManager();
            InitializeComponent();
        }

        private void btView_Click(object sender, EventArgs e)
        {
            LoadAllMessageByDate(dtpFromDate.Value.ToString("yyyy/MM/dd"), dtpToDate.Value.ToString("yyyy/MM/dd"));
        }


        private void LoadAllMessageByDate(string fromDate, string toDate)
        {
            fromDate = fromDate + " 00:00:01";
            toDate = toDate + " 23:59:59";

            List<DeliveryMessages> lstDeliveryMessage = new List<DeliveryMessages>();
            lstDeliveryMessage = DataAccessProxy.GetAllDeliveryMessagesByDateBranchAndOrganization(fromDate, toDate, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID);

            DataTable messageTable = BuildMessageTable();
            foreach (DeliveryMessages message in lstDeliveryMessage)
            {
                DataRow row = messageTable.NewRow();
                row["MessageID"] = message.MessageID;
                row["Description"] = message.Description;
                row["Sending Date"] = message.SendingDate.ToString("dd/MM/yyyy");
                row["Sender"] = message.SenderName;
                messageTable.Rows.Add(row);

            }
            grvMessagesInfo.DataSource = messageTable;
        }


        private DataTable BuildMessageTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("MessageID");
            table.Columns.Add("Description");
            table.Columns.Add("Sending Date");
            table.Columns.Add("Sender");
            return table;
        }


        private void btAdd_Click(object sender, EventArgs e)
        {
            frmAddMessage frm = new frmAddMessage();
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            if (grvMessagesInfo.Selected.Rows.Count > 0 && grvMessagesInfo.Selected.Rows[0].Cells["MessageID"].Value != null)
            {

                int messageID = Convert.ToInt32(grvMessagesInfo.Selected.Rows[0].Cells["MessageID"].Value);
                frmAddMessage frm = new frmAddMessage(messageID, true);
                frm.ShowDialog();
            }
            else
            {
                MessageBoxHelper.ShowInformation("You need to select a row.");
            }
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            LoadAllMessageByDate(dtpFromDate.Value.ToString("yyyy/MM/dd"), dtpToDate.Value.ToString("yyyy/MM/dd"));
        }

        private void frmVeiwMessage_Load(object sender, EventArgs e)
        {
            grvMessagesInfo.DataSource = BuildMessageTable();
            base.CheckAction(this);
        }

        private void btShowMessage_Click(object sender, EventArgs e)
        {
            if (grvMessagesInfo.Selected.Rows.Count > 0 && grvMessagesInfo.Selected.Rows[0].Cells["MessageID"].Value != null)
            {

                int messageID = Convert.ToInt32(grvMessagesInfo.Selected.Rows[0].Cells["MessageID"].Value);
                frmShowMessage frm = new frmShowMessage(messageID);
                frm.ShowDialog();
            }
            else
            {
                MessageBoxHelper.ShowInformation("You need to select a row.");
            }
        }


    }
}
