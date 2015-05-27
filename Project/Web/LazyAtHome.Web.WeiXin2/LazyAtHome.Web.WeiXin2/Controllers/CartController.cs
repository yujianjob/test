using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LazyAtHome.Library.Pay.MobileEmbed.Alipay;
using System.Collections.Specialized;
using System.Xml;
using LazyAtHome.Library.Pay.MobileEmbed.WXPayment;

namespace LazyAtHome.Web.WeiXin2.Controllers
{
    public class CartController : BaseController
    {
        //
        // GET: /Cart/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OneKey()
        {
            var user = GetUserInfo();
            if (user.User == null)
            {
                HttpContext.Cache[AppConfig.Cache_CallbackUrl + _UserOpenID] = Request.Url.ToString();
                return RedirectToAction("Reg", "Member");
            }

            var rtn = App_Code.Proxy.UserProxycs.Select_UserAddress(user.User.UserInfo.ID);
            if (rtn.Success)
            {
                ViewBag.AddressList = rtn.OBJ;
            }
            return View();
        }

        public JsonResult OneKeySubmit(int getAddressID, int sendAddreddID, DateTime? exceptTime, string remark)
        {
            int code = 0;
            string msg = "";

            var user = GetUserInfo();

            WCF.UserSystem.Contract.DataContract.User_ConsigneeAddressDC GetAddress = null;
            WCF.UserSystem.Contract.DataContract.User_ConsigneeAddressDC SendAddress = null;

            var rtn = App_Code.Proxy.UserProxycs.User_ConsigneeAddress_SELECT_Entity(getAddressID);
            if (rtn.Success)
                GetAddress = rtn.OBJ;

            if (sendAddreddID == getAddressID)
                SendAddress = GetAddress;
            else
            {
                rtn = App_Code.Proxy.UserProxycs.User_ConsigneeAddress_SELECT_Entity(sendAddreddID);
                if (rtn.Success)
                    SendAddress = rtn.OBJ;
            }

            if (GetAddress == null)
            {
                code = -1;
                msg = "请添加取件地址";
            }
            else if (SendAddress == null)
            {
                code = -1;
                msg = "请添加送件地址";
            }

            if (ViewBag.Msg != null)
                return Json(new { Code = code, Msg = msg });

            WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC GA = new WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC();
            GA.Consignee = GetAddress.Consignee;
            GA.Mpno = GetAddress.MPNo;
            GA.Address = GetAddress.DistrictName + GetAddress.Address;
            GA.CityName = GetAddress.CityName;
            GA.ProvinceName = GetAddress.ProvinceName;
            GA.DistrictID = GetAddress.DistrictID;

            WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC SA = new WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC();
            SA.Consignee = SendAddress.Consignee;
            SA.Mpno = SendAddress.MPNo;
            SA.Address = SendAddress.DistrictName + SendAddress.Address;
            SA.CityName = SendAddress.CityName;
            SA.ProvinceName = SendAddress.ProvinceName;
            SA.DistrictID = SendAddress.DistrictID;

            var orderRTN = App_Code.Proxy.OrderProxy.Order_Onekey_Create(user.User.UserInfo.ID, 1, WCF.OrderSystem.Contract.Enumerate.Channel.Web, GA, SA, exceptTime);
            if (!orderRTN.Success)
            {
                code = orderRTN.Code;
                msg = orderRTN.Message;
            }

            code = 1;
            msg = "一键下单完成";

            return Json(new { Code = code, Msg = msg });
            //return RedirectToAction("PaymentOk", "Cart", new { orderNum = orderRTN.OBJ.OrderNumber });
        }

        public void CartProductAdd(int spid, int pcount)
        {
            var user = GetUserInfo();
            var rtn = WCF.Wash.Contract.ClientProxy.web_ProductClient.Cache_web_Wash_Product_SELECT_Entity(spid);

            var pItem = user.Cart.ProductList.FirstOrDefault(p => p.ProductInfo.ID == spid);
            if (pItem == null)
            {
                pItem = new Models.CartProductItemModel();
                pItem.ProductInfo = rtn.OBJ;
                pItem.Count = pcount;
                user.Cart.ProductList.Add(pItem);
            }
            else
                pItem.Count += pcount;

            user.Cart.TotalPrice += pItem.ProductInfo.SalesPrice * pcount;
            user.Cart.TotalCount += pcount;

            ClearPayInfo(user.Cart);
        }

        public JsonResult jsAddToCart(int pid, bool opearFlag)
        {
            int code = 0;
            string msg = "";
            var user = GetUserInfo();
            if (pid != 0)
            {
                var pItem = user.Cart.ProductList.FirstOrDefault(p => p.ProductInfo.ID == pid);
                if (pItem == null)
                {
                    var rtn = WCF.Wash.Contract.ClientProxy.web_ProductClient.Cache_web_Wash_Product_SELECT_Entity(pid);
                    pItem = new Models.CartProductItemModel();
                    pItem.ProductInfo = rtn.OBJ;
                    pItem.Count = 1;
                    user.Cart.ProductList.Add(pItem);
                }
                else
                {
                    if (opearFlag)
                        pItem.Count++;
                    else
                        pItem.Count--;

                    if (pItem.Count < 1)
                        user.Cart.ProductList.Remove(pItem);
                    if (pItem.Count > 20)
                        pItem.Count = 20;
                }
                //ClearPayInfo(user.Cart);
                user.Cart.TotalProductInfo();
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (var item in user.Cart.ProductList)
            {
                sb.Append("<div class=\"unit\"><div class=\"lfbt\">" + item.ProductInfo.Name + "</div><div class=\"setnum\"><button onclick=\"AddToCart(" + item.ProductInfo.ID + ", true)\">+</button><input type=\"text\" readonly=\"readonly\" value=\"" + item.Count + "\"><button onclick=\"AddToCart(" + item.ProductInfo.ID + ", false)\">-</button></div></div>");
            }
            code = 1;

            return Json(new { Code = code, Msg = msg, Content = sb.ToString(), TotalPrice = user.Cart.PayPrice, TotalCount = user.Cart.TotalCount });
        }

        public JsonResult jsCartClear()
        {
            int code = 0;
            string msg = "";
            var user = GetUserInfo();
            user.Cart.Clear();
            code = 1;
            return Json(new { Code = code, Msg = msg });
        }

        public JsonResult jsCartDelete(int id)
        {
            int code = 1;
            string rtnMsg = null;

            var user = GetUserInfo();
            var item = user.Cart.ProductList.FirstOrDefault(p => p.ProductInfo.ID == id);
            if (item != null)
            {
                user.Cart.ProductList.Remove(item);
                user.Cart.TotalCount -= item.Count;
                user.Cart.TotalPrice -= item.ProductInfo.SalesPrice * item.Count;
            }
            ClearPayInfo(user.Cart);
            return Json(new { Code = code, Msg = rtnMsg, Price = user.Cart.TotalPrice, Count = user.Cart.TotalCount });
        }

        /// <summary>
        /// 清除抵扣记录
        /// </summary>
        public void ClearPayInfo(Models.CartModel Cart)
        {
            if (Cart != null)
            {
                Cart.UseBalance = 0;
                Cart.UseCard.Clear();
                Cart.UseCoupon.Clear();
            }
        }

        public ActionResult OrderConfirm()
        {
            var user = GetUserInfo();

            if (user.User == null)
            {
                HttpContext.Cache[AppConfig.Cache_CallbackUrl + _UserOpenID] = Request.Url.ToString();
                return RedirectToAction("Reg", "Member");
            }

            var rtn = App_Code.Proxy.UserProxycs.Select_UserAddress(user.User.UserInfo.ID);
            if (rtn.Success)
            {
                ViewBag.AddressList = rtn.OBJ;
            }

            user.Cart.Clear(false);

            var rtnCardList = App_Code.Proxy.UserProxycs.Select_UserCardList(user.User.UserInfo.ID);
            if (rtnCardList.Success)
            {
                ViewBag.CardList = rtnCardList.OBJ;
            }

            var rtnCouponList = App_Code.Proxy.UserProxycs.User_Coupon_SELECT_List(user.User.UserInfo.ID, null, null, WCF.UserSystem.Contract.Enumerate.CouponStatus.Normal, null, null, 1, 20);
            if (rtnCouponList.Success)
            {
                ViewBag.CouponList = rtnCouponList.OBJ.ReturnList;
            }

            return View(user);
        }

        public JsonResult OrderCancel(int orderID)
        {
            int code = 1;
            string rtnMsg = null;

            var rtn = App_Code.Proxy.OrderProxy.Order_Order_Cancel(orderID, WCF.OrderSystem.Contract.Enumerate.OrderStatus.UserChannel);
            if (rtn.Success)
                code = 1;
            else
            {
                code = rtn.Code;
                rtnMsg = rtn.Message;
            }
            return Json(new { Code = code, Msg = rtnMsg });
        }

        public JsonResult CalcuPayMoney(int payType, int cardid, int opera, int source = 0)
        {
            int code = 1;
            string rtnMsg = null;

            var u = GetUserInfo();
            var user = u.User;

            Models.CartModel cart = null;
            //if (source == 1)
            //    cart = u.ActiveCart;
            //else
            //    cart = u.Cart;
            cart = u.Cart;

            switch (payType)
            {
                case 1:
                    //余额支付
                    if (opera == 0)
                        cart.UseBalanceEnable = false;
                    else
                        cart.UseBalanceEnable = true;
                    break;
                case 2:
                    //懒人卡支付
                    if (opera == 0)
                        cart.UseCard.Clear();
                    else
                        cart.UseCard[cardid] = 0;
                    break;
                case 3:
                    //优惠券
                    cart.UseCoupon.Clear();
                    if (cardid != 0)
                        cart.UseCoupon.Add(cardid, 0);
                    break;
            }

            decimal TotalPayPrice = cart.PayPrice;

            //cart.PayPrice = cart.TotalPrice;
            cart.UseCardPrice = 0;
            cart.UseBalance = 0;
            cart.UseCouponPrice = 0;
            //if (cart.SalePrice != 0)
            //    TotalPayPrice -= cart.SalePrice;

            int couponID = 0;
            //使用优惠券抵扣
            if (cart.UseCoupon.Count > 0)
            {
                var userCouponList = App_Code.Proxy.UserProxycs.User_Coupon_SELECT_List(user.UserInfo.ID, null, null, WCF.UserSystem.Contract.Enumerate.CouponStatus.Normal, null, null, 1, 20);
                if (userCouponList.Success)
                {
                    var cList = cart.UseCoupon.ToList();
                    foreach (var item in cList)
                    {
                        if (TotalPayPrice == 0)
                        {
                            cart.UseCoupon.Remove(item.Key);
                            continue;
                        }
                        var couponItem = userCouponList.OBJ.ReturnList.FirstOrDefault(p => p.ID == item.Key);
                        if (couponItem != null)
                        {
                            cart.UseCoupon[item.Key] = couponItem.FaceValue;
                            if (TotalPayPrice > couponItem.FaceValue)
                            {
                                cart.UseCouponPrice += couponItem.FaceValue;
                                TotalPayPrice -= couponItem.FaceValue;
                            }
                            else
                            {
                                cart.UseCouponPrice += TotalPayPrice;
                                TotalPayPrice = 0;
                            }
                            couponID = item.Key;
                        }
                    }
                }
            }

            //进行懒人卡抵扣计算
            if (cart.UseCard.Count > 0)
            {
                var userCardList = App_Code.Proxy.UserProxycs.Select_UserCardList(user.UserInfo.ID);
                if (userCardList.Success)
                {
                    var CartUseCardList = cart.UseCard.ToList();
                    foreach (var pair in CartUseCardList)
                    {
                        var cardItem = userCardList.OBJ.FirstOrDefault(p => p.ID == pair.Key);
                        if (cardItem != null)
                        {
                            decimal tmpPrice = 0;
                            //计算懒人卡抵扣
                            if (TotalPayPrice != 0)
                            {
                                if (cardItem.Balance > TotalPayPrice)
                                {
                                    tmpPrice = TotalPayPrice;
                                    TotalPayPrice = 0;
                                }
                                else
                                {
                                    tmpPrice = cardItem.Balance;
                                    TotalPayPrice -= tmpPrice;
                                }
                                cart.UseCardPrice += tmpPrice;
                                cart.UseCard[pair.Key] = tmpPrice;
                            }
                        }
                    }
                }
            }

            //使用余额抵扣
            if (cart.UseBalanceEnable && TotalPayPrice != 0)
            {
                if (user.UserInfo.Money > TotalPayPrice)
                {
                    cart.UseBalance += TotalPayPrice;
                    TotalPayPrice = 0;
                }
                else
                {
                    cart.UseBalance += user.UserInfo.Money;
                    TotalPayPrice -= user.UserInfo.Money;
                }
            }

            var rtnCardList = new List<SelectListItem>();
            foreach (var c in cart.UseCard)
            {
                SelectListItem li = new SelectListItem();
                li.Text = c.Key.ToString();
                li.Value = c.Value.ToString();
                rtnCardList.Add(li);
            }

            return Json(new { Code = code, Msg = rtnMsg, Pay = TotalPayPrice, Balance = cart.UseBalance, CardPay = cart.UseCardPrice, CardList = rtnCardList, CouponPay = cart.UseCouponPrice, CouponID = couponID, ExpressPrice = cart.ExpressMoney, SalePrice = cart.SalePrice });
        }

        public JsonResult SubmitProcess(int address, string remark)
        {
            var user = GetUserInfo();
            int code = 1;
            string rtnMsg = null;

            WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC GetAddress = null;
            WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC SendAddress = null;

            var rtnAddress = App_Code.Proxy.UserProxycs.User_ConsigneeAddress_SELECT_Entity(address);
            if (rtnAddress.Success)
            {
                GetAddress = new WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC();
                GetAddress.Consignee = rtnAddress.OBJ.Consignee;
                GetAddress.Mpno = rtnAddress.OBJ.MPNo;
                GetAddress.Address = rtnAddress.OBJ.DistrictName + rtnAddress.OBJ.Address;
                GetAddress.CityName = rtnAddress.OBJ.CityName;
                GetAddress.ProvinceName = rtnAddress.OBJ.ProvinceName;
                GetAddress.DistrictID = rtnAddress.OBJ.DistrictID;

                SendAddress = new WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC();
                SendAddress.Consignee = rtnAddress.OBJ.Consignee;
                SendAddress.Mpno = rtnAddress.OBJ.MPNo;
                SendAddress.Address = rtnAddress.OBJ.DistrictName + rtnAddress.OBJ.Address;
                SendAddress.CityName = rtnAddress.OBJ.CityName;
                SendAddress.ProvinceName = rtnAddress.OBJ.ProvinceName;
                SendAddress.DistrictID = rtnAddress.OBJ.DistrictID;
            }
            else
            {
                code = rtnAddress.Code;
                rtnMsg = rtnAddress.Message;
            }

            List<int> productList = new List<int>();
            foreach (var p in user.Cart.ProductList)
            {
                for (int i = 0; i < p.Count; i++)
                    productList.Add(p.ProductInfo.ID);
            }

            decimal expressFee = 8m;
            decimal realTotalPrice = user.Cart.PayPrice;
            IDictionary<string, decimal> activeList = null;
            IList<int> couponList = null;

            //优惠券
            if (user.Cart.UseCoupon.Count > 0)
            {
                couponList = new List<int>();
                foreach (KeyValuePair<int, decimal> coupon in user.Cart.UseCoupon)
                {
                    couponList.Add(coupon.Key);
                    realTotalPrice -= coupon.Value;
                }
            }

            ////创始会员
            //if (user.User.Level != (int)WCF.UserSystem.Contract.Enumerate.UserLevel.CharterMembers)
            //{
            //    if (realTotalPrice < 25)
            //    {
            //        expressFee = 0m;
            //        realTotalPrice += 8;
            //    }
            //}
            //else
            //{
            //    activeList = new Dictionary<string, decimal>();

            //    decimal charterSaleMoney = Math.Round(realTotalPrice * 0.2m, 1, MidpointRounding.AwayFromZero);
            //    activeList.Add("创始会员8折优惠", charterSaleMoney);
            //    realTotalPrice -= charterSaleMoney;
            //}

            //懒人卡
            if (user.Cart.UseCard != null)
            {
                foreach (KeyValuePair<int, decimal> item in user.Cart.UseCard)
                {
                    realTotalPrice -= item.Value;
                }
            }

            //余额
            realTotalPrice -= user.Cart.UseBalance;

            string url = "";
            var rtn = App_Code.Proxy.OrderProxy.Order_Weixin_Create(_UserOpenID, AppConfig.SiteID, productList, user.Cart.ExpressMoney, 0m, user.Cart.UseBalance, user.Cart.UseCard, GetAddress, SendAddress, realTotalPrice, activeList, couponList);
            if (rtn.Success)
            {
                if ((rtn.OBJ.TotalAmount - rtn.OBJ.PayAmount) > 0)
                    url = Url.Action("Payment", new { OrderNum = rtn.OBJ.OrderNumber });
                else
                    url = Url.Action("PaymentOk", new { OrderNum = rtn.OBJ.OrderNumber });
                Session["Order_" + rtn.OBJ.OrderNumber] = rtn.OBJ;
                user.Cart.Clear();
                UpdateUserInfo();
            }
            else
            {
                code = rtn.Code;
                rtnMsg = rtn.Message;
            }

            return Json(new { Code = code, Msg = rtnMsg, TagUrl = url });
        }

        public ActionResult Payment(string OrderNum)
        {
            ViewBag.OrderNum = OrderNum;
            return View();
        }

        public ActionResult PaymentOk(string OrderNum)
        {
            ViewBag.OrderNum = OrderNum;
            return View();
        }

        public ActionResult OrderPayment(string OrderNum, int payType = 1)
        {
            if (payType == 1)
                return RedirectToAction("Alipay", new { OrderNum = OrderNum });
            else
                return RedirectToAction("WXPayment", new { OrderNum = OrderNum });
        }

        public ActionResult Alipay(string OrderNum)
        {
            var OrderItem = App_Code.Proxy.OrderProxy.Select_OrderEntity(OrderNum, true, false, false, false, false, false).OBJ;

            decimal payMoney = OrderItem.TotalAmount - OrderItem.PayAmount;
            if (OrderItem.PayStatus == 0 && payMoney > 0)
            {
                string body = "";
                var pList = OrderItem.ProductList.GroupBy(p => p.ProductID);
                foreach (var product in pList)
                {
                    var pDC = product.First();
                    body += pDC.Name + " 数量：" + product.Count() + " 价格：" + pDC.Price * product.Count() + " ";
                }

                //返回格式
                string format = "xml";
                //版本
                string v = "2.0";
                //请求号 必填，须保证每次请求都是唯一
                string req_id = DateTime.Now.ToString("yyyyMMddHHmmssfff");

                //请求业务参数详细
                StringBuilder req_dataToken = new StringBuilder("<direct_trade_create_req>");
                req_dataToken.Append("<notify_url>" + AppConfig.Alipay_Notify_URL + "</notify_url>");
                req_dataToken.Append("<call_back_url>" + AppConfig.Alipay_Return_URL + "</call_back_url>");
                req_dataToken.Append("<seller_account_name>" + AppConfig.Alipay_Seller_Email + "</seller_account_name>");
                req_dataToken.Append("<out_trade_no>" + OrderItem.OrderNumber + "</out_trade_no>");
                req_dataToken.Append("<subject>" + OrderItem.Title + "</subject>");
                req_dataToken.Append("<total_fee>" + payMoney + "</total_fee>");
                req_dataToken.Append("</direct_trade_create_req>");

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
                string sHtmlTextToken = Submit.BuildRequest(Config.GateWay, sParaTempToken);
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
                string alipayUrl = Config.GateWay + param;
                return Redirect(alipayUrl);
            }
            else
            {
                ViewBag.OrderNum = OrderNum;
                return View("PaymentOk");
            }
        }

        public JsonResult jsAlipay(string OrderNum)
        {
            int rtncode = 0;
            string rtnmsg = "";

            var OrderItem = App_Code.Proxy.OrderProxy.Select_OrderEntity(OrderNum, true, false, false, false, false, false).OBJ;

            decimal payMoney = OrderItem.TotalAmount - OrderItem.PayAmount;
            if (OrderItem.PayStatus == 0 && payMoney > 0)
            {
                string body = "";
                var pList = OrderItem.ProductList.GroupBy(p => p.ProductID);
                foreach (var product in pList)
                {
                    var pDC = product.First();
                    body += pDC.Name + " 数量：" + product.Count() + " 价格：" + pDC.Price * product.Count() + " ";
                }

                //返回格式
                string format = "xml";
                //版本
                string v = "2.0";
                //请求号 必填，须保证每次请求都是唯一
                string req_id = DateTime.Now.ToString("yyyyMMddHHmmssfff");

                //请求业务参数详细
                StringBuilder req_dataToken = new StringBuilder("<direct_trade_create_req>");
                req_dataToken.Append("<notify_url>" + AppConfig.Alipay_Notify_URL + "</notify_url>");
                req_dataToken.Append("<call_back_url>" + AppConfig.Alipay_Return_URL + "</call_back_url>");
                req_dataToken.Append("<seller_account_name>" + AppConfig.Alipay_Seller_Email + "</seller_account_name>");
                req_dataToken.Append("<out_trade_no>" + OrderItem.OrderNumber + "</out_trade_no>");
                req_dataToken.Append("<subject>" + OrderItem.Title + "</subject>");
                req_dataToken.Append("<total_fee>" + payMoney + "</total_fee>");
                req_dataToken.Append("</direct_trade_create_req>");

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
                string sHtmlTextToken = Submit.BuildRequest(Config.GateWay, sParaTempToken);
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
                string alipayUrl = Config.GateWay + param;
                rtnmsg = alipayUrl;
            }
            else
            {
                rtnmsg = "/Cart/PaymentOk?OrderNum=" + OrderNum;
            }

            rtncode = 1;
            return Json(new { Code = rtncode, Msg = rtnmsg });
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

                    //打印页面
                    //Response.Write("验证成功<br />");
                    ViewBag.OrderNum = out_trade_no;
                    return View("PaymentOk");
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
            App_Code.UtilityFunc.Add("------------------------------------\r\nCall AlipayNotify");
            Dictionary<string, string> sPara = GetRequest("post");

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

        public ActionResult ReOrder(string orderNum)
        {
            var user = GetUserInfo();

            if (user.User == null)
            {
                HttpContext.Cache[AppConfig.Cache_CallbackUrl + _UserOpenID] = Request.Url.ToString();
                return RedirectToAction("Reg", "Member");
            }

            WCF.OrderSystem.Contract.DataContract.Order_OrderDC orderItem = null;
            var rtn = App_Code.Proxy.OrderProxy.Select_OrderEntity(orderNum, true, true, true, true, true, true);
            if (rtn.Success)
            {
                user.Cart.Clear();
                orderItem = rtn.OBJ;
                IList<Models.CartProductItemModel> xList = new List<Models.CartProductItemModel>();
                if (orderItem.ProductList != null)
                {
                    var plist = orderItem.ProductList.GroupBy(p => p.ProductID);
                    foreach (var product in plist)
                    {
                        Models.CartProductItemModel pi = new Models.CartProductItemModel();
                        var pDC = product.First();
                        var productRtn = WCF.Wash.Contract.ClientProxy.web_ProductClient.Cache_web_Wash_Product_SELECT_Entity(pDC.ProductID);
                        pi.ProductInfo = productRtn.OBJ;
                        pi.Count = product.Count();
                        xList.Add(pi);
                    }
                }
                user.Cart.ProductList = xList;
                user.Cart.TotalProductInfo();
            }
            return Redirect("OrderConfirm");
        }

        public ActionResult WXPayment(string OrderNum)
        {
            var OrderItem = App_Code.Proxy.OrderProxy.Select_OrderEntity(OrderNum, true, false, false, false, false, false).OBJ;
            decimal payMoney = OrderItem.TotalAmount - OrderItem.PayAmount;
            if (OrderItem.PayStatus == 0 && payMoney > 0)
            {
                //创建支付应答对象
                RequestHandler packageReqHandler = new RequestHandler(Request.ContentEncoding.BodyName);
                //初始化
                packageReqHandler.init();
                //设置package订单参数
                packageReqHandler.setParameter("partner", TenpayUtil.partner);		  //商户号
                packageReqHandler.setParameter("fee_type", "1");                    //币种，1人民币
                packageReqHandler.setParameter("input_charset", "GBK");
                packageReqHandler.setParameter("out_trade_no", OrderNum);		//商家订单号
                packageReqHandler.setParameter("total_fee", "1");			        //商品金额,以分为单位(money * 100).ToString()
                packageReqHandler.setParameter("notify_url", "");		    //接收财付通通知的URL
                packageReqHandler.setParameter("body", "JSAPIdemo");	                    //商品描述
                packageReqHandler.setParameter("spbill_create_ip", Request.UserHostAddress);   //用户的公网ip，不是商户服务器IP

                //获取package包
                var packageValue = packageReqHandler.getRequestURL();

                //调起微信支付签名
                var timeStamp = TenpayUtil.getTimestamp();
                var nonceStr = TenpayUtil.getNoncestr();

                //设置支付参数
                RequestHandler paySignReqHandler = new RequestHandler(Request.ContentEncoding.BodyName);
                paySignReqHandler.setParameter("appid", TenpayUtil.appid);
                paySignReqHandler.setParameter("appkey", TenpayUtil.appkey);
                paySignReqHandler.setParameter("noncestr", nonceStr);
                paySignReqHandler.setParameter("timestamp", timeStamp);
                paySignReqHandler.setParameter("package", packageValue);
                var paySign = paySignReqHandler.createSHA1Sign();

                ViewBag.packageValue = packageValue;
                ViewBag.timeStamp = timeStamp;
                ViewBag.nonceStr = nonceStr;
                ViewBag.paySign = paySign;
                return View();
            }
            else
            {
                ViewBag.OrderNum = OrderNum;
                return View("PaymentOk");
            }
        }

        public JsonResult jsWXPayment(string OrderNum)
        {
            int rtncode = 0;
            string rtnmsg = "";

            string packageValue = "";
            string timeStamp = "";
            string nonceStr = "";
            string paySign = "";

            var OrderItem = App_Code.Proxy.OrderProxy.Select_OrderEntity(OrderNum, true, false, false, false, false, false).OBJ;
            decimal payMoney = OrderItem.TotalAmount - OrderItem.PayAmount;
            if (OrderItem.PayStatus == 0 && payMoney > 0)
            {
                //创建支付应答对象
                RequestHandler packageReqHandler = new RequestHandler(Request.ContentEncoding.BodyName);
                //初始化
                packageReqHandler.init();
                //设置package订单参数
                packageReqHandler.setParameter("partner", TenpayUtil.partner);		  //商户号
                packageReqHandler.setParameter("fee_type", "1");                    //币种，1人民币
                packageReqHandler.setParameter("input_charset", "GBK");
                packageReqHandler.setParameter("out_trade_no", OrderNum);		//商家订单号
                packageReqHandler.setParameter("total_fee", "1");			        //商品金额,以分为单位(money * 100).ToString()
                packageReqHandler.setParameter("notify_url", "");		    //接收财付通通知的URL
                packageReqHandler.setParameter("body", "JSAPIdemo");	                    //商品描述
                packageReqHandler.setParameter("spbill_create_ip", Request.UserHostAddress);   //用户的公网ip，不是商户服务器IP

                //获取package包
                packageValue = packageReqHandler.getRequestURL();

                //调起微信支付签名
                timeStamp = TenpayUtil.getTimestamp();
                nonceStr = TenpayUtil.getNoncestr();

                //设置支付参数
                RequestHandler paySignReqHandler = new RequestHandler(Request.ContentEncoding.BodyName);
                paySignReqHandler.setParameter("appid", TenpayUtil.appid);
                paySignReqHandler.setParameter("appkey", TenpayUtil.appkey);
                paySignReqHandler.setParameter("noncestr", nonceStr);
                paySignReqHandler.setParameter("timestamp", timeStamp);
                paySignReqHandler.setParameter("package", packageValue);
                paySign = paySignReqHandler.createSHA1Sign();
                rtncode = 1;
            }
            else
            {
                rtncode = 2;
                rtnmsg = "/Cart/PaymentOk?OrderNum=" + OrderNum;
            }

            return Json(new { Code = rtncode, Msg = rtnmsg, appid = TenpayUtil.appid, packageValue = packageValue, timeStamp = timeStamp, nonceStr = nonceStr, paySign = paySign });
        }

        [ValidateInput(false)]
        public ActionResult WxPayNotify()
        {
            App_Code.UtilityFunc.Add("------------------------------------\r\nCall WxPayNotify");

            ResponseHandler resHandler = new ResponseHandler(HttpContext);
            resHandler.init();
            resHandler.setKey(TenpayUtil.key, TenpayUtil.appkey);

            //判断签名
            if (resHandler.isTenpaySign())
            {
                if (resHandler.isWXsign())
                {
                    //商户在收到后台通知后根据通知ID向财付通发起验证确认，采用后台系统调用交互模式
                    string notify_id = resHandler.getParameter("notify_id");
                    //取结果参数做业务处理
                    string out_trade_no = resHandler.getParameter("out_trade_no");
                    //财付通订单号
                    string transaction_id = resHandler.getParameter("transaction_id");
                    //金额,以分为单位
                    decimal total_fee = int.Parse(resHandler.getParameter("total_fee")) / 100m;
                    //如果有使用折扣券，discount有值，total_fee+discount=原请求的total_fee
                    string discount = resHandler.getParameter("discount");
                    //支付结果
                    string trade_state = resHandler.getParameter("trade_state");

                    //即时到账
                    if ("0".Equals(trade_state))
                    {
                        //------------------------------
                        //处理业务开始
                        //------------------------------

                        //处理数据库逻辑
                        //注意交易单不要重复处理
                        //注意判断返回金额

                        var rtn = App_Code.Proxy.OrderProxy.Update_OrderPay(out_trade_no, total_fee, WCF.OrderSystem.Contract.Enumerate.PayMoneyType.TenPay, WCF.OrderSystem.Contract.Enumerate.PayMoneyChannel.Weixin, DateTime.Now, out_trade_no);
                        if (!rtn.Success)
                        {
                            if (rtn.Code != -70)
                            {
                                App_Code.UtilityFunc.Add("处理订单失败：" + out_trade_no + " Error:" + rtn.Message);
                                return Content("支付失败");
                            }
                        }

                        //------------------------------
                        //处理业务完毕
                        //------------------------------

                        //给财付通系统发送成功信息，财付通系统收到此结果后不再进行后续通知
                        return Content("success 后台通知成功");
                    }
                    else
                    {
                        return Content("支付失败");
                    }
                }
                else
                {
                    //SHA1签名失败
                    App_Code.UtilityFunc.Add(resHandler.getDebugInfo());
                    return Content("fail -SHA1 failed");
                }
            }
            else
            {
                //md5签名失败
                App_Code.UtilityFunc.Add(resHandler.getDebugInfo());
                return Content("fail -md5 failed");
            }
        }
    }
}