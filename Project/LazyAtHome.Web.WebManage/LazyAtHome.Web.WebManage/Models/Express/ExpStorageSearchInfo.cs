using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WebManage.Models.Express
{
    /// <summary>
    /// 仓库列表查询实体
    /// </summary>
    [Serializable]
    public class ExpStorageSearchInfo : CodeSource.PagingInfo
    {
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 仓库类型
        /// -1:全部 0:系统 1:站点 2:干线 3:工厂
        /// </summary>
        public int? Type { get; set; }
    }

    public class ExpStorageForItemSearchInfo : ExpStorageSearchInfo
    { }
}