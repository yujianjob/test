using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Collections.Specialized;

namespace LazyAtHome.Library.Pay.MobileEmbed.Alipay
{
    public class AlipayEmbed
    {
        public static string Notify_url = "http://newsyue.8866.org/MobileService/Alipay_Notify.aspx";
        public static string Return_url = null;

        public static string CreateParam(string out_trade_no, string subject, string body, decimal total_fee)
        {
            //string result = "partner=\"{0}\"&seller=\"{1}\"&out_trade_no=\"{2}\"&subject=\"{3}\"&body=\"{4}\"&total_fee=\"{5}\"&notify_url=\"{6}\"&sign_type=\"{7}\"";
            string result = "partner=\"{0}\"&seller=\"{1}\"&out_trade_no=\"{2}\"&subject=\"{3}\"&body=\"{4}\"&total_fee=\"{5}\"&notify_url=\"{6}\"";
            result = string.Format(result, Common.Alipay.Config.Partner, Common.Alipay.Config.Partner, out_trade_no, subject, body, total_fee.ToString("0.00"), Notify_url);

            string sign = Common.RSAFromPkcs8.sign(result, Common.Alipay.Config.Key, Common.Alipay.Config.Input_charset);

            sign = System.Web.HttpUtility.UrlEncode(sign);
            result += "&sign=\"" + sign + "\"";
            return result;
        }

        /// <summary>
        /// 支付宝1.2版本参数生成
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="total_fee"></param>
        /// <returns></returns>
        public static string CreateParamV12(string out_trade_no, string subject, string body, decimal total_fee)
        {
            SortedDictionary<string, string> sPara = new SortedDictionary<string, string>();
            sPara.Add("partner", Common.Alipay.Config.Partner);
            sPara.Add("seller_id", Common.Alipay.Config.Partner);
            sPara.Add("out_trade_no", out_trade_no);
            sPara.Add("subject", subject);
            sPara.Add("body", body);
            sPara.Add("total_fee", total_fee.ToString("0.00"));
            sPara.Add("notify_url", HttpUtility.UrlEncode(Notify_url));
            sPara.Add("service", "mobile.securitypay.pay");
            sPara.Add("_input_charset", "utf-8");
            sPara.Add("payment_type", "1");
            if (!string.IsNullOrWhiteSpace(Return_url))
                sPara.Add("return_url", HttpUtility.UrlEncode(Return_url));
            sPara.Add("it_b_pay", "15m");
            //sPara.Add("show_url", "www.xxx.com");

            string result = CreateLinkString(sPara);

            //string format = "partner=\"{0}\"&seller_id=\"{1}\"&out_trade_no=\"{2}\"&subject=\"{3}\"&body=\"{4}\"&total_fee=\"{5}\"&notify_url=\"{6}\"&service=\"mobile.securitypay.pay\"&payment_type=\"1\"&_input_charset=\"utf-8\"&it_b_pay=\"15m\"";
            //string rtn = string.Format(format, AliPay_PartnerID, AliPay_AccountID, out_trade_no, subject, body, total_fee.ToString("0.00"), HttpUtility.UrlEncode(Notify_url));
            //string result = string.Format(format, AliPay_PartnerID, AliPay_AccountID, out_trade_no, subject, body, total_fee.ToString("0.00"), Notify_url);

            string sign = Common.RSAFromPkcs8.sign(result, Common.Alipay.Config.Private_key, Common.Alipay.Config.Input_charset);
            sign = HttpUtility.UrlEncode(sign);
            result += "&sign=\"" + sign + "\"&sign_type=\"RSA\"";
            return result;
        }

        private static string CreateLinkString(SortedDictionary<string, string> dicArray)
        {
            StringBuilder prestr = new StringBuilder();
            foreach (KeyValuePair<string, string> temp in dicArray)
            {
                prestr.Append(temp.Key + "=\"" + temp.Value + "\"&");
            }

            //去掉最後一個&字符
            int nLen = prestr.Length;
            prestr.Remove(nLen - 1, 1);

            return prestr.ToString();
        }

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public static SortedDictionary<string, string> GetRequestPost(NameValueCollection coll)
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], HttpUtility.UrlDecode(coll[requestItem[i]]));
            }

            return sArray;
        }
    }
}
