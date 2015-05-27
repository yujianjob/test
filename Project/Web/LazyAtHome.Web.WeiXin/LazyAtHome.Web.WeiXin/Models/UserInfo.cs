using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WeiXin.Models
{
    public class UserInfo
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public WCF.UserSystem.Contract.DataContract.weixin.User_WeixinDC User { get; set; }
        /// <summary>
        /// 用户微信ID
        /// </summary>
        public string OpenID { get; set; }
        /// <summary>
        /// 收货地址
        /// </summary>
        public WCF.UserSystem.Contract.DataContract.User_ConsigneeAddressDC GetAddress { get; set; }
        /// <summary>
        /// 送货地址
        /// </summary>
        public WCF.UserSystem.Contract.DataContract.User_ConsigneeAddressDC SendAddress { get; set; }
        /// <summary>
        /// 支付类型
        /// </summary>
        public int PayType { get; set; }
        /// <summary>
        /// 应付金额
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// 产品列表
        /// </summary>
        public IList<Models.ShopCart.ProductItem> ProductList { get; set; }
        /// <summary>
        /// 地址修改标记
        /// </summary>
        public string AddressModifyFlag { get; set; }
        /// <summary>
        /// 预约日期
        /// </summary>
        public string ReceviceDay { get; set; }
        /// <summary>
        /// 预约时间
        /// </summary>
        public string ReceviceTime { get; set; }

    }
}