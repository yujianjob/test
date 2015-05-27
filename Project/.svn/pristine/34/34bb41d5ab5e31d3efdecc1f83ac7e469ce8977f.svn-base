using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.SFSupport.CodeSource
{
    public class ReturnJsonEntity
    {
        public string statusCode { get; set; }
        public string message { get; set; }
        public string navTabId { get; set; }
        public string forwardUrl { get; set; }
        public string callbackType { get; set; }
        public string rel { get; set; }
    }

    public class StatusCode
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        public const string Success = "200";
        /// <summary>
        /// 操作失败
        /// </summary>
        public const string Faild = "300";
        /// <summary>
        /// 会话超时
        /// </summary>
        public const string SessionTimeout = "301";
    }

    public class CallbackType
    {
        /// <summary>
        /// 关闭当前页面
        /// </summary>
        public const string CloseCurrent = "closeCurrent";
        public const string Forward = "forward";
        public const string ForwardConfirm = "forwardConfirm";
    }
}