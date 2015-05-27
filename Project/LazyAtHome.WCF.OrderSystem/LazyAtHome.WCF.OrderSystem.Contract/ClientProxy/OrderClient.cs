using LazyAtHome.Core.Infrastructure.WCF.Client;
using LazyAtHome.WCF.OrderSystem.Contract.InterfaceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.OrderSystem.Contract.ClientProxy
{
    public class OrderClient : ClientProxyBase<IOrder>
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

        public static object Cache_Remove(string iKey)
        {
            return LazyAtHome.Core.Helper.CacheHelper.Remove(iKey);
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
    }
}
