using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WashSite
{
    public class AppConfig
    {
        public static int SiteID = 1;

        internal const string Alipay_Partner = "2088412243883939";
        internal const string Alipay_Key = "5cjljee4z9coxxfmyvogt4kzkixml3kv";        
        internal const string Alipay_Seller_Email = "host@landaojia.com";
        internal const string Alipay_Notify_URL = "http://www.landaojia.com/Cart/AlipayNotify";
        internal const string Alipay_Return_URL = "http://www.landaojia.com/Cart/AlipayReturn";
        //internal const string Alipay_Notify_URL = "http://newsyue.8866.org/Cart/AlipayNotify";
        //internal const string Alipay_Return_URL = "http://newsyue.8866.org/Cart/AlipayReturn";

        /// <summary>
        /// 验证码
        /// </summary>
        public const string Cache_ValidateCode = "vCode";
        /// <summary>
        /// 短信验证码
        /// </summary>
        public const string Cache_SmsCode = "smsCode";
    }
}