using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LazyAtHome.Web.WeiXin.Controllers
{
    public class TopController : BaseController
    {
        [NoCacheFilter]
        public ActionResult SpecialEvents(string code)
        {
            if (!string.IsNullOrWhiteSpace(code))
            {
                HttpCookie cook = new HttpCookie("LazywxLinkCode");
                cook.Expires = DateTime.Now.AddDays(7);
                cook.Value = code;
                Response.Cookies.Add(cook);
            }

            var user = GetUserInfo();            
            ViewBag.Count = 0;
            if (user.User != null)
            {
                ViewBag.Code = user.User.UserInfo.RecommendedCode;
                ViewBag.IsGet = true;
                var rtn = App_Code.Proxy.UserProxy.User_Invite_Count(user.User.UserInfo.ID);
                if (rtn.Success)
                    ViewBag.Count = rtn.OBJ;
            }
            else
            {
                ViewBag.IsGet = false;
                HttpContext.Cache[CustomerConfig.Cache_CallbackUrl + _UserOpenID] = Request.Url.ToString();
            }

            ViewBag.IsUser = 0;
            var openIDCheck = App_Code.Proxy.UserProxy.User_WeixinAttention_Check(_UserOpenID);
            if (openIDCheck.Success && openIDCheck.OBJ)
                ViewBag.IsUser = 1;

            return View();
        }

        public ActionResult ActivesA()
        {
            return View();
        }

        public ActionResult ActivesB()
        {
            return View();
        }

        public ActionResult ActivesC()
        {
            return View();
        }

        public ActionResult GuanZhu(string code)
        {
            if (!string.IsNullOrWhiteSpace(code))
            {
                HttpCookie cook = new HttpCookie("LazywxLinkCode");
                cook.Expires = DateTime.Now.AddDays(7);
                cook.Value = code;
                Response.Cookies.Add(cook);
            }
            return Redirect("http://mp.weixin.qq.com/s?__biz=MzA4MjYwNzAwNg==&mid=200250752&idx=1&sn=ea5dbd8cdc5cde20fb0a711ee40f10c7#rd");            
        }
	}
}