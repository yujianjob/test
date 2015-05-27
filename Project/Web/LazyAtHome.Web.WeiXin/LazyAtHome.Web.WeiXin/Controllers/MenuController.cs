using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Senparc.Weixin.MP;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities.Menu;

namespace LazyAtHome.Web.WeiXin.Controllers
{
    public class MenuController : Controller
    {

        public ActionResult Test()
        {
            return View();
        }

        private string GetToken()
        {
            try
            {
                if (!AccessTokenContainer.CheckRegistered(CustomerConfig.AppId))
                {
                    AccessTokenContainer.Register(CustomerConfig.AppId, CustomerConfig.AppSecret);
                }
                var result = AccessTokenContainer.GetToken(CustomerConfig.AppId); //CommonAPIs.CommonApi.GetToken(appId, appSecret);
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet]
        public ActionResult CreateMenu()
        {
            ButtonGroup bg = new ButtonGroup();

            SingleViewButton vbGXService = new SingleViewButton();
            vbGXService.name = "干洗服务";
            vbGXService.url = CustomerConfig.BaseUrl + "/Wash";

            bg.button.Add(vbGXService);

            //===============================================================================
            SubButton sbTopRecommended = new SubButton();
            sbTopRecommended.name = "热门推荐";

            SingleViewButton vbSpecialEvents = new SingleViewButton();
            vbSpecialEvents.name = "我要领现金";
            vbSpecialEvents.url = CustomerConfig.BaseUrl + "/Top/SpecialEvents";

            sbTopRecommended.sub_button.Add(vbSpecialEvents);

            bg.button.Add(sbTopRecommended);

            //===============================================================================

            SubButton sbPersonalCenter = new SubButton();
            sbPersonalCenter.name = "个人中心";

            SingleViewButton vbMemberCenter = new SingleViewButton();
            vbMemberCenter.name = "会员中心";
            vbMemberCenter.url = CustomerConfig.BaseUrl + "/PersonalCenter/MemberCenter";

            //SingleViewButton vbMyFavorites = new SingleViewButton();
            //vbMyFavorites.name = "我的收藏";
            //vbMyFavorites.url = CustomerConfig.BaseUrl + "PersonalCenter/MyFavorites";

            SingleViewButton vbNotice = new SingleViewButton();
            vbNotice.name = "通知";
            vbNotice.url = CustomerConfig.BaseUrl + "/PersonalCenter/Notice";

            sbPersonalCenter.sub_button.Add(vbMemberCenter);
            //sbPersonalCenter.sub_button.Add(vbMyFavorites);
            sbPersonalCenter.sub_button.Add(vbNotice);

            bg.button.Add(sbPersonalCenter);

            //===============================================================================

            try
            {
                string token = GetToken();

                var result = Senparc.Weixin.MP.CommonAPIs.CommonApi.CreateMenu(token, bg);
                var json = new
                {
                    Success = result.errmsg == "ok",
                    Message = result.errmsg
                };
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var json = new { Success = false, Message = ex.Message };
                return Json(json, JsonRequestBehavior.AllowGet);
            }
        }
    }
}