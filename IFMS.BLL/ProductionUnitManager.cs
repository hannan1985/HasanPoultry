using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Enums;

namespace IFMS.BLL
{
    public class ProductionUnitManager : BllBase
    {
        public int InsertProductionUnit(ProductionUnit pUnit)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                base.DataAccessManager.Insert<ProductionUnit>(pUnit);

            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }


        public int UpdateProductionUnit(ProductionUnit pUnit)
        {

            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                base.DataAccessManager.Update<ProductionUnit>(pUnit);

            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }


        public ProductionUnit GetProductionUnitByID(int _branchID)
        {
            ProductionUnit ProductionUnit = base.DataAccessManager.GetByID<ProductionUnit>(_branchID, "UnitID");
            return ProductionUnit;
        }

        public List<ProductionUnit> GetAllProductionUnit()
        {
            return base.DataAccessManager.GetAll<ProductionUnit>().Cast<ProductionUnit>().ToList();
        }

        public List<ProductionUnit> GetFilteredProductionUnit(string filter)
        {
            return base.DataAccessManager.GetFilteredData<ProductionUnit>(filter).Cast<ProductionUnit>().ToList();
        }
    }
}
