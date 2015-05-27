using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WebManage.Models.Gift
{
    /// <summary>
    /// 商城礼品查询实体
    /// </summary>
    [Serializable]
    public class GiftSearchInfo : CodeSource.PagingInfo
    {
        /// <summary>
        /// 类型 1:懒人卡
        /// </summary>
        public int? Type { set; get; }

        /// <summary>
        /// 类别 懒人卡: 1:实物卡 2:电子卡
        /// </summary>
        public int? Class { set; get; }

        /// <summary>
        /// 名称
        /// </summary>
        public string GiftName { set; get; }

        /// <summary>
        /// 站点
        /// </summary>
        public int? Site { set; get; }

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