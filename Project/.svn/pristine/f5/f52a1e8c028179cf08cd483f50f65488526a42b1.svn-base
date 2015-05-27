using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WebManage.Models.Express
{
    /// <summary>
    /// 仓库出入库查询实体
    /// </summary>
    [Serializable]
    public class ExpStorageLogSearchInfo : CodeSource.PagingInfo
    {
        /// <summary>
        /// 类型
        /// 1:出库 2:入库
        /// </summary>
        public int? Type { get; set; }

        /// <summary>
        /// 物品编号
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 物品子编号
        /// </summary>
        public string OtherNumber { get; set; }

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