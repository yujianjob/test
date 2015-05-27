using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Senparc.Weixin.MP;
using Senparc.Weixin.MP.MvcExtension;
using Senparc.Weixin.MP.CommonAPIs;
using LazyAtHome.Web.WeiXin4.CouponService;
using LazyAtHome.Web.WeiXin4.MarketService;

using LazyAtHome.Web.WeiXin4.App_Code;
using Senparc.Weixin.HttpUtility;

namespace LazyAtHome.Web.WeiXin4.Controllers
{
    public class MemberController : BaseController
    {

        public ActionResult MobileBindingShow()
        {
            if (Request.UrlReferrer != null)
                HttpContext.Cache[AppConfig.Cache_CallbackUrl + _UserOpenID] = Request.UrlReferrer.ToString();
            return View();
        }

        public JsonResult MobileBinding(string MPNo, string vCode, string inviteCode)
        {
            int code = 0;
            string msg = "/Cart/OneKey";

            var obj = HttpContext.Cache[AppConfig.Cache_SmsCode + MPNo];
            if ((obj != null) && (vCode == obj.ToString()))
            {
                if (string.IsNullOrWhiteSpace(inviteCode))
                    inviteCode = GetCooking("LazywxLinkCode");
                string url = AppConfig.AppService + "/user/isRegisted.do";

                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("mpNo", MPNo);
                var result = UtilityFunc.GetPostResponse<LazyAtHome.Web.WeiXin4.Models.EntityModel.resultEntity<bool>>(url, dic);

                App_Code.UtilityFunc.AddToFile("MobileBinding" + "-" + MPNo, "test");

                if (result.succFlag && !result.data)//用户不存在则领取新用户红包
                {
                    CouponServiceImplService couponService = new CouponServiceImplService();
                    couponService.getNewUserCoupon(MPNo, AppConfig.SRC_WX);
                }

                string url2 = AppConfig.AppService + "/register/registerUser.do";

                Dictionary<string, string> dic2 = new Dictionary<string, string>();
                dic2.Add("mpNo", MPNo);
                dic2.Add("checkCode", vCode);
                dic2.Add("channel", "23");
                dic2.Add("openId", _UserOpenID);

                var result2 = UtilityFunc.GetPostResponse<LazyAtHome.Web.WeiXin4.Models.EntityModel.resultEntity<LazyAtHome.Web.WeiXin4.Models.EntityModel.DataEntity>>(url2, dic2);
                App_Code.UtilityFunc.AddToFile("MobileBinding" + "-" + MPNo, "test");

                if (result2.succFlag && result2.data.resultCode == 0)
                {
                    code = 1;
                    UpdateUserInfo();

                    HttpCookie cook = new HttpCookie("Lazywxlogin4");
                    cook.Expires = DateTime.Now.AddMonths(1);
                    cook.Value = "login";
                    Response.Cookies.Add(cook);

                    var callBack = HttpContext.Cache[AppConfig.Cache_CallbackUrl + _UserOpenID];
                    if (callBack != null)
                    {
                        HttpContext.Cache.Remove(AppConfig.Cache_CallbackUrl + _UserOpenID);
                        msg = callBack.ToString();
                    }
                    else
                        msg = "/Cart/OneKey";
                }
                else
                    msg = result2.msg;
            }
            else
            {
                msg = "验证码错误，请重新输入";
            }

            return Json(new { Code = code, Msg = msg });
        }

        public JsonResult VerifyMobile(string mpno, short check = 0)
        {
            int code = 1;
            string rtnMsg = null;
            bool send = true;

            if (check == 1)
            {
                string url = AppConfig.AppService + "/user/isRegisted.do";
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("mpNo", mpno);

                var result = UtilityFunc.GetPostResponse<LazyAtHome.Web.WeiXin4.Models.EntityModel.resultEntity<bool>>(url, dic);

                App_Code.UtilityFunc.AddToFile(url + "-" + mpno, "test");
                if (result.succFlag == true && result.data == true)//已注册
                {
                    send = false;
                    code = 0;
                    rtnMsg = "手机号已经存在";
                }
            }
            if (send)
            {
                string url = AppConfig.AppService + "/sms/sendSmForVerifyMobliePhone.do";
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("mpNo", mpno);
                var result = UtilityFunc.GetPostResponse<LazyAtHome.Web.WeiXin4.Models.EntityModel.resultEntity<string>>(url, dic);

                App_Code.UtilityFunc.AddToFile(url + "-" + mpno, "test");
                if (result.succFlag == true)
                {
                    string strCode = result.data;
                    HttpContext.Cache[AppConfig.Cache_SmsCode + mpno] = strCode;
                    App_Code.UtilityFunc.AddToFile(strCode, "test");
                }
            }

            return Json(new { Code = code, Msg = rtnMsg });
        }

        public ActionResult MyWallet()
        {
            var user = GetUserInfo();
            if (user == null || !CheckLoginCook())
            {
                HttpContext.Cache[AppConfig.Cache_CallbackUrl + _UserOpenID] = Request.Url.ToString();
                return RedirectToAction("MobileBindingShow", "Member");
            }
            UpdateUserInfo();
            ViewBag.Money = user.Money;
            ViewBag.Bonuses = 0;
            ViewBag.MPNo = user.MpNo;
            CouponServiceImplService couponService = new CouponServiceImplService();

            var rtn = couponService.queryCoupon(user.MpNo);
            if (rtn.resultCode == 0 && rtn.couponSum != null && rtn.couponSum.Where(s => s.type == 2) != null)
            {
                ViewBag.Bonuses = rtn.couponSum.Where(s => s.type == 2).Sum(s => s.denomination);
            }
            return View();
        }

        //public ActionResult MyCoupons()
        //{
        //    var user = GetUserInfo();
        //    if (user == null || !CheckLoginCook())
        //    {
        //        HttpContext.Cache[AppConfig.Cache_CallbackUrl + _UserOpenID] = Request.Url.ToString();
        //        return RedirectToAction("MobileBindingShow", "Member");
        //    }

        //    IList<WCF.UserSystem.Contract.DataContract.User_CouponDC> xlist = null;
        //    var rtnCouponList = App_Code.Proxy.UserProxycs.User_Coupon_SELECT_List(user.User.UserInfo.ID, null, null, WCF.UserSystem.Contract.Enumerate.CouponStatus.Normal, null, null, 1, 20);
        //    if (rtnCouponList.Success)
        //        xlist = rtnCouponList.OBJ.ReturnList;
        //    if (xlist == null)
        //        xlist = new List<WCF.UserSystem.Contract.DataContract.User_CouponDC>();
        //    return View(xlist);
        //}

        //public ActionResult AddCoupon()
        //{
        //    return View();
        //}

        //public JsonResult User_BindCoupon(string couponPass, string vCode = "")
        //{
        //    int code = 0;
        //    string rtnMsg = null;


        //    var obj = HttpContext.Cache[AppConfig.Cache_ValidateCode + _UserOpenID];
        //    if ((obj == null) || (vCode != obj.ToString()))
        //    {
        //        code = -3;
        //        rtnMsg = "验证码输入有误！";
        //        return Json(new { Code = code, Msg = rtnMsg });
        //    }

        //    int couponID = 0;
        //    string couponName = "";
        //    string couponDate = "";


        //    var user = GetUserInfo();

        //    var rtn = App_Code.Proxy.UserProxycs.User_Coupon_Bind(user.User.UserInfo.ID, couponPass.ToUpper());
        //    if (rtn.Success)
        //    {
        //        code = 1;
        //        couponID = rtn.OBJ.ID;
        //        couponName = rtn.OBJ.Name;
        //        couponDate = rtn.OBJ.UseEndDate.ToString("yyyy-MM-dd");
        //    }
        //    else
        //    {
        //        code = rtn.Code;
        //        rtnMsg = rtn.Message;
        //    }

        //    return Json(new { Code = code, Msg = rtnMsg, CouponID = couponID, CouponName = couponName, CouponDate = couponDate });
        //}

        //public ActionResult MyBalance()
        //{
        //    var user = GetUserInfo();
        //    if (user == null || !CheckLoginCook())
        //    {
        //        HttpContext.Cache[AppConfig.Cache_CallbackUrl + _UserOpenID] = Request.Url.ToString();
        //        return RedirectToAction("MobileBindingShow", "Member");
        //    }

        //    ViewBag.Balance = user.Money;

        //    IList<WCF.UserSystem.Contract.DataContract.User_AmountLogDC> xList = null;
        //    var rtnLogList = App_Code.Proxy.UserProxycs.User_AmountLog_SELECT_List(user.User.UserInfo.ID, null, null, null, 1, 20);
        //    if (rtnLogList.Success)
        //        xList = rtnLogList.OBJ.ReturnList;
        //    if (xList == null)
        //        xList = new List<WCF.UserSystem.Contract.DataContract.User_AmountLogDC>();

        //    return View(xList);
        //}

        public ActionResult Logout()
        {
            HttpCookie cook = Request.Cookies.Get("Lazywxlogin4");
            cook.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cook);
            return RedirectToAction("OneKey", "Cart");
        }

        /// <summary>
        /// 系统红包首页
        /// </summary>
        /// <returns></returns>
        public ActionResult SystemBonuses()
        {
            UtilityFunc.AddToFile("系统红包首页SystemBonuses:", "test");

            ViewBag.UserPhone = "";
            ViewBag.Registed = "0";
            var user = GetUserInfo();

            if (user != null)
            {
                ViewBag.UserPhone = user.MpNo;
                ViewBag.Registed = "1";
            }
            return View();
        }

        /// <summary>
        /// 获取市场活动
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMarket(string mpno, string isRegisted)
        {
            UtilityFunc.AddToFile("获取市场活动GetMarket:" + mpno + isRegisted, "test");
            int code = 0;
            string rtnMsg = null;
            MarketingServiceImplService marketService = new MarketingServiceImplService();

            var rtn = marketService.queryMarketing(mpno
                , new queryMarketingEntry[] { new queryMarketingEntry() { key = "registed", value = isRegisted } });
            if (rtn.resultCode == 0 && rtn.marketings != null && rtn.marketings.Length > 0)
            {
                var participate = rtn.marketings.Where(s => s.userMarketingInfo.Where(t => t.key == AppConfig.UM_K_CAN_PARTICIPATE)
                .First().value.ToString() == "True");
                if (participate.Count() > 0)//用户可以参与活动
                {
                    code = 1;
                    rtnMsg = participate.First().marketingId.ToString();
                    var nextActive = participate.First().userMarketingInfo
                        .Where(s => s.key == AppConfig.UM_K_NEXT_ACTIVED);
                    if (nextActive.Count() > 0 && Convert.ToDateTime(nextActive.First().value) > DateTime.MinValue)//用户已经领取过
                    {
                        code = 2;
                        rtnMsg = "/Member/GetSystemBonuses?MarketID=" + participate.First().marketingId + "&Mpno=" + mpno;
                    }
                }
            }
            return Json(new { Code = code, Msg = rtnMsg });
        }

        /// <summary>
        /// 检查手机用户是否存在
        /// </summary>
        /// <returns></returns>
        public JsonResult CheckMPNo(string mpno, string type, string MarketID, string CouponID)
        {
            UtilityFunc.AddToFile("检查手机用户是否存在CheckMPNo:" + mpno + "-" + type, "test");
            int code = 1;
            string rtnMsg = null;
            string url = AppConfig.AppService + "/user/isRegisted.do";

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("mpNo", mpno);
            var result = UtilityFunc.GetPostResponse<LazyAtHome.Web.WeiXin4.Models.EntityModel.resultEntity<bool>>(url, dic);

            if (result.succFlag&&result.data)//手机号已经存在
            {
                string url2 = AppConfig.AppService + "/user/getUserByOpenId.do";

                Dictionary<string, string> dic2 = new Dictionary<string, string>();
                dic.Add("openId", _UserOpenID);
                var result2 = UtilityFunc.GetPostResponse<LazyAtHome.Web.WeiXin4.Models.EntityModel.resultEntity<LazyAtHome.Web.WeiXin4.Models.EntityModel.UserInfoEntity>>(url2, dic2);
                
                if (result2.succFlag&&result2.data!=null)//微信号与手机号已绑定,直接去领红包
                {
                    rtnMsg = type == "1" ? "/Member/GetSystemBonuses?Mpno=" + mpno + "&MarketID=" + MarketID : "/Member/GetShareBonuses?Mpno=" + mpno + "&MarketID=" + MarketID + "&CouponID=" + CouponID;
                }
                else//微信号与手机号没有绑定
                {
                    var rtnBind = App_Code.Proxy.UserProxycs.wx_User_Weixin_BindMPNo(_UserOpenID, mpno, mpno, 1, "");
                    if (rtnBind.Success)//微信号与手机号绑定成功,直接去领红包
                    {
                        rtnMsg = type == "1" ? "/Member/GetSystemBonuses?Mpno=" + mpno + "&MarketID=" + MarketID : "/Member/GetShareBonuses?Mpno=" + mpno + "&MarketID=" + MarketID + "&CouponID=" + CouponID;
                    }
                    else
                    {
                        code = 0;
                        rtnMsg = "系统异常，请稍微再试。";
                    }
                }
            }
            else//手机号不存在,跳转至手机认证页面
            {
                code = 2;
                rtnMsg = type == "1" ? "/Member/GetSystemBonuses?Mpno=" + mpno + "&MarketID=" + MarketID : "/Member/GetShareBonuses?Mpno=" + mpno + "&MarketID=" + MarketID + "&CouponID=" + CouponID; ;
            }
            return Json(new { Code = code, Msg = rtnMsg });
        }

        /// <summary>
        /// 打开系统红包
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSystemBonuses(string MarketID, string Mpno)
        {
            UtilityFunc.AddToFile("打开系统红包GetSystemBonuses:" + MarketID, "test");
            ViewBag.NextActivedTime = 0;
            ViewBag.UserPhone = Mpno;
            ViewBag.Count = 0;
            ViewBag.Coupon = 0;
            ViewBag.CouponID = "";
            ViewBag.appId = AppConfig.WeiXin_AppId;
            ViewBag.MarketID = MarketID;
            ViewBag.BaseUrl = AppConfig.BaseUrl;
            ViewBag.NextActivedDay = "天";
            string registed = "0";
            var user = GetUserInfo();
            if (user != null)
            {
                registed = "1";
            }

            MarketingServiceImplService marketService = new MarketingServiceImplService();

            var rtn = marketService.queryMarketingDetail(Convert.ToInt64(MarketID), true, Mpno
                , new queryMarketingDetailEntry[] { new queryMarketingDetailEntry() { key = "registed", value = registed } });
            if (rtn.userMarketingInfo.Length > 0)
            {
                var timeLeft = rtn.userMarketingInfo.Where(s => s.key == AppConfig.UM_K_TIMES_LEFT);
                if (timeLeft.Count() > 0)
                    ViewBag.Count = Convert.ToInt32(timeLeft.First().value);

                var nextActive = rtn.userMarketingInfo.Where(s => s.key == AppConfig.UM_K_NEXT_ACTIVED);

                if (nextActive.Count() > 0)
                {
                    if (Convert.ToDateTime(nextActive.First().value) > DateTime.MinValue)//用户已经领取过
                    {
                        var dateDiff = (Convert.ToDateTime(nextActive.First().value) - DateTime.Now);
                        if (dateDiff.Days == 0)
                        {
                            ViewBag.NextActivedDay = "小时";
                            ViewBag.NextActivedTime = dateDiff.Hours == 0 ? 1 : dateDiff.Hours;
                        }
                        else
                        {
                            ViewBag.NextActivedTime = dateDiff.Days;
                        }
                    }
                    var couponNo = rtn.userMarketingInfo.Where(s => s.key == AppConfig.UM_K_COUPON_NO);
                    if (couponNo.Count() > 0)
                    {
                        ViewBag.CouponID = couponNo.First().value.ToString();
                    }
                }
                else
                {
                    getCouponEntry isNewUser = new getCouponEntry();
                    isNewUser.key = AppConfig.EXT_KEY_NEWUSER;
                    string url = AppConfig.AppService + "/order/pagingQueryOrder.do";

                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("userId", user.UserId);
                    dic.Add("pageSize", "20");
                    dic.Add("pageNo", "1");

                    var result = UtilityFunc.GetPostResponse<LazyAtHome.Web.WeiXin4.Models.EntityModel.resultEntity<LazyAtHome.Web.WeiXin4.Models.EntityModel.OrderInfoList>>(url, dic);

                    if (result.succFlag&&result.data==null)
                    {
                        isNewUser.value = "1";
                    }
                    else
                    {
                        isNewUser.value = "0";
                    }
                    CouponServiceImplService couponService = new CouponServiceImplService();
                    var cRtn = couponService.getCoupon(user.MpNo, rtn.marketingId
                        , true, AppConfig.SRC_WX, new getCouponEntry[] { new getCouponEntry() { key = AppConfig.EXT_KEY_REGIST, value = AppConfig.EXT_VALUE_REGIST }, isNewUser });
                    if (cRtn.resultCode == 0)
                    {
                        ViewBag.Coupon = cRtn.amount;
                        ViewBag.CouponID = cRtn.couponNo;
                    }
                }
            }


            return View();
        }

        //创建签名
        public JsonResult GetSignature(string noncestr, string timestamp, string url)
        {
            UtilityFunc.AddToFile("创建签名GetSignature:", "test");
            int code = 1;

            var helper = new Senparc.Weixin.MP.Helpers.JSSDKHelper();

            if (!JsApiTicketContainer.CheckRegistered(AppConfig.WeiXin_AppId))
            {
                JsApiTicketContainer.Register(AppConfig.WeiXin_AppId, AppConfig.WeiXin_AppSecret);
            }
            var ticket = JsApiTicketContainer.GetTicket(AppConfig.WeiXin_AppId);

            var signature = helper.GetSignature(ticket, noncestr, timestamp, url);
            UtilityFunc.AddToFile(timestamp + "-" + noncestr + "-" + ticket + "-" + url + "-" + signature, "test");
            return Json(new { Code = code, Msg = signature });
        }

        //分享红包首面
        public ActionResult ShareBonuses(string MarketID, string CouponID)
        {
            UtilityFunc.AddToFile("分享红包首面ShareBonuses:" + MarketID + "-" + CouponID, "test");
            ViewBag.UserPhone = "";
            ViewBag.MarketID = MarketID;
            ViewBag.CouponID = CouponID;
            ViewBag.Registed = "0";
            var user = GetUserInfo();
            if (user != null)
            {
                ViewBag.UserPhone = user.MpNo;
                ViewBag.Registed = "1";
            }
            return View();
        }

        /// <summary>
        /// 打开分享红包
        /// </summary>
        /// <returns></returns>
        public ActionResult GetShareBonuses(string MarketID, string CouponID, string Mpno)
        {
            ViewBag.MarketID = MarketID;
            ViewBag.CouponID = CouponID;
            ViewBag.appId = AppConfig.WeiXin_AppId;
            ViewBag.BaseUrl = AppConfig.BaseUrl;
            ViewBag.UserPhone = Mpno;
            ViewBag.Coupon = 0;
            ViewBag.OverCount = 0;
            ViewBag.Getted = 0;

            string registed = "0";
            var user = GetUserInfo();

            if (user != null)
            {
                registed = "1";
            }
            MarketingServiceImplService marketService = new MarketingServiceImplService();

            var rtn = marketService.queryMarketingDetail(Convert.ToInt64(MarketID), true, user.MpNo
                , new queryMarketingDetailEntry[] { new queryMarketingDetailEntry() { key = "registed", value = registed } });
            if (rtn != null)
            {
                getCouponEntry isNewUser = new getCouponEntry();
                isNewUser.key = AppConfig.EXT_KEY_NEWUSER;
                string url = AppConfig.AppService + "/order/pagingQueryOrder.do";

                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("userId", user.UserId);
                dic.Add("pageSize", "20");
                dic.Add("pageNo", "1");


                var result = UtilityFunc.GetPostResponse<LazyAtHome.Web.WeiXin4.Models.EntityModel.resultEntity<LazyAtHome.Web.WeiXin4.Models.EntityModel.OrderInfoList>>(url, dic);

                if (result.succFlag && result.data == null)
                {
                    isNewUser.value = "1";
                }
                else
                {
                    isNewUser.value = "0";
                }
                CouponServiceImplService couponService = new CouponServiceImplService();
                var cRtn = couponService.getCoupon(user.MpNo, rtn.marketingId
                    , true, AppConfig.SRC_WX, new getCouponEntry[] { 
                            new getCouponEntry() { key = AppConfig.EXT_KEY_REGIST, value = AppConfig.EXT_VALUE_REGIST }
                            ,new getCouponEntry(){key=AppConfig.EXT_KEY_SHARE_ID,value=CouponID}
                            ,isNewUser
                        });
                if (cRtn.resultCode == 0)
                {
                    ViewBag.Coupon = cRtn.amount;
                }
                if (cRtn.resultCode == 3007)//用户领取的分享红包超限
                {
                    ViewBag.OverCount = 1;
                }
                if (cRtn.resultCode == 3010)//已经领取过
                {
                    ViewBag.Getted = 1;
                }
            }


            return View();
        }

        //我的红包列表
        public ActionResult MyBonusesList(string MPNo)
        {
            UtilityFunc.AddToFile("MyBonusesList:", "test");

            CouponServiceImplService couponService = new CouponServiceImplService();

            var rtn = couponService.queryCoupon(MPNo);
            if (rtn.resultCode == 0)
            {
                ViewBag.Bonuses = rtn.coupons.OrderBy(s => s.status);
            }
            return View();
        }

    }
}