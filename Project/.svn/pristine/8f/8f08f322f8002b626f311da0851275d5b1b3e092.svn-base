using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WebManage.Models.Express
{
    /// <summary>
    /// 存衣点列表查询实体
    /// </summary>
    [Serializable]
    public class ExpNodeSearchInfo : CodeSource.PagingInfo
    {
        /// <summary>
        /// 行政区
        /// </summary>
        public int? DistrictID { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        public int? AreaID { get; set; }

        /// <summary>
        /// 存衣点名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 站点类型
        /// 1：站点 2：干线 3：工厂
        /// </summary>
        public int? Type { get; set; }

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