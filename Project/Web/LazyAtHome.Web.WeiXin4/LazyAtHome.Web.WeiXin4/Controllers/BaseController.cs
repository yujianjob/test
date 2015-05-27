using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LazyAtHome.Web.WeiXin4.App_Code;
using Senparc.Weixin.HttpUtility;

namespace LazyAtHome.Web.WeiXin4.Controllers
{
    [ErrorFilter]
    public class BaseController : Controller
    {

        public string _UserOpenID = "";//"testUserOpenID_3  oHiw3uPSbWZ2o3OAJNcKG5q30rp811"; 11
        //static int aaa=0;


        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string currAction = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "_" + filterContext.ActionDescriptor.ActionName;

            if (currAction != "Payment_AlipayReturn" && currAction != "Payment_AlipayNotify" && currAction != "Payment_WxPayNotify")
            {
                _UserOpenID = GetCooking("LazywxInfo4");

                string state = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "||" + filterContext.ActionDescriptor.ActionName;
                if (filterContext.ActionParameters.Count > 0)
                {
                    state += "||";
                    foreach (KeyValuePair<string, object> item in filterContext.ActionParameters)
                    {
                        if (item.Value != null)
                            state += item.Key + "=" + item.Value.ToString() + "&";
                    }
                }
                state = Server.UrlEncode(state);

                App_Code.UtilityFunc.Add(filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "||" + filterContext.ActionDescriptor.ActionName + " UserOpenID:" + _UserOpenID);
                if (string.IsNullOrWhiteSpace(_UserOpenID))
                {
                    //string state = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "_" + filterContext.ActionDescriptor.ActionName;
                    App_Code.UtilityFunc.Add("OnActionExecuting:" + AppConfig.WeiXin_AuthorizeRedirectUrl + " " + state);
                    string url = Senparc.Weixin.MP.AdvancedAPIs.OAuth.GetAuthorizeUrl(AppConfig.WeiXin_AppId, AppConfig.WeiXin_AuthorizeRedirectUrl, state, Senparc.Weixin.MP.AdvancedAPIs.OAuthScope.snsapi_base);
                    App_Code.UtilityFunc.Add("AuthorizeUrl：" + url);
                    filterContext.Result = Redirect(url);
                    //base.OnActionExecuting(filterContext);
                }
                else
                {
                    //    if (_UserOpenID != "ontm8t8X4AdUTYRUK2pNpIE_4Ig4" &&
                    //_UserOpenID != "oSKuOt_IH92WLP7pOzUtvxS6MSpA" &&
                    //_UserOpenID != "ontm8t9Y4NNfQMyEb9kHflv7bvqU"
                    //        )
                    //    {
                    //        filterContext.Result = Redirect(AppConfig.BaseUrl + "/Home/Index");
                    //    }
                }
            }
        }

        public string GetCooking(string name)
        {
            string rtn = null;
            HttpCookie cook = Request.Cookies.Get(name);
            if (cook != null)
                rtn = cook.Value;
            return rtn;
        }

        public bool CheckLoginCook()
        {
            string loginStatus = GetCooking("Lazywxlogin4");
            if (loginStatus == "login")
                return true;
            return false;
        }

        //[ChildActionOnly]
        //public Models.UserModel GetUserInfo(bool bGet = true, bool bCreate = false)
        //{
        //    Models.UserModel user = HttpContext.Cache[_UserOpenID] as Models.UserModel;
        //    if (user == null)
        //    {
        //        user = new Models.UserModel();
        //        user.Cart = new Models.CartModel();
        //        HttpContext.Cache[_UserOpenID] = user;

        //        if (bGet)
        //        {
        //            var userinfo = App_Code.Proxy.UserProxycs.wx_User_Weixin_SELECT(_UserOpenID);
        //            if (userinfo.Success)
        //            {
        //                user.User = userinfo.OBJ;
        //                user.Cart.UserLevel = userinfo.OBJ.UserInfo.Level;
        //            }
        //            else if (bCreate && userinfo.Code == -1)
        //            {
        //                var regRtn = App_Code.Proxy.UserProxycs.wx_User_Weixin_Create(_UserOpenID, AppConfig.SiteID, "", "");
        //                if (regRtn.Success)
        //                    user.User = regRtn.OBJ;
        //            }
        //        }
        //    }
        //    return user;
        //}

        [ChildActionOnly]
        public LazyAtHome.Web.WeiXin4.Models.EntityModel.UserModel2 GetUserInfo()
        {
            LazyAtHome.Web.WeiXin4.Models.EntityModel.UserModel2 user = HttpContext.Cache[_UserOpenID] as LazyAtHome.Web.WeiXin4.Models.EntityModel.UserModel2;

            if (user == null)
            {
                user = new LazyAtHome.Web.WeiXin4.Models.EntityModel.UserModel2();
                string url = AppConfig.AppService + "/user/getUserByOpenId.do";

                Dictionary<string, string> dic = new Dictionary<string, string>();

                var result = Post.GetResult<LazyAtHome.Web.WeiXin4.Models.EntityModel.resultEntity<LazyAtHome.Web.WeiXin4.Models.EntityModel.UserInfoEntity>>(Senparc.Weixin.HttpUtility.RequestUtility.HttpPost(url, null, dic));
                if (result.succFlag && result.data != null && !string.IsNullOrEmpty(result.data.userId))
                {
                    user.MpNo = result.data.mpNo;
                    user.Openid = _UserOpenID;
                    user.UserId = result.data.userId;

                    string url2 = AppConfig.AppService + "/balance/queryBalance.do";

                    Dictionary<string, string> dic2 = new Dictionary<string, string>();
                    dic.Add("uname", result.data.mpNo);

                    var result2 = Post.GetResult<LazyAtHome.Web.WeiXin4.Models.EntityModel.resultEntity<LazyAtHome.Web.WeiXin4.Models.EntityModel.amountEntity>>(Senparc.Weixin.HttpUtility.RequestUtility.HttpPost(url2, null, dic2));
                    if (result2.succFlag && result2.data != null && !string.IsNullOrEmpty(result2.data.amount))
                    {
                        user.Money = result2.data.amount;
                    }
                    HttpContext.Cache[_UserOpenID] = user;
                }
            }
            return user;
        }

        public void UpdateUserInfo()
        {
            LazyAtHome.Web.WeiXin4.Models.EntityModel.UserModel2 user = HttpContext.Cache[_UserOpenID] as LazyAtHome.Web.WeiXin4.Models.EntityModel.UserModel2;
            if (user != null)
            {
                string url = AppConfig.AppService + "/user/getUserByOpenId.do";

                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("openId", _UserOpenID);

                var result = Post.GetResult<LazyAtHome.Web.WeiXin4.Models.EntityModel.resultEntity<LazyAtHome.Web.WeiXin4.Models.EntityModel.UserInfoEntity>>(Senparc.Weixin.HttpUtility.RequestUtility.HttpPost(url, null, dic));
                if (result.succFlag && result.data != null && !string.IsNullOrEmpty(result.data.userId))
                {
                    user.MpNo = result.data.mpNo;
                    user.Openid = _UserOpenID;
                    user.UserId = result.data.userId;

                    string url2 = AppConfig.AppService + "/balance/queryBalance.do";

                    Dictionary<string, string> dic2 = new Dictionary<string, string>();
                    dic.Add("uname", result.data.mpNo);

                    var result2 = Post.GetResult<LazyAtHome.Web.WeiXin4.Models.EntityModel.resultEntity<LazyAtHome.Web.WeiXin4.Models.EntityModel.amountEntity>>(Senparc.Weixin.HttpUtility.RequestUtility.HttpPost(url2, null, dic2));
                    if (result2.succFlag && result2.data != null && !string.IsNullOrEmpty(result2.data.amount))
                    {
                        user.Money = result2.data.amount;
                    }
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

        /// <summary>
        /// 生成验证码图像对象
        /// </summary>
        /// <returns></returns>
        public ActionResult GetValidateCode()
        {
            LazyAtHome.Core.Web.Extension.ValidateCode vCode = new Core.Web.Extension.ValidateCode();
            string code = vCode.CreateValidateCode(5);
            HttpContext.Cache.Insert(AppConfig.Cache_ValidateCode + _UserOpenID, code, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 10, 0), System.Web.Caching.CacheItemPriority.Default, null);
            byte[] bytes = vCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }
    }

    public class ErrorFilterAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {

            //出错时间 
            string ErrTime = DateTime.Now.ToString();
            //错误类型 
            string ErrType = filterContext.Exception.GetType().ToString();
            //错误信息 
            string ErrMessage = filterContext.Exception.Message;
            //引发异常的方法 
            string targetsite = filterContext.Exception.TargetSite.ToString();
            //引发异常的对象 
            string source = filterContext.Exception.Source.ToString();
            //异常Controller名称 
            string controller = filterContext.RouteData.Values["controller"].ToString();
            //异常Action名称 
            string action = filterContext.RouteData.Values["action"].ToString();

            string ErrStr = string.Format("时间:{0} ,错误类型:{0},错误信息:{2},异常的方法:{3},异常的对象:{4},Controller名称:{5},Action名称:{6}", ErrTime, ErrType, ErrMessage, targetsite, source, controller, action);
            App_Code.UtilityFunc.AddToFile(ErrStr, "error");
        }
    }
}