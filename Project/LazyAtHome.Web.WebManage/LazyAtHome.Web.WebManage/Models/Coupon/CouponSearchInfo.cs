using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WebManage.Models.Coupon
{
    /// <summary>
    /// 优惠券列表查询实体
    /// </summary>
    [Serializable]
    public class CouponSearchInfo : CodeSource.PagingInfo
    {
        /// <summary>
        /// 优惠券名称
        /// </summary>
        public string CouponName { get; set; }

        /// <summary>
        /// 使用类别
        /// </summary>
        public int? UseClass { get; set; }

        /// <summary>
        /// 使用类型
        /// </summary>
        public int? UseType { get; set; }

        /// <summary>
        /// 确认状态
        /// </summary>
        public int? CommitStatus { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? DateFrom { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? DateTo { get; set; }
    }
}