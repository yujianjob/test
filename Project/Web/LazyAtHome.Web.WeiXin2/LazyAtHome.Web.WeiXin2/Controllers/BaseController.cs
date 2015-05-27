using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LazyAtHome.Web.WeiXin2.Controllers
{
    public class BaseController : Controller
    {
        public string _UserOpenID = "testUserOpenID_3";//"testUserOpenID_3";
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string currAction = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "_" + filterContext.ActionDescriptor.ActionName;
            //_UserOpenID = "wx9ab0f092bce2810D";

            //if (currAction != "WeiXin_Index" && currAction != "WeiXin_OAuth_Response" && currAction != "Cart_AlipayNotify" && currAction != "Cart_AlipayReturn")
            //{
            //    _UserOpenID = GetCooking("LazywxInfo");

            //    App_Code.UtilityFunc.Add(filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "_" + filterContext.ActionDescriptor.ActionName + " UserOpenID:" + _UserOpenID);

            //    if (string.IsNullOrWhiteSpace(_UserOpenID))
            //    {
            //        string redirectUrl = AppConfig.WeiXin_BaseUrl + Url.Action("OAuth_Response", "WeiXin");
            //        string state = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "_" + filterContext.ActionDescriptor.ActionName;
            //        App_Code.UtilityFunc.Add("OnActionExecuting:" + redirectUrl + " " + state);
            //        string url = Senparc.Weixin.MP.AdvancedAPIs.OAuth.GetAuthorizeUrl(AppConfig.WeiXin_AppId, redirectUrl, state, Senparc.Weixin.MP.AdvancedAPIs.OAuthScope.snsapi_base);
            //        filterContext.Result = Redirect(url);
            //        base.OnActionExecuting(filterContext);
            //    }
            //}
        }

        public string GetCooking(string name)
        {
            string rtn = null;
            HttpCookie cook = Request.Cookies.Get(name);
            if (cook != null)
                rtn = cook.Value;
            return rtn;
        }

        [ChildActionOnly]
        public Models.UserModel GetUserInfo()
        {

            Models.UserModel user = HttpContext.Cache[_UserOpenID] as Models.UserModel;
            if (user == null)
            {
                user = new Models.UserModel();
                user.Cart = new Models.CartModel();
                HttpContext.Cache[_UserOpenID] = user;

                var userinfo = App_Code.Proxy.UserProxycs.wx_User_Weixin_SELECT(_UserOpenID);
                if (userinfo.Success)
                {
                    user.User = userinfo.OBJ;                    
                }
            }

            return user;
        }

        public void UpdateUserInfo()
        {
            Models.UserModel user = HttpContext.Cache[_UserOpenID] as Models.UserModel;
            if (user != null)
            {
                var userinfo = App_Code.Proxy.UserProxycs.wx_User_Weixin_SELECT(_UserOpenID);
                if (userinfo.Success)
                {
                    user.User = userinfo.OBJ;
                }
            }
        }

        public IList<Models.OrderViewModel> ConvertOrderList(IList<WCF.OrderSystem.Contract.DataContract.Order_OrderDC> list)
        {
            IList<Models.OrderViewModel> xList = new List<Models.OrderViewModel>();
            if (list != null)
                foreach (var orderItem in list)
                {
                    Models.OrderViewModel orderView = new Models.OrderViewModel();
                    orderView.ID = orderItem.ID;
                    orderView.OrderNum = orderItem.OrderNumber;
                    orderView.CreateTime = orderItem.Obj_Cdate.Value;
                    orderView.OrderStatus = orderItem.OrderStatus;
                    orderView.OrderType = orderItem.OrderType;
                    orderView.PayStatus = orderItem.PayStatus;
                    orderView.OrderClass = orderItem.OrderClass;
                    orderView.ProductList = new List<Models.ProductItemModel>();

                    var plist = orderItem.ProductList.GroupBy(p => p.ProductID);
                    foreach (var product in plist)
                    {
                        Models.ProductItemModel pi = new Models.ProductItemModel();
                        var pDC = product.First();
                        pi.ProductInfo = pDC;
                        pi.Count = product.Count();
                        orderView.ProductList.Add(pi);
                        orderView.TotalPrice += pi.ProductInfo.Price * pi.Count;
                    }
                    xList.Add(orderView);
                }

            return xList;
        }
	}
}