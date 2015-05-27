using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.Web.WebManage.CodeSource.Common;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;
using LazyAtHome.WCF.Common.Contract.ClientProxy;
using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Web.Base;

namespace LazyAtHome.Web.WebManage.CodeSource.Proxy
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
                   (client => client.Proxy.PR_Operator_Login(loginName, loginPwd, WCF.Common.Contract.Enumerate.OperatorType.Manage));
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
        /// 获取管理员列表
        /// </summary>
        /// <param name="iName"></param>
        /// <param name="iMpNo"></param>
        /// <param name="iEmail"></param>
        /// <param name="iEnableType"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<OperatorDC> GetManagerList(string iName, string iMpNo, string iEmail, int? iEnableType, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize,int? iNodeID = null)
        {
            RecordCountEntity<OperatorDC> rtn = null;
            ReturnEntity<RecordCountEntity<OperatorDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<PRClient>.Invoke<ReturnEntity<RecordCountEntity<OperatorDC>>>
                    (client => client.Proxy.PR_Operator_SELECT_List(iName, iMpNo, iEmail, iEnableType, iStartDate, iEndDate, iPageIndex, iPageSize, iNodeID));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OperatorProxy GetManagerList|" + ex.Message);
            }
            if (rce != null && rce.Success)
            {
                rtn = rce.OBJ;
            }
            return rtn;
        }



        /// <summary>
        /// 新增管理员
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ReturnEntity<int> AddManager(OperatorDC entity)
        {
            ReturnEntity<int> re = null;

            try
            {

                re = WCFInvokeHelper<PRClient>.Invoke<ReturnEntity<int>>
                   (client => client.Proxy.PR_Operator_ADD(entity));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OperatorProxy AddManager|" + ex.Message);
            }
            return re;
        }

        
        /// <summary>
        /// 根据ID获取管理员信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ReturnEntity<OperatorDC> GetManagerBYID(int id)
        {
            ReturnEntity<OperatorDC> re = null;
            try
            {

                re = WCFInvokeHelper<PRClient>.Invoke<ReturnEntity<OperatorDC>>
                   (client => client.Proxy.PR_Operator_SELECT_BYID(id));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OperatorProxy GetManagerBYID|" + ex.Message);
            }
            return re;

        }


        /// <summary>
        /// 更新管理员
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> UpdateManager(OperatorDC entity)
        {
            ReturnEntity<bool> re = null;

            try
            {

                re = WCFInvokeHelper<PRClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.PR_Operator_UPDATE(entity));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OperatorProxy UpdateManager|" + ex.Message);
            }
            return re;
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



        /// <summary>
        /// 获取职责清单
        /// </summary>
        /// <returns></returns>
        public static IList<RoleDC> GetRoleList()
        {
            IList<RoleDC> list = null;
            try
            {
                list = LazyAtHome.WCF.Common.Contract.DataContract.PR.RoleDC.CONFIG_LIST;
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OperatorProxy GetRoleList|" + ex.Message);
                return null;
            }

            return list;
        }

    }
}