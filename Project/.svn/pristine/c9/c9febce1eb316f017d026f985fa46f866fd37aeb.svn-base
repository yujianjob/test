using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.Web.SFSupport.CodeSource.Common;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;
using LazyAtHome.WCF.Common.Contract.ClientProxy;
using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Web.Base;

namespace LazyAtHome.Web.SFSupport.CodeSource.Proxy
{
    /// <summary>
    /// 管理员wcf代理类
    /// </summary>
    public class OperatorProxy
    {
        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <param name="loginPwd">登录密码</param>
        /// <returns></returns>
        public static OperatorDC Login(string loginName, string loginPwd)
        {
            OperatorDC entity = null;
            ReturnEntity<OperatorDC> re = null;
            try
            {
                //Core.Infrastructure.WCF.ReturnEntity<OperatorDC> rtn = wcf.Proxy.PR_Operator_Login(loginName, loginPwd);
                re = WCFInvokeHelper<PRClient>.Invoke<ReturnEntity<OperatorDC>>
                   (client => client.Proxy.PR_Operator_Login(loginName, loginPwd, WCF.Common.Contract.Enumerate.OperatorType.Partner));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OperatorProxy Login|" + ex.Message);
            }

            if (re != null && re.Success)
            {
                entity = re.OBJ;
            }
            else
            {
                //处理报错信息
            }
            return entity;
        }


        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static ReturnEntity<bool> ResetPassword(int iOperatorID, string iOldPassword, string iNewPassword, int iMuser)
        {
            ReturnEntity<bool> re = null;

            try
            {
                re = WCFInvokeHelper<PRClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.PR_Operator_UPDATE_Password(iOperatorID, iOldPassword, iNewPassword));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OperatorProxy ResetPassword|" + ex.Message);
            }
            return re;
        }



        /// <summary>
        /// 添加操作日志
        /// </summary>
        /// <param name="iOperatorLogDC">操作日志实体类</param>
        /// <returns></returns>
        public static bool OperateLog_Add(OperatorLogDC iOperatorLogDC)
        {
            ReturnEntity<bool> re = null;
            try
            {

                re = WCFInvokeHelper<PRClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.PR_OperatorLog_ADD(iOperatorLogDC.Type, iOperatorLogDC.OperatorID, iOperatorLogDC.OperatorName, iOperatorLogDC.LogContent));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OperatorProxy OperateLog_Add|" + ex.Message);
            }
            return re.OBJ;

        }
    }
}