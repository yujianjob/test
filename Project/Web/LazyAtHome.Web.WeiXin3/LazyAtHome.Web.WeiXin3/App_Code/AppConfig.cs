using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WeiXin3
{
    public class AppConfig
    {
        public static int SiteID = 1;
        internal const string CookingName = "";

        //**************************正式环境*********************************
        public const string BaseUrl = "http://wx.landaojia.com";
        internal const string WeiXin_AppId = "wx9ab0f092bce2810f";//正式
        internal const string WeiXin_AppSecret = "a901c2204102f19278af8b30eac6d450";//正式
        //*******************************************************************

        //**************************测试环境*********************************
        //public const string BaseUrl = "http://newsyue.8866.org";
        //internal const string WeiXin_AppId = "wx35d137ac8ed7c11a";//测试
        //internal const string WeiXin_AppSecret = "d0b0dde598e112d1972a5257fc08a495";//测试
        //*******************************************************************

        /// <summary>
        /// 免费产品ID组
        /// </summary>
        public static int[] FreeProductID = { 1, 3, 5, 6, 9, 10, 12, 13, 14, 16, 17, 22, 23, 24, 25, 26, 27, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 67, 112, 113, 117, 121, 123, 142 };

        internal const string Alipay_Partner = "2088412243883939";
        internal const string Alipay_Key = "5cjljee4z9coxxfmyvogt4kzkixml3kv";
        internal const string Alipay_Seller_Email = "host@landaojia.com";
        internal const string Alipay_Notify_URL = BaseUrl + "/Notify/AlipayNotify";
        internal const string Alipay_Return_URL = BaseUrl + "/Cart/AlipayReturn";

        internal const string WeiXin_Token = "LazyAtHomeWeiXinTokenAAABB";
        internal const string WeiXin_AuthorizeRedirectUrl = BaseUrl + "/weixin/OAuth_Response";
        internal static string WeiXin_UserOpenID = "";

        internal static string Baidu_AK = "XHMrT6GKi5OGpGpNlBuD5gGc";

        internal const string wxPay_AppID = "wx9ab0f092bce2810f";
        internal const string wxPay_Mch_ID = "10016804";
        internal const string wxPay_Notify_URL = BaseUrl + "/Notify/WxPayNotify";



        /// <summary>
        /// 验证码
        /// </summary>
        public const string Cache_ValidateCode = "vCode";
        /// <summary>
        /// 短信验证码
        /// </summary>
        public const string Cache_SmsCode = "smsCode";

        public const string Cache_CallbackUrl = "callback";
        public const string Cache_Location = "loc";
    }
}