using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WebManage.Models.Luxury
{
    /// <summary>
    /// 奢侈品查询实体
    /// </summary>
    public class LuxurySearchInfo : CodeSource.PagingInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string LuxuryName { get; set; }

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