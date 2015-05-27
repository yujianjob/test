using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WebManage.Models.Export
{
    /// <summary>
    /// 订单预警查询条件
    /// </summary>
    public class OrderWarningSearchInfo : SearchInfoBase
    {
        /// <summary>
        /// 取件预警
        /// </summary>
        public bool isGetPackage { get; set; }

        /// <summary>
        /// 洗涤预警
        /// </summary>
        public bool isWash { get; set; }

        /// <summary>
        /// 送件预警
        /// </summary>
        public bool isSendPackage { get; set; }
    }
}