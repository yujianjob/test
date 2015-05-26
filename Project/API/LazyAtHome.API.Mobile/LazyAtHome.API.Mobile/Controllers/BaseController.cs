using LazyAtHome.API.Mobile.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace LazyAtHome.API.Mobile.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            System.Collections.Specialized.NameValueCollection _Params = null;
            if (filterContext.RequestContext.HttpContext.Request.RequestType == "GET")
                _Params = filterContext.RequestContext.HttpContext.Request.QueryString;
            else
                _Params = filterContext.RequestContext.HttpContext.Request.Form;

            //记录日志
            ParamsLog(filterContext.RequestContext.HttpContext.Request.Path + " " + filterContext.RequestContext.HttpContext.Request.RequestType, _Params.ToString());

            //检查参数
            if (!CheckParams(_Params))
            {
                filterContext.Result = MyJson(Models.JsonResultModels.BaseResult.ParametersError);
                return;
            }

            //检查vkey
            if (!CheckSign(_Params))
            {
                filterContext.Result = MyJson(Models.JsonResultModels.BaseResult.MD5Error);
                return;
            }
        }

        //protected override void OnResultExecuted(ResultExecutedContext filterContext)
        //{
        //    base.OnResultExecuted(filterContext);
        //}

        //protected override void OnResultExecuting(ResultExecutingContext filterContext)
        //{
        //    //var t = (filterContext.Result as JsonResult).Data.ToString();

        //    //PublicFun.LogAdd(t);

        //    //base.OnResultExecuting(filterContext);
        //}


        //记录日志
        private void ParamsLog(string path, string logContent)
        {
            PublicFun.LogAdd(path + ": " + logContent);
        }

        //检查参数
        private bool CheckParams(System.Collections.Specialized.NameValueCollection paramList)
        {
            var paramsKeys = paramList.AllKeys;
            if (!paramsKeys.Contains("actid") || !paramsKeys.Contains("version")
                || !paramsKeys.Contains("ts") || !paramsKeys.Contains("vkey"))
            {
                return false;
            }

            return true;
        }

        //检查vkey
        private bool CheckSign(System.Collections.Specialized.NameValueCollection paramList)
        {
            var paramsKeys = paramList.AllKeys;

            var paramsvalue = string.Empty;
            var vkey = string.Empty;
            foreach (var item in paramsKeys)
            {
                if (item == null) continue;
                if (item.Contains("vkey"))
                {
                    vkey = paramList[item];
                    continue;
                }
                paramsvalue += paramList[item];
            }
            //第一步转大写
            paramsvalue = paramsvalue.ToUpper();
            //MD5
            paramsvalue = LazyAtHome.Core.Helper.CryptoHelper.MD5Encrypt(paramsvalue).ToUpper();
            paramsvalue += APPConfig.Key;

            paramsvalue = LazyAtHome.Core.Helper.CryptoHelper.MD5Encrypt(paramsvalue).ToUpper();

            if (paramsvalue != vkey)
            {
                PublicFun.LogAdd("vkey计算失败:" + vkey + " 服务器计算值:" + paramsvalue);
                return false;
            }

            return true;
        }

        //private string GetSignString(System.Collections.Specialized.NameValueCollection paramList)
        //{
        //    //if (!paramList.ContainsKey("actid") || !paramList.ContainsKey("version") || !paramList.ContainsKey("ts") || !paramList.ContainsKey("vkey"))            
        //    //    return null;

        //    //string actid = paramList["actid"] as string;
        //    //string version = paramList["version"] as string;
        //    //string ts = paramList["ts"] as string;
        //    //string vkey = paramList["vkey"] as string;

        //    //string param = "";
        //    //foreach (string keyName in paramList.Keys)
        //    //{
        //    //    if (keyName != "actid" && keyName != "version" && keyName != "ts" && keyName != "vkey")
        //    //        param += paramList[keyName];
        //    //}

        //    //return param;
        //}

        //private bool check(string str)
        //{
        //    return true;
        //}

        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <returns></returns>
        public static string getIPAddress()
        {
            string result = String.Empty;

            result = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            // 如果使用代理，获取真实IP 
            if (result != null && result.IndexOf(".") == -1)    //没有“.”肯定是非IPv4格式 
                result = null;
            else if (result != null)
            {
                if (result.IndexOf(",") != -1)
                {
                    //有“,”，估计多个代理。取第一个不是内网的IP。 
                    result = result.Replace(" ", "").Replace("'", "");
                    string[] temparyip = result.Split(",;".ToCharArray());
                    for (int i = 0; i < temparyip.Length; i++)
                    {
                        if (IsIPAddress(temparyip[i])
                            && temparyip[i].Substring(0, 3) != "10."
                            && temparyip[i].Substring(0, 7) != "192.168"
                            && temparyip[i].Substring(0, 7) != "172.16.")
                        {
                            return temparyip[i];    //找到不是内网的地址 
                        }
                    }
                }
                else if (IsIPAddress(result)) //代理即是IP格式 
                    return result;
                else
                    result = null;    //代理中的内容 非IP，取IP 
            }
            if (null == result || result == String.Empty)
                result = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            if (result == null || result == String.Empty)
                result = System.Web.HttpContext.Current.Request.UserHostAddress;

            return result;
        }

        /// <summary>
        /// 判断是否是IP地址格式 0.0.0.0
        /// </summary>
        /// <param name="str1">待判断的IP地址</param>
        /// <returns>true or false</returns>
        private static bool IsIPAddress(string str1)
        {
            if (str1 == null || str1 == string.Empty || str1.Length < 7 || str1.Length > 15) return false;

            string regformat = @"^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}$";

            Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);
            return regex.IsMatch(str1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected JsonResult MyJson(object obj)
        {
#if DEBUG
            return Json(obj, JsonRequestBehavior.AllowGet);
#else
            //return Json(obj, JsonRequestBehavior.AllowGet);
            return Json(obj);
#endif
        }

    }
}