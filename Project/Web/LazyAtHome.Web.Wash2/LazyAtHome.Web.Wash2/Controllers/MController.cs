using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LazyAtHome.Web.Wash2.Controllers
{
    public class MController : Controller
    {
        protected DBDataClassesDataContext dbContext = new DBDataClassesDataContext();
        //
        // GET: /M/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Android()
        {
            return File("E://File/Mobile/landaojia/Android/landaojia.apk", "application/octet-stream", "landaojia.apk");

            //return File("E://landaojia.apk", "application/octet-stream", "landaojia.apk");
        }

        public ActionResult DownLoadApp()
        {
            var scan_download_request = new scan_download_request();

            string tagUrl = "http://a.app.qq.com/o/simple.jsp?pkgname=com.lazyathome.wash";
            string[] keywords = {  "iphone", "ipod", "ipad" };
            var agent = Request.UserAgent.ToLower();
            var ip = Request.UserHostAddress;

            scan_download_request.http_head = agent;
            scan_download_request.ip = ip;
            scan_download_request.create_time = DateTime.Now;

            //if (agent.Contains("android"))//安卓平台
            //{
            //    tagUrl = "http://www.landaojia.com/M/Android";
            //}
            //if (agent.Contains(keywords[0]) || agent.Contains(keywords[1]) || agent.Contains(keywords[1]))
            //{
            //    tagUrl = "https://itunes.apple.com/cn/app/id923806169";
            //}
            dbContext.scan_download_request.InsertOnSubmit(scan_download_request);
            dbContext.SubmitChanges();

            return Redirect(tagUrl);
        }
	}
}