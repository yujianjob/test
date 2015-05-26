using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.API.Mobile.Models.LocalModels
{
    /// <summary>
    /// 5.2	用户地址实体类
    /// </summary>
    public class UserAddsModel
    {
        /// <summary>
        /// 地址ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 收件人
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 行政区ID
        /// </summary>
        public int districtid { get; set; }
        /// <summary>
        /// 行政区全名
        /// </summary>
        public string districtname { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string adds { get; set; }

        /// <summary>
        /// 1:设置为默认
        /// </summary>
        public int @default { get; set; }
    }
}