using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WebManage.Models.Activity
{
    /// <summary>
    /// 活动列表查询实体
    /// </summary>
    [Serializable]
    public class ActivitySearchInfo : CodeSource.PagingInfo
    {
        /// <summary>
        /// 活动名称
        /// </summary>
        public string ActivityName { get; set; }

        /// <summary>
        /// 站点
        /// </summary>
        public int? Site { get; set; }

        /// <summary>
        /// 渠道
        /// </summary>
        public int? Channel { get; set; }

        /// <summary>
        /// 启用状态
        /// </summary>
        public int? CommitStatus { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? DateFrom { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? DateTo { get; set; }
    }
}