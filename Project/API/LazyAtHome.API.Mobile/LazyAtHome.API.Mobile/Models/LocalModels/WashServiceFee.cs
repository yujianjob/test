using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.API.Mobile.Models.LocalModels
{
    public class WashServiceFee
    {
        /// <summary>
        /// 服务费ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 服务费名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 分组ID
        /// </summary>
        public string groupid { get; set; }
        /// <summary>
        /// 	是否必选一个 0:可不选 1:必选
        /// </summary>
        public int mustcheck { get; set; }
        /// <summary>
        /// 是否选中	0:未选中 1:已选中
        /// </summary>
        public int @checked { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public int money { get; set; }
    }
}