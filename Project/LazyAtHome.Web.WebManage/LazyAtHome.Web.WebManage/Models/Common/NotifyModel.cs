using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.Common.Contract.DataContract.Base;

namespace LazyAtHome.Web.WebManage.Models.Common
{
    /// <summary>
    /// 通知消息页面展示实体类
    /// </summary>
    [Serializable]
    public class NotifyModel
    {
        /// <summary>
        /// 通知消息信息
        /// </summary>
        public Base_NotifyDC NotifyInfo { get; set; }
    }
}