using LazyAtHome.Core.Infrastructure.WCF.Service;
using LazyAtHome.WCF.WebService.Contract.ClientProxy;
using LazyAtHome.WCF.WebService.Contract.DataContract;
using LazyAtHome.WCF.WebService.Contract.DataContract.PR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.WebService.Business.Portal
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class BasePortal : ServiceBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iCredentials"></param>
        /// <returns></returns>
        public bool LoginCredentials_Check(LoginCredentials iCredentials)
        {
            if (iCredentials == null) return false;


            var password = CacheClient.Cache_GetByKey(iCredentials.CacheKey) as string;

            if (password == null)
            {
                //缓存丢失,重新登录
                var entity = LazyAtHome.WCF.WebService.Business.Business.PR.Instance.PR_Operator_Login(iCredentials);
                if (entity != null && entity.Success && entity.OBJ != null)
                { return true; }
                else
                {
                    return false;
                }
            }
            else if (password == iCredentials.Password)
            {
                return true;
            }
            else
            {
                //密码错误
                return false;
            }
        }
    }
}
