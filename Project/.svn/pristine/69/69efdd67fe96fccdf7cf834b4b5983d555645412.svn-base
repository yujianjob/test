using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using LazyAtHome.Core.Infrastructure.WCF;

namespace LazyAtHome.Web.WeiXin.Controllers
{
    public class PersonalCenterController : BaseController
    {
        public ActionResult MemberCenter()
        {
            var userinfo = GetUserInfo();
            if (userinfo.User == null)
            {
                HttpContext.Cache[CustomerConfig.Cache_CallbackUrl + _UserOpenID] = Request.Url.ToString();
                return RedirectToAction("MobileBindShow", "PersonalCenter");
            }
            else
            {
                ViewBag.UserName = userinfo.User.UserDetail.NickName;
                ViewBag.Money = userinfo.User.UserInfo.Money;
                ViewBag.MPNo = userinfo.User.UserInfo.MPNo;
            }

            return View();
        }

        [NoCacheFilter]
        public ActionResult MyOrders(int? status)
        {
            WCF.OrderSystem.Contract.Enumerate.OrderStatus orderStatus = WCF.OrderSystem.Contract.Enumerate.OrderStatus.UnPay;
            if (status.HasValue)
            {
                if (status.Value == 900)
                    orderStatus = WCF.OrderSystem.Contract.Enumerate.OrderStatus.WaitExpress;
                else if (status.Value == 999)
                    orderStatus = WCF.OrderSystem.Contract.Enumerate.OrderStatus.AllFinish;
            }

            ViewBag.Status = (int)orderStatus;

            List<Models.ViewModel.OrderViewModel> vList = new List<Models.ViewModel.OrderViewModel>();

            var orderList = App_Code.Proxy.OrderProxy.Select_UserOrder(_UserOpenID, orderStatus);
            UtilityFunc.Add("Myorders orderList Get:" + orderList.Success + " msg:" + orderList.Message);

            if (orderList.Success)
            {
                var xList = orderList.OBJ.ReturnList;
                foreach (var orderItem in xList)
                {
                    Models.ViewModel.OrderViewModel orderView = new Models.ViewModel.OrderViewModel();
                    orderView.ID = orderItem.ID;
                    orderView.OrderNum = orderItem.OrderNumber;
                    orderView.ProductList = new List<Models.ShopCart.ProductItem>();
                    decimal totalPrice = 0m;

                    if (orderItem.ProductList != null)
                    {
                        var plist = orderItem.ProductList.GroupBy(p => p.ProductID);
                        foreach (var product in plist)
                        {
                            Models.ShopCart.ProductItem pi = new Models.ShopCart.ProductItem();
                            pi.Count = product.Count();
                            var pDC = product.First();
                            pi.ProductInfo = new WCF.Wash.Contract.DataContract.weixin.wx_Wash_ProductDC();
                            pi.ProductInfo.Name = pDC.Name;
                            pi.ProductInfo.WebName = pDC.WebName;
                            pi.ProductInfo.SalesPrice = pDC.Price;
                            pi.ProductInfo.PictureS = pDC.PictureS;
                            orderView.ProductList.Add(pi);
                            totalPrice += pi.ProductInfo.SalesPrice * pi.Count;
                        }
                        orderView.TotalPrice = totalPrice;
                    }

                    vList.Add(orderView);
                }
            }

            return View(vList);
        }

        public ActionResult MyOrdersCannel(int id)
        {
            App_Code.Proxy.OrderProxy.Update_UserOrderCancel(id, WCF.OrderSystem.Contract.Enumerate.OrderStatus.UserChannel);
            return RedirectToAction("MyOrders");
        }

        [NoCacheFilter]
        public ActionResult OrderDetails(string orderNum)
        {
            if (string.IsNullOrWhiteSpace(orderNum))
                return RedirectToAction("MyOrders");

            LazyAtHome.WCF.OrderSystem.Contract.DataContract.Order_OrderDC order = null;
            var rtn = App_Code.Proxy.OrderProxy.Select_UserOrderDetail(orderNum, true, true, true, true, true, true);
            if (rtn.Success)
                order = rtn.OBJ;
            else
                return RedirectToAction("MyOrders");

            Response.Cache.SetNoStore();
            return View(order);
        }

        [NoCacheFilter]
        public ActionResult MyCard()
        {
            var user = GetUserInfo();

            if (user.User == null)
            {
                HttpContext.Cache[CustomerConfig.Cache_CallbackUrl + _UserOpenID] = Request.Url.ToString();
                return RedirectToAction("MobileBindShow", "PersonalCenter");
            }

            return View(user);
        }

        [ValidateAntiForgeryToken]
        public ActionResult CardBind(string CardPass, string VCode)
        {
            var vc = HttpContext.Cache[CustomerConfig.Cache_ValidateCode + _UserOpenID];
            if (vc == null || VCode != vc.ToString())
            {
                ViewBag.Msg = "0_验证码输入错误，请重新输入";
            }
            else
            {
                CardPass = CardPass.Replace("-", "");
                var rtn = App_Code.Proxy.UserProxy.Update_UserCardBind(_UserOpenID, CardPass);

                if (rtn.Success)
                {
                    ViewBag.Msg = "1_卡绑定成功";
                    CardPass = "";
                    UpdateUserInfo();
                }
                else
                    ViewBag.Msg = "0_" + rtn.Message;
            }

            ViewBag.CardPass = CardPass;
            var user = GetUserInfo();
            return View("MyCard", user);
        }

        public ActionResult Notice()
        {
            var rtn = App_Code.Proxy.UserProxy.Select_User_Notify(_UserOpenID);

            return View(rtn.OBJ.ReturnList);
        }

        public ActionResult NoticeDel(int? id)
        {
            if (id.HasValue)
            {
                App_Code.Proxy.UserProxy.Delete_User_Notify(id.Value);
                UpdateUserInfo();
            }
            return RedirectToAction("Notice");
        }

        [NoCacheFilter]
        public ActionResult Address()
        {
            IList<WCF.UserSystem.Contract.DataContract.User_ConsigneeAddressDC> xList = null;

            var addressList = App_Code.Proxy.UserProxy.Select_UserAddressList(_UserOpenID);
            if (addressList.Success)
                xList = addressList.OBJ;
            else
                xList = new List<WCF.UserSystem.Contract.DataContract.User_ConsigneeAddressDC>();            
            return View(xList);
        }

        public ActionResult AddressDel(int? id)
        {
            if (id.HasValue)
            {
                App_Code.Proxy.UserProxy.Delete_User_Address(id.Value);
                UpdateUserInfo();
            }
            return RedirectToAction("Address");
        }

        public JsonResult AddressDefault(int? id)
        {
            int Code = 0;
            string Msg = "";
            if (id.HasValue)
            {
                var rtn = App_Code.Proxy.UserProxy.Update_User_AddressDefault(id.Value);
                if (rtn.Success)
                    UpdateUserInfo();
                else
                    Code = -1;
            }
            if (Code != 0)
                Msg = "默认地址设置失败，请稍微重试";
            return Json(new { code = Code, msg = Msg });
        }

        [NoCacheFilter]
        public ActionResult ModifyAddress(int? aid, string flag)
        {
            var user = GetUserInfo();

            user.AddressModifyFlag = flag;

            WCF.UserSystem.Contract.DataContract.User_ConsigneeAddressDC address = null;

            var callback = HttpContext.Cache[CustomerConfig.Cache_CallbackUrl + _UserOpenID];
            if (callback == null)
                if (Request.UrlReferrer != null)
                    HttpContext.Cache[CustomerConfig.Cache_CallbackUrl + _UserOpenID] = Request.UrlReferrer.ToString();

            if (aid.HasValue)
            {
                var addressList = App_Code.Proxy.UserProxy.Select_UserAddressList(_UserOpenID);
                if (addressList.Success)
                {
                    UtilityFunc.Add(user.User.UserInfo.MPNo + " Address Count: " + addressList.OBJ.Count);
                    address = addressList.OBJ.First(item => item.ID == aid.Value);
                }
            }
            else
            {
                address = new WCF.UserSystem.Contract.DataContract.User_ConsigneeAddressDC();
                address.Consignee = "收件人姓名";
                if (user.User != null)
                    address.MPNo = user.User.UserInfo.MPNo;
                else
                    address.MPNo = "联系电话";
                address.Address = "详细地址";
                address.IsDefault = 1;
            }
            return View(address);
        }

        [ValidateAntiForgeryToken]
        public ActionResult ModifyAddressUpdate(WCF.UserSystem.Contract.DataContract.User_ConsigneeAddressDC Item)
        {
            bool isDefault = false;
            if (Request.Form.GetValues("Default") != null)
            {
                isDefault = true;
                Item.IsDefault = 1;
            }

            var user = GetUserInfo();

            if (Item.ID == 0)
            {
                var rtn = App_Code.Proxy.UserProxy.Update_UserAddressAdd(_UserOpenID, Item.Consignee, Item.DistrictID, Item.Address, Item.MPNo, isDefault);
                if (rtn.Success)
                    Item.ID = rtn.OBJ;
            }
            else
            {
                if (user.AddressModifyFlag != "send")
                    App_Code.Proxy.UserProxy.Update_UserAddressUpdate(Item.ID, Item.Consignee, Item.DistrictID, Item.Address, Item.MPNo, isDefault);
            }

            if (user.AddressModifyFlag == "get")
            {
                user.GetAddress = Item;
            }
            else if (user.AddressModifyFlag == "send")
                user.SendAddress = Item;

            if (user.GetAddress == null)
                user.GetAddress = Item;
            if (user.SendAddress == null)
                user.SendAddress = Item;

            user.AddressModifyFlag = string.Empty;

            var callback = HttpContext.Cache[CustomerConfig.Cache_CallbackUrl + _UserOpenID];
            if (callback != null)
            {
                HttpContext.Cache.Remove(CustomerConfig.Cache_CallbackUrl + _UserOpenID);
                return Redirect(callback.ToString());
            }
            else
                return RedirectToAction("Address");
        }

        public ActionResult Prepaid()
        {
            return View();
        }

        [NoCacheFilter]
        public ActionResult MobileBindShow()
        {
            LazyAtHome.Web.WeiXin.Models.ViewModel.VerifyViewModel item = new Models.ViewModel.VerifyViewModel();
            item.MPNo = "请输入手机号码";
            item.VerifyCode = "请输入验证码";
            HttpCookie cook = Request.Cookies.Get("LazywxLinkCode");
            if (cook != null)
                ViewBag.Code = cook.Value;
            return View(item);
        }

        [ValidateAntiForgeryToken]
        public ActionResult MobileBinding(Models.ViewModel.VerifyViewModel model)
        {
            if (model.MPNo == null)
            {
                model.MPNo = string.Empty;
            }
            else
            {
                var obj = HttpContext.Cache[CustomerConfig.Cache_Sms_Code + model.MPNo];
                if ((obj != null) && (model.VerifyCode == obj.ToString()))
                {
                    string inviteCode = GetCooking("LazywxLinkCode");
                    var rtn = App_Code.Proxy.UserProxy.Update_UserBind(_UserOpenID, model.MPNo, model.MPNo, 1, inviteCode);
                    if (rtn.Success)
                    {
                        UpdateUserInfo();
                        var callBack = HttpContext.Cache[CustomerConfig.Cache_CallbackUrl + _UserOpenID];
                        if (callBack != null)
                        {
                            HttpContext.Cache.Remove(CustomerConfig.Cache_CallbackUrl + _UserOpenID);
                            return Redirect(callBack.ToString());
                        }
                        return RedirectToAction("MemberCenter");
                    }
                    else
                        ViewBag.Msg = rtn.Message;
                }
                else
                {
                    ViewBag.Msg = "验证码错误，请重新输入";
                }
            }
            return View("MobileBindShow", model);
        }

        public JsonResult VerifyMobile(string mpno)
        {
            Random rnd = new Random();
            string strCode = rnd.Next(0, 999999).ToString().PadLeft(6, '0');
            HttpContext.Cache[CustomerConfig.Cache_Sms_Code + mpno] = strCode;
            var rtnMsg = "验证码已经发送，请耐心等待";
            WCF.Common.Contract.ClientProxy.SmsClient.Base_SmsSend_Create(mpno, "尊敬的用户您好，为了保证您的安全，本次操作您的验证码为" + strCode + "，请您在5分钟之内完成验证。");

            return Json(new { Msg = rtnMsg });
        }

        public JsonResult ModifyNickName(string nickName)
        {
            int code = 0;
            string msg = "";

            var rtn = App_Code.Proxy.UserProxy.User_NickName_Check(nickName.Trim());
            if (!rtn.Success)
            {
                code = rtn.Code;
                msg = rtn.Message;
            }
            else
            {
                var user = GetUserInfo();
                rtn = App_Code.Proxy.UserProxy.User_NickName_Change(user.User.UserInfo.ID, nickName);
                if (rtn.Success)
                {
                    user.User.UserDetail.NickName = nickName;
                    code = 1;
                }
                else
                {
                    code = rtn.Code;
                    msg = rtn.Message;
                }
            }

            return Json(new { Code = code, Msg = msg });
        }
    }
}