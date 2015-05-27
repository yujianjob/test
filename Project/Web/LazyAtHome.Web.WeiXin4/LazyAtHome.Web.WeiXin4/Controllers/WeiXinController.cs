using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Senparc.Weixin.MP;
using Senparc.Weixin.MP.MvcExtension;
using Senparc.Weixin.MP.CommonAPIs;
using LazyAtHome.Library.Pay.MobileEmbed.WXPayment;

namespace LazyAtHome.Web.WeiXin4.Controllers
{
    public class WeiXinController : Controller
    {
        /// <summary>
        /// 微信后台验证地址（使用Get），微信后台的“接口配置信息”的Url填写如：http://weixin.senparc.com/weixin
        /// </summary>
        [HttpGet]
        [ActionName("Index")]
        public ActionResult Get(string signature, string timestamp, string nonce, string echostr)
        {
            if (CheckSignature.Check(signature, timestamp, nonce, AppConfig.WeiXin_Token))
            {
                return Content(echostr); //返回随机字符串则表示验证通过
            }
            else
            {
                return Content("failed:" + signature + "," + CheckSignature.GetSignature(timestamp, nonce, AppConfig.WeiXin_Token) + "。" +
                    "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。");
            }
        }

        /// <summary>
        /// 最简化的处理流程（不加密）
        /// </summary>
        [HttpPost]
        [ActionName("Index")]
        public ActionResult Post(string signature, string timestamp, string nonce, string echostr)
        {
            if (!CheckSignature.Check(signature, timestamp, nonce, AppConfig.WeiXin_Token))
            {
                //return Content("参数错误！");//v0.7-
                return new WeixinResult("参数错误！");//v0.8+
            }

            var messageHandler = new CustomMessageHandler(Request.InputStream, null, 10);

            messageHandler.OmitRepeatedMessage = true;//启用消息去重功能

            messageHandler.Execute();//执行微信处理过程

            //return Content(messageHandler.ResponseDocument.ToString());//v0.7-
            //return new FixWeixinBugWeixinResult(messageHandler);//v0.8+
            return new WeixinResult(messageHandler);//v0.8+
        }

        public ActionResult Welcome()
        {
            return View();
        }

        public ActionResult CreateQrCode(int codeNumber)
        {
            var rtn = Senparc.Weixin.MP.AdvancedAPIs.QrCode.Create(GetToken(), 0, codeNumber);
            return Content("https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=" + rtn.ticket);
        }

        public ActionResult CreateQrCodeBat(int codeNumber, int count)
        {
            string token = GetToken();
            string strRtn = "";
            for (int i = 0; i < count; i++)
            {
                var rtn = Senparc.Weixin.MP.AdvancedAPIs.QrCode.Create(token, 0, codeNumber);
                strRtn += (codeNumber + "：https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=" + rtn.ticket + Environment.NewLine);
                codeNumber++;
            }
            return Content(strRtn);
        }

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
        public ActionResult feedback()
        {
            ResponseHandler resHandler = new ResponseHandler(HttpContext);
            resHandler.init();
            resHandler.setKey(TenpayUtil.key, TenpayUtil.appkey);

            //判断签名
            if (resHandler.isWXsignfeedback())
            {
                //回复服务器处理成功
                App_Code.UtilityFunc.Add("OK:" + resHandler.getDebugInfo());
                return Content("OK");
            }
            else
            {
                //sha1签名失败
                App_Code.UtilityFunc.Add("fail:" + resHandler.getDebugInfo());
                return Content("fail");
            }
        }
	}
}