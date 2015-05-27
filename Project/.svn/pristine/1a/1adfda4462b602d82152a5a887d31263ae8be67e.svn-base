using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Helper;
using LazyAtHome.WCF.Wash.Contract.ClientProxy;
using LazyAtHome.WCF.Wash.Contract.DataContract.weixin;


namespace LazyAtHome.Web.WeiXin.App_Code.Proxy
{
    public class WashProxy
    {
        public void aaa()
        {
        }


        /// <summary>
        /// 获取产品类别
        /// </summary>
        /// <returns></returns>
        public static ReturnEntity<IList<wx_Wash_ClassDC>> Select_ProductClass()
        {
            var rtn = WCFInvokeHelper<wx_ProductClient>.Invoke<ReturnEntity<IList<wx_Wash_ClassDC>>>(c => c.Proxy.wx_Wash_Class_SELECT_List());
            return rtn;
        }

        /// <summary>
        /// 根据产品类别获取产品列表
        /// </summary>
        /// <param name="iClassID">产品类别ID</param>
        /// <returns></returns>
        public static ReturnEntity<IList<wx_Wash_CategoryDC>> Select_ProductList(int iClassID)
        {
            var rtn = WCFInvokeHelper<wx_ProductClient>.Invoke<ReturnEntity<IList<wx_Wash_CategoryDC>>>(c => c.Proxy.wx_Wash_Category_SELECT_List(iClassID, 1));
            return rtn;
        }

        /// <summary>
        /// 根据产品ID获取详情
        /// </summary>
        /// <param name="iProductID"></param>
        /// <returns></returns>
        public static ReturnEntity<wx_Wash_CategoryDC> Select_ProductInfo(int iProductID)
        {
            var rtn = WCFInvokeHelper<wx_ProductClient>.Invoke<ReturnEntity<wx_Wash_CategoryDC>>(c => c.Proxy.wx_Wash_Category_SELECT_Entity(iProductID));
            return rtn;
        }
    }
}