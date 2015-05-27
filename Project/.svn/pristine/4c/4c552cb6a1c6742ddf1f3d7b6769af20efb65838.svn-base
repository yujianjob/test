using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LazyAtHome.Web.WeiXin3.Controllers
{
    public class ActivitesController : BaseController
    {
        //
        // GET: /Activites/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FirstFree(string username, string mpno, int? areaid, string address, string couponcode, string openid)
        {
            App_Code.UtilityFunc.AddToFile("username:" + username + " mpno:" + mpno + " areaid:" + areaid + " address:" + address + " couponcode:" + couponcode + " rhopenid:" + openid + " openid:" + _UserOpenID, "RHlog");

            if (string.IsNullOrWhiteSpace(openid) || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(mpno) || string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(couponcode))
                return RedirectToAction("RHError");

            if (couponcode == "used")
                return RedirectToAction("RHUse");

            var user = GetUserInfo(true, true);

            var couponRtn = App_Code.Proxy.UserProxycs.User_Coupon_Bind_Check(user.User.UserInfo.ID, couponcode);
            if (!couponRtn.Success)
            {
                App_Code.UtilityFunc.AddToFile("User_Coupon_Bind_Check: " + couponRtn.Message, "RHlog");
                return RedirectToAction("RHUse");
            }

            if (string.IsNullOrWhiteSpace(user.User.UserInfo.MPNo))
            {
                var userRtn = App_Code.Proxy.UserProxycs.wx_User_Weixin_Bind_Part(_UserOpenID, mpno, username, AppConfig.SiteID, "");
                if (userRtn.Success)
                {
                    user.User = userRtn.OBJ;

                    WCF.UserSystem.Contract.DataContract.User_ConsigneeAddressDC item = new WCF.UserSystem.Contract.DataContract.User_ConsigneeAddressDC();
                    item.UserID = user.User.UserInfo.ID;
                    item.Consignee = username;
                    if (areaid.HasValue)
                        item.DistrictID = areaid.Value;
                    item.Address = address;
                    item.MPNo = mpno;

                    var addRtn = App_Code.Proxy.UserProxycs.User_ConsigneeAddress_ADD(item);
                    if (!addRtn.Success)
                        App_Code.UtilityFunc.AddToFile("User_ConsigneeAddress_ADD Error, " + addRtn.Message, "RHlog");
                    else
                        App_Code.UtilityFunc.AddToFile("User_ConsigneeAddress_ADD ok", "RHlog");
                }
                else
                    App_Code.UtilityFunc.AddToFile("wx_User_Weixin_Bind_Part: " + userRtn.Message, "RHlog");
            }

            if (user.RHCart == null)
                user.RHCart = new Models.RHModel();

            user.RHCart.MPNo = user.User.UserInfo.MPNo;
            user.RHCart.NickName = username;
            user.RHCart.OpenID = openid;
            user.RHCart.Address = address;
            user.RHCart.CouponCode = couponcode;
            if (areaid.HasValue)
                user.RHCart.AreaID = areaid.Value;

            App_Code.UtilityFunc.AddToFile("==================================================FirstFree View=================================================", "RHlog");
            return View();
        }

        public ActionResult FirstFreeConfirm()
        {
            var user = GetUserInfo();
            return View(user.RHCart);
        }

        public JsonResult FirstFreeSubmit()
        {
            var user = GetUserInfo();
            int code = 1;
            string rtnMsg = null;

            WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC GetAddress = null;

            GetAddress = new WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC();
            GetAddress.Consignee = user.RHCart.NickName;
            GetAddress.Mpno = user.RHCart.MPNo;
            GetAddress.Address = user.RHCart.Address;
            GetAddress.CityName = "上海市";
            GetAddress.ProvinceName = "";
            GetAddress.DistrictID = user.RHCart.AreaID;

            List<int> productList = new List<int>();
            foreach (var p in user.RHCart.ProductList)
            {
                for (int i = 0; i < p.Count; i++)
                    productList.Add(p.Product.ID);
            }

            string url = "";
            var rtn = App_Code.Proxy.OrderProxy.Order_Weixin_Create(_UserOpenID, AppConfig.SiteID, productList, user.Cart.ExpressMoney, 0m, 0m, null, GetAddress, GetAddress, user.RHCart.TotalPrice, null, null, true, user.RHCart.OpenID, user.RHCart.CouponCode, WCF.OrderSystem.Contract.Enumerate.Channel.Partner_Ruohai);
            if (rtn.Success)
            {
                string requestUrl = "http://www.campusmart.cn/weixin/freewash?openid=" + rtn.OBJ.UserRemark + "&ordernum=" + rtn.OBJ.OrderNumber;
                string str = App_Code.UtilityFunc.WebGetRequest(requestUrl);
                App_Code.UtilityFunc.AddToFile(requestUrl + str, "RHlog");

                url = Url.Action("PaymentOk", "Cart", new { OrderNum = rtn.OBJ.OrderNumber });
            }
            else
            {
                code = rtn.Code;
                rtnMsg = rtn.Message;
            }

            return Json(new { Code = code, Msg = rtnMsg, TagUrl = url });
        }

        public ActionResult RHError()
        {
            return View();
        }

        public ActionResult RHUse()
        {
            return View();
        }
    }
}