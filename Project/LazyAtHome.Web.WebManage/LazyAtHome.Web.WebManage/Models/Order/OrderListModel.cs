using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.OrderSystem.Contract.DataContract;

namespace LazyAtHome.Web.WebManage.Models.Order
{
    /// <summary>
    /// 订单列表页绑定实体
    /// </summary>
    [Serializable]
    public class OrderListModel
    {
        public OrderSearchInfo SearchInfo;
        public IList<Order_OrderDC> OrderList;
    }

    /// <summary>
    /// 逾期订单列表页绑定实体
    /// </summary>
    [Serializable]
    public class ExpireOrderListModel
    {
        public ExpireOrderSearchInfo SearchInfo;
        public IList<Order_OrderDC> OrderList;
    }

    /// <summary>
    /// 今日订单列表页绑定实体
    /// </summary>
    [Serializable]
    public class TodayOrderListModel
    {
        public TodayOrderSearchInfo SearchInfo;
        public IList<Order_OrderDC> OrderList;
    }
}