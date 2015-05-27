using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.Web.WebManage.CodeSource.Common;
using LazyAtHome.WCF.Wash.Contract.DataContract;
using LazyAtHome.WCF.Wash.Contract.ClientProxy;
using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Web.Base;

namespace LazyAtHome.Web.WebManage.CodeSource.Proxy
{
    /// <summary>
    /// 运价wcf代理类
    /// </summary>
    public class ProductProxy
    {
        /// <summary>
        /// 获取产品列表
        /// </summary>
        /// <param name="iName"></param>
        /// <param name="iCode"></param>
        /// <param name="iType"></param>
        /// <param name="iCommitStatus"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<Wash_ProductDC> GetProductList(string iName, string iCode, int? iType, int? iCommitStatus, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            RecordCountEntity<Wash_ProductDC> rtn = null;
            ReturnEntity<RecordCountEntity<Wash_ProductDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<RecordCountEntity<Wash_ProductDC>>>
                    (client => client.Proxy.Wash_Product_SELECT_List(iName, iCode, iType, iCommitStatus, iStartDate, iEndDate, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ProductProxy GetProductList|" + ex.Message);
            }
            if (rce != null && rce.Success)
            {
                rtn = rce.OBJ;
            }
            return rtn;
        }


        /// <summary>
        /// 新增运价信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ReturnEntity<int> AddProduct(Wash_ProductDC entity)
        {
            ReturnEntity<int> re = null;

            try
            {
                re = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<int>>
                   (client => client.Proxy.Wash_Product_ADD(entity));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ProductProxy AddProduct|" + ex.Message);
            }
            return re;
        }


        /// <summary>
        /// 根据ID获取运价信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ReturnEntity<Wash_ProductDC> GetProductBYID(int id)
        {
            ReturnEntity<Wash_ProductDC> re = null;
            try
            {

                re = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<Wash_ProductDC>>
                   (client => client.Proxy.Wash_Product_SELECT_Entity(id));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ProductProxy GetProductBYID|" + ex.Message);
            }
            return re;

        }

        /// <summary>
        /// 根据运价id获取已经选的属性配置
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public static ReturnEntity<IList<Wash_ProductAttributeDC>> GetProductAttributeBYProductID(int ProductID)
        {
            ReturnEntity<IList<Wash_ProductAttributeDC>> re = null;
            try
            {
                re = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<IList<Wash_ProductAttributeDC>>>
                   (client => client.Proxy.Wash_ProductAttribute_SELECT(ProductID));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ProductProxy GetProductAttributeBYProductID|" + ex.Message);
            }
            return re;

        }


        /// <summary>
        /// 更新产品信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> UpdateProduct(Wash_ProductDC entity)
        {
            ReturnEntity<bool> re = null;
            try
            {

                re = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Wash_Product_UPDATE(entity));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ProductProxy UpdateProduct|" + ex.Message);
            }
            return re;
        }
    }
}