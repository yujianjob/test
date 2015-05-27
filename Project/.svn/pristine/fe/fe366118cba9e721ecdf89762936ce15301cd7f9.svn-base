using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.UserSystem.Contract.ClientProxy;
using LazyAtHome.WCF.UserSystem.Contract.DataContract;
using LazyAtHome.WCF.UserSystem.Contract.Enumerate;

namespace LazyAtHome.Web.Site
{
    public class UserProxy
    {
        public static BaseClient c;

        public static void aaa()
        {
            //UserProxy.c.Proxy.us
        }

        /// <summary>
        /// 检测用户名类型
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static LoginType CheckLoginType(string userName)
        {
            LoginType logType = LoginType.LoginName;
            if (LazyValidator.IsEmail(userName))
                logType = LoginType.Email;
            else if (LazyValidator.IsMobile(userName))
                logType = LoginType.MPNo;
            return logType;
        }

        /// <summary>
        /// 获取用户详细信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static ReturnEntity<User_DetailDC> Select_UserDetail(Guid userID)
        {
            var rtn = WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<User_DetailDC>>(c => c.Proxy.User_Detail_SELECT_Entity(userID));
            return rtn;
        }

        public static ReturnEntity<bool> Update_UserDetail(User_DetailDC userDetail)
        {
            var rtn = WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<bool>>(c => c.Proxy.User_Detail_UPDATE(userDetail));
            return rtn;
        }

        /// <summary>
        /// 检查用户名是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> UserExistCheck(string userName)
        {
            LoginType logType = CheckLoginType(userName);

            var rtn = WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<bool>>(c => c.Proxy.User_Exist_Check(userName, logType));
            return rtn;
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ReturnEntity<User_InfoDC> Register(Areas.Member.Models.RegisterViewModel model)
        {
            LoginType logType = CheckLoginType(model.UserName);

            var rtn = WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<User_InfoDC>>(c => c.Proxy.User_Reg_Web(model.UserName, logType, model.Password, "", model.LoginIP, 1));
            return rtn;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ReturnEntity<User_InfoDC> Login(Areas.Member.Models.LoginViewModel model)
        {
            LoginType logType = CheckLoginType(model.UserName);

            var rtn = WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<User_InfoDC>>(c => c.Proxy.User_Login_Web(model.UserName, logType, model.Password, model.LoginIP));
            return rtn;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="oldPass"></param>
        /// <param name="newPass"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> Update_ChangePassword(Guid userID, string oldPass, string newPass)
        {
            var rtn = WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<bool>>(c => c.Proxy.User_LoginPassword_Change(userID, oldPass, newPass));
            return rtn;
        }
    }
}