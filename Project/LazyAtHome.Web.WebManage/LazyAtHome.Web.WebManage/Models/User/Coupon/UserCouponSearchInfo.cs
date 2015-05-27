using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.UserSystem.Contract.Enumerate;

namespace LazyAtHome.Web.WebManage.Models.User
{
    /// <summary>
    /// 用户优惠券查询实体类
    /// </summary>
    [Serializable]
    public class UserCouponSearchInfo : CodeSource.PagingInfo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid? UserID { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string MPNo { get; set; }

        /// <summary>
        /// 优惠券名称
        /// </summary>
        public string CouponName { get; set; }


        /// <summary>
        /// 持券状态 用于界面显示
        /// 1:正常 2:已使用 3:删除
        /// </summary>
        public int CouponStatusInt { get; set; }


        /// <summary>
        /// 持券状态
        /// 1:正常 2:已使用 3:删除
        /// </summary>
        public CouponStatus? CouponStatus 
        {
            get 
            {
                if (CouponStatusInt == -1)
                {
                    return null;
                }
                else
                {
                    return (WCF.UserSystem.Contract.Enumerate.CouponStatus)CouponStatusInt;
                }
            }
        }

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