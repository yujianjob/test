using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WebManage.Models.Store
{
    /// <summary>
    /// 合作门店列表查询实体
    /// </summary>
    [Serializable]
    public class StoreSearchInfo : CodeSource.PagingInfo
    {
        /// <summary>
        /// 合作门店名称
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// 合作门店编号
        /// </summary>
        public string StoreCode { get; set; }

        /// <summary>
        /// 站点
        /// </summary>
        public int? Site { get; set; }

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