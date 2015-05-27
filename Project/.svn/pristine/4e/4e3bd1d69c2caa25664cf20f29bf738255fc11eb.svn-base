using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WebManage.Models.Manager
{
    /// <summary>
    /// 管理员查询实体
    /// </summary>
    [Serializable]
    public class ManagerSearchInfo : CodeSource.PagingInfo
    {
        //string format = "UserName={0}&MPNo={1}&Enable={2}&DateFrom={3}&DateTo={4}";//查询参数字段
        public string UserName { get; set; }
        public string MPNo { get; set; }
        public int? Enable { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? DateFrom { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? DateTo { get; set; }

        //public override string PageParam
        //{
        //    get
        //    {
        //        return string.Format(format, UserName, MPNo, Enable, DateFrom, DateTo);
        //    }
        //}
    }
}