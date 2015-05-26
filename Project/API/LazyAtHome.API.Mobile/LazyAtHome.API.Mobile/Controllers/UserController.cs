using LazyAtHome.API.Mobile.App_Code;
using LazyAtHome.API.Mobile.App_Code.Proxy;
using LazyAtHome.API.Mobile.Models.JsonResultModels;
using LazyAtHome.API.Mobile.Models.LocalModels;
using LazyAtHome.WCF.UserSystem.Contract.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LazyAtHome.API.Mobile.Controllers
{
    public partial class HomeController : BaseController
    {
        /// <summary>
        /// 4.1	用户登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="mobiletype"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public JsonResult Login(string username, string password, int mobiletype, string flag, string version)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return MyJson(BaseResult.BadResult(-1, "登录名不能为空"));
            }
            else if (string.IsNullOrWhiteSpace(password))
            {
                return MyJson(BaseResult.BadResult(-1, "密码不能为空"));
            }
            else if (string.IsNullOrWhiteSpace(flag))
            {
                return MyJson(BaseResult.BadResult(-1, "手机识别号为空"));
            }

            username = PublicFun.Des3DecodeECB(username);
            if (string.IsNullOrWhiteSpace(username))
            {
                return MyJson(BaseResult.BadResult(-1, "数据解密错误"));
            }
            password = PublicFun.Des3DecodeECB(password);
            if (string.IsNullOrWhiteSpace(password))
            {
                return MyJson(BaseResult.BadResult(-1, "数据解密错误"));
            }

            var loginip = getIPAddress();

            try
            {
                //获取用户
                var userRtn = LazyAtHome.API.Mobile.App_Code.Proxy.UserProxy.User_Login_App(
                    username, PublicFun.GetLoginNameType(username), password, loginip, version, mobiletype, flag);
                if (userRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (userRtn.Success == false)
                {
                    if (userRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(userRtn.Code, userRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(userRtn.Code, userRtn.Message));
                    }
                }
                else if (userRtn.OBJ == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else
                {
                    //生成对象
                    var user = new LoginResult()
                    {
                        user = new Models.LocalModels.UserModel()
                        {
                            cid = userRtn.OBJ.ID.ToString(),
                            username = userRtn.OBJ.LoginName,
                            email = userRtn.OBJ.Email,
                            mobile = userRtn.OBJ.MPNo,
                            money = Convert.ToInt32(userRtn.OBJ.Money * 100),
                        }
                    };

                    //返回
                    return MyJson(user);
                }
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        /// <summary>
        /// 4.2	用户名有效性检查
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public JsonResult UserNameCheck(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return Json(BaseResult.BadResult(-1, "登录名不能为空"));
            }
            username = PublicFun.Des3DecodeECB(username);
            if (string.IsNullOrWhiteSpace(username))
            {
                return MyJson(BaseResult.BadResult(-1, "数据解密错误"));
            }

            var loginip = getIPAddress();

            try
            {
                //获取用户
                var userRtn = LazyAtHome.API.Mobile.App_Code.Proxy.UserProxy.User_Exist_Check(
                    username, PublicFun.GetLoginNameType(username));
                if (userRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (!userRtn.Success)
                {
                    if (userRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(userRtn.Code, userRtn.Message));
                    }
                    else
                    {
                        //用户已存在,不可以注册
                        return MyJson(new UserNameCheckResult() { flag = 1 });
                    }
                }
                else
                {
                    //用户不存在,可以注册
                    return MyJson(new UserNameCheckResult() { flag = 0 });
                }
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        /// <summary>
        /// 4.3	用户注册
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="mobiletype"></param>
        /// <param name="flag"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public JsonResult UserReg(string username, string password, int mobiletype, string flag, string version)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return MyJson(BaseResult.BadResult(-1, "登录名不能为空"));
            }
            else if (string.IsNullOrWhiteSpace(password))
            {
                return MyJson(BaseResult.BadResult(-1, "密码不能为空"));
            }
            else if (string.IsNullOrWhiteSpace(flag))
            {
                return MyJson(BaseResult.BadResult(-1, "手机识别号为空"));
            }
            username = PublicFun.Des3DecodeECB(username);
            if (string.IsNullOrWhiteSpace(username))
            {
                return MyJson(BaseResult.BadResult(-1, "数据解密错误"));
            }
            password = PublicFun.Des3DecodeECB(password);
            if (string.IsNullOrWhiteSpace(password))
            {
                return MyJson(BaseResult.BadResult(-1, "数据解密错误"));
            }

            var loginip = getIPAddress();

            try
            {
                var regType = PublicFun.GetLoginNameType(username);
                if (regType == WCF.UserSystem.Contract.Enumerate.LoginType.MPNo)
                {
                    return MyJson(BaseResult.BadResult(-1, "注册帐号不能为纯数字"));
                }
                //获取用户
                var userRtn = LazyAtHome.API.Mobile.App_Code.Proxy.UserProxy.User_Reg_App(
                    username, regType, password, null, loginip, version, mobiletype, flag);
                if (userRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (userRtn.Success == false)
                {
                    if (userRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(userRtn.Code, userRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(userRtn.Code, userRtn.Message));
                    }
                }
                else if (userRtn.OBJ == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else
                {
                    //生成对象
                    var user = new LoginResult()
                    {
                        user = new Models.LocalModels.UserModel()
                        {
                            cid = userRtn.OBJ.ID.ToString(),
                            username = userRtn.OBJ.LoginName,
                            email = userRtn.OBJ.Email,
                            mobile = userRtn.OBJ.MPNo,
                            money = Convert.ToInt32(userRtn.OBJ.Money * 100),
                        }
                    };

                    //返回
                    return MyJson(user);
                }
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        const string CheckMessage = "尊敬的用户您好，为了保证您的安全，本次操作您的验证码为{0}，请您在5分钟之内完成验证。";

        /// <summary>
        /// 4.4	发送验证码短信
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public JsonResult SendCheckCode(string mobile, int type)
        {
            if (string.IsNullOrWhiteSpace(mobile))
            {
                return MyJson(BaseResult.BadResult(-1, "手机号不能为空"));
            }

            //1:绑定手机 2:忘记密码
            if (type != 1 && type != 2)
            {
                return MyJson(BaseResult.ParametersError);
            }

            SendCheckCodeResult rtn = new SendCheckCodeResult();

            var checkCode = GetCheckCode(mobile, type);

            //测试返回
            //rtn.checkcode = checkCode;
            rtn.checkcode = string.Empty;

            var message = string.Format(CheckMessage, checkCode);

            var sendRtn = CommonProxy.Base_SmsSend_Create(mobile, message);
            if (sendRtn != null && sendRtn.Success)
            {
                return MyJson(rtn);
            }
            else
            {
                return MyJson(BaseResult.SystemError(-1, "短信发送失败"));
            }
        }

        //生成验证码
        private string GetCheckCode(string mobile, int type)
        {
            var cache_key = "APP_CheckCode_" + mobile + "_" + type;

            var checkCode = CommonProxy.Cache_GetByKey(cache_key) as string;

            if (string.IsNullOrEmpty(checkCode))
            {
                checkCode = LazyAtHome.API.Mobile.App_Code.PublicFun.rd.Next(100000, 999999).ToString().PadLeft(6, '0');

                CommonProxy.Cache_Put(cache_key, checkCode, new TimeSpan(0, 5, 0));

                return checkCode;
            }
            else
            {
                return checkCode;
            }
        }

        /// <summary>
        /// 4.5	设置新密码
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="checkcode"></param>
        /// <param name="password"></param>
        /// <param name="mobiletype"></param>
        /// <param name="flag"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public JsonResult SetNewPassword(string mobile, string checkcode, string password, int mobiletype, string flag, string version)
        {
            if (string.IsNullOrWhiteSpace(mobile))
            {
                return MyJson(BaseResult.BadResult(-1, "手机号不能为空"));
            }
            if (string.IsNullOrWhiteSpace(checkcode))
            {
                return MyJson(BaseResult.BadResult(-1, "验证码不能为空"));
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                return MyJson(BaseResult.BadResult(-1, "新密码不能为空"));
            }
            if (password.Length < 6 && password.Length > 16)
            {
                return MyJson(BaseResult.BadResult(-1, "新密码长度错误"));
            }

            var dec_password = PublicFun.Des3DecodeECB(password);
            if (string.IsNullOrWhiteSpace(dec_password))
            {
                return MyJson(BaseResult.BadResult(-1, "数据解密错误"));
            }

            var cacheKey = "APP_CheckCode_" + mobile + "_" + 2;

            var cacheCheckCode = CommonProxy.Cache_GetByKey(cacheKey) as string;

            if (cacheCheckCode != checkcode)
            {
                return MyJson(BaseResult.BadResult(-1, "验证码错误"));
            }

            try
            {
                //获取用户
                var rtn = LazyAtHome.API.Mobile.App_Code.Proxy.UserProxy.User_LoginPassword_Reset(mobile, dec_password);
                if (rtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (rtn.Success == false)
                {
                    if (rtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(rtn.Code, rtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(rtn.Code, rtn.Message));
                    }
                }
                else if (rtn.OBJ == true)
                {
                    //移除缓存中的验证码
                    CommonProxy.Cache_Remove(cacheKey);
                    //修改密码成功,跳转登录
                    mobile = PublicFun.Des3EncodeECB(mobile);
                    return Login(mobile, password, mobiletype, flag, version);
                }
                else
                {
                    //修改密码失败
                    return MyJson(BaseResult.BadResult(-1, "修改密码失败"));
                }
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        /// <summary>
        /// 4.12	用户默认地址获取
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public JsonResult DefaultAdds(string cid)
        {
            if (string.IsNullOrWhiteSpace(cid))
            {
                return MyJson(BaseResult.ParametersError);
            }
            Guid userid = Guid.Empty;

            if (!Guid.TryParse(cid, out userid))
            {
                return MyJson(BaseResult.ParametersError);
            }

            try
            {
                var addsRtn = UserProxy.User_ConsigneeAddress_SELECT_List(userid);
                if (addsRtn == null || addsRtn.OBJ == null)
                {
                    return MyJson(BaseResult.BadResult(-1, "获取地址失败"));
                }
                DefaultAddsResult rtn = new DefaultAddsResult();

                //取第一条记录
                if (addsRtn.OBJ.Count >= 1)
                {
                    rtn.address = new Models.LocalModels.UserAddsModel()
                    {
                        id = addsRtn.OBJ[0].ID,
                        name = addsRtn.OBJ[0].Consignee,
                        phone = addsRtn.OBJ[0].MPNo,
                        districtid = addsRtn.OBJ[0].DistrictID,
                        districtname = addsRtn.OBJ[0].DistrictName,
                        adds = addsRtn.OBJ[0].Address,
                        @default = addsRtn.OBJ[0].IsDefault,
                    };

                }

                //返回
                return MyJson(rtn);

            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        /// <summary>
        /// 4.18	获取用户信息
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public JsonResult UserGet(string cid)
        {
            if (string.IsNullOrWhiteSpace(cid))
            {
                return MyJson(BaseResult.ParametersError);
            }
            Guid userid = Guid.Empty;

            if (!Guid.TryParse(cid, out userid))
            {
                return MyJson(BaseResult.ParametersError);
            }

            try
            {
                var userRtn = UserProxy.User_Info_SELECT_Entity(userid);
                if (userRtn == null || userRtn.OBJ == null)
                {
                    return MyJson(BaseResult.BadResult(-1, "获取用户失败"));
                }

                //生成对象
                var rtn = new LoginResult()
                {
                    user = new Models.LocalModels.UserModel()
                    {
                        cid = userRtn.OBJ.ID.ToString(),
                        username = userRtn.OBJ.LoginName,
                        email = userRtn.OBJ.Email,
                        mobile = userRtn.OBJ.MPNo,
                        money = Convert.ToInt32(userRtn.OBJ.Money * 100),
                    }
                };

                //返回
                return MyJson(rtn);

            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        /// <summary>
        /// 4.19	保存用户信息
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public JsonResult UserEdit(string cid, string username, string email)
        {
            if (!string.IsNullOrWhiteSpace(username))
            {
                username = PublicFun.Des3DecodeECB(username);
                if (string.IsNullOrWhiteSpace(username))
                {
                    return MyJson(BaseResult.BadResult(-1, "数据解密错误"));
                }
            }

            if (string.IsNullOrWhiteSpace(cid))
            {
                return MyJson(BaseResult.ParametersError);
            }
            Guid userid = Guid.Empty;

            if (!Guid.TryParse(cid, out userid))
            {
                return MyJson(BaseResult.ParametersError);
            }

            try
            {
                var userRtn = UserProxy.User_Info_SELECT_Entity(userid);
                if (userRtn == null || userRtn.OBJ == null)
                {
                    return MyJson(BaseResult.BadResult(-1, "用户不存在"));
                }
                if (!string.IsNullOrWhiteSpace(userRtn.OBJ.LoginName) && string.IsNullOrWhiteSpace(username))
                {
                    return MyJson(BaseResult.BadResult(-1, "用户名不能为空"));
                }
                if (!string.IsNullOrWhiteSpace(userRtn.OBJ.Email) && string.IsNullOrWhiteSpace(email))
                {
                    return MyJson(BaseResult.BadResult(-1, "邮箱不能为空"));
                }

                if (!string.IsNullOrWhiteSpace(username) && userRtn.OBJ.LoginName != username)
                {
                    var checkRtn = UserProxy.User_Exist_Check(username, WCF.UserSystem.Contract.Enumerate.LoginType.LoginName, userid);
                    if (checkRtn == null)
                    {
                        return MyJson(BaseResult.EmptyResult);
                    }
                    else if (!checkRtn.Success)
                    {
                        return MyJson(BaseResult.BadResult(-1, "用户名已存在"));
                    }
                }
                if (!string.IsNullOrWhiteSpace(email) && userRtn.OBJ.Email != email)
                {
                    var checkRtn = UserProxy.User_Exist_Check(email, WCF.UserSystem.Contract.Enumerate.LoginType.Email, userid);
                    if (checkRtn == null)
                    {
                        return MyJson(BaseResult.EmptyResult);
                    }
                    else if (!checkRtn.Success)
                    {
                        return MyJson(BaseResult.BadResult(-1, "邮箱已存在"));
                    }
                }

                if (!string.IsNullOrWhiteSpace(username) && userRtn.OBJ.LoginName != username)
                {
                    var changeRtn = UserProxy.User_LoginName_Change(userid, username);
                    if (changeRtn == null)
                    {
                        return MyJson(BaseResult.EmptyResult);
                    }
                    else if (!changeRtn.Success)
                    {
                        if (changeRtn.Code != -999)
                        {
                            return MyJson(BaseResult.BadResult(changeRtn.Code, changeRtn.Message));
                        }
                        else
                        {
                            return MyJson(BaseResult.SystemError(changeRtn.Code, changeRtn.Message));
                        }
                    }
                }
                if (!string.IsNullOrWhiteSpace(email) && userRtn.OBJ.Email != email)
                {
                    var changeRtn = UserProxy.User_Email_Change(userid, email);
                    if (changeRtn == null)
                    {
                        return MyJson(BaseResult.EmptyResult);
                    }
                    else if (!changeRtn.Success)
                    {
                        if (changeRtn.Code != -999)
                        {
                            return MyJson(BaseResult.BadResult(changeRtn.Code, changeRtn.Message));
                        }
                        else
                        {
                            return MyJson(BaseResult.SystemError(changeRtn.Code, changeRtn.Message));
                        }
                    }
                }

                //返回
                return MyJson(BaseResult.Success);

            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        /// <summary>
        /// 4.21	用户注销
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="mobiletype"></param>
        /// <returns></returns>
        public JsonResult Logout(string cid, int mobiletype)
        {
            if (string.IsNullOrWhiteSpace(cid))
            {
                return MyJson(BaseResult.ParametersError);
            }
            Guid userid = Guid.Empty;

            if (!Guid.TryParse(cid, out userid))
            {
                return MyJson(BaseResult.ParametersError);
            }

            try
            {
                UserProxy.User_LoginOut(userid, mobiletype);

                return MyJson(BaseResult.Success);
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        /// <summary>
        /// 4.22	检查新手机是否存在
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public JsonResult MobileExist(string cid, string mobile)
        {
            if (string.IsNullOrWhiteSpace(cid))
            {
                return MyJson(BaseResult.ParametersError);
            }
            if (string.IsNullOrWhiteSpace(mobile))
            {
                return MyJson(BaseResult.BadResult(-1, "手机号不能为空"));
            }

            Guid userid = Guid.Empty;

            if (!Guid.TryParse(cid, out userid))
            {
                return MyJson(BaseResult.ParametersError);
            }
            try
            {
                var checkRtn = UserProxy.User_Exist_Check(mobile, WCF.UserSystem.Contract.Enumerate.LoginType.MPNo);
                if (checkRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (!checkRtn.Success)
                {
                    if (checkRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(checkRtn.Code, checkRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(checkRtn.Code, checkRtn.Message));
                    }
                }
                else if (checkRtn.OBJ == true)
                {
                    return MyJson(new MobileExistResult() { flag = 1 });
                }
                else
                {
                    return MyJson(new MobileExistResult() { flag = 0 });
                }
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }

        }

        /// <summary>
        /// 4.23	检查手机号与验证码匹配
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="mobile"></param>
        /// <param name="checkcode"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public JsonResult MoblieCodeCheck(string cid, string mobile, string checkcode, int type)
        {
            if (string.IsNullOrWhiteSpace(cid))
            {
                return MyJson(BaseResult.ParametersError);
            }
            if (string.IsNullOrWhiteSpace(checkcode))
            {
                return MyJson(BaseResult.BadResult(-1, "验证码不能为空"));
            }
            if (string.IsNullOrWhiteSpace(mobile))
            {
                return MyJson(BaseResult.BadResult(-1, "手机号不能为空"));
            }

            Guid userid = Guid.Empty;

            if (!Guid.TryParse(cid, out userid))
            {
                return MyJson(BaseResult.ParametersError);
            }

            var cacheCheckCode = CommonProxy.Cache_GetByKey("APP_CheckCode_" + mobile + "_" + type) as string;

            if (cacheCheckCode != checkcode)
            {
                return MyJson(new MoblieCodeCheckResult() { flag = 1 });
            }
            else
            {
                return MyJson(new MoblieCodeCheckResult() { flag = 0 });
            }
        }

        /// <summary>
        /// 4.24	改绑新手机
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="oldmobile"></param>
        /// <param name="newmobile"></param>
        /// <param name="oldcheckcode"></param>
        /// <param name="newcheckcode"></param>
        /// <returns></returns>
        public JsonResult MoblieChange(string cid, string oldmobile, string newmobile, string oldcheckcode, string newcheckcode)
        {
            if (string.IsNullOrWhiteSpace(cid))
            {
                return MyJson(BaseResult.ParametersError);
            }

            if (string.IsNullOrWhiteSpace(newmobile))
            {
                return MyJson(BaseResult.BadResult(-1, "手机号不能为空"));
            }
            if (oldmobile == newmobile)
            {
                return MyJson(BaseResult.BadResult(-1, "手机号不能相同"));
            }
            //if (string.IsNullOrWhiteSpace(oldcheckcode))
            //{
            //    return MyJson(BaseResult.BadResult(-1, "验证码不能为空"));
            //}
            if (string.IsNullOrWhiteSpace(newcheckcode))
            {
                return MyJson(BaseResult.BadResult(-1, "验证码不能为空"));
            }

            Guid userid = Guid.Empty;

            if (!Guid.TryParse(cid, out userid))
            {
                return MyJson(BaseResult.ParametersError);
            }

            var cacheCheckCode = CommonProxy.Cache_GetByKey("APP_CheckCode_" + newmobile + "_" + 1) as string;

            if (cacheCheckCode != newcheckcode)
            {
                return MyJson(BaseResult.BadResult(-1, "新手机号验证码错误"));
            }
            //if (!string.IsNullOrWhiteSpace(oldmobile))
            //{
            //    cacheCheckCode = CommonProxy.Cache_GetByKey("APP_CheckCode_" + oldmobile + "_" + 1) as string;

            //    if (cacheCheckCode != oldcheckcode)
            //    {
            //        return MyJson(BaseResult.BadResult(-1, "原手机号验证码错误"));
            //    }
            //}

            try
            {
                var userRtn = UserProxy.User_Info_SELECT_Entity(userid);
                if (userRtn == null || userRtn.OBJ == null)
                {
                    return MyJson(BaseResult.BadResult(-1, "用户不存在"));
                }
                if (!string.IsNullOrWhiteSpace(userRtn.OBJ.MPNo) && userRtn.OBJ.MPNo != oldmobile)
                {
                    return MyJson(BaseResult.BadResult(-1, "原手机号错误"));
                }

                var updateRtn = UserProxy.User_MPNo_Change(userid, newmobile);
                if (updateRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (!updateRtn.Success)
                {
                    if (updateRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(updateRtn.Code, updateRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(updateRtn.Code, updateRtn.Message));
                    }
                }

                //移除已使用的验证码
                CommonProxy.Cache_Remove("APP_CheckCode_" + oldmobile + "_" + 1);
                CommonProxy.Cache_Remove("APP_CheckCode_" + newmobile + "_" + 1);

                //返回
                return MyJson(BaseResult.Success);
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }

        }

        /// <summary>
        /// 4.25	获取用户地址列表
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public JsonResult AddsList(string cid)
        {
            if (string.IsNullOrWhiteSpace(cid))
            {
                return MyJson(BaseResult.ParametersError);
            }

            Guid userid = Guid.Empty;

            if (!Guid.TryParse(cid, out userid))
            {
                return MyJson(BaseResult.ParametersError);
            }

            try
            {
                var addsRtn = UserProxy.User_ConsigneeAddress_SELECT_List(userid);
                if (addsRtn == null || addsRtn.OBJ == null)
                {
                    return MyJson(BaseResult.BadResult(-1, "获取地址失败"));
                }
                AddsListResult rtn = new AddsListResult()
                {
                    addresslist = new List<UserAddsModel>()
                };
                foreach (var item in addsRtn.OBJ)
                {
                    rtn.addresslist.Add(new Models.LocalModels.UserAddsModel()
                    {
                        id = item.ID,
                        name = item.Consignee,
                        phone = item.MPNo,
                        districtid = item.DistrictID,
                        districtname = item.DistrictName,
                        adds = item.Address,
                        @default = item.IsDefault,
                    }
                    );
                }

                //返回
                return MyJson(rtn);
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        /// <summary>
        /// 4.27	新增或保存用户地址
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="addsid"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="districtid"></param>
        /// <param name="adds"></param>
        /// <returns></returns>
        public JsonResult AddsEdit(string cid, int addsid, string name, string phone, int districtid, string adds, int @default)
        {
            if (string.IsNullOrWhiteSpace(cid))
            {
                return MyJson(BaseResult.ParametersError);
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                return MyJson(BaseResult.BadResult(-1, "收货人不能为空"));
            }
            if (string.IsNullOrWhiteSpace(phone))
            {
                return MyJson(BaseResult.BadResult(-1, "收货人手机不能为空"));
            }
            if (string.IsNullOrWhiteSpace(adds))
            {
                return MyJson(BaseResult.BadResult(-1, "收货地址不能为空"));
            }

            Guid userid = Guid.Empty;

            if (!Guid.TryParse(cid, out userid))
            {
                return MyJson(BaseResult.ParametersError);
            }

            try
            {
                if (addsid == 0)
                {
                    //新增
                    var addRtn = UserProxy.User_ConsigneeAddress_ADD(new WCF.UserSystem.Contract.DataContract.User_ConsigneeAddressDC
                    {
                        UserID = userid,
                        Consignee = name,
                        MPNo = phone,
                        DistrictID = districtid,
                        Address = adds,
                        IsDefault = @default,
                    });
                    if (addRtn == null || !addRtn.Success)
                    {
                        return MyJson(BaseResult.BadResult(-1, "保存地址失败"));
                    }
                }
                else
                {
                    //修改
                    var addsRtn = UserProxy.User_ConsigneeAddress_SELECT_Entity(addsid);
                    if (addsRtn == null || addsRtn.OBJ == null)
                    {
                        return MyJson(BaseResult.BadResult(-1, "收货地址不存在,无法修改"));
                    }
                    else
                    {
                        if (addsRtn.OBJ.UserID != userid)
                        {
                            return MyJson(BaseResult.BadResult(-1, "非该用户地址,无法修改"));
                        }
                        addsRtn.OBJ.Consignee = name;
                        addsRtn.OBJ.MPNo = phone;
                        addsRtn.OBJ.DistrictID = districtid;
                        addsRtn.OBJ.Address = adds;
                        addsRtn.OBJ.IsDefault = @default;

                        var updateRtn = UserProxy.User_ConsigneeAddress_UPDATE(addsRtn.OBJ);
                        if (updateRtn == null || !updateRtn.Success || !updateRtn.OBJ)
                        {
                            return MyJson(BaseResult.BadResult(-1, "保存地址失败"));
                        }
                    }
                }

                if (@default == 1)
                {

                }

                //返回
                return MyJson(BaseResult.Success);
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }

        }

        /// <summary>
        /// 4.28	删除用户地址
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="addsid"></param>
        /// <returns></returns>
        public JsonResult AddsDel(string cid, int addsid)
        {
            if (string.IsNullOrWhiteSpace(cid))
            {
                return MyJson(BaseResult.ParametersError);
            }

            Guid userid = Guid.Empty;

            if (!Guid.TryParse(cid, out userid))
            {
                return MyJson(BaseResult.ParametersError);
            }

            try
            {
                //
                UserProxy.User_ConsigneeAddress_DELETE(addsid);

                //返回
                return MyJson(BaseResult.Success);
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        /// <summary>
        /// 4.29	获取懒人卡列表
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public JsonResult CardList(string cid)
        {
            if (string.IsNullOrWhiteSpace(cid))
            {
                return MyJson(BaseResult.ParametersError);
            }

            Guid userid = Guid.Empty;

            if (!Guid.TryParse(cid, out userid))
            {
                return MyJson(BaseResult.ParametersError);
            }

            try
            {
                CardListResult rtn = new CardListResult()
                {
                    cardlist = new List<UserCardModel>()
                };
                //
                var cardRtn = UserProxy.User_Card_SELECT_List(userid);
                if (cardRtn == null || cardRtn.Success == false || cardRtn.OBJ == null)
                {
                    return MyJson(BaseResult.BadResult(-1, "获取卡列表失败"));
                }
                else
                {
                    foreach (var item in cardRtn.OBJ)
                    {
                        var tmp = new UserCardModel()
                        {
                            id = item.ID,
                            name = item.Money.ToString("0") + "元懒人卡",
                            no = item.Number,
                            money = Convert.ToInt32(item.Money * 100),
                            balance = Convert.ToInt32(item.Balance * 100),
                        };

                        tmp.loglist = new List<UserCardLogModel>();

                        var cardLogRtn = UserProxy.Base_CardLog_SELECT_List(userid, item.CardID, null, null, 1, 10);
                        if (cardLogRtn != null && cardLogRtn.OBJ != null && cardLogRtn.OBJ.ReturnList != null)
                        {
                            foreach (var itemlog in cardLogRtn.OBJ.ReturnList)
                            {
                                var tmplog = new UserCardLogModel();
                                tmplog.id = itemlog.ID;
                                tmplog.stime = itemlog.Obj_Cdate.Value.ToString("yyyy.MM.dd HH:mm");
                                switch (itemlog.Type)
                                {
                                    case 3://3:使用 4:退单
                                        tmplog.stype = "消费";
                                        tmplog.smoney = itemlog.Money.ToString("0.00");
                                        break;
                                    case 4:
                                        tmplog.stype = "退还";
                                        tmplog.smoney = itemlog.Money.ToString("-0.00");
                                        break;
                                    default: continue;
                                }

                                tmp.loglist.Add(tmplog);
                            }
                        }

                        rtn.cardlist.Add(tmp);
                    }
                }

                //返回
                return MyJson(rtn);
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        /// <summary>
        /// 4.30	添加懒人卡
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public JsonResult CardAdd(string cid, string code)
        {
            if (string.IsNullOrWhiteSpace(cid))
            {
                return MyJson(BaseResult.ParametersError);
            }
            if (string.IsNullOrWhiteSpace(code))
            {
                return MyJson(BaseResult.BadResult(-1, "卡密不能为空"));
            }

            code = code.Trim().Replace("-", "").ToUpper();

            if (code.Length < 16)
            {
                return CouponAdd(cid, code);
            }

            Guid userid = Guid.Empty;

            if (!Guid.TryParse(cid, out userid))
            {
                return MyJson(BaseResult.ParametersError);
            }

            try
            {
                var cardRtn = UserProxy.User_Card_Bind(userid, WCF.UserSystem.Contract.Enumerate.UserCardType.LazyCard, code);
                if (cardRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                if (cardRtn.Success == false)
                {
                    if (cardRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(cardRtn.Code, cardRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(cardRtn.Code, cardRtn.Message));
                    }
                }


                //返回
                return MyJson(BaseResult.Success);
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        /// <summary>
        /// 4.31	获取优惠券列表
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public JsonResult CouponList(string cid)
        {
            if (string.IsNullOrWhiteSpace(cid))
            {
                return MyJson(BaseResult.ParametersError);
            }

            Guid userid = Guid.Empty;

            if (!Guid.TryParse(cid, out userid))
            {
                return MyJson(BaseResult.ParametersError);
            }

            try
            {
                CouponListResult rtn = new CouponListResult()
                {
                    couponlist = new List<UserCouponModel>(),
                    usedcouponlist = new List<UserCouponModel>(),
                    expcouponlist = new List<UserCouponModel>(),
                };
                //未使用
                var couponRtn = UserProxy.User_Coupon_SELECT_List(userid, null, null, LazyAtHome.WCF.UserSystem.Contract.Enumerate.CouponStatus.Normal, null, null, 1, 10);
                if (couponRtn == null || couponRtn.Success == false || couponRtn.OBJ == null || couponRtn.OBJ.ReturnList == null)
                {
                    return MyJson(BaseResult.BadResult(-1, "获取优惠券列表失败"));
                }
                else
                {
                    foreach (var item in couponRtn.OBJ.ReturnList)
                    {
                        rtn.couponlist.Add(new UserCouponModel()
                        {
                            id = item.ID,
                            name = item.Name,
                            money = Convert.ToInt32(item.FaceValue < 0 ? 0 : item.FaceValue * 100),
                            expdate = item.UseEndDate.ToString("yyyy.MM.dd HH:mm:ss"),
                            expflag = (item.UseEndDate - DateTime.Now).Days <= 7 ? 1 : 0,
                        });
                    }
                }
                //已使用
                couponRtn = UserProxy.User_Coupon_SELECT_List(userid, null, null, LazyAtHome.WCF.UserSystem.Contract.Enumerate.CouponStatus.Used, null, null, 1, 10);
                if (couponRtn == null || couponRtn.Success == false || couponRtn.OBJ == null || couponRtn.OBJ.ReturnList == null)
                {
                    return MyJson(BaseResult.BadResult(-1, "获取优惠券列表失败"));
                }
                else
                {
                    foreach (var item in couponRtn.OBJ.ReturnList)
                    {
                        rtn.usedcouponlist.Add(new UserCouponModel()
                        {
                            id = item.ID,
                            name = item.Name,
                            money = Convert.ToInt32(item.FaceValue < 0 ? 0 : item.FaceValue * 100),
                            expdate = item.UseDate.Value.ToString("yyyy.MM.dd HH:mm:ss"),
                            expflag = 0,
                        });
                    }
                }
                //已过期
                couponRtn = UserProxy.User_Coupon_SELECT_List(userid, null, null, LazyAtHome.WCF.UserSystem.Contract.Enumerate.CouponStatus.Expired, null, null, 1, 10);
                if (couponRtn == null || couponRtn.Success == false || couponRtn.OBJ == null || couponRtn.OBJ.ReturnList == null)
                {
                    return MyJson(BaseResult.BadResult(-1, "获取优惠券列表失败"));
                }
                else
                {
                    foreach (var item in couponRtn.OBJ.ReturnList)
                    {
                        rtn.expcouponlist.Add(new UserCouponModel()
                        {
                            id = item.ID,
                            name = item.Name,
                            money = Convert.ToInt32(item.FaceValue < 0 ? 0 : item.FaceValue * 100),
                            expdate = item.UseEndDate.ToString("yyyy.MM.dd HH:mm:ss"),
                            expflag = 0,
                        });
                    }
                }

                //返回
                return MyJson(rtn);
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        /// <summary>
        /// 4.32	添加优惠券
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public JsonResult CouponAdd(string cid, string code)
        {
            if (string.IsNullOrWhiteSpace(cid))
            {
                return MyJson(BaseResult.ParametersError);
            }
            if (string.IsNullOrWhiteSpace(code))
            {
                return MyJson(BaseResult.BadResult(-1, "优惠券密码不能为空"));
            }

            Guid userid = Guid.Empty;

            if (!Guid.TryParse(cid, out userid))
            {
                return MyJson(BaseResult.ParametersError);
            }

            try
            {
                var couponRtn = UserProxy.User_Coupon_Bind(userid, code);
                if (couponRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                if (couponRtn.Success == false)
                {
                    if (couponRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(couponRtn.Code, couponRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(couponRtn.Code, couponRtn.Message));
                    }
                }


                //返回
                return MyJson(BaseResult.Success);
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        /// <summary>
        /// 4.33	用户反馈  
        /// </summary>
        /// <param name="name"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public JsonResult Feedback(string name, string msg)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return MyJson(BaseResult.BadResult(-1, "用户不能为空"));
            }
            if (string.IsNullOrWhiteSpace(msg))
            {
                return MyJson(BaseResult.BadResult(-1, "内容不能为空"));
            }

            try
            {
                UserProxy.User_Complaints_ADD(new User_ComplaintsDC()
                {
                    No = "A" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    UserID = Guid.Empty,
                    Content = msg,
                    Type = 0,
                    MPNo = name,
                });

                //返回
                return MyJson(BaseResult.Success);
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }
    }
}