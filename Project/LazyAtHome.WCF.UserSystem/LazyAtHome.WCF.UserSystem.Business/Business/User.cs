using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Express.Contract.ClientProxy;
using LazyAtHome.WCF.Express.Contract.DataContract;
using LazyAtHome.WCF.UserSystem.Contract.DataContract;
using LazyAtHome.WCF.UserSystem.Contract.DataContract.weixin;
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
        private LazyAtHome.WCF.UserSystem.Interface.IDAL.IUserDAL userDAL;
        public User()
        {
            if (userDAL == null)
                userDAL = new LazyAtHome.WCF.UserSystem.DAL.UserDAL();
        }

        static User _User;

        public static User Instance
        {
            get
            {
                if (_User == null)
                {
                    _User = new User();
                }
                return _User;
            }
        }

        /// <summary>
        /// 用户存在查询
        /// </summary>
        /// <param name="iParameter"></param>
        /// <param name="iType"></param>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_Exist_Check(string iParameter, LoginType iType, Guid? iUserID = null)
        {
            var rtn = userDAL.User_Exist_Check(iParameter, iType, iUserID);
            if (rtn.HasValue)
            {
                switch (iType)
                {
                    case LoginType.LoginName:
                        return new ReturnEntity<bool>(-1, "登录名已存在");
                    case LoginType.MPNo:
                        return new ReturnEntity<bool>(-1, "手机号已存在");
                    case LoginType.Email:
                        return new ReturnEntity<bool>(-1, "注册邮箱已存在");
                    default:
                        return new ReturnEntity<bool>(-1, "注册帐号已存在");
                }
            }
            return new ReturnEntity<bool>(true);
        }

        /// <summary>
        /// 昵称重名验证
        /// </summary>
        /// <param name="iNickName"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_NickName_Check(string iNickName, Guid? iUserID = null)
        {
            if (userDAL.User_NickName_Exist(iNickName, iUserID))
            {
                return new ReturnEntity<bool>(-1, "昵称已存在");
            }
            return new ReturnEntity<bool>(true);
        }

        private ReturnEntity<bool> User_register(string iLoginName, string iMPNo, string iEmail,
            string iLoginPassword, RegChannel iRegChannel, string iSourceID, int iSite,
            string iRegisterIP, string iInviteCode, out Guid? iUserID)
        {
            iUserID = null;
            if (string.IsNullOrWhiteSpace(iLoginName) && string.IsNullOrWhiteSpace(iMPNo) && string.IsNullOrWhiteSpace(iEmail))
            {
                return new ReturnEntity<bool>(-1, "登录名或手机号不能为空");
            }

            //检查邀请码
            if (!string.IsNullOrWhiteSpace(iInviteCode))
            {
                if (iInviteCode.Length <= 7)
                {
                    //特殊类型
                    switch (iInviteCode[0])
                    {
                        case 'e'://快递类型
                        case 'E'://快递类型
                            if (!userDAL.PR_Operator_Exist(iInviteCode))
                            {
                                return new ReturnEntity<bool>(-1, "推广邀请码错误");
                            }
                            break;
                        default:
                            return new ReturnEntity<bool>(-1, "邀请码错误");
                    }
                }
                else
                {
                    Guid? _InviteUserID = userDAL.User_Exist_Check(iInviteCode, LoginType.RecommendedCode, null);
                    if (!_InviteUserID.HasValue)
                    {
                        return new ReturnEntity<bool>(-1, "用户邀请码错误");
                    }
                }
            }

            iUserID = Guid.NewGuid();

            var _User_Info = new User_InfoDC()
            {
                ID = iUserID.Value,
                LoginName = iLoginName,
                MPNo = iMPNo,
                Email = iEmail,
                LoginPassword = string.IsNullOrWhiteSpace(iLoginPassword) ? string.Empty : LazyAtHome.Core.Helper.CryptoHelper.Encrypt(iLoginPassword, LazyAtHome.Core.Enumerate.CryptoMode.MD5),
                Type = 1,
                Site = iSite,
                LastLoginTime = DateTime.Now,
                UserStatus = 1,
                AccountStatus = 1,
                RegistSource = (int)iRegChannel,
            };

            var _User_Detail = new User_DetailDC()
            {

            };

            var _User_RegisterLog = new User_RegisterLogDC()
            {
                Channel = (int)iRegChannel,
                RegisterIP = iRegisterIP,
                SourceID = iSite.ToString(),
                InviteCode = iInviteCode,
            };


            if (!userDAL.User_Regist(_User_Info, _User_Detail, _User_RegisterLog))
            {
                return new ReturnEntity<bool>(-2, "用户注册失败!");
            }

            if (!string.IsNullOrWhiteSpace(iInviteCode))
            {
                Console.WriteLine("注册缓存邀请码:" + iUserID + "  " + iInviteCode);
                LazyAtHome.WCF.UserSystem.Contract.ClientProxy.UserClient.Cache_Put("USER_INVITECODE_" + iUserID, iInviteCode, new TimeSpan(0, 30, 0));
            }

            return new ReturnEntity<bool>(true);
        }

        /// <summary>
        /// 网站登录
        /// </summary>
        /// <param name="iParameter"></param>
        /// <param name="iType"></param>
        /// <param name="iLoginPassword"></param>
        /// <param name="iLoginIP"></param>
        /// <returns></returns>
        public ReturnEntity<User_InfoDC> User_Login_Web(string iParameter, LoginType iType,
           string iLoginPassword, string iLoginIP)
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
                userDAL.User_LoginLog_ADD(new User_LoginLogDC()
                {
                    LogType = 1,
                    LoginIP = iLoginIP,
                    UserID = entity.ID,
                    Channel = 5,
                });
            }
            return new ReturnEntity<User_InfoDC>(entity);
        }

        //邀请日志

        /// <summary>
        /// 后台用户列表
        /// </summary>
        /// <param name="iLoginName"></param>
        /// <param name="iMPNo"></param>
        /// <param name="iEmail"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<User_InfoDC>> User_Info_SELECT_List(string iLoginName, string iMPNo,
            string iEmail, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            return new ReturnEntity<RecordCountEntity<User_InfoDC>>(userDAL.User_Info_SELECT_List(
                iLoginName, iMPNo, iEmail, iStartDate, iEndDate, iPageIndex, iPageSize));
        }

        /// <summary>
        /// 修改用户详情
        /// </summary>
        /// <param name="iPR_OperatorDC"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_Detail_UPDATE(User_DetailDC iUser_DetailDC)
        {
            if (!string.IsNullOrWhiteSpace(iUser_DetailDC.NickName)
                && userDAL.User_NickName_Exist(iUser_DetailDC.NickName, iUser_DetailDC.ID))
            {
                return new ReturnEntity<bool>(-1, "昵称已存在");
            }

            return new ReturnEntity<bool>(userDAL.User_Detail_UPDATE(iUser_DetailDC));
        }

        //用户信息
        public ReturnEntity<User_InfoDC> User_Info_SELECT_Entity(Guid iID)
        {
            return new ReturnEntity<User_InfoDC>(userDAL.User_Info_SELECT_Entity(iID));
        }

        /// <summary>
        /// 用户获取
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iOAuthType"></param>
        /// <returns></returns>
        public ReturnEntity<User_InfoDC> User_Info_SELECT_Entity(string iOpenid, OAuthType iOAuthType)
        {
            return new ReturnEntity<User_InfoDC>(userDAL.User_Info_SELECT_Entity(iOpenid, OAuthType.Weixin));
        }

        //用户详情
        public ReturnEntity<User_DetailDC> User_Detail_SELECT_Entity(Guid iID)
        {
            return new ReturnEntity<User_DetailDC>(userDAL.User_Detail_SELECT_Entity(iID));
        }


        /// <summary>
        /// 修改登录密码
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iOldPassword"></param>
        /// <param name="iNewPassword"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_LoginPassword_Change(Guid iUserID, string iOldPassword, string iNewPassword)
        {
            if (string.IsNullOrEmpty(iOldPassword))
            {
                return new ReturnEntity<bool>(-1, "密码不能为空");
            }
            if (string.IsNullOrEmpty(iNewPassword))
            {
                return new ReturnEntity<bool>(-1, "密码不能为空");
            }
            iOldPassword = LazyAtHome.Core.Helper.CryptoHelper.Encrypt(iOldPassword, LazyAtHome.Core.Enumerate.CryptoMode.MD5);
            iNewPassword = LazyAtHome.Core.Helper.CryptoHelper.Encrypt(iNewPassword, LazyAtHome.Core.Enumerate.CryptoMode.MD5);

            return new ReturnEntity<bool>(userDAL.User_LoginPassword_Change(iUserID, iOldPassword, iNewPassword));
        }

        /// <summary>
        /// 重置登录密码
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iNewPassword"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_LoginPassword_Reset(Guid iUserID, string iNewPassword)
        {
            if (string.IsNullOrEmpty(iNewPassword))
            {
                return new ReturnEntity<bool>(-1, "密码不能为空");
            }
            iNewPassword = LazyAtHome.Core.Helper.CryptoHelper.Encrypt(iNewPassword, LazyAtHome.Core.Enumerate.CryptoMode.MD5);
            return new ReturnEntity<bool>(userDAL.User_LoginPassword_Reset(iUserID, iNewPassword));
        }

        /// <summary>
        /// 重置登录密码
        /// </summary>
        /// <param name="iUserMPNo"></param>
        /// <param name="iNewPassword"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_LoginPassword_Reset(string iUserMPNo, string iNewPassword)
        {
            if (string.IsNullOrEmpty(iNewPassword))
            {
                return new ReturnEntity<bool>(-1, "密码不能为空");
            }
            var userid = userDAL.User_Exist_Check(iUserMPNo, LoginType.MPNo);
            if (userid.HasValue)
            {
                iNewPassword = LazyAtHome.Core.Helper.CryptoHelper.Encrypt(iNewPassword, LazyAtHome.Core.Enumerate.CryptoMode.MD5);
                return new ReturnEntity<bool>(userDAL.User_LoginPassword_Reset(userid.Value, iNewPassword));
            }
            else
            {
                return new ReturnEntity<bool>(-1, "用户不存在");
            }
        }

        /// <summary>
        /// 修改支付密码
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iOldPassword"></param>
        /// <param name="iNewPassword"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_PayPassword_Change(Guid iUserID, string iOldPassword, string iNewPassword)
        {
            if (string.IsNullOrEmpty(iOldPassword))
            {
                return new ReturnEntity<bool>(-1, "密码不能为空");
            }
            if (string.IsNullOrEmpty(iNewPassword))
            {
                return new ReturnEntity<bool>(-1, "密码不能为空");
            }
            iOldPassword = LazyAtHome.Core.Helper.CryptoHelper.Encrypt(iOldPassword, LazyAtHome.Core.Enumerate.CryptoMode.MD5);
            iNewPassword = LazyAtHome.Core.Helper.CryptoHelper.Encrypt(iNewPassword, LazyAtHome.Core.Enumerate.CryptoMode.MD5);

            return new ReturnEntity<bool>(userDAL.User_PayPassword_Change(iUserID, iOldPassword, iNewPassword));
        }

        /// <summary>
        /// 重置登录密码
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iNewPassword"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_PayPassword_Reset(Guid iUserID, string iNewPassword)
        {
            if (string.IsNullOrEmpty(iNewPassword))
            {
                return new ReturnEntity<bool>(-1, "密码不能为空");
            }
            iNewPassword = LazyAtHome.Core.Helper.CryptoHelper.Encrypt(iNewPassword, LazyAtHome.Core.Enumerate.CryptoMode.MD5);
            return new ReturnEntity<bool>(userDAL.User_PayPassword_Reset(iUserID, iNewPassword));
        }

        /// <summary>
        /// 修改登录名
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iLoginName"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_LoginName_Change(Guid iUserID, string iLoginName)
        {
            return new ReturnEntity<bool>(userDAL.User_LoginName_Change(iUserID, iLoginName));
        }

        /// <summary>
        /// 修改手机号
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iMPNo"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_MPNo_Change(Guid iUserID, string iMPNo)
        {
            if (string.IsNullOrEmpty(iMPNo))
            {
                return new ReturnEntity<bool>(-1, "手机号不能为空");
            }
            if (userDAL.User_Exist_Check(iMPNo, LoginType.MPNo, iUserID).HasValue)
            {
                return new ReturnEntity<bool>(-1, "手机号已注册");
            }

            return new ReturnEntity<bool>(userDAL.User_MPNo_Change(iUserID, iMPNo));
        }

        /// <summary>
        /// 修改邮箱
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iMPNo"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_Email_Change(Guid iUserID, string iEmail)
        {
            if (userDAL.User_Exist_Check(iEmail, LoginType.Email, iUserID).HasValue)
            {
                return new ReturnEntity<bool>(-1, "邮箱已存在");
            }

            return new ReturnEntity<bool>(userDAL.User_Email_Change(iUserID, iEmail));
        }

        /// <summary>
        /// 修改昵称
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iNickName"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_NickName_Change(Guid iUserID, string iNickName)
        {
            if (string.IsNullOrEmpty(iNickName))
            {
                return new ReturnEntity<bool>(-1, "昵称不能为空");
            }

            if (userDAL.User_NickName_Exist(iNickName, iUserID))
            {
                return new ReturnEntity<bool>(-1, "昵称已存在");
            }
            return new ReturnEntity<bool>(userDAL.User_NickName_Change(iUserID, iNickName));
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iNickName"></param>
        /// <param name="iSex"></param>
        /// <param name="iMPNo"></param>
        /// <param name="iEmail"></param>
        /// <param name="iRealName"></param>
        /// <param name="iIDCard"></param>
        /// <param name="iDistrictID"></param>
        /// <param name="iLocation"></param>
        /// <returns></returns>
        public ReturnEntity<bool> web_User_Detail_UPDATE(Guid iUserID, string iNickName, int iSex, string iMPNo
            , string iEmail, string iRealName, string iIDCard, int? iDistrictID, string iLocation)
        {
            var user = userDAL.User_Info_SELECT_Entity(iUserID);
            if (user == null)
            {
                return new ReturnEntity<bool>(-1, "用户不存在");
            }
            var userdetail = userDAL.User_Detail_SELECT_Entity(iUserID);
            if (userdetail == null)
            {
                return new ReturnEntity<bool>(-1, "用户不存在");
            }

            ReturnEntity<bool> rtn = null;

            if (!string.IsNullOrEmpty(iNickName))
            {
                if (userdetail.NickName != iNickName)
                {
                    rtn = User_NickName_Change(iUserID, iNickName);
                    if (!rtn.Success)
                    {
                        return rtn;
                    }
                }
            }

            if (user.MPNo != iMPNo)
            {
                if (!string.IsNullOrWhiteSpace(iMPNo))
                {
                    rtn = User_MPNo_Change(iUserID, iMPNo);
                    if (!rtn.Success)
                    {
                        return rtn;
                    }
                }
            }

            if (user.Email != iEmail)
            {
                if (!string.IsNullOrWhiteSpace(iEmail))
                {
                    rtn = User_Email_Change(iUserID, iEmail);
                    if (!rtn.Success)
                    {
                        return rtn;
                    }
                }
            }

            userdetail.IDCard = iIDCard;
            userdetail.Sex = iSex;
            userdetail.RealName = iRealName;
            userdetail.DistrictID = iDistrictID;
            userdetail.Location = iLocation;

            if (!userDAL.User_Detail_UPDATE(userdetail))
            {
                return new ReturnEntity<bool>(-1, "用户详情更新失败");
            }

            return new ReturnEntity<bool>(true);
        }

        /// <summary>
        /// 用户邀请数量
        /// </summary>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        public ReturnEntity<int> User_Invite_Count(Guid iUserID)
        {
            var rtn = userDAL.User_Invite_Count(iUserID);

            return new ReturnEntity<int>(rtn);
        }

        /// <summary>
        /// 更新用户基本信息
        /// </summary>
        /// <param name="iUser_InfoDC"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_Info_UPDATE(User_InfoDC iUser_InfoDC)
        {
            var rtn = userDAL.User_Info_UPDATE(iUser_InfoDC);

            return new ReturnEntity<bool>(rtn);
        }

        /// <summary>
        /// 收货地址管理
        /// </summary>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        public ReturnEntity<IList<User_ConsigneeAddressDC>> User_ConsigneeAddress_SELECT_List(Guid iUserID)
        {
            var list = userDAL.User_ConsigneeAddress_SELECT_List(iUserID);

            return new ReturnEntity<IList<User_ConsigneeAddressDC>>(list);
        }

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public ReturnEntity<User_ConsigneeAddressDC> User_ConsigneeAddress_SELECT_Entity(int iID)
        {
            return new ReturnEntity<User_ConsigneeAddressDC>(userDAL.User_ConsigneeAddress_SELECT_Entity(iID));
        }

        /// <summary>
        /// 收货地址添加
        /// </summary>
        /// <param name="iUser_ConsigneeAddressDC"></param>
        /// <returns></returns>
        public ReturnEntity<int> User_ConsigneeAddress_ADD(User_ConsigneeAddressDC iUser_ConsigneeAddressDC)
        {
            if (iUser_ConsigneeAddressDC == null)
            {
                return new ReturnEntity<int>(-1, "不能为空");
            }

            if (string.IsNullOrWhiteSpace(iUser_ConsigneeAddressDC.MPNo))
            {
                return new ReturnEntity<int>(-1, "手机错误");
            }
            if (string.IsNullOrWhiteSpace(iUser_ConsigneeAddressDC.Address))
            {
                return new ReturnEntity<int>(-1, "地址错误");
            }
            if (string.IsNullOrWhiteSpace(iUser_ConsigneeAddressDC.Consignee))
            {
                return new ReturnEntity<int>(-1, "联系人错误");
            }

            iUser_ConsigneeAddressDC.MPNo = iUser_ConsigneeAddressDC.MPNo.Replace("\r", "").Replace("\n", "");
            iUser_ConsigneeAddressDC.Address = iUser_ConsigneeAddressDC.Address.Replace("\r", "").Replace("\n", "");
            iUser_ConsigneeAddressDC.Consignee = iUser_ConsigneeAddressDC.Consignee.Replace("\r", "").Replace("\n", "");

            var rtn = userDAL.User_ConsigneeAddress_ADD(iUser_ConsigneeAddressDC);

            return new ReturnEntity<int>(rtn);
        }

        /// <summary>
        /// 收货地址更新
        /// </summary>
        /// <param name="iUser_ConsigneeAddressDC"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_ConsigneeAddress_UPDATE(User_ConsigneeAddressDC iUser_ConsigneeAddressDC)
        {
            if (iUser_ConsigneeAddressDC == null)
            {
                return new ReturnEntity<bool>(-1, "不能为空");
            }

            if (string.IsNullOrWhiteSpace(iUser_ConsigneeAddressDC.MPNo))
            {
                return new ReturnEntity<bool>(-1, "手机错误");
            }
            if (string.IsNullOrWhiteSpace(iUser_ConsigneeAddressDC.Address))
            {
                return new ReturnEntity<bool>(-1, "地址错误");
            }
            if (string.IsNullOrWhiteSpace(iUser_ConsigneeAddressDC.Consignee))
            {
                return new ReturnEntity<bool>(-1, "联系人错误");
            }

            iUser_ConsigneeAddressDC.MPNo = iUser_ConsigneeAddressDC.MPNo.Replace("\r", "").Replace("\n", "");
            iUser_ConsigneeAddressDC.Address = iUser_ConsigneeAddressDC.Address.Replace("\r", "").Replace("\n", "");
            iUser_ConsigneeAddressDC.Consignee = iUser_ConsigneeAddressDC.Consignee.Replace("\r", "").Replace("\n", "");

            var rtn = userDAL.User_ConsigneeAddress_UPDATE(iUser_ConsigneeAddressDC);

            return new ReturnEntity<bool>(rtn);
        }

        /// <summary>
        /// 收货地址删除
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_ConsigneeAddress_DELETE(int iID)
        {
            return new ReturnEntity<bool>(userDAL.User_ConsigneeAddress_DELETE(iID));
        }

        /// <summary>
        /// 设置默认地址
        /// </summary>
        /// <param name="iConsigneeAddressID"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_ConsigneeAddress_SetDefault(int iID)
        {
            return new ReturnEntity<bool>(userDAL.User_ConsigneeAddress_SetDefault(iID));
        }

        /// <summary>
        /// 余额日志
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iMPNo"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<User_AmountLogDC>> User_AmountLog_SELECT_List(
              Guid? iUserID, string iMPNo, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            return new ReturnEntity<RecordCountEntity<User_AmountLogDC>>(userDAL.User_AmountLog_SELECT_List(
              iUserID, iMPNo, iStartDate, iEndDate, iPageIndex, iPageSize));
        }

        /// <summary>
        /// 积分日志
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iMPNo"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<User_ScoreLogDC>> User_ScoreLog_SELECT_List(
              Guid? iUserID, string iMPNo, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            return new ReturnEntity<RecordCountEntity<User_ScoreLogDC>>(userDAL.User_ScoreLog_SELECT_List(
                iUserID, iMPNo, iStartDate, iEndDate, iPageIndex, iPageSize));
        }

        //邮件订阅

        //投诉

        //产品收藏

        /// <summary>
        /// 用户卡绑定
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iCardPassword"></param>
        /// <returns></returns>
        public ReturnEntity<User_CardDC> User_Card_Bind(Guid iUserID, UserCardType iUserCardType, string iCardPassword)
        {
            var rtn = userDAL.User_Card_Bind(iUserID, iUserCardType, iCardPassword);

            if (rtn > 0)
            {
                return new ReturnEntity<User_CardDC>(userDAL.User_Card_SELECT_Entity(iUserID, rtn));
            }
            else
            {
                switch (rtn)
                {
                    case -1:
                        return new ReturnEntity<User_CardDC>(-1, "卡密码无效");
                    case -2:
                        return new ReturnEntity<User_CardDC>(-2, "卡已被其他人使用");
                    case -3:
                        return new ReturnEntity<User_CardDC>(-2, "您已使用过同类卡");
                    default:
                        return new ReturnEntity<User_CardDC>(-99, "无法记录的错误" + rtn);
                }
            }
        }

        /// <summary>
        /// 卡绑定前检查
        /// </summary>
        /// <param name="iUserCardType"></param>
        /// <param name="iCardPassword"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_Card_Bind_Check(UserCardType iUserCardType, string iCardPassword)
        {
            var rtn = userDAL.User_Card_Bind_Check(iUserCardType, iCardPassword);

            switch (rtn)
            {
                case 1:
                    return new ReturnEntity<bool>(true);
                case -1:
                    return new ReturnEntity<bool>(-1, "卡密码无效");
                case -2:
                    return new ReturnEntity<bool>(-2, "卡已被其他人绑定");
                default:
                    return new ReturnEntity<bool>(-99, "无法记录的错误" + rtn);
            }
        }

        /// <summary>
        /// 用户卡列表获取
        /// </summary>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        public ReturnEntity<IList<User_CardDC>> User_Card_SELECT_List(Guid iUserID)
        {
            return new ReturnEntity<IList<User_CardDC>>(userDAL.User_Card_SELECT_List(iUserID));
        }

        /// <summary>
        /// 查询卡消费日志
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iCardID"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Base_CardLogDC>> Base_CardLog_SELECT_List(Guid? iUserID, int? iCardID,
              DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            return new ReturnEntity<RecordCountEntity<Base_CardLogDC>>(userDAL.Base_CardLog_SELECT_List(
                iUserID, iCardID, iStartDate, iEndDate, iPageIndex, iPageSize));
        }

        /// <summary>
        /// 检查用户是否绑定手机
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <returns></returns>
        public ReturnEntity<bool> wx_User_Weixin_CheckIsBind(string iOpenid)
        {
            if (string.IsNullOrWhiteSpace(iOpenid))
            {
                return new ReturnEntity<bool>(-1, "微信号错误");
            }
            return new ReturnEntity<bool>(userDAL.wx_User_Weixin_CheckIsBind(iOpenid));
        }

        /// <summary>
        /// 微信用户信息获取
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <returns></returns>
        public ReturnEntity<User_WeixinDC> wx_User_Weixin_SELECT(string iOpenid)
        {
            if (string.IsNullOrWhiteSpace(iOpenid))
            {
                return new ReturnEntity<User_WeixinDC>(-1, "微信号错误");
            }

            var rtn = new User_WeixinDC();

            var user = userDAL.User_Info_SELECT_Entity(iOpenid, OAuthType.Weixin);
            if (user == null)
            {
                return new ReturnEntity<User_WeixinDC>(-1, "用户不存在");
            }
            else
            {
                rtn.UserInfo = user;
            }

            var detail = userDAL.User_Detail_SELECT_Entity(user.ID);
            if (detail == null)
            {
                return new ReturnEntity<User_WeixinDC>(-1, "用户信息错误");
            }
            else
            {
                rtn.UserDetail = detail;
            }

            var addressList = userDAL.User_ConsigneeAddress_SELECT_List(user.ID);
            if (addressList != null)
            {
                rtn.iConsigneeAddress = addressList.FirstOrDefault();
            }

            rtn.CardList = userDAL.User_Card_SELECT_List(user.ID);
            if (rtn.CardList != null)
            {
                foreach (var item in rtn.CardList)
                {
                    var logList = userDAL.Base_CardLog_SELECT_List(user.ID, item.CardID, null, null, 1, 10);
                    if (logList != null)
                        item.CardLogList = logList.ReturnList;
                }
            }

            rtn.CouponList = userDAL.User_Coupon_SELECT_List(user.ID, null, null, CouponStatus.Normal, null, null, 1, 10).ReturnList;

            return new ReturnEntity<User_WeixinDC>(rtn);
        }

        /// <summary>
        /// 微信用户信息获取
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <returns></returns>
        public ReturnEntity<User_WeixinDC> wx_User_Weixin_SELECT(Guid iUserID)
        {
            var rtn = new User_WeixinDC();

            var user = userDAL.User_Info_SELECT_Entity(iUserID);
            if (user == null)
            {
                return new ReturnEntity<User_WeixinDC>(-1, "用户不存在");
            }
            else
            {
                rtn.UserInfo = user;
            }

            var detail = userDAL.User_Detail_SELECT_Entity(user.ID);
            if (detail == null)
            {
                return new ReturnEntity<User_WeixinDC>(-1, "用户信息错误");
            }
            else
            {
                rtn.UserDetail = detail;
            }

            var addressList = userDAL.User_ConsigneeAddress_SELECT_List(user.ID);
            if (addressList != null)
            {
                rtn.iConsigneeAddress = addressList.FirstOrDefault();
            }

            rtn.CardList = userDAL.User_Card_SELECT_List(user.ID);
            if (rtn.CardList != null)
            {
                foreach (var item in rtn.CardList)
                {
                    var logList = userDAL.Base_CardLog_SELECT_List(user.ID, item.CardID, null, null, 1, 10);
                    if (logList != null)
                        item.CardLogList = logList.ReturnList;
                }
            }

            rtn.CouponList = userDAL.User_Coupon_SELECT_List(user.ID, null, null, CouponStatus.Normal, null, null, 1, 10).ReturnList;

            return new ReturnEntity<User_WeixinDC>(rtn);
        }

        static object lockobj = new object();


        /// <summary>
        /// 微信用户绑定手机
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iMPNo"></param>
        /// <param name="iNickName"></param>
        /// <param name="iSite"></param>
        /// <param name="iIPAddress"></param>
        /// <param name="iInviteCode"></param>
        /// <returns></returns>
        public ReturnEntity<User_WeixinDC> wx_User_Weixin_Bind_Part(string iOpenid, string iMPNo, string iNickName,
            int iSite, string iIPAddress, string iInviteCode = null)
        {
            if (string.IsNullOrWhiteSpace(iOpenid))
            {
                return new ReturnEntity<User_WeixinDC>(-1, "微信号错误");
            }
            if (string.IsNullOrWhiteSpace(iMPNo))
            {
                return new ReturnEntity<User_WeixinDC>(-1, "手机号错误");
            }

            if (userDAL.User_Exist_Check(iMPNo, LoginType.MPNo, null).HasValue)
            {
                return new ReturnEntity<User_WeixinDC>(-1, "手机号已存在");
            }

            var user = userDAL.User_Info_SELECT_Entity(iOpenid, OAuthType.Weixin);
            if (user == null)
            {
                var bindRtn = wx_User_Weixin_BindMPNo(iOpenid, iMPNo, iNickName, iSite, iIPAddress, iInviteCode);
                if (bindRtn != null)
                {
                    if (bindRtn.Success && bindRtn.OBJ)
                    {
                        return wx_User_Weixin_SELECT(iOpenid);
                    }
                    else
                    {
                        return new ReturnEntity<User_WeixinDC>(bindRtn.Code, bindRtn.Message);
                    }
                }
                else
                {
                    return new ReturnEntity<User_WeixinDC>(-1, "绑定失败");
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(user.MPNo))
                {
                    return new ReturnEntity<User_WeixinDC>(-1, "手机号已存在");
                }

                if (userDAL.User_Exist_Check(iMPNo, LoginType.MPNo, user.ID).HasValue)
                {
                    return new ReturnEntity<User_WeixinDC>(-1, "手机号已注册");
                }

                //更新手机号
                userDAL.User_MPNo_Change(user.ID, iMPNo);

                //返回
                return wx_User_Weixin_SELECT(iOpenid);
            }
        }

        /// <summary>
        /// 收货地址管理
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <returns></returns>
        public ReturnEntity<IList<User_ConsigneeAddressDC>> wx_User_ConsigneeAddress_SELECT_List(string iOpenid)
        {
            Guid? userID = userDAL.User_Weixin_SELECT_UserID(iOpenid);
            if (!userID.HasValue)
            {
                return new ReturnEntity<IList<User_ConsigneeAddressDC>>(-1, "用户未绑定");
            }

            return new ReturnEntity<IList<User_ConsigneeAddressDC>>(userDAL.User_ConsigneeAddress_SELECT_List(userID.Value));
        }

        /// <summary>
        /// 收货地址添加
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iConsignee"></param>
        /// <param name="iDistrictID"></param>
        /// <param name="iAddress"></param>
        /// <param name="iMPNo"></param>
        /// <param name="iDefault"></param>
        /// <returns></returns>
        public ReturnEntity<int> wx_User_ConsigneeAddress_ADD(
            string iOpenid, string iConsignee, int iDistrictID, string iAddress, string iMPNo, bool iDefault)
        {
            if (string.IsNullOrWhiteSpace(iMPNo))
            {
                return new ReturnEntity<int>(-1, "手机错误");
            }

            Guid? userID = userDAL.User_Weixin_SELECT_UserID(iOpenid);
            if (!userID.HasValue)
            {
                return new ReturnEntity<int>(-1, "用户未绑定");
            }

            User_ConsigneeAddressDC entity = new User_ConsigneeAddressDC()
            {
                Address = iAddress,
                Consignee = iConsignee,
                DistrictID = iDistrictID,
                MPNo = iMPNo,
                UserID = userID.Value,
                IsDefault = iDefault ? 1 : 0,
            };

            var id = userDAL.User_ConsigneeAddress_ADD(entity);

            return new ReturnEntity<int>(id);
        }

        /// <summary>
        /// 收货地址更新
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="iConsignee"></param>
        /// <param name="iDistrictID"></param>
        /// <param name="iAddress"></param>
        /// <param name="iMPNo"></param>
        /// <returns></returns>
        public ReturnEntity<bool> wx_User_ConsigneeAddress_UPDATE(int iID, string iConsignee,
            int iDistrictID, string iAddress, string iMPNo, bool iDefault)
        {
            var conAddress = userDAL.User_ConsigneeAddress_SELECT_Entity(iID);
            if (conAddress == null)
            {
                return new ReturnEntity<bool>(-1, "未找到收货地址");
            }

            conAddress.Consignee = iConsignee;
            conAddress.DistrictID = iDistrictID;
            conAddress.Address = iAddress;
            conAddress.MPNo = iMPNo;
            conAddress.IsDefault = iDefault ? 1 : 0;

            return new ReturnEntity<bool>(userDAL.User_ConsigneeAddress_UPDATE(conAddress));
        }

        /// <summary>
        /// 收货地址删除
        /// </summary>
        /// <param name="iUser_ConsigneeAddressDC"></param>
        /// <returns></returns>
        public ReturnEntity<bool> wx_User_ConsigneeAddress_DELETE(int iID)
        {
            return new ReturnEntity<bool>(userDAL.User_ConsigneeAddress_DELETE(iID));
        }

        /// <summary>
        /// 用户卡绑定
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iUserCardType"></param>
        /// <param name="iCardPassword"></param>
        /// <returns></returns>
        public ReturnEntity<bool> wx_User_Card_Bind(string iOpenid, UserCardType iUserCardType, string iCardPassword)
        {
            Guid? userID = userDAL.User_Weixin_SELECT_UserID(iOpenid);
            if (!userID.HasValue)
            {
                return new ReturnEntity<bool>(-1, "用户未绑定");
            }

            var rtn = User_Card_Bind(userID.Value, iUserCardType, iCardPassword);
            if (rtn.Success)
            {
                return new ReturnEntity<bool>(true);
            }
            else
            {
                return new ReturnEntity<bool>(rtn.Code, rtn.Message);
            }
        }

        /// <summary>
        /// 用户卡列表获取
        /// </summary>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        public ReturnEntity<IList<User_CardDC>> wx_User_Card_SELECT_List(string iOpenid)
        {
            Guid? userID = userDAL.User_Weixin_SELECT_UserID(iOpenid);
            if (!userID.HasValue)
            {
                return new ReturnEntity<IList<User_CardDC>>(-1, "用户未绑定");
            }

            var cardList = userDAL.User_Card_SELECT_List(userID.Value);

            if (cardList != null)
            {
                foreach (var item in cardList)
                {
                    var logList = userDAL.Base_CardLog_SELECT_List(userID.Value, item.CardID, null, null, 1, 10);
                    if (logList != null)
                        item.CardLogList = logList.ReturnList;
                }
            }
            return new ReturnEntity<IList<User_CardDC>>(cardList);
        }

        #region 用户消息

        /// <summary>
        /// 查询用户消息
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iCardID"></param>
        /// <param name="iUserCardType"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<User_Message_PrivateDC>> User_Message_Private_SELECT_List(
            Guid iUserID, int iPageIndex, int iPageSize)
        {
            return new ReturnEntity<RecordCountEntity<User_Message_PrivateDC>>(
                userDAL.User_Message_Private_SELECT_List(iUserID, iPageIndex, iPageSize));
        }

        /// <summary>
        /// 消息置已读
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_Message_Private_Read(int iID)
        {
            return new ReturnEntity<bool>(userDAL.User_Message_Private_Read(iID));
        }

        /// <summary>
        /// 消息删除
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_Message_Private_DELETE(int iID)
        {
            return new ReturnEntity<bool>(userDAL.User_Message_Private_DELETE(iID));
        }

        /// <summary>
        /// 根据ID查询消息
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public ReturnEntity<User_Message_PrivateDC> User_Message_Private_SELECT_Entity(int iID)
        {
            return new ReturnEntity<User_Message_PrivateDC>(userDAL.User_Message_Private_SELECT_Entity(iID));
        }

        /// <summary>
        /// 微信获取用户消息
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<User_Message_PrivateDC>> wx_User_Message_Private_SELECT_List(
            string iOpenid, int iPageIndex, int iPageSize)
        {
            Guid? userID = userDAL.User_Weixin_SELECT_UserID(iOpenid);
            if (!userID.HasValue)
            {
                return new ReturnEntity<RecordCountEntity<User_Message_PrivateDC>>(-1, "用户未绑定");
            }
            return User_Message_Private_SELECT_List(userID.Value, iPageIndex, iPageSize);
        }

        #endregion

        /// <summary>
        /// 修改用户昵称
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iNickName"></param>
        /// <returns></returns>
        public ReturnEntity<bool> wx_User_NickName_Change(string iOpenid, string iNickName)
        {
            if (!string.IsNullOrWhiteSpace(iNickName))
            {
                return new ReturnEntity<bool>(-1, "昵称不能为空");
            }

            Guid? userID = userDAL.User_Weixin_SELECT_UserID(iOpenid);
            if (!userID.HasValue)
            {
                return new ReturnEntity<bool>(-1, "用户未绑定手机号");
            }
            return User_NickName_Change(userID.Value, iNickName);
        }

        /// <summary>
        /// 查询优惠券列表
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iMPNo"></param>
        /// <param name="iCouponName"></param>
        /// <param name="iCouponStatus"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<User_CouponDC>> User_Coupon_SELECT_List(Guid? iUserID, string iMPNo,
            string iCouponName, CouponStatus? iCouponStatus, DateTime? iStartDate, DateTime? iEndDate,
            int iPageIndex, int iPageSize)
        {
            var rtn = userDAL.User_Coupon_SELECT_List(iUserID, iMPNo, iCouponName, iCouponStatus, iStartDate, iEndDate, iPageIndex, iPageSize);

            return new ReturnEntity<RecordCountEntity<User_CouponDC>>(rtn);
        }

        /// <summary>
        /// 用户绑定券检查
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iCouponCode"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_Coupon_Bind_Check(Guid iUserID, string iCouponCode)
        {
            if (string.IsNullOrWhiteSpace(iCouponCode))
            {
                return new ReturnEntity<bool>(-1, "优惠券编号不能为空");
            }

            var rtn = userDAL.User_Coupon_Bind_Check(iUserID, iCouponCode);
            if (rtn > 0)
            {
                return new ReturnEntity<bool>(true);
            }
            else
            {
                switch (rtn)
                {
                    case -1://用户不存在
                        return new ReturnEntity<bool>(-7, "用户不存在");
                    case -3://优惠券已送完
                        return new ReturnEntity<bool>(-6, "优惠券已领完");
                    case -4://拥有数量超限
                        return new ReturnEntity<bool>(-5, "您已拥有此类优惠券");
                    case -5://优惠券已过期
                        return new ReturnEntity<bool>(-4, "优惠券已过期");
                    case -101://已被领取
                        return new ReturnEntity<bool>(-3, "优惠券编号已使用");
                    case -2://优惠券不存在
                    default:
                        return new ReturnEntity<bool>(-2, "优惠券编号不存在");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iCouponCode"></param>
        /// <returns></returns>
        public ReturnEntity<User_CouponDC> User_Coupon_Bind(Guid iUserID, string iCouponCode)
        {
            if (string.IsNullOrWhiteSpace(iCouponCode))
            {
                return new ReturnEntity<User_CouponDC>(-1, "优惠券编号不能为空");
            }

            var rtn = userDAL.User_Coupon_Bind(iUserID, iCouponCode);
            if (rtn > 0)
            {
                return new ReturnEntity<User_CouponDC>(userDAL.User_Coupon_SELECT_Entity(rtn));
            }
            else
            {
                switch (rtn)
                {
                    case -1://用户不存在
                        return new ReturnEntity<User_CouponDC>(-7, "用户不存在");
                    case -3://优惠券已送完
                        return new ReturnEntity<User_CouponDC>(-6, "优惠券已领完");
                    case -4://拥有数量超限
                        return new ReturnEntity<User_CouponDC>(-5, "您已拥有此类优惠券");
                    case -5://优惠券已过期
                        return new ReturnEntity<User_CouponDC>(-4, "优惠券已过期");
                    case -101://已被领取
                        return new ReturnEntity<User_CouponDC>(-3, "优惠券编号已使用");
                    case -2://优惠券不存在
                    default:
                        return new ReturnEntity<User_CouponDC>(-2, "优惠券编号不存在");
                }
            }
        }

        #region 微信关注日志

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="iUser_WeixinAttentionLogDC"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_WeixinAttention_ADD(string iOpenID, DateTime iAttentionTime, string iSource)
        {
            if (string.IsNullOrWhiteSpace(iOpenID))
            {
                return new ReturnEntity<bool>(-1, "微信ID不能为空");
            }
            //先删除
            userDAL.User_WeixinAttentionLog_DELETE(iOpenID);

            var rtn = userDAL.User_WeixinAttentionLog_ADD(new User_WeixinAttentionLogDC()
            {
                OpenID = iOpenID,
                AttentionTime = iAttentionTime,
                Source = userDAL.User_RegisterSource_SELECT_InternalKey(iSource),
            }
            );

            return new ReturnEntity<bool>(rtn);
        }

        /// <summary>
        /// 删除关注
        /// </summary>
        /// <param name="iOpenID"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_WeixinAttention_Remove(string iOpenID)
        {
            userDAL.User_WeixinAttentionLog_DELETE(iOpenID);

            return new ReturnEntity<bool>(true);
        }

        /// <summary>
        /// 用户是否关注
        /// </summary>
        /// <param name="iOpenID"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_WeixinAttention_Check(string iOpenID)
        {
            var rtn = userDAL.User_WeixinAttention_Check(iOpenID);

            return new ReturnEntity<bool>(rtn);
        }

        /// <summary>
        /// 微信关注统计
        /// </summary>
        /// <param name="iInternalKey"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <returns></returns>
        public ReturnEntity<IList<User_WeixinAttentionLogStatDC>> User_WeixinAttentionLog_Stat(string iInternalKey, DateTime iStartDate, DateTime iEndDate)
        {
            var rtn = userDAL.User_WeixinAttentionLog_Stat(iInternalKey, iStartDate, iEndDate);

            if (rtn == null)
            {
                return new ReturnEntity<IList<User_WeixinAttentionLogStatDC>>(-1, "参数错误");
            }

            return new ReturnEntity<IList<User_WeixinAttentionLogStatDC>>(rtn);
        }

        #endregion

        /// <summary>
        /// 投诉
        /// </summary>
        /// <param name="iUser_ComplaintsDC"></param>
        /// <returns></returns>
        public ReturnEntity<int> User_Complaints_ADD(User_ComplaintsDC iUser_ComplaintsDC)
        {
            var rtn = userDAL.User_Complaints_ADD(iUser_ComplaintsDC);

            return new ReturnEntity<int>(rtn);
        }

        #region User_RegisterSource

        /// <summary>
        /// 新增 User_RegisterSource
        /// </summary>
        /// <param name="iUser_RegisterSourceDC"></param>
        /// <returns></returns>
        public ReturnEntity<int> User_RegisterSource_ADD(User_RegisterSourceDC iUser_RegisterSourceDC)
        {
            if (string.IsNullOrWhiteSpace(iUser_RegisterSourceDC.InternalKey))
            {
                return new ReturnEntity<int>(-1, "内部编号不能为空");
            }
            if (string.IsNullOrWhiteSpace(iUser_RegisterSourceDC.SourceID))
            {
                return new ReturnEntity<int>(-1, "来源编号不能为空");
            }
            if (userDAL.User_RegisterSource_Exist_Check_InternalKey(iUser_RegisterSourceDC.InternalKey))
            {
                return new ReturnEntity<int>(-1, "内部编号已存在");
            }
            if (userDAL.User_RegisterSource_Exist_Check_SourceID(iUser_RegisterSourceDC.SourceID))
            {
                return new ReturnEntity<int>(-1, "来源编号已存在");
            }
            var rtn = userDAL.User_RegisterSource_ADD(iUser_RegisterSourceDC);

            return new ReturnEntity<int>(rtn);
        }

        /// <summary>
        /// 删除User_RegisterSource
        /// </summary>
        /// <param name="iID">主键</param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        public ReturnEntity<bool> User_RegisterSource_DELETE(int iID, int iMuser)
        {
            var rtn = userDAL.User_RegisterSource_DELETE(iID, iMuser);

            return new ReturnEntity<bool>(rtn);
        }

        /// <summary>
        /// 查询全部User_RegisterSource
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<User_RegisterSourceDC>> User_RegisterSource_SELECT_List(int? iType, int iPageIndex, int iPageSize)
        {
            var rtn = userDAL.User_RegisterSource_SELECT_List(iType, iPageIndex, iPageSize);

            return new ReturnEntity<RecordCountEntity<User_RegisterSourceDC>>(rtn);
        }

        /// <summary>
        /// 根据ID查询User_RegisterSource
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public ReturnEntity<User_RegisterSourceDC> User_RegisterSource_SELECT_Entity(int iID)
        {
            var rtn = userDAL.User_RegisterSource_SELECT_Entity(iID);

            return new ReturnEntity<User_RegisterSourceDC>(rtn);
        }

        #endregion

        /// <summary>
        /// 用户更新地理位置
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iLatitude"></param>
        /// <param name="iLongitude"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_Info_UPDATE_Location(Guid iUserID, decimal iLatitude, decimal iLongitude)
        {
            int? nodeid = null;
            var nodeRtn = Internal_GetNode_Location(iLatitude, iLongitude);
            if (nodeRtn.Success)
            {
                nodeid = nodeRtn.OBJ.ID;
            }

            var rtn = userDAL.User_Info_UPDATE_Location(iUserID, nodeid, iLatitude, iLongitude);

            return new ReturnEntity<bool>(rtn);
        }

        /// <summary>
        /// 订单刷新(修改产品内容)
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <returns></returns>
        private ReturnEntity<Exp_NodeDC> Internal_GetNode_Location(decimal iLatitude, decimal iLongitude)
        {
            try
            {
                return WCFInvokeHelper<ApiExpressClient>.Invoke<ReturnEntity<Exp_NodeDC>>(
                   c => c.Proxy.GetNode_Location(iLatitude, iLongitude));
            }
            catch (Exception ex)
            {
                return new ReturnEntity<Exp_NodeDC>(-999, ex.Message);
            }

        }
    }
}
