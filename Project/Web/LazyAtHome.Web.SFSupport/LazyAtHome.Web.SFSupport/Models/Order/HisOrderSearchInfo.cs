using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.SFSupport.Models.Order
{
    /// <summary>
    /// 历史订单查询实体
    /// </summary>
    [Serializable]
    public class HisOrderSearchInfo : CodeSource.PagingInfo
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }


        /// <summary>
        /// 手机号
        /// </summary>
        public string MPNo { get; set; }

        /// <summary>
        /// 收件快递单号
        /// </summary>
        public string GetExpressNumber { get; set; }
    }
}