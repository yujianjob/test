using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LazyAtHome.Core.Web.Extension;
using LazyAtHome.Web.Site.Areas.Member.Models;
using LazyAtHome.WCF.UserSystem.Contract.DataContract;


namespace LazyAtHome.Web.Site.Areas.Member.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /Member/User/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel register)
        {
            if (TempData["VerificationCode"] == null || TempData["VerificationCode"].ToString() != register.VerificationCode.ToUpper())
            {
                ModelState.AddModelError("VerificationCode", "验证码不正确");
                return View(register);
            }
            if (ModelState.IsValid)
            {
                var _check = UserProxy.UserExistCheck(register.UserName);
                if (!_check.Success)
                {
                    ModelState.AddModelError("UserName", "用户名已存在");
                    return View(register);
                }

                register.LoginIP = Request.UserHostAddress;

                var _user = UserProxy.Register(register);
                if (_user.Success)
                {
                    ModelState.AddModelError("", "用户注册成功");
                }
                else
                {
                    ModelState.AddModelError("", _user.Message);
                }
            }
            return View(register);
        }

        public ActionResult RegisterSuccess()
        {
            return View();
        }

        public ActionResult Login()
        {
            if (HttpContext.Request.UrlReferrer != null)
                ViewBag.ReturnUrl = HttpContext.Request.UrlReferrer.ToString();
            return View();
        }

        public ActionResult Logout()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            return Redirect(Url.Content("~/"));
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel, string ReturnUrl)
        {
            if (TempData["VerificationCode"] == null || TempData["VerificationCode"].ToString() != loginViewModel.VerificationCode.ToUpper())
            {
                ModelState.AddModelError("VerificationCode", "验证码不正确");
                return View(loginViewModel);
            }

            if (ModelState.IsValid)
            {
                loginViewModel.LoginIP = Request.UserHostAddress;

                var _userQuery = UserProxy.Login(loginViewModel);
                if (_userQuery.Success)
                {
                    Session[SessionConfig.UserInfo] = _userQuery.OBJ;

                    System.Web.Security.FormsAuthentication.SetAuthCookie(_userQuery.OBJ.LoginName, false);
                    return Redirect(ReturnUrl);
                }
                else
                {
                    switch (_userQuery.Code)
                    {
                        case -1:
                            ModelState.AddModelError("UserName", "用户不存在");
                            break;
                        case -2:
                            ModelState.AddModelError("", "用户名或密码错误");
                            break;
                    }
                }
            }
            return View(loginViewModel);
        }


        /// <summary>
        /// 生成验证码图像对象
        /// </summary>
        /// <returns></returns>
        public ActionResult VerificationCode()
        {
            ValidateCode vCode = new ValidateCode();
            string code = vCode.CreateValidateCode(5);
            TempData["VerificationCode"] = code;
            byte[] bytes = vCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }

        //[Authorize]
        //public ActionResult Details(User_DetailDC UserDetail)
        //{
        //    User_InfoDC _user = (User_InfoDC)Session[SessionConfig.UserInfo];

        //    User_DetailDC _detail = (User_DetailDC)Session[SessionConfig.UserDetail];
        //    if (_detail == null)
        //        _detail = UserProxy.Select_UserDetail(_user.ID).OBJ;

        //    UserDetailViewModel model = new UserDetailViewModel();
        //    model.UserInfo = _user;
        //    model.UserDetail = _detail;
        //    return View(model);
        //}

        public ActionResult Details(User_DetailDC UserDetail)
        {
            User_InfoDC _user = (User_InfoDC)Session[SessionConfig.UserInfo];
            User_DetailDC _detail = (User_DetailDC)Session[SessionConfig.UserDetail];
            if (_detail == null)
                _detail = UserProxy.Select_UserDetail(_user.ID).OBJ;

            if (UserDetail.NickName != null)
            {
                _detail.NickName = UserDetail.NickName;
                _detail.Birthday = UserDetail.Birthday;
                _detail.IDCard = UserDetail.IDCard;
                _detail.Sex = UserDetail.Sex;
                _detail.Hobbies = UserDetail.Hobbies;
                _detail.RealName = UserDetail.RealName;

                if (ModelState.IsValid)
                {
                    var rtn = UserProxy.Update_UserDetail(_detail);
                    if (rtn.Success)
                    {
                        ModelState.AddModelError("", "修改用户资料成功");
                    }
                    else
                    {
                        ModelState.AddModelError("", rtn.Message);
                    }
                }
            }

            UserDetailViewModel model = new UserDetailViewModel();
            model.UserInfo = _user;
            model.UserDetail = _detail;
            return View(model);
        }
        
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }
        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel passwordViewModel)
        {
            if (ModelState.IsValid)
            {
                User_InfoDC _user = (User_InfoDC)Session[SessionConfig.UserInfo];
                if (_user == null)
                {
                    return RedirectToAction("login");
                }
                else
                {
                    var rtn = UserProxy.Update_ChangePassword(_user.ID, passwordViewModel.OriginalPassword, passwordViewModel.Password);
                    if (!rtn.Success)
                        ModelState.AddModelError("", rtn.Message);
                    else
                    {
                        ModelState.AddModelError("", "修改密码成功");
                    }                    
                }
            }
            return View(passwordViewModel);
        }
	}
}