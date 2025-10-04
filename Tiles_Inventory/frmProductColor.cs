using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NybSys.MTBF.Utility.Resources;
using NybSys.MTBF.Utility.Helper;
using IMFS.Common.DTO;
using IFMS.Facade;

namespace Tiles_Inventory
{
    public partial class frmProductColor : BaseForm
    {

        public event LoadProductTypeEventHandler LoadProductColor;
        public delegate void LoadProductTypeEventHandler();

        public frmProductColor()
        {
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        private void btnSave_Click(System.Object sender, System.EventArgs e)
        {
            ProductColor productColor = new ProductColor();
            productColor.Name = txtColorName.Text;
            if (IsValidFormData())
            {
                if (DataAccessProxy.InsertProductColor(productColor) > 0)
                {
                    MessageBoxHelper.ShowInformation("Product color saved successfully.");
                    if (LoadProductColor != null)
                    {
                        LoadProductColor();
                    }
                }
            }
        }

        private void btnClose_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Method to check duplicate record.
        /// </summary>
        /// <returns></returns>
        private bool IsValidFormData()
        {

            if (string.IsNullOrEmpty(txtColorName.Text))
            {
                MessageBoxHelper.ShowInformation("You need to enter color name");
                return false;
            }

            ProductColor productColor = DataAccessProxy.GetAllProductColor().Cast<ProductColor>().Where(s => s.Name.ToUpper().Trim() == txtColorName.Text.ToUpper().Trim()).ToList().FirstOrDefault();

            if (productColor != null)
            {
                MessageBoxHelper.ShowInformation("Duplicate product color information");
                return false;
            }
            return true;
        }

    }
}
