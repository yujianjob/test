using LazyAtHome.API.Mobile.App_Code;
using LazyAtHome.API.Mobile.App_Code.Proxy;
using LazyAtHome.Library.Pay.Common.Alipay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LazyAtHome.API.Mobile.Controllers
{
    public class NotifyController : Controller
    {
        public ActionResult Test()
        {
            return Content("OK");
        }

        //
        // GET: /Alipay/
        [ValidateInput(false)]
        public ActionResult Alipay()
        {
            System.Collections.Specialized.NameValueCollection _Params = null;
            if (Request.RequestType == "GET")
                _Params = Request.QueryString;
            else
                _Params = Request.Form;

            ParamsLog(Request.Path + " " + Request.RequestType, _Params.ToString());

            SortedDictionary<string, string> sPara = LazyAtHome.Library.Pay.MobileEmbed.Alipay.AlipayEmbed.GetRequestPost(_Params);

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                Notify aliNotify = new Notify();
                bool verifyResult = aliNotify.Verify(sPara, sPara["notify_id"], sPara["sign"]);

                verifyResult = true;

                if (verifyResult)//验证成功
                //if (true)
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //请在这里加上商户的业务逻辑程序代码

                    //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                    //获取支付宝的通知返回参数，可参考技术文档中服务器异步通知参数列表

                    //商户订单号                 
                    string out_trade_no = sPara["out_trade_no"];

                    //支付宝交易号
                    string trade_no = sPara["trade_no"];

                    //支付金额
                    decimal total_fee = Convert.ToDecimal(sPara["total_fee"]);

                    //
                    DateTime gmt_payment = Convert.ToDateTime(sPara["gmt_payment"]);

                    //交易状态
                    string trade_status = sPara["trade_status"];


                    PublicFun.LogAdd(string.Format("AlipayNotify:{0} {1} {2}", out_trade_no, trade_no, trade_status));

                    if (trade_status == "TRADE_FINISHED" || trade_status == "TRADE_SUCCESS")
                    {
                        //判断该笔订单是否在商户网站中已经做过处理
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //如果有做过处理，不执行商户的业务程序

                        //注意：
                        //该种交易状态只在两种情况下出现
                        //1、开通了普通即时到账，买家付款成功后。
                        //2、开通了高级即时到账，从该笔交易成功时间算起，过了签约时的可退款时限（如：三个月以内可退款、一年以内可退款等）后。

                        //获取订单
                        var order = OrderProxy.Order_Order_SELECT_Entity(out_trade_no, false, false, false, false, true, false);
                        if (order == null || order.OBJ == null)
                        {
                            //订单不存在
                            PublicFun.LogAdd("AlipayNotify:订单不存在");
                        }
                        else if (order.OBJ.OrderStatus != 1)
                        {
                            //订单状态错误
                            PublicFun.LogAdd("AlipayNotify:订单状态错误");
                        }
                        else if (order.OBJ.PaymentList.Count(p => p.PayMoneyType == 3 && p.RelationID == trade_no) > 0)
                        {
                            //已经回调支付过
                            PublicFun.LogAdd("AlipayNotify:已经回调支付过");
                        }
                        else
                        {
                            var payRtn = OrderProxy.Order_Order_Pay(out_trade_no, total_fee,
                                WCF.OrderSystem.Contract.Enumerate.PayMoneyType.AliPay,
                                WCF.OrderSystem.Contract.Enumerate.PayMoneyChannel.APP,
                                gmt_payment, trade_no);
                            if (payRtn == null)
                            {
                                PublicFun.LogAdd("AlipayNotify:支付接口返回空");
                            }
                            else if (!payRtn.Success)
                            {
                                PublicFun.LogAdd("AlipayNotify:支付接口返回错误:" + payRtn.Code);
                            }
                            else
                            {
                                PublicFun.LogAdd("AlipayNotify:支付成功");
                            }
                        }

                    }
                    //else if (Request.Form["trade_status"] == "TRADE_SUCCESS")
                    //{
                    //    //判断该笔订单是否在商户网站中已经做过处理
                    //    //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                    //    //如果有做过处理，不执行商户的业务程序

                    //    //注意：
                    //    //该种交易状态只在一种情况下出现——开通了高级即时到账，买家付款成功后。



                    //}
                    else
                    {
                        PublicFun.LogAdd("AlipayNotify:交易状态无法处理");
                    }

                    //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——
                    PublicFun.LogAdd("AlipayNotify:success");
                    return Content("success");
                    //Response.Write("success");  //请不要修改或删除

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else//验证失败
                {
                    PublicFun.LogAdd("AlipayNotify:verify fail");
                    return Content("fail");
                    //Response.Write("fail");
                }
            }
            PublicFun.LogAdd("AlipayNotify:无通知参数");
            return Content("无通知参数");
            //Response.Write("无通知参数");
        }

        private void ParamsLog(string path, string logContent)
        {
            PublicFun.LogAdd(path + ": " + logContent);
        }
    }
}