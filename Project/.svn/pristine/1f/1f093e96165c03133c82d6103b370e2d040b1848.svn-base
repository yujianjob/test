using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LazyAtHome.Core.Web.Extension;
using LazyAtHome.WCF.Wash.Contract.DataContract;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Web.StoreManage.CodeSource.Proxy;

namespace LazyAtHome.Web.StoreManage.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //获取登录信息
            Wash_StoreLoginInfoDC item = CodeSource.SiteSession.OperatorInfo;
            //未登录返回登陆页
            if (item == null)
            {
                return View("Login");
            }
            return View();
            
        }

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
        /// <param name="iStoreCode"></param>
        /// <param name="iLoginName"></param>
        /// <param name="iPassWord"></param>
        /// <param name="iValidateCode"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LoginUser(string iStoreCode, string iLoginName, string iPassWord, string iValidateCode)
        {
            //string loginname = iLoginName;
            //string password = iPassWord;

            if (iValidateCode != Convert.ToString(Session["ValidateCode"]))
            {
                return Json(new { success = false, message = "验证码输入有误，请重新输入！" });
            }

            var LoginRes = OperatorProxy.Login(iStoreCode, iLoginName, iPassWord);
            if (LoginRes != null)
            {
                CodeSource.SiteSession.OperatorInfo = LoginRes;
                if (IsInitPassword(iPassWord))
                {
                    CodeSource.SiteSession.InitPassword = true;
                }
                else
                {
                    CodeSource.SiteSession.InitPassword = false;
                }
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





        /// <summary>
        /// 检查密码是否是初始密码
        /// </summary>
        /// <param name="Password"></param>
        /// <returns></returns>
        private bool IsInitPassword(string Password)
        {
            return Password == "123456" ? true : false;

        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}