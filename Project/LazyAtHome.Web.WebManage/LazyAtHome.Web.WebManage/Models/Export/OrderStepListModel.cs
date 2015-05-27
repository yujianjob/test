using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.OrderSystem.Contract.DataContract;

namespace LazyAtHome.Web.WebManage.Models.Export
{
    /// <summary>
    /// 订单跟踪列表页绑定实体
    /// </summary>
    [Serializable]
    public class OrderStepListModel
    {
        public OrderStepSearchInfo SearchInfo;
        public IList<Order_Order_StatDC> OrderStepList;
    }
}