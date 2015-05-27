﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Senparc.Weixin.MP.AdvancedAPIs;

namespace LazyAtHome.Web.WeiXin4.Controllers
{
    public class OAuth2Controller : Controller
    {
        public ActionResult BaseCallback(string code, string state)
        {
            App_Code.UtilityFunc.Add("OAuth_Response: " + code + " " + state);
            if (string.IsNullOrEmpty(code))
            {
                return Content("您拒绝了授权！");
            }

            //通过，用code换取access_token
            var result = OAuth.GetAccessToken(AppConfig.WeiXin_AppId, AppConfig.WeiXin_AppSecret, code);
            if (result.errcode != Senparc.Weixin.ReturnCode.请求成功)
            {
                return Content("错误：" + result.errmsg);
            }

            HttpCookie cook = new HttpCookie("LazywxInfo4");
            cook.Expires = DateTime.Now.AddDays(7);
            cook.Value = result.openid;
            Response.Cookies.Add(cook);

            //if (result.openid != "ontm8t8X4AdUTYRUK2pNpIE_4Ig4" &&
            //    result.openid != "oSKuOt_IH92WLP7pOzUtvxS6MSpA" &&
            //    result.openid != "ontm8t9Y4NNfQMyEb9kHflv7bvqU")
            //{
            //    Redirect(AppConfig.BaseUrl + "/Home/Index");
            //}

            var rData = state.Split(new string[] { "||" }, StringSplitOptions.None);
            string tagUrl = AppConfig.BaseUrl + "/" + rData[0] + "/" + rData[1];
            if (rData.Length == 3)
                tagUrl += "?" + rData[2].Substring(0,rData[2].Length-1);

            App_Code.UtilityFunc.Add("OAuth_Response TagUrl：" + tagUrl);

            return Redirect(tagUrl);
        }
	}
}