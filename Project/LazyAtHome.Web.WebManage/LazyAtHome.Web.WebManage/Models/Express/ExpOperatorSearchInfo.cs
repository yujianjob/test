using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WebManage.Models.Express
{
    /// <summary>
    /// 快递员列表查询实体
    /// </summary>
    [Serializable]
    public class ExpOperatorSearchInfo : CodeSource.PagingInfo
    {
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 快递员名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 1：快递员 2：装车员
        /// </summary>
        public int? Type { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string MpNo { get; set; }

        /// <summary>
        /// 收衣点名称
        /// </summary>
        public string NodeName { get; set; }

        /// <summary>
        /// 收衣点ID
        /// </summary>
        public int? NodeID { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? DateFrom { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? DateTo { get; set; }

        /// <summary>
        /// 搜索来源
        /// </summary>
        public int Source { get; set; }
    }
}