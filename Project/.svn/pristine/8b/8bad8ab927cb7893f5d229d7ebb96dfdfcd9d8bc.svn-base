using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LazyAtHome.Winform.FactoryPortal.Common;
using LazyAtHome.Winform.FactoryPortal.PRService;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Helper;


namespace LazyAtHome.Winform.FactoryPortal.Business
{
    public class FormPRClient : LazyAtHome.Core.Infrastructure.WCF.Client.ClientProxyBase<IPR>
    {
        public FormPRClient()
            : base("WSHttpBinding_IPR")
        { }

    }
    /// <summary>
    /// 操作员业务逻辑类
    /// </summary>
    public class Operator : Base
    {
        //public IPR WSPRService = new PRClient();
        public FormPRClient WSPRService = new FormPRClient();

        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="login">登录实体</param>
        /// <returns></returns>
        public OperatorDC Login(LoginCredentials login)
        {
            OperatorDC entity = null;

            //ReturnEntityOfOperatorDCcHStGbqD re = null;
            ReturnEntity<OperatorDC> re = null;

            try
            {
                //re = WSPRService.Proxy.PR_Operator_Login(login);

                re = WCFInvokeHelper<FormPRClient>.Invoke<ReturnEntity<OperatorDC>>
                    (client => client.Proxy.PR_Operator_Login(login));

            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(ConstConfig.WRONG_LOG_PATH, "|Operator Login|" + ex.Message);
                LastError = ex.Message;
            }

            if (re != null)
            {
                if (re.Success)
                {
                    entity = re.OBJ;
                }
                else
                {
                    //处理报错信息
                    LastError = re.Message;
                }
                
            }
            else
            {
                //处理报错信息
                //LastError = re.Message;
            }
            return entity;
        }



        /// <summary>
        /// 添加操作日志
        /// </summary>
        /// <param name="iOperatorLogDC">操作日志实体类</param>
        /// <returns></returns>
        public bool OperateLog_Add(Model.OperatorLog iOperatorLog)
        {

            LoginCredentials login = new LoginCredentials();
            login.LoginName = Common.ConstConfig.currOperator.LoginName;
            login.Password = Common.ConstConfig.currPwd;//currOperator.LoginPwd;
            login.OperatorType = OperatorType.Factory;

            ReturnEntity<bool> re = null;
            try
            {
                //re = WSPRService.Proxy.PR_OperatorLog_ADD(login, iOperatorLog.Type, iOperatorLog.OperatorID, iOperatorLog.OperatorName, iOperatorLog.LogContent);

                re = WCFInvokeHelper<FormPRClient>.Invoke<ReturnEntity<bool>>
                    (client => client.Proxy.PR_OperatorLog_ADD(login, iOperatorLog.Type, iOperatorLog.OperatorID, iOperatorLog.OperatorName, iOperatorLog.LogContent));

            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(ConstConfig.WRONG_LOG_PATH, "|OperatorProxy OperateLog_Add|" + ex.Message);
            }
            return re.OBJ;

        }



        ///// <summary>
        ///// 修改密码
        ///// </summary>
        ///// <param name="iNewPassword"></param>
        ///// <returns></returns>
        //public bool PasswordChange(string iNewPassword)
        //{
        //    ReturnEntity<bool> re = null;
        //    bool rtn = false;
        //    try
        //    {

        //        re = WCFInvokeHelper<PRClient>.Invoke<ReturnEntity<bool>>
        //           (client => client.Proxy.PR_Operator_UPDATE_Password(Common.ConstConfig.currOperator.ID, iNewPassword, Common.ConstConfig.currOperator.ID));
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteLog.tradeLog(ConstConfig.WRONG_LOG_PATH, "|Operator PasswordChange|" + ex.Message);
        //        rtn = false;
        //        LastError = ex.Message;
        //    }
        //    if (re != null && re.Success)
        //    {
        //        rtn = true;
        //    }
        //    else
        //    {
        //        rtn = false;
        //        LastError = re.Message;
        //    }
        //    return rtn;
        //}
    }
}
