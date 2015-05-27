using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.OrderSystem.Contract.DataContract;

namespace LazyAtHome.Web.SFSupport.Models.Order
{
    /// <summary>
    /// 历史列表页绑定实体
    /// </summary>
    [Serializable]
    public class HisOrderListModel
    {
        public HisOrderSearchInfo SearchInfo;
        public IList<Partner_Order_ExpressDC> OrderList;
    }
}