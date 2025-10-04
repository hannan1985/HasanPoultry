using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Enums;
using NybSys.MTBF.Utility.Helper;
using NybSys.MTBF.Utility.Constants;
using IFMS.Facade;

namespace Tiles_Inventory
{
    public partial class frmDistributeSample : BaseForm
    {
        private int _distributionID = 0;
        private bool isUpdate = false;

        public frmDistributeSample()
        {
            DataAccessProxy = new FacadeManager();
            InitializeComponent();
        }

        public frmDistributeSample(int distributionID, bool isEdit)
        {
            isUpdate = isEdit;
            _distributionID = distributionID;
            DataAccessProxy = new FacadeManager();
            InitializeComponent();
        }


        private void frmDistributeSample_Load(object sender, EventArgs e)
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
            txtQuantity.Text = distributeSample.GivenQuantity.ToString();
            dtpDistributeDate.Value = distributeSample.DistributeDate;
        }


        /// <summary>
        /// Method to load seller information in combo box.
        /// </summary>
        private void LoadSellerInformation()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Seller ID");
            table.Columns.Add("Seller Name");

            foreach (Seller seller in DataAccessProxy.GetAllSeller().Where(s => s.BranchID == MTBFConstants.AppConstants.BranchID && s.OrganizationID == MTBFConstants.AppConstants.OrganizationID))
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
            //lstProductInfo = RemoveAllGivenSample(lstProductInfo);

            //lstProductInfo = RemoveTransferProduct(lstProductInfo);
            //lstProductInfo = RemoveDamageProduct(lstProductInfo);

            cmbProductInformation.DataSource = lstProductInfo;
            cmbProductInformation.DisplayMember = "ProductName";
            cmbProductInformation.ValueMember = "ProductId";
            cmbProductInformation.DisplayLayout.Bands[0].Columns[0].Hidden = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidFormData())
            {
                if (isUpdate)
                {
                    if (UpdateDistributionInformation() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Distribution information saved successfully");
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save distribution information .");
                    }
                }
                else
                {
                    if (InsertDistributionInformation() == (int)MTBFEnums.ReturnResult.SUCCESS)
                    {
                        MessageBoxHelper.ShowInformation("Distribution information saved successfully");
                    }
                    else
                    {
                        MessageBoxHelper.ShowInformation("Failed to save distribution information .");
                    }
                }
            }
        }

        private int UpdateDistributionInformation()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            DistributeSample distributeSample = DataAccessProxy.GetDistributeSampleByID(_distributionID);
            distributeSample.GivenQuantity = Convert.ToDecimal(txtQuantity.Text);
            distributeSample.DistributeDate = dtpDistributeDate.Value;
            distributeSample.SellerID = Convert.ToInt32(cmbSellerName.Value);
            distributeSample.ProductID = cmbProductInformation.Value.ToString();
            result = DataAccessProxy.UpdateDistributeSample(distributeSample);

            return result;
        }


        private List<ProductInformationForSales> RemoveTransferProduct(List<ProductInformationForSales> lstProductInfo)
        {
            List<ProductInformationForSales> lstFilteredProductInfo = new List<ProductInformationForSales>();
            foreach (ProductInformationForSales productifo in lstProductInfo)
            {
                decimal transferProduct = DataAccessProxy.GetAllTransferProductByProudctCode(productifo.ProductID, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID);
                productifo.Quantity = (productifo.Quantity - transferProduct);
                lstFilteredProductInfo.Add(productifo);
            }

            return lstFilteredProductInfo;
        }

        /// <summary>
        /// Method to remove damage product.
        /// </summary>
        /// <param name="lstProductInfo"></param>
        /// <returns></returns>
        private List<ProductInformationForSales> RemoveDamageProduct(List<ProductInformationForSales> lstProductInfo)
        {
            List<ProductInformationForSales> lstFilteredProductInfo = new List<ProductInformationForSales>();
            foreach (ProductInformationForSales productifo in lstProductInfo)
            {
                decimal transferProduct = DataAccessProxy.GetAllDamageProductByProudctCode(productifo.ProductID, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID);
                productifo.Quantity = (productifo.Quantity - transferProduct);
                lstFilteredProductInfo.Add(productifo);
            }

            return lstFilteredProductInfo;
        }

        /// <summary>
        /// Method to deduct all given sample to stock.
        /// </summary>
        /// <param name="lstProductInfo"></param>
        /// <returns></returns>
        private List<ProductInformationForSales> RemoveAllGivenSample(List<ProductInformationForSales> lstProductInfo)
        {

            List<ProductInformationForSales> lstFilteredProductInfo = new List<ProductInformationForSales>();

            foreach (ProductInformationForSales productifo in lstProductInfo)
            {
                decimal givenSample = DataAccessProxy.GetAllGivenSampleByProductID(productifo.ProductID, MTBFConstants.AppConstants.BranchID, MTBFConstants.AppConstants.OrganizationID);
                productifo.Quantity = (productifo.Quantity - givenSample);
                lstFilteredProductInfo.Add(productifo);

            }
            return lstFilteredProductInfo;

        }



        /// <summary>
        /// Method to insert distribution information.
        /// </summary>
        private int InsertDistributionInformation()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            DistributeSample distributeSample = new DistributeSample();
            distributeSample.GivenQuantity = Convert.ToDecimal(txtQuantity.Text);
            distributeSample.DistributeDate = dtpDistributeDate.Value;
            distributeSample.SellerID = Convert.ToInt32(cmbSellerName.Value);
            distributeSample.ProductID = cmbProductInformation.Value.ToString();
            distributeSample.BranchID = MTBFConstants.AppConstants.BranchID;
            distributeSample.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            result = DataAccessProxy.InsertDistributeSample(distributeSample);

            return result;
        }

        /// <summary>
        /// Method to valid form data.
        /// </summary>
        /// <returns></returns>
        private bool ValidFormData()
        {
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

            return true;
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
