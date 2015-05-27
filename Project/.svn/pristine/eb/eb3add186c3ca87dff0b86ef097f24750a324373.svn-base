using LazyAtHome.API.TinyURL.App_Code;
using LazyAtHome.API.TinyURL.App_Code.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LazyAtHome.API.TinyURL.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string id)
        {
            var targetUrl = "http://www.landaojia.com/";

            if (string.IsNullOrWhiteSpace(id))
            {
                return Redirect(targetUrl);
            }

            try
            {
                var rtn = CommonProxy.Tinyurl_Get(id);
                if (rtn == null || !rtn.Success)
                {
                    return Redirect(targetUrl);
                }
                if (!string.IsNullOrWhiteSpace(rtn.OBJ))
                {
                    targetUrl = rtn.OBJ;
                }
            }
            catch (Exception ex)
            {
                UtilityFunc.Add(ex.Message);
                return Redirect(targetUrl);
            }

            return Redirect(targetUrl);
        }
    }
}
