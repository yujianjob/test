using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.Web.StoreManage.CodeSource.Common;
using LazyAtHome.WCF.Wash.Contract.ClientProxy;
using LazyAtHome.WCF.Wash.Contract.DataContract;
using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Web.Base;

namespace LazyAtHome.Web.StoreManage.CodeSource.Proxy
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
        /// 获取产品品类列表
        /// </summary>
        /// <returns></returns>
        public static IList<Wash_ClassDC> GetCategorySecondList()
        {
            IList<Wash_ClassDC> list = null;
            try
            {
                list = ProductClient.Wash_Class_SELECT_List_Second();
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ProductCategoryProxy GetCategorySecondList|" + ex.Message);
                return null;
            }

            return list;
        }

        /// <summary>
        /// 根据类别ID获取产品
        /// </summary>
        /// <param name="iClassId"></param>
        /// <returns></returns>
        public static IList<Wash_CategoryDC> GetCategoryListBYClassID(int iClassId)
        {
            if (iClassId <= 0)
            {
                return new List<Wash_CategoryDC>();
            }

            ReturnEntity<IList<Wash_CategoryDC>> re = null;
            IList<Wash_CategoryDC> list = null;
            try
            {
                re = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<IList<Wash_CategoryDC>>>
                    (client => client.Proxy.Wash_Category_SELECT_List_ByClassID(iClassId));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|CategoryProxy GetCategoryListBYClassID|" + ex.Message);
                return null;
            }
            if (re.Success)
            {
                list = re.OBJ;
            }

            return list;
        }


        /// <summary>
        /// 根据产品ID获取运价
        /// </summary>
        /// <param name="iCategoryID"></param>
        /// <returns></returns>
        public static IList<Wash_ProductDC> GetProductListBYCategoryID(int iCategoryID)
        {
            ReturnEntity<IList<Wash_ProductDC>> re = null;
            IList<Wash_ProductDC> list = null;
            try
            {
                re = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<IList<Wash_ProductDC>>>
                    (client => client.Proxy.Wash_Product_SELECT_List_CategoryID(iCategoryID));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|CategoryProxy GetCategoryListBYClassID|" + ex.Message);
                return null;
            }
            if (re.Success)
            {
                list = re.OBJ;
            }

            return list;
        }

    }
}