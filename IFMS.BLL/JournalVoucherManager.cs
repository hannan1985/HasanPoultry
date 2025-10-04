using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Enums;
using NybSys.MTBF.Utility.Constants;
using NybSys.MTBF.Utility.Helper;
namespace IFMS.BLL
{
    public class JournalVoucherManager : BllBase
    {

        public int SaveJournalVouhcer(List<JournalVoucher> lstJournalVoucher)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            foreach (JournalVoucher jvoucher in lstJournalVoucher)
            {
                if (jvoucher.JournalVoucherID > 0)
                {
                    jvoucher.UpdatedBy = MTBFConstants.AppConstants.LoggedinUserID;
                    jvoucher.UpdatedDate = DateTime.Now;
                    base.DataAccessManager.Update<JournalVoucher>(jvoucher);
                }
                else
                {
                    jvoucher.CreatedBy = MTBFConstants.AppConstants.LoggedinUserID;
                    jvoucher.CreatedDate = DateTime.Now;
                    jvoucher.UpdatedDate = Convert.ToDateTime(MTBFConstants.DefauldDate);
                    base.DataAccessManager.Insert<JournalVoucher>(jvoucher);
                }
            }


            return result;

        }


        public List<JournalVoucher> GetFilteredJournalVoucher(string filter)
        {
            return base.DataAccessManager.GetFilteredData<JournalVoucher>(filter).Cast<JournalVoucher>().ToList();
        }

        public string GetVoucherNo()
        {
            string voucherNumber = string.Empty;
            string sql = "Select isnull(Max(VoucherNumber),10000000)+ 1  from JournalVoucher where BranchID=" + MTBFConstants.AppConstants.BranchID;
            voucherNumber =  Convert.ToInt32(base.DataAccessManager.ExecuteScalar(sql)).ToString().PadLeft(8, '0');
            return voucherNumber;
        }
    }
}
