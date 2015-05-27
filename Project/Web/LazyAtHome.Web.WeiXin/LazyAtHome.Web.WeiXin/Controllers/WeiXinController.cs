using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.MvcExtension;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.AdvancedAPIs;

namespace LazyAtHome.Web.WeiXin.Controllers
{
    public class WeiXinController : BaseController
    {
        [HttpGet]
        [ActionName("Index")]
        public ActionResult Get(string signature, string timestamp, string nonce, string echostr)
        {
            if (CheckSignature.Check(signature, timestamp, nonce, CustomerConfig.Token))
            {
                return Content(echostr); //返回随机字符串则表示验证通过
            }
            else
            {
                return Content("failed:" + signature + "," + CheckSignature.GetSignature(timestamp, nonce, CustomerConfig.Token) + "。如果您在浏览器中看到这条信息，表明此Url可以填入微信后台。");
            }
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult Post(string signature, string timestamp, string nonce, string echostr)
        {
            if (!CheckSignature.Check(signature, timestamp, nonce, CustomerConfig.Token))
            {
                return Content("参数错误！");
            }

            var messageHandler = new WeiXin.CustomMessageHandler(Request.InputStream);//接收消息

            messageHandler.Execute();//执行微信处理过程

            return new WeixinResult(messageHandler);//返回结果
        }

        public ActionResult UserInfo(string openID)
        {
            string token = AccessTokenContainer.TryGetToken(CustomerConfig.AppId, CustomerConfig.AppSecret);
            //string openID = CustomerConfig.UserOpenID;
            var user = CommonApi.GetUserInfo(token, openID);
            return View(user);
        }

        public ActionResult OAuth_Response(string code, string state)
        {
            UtilityFunc.Add("OAuth_Response: " + code + " " + state);
            OAuthAccessTokenResult rtn = null;
            try
            {
                rtn = Senparc.Weixin.MP.AdvancedAPIs.OAuth.GetAccessToken(CustomerConfig.AppId, CustomerConfig.AppSecret, code);
            }
            catch (Exception ex)
            {
                return Content(ex.Message + " - " + code + " - " + state);
            }

            HttpCookie cook = new HttpCookie("LazywxInfo");
            cook.Expires = DateTime.Now.AddDays(7);
            cook.Value = rtn.openid;
            Response.Cookies.Add(cook);

            var rData = state.Split('_');

            return RedirectToAction(rData[1], rData[0]);
        }

        public ActionResult Welcome()
        {
            return View();
        }

        public ActionResult CreateQrCode()
        {
            var rtn = Senparc.Weixin.MP.AdvancedAPIs.QrCode.Create(GetToken(), 0, 11);
            return Content("https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=" + rtn.ticket);
        }

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
    }
}