using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LazyAtHome.Web.WeiXin2.Controllers
{
    public class MemberController : BaseController
    {
        public ActionResult Index()
        {
            var user = GetUserInfo();
            if (user.User == null)
            {
                HttpContext.Cache[AppConfig.Cache_CallbackUrl + _UserOpenID] = Request.Url.ToString();
                return RedirectToAction("Reg", "Member");
            }

            if (user.UserDetail == null)
            {
                var rtn = App_Code.Proxy.UserProxycs.Select_UserDetail(user.User.UserInfo.ID);
                if (rtn.Success)
                    user.UserDetail = rtn.OBJ;
            }

            ViewBag.LoginName = user.User.UserInfo.LoginName;
            ViewBag.NickName = user.UserDetail.NickName;
            ViewBag.MPNo = user.User.UserInfo.MPNo;
            ViewBag.Email = user.User.UserInfo.Email;
            ViewBag.Money = user.User.UserInfo.Money;

            return View();
        }

        public ActionResult MyOrders(string oid)
        {
            var user = GetUserInfo();
            if (user.User == null)
            {
                HttpContext.Cache[AppConfig.Cache_CallbackUrl + _UserOpenID] = Request.Url.ToString();
                return RedirectToAction("Reg", "Member");
            }

            IList<WCF.OrderSystem.Contract.DataContract.Order_OrderDC> xList = null;
            
            var rtn = App_Code.Proxy.OrderProxy.Order_SELECT_List(user.User.UserInfo.ID, WCF.OrderSystem.Contract.Enumerate.OrderStatus.UnFinish, null, null, 1, 10);
            if (rtn.Success)
                xList = rtn.OBJ.ReturnList;
            if (xList == null)
                xList = new List<WCF.OrderSystem.Contract.DataContract.Order_OrderDC>();

            if (xList.Count == 0)
            {
                return View();
            }

            ViewBag.OrderList = xList;
            WCF.OrderSystem.Contract.DataContract.Order_OrderDC ShowOrder = null;

            if (string.IsNullOrWhiteSpace(oid))
                oid = xList[0].OrderNumber;


            var orderRtn = App_Code.Proxy.OrderProxy.Select_OrderEntity(oid, true, true, true, true, true, false);
            if (orderRtn.Success)
                ShowOrder = orderRtn.OBJ;
            else
            {
                if (xList[0].OrderNumber != oid)
                {
                    orderRtn = App_Code.Proxy.OrderProxy.Select_OrderEntity(oid, true, true, true, true, true, false);
                    if (orderRtn.Success)
                        ShowOrder = orderRtn.OBJ;
                    else
                    { }
                }
            }

            IList<Models.ProductItemModel> piList = new List<Models.ProductItemModel>();
            if (ShowOrder.ProductList != null)
            {
                var plist = ShowOrder.ProductList.GroupBy(p => p.ProductID);
                foreach (var product in plist)
                {
                    Models.ProductItemModel pi = new Models.ProductItemModel();
                    var pDC = product.First();
                    pi.ProductInfo = pDC;
                    pi.Count = product.Count();
                    piList.Add(pi);
                }
            }
            ViewBag.ProductList = piList;

            if (ShowOrder.ExpectTime != null)
            {
                ViewBag.GetTime = ShowOrder.ExpectTime.Value;
                ViewBag.SendTime = ShowOrder.ExpectTime.Value.AddHours(48);
            }
            else
            {
                DateTime initTime = ShowOrder.Obj_Cdate.Value;
                if (ShowOrder.AllFinishTime != null)
                    initTime = ShowOrder.AllFinishTime.Value;

                int day = initTime.Day;
                int hour = initTime.Hour;
                int minute = 0;
                if (initTime.Minute < 30)
                    minute = 30;
                else
                {
                    hour += 1;
                }

                if (hour < 10)
                {
                    hour = 10;
                    minute = 0;
                }
                else if (hour > 19)
                {
                    hour = 10;
                    minute = 0;
                    day++;
                }
                else
                    hour++;

                DateTime sDateTime = new DateTime(initTime.Year, initTime.Month, day, hour, minute, 0);
                DateTime eDateTime = sDateTime.AddHours(48);

                ViewBag.GetTime = sDateTime;
                ViewBag.SendTime = eDateTime;
            }

            return View(ShowOrder);
        }

        public ActionResult OrderDetail(string orderNum)
        {
            WCF.OrderSystem.Contract.DataContract.Order_OrderDC orderItem = null;
            var rtn = App_Code.Proxy.OrderProxy.Select_OrderEntity(orderNum, true, true, true, true, true, true);
            if (rtn.Success)
            {
                orderItem = rtn.OBJ;

                IList<Models.ProductItemModel> xList = new List<Models.ProductItemModel>();
                if (orderItem.ProductList != null)
                {
                    var plist = orderItem.ProductList.GroupBy(p => p.ProductID);
                    foreach (var product in plist)
                    {
                        Models.ProductItemModel pi = new Models.ProductItemModel();
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

        public ActionResult AddressManage()
        {
            var user = GetUserInfo();

            if (user.User == null)
            {
                HttpContext.Cache[AppConfig.Cache_CallbackUrl + _UserOpenID] = Request.Url.ToString();
                return RedirectToAction("Reg", "Member");
            }

            IList<WCF.UserSystem.Contract.DataContract.User_ConsigneeAddressDC> xList = null;
            var rtn = App_Code.Proxy.UserProxycs.Select_UserAddress(user.User.UserInfo.ID);
            if (rtn.Success)
            {
                xList = rtn.OBJ;
            }
            if (xList == null)
                xList = new List<WCF.UserSystem.Contract.DataContract.User_ConsigneeAddressDC>();
            return View(xList);
        }

        public ActionResult AddressModify(int? id)
        {
            WCF.UserSystem.Contract.DataContract.User_ConsigneeAddressDC item = null;
            if (id.HasValue)
            {
                var rtn = App_Code.Proxy.UserProxycs.User_ConsigneeAddress_SELECT_Entity(id.Value);
                if (rtn.Success)
                    item = rtn.OBJ;
            }
            else
                item = new WCF.UserSystem.Contract.DataContract.User_ConsigneeAddressDC();

            HttpContext.Cache[AppConfig.Cache_CallbackUrl + _UserOpenID] = Request.UrlReferrer.ToString();            

            return View(item);
        }

        public JsonResult AddressDel(int id)
        {
            int code = 0;
            string rtnMsg = "";
            var rtn = App_Code.Proxy.UserProxycs.User_ConsigneeAddress_DELETE(id);
            if (rtn.Success)
                code = 1;
            else
            {
                code = rtn.Code;
                rtnMsg = rtn.Message;
            }

            return Json(new { Code = code, Msg = rtnMsg });
        }

        public JsonResult User_AddressUpdate(int id, string consignee, int districtID, string address, string mpno, short isdefault)
        {
            int code = 0;
            string rtnMsg = "";

            var user = GetUserInfo();
            WCF.UserSystem.Contract.DataContract.User_ConsigneeAddressDC item = new WCF.UserSystem.Contract.DataContract.User_ConsigneeAddressDC();
            item.ID = id;
            item.UserID = user.User.UserInfo.ID;
            item.Consignee = consignee;
            item.DistrictID = districtID;
            item.Address = address;
            item.MPNo = mpno;
            item.IsDefault = isdefault;

            if (item.ID == 0)
            {
                var rtn = App_Code.Proxy.UserProxycs.User_ConsigneeAddress_ADD(item);
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
            }
            else
            {
                var rtn = App_Code.Proxy.UserProxycs.User_ConsigneeAddress_UPDATE(item);
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

            }

            string callbackUrl = "";
            if (code == 1 && HttpContext.Cache[AppConfig.Cache_CallbackUrl + _UserOpenID] != null)
                callbackUrl = HttpContext.Cache[AppConfig.Cache_CallbackUrl + _UserOpenID].ToString();

            return Json(new { Code = code, Msg = rtnMsg, CallBack = callbackUrl });
        }

        public ActionResult MyLazyCards()
        {            
            var user = GetUserInfo();
            if (user.User == null)
            {
                HttpContext.Cache[AppConfig.Cache_CallbackUrl + _UserOpenID] = Request.Url.ToString();
                return RedirectToAction("Reg", "Member");
            }

            IList<LazyAtHome.WCF.UserSystem.Contract.DataContract.User_CardDC> xList = null;
            var rtnCardList = App_Code.Proxy.UserProxycs.Select_UserCardList(user.User.UserInfo.ID);
            if (rtnCardList.Success)
                xList = rtnCardList.OBJ;

            if (xList == null)
                xList = new List<LazyAtHome.WCF.UserSystem.Contract.DataContract.User_CardDC>();

            return View(xList);
        }
        /// <summary>
        /// 我的优惠劵
        /// </summary>
        /// <returns></returns>
        public ActionResult MyCoupons(int? cs)
        {
            var user = GetUserInfo();
            if (user.User == null)
            {
                HttpContext.Cache[AppConfig.Cache_CallbackUrl + _UserOpenID] = Request.Url.ToString();
                return RedirectToAction("Reg", "Member");
            }

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
            var rtnCouponList = App_Code.Proxy.UserProxycs.User_Coupon_SELECT_List(user.User.UserInfo.ID, null, null, couponStatus, null, null, 1, 20);
            if (rtnCouponList.Success)
                xlist = rtnCouponList.OBJ.ReturnList;
            if (xlist == null)
                xlist = new List<WCF.UserSystem.Contract.DataContract.User_CouponDC>();
            return View(xlist);
        }

        public ActionResult LazyCardBinding()
        {
            return View();
        }

        public ActionResult HistoryOrder()
        {
            var user = GetUserInfo();
            if (user.User == null)
            {
                HttpContext.Cache[AppConfig.Cache_CallbackUrl + _UserOpenID] = Request.Url.ToString();
                return RedirectToAction("Reg", "Member");
            }

            IList<Models.OrderViewModel> xList = null;
            
            var rtn = App_Code.Proxy.OrderProxy.Order_SELECT_List(user.User.UserInfo.ID, WCF.OrderSystem.Contract.Enumerate.OrderStatus.Finish, null, null, 1, 10);
            if (rtn.Success)
                xList = ConvertOrderList(rtn.OBJ.ReturnList);

            if (xList == null)
                xList = new List<Models.OrderViewModel>();
            return View(xList);
        }

        public ActionResult CommunityCenter()
        {
            return View();
        }

        public ActionResult Reg()
        {
            if (Request.UrlReferrer != null)
                HttpContext.Cache[AppConfig.Cache_CallbackUrl + _UserOpenID] = Request.UrlReferrer.ToString();
            return View();
        }

        public ActionResult Login(string ReturnUrl)
        {
            if (string.IsNullOrWhiteSpace(ReturnUrl))
                ReturnUrl = Request.UrlReferrer.AbsolutePath;

            if (ReturnUrl == "/")
                ReturnUrl = "/Wash";
            ViewBag.ReturnUrl = ReturnUrl;

            return View();
        }

        public JsonResult VerifyMobile(string mpno, short check = 0)
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
                HttpContext.Cache[AppConfig.Cache_SmsCode + mpno] = strCode;
                WCF.Common.Contract.ClientProxy.SmsClient.Base_SmsSend_Create(mpno, "尊敬的用户您好，为了保证您的安全，本次操作您的验证码为" + strCode + "，请您在5分钟之内完成验证。");
            }

            return Json(new { Code = code, Msg = rtnMsg });
        }

        public JsonResult User_UpdateInfo(string txtLoginName, string txtNickName, string txtMPNo, string txtEmail, string txtVCode)
        {

            int code = 0;
            string rtnMsg = "";

            var user = GetUserInfo();

            if (user.User == null)
            {
                code = -99;
                rtnMsg = "请重新登录！";
            }

            if (user.User.UserInfo.MPNo != txtMPNo)
            {
                string sCode = HttpContext.Cache[AppConfig.Cache_SmsCode + txtMPNo] as string;
                if (txtVCode != sCode)
                {
                    code = 5;
                    rtnMsg = "验证码错误，请重新输入";
                }
            }

            if (code == 0)
            {
                var rtn = App_Code.Proxy.UserProxycs.Update_Userinfo(user.User.UserInfo.ID, txtLoginName, txtNickName, txtMPNo, txtEmail);
                if (rtn == null)
                    rtnMsg = "用户信息更新失败，服务器异常";
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

        public JsonResult User_BindCard(string cardPass)
        {
            int code = 0;
            string rtnMsg = null;

            var user = GetUserInfo();

            var rtn = App_Code.Proxy.UserProxycs.Add_UserBindCard(user.User.UserInfo.ID, WCF.UserSystem.Contract.Enumerate.UserCardType.LazyCard, cardPass.ToUpper());
            if (rtn.Success)
                code = 1;
            else
            {
                code = rtn.Code;
                rtnMsg = rtn.Message;
            }
            return Json(new { Code = code, Msg = rtnMsg });
        }

        public JsonResult User_BindCoupon(string couponPass)
        {
            int code = 0;
            string rtnMsg = null;

            var user = GetUserInfo();

            var rtn = App_Code.Proxy.UserProxycs.User_Coupon_Bind(user.User.UserInfo.ID, couponPass.ToUpper());
            if (rtn.Success)
                code = 1;
            else
            {
                code = rtn.Code;
                rtnMsg = rtn.Message;
            }

            return Json(new { Code = code, Msg = rtnMsg });
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

        public JsonResult MobileBinding(string MPNo, string vCode)
        {
            int code = 0;
            string msg = "/Wash";

            var obj = HttpContext.Cache[AppConfig.Cache_SmsCode + MPNo];
            if ((obj != null) && (vCode == obj.ToString()))
            {
                string inviteCode = GetCooking("LazywxLinkCode");

                var rtn = App_Code.Proxy.UserProxycs.wx_User_Weixin_BindMPNo(_UserOpenID, MPNo, MPNo, 1, inviteCode);
                if (rtn.Success)
                {
                    code = 1;
                    UpdateUserInfo();
                    var callBack = HttpContext.Cache[AppConfig.Cache_CallbackUrl + _UserOpenID];
                    if (callBack != null)
                    {
                        HttpContext.Cache.Remove(AppConfig.Cache_CallbackUrl + _UserOpenID);
                        msg = callBack.ToString();
                    }
                }
                else
                    msg = rtn.Message;
            }
            else
            {
                msg = "验证码错误，请重新输入";
            }

            return Json(new { Code = code, Msg = msg });
        }
    }
}