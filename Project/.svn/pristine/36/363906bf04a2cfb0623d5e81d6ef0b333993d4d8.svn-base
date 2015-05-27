using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WeiXin
{
    /// <summary>
    /// 禁止页面缓存
    /// </summary>
    public class NoCacheFilter : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnResultExecuting(System.Web.Mvc.ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetNoStore();
        }
    }
}