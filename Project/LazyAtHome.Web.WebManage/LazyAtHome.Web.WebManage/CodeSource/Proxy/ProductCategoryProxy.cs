using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.Web.WebManage.CodeSource.Common;
using LazyAtHome.WCF.Wash.Contract.ClientProxy;
using LazyAtHome.WCF.Wash.Contract.DataContract;
using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Web.Base;

namespace LazyAtHome.Web.WebManage.CodeSource.Proxy
{
    /// <summary>
    /// 产品品类wcf代理类
    /// </summary>
    public class ProductCategoryProxy
    {

        /// <summary>
        /// 获取产品分类列表
        /// </summary>
        /// <returns></returns>
        public static IList<Wash_ClassDC> GetCategoryFirstList()
        {
            IList<Wash_ClassDC> list = null;
            try
            {
                list = ProductClient.Wash_Class_SELECT_List_First();
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ProductCategoryProxy GetCategoryFirstList|" + ex.Message);
                return null;
            }

            return list;
        }


        /// <summary>
        /// 获取产品品类列表
        /// </summary>
        /// <returns></returns>
        public static IList<Wash_ClassDC> GetCategorySecondList(int parentID)
        {
            IList<Wash_ClassDC> list = null;
            try
            {
                list = ProductClient.Wash_Class_SELECT_List_Second(parentID);
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ProductCategoryProxy GetCategorySecondList|" + ex.Message);
                return null;
            }

            return list;
        }


        /// <summary>
        /// 新增产品分类或品类
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ReturnEntity<int> AddProductCategory(Wash_ClassDC entity)
        {
            ReturnEntity<int> re = null;

            try
            {

                re = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<int>>
                   (client => client.Proxy.Wash_Class_ADD(entity));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ProductCategoryProxy AddProductCategory|" + ex.Message);
            }
            return re;
        }

        /// <summary>
        /// 根据ID获取产品分类或品类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ReturnEntity<Wash_ClassDC> GetProductCategoryBYID(int id)
        {
            ReturnEntity<Wash_ClassDC> re = null;
            try
            {

                re = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<Wash_ClassDC>>
                   (client => client.Proxy.Wash_Class_SELECT_Entity(id));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ProductCategoryProxy GetProductCategoryBYID|" + ex.Message);
            }
            return re;

        }

        /// <summary>
        /// 更新产品分类或品类
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> UpdateProductCategory(Wash_ClassDC entity)
        {
            ReturnEntity<bool> re = null;
            try
            {

                re = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Wash_Class_UPDATE(entity));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ProductCategoryProxy UpdateProductCategory|" + ex.Message);
            }
            return re;
        }



    }
}