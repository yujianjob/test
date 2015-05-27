using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;
using LazyAtHome.WCF.Common.Contract.Enumerate;
using LazyAtHome.WCF.WebService.Contract.DataContract;
using LazyAtHome.WCF.WebService.Contract.DataContract.PR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.WebService.Contract.InterfaceContract
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IPR
    {
        /// <summary>
        /// 操作员登录	
        /// </summary>
        /// <param name="iCredentials"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<OperatorDC> PR_Operator_Login(LoginCredentials iCredentials);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iCredentials"></param>
        /// <param name="iType"></param>
        /// <param name="iOperatorID"></param>
        /// <param name="iOperatorName"></param>
        /// <param name="iLogContent"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> PR_OperatorLog_ADD(LoginCredentials iCredentials, int iType, int iOperatorID, string iOperatorName, string iLogContent);

    }
}
