using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.Core.Web.Base;

namespace LazyAtHome.Web.WebManage.Models.User
{
    /// <summary>
    /// 用户查询实体类
    /// </summary>
    [Serializable]
    public class UserSearchInfo :  CodeSource.PagingInfo
    {
        public string LoginName { get; set; }
        public string MPNo { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// 注册开始时间
        /// </summary>
        public DateTime? DateFrom { get; set; }
        /// <summary>
        /// 注册结束时间
        /// </summary>
        public DateTime? DateTo { get; set; }

        //string format = "LoginName={0}&MPNo={1}&Email={2}";//查询参数字段


        //public override string PageParam
        //{
        //    get
        //    {
        //        return string.Format(format, LoginName, MPNo, Email);
        //    }
        //}
    }
}