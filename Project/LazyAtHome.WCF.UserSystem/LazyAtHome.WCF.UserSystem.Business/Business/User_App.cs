using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.UserSystem.Contract.DataContract;
using LazyAtHome.WCF.UserSystem.Contract.Enumerate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.UserSystem.Business.Business
{
    public partial class User
    {
        /// <summary>
        /// app登录
        /// </summary>
        /// <param name="iParameter"></param>
        /// <param name="iType"></param>
        /// <param name="iLoginPassword"></param>
        /// <param name="iLoginIP"></param>
        /// <returns></returns>
        public ReturnEntity<User_InfoDC> User_Login_App(string iParameter, LoginType iType,
           string iLoginPassword, string iLoginIP, string iVersion, int iMobileType, string iMobileFlag)
        {
            var entity = userDAL.User_Info_SELECT_Entity(iParameter, iType,
                //密码加密
                LazyAtHome.Core.Helper.CryptoHelper.Encrypt(iLoginPassword, LazyAtHome.Core.Enumerate.CryptoMode.MD5));
            if (entity == null)
            {
                if (!userDAL.User_Exist_Check(iParameter, iType).HasValue)
                {
                    return new ReturnEntity<User_InfoDC>(-1, "用户不存在");
                }
                else
                {
                    return new ReturnEntity<User_InfoDC>(-2, "用户名或密码错误");
                }
            }

            if (entity != null)
            {
                //登录日志
                userDAL.User_LoginLog_ADD(new User_LoginLogDC()
                {
                    LogType = 1,
                    LoginIP = iLoginIP,
                    UserID = entity.ID,
                    Channel = 2,
                });
                //app配置日志 User_AppInfo
                userDAL.User_AppInfo_ADD(new User_AppInfoDC()
                {
                    UserID = entity.ID,
                    Type = iMobileType,
                    Flag = iMobileFlag,
                    Version = iVersion,
                });
            }
            return new ReturnEntity<User_InfoDC>(entity);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="iParameter"></param>
        /// <param name="iType"></param>
        /// <param name="iLoginPassword"></param>
        /// <param name="iInviteCode"></param>
        /// <param name="iRegisterIP"></param>
        /// <param name="iVersion"></param>
        /// <returns></returns>
        public ReturnEntity<User_InfoDC> User_Reg_App(string iParameter, LoginType iType, string iLoginPassword,
            string iInviteCode, string iRegisterIP, string iVersion, int iMobileType, string iMobileFlag)
        {
            if (userDAL.User_Exist_Check(iParameter, iType).HasValue)
            {
                return new ReturnEntity<User_InfoDC>(-1, "注册帐号已存在");
            }

            var _UserID = Guid.NewGuid();
            string _LoginName = null;
            string _MPno = null;
            string _Email = null;
            string _NickName = null;

            var rd = new Random(this.GetHashCode());

            switch (iType)
            {
                case LoginType.LoginName:
                case LoginType.MPNo:
                    _NickName = iParameter;
                    break;
                case LoginType.Email:
                    _NickName = iParameter.Split('@')[0];
                    if (_NickName.Length > 20)
                    {
                        _NickName = _NickName.Substring(0, 20);
                    }
                    break;
                default:
                    _NickName = "ldj" + rd.Next(1000, 10000);
                    break;
            }

            while (userDAL.User_NickName_Exist(_NickName))
            {
                switch (iType)
                {
                    case LoginType.LoginName:
                        _NickName = iParameter + rd.Next(1000, 10000);
                        break;
                    default:
                        _NickName = "ldj" + rd.Next(1000, 10000);
                        break;
                }
            }

            switch (iType)
            {
                case LoginType.LoginName://用户名注册
                    _LoginName = iParameter;
                    break;
                case LoginType.MPNo://手机号注册
                    _MPno = iParameter;
                    break;
                case LoginType.Email://邮箱注册
                    _Email = iParameter;
                    break;
                default:
                    throw new Exception("注册类型错误");
            }

            //生成用户
            var _User_Info = new User_InfoDC()
            {
                ID = _UserID,
                LoginName = _LoginName,
                MPNo = _MPno,
                Email = _Email,
                LoginPassword = LazyAtHome.Core.Helper.CryptoHelper.Encrypt(iLoginPassword, LazyAtHome.Core.Enumerate.CryptoMode.MD5),
                Type = 1,
                Site = null,
                LastLoginTime = DateTime.Now,
                UserStatus = 1,
                AccountStatus = 1,
                RegistSource = 2,
            };

            var _User_Detail = new User_DetailDC()
            {
                NickName = _NickName,
            };

            var _User_RegisterLog = new User_RegisterLogDC()
            {
                Channel = 2,
                RegisterIP = iRegisterIP,
                SourceID = iVersion,
                InviteCode = iInviteCode,
            };

            if (!userDAL.User_Regist(_User_Info, _User_Detail, _User_RegisterLog))
            {
                return new ReturnEntity<User_InfoDC>(-2, "用户注册失败!");
            }

            return User_Login_App(iParameter, iType, iLoginPassword, iRegisterIP, iVersion, iMobileType, iMobileFlag);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iMobileType"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_LoginOut(Guid iUserID, int iMobileType)
        {
            var rtn = userDAL.User_LoginOut(iUserID, iMobileType);

            return new ReturnEntity<bool>(rtn);
        }

        /// <summary>
        /// 唤醒日志
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iMobileType"></param>
        /// <param name="iMobileFlag"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_WakeUp(Guid iUserID, int iMobileType, string iMobileFlag)
        {
            var rtn = userDAL.User_LoginLog_ADD(new User_LoginLogDC()
            {
                UserID = iUserID,
                Channel = 2,
                LogType = 2,
                LoginIP = iMobileType.ToString(),
                Obj_Remark = iMobileFlag,

            });
            return new ReturnEntity<bool>(rtn);
        }

    }
}
