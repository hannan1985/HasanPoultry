using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NybSys.MTBF.Utility.Enums;

using NybSys.MTBF.Utility.Constants;
using IMFS.Common.DTO;


namespace IFMS.BLL
{
    public class ProductDeliveryMnager : BllBase
    {
        #region "Delivery Product"

        /// <summary>
        /// Method to insert product type.
        /// </summary>
        /// <param name="productDelivery"></param>
        /// <returns></returns>
        public int InsertDeliveryProduct(DeliveryProduct productDelivery, List<DeliveryProductDetail> lstDeliveryProductDetail)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            int DeliveryProductID = 0;

            try
            {
                this.DataAccessManager.BeginTransaction();
                DeliveryProductID = this.DataAccessManager.Insert<DeliveryProduct>(productDelivery);
                productDelivery.DeliveryID = DeliveryProductID;
                InsertDeliveryProductDetail(productDelivery, lstDeliveryProductDetail);
                this.DataAccessManager.CommitTransaction();
            }
            catch (Exception exp)
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
                this.DataAccessManager.Rollback();
            }
            finally
            {
                this.DataAccessManager.EndTransaction();
            }

            return result;
        }

        public List<DeliveryProduct> GetDeliveryProductBySalesID(int salesID)
        {
            string filter = string.Format("{0}='{1}'", "SalesID", salesID);
            return base.DataAccessManager.GetFilteredData<DeliveryProduct>(filter).Cast<DeliveryProduct>().ToList();
        }

        /// <summary>
        /// Method to update product type.
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public int UpdateDeliveryProduct(DeliveryProduct productDelivery, List<DeliveryProductDetail> lstDeliveryProductDetail)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                base.DataAccessManager.BeginTransaction();

                this.DataAccessManager.Update<DeliveryProduct>(productDelivery);

                List<DeliveryProductDetail> lstDeleteDeliveryDetail = GetAllDeliveryProductDetailByDeliveryID(productDelivery.DeliveryID);
                DeleteDeliveryProductDetail(lstDeleteDeliveryDetail);

                InsertDeliveryProductDetail(productDelivery, lstDeliveryProductDetail);

                base.DataAccessManager.CommitTransaction();
            }
            catch (Exception exp)
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

        /// <summary>
        /// Method to get product type by id.
        /// </summary>
        /// <param name="productModelID"></param>
        /// <returns></returns>
        public DeliveryProduct GetDeliveryProductByID(int productDeliveryID)
        {
            return base.DataAccessManager.GetByID<DeliveryProduct>(productDeliveryID, "DeliveryID");
        }


        /// <summary>
        /// Method to get all product Delivery information by product code.
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        public List<DeliveryProduct> GetDeliveryProductByProductCode(string productCode)
        {
            string filter = string.Format("{0}='{1}' ", MTBFConstants.DataField.PRODUCT_CODE, productCode);
            List<DeliveryProduct> lstDeliveryProduct = new List<DeliveryProduct>();

            foreach (DeliveryProductDetail item in base.DataAccessManager.GetFilteredData<DeliveryProductDetail>(filter).Cast<DeliveryProductDetail>().ToList())
            {
                DeliveryProduct DeliveryProduct = lstDeliveryProduct.Cast<DeliveryProduct>().Where(r => r.DeliveryID == item.DeliveryProductID).ToList().FirstOrDefault();
                if (DeliveryProduct == null)
                {
                    DeliveryProduct = GetDeliveryProductByID(item.DeliveryProductID);
                    lstDeliveryProduct.Add(DeliveryProduct);
                }

            }
            return lstDeliveryProduct;
        }
        #endregion

        #region "Delivery Product Detail"

        /// <summary>
        /// Method to insert product Delivery detail information.
        /// </summary>
        /// <param name="productDeliveryDetail"></param>
        /// <returns></returns>
        private void InsertDeliveryProductDetail(DeliveryProduct productDelivery, List<DeliveryProductDetail> lstDeliveryProductDetail)
        {
            bool isDelivered = true;

            foreach (DeliveryProductDetail productDeliveryDetail in lstDeliveryProductDetail)
            {
                productDeliveryDetail.DeliveryProductID = productDelivery.DeliveryID;
                if (productDeliveryDetail.Due > 0)
                {
                    isDelivered = false;
                }

                this.DataAccessManager.Insert<DeliveryProductDetail>(productDeliveryDetail);
            }


            if (isDelivered)
            {
                SalesOrder salesOrder = base.DataAccessManager.GetByID<SalesOrder>(productDelivery.SalesID, "SalesID");
                salesOrder.Status = (int)MTBFEnums.DeliveryStatus.Delivered;
                base.DataAccessManager.Update<SalesOrder>(salesOrder);
            }
        }

        /// <summary>
        /// Method to delete all lst  product Delivery informaiton.
        /// </summary>
        /// <param name="lstDeliveryProductDetail"></param>
        private void DeleteDeliveryProductDetail(List<DeliveryProductDetail> lstDeliveryProductDetail)
        {
            foreach (DeliveryProductDetail productDeliveryDetail in lstDeliveryProductDetail)
            {
                this.DataAccessManager.Delete<DeliveryProductDetail>(productDeliveryDetail.DeliveryProductDetailID);
            }
        }


        /// <summary>
        /// Method to update product type.
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public int UpdateDeliveryProductDetail(DeliveryProductDetail productDeliveryDetail)
        {
            try
            {
                int result = this.DataAccessManager.Update<DeliveryProductDetail>(productDeliveryDetail);
                return result;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// Method to get all product type information.
        /// </summary>
        /// <returns></returns>
        public List<DeliveryProductDetail> GetAllDeliveryProductDetailByDeliveryID(int productDeliveryID)
        {
            string filter = string.Format("{0}={1}", "DeliveryProductID", productDeliveryID);
            List<DeliveryProductDetail> lstProductDeliveryDetail = this.DataAccessManager.GetFilteredData<DeliveryProductDetail>(filter).Cast<DeliveryProductDetail>().ToList();
            return lstProductDeliveryDetail;
        }

        /// <summary>
        /// Method to get product type by id.
        /// </summary>
        /// <param name="productModelID"></param>
        /// <returns></returns>
        public DeliveryProductDetail GetDeliveryProductDetailByID(int productDeliveryDetailID)
        {
            return base.DataAccessManager.GetByID<DeliveryProductDetail>(productDeliveryDetailID, "DeliveryProductDetailID");
        }



        public List<DeliveryProductDetail> GetAllDeliveryProductDetailByDeliveryIDAndProductCode(int deliveryProductID, string productCode)
        {
            string filter = string.Format("{0}={1} And {2}='{3}' ", "DeliveryProductID", deliveryProductID, "ProductID", productCode);
            return base.DataAccessManager.GetFilteredData<DeliveryProductDetail>(filter).Cast<DeliveryProductDetail>().ToList();
        }

        #endregion

        public List<DeliveryProduct> GetDeliveryProductByDate(string fromDate, string toDate)
        {
            string filter = string.Format("{0} between '{1}' And '{2}' ", "DeliveryDate", fromDate, toDate);
            return base.DataAccessManager.GetFilteredData<DeliveryProduct>(filter).Cast<DeliveryProduct>().ToList();
        }
    }
}
