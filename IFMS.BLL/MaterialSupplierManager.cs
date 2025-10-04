using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Enums;
using IMFS.Common.Utility;
using NybSys.MTBF.Utility.Constants;

namespace IFMS.BLL
{
    public class MaterialSupplierManager : BllBase
    {
        public int InsertMaterialSupplier(MaterialSupplier materialSupplier)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            int supplierID = 0;
            try
            {
                base.DataAccessManager.BeginTransaction();

                supplierID = base.DataAccessManager.Insert<MaterialSupplier>(materialSupplier);

                materialSupplier.SupplierID = supplierID;

                InsertCustomerChildAccount(materialSupplier);

                base.DataAccessManager.CommitTransaction();

            }
            catch
            {
                base.DataAccessManager.Rollback();
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            finally
            {
                base.DataAccessManager.EndTransaction();
            }

            return result;
        }


        private void InsertCustomerChildAccount(MaterialSupplier supplier)
        {
            ChildAccount childAccount = new ChildAccount();
            childAccount.AccountID = IFMSConstant.AccountPayableID;
            childAccount.Description = supplier.SupplierName;

            childAccount.MaterailSupplierID = supplier.SupplierID;
            childAccount.CreatedBy = MTBFConstants.AppConstants.LoggedinUser.UserID;
            childAccount.CreatedDate = DateTime.Now;
            childAccount.Status = 1;

            base.DataAccessManager.Insert<ChildAccount>(childAccount);
        }



        public int UpdateMaterialSupplier(MaterialSupplier meterials)
        {

            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                base.DataAccessManager.Update<MaterialSupplier>(meterials);

            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }


        public MaterialSupplier GetMaterialSupplierByID(int _meterialsID)
        {
            MaterialSupplier Meterials = base.DataAccessManager.GetByID<MaterialSupplier>(_meterialsID, "SupplierID");
            return Meterials;
        }

        public List<MaterialSupplier> GetAllMaterialSupplier()
        {
            return base.DataAccessManager.GetAll<MaterialSupplier>().Cast<MaterialSupplier>().ToList();
        }


        public List<MaterialSupplier> GetAllMaterialSupplierByOrganizationID(int organizationID)
        {
            string filter = string.Format("{0}={1}", "OrganizationID", organizationID);
            return base.DataAccessManager.GetFilteredData<MaterialSupplier>(filter).Cast<MaterialSupplier>().ToList();

        }

        public List<MaterialSupplier> GetFilteredMaterialSupplier(string filter)
        {
            return base.DataAccessManager.GetFilteredData<MaterialSupplier>(filter).Cast<MaterialSupplier>().ToList();
        }

    }
}
