using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WebManage.Models.Product
{
    /// <summary>
    /// 产品运价查询实体
    /// </summary>
    [Serializable]
    public class ProductSearchInfo : CodeSource.PagingInfo
    {
        /// <summary>
        /// 产品运价名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 产品运价编号
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// 启用状态
        /// </summary>
        public int? CommitStatus { get; set; }

        /// <summary>
        /// 类型 1：普通 2：活动
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
    }


    public class ProductForOrderSearchInfo : CodeSource.PagingInfo
    {
        /// <summary>
        /// 产品运价名称
        /// </summary>
        public string ProductName { get; set; }
    }
}