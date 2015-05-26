using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.API.Mobile.Models.JsonResultModels
{
    /// <summary>
    /// 4.2	用户名有效性检查
    /// </summary>
    public class UserNameCheckResult : BaseResult
    {
        /// <summary>
        /// 状态 0:可以注册 其他:已存在
        /// </summary>
        public int flag { get; set; }

    }
}