using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Xml;
using System.Collections.Specialized;

using LazyAtHome.Library.Pay.MobileEmbed.Alipay;
using LazyAtHome.Library.Pay.MobileEmbed.WXPayment;
using LazyAtHome.Web.WeiXin4.CouponService;
using LazyAtHome.Web.WeiXin4.App_Code;
using Senparc.Weixin.HttpUtility;

namespace LazyAtHome.Web.WeiXin4.Controllers
{
    public class PaymentController : BaseController
    {

        public ActionResult Show(string OrderNum)
        {
            var user = GetUserInfo();
            if (user == null || !CheckLoginCook())
            {
                HttpContext.Cache[AppConfig.Cache_CallbackUrl + _UserOpenID] = Request.Url.ToString();
                return RedirectToAction("MobileBindingShow", "Member");
            }

            UpdateUserInfo();

            ViewBag.OrderNum = OrderNum;
            ViewBag.UserMoney = user.Money;
            ViewBag.oneMoney = 0;
            ViewBag.coupon = 0;
            ViewBag.userID = user.UserId;
            ViewBag.MPNo = user.MpNo;
            ViewBag.couponSum = 0;
            ViewBag.CouponNo = 0;
            ViewBag.expDate = DateTime.Now.ToString("yyyy.MM.dd");
            bool yiyuan = true;

            string url = AppConfig.AppService + "/order/queryOrderDetail.do";

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("userId", user.UserId);
            dic.Add("orderNumber", OrderNum);
            var result = UtilityFunc.GetPostResponse<LazyAtHome.Web.WeiXin4.Models.EntityModel.resultEntity<LazyAtHome.Web.WeiXin4.Models.EntityModel.OrderDetailInfo>>(url, dic);

            App_Code.UtilityFunc.AddToFile("MyOrders" + "-", "test");

            LazyAtHome.Web.WeiXin4.Models.EntityModel.OrderDetailInfo detailInfo = new LazyAtHome.Web.WeiXin4.Models.EntityModel.OrderDetailInfo();
            detailInfo.washItemSubtVoList = new List<Models.EntityModel.WashItem>();

            if (result.succFlag && result.data != null)
            {
                ViewBag.TotalMoney = result.data.totalPrice;
                ViewBag.PayMoney = 0;
                ViewBag.CreateTime = result.data.orderSimpleVo.orderTime;

                if (result.data.washItemSubtVoList.Count() > 1 && result.data.washItemSubtVoList.Where(s => Convert.ToDouble(s.price) == 9.9).Count() == 0)
                {
                    ViewBag.coupon = 0;
                    yiyuan = false;
                }
            }

            CouponServiceImplService couponService = new CouponServiceImplService();

            var rtn = couponService.queryCoupon(user.MpNo);
            if (rtn.resultCode == 0)
            {
                var oneMoney = rtn.coupons.Where(s => s.type == 1 && s.status == 1);//一元洗券
                var coupon = rtn.coupons.Where(s => s.type == 2 && s.status == 1);//红包
                if (oneMoney.Count() > 0 && yiyuan)
                {
                    ViewBag.oneMoney = 1;
                    ViewBag.expDate = oneMoney.First().expDate.ToString("yyyy.MM.dd");
                    ViewBag.CouponNo = oneMoney.First().couponNo;
                }
                if (coupon.Count() > 0)
                {
                    ViewBag.coupon = 1;
                    ViewBag.couponSum = coupon.Sum(s => s.denomination);
                }
            }

            return View();
        }

        //public JsonResult jsAlipay(string OrderNum, int UseBalance = 0)
        //{
        //    int rtncode = 0;
        //    string rtnmsg = "";

        //    var OrderItem = App_Code.Proxy.OrderProxy.Select_OrderEntity(OrderNum, true, false, false, false, false, false).OBJ;

        //    decimal payMoney = OrderItem.TotalAmount - OrderItem.PayAmount;

        //    if (UseBalance == 1)
        //    {
        //        decimal tmpUseBalance = 0;
        //        UpdateUserInfo();
        //        var user = GetUserInfo();
        //        if (user.User.UserInfo.Money > payMoney)
        //            tmpUseBalance = payMoney;
        //        else
        //            tmpUseBalance = user.User.UserInfo.Money;

        //        var rtn = App_Code.Proxy.OrderProxy.Update_OrderPay(OrderNum, tmpUseBalance, WCF.OrderSystem.Contract.Enumerate.PayMoneyType.Balance, WCF.OrderSystem.Contract.Enumerate.PayMoneyChannel.Weixin, DateTime.Now, "");
        //        if (!rtn.Success)
        //        {
        //            if (rtn.Code != -70)
        //            {
        //                rtncode = rtn.Code;
        //                rtnmsg = "订单支付失败！";
        //                App_Code.UtilityFunc.Add("处理订单失败：" + OrderNum + " Error:" + rtn.Message);
        //                return Json(new { Code = rtncode, Msg = rtnmsg });
        //            }
        //        }
        //        else
        //            OrderItem = App_Code.Proxy.OrderProxy.Select_OrderEntity(OrderNum, true, false, false, false, false, false).OBJ;
        //    }

        //    payMoney = OrderItem.TotalAmount - OrderItem.PayAmount;
        //    UpdateUserInfo();
        //    if (payMoney == 0)
        //    {
        //        rtncode = 1;
        //        rtnmsg = "/Cart/OrderDetail?orderNum=" + OrderNum;
        //        return Json(new { Code = rtncode, Msg = rtnmsg });
        //    }

        //    if (OrderItem.PayStatus == 0 && payMoney > 0)
        //    {
        //        string body = "";
        //        var pList = OrderItem.ProductList.GroupBy(p => p.ProductID);
        //        foreach (var product in pList)
        //        {
        //            var pDC = product.First();
        //            body += pDC.Name + " 数量：" + product.Count() + " 价格：" + pDC.Price * product.Count() + " ";
        //        }

        //        //返回格式
        //        string format = "xml";
        //        //版本
        //        string v = "2.0";
        //        //请求号 必填，须保证每次请求都是唯一
        //        string req_id = DateTime.Now.ToString("yyyyMMddHHmmssfff");

        //        //请求业务参数详细
        //        StringBuilder req_dataToken = new StringBuilder("<direct_trade_create_req>");
        //        req_dataToken.Append("<notify_url>" + AppConfig.Alipay_Notify_URL + "</notify_url>");
        //        req_dataToken.Append("<call_back_url>" + AppConfig.Alipay_Return_URL + "</call_back_url>");
        //        req_dataToken.Append("<seller_account_name>" + AppConfig.Alipay_Seller_Email + "</seller_account_name>");
        //        req_dataToken.Append("<out_trade_no>" + OrderItem.OrderNumber + "</out_trade_no>");
        //        req_dataToken.Append("<subject>" + OrderItem.Title + "</subject>");
        //        req_dataToken.Append("<total_fee>" + payMoney + "</total_fee>");
        //        req_dataToken.Append("</direct_trade_create_req>");

        //        //把请求参数打包成数组
        //        Dictionary<string, string> sParaTempToken = new Dictionary<string, string>();
        //        sParaTempToken.Add("partner", Config.Partner);
        //        sParaTempToken.Add("_input_charset", Config.Input_charset.ToLower());
        //        sParaTempToken.Add("sec_id", Config.Sign_type.ToUpper());
        //        sParaTempToken.Add("service", "alipay.wap.trade.create.direct");
        //        sParaTempToken.Add("format", format);
        //        sParaTempToken.Add("v", v);
        //        sParaTempToken.Add("req_id", req_id);
        //        sParaTempToken.Add("req_data", req_dataToken.ToString());

        //        //建立请求
        //        string sHtmlTextToken = Submit.BuildRequest(Config.GateWay, sParaTempToken);
        //        //URLDECODE返回的信息
        //        Encoding code = Encoding.GetEncoding(Config.Input_charset);
        //        sHtmlTextToken = HttpUtility.UrlDecode(sHtmlTextToken, code);
        //        //解析远程模拟提交后返回的信息
        //        Dictionary<string, string> dicHtmlTextToken = Submit.ParseResponse(sHtmlTextToken);
        //        //获取token
        //        string request_token = dicHtmlTextToken["request_token"];

        //        ////////////////////////////////////////////根据授权码token调用交易接口alipay.wap.auth.authAndExecute////////////////////////////////////////////
        //        //业务详细
        //        string req_data = "<auth_and_execute_req><request_token>" + request_token + "</request_token></auth_and_execute_req>";

        //        //把请求参数打包成数组
        //        Dictionary<string, string> sParaTemp = new Dictionary<string, string>();
        //        sParaTemp.Add("partner", Config.Partner);
        //        sParaTemp.Add("_input_charset", Config.Input_charset.ToLower());
        //        sParaTemp.Add("sec_id", Config.Sign_type.ToUpper());
        //        sParaTemp.Add("service", "alipay.wap.auth.authAndExecute");
        //        sParaTemp.Add("format", format);
        //        sParaTemp.Add("v", v);
        //        sParaTemp.Add("req_data", req_data);

        //        //建立请求url
        //        string param = Submit.BuildRequestParaToString(sParaTemp, Encoding.UTF8);
        //        string alipayUrl = Config.GateWay + param;
        //        rtnmsg = alipayUrl;
        //    }
        //    else
        //    {
        //        rtnmsg = "/Cart/OrderDetail?orderNum=" + OrderNum;
        //    }

        //    rtncode = 1;
        //    return Json(new { Code = rtncode, Msg = rtnmsg });
        //}

        public JsonResult jsWXPayment(string MPNo, string UserID, string OrderNum, string CouponType, string PayMoney, string CouponMoney, string ShowMoney, string CouponNo)
        {
            App_Code.UtilityFunc.AddToFile(MPNo + "-" + UserID + "-" + OrderNum + "-" + CouponType + "-" + PayMoney + "-" + CouponMoney + "-" +
             ShowMoney + "-" + CouponNo, "test");
            int rtncode = 1;
            string rtnmsg = "";

            string timeStamp = "";
            string nonceStr = "";
            string paySign = "";
            string prepay_id = "";

            string url = AppConfig.AppService + "/order/queryOrderDetail.do";

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("userId", UserID);
            dic.Add("orderNumber", OrderNum);
            var result = UtilityFunc.GetPostResponse<LazyAtHome.Web.WeiXin4.Models.EntityModel.resultEntity<LazyAtHome.Web.WeiXin4.Models.EntityModel.OrderDetailInfo>>(url, dic);

            LazyAtHome.Web.WeiXin4.Models.EntityModel.OrderDetailInfo detailInfo = new LazyAtHome.Web.WeiXin4.Models.EntityModel.OrderDetailInfo();
            detailInfo.washItemSubtVoList = new List<Models.EntityModel.WashItem>();

            if (result.succFlag && result.data != null)
            {
                var OrderItem = result.data;

                if (OrderItem.orderSimpleVo.payStatus == "0")
                {
                    if (CouponMoney != "0")//有优惠金额
                    {
                        string url2 = AppConfig.AppService + "/coupon/lockCoupon.do";

                        Dictionary<string, string> dic2 = new Dictionary<string, string>();
                        dic2.Add("uname", MPNo);
                        dic2.Add("lockRef", OrderNum);
                        dic2.Add("type", CouponMoney == "1" ? "1" : "2");
                        if (CouponMoney == "1")
                        {
                            dic2.Add("couponNo", CouponNo.Trim());
                            dic2.Add("amount", "");
                        }
                        else
                        {
                            dic2.Add("couponNo", "");
                            dic2.Add("amount", CouponMoney);
                        }
                        var result2 = UtilityFunc.GetPostResponse<LazyAtHome.Web.WeiXin4.Models.EntityModel.resultEntity<LazyAtHome.Web.WeiXin4.Models.EntityModel.DataEntity>>(url2, dic2);

                        App_Code.UtilityFunc.AddToFile(url2 + "-" + CouponMoney, "test");
                        if (result2.succFlag == true && result2.data.resultCode == 0)//红包锁定成功
                        {
                            rtncode = 1;
                        }
                        else
                        {
                            rtncode = 0;
                            rtnmsg = result2.msg;
                        }
                    }
                    if (rtncode == 1)
                    {
                        WXPay pay = new WXPay();
                        pay.SetParameter("appid", AppConfig.wxPay_AppID);//公众账号ID
                        pay.SetParameter("mch_id", AppConfig.wxPay_Mch_ID);//商户号
                        pay.SetParameter("nonce_str", TenpayUtil.getNoncestr());//随机字符串            
                        pay.SetParameter("body", OrderItem.orderSimpleVo.orderNumber);//商品描述
                        pay.SetParameter("out_trade_no", OrderItem.orderSimpleVo.orderNumber);//商户订单号
                        pay.SetParameter("total_fee", Convert.ToInt32(Convert.ToDecimal(ShowMoney) * 100).ToString());//总金额 单位为分，不能带小数点
                        pay.SetParameter("spbill_create_ip", Request.UserHostAddress);//终端IP
                        pay.SetParameter("notify_url", AppConfig.wxPay_Notify_URL);//通知地址
                        pay.SetParameter("trade_type", "JSAPI");//交易类
                        pay.SetParameter("openid", _UserOpenID);//用户标识
                        string sign = pay.CreateSign();
                        pay.SetParameter("sign", sign);

                        string msg = null;
                        string postXml = pay.ConvertToXml();
                        msg = postXml + Environment.NewLine;
                        string rtn = pay.BuildRequest(postXml);
                        msg += rtn + Environment.NewLine;
                        pay.ParseXML(rtn);
                        if (pay.Parameters["return_code"] == "SUCCESS" && pay.Parameters["result_code"] == "SUCCESS")
                        {
                            timeStamp = TenpayUtil.getTimestamp();
                            nonceStr = TenpayUtil.getNoncestr();
                            prepay_id = "prepay_id=" + pay.Parameters["prepay_id"];
                            WXPay jsAPIPay = new WXPay();
                            jsAPIPay.SetParameter("appId", AppConfig.wxPay_AppID);//公众账号ID
                            jsAPIPay.SetParameter("timeStamp", timeStamp);//
                            jsAPIPay.SetParameter("nonceStr", nonceStr);//
                            jsAPIPay.SetParameter("package", prepay_id);//
                            jsAPIPay.SetParameter("signType", "MD5");//
                            paySign = jsAPIPay.CreateSign();
                            msg += "---------------------------------------------------------------------------------------";
                        }
                        else
                        {
                            rtncode = -1;
                            rtnmsg = "支付失败！";
                            msg += "====================================ERROR==============================================";
                        }

                        App_Code.UtilityFunc.AddToFile(msg, "wxPay");
                    }
                }
            }
            return Json(new { Code = rtncode, Msg = rtnmsg, appid = AppConfig.wxPay_AppID, prepay_id = prepay_id, timeStamp = timeStamp, nonceStr = nonceStr, paySign = paySign });
        }

        public JsonResult jsCashPay(string MPNo, string UserID, string OrderNum, string CouponType, string PayMoney, string CouponMoney, string ShowMoney, string CouponNo)
        {
            App_Code.UtilityFunc.AddToFile(MPNo + "-" + UserID + "-" + OrderNum + "-" + CouponType + "-" + PayMoney + "-" + CouponMoney + "-" +
            ShowMoney + "-" + CouponNo, "test");
            int rtncode = 0;
            string rtnmsg = "";

            string payType;
            string payAmount;
            string couponNo;

            if (CouponType == "0")//余额
            {
                payType = "1";
                payAmount = ShowMoney;
                couponNo = "";
            }
            else if (CouponType == "1")//一元洗
            {
                payType = "9,1";
                payAmount = "0," + ShowMoney;
                couponNo = CouponNo;
            }
            else//红包
            {
                payType = "9,1";
                payAmount = CouponMoney + "," + ShowMoney;
                couponNo = "";
            }
            string url = AppConfig.AppService + "/pay/payConfirm.do";

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("userId", UserID.Trim().ToUpper());
            dic.Add("orderNumber", OrderNum);
            dic.Add("payType", payType);
            dic.Add("payAmount", payAmount);
            dic.Add("payStatus", "1");
            dic.Add("orderPayAmount", PayMoney);
            dic.Add("couponNo", couponNo.Trim());
            var result = UtilityFunc.GetPostResponse<LazyAtHome.Web.WeiXin4.Models.EntityModel.resultEntity<LazyAtHome.Web.WeiXin4.Models.EntityModel.DataEntity>>(url, dic);

            if (result.succFlag == true && result.data.resultCode == 0)
            {
                rtncode = 1;
                rtnmsg = "支付成功!";
            }
            else
            {
                rtnmsg = result.msg;
            }
            return Json(new { Code = rtncode, Msg = rtnmsg });
        }


        public JsonResult PayResult(string UserID, string OrderNum, string CouponType, string PayMoney, string CouponMoney, string ShowMoney, string CouponNo)
        {
            int rtncode = 0;
            string rtnmsg = "";

            string payType;
            string payAmount;
            string couponNo;

            if (CouponType == "0")//余额
            {
                payType = "10";
                payAmount = ShowMoney;
                couponNo = "";
            }
            else if (CouponType == "1")//一元洗
            {
                payType = "9,10";
                payAmount = "0," + ShowMoney;
                couponNo = CouponNo;
            }
            else//红包
            {
                payType = "9,10";
                payAmount = CouponMoney + "," + ShowMoney;
                couponNo = "";
            }

            string url = AppConfig.AppService + "/pay/payConfirm.do";

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("userId", UserID.Trim().ToUpper());
            dic.Add("orderNumber", OrderNum);
            dic.Add("payType", payType);
            dic.Add("payAmount", payAmount);
            dic.Add("payStatus", "1");
            dic.Add("orderPayAmount", PayMoney);
            dic.Add("couponNo", couponNo.Trim());

            var result = UtilityFunc.GetPostResponse<LazyAtHome.Web.WeiXin4.Models.EntityModel.resultEntity<LazyAtHome.Web.WeiXin4.Models.EntityModel.DataEntity>>(url, dic);

            if (result.succFlag == true && result.data.resultCode == 0)
            {
                rtncode = 1;
                rtnmsg = "支付成功!";
            }
            else
            {
                rtnmsg = result.msg;
            }
            return Json(new { Code = rtncode, Msg = rtnmsg });
        }

        public JsonResult unLockCoupon(string MPNo, string OrderNum)
        {
            int rtncode = 0;
            string rtnmsg = "";

            var user = GetUserInfo();
            string url = AppConfig.AppService + "/coupon/unLockCoupon.do";

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("uname", MPNo);
            dic.Add("lockRef", OrderNum);

            var result = UtilityFunc.GetPostResponse<LazyAtHome.Web.WeiXin4.Models.EntityModel.resultEntity<LazyAtHome.Web.WeiXin4.Models.EntityModel.DataEntity>>(url, dic);

            App_Code.UtilityFunc.AddToFile(MPNo + "-" + OrderNum, "test");
            if (result.succFlag == true && result.data.resultCode == 0)
            {
                rtncode = 1;
            }
            else
            {
                rtnmsg = result.msg;
            }
            return Json(new { Code = rtncode, Msg = rtnmsg });
        }

        public ActionResult PaymentOk(string OrderNum)
        {
            ViewBag.OrderNum = OrderNum;
            return View();
        }


        public ActionResult AlipayReturn()
        {
            App_Code.UtilityFunc.Add("------------------------------------\r\nCall AlipayReturn");

            Dictionary<string, string> sPara = GetRequest("get");

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                Notify aliNotify = new Notify();
                bool verifyResult = aliNotify.VerifyReturn(sPara, Request.QueryString["sign"]);
                App_Code.UtilityFunc.Add("verifyResult:" + verifyResult);

                if (verifyResult)//验证成功
                {
                    //商户订单号
                    string out_trade_no = Request.QueryString["out_trade_no"];
                    //支付宝交易号
                    string trade_no = Request.QueryString["trade_no"];
                    //交易状态
                    string result = Request.QueryString["result"];

                    decimal payMoney = 0;
                    var rtnOrder = App_Code.Proxy.OrderProxy.Select_OrderEntity(out_trade_no, false, false, false, false, false, false);
                    if (rtnOrder.Success)
                        payMoney = rtnOrder.OBJ.TotalAmount - rtnOrder.OBJ.PayAmount;

                    if (payMoney > 0)
                    {
                        //支付时间
                        DateTime alipayPayTime = DateTime.Now;

                        var rtn = App_Code.Proxy.OrderProxy.Update_OrderPay(out_trade_no, payMoney, WCF.OrderSystem.Contract.Enumerate.PayMoneyType.AliPay, WCF.OrderSystem.Contract.Enumerate.PayMoneyChannel.Weixin, alipayPayTime, trade_no);
                        if (!rtn.Success)
                        {
                            if (rtn.Code != -70)
                                App_Code.UtilityFunc.Add("处理订单失败：" + out_trade_no + " Error:" + rtn.Message);
                        }
                    }
                    else
                        App_Code.UtilityFunc.Add("处理订单失败：" + out_trade_no + " Error:payMoney is 0");

                    //ViewBag.OrderNum = out_trade_no;
                    //return View("PaymentOk");
                    return RedirectToAction("OrderDetail", "Cart", new { orderNum = out_trade_no });
                }
                else//验证失败
                {
                    return Content("验证失败");
                }
            }
            else
            {
                return Content("无返回参数");
            }
        }


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
            App_Code.UtilityFunc.AddToFile("------------------------------------\r\nCall  ", "wxPay");

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

                var rtn = App_Code.Proxy.OrderProxy.Update_PayRecord(out_trade_no);
                if (!rtn.Success)
                {
                    App_Code.UtilityFunc.AddToFile("处理订单失败：" + out_trade_no + " Error:" + rtn.Message, "wxPay");
                    return Content("<xml><return_code><![CDATA[FAIL]]></return_code><return_msg><![CDATA[处理订单失败]]></return_msg></xml>");
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