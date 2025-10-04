using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;
using IMFS.Common.Utility;
using IMFS.Common.View;
using NybSys.MTBF.Utility.Constants;
using NybSys.MTBF.Utility.Enums;

namespace IFMS.BLL
{
    public class ProductManager : BllBase
    {

        public int InsertProduct(Product product)
        {
            try
            {
                product.ProductID = GenerateProductCode();
                int result = base.DataAccessManager.Insert<Product>(product);
                return result;
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Method to get max product code.
        /// </summary>
        /// <returns></returns>
        private int GetMaxProductCode()
        {
            return Convert.ToInt32(base.DataAccessManager.ExecuteScalar("Select  isnull(MAX(CAST(ProductID AS INT)),0)+1 from ProductInformation"));
        }

        /// <summary>
        /// Method to generate prodcut code.
        /// </summary>
        /// <returns></returns>
        private string GenerateProductCode()
        {
            int maxProductCode = GetMaxProductCode();
            string productCode = maxProductCode.ToString().PadLeft(4, '0');
            return productCode;
        }

        public int UpdateProduct(Product product)
        {
            try
            {
                int result = base.DataAccessManager.Update<Product>(product);
                return result;
            }
            catch
            {
                throw;
            }
        }


        public System.Collections.IList GetAllProduct()
        {
            return base.DataAccessManager.GetAll<Product>();
        }


        public System.Collections.IList GetAllProduct(int branchID, int organizationID)
        {
            string filter = string.Format("{0}={1} and {2}={3}", "BranchID", branchID, "OrganizationID", organizationID);
            return base.DataAccessManager.GetFilteredData<Product>(filter);
        }
        public List<ProductInformationForSales> GetAllProductForSales(int branchID, int organizationID)
        {
            string query = string.Format(IFMSConstant.QueryConstants.PRODUCT_INFORMATION_FOR_SALES, branchID, organizationID);
            return base.DataAccessManager.ExecuteSQL<ProductInformationForSales>(query).Cast<ProductInformationForSales>().ToList();
        }

        public decimal GetSalesPriceByProductID(string productID)
        {
            decimal salesPrice = 0;
            string sql = "Select isnull(Price,0) SalesPrice from StockPrice Where ProductID ='" + productID + "'";
            salesPrice = Convert.ToDecimal(base.DataAccessManager.ExecuteScalar(sql));
            if (salesPrice == 0)
            {
                string query = string.Format(IFMSConstant.QueryConstants.GetSalesPrice, productID);
                salesPrice = Convert.ToDecimal(base.DataAccessManager.ExecuteScalar(query));
            }
            return salesPrice;
        }

        public Product GetProductInformationByID(string productID)
        {
            return base.DataAccessManager.GetByCode<Product>(productID, "ProductID");
        }

        public List<ProductInformationForPurchase> GetProductInformationforPurchase(int branchID)
        {
            string query = string.Empty; //string.Format(IFMSConstant.QueryConstants.GetProductInformationBySupplier, supplierID);

            query = @"Select ProductId ,ProductName,pm.Name as Model,t.TypeName ,p.BranchID ,p.OrganizationID, p.Barcode   from ProductInformation p
                    join TypeInformation t
                    on t.ProductTypeID =p.ProductTypeId
                    left outer join ProductModel pm on pm.ProductModelID =p.ProductModelID 
                    Where p.BranchID={0} and  p.Discountinued='false'";

            query = string.Format(query, branchID);
            return base.DataAccessManager.ExecuteSQL<ProductInformationForPurchase>(query).Cast<ProductInformationForPurchase>().ToList();

        }

        public List<ProductInformationForPurchase> GetProductInformationForReturnByCompanyID(int companyID)
        {
            string query = string.Format(IFMSConstant.QueryConstants.GetProductInformationForReturn, companyID);
            return base.DataAccessManager.ExecuteSQL<ProductInformationForPurchase>(query).Cast<ProductInformationForPurchase>().ToList();

        }


        public List<ProductType> GetAllProductType()
        {
            return base.DataAccessManager.GetAll<ProductType>().Cast<ProductType>().ToList();
        }

        public int GetMaxProductSerialNumber()
        {
            return Convert.ToInt32(base.DataAccessManager.ExecuteScalar(IFMSConstant.QueryConstants.GetMaxProductSerialNumber));
        }

        public ProductType GetProductTypeByName(string productTypeName)
        {
            return base.DataAccessManager.GetByCode<ProductType>(productTypeName, "TypeName");
        }

        public int InsertProductType(ProductType productType)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                base.DataAccessManager.Insert(productType);
            }
            catch (Exception)
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }

        public ProductType GetProductTypeByID(int productTypeID)
        {
            return base.DataAccessManager.GetByID<ProductType>(productTypeID, "ProductTypeID");
        }

        public System.Collections.IList GetStockDifferences()
        {
            return base.DataAccessManager.ExecuteSQL<Product>(IFMSConstant.QueryConstants.StockDifference).Cast<StockDifference>().ToList();
        }

        public System.Collections.IList GetAllExpireProduct()
        {
            return base.DataAccessManager.ExecuteSQL<ExpireProduct>(IFMSConstant.QueryConstants.ExpireProduct).Cast<ExpireProduct>().ToList();
        }

        public List<Inventroy> GetAllInventroyInformation(int branchID, int organizationID)
        {
            string filter = string.Format(IFMSConstant.QueryConstants.InventoryReport, branchID);
            return base.DataAccessManager.ExecuteSQL<Inventroy>(filter).Cast<Inventroy>().ToList();
        }

        /// <summary>
        /// Metod to get product by name.
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        public List<Product> GetProductByName(string productName)
        {
            string filter = string.Format(MTBFConstants.QueryConstants.GET_PRODUCT_BY_NAME, productName.Replace("'", "''"));
            return base.DataAccessManager.GetFilteredData<Product>(filter).Cast<Product>().ToList();
        }


        #region "Product Size"

        /// <summary>
        /// Method to insert product type.
        /// </summary>
        /// <param name="productSize"></param>
        /// <returns></returns>
        public int InsertProductSize(ProductSize productSize)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                this.DataAccessManager.Insert<ProductSize>(productSize);
            }
            catch (Exception exp)
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;

        }

        /// <summary>
        /// Method to update product type.
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public int UpdateProductSize(ProductSize productSize)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                this.DataAccessManager.Update<ProductSize>(productSize);
            }
            catch (Exception exp)
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }

        /// <summary>
        /// Method to get all product type information.
        /// </summary>
        /// <returns></returns>
        public List<ProductSize> GetAllProductSize()
        {
            List<ProductSize> lstProductSize = this.DataAccessManager.GetAll<ProductSize>().Cast<ProductSize>().ToList();
            return lstProductSize;
        }

        /// <summary>
        /// Method to get product size by id.
        /// </summary>
        /// <param name="productModelID"></param>
        /// <returns></returns>
        public ProductSize GetProductSizeByID(int productSizeID)
        {
            return base.DataAccessManager.GetByID<ProductSize>(productSizeID, MTBFConstants.DataField.PRODUCT_SIZE_ID);
        }

        #endregion

        #region "Product Model"

        /// <summary>
        /// Method to insert product type.
        /// </summary>
        /// <param name="productModel"></param>
        /// <returns></returns>
        public int InsertProductModel(ProductModel productModel)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                this.DataAccessManager.Insert<ProductModel>(productModel);
            }
            catch (Exception exp)
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }

        /// <summary>
        /// Method to update product type.
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public int UpdateProductModel(ProductModel productModel)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                this.DataAccessManager.Update<ProductModel>(productModel);
            }
            catch (Exception exp)
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }

        /// <summary>
        /// Method to get all product type information.
        /// </summary>
        /// <returns></returns>
        public List<ProductModel> GetAllProductModel()
        {
            List<ProductModel> lstProductModel = this.DataAccessManager.GetAll<ProductModel>().Cast<ProductModel>().ToList();
            return lstProductModel;
        }

        /// <summary>
        /// Method to get product model by id.
        /// </summary>
        /// <param name="productSizeID"></param>
        /// <returns></returns>
        public ProductModel GetPrductModelByID(int productModelID)
        {
            return base.DataAccessManager.GetByID<ProductModel>(productModelID, MTBFConstants.DataField.PRODUCT_MODEL_ID);
        }

        #endregion

        #region "Product Color"

        /// <summary>
        /// Method to insert product type.
        /// </summary>
        /// <param name="productColor"></param>
        /// <returns></returns>
        public int InsertProductColor(ProductColor productColor)
        {
            try
            {
                int result = this.DataAccessManager.Insert<ProductColor>(productColor);
                return result;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// Method to update product type.
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public int UpdateProductColor(ProductColor productColor)
        {
            try
            {
                int result = this.DataAccessManager.Update<ProductColor>(productColor);
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
        public List<ProductColor> GetAllProductColor()
        {
            List<ProductColor> lstProductColor = this.DataAccessManager.GetAll<ProductColor>().Cast<ProductColor>().ToList();
            return lstProductColor;
        }


        /// <summary>
        /// Method to get product model by id.
        /// </summary>
        /// <param name="productSizeID"></param>
        /// <returns></returns>
        public ProductColor GetProductColorByID(int productColorID)
        {
            return base.DataAccessManager.GetByID<ProductColor>(productColorID, MTBFConstants.DataField.PRODUCT_COLOR_ID);
        }

        #endregion

        public List<Product> GetAllProductByProductTypeID(int productTypeID)
        {
            string filter = string.Format("{0}={1}", IFMSConstant.PRODUCT_TYPE_ID, productTypeID);
            return base.DataAccessManager.GetFilteredData<Product>(filter).Cast<Product>().ToList();
        }

        public List<Product> GetAllProductByProductModel()
        {
            string filter = string.Format(IFMSConstant.QueryConstants.GET_ALL_PRODUCT_BY_MODEL);
            return base.DataAccessManager.ExecuteSQL<Product>(filter).Cast<Product>().ToList();
        }

        public Product GetProductByID(string productID)
        {
            return base.DataAccessManager.GetByCode<Product>(productID, "ProductID");
        }

        public List<StockDetailInformation> GetAllStockDetailInformationByDate(string stockDate, int branchID, int organizatonID)
        {
            string filter = string.Format(IFMSConstant.QueryConstants.PREVIOUS_STOCK_QUANTITY, stockDate, branchID, organizatonID);
            return base.DataAccessManager.ExecuteSQL<StockDetailInformation>(filter).Cast<StockDetailInformation>().ToList();
        }

        public List<ProductType> GetFilteedProductType(string filter)
        {
            return base.DataAccessManager.GetFilteredData<ProductType>(filter).Cast<ProductType>().ToList();
        }

        public List<ProductModel> GetFilteedProductModel(string filter)
        {
            return base.DataAccessManager.GetFilteredData<ProductModel>(filter).Cast<ProductModel>().ToList();
        }

        public List<Product> GetFilteredProduct(string filter)
        {
            string sql = string.Format(filter);
            return base.DataAccessManager.GetFilteredData<Product>(filter).Cast<Product>().ToList();
        }

        public List<Product> GetAllProductByBranchAndOrganizationID(int branchID, int organizationID)
        {
            string filter = string.Format("{0}={1} And {2}={3}", "BranchID", branchID, "OrganizationID", organizationID);
            return base.DataAccessManager.GetFilteredData<Product>(filter).Cast<Product>().ToList();
        }

        public Product GetProductByBarCode(string barCode)
        {
            string filter = string.Format("{0}='{1}'", "BarCode", barCode);
            return base.DataAccessManager.GetFilteredData<Product>(filter).Cast<Product>().ToList().FirstOrDefault();
        }

        public int UpdateProductType(ProductType productType)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;
            try
            {
                base.DataAccessManager.Update<ProductType>(productType);
            }
            catch (Exception)
            {

                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;

        }


    }
}
