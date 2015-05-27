using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LazyAtHome.Web.WeiXin2.Controllers
{
    public class WashController : Controller
    {
        //
        // GET: /Wash/
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
                    sb.Append("<div class=\"panel panel-default\"><div class=\"panel-heading\" data-parent=\"#accordion\" data-target=\"#collapse" + item.ID + "\" data-toggle=\"collapse\" onclick=\"showXdfw(this);\"><h4 class=\"panel-title\"><span class=\"cloth-bt\">" + item.Name + "</span>");
                    sb.Append("<a data-toggle=\"collapse\">∧</a></h4></div>");
                    sb.Append("<div id=\"collapse" + item.ID + "\" class=\"panel-collapse collapse\"><div class=\"panel-body\"><table class=\"table cloth-list\"><tbody>");
                    foreach (var detail in item.ProductList)
                    {
                        sb.Append("<tr><td>" + detail.Name + "</td><td align='left'>");
                        if (detail.MarketPrice != detail.SalesPrice)
                            sb.Append("<strike>￥" + detail.MarketPrice + "</strike> <font style='color:#e95259'>" + detail.SalesPrice + "</font>");
                        else
                            sb.Append("￥" + detail.MarketPrice);
                        sb.Append("</td><td><button class=\"btn btn-default\" onclick=\"AddToCart(" + detail.ID + ", true)\">+</button></td></tr>");
                    }
                    sb.Append("</tbody></table></div></div></div>");
                }
                Code = 1;
                Msg = sb.ToString();
            }

            return Json(new { code = Code, msg = Msg });
        }
    }
}