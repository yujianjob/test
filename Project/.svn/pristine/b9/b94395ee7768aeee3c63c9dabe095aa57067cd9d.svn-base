using System.Web;
using System.Text;
using System.IO;
using System.Net;
using System;
using System.Collections.Generic;

namespace LazyAtHome.Library.Pay.Common.Alipay
{
    /// <summary>
    /// 类名：Config
    /// 功能：基础配置类
    /// 详细：设置帐户有关信息及返回路径
    /// 版本：3.3
    /// 日期：2012-07-05
    /// 说明：
    /// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    /// 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
    /// 
    /// 如何获取安全校验码和合作身份者ID
    /// 1.用您的签约支付宝账号登录支付宝网站(www.alipay.com)
    /// 2.点击“商家服务”(https://b.alipay.com/order/myOrder.htm)
    /// 3.点击“查询合作者身份(PID)”、“查询安全校验码(Key)”
    /// </summary>
    public class Config
    {
        #region 字段
        private static string partner = "";
        private static string key = "";
        private static string private_key = "";
        private static string public_key = "";
        private static string input_charset = "";
        private static string sign_type = "";
        private static string selleremail = "host@landaojia.com";
        #endregion

        static Config()
        {
            //↓↓↓↓↓↓↓↓↓↓请在这里配置您的基本信息↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓

            //合作身份者ID，以2088开头由16位纯数字组成的字符串
            partner = "2088412243883939";

            //交易安全检验码，由数字和字母组成的32位字符串
            //如果签名方式设置为“MD5”时，请设置该参数
            key = "5cjljee4z9coxxfmyvogt4kzkixml3kv";

            //商户的私钥
            //如果签名方式设置为“0001”时，请设置该参数
            private_key = @"MIICdwIBADANBgkqhkiG9w0BAQEFAASCAmEwggJdAgEAAoGBAK+IfHcEueXWAD5nth6DxRndL4CFzlgyGipAlqWNGUJDHZ7AXjhre+uZBmyJeBctou+Mu+lDQucsX4VFEObauBaxyZ5wGVCmppmSfrtpZu4twKo45skkLTGK1HtjyL9KQRm4Nzps8BYMZUKBmcGnYQuzWNRSxx6fLEUqfUwJK/ANAgMBAAECgYEAlWRjEeSK1qqQoIwh+syJFp8iC49V88gIbsdzr0hsI/H9JpxwVDJeUEB3sC47b/aeQqSTVU6yhQ9F6KXEYcXqf7SvIY2mZ7Ae+UCyrue92JasiE7Rn1+0GX3uft4LsUxQw8uUKCEQwUcugHGAUGWONYeBbbN6OiheXxfPJYL1OH0CQQDg7h1QQvhzQgCtZ/E7u9CKLYNcFB6I089oE/+eQVBVXUUEhLOV7WNPSqMGzHPJunX1vvEUUiwn7WMJgKWMFWI3AkEAx8eduUyCcSYktmvTNqywQ47AF8crGBmKJg55AZB8wf+/4aeA6wuOBEloVZlTgwKsIWnkZXvBXfMIHyGu5qzt2wJAZ/HiHgs04Z6ozXnxYxdiQdjSkaTCj5zChmhngmzQgQJ/OJ1SmICBmkz1ldi50YmXpZ89rZRjz3fGgseuuVPQdQJADas2u2rkseEuOdz+wormNHkb44SZCjkVHq120giUwKFC+6l+RJaxzNFI9jJbaGdSZ6bbHaZuAIOUVqzzTo0eaQJBAJUH4fVTE7C9gEXmsmTTDNLcJjwC8F42u3j13QJRQ/KE5koDPzgrjQL3UNkWgMq7eI8GNy0jAc+BqlHgc7IzY3Q=";

            //支付宝的公钥
            //如果签名方式设置为“0001”时，请设置该参数
            public_key = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCnxj/9qwVfgoUh/y2W89L6BkRAFljhNhgPdyPuBV64bfQNN1PjbCzkIM6qRdKBoLPXmKKMiFYnkd6rAoprih3/PrQEB/VsW8OoM8fxn67UDYuyBTqA23MML9q1+ilIZwBC2AQ2UBVOrFXfFl75p6/B5KsiNG9zpgmLCUYuLkxpLQIDAQAB";

            //↑↑↑↑↑↑↑↑↑↑请在这里配置您的基本信息↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑



            //字符编码格式 目前支持 utf-8
            input_charset = "utf-8";

            //签名方式，选择项：0001(RSA)、MD5
            sign_type = "MD5";
            //无线的产品中，签名方式为rsa时，sign_type需赋值为0001而不是RSA
        }

        #region 属性
        /// <summary>
        /// 获取或设置合作者身份ID
        /// </summary>
        public static string Partner
        {
            get { return partner; }
            set { partner = value; }
        }

        /// <summary>
        /// 获取或设交易安全校验码
        /// </summary>
        public static string Key
        {
            get { return key; }
            set { key = value; }
        }

        /// <summary>
        /// 账户EMAIL
        /// </summary>
        public static string Seller_Email
        {
            get { return selleremail; }
            set { selleremail = value; }
        }

        /// <summary>
        /// 获取或设置商户的私钥
        /// </summary>
        public static string Private_key
        {
            get { return private_key; }
            set { private_key = value; }
        }

        /// <summary>
        /// 获取或设置支付宝的公钥
        /// </summary>
        public static string Public_key
        {
            get { return public_key; }
            set { public_key = value; }
        }

        /// <summary>
        /// 获取字符编码格式
        /// </summary>
        public static string Input_charset
        {
            get { return input_charset; }
        }

        /// <summary>
        /// 获取签名方式
        /// </summary>
        public static string Sign_type
        {
            get { return sign_type; }
        }
        #endregion
    }
}