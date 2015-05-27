using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.OrderSystem.Contract.DataContract;

namespace LazyAtHome.Web.WebManage.Models.Order
{
    /// <summary>
    /// 订单页信息
    /// </summary>
    [Serializable]
    public class OrderModel
    {
        /// <summary>
        /// 订单详情
        /// </summary>
        public Order_OrderDC OrderInfo { get; set; }

        /// <summary>
        /// 历史订单
        /// </summary>
        public IList<Order_OrderDC> HistroyOrderList { get; set; }

        //public SF_OrderInfoDC SFOrderInfo { get; set; }

        ///// <summary>
        ///// 收货地址
        ///// </summary>
        //public string rAddress { get; set; }

        ///// <summary>
        ///// 送货地址
        ///// </summary>
        //public string sAddress { get; set; }
    }
}