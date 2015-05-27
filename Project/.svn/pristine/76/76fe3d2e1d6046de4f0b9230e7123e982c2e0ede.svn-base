using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WebManage.Models.Log
{
    /// <summary>
    /// 系统日志查询实体
    /// </summary>
    [Serializable]
    public class SystemLogSearchInfo : CodeSource.PagingInfo
    {
        /// <summary>
        /// 项目
        /// </summary>
        public string AppDomainName { get; set; }

        /// <summary>
        /// 日志标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 日志类型
        /// </summary>
        public int? EventType { get; set; }

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