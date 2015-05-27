using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Common.Contract.ClientProxy;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;
using LazyAtHome.WCF.Common.Contract.Enumerate;
using LazyAtHome.WCF.WebService.Contract.ClientProxy;
using LazyAtHome.WCF.WebService.Contract.DataContract;
using LazyAtHome.WCF.WebService.Contract.DataContract.PR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.WebService.Business.Business
{
    /// <summary>
    /// 
    /// </summary>
    public class PR
    {
        public PR()
        {

        }

        static PR _PR;
        public static PR Instance
        {
            get
            {
                if (_PR == null)
                {
                    _PR = new PR();
                }
                return _PR;
            }
        }

        /// <summary>
        /// 操作员登录
        /// </summary>
        /// <param name="iLoginName">登录名</param>								
        /// <param name="iPassword">密码</param>								
        /// <returns></returns>
        public ReturnEntity<OperatorDC> PR_Operator_Login(LoginCredentials iCredentials)
        {
            if (iCredentials == null) return new ReturnEntity<OperatorDC>(-1, "参数错误");

            var entity = WCFInvokeHelper<PRClient>.Invoke<ReturnEntity<OperatorDC>>
                  (client => client.Proxy.PR_Operator_Login(iCredentials.LoginName, iCredentials.Password, iCredentials.OperatorType));
            if (entity != null && entity.Success && entity.OBJ != null)
            {
                LazyAtHome.Core.Helper.CacheHelper.Put(iCredentials.CacheKey, iCredentials.Password, CacheClient.GetTimeSpanOneHour);
            }

            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iType"></param>
        /// <param name="iOperatorID"></param>
        /// <param name="iOperatorName"></param>
        /// <param name="iLogContent"></param>
        /// <returns></returns>
        public ReturnEntity<bool> PR_OperatorLog_ADD(int iType, int iOperatorID, string iOperatorName, string iLogContent)
        {
            return WCFInvokeHelper<PRClient>.Invoke<ReturnEntity<bool>>
                 (client => client.Proxy.PR_OperatorLog_ADD(iType, iOperatorID, iOperatorName, iLogContent));
        }

    }
}
