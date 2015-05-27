using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Infrastructure.WCF.Service;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;
using LazyAtHome.WCF.Common.Contract.Enumerate;
using LazyAtHome.WCF.WebService.Contract.DataContract;
using LazyAtHome.WCF.WebService.Contract.DataContract.PR;
using LazyAtHome.WCF.WebService.Contract.InterfaceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.WebService.Business.Portal
{
    /// InstanceContextMode - 获取或设置指示新服务对象何时创建的值。
    /// InstanceContextMode.PerSession - 为每个会话创建一个新的 System.ServiceModel.InstanceContext 对象。
    /// InstanceContextMode 的默认值为 InstanceContextMode.PerSession
    public class PRPortal : BasePortal, IPR
    {
        protected LazyAtHome.WCF.WebService.Business.Business.PR PRInstance = LazyAtHome.WCF.WebService.Business.Business.PR.Instance;

        /// <summary>								
        /// 操作员登录								
        /// </summary>								
        /// <param name="iLoginName">登录名</param>								
        /// <param name="iPassword">密码</param>								
        /// <returns></returns>
        public ReturnEntity<OperatorDC> PR_Operator_Login(LoginCredentials iCredentials)
        {
            return PRInstance.PR_Operator_Login(iCredentials);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iCredentials"></param>
        /// <param name="iType"></param>
        /// <param name="iOperatorID"></param>
        /// <param name="iOperatorName"></param>
        /// <param name="iLogContent"></param>
        /// <returns></returns>
        public ReturnEntity<bool> PR_OperatorLog_ADD(LoginCredentials iCredentials, int iType, int iOperatorID, string iOperatorName, string iLogContent)
        {
            if (!base.LoginCredentials_Check(iCredentials))
            {
                return new ReturnEntity<bool>(-99, "操作员验证错误");
            }

            return PRInstance.PR_OperatorLog_ADD(iType, iOperatorID, iOperatorName, iLogContent);
        }
    }
}
