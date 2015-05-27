using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LazyAtHome.Web.WashSite.Controllers
{
    public class MemberController : BaseController
    {
        //
        // GET: /Member/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Login(string ReturnUrl)
        {
            if (string.IsNullOrWhiteSpace(ReturnUrl))
                ViewBag.ReturnUrl = "/";
            else
                ViewBag.ReturnUrl = ReturnUrl;

            return View();
        }

        public JsonResult User_Login(string user, string pass, WCF.UserSystem.Contract.Enumerate.LoginType type, string vcode)
        {
            int code = 0;
            string rtnMsg = "";
            if (type == WCF.UserSystem.Contract.Enumerate.LoginType.Weixin)
            {
                string sysCode = Session[AppConfig.Cache_SmsCode + user] as string;

                if (vcode != sysCode)
                {
                    code = 5;
                    rtnMsg = "验证码错误，请重新输入";
                }
                else
                {
                    var rtn = App_Code.Proxy.UserProxycs.Update_UserModifyPass(user, pass);
                    if (!rtn.Success)
                    {
                        code = rtn.Code;
                        rtnMsg = rtn.Message;
                    }
                }
            }

            if (code == 0)
            {
                var rtn = App_Code.Proxy.UserProxycs.User_Login(user, type, pass, Request.ServerVariables["REMOTE_ADDR"]);
                if (rtn == null)
                    rtnMsg = "服务异常，请稍微再试";
                else
                {
                    if (rtn.Success)
                    {
                        System.Web.Security.FormsAuthentication.SetAuthCookie(user, false);
                        Models.User.UserInfo userinfo = (Models.User.UserInfo)Session["UserInfo"];
                        if (userinfo == null)
                        {
                            userinfo = new Models.User.UserInfo();
                            userinfo.Cart = new Models.User.Cart();
                            userinfo.ActiveCart = new Models.User.Cart();
                            Session["UserInfo"] = userinfo;
                        }
                        userinfo.User = rtn.OBJ;
                        code = 1;
                        HttpCookie cook = new HttpCookie("LazywebInfo");
                        cook.Value = userinfo.User.ID.ToString();
                        Response.Cookies.Add(cook);
                    }
                    else
                    {
                        code = rtn.Code;
                        rtnMsg = rtn.Message;
                    }
                }
            }

            return Json(new { Code = code, Msg = rtnMsg });
        }

        public JsonResult User_ChangePass(string mpno, string pass, string vcode)
        {
            int code = 0;
            string rtnMsg = "";
            string sysCode = Session[AppConfig.Cache_SmsCode + mpno] as string;

            if (vcode != sysCode)
            {
                code = 5;
                rtnMsg = "验证码错误，请重新输入";
            }
            else
            {
                var rtn = App_Code.Proxy.UserProxycs.Update_UserModifyPass(mpno, pass);
                if (!rtn.Success)
                {
                    code = rtn.Code;
                    rtnMsg = rtn.Message;
                }
                else
                    code = 1;
            }

            return Json(new { Code = code, Msg = rtnMsg });
        }

        public ActionResult Loginout()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("index", "home");
        }

        public ActionResult Reg()
        {
            return View();
        }

        /// <summary>
        /// 我的订单
        /// </summary>
        /// <param name="ot"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult MyOrders(int? ot)
        {
            ViewBag.OrderType = 1;
            if (ot.HasValue)
                ViewBag.OrderType = ot.Value;

            var user = GetUserInfo();
            IList<Models.Wash.OrderViewModel> xList = null;

            WCF.OrderSystem.Contract.Enumerate.OrderStatus? status = null;
            if (ViewBag.OrderType == 2)
                status = WCF.OrderSystem.Contract.Enumerate.OrderStatus.UnPay;
            else if (ViewBag.OrderType == 3)
                status = WCF.OrderSystem.Contract.Enumerate.OrderStatus.WaitExpress;

            var rtn = App_Code.Proxy.OrderProxy.Select_OrderList(user.User.ID, status, null, null, 1, 20);
            if (rtn.Success)
                xList = ConvertOrderList(rtn.OBJ.ReturnList);
            if (xList == null)
                xList = new List<Models.Wash.OrderViewModel>();

            return View(xList);
        }

        [Authorize]
        public ActionResult OrderDetail(string orderNum)
        {
            WCF.OrderSystem.Contract.DataContract.Order_OrderDC orderItem = null;
            var rtn = App_Code.Proxy.OrderProxy.Select_OrderEntity(orderNum, true, true, true, true, true, true);
            if (rtn.Success)
            {
                orderItem = rtn.OBJ;

                IList<Models.Wash.ProductItem> xList = new List<Models.Wash.ProductItem>();
                if (orderItem.ProductList != null)
                {
                    var plist = orderItem.ProductList.GroupBy(p => p.ProductID);
                    foreach (var product in plist)
                    {
                        Models.Wash.ProductItem pi = new Models.Wash.ProductItem();
                        var pDC = product.First();
                        pi.ProductInfo = pDC;
                        pi.Count = product.Count();
                        xList.Add(pi);
                    }
                }
                ViewBag.ProductList = xList;
            }
            else
                return RedirectToAction("MyOrders");

            return View(orderItem);
        }

        /// <summary>
        /// 账户信息
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult MyInfo()
        {
            var user = GetUserInfo();
            if (user.UserDetail == null)
            {
                var rtn = App_Code.Proxy.UserProxycs.Select_UserDetail(user.User.ID);
                if (rtn.Success)
                    user.UserDetail = rtn.OBJ;
            }

            ViewBag.LoginName = string.IsNullOrEmpty(user.User.MPNo) ? user.User.Email : user.User.MPNo;
            ViewBag.NickName = user.UserDetail.NickName;
            ViewBag.MPNo = user.User.MPNo;
            ViewBag.Email = user.User.Email;
            ViewBag.RealName = user.UserDetail.RealName;
            ViewBag.IDCard = user.UserDetail.IDCard;
            ViewBag.Address = user.UserDetail.Location;
            ViewBag.Sex = user.UserDetail.Sex;
            ViewBag.DistrictID = user.UserDetail.DistrictID == null ? 1 : user.UserDetail.DistrictID.Value;
            return View();
        }

        public JsonResult SaveMyInfo()
        {
            int code = 0;
            string rtnMsg = null;
            WCF.UserSystem.Contract.DataContract.User_CardDC cardInfo = new WCF.UserSystem.Contract.DataContract.User_CardDC();

            var user = GetUserInfo();


            return Json(new { Code = code, Msg = rtnMsg });
        }

        public JsonResult VerifyMobile(string mpno, short check = 1)
        {
            int code = 1;
            string rtnMsg = null;
            bool send = true;

            if (check == 1)
            {
                var rtn = App_Code.Proxy.UserProxycs.User_Exist_Check(mpno, WCF.UserSystem.Contract.Enumerate.LoginType.MPNo);
                if (!rtn.Success)
                {
                    send = false;
                    code = 0;
                    rtnMsg = "手机号已经存在";
                }
            }
            if (send)
            {
                Random rnd = new Random();
                string strCode = rnd.Next(0, 999999).ToString().PadLeft(6, '0');
                Session[AppConfig.Cache_SmsCode + mpno] = strCode;
                WCF.Common.Contract.ClientProxy.SmsClient.Base_SmsSend_Create(mpno, "尊敬的用户您好，为了保证您的安全，本次操作您的验证码为" + strCode + "，请您在5分钟之内完成验证。");
            }

            return Json(new { Code = code, Msg = rtnMsg });
        }

        /// <summary>
        /// 我的懒人卡
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult LazyCard()
        {
            var user = GetUserInfo();
            var rtnCardList = App_Code.Proxy.UserProxycs.Select_UserCardList(user.User.ID);
            if (rtnCardList.Success)
                ViewBag.CardList = rtnCardList.OBJ;

            return View();
        }

        /// <summary>
        /// 反洗/退换货
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult fxthh()
        {
            var user = GetUserInfo();
            IList<Models.Wash.OrderViewModel> xList = null;

            var rtn = App_Code.Proxy.OrderProxy.Select_OrderList(user.User.ID, WCF.OrderSystem.Contract.Enumerate.OrderStatus.UserChannel, null, null, 1, 20);
            if (rtn.Success)
                xList = ConvertOrderList(rtn.OBJ.ReturnList);
            if (xList == null)
                xList = new List<Models.Wash.OrderViewModel>();

            return View(xList);
        }

        /// <summary>
        /// 取消订单记录
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult OrderCancelRecord()
        {
            var user = GetUserInfo();
            IList<Models.Wash.OrderViewModel> xList = null;

            var rtn = App_Code.Proxy.OrderProxy.Select_OrderList(user.User.ID, WCF.OrderSystem.Contract.Enumerate.OrderStatus.UserChannel, null, null, 1, 20);
            if (rtn.Success)
                xList = ConvertOrderList(rtn.OBJ.ReturnList);
            if (xList == null)
                xList = new List<Models.Wash.OrderViewModel>();

            return View(xList);
        }

        /// <summary>
        /// 我的投诉
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Complaint()
        {
            return View();
        }

        /// <summary>
        /// 账户安全
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult AccountSafe()
        {
            return View();
        }

        /// <summary>
        /// 账户余额
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Balance(int? type)
        {
            if (type == null)
                type = 1;
            DateTime? beginTime = null;
            DateTime? endTime = null;
            ViewBag.Type = type;

            if (type == 1)
            {
                beginTime = DateTime.Parse(DateTime.Now.AddMonths(-3).ToShortDateString());
            }
            else if (type == 2)
            {
                endTime = DateTime.Parse(DateTime.Now.AddMonths(-3).ToShortDateString());
            }

            IList<WCF.UserSystem.Contract.DataContract.User_AmountLogDC> xList = null;
            var user = GetUserInfo();
            var rtn = App_Code.Proxy.UserProxycs.Select_UserAmountLog(user.User.ID, null, beginTime, endTime, 1, 20);
            if (rtn.Success)
            {
                xList = rtn.OBJ.ReturnList;
            }
            if (xList == null)
                xList = new List<WCF.UserSystem.Contract.DataContract.User_AmountLogDC>();

            ViewBag.Money = user.User.Money;
            return View(xList);
        }

        /// <summary>
        /// 消费记录
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Record()
        {
            return View();
        }

        /// <summary>
        /// 我的积分
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Integral()
        {
            return View();
        }

        /// <summary>
        /// 我的优惠劵
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult MyCoupon(int? cs)
        {               
            WCF.UserSystem.Contract.Enumerate.CouponStatus couponStatus = WCF.UserSystem.Contract.Enumerate.CouponStatus.Normal;
            if (cs.HasValue)
            {
                if (cs == 2)
                    couponStatus = WCF.UserSystem.Contract.Enumerate.CouponStatus.Used;
                else if (cs == -1)
                    couponStatus = WCF.UserSystem.Contract.Enumerate.CouponStatus.Expired;
            }

            ViewBag.Status = (int)couponStatus;
            IList<WCF.UserSystem.Contract.DataContract.User_CouponDC> xlist = null;
            var user = GetUserInfo();
            var rtnCouponList = App_Code.Proxy.UserProxycs.User_Coupon_SELECT_List(user.User.ID, null, null, couponStatus, null, null, 1, 20);
            if (rtnCouponList.Success)
                xlist = rtnCouponList.OBJ.ReturnList;
            if (xlist == null)
                xlist = new List<WCF.UserSystem.Contract.DataContract.User_CouponDC>();
            return View(xlist);
        }

        /// <summary>
        /// 收货地址
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult ReceiptGoods()
        {
            var user = GetUserInfo();
            IList<WCF.UserSystem.Contract.DataContract.User_ConsigneeAddressDC> xList = null;
            var rtn = App_Code.Proxy.UserProxycs.Select_UserAddress(user.User.ID);
            if (rtn.Success)
            {
                xList = rtn.OBJ;
            }
            if (xList == null)
                xList = new List<WCF.UserSystem.Contract.DataContract.User_ConsigneeAddressDC>();
            return View(xList);
        }

        public ActionResult AddressDel(int id)
        {
            App_Code.Proxy.UserProxycs.Delete_UserAddress(id);
            return RedirectToAction("ReceiptGoods");
        }

        /// <summary>
        /// 站内信
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Letter()
        {
            IList<WCF.UserSystem.Contract.DataContract.User_Message_PrivateDC> xList = null;
            var user = GetUserInfo();
            var rtn = App_Code.Proxy.UserProxycs.Select_UserPrivateMessage(user.User.ID, 1, 20);
            if (rtn.Success)
                xList = rtn.OBJ.ReturnList;
            if (xList == null)
                xList = new List<WCF.UserSystem.Contract.DataContract.User_Message_PrivateDC>();
            return View(xList);
        }

        public ActionResult Password()
        {
            return View();
        }

        public ActionResult Password2()
        {
            return View();
        }

        public ActionResult Password3()
        {
            return View();
        }

        public ActionResult Password4()
        {
            return View();
        }

        public JsonResult User_Exist_Check(string user, WCF.UserSystem.Contract.Enumerate.LoginType type)
        {
            int code = 0;
            string rtnMsg = "";
            var rtn = App_Code.Proxy.UserProxycs.User_Exist_Check(user, type);
            if (rtn == null)
                rtnMsg = "用户名检查失败";
            else
            {
                if (rtn.Success && rtn.OBJ)
                {
                    code = 1;
                    rtnMsg = "用户名可用";
                }
                else
                {
                    code = rtn.Code;
                    rtnMsg = rtn.Message;
                }
            }

            return Json(new { Code = code, Msg = rtnMsg });
        }

        public JsonResult User_UpdateInfo(string txtNickName, int rbSex, string txtMPNo, string txtEmail, string txtRealName, string txtIDCard, int? txtDistrictID, string txtAddress, string txtVCode)
        {

            int code = 0;
            string rtnMsg = "";

            var user = GetUserInfo();

            if (user.User == null)
            {
                code = -99;
                rtnMsg = "请重新登录！";
            }

            if (user.User.MPNo != txtMPNo)
            {
                string sCode = Session[AppConfig.Cache_SmsCode + txtMPNo] as string;
                if (txtVCode != sCode)
                {
                    code = 5;
                    rtnMsg = "验证码错误，请重新输入";
                }
            }

            if (code == 0)
            {
                var rtn = App_Code.Proxy.UserProxycs.Update_Userinfo(user.User.ID, txtNickName, rbSex, txtMPNo, txtEmail, txtRealName, txtIDCard, txtDistrictID, txtAddress);
                if (rtn == null)
                    rtnMsg = "用户注册失败，服务器异常";
                else
                {
                    if (rtn.Success)
                    {
                        code = 1;
                        UpdateUserInfo();
                    }
                    else
                    {
                        code = rtn.Code;
                        rtnMsg = rtn.Message;
                    }
                }
            }

            return Json(new { Code = code, Msg = rtnMsg });
        }

        public JsonResult User_Reg(string user, string pass, WCF.UserSystem.Contract.Enumerate.LoginType type, string vcode)
        {
            int code = 0;
            string rtnMsg = "";
            string sCode = null;
            if (type == WCF.UserSystem.Contract.Enumerate.LoginType.MPNo)
                sCode = Session[AppConfig.Cache_SmsCode + user] as string;
            else if (type== WCF.UserSystem.Contract.Enumerate.LoginType.Email)
                sCode = Session[AppConfig.Cache_ValidateCode] as string;

            if (vcode != sCode)
            {
                code = 5;
                rtnMsg = "验证码错误，请重新输入";
            }
            else
            {
                string invitecode = null;
                if (Session["invitecode"] != null)
                    invitecode = Session["invitecode"].ToString();

                var rtn = App_Code.Proxy.UserProxycs.User_Reg(user, type, pass, invitecode, Request.UserHostAddress, AppConfig.SiteID);
                if (rtn == null)
                    rtnMsg = "用户注册失败，服务器异常";
                else
                {
                    if (rtn.Success)
                    {
                        System.Web.Security.FormsAuthentication.SetAuthCookie(user, false);

                        Models.User.UserInfo userinfo = (Models.User.UserInfo)Session["UserInfo"];
                        if (userinfo == null)
                        {
                            userinfo = new Models.User.UserInfo();
                            userinfo.Cart = new Models.User.Cart();
                            userinfo.ActiveCart = new Models.User.Cart();
                            Session["UserInfo"] = userinfo;
                        }
                        userinfo.User = rtn.OBJ;

                        code = 1;
                    }
                    else
                    {
                        code = rtn.Code;
                        rtnMsg = rtn.Message;
                    }
                }
            }

            return Json(new { Code = code, Msg = rtnMsg });
        }

        public JsonResult User_AddressAdd(string consignee, int districtID, string address, string mpno, string zipcode, short isdefault)
        {
            int code = 0;
            string rtnMsg = "";

            var user = GetUserInfo();
            WCF.UserSystem.Contract.DataContract.User_ConsigneeAddressDC item = new WCF.UserSystem.Contract.DataContract.User_ConsigneeAddressDC();
            item.UserID = user.User.ID;
            item.Consignee = consignee;
            item.DistrictID = districtID;
            item.Address = address;
            item.MPNo = mpno;
            item.IsDefault = isdefault;
            item.ZipCode = zipcode;

            var rtn = App_Code.Proxy.UserProxycs.Add_UserAddress(item);
            if (rtn.Success)
            {
                code = 1;
                rtnMsg = rtn.OBJ.ToString();
            }
            else
            {
                code = rtn.Code;
                rtnMsg = rtn.Message;
            }

            return Json(new { Code = code, Msg = rtnMsg });
        }

        public JsonResult User_BindCard(string cardPass)
        {
            int code = 0;
            string rtnMsg = null;
            WCF.UserSystem.Contract.DataContract.User_CardDC cardInfo = new WCF.UserSystem.Contract.DataContract.User_CardDC();

            var user = GetUserInfo();

            var rtn = App_Code.Proxy.UserProxycs.Add_UserBindCard(user.User.ID, WCF.UserSystem.Contract.Enumerate.UserCardType.LazyCard, cardPass.ToUpper());
            if (rtn.Success)
            {
                code = 1;
                cardInfo = rtn.OBJ;
            }
            else
            {
                code = rtn.Code;
                rtnMsg = rtn.Message;
            }

            return Json(new { Code = code, Msg = rtnMsg, CardID = cardInfo.ID, Number = cardInfo.Number, Money = cardInfo.Money, Balance = cardInfo.Balance, ExpiryDate = cardInfo.ExpiryDate.ToString("yyyy-MM-dd"), CardType = "懒人卡" });
        }

        public JsonResult CalcuPayMoney(int payType, int cardid, int opera, int source = 0)
        {
            int code = 1;
            string rtnMsg = null;

            var u = GetUserInfo();
            var user = u.User;

            Models.User.Cart cart = null;
            if (source == 1)
                cart = u.ActiveCart;
            else
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
                        cart.UseCard.Remove(cardid);
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
            if (user.Level == (int)WCF.UserSystem.Contract.Enumerate.UserLevel.CharterMembers)
                cart.ExpressEnable = true;
            else
                cart.ExpressEnable = false;
           
            cart.PayPrice = cart.TotalPrice;            
            cart.UseCardPrice = 0;
            cart.UseBalance = 0;
            cart.UseCouponPrice = 0;
            if (cart.SalePrice != 0)
                cart.PayPrice -= cart.SalePrice;

            int couponID = 0;
            //使用优惠券抵扣
            if (cart.UseCoupon.Count > 0)
            {
                var userCouponList = App_Code.Proxy.UserProxycs.User_Coupon_SELECT_List(user.ID, null, null, WCF.UserSystem.Contract.Enumerate.CouponStatus.Normal, null, null, 1, 20);
                if (userCouponList.Success)
                {
                    var cList = cart.UseCoupon.ToList();
                    foreach (var item in cList)
                    {
                        if (cart.PayPrice == 0)
                        {
                            cart.UseCoupon.Remove(item.Key);
                            continue;
                        }
                        var couponItem = userCouponList.OBJ.ReturnList.FirstOrDefault(p => p.ID == item.Key);
                        if (couponItem != null)
                        {
                            cart.UseCoupon[item.Key] = couponItem.FaceValue;
                            if (cart.PayPrice > couponItem.FaceValue)
                            {
                                cart.UseCouponPrice += couponItem.FaceValue;
                                cart.PayPrice -= couponItem.FaceValue;
                            }
                            else
                            {
                                cart.UseCouponPrice += cart.PayPrice;
                                cart.PayPrice = 0;
                            }
                            couponID = item.Key;
                        }
                    }
                }
            }

            //创始会员8折优惠
            if (user.Level == (int)WCF.UserSystem.Contract.Enumerate.UserLevel.CharterMembers)
            {
                cart.CharterSale = Math.Round(cart.PayPrice * 0.2m, 1, MidpointRounding.AwayFromZero);
                cart.PayPrice -= cart.CharterSale;
            }

            //进行懒人卡抵扣计算
            if (cart.UseCard.Count > 0)
            {
                var userCardList = App_Code.Proxy.UserProxycs.Select_UserCardList(user.ID);
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
                            if (cart.PayPrice != 0)
                            {
                                if (cardItem.Balance > cart.PayPrice)
                                {
                                    tmpPrice = cart.PayPrice;
                                    cart.PayPrice = 0;
                                }
                                else
                                {
                                    tmpPrice = cardItem.Balance;
                                    cart.PayPrice -= tmpPrice;
                                }
                                cart.UseCardPrice += tmpPrice;
                                cart.UseCard[pair.Key] = tmpPrice;
                            }
                        }
                    }
                }
            }

            //使用余额抵扣
            if (cart.UseBalanceEnable && cart.PayPrice != 0)
            {
                if (user.Money > cart.PayPrice)
                {
                    cart.UseBalance += cart.PayPrice;
                    cart.PayPrice = 0;
                }
                else
                {
                    cart.UseBalance += user.Money;
                    cart.PayPrice -= user.Money;
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

            return Json(new { Code = code, Msg = rtnMsg, Pay = cart.PayPrice, Balance = cart.UseBalance, CardPay = cart.UseCardPrice, CardList = rtnCardList, CouponPay = cart.UseCouponPrice, CouponID = couponID, CharterSale = cart.CharterSale });
        }

        [Authorize]
        public ActionResult Invite()
        {
            var user = GetUserInfo();            

            var rtn = App_Code.Proxy.UserProxycs.User_Invite_Count(user.User.ID);
            if (rtn.Success)
            {
                ViewBag.Count = rtn.OBJ;
                ViewBag.Money = rtn.OBJ >= 5 ? 20 : 0;
            }

            ViewBag.Code = user.User.RecommendedCode;            
            return View();
        }
    }
}