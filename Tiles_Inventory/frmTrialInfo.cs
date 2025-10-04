using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IMFS.Common.Utility;
using IMFS.Common.DTO;
using IFMS.Facade;


namespace Tiles_Inventory
{
    public partial class frmTrialInfo : BaseForm 
    {
        public frmTrialInfo()
        {
            InitializeComponent();
            DataAccessProxy = new FacadeManager();
        }

        private int SaveTrialInformation()
        {
            int result = (int)IFMSEnum.ReturnResult.Success;
            IFMSEncription objEncryptDecrypt = new IFMSEncription();
            string strTrialPeriod = objEncryptDecrypt.Encrypt(txtTialPeriod.Text.Trim());
            string strCheckPeriod = objEncryptDecrypt.Encrypt(0.ToString());
            string strCheckDate = objEncryptDecrypt.Encrypt(System.DateTime.Now.ToString("yyyy/MM/dd"));
            TrialPeriodInformation trialPeriod = new TrialPeriodInformation();
            trialPeriod = DataAccessProxy.GetTrialPeriod();

            if (trialPeriod != null)
            {
                trialPeriod.TrialPeriod = strTrialPeriod;
                trialPeriod.CheckPeriod = strCheckPeriod;
                trialPeriod.CheckDate = strCheckDate;
                DataAccessProxy.UpdateTrialPeriod(trialPeriod);
            }
            else
            {
                trialPeriod = new TrialPeriodInformation();
                trialPeriod.TrialPeriod = strTrialPeriod;
                trialPeriod.CheckPeriod = strCheckPeriod;
                trialPeriod.CheckDate = strCheckDate;
                DataAccessProxy.InsertTrialPeriod(trialPeriod);
            }

            return result;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (SaveTrialInformation() == (int)IFMSEnum.ReturnResult.Success)
                {
                    MessageBox.Show("Saved Successfully", "Pharmacy Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                
                throw;
            }

        }
    }
}
