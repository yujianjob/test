using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.Web.WebManage.CodeSource.Common;
using LazyAtHome.WCF.UserSystem.Contract.DataContract;
using LazyAtHome.WCF.UserSystem.Contract.ClientProxy;
using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Web.Base;

namespace LazyAtHome.Web.WebManage.CodeSource.Proxy
{
    /// <summary>
    /// 用户推广wcf代理类
    /// </summary>
    public class RegisterSourceConfigProxy
    {
        /// <summary>
        /// 获取用户推广配置列表
        /// </summary>
        /// <param name="iType"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<User_RegisterSourceDC> GetRegisterSourceList(int? iType, int iPageIndex, int iPageSize)
        {
            RecordCountEntity<User_RegisterSourceDC> rtn = null;
            ReturnEntity<RecordCountEntity<User_RegisterSourceDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<RecordCountEntity<User_RegisterSourceDC>>>
                    (client => client.Proxy.User_RegisterSource_SELECT_List(iType, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|RegisterSourceConfigProxy GetRegisterSourceList|" + ex.Message);
            }
            if (rce != null && rce.Success)
            {
                rtn = rce.OBJ;
            }
            return rtn;
        }

        /// <summary>
        /// 新增用户推广配置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ReturnEntity<int> AddRegisterSource(User_RegisterSourceDC entity)
        {
            ReturnEntity<int> re = null;

            try
            {
                re = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<int>>
                   (client => client.Proxy.User_RegisterSource_ADD(entity));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|RegisterSourceConfigProxy AddRegisterSource|" + ex.Message);
            }
            return re;
        }

        /// <summary>
        /// 根据ID获取用户推广配置信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ReturnEntity<User_RegisterSourceDC> GetRegisterSourceBYID(int id)
        {
            ReturnEntity<User_RegisterSourceDC> re = null;
            try
            {

                re = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<User_RegisterSourceDC>>
                   (client => client.Proxy.User_RegisterSource_SELECT_Entity(id));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|RegisterSourceConfigProxy GetRegisterSourceBYID|" + ex.Message);
            }
            return re;

        }


        /// <summary>
        /// 删除用户推广配置信息
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> DeleteRegisterSource(int iID, int iMuser)
        {
            ReturnEntity<bool> re = null;
            try
            {

                re = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.User_RegisterSource_DELETE(iID, iMuser));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|RegisterSourceConfigProxy DeleteRegisterSource|" + ex.Message);
            }
            return re;
        }




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