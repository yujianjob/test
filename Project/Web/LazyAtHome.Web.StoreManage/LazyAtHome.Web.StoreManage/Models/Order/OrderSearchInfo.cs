using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.OrderSystem.Contract.Enumerate;

namespace LazyAtHome.Web.StoreManage.Models.Order
{
    /// <summary>
    /// 订单查询实体
    /// </summary>
    [Serializable]
    public class OrderSearchInfo : CodeSource.PagingInfo
    {
        ///// <summary>
        ///// 订单号
        ///// </summary>
        //public string OrderNumber { get; set; }

        ///// <summary>
        ///// 合作门店ID
        ///// </summary>
        //public Guid UserID = CodeSource.SiteSession.OperatorInfo.StoreID;

        /// <summary>
        /// 订单状态
        /// </summary>
        public OrderStatus? OrderStatus { get; set; }

        /// <summary>
        /// 下单开始时间
        /// </summary>
        public DateTime DateFrom { get; set; }
        /// <summary>
        /// 下单结束时间
        /// </summary>
        public DateTime DateTo { get; set; }
    }
}