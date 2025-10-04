using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Helper;
using NybSys.MTBF.Utility.Constants;
using NybSys.MTBF.Utility.Enums;
using IFMS.Facade;

namespace Tiles_Inventory
{
    public partial class frmReceiveSample : BaseForm
    {
        private int _distributionID = 0;
        private bool isUpdate = false;

        public frmReceiveSample()
        {
            DataAccessProxy = new FacadeManager();
            InitializeComponent();
        }

        public frmReceiveSample(int distributionID, bool isEdit)
        {
            isUpdate = isEdit;
            _distributionID = distributionID;
            DataAccessProxy = new FacadeManager();
            InitializeComponent();
        }

        private void frmReceiveSample_Load(object sender, EventArgs e)
        {
            LoadProductInformation();
            LoadSellerInformation();
            if (isUpdate)
            {
                LoadExistingDistributionInformaiton();

            }
        }

        private void LoadExistingDistributionInformaiton()
        {
            DistributeSample distributeSample = DataAccessProxy.GetDistributeSampleByID(_distributionID);
            cmbSellerName.Value = distributeSample.SellerID;
            cmbProductInformation.Value = distributeSample.ProductID;
            txtGivenQuantity.Text = distributeSample.GivenQuantity.ToString();
            txtQuantity.Text = distributeSample.ReceiveQuantity.ToString();
            dtpReceiveDate.Value = distributeSample.ReceiveDate;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidFormData())
            {
                if (isUpdate)
                {
                    if (UpdateDistributionInformation() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Receive information saved successfully");
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save receive information .");
                    }
                }
            }


        }

        /// <summary>
        /// Method to update distribution information``
        /// </summary>
        /// <returns></returns>
        private int UpdateDistributionInformation()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            DistributeSample distributeSample = DataAccessProxy.GetDistributeSampleByID(_distributionID);
            distributeSample.ReceiveQuantity = Convert.ToDecimal(txtQuantity.Text);
            distributeSample.ReceiveDate = dtpReceiveDate.Value;
            distributeSample.SellerID = Convert.ToInt32(cmbSellerName.Value);
            result = DataAccessProxy.UpdateDistributeSample(distributeSample);

            return result;
        }

        /// <summary>
        /// Method to insert distribution information.
        /// </summary>
        private int InsertDistributionInformation()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            DistributeSample distributeSample = new DistributeSample();
            distributeSample.ReceiveQuantity = Convert.ToDecimal(txtQuantity.Text);
            distributeSample.ReceiveDate = dtpReceiveDate.Value;
            distributeSample.SellerID = Convert.ToInt32(cmbSellerName.Value);
            result = DataAccessProxy.InsertDistributeSample(distributeSample);

            return result;
        }

        /// <summary>
        /// Method to valid form data.
        /// </summary>
        /// <returns></returns>
        private bool ValidFormData()
        {
            decimal givenQty = 0;
            decimal receiveQty = 0;

            decimal.TryParse(txtGivenQuantity.Text, out givenQty);
            decimal.TryParse(txtQuantity.Text, out receiveQty);

            if (cmbSellerName.Value == null && Convert.ToInt32(cmbSellerName.Value) == MTBFConstants.DataField.COMBO_DEFAULT_ID)
            {
                MessageBoxHelper.ShowInformation("You need to select seller name.");
                cmbSellerName.Focus();
                return false;
            }

            if (cmbProductInformation.Value == null && cmbProductInformation.Value == MTBFConstants.DataField.COMBO_DEFULT_CODE)
            {
                MessageBoxHelper.ShowInformation("You need to select product name.");
                cmbProductInformation.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtQuantity.Text))
            {
                MessageBoxHelper.ShowInformation("You need to enter quantity.");
                txtQuantity.Focus();
                return false;
            }


            if (receiveQty > givenQty)
            {
                MessageBoxHelper.ShowInformation("Receive quantity can't be gather  than given quantity.");
                txtQuantity.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Method to load seller information in combo box.
        /// </summary>
        private void LoadSellerInformation()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Seller ID");
            table.Columns.Add("Seller Name");

            foreach (Seller seller in DataAccessProxy.GetAllSeller())
            {
                DataRow row = table.NewRow();
                row["Seller ID"] = seller.SellerID;
                row["Seller Name"] = seller.SellerName;
                table.Rows.Add(row);
            }

            cmbSellerName.DataSource = table;
            cmbSellerName.DisplayMember = "Seller Name";
            cmbSellerName.ValueMember = "Seller ID";

        }

        /// <summary>
        /// Metho to load product information in combo box.
        /// </summary>
        private void LoadProductInformation()
        {
            List<ProductInformationForSales> lstProductInfo = new List<ProductInformationForSales>();
            lstProductInfo = DataAccessProxy.GetAllProductForSales(MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID).Cast<ProductInformationForSales>().ToList();

            cmbProductInformation.DataSource = lstProductInfo;
            cmbProductInformation.DisplayMember = "ProductName";
            cmbProductInformation.ValueMember = "ProductId";
            cmbProductInformation.DisplayLayout.Bands[0].Columns[0].Hidden = true;
        }

        private void cmbSellerName_ValueChanged(object sender, EventArgs e)
        {
            if (cmbSellerName.Value != null)
            {
                Seller seller = DataAccessProxy.GetSellerByID(Convert.ToInt32(cmbSellerName.Value));
                if (seller != null)
                {
                    txtAddress.Text = seller.Address;
                    txtPhone.Text = seller.Phone;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
