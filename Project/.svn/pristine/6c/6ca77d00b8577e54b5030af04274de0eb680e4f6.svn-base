using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LazyAtHome.Web.WebManage.CodeSource.Proxy;

namespace LazyAtHome.Web.WebManage.Controllers
{
    public class CacheController : Controller
    {
        //
        // GET: /Cache/
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult RemoveCache(string cachename)
        {
            CodeSource.ReturnJsonEntity rjEntity = new CodeSource.ReturnJsonEntity();


            string mess = CacheProxy.RemoveCache(cachename);


            rjEntity.statusCode = CodeSource.StatusCode.Success;
            rjEntity.message = mess;
            //rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
            //rjEntity.navTabId = "managerList";

            return Json(rjEntity);

        }
	}
}