using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LazyAtHome.Web.WeiXin.Controllers
{
    public class WashController : BaseController
    {
        public ActionResult Index()
        {
            //if (_UserOpenID != null)
            //    ViewBag.openid = _UserOpenID;
            //else
            //    ViewBag.openid = "无cook";

            //ViewBag.openid += "-" + Request.Url.ToString() + "-" + CustomerConfig.AppId;
            return View();
        }

        public ActionResult Product(int? iClassID)
        {
            var _ProductClass = App_Code.Proxy.WashProxy.Select_ProductClass().OBJ;

            if (iClassID == null)
                iClassID = _ProductClass[0].ID;

            var _ProductList = App_Code.Proxy.WashProxy.Select_ProductList(iClassID.Value).OBJ;

            Models.ViewModel.ProductViewModel model = new Models.ViewModel.ProductViewModel();
            model.ProductClassList = _ProductClass;
            model.ProductList = _ProductList;
            model.SelectClassID = iClassID.Value;

            return View(model);
        }

        public ActionResult ProductDetail(int pID = 1)
        {
            var productInfo = App_Code.Proxy.WashProxy.Select_ProductInfo(pID);

            if (!productInfo.Success)
                return ShowError();

            return View(productInfo.OBJ);
        }

        [NoCacheFilter]
        public ActionResult OneKey()
        {
            var user = GetUserInfo();
            if (user.User == null)
            {
                HttpContext.Cache[CustomerConfig.Cache_CallbackUrl + _UserOpenID] = Request.Url.ToString();
                return RedirectToAction("MobileBindShow", "PersonalCenter");
            }
            return View(user);
        }

        public ActionResult OneKeySubmit()
        {
            var user = GetUserInfo();

            if (user.GetAddress == null)
                ViewBag.Msg = "请添加取件地址";
            else if (user.SendAddress == null)
                ViewBag.Msg = "请添加送件地址";

            if (ViewBag.Msg != null)
                return View("OneKey", user);

            WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC GetAddress = new WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC();
            GetAddress.Consignee = user.GetAddress.Consignee;
            GetAddress.Mpno = user.GetAddress.MPNo;
            GetAddress.Address = user.GetAddress.DistrictName + user.GetAddress.Address;
            GetAddress.CityName = user.GetAddress.CityName;
            GetAddress.ProvinceName = user.GetAddress.ProvinceName;
            GetAddress.DistrictID = user.GetAddress.DistrictID;

            WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC SendAddress = new WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC();
            SendAddress.Consignee = user.SendAddress.Consignee;
            SendAddress.Mpno = user.SendAddress.MPNo;
            SendAddress.Address = user.SendAddress.DistrictName + user.SendAddress.Address;
            SendAddress.CityName = user.SendAddress.CityName;
            SendAddress.ProvinceName = user.SendAddress.ProvinceName;
            SendAddress.DistrictID = user.SendAddress.DistrictID;

            var rtn = App_Code.Proxy.OrderProxy.Update_UserOrderOneKeyAdd(_UserOpenID, GetAddress, SendAddress, null);
            if (!rtn.Success)
            {
                ViewBag.Msg = rtn.Message;
                return View("OneKey", user);
            }
            return RedirectToAction("OrderSubmitFinish", "ShoppingCart", new { orderNum = rtn.OBJ.OrderNumber });
        }

        [NoCacheFilter]
        public ActionResult Book()
        {
            var userinfo = GetUserInfo();
            if (userinfo.User == null)
            {
                HttpContext.Cache[CustomerConfig.Cache_CallbackUrl + _UserOpenID] = Request.Url.ToString();
                return RedirectToAction("MobileBindShow", "PersonalCenter");
            }

            List<SelectListItem> dList = new List<SelectListItem>();
            if (DateTime.Now.Hour < 14)
            {
                SelectListItem today = new SelectListItem();
                today.Text = "今天";
                today.Value = DateTime.Now.ToShortDateString();
                dList.Add(today);
            }
            SelectListItem tomorrow = new SelectListItem();
            tomorrow.Text = "明天";
            tomorrow.Value = DateTime.Now.AddDays(1).ToShortDateString();
            dList.Add(tomorrow);

            for (int i = 2; i < 6; i++)
            {
                SelectListItem dItem = new SelectListItem();
                dItem.Text = DateTime.Now.AddDays(i).ToString("MM月dd日");
                dItem.Value = DateTime.Now.AddDays(i).ToShortDateString();
                dList.Add(dItem);
            }

            List<SelectListItem> tList = new List<SelectListItem>();
            int tIndex = DateTime.Now.Hour + 2;
            if (tIndex >= 18)
                tIndex = 8;

            for (int i = tIndex; i < 18; i++)
            {
                SelectListItem tItem = new SelectListItem();
                tItem.Text = i + "时";
                tItem.Value = i.ToString();
                tList.Add(tItem);
            }

            ViewBag.Day = dList;
            ViewBag.Time = tList;

            var user = GetUserInfo();
            return View(user);
        }

        public ActionResult BookSubmit()
        {
            var user = GetUserInfo();

            DateTime revTime = DateTime.Parse(user.ReceviceDay);
            revTime = revTime.AddHours(int.Parse(user.ReceviceTime));

            WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC GetAddress = new WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC();
            GetAddress.Consignee = user.GetAddress.Consignee;
            GetAddress.Mpno = user.GetAddress.MPNo;
            GetAddress.Address = user.GetAddress.DistrictName + user.GetAddress.Address;
            GetAddress.CityName = user.GetAddress.CityName;
            GetAddress.ProvinceName = user.GetAddress.ProvinceName;
            GetAddress.DistrictID = user.GetAddress.DistrictID;

            WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC SendAddress = new WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC();
            SendAddress.Consignee = user.SendAddress.Consignee;
            SendAddress.Mpno = user.SendAddress.MPNo;
            SendAddress.Address = user.SendAddress.DistrictName + user.SendAddress.Address;
            SendAddress.CityName = user.SendAddress.CityName;
            SendAddress.ProvinceName = user.SendAddress.ProvinceName;
            SendAddress.DistrictID = user.SendAddress.DistrictID;

            var rtn = App_Code.Proxy.OrderProxy.Update_UserOrderOneKeyAdd(_UserOpenID, GetAddress, SendAddress, revTime);
            if (!rtn.Success)
            {
                ViewBag.Msg = rtn.Message;
                return View("Book", user);
            }
            return RedirectToAction("OrderSubmitFinish", "ShoppingCart", new { orderNum = rtn.OBJ.OrderNumber });
        }

        public JsonResult ReceviceTime(string day, string time)
        {
            var user = GetUserInfo();
            int Code = 0;
            string Msg = "";

            if (user.GetAddress == null)
            {
                Code = -1;
                Msg = "请添加取件地址";
            }
            else if (user.SendAddress == null)
            {
                Code = -1;
                Msg = "请添加送件地址";
            }
            else
            {
                user.ReceviceDay = day;
                user.ReceviceTime = time;
            }

            return Json(new { code = Code, msg = Msg });
        }
    }
}