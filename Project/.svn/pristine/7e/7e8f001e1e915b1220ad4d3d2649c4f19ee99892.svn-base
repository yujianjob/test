using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LazyAtHome.Core.Web.Extension;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;

namespace LazyAtHome.Web.WebManage.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            //获取登录信息
            OperatorDC item = CodeSource.SiteSession.OperatorInfo;
            //未登录返回登陆页
            if (item == null)
            {
                return View("Login");
            }
            return View();
        }

        //public ActionResult Main()
        //{
        //    return View();
        //}

        /// <summary>
        /// 登陆页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="iLoginName"></param>
        /// <param name="iPassWord"></param>
        /// <param name="iValidateCode"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LoginUser(string iLoginName, string iPassWord, string iValidateCode)
        {
            string loginname = iLoginName;
            string password = iPassWord;

            if (iValidateCode != Convert.ToString(Session["ValidateCode"]))
            {
                return Json(new { success = false, message = "验证码输入有误，请重新输入！" });
            }

            var LoginRes = CodeSource.Proxy.OperatorProxy.Login(loginname, password);
            if (LoginRes != null)
            {
                CodeSource.SiteSession.OperatorInfo = LoginRes;
                return Json(new { success = true, message = "登录成功" });
            }
            else
            {
                return Json(new { success = false, message = "用户名或密码不正确，请重新输入！" });
            }

        }

        /// <summary>
        /// 生成验证码图像对象
        /// </summary>
        /// <returns></returns>
        public ActionResult GetValidateCode()
        {
            ValidateCode vCode = new ValidateCode();
            string code = vCode.CreateValidateCode(5);
            Session["ValidateCode"] = code;
            byte[] bytes = vCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }


        //public ActionResult login_dialog()
        //{
        //    return View();
        //}

        //public ActionResult Test()
        //{
        //    return View();
        //}
    }
}