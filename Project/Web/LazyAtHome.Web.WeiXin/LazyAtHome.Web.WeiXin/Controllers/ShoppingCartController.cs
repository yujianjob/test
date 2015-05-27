using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Collections.Specialized;
using System.Xml;

using LazyAtHome.Library.Pay.Wap.Alipay;

namespace LazyAtHome.Web.WeiXin.Controllers
{
    public class ShoppingCartController : BaseController
    {
        [NoCacheFilter]
        public ActionResult Index(int? spid, int? pidcount, int? submitType)
        {
            var userCart = GetUserInfo();

            if (spid.HasValue)
            {
                if (!pidcount.HasValue)
                    pidcount = 1;

                var product = userCart.ProductList.FirstOrDefault(p => p.ProductInfo.ID == spid.Value);
                if (product == null)
                {
                    var pItem = WCF.Wash.Contract.ClientProxy.wx_ProductClient.Cache_wx_Wash_Product_SELECT_Entity(spid.Value);
                    if (pItem.Success)
                    {
                        userCart.ProductList.Add(new Models.ShopCart.ProductItem(pItem.OBJ, pidcount.Value));
                        userCart.TotalPrice += (pItem.OBJ.SalesPrice * pidcount.Value);
                    }
                }
                else
                {
                    product.Count += pidcount.Value;
                    userCart.TotalPrice += (product.ProductInfo.SalesPrice * pidcount.Value);
                }                
            }

            if (submitType == 2)
                return RedirectToAction("OrderConfirm");

            return View(userCart);
        }

        public ActionResult ProductDel(int pid)
        {
            var user = GetUserInfo();
            var product = user.ProductList.First(item => item.ProductInfo.ID == pid);
            if (product != null)
            {
                user.TotalPrice -= product.ProductInfo.SalesPrice * product.Count;
                user.ProductList.Remove(product);
            }

            return View("Index", user);
        }

        [NoCacheFilter]
        public ActionResult OrderConfirm()
        {
            var user = GetUserInfo();
            if (user.TotalPrice == 0)
            {
                return RedirectToAction("Product", "Wash");
            }

            if (user.User == null)
            {
                HttpContext.Cache[CustomerConfig.Cache_CallbackUrl + _UserOpenID] = Request.Url.ToString();
                return RedirectToAction("MobileBindShow", "PersonalCenter");
            }

            var rtnCouponList = App_Code.Proxy.UserProxy.User_Coupon_SELECT_List(user.User.UserInfo.ID, null, null, WCF.UserSystem.Contract.Enumerate.CouponStatus.Normal, null, null, 1, 20);
            if (rtnCouponList.Success)
                ViewBag.CouponList = rtnCouponList.OBJ.ReturnList;

            return View(user);
        }


        public ActionResult OrderSubmit(decimal UseBalance, int Lazycard, decimal UseCardVal, int lazycoupon, decimal useCouponVal)
        {
            var user = GetUserInfo();

            if (user.GetAddress == null)
            {
                ViewBag.Msg = "收件地址不能为空";
                return View("OrderConfirm", user);
            }

            if (user.SendAddress == null)
            {
                ViewBag.Msg = "送件地址不能为空";
                return View("OrderConfirm", user);
            }

            WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC GetAddress = new WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC();
            GetAddress.Consignee = user.GetAddress.Consignee;
            GetAddress.Mpno = user.GetAddress.MPNo;
            GetAddress.Address = user.GetAddress.DistrictName + user.GetAddress.Address;
            GetAddress.CityName = user.GetAddress.CityName;
            GetAddress.ProvinceName = user.GetAddress.ProvinceName;
            GetAddress.DistrictID = user.GetAddress.DistrictID;

            WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC SendAddress = new WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC();
            SendAddress.Consignee = user.SendAddress.Consignee;
            SendAddress.Mpno = user.SendAddress.MPNo;
            SendAddress.Address = user.SendAddress.DistrictName + user.SendAddress.Address;
            SendAddress.CityName = user.SendAddress.CityName;
            SendAddress.ProvinceName = user.SendAddress.ProvinceName;
            SendAddress.DistrictID = user.SendAddress.DistrictID;

            List<int> productList = new List<int>();
            foreach (var p in user.ProductList)
            {
                for (int i = 0; i < p.Count; i++)
                    productList.Add(p.ProductInfo.ID);
            }

            decimal realPrice = user.TotalPrice;

            //计算优惠券
            IList<int> couponList = null;
            if (lazycoupon != 0 && useCouponVal != 0)
            {
                couponList = new List<int>();
                couponList.Add(lazycoupon);
                realPrice -= useCouponVal;
                if (realPrice < 0)
                    realPrice = 0;
            }

            IDictionary<string, decimal> activeList = null;
            //计算创始会员
            if (user.User.UserInfo.Level == 100)
            {
                activeList = new Dictionary<string, decimal>();
                decimal CharterSale = Math.Round(realPrice * 0.2m, 1, MidpointRounding.AwayFromZero);
                activeList.Add("创始会员8折优惠", CharterSale);
                realPrice -= CharterSale;
            }

            //计算懒人卡
            Dictionary<int, decimal> lazyCardList = null;
            if (Lazycard != 0 && UseCardVal != 0)
            {
                if (realPrice > 0)
                {
                    lazyCardList = new Dictionary<int, decimal>();
                    lazyCardList.Add(Lazycard, UseCardVal);
                    realPrice -= UseCardVal;
                }
            }

            //计算余额
            if (UseBalance > 0)
                realPrice -= UseBalance;

            var Orderrtn = App_Code.Proxy.OrderProxy.Order_Weixin_Create(_UserOpenID, 1, productList, 8m, 8m, UseBalance, lazyCardList, GetAddress, SendAddress, realPrice, activeList, couponList);
            if (!Orderrtn.Success)
            {
                ViewBag.Msg = Orderrtn.Message;
                return View("OrderConfirm", user);
            }

            user.ProductList.Clear();
            user.TotalPrice = 0;
            UpdateUserInfo(false);
            if ((Orderrtn.OBJ.TotalAmount - Orderrtn.OBJ.PayAmount) > 0)
                return RedirectToAction("Alipay", new { OrderNum = Orderrtn.OBJ.OrderNumber });
            else
                return RedirectToAction("OrderSubmitFinish", new { orderNum = Orderrtn.OBJ.OrderNumber });
        }

        public ActionResult OrderSubmitFinish(string orderNum, string msg)
        {
            var user = GetUserInfo();
            user.ReceviceTime = string.Empty;
            user.ReceviceDay = string.Empty;
            ViewBag.OrderNum = orderNum;
            ViewBag.Msg = msg;
            return View();
        }

        public ActionResult Alipay(string OrderNum)
        {
            var OrderItem = App_Code.Proxy.OrderProxy.Select_UserOrderDetail(OrderNum, false, false, false, false, false, false).OBJ;
            decimal payMoney = OrderItem.TotalAmount - OrderItem.PayAmount;
            if (OrderItem.PayStatus == 0 && payMoney > 0)
            {
                ////////////////////////////////////////////调用授权接口alipay.wap.trade.create.direct获取授权码token////////////////////////////////////////////

                //返回格式
                string format = "xml";
                //版本
                string v = "2.0";
                //请求号 必填，须保证每次请求都是唯一
                string req_id = DateTime.Now.ToString("yyyyMMddHHmmssfff");

                //请求业务参数详细
                StringBuilder req_dataToken = new StringBuilder("<direct_trade_create_req>");
                req_dataToken.Append("<notify_url>" + CustomerConfig.Alipay_Notify_URL + "</notify_url>");
                req_dataToken.Append("<call_back_url>" + CustomerConfig.Alipay_Return_URL + "</call_back_url>");
                req_dataToken.Append("<seller_account_name>" + CustomerConfig.Alipay_Seller_Email + "</seller_account_name>");
                req_dataToken.Append("<out_trade_no>" + OrderItem.OrderNumber + "</out_trade_no>");
                req_dataToken.Append("<subject>" + OrderItem.Title + "</subject>");
                req_dataToken.Append("<total_fee>" + payMoney + "</total_fee>");
                req_dataToken.Append("</direct_trade_create_req>");

                Config.Partner = CustomerConfig.Alipay_Partner;
                Config.Key = CustomerConfig.Alipay_Key;

                //把请求参数打包成数组
                Dictionary<string, string> sParaTempToken = new Dictionary<string, string>();
                sParaTempToken.Add("partner", Config.Partner);
                sParaTempToken.Add("_input_charset", Config.Input_charset.ToLower());
                sParaTempToken.Add("sec_id", Config.Sign_type.ToUpper());
                sParaTempToken.Add("service", "alipay.wap.trade.create.direct");
                sParaTempToken.Add("format", format);
                sParaTempToken.Add("v", v);
                sParaTempToken.Add("req_id", req_id);
                sParaTempToken.Add("req_data", req_dataToken.ToString());

                //建立请求
                string sHtmlTextToken = Submit.BuildRequest(CustomerConfig.Alipay_GATEWAY_NEW, sParaTempToken);
                //URLDECODE返回的信息
                Encoding code = Encoding.GetEncoding(Config.Input_charset);
                sHtmlTextToken = HttpUtility.UrlDecode(sHtmlTextToken, code);
                //解析远程模拟提交后返回的信息
                Dictionary<string, string> dicHtmlTextToken = Submit.ParseResponse(sHtmlTextToken);
                //获取token
                string request_token = dicHtmlTextToken["request_token"];

                ////////////////////////////////////////////根据授权码token调用交易接口alipay.wap.auth.authAndExecute////////////////////////////////////////////


                //业务详细
                string req_data = "<auth_and_execute_req><request_token>" + request_token + "</request_token></auth_and_execute_req>";
                //必填

                //把请求参数打包成数组
                Dictionary<string, string> sParaTemp = new Dictionary<string, string>();
                sParaTemp.Add("partner", Config.Partner);
                sParaTemp.Add("_input_charset", Config.Input_charset.ToLower());
                sParaTemp.Add("sec_id", Config.Sign_type.ToUpper());
                sParaTemp.Add("service", "alipay.wap.auth.authAndExecute");
                sParaTemp.Add("format", format);
                sParaTemp.Add("v", v);
                sParaTemp.Add("req_data", req_data);


                //建立请求url
                string param = Submit.BuildRequestParaToString(sParaTemp, Encoding.UTF8);
                string alipayUrl = CustomerConfig.Alipay_GATEWAY_NEW + param;
                return Redirect(alipayUrl);
            }
            else
                return RedirectToAction("OrderSubmitFinish", new { orderNum = OrderNum });
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

            UtilityFunc.Add("支付宝返回参数：" + sb.ToString());

            return sArray;
        }

        public ActionResult AlipayReturn()
        {
            UtilityFunc.Add("------------------------------------\r\nCall AlipayReturn");

            Dictionary<string, string> sPara = GetRequest("get");

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                Notify aliNotify = new Notify();
                bool verifyResult = aliNotify.VerifyReturn(sPara, Request.QueryString["sign"]);
                UtilityFunc.Add("verifyResult:" + verifyResult);

                if (verifyResult)//验证成功
                {
                    //商户订单号
                    string out_trade_no = Request.QueryString["out_trade_no"];
                    //支付宝交易号
                    string trade_no = Request.QueryString["trade_no"];
                    //交易状态
                    string result = Request.QueryString["result"];

                    decimal payMoney = 0;
                    var rtnOrder = App_Code.Proxy.OrderProxy.Select_UserOrderDetail(out_trade_no, false, false, false, false, false, false);
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
                                UtilityFunc.Add("处理订单失败：" + out_trade_no + " Error:" + rtn.Message);
                        }
                    }
                    else
                        UtilityFunc.Add("处理订单失败：" + out_trade_no + " Error:payMoney is 0");

                    //打印页面
                    //Response.Write("验证成功<br />");
                    return RedirectToAction("OrderSubmitFinish", new { orderNum = out_trade_no });
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
            UtilityFunc.Add("------------------------------------\r\nCall AlipayNotify");
            Dictionary<string, string> sPara = GetRequest("post");

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                Notify aliNotify = new Notify();
                bool verifyResult = aliNotify.VerifyNotify(sPara, Request.Form["sign"]);
                UtilityFunc.Add("verifyResult:" + verifyResult);

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
                        UtilityFunc.Add("trade_status:" + trade_status);

                        decimal alipayMoney = decimal.Parse(xmlDoc.SelectSingleNode("/notify/total_fee").InnerText);
                        DateTime alipayPayTime = DateTime.Parse(xmlDoc.SelectSingleNode("/notify/gmt_payment").InnerText);

                        if (trade_status == "TRADE_FINISHED" || trade_status == "TRADE_SUCCESS")
                        {
                            var rtn = App_Code.Proxy.OrderProxy.Update_OrderPay(out_trade_no, alipayMoney, WCF.OrderSystem.Contract.Enumerate.PayMoneyType.AliPay, WCF.OrderSystem.Contract.Enumerate.PayMoneyChannel.Weixin, alipayPayTime, trade_no);
                            if (!rtn.Success)
                            {
                                if (rtn.Code != -70)
                                    UtilityFunc.Add("处理订单失败：" + out_trade_no + " Error:" + rtn.Message);
                            }
                            return Content("success");
                        }
                        else
                        {
                            UtilityFunc.Add(trade_status);
                            return Content(trade_status);
                        }

                    }
                    catch (Exception exc)
                    {
                        UtilityFunc.Add(exc.ToString());
                        return Content(exc.ToString());
                    }
                }
                else//验证失败
                {
                    UtilityFunc.Add("fail");
                    return Content("fail");
                }
            }
            else
            {
                UtilityFunc.Add("无通知参数");
                return Content("无通知参数");
            }
        }
    }
}