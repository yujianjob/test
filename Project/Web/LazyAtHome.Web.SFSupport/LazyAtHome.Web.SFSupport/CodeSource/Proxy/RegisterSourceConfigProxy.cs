using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.Web.SFSupport.CodeSource.Common;
using LazyAtHome.WCF.UserSystem.Contract.DataContract;
using LazyAtHome.WCF.UserSystem.Contract.ClientProxy;
using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Web.Base;

namespace LazyAtHome.Web.SFSupport.CodeSource.Proxy
{
    /// <summary>
    /// 用户推广wcf代理类
    /// </summary>
    public class RegisterSourceConfigProxy
    {
        /// <summary>
        /// 获取微信关注及注册统计
        /// </summary>
        /// <param name="iInternalKey"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <returns></returns>
        public static ReturnEntity<IList<User_WeixinAttentionLogStatDC>> GetWeixinAttentionLogStatList(string iInternalKey, DateTime iStartDate, DateTime iEndDate)
        {
            //IList<User_WeixinAttentionLogStatDC> rtn = null;
            ReturnEntity<IList<User_WeixinAttentionLogStatDC>> re = null;
            try
            {
                re = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<IList<User_WeixinAttentionLogStatDC>>>
                    (client => client.Proxy.User_WeixinAttentionLog_Stat(iInternalKey, iStartDate, iEndDate));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|RegisterSourceConfigProxy GetWeixinAttentionLogStatList|" + ex.Message);
            }
            return re;
            //if (re != null && re.Success)
            //{
            //    rtn = re.OBJ;
            //}
            //return rtn;
        }
    }
}