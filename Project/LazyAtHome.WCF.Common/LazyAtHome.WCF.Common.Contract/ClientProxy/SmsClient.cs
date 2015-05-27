using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Infrastructure.WCF.Client;
using LazyAtHome.WCF.Common.Contract.Enumerate;
using LazyAtHome.WCF.Common.Contract.InterfaceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Common.Contract.ClientProxy
{
    public class SmsClient : ClientProxyBase<ISms>
    {
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <returns></returns>
        public static ReturnEntity<bool> Base_SmsSend_Create(string iMPNo, string iContent,
            DateTime? iRunTime = null,
            Sms_Priority iPriority = Sms_Priority.Normal,
            Sms_Type iType = Sms_Type.Normal,
            Sms_Channel iChannel = Sms_Channel.YM,
            Sms_Source iSource = Sms_Source.Null,
            string iSourceValue = null
            )
        {
            return WCFInvokeHelper<SmsClient>.Invoke<ReturnEntity<bool>>
                (client => client.Proxy.Base_SmsSend_Create(iMPNo, iContent,
                    iRunTime, iPriority, iType,
                    iChannel, iSource, iSourceValue));
        }
    }
}
