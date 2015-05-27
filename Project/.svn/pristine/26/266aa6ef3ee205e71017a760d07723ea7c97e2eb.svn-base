using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WebManage.Models.Log
{    
    /// <summary>
    /// 操作日志查询实体基类
    /// </summary>
    [Serializable]
    public class OperatorLogBaseSearchInfo : CodeSource.PagingInfo
    {
        /// <summary>
        /// 日志类型
        /// </summary>
        public int? Type { get; set; }

        /// <summary>
        /// 操作员名称
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// 日志内容
        /// </summary>
        public string LogContent { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime DateFrom  { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime DateTo{ get; set; }
    }
}