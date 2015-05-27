using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.Common.Contract.DataContract.Base;

namespace LazyAtHome.Web.WebManage.Models.Coupon
{
    /// <summary>
    /// 优惠券列表页绑定实体
    /// </summary>
    [Serializable]
    public class CouponListModel
    {
        public CouponSearchInfo SearchInfo;
        public IList<Base_CouponDC> CouponList;
    }
}