using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.OrderSystem.Contract.Enumerate;

namespace LazyAtHome.Web.StoreManage.Models.User
{
    /// <summary>
    /// 用户衣物查询实体
    /// </summary>
    [Serializable]
    public class UserProductSearchInfo : CodeSource.PagingInfo
    {
        /// <summary>
        /// 用户手机
        /// </summary>
        public string UserMPNo { get; set; }

        /// <summary>
        /// 用户称呼
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 签收状态
        /// </summary>
        public UserSignType? UserSignType { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime DateFrom { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime DateTo { get; set; }
    }
}