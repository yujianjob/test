using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.OrderSystem.Contract.DataContract;

namespace LazyAtHome.Web.WebManage.Models.Order
{
    /// <summary>
    /// 订单合并页绑定实体
    /// </summary>
    [Serializable]
    public class MergerOrderModel
    {
        /// <summary>
        /// 主订单
        /// </summary>
        public Order_OrderDC PrimaryOrder;

        /// <summary>
        /// 副订单
        /// </summary>
        public Order_OrderDC SlaveOrder;
    }
}