using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.Web.WeiXin4
{
    public class NoCacheFilter : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnResultExecuting(System.Web.Mvc.ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetNoStore();
        }
    }
}
