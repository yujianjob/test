using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Infrastructure.WCF.Client;
using LazyAtHome.WCF.Express.Contract.DataContract;
using LazyAtHome.WCF.Express.Contract.InterfaceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Express.Contract.ClientProxy
{
    public class ExpressClient : ClientProxyBase<IExpress>
    {
        /// <summary>
        /// Key取缓存
        /// </summary>
        /// <param name="iKey"></param>
        /// <returns></returns>
        public static object Cache_GetByKey(string iKey)
        {
            return LazyAtHome.Core.Helper.CacheHelper.Get(iKey);
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //public static IList<Exp_NodeDC> Cache_Exp_Node_GetList()
        //{
        //    var list = Cache_GetByKey("WCF_Exp_NodeList") as IList<Exp_NodeDC>;
        //    if (list == null)
        //    {
        //        return WCFInvokeHelper<ExpressClient>.Invoke<IList<Exp_NodeDC>>
        //            (client => client.Proxy.Exp_Node_SELECT_List());
        //    }
        //    else
        //    {
        //        return list;
        //    }
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //public static ReturnEntity<Exp_OperatorDC> Cache_Exp_Operator_GetEntity(int iID)
        //{
        //    var entity = Cache_GetByKey("WCF_Exp_Operator_" + iID) as ReturnEntity<Exp_OperatorDC>;
        //    if (entity == null)
        //    {
        //        return WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<Exp_OperatorDC>>
        //            (client => client.Proxy.Exp_Operator_SELECT_Entity(iID));
        //    }
        //    else
        //    {
        //        return entity;
        //    }
        //}
    }
}
