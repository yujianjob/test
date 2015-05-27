using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using LazyAtHome.Library.Pay.MobileEmbed.Alipay;
using System.Collections.Specialized;
using System.Xml;
using LazyAtHome.Library.Pay.MobileEmbed.WXPayment;

namespace LazyAtHome.Web.WeiXin3.Controllers
{
    public class CartController : BaseController
    {
        public ActionResult OneKey()
        {
            var user = GetUserInfo();
            if (user.User == null)
            {
                HttpContext.Cache[AppConfig.Cache_CallbackUrl + _UserOpenID] = Request.Url.ToString();
                return RedirectToAction("MobileBindingShow", "Member");
            }

            if (user.User != null)
            {
                var rtn = App_Code.Proxy.UserProxycs.Select_UserAddress(user.User.UserInfo.ID);
                if (rtn.Success)
                {
                    ViewBag.AddressList = rtn.OBJ;
                }

                var rtnCouponList = App_Code.Proxy.UserProxycs.User_Coupon_SELECT_List(user.User.UserInfo.ID, null, null, WCF.UserSystem.Contract.Enumerate.CouponStatus.Normal, null, null, 1, 20);
                if (rtnCouponList.Success)
                {
                    ViewBag.CouponList = rtnCouponList.OBJ.ReturnList;
                }

                ViewBag.UserMoney = user.User.UserInfo.Money;
            }

            return View();
        }

        public JsonResult OneKeySubmit(int AddressID, DateTime? exceptTime, string remark, int? couponid,int? balance)
        {
            int code = 0;
            string msg = "";
            

            int bUseBalance = 0;
            if (balance.HasValue && balance.Value != 0)
                bUseBalance = 1;

            var user = GetUserInfo();

            if (user.User != null)
            {
                App_Code.UtilityFunc.AddToFile(user.User.UserInfo.MPNo + " OneKeySubmit", "OneKeySubmit");
            }

            WCF.UserSystem.Contract.DataContract.User_ConsigneeAddressDC SelectAddress = null;
            var rtn = App_Code.Proxy.UserProxycs.User_ConsigneeAddress_SELECT_Entity(AddressID);
            if (rtn.Success)
                SelectAddress = rtn.OBJ;


            if (SelectAddress == null)
            {
                code = -1;
                msg = "请添加取送地址";
            }

            if (ViewBag.Msg != null)
                return Json(new { Code = code, Msg = msg });

            WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC OrderAddress = new WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC();
            OrderAddress.Consignee = SelectAddress.Consignee;
            OrderAddress.Mpno = SelectAddress.MPNo;
            OrderAddress.Address = SelectAddress.Address;
            OrderAddress.CityName = SelectAddress.CityName;
            OrderAddress.ProvinceName = SelectAddress.ProvinceName;
            OrderAddress.DistrictID = SelectAddress.DistrictID;

            var orderRTN = App_Code.Proxy.OrderProxy.Order_Onekey_Create(_UserOpenID, 1, WCF.OrderSystem.Contract.Enumerate.Channel.Weixin, OrderAddress, OrderAddress, exceptTime, remark, couponid, bUseBalance);
            if (!orderRTN.Success)
            {
                code = orderRTN.Code;
                msg = orderRTN.Message;
            }
            else
            {
                code = 1;
                msg = "一键下单完成";
            }

            return Json(new { Code = code, Msg = msg });
            //return RedirectToAction("PaymentOk", "Cart", new { orderNum = orderRTN.OBJ.OrderNumber });
        }

        public JsonResult OneKeyFirstSubmit(string username, string mpno, string address, DateTime? exceptTime, string remark, int? couponid,int? balance)
        {
            int code = 0;
            string rtnMsg = "";

            int bUseBalance = 0;
            if (balance.HasValue && balance.Value != 0)
                bUseBalance = 1;

            var user = GetUserInfo();
            if (user.User != null)
            {
                App_Code.UtilityFunc.AddToFile(user.User.UserInfo.MPNo + " OneKeyFirstSubmit", "OneKeySubmit");
                
                code = 1;

                WCF.UserSystem.Contract.DataContract.User_ConsigneeAddressDC item = new WCF.UserSystem.Contract.DataContract.User_ConsigneeAddressDC();
                item.UserID = user.User.UserInfo.ID;
                item.Consignee = username;
                item.DistrictID = 0;
                item.Address = address;
                item.MPNo = mpno;
                item.IsDefault = 1;

                var addressRtn = App_Code.Proxy.UserProxycs.User_ConsigneeAddress_ADD(item);
                if (addressRtn.Success)
                {
                    WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC orderAddress = new WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC();
                    orderAddress.Consignee = username;
                    orderAddress.Mpno = mpno;
                    orderAddress.Address = address;
                    orderAddress.CityName = "";
                    orderAddress.ProvinceName = "";
                    orderAddress.DistrictID = 0;

                    var orderRTN = App_Code.Proxy.OrderProxy.Order_Onekey_Create(_UserOpenID, 1, WCF.OrderSystem.Contract.Enumerate.Channel.Weixin, orderAddress, orderAddress, exceptTime, remark, couponid, bUseBalance);
                    if (!orderRTN.Success)
                    {
                        code = orderRTN.Code;
                        rtnMsg = orderRTN.Message;
                    }
                    else
                    {
                        code = 1;
                        rtnMsg = "一键下单完成";
                    }
                }
                else
                {
                    code = 0;
                    rtnMsg = "添加用户地址失败";
                    App_Code.UtilityFunc.Add("添加用户地址失败：" + addressRtn.Message);
                }
            }
            else
            {
                code = -1;
                rtnMsg = "订单提交失败";
                App_Code.UtilityFunc.Add("订单提交失败，用户不存在，openid:" + _UserOpenID);
            }

            return Json(new { Code = code, Msg = rtnMsg });
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
                sb.Append("<div class=\"unit\"><div class=\"lfbt\">" + item.ProductInfo.Name + "</div><div class=\"setnum\"><button ontouchend=\"AddToCart(" + item.ProductInfo.ID + ", true, event)\">+</button><button style=\"margin-left:1px;margin-right:1px\">" + item.Count + "</button><button ontouchend=\"AddToCart(" + item.ProductInfo.ID + ", false, event)\">-</button></div></div>");
            }
            code = 1;

            return Json(new { Code = code, Msg = msg, Content = sb.ToString(), TotalPrice = user.Cart.PayPrice, TotalCount = user.Cart.TotalCount });
        }

        public JsonResult jsRHAddToCart(int pid, bool opearAdd, int type)
        {
            int code = 0;
            string msg = "";
            var user = GetUserInfo(false);
            if (user.RHCart == null)
                user.RHCart = new Models.RHModel();

            if (pid != 0)
            {
                if (opearAdd)
                {

                    if (user.RHCart.TotalCount >= 2)
                        return Json(new { Code = -1, Msg = "只能选择2件A类" });


                    if (user.RHCart.ClassB != 0)
                        return Json(new { Code = -1, Msg = "只能选择1件B类" });

                    if (type == 1)
                    {
                        user.RHCart.ClassB = 0;
                        if (user.RHCart.ClassAList.ContainsKey(pid))
                            user.RHCart.ClassAList[pid] = 2;
                        else
                            user.RHCart.ClassAList[pid] = 1;
                        user.RHCart.TotalCount++;
                    }
                    else
                    {
                        user.RHCart.ClassB = pid;
                        user.RHCart.ClassAList.Clear();
                    }
                }
                else
                {
                    if (type == 1)
                    {
                        if (user.RHCart.ClassAList.ContainsKey(pid))
                        {
                            if (user.RHCart.ClassAList[pid] == 2)
                                user.RHCart.ClassAList[pid] = 1;
                            else
                                user.RHCart.ClassAList.Remove(pid);
                            user.RHCart.TotalCount--;
                        }
                    }
                    else
                        user.RHCart.ClassB = 0;
                }

                user.RHCart.ProductList.Clear();
                if (user.RHCart.ClassB != 0)
                {
                    var rtn = WCF.Wash.Contract.ClientProxy.web_ProductClient.Cache_web_Wash_Product_SELECT_Entity(pid);
                    if (rtn.Success)
                    {
                        Models.RHProductModel pItem = new Models.RHProductModel();
                        pItem.Product = rtn.OBJ;
                        pItem.Count = 1;
                        pItem.Type = 2;
                        user.RHCart.ProductList.Add(pItem);
                    }
                }
                else
                {
                    foreach (KeyValuePair<int, int> p in user.RHCart.ClassAList)
                    {
                        var rtn = WCF.Wash.Contract.ClientProxy.web_ProductClient.Cache_web_Wash_Product_SELECT_Entity(p.Key);
                        if (rtn.Success)
                        {
                            Models.RHProductModel pItem = new Models.RHProductModel();
                            pItem.Product = rtn.OBJ;
                            pItem.Count = p.Value;
                            pItem.Type = 1;
                            user.RHCart.ProductList.Add(pItem);
                        }
                    }
                }
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            decimal totalPrice = 0;
            int totalCount = 0;
            foreach (var item in user.RHCart.ProductList)
            {
                totalPrice += item.Product.SalesPrice * item.Count;
                totalCount += item.Count;
                sb.Append("<div class=\"unit\"><div class=\"lfbt\">" + item.Product.Name + "</div><div class=\"setnum\"><button ontouchend=\"OpearCart(" + item.Product.ID + ", true, " + item.Type + ", event)\">+</button><button style=\"margin-left:1px;margin-right:1px\">" + item.Count + "</button><button ontouchend=\"OpearCart(" + item.Product.ID + ", false, " + item.Type + ", event)\">-</button></div></div>");                
            }
            user.RHCart.TotalPrice = totalPrice;
            code = 1;

            return Json(new { Code = code, Msg = msg, Content = sb.ToString(), TotalPrice = totalPrice, TotalCount = totalCount });
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

        public ActionResult OrderConfirm()
        {
            var user = GetUserInfo();

            if (user.User == null)
            {
                HttpContext.Cache[AppConfig.Cache_CallbackUrl + _UserOpenID] = Request.Url.ToString();
                return RedirectToAction("MobileBindingShow", "Member");
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

            cart.UseCardPrice = 0;
            cart.UseBalance = 0;
            cart.UseCouponPrice = 0;
            cart.ExpressEnable = true;
            cart.TotalProductInfo();
            decimal TotalPayPrice = cart.PayPrice;

            //if (cart.SalePrice != 0)
            //    TotalPayPrice -= cart.SalePrice;

            decimal tmpPrice = 0;
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
                            cart.ExpressEnable = false;
                            cart.TotalProductInfo();
                            TotalPayPrice = cart.PayPrice;

                            if (couponItem.CouponID == 4 || couponItem.CouponID == 5)
                            {
                                TotalPayPrice = cart.PayPrice;
                                if (cart.MaxFreeMoney != 0)
                                {
                                    cart.UseCouponPrice += cart.MaxFreeMoney;
                                    TotalPayPrice -= cart.MaxFreeMoney;
                                }
                                else
                                {
                                    code = -3;
                                    rtnMsg = "优惠券无法使用！";
                                    return Json(new { Code = code, Msg = rtnMsg });
                                }
                            }
                            else if (couponItem.CouponID == 2)
                            {
                                if (cart.TotalMinPrice > couponItem.FaceValue)
                                {
                                    cart.UseCouponPrice += couponItem.FaceValue;
                                    TotalPayPrice -= couponItem.FaceValue;
                                }
                                else
                                {
                                    cart.UseCouponPrice += cart.TotalMinPrice;
                                    TotalPayPrice -= cart.TotalMinPrice;
                                }
                                cart.UseCoupon[item.Key] = cart.UseCouponPrice;
                            }
                            else
                            {
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
                            tmpPrice = 0;
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

        public JsonResult SubmitProcessFirst(string username, string mpno, string address, string remark)
        {
            int code = 0;
            string rtnMsg = "";

            var user = GetUserInfo();
            if (user.User != null)
            {
                code = 1;

                WCF.UserSystem.Contract.DataContract.User_ConsigneeAddressDC item = new WCF.UserSystem.Contract.DataContract.User_ConsigneeAddressDC();
                item.UserID = user.User.UserInfo.ID;
                item.Consignee = username;
                item.DistrictID = 0;
                item.Address = address;
                item.MPNo = mpno;
                item.IsDefault = 1;

                var addressRtn = App_Code.Proxy.UserProxycs.User_ConsigneeAddress_ADD(item);
                if (addressRtn.Success)
                {
                    return SubmitProcess(addressRtn.OBJ, remark);
                }
                else
                {
                    code = 0;
                    rtnMsg = "添加用户地址失败";
                    App_Code.UtilityFunc.Add("添加用户地址失败：" + addressRtn.Message);
                }
            }
            else
            {
                code = -1;
                rtnMsg = "订单提交失败";
                App_Code.UtilityFunc.Add("订单提交失败，用户不存在，openid:" + _UserOpenID);
            }

            return Json(new { Code = code, Msg = rtnMsg });
        }

        public JsonResult SubmitProcess(int address, string remark)
        {
            var user = GetUserInfo();
            int code = 1;
            string rtnMsg = null;
            
            WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC OrderAddress = null;

            var rtnAddress = App_Code.Proxy.UserProxycs.User_ConsigneeAddress_SELECT_Entity(address);
            if (rtnAddress.Success)
            {
                OrderAddress = new WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC();
                OrderAddress.Consignee = rtnAddress.OBJ.Consignee;
                OrderAddress.Mpno = rtnAddress.OBJ.MPNo;
                OrderAddress.Address = rtnAddress.OBJ.DistrictName + rtnAddress.OBJ.Address;
                OrderAddress.CityName = rtnAddress.OBJ.CityName;
                OrderAddress.ProvinceName = rtnAddress.OBJ.ProvinceName;
                OrderAddress.DistrictID = rtnAddress.OBJ.DistrictID;
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
            var rtn = App_Code.Proxy.OrderProxy.Order_Weixin_Create(_UserOpenID, AppConfig.SiteID, productList, user.Cart.ExpressMoney, 0m, user.Cart.UseBalance, user.Cart.UseCard, OrderAddress, OrderAddress, realTotalPrice, activeList, couponList);
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
            App_Code.UtilityFunc.Add("------------------------------------\r\nCall Cart AlipayNotify");
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

        public JsonResult jsWXPayment(string OrderNum)
        {
            int rtncode = 0;
            string rtnmsg = "";

            string timeStamp = "";
            string nonceStr = "";
            string paySign = "";
            string prepay_id = "";

            var OrderItem = App_Code.Proxy.OrderProxy.Select_OrderEntity(OrderNum, true, false, false, false, false, false).OBJ;
            decimal payMoney = OrderItem.TotalAmount - OrderItem.PayAmount;
            if (OrderItem.PayStatus == 0 && payMoney > 0)
            {
                WXPay pay = new WXPay();
                pay.SetParameter("appid", AppConfig.wxPay_AppID);//公众账号ID
                pay.SetParameter("mch_id", AppConfig.wxPay_Mch_ID);//商户号
                pay.SetParameter("nonce_str", TenpayUtil.getNoncestr());//随机字符串            
                pay.SetParameter("body", OrderItem.Title);//商品描述
                pay.SetParameter("out_trade_no", OrderItem.OrderNumber);//商户订单号
                pay.SetParameter("total_fee", Convert.ToInt32(payMoney * 100).ToString());//总金额 单位为分，不能带小数点
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
                    rtncode = 1;
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
            else
            {
                rtncode = 2;
                rtnmsg = "/Cart/PaymentOk?OrderNum=" + OrderNum;
            }

            return Json(new { Code = rtncode, Msg = rtnmsg, appid = AppConfig.wxPay_AppID, prepay_id = prepay_id, timeStamp = timeStamp, nonceStr = nonceStr, paySign = paySign });
        }

        [ValidateInput(false)]
        public ActionResult WxPayNotify()
        {
            App_Code.UtilityFunc.AddToFile("------------------------------------\r\nCall WxPayNotify", "wxPay");

            WXPay pay = new WXPay();
            var rtnXML = pay.GetResponseXML(HttpContext);
            App_Code.UtilityFunc.AddToFile(rtnXML, "wxPay");


            if (pay.Verify())
            {
                string out_trade_no = pay.Parameters["out_trade_no"];

                string transaction_id = pay.Parameters["transaction_id"];
                //金额,以分为单位
                decimal total_fee = int.Parse(pay.Parameters["total_fee"]) / 100m;

                var rtn = App_Code.Proxy.OrderProxy.Update_OrderPay(out_trade_no, total_fee, WCF.OrderSystem.Contract.Enumerate.PayMoneyType.Weixin, WCF.OrderSystem.Contract.Enumerate.PayMoneyChannel.Weixin, DateTime.Now, out_trade_no);
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
                App_Code.UtilityFunc.AddToFile("验证签名失败", "wxPay");
                return Content("<xml><return_code><![CDATA[FAIL]]></return_code><return_msg><![CDATA[签名失败]]></return_msg></xml>");
            }
        }
    }
}