using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WebManage.Models.Express
{
    /// <summary>
    /// 物流订单列表查询实体
    /// </summary>
    [Serializable]
    public class ExpOrderSearchInfoBase : CodeSource.PagingInfo
    {
        /// <summary>
        /// 内部单号 物流单号
        /// </summary>
        public string ExpNumber { get; set; }

        /// <summary>
        /// 外部单号 订单号
        /// </summary>
        public string OutNumber { get; set; }

        /// <summary>
        /// 物流类型
        /// </summary>
        public int? TransportType { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 用户联系人
        /// </summary>
        public string Contacts { get; set; }

        /// <summary>
        /// 用户手机号
        /// </summary>
        public string Mpno { get; set; }

        /// <summary>
        /// 物流状态
        /// </summary>
        public int? Step { get; set; }

        /// <summary>
        /// 其他通用查询条件
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? DateFrom { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? DateTo { get; set; }
    }


    public class ExpOrderSearchInfo : ExpOrderSearchInfoBase
    { }

    public class UnAllocationExpOrderSearchInfo : ExpOrderSearchInfoBase
    { }
}