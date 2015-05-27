using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.OrderSystem.Contract.DataContract;

namespace LazyAtHome.Web.SFSupport.Models.Order
{
    /// <summary>
    /// 订单列表页绑定实体
    /// </summary>
    [Serializable]
    public class OrderListModel
    {
        public OrderSearchInfo SearchInfo;
        public IList<Partner_Order_ExpressDC> OrderList;
    }
}