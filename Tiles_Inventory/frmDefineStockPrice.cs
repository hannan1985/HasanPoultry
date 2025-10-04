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
using IFMS.BLL;
using NybSys.MTBF.Utility.Helper;

namespace Tiles_Inventory
{
    public partial class frmDefineStockPrice : Form
    {
        private Product _product = new Product();
        private decimal _oldPrice = 0;

        public event LoadDefineStockPriceHandler LoadStockPrice;
        public delegate void LoadDefineStockPriceHandler();

        public frmDefineStockPrice(Product product, decimal oldPrice)
        {
            _oldPrice = oldPrice;
            _product = product;       
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (InsertStockPrice() == (int)MTBFEnums.ReturnResult.SUCCESS)
            {
                MessageBoxHelper.ShowInformation("Information saved successfully.");
                ResetAllControls();
                if (LoadStockPrice != null)
                    LoadStockPrice();
            }
            else
            {
                MessageBoxHelper.ShowInformation("Failed to save information.");
            }
        }

        private void ResetAllControls()
        {
            txtNewPrice.Clear();
        }

        private int InsertStockPrice()
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            StockPrice stockPrice = new StockPrice();
            stockPrice.ProductID = _product.ProductID;
            stockPrice.OldPrice = decimal.Parse(txtOldPrice.Text);
            stockPrice.Price = decimal.Parse(txtNewPrice.Text);
            stockPrice.CreatedBy = MTBFConstants.AppConstants.LoggedinUser.UserID;
            stockPrice.CreatedDate = DateTime.Now;
            stockPrice.UpdateDate = Convert.ToDateTime(MTBFConstants.DefauldDate);
            stockPrice.BranchID = MTBFConstants.AppConstants.BranchID;
            stockPrice.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
            
            result = new StockPriceManager().InsertStockPrice(stockPrice);

            return result;
        }

        private void frmDefineStockPrice_Load(object sender, EventArgs e)
        {
            txtRoleName.Text = _product.ProductName;
            txtOldPrice.Text = _oldPrice.ToString();
        }
        
    }
}
