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
    /// 产品属性wcf代理类
    /// </summary>
    public class ProductAttributeProxy
    {
        /// <summary>
        /// 获取产品属性列表
        /// </summary>
        /// <returns></returns>
        public static IList<Wash_AttributeDC> GetAttributeFirstList()
        {
            return new List<Wash_AttributeDC>();
            //IList<Wash_AttributeDC> list = null;
            //try
            //{
            //    list = ProductClient.Wash_Attribute_SELECT_List_First();
            //}
            //catch (Exception ex)
            //{
            //    WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ProductAttributeProxy GetAttributeFirstList|" + ex.Message);
            //    return null;
            //}

            //return list;
        }


        /// <summary>
        /// 获取产品属性明细列表
        /// </summary>
        /// <returns></returns>
        public static IList<Wash_AttributeDC> GetAttributeSecondList(int parentID)
        {
            return new List<Wash_AttributeDC>();

            //IList<Wash_AttributeDC> list = null;
            //try
            //{
            //    list = ProductClient.Wash_Attribute_SELECT_List_Second(parentID);
            //}
            //catch (Exception ex)
            //{
            //    WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ProductAttributeProxy GetAttributeSecondList|" + ex.Message);
            //    return null;
            //}

            //return list;
        }


        /// <summary>
        /// 新增产品属性
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ReturnEntity<int> AddProductAttribute(Wash_AttributeDC entity)
        {
            ReturnEntity<int> re = null;

            try
            {

                re = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<int>>
                   (client => client.Proxy.Wash_Attribute_ADD(entity));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ProductAttributeProxy AddProductCategory|" + ex.Message);
            }
            return re;
        }

        /// <summary>
        /// 根据ID获取产品属性
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ReturnEntity<Wash_AttributeDC> GetProductAttributeBYID(int id)
        {
            ReturnEntity<Wash_AttributeDC> re = null;
            try
            {

                re = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<Wash_AttributeDC>>
                   (client => client.Proxy.Wash_Attribute_SELECT_Entity(id));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ProductAttributeProxy GetProductCategoryBYID|" + ex.Message);
            }
            return re;

        }

        /// <summary>
        /// 更新产品属性
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> UpdateProductAttribute(Wash_AttributeDC entity)
        {
            ReturnEntity<bool> re = null;
            try
            {

                re = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Wash_Attribute_UPDATE(entity));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ProductAttributeProxy UpdateProductCategory|" + ex.Message);
            }
            return re;
        }


    }
}