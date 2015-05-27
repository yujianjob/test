using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LazyAtHome.Web.WeiXin.Controllers
{
    public class BaseController : Controller
    {
        public string _UserOpenID = "";//"testUserOpenID_3";

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string currAction = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "_" + filterContext.ActionDescriptor.ActionName;
            //_UserOpenID = "wx9ab0f092bce2810D";

            if (currAction != "WeiXin_Index" && currAction != "WeiXin_OAuth_Response" && currAction != "ShoppingCart_AlipayNotify" && currAction != "ShoppingCart_AlipayReturn")
            {
                _UserOpenID = GetCooking("LazywxInfo");

                UtilityFunc.Add(filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "_" + filterContext.ActionDescriptor.ActionName + " UserOpenID:" + _UserOpenID);

                if (string.IsNullOrWhiteSpace(_UserOpenID))
                {
                    string redirectUrl = CustomerConfig.BaseUrl + Url.Action("OAuth_Response", "WeiXin");
                    string state = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "_" + filterContext.ActionDescriptor.ActionName;
                    UtilityFunc.Add("OnActionExecuting:" + redirectUrl + " " + state);
                    string url = Senparc.Weixin.MP.AdvancedAPIs.OAuth.GetAuthorizeUrl(CustomerConfig.AppId, redirectUrl, state, Senparc.Weixin.MP.AdvancedAPIs.OAuthScope.snsapi_base);
                    filterContext.Result = Redirect(url);
                    base.OnActionExecuting(filterContext);
                }
            }
        }

        protected virtual ActionResult ShowError()
        {
            return View();
        }

        public string GetCooking(string name)
        {
            string rtn = null;
            HttpCookie cook = Request.Cookies.Get(name);
            if (cook != null)
                rtn = cook.Value;
            return rtn;
        }

        protected virtual Models.UserInfo GetUserInfo()
        {
            Models.UserInfo userCart = HttpContext.Cache[_UserOpenID] as Models.UserInfo;
            if (userCart == null)
            {
                userCart = new Models.UserInfo();
                userCart.OpenID = _UserOpenID;
                userCart.ProductList = new List<Models.ShopCart.ProductItem>();

                var userinfo = App_Code.Proxy.UserProxy.Select_UserInfo(_UserOpenID);
                if (userinfo.Success)
                {
                    userCart.User = userinfo.OBJ;
                    if (userCart.User.iConsigneeAddress != null && userCart.User.iConsigneeAddress.ID != 0)
                    {
                        userCart.GetAddress = new WCF.UserSystem.Contract.DataContract.User_ConsigneeAddressDC();
                        userCart.GetAddress.ID = userCart.User.iConsigneeAddress.ID;
                        userCart.GetAddress.Consignee = userCart.User.iConsigneeAddress.Consignee;
                        userCart.GetAddress.Address = userCart.User.iConsigneeAddress.Address;
                        userCart.GetAddress.DistrictID = userCart.User.iConsigneeAddress.DistrictID;
                        userCart.GetAddress.IsDefault = userCart.User.iConsigneeAddress.IsDefault;
                        userCart.GetAddress.MPNo = userCart.User.iConsigneeAddress.MPNo;
                        userCart.GetAddress.DistrictName = userCart.User.iConsigneeAddress.DistrictName;
                        userCart.GetAddress.ProvinceName = userCart.User.iConsigneeAddress.ProvinceName;
                        userCart.GetAddress.CityName = userCart.User.iConsigneeAddress.CityName;

                        userCart.SendAddress = new WCF.UserSystem.Contract.DataContract.User_ConsigneeAddressDC();
                        userCart.SendAddress.ID = userCart.User.iConsigneeAddress.ID;
                        userCart.SendAddress.Consignee = userCart.User.iConsigneeAddress.Consignee;
                        userCart.SendAddress.Address = userCart.User.iConsigneeAddress.Address;
                        userCart.SendAddress.DistrictID = userCart.User.iConsigneeAddress.DistrictID;
                        userCart.SendAddress.IsDefault = userCart.User.iConsigneeAddress.IsDefault;
                        userCart.SendAddress.MPNo = userCart.User.iConsigneeAddress.MPNo;
                        userCart.SendAddress.DistrictName = userCart.User.iConsigneeAddress.DistrictName;
                        userCart.SendAddress.ProvinceName = userCart.User.iConsigneeAddress.ProvinceName;
                        userCart.SendAddress.CityName = userCart.User.iConsigneeAddress.CityName;
                    }
                }

                HttpContext.Cache[_UserOpenID] = userCart;
            }

            return userCart;
        }

        protected void UpdateUserInfo(bool UpdateAddress = true)
        {
            Models.UserInfo userCart = HttpContext.Cache[_UserOpenID] as Models.UserInfo;
            if (userCart != null)
            {
                var userinfo = App_Code.Proxy.UserProxy.Select_UserInfo(_UserOpenID);
                if (userinfo.Success)
                {
                    userCart.User = userinfo.OBJ;

                    if (UpdateAddress)
                    {
                        if (userCart.User.iConsigneeAddress != null && userCart.User.iConsigneeAddress.ID != 0)
                        {
                            userCart.GetAddress = new WCF.UserSystem.Contract.DataContract.User_ConsigneeAddressDC();
                            userCart.GetAddress.ID = userCart.User.iConsigneeAddress.ID;
                            userCart.GetAddress.Consignee = userCart.User.iConsigneeAddress.Consignee;
                            userCart.GetAddress.Address = userCart.User.iConsigneeAddress.Address;
                            userCart.GetAddress.DistrictID = userCart.User.iConsigneeAddress.DistrictID;
                            userCart.GetAddress.IsDefault = userCart.User.iConsigneeAddress.IsDefault;
                            userCart.GetAddress.MPNo = userCart.User.iConsigneeAddress.MPNo;
                            userCart.GetAddress.DistrictName = userCart.User.iConsigneeAddress.DistrictName;
                            userCart.GetAddress.ProvinceName = userCart.User.iConsigneeAddress.ProvinceName;
                            userCart.GetAddress.CityName = userCart.User.iConsigneeAddress.CityName;

                            userCart.SendAddress = new WCF.UserSystem.Contract.DataContract.User_ConsigneeAddressDC();
                            userCart.SendAddress.ID = userCart.User.iConsigneeAddress.ID;
                            userCart.SendAddress.Consignee = userCart.User.iConsigneeAddress.Consignee;
                            userCart.SendAddress.Address = userCart.User.iConsigneeAddress.Address;
                            userCart.SendAddress.DistrictID = userCart.User.iConsigneeAddress.DistrictID;
                            userCart.SendAddress.IsDefault = userCart.User.iConsigneeAddress.IsDefault;
                            userCart.SendAddress.MPNo = userCart.User.iConsigneeAddress.MPNo;
                            userCart.SendAddress.DistrictName = userCart.User.iConsigneeAddress.DistrictName;
                            userCart.SendAddress.ProvinceName = userCart.User.iConsigneeAddress.ProvinceName;
                            userCart.SendAddress.CityName = userCart.User.iConsigneeAddress.CityName;
                        }
                        else
                        {
                            userCart.GetAddress = null;
                            userCart.SendAddress = null;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 生成验证码图像对象
        /// </summary>
        /// <returns></returns>
        public ActionResult GetValidateCode()
        {
            LazyAtHome.Core.Web.Extension.ValidateCode vCode = new Core.Web.Extension.ValidateCode();
            string code = vCode.CreateValidateCode(5);
            HttpContext.Cache[CustomerConfig.Cache_ValidateCode + _UserOpenID] = code;             
            byte[] bytes = vCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }
    }
}