using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.API.Mobile.Models.LocalModels
{
    /// <summary>
    /// 5.14	优惠券实体类
    /// </summary>
    public class UserCouponModel
    {
        /// <summary>
        /// 优惠券ID	
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 优惠券名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 抵扣金额 单位:分
        /// </summary>
        public int money { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public string expdate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int expflag { get; set; }
    }
}