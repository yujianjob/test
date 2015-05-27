using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Infrastructure.WCF.Client;
using LazyAtHome.WCF.Wash.Contract.DataContract;
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
    public class ProductClient : ClientProxyBase<IProduct>
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

        public static IList<Wash_ClassDC> Wash_Class_SELECT_List_First()
        {
            var list = Wash_Class_SELECT_List_ALL();
            if (list != null)
            {
                return list.Where(p => p.ParentID == 0).ToList();
            }
            else
            {
                return new List<Wash_ClassDC>();
            }
        }

        /// <summary>
        /// 查询小类
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public static IList<Wash_ClassDC> Wash_Class_SELECT_List_Second(int iID)
        {
            var list = Wash_Class_SELECT_List_ALL();
            if (list != null)
            {
                return list.Where(p => p.ParentID == iID && iID != 0).ToList();
            }
            else
            {
                return new List<Wash_ClassDC>();
            }
        }

        /// <summary>
        /// 查询小类
        /// </summary>
        /// <returns></returns>
        public static IList<Wash_ClassDC> Wash_Class_SELECT_List_Second()
        {
            var list = Wash_Class_SELECT_List_ALL();
            if (list != null)
            {
                return list.Where(p => p.ParentID != 0).ToList();
            }
            else
            {
                return new List<Wash_ClassDC>();
            }
        }

        /// <summary>
        /// 产品类别表
        /// </summary>
        /// <returns></returns>
        public static IList<Wash_ClassDC> Wash_Class_SELECT_List_ALL()
        {
            var list = Cache_GetByKey("Wash_Class_List") as IList<Wash_ClassDC>;
            if (list == null)
            {
                var rtn = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<IList<Wash_ClassDC>>>
                    (client => client.Proxy.Wash_Class_SELECT_List_ALL());
                if (rtn.Success)
                {
                    return rtn.OBJ;
                }
                else
                {
                    return new List<Wash_ClassDC>();
                }
            }
            else
            {
                return list;
            }
        }

        //public static IList<Wash_AttributeDC> Wash_Attribute_SELECT_List_First()
        //{
        //    var list = Wash_Attribute_SELECT_List_ALL();
        //    if (list != null)
        //    {
        //        return list.Where(p => p.ParentID == 0).ToList();
        //    }
        //    else
        //    {
        //        return new List<Wash_AttributeDC>();
        //    }
        //}

        ////查询小类
        //public static IList<Wash_AttributeDC> Wash_Attribute_SELECT_List_Second(int iID)
        //{
        //    var list = Wash_Attribute_SELECT_List_ALL();
        //    if (list != null)
        //    {
        //        return list.Where(p => p.ParentID == iID).ToList();
        //    }
        //    else
        //    {
        //        return new List<Wash_AttributeDC>();
        //    }
        //}

        ///// <summary>
        ///// 产品类别表
        ///// </summary>
        ///// <returns></returns>
        //public static IList<Wash_AttributeDC> Wash_Attribute_SELECT_List_ALL()
        //{
        //    return Cache_GetByKey("Wash_Attribute_List") as IList<Wash_AttributeDC>;
        //}
    }
}
