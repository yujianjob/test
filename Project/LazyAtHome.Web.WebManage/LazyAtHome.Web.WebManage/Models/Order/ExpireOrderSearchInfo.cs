using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WebManage.Models.Order
{
    /// <summary>
    /// 逾期订单查询实体
    /// </summary>
    [Serializable]
    public class ExpireOrderSearchInfo : CodeSource.PagingInfo
    {
        /// <summary>
        /// 期限
        /// </summary>
        public DateTime Deadline { get; set; }


        /// <summary>
        /// 逾期规则
        /// </summary>
        public int DateCount = 3;
    }
}