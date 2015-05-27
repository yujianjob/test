using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LazyAtHome.Web.WeiXin3.Controllers
{
    public class MemberController : BaseController
    {
        //
        // GET: /Member/
        public ActionResult Index()
        {
            var user = GetUserInfo();
            if (user.User == null)
            {
                HttpContext.Cache[AppConfig.Cache_CallbackUrl + _UserOpenID] = Request.Url.ToString();
                return RedirectToAction("MobileBindingShow", "Member");
            }

            if (string.IsNullOrWhiteSpace(user.User.UserInfo.LoginPassword) && string.IsNullOrWhiteSpace(user.User.UserInfo.MPNo))
            {
                return RedirectToAction("RegPortal");
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

        public ActionResult MobileBindingShow()
        {
            if (Request.UrlReferrer != null)
                HttpContext.Cache[AppConfig.Cache_CallbackUrl + _UserOpenID] = Request.UrlReferrer.ToString();
            return View();
        }

        public JsonResult MobileBinding(string MPNo, string vCode, string inviteCode)
        {
            int code = 0;
            string msg = "/Wash";

            var obj = HttpContext.Cache[AppConfig.Cache_SmsCode + MPNo];
            if ((obj != null) && (vCode == obj.ToString()))
            {
                if (string.IsNullOrWhiteSpace(inviteCode))
                    inviteCode = GetCooking("LazywxLinkCode");

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
                    else
                        msg = "/Wash";
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

        public ActionResult AddressManage()
        {
            var user = GetUserInfo();
            if (user.User == null)
            {
                HttpContext.Cache[AppConfig.Cache_CallbackUrl + _UserOpenID] = Request.Url.ToString();
                return RedirectToAction("MobileBindingShow", "Member");
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

        public JsonResult GetUserLocation()
        {
            //return GetBaiduLocation();
            //return GetSogouLocation();
            return GetQQLocation();
        }

        public JsonResult GetBaiduLocation()
        {
            int code = 1;
            string msg = "";
            string address = "";

            var objLocation = HttpContext.Cache[AppConfig.Cache_Location + _UserOpenID];
            if (objLocation != null)
            {
                string requestUrl = "http://api.map.baidu.com/geocoder/v2/?ak=" + AppConfig.Baidu_AK + "&output=xml&location=" + objLocation.ToString() + "&coordtype=wgs84ll";
                App_Code.UtilityFunc.Add(requestUrl);
                string strXml = App_Code.UtilityFunc.WebGetRequest(requestUrl);

                try
                {
                    System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                    xmlDoc.LoadXml(strXml);
                    System.Xml.XmlNode node = xmlDoc.DocumentElement.SelectNodes("/GeocoderSearchResponse/result/formatted_address")[0];
                    address = node.InnerText;
                }
                catch (Exception ex)
                {
                    App_Code.UtilityFunc.Add(ex.Message);
                    code = -1;
                    msg = "获取当前位置失败";
                }
            }
            else
            {
                code = -2;
                msg = "未获取到当前位置";
            }

            return Json(new { Code = code, Msg = msg, Address = address });
        }

        public JsonResult GetQQLocation()
        {
            int code = 1;
            string msg = "";
            string address = "";

            var objLocation = HttpContext.Cache[AppConfig.Cache_Location + _UserOpenID];
            if (objLocation != null)
            {
                string requestUrl = "http://apis.map.qq.com/ws/geocoder/v1?coord_type=1&key=O3MBZ-TRCW5-XGUIJ-QHO5L-MVOGH-YQF72&location=" + objLocation.ToString();
                App_Code.UtilityFunc.Add(requestUrl);
                string strJson = App_Code.UtilityFunc.WebGetRequest(requestUrl);

                try
                {
                    Models.QQLocationEntity location = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.QQLocationEntity>(strJson);
                    if (location.status == 0)
                    {
                        address = location.result.address_component.city + location.result.address_component.district + location.result.address_component.street_number;
                        if (location.result.address_reference != null && location.result.address_reference.landmark_l2 != null)
                            address += location.result.address_reference.landmark_l2.title;
                    }
                    else
                    {
                        code = -1;
                        msg = "获取当前位置失败";
                    }
                    
                }
                catch (Exception ex)
                {
                    App_Code.UtilityFunc.Add(ex.Message);
                    code = -1;
                    msg = "获取当前位置失败";
                }
            }
            else
            {
                code = -2;
                msg = "未获取到当前位置";
            }

            return Json(new { Code = code, Msg = msg, Address = address });
        }

        public JsonResult GetSogouLocation()
        {
            int code = 1;
            string msg = "";
            string address = "";

            var objLocation = HttpContext.Cache[AppConfig.Cache_Location + _UserOpenID];
            if (objLocation != null)
            {
                string requestUrl = "http://api.go2map.com/engine/api/regeocoder/xml?points=" + objLocation.ToString() + "&type=1&contenttype=utf8";
                App_Code.UtilityFunc.Add(requestUrl);
                string strXml = App_Code.UtilityFunc.WebGetRequest(requestUrl);

                try
                {
                    System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                    xmlDoc.LoadXml(strXml);
                    System.Xml.XmlNode node = xmlDoc.DocumentElement.SelectNodes("/xml/response/data/province")[0];//省
                    address = node.InnerText;
                    node = xmlDoc.DocumentElement.SelectNodes("/xml/response/data/city")[0];//市
                    address += node.InnerText;
                    node = xmlDoc.DocumentElement.SelectNodes("/xml/response/data/district")[0];//区
                    address += node.InnerText;
                    node = xmlDoc.DocumentElement.SelectNodes("/xml/response/data/address")[0];//地址
                    address += node.InnerText;
                }
                catch (Exception ex)
                {
                    App_Code.UtilityFunc.Add(ex.Message);
                    code = -1;
                    msg = "获取当前位置失败";
                }
            }
            else
            {
                code = -2;
                msg = "未获取到当前位置";
            }

            return Json(new { Code = code, Msg = msg, Address = address });
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
            {
                item = new WCF.UserSystem.Contract.DataContract.User_ConsigneeAddressDC();
                item.ID = 0;
                item.DistrictID = 310113;
                item.Address = string.Empty;
                item.MPNo = string.Empty;
                item.Consignee = string.Empty;
            }

            if (Request.UrlReferrer != null)
                HttpContext.Cache[AppConfig.Cache_CallbackUrl + _UserOpenID] = Request.UrlReferrer.ToString();

            return View(item);
        }

        public ActionResult MyOrders(string oid)
        {
            var user = GetUserInfo();
            if (user.User == null)
            {
                HttpContext.Cache[AppConfig.Cache_CallbackUrl + _UserOpenID] = Request.Url.ToString();
                return RedirectToAction("MobileBindingShow", "Member");
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
                ViewBag.SendTime = ShowOrder.ExpectTime.Value.AddHours(72);
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
                else if (hour > 18)
                {
                    hour = 10;
                    minute = 0;
                    day++;
                }
                else
                    hour++;

                DateTime sDateTime = new DateTime(initTime.Year, initTime.Month, day, hour, minute, 0);
                DateTime eDateTime = sDateTime.AddHours(72);

                ViewBag.GetTime = sDateTime;
                ViewBag.SendTime = eDateTime;
            }

            return View(ShowOrder);
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
                return RedirectToAction("MobileBindingShow", "Member");
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

        public JsonResult User_BindCoupon(string couponPass)
        {
            int code = 0;
            string rtnMsg = null;
            int couponID = 0;
            string couponName = "";
            string couponDate = "";

            var user = GetUserInfo();

            var rtn = App_Code.Proxy.UserProxycs.User_Coupon_Bind(user.User.UserInfo.ID, couponPass.ToUpper());
            if (rtn.Success)
            {
                code = 1;
                couponID = rtn.OBJ.ID;
                couponName = rtn.OBJ.Name;
                couponDate = rtn.OBJ.UseEndDate.ToString("yyyy-MM-dd");
            }
            else
            {
                code = rtn.Code;
                rtnMsg = rtn.Message;
            }

            return Json(new { Code = code, Msg = rtnMsg, CouponID = couponID, CouponName = couponName, CouponDate = couponDate });
        }

        public JsonResult User_RechargeCard(string CardPass)
        {
            int code = 0;
            string rtnMsg = null;
            CardPass = CardPass.Replace("-", "").ToUpper();

            var user = GetUserInfo();

            var rtn = App_Code.Proxy.UserProxycs.User_Card_Bind(user.User.UserInfo.ID, WCF.UserSystem.Contract.Enumerate.UserCardType.RechargeCard, CardPass);
            if (rtn.Success)
            {
                code = 1;
                rtnMsg = "充值完成，金额：￥" + rtn.OBJ.Money + "元";
                UpdateUserInfo();
            }
            else
            {
                code = rtn.Code;
                rtnMsg = rtn.Message;
            }

            return Json(new { Code = code, Msg = rtnMsg });
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

        public ActionResult CommunityCenter()
        {
            var user = GetUserInfo();

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
                var rtn = App_Code.Proxy.UserProxycs.Update_Userinfo(user.User.UserInfo.ID, txtLoginName, string.Empty, txtMPNo, txtEmail);
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

        public ActionResult RegPortal()
        {
            return View();
        }


        public ActionResult Reg()
        {
            return View();
        }

        public JsonResult User_Reg(string loginName, string pass, string email)
        {
            int code = 0;
            string rtnMsg = "";

            var rtn = App_Code.Proxy.UserProxycs.wx_User_Weixin_Reg(_UserOpenID, loginName, pass, email);
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

            return Json(new { Code = code, Msg = rtnMsg });
        }

        public ActionResult Login()
        {
            return View();
        }

        public JsonResult User_Login(string loginName, string pass, string vCode, int loginType)
        {
            int code = 1;
            string rtnMsg = "";

            Core.Infrastructure.WCF.ReturnEntity<WCF.UserSystem.Contract.DataContract.weixin.User_WeixinDC> userRtn = null;
            if (loginType == 1)
            {
                var obj = HttpContext.Cache[AppConfig.Cache_SmsCode + loginName];
                if ((obj != null) && (vCode == obj.ToString()))
                    userRtn = App_Code.Proxy.UserProxycs.wx_User_Weixin_Login(_UserOpenID, loginName);
                else
                {
                    code = 0;
                    rtnMsg = "验证码错误";
                }
            }
            else
                userRtn = App_Code.Proxy.UserProxycs.wx_User_Weixin_Login(_UserOpenID, loginName, pass);
            if (code == 1)
            {
                if (userRtn.Success)
                    UpdateUserInfo();
                else
                {
                    code = userRtn.Code;
                    rtnMsg = userRtn.Message;
                }
            }

            return Json(new { Code = code, Msg = rtnMsg });
        }
    }
}