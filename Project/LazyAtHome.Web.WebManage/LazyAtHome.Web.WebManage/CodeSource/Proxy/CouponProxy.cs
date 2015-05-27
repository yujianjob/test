using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.Web.WebManage.CodeSource.Common;
using LazyAtHome.WCF.Common.Contract.DataContract.Base;
using LazyAtHome.WCF.Common.Contract.ClientProxy;
using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Web.Base;

namespace LazyAtHome.Web.WebManage.CodeSource.Proxy
{
    /// <summary>
    /// 优惠券wcf代理类
    /// </summary>
    public class CouponProxy
    {
        /// <summary>
        /// 获取优惠券列表
        /// </summary>
        /// <param name="iName"></param>
        /// <param name="iUseClass"></param>
        /// <param name="iUseType"></param>
        /// <param name="iCommitStatus"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<Base_CouponDC> GetCouponList(string iName, int? iUseClass, int? iUseType, int? iCommitStatus,  DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            RecordCountEntity<Base_CouponDC> rtn = null;
            ReturnEntity<RecordCountEntity<Base_CouponDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<RecordCountEntity<Base_CouponDC>>>
                    (client => client.Proxy.Base_Coupon_SELECT_List(iName, iUseClass, iUseType, iCommitStatus, iStartDate, iEndDate, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|CouponProxy GetCouponList|" + ex.Message);
            }
            if (rce != null && rce.Success)
            {
                rtn = rce.OBJ;
            }
            return rtn;
        }
    }
}