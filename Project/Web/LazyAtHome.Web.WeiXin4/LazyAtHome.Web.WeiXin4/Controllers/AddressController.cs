using LazyAtHome.Web.WeiXin4.App_Code;
using Senparc.Weixin.HttpUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LazyAtHome.Web.WeiXin4.Controllers
{
    public class AddressController : BaseController
    {
        public ActionResult AddressManage()
        {
            var user = GetUserInfo();
            if (user == null || !CheckLoginCook())
            {
                HttpContext.Cache[AppConfig.Cache_CallbackUrl + _UserOpenID] = Request.Url.ToString();
                return RedirectToAction("MobileBindingShow", "Member");
            }

            string url = AppConfig.AppService + "/userAddress/getUserAddressAll.do";
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("userId", user.UserId);

            List<LazyAtHome.Web.WeiXin4.Models.EntityModel.AdressInfo> xList = null;

            var result = UtilityFunc.GetPostResponse<LazyAtHome.Web.WeiXin4.Models.EntityModel.resultEntity<System.Collections.Generic.List<LazyAtHome.Web.WeiXin4.Models.EntityModel.AdressInfo>>>(url, dic);
            if (result.succFlag == true && result.data != null)
            {
                xList = result.data;
            }
            if (xList == null)
                xList = new List<LazyAtHome.Web.WeiXin4.Models.EntityModel.AdressInfo>();

            return View(xList);
        }

        public ActionResult AddressModify(int? id)
        {
            var user = GetUserInfo();

            LazyAtHome.Web.WeiXin4.Models.EntityModel.AdressInfo item = null;
            if (id.HasValue)
            {
                string url = AppConfig.AppService + "/userAddress/getUserAddressAll.do";
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("userId", user.UserId);

                var result = UtilityFunc.GetPostResponse<LazyAtHome.Web.WeiXin4.Models.EntityModel.resultEntity<List<LazyAtHome.Web.WeiXin4.Models.EntityModel.AdressInfo>>>(url, dic);
                if (result.succFlag && result.data != null)
                    item = result.data.Where(s => s.id == id.Value).First();
            }
            else
            {
                item = new Models.EntityModel.AdressInfo();
                item.id = 0;
                item.userId = user.UserId;
                item.consigneeName = "";
                item.consigneeMpNo = "";
                item.address = "";
                item.defaultFlag = true;
            }

            if (Request.UrlReferrer != null)
                HttpContext.Cache[AppConfig.Cache_CallbackUrl + _UserOpenID] = Request.UrlReferrer.ToString();

            return View(item);
        }

        public JsonResult AddressDel(int id)
        {
            int code = 0;
            string rtnMsg = "";
            var user = GetUserInfo();
            string url = AppConfig.AppService + "/userAddress/deleteUserAddress.do";
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("userId", user.UserId);
            dic.Add("id", id.ToString());

            var result = UtilityFunc.GetPostResponse<LazyAtHome.Web.WeiXin4.Models.EntityModel.resultEntity<LazyAtHome.Web.WeiXin4.Models.EntityModel.DataEntity>>(url, dic);
            if (result.succFlag == true && result.data.resultCode == 0)
            {
                code = 1;
                UpdateUserInfo();
            }
            else
            {
                code = -1;
                rtnMsg = result.msg;
            }

            return Json(new { Code = code, Msg = rtnMsg });
        }

        public JsonResult User_AddressUpdate(int id, string consignee, string address, string mpno, short isdefault)
        {
            int code = 0;
            string rtnMsg = "";

            var user = GetUserInfo();

            if (id == 0)
            {
                string url = AppConfig.AppService + "/userAddress/addUserAddress.do";
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("userId", user.UserId);
                dic.Add("consigneeName", consignee);
                dic.Add("consigneeMpNo", mpno);
                dic.Add("address", address);
                dic.Add("defaultFlag", isdefault == 1 ? "true" : "false");

                var result = UtilityFunc.GetPostResponse<LazyAtHome.Web.WeiXin4.Models.EntityModel.resultEntity<LazyAtHome.Web.WeiXin4.Models.EntityModel.AdressInfo>>(url, dic);
                if (result.succFlag == true && result.data != null)
                {
                    code = 1;
                    UpdateUserInfo();
                }
                else
                {
                    code = -1;
                    rtnMsg =result.msg;
                }
            }
            else
            {
                string url = AppConfig.AppService + "/userAddress/updateUserAddress.do";
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("id", id.ToString());
                dic.Add("userId", user.UserId);
                dic.Add("consigneeName", consignee);
                dic.Add("consigneeMpNo", mpno);
                dic.Add("address", address);
                dic.Add("defaultFlag", isdefault == 1 ? "true" : "false");

                var result = UtilityFunc.GetPostResponse<LazyAtHome.Web.WeiXin4.Models.EntityModel.resultEntity<LazyAtHome.Web.WeiXin4.Models.EntityModel.AdressInfo>>(url, dic);
                if (result.succFlag == true && result.data != null)
                {
                    code = 1;
                    UpdateUserInfo();
                }
                else
                {
                    code = -1;
                    rtnMsg = result.msg;
                }
            }

            string callbackUrl = "";
            if (code == 1 && HttpContext.Cache[AppConfig.Cache_CallbackUrl + _UserOpenID] != null)
                callbackUrl = HttpContext.Cache[AppConfig.Cache_CallbackUrl + _UserOpenID].ToString();

            return Json(new { Code = code, Msg = rtnMsg, CallBack = callbackUrl });
        }

    }
}