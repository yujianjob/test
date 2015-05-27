using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.OrderSystem.Contract.DataContract;
using LazyAtHome.WCF.OrderSystem.Contract.Enumerate;

namespace LazyAtHome.Web.WebManage.Models.Order
{
    /// <summary>
    /// 订单查询实体
    /// </summary>
    [Serializable]
    public class OrderSearchInfo : CodeSource.PagingInfo
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNumber { get; set; }


        /// <summary>
        /// 手机号
        /// </summary>
        public string MPNo { get; set; }

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


        /// <summary>
        /// 取件开始时间
        /// </summary>
        public DateTime? GetDateFrom { get; set; }
        /// <summary>
        /// 取件结束时间
        /// </summary>
        public DateTime? GetDateTo { get; set; }


        /// <summary>
        /// 金额下限
        /// </summary>
        public int? TotalAmountMin { get; set; }

        /// <summary>
        /// 金额上限
        /// </summary>
        public int? TotalAmountMax { get; set; }

        /// <summary>
        /// 取件客户
        /// </summary>
        public string Consignee { get; set; }

        /// <summary>
        /// 排序类型
        /// 0：按下单时间 2：按取件时间
        /// </summary>
        public int SortType { get; set; }

        /// <summary>
        /// 订单进程
        /// </summary>
        public int? Step { get; set; }

        /// <summary>
        /// 物流类型
        /// 0：自主物流  1：赛奥递
        /// </summary>
        public int? GetExpressType { get; set; }
    }



    /// <summary>
    /// 今日订单查询实体
    /// </summary>
    [Serializable]
    public class TodayOrderSearchInfo : OrderSearchInfo
    { }
}