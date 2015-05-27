using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.UserSystem.Contract.DataContract;
using LazyAtHome.WCF.UserSystem.Contract.DataContract.weixin;
using LazyAtHome.WCF.UserSystem.Contract.Enumerate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.UserSystem.Business.Business
{
    public partial class User
    {
        /// <summary>
        /// 微信用户绑定手机
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iMPNo"></param>
        /// <param name="iSite"></param>
        /// <param name="iIPAddress"></param>
        /// <returns></returns>
        public ReturnEntity<bool> wx_User_Weixin_BindMPNo(string iOpenid, string iMPNo, string iNickName,
            int iSite, string iIPAddress, string iInviteCode = null)
        {
            if (string.IsNullOrWhiteSpace(iOpenid))
            {
                return new ReturnEntity<bool>(-1, "微信号错误");
            }

            var user = userDAL.User_Info_SELECT_Entity(iOpenid, OAuthType.Weixin);
            //微信openID已注册用户
            if (user != null)
            {
                //已经是该用户,回复成功
                if (user.MPNo == iMPNo)
                {
                    return new ReturnEntity<bool>(true);
                }
                //解绑该用户
                else
                {
                    wx_User_Weixin_UnBindMPNo(iOpenid, iMPNo);
                }
            }

            //注册用户
            var userID = userDAL.User_Exist_Check(iMPNo, Contract.Enumerate.LoginType.MPNo);

            if (!userID.HasValue)
            {
                var regRtn = User_register(null, iMPNo, null, string.Empty,
                    RegChannel.Weixin, iSite.ToString(), iSite, iIPAddress, iInviteCode, out userID);

                if (regRtn.Success)
                {
                    //return User_Login_Web(iParameter, iType, iLoginPassword, iRegisterIP);
                }
                else
                {
                    return new ReturnEntity<bool>(regRtn.Code, regRtn.Message);
                }

            }

            //添加第三方合作登录表
            userDAL.User_OAuth_ADD(new User_OAuthDC()
            {
                UserID = userID.Value,
                Type = (int)OAuthType.Weixin,
                OpenID = iOpenid,
            });

            return new ReturnEntity<bool>(true);
        }

        /// <summary>
        /// 解绑用户微信
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iMPNo"></param>
        /// <returns></returns>
        public ReturnEntity<bool> wx_User_Weixin_UnBindMPNo(string iOpenid, string iMPNo)
        {
            if (string.IsNullOrWhiteSpace(iOpenid))
            {
                return new ReturnEntity<bool>(-1, "微信号错误");
            }

            Guid? userID = userDAL.User_Weixin_SELECT_UserID(iOpenid);
            if (!userID.HasValue)
            {
                return new ReturnEntity<bool>(true);
            }
            var user = userDAL.User_Info_SELECT_Entity(userID.Value);
            if (user == null)
            {
                //userDAL.User_OAuth_DELETE(user.ID, iOpenid, OAuthType.Weixin);
                return new ReturnEntity<bool>(-1, "用户不存在");
            }
            if (user.MPNo != iMPNo)
            {
                return new ReturnEntity<bool>(-1, "用户手机号不符");
            }
            userDAL.User_OAuth_DELETE(user.ID, iOpenid, OAuthType.Weixin);
            return new ReturnEntity<bool>(true);
        }

        /// <summary>
        /// 微信创建用户
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iSite"></param>
        /// <param name="iIPAddress"></param>
        /// <param name="iInviteCode"></param>
        /// <returns></returns>
        public ReturnEntity<User_WeixinDC> wx_User_Weixin_Create(string iOpenid, int iSite, string iIPAddress, string iInviteCode = null)
        {
            if (string.IsNullOrWhiteSpace(iOpenid))
            {
                return new ReturnEntity<User_WeixinDC>(-1, "微信号错误");
            }

            if (userDAL.wx_User_Weixin_CheckIsBind(iOpenid))
            {
                return new ReturnEntity<User_WeixinDC>(-1, "微信帐号已绑定用户");
            }

            var userID = Guid.NewGuid();

            //生成用户
            var _User_Info = new User_InfoDC()
            {
                ID = userID,
                LoginName = null,
                MPNo = null,
                Email = null,
                LoginPassword = string.Empty,
                RecommendedCode = null,
                Type = 1,
                Site = iSite,
                LastLoginTime = DateTime.Now,
                UserStatus = 1,
                AccountStatus = 1,
                RegistSource = 4,
            };

            var _User_Detail = new User_DetailDC()
            {
                //NickName = iOpenid,
            };

            var _User_RegisterLog = new User_RegisterLogDC()
            {
                UserID = userID,
                Channel = 4,
                RegisterIP = iIPAddress,
                SourceID = iOpenid,
                InviteCode = iInviteCode,
            };

            if (!userDAL.User_Regist(_User_Info, _User_Detail, _User_RegisterLog))
            {
                return new ReturnEntity<User_WeixinDC>(-2, "用户注册失败!");
            }

            //添加第三方合作登录表
            userDAL.User_OAuth_ADD(new User_OAuthDC()
            {
                UserID = userID,
                Type = (int)OAuthType.Weixin,
                OpenID = iOpenid,
            });

            return wx_User_Weixin_SELECT(iOpenid);
        }

        /// <summary>
        /// 微信旧用户绑定
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iMPNo"></param>
        /// <returns></returns>
        public ReturnEntity<User_WeixinDC> wx_User_Weixin_Login(string iOpenid, string iMPNo)
        {
            if (string.IsNullOrWhiteSpace(iOpenid))
            {
                return new ReturnEntity<User_WeixinDC>(-1, "微信号错误");
            }
            if (string.IsNullOrWhiteSpace(iMPNo))
            {
                return new ReturnEntity<User_WeixinDC>(-1, "手机号错误");
            }

            var user = userDAL.User_Info_SELECT_Entity(iMPNo);
            if (user == null)
            {
                return new ReturnEntity<User_WeixinDC>(-1, "用户不存在");
            }

            //添加第三方合作登录表
            userDAL.User_OAuth_ADD(new User_OAuthDC()
            {
                UserID = user.ID,
                Type = (int)OAuthType.Weixin,
                OpenID = iOpenid,
            });

            return wx_User_Weixin_SELECT(iOpenid);
        }

        static Regex rMobile = new System.Text.RegularExpressions.Regex("^1\\d{10}$", System.Text.RegularExpressions.RegexOptions.Compiled);
        static Regex rEmail = new System.Text.RegularExpressions.Regex(@"^(\w)+(\.\w+)*@(\w)+((\.\w+)+)$", System.Text.RegularExpressions.RegexOptions.Compiled);

        /// <summary>
        /// 微信旧用户绑定
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iMPNo"></param>
        /// <returns></returns>
        public ReturnEntity<User_WeixinDC> wx_User_Weixin_Login(string iOpenid, string iLoginName, string iPassword)
        {
            if (string.IsNullOrWhiteSpace(iOpenid))
            {
                return new ReturnEntity<User_WeixinDC>(-1, "微信号错误");
            }
            if (string.IsNullOrWhiteSpace(iLoginName))
            {
                return new ReturnEntity<User_WeixinDC>(-1, "用户名或密码错误");
            }
            if (iLoginName.Length < 4)
            {
                return new ReturnEntity<User_WeixinDC>(-1, "用户名或密码错误");
            }
            if (string.IsNullOrWhiteSpace(iPassword))
            {
                return new ReturnEntity<User_WeixinDC>(-1, "用户名或密码错误");
            }
            var user = userDAL.User_Info_SELECT_Entity(iOpenid, OAuthType.Weixin);
            if (user == null)
            {
                return new ReturnEntity<User_WeixinDC>(-1, "微信号错误");
            }

            LoginType lType = LoginType.LoginName;

            if (rMobile.IsMatch(iLoginName))
            {
                lType = LoginType.MPNo;
            }
            else if (rEmail.IsMatch(iLoginName))
            {
                lType = LoginType.Email;
            }

            iPassword = LazyAtHome.Core.Helper.CryptoHelper.Encrypt(iPassword, LazyAtHome.Core.Enumerate.CryptoMode.MD5);

            user = userDAL.User_Info_SELECT_Entity(iLoginName, lType, iPassword);
            if (user == null)
            {
                return new ReturnEntity<User_WeixinDC>(-1, "用户名或密码错误");
            }

            //添加第三方合作登录表
            userDAL.User_OAuth_ADD(new User_OAuthDC()
            {
                UserID = user.ID,
                Type = (int)OAuthType.Weixin,
                OpenID = iOpenid,
            });

            return wx_User_Weixin_SELECT(iOpenid);
        }

        /// <summary>
        /// 微信用户注册
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iLoginName"></param>
        /// <param name="iPassword"></param>
        /// <param name="iEmail"></param>
        /// <returns></returns>
        public ReturnEntity<bool> wx_User_Weixin_Reg(string iOpenid, string iLoginName, string iPassword, string iEmail)
        {
            if (string.IsNullOrWhiteSpace(iOpenid))
            {
                return new ReturnEntity<bool>(-1, "微信号错误");
            }

            var user = userDAL.User_Info_SELECT_Entity(iOpenid, OAuthType.Weixin);
            if (user == null)
            {
                wx_User_Weixin_Create(iOpenid, 0, null, null);
            }

            //重新获取用户
            user = userDAL.User_Info_SELECT_Entity(iOpenid, OAuthType.Weixin);
            if (user == null)
            {
                return new ReturnEntity<bool>(-1, "用户不存在");
            }

            //检查邮箱
            if (!string.IsNullOrWhiteSpace(iEmail))
            {
                if (userDAL.User_Exist_Check(iEmail, LoginType.Email, user.ID).HasValue)
                {
                    return new ReturnEntity<bool>(-1, "邮箱重复");
                }
            }

            //修改登录名
            if (userDAL.User_Exist_Check(iLoginName, LoginType.LoginName, user.ID).HasValue)
            {
                return new ReturnEntity<bool>(-1, "用户名已存在");
            }

            //修改登录名
            userDAL.User_LoginName_Change(user.ID, iLoginName);

            iPassword = LazyAtHome.Core.Helper.CryptoHelper.Encrypt(iPassword, LazyAtHome.Core.Enumerate.CryptoMode.MD5);
            //修改密码
            userDAL.User_LoginPassword_Reset(user.ID, iPassword);

            //修改邮箱
            if (!string.IsNullOrWhiteSpace(iEmail))
            {
                userDAL.User_Email_Change(user.ID, iEmail);
            }

            return new ReturnEntity<bool>(true);
        }


    }
}
