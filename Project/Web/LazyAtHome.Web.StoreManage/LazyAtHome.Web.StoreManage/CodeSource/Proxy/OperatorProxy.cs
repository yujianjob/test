using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.Web.StoreManage.CodeSource.Common;
using LazyAtHome.WCF.Wash.Contract.DataContract;
using LazyAtHome.WCF.Wash.Contract.ClientProxy;
using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
//using LazyAtHome.Core.Web.Base;

namespace LazyAtHome.Web.StoreManage.CodeSource.Proxy
{
    /// <summary>
    /// 操作员wcf代理类
    /// </summary>
    public class OperatorProxy
    {
        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="storecode">商户号</param>
        /// <param name="loginName">登录名</param>
        /// <param name="loginPwd">登录密码</param>
        /// <returns></returns>
        public static Wash_StoreLoginInfoDC Login(string storecode, string loginName, string loginPwd)
        {
            Wash_StoreLoginInfoDC entity = null;
            ReturnEntity<Wash_StoreLoginInfoDC> re = null;
            try
            {
                re = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<Wash_StoreLoginInfoDC>>
                   (client => client.Proxy.Wash_Store_Login(storecode,loginName, loginPwd));
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
                re = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Wash_Store_Operator_UPDATE_Password(iOperatorID, iOldPassword, iNewPassword, iMuser));
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
        public static bool OperateLog_Add(Wash_Store_OperatorLogDC iOperatorLogDC)
        {
            ReturnEntity<bool> re = null;
            try
            {

                re = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Wash_Store_OperatorLog_ADD(iOperatorLogDC.Type, iOperatorLogDC.OperatorID, iOperatorLogDC.OperatorName, iOperatorLogDC.LogContent));
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