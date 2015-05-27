using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LazyAtHome.Web.WashSite.Controllers
{
    /// <summary>
    /// 控制器基础类
    /// </summary>
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string invitecode = Request.QueryString["invitecode"];
            if (!string.IsNullOrWhiteSpace(invitecode))
                Session["invitecode"] = invitecode;
        }

        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);

            var rtn = WCF.Common.Contract.ClientProxy.BaseClient.Cache_web_Base_WebAttribute_SELECT_Entity(Request.Path);
            if (rtn.Success)
            {
                ViewBag.Title = rtn.OBJ.Title;
                ViewBag.Keywords = rtn.OBJ.Keywords;
                ViewBag.Description = rtn.OBJ.Description;
            }
            else
                ViewBag.Title = "懒到家";
        }

        public int Site
        {
            get { return AppConfig.SiteID; }
        }

        [ChildActionOnly]
        public Models.User.UserInfo GetUserInfo()
        {
            Models.User.UserInfo user = (Models.User.UserInfo)Session["UserInfo"];
            if (user == null)
            {
                user = new Models.User.UserInfo();
                user.Cart = new Models.User.Cart();
                user.ActiveCart = new Models.User.Cart();
                Session["UserInfo"] = user;

                if (User.Identity.IsAuthenticated)
                {
                    HttpCookie cook = Request.Cookies.Get("LazywebInfo");
                    if (cook != null)
                    {
                        UpdateUserInfo(cook.Value);
                    }
                }
            }

            return user;
        }

        public void UpdateUserInfo(string userid = null)
        {
            var user = GetUserInfo();

            if (userid == null)
                userid = user.User.ID.ToString();

            var rtn = App_Code.Proxy.UserProxycs.Select_UserInfo(Guid.Parse(userid));
            if (rtn.Success)
                user.User = rtn.OBJ;

            var rtnDetail = App_Code.Proxy.UserProxycs.Select_UserDetail(Guid.Parse(userid));
            if (rtnDetail.Success)
                user.UserDetail = rtnDetail.OBJ;
        }

        public void CartProductAdd(int spid, int pcount)
        {
            var user = GetUserInfo();
            var rtn = WCF.Wash.Contract.ClientProxy.web_ProductClient.Cache_web_Wash_Product_SELECT_Entity(spid);

            var pItem = user.Cart.ProductList.FirstOrDefault(p => p.ProductInfo.ID == spid);
            if (pItem == null)
            {
                pItem = new Models.User.CartProductItem();
                pItem.ProductInfo = rtn.OBJ;
                pItem.Count = pcount;
                user.Cart.ProductList.Add(pItem);
            }
            else
                pItem.Count += pcount;

            user.Cart.TotalPrice += pItem.ProductInfo.SalesPrice * pcount;
            user.Cart.TotalCount += pcount;

            ClearPayInfo(user.Cart);
        }

        public void CartActiveAdd(int spid, int pcount)
        {
            var user = GetUserInfo();
            var rtn = WCF.Wash.Contract.ClientProxy.web_ProductClient.Cache_web_Wash_Product_SELECT_Entity(spid);

            var pItem = user.ActiveCart.ProductList.FirstOrDefault(p => p.ProductInfo.ID == spid);
            if (pItem == null)
            {
                pItem = new Models.User.CartProductItem();
                pItem.ProductInfo = rtn.OBJ;
                pItem.Count = pcount;
                user.ActiveCart.ProductList.Add(pItem);
            }
            else
                pItem.Count += pcount;

            user.ActiveCart.TotalPrice += pItem.ProductInfo.MarketPrice * pcount;
            user.ActiveCart.TotalCount += pcount;

            ClearPayInfo(user.ActiveCart);
        }

        public void UserLoginProcess()
        { }

        /// <summary>
        /// 生成验证码图像对象
        /// </summary>
        /// <returns></returns>        
        public ActionResult GetValidateCode()
        {
            LazyAtHome.Core.Web.Extension.ValidateCode vCode = new Core.Web.Extension.ValidateCode();
            string code = vCode.CreateValidateCode(5);
            Session[AppConfig.Cache_ValidateCode] = code;
            byte[] bytes = vCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }

        public ActionResult ShowError(string msg)
        {
            return View();
        }

        /// <summary>
        /// 清除抵扣记录
        /// </summary>
        public void ClearPayInfo(Models.User.Cart Cart)
        {
            if (Cart != null)
            {
                Cart.UseBalance = 0;
                Cart.UseCard.Clear();
                Cart.PayPrice = Cart.TotalPrice;
                Cart.ExpressEnable = false;
            }
        }

        public IList<Models.Wash.OrderViewModel> ConvertOrderList(IList<WCF.OrderSystem.Contract.DataContract.Order_OrderDC> list)
        {            
            IList<Models.Wash.OrderViewModel> xList = new List<Models.Wash.OrderViewModel>();
            if(list!=null)
                foreach (var orderItem in list)
                {
                    Models.Wash.OrderViewModel orderView = new Models.Wash.OrderViewModel();
                    orderView.ID = orderItem.ID;
                    orderView.OrderNum = orderItem.OrderNumber;
                    orderView.CreateTime = orderItem.Obj_Cdate.Value;
                    orderView.OrderStatus = orderItem.OrderStatus;
                    orderView.OrderType = orderItem.OrderType;
                    orderView.PayStatus = orderItem.PayStatus;
                    orderView.OrderClass = orderItem.OrderClass;
                    orderView.ProductList = new List<Models.Wash.ProductItem>();

                    var plist = orderItem.ProductList.GroupBy(p => p.ProductID);
                    foreach (var product in plist)
                    {
                        Models.Wash.ProductItem pi = new Models.Wash.ProductItem();
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