using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WebManage.Models.Express
{
    /// <summary>
    /// 仓库一栏查询实体
    /// </summary>
    [Serializable]
    public class ExpStorageItemSearchInfo : CodeSource.PagingInfo
    {
        /// <summary>
        /// 仓库ID
        /// </summary>
        public int? StorageID { get; set; }

        /// <summary>
        /// 仓库名称
        /// </summary>
        public string StorageName { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 子编号
        /// </summary>
        public string OtherNumber { get; set; }

        /// <summary>
        /// 物品类型
        /// 1：包裹 2：衣物
        /// </summary>
        public int? ItemType { get; set; }

        /// <summary>
        /// 目标去向
        /// </summary>
        public int? TargetType { get; set; }
    }
}