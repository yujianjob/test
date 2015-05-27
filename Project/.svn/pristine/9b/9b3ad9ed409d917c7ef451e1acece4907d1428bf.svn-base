using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LazyAtHome.Library.Pay.MobileEmbed.WXPayment;

namespace LazyAtHome.Library.Pay.Test_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            WXPay pay = new WXPay();
            pay.SetParameter("appid", "wx9ab0f092bce2810f");//公众账号ID
            pay.SetParameter("mch_id", "10016804");//商户号
            pay.SetParameter("nonce_str", TenpayUtil.getNoncestr());//随机字符串            
            pay.SetParameter("body", "开发测试商品描述");//商品描述
            pay.SetParameter("out_trade_no", "2014555544446687");//商户订单号
            pay.SetParameter("total_fee", "1700");//总金额 单位为分，不能带小数点
            pay.SetParameter("spbill_create_ip", "218.242.148.39");//终端IP
            pay.SetParameter("notify_url", "http://wx.landaojia.com/Cart/WxPayNotify");//通知地址
            pay.SetParameter("trade_type", "JSAPI");//交易类
            pay.SetParameter("openid", "ontm8t4Zdpy3cSmOrHaelrKjEK1Q");//用户标识
            pay.CreateSign();
            string postXml = pay.ConvertToXml();
            Console.WriteLine(postXml);
            Console.WriteLine(Environment.NewLine);

            string rtn = pay.BuildRequest(postXml);
            Console.WriteLine(rtn);
            pay.ParseXML(rtn);
            Console.WriteLine(pay.Parameters.Count);
            Console.ReadLine();
        }
    }
}
