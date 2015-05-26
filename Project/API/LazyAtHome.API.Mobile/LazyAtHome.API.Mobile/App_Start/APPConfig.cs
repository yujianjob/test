using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace LazyAtHome.API.Mobile
{
    public class APPConfig
    {
        /// <summary>
        /// 加密串
        /// </summary>
        public const string Key = "lz123456789012345678901234567890";


        public static byte[] Des_key = Encoding.UTF8.GetBytes("123456789012345678901234");
        public static byte[] Des_iv = Encoding.UTF8.GetBytes("12345678");

        public static string startpage = ConfigurationManager.AppSettings["startpage"];

        public static string alipaynotifyurl = ConfigurationManager.AppSettings["AlipayNotifyUrl"];


        public static string iphone_download_url = ConfigurationManager.AppSettings["iphone_download_url"];
        public static string android_download_url = ConfigurationManager.AppSettings["android_download_url"];


    }
}