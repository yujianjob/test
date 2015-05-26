using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.API.Mobile.Models.LocalModels
{
    /// <summary>
    /// 5.5	产品实体类
    /// </summary>
    public class WashProductModel
    {
        /// <summary>
        /// 产品ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 产品名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 市场价金额	单位:分
        /// </summary>
        public int marketprice { get; set; }

        /// <summary>
        /// 销售金额	单位:分
        /// </summary>
        public int price { get; set; }

        /// <summary>
        /// 数量	购物车或结算时用到
        /// </summary>
        public int count { get; set; }
    }
}