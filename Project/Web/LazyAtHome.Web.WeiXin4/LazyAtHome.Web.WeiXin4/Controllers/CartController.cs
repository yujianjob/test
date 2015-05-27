using LazyAtHome.Web.WeiXin4.App_Code;
using Senparc.Weixin.HttpUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LazyAtHome.Web.WeiXin4.Controllers
{
    public class CartController : BaseController
    {
        [NoCacheFilter]
        public ActionResult OneKey()
        {
            ViewBag.IsLogin = 0;
            if (CheckLoginCook())
            {
                ViewBag.IsLogin = 1;
                var user = GetUserInfo();
                if (user != null)
                {
                    string url = AppConfig.AppService + "/userAddress/getUserAddressAll.do";
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("userId", user.UserId);

                    var result = UtilityFunc.GetPostResponse<LazyAtHome.Web.WeiXin4.Models.EntityModel.resultEntity<System.Collections.Generic.List<LazyAtHome.Web.WeiXin4.Models.EntityModel.AdressInfo>>>(url, dic);
                    if (result.succFlag == true && result.data != null)
                    {
                        ViewBag.AddressList = result.data;
                    }

                    ViewBag.UserMoney = user.Money;
                    ViewBag.UserMPNo = user.MpNo;
                }
            }
            else
            {
                ViewBag.UserMoney = 0;
                ViewBag.UserMPNo = "";
            }

            return View();
        }

        public ActionResult ProductList()
        {
            string url = AppConfig.AppService + "/washPrice/getAllWashPriceListForPrice.do";
            Dictionary<string, string> dic = new Dictionary<string, string>();

            var result = UtilityFunc.GetPostResponse<LazyAtHome.Web.WeiXin4.Models.EntityModel.resultEntity<System.Collections.Generic.List<LazyAtHome.Web.WeiXin4.Models.EntityModel.ProductItem>>>(url, dic);
            if (result.succFlag == true && result.data != null)
            {
                ViewBag.ProductItem = result.data;
            }
            return View();
        }

        public JsonResult ClassShow(string classid)
        {
            int Code = 0;
            string Msg = "";
            string url = AppConfig.AppService + "/washPrice/getAllWashPriceListForPrice.do";
            Dictionary<string, string> dic = new Dictionary<string, string>();

            var result = UtilityFunc.GetPostResponse<LazyAtHome.Web.WeiXin4.Models.EntityModel.resultEntity<System.Collections.Generic.List<LazyAtHome.Web.WeiXin4.Models.EntityModel.ProductItem>>>(url, dic);
            if (result.succFlag == true && result.data != null)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                var list = result.data.Where(s => s.price == classid).First().washProductPriceVOList;
                var mol = list.Count % 3;
                var count = list.Count / 3;
                for (int i = 0; i < count; i++)
                {
                    sb.Append("<div class=\"unit\"><span class=\"bt\">" + list[i].prName + "</span>");
                    sb.Append("<span class=\"bt\">" + list[i + 1].prName + "</span>");
                    sb.Append("<span class=\"bt\">" + list[i + 2].prName + "</span></div>");
                }
                if (mol != 0)
                {
                     sb.Append("<div class=\"unit\">");
                    for (int i = 0; i < mol; i++)
                    {
                        sb.Append("<span class=\"bt\">" + list[count * 3+i].prName + "</span>");
                    }
                    sb.Append("</div>");
                }
                Code = 1;
                Msg = sb.ToString();
            }
            return Json(new { code = Code, msg = Msg });
        }

        public ActionResult MyOrders(int? pageIndex)
        {
            var user = GetUserInfo();
            if (user == null || !CheckLoginCook())
            {
                HttpContext.Cache[AppConfig.Cache_CallbackUrl + _UserOpenID] = Request.Url.ToString();
                return RedirectToAction("MobileBindingShow", "Member");
            }
            string url = AppConfig.AppService + "/order/pagingQueryOrder.do";

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("userId", user.UserId);
            dic.Add("pageSize", "20");
            if (pageIndex.HasValue)
                dic.Add("pageNo", pageIndex.Value.ToString());

            var result = UtilityFunc.GetPostResponse<LazyAtHome.Web.WeiXin4.Models.EntityModel.resultEntity<LazyAtHome.Web.WeiXin4.Models.EntityModel.OrderInfoList>>(url, dic);
            App_Code.UtilityFunc.AddToFile("MyOrders" + "-", "test");

            LazyAtHome.Web.WeiXin4.Models.EntityModel.OrderInfoList xList = new LazyAtHome.Web.WeiXin4.Models.EntityModel.OrderInfoList();
            xList.dataList = new List<LazyAtHome.Web.WeiXin4.Models.EntityModel.OrderInfo>();
            xList.pageCount = "0";

            if (result.succFlag == true && result.data != null && result.data.dataList != null)
            {
                xList = result.data;
            }
            return View(xList);
        }

        //public JsonResult CheckOrderList()
        //{
        //    var user = GetUserInfo();
        //    if (user == null || !CheckLoginCook())
        //    {
        //        return Json(new { Code = 0 });
        //    }

        //    var rtn = App_Code.Proxy.OrderProxy.Order_SELECT_List(user.User.UserInfo.ID, null, null, null, 1, 10);
        //    if (rtn.Success && rtn.OBJ.RecordCount == 0)
        //        return Json(new { Code = 1 });
        //    return Json(new { Code = 0 });
        //}

        public ActionResult OrderDetail(string orderNum)
        {
            var user = GetUserInfo();
            if (user == null || !CheckLoginCook())
            {
                HttpContext.Cache[AppConfig.Cache_CallbackUrl + _UserOpenID] = Request.Url.ToString();
                return RedirectToAction("MobileBindingShow", "Cart");
            }
            string url = AppConfig.AppService + "/order/queryOrderDetail.do";

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("userId", user.UserId);
            dic.Add("orderNumber", orderNum);

            var result = UtilityFunc.GetPostResponse<LazyAtHome.Web.WeiXin4.Models.EntityModel.resultEntity<LazyAtHome.Web.WeiXin4.Models.EntityModel.OrderDetailInfo>>(url, dic);
            App_Code.UtilityFunc.AddToFile("MyOrders" + "-", "test");

            LazyAtHome.Web.WeiXin4.Models.EntityModel.OrderDetailInfo detailInfo = new LazyAtHome.Web.WeiXin4.Models.EntityModel.OrderDetailInfo();
            

            if (result.succFlag)
            {
                detailInfo = result.data;
                if (detailInfo.washItemSubtVoList==null)
                {
                    detailInfo.washItemSubtVoList = new List<Models.EntityModel.WashItem>();
                }
            }
            else
            {
                return Content(result.msg);
            }

            return View(detailInfo);
        }

        public JsonResult OneKeyFirstSubmit(string username, string mpno, string address, DateTime? exceptTime, string remark)
        {
            UtilityFunc.AddToFile(username+"-"+mpno+"-"+address+"-"+remark, "test");
            int code = 0;
            string rtnMsg = "";

            var user = GetUserInfo();
            if (user != null)
            {
                code = 1;

                string url = AppConfig.AppService + "/userAddress/addUserAddress.do";
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("userId", user.UserId);
                dic.Add("consigneeName", username);
                dic.Add("address", address);
                dic.Add("consigneeMpNo", mpno);
                dic.Add("defaultFlag", "true");

                var result = UtilityFunc.GetPostResponse<LazyAtHome.Web.WeiXin4.Models.EntityModel.resultEntity<LazyAtHome.Web.WeiXin4.Models.EntityModel.AdressInfo>>(url, dic);
                if (result.succFlag == true)//添加地址成功,一键下单
                {
                    string url2 = AppConfig.AppService + "/order/oneKeyOrder.do";
                    Dictionary<string, string> dic2 = new Dictionary<string, string>();
                    dic2.Add("userId", user.UserId);
                    dic2.Add("userName", username);
                    dic2.Add("address", address);
                    dic2.Add("mpNo", mpno);
                    if (exceptTime.HasValue)
                        dic2.Add("expectTime", exceptTime.Value.ToShortTimeString());
                    dic2.Add("userRemark", remark);
                    dic2.Add("channel", "23");

                    var result2 = UtilityFunc.GetPostResponse<LazyAtHome.Web.WeiXin4.Models.EntityModel.resultEntity<LazyAtHome.Web.WeiXin4.Models.EntityModel.OrderInfo>>(url2, dic2);
                    if (result2.succFlag == true && result2.data != null)
                    {
                        code = 1;
                        rtnMsg = "一键下单完成";
                    }
                    else
                    {
                        code = -1;
                        rtnMsg = result2.msg;
                    }
                }
                else
                {
                    code = 0;
                    rtnMsg = "添加用户地址失败";
                    App_Code.UtilityFunc.Add("添加用户地址失败：" + result.msg);
                }
            }
            else
            {
                code = -1;
                rtnMsg = "订单提交失败";
                App_Code.UtilityFunc.Add("订单提交失败，用户不存在，openid:" + _UserOpenID);
            }

            return Json(new { Code = code, Msg = rtnMsg });
        }

        public JsonResult OneKeySubmit(int AddressID, DateTime? exceptTime, string remark)
        {
            UtilityFunc.AddToFile(AddressID.ToString() + "-"+ remark, "test");
            int code = 0;
            string msg = "";

            var user = GetUserInfo();
            string url = AppConfig.AppService + "/userAddress/getUserAddressAll.do";
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("userId", user.UserId);

            var result = UtilityFunc.GetPostResponse<LazyAtHome.Web.WeiXin4.Models.EntityModel.resultEntity<System.Collections.Generic.List<LazyAtHome.Web.WeiXin4.Models.EntityModel.AdressInfo>>>(url, dic); ;
            if (result.succFlag == false || result.data == null || result.data.Where(s => s.id == AddressID) == null)
            {
                code = -1;
                msg = "请添加取送地址";
            }
            else
            {
                var address = result.data.Where(s => s.id == AddressID).First();
                string url2 = AppConfig.AppService + "/order/oneKeyOrder.do";
                Dictionary<string, string> dic2 = new Dictionary<string, string>();
                dic2.Add("userId", user.UserId);
                dic2.Add("userName", address.consigneeName);
                dic2.Add("address", address.address);
                dic2.Add("mpNo", address.consigneeMpNo);
                if (exceptTime.HasValue)
                    dic2.Add("expectTime", exceptTime.Value.ToShortTimeString());
                dic2.Add("userRemark", remark);
                dic2.Add("channel", "23");

                var result2 = UtilityFunc.GetPostResponse<LazyAtHome.Web.WeiXin4.Models.EntityModel.resultEntity<LazyAtHome.Web.WeiXin4.Models.EntityModel.OrderInfo>>(url2, dic2);
                if (result2.succFlag == true && result2.data != null)
                {
                    code = 1;
                    msg = "一键下单完成";
                }
                else
                {
                    code = -1;
                    msg = result2.msg;
                }
            }
            return Json(new { Code = code, Msg = msg });
        }
    }
}