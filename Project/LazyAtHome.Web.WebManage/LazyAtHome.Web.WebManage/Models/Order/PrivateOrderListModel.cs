using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.OrderSystem.Contract.DataContract;

namespace LazyAtHome.Web.WebManage.Models.Order
{
    /// <summary>
    /// 个人订单列表页绑定实体
    /// </summary>
    [Serializable]
    public class PrivateOrderListModel
    {
        public PrivateOrderSearchInfo SearchInfo;
        public IList<Order_OrderDC> OrderList;
        
        /// <summary>
        /// 主订单ID
        /// </summary>
        public int PrimaryOrderID;
    }
}