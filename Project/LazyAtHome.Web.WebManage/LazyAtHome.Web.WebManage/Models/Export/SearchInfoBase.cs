using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WebManage.Models.Export
{
    /// <summary>
    /// 报表导出条件基类
    /// </summary>
    public class SearchInfoBase : CodeSource.PagingInfo
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime DateFrom { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime DateTo { get; set; }
    }
}