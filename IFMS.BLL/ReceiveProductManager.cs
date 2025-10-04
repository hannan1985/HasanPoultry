using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMFS.Common.DTO;
using NybSys.MTBF.Utility.Enums;
using IMFS.Common.Utility;
using IMFS.Common.View;
using NybSys.MTBF.Utility.Constants;

namespace IFMS.BLL
{
    public class ReceiveProductManager : BllBase
    {
        #region "ReceiveProduct"

        public int SaveReceiveProductInformation(VMReceiveProduct vmReceiveProduct)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            try
            {
                base.DataAccessManager.BeginTransaction();
                if (vmReceiveProduct.ReceiveProduct.ReceiveProductID > 0)
                {
                    base.DataAccessManager.Update<ReceiveProduct>(vmReceiveProduct.ReceiveProduct);
                    string filter = string.Format("{0}={1}", "ReceiveProductID", vmReceiveProduct.ReceiveProduct.ReceiveProductID);
                    List<ReceiveProductDetail> lstExisting = new List<ReceiveProductDetail>();
                    lstExisting = base.DataAccessManager.GetFilteredData<ReceiveProductDetail>(filter).Cast<ReceiveProductDetail>().ToList();
                    foreach (ReceiveProductDetail rDetail in lstExisting)
                    {
                        base.DataAccessManager.Delete<ReceiveProductDetail>(rDetail.ReceiveProductDetailID);
                    }

                    foreach (ReceiveProductDetail receiveDetail in vmReceiveProduct.lstRecevieProductDetail)
                    {
                        receiveDetail.ReceiveProductID = vmReceiveProduct.ReceiveProduct.ReceiveProductID;
                        base.DataAccessManager.Insert<ReceiveProductDetail>(receiveDetail);
                    }
                }
                else
                {
                    vmReceiveProduct.ReceiveProduct.ReceiveProductID = base.DataAccessManager.Insert<ReceiveProduct>(vmReceiveProduct.ReceiveProduct);

                    foreach (ReceiveProductDetail receiveDetail in vmReceiveProduct.lstRecevieProductDetail)
                    {
                        receiveDetail.ReceiveProductID = vmReceiveProduct.ReceiveProduct.ReceiveProductID;
                        base.DataAccessManager.Insert<ReceiveProductDetail>(receiveDetail);
                    }
                }

                string childAccountFilter = string.Format("{0}='{1}' and {2}={3}", "ReferenceType", (int)MTBFEnums.ChildAccountType.SalesCenter, "ReferenceID", vmReceiveProduct.ReceiveProduct.SalesCenterID);
                ChildAccount childAccount = base.DataAccessManager.GetFilteredData<ChildAccount>(childAccountFilter).Cast<ChildAccount>().ToList().FirstOrDefault();


                childAccountFilter = string.Format("{0}={1}", "ReceiveProductID", vmReceiveProduct.ReceiveProduct.ReceiveProductID);
                List<Journal> lstExistingJournal = new List<Journal>();
                lstExistingJournal = base.DataAccessManager.GetFilteredData<Journal>(childAccountFilter).Cast<Journal>().ToList();

                foreach (Journal journal in lstExistingJournal)
                {
                    base.DataAccessManager.Delete<Journal>(journal.JournalID);
                }

                InsertJournalInformationForGoodsReceive(vmReceiveProduct, childAccount);

                base.DataAccessManager.CommitTransaction();
            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
                base.DataAccessManager.Rollback();
            }
            finally
            {
                base.DataAccessManager.EndTransaction();
            }
            return result;
        }

        private void InsertJournalInformationForGoodsReceive(VMReceiveProduct vmReceiveProduct, ChildAccount supplierChildAccount)
        {
            decimal amount = CalculatePurchasePrice(vmReceiveProduct.lstRecevieProductDetail);
            int referenceID = new JournalAccountsManager().GetJournalReferenceID();

            for (int i = 0; i <= 1; i++)
            {
                Journal journal = new Journal();

                if (i == 0)
                {
                    journal.DrCrIndecator = "Dr";
                    journal.AccountID = IFMSConstant.InventoryAccountID;
                }
                else
                {
                    journal.DrCrIndecator = "Cr";
                    journal.AccountID = supplierChildAccount.AccountID;
                    journal.ChildAccountID = supplierChildAccount.ChildAccountID;
                }

                journal.JAccountID = 0;
                journal.Amount = amount;
                journal.ReferenceNo = referenceID;
                journal.ReceiveProductID = vmReceiveProduct.ReceiveProduct.ReceiveProductID;
                journal.BranchID = vmReceiveProduct.ReceiveProduct.BranchID;
                journal.OrganizationID = vmReceiveProduct.ReceiveProduct.OrganizationID;
                base.DataAccessManager.Insert<Journal>(journal);
            }
        }


        private decimal CalculatePurchasePrice(List<ReceiveProductDetail> lstDamageDetail)
        {
            decimal total = 0;
            List<PurchaseOrderDetail> lstPurchaseOrderDetail = new List<PurchaseOrderDetail>();
            string[] productID = lstDamageDetail.Select(i => i.ProductCode).Distinct().ToArray();
            string filter = string.Empty;


            if (productID.Length > 0)
            {
                for (int i = 0; i < productID.Length; i++)
                {
                    if (filter != string.Empty) filter = filter + ",";
                    filter = filter + "'" + productID[i] + "'";
                }

                filter = String.Format("{0} IN ({1})", "ProductID", filter);
                lstPurchaseOrderDetail = base.DataAccessManager.GetFilteredData<PurchaseOrderDetail>(filter).Cast<PurchaseOrderDetail>().ToList();

            }

            List<StockPrice> lstStockPrice = new List<StockPrice>();
            lstStockPrice = base.DataAccessManager.GetFilteredData<StockPrice>(filter).Cast<StockPrice>().ToList();
            foreach (ReceiveProductDetail damageDetail in lstDamageDetail)
            {
                decimal price = CalculateAveragePrice(lstPurchaseOrderDetail.Where(p => p.ProductID == damageDetail.ProductCode).Cast<PurchaseOrderDetail>().ToList());
                if (price == 0)
                {
                    StockPrice stockPrice = lstStockPrice.Where(s => s.ProductID == damageDetail.ProductCode).FirstOrDefault();
                    if (stockPrice != null)
                    {
                        price = stockPrice.Price;
                    }
                }

                total += damageDetail.Quantity * price;
            }

            return total;
        }

        private decimal CalculateAveragePrice(List<PurchaseOrderDetail> lstPurchaseOrderDetail)
        {
            decimal price = 0;
            decimal total = 0;


            foreach (PurchaseOrderDetail detail in lstPurchaseOrderDetail)
            {
                total += detail.PurchasePrice;
            }

            price = (lstPurchaseOrderDetail.Count > 0) ? (total / lstPurchaseOrderDetail.Count) : 0;

            return price;
        }

        public int UpdateReceiveProductInformation(ReceiveProduct receiveProduct, List<ReceiveProductDetail> lstReceiveProductDetail)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            try
            {
                base.DataAccessManager.BeginTransaction();
                base.DataAccessManager.Update<ReceiveProduct>(receiveProduct);
                DeleteAllReceiveProductDetail(receiveProduct);
                InsertReceiveProductDetail(receiveProduct, lstReceiveProductDetail);
                base.DataAccessManager.CommitTransaction();
            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
                base.DataAccessManager.Rollback();
            }
            finally
            {
                base.DataAccessManager.EndTransaction();
            }
            return result;
        }


        private void DeleteAllReceiveProductDetail(ReceiveProduct receiveProduct)
        {
            foreach (ReceiveProductDetail ReceiveProductDetail in GetAllReceiveProductDetailByReceiveProductID(receiveProduct.ReceiveProductID))
            {
                base.DataAccessManager.Delete<ReceiveProductDetail>(ReceiveProductDetail.ReceiveProductDetailID);
            }
        }


        public ReceiveProduct GetReceiveProductByID(int _receiveProductID)
        {
            ReceiveProduct ReceiveProduct = base.DataAccessManager.GetByID<ReceiveProduct>(_receiveProductID, "ReceiveProductID");
            return ReceiveProduct;
        }

        public List<ReceiveProduct> GetAllReceiveProduct()
        {
            return base.DataAccessManager.GetAll<ReceiveProduct>().Cast<ReceiveProduct>().ToList();
        }

        #endregion

        #region "ReceiveProduct Detail"

        private void InsertReceiveProductDetail(ReceiveProduct receiveProduct, List<ReceiveProductDetail> lstReceiveProductDetail)
        {
            foreach (ReceiveProductDetail ReceiveProductDetail in lstReceiveProductDetail)
            {
                ReceiveProductDetail.ReceiveProductID = receiveProduct.ReceiveProductID;
                base.DataAccessManager.Insert<ReceiveProductDetail>(ReceiveProductDetail);

            }
        }

        public int UpdateReceiveProductDetail(ReceiveProductDetail receiveProductDetail)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            try
            {
                base.DataAccessManager.Update<ReceiveProductDetail>(receiveProductDetail);

            }
            catch
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
            }
            return result;
        }

        public ReceiveProductDetail GetReceiveProductDetailByID(int _receiveProductDetailID)
        {
            ReceiveProductDetail ReceiveProductDetail = base.DataAccessManager.GetByID<ReceiveProductDetail>(_receiveProductDetailID, "ReceiveProductDetailID");
            return ReceiveProductDetail;
        }

        public List<ReceiveProductDetail> GetAllReceiveProductDetail()
        {
            return base.DataAccessManager.GetAll<ReceiveProductDetail>().Cast<ReceiveProductDetail>().ToList();
        }

        #endregion



        public List<ReceiveProductDetail> GetAllReceiveProductDetailByReceiveProductID(int receiveProductID)
        {
            string filter = string.Format("{0}={1}", "ReceiveProductID", receiveProductID);
            return base.DataAccessManager.GetFilteredData<ReceiveProductDetail>(filter).Cast<ReceiveProductDetail>().ToList();
        }

        public List<ReceiveProduct> GetReceiveProductBySalesCenterAndDate(int salesCenterID, string fromDate, string toDate)
        {
            string filter = string.Format("{0}={1} And {2} between '{3}' and '{4}'", "SalesCenterID", salesCenterID, "ReceiveProductDate", fromDate, toDate);
            return base.DataAccessManager.GetFilteredData<ReceiveProduct>(filter).Cast<ReceiveProduct>().ToList();
        }

        public decimal GetAllReceiveProductProductByProudctCode(string productCode, int branchID, int organizationID)
        {
            string filter = string.Format(IFMSConstant.QueryConstants.GET_RECEIVE_PRODUCT_BY_PRODUCTID, productCode, branchID, organizationID);
            return Convert.ToDecimal(base.DataAccessManager.ExecuteScalar(filter));
        }

        //public int GetAllTransterProductByProductIDandDAate(string productCode, string fromDate, string toDate)
        //{
        //    string filter = string.Format(IFMSConstant.QueryConstants.GET_ALL_ReceiveProduct_PRODUCT_BY_PRODUCTID_DATE, productCode, fromDate, toDate);
        //    return Convert.ToInt32(base.DataAccessManager.ExecuteScalar(filter));
        //}

        public List<ReceiveProduct> GetAllReceiveProductByBranchAndOrganization(int branchID, int organizationID)
        {
            string filter = string.Format("{0}={1} And {2}={3}", "BranchID", branchID, "OrganizationID", organizationID);
            return base.DataAccessManager.GetFilteredData<ReceiveProduct>(filter).Cast<ReceiveProduct>().ToList();
        }



        public List<ReceiveProduct> GetAllReceiveProductByDate(string fromDate, string toDate)
        {
            string filter = string.Format("{0} between '{1}' And '{2}'", "ReceiveDate", fromDate, toDate);
            return base.DataAccessManager.GetFilteredData<ReceiveProduct>(filter).Cast<ReceiveProduct>().ToList();
        }

        public List<VMReceiveProduct> GetFilteredReceiveProduct(string filter)
        {
            List<VMReceiveProduct> lstVMReceiveProduct = new List<VMReceiveProduct>();
            List<ReceiveProduct> lstReceiveProduct = new List<ReceiveProduct>();
            lstReceiveProduct = base.DataAccessManager.GetFilteredData<ReceiveProduct>(filter).Cast<ReceiveProduct>().ToList();
            foreach (ReceiveProduct receiveProduct in lstReceiveProduct)
            {
                VMReceiveProduct vmReceiveProduct = new VMReceiveProduct();
                vmReceiveProduct.ReceiveProduct = receiveProduct;
                filter = string.Format("{0} ={1}", "ReceiveProductID", receiveProduct.ReceiveProductID);
                vmReceiveProduct.lstRecevieProductDetail = base.DataAccessManager.GetFilteredData<ReceiveProductDetail>(filter).Cast<ReceiveProductDetail>().ToList();
                lstVMReceiveProduct.Add(vmReceiveProduct);
            }

            return lstVMReceiveProduct;

        }

        private int GetMaxProductCode()
        {
            return Convert.ToInt32(base.DataAccessManager.ExecuteScalar("Select  isnull(MAX(CAST(ProductID AS INT)),0)+1 from ProductInformation"));
        }

        private string GenerateProductCode()
        {
            int maxProductCode = GetMaxProductCode();
            string productCode = maxProductCode.ToString().PadLeft(4, '0');
            return productCode;
        }

        public int ImportProduct(VMProductImport vmProductImport)
        {
            int result = (int)MTBFEnums.ReturnResult.SUCCESS;

            List<Product> lstExistingProduct = new List<Product>();

            List<ProductType> lstExistingProductType = new List<ProductType>();
            List<ProductModel> lstExistingProductModel = new List<ProductModel>();
            List<ProductSize> lstExistingProductSize = new List<ProductSize>();


            try
            {
                base.DataAccessManager.BeginTransaction();

                string filter = string.Empty;
                filter = string.Format("{0}={1}", "BranchID", MTBFConstants.AppConstants.BranchID);
                lstExistingProduct = base.DataAccessManager.GetFilteredData<Product>(filter).Cast<Product>().ToList();

                lstExistingProductType = base.DataAccessManager.GetAll<ProductType>().Cast<ProductType>().ToList();
                lstExistingProductSize = base.DataAccessManager.GetAll<ProductSize>().Cast<ProductSize>().ToList();
                lstExistingProductModel = base.DataAccessManager.GetAll<ProductModel>().Cast<ProductModel>().ToList();

                foreach (ProductType productType in vmProductImport.lstProductType)
                {
                    ProductType existing = lstExistingProductType.Where(t => t.TypeName.ToLower().Trim() == productType.TypeName.ToLower().Trim()).FirstOrDefault();
                    if (existing == null)
                    {
                        productType.ProductTypeID = base.DataAccessManager.Insert<ProductType>(productType);
                    }
                    else
                    {
                        productType.ProductTypeID = existing.ProductTypeID;
                    }
                }

                foreach (ProductSize productSize in vmProductImport.lstProductSize)
                {
                    ProductSize existing = lstExistingProductSize.Where(t => t.Name.ToLower().Trim() == productSize.Name.ToLower().Trim()).FirstOrDefault();
                    if (existing == null)
                    {
                        productSize.ProductSizeID = base.DataAccessManager.Insert<ProductSize>(productSize);
                    }
                    else
                    {
                        productSize.ProductSizeID = existing.ProductSizeID;
                    }
                }

                foreach (ProductModel productModel in vmProductImport.lstProductModel)
                {
                    ProductModel existing = lstExistingProductModel.Where(t => t.Name.ToLower().Trim() == productModel.Name.ToLower().Trim()).FirstOrDefault();
                    if (existing == null)
                    {
                        productModel.ProductModelID = base.DataAccessManager.Insert<ProductModel>(productModel);
                    }
                    else
                    {
                        productModel.ProductModelID = existing.ProductModelID;
                    }
                }
                vmProductImport.lstProduct = vmProductImport.lstProduct.OrderBy(p => p.ProductName).ToList();

                foreach (Product product in vmProductImport.lstProduct)
                {
                    Product existing = new Product();
                    if (!string.IsNullOrEmpty(product.BarCode))
                    {
                        existing = lstExistingProduct.Where(t => t.ProductName.ToLower().Trim() == product.ProductName.ToLower().Trim() || t.BarCode.ToLower().Trim() == product.BarCode.ToLower().Trim()).FirstOrDefault();
                    }
                    else
                    {
                        existing = lstExistingProduct.Where(t => t.ProductName.ToLower().Trim() == product.ProductName.ToLower().Trim()).FirstOrDefault();

                    }

                    if (existing == null)
                    {

                        ProductType productType = vmProductImport.lstProductType.Where(t => t.TypeName.ToLower().Trim() == product.TypeName.ToLower().Trim()).FirstOrDefault();
                        ProductSize productSize = vmProductImport.lstProductSize.Where(t => t.Name.ToLower().Trim() == product.SizeName.ToLower().Trim()).FirstOrDefault();
                        ProductModel productModel = vmProductImport.lstProductModel.Where(t => t.Name.ToLower().Trim() == product.ProductModelName.ToLower().Trim()).FirstOrDefault();

                        product.ProductID = GenerateProductCode();
                        product.ProductTypeID = (productType != null) ? productType.ProductTypeID : 0;
                        product.ProductSizeID = (productSize != null) ? productSize.ProductSizeID : 0;
                        product.ProductModelID = (productModel != null) ? productModel.ProductModelID : 0;


                        product.BranchID = MTBFConstants.AppConstants.BranchID;
                        product.OrganizationID = MTBFConstants.AppConstants.OrganizationID;

                        product.SerialNo = base.DataAccessManager.Insert<Product>(product);

                        //lstExistingProduct.Add(existing);
                    }
                    else
                    {
                        product.ProductName = existing.ProductName;
                        product.ProductID = existing.ProductID;
                        product.SerialNo = existing.SerialNo;
                    }
                }

                ReceiveProduct recevieProduct = new ReceiveProduct();
                recevieProduct.ReceiveBy = MTBFConstants.AppConstants.LoggedinUserID;
                recevieProduct.BranchID = MTBFConstants.AppConstants.BranchID;
                recevieProduct.OrganizationID = MTBFConstants.AppConstants.OrganizationID;
                recevieProduct.ReceiveDate = DateTime.Now;
                recevieProduct.UpdatedDate = Convert.ToDateTime(MTBFConstants.DefauldDate);
                recevieProduct.SalesCenterID = 0;

                recevieProduct.ReceiveProductID = base.DataAccessManager.Insert<ReceiveProduct>(recevieProduct);

                 filter=string.Format("{0}='{1}'","BranchID",MTBFConstants.AppConstants.BranchID);

                 List<Product> lstProduct = base.DataAccessManager.GetFilteredData<Product>(filter).Cast<Product>().ToList();

                lstProduct = lstProduct.OrderBy(p => p.ProductName).ToList();
                vmProductImport.lstReceiveProductDetail= vmProductImport.lstReceiveProductDetail.OrderBy(p => p.ProductName).ToList(); 

                foreach (ReceiveProductDetail rDetial in vmProductImport.lstReceiveProductDetail)
                {
                    Product product = lstProduct.Where(p => p.ProductName.Trim().ToLower() == rDetial.ProductName.Trim().ToLower()).FirstOrDefault();
                    if (product != null)
                    {
                        rDetial.ReceiveProductID = recevieProduct.ReceiveProductID;
                        rDetial.ProductCode = product.ProductID;
                        base.DataAccessManager.Insert<ReceiveProductDetail>(rDetial);
                    }
                    else
                    {
                        var a = rDetial;
                    }
                }

             

                foreach (StockPrice stockPrice in vmProductImport.lstStockPrice)
                {
                    Product product = vmProductImport.lstProduct.Where(p => p.ProductName.ToLower().Trim() == stockPrice.ProductName.ToLower().Trim()).FirstOrDefault();
                    if (product != null)
                    {
                        filter = string.Format("{0}='{1}'", "ProductID", product.ProductID);
                        StockPrice existing = base.DataAccessManager.GetFilteredData<StockPrice>(filter).Cast<StockPrice>().FirstOrDefault();
                        if (existing != null)
                        {
                            existing.Price = stockPrice.Price;
                            existing.UpdateDate = DateTime.Now;
                            existing.UpdatedBy = MTBFConstants.AppConstants.LoggedinUserID;
                            base.DataAccessManager.Update<StockPrice>(existing);
                        }
                        else
                        {
                            stockPrice.ProductID = product.ProductID;
                            stockPrice.CreatedDate = DateTime.Now;
                            stockPrice.CreatedBy = MTBFConstants.AppConstants.LoggedinUserID;
                            stockPrice.UpdateDate = Convert.ToDateTime(MTBFConstants.DefauldDate);
                            base.DataAccessManager.Insert<StockPrice>(stockPrice);
                        }
                    }
                }




                base.DataAccessManager.CommitTransaction();
            }
            catch (Exception ex)
            {
                result = (int)MTBFEnums.ReturnResult.FAILED;
                base.DataAccessManager.Rollback();
            }
            finally
            {
                base.DataAccessManager.EndTransaction();
            }

            return result;
        }
    }
}
