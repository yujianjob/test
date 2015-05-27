using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Senparc.Weixin.MP;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities.Menu;

namespace LazyAtHome.Web.WeiXin2.Controllers
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
                if (!AccessTokenContainer.CheckRegistered(AppConfig.WeiXin_AppId))
                {
                    AccessTokenContainer.Register(AppConfig.WeiXin_AppId, AppConfig.WeiXin_AppSecret);
                }
                var result = AccessTokenContainer.GetToken(AppConfig.WeiXin_AppId); //CommonAPIs.CommonApi.GetToken(appId, appSecret);
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

            SingleViewButton vbOneKey = new SingleViewButton();
            vbOneKey.name = "一键下单";
            vbOneKey.url = AppConfig.BaseUrl + "/Cart/OneKey";

            bg.button.Add(vbOneKey);

            //===============================================================================


            SingleViewButton vbFree = new SingleViewButton();
            vbFree.name = "免费洗衣";
            vbFree.url = AppConfig.BaseUrl + "/Wash";

            bg.button.Add(vbFree);

            //===============================================================================

            SubButton sbPersonalCenter = new SubButton();
            sbPersonalCenter.name = "用户中心";

            SingleViewButton vbMemberCenter = new SingleViewButton();
            vbMemberCenter.name = "个人信息";
            vbMemberCenter.url = AppConfig.BaseUrl + "/Member";

            SingleViewButton vbNotice = new SingleViewButton();
            vbNotice.name = "地址管理";
            vbNotice.url = AppConfig.BaseUrl + "/Member/AddressManage";

            sbPersonalCenter.sub_button.Add(vbMemberCenter);
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