using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.API.Mobile.Models.LocalModels
{
    /// <summary>
    /// 5.15	懒人卡消费日志
    /// </summary>
    public class UserCardLogModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 交易时间
        /// </summary>
        public string stime { get; set; }

        /// <summary>
        /// 交易类型
        /// </summary>
        public string stype { get; set; }

        /// <summary>
        /// 交易金额
        /// </summary>
        public string smoney { get; set; }
    }
}