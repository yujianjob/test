using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Specialized;

using LazyAtHome.Library.Pay.Common.Alipay;
using LazyAtHome.WCF.Wash.Contract.ClientProxy;
using LazyAtHome.WCF.Wash.Contract.DataContract.web;

namespace LazyAtHome.Web.WashSite.Controllers
{
    public class CartController : BaseController
    {
        [NoCacheFilter]
        public ActionResult Index()
        {
            var user = GetUserInfo();
            
            return View(user);
        }

        public ActionResult CartAdd(int spid, int pcount, int submitType)
        {
            CartProductAdd(spid, pcount);
            //var user = GetUserInfo();
            //var rtn = web_ProductClient.Cache_web_Wash_Product_SELECT_Entity(spid);

            //var pItem = user.Cart.ProductList.FirstOrDefault(p => p.ProductInfo.ID == spid);
            //if (pItem == null)
            //{
            //    pItem = new Models.User.CartProductItem();
            //    pItem.ProductInfo = rtn.OBJ;
            //    pItem.Count = pcount;
            //    user.Cart.ProductList.Add(pItem);
            //}
            //else
            //    pItem.Count += pcount;

            //user.Cart.TotalPrice += pItem.ProductInfo.SalesPrice * pcount;
            //user.Cart.TotalCount += pcount;

            //ClearPayInfo();

            if (submitType == 2)
                return RedirectToAction("Submit");
            else
                return RedirectToAction("Index");
        }

        public ActionResult CartDelete(int id)
        {
            var user = GetUserInfo();
            var item = user.Cart.ProductList.FirstOrDefault(p => p.ProductInfo.ID == id);
            if (item != null)
            {
                user.Cart.ProductList.Remove(item);
                user.Cart.TotalCount -= item.Count;
                user.Cart.TotalPrice -= item.ProductInfo.SalesPrice * item.Count;
            }
            ClearPayInfo(user.Cart);
            return View("index", user);
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

        public JsonResult CartCountChange(int pid, int pcount)
        {
            var user = GetUserInfo();
            var item = user.Cart.ProductList.First(p => p.ProductInfo.ID == pid);
            if (item != null)
            {
                user.Cart.TotalPrice -= item.ProductInfo.SalesPrice * item.Count;
                int change = pcount - item.Count;
                item.Count = pcount;
                user.Cart.TotalPrice += item.ProductInfo.SalesPrice * item.Count;
                user.Cart.TotalCount += change;
            }
            ClearPayInfo(user.Cart);
            return Json(new Object());
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

        [NoCacheFilter]
        [Authorize]
        public ActionResult Submit()
        {
            var user = GetUserInfo();
            var rtn = App_Code.Proxy.UserProxycs.Select_UserAddress(user.User.ID);
            if (rtn.Success)
            {
                ViewBag.AddressList = rtn.OBJ;
            }

            user.Cart.Clear(false);

            var rtnCardList = App_Code.Proxy.UserProxycs.Select_UserCardList(user.User.ID);
            if (rtnCardList.Success)
                ViewBag.CardList = rtnCardList.OBJ;

            var rtnCouponList = App_Code.Proxy.UserProxycs.User_Coupon_SELECT_List(user.User.ID, null, null, WCF.UserSystem.Contract.Enumerate.CouponStatus.Normal, null, null, 1, 20);
            if (rtnCouponList.Success)
                ViewBag.CouponList = rtnCouponList.OBJ.ReturnList;

            return View(user);
        }

        public JsonResult SubmitProcess(int address, string remark)
        {
            var user = GetUserInfo();
            int code = 1;
            string rtnMsg = null;

            WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC GetAddress = null;
            WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC SendAddress = null;

            var rtnAddress = App_Code.Proxy.UserProxycs.Select_UserAddressEntity(address);
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
            decimal realTotalPrice = user.Cart.TotalPrice;
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

            //创始会员
            if (user.User.Level != (int)WCF.UserSystem.Contract.Enumerate.UserLevel.CharterMembers)
            {
                if (realTotalPrice < 25)
                {
                    expressFee = 0m;
                    realTotalPrice += 8;
                }
            }
            else
            {
                activeList = new Dictionary<string, decimal>();

                decimal charterSaleMoney = Math.Round(realTotalPrice * 0.2m, 1, MidpointRounding.AwayFromZero);
                activeList.Add("创始会员8折优惠", charterSaleMoney);
                realTotalPrice -= charterSaleMoney;
            }

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
            var rtn = App_Code.Proxy.OrderProxy.Order_Create_Common(user.User.ID, AppConfig.SiteID, productList, 8m, expressFee, user.Cart.UseBalance, user.Cart.UseCard, GetAddress, SendAddress, realTotalPrice, activeList, couponList);
            if (rtn.Success)
            {
                if (user.Cart.PayPrice > 0)
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

        public ActionResult Alipay(string OrderNum)
        {
            var OrderItem = App_Code.Proxy.OrderProxy.Select_OrderEntity(OrderNum, true, false, false, false, false, false).OBJ;

            decimal payMoney = OrderItem.TotalAmount - OrderItem.PayAmount;
            if (OrderItem.PayStatus == 0 && payMoney > 0)
            {
                Config.Partner = AppConfig.Alipay_Partner;
                Config.Key = AppConfig.Alipay_Key;

                string body = "";
                var pList = OrderItem.ProductList.GroupBy(p => p.ProductID);
                foreach (var product in pList)
                {
                    var pDC = product.First();
                    body += pDC.Name + " 数量：" + product.Count() + " 价格：" + pDC.Price * product.Count() + " ";
                }

                //把请求参数打包成数组
                SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
                sParaTemp.Add("partner", AppConfig.Alipay_Partner);
                sParaTemp.Add("_input_charset", Config.Input_charset.ToLower());
                sParaTemp.Add("service", "create_direct_pay_by_user");
                sParaTemp.Add("out_trade_no", OrderItem.OrderNumber);
                sParaTemp.Add("subject", OrderItem.Title);//
                sParaTemp.Add("payment_type", "1");
                sParaTemp.Add("seller_email", AppConfig.Alipay_Seller_Email);
                sParaTemp.Add("total_fee", payMoney.ToString());
                sParaTemp.Add("body", body);
                //sParaTemp.Add("it_b_pay", "1h");
                sParaTemp.Add("notify_url", AppConfig.Alipay_Notify_URL);
                sParaTemp.Add("return_url", AppConfig.Alipay_Return_URL);

                string param = LazyAtHome.Library.Pay.Common.Alipay.Submit.BuildRequestParaToString(sParaTemp, System.Text.Encoding.UTF8);
                string alipayUrl = LazyAtHome.Library.Pay.Common.Alipay.Submit.GATEWAY_NEW + param;
                return Redirect(alipayUrl);
            }
            else
            {
                ViewBag.OrderNum = OrderNum;
                return View("PaymentOk");
            }            
        }

        public ActionResult AlipayNotify()
        {
            App_Code.UtilityFunc.Add("------------------------------------\r\nCall AlipayNotify");
            SortedDictionary<string, string> sPara = GetRequestPost("post");
            var RequestColl = Request.Form;

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                Notify aliNotify = new Notify();
                bool verifyResult = aliNotify.Verify(sPara, RequestColl["notify_id"], RequestColl["sign"]);
                string trade_status = RequestColl["trade_status"];

                App_Code.UtilityFunc.Add("verifyResult:" + verifyResult);
                App_Code.UtilityFunc.Add("trade_status:" + trade_status);

                if (verifyResult)//验证成功
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //请在这里加上商户的业务逻辑程序代码


                    //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                    //获取支付宝的通知返回参数，可参考技术文档中服务器异步通知参数列表

                    //商户订单号
                    string out_trade_no = RequestColl["out_trade_no"];
                    //支付宝交易号
                    string trade_no = RequestColl["trade_no"];
                    
                    decimal alipayMoney = decimal.Parse(RequestColl["total_fee"]);
                    DateTime alipayPayTime = DateTime.Parse(RequestColl["gmt_payment"]);


                    if (trade_status == "TRADE_FINISHED" || trade_status == "TRADE_SUCCESS")
                    {
                        var rtn = App_Code.Proxy.OrderProxy.Update_OrderPay(out_trade_no, alipayMoney, WCF.OrderSystem.Contract.Enumerate.PayMoneyType.AliPay, WCF.OrderSystem.Contract.Enumerate.PayMoneyChannel.Web, alipayPayTime, trade_no);
                        if (!rtn.Success)
                        {
                            if (rtn.Code != -70)
                                App_Code.UtilityFunc.Add("处理订单失败：" + out_trade_no + " Error:" + rtn.Message);
                        }
                    }
                    else
                    {
                        App_Code.UtilityFunc.Add("处理订单失败：" + out_trade_no + " Error:" + trade_status);
                    }

                    return Content("success");  //请不要修改或删除
                }
                else//验证失败
                {
                    return Content("fail");
                }
            }
            else
            {
                return Content("无通知参数");
            }
        }

        public ActionResult AlipayReturn()
        {
            App_Code.UtilityFunc.Add("------------------------------------\r\nCall AlipayReturn");
            SortedDictionary<string, string> sPara = GetRequestPost("get");

            var RequestColl = Request.QueryString;

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                Notify aliNotify = new Notify();
                bool verifyResult = aliNotify.Verify(sPara, RequestColl["notify_id"], RequestColl["sign"]);
                string trade_status = RequestColl["trade_status"];

                App_Code.UtilityFunc.Add("verifyResult:" + verifyResult);
                App_Code.UtilityFunc.Add("trade_status:" + RequestColl["trade_status"]);

                if (verifyResult)//验证成功
                {
                    //商户订单号
                    string out_trade_no = RequestColl["out_trade_no"];
                    //支付宝交易号
                    string trade_no = RequestColl["trade_no"];
                    //支付金额
                    decimal alipayMoney = decimal.Parse(RequestColl["total_fee"]);
                    //支付时间
                    DateTime alipayPayTime = DateTime.Now;

                    if (trade_status == "TRADE_FINISHED" || trade_status == "TRADE_SUCCESS")
                    {
                        var rtn = App_Code.Proxy.OrderProxy.Update_OrderPay(out_trade_no, alipayMoney, WCF.OrderSystem.Contract.Enumerate.PayMoneyType.AliPay, WCF.OrderSystem.Contract.Enumerate.PayMoneyChannel.Web, alipayPayTime, trade_no);
                        if (!rtn.Success)
                        {
                            if (rtn.Code != -70)
                                App_Code.UtilityFunc.Add("处理订单失败：" + out_trade_no + " Error:" + rtn.Message);
                        }
                    }
                    else
                    {
                        App_Code.UtilityFunc.Add("处理订单失败：" + out_trade_no + " Error:" + trade_status);
                    }
                    App_Code.UtilityFunc.Add("PaymentOk");
                    ViewBag.OrderNum = out_trade_no;
                    return View("PaymentOk");
                }
                else//验证失败
                {
                    return Content("fail");
                }
            }
            else
            {
                return Content("无通知参数");
            }
        }

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestPost(string type)
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            if (type == "post")
                coll = Request.Form;
            else
                coll = Request.QueryString;


            String[] requestItem = coll.AllKeys;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();


            for (i = 0; i < requestItem.Length; i++)
            {
                sb.Append(requestItem[i] + ":" + coll[requestItem[i]] + "; ");
                sArray.Add(requestItem[i], coll[requestItem[i]]);
            }
            


            App_Code.UtilityFunc.Add("支付宝返回参数：" + sb.ToString());

            return sArray;
        }
    }
}