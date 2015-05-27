using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Senparc.Weixin.MP;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities.Menu;

namespace LazyAtHome.Web.WeiXin4.Controllers
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
            vbOneKey.name = "我要洗衣";
            vbOneKey.url = AppConfig.BaseUrl + "/Cart/OneKey";

            bg.button.Add(vbOneKey);

            //===============================================================================
            SubButton sbBonuseActive = new SubButton();
            sbBonuseActive.name = "百万红包";

            SingleViewButton sbBonuse = new SingleViewButton();
            sbBonuse.name = "百万红包";
            sbBonuse.url = AppConfig.BaseUrl + "/Member/SystemBonuses";

            SingleViewButton sbActive = new SingleViewButton();
            sbActive.name = "缤纷活动";
            sbActive.url = AppConfig.BaseUrl + "/Home/Activites";

            sbBonuseActive.sub_button.Add(sbBonuse);
            sbBonuseActive.sub_button.Add(sbActive);

            bg.button.Add(sbBonuseActive);
            //===============================================================================

            SubButton sbPersonalCenter = new SubButton();
            sbPersonalCenter.name = "帮助中心";

            SingleViewButton vbCommunity = new SingleViewButton();
            vbCommunity.name = "微社区";
            vbCommunity.url = "http://m.wsq.qq.com/263973430";

            SingleViewButton vbMemberCenter = new SingleViewButton();
            vbMemberCenter.name = "下载APP";
            vbMemberCenter.url = AppConfig.BaseUrl + "/Home/DownAPP";

            //SingleViewButton vbNotice = new SingleViewButton();
            //vbNotice.name = "问题反馈";
            //vbNotice.url = AppConfig.BaseUrl + "/";

            SingleViewButton vbTest = new SingleViewButton();
            vbTest.name = "客服热线";
            vbTest.url = "http://mp.weixin.qq.com/s?__biz=MzA4MjYwNzAwNg==&mid=201518587&idx=1&sn=4a722f4913e59468e5181a3e026700c8#rd";

            sbPersonalCenter.sub_button.Add(vbCommunity);
            sbPersonalCenter.sub_button.Add(vbMemberCenter);
            //sbPersonalCenter.sub_button.Add(vbNotice);
            sbPersonalCenter.sub_button.Add(vbTest);

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