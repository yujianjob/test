using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.API.Mobile.Models.LocalModels
{
    /// <summary>
    /// 5.7	金额变动实体类
    /// </summary>
    public class OrderAmountModel
    {
        /// <summary>
        /// 金额ID	
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 金额名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 变动金额	单位:分 带正负值
        /// </summary>
        public int money { get; set; }
    }
}