using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Infrastructure.WCF.Client;
using LazyAtHome.WCF.Common.Contract.DataContract.Base;
using LazyAtHome.WCF.Common.Contract.InterfaceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Common.Contract.ClientProxy
{
    public class BaseClient : ClientProxyBase<IBase>
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
        /// 存缓存
        /// </summary>
        /// <param name="iKey"></param>
        /// <returns></returns>
        public static void Cache_Put(string iKey, object iValue, TimeSpan iTimeSpan)
        {
            LazyAtHome.Core.Helper.CacheHelper.Put(iKey, iValue, iTimeSpan);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public static IList<Base_DistrictDC> Cache_Base_District_GetList(int iID)
        {
            IList<Base_DistrictDC> list = new List<Base_DistrictDC>();

            var _DistrictList = Cache_Base_District_GetList();
            if (_DistrictList == null)
            {
                return list;
            }
            var level_3 = _DistrictList.FirstOrDefault(p => p.ID == iID);
            if (level_3 == null)
            {
                return list;
            }
            else
            {
                list.Add(level_3);
            }

            var level_2 = _DistrictList.FirstOrDefault(p => p.Code1 == level_3.Code1 && p.Code2 == level_3.Code2 && p.Code3 == "00" && p.Level == 2);
            if (level_2 == null)
            {
                return list;
            }
            else
            {
                list.Add(level_2);
            }

            var level_1 = _DistrictList.FirstOrDefault(p => p.Code1 == level_3.Code1 && p.Code2 == "00" && p.Code3 == "00" && p.Level == 1);
            if (level_1 == null)
            {
                return list;
            }
            else
            {
                list.Add(level_1);
            }

            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        public static IList<Base_DistrictDC> Cache_Base_District_GetList()
        {
            return Cache_GetByKey("Base_District_List") as IList<Base_DistrictDC>;
        }

        /// <summary>
        /// 
        /// </summary>
        public static IList<Base_SiteDC> Cache_Base_SiteDC_GetList()
        {
            return Cache_GetByKey("Base_SiteDC_List") as IList<Base_SiteDC>;
        }


        /// <summary>
        /// 根据Page查询
        /// </summary>
        /// <param name="iPage"></param>
        /// <returns></returns>
        public static ReturnEntity<Base_WebAttributeDC> Cache_web_Base_WebAttribute_SELECT_Entity(string iPage)
        {
            var entity = Cache_GetByKey("WEB_Base_WebAttribute_" + iPage) as Base_WebAttributeDC;
            if (entity == null)
            {
                return WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<Base_WebAttributeDC>>
                    (client => client.Proxy.web_Base_WebAttribute_SELECT_Entity(iPage));
            }
            else
            {
                return new ReturnEntity<Base_WebAttributeDC>(entity);
            }
        }
    }
}
