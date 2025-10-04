using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IMFS.Common.View;
using NybSys.MTBF.Utility.Constants;
using IFMS.BLL;

namespace Tiles_Inventory
{
    public partial class frmViewProductConsumed : BaseForm
    {
        List<VMProductUsed> _lstVMProductUsed = new List<VMProductUsed>();

        public frmViewProductConsumed()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {

        }



        private DataTable BuildConsumedTable()
        {
            DataTable table = new DataTable();        
            table.Columns.Add("Used Date");
            table.Columns.Add("Sales Center");
            table.Columns.Add("Received By");
            table.Columns.Add("Is Edited");
            return table;

        }

        private DataTable BuildConsumedDetailTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Product Name");
            table.Columns.Add("Quantity");
            table.Columns.Add("Square Feet");

            return table;

        }
        private void LoadProductConsumed()
        {
            string filter=string.Format ("{0} between '{1}' and '{2}'","UsedDate",dtpFromDate.Value.ToString("yyyy/MM/dd")+MTBFConstants.DAY_START_TIME,dtpToDate.Value.ToString("yyyy/MM/dd")+MTBFConstants.DAY_END_TIME);
            _lstVMProductUsed = new ProductUsedManager().GetFilteredProductUsed(filter);
            DataTable dt = BuildConsumedTable();
            foreach (VMProductUsed vmUserd in _lstVMProductUsed)
            {
                DataRow row = dt.NewRow();
                
            }


        }
    }
}
