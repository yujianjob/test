using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.WebService.Contract.ClientProxy
{
    /// <summary>
    /// 
    /// </summary>
    public class CacheClient
    {
        public static TimeSpan GetTimeSpanOneHour
        {
            get
            {
                return new TimeSpan(0, (DateTime.Now.Minute >= 5 ? (65 - DateTime.Now.Minute) : (5 - DateTime.Now.Minute)), 0);
            }
        }

        public static TimeSpan GetTimeSpanOneDay
        {
            get
            {
                return new TimeSpan((25 - DateTime.Now.Hour), 5, 0);
            }
        }

        /// <summary>
        /// Key取缓存
        /// </summary>
        /// <param name="iKey"></param>
        /// <returns></returns>
        public static object Cache_GetByKey(string iKey)
        {
            return LazyAtHome.Core.Helper.CacheHelper.Get(iKey);
        }

    }
}
