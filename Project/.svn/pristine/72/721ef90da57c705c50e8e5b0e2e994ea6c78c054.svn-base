using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Specialized;

using LazyAtHome.Library.Pay.MobileEmbed.Alipay;
using LazyAtHome.Library.Pay.MobileEmbed.WXPayment;
using System.Xml;

namespace LazyAtHome.Web.WeiXin3.Controllers
{
    public class NotifyController : Controller
    {
        [ValidateInput(false)]
        public ActionResult AlipayNotify()
        {
            App_Code.UtilityFunc.Add("------------------------------------\r\nCall Notify AlipayNotify");
            Dictionary<string, string> sPara = null;

            try
            {
                sPara = GetRequest("post");
            }
            catch (Exception ex)
            {
                App_Code.UtilityFunc.Add("GetRequest:" + ex.Message);
                return Content("fail");
            }

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                Notify aliNotify = new Notify();
                bool verifyResult = aliNotify.VerifyNotify(sPara, Request.Form["sign"]);
                App_Code.UtilityFunc.Add("verifyResult:" + verifyResult);

                if (verifyResult)//验证成功
                {
                    //解密（如果是RSA签名需要解密，如果是MD5签名则下面一行清注释掉）
                    //sPara = aliNotify.Decrypt(sPara);

                    //XML解析notify_data数据
                    try
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(sPara["notify_data"]);
                        //商户订单号
                        string out_trade_no = xmlDoc.SelectSingleNode("/notify/out_trade_no").InnerText;
                        //支付宝交易号
                        string trade_no = xmlDoc.SelectSingleNode("/notify/trade_no").InnerText;
                        //交易状态
                        string trade_status = xmlDoc.SelectSingleNode("/notify/trade_status").InnerText;
                        App_Code.UtilityFunc.Add("trade_status:" + trade_status);

                        decimal alipayMoney = decimal.Parse(xmlDoc.SelectSingleNode("/notify/total_fee").InnerText);
                        DateTime alipayPayTime = DateTime.Parse(xmlDoc.SelectSingleNode("/notify/gmt_payment").InnerText);

                        if (trade_status == "TRADE_FINISHED" || trade_status == "TRADE_SUCCESS")
                        {
                            var rtn = App_Code.Proxy.OrderProxy.Update_OrderPay(out_trade_no, alipayMoney, WCF.OrderSystem.Contract.Enumerate.PayMoneyType.AliPay, WCF.OrderSystem.Contract.Enumerate.PayMoneyChannel.Weixin, alipayPayTime, trade_no);
                            if (!rtn.Success)
                            {
                                if (rtn.Code != -70)
                                    App_Code.UtilityFunc.Add("处理订单失败：" + out_trade_no + " Error:" + rtn.Message);
                            }
                            return Content("success");
                        }
                        else
                        {
                            App_Code.UtilityFunc.Add(trade_status);
                            return Content(trade_status);
                        }

                    }
                    catch (Exception exc)
                    {
                        App_Code.UtilityFunc.Add(exc.ToString());
                        return Content(exc.ToString());
                    }
                }
                else//验证失败
                {
                    App_Code.UtilityFunc.Add("fail");
                    return Content("fail");
                }
            }
            else
            {
                App_Code.UtilityFunc.Add("无通知参数");
                return Content("无通知参数");
            }
        }

        /// <summary>
        /// 获取支付宝GET过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        [ValidateInput(false)]
        public Dictionary<string, string> GetRequest(string type)
        {
            Dictionary<string, string> sArray = new Dictionary<string, string>();
            NameValueCollection coll;
            if (type == "get")
                coll = Request.QueryString;
            else
                coll = Request.Form;

            String[] requestItem = coll.AllKeys;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < requestItem.Length; i++)
            {
                sb.Append(requestItem[i] + ":" + coll[requestItem[i]] + "; ");
                sArray.Add(requestItem[i], coll[requestItem[i]]);
            }

            App_Code.UtilityFunc.Add("支付宝返回参数：" + sb.ToString());

            return sArray;
        }

        [ValidateInput(false)]
        public ActionResult WxPayNotify()
        {
            App_Code.UtilityFunc.AddToFile("------------------------------------\r\nCall WxPayNotify", "wxPay");

            WXPay pay = new WXPay();
            var rtnXML = pay.GetResponseXML(HttpContext);
            App_Code.UtilityFunc.AddToFile(rtnXML, "wxPay");

            if (pay.Parameters.Count > 0)
            {
                string sign = pay.CreateSign();
                if (sign != pay.Parameters["sign"])
                {
                    App_Code.UtilityFunc.AddToFile("验证签名失败 sign:" + sign + " wxSign:" + pay.Parameters["sign"], "wxPay");
                }
                string out_trade_no = pay.Parameters["out_trade_no"];
                string transaction_id = pay.Parameters["transaction_id"];
                //金额,以分为单位
                decimal total_fee = int.Parse(pay.Parameters["total_fee"]) / 100m;

                var rtn = App_Code.Proxy.OrderProxy.Update_OrderPay(out_trade_no, total_fee, WCF.OrderSystem.Contract.Enumerate.PayMoneyType.Weixin, WCF.OrderSystem.Contract.Enumerate.PayMoneyChannel.Weixin, DateTime.Now, transaction_id);
                if (!rtn.Success)
                {
                    if (rtn.Code != -70)
                    {
                        App_Code.UtilityFunc.AddToFile("处理订单失败：" + out_trade_no + " Error:" + rtn.Message, "wxPay");
                        return Content("<xml><return_code><![CDATA[FAIL]]></return_code><return_msg><![CDATA[处理订单失败]]></return_msg></xml>");
                    }
                }
                return Content("<xml><return_code><![CDATA[SUCCESS]]></return_code><return_msg></return_msg></xml>");
            }
            else
            {
                App_Code.UtilityFunc.AddToFile("参数格式校验错误", "wxPay");
                return Content("<xml><return_code><![CDATA[FAIL]]></return_code><return_msg><![CDATA[签名失败]]></return_msg></xml>");
            }
        }
    }
}