using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WebManage.Models.Common
{
    /// <summary>
    /// 消息通知查询实体
    /// </summary>
    [Serializable]
    public class NotifySearchInfo : CodeSource.PagingInfo
    {
        /// <summary>
        /// 事件编号
        /// </summary>
        public string EventNumber { get; set; }


        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNumber { get; set; }


        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }


        /// <summary>
        /// 事件等级
        /// </summary>
        public int? Level { get; set; }

        /// <summary>
        /// 事件编号
        /// 0:未处理 1:处理中 2:处理完成 6:关闭 100:待处理
        /// </summary>
        public int? NotifyStatus { get; set; }


    }
}