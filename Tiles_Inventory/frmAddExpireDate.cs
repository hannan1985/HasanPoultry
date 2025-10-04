using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IMFS.Common.DTO;
using IFMS.BLL;
using NybSys.MTBF.Utility.Enums;
using NybSys.MTBF.Utility.Helper;

namespace Tiles_Inventory
{
    public partial class frmAddExpireDate : Form
    {
        int _PurcahseOrderDetailID = 0;
        string _productName = string.Empty;
        public frmAddExpireDate(int PurcahseOrderDetailID, string productName)
        {
            _productName = productName;
            _PurcahseOrderDetailID = PurcahseOrderDetailID;
            InitializeComponent();
        }

        private void ultraGroupBox2_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (UpdatePurcaseOrderDetail() == (int)MTBFEnums.ReturnResult.SUCCESS)
            {
                MessageBoxHelper.ShowInformation("Successfully Saved");
                this.Close();
            }
            else
            {
                MessageBoxHelper.ShowInformation("Failed to save information.");
            }
        }
        private int UpdatePurcaseOrderDetail()
        {
            PurchaseOrderDetail pDetail = new PurchaseOrderDetail();
            pDetail = new PurchaseManager().GetPurchaseOrderDetailByID(_PurcahseOrderDetailID);
            pDetail.ExpireDate = dtpExpireDate.Value;
            return new PurchaseManager().UpdatePurchaseOrderDetail(pDetail);
        }

        private void frmAddExpireDate_Load(object sender, EventArgs e)
        {
            txtProductName.Text = _productName;
        }
    }
}
