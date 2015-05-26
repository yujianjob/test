using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Common.Contract.ClientProxy;
using LazyAtHome.WCF.Common.Contract.DataContract.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace LazyAtHome.API.Mobile.App_Code.Proxy
{
    public class CommonProxy
    {
        //4.26	获取行政区列表	15
        public static IList<Base_DistrictDC> Cache_Base_District_GetList()
        {
            var rtn = BaseClient.Cache_Base_District_GetList();

            return rtn;
        }

        //4.4	发送验证码短信	8
        public static ReturnEntity<bool> Base_SmsSend_Create(string iMPNo, string iContent)
        {
            var rtn = SmsClient.Base_SmsSend_Create(iMPNo, iContent);

            return rtn;
        }

        public static void Cache_Put(string iKey, object iValue, TimeSpan iTimeSpan)
        {
            BaseClient.Cache_Put(iKey, iValue, iTimeSpan);
        }

        public static void Cache_Remove(string iKey)
        {
            LazyAtHome.Core.Helper.CacheHelper.Remove(iKey);
        }

        public static object Cache_GetByKey(string iKey)
        {
            var rtn = BaseClient.Cache_GetByKey(iKey);

            return rtn;
        }
    }
}