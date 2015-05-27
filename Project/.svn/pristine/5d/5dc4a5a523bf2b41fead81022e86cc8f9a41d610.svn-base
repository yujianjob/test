using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LazyAtHome.Web.WeiXin3.Controllers
{
    public class WashController : Controller
    {
        public ActionResult Index()
        {
            var rtn = WCF.Wash.Contract.ClientProxy.web_ProductClient.Cache_web_Wash_Class_Second_SELECT_List();
            ViewBag.WashClass = rtn.OBJ;
            return View();
        }

        public JsonResult ClassShow(int classid)
        {
            int Code = 0;
            string Msg = "";

            var rtn = WCF.Wash.Contract.ClientProxy.web_ProductClient.Cache_web_Wash_Category_SELECT_List(classid, AppConfig.SiteID);
            if (rtn.Success)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                foreach (var item in rtn.OBJ)
                {
                    foreach (var detail in item.ProductList)
                    {
                        sb.Append("<div class=\"unit\"><span class=\"bt\">" + detail.Name + "</span>");
                        if (detail.MarketPrice != detail.SalesPrice)
                            sb.Append("<span class=\"price\"><s>￥" + detail.MarketPrice + "</s> <font class=\"green\">￥" + detail.SalesPrice + "</font></span>");
                        else
                            sb.Append("<span class=\"price\">￥" + detail.MarketPrice + "</span>");
                        //sb.Append("<span class=\"add\" onclick=\"AddToCart(" + detail.ID + ", true, event, true)\">+</span></div>");
                        sb.Append("<span class=\"add\" ontouchend=\"AddToCart(" + detail.ID + ", true, event, true)\">+</span></div>");
                    }
                }
                Code = 1;
                Msg = sb.ToString();
            }
            return Json(new { code = Code, msg = Msg });
        }

        public ActionResult test()
        {
            string address = Server.UrlEncode("上海市上海市杨浦区恒仁路200号(体院)");
            string url = "openid=oJ2qEjrBuY6q1mdH6JX9XndOGacI&mpno=18817873313&areaid=310110&address=" + address + "&couponcode=RH09C0RSL1&username=yjt";            
            url = "http://newsyue.8866.org/Activites/FirstFree?" + url;
            return Redirect( url);
            //var rtn = App_Code.UtilityFunc.WebGetRequest("http://newsyue.8866.org:9000/Activites/FirstFree?openid=1aab");
            //return Content(rtn);
        }
    }
}