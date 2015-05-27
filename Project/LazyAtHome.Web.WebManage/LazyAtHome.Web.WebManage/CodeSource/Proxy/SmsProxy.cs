using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.Web.WebManage.CodeSource.Common;
using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Web.Base;
using LazyAtHome.WCF.Common.Contract.DataContract.Sms;
using LazyAtHome.WCF.Common.Contract.ClientProxy;
using LazyAtHome.WCF.Common.Contract.Enumerate;

namespace LazyAtHome.Web.WebManage.CodeSource.Proxy
{
    /// <summary>
    /// 短信wcf代理类
    /// </summary>
    public class SmsProxy
    {
        /// <summary>
        /// 获取短信下行列表
        /// </summary>
        /// <param name="iMPNo"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<Base_SmsSendDC> GetUserSmsSendList(string iMPNo, DateTime iStartDate, DateTime iEndDate, int iPageIndex, int iPageSize)
        {
            RecordCountEntity<Base_SmsSendDC> rtn = null;
            ReturnEntity<RecordCountEntity<Base_SmsSendDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<SmsClient>.Invoke<ReturnEntity<RecordCountEntity<Base_SmsSendDC>>>
                    (client => client.Proxy.Base_SmsSend_SELECT_List(iMPNo, iStartDate, iEndDate, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|SmsProxy GetUserSmsSendList|" + ex.Message);
            }
            if (rce != null && rce.Success)
            {
                rtn = rce.OBJ;
            }
            return rtn;
        }

        /// <summary>
        /// 短信下行
        /// </summary>
        /// <param name="iMPNo"></param>
        /// <param name="iContent"></param>
        /// <param name="iRunTime"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> AddUserSmsSend(string iMPNo, string iContent, DateTime? iRunTime = null, Sms_Priority iPriority = Sms_Priority.Normal, Sms_Type iType = Sms_Type.Normal, Sms_Channel iChannel = Sms_Channel.YM, Sms_Source iSource = Sms_Source.Null, string iSourceValue = null)
        {
            ReturnEntity<bool> re = null;

            try
            {

                re = WCFInvokeHelper<SmsClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Base_SmsSend_Create(iMPNo, iContent, iRunTime, iPriority,iType, iChannel, iSource , iSourceValue));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|SmsProxy AddUserSmsSend|" + ex.Message);
            }
            return re;
        }
    }
}