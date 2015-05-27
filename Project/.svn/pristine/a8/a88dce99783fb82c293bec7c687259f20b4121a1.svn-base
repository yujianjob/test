using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.OrderSystem.Contract.Enumerate;

namespace LazyAtHome.Web.WebManage.Models.Order
{
    /// <summary>
    /// 个人订单查询实体
    /// </summary>
    [Serializable]
    public class PrivateOrderSearchInfo : CodeSource.PagingInfo
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNumber { get; set; }


        /// <summary>
        /// 用户手机号
        /// </summary>
        public string MPNo { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserID { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public OrderStatus? OrderStatus { get; set; }


        /// <summary>
        /// 下单渠道
        /// </summary>
        public int? Channel { get; set; }

        /// <summary>
        /// 订单分类
        /// </summary>
        public OrderClass? OrderClass { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public OrderType? OrderType { get; set; }

        /// <summary>
        /// 站点
        /// </summary>
        public int? Site { get; set; }

        /// <summary>
        /// 下单开始时间
        /// </summary>
        public DateTime? DateFrom { get; set; }
        /// <summary>
        /// 下单结束时间
        /// </summary>
        public DateTime? DateTo { get; set; }


    }
}