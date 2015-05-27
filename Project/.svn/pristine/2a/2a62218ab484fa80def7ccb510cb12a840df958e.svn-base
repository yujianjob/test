using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LazyAtHome.Web.Site.Areas.Member.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Member/Home/
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Menu()
        {
            return PartialView();
        }
	}
}