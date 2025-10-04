using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tiles_Inventory
{
    public  class AppUtil
    {
        public static void ShowLoader()
        {
            frmShowProgress frm = new frmShowProgress();
            frm.ShowDialog();

        }
    }
}
