using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.Web.StoreManage.CodeSource.Common;
using LazyAtHome.WCF.OrderSystem.Contract.DataContract;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Helper;
using LazyAtHome.WCF.OrderSystem.Contract.ClientProxy;
using LazyAtHome.WCF.OrderSystem.Contract.Enumerate;

namespace LazyAtHome.Web.StoreManage.CodeSource.Proxy
{
    /// <summary>
    /// 用户wcf代理类
    /// </summary>
    public class UserProxy
    {
        /// <summary>
        /// 获取用户的产品列表
        /// </summary>
        /// <param name="iUserMPNo"></param>
        /// <param name="iUserName"></param>
        /// <param name="iUserSignType"></param>
        /// <param name="DateFrom"></param>
        /// <param name="DateTo"></param>
        /// <returns></returns>
        public static ReturnEntity<IList<Order_ProductDC>> GetProductListByUser(Guid iStoreID, string iUserMPNo, string iUserName, UserSignType? iUserSignType, DateTime DateFrom, DateTime DateTo)
        {
            ReturnEntity<IList<Order_ProductDC>> re = null;
            try
            {
                re = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<IList<Order_ProductDC>>>
                   (client => client.Proxy.Order_Product_SELECT_List_Store(iStoreID, iUserMPNo, iUserName, iUserSignType, DateFrom, DateTo));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|UserProxy GetProductListByUser|" + ex.Message);
            }

            return re;

        }

        /// <summary>
        /// 用户签收/撤销签收
        /// </summary>
        /// <param name="iOrderProductID"></param>
        /// <param name="iUserSignType"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> UpdateUserSignType(int iOrderProductID, UserSignType iUserSignType)
        {
            ReturnEntity<bool> re = null;
            try
            {
                re = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Order_Product_UPDATE_UserSignType(iOrderProductID, iUserSignType));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|UserProxy UpdateUserSignType|" + ex.Message);
            }

            return re;

        }
    }
}