using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Infrastructure.WCF.Client;
using LazyAtHome.WCF.Wash.Contract.DataContract;
using LazyAtHome.WCF.Wash.Contract.DataContract.web;
using LazyAtHome.WCF.Wash.Contract.InterfaceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Wash.Contract.ClientProxy
{
    public class web_ProductClient : ClientProxyBase<IwebProduct>
    {

        /// <summary>
        /// Key取缓存
        /// </summary>
        /// <param name="iKey"></param>
        /// <returns></returns>
        public static object Cache_GetByKey(string iKey)
        {
            return LazyAtHome.Core.Helper.CacheHelper.Get(iKey);
        }

        /// <summary>
        /// 获取所有小类
        /// </summary>
        /// <returns></returns>
        public static ReturnEntity<IList<web_Wash_ClassDC>> Cache_web_Wash_Class_SELECT_List()
        {
            var list = Cache_GetByKey("WEB_Wash_Class_List") as IList<web_Wash_ClassDC>;
            if (list == null)
            {
                return WCFInvokeHelper<web_ProductClient>.Invoke<ReturnEntity<IList<web_Wash_ClassDC>>>
                    (client => client.Proxy.web_Wash_Class_SELECT_List());
            }
            else
            {
                return new ReturnEntity<IList<web_Wash_ClassDC>>(list);
            }
        }

        /// <summary>
        /// 获取所有小类
        /// </summary>
        /// <returns></returns>
        public static ReturnEntity<IList<web_Wash_ClassDC>> Cache_web_Wash_Class_Second_SELECT_List()
        {
            var list = Cache_GetByKey("WEB_Wash_Class_List") as IList<web_Wash_ClassDC>;
            if (list == null)
            {
                var rtn = WCFInvokeHelper<web_ProductClient>.Invoke<ReturnEntity<IList<web_Wash_ClassDC>>>
                    (client => client.Proxy.web_Wash_Class_SELECT_List());
                if (rtn != null && rtn.OBJ != null)
                {
                    list = rtn.OBJ;
                }
            }
            if (list != null)
            {
                list = list.Where(p => p.ParentID != 0).ToList();

            }
            return new ReturnEntity<IList<web_Wash_ClassDC>>(list);
        }

        /// <summary>
        /// 根据小类ID获取产品列表
        /// </summary>
        /// <returns></returns>
        public static ReturnEntity<IList<web_Wash_CategoryDC>> Cache_web_Wash_Category_SELECT_List(int iClassID, int iSite)
        {
            return WCFInvokeHelper<web_ProductClient>.Invoke<ReturnEntity<IList<web_Wash_CategoryDC>>>
                    (client => client.Proxy.web_Wash_Category_SELECT_List(iClassID, iSite));
            var list = Cache_GetByKey("WEB_Wash_Category_ClassID_" + iClassID + "_" + iSite) as IList<web_Wash_CategoryDC>;
            if (list == null)
            {
                return WCFInvokeHelper<web_ProductClient>.Invoke<ReturnEntity<IList<web_Wash_CategoryDC>>>
                    (client => client.Proxy.web_Wash_Category_SELECT_List(iClassID, iSite));
            }
            else
            {
                return new ReturnEntity<IList<web_Wash_CategoryDC>>(list);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iCategoryID"></param>
        /// <returns></returns>
        public static ReturnEntity<web_Wash_CategoryDC> Cache_web_Wash_Category_SELECT_Entity(int iCategoryID)
        {
            var list = Cache_GetByKey("WEB_Wash_Product_CategoryID_" + iCategoryID) as web_Wash_CategoryDC;
            if (list == null)
            {
                return WCFInvokeHelper<web_ProductClient>.Invoke<ReturnEntity<web_Wash_CategoryDC>>
                    (client => client.Proxy.web_Wash_Category_SELECT_Entity(iCategoryID));
            }
            else
            {
                return new ReturnEntity<web_Wash_CategoryDC>(list);
            }
        }

        /// <summary>
        /// 根据运价ID获取运价
        /// </summary>
        /// <param name="iProductID"></param>
        /// <returns></returns>
        public static ReturnEntity<web_Wash_ProductDC> Cache_web_Wash_Product_SELECT_Entity(int iProductID)
        {
            var list = Cache_GetByKey("WEB_Wash_Product_ID_" + iProductID) as web_Wash_ProductDC;
            if (list == null)
            {
                return WCFInvokeHelper<web_ProductClient>.Invoke<ReturnEntity<web_Wash_ProductDC>>
                    (client => client.Proxy.web_Wash_Product_SELECT_Entity(iProductID));
            }
            else
            {
                return new ReturnEntity<web_Wash_ProductDC>(list);
            }
        }

        /// <summary>
        /// 查询小类(包括运价)
        /// </summary>
        /// <returns></returns>
        public static ReturnEntity<IList<web_Wash_ClassDC>> Cache_web_Wash_Class_SELECT_List_Second_Detail(int iSite)
        {
            var list = Cache_GetByKey("WEB_Wash_Class_List_Second_Detail_" + iSite) as ReturnEntity<IList<web_Wash_ClassDC>>;
            if (list == null)
            {
                return WCFInvokeHelper<web_ProductClient>.Invoke<ReturnEntity<IList<web_Wash_ClassDC>>>
                    (client => client.Proxy.web_Wash_Class_SELECT_List_Second_Detail(iSite));
            }
            else
            {
                return list;
            }
        }

        /// <summary>
        /// 查询推荐产品
        /// </summary>
        /// <param name="iSite"></param>
        /// <returns></returns>
        public static ReturnEntity<IList<web_Wash_ProductDC>> Cache_web_Wash_Product_SELECT_Recommend(int iSite)
        {
            var list = Cache_GetByKey("WEB_Wash_Product_Recommend_" + iSite) as IList<web_Wash_ProductDC>;
            if (list == null)
            {
                return WCFInvokeHelper<web_ProductClient>.Invoke<ReturnEntity<IList<web_Wash_ProductDC>>>
                    (client => client.Proxy.web_Wash_Product_SELECT_Recommend(iSite));
            }
            else
            {
                return new ReturnEntity<IList<web_Wash_ProductDC>>(list);
            }
        }

        /// <summary>
        /// 活动列表
        /// </summary>
        /// <param name="iSite"></param>
        /// <returns></returns>
        public static ReturnEntity<IList<Wash_ActivityDC>> Cache_web_Wash_Activity_SELECT_List(int iSite)
        {
            var list = Cache_GetByKey("WEB_Wash_Activity_" + iSite) as IList<Wash_ActivityDC>;
            if (list == null)
            {
                return WCFInvokeHelper<web_ProductClient>.Invoke<ReturnEntity<IList<Wash_ActivityDC>>>
                    (client => client.Proxy.web_Wash_Activity_SELECT_List(iSite));
            }
            else
            {
                return new ReturnEntity<IList<Wash_ActivityDC>>(list);
            }
        }
    }
}
