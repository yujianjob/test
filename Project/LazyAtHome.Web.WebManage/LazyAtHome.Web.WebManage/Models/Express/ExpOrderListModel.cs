using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.Express.Contract.DataContract;

namespace LazyAtHome.Web.WebManage.Models.Express
{
    /// <summary>
    /// 物流订单列表页绑定实体
    /// </summary>
    [Serializable]
    public class ExpOrderListModel
    {
        public ExpOrderSearchInfo SearchInfo;
        public IList<Exp_OrderDC> ExpOrderList;
    }

    /// <summary>
    /// 待处理物流订单列表页绑定实体
    /// </summary>
    [Serializable]
    public class UnAllocationExpOrderListModel
    {
        public UnAllocationExpOrderSearchInfo SearchInfo;
        public IList<Exp_OrderDC> ExpOrderList;
    }
}