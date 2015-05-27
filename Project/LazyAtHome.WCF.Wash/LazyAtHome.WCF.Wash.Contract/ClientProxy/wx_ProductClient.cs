using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Infrastructure.WCF.Client;
using LazyAtHome.WCF.Wash.Contract.DataContract.weixin;
using LazyAtHome.WCF.Wash.Contract.InterfaceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Wash.Contract.ClientProxy
{
    /// <summary>
    /// 
    /// </summary>
    public class wx_ProductClient : ClientProxyBase<IwxProduct>
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
        public static ReturnEntity<IList<wx_Wash_ClassDC>> Cache_wx_Wash_Class_SELECT_List()
        {
            var list = Cache_GetByKey("WX_Wash_Class_List") as IList<wx_Wash_ClassDC>;
            if (list == null)
            {
                return WCFInvokeHelper<wx_ProductClient>.Invoke<ReturnEntity<IList<wx_Wash_ClassDC>>>
                    (client => client.Proxy.wx_Wash_Class_SELECT_List());
            }
            else
            {
                return new ReturnEntity<IList<wx_Wash_ClassDC>>(list);
            }
        }

        /// <summary>
        /// 根据小类ID获取产品列表
        /// </summary>
        /// <returns></returns>
        public static ReturnEntity<IList<wx_Wash_CategoryDC>> Cache_wx_Wash_Category_SELECT_List(int iClassID, int iSite)
        {
            var list = Cache_GetByKey("WX_Wash_Category_ClassID_" + iClassID + "_" + iSite) as IList<wx_Wash_CategoryDC>;
            if (list == null)
            {
                return WCFInvokeHelper<wx_ProductClient>.Invoke<ReturnEntity<IList<wx_Wash_CategoryDC>>>
                    (client => client.Proxy.wx_Wash_Category_SELECT_List(iClassID, iSite));
            }
            else
            {
                return new ReturnEntity<IList<wx_Wash_CategoryDC>>(list);
            }
        }

        /// <summary>
        /// 根据ID获取运价
        /// </summary>
        /// <returns></returns>
        public static ReturnEntity<wx_Wash_CategoryDC> Cache_wx_Wash_Category_SELECT_Entity(int iCategoryID)
        {
            var entity = Cache_GetByKey("WX_Wash_Category_ID_" + iCategoryID) as wx_Wash_CategoryDC;
            if (entity == null)
            {
                return WCFInvokeHelper<wx_ProductClient>.Invoke<ReturnEntity<wx_Wash_CategoryDC>>
                    (client => client.Proxy.wx_Wash_Category_SELECT_Entity(iCategoryID));
            }
            else
            {
                return new ReturnEntity<wx_Wash_CategoryDC>(entity);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iProductID"></param>
        /// <returns></returns>
        public static ReturnEntity<wx_Wash_ProductDC> Cache_wx_Wash_Product_SELECT_Entity(int iProductID)
        {
            var entity = Cache_GetByKey("WX_Wash_Product_ID_" + iProductID) as wx_Wash_ProductDC;
            if (entity == null)
            {
                return WCFInvokeHelper<wx_ProductClient>.Invoke<ReturnEntity<wx_Wash_ProductDC>>
                    (client => client.Proxy.wx_Wash_Product_SELECT_Entity(iProductID));
            }
            else
            {
                return new ReturnEntity<wx_Wash_ProductDC>(entity);
            }

        }









    }
}
