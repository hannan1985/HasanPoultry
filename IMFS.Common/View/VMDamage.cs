using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;

namespace IMFS.Common.View
{
    public class VMDamage
    {
        public DamageInfo DamageInfo { get; set; }
        public List<DamageDetail> lstDamageDetail { get; set; }

        public VMDamage()
        {
            DamageInfo = new DamageInfo();
            lstDamageDetail = new List<DamageDetail>();
        }

    }
}
