using LazyAtHome.Core.Infrastructure.WCF.Client;
using LazyAtHome.WCF.UserSystem.Contract.InterfaceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.UserSystem.Contract.ClientProxy
{
    public class UserClient : ClientProxyBase<IUser>
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
    }
}
